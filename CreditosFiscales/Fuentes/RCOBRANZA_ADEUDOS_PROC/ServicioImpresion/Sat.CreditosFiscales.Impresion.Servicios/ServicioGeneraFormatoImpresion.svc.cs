//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Impresion.Servicios:Sat.CreditosFiscales.Impresion.Servicios.ServicioGeneraFormatoImpresion:1:12/07/2013[Assembly:1.0:12/07/2013])

/*
 * Actualizado por: Mario Escarpulli
 * Actualizado el: 16/Junio/2019
*/

// Referencias de sistemas
using System.ServiceModel;
using System.Collections.Generic;

// Referencias personalizadas.
using Sat.CreditosFiscales.Impresion.Entidades;
using Sat.CreditosFiscales.Impresion.Entidades.AccesoLogEventos;
using Sat.CreditosFiscales.Impresion.LogicaNegocio;

namespace Sat.CreditosFiscales.Impresion.Servicios
{
	/// <summary>
	/// Clase para el manejo de los métodos publicados por el servicio de impresión de créditos fiscales
	/// </summary>
	public class ServicioGeneraFormatoImpresion : IServicioGeneraFormatoImpresion
	{
		/// <summary>
		/// Genera un archivo en arreglo de bytes el cual recibe una lista de folios de líneas de captura
		/// </summary>
		/// <param name="template"><see cref=""/>Enumeración para el tipo de documento (Template)</param>
		/// <param name="listaDeFolios">Lista de folios</param>
		/// <returns>Mapa de bytes del archivo</returns>
		public byte[] GeneraArchivo(EnumTemplate template, List<string> listaDeFolios)
		{
			try
			{
				return new GeneraFormato().GeneraArchivo(template, listaDeFolios);
			}
			catch (ExcepcionTipificada err)
			{
				var reporteError = new ReporteErrorWcf(err.Mensaje);
				if (err.InnerException != null)
					reporteError.InnerException = err.InnerException.Message;

				throw new FaultException<ReporteErrorWcf>(reporteError, new FaultReason(reporteError.Mensaje));
			}
		}

		/// <summary>
		/// Genera un archivo en arreglo de bytes el cual recibe una lista de folios de líneas de captura para generar una imagen.
		/// </summary>
		/// <param name="template">Enumeración para el tipo de documento (Template)</param>
		/// <param name="listaDeFolios">Lista de folios</param>
		/// <returns>Mapa de bytes del archivo</returns>
		public byte[] GeneraArchivoImagen(EnumTemplate template, List<string> listaDeFolios)
		{
			try
			{
				return new GeneraFormato().GeneraArchivoImagen(template, listaDeFolios);
			}
			catch (ExcepcionTipificada err)
			{
				var reporteError = new ReporteErrorWcf(err.Mensaje);
				if (err.InnerException != null)
					reporteError.InnerException = err.InnerException.Message;

				throw new FaultException<ReporteErrorWcf>(reporteError, new FaultReason(reporteError.Mensaje));
			}
		}
	}
}