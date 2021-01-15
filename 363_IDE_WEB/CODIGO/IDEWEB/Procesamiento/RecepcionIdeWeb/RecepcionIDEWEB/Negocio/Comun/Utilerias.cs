using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SAT.DyP.Util.Web.WebServiceConfiguracion;
using System.Text;
using System.Diagnostics;

namespace RecepcionIDEWEB.Negocio.Comun
{
    public static class Utilerias
    {
        public static string ConsultarArchivoConfiguracion(string parametro)
        {
            if (String.IsNullOrEmpty(parametro))
            {
                throw new ArgumentNullException("parametro", "El argumento 'parametro' es nulo o vacio.");
            }

            string valor = ConfigurationManager.AppSettings[parametro];

            if (String.IsNullOrEmpty(valor))
            {
                throw new Exception(String.Format(Mensajes.ParametroArchivoConfiguracionRequerido, parametro));
            }

            return valor;
        }

        public static string ConsultarBDConfiguracion(string parametro)
        {
            if (String.IsNullOrEmpty(parametro))
            {
                throw new ArgumentNullException("parametro", "El argumento 'parametro' es nulo o vacio.");
            }

            string url = Parametros.UrlServicioConfiguracion;
            Configuracion servicioConfiguracion = null;
            string valor = null;

            try
            {
                servicioConfiguracion = new Configuracion();
                servicioConfiguracion.Url = url;

                valor = servicioConfiguracion.ReadSetting(parametro);
            }
            catch (Exception excepcion)
            {
                throw new Exception(Mensajes.ServicioConfiguracionNoDisponible, excepcion);
            }
            finally
            {
                if (servicioConfiguracion != null)
                {
                    servicioConfiguracion.Dispose();
                    servicioConfiguracion = null;
                }
            }

            if (String.IsNullOrEmpty(valor))
            {
                throw new Exception(String.Format(Mensajes.ParametroBDConfiguracionRequerido, parametro));
            }

            return valor;
        }

        public static void RegistrarLogEventosAuditoria(string mensaje)
        {
            try
            {
                EventLog.WriteEntry(Parametros.OrigenEvento, mensaje, EventLogEntryType.SuccessAudit, Parametros.IdEvento);
            }
            catch
            {
                //Sino se puede escribir en el visor de eventos, 
                //no se hace nada con la excepcion.
            }
        }

        public static void RegistrarLogEventos(string mensaje)
        {
            try
            {
                EventLog.WriteEntry(Parametros.OrigenEvento, mensaje, EventLogEntryType.Error, Parametros.IdEvento);
            }
            catch
            {
                //Sino se puede escribir en el visor de eventos, 
                //no se hace nada con la excepcion.
            }
        }

        public static void RegistrarLogEventos(string mensaje, Exception excepcion)
        {
            RegistrarLogEventos(GenerarMensaje(mensaje, excepcion));
        }

        public static void RegistrarLogEventos(Exception excepcion)
        {
            RegistrarLogEventos(null, excepcion);
        }

        public static string GenerarMensaje(string mensaje, Exception excepcion)
        {
            StringBuilder error = new StringBuilder();
            error.AppendLine(String.Format("Origen\t: {0}", excepcion.Source));
            error.AppendLine(String.Format("Clase\t: {0}", excepcion.TargetSite.DeclaringType.FullName));
            error.AppendLine(String.Format("Método\t: {0}", excepcion.TargetSite.Name));

            if (!String.IsNullOrEmpty(mensaje))
            {
                error.AppendLine(String.Format("Mensaje\t: {0}", mensaje));
            }

            error.AppendLine(String.Format("{0}{1}", Environment.NewLine, excepcion.ToString()));

            return error.ToString();
        }

        public static string GenerarMensaje(Exception excepcion)
        {
            return GenerarMensaje(null, excepcion);
        }
    }
}
