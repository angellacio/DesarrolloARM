
//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Negocio.Comun.Procesos:SelloDigital:0:21/Mayo/2008[SAT.DyP.Negocio.Comun.Procesos:1.1:21/Mayo/2008])
	
using System;
using System.Collections.Generic;
using System.Text;
using SAT.DyP.Util.Configuration;
using SAT.DyP.Util.Security;
using SAT.DyP.Util.ExceptionHandling;
using SAT.DyP.Negocio.Comun.Procesos.SgiHelper;

namespace SAT.DyP.Negocio.Comun.Procesos.Seguridad
{
    /// <summary>
    /// Componente que contiene las operaciones que aplican al sello digital
    /// </summary>
    public class SelloDigital
    {
        /// <summary>
        /// Devuelve el número de serie dle certificado organizacional
        /// </summary>
        /// <returns></returns>
        public static string NumeroSerieCertificadoOrganizacional()
        {
            return ConfigurationManager.ApplicationSettings.ReadSetting(Constantes.CONFIG_SIGN_CERTIFICATE_SERIAL_NUMBER);
        }

        /// <summary>
        /// Genera una firma digital
        /// </summary>
        /// <param name="cadenaOriginal">cadena original</param>
        /// <returns></returns>
        public static string ObtenerFirma(string cadenaOriginal)
        {
            string cadenaFirmada = string.Empty;
            
            //TODO:Invocar WS SgiHelper
            SgiManager.Provider = SgiFactory.CreateProvider();
            try
            {
                cadenaFirmada = SgiManager.Provider.GenerarSelloDigital(cadenaOriginal);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
            finally
            {
                SgiManager.Provider.Dispose();
            }

            return cadenaFirmada;
        }
        
    }
}
