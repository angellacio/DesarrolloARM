//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.AccesoDatos.Informacion:SAT.CreditosFiscales.Motor.AccesoDatos.Informacion.DalTransacciones:1:12/07/2012[Assembly:1.0:12/07/2013])

// Referencias de sistema.
using System;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace SAT.CreditosFiscales.Motor.AccesoDatos.Informacion
{
	/// <summary>
	/// Clase de acceso a datos para obtener información de transacciones
	/// </summary>
	public class DalTransacciones
	{
		private const string SP_INSERTATRANSACCION = "pInsertaTransaccion";
		private const string SP_INSERTATRANSACCIONSIAT = "pInsertaTransaccionSIAT";

		/// <summary>
		/// Inserta los datos de una transaccion
		/// </summary>
		/// <param name="IdProcesamiento">Id procesamiento</param>
		/// <param name="ClaveConcepto">Clave del concepto</param>
		/// <param name="ClaveTransaccion">Clave de la transaccion</param>
		/// <param name="Descripcion">Descripción</param>
		/// <param name="Valor">Importe</param>
		/// <param name="IdTipoDocumento">Id tipo documento</param>
		/// <param name="IdAplicacion">Id de la aplicación</param>
		/// <returns></returns>
		public static bool InsertaTransaccion
											(
												Guid IdProcesamiento
												, string ClaveConcepto
												, string ClaveTransaccion
												, string Descripcion
												, Int64 Valor
												, int IdTipoDocumento
												, int IdAplicacion
												, int IdConcepto
												, int IdAgrupador
											)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand cmd = db.GetStoredProcCommand(SP_INSERTATRANSACCION);
			int registrosAfectados;

			db.AddInParameter(cmd, "@IdProcesamiento", System.Data.DbType.Guid, IdProcesamiento);
			db.AddInParameter(cmd, "@ClaveConcepto", System.Data.DbType.String, ClaveConcepto);
			db.AddInParameter(cmd, "@ClaveTransaccion", System.Data.DbType.String, ClaveTransaccion);
			db.AddInParameter(cmd, "@Descripcion", System.Data.DbType.String, Descripcion);
			db.AddInParameter(cmd, "@Valor", System.Data.DbType.Int64, Valor);
			db.AddInParameter(cmd, "@IdTipoDocumento", System.Data.DbType.Int16, IdTipoDocumento);
			db.AddInParameter(cmd, "@IdAplicacion", System.Data.DbType.Int16, IdAplicacion);
			db.AddInParameter(cmd, "@IDConcepto", System.Data.DbType.Int32, IdConcepto);
			db.AddInParameter(cmd, "@IdAgrupador", System.Data.DbType.Int32, IdAgrupador);

			registrosAfectados = Convert.ToInt32(db.ExecuteNonQuery(cmd));

			return registrosAfectados > 0;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdProcesamiento"></param>
		/// <param name="IdAgrupador"></param>
		/// <param name="IdConcepto"></param>
		/// <param name="ClaveTransaccion"></param>
		/// <returns></returns>
		public static bool InsertaTransaccionSIAT
												(
													Guid IdProcesamiento
													, int IdAgrupador
													, int IdConcepto
													, string ClaveTransaccion
												)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand cmd = db.GetStoredProcCommand(SP_INSERTATRANSACCIONSIAT);
			int registrosAfectados;
			db.AddInParameter(cmd, "@IdProcesamiento", System.Data.DbType.Guid, IdProcesamiento);
			db.AddInParameter(cmd, "@IdAgrupador", System.Data.DbType.Int32, IdAgrupador);
			db.AddInParameter(cmd, "@IDConcepto", System.Data.DbType.Int32, IdConcepto);
			db.AddInParameter(cmd, "@ClaveTransaccion", System.Data.DbType.String, ClaveTransaccion);

			registrosAfectados = Convert.ToInt32(db.ExecuteNonQuery(cmd));

			return registrosAfectados > 0;
		}
	}
}