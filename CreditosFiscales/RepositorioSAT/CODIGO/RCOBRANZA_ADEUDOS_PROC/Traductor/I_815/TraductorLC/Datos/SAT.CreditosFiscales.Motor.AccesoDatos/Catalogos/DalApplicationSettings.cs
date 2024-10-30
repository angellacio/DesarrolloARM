//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.AccesoDatos.Catalogos:SAT.CreditosFiscales.Motor.AccesoDatos.Catalogos.DalApplicationSettings:1:12/07/2012[Assembly:1.0:12/07/2013])

// Referencias de sistema.
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace SAT.CreditosFiscales.Motor.AccesoDatos.Catalogos
{
	/// <summary>
	/// 
	/// </summary>
	public class DalApplicationSettings
	{
		/// <summary>
		/// Obtiene los datos de configuración 
		/// </summary>
		/// <param name="settingName"></param>
		/// <returns></returns>
		public static object RecuperaConfiguracion(string settingName)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand cmd = db.GetStoredProcCommand("pApplicationSettingsConsulta");
			db.AddInParameter(cmd, "SettingName", DbType.String, settingName);
			return db.ExecuteScalar(cmd);
		}
	}
}