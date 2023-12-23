//@(#)CREDITOSFISCALES(W:71950:Sat.ServicioImpresion.Entidades.Catalogos:Sat.ServicioImpresion.Entidades.Catalogos.ApplicationSetting:1:12/07/2013[Assembly:1.0:12/07/2013])

// Referencias de sistema.
using System.Runtime.Serialization;

// Referencias personalizadas.
using E = Sat.ServicioImpresion.Comunes.Herramientas.Enumeradores.CustomAttributes;

namespace Sat.ServicioImpresion.Entidades.Catalogos
{
	/// <summary>
	/// Clase definida para los ApplicationSetting
	/// </summary>
	[DataContract]
	public class ApplicationSetting
	{
		/// <summary>
		/// Nombre del setting
		/// </summary>
		[DataMember]
		public string SettingName { get; set; }

		/// <summary>
		/// Valor del setting
		/// </summary>
		[DataMember]
		public object SettingValue { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public enum Campos
		{
			[E.NombreCampo("SettingName")]
			[E.NombreParametroSql("@SettingName")]
			SettingName = 1,

			[E.NombreCampo("SettingValue")]
			[E.NombreParametroSql("@SettingValue")]
			SettingValue
		}
	}
}