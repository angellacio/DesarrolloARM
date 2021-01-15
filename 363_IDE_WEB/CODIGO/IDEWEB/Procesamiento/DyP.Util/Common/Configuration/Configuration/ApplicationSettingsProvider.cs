//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Util:IApplicationSettingsProvider:0:21/May/2008[SAT.DyP.Util:1.0:21/May/2008])
using System;
using System.Collections.Generic;
using System.Text;

namespace SAT.DyP.Util.Configuration
{
    /// <summary>
    /// Clase que especifica el contrato abstracto que permite a los usuarios del framework de 
    /// configuración de DyP leer valores de configuración.
    /// </summary>
    public interface IApplicationSettingsProvider
    {
        /// <summary>
        /// Lee el valor de un parámetro de configuración.
        /// </summary>
        /// <param name="settingName">El nombre del parámetro de configuración.</param>
        /// <returns>El valor actual del parámetro de configuración.</returns>
        string ReadSetting(string settingName);

        /// <summary>
        /// Busca en configuración un parámetro con el nombre especificado, y en caso de no 
        /// existir, lo crea con el valor default especificado.
        /// </summary>
        /// <param name="settingName">El nombre del parámetro de configuración.</param>
        /// <param name="defaultValue">El valor que se escribirá a configuración en caso de que el 
        /// parámetro de configuración no exista.</param>
        /// <returns>El valor del parámetro de configuración.</returns>
        string ReadSetting(string settingName, string defaultValue);


        /*
         * Fecha de Modificacion : 27 Julio 2012
         * 
         */
        /// <summary>
        /// Busca en configuración un parámetro con el nombre especificado, y en caso de no 
        /// existir, lo crea con el valor default especificado.
        /// </summary>
        /// <param name="settingName">El nombre del parámetro de configuración.</param>
        /// <param name="tpDeclaracion">El valor que se escribirá a configuración en caso de que el 
        /// parámetro de configuración no exista.</param>
        /// <returns>El valor del parámetro de configuración.</returns>
        string ReadSettingCfg(string settingName, string tpDeclaracion);


        string ReadEncryptSetting(string settingName);

        /// <summary>
        /// Guarda el valon de un parámetro de configuración.
        /// </summary>
        /// <param name="settingName">El nombre del parámetro de configuración.</param>
        /// <param name="settingValue">El valor que se guardará.</param>
        void SaveSetting(string settingName, string settingValue);


    }
}
