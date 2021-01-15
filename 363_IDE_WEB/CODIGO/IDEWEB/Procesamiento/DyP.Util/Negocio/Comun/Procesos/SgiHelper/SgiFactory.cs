
//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Negocio.Comun.Procesos:SgiFactory:0:21/Mayo/2008[SAT.DyP.Negocio.Comun.Procesos:1.1:21/Mayo/2008])
	
using System;
using System.Collections.Generic;
using System.Text;
using SAT.DyP.Util.Security;
using SAT.DyP.Util.Configuration;

namespace SAT.DyP.Negocio.Comun.Procesos.SgiHelper
{
    /// <summary>
    /// Clase facory que devuelve el proveedor configurado de interacción
    /// con SgiHelpers (ARA)
    /// </summary>
    public class SgiFactory
    {
        private static string KEY_CONFIG = "SAT.DyP.Negocio.Comun.Seguridad::USE_SGI_HELPER_INPROC";

        /// <summary>
        /// Devuelve un proveedor de SgiHelpers
        /// </summary>
        /// <returns></returns>
        public static ISgiHelperProvider CreateProvider()
        {
            ISgiHelperProvider provider = null;
           
            //Se revisa el modo configurado
            if (IsInProcessMode)
                provider= new NetSgiHelper();
            else
                provider= new WSSgiHelperProxy();

            return provider;
        }

        /// <summary>
        /// Revisa en modo In-Proc para el uso de SgiHelpers.
        /// </summary>
        public static bool IsInProcessMode
        {
            get
            {
                string configured = ConfigurationManager.ApplicationSettings.ReadSetting(KEY_CONFIG);
                if (configured.Equals("1"))
                    return true;
                else
                    return false;
            }
        }

    }
}
