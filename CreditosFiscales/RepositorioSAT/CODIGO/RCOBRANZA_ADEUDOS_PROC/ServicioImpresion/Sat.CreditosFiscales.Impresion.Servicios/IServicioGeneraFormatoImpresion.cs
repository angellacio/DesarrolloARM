//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Impresion.Servicios:Sat.CreditosFiscales.Impresion.Servicios.IServicioGeneraFormatoImpresion:1:12/07/2013[Assembly:1.0:12/07/2013])

/*
 * Actualizado por: Mario Escarpulli
 * Actualizado el: 16/Junio/2019
*/

// Referencias de sistemas
using System.ServiceModel;
using System.Collections.Generic;

// Referencias personalizadas.
using Sat.CreditosFiscales.Impresion.Entidades;

namespace Sat.CreditosFiscales.Impresion.Servicios
{
	/// <summary>
	/// Interface para el manejo de los métodos publicados por el servicio de impresión de créditos fiscales
	/// </summary>
	[ServiceContract]
	public interface IServicioGeneraFormatoImpresion
	{
		/// <summary>
		/// Genera un archivo en arreglo de bytes el cual recibe una lista de folios de líneas de captura
		/// </summary>
		/// <param name="template"><see cref=""/>Enumeración para el tipo de documento (Template)</param>
		/// <param name="listaDeFolios">Lista de folios</param>
		/// <returns>Mapa de bytes del archivo</returns>
		[OperationContract]
		byte[] GeneraArchivo(EnumTemplate template, List<string> listaDeFolios);

		/// <summary>
		/// Genera un archivo en arreglo de bytes a través de un listado de Folios de Líneas de Captura para generar un archivo de tipo imagen.
		/// </summary>
		/// <param name="template">Enumeración para el Tipo de Documento (Template) a utilizar.</param>
		/// <param name="listaDeFolios">Lista de Folios.</param>
		/// <returns>Arreglo de bytes</returns>
		[OperationContract]
		byte[] GeneraArchivoImagen(EnumTemplate template, List<string> listaDeFolios);
	}
}