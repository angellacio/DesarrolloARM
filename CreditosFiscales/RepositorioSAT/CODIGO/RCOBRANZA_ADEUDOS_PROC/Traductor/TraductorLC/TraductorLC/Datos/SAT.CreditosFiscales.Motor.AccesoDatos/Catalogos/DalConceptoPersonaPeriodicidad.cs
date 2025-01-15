// Referencias de sistema.
using System;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;

// Referenciaspersonalizadas.
using SAT.CreditosFiscales.Motor.Entidades.Catalogos;

namespace SAT.CreditosFiscales.Motor.AccesoDatos.Catalogos
{
	/// <summary>
	/// 
	/// </summary>
	public class DalConceptoPersonaPeriodicidad
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="idTipoPersona"></param>
		/// <returns></returns>
		public static List<CatConceptoPersonaPeriodicidad> ObtenerCatConceptoPersonaPeriodicidad(int idTipoPersona)
		{
			var lstPeridicidadEquivalenciaConceptoPersonaPeriodicidad = new List<CatConceptoPersonaPeriodicidad>();
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetStoredProcCommand("pObtenerCatConceptoPersonaPeriodicidad");
			db.AddInParameter(dbCommand, "@pIdTipoPersona", System.Data.DbType.Int32, idTipoPersona);
			using (var dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
					var periodicidad = new CatConceptoPersonaPeriodicidad();
					periodicidad.ConceptoDyP = (string) dataReader["ConceptoDyP"];
					periodicidad.IdTipoPersona = Convert.ToInt32(dataReader["IdTipoPersona"]);
					periodicidad.IdPeriodicidadDyP = (string) dataReader["IdPeriodicidadDyP"];
					lstPeridicidadEquivalenciaConceptoPersonaPeriodicidad.Add(periodicidad);
				}
			}

			return lstPeridicidadEquivalenciaConceptoPersonaPeriodicidad;
		}
	}
}