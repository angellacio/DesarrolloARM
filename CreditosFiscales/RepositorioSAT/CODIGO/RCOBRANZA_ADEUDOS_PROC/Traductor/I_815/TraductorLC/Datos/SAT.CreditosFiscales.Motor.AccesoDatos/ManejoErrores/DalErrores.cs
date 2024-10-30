//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.AccesoDatos.Informacion:SAT.CreditosFiscales.Motor.AccesoDatos.Informacion.DalErrores:1:12/07/2012[Assembly:1.0:12/07/2013])

// Referencias de sistema.
using System;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace SAT.CreditosFiscales.Motor.AccesoDatos.ManejoErrores
{
	/// <summary>
	/// Clase de acceso a datos para obtener información de errores
	/// </summary>
	public class DalErrores
	{
		private const string SP_INSERTAERRORES = "pInsertaErrores";

		/// <summary>
		/// Inserta errores del proceso
		/// </summary>
		/// <param name="idAplicacion">Id de la aplicación</param>
		/// <param name="idTipoDocPago">Id tipo de documento</param>
		/// <param name="idProcesamiento">Id procesamiento</param>
		/// <param name="errores">Errores generados</param>
		/// <param name="mensaje">mensaje de error</param>
		/// <param name="paso">paso del proceso en que se generó el error</param>
		/// <returns>true si se insertó correctamente</returns>
		public static bool InsertaErrores(short idAplicacion, short idTipoDocPago, Guid idProcesamiento, string errores, string mensaje, SAT.CreditosFiscales.Motor.Entidades.Enumeraciones.PasosTraductor paso)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand cmd = db.GetStoredProcCommand(SP_INSERTAERRORES);
			int registrosAfectados;

			db.AddInParameter(cmd, "@pIdAplicacion", System.Data.DbType.Int16, idAplicacion);
			db.AddInParameter(cmd, "@pIdTipoDocPago", System.Data.DbType.Int16, idTipoDocPago);
			db.AddInParameter(cmd, "@pIdProcesamiento", System.Data.DbType.Guid, idProcesamiento);
			db.AddInParameter(cmd, "@pErrores", System.Data.DbType.String, errores);
			db.AddInParameter(cmd, "@pMensaje", System.Data.DbType.String, mensaje);
			db.AddInParameter(cmd, "@IdPaso", System.Data.DbType.Int16, paso);


			registrosAfectados = Convert.ToInt32(db.ExecuteNonQuery(cmd));

			return registrosAfectados > 0;
		}
	}
}