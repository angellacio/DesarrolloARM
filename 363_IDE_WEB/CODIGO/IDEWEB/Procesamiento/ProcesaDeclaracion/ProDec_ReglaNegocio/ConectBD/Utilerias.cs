using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using SAT.DyP.Util.Web.WebServiceConfiguracion;

namespace ProDec_ReglaNegocio.ConectBD
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
    }
}
