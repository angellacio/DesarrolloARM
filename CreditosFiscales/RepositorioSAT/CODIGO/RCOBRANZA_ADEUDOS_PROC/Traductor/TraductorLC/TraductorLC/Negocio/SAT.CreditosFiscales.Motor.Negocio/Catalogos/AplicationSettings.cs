//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.Negocio:SAT.CreditosFiscales.Motor.Negocio.AplicationSettings:1:12/07/2012[Assembly:1.0:12/07/2013])

// Referencias de sistema.
using System;

// Referencias personalizadas.
using SAT.CreditosFiscales.Motor.AccesoDatos.Catalogos;

namespace SAT.CreditosFiscales.Motor.Negocio.Catalogos
{
	/// <summary>
	/// 
	/// </summary>
	public static class AplicationSettings
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="settingName"></param>
		/// <returns></returns>
		public static object ConsultaConfiguracion(string settingName)
		{
			//DalApplicationSettings dalAppSettings = new DalApplicationSettings();
			return DalApplicationSettings.RecuperaConfiguracion(settingName);
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