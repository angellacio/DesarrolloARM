//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Util.Web.Configuration:AsmxApplicationSettingsProvider:0:21/May/2008[SAT.DyP.Util.Web:1.0:21/May/2008])
using System;
using System.Collections.Generic;
using System.Text;
using SAT.DyP.Util.Configuration;
using System.Configuration;

namespace SAT.DyP.Util.Web.Configuration
{
    /// <summary>
    /// Implementación de la interfaz de configuración de DyP usando un
    /// web service como backing-store para que sea usado por las capas de la 
    /// plataforma que no tienen conectividad con el tier de datos.
    /// </summary>
    public class AsmxApplicationSettingsProvider : SAT.DyP.Util.Configuration.IApplicationSettingsProvider
    {
        public string ReadSetting(string settingName)
        {
            WebServiceConfiguracion.Configuracion service = new WebServiceConfiguracion.Configuracion();
            string result = null;

            try
            {
                result = service.ReadSetting(settingName);
            }
            catch (Exception ex)
            {
                string errorMessage = String.Format("Error al leer el valor del parámetro '{0}' a través del web service de configuración.", settingName);
                throw new SAT.DyP.Util.Types.PlatformException(errorMessage, ex);
            }

            return result;
        }
        /* 
         * FECHA:30 Julio 2012 
         * 
         */
        public string ReadSettingCfg(string settingName, string tipoDeclaracion)
        {
            
            WebServiceConfiguracionSetting.ConfiguracionSetting service = new WebServiceConfiguracionSetting.ConfiguracionSetting();
            string result = null;

            try
            {
                result = service.ReadSettingCfg(settingName, tipoDeclaracion);
            }
            catch (Exception ex)
            {
                string errorMessage = String.Format("Error al leer el valor del parámetro '{0}' a través del web service de configuración.", settingName);
                throw new SAT.DyP.Util.Types.PlatformException(errorMessage, ex);
            }

            return result;
        }


        public string ReadSetting(string settingName, string defaultValue)
        {
            throw new NotSupportedException(@"La operación ReadSetting(string settingName, string defaultValue) no está soportada cuando el configuration provider es el Servicio Web de configuración.");
        }

        public void SaveSetting(string settingName, string settingValue)
        {
            throw new NotSupportedException(@"La operación SaveSetting(string settingName, string settingValue) no está soportada cuando el configuration provider es el Servicio Web de configuración.");
        }

        public string ReadEncryptSetting(string settingName)
        {
            throw new NotSupportedException(@"La operación ReadEncryptSetting(string settingName) no está soportada cuando el configuration provider es el Servicio Web de configuración.");
        }
    }
}
