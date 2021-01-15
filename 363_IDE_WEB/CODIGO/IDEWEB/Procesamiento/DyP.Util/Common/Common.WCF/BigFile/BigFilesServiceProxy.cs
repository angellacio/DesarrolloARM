//@(#)SCADE2(W:SKD08212CO2:SAT.DyP.Util.Service.WCF:BigFilesServiceProxy:0:21/May/2008[SAT.DyP.Util.Service.WCF:1.0:21/May/2008])
using System;
using System.Data;
using System.Configuration;
using System.IO;
using SAT.DyP.Util.Service.WCF.DynamicClientProxy;

namespace SAT.DyP.Util.Service.WCF
{
    /// <summary>
    /// Clase proxy que simplifica el acceso al servicio WCF de archivos grandes
    /// </summary>   
    public class BigFilesServiceProxy:IDisposable
    {
        private IBigFilesService _proxyContract;

        public BigFilesServiceProxy()
        {
            _proxyContract = WCFClientProxy<IBigFilesService>.GetInstance("BasicHttpBinding_BigFilesService");
        }

        /// <summary>
        /// Realiza el upload de un archivo a un destino especifico
        /// </summary>
        /// <param name="destination">Destino del archivo</param>
        /// <param name="content">Contenido (bytes)</param>
        public void Send(string destination, Stream content)
        {
            
            UploadContent data = new UploadContent();
            data.SendToFile = destination;
            data.Content=content;           

            _proxyContract.UploadData(data);
        }

        /// <summary>
        /// Realiza el upload de un archivo a un destino especifico
        /// </summary>
        /// <param name="destination">Destino del archivo</param>
        /// <param name="content">Contenido (bytes)</param>
        public TransferResponse Send(Stream fileContent, string fileName, string databaseKeyConfig, string storedProcedureName, string storedProcedureParameterName, string message)
        {
            TransferRequest request = new TransferRequest();

            request.FileContent = fileContent;
            request.FileName = fileName;
            request.DatabaseKeyConfig = databaseKeyConfig;
            request.StoredProcedureName = storedProcedureName;
            request.StoredProcedureParameterName = storedProcedureParameterName;
            request.Message = message;

            return _proxyContract.Transfer(request);
        }

        public void UndoSend(string fileName)
        {
            _proxyContract.UndoTransfer(fileName);
        }

        /// <summary>
        /// Devuelve la información de transferencia de un archivo solicitado
        /// </summary>
        /// <param name="fileName">Archivo</param>
        /// <returns></returns>
        public TransferInfo GetTransferInfo(string fileName)
        {
            TransferInfo request = new TransferInfo();
            request.FileName = fileName;

            TransferInfo result = _proxyContract.GetTransferInfo(request);

            return result;
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (_proxyContract != null)
            {
                _proxyContract = null;
            }
        }

        #endregion
    }
}
