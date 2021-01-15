
//@(#)SCADE2(W:SKDN08221CO2:SAT.DyP.Util.Service:Service:0:21/Mayo/2008[SAT.DyP.Util.Service:1.0:21/Mayo/2008])
	
using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.IO;
using System.Diagnostics;
using SAT.DyP.Util.Service.WCF;
using SAT.DyP.Util.Data;
using SAT.DyP.Util.Configuration;
using System.Data.SqlClient;
using System.Data.Common;

namespace SAT.DyP.Util.Service
{
    
    [ServiceBehavior(Namespace = "http://www.sat.gob.mx/scade.net/2008/BigFilesService", ConfigurationName = "SAT.DyP.Util.Service.BigFilesService")]
    public class BigFilesService : IBigFilesService
    {
        private object _lockSafeTrace = new object();

        private TransferRequest request = null;
        private TransferResponse response = null;

        #region IBigFiles Members
        public void UploadData(UploadContent data)
        {
            Trace.WriteLine(string.Format("Se intenta bajar archivo: {0}, hora inicio: {1}", data.SendToFile, DateTime.Now.ToString("hh:mm:ss")));

            int chunkSize = 2048;
            byte[] buffer = new byte[chunkSize];

            try
            {
                using (FileStream writeStream = new FileStream(@data.SendToFile, FileMode.Create, FileAccess.Write))
                {
                    do
                    {
                        // read bytes from input stream
                        int bytesRead = data.Content.Read(buffer, 0, chunkSize);
                        if (bytesRead == 0) break;

                        // write bytes to output stream
                        writeStream.Write(buffer, 0, bytesRead);
                    } while (true);

                    writeStream.Close();
                }
            }
            catch (Exception exception)
            {
                Trace.TraceError(exception.Message);
                Log("WCF Upload file error:" + exception.Message);
            }


            Trace.WriteLine(string.Format("Proceso terminado para archivo: {0}, hora fin: {1}", data.SendToFile, DateTime.Now.ToString("hh:mm:ss")));
        }

        public TransferInfo GetTransferInfo(TransferInfo request)
        {
            TransferInfo findData = new TransferInfo();
            findData.Exists = false;
                        
            lock (_lockSafeTrace)
            {
                Trace.WriteLine(string.Format("Se solicita información del archivo:{0}", request.FileName));

                //se obtiene la información del archivo para verificar el estado
                FileInfo fileInfo = new FileInfo(request.FileName);
                if (fileInfo.Exists)
                {
                    findData.Exists = fileInfo.Exists;
                    findData.Status = TransferStatus.Downloaded;
                    findData.Id = Guid.NewGuid().ToString();
                    findData.TotalSize = (int)fileInfo.Length;
                    findData.FileName = Path.GetFileName(request.FileName);

                    Trace.WriteLine(string.Format("Existe información del archivo:{0}", request.FileName));

                }
                else
                {
                    Trace.WriteLine(string.Format("No existe el archivo:{0}", request.FileName));
                }
            }

            return findData;
        }


        public TransferResponse Transfer(TransferRequest request)
        {
            this.request = request;
            FileStream file = null;
            int folio = 0;

            try
            {          
                if ((folio = ExistsFileInDB()) > 0)
                {
                    response = new TransferResponse();
                    response.Status = TransferStatus.Uploaded;
                    response.Message = String.Format(request.Message, folio);
                }
                else
                {
                    if ((file = CreateFile()) != null)
                    {
                        WriteFile(file);

                        response = new TransferResponse();
                        response.Status = TransferStatus.Downloaded;
                    }
                    else
                    {
                        response = new TransferResponse();
                        response.Status = TransferStatus.InProgress;
                        response.Message = "Ya existe un envío en progreso de este archivo. Si el archivo estaba correcto no lo vuelva a enviar y espere su acuse; en caso contrario genérelo nuevamente y envíelo.";
                    }
                }
            }
            catch (Exception exception)
            {
                response = new TransferResponse();
                response.Status = TransferStatus.Unkwond;
                response.Message = exception.Message;
            }

            return response;
        }

        public void UndoTransfer(string fileName)
        {
            this.DeleteFile(fileName);
        }
       
        #endregion


        /// <summary>
        /// Verifica si existe el archivo en la base de datos del aplicativo.
        /// </summary>
        /// <returns>Si el archivo existe devuelve el número de folio, de lo contrario regresa cero.</returns>
        private int ExistsFileInDB()
        {
            DataAccessHelper helper = new DataAccessHelper(GetConnection(request.DatabaseKeyConfig));
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter(request.StoredProcedureParameterName, Path.GetFileName(request.FileName));

            return Convert.ToInt32(helper.ExecuteStoreProcedure(request.StoredProcedureName, parameters));
        }

        private DbConnection GetConnection(string databaseKeyConfig)
        {
            DbConnection connection = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateConnection();
            connection.ConnectionString = String.Format(ConfigurationManager.ApplicationSettings.ReadSetting(databaseKeyConfig), DateTime.Now.Year);

            return connection;
        }

        private FileStream CreateFile()
        {
            FileStream stream = null;

            try
            {
                if (!File.Exists(request.FileName))
                {
                    stream = new FileStream(request.FileName, FileMode.CreateNew, FileAccess.Write, FileShare.None);
                }
            }
            catch(Exception exception)
            {
                Log(String.Format("Ocurrio un error al intentar crear el archivo '{0}', el mensaje de error es: {1}", request.FileName, exception.Message));
            }

            return stream;
        }

        private void WriteFile(FileStream file)
        {
            try
            {
                int bytesRead = 0;
                int chunkSize = 2048;
                byte[] buffer = new byte[chunkSize];

                while ((bytesRead = request.FileContent.Read(buffer, 0, chunkSize)) > 0)
                {
                    file.Write(buffer, 0, bytesRead);
                }
            }
            catch(Exception exception)
            {
                Log(String.Format("Ocurrio un error al intentar escribir en el archivo '{0}', el mensaje de error es: {1}", request.FileName, exception.Message));

                if (file != null)
                {
                    file.Close();
                    file = null;
                }

                DeleteFile(this.request.FileName);
            }
            finally
            {
                if (file != null)
                {
                    file.Close();
                    file = null;
                }
            }
        }

        private void DeleteFile(string fileName)
        {
            try
            {
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
            }
            catch (Exception exception)
            {
                Log(String.Format("Error al borrar el archivo '{0}, el mensaje de error es: {1}'", request.FileName, exception.Message));
            }
        }


        private void Log(string message)
        {
            try
            {
                EventLog.WriteEntry("Scade.Net.BigFiles", message, EventLogEntryType.Information);
            }
            catch
            {
            }
        }
    }   
}
