using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sat.CreditosFiscales.Comunes.Entidades.Catalogos;
using Sat.CreditosFiscales.Datos.AccesoDatos.Catalogos;

//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Procesamiento.LogicaNegocio.Catalogos:Sat.CreditosFiscales.Procesamiento.LogicaNegocio.Catalogos.ApplicationSettings:1:12/07/2013[Assembly:1.0:12/07/2013])

namespace Sat.CreditosFiscales.Procesamiento.LogicaNegocio.Catalogos
{
    /// <summary>
    /// Clase de acceso a la tabla de configuración de la aplicación en BD
    /// </summary>
    public static class ApplicationSettings
    {
        /// <summary>
        /// Obtiene un grupo de parámetros de configuración
        /// </summary>
        /// <param name="settingName">Nombre de los parámetros a recuperar</param>
        /// <returns>Lista de parámetros</returns>
        public static List<ApplicationSetting> ObtieneGrupoConfiguracion(string settingName)
        {
            DalApplicationSettings dalAppSettings = new DalApplicationSettings();
            return dalAppSettings.RecuperaGrupoConfiguracion(settingName);
        }

        /// <summary>
        /// Consulta un parámetro de configuración y retorna un objeto
        /// </summary>
        /// <param name="settingName">Nombre del parámetro</param>
        /// <returns>Valor del parámetro</returns>
        public static object ConsultaConfiguracion(string settingName)
        {
            DalApplicationSettings dalAppSettings = new DalApplicationSettings();
            return dalAppSettings.RecuperaConfiguracion(settingName);           
        }

        /// <summary>
        /// Consulta un parámetro de configuración y lo convierte al tipo especificado.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="settingName">Nombre del parámetro</param>
        /// <returns>Valor del parámetro</returns>
        public static T ConsultaConfiguracion<T>(string settingName)
        {
            return (T)Convert.ChangeType(ConsultaConfiguracion(settingName), typeof(T));
        }
    }
}
