using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using EntApp = RecepcionEnt.DyPDeclaraciones;
using exC = ControlMensajes.Errores;
using cManLog = ControlMensajes;

namespace RecepcionBD
{
    public class ManejoBD
    {
        private cManLog.ManejoLog log { get; set; }
        
        private SqlConnection sqlCon { get; set; }
        private SqlCommand sqlCom { get; set; }
        private SqlDataAdapter sqlDat { get; set; }

        public ManejoBD()
        {
            log = new cManLog.ManejoLog(ConfigurationManager.AppSettings["RegLogSours"].ToString(), 2);

            sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["sConDatos"].ConnectionString.Trim());
            if (sqlCon.State == ConnectionState.Closed) sqlCon.Open();

            sqlCom = new SqlCommand("", sqlCon);
        }

        public void FinalizaComponentes()
        {
            if (sqlDat != null) sqlDat.Dispose();
            if (sqlCom != null) sqlCom.Dispose();
            if (sqlCon != null)
            {
                sqlCon.Close();
                sqlCon.Dispose();
            }
        }

        public List<EntApp.entDeclaracion> ConsultaDeclaraciones(string sEquipo, int nIntentos)
        {
            List<EntApp.entDeclaracion> result = null;
            SqlDataReader sqlDR = null;
            try
            {
                result = new List<EntApp.entDeclaracion>();

                sqlCom.CommandText = "ProNue_ConsultaSolicitudes";
                sqlCom.CommandType = CommandType.StoredProcedure;

                sqlCom.Parameters.Add(new SqlParameter("@nTipCon", SqlDbType.Int)).Value = 1;
                sqlCom.Parameters.Add(new SqlParameter("@sEquipo", SqlDbType.NVarChar, 150)).Value = sEquipo;
                sqlCom.Parameters.Add(new SqlParameter("@numIntentos", SqlDbType.Int)).Value = nIntentos;

                sqlDR = sqlCom.ExecuteReader();

                while (sqlDR.Read())
                {
                    result.Add(new EntApp.entDeclaracion()
                    {
                        nIdProceso = sqlDR.GetInt32(sqlDR.GetOrdinal("IDProceso")),
                        sNombreArchivo = sqlDR.GetString(sqlDR.GetOrdinal("NombreArchivo")),
                        nMedio = sqlDR.GetString(sqlDR.GetOrdinal("IDMedio")),
                        sMedio = sqlDR.GetString(sqlDR.GetOrdinal("Medio")),
                        nEntRec = sqlDR.GetInt32(sqlDR.GetOrdinal("EntReceptora")),
                        dPresentacion = sqlDR.GetDateTime(sqlDR.GetOrdinal("FechaPresentacion")),
                        sRFCAut = sqlDR.GetString(sqlDR.GetOrdinal("RFCAutenticacion")),
                        sRFC = sqlDR.GetString(sqlDR.GetOrdinal("RFC")),
                        nMateria = sqlDR.GetInt16(sqlDR.GetOrdinal("d_materia")),
                        nMensaje = sqlDR.GetInt16(sqlDR.GetOrdinal("IDMensaje")),
                        sMensaje = sqlDR.GetString(sqlDR.GetOrdinal("Mensaje"))
                    });
                }
            }
            catch (SqlException ex)
            {
                log.MensajeError(ex.Message);
                throw new exC.exSalidaNext("", ex);
            }
            catch (ApplicationException ex)
            {
                log.MensajeAuditoria(ex.Message);
                throw new exC.exSalidaNext("", ex);
            }
            catch (Exception ex)
            {
                log.MensajeError(ex.Message);
                throw new exC.exSalidaAll("", ex);
            }
            finally
            {
                if (sqlDR != null)
                {
                    sqlDR.Close();
                    sqlDR = null;
                }
            }
            return result;
        }

        public List<EntApp.entDeclaracion> ActualizaDeclaracionFile(int nProceso, int nFileSize, int nFileSizeMax, byte[] bFile)
        {
            List<EntApp.entDeclaracion> result = null;
            try
            {
                result = new List<EntApp.entDeclaracion>();

                sqlCom.CommandText = "ProNue_AltaArchivo";
                sqlCom.CommandType = CommandType.StoredProcedure;
                
                sqlCom.Parameters.Add(new SqlParameter("@nIdProceso", SqlDbType.Int)).Value = nProceso;
                sqlCom.Parameters.Add(new SqlParameter("@nFileSize", SqlDbType.Int)).Value = nFileSize;
                sqlCom.Parameters.Add(new SqlParameter("@nFileSizeMax", SqlDbType.Int)).Value = nFileSizeMax;
                sqlCom.Parameters.Add(new SqlParameter("@iDeclaracion", SqlDbType.Image)).Value = bFile;

                sqlCom.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                log.MensajeError(ex.Message);
                throw new exC.exSalidaNext("", ex);
            }

            catch (ApplicationException ex)
            {
                log.MensajeAuditoria(ex.Message);
                throw new exC.exSalidaNext("", ex);
            }
            catch (Exception ex)
            {
                log.MensajeError(ex.Message);
                throw new exC.exSalidaAll("", ex);
            }
            finally
            { }
            return result;
        }
    }
}
