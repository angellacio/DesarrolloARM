//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Util:IApplicationSettingsProvider:0:21/May/2008[SAT.DyP.Util:1.0:21/May/2008])
using System;
using System.Collections.Generic;
using System.Text;

namespace SAT.DyP.Util.Configuration
{
    /// <summary>
    /// Clase que especifica el contrato abstracto que permite a los usuarios del framework de 
    /// configuraci�n de DyP leer valores de configuraci�n.
    /// </summary>
    public interface IApplicationSettingsProvider
    {
        /// <summary>
        /// Lee el valor de un par�metro de configuraci�n.
        /// </summary>
        /// <param name="settingName">El nombre del par�metro de configuraci�n.</param>
        /// <returns>El valor actual del par�metro de configuraci�n.</returns>
        string ReadSetting(string settingName);

        /// <summary>
        /// Busca en configuraci�n un par�metro con el nombre especificado, y en caso de no 
        /// existir, lo crea con el valor default especificado.
        /// </summary>
        /// <param name="settingName">El nombre del par�metro de configuraci�n.</param>
        /// <param name="defaultValue">El valor que se escribir� a configuraci�n en caso de que el 
        /// par�metro de configuraci�n no exista.</param>
        /// <returns>El valor del par�metro de configuraci�n.</returns>
        string ReadSetting(string settingName, string defaultValue);


        /*
         * Fecha de Modificacion : 27 Julio 2012
         * 
         */
        /// <summary>
        /// Busca en configuraci�n un par�metro con el nombre especificado, y en caso de no 
        /// existir, lo crea con el valor default especificado.
        /// </summary>
        /// <param name="settingName">El nombre del par�metro de configuraci�n.</param>
        /// <param name="tpDeclaracion">El valor que se escribir� a configuraci�n en caso de que el 
        /// par�metro de configuraci�n no exista.</param>
        /// <returns>El valor del par�metro de configuraci�n.</returns>
        string ReadSettingCfg(string settingName, string tpDeclaracion);


        string ReadEncryptSetting(string settingName);

        /// <summary>
        /// Guarda el valon de un par�metro de configuraci�n.
        /// </summary>
        /// <param name="settingName">El nombre del par�metro de configuraci�n.</param>
        /// <param name="settingValue">El valor que se guardar�.</param>
        void SaveSetting(string settingName, string settingValue);


    }
}
