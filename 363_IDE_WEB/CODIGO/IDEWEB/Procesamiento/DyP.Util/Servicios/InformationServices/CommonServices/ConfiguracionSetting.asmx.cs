
//@(#)SCADE2(W:SKDN08221CO2:SAT.DyP.Util.Services:Configuracion:0:21/Mayo/2008[SAT.DyP.Util.Services:1.0:21/Mayo/2008])
	
using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using SAT.DyP.Util.Logging;
using SAT.DyP.Util.Types;

namespace SAT.DyP.Util.Services
{
    /// <summary>
    /// Summary description for Configuracion
    /// </summary>
    [WebService(Namespace = "http://www.sat.gob.mx/scade.net/services/2007_10_07/comun/configuracion")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class ConfiguracionSetting : System.Web.Services.WebService
    {

        /*
         * Fecha de Modificacion : 27 Julio 2012
         * 
         */
        /// <summary>
        /// Se obtiene el valor de la configuracion de acuerdo al tipo de forma
        /// <summary>
        /// <param name="settingName">El nombre del parámetro de configuración.</param>
        /// <param name="tipoDeclaracion">Tipo de declaración que se desea obtener la configuración.</param>
        /// <returns> El valor actual del parámetro de configuración.<returns>
        
        [WebMethod(MessageName = "ReadSettingCfg")]
        public string ReadSettingCfg(string settingName, string tpDeclaracion)
        {
            try
            {
                return SAT.DyP.Util.Configuration.ConfigurationManager.ApplicationSettings.ReadSettingCfg(settingName, tpDeclaracion);
                    
            }
            catch (Exception ex)
            {
                string errorMessage = String.Format(
                    "Error al leer el valor del parámetro de configuración '{0}': {1}.",
                    settingName,
                    ex);
                EventLogHelper.WriteErrorEntry(errorMessage, CoreLogEventIdentifier.CONFIG_EVENT_ID);
                throw new PlatformException("Servicio de configuración no disponible.", ex);
            }
        }



    }
}
