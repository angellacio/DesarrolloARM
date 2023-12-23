//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.Negocio:SAT.CreditosFiscales.Motor.Negocio.GeneracionEquivalenciaFormato:1:12/07/2012[Assembly:1.0:12/07/2013])

// Referencias de sistema.
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Collections.Generic;

// Referencias personalizadas.
using SAT.CreditosFiscales.Motor.Entidades.Catalogos;
using SAT.CreditosFiscales.Motor.AccesoDatos.Catalogos;

namespace SAT.CreditosFiscales.Motor.Negocio.Procesamiento
{
	/// <summary>
	/// Clase para manejar equivalencias de formato de impresiones
	/// </summary>
	public class GeneracionEquivalenciaFormato
	{
		/// <summary>
		/// Obtiene el formato de impresión para el id de aplicacion y tipo de documento asignado
		/// </summary>
		/// <param name="IdAplicacion">Identificador único (ID) de la Aplicación que envía la petición.</param>
		/// <param name="IdTipoDocumento">Identificador único (ID) del Tipo de Documento que se envía.</param>
		/// <param name="documento">Documento en formato XML con los datos necesarios para generar la Línea de Captura.</param>
		/// <returns>Valor entero con el Id del formato de impresión</returns>
		public static int GeneraEquivalenciaFormato(int IdAplicacion, int IdTipoDocumento, string documento)
		{
			int iIdFormatoImpresion = 0;

			List<CatFormatoImpresionEquivalencia> oReglasFormatoImpresion = DalFormatoEquivalencia.ObtieneEquivalenciaFormatoImpresion(IdTipoDocumento, IdAplicacion);

			XDocument objetoFiltro = XDocument.Parse(documento);
			if (oReglasFormatoImpresion.Count > 1)
			{
				foreach (var vRegla in oReglasFormatoImpresion)
				{
					if (vRegla.XPathEvaluacion != string.Empty)
					{
						XElement nodoObjeto = objetoFiltro.XPathSelectElement(vRegla.XPathEvaluacion);
						if (nodoObjeto != null && (nodoObjeto.Value == vRegla.ValorEvaluacion || string.IsNullOrEmpty(vRegla.ValorEvaluacion)))
						{
							iIdFormatoImpresion = vRegla.IdFormatoImpresion;
							break;
						}
					}
					else
						iIdFormatoImpresion = vRegla.IdFormatoImpresion;
				}
			}
			else
			{
				CatFormatoImpresionEquivalencia oRegla = oReglasFormatoImpresion.FirstOrDefault();
				if (oRegla != null)
				{
					if (oRegla.XPathEvaluacion != string.Empty)
					{
						XElement nodoObjeto = objetoFiltro.XPathSelectElement(oRegla.XPathEvaluacion);
						if (nodoObjeto != null && (nodoObjeto.Value == oRegla.ValorEvaluacion || string.IsNullOrEmpty(oRegla.ValorEvaluacion)))
							iIdFormatoImpresion = oRegla.IdFormatoImpresion;
					}
					else
						iIdFormatoImpresion = oRegla.IdFormatoImpresion;
				}
			}

			return iIdFormatoImpresion;
		}
	}
}