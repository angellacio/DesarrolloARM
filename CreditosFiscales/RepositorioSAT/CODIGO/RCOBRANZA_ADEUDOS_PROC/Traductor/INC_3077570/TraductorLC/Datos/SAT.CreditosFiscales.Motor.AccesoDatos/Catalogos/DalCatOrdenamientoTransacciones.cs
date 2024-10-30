// Referencias de sistema.
using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;

// Referencias personalizadas.
using SAT.CreditosFiscales.Motor.Entidades.Catalogos;

namespace SAT.CreditosFiscales.Motor.AccesoDatos.Catalogos
{
	/// <summary>
	/// 
	/// </summary>
	public class DalCatOrdenamientoTransacciones
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdAplicacion"></param>
		/// <param name="IdTipoDocumento"></param>
		/// <returns></returns>
		public static List<CatOrdenamientoTransacciones> ConsultaOrdenamiento(int IdAplicacion, int IdTipoDocumento)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand cmd = db.GetStoredProcCommand("pObtieneCatOrdenamientoTransacciones");
			IDataReader rdOrdenamiento = null;
			var lstOrdenamiento = new List<CatOrdenamientoTransacciones>();

			db.AddInParameter(cmd, "@IdAplicacion", System.Data.DbType.Int32, IdAplicacion);
			db.AddInParameter(cmd, "@IdTipoDocumento", System.Data.DbType.Int32, IdTipoDocumento);

			using (rdOrdenamiento = db.ExecuteReader(cmd))
			{

				while (rdOrdenamiento.Read())
				{
					var CatOrdenamiento = new CatOrdenamientoTransacciones();
					CatOrdenamiento.IdAplicacion = Convert.ToInt32(rdOrdenamiento["IdAplicacion"]);
					CatOrdenamiento.TipoDocumento = Convert.ToInt32(rdOrdenamiento["IdTipoDocumento"]);
					CatOrdenamiento.Ordenamiento = rdOrdenamiento["Ordenamiento"].ToString();
					CatOrdenamiento.Secuencia = Convert.ToInt32(rdOrdenamiento["Secuencia"]);
					lstOrdenamiento.Add(CatOrdenamiento);
				}
			}

			return lstOrdenamiento;
		}
	}
}