// Referencias de sistemas.
using System.Linq;
using System.Collections.Generic;

namespace CargaDeCatalogos.TransaccionesObligatorias
{
	/// <summary>
	/// 
	/// </summary>
	public class TransaccionesObligatorias
	{
		public const string PagosUnaSolaExhibicion = "4011,4243,4273,4283";
		public const string PagosParcialidadesInicial = "4011,4243,4293,4302,4423";
		public const string PagosParcialidadesAutorizacion = "4011,4243,4423";
		public const string PagosMovimientosContables = "";
		public const string PagosMovimientosRectificacion = "4011,4243,4273,4283,4423";

		/// <summary>
		/// 
		/// </summary>
		/// <param name="idDocumento"></param>
		/// <param name="ClaveTransaccion"></param>
		/// <returns></returns>
		public static bool ObtieneTransaccionObligatoria(int idDocumento, string ClaveTransaccion)
		{
			bool EsObligatorio = false;
			var sTransacciones = new List<string>();

			switch (idDocumento)
			{
				case 10:
					sTransacciones = PagosUnaSolaExhibicion.Split(',').ToList();
					break;
				case 23:
					sTransacciones = PagosUnaSolaExhibicion.Split(',').ToList();
					break;
				case 21:
					sTransacciones = PagosParcialidadesInicial.Split(',').ToList();
					break;
				case 22:
					sTransacciones = PagosParcialidadesAutorizacion.Split(',').ToList();
					break;
				case 30:
					EsObligatorio = false;
					break;
				case 40:
					sTransacciones = PagosMovimientosRectificacion.Split(',').ToList();
					break;
				default:
					EsObligatorio = false;
					break;
			}

			EsObligatorio = sTransacciones.Exists(delegate (string s) { return s == ClaveTransaccion; });

			return EsObligatorio;
		}
	}
}