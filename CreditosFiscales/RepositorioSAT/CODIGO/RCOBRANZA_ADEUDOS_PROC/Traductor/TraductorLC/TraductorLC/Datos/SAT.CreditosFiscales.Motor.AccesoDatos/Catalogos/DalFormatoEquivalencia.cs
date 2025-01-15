//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.AccesoDatos.Catalogos:SAT.CreditosFiscales.Motor.AccesoDatos.Catalogos.DalFormatoEquivalencia:1:12/07/2012[Assembly:1.0:12/07/2013])

// Referencias de sistema.
using System;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;

// Referencias personalizadas.
using SAT.CreditosFiscales.Motor.Entidades.Catalogos;

namespace SAT.CreditosFiscales.Motor.AccesoDatos.Catalogos
{
	/// <summary>
	/// Clase de acceso a datos de formato de impresión 
	/// </summary>
	public class DalFormatoEquivalencia
	{
		public const string SP_OBTIENEEQUIVALENCIAFORMATOIMP = "pObtieneEquivalenciaFormatoImpresion";

		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTipoDocumento"></param>
		/// <param name="IdAplicacion"></param>
		/// <returns></returns>
		public static List<CatFormatoImpresionEquivalencia> ObtieneEquivalenciaFormatoImpresion(
																									int IdTipoDocumento
																								   , int IdAplicacion
																							   )
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand cmd = db.GetStoredProcCommand(SP_OBTIENEEQUIVALENCIAFORMATOIMP);
			var lstReglas = new List<CatFormatoImpresionEquivalencia>();

			db.AddInParameter(cmd, "@IdTipoDocumento", System.Data.DbType.String, IdTipoDocumento);
			db.AddInParameter(cmd, "@IdAplicacion", System.Data.DbType.String, IdAplicacion);

			using (var dataReader = db.ExecuteReader(cmd))
			{
				while (dataReader.Read())
				{
					var catReglasEquivalencia = new CatFormatoImpresionEquivalencia();
					catReglasEquivalencia.IdAplicacion = Convert.ToInt32(dataReader["IdAplicacion"]);
					catReglasEquivalencia.IdTipoDocumento = Convert.ToInt32(dataReader["IdTipoDocumento"]);
					catReglasEquivalencia.IdFormatoImpresion = Convert.ToInt32(dataReader["IdFormatoImpresion"]);
					catReglasEquivalencia.XPathEvaluacion = dataReader["XPathEvaluacion"].ToString();
					catReglasEquivalencia.ValorEvaluacion = dataReader["ValorEvaluacion"].ToString();
					lstReglas.Add(catReglasEquivalencia);
				}
			}

			return lstReglas;
		}
	}
}