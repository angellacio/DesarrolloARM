using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using TextEnum = RecepcionEnt.TextosEnumer;
using entDyP = RecepcionEnt;
using exC = ControlMensajes.Errores;
using cManLog = ControlMensajes;

namespace RecepcionBD.DyPManejoDatosC
{
    public class DatosConfiguracion
    {
        private cManLog.ManejoLog log { get; set; }
        
        private SqlConnection sqlCon { get; set; }
        private SqlCommand sqlCom { get; set; }
        private SqlDataAdapter sqlDat { get; set; }
        
        public DatosConfiguracion(TextEnum.Enumeradores.Conexion enumConexion)
        {
            log = new cManLog.ManejoLog(ConfigurationManager.AppSettings["RegLogSours"].ToString(), 2);

            switch (enumConexion)
            {
                case TextEnum.Enumeradores.Conexion.Servicio:
                    sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["sConServicios"].ConnectionString.Trim());
                    break;
                case TextEnum.Enumeradores.Conexion.scadeNet:
                    sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["sConScadeNet"].ConnectionString.Trim());
                    break;
                default:
                    sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["sConDatos"].ConnectionString.Trim());
                    break;
            }
            
            if (sqlCon.State == ConnectionState.Closed) sqlCon.Open();

            sqlCom = new SqlCommand("", sqlCon);
        }

        public List<entDyP.DyPManejoDatosC.entCatalogoServicios> ConsultaServicios(int nServicio)
        {
            List<entDyP.DyPManejoDatosC.entCatalogoServicios> result = null;
            SqlDataReader sqlDR = null;
            try
            {
                result = new List<entDyP.DyPManejoDatosC.entCatalogoServicios>();

                sqlCom.CommandText = "AdSeNu_ConsultaServicios";
                sqlCom.CommandType = CommandType.StoredProcedure;

                sqlCom.Parameters.Add(new SqlParameter("@nServicio", SqlDbType.Int)).Value = nServicio;

                sqlDR = sqlCom.ExecuteReader();

                while (sqlDR.Read())
                {
                    result.Add(new entDyP.DyPManejoDatosC.entCatalogoServicios()
                    {
                        nServicio = sqlDR.GetInt32(sqlDR.GetOrdinal("IDServicio")),
                        sServicio = sqlDR.GetString(sqlDR.GetOrdinal("SerNombre")),
                        sServicioRuta = sqlDR.GetString(sqlDR.GetOrdinal("SerRuta")),
                        bolEstado = sqlDR.GetBoolean(sqlDR.GetOrdinal("SerEstado")),
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

        public entDyP.entCatalogo ConsultaDatosConfiguracion(string sSettingName)
        {
            entDyP.entCatalogo result = null;
            SqlDataReader sqlDR = null;
            try
            {
                result = new entDyP.entCatalogo();

                sqlCom.CommandText = "ApplicationSettings_GetSetting";
                sqlCom.CommandType = CommandType.StoredProcedure;

                sqlCom.Parameters.Add(new SqlParameter("@SettingName", SqlDbType.NVarChar, 200)).Value = sSettingName;
                sqlCom.Parameters.Add(new SqlParameter("@DefaultValue", SqlDbType.NVarChar, 200)).Value = "";

                sqlDR = sqlCom.ExecuteReader();

                while (sqlDR.Read())
                {
                    result.nId = 0;
                    result.sId = sSettingName;
                    result.sDescripcion = sqlDR[0].ToString().Trim();
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
    }
}
