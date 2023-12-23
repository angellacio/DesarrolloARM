//@(#)CREDITOSFISCALES(W:71950:Sat.ServicioImpresion.LogicaNegocio.Catalogos:Sat.ServicioImpresion.LogicaNegocio.Catalogos.ApplicationSettings:1:12/07/2013[Assembly:1.0:12/07/2013])

// Referencias de sistemas.
using System;

// Referencias personalizadas.
using Sat.ServicioImpresion.AccesoDatos.Catalogos;

namespace Sat.ServicioImpresion.LogicaNegocio.Catalogos
{
	/// <summary>
	/// Clase con los métodos necesarios para el manejo de los ApplicationSettings
	/// </summary>
	public static class ApplicationSettings
	{
		/// <summary>
		/// Método para obtener la información de un setting
		/// </summary>
		/// <param name="settingName">Nombre del setting</param>
		/// <returns>Objeto con los datos del setting</returns>
		public static object ConsultaConfiguracion(string settingName)
		{
			var dalAppSettings = new DalApplicationSettings();
			return dalAppSettings.RecuperaConfiguracion(settingName);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="settingName"></param>
		/// <returns></returns>
		public static T ConsultaConfiguracion<T>(string settingName)
		{
			return (T) Convert.ChangeType(ConsultaConfiguracion(settingName), typeof(T));
		}
	}
}