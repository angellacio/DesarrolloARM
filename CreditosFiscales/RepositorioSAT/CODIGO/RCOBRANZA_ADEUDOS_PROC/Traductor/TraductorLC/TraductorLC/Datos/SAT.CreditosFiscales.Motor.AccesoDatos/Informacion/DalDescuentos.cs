//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.AccesoDatos.Informacion:SAT.CreditosFiscales.Motor.AccesoDatos.Informacion.DalDescuentos:1:12/07/2012[Assembly:1.0:12/07/2013])

// Referencias de sistema.
using System;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace SAT.CreditosFiscales.Motor.AccesoDatos.Informacion
{
	/// <summary>
	/// Clase de acceso a datos para obtener información de descuentos
	/// </summary>
	public class DalDescuentos
	{
		private const string SP_INSERTADESCUENTOORIGINAL = "pInsertaDescuentoConceptoOrignal";
		private const string SP_INSERTADESCUENTOORIGINALHIJO = "pInsertaDescuentoConceptoOrignalHijo";

		/// <summary>
		/// Inserta descuentos del concepto original
		/// </summary>
		/// <param name="IdAgrupador">Id de agrupador</param>
		/// <param name="IdProcesamiento">Id de procesamiento</param>
		/// <param name="IdConceptoOriginal">Id del concepto original</param>
		/// <param name="IdDescuento">Id del descuento</param>
		/// <param name="ImporteDescuento">Importe descuento</param>
		/// <returns>true si la inserción fue correcta</returns>
		public static bool InsertaDescuentoConceptoOriginal
															(
																int IdAgrupador
															   , Guid IdProcesamiento
															   , int IdConceptoOriginal
															   , string IdDescuento
															   , decimal ImporteDescuento
															)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand cmd = db.GetStoredProcCommand(SP_INSERTADESCUENTOORIGINAL);
			int registrosAfectados;

			db.AddInParameter(cmd, "@IdAgrupador", System.Data.DbType.Int32, IdAgrupador);
			db.AddInParameter(cmd, "@IdProcesamiento", System.Data.DbType.Guid, IdProcesamiento);
			db.AddInParameter(cmd, "@IdConceptoOriginal", System.Data.DbType.Int32, IdConceptoOriginal);
			db.AddInParameter(cmd, "@IdDescuento", System.Data.DbType.String, IdDescuento);
			db.AddInParameter(cmd, "@ImporteDescuento", System.Data.DbType.Decimal, ImporteDescuento);

			registrosAfectados = Convert.ToInt32(db.ExecuteNonQuery(cmd));

			return registrosAfectados > 0;
		}

		/// <summary>
		/// Inserta descuentos del concepto hijo
		/// </summary>
		/// <param name="IdAgrupador">Id agrupador</param>
		/// <param name="IdProcesamiento">Id procesamiento</param>
		/// <param name="IdConceptoOriginal">Id concepto original</param>
		/// <param name="IdConceptoOriginalHijo">Id concepto original hijo</param>
		/// <param name="IdDescuento">Id descuento</param>
		/// <param name="ImporteDescuento">Importe descuento</param>
		/// <returns>true si la inserción fue correcta</returns>
		public static bool InsertaDescuentoConceptoOriginalHijo
															(
																int IdAgrupador
															   , Guid IdProcesamiento
															   , int IdConceptoOriginal
															   , int IdConceptoOriginalHijo
															   , string IdDescuento
															   , decimal ImporteDescuento
															)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand cmd = db.GetStoredProcCommand(SP_INSERTADESCUENTOORIGINALHIJO);
			int registrosAfectados;

			db.AddInParameter(cmd, "@IdAgrupador", System.Data.DbType.Int32, IdAgrupador);
			db.AddInParameter(cmd, "@IdProcesamiento", System.Data.DbType.Guid, IdProcesamiento);
			db.AddInParameter(cmd, "@IdConceptoOriginal", System.Data.DbType.Int32, IdConceptoOriginal);
			db.AddInParameter(cmd, "@IdConceptoOriginalHijo", System.Data.DbType.Int32, IdConceptoOriginalHijo);
			db.AddInParameter(cmd, "@IdDescuento", System.Data.DbType.String, IdDescuento);
			db.AddInParameter(cmd, "@ImporteDescuento", System.Data.DbType.Decimal, ImporteDescuento);

			registrosAfectados = Convert.ToInt32(db.ExecuteNonQuery(cmd));

			return registrosAfectados > 0;
		}
	}
}