//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.AccesoDatos.Informacion:SAT.CreditosFiscales.Motor.AccesoDatos.Informacion.DalAgrupador:1:12/07/2012[Assembly:1.0:12/07/2013])

// Referencias de sistema.
using System;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace SAT.CreditosFiscales.Motor.AccesoDatos.Informacion
{
	/// <summary>
	/// Clase de acceso a datos para obtener información de agrupadores
	/// </summary>
	public class DalAgrupador
	{
		private const string SP_INSERTAAGRUPADOR = "pInsertaAgrupador";
		private const string SP_INSERTASIATAGRUPADOR = "pInsertaSIATAgrupador";
		private const string SP_ACTUALIZAIMPORTEAGRUPADOR = "pObtenerImporteTotal";

		/// <summary>
		/// Inserta un registro con los datos del agrupador
		/// </summary>
		/// <param name="idProcesamiento">Id del procesamiento del agrupador</param>
		/// <param name="IdAgrupador">Id del agrupador a insertar</param>
		/// <param name="Valor">Valor del agrupador</param>
		/// <param name="Total">Importe total del agrupador</param>
		/// <param name="SaldoInformativo">Saldo informativo</param>
		/// <returns>true si se insertó correctamente</returns>
		public static bool InsertaAgrupador(Guid idProcesamiento, int IdAgrupador, string Valor, decimal Total, decimal SaldoInformativo)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand cmd = db.GetStoredProcCommand(SP_INSERTAAGRUPADOR);
			int registrosAfectados;

			db.AddInParameter(cmd, "@pIdProcesamiento", System.Data.DbType.Guid, idProcesamiento);
			db.AddInParameter(cmd, "@Valor", System.Data.DbType.String, Valor);
			db.AddInParameter(cmd, "@Total", System.Data.DbType.Decimal, Total);
			db.AddInParameter(cmd, "@IdAgrupador", System.Data.DbType.Int32, IdAgrupador);
			db.AddInParameter(cmd, "@SaldoInformativo", System.Data.DbType.Decimal, SaldoInformativo);

			registrosAfectados = Convert.ToInt32(db.ExecuteNonQuery(cmd));

			return registrosAfectados > 0;
		}

		/// <summary>
		/// Actualiza el importe total del registro del agrupador
		/// </summary>
		/// <param name="idProcesamiento">Id del procesamiento</param>
		/// <param name="IdAgrupador">Id del agrupador</param>
		/// <returns>true si se actualizó correctamente</returns>
		public static bool ActualizaImporteAgrupador(Guid idProcesamiento, int IdAgrupador)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand cmd = db.GetStoredProcCommand(SP_ACTUALIZAIMPORTEAGRUPADOR);
			int registrosAfectados;

			db.AddInParameter(cmd, "@pIdProcesamiento", System.Data.DbType.Guid, idProcesamiento);
			db.AddInParameter(cmd, "@pIdAgrupador", System.Data.DbType.Int32, IdAgrupador);

			registrosAfectados = Convert.ToInt32(db.ExecuteNonQuery(cmd));

			return registrosAfectados > 0;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdProcesamiento"></param>
		/// <param name="idAgrupador"></param>
		/// <param name="agrupador"></param>
		/// <returns></returns>
		public static bool InsertaSIATAgrupador(Guid IdProcesamiento, int idAgrupador, Entidades.DatosProcesamiento.CreditosFiscalesSIATAgrupador agrupador)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand cmd = db.GetStoredProcCommand(SP_INSERTASIATAGRUPADOR);
			int registrosAfectados;

			db.AddInParameter(cmd, "@pIdProcesamiento", System.Data.DbType.Guid, IdProcesamiento);
			db.AddInParameter(cmd, "@pIdAgrupador", System.Data.DbType.Int16, idAgrupador);
			db.AddInParameter(cmd, "@pResolucion", System.Data.DbType.String, agrupador.IdResolucion);

			registrosAfectados = Convert.ToInt32(db.ExecuteNonQuery(cmd));

			return registrosAfectados > 0;
		}
	}
}