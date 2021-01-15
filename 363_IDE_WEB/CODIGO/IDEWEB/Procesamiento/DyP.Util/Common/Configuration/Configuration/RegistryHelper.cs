//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Util:RegistryHelper:0:21/May/2008[SAT.DyP.Util:1.0:21/May/2008])
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace SAT.DyP.Util.Configuration
{
    internal class RegistryHelper
    {
        private const string PRODUCT_REGISTRY_KEY = @"SOFTWARE\SAT\DyP\1.0\";

        /// <summary>
        /// Lee un setting del registry del registry key del producto.
        /// </summary>
        /// <param name="value">El nombre del valor a leer. Debe ser un valor de tipo REG_SZ.</param>
        /// <returns>El valor del setting del registry</returns>
        public static string ReadStringValueFromProductRegistryKey(string valueName)
        {
            RegistryKey regKeySettings = Registry.LocalMachine.OpenSubKey(PRODUCT_REGISTRY_KEY, false);
            if (regKeySettings == null)
            {
                string errorMessage = String.Format("No se pudo leer el valor '{0}' del registry porque no existe la llave '{1}'.", valueName, PRODUCT_REGISTRY_KEY);
                throw new SAT.DyP.Util.Types.PlatformException(errorMessage);
            }

            string settingValue = Convert.ToString(regKeySettings.GetValue(valueName));

            return settingValue;
        }
    }
}
