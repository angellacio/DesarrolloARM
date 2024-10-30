//@(#)CREDITOSFISCALES(W:71950:Sat.ServicioImpresion.Servicios:Sat.ServicioImpresion.Servicios.IServicioImpresion:1:12/07/2013[Assembly:1.0:12/07/2013])

/*
 * Actualizado por: Mario Escarpulli
 * Actualizado el: 16/Junio/2019
*/

// Referencias de sistema.
using System.ServiceModel;

namespace Sat.ServicioImpresion.Servicios
{
	/// <summary>
	/// Interface para el manejo de los métodos publicados del servicio de impresión
	/// </summary>
	[ServiceContract]
	public interface IServicioImpresion
	{
		/// <summary>
		/// Método para generar un archivo en un mapa de bytes de acuerdo a un XML de entrada previamente definido.
		/// </summary>
		/// <param name="strXmlDatos">Cadena en formato xml</param>
		/// <param name="IdDocumento">Identificador del documento</param>
		/// <returns>Archivo en mapa de bytes</returns>
		[OperationContract]
		byte[] ObtenerPDF(string strXmlDatos, int IdDocumento);

		/// <summary>
		/// Método para generar un archivo de imagen en un arreglo de bytes a través de una cadena de caracter con formato XML de entrada previamente definido.
		/// </summary>
		/// <param name="strXmlDatos">Cadena en formato xml</param>
		/// <param name="IdDocumento">Identificador del documento</param>
		/// <returns>Arreglo de mapa de bytes.</returns>
		[OperationContract]
		byte[] ObtenerPNG(string strXmlDatos, int IdDocumento);
	}
}