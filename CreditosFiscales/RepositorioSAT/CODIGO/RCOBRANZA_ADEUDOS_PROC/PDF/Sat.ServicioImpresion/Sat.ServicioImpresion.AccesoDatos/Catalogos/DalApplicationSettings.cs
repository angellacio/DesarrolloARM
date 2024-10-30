//@(#)CREDITOSFISCALES(W:71950:Sat.ServicioImpresion.AccesoDatos.Catalogos:Sat.ServicioImpresion.AccesoDatos.Catalogos.DalApplicationSettings:1:12/07/2013[Assembly:1.0:12/07/2013])

// Referencias de sistema.
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

// Referencias personalizadas.
using Sat.ServicioImpresion.Entidades.Catalogos;
using Sat.ServicioImpresion.Comunes.Herramientas;

namespace Sat.ServicioImpresion.AccesoDatos.Catalogos
{
	/// <summary>
	/// Clase con los métodos necesarios para el manejo en base de datos de los ApplicationSettings
	/// </summary>
	public class DalApplicationSettings
	{
		/// <summary>
		/// Método que obtiene la información de un ApplicationSettings
		/// </summary>
		/// <param name="settingName">Nombre del setting</param>
		/// <returns>Objeto con los datos del setting</returns>
		public object RecuperaConfiguracion(string settingName)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand cmd = db.GetStoredProcCommand("pApplicationSettingsConsulta");
			db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(ApplicationSetting.Campos.SettingName), DbType.String, settingName);
			return db.ExecuteScalar(cmd);
		}
	}
}