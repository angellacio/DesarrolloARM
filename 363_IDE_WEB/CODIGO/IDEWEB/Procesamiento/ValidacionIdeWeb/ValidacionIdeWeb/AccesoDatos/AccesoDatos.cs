using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAT.DyP.Util.Data;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using SAT.DyP.Util.Configuration;

namespace ValidacionIdeWeb
{
    public class AccesoDatos
    {
        private static DataAccessHelper CrearIde()
        {
            return new DataAccessHelper(Constantes.Parametros.BaseDatosIde, DataProviderType.SqlServer);
        }   

        public static Declaracion BuscarDeclaracionPorFolioEstatus(int folio, int estatus)
        {
            Declaracion declaracion = null;
            string sql = "BuscarDeclaracionPorFolioEstatus";
            DataAccessHelper acceso = null;
            IDataParameter[] parametros = new IDataParameter[2];

            parametros[0] = new SqlParameter("@folio", folio);
            parametros[1] = new SqlParameter("@idEstatusRecepcion", estatus);

            try
            {
                acceso = CrearIde();

                using (IDataReader reader = acceso.ExecuteReaderStoreProcedure(sql, parametros))
                {
                    if (reader.Read())
                    {
                        declaracion = new Declaracion();

                        declaracion.Folio = reader.GetInt32(0);
                        declaracion.IdMedioRecepcion = reader.GetInt16(1);
                        declaracion.IdEstatusRecepcion = reader.GetInt16(2);
                        declaracion.IdEntidadReceptora = reader.GetInt32(3);
                        declaracion.RfcAutenticacion = reader.GetString(4);
                        declaracion.RfcContribuyente = reader.GetString(5);
                        declaracion.DireccionIP = reader.GetString(6);
                        declaracion.NombreArchivo = reader.GetString(7);
                        declaracion.TamañoArchivo = reader.GetInt32(8);
                        declaracion.Formato = reader.GetString(9);
                        declaracion.Materia = reader.GetInt32(10);
                        declaracion.RecepcionNotificado = reader.GetBoolean(11);
                        declaracion.ProcesamientoNotificado = reader.GetBoolean(12);
                        declaracion.FechaRecepcion = reader.GetDateTime(13);
                        declaracion.FechaModificacion = reader.GetDateTime(14);
                        declaracion.EsNormal = reader.GetBoolean(15);
                        declaracion.EsAnual = reader.GetBoolean(16);
                        declaracion.ArchivoFisico = reader.GetString(22);

                        declaracion.CadenaOriginal = new CadenaOriginal() { EsAnual = reader.GetBoolean(16), EsNormal = reader.GetBoolean(15) };
                    }
                }
            }
            catch(Exception e) 
            {
                AccesoDatos.RegistrarEvento(folio, e.Message);
                
            }
            finally
            {
                if (acceso != null)
                    acceso.CloseConnection();
            }
            return declaracion;
        }

        public static bool ActualizarEstatusDeclaracion(int folio, int estatus)
        {
            string sql = "ActualizarEstatusDeclaracion";
            DataAccessHelper acceso = null;
            IDataParameter[] parametros = new IDataParameter[3];

            parametros[0] = new SqlParameter("@folio", folio);
            parametros[1] = new SqlParameter("@idEstatusRecepcion", estatus);
            parametros[2] = new SqlParameter("@fechaModificacion ", DateTime.Now);


            acceso = CrearIde();
            try
            {
                acceso.ExecuteReaderStoreProcedure(sql, parametros);
            }
            catch (Exception e)
            {
                AccesoDatos.RegistrarEvento(folio, e.Message);
                return false;
            }
            finally
            {
                if(acceso!=null)
                    acceso.CloseConnection();
            }
            return true;
        }

        public static Mensaje ObtenerMensajeError(int idMensaje, int folio)
        {
            Mensaje mensaje = null;
            string sql = "ObtenerMensajeError";
            DataAccessHelper acceso = null;
            IDataParameter[] parametros = new IDataParameter[1];

            parametros[0] = new SqlParameter("@idMensaje", idMensaje);

            try
            {
                acceso = CrearIde();

                using (IDataReader reader = acceso.ExecuteReaderStoreProcedure(sql, parametros))
                {
                    if (reader.Read())
                    {
                        mensaje = new Mensaje();

                        mensaje.IdTipoMensaje = reader.GetInt32(0);
                        mensaje.IdMensaje = reader.GetInt32(1);
                        mensaje.Descripcion = reader.GetString(2);                       
                    }
                }
            }
            catch(Exception e)
            {
                AccesoDatos.RegistrarEvento(folio,e.Message);
            }
            finally
            {
                if (acceso != null)
                    acceso.CloseConnection();
            }
            return mensaje;
        }

        public static void GuardarAceptacion(Declaracion declaracion)
        {
            if (declaracion != null)
            {
                string sql = "GuardarAceptacion";
                DataAccessHelper acceso = null;
                IDataParameter[] parametros = new IDataParameter[11];

                parametros[0] = new SqlParameter("@folio", declaracion.Folio);
                parametros[1] = new SqlParameter("@numeroOperacion", declaracion.NumeroOperacion);
                parametros[2] = new SqlParameter("@relojFranqueador", "0");
                parametros[3] = new SqlParameter("@ejercicio", declaracion.CadenaOriginal.Ejercicio);
                parametros[4] = new SqlParameter("@cadenaOriginal", declaracion.CadenaOriginal.ToString());
                parametros[5] = new SqlParameter("@selloDigital", declaracion.SelloDigital);
                parametros[6] = new SqlParameter("@numeroSerie", declaracion.NumeroSerie);
                parametros[7] = new SqlParameter("@periodo", declaracion.CadenaOriginal.Periodo);
                parametros[8] = new SqlParameter("@materia", declaracion.Materia);
                parametros[9] = new SqlParameter("@totalRecaudado", declaracion.CadenaOriginal.ImporteRecaudadoDepositos);
                parametros[10] = new SqlParameter("@totalEnterado", declaracion.CadenaOriginal.ImporteEnterado);

                acceso = CrearIde();
                try
                {
                    acceso.ExecuteReaderStoreProcedure(sql, parametros);
                }
                catch (Exception e)
                {
                    AccesoDatos.RegistrarEvento(declaracion.Folio, e.Message);
                }
                finally
                {
                    if (acceso != null)
                        acceso.CloseConnection();
                } 
            }
        }

        private static void GuardarRechazo(Declaracion declaracion, String motivoRechazo)
        {
            if (declaracion != null && motivoRechazo != null)
            {
                string sql = "GuardarRechazo";
                DataAccessHelper acceso = null;
                IDataParameter[] parametros = new IDataParameter[7];
                int consecutivo = ObtenerConsecutivoRechazo(declaracion.Folio);

                parametros[0] = new SqlParameter("@folio", declaracion.Folio);
                parametros[1] = new SqlParameter("@concecutivo", consecutivo+1);
                parametros[2] = new SqlParameter("@materia", declaracion.Materia);
                parametros[3] = new SqlParameter("@anexoControl", 1);
                parametros[4] = new SqlParameter("@anexoConsecutivo", 1);
                parametros[5] = new SqlParameter("@claves", "0");
                parametros[6] = new SqlParameter("@motivoRechazo", motivoRechazo);  
              
                acceso = CrearIde();
                try
                {
                    acceso.ExecuteReaderStoreProcedure(sql, parametros);
                }
                catch (Exception e)
                {
                    AccesoDatos.RegistrarEvento(declaracion.Folio, e.Message);
                }
                finally
                {
                    if (acceso != null)
                        acceso.CloseConnection();
                }           
            }
        }

        private static int ObtenerConsecutivoRechazo(int folio)
        {
            string sql = "ObtenerConsecutivoRechazo";
            DataAccessHelper acceso = null;
            IDataParameter[] parametros = new IDataParameter[1];

            parametros[0] = new SqlParameter("@folio", folio);

            int intFolio = 0;
            try
            {
                acceso = CrearIde();

                using (IDataReader reader = acceso.ExecuteReaderStoreProcedure(sql, parametros))
                {
                    if (reader.Read())
                    {
                        intFolio = reader.GetInt32(1);
                        
                    }
                }
            }
            catch (Exception e)
            {
                AccesoDatos.RegistrarEvento(folio, e.Message); 
            }
            finally
            {
                if (acceso != null)
                    acceso.CloseConnection();
            }
           
            return intFolio;
        }

        public static void GuardarMotivosRechazo(Declaracion declaracion, List<String> motivosRechazo)
        {
            if (declaracion != null && motivosRechazo != null && motivosRechazo.Count > 0)
            {
                for (int i = 0; i < motivosRechazo.Count; i++)
                {
                    GuardarRechazo(declaracion, motivosRechazo[i]);
                }
            }
        }

        public static bool GuardarDeclaracion(Declaracion declaracion, string pathDeclaracion)
        {
            string sql = "GuardarDeclaracion";
            DataAccessHelper acceso = null;
            IDataParameter[] parametros = new IDataParameter[2];

            parametros[0] = new SqlParameter("@folio", declaracion.Folio);
            parametros[1] = new SqlParameter("@contenido", File.ReadAllBytes(pathDeclaracion));

            acceso = CrearIde();
            try
            {
                acceso.ExecuteReaderStoreProcedure(sql, parametros);
            }
            catch (Exception e)
            {
                AccesoDatos.RegistrarEvento(declaracion.Folio, e.Message);
                return false;
            }
            finally
            {
                if (acceso != null)
                    acceso.CloseConnection();
            }

            return true;
        }

        public static void RegistrarBitacora(int folio, int estatus)
        {
            string sql = "RegistrarBitacora";
            DataAccessHelper acceso = null;
            IDataParameter[] parametros = new IDataParameter[3];

            parametros[0] = new SqlParameter("@folio", folio);
            parametros[1] = new SqlParameter("@idEstatusRecepcion", estatus);
            parametros[2] = new SqlParameter("@fechaModificacion", DateTime.Now);

            acceso = CrearIde();
            try
            {
                acceso.ExecuteReaderStoreProcedure(sql, parametros);
            }
            catch (Exception e)
            {
                AccesoDatos.RegistrarEvento(folio,e.Message);
            }
            finally
            {
                if (acceso != null)
                    acceso.CloseConnection();
            }
        }

        private static int ObtenerConsecutivoEventos(int folio)
        {
            string sql = "ObtenerConsecutivoEventos";
            DataAccessHelper acceso = null;
            IDataParameter[] parametros = new IDataParameter[1];

            parametros[0] = new SqlParameter("@folio", folio);

            int intFolio = 0;
            try
            {
                acceso = CrearIde();

                using (IDataReader reader = acceso.ExecuteReaderStoreProcedure(sql, parametros))
                {
                    if (reader.Read())
                    {
                        intFolio = reader.GetInt32(1);

                    }
                }
            }
            catch (Exception e)
            {
                AccesoDatos.RegistrarEvento(folio, e.Message);
            }
            finally
            {
                if (acceso != null)
                    acceso.CloseConnection();
            }

            return intFolio;
        }

        public static void RegistrarEvento(int folio, string mensaje )
        {
            SAT.DyP.Util.Logging.EventLogHelper.WriteErrorEntry(mensaje, 251);
            string sql = "RegistrarEvento";
            DataAccessHelper acceso = null;
            IDataParameter[] parametros = new IDataParameter[4];

            parametros[0] = new SqlParameter("@folio", folio);
            parametros[1] = new SqlParameter("@concecutivo", ObtenerConsecutivoEventos(folio) + 1);
            parametros[2] = new SqlParameter("@fechaModificacion", DateTime.Now);
            parametros[3] = new SqlParameter("@mensaje", mensaje);

            acceso = CrearIde();
            try
            {
                acceso.ExecuteReaderStoreProcedure(sql, parametros);
            }
            catch (Exception e)
            {
                SAT.DyP.Util.Logging.EventLogHelper.WriteErrorEntry(e, 251);
            }
            finally
            {
                if (acceso != null)
                    acceso.CloseConnection();
            }
        }

        public static void NotificarProcesamiento(int folio)
        {
            string sql = "NotificarProcesamiento";
            DataAccessHelper acceso = null;
            IDataParameter[] parametros = new IDataParameter[2];

            parametros[0] = new SqlParameter("@folio", folio);
            parametros[1] = new SqlParameter("@fechaModificacion ", DateTime.Now);

            try
            {
                acceso = CrearIde();

                acceso.ExecuteReaderStoreProcedure(sql, parametros);
            }
            catch (Exception e)
            {
                AccesoDatos.RegistrarEvento(folio, e.Message);
            }
            finally
            {
                if (acceso != null)
                    acceso.CloseConnection();
            }
        }

        public static void ComplementarDatosRecepcion(Declaracion declaracion, int tipoArchivo)
        {
            string sql = "ActualizarDatosRecepcion";
            DataAccessHelper acceso = null;
            IDataParameter[] parametros = new IDataParameter[6];

            parametros[0] = new SqlParameter("@folio", declaracion.Folio);
            parametros[1] = new SqlParameter("@periodo", declaracion.CadenaOriginal.Periodo);
            parametros[2] = new SqlParameter("@ejercicio", declaracion.CadenaOriginal.Ejercicio);
            parametros[3] = new SqlParameter("@operaciones", declaracion.CadenaOriginal.OperacionesRelacionadas);
            parametros[4] = new SqlParameter("@idTipoArchivo", tipoArchivo);
            parametros[5] = new SqlParameter("@fechaModificacion", DateTime.Now);
            
            acceso = CrearIde();
            try
            {
                acceso.ExecuteReaderStoreProcedure(sql, parametros);
            }
            catch (Exception e)
            {
                AccesoDatos.RegistrarEvento(declaracion.Folio, e.Message);
            }
            finally
            {
                if (acceso != null)
                    acceso.CloseConnection();
            }
        }

        public static string ObtenerMensaje(int tipoMensaje,int idMensaje, int folio)
        {
            string descripcion = "";
            string sql = "ObtenerMensaje";
            DataAccessHelper acceso = null;
            IDataParameter[] parametros = new IDataParameter[2];

            parametros[0] = new SqlParameter("@idTipoMensaje", tipoMensaje);
            parametros[1] = new SqlParameter("@idMensaje", idMensaje);

           
            try
            {
                acceso = CrearIde();

                using (IDataReader reader = acceso.ExecuteReaderStoreProcedure(sql, parametros))
                {
                    if (reader.Read())
                    {
                        descripcion = reader.GetString(2);

                    }
                }
            }
            catch (Exception e)
            {
                AccesoDatos.RegistrarEvento(folio, e.Message);
            }
            finally
            {
                if (acceso != null)
                    acceso.CloseConnection();
            }

            return descripcion;
        }

        public static string getValorString(string settingName)
        {
           return SAT.DyP.Util.Configuration.ConfigurationManager.ApplicationSettings.ReadSetting(settingName); 
        }

        public static int getValorInt(string settingName)
        {
            return Convert.ToInt32(SAT.DyP.Util.Configuration.ConfigurationManager.ApplicationSettings.ReadSetting(settingName));
        }

        public static long getValorLong(string settingName)
        {
            return Convert.ToInt64(SAT.DyP.Util.Configuration.ConfigurationManager.ApplicationSettings.ReadSetting(settingName));
        }
    }
}