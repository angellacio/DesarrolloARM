//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.AccesoDatos.Catalogos:SAT.CreditosFiscales.Motor.AccesoDatos.Catalogos.DalCatTransaccion:1:12/07/2012[Assembly:1.0:12/07/2013])

// Referencias de sistema.
using System;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace SAT.CreditosFiscales.Motor.AccesoDatos.Catalogos
{
	/// <summary>
	/// Clase de acceso a datos para obtener información de transacciones
	/// </summary>
	public class DalCatTransaccion
	{
		public const string SP_INSERTACATTRANSACCION = "pInsertaCatTransaccion";

		/// <summary>
		/// Inserta un registro en CatTransaccion
		/// </summary>
		/// <param name="CveTransaccion">Clave de la transacción</param>
		/// <param name="Descripcion">Descripción de la transacción</param>
		/// <param name="IdTipoTransaccion">Id Tipo de transacción</param>
		/// <param name="TipoTransaccion">Tipo de transacción</param>
		/// <param name="IdTipoDocumento">Id Tipo de documento</param>
		/// <param name="IdAplicacion">Id de aplicación</param>
		/// <param name="EsObligatorio">Indica si la transacción es obligatoria</param>
		/// <param name="EsActivo">Indica si el registro está activo</param>
		/// <returns>número de registros afectados</returns>
		public static int InsertaCatTrasaccion(
													string CveTransaccion
												   , string Descripcion
												   , int IdTipoTransaccion
												   , string TipoTransaccion
												   , int IdTipoDocumento
												   , int IdAplicacion
												   , bool EsObligatorio
												   , bool EsActivo
											   )
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand cmd = db.GetStoredProcCommand(SP_INSERTACATTRANSACCION);
			int idRegla;

			db.AddInParameter(cmd, "@CveTransaccion", System.Data.DbType.String, CveTransaccion);
			db.AddInParameter(cmd, "@Descripcion", System.Data.DbType.String, Descripcion);
			db.AddInParameter(cmd, "@IdTipoTransaccion", System.Data.DbType.String, IdTipoTransaccion);
			db.AddInParameter(cmd, "@TipoTransaccion", System.Data.DbType.String, TipoTransaccion);
			db.AddInParameter(cmd, "@IdTipoDocumento", System.Data.DbType.String, IdTipoDocumento);
			db.AddInParameter(cmd, "@IdAplicacion", System.Data.DbType.String, IdAplicacion);
			db.AddInParameter(cmd, "@EsObligatorio", System.Data.DbType.String, EsObligatorio);
			db.AddInParameter(cmd, "@EsActivo", System.Data.DbType.Boolean, EsActivo);

			idRegla = Convert.ToInt32(db.ExecuteScalar(cmd));

			return idRegla;
		}
	}
}