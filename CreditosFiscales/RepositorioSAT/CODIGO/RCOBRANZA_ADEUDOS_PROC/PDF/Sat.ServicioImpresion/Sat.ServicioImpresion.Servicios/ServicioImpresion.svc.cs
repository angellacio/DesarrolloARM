//@(#)CREDITOSFISCALES(W:71950:Sat.ServicioImpresion.Servicios:Sat.ServicioImpresion.Servicios.ServicioImpresion:1:12/07/2013[Assembly:1.0:12/07/2013])

/*
 * Actualizado por: Mario Escarpulli
 * Actualizado el: 16/Junio/2019
*/

// Referencias de sistema.
using System.ServiceModel;

// Referencias personalizadas
using Sat.ServicioImpresion.LogicaNegocio;
using Sat.ServicioImpresion.Entidades.AccesoLogEventos;

namespace Sat.ServicioImpresion.Servicios
{
	/// <summary>
	/// Clase para el manejo de los métodos publicados para el servicio de impresión
	/// </summary>
	public class ServicioImpresion : IServicioImpresion
	{
		/// <summary>
		/// Método para generar un archivo en un mapa de bytes de acuerdo a un xml de entrada previamente definido
		/// </summary>
		/// <param name="strXmlDatos">Cadena en formato XML.</param>
		/// <param name="IdDocumento">Identificador del documento.</param>
		/// <returns>Archivo en mapa de bytes</returns>
		public byte[] ObtenerPDF(string strXmlDatos, int IdDocumento)
		{
			try
			{
				return GeneraPDF.GenerarArchivoPDF(strXmlDatos, IdDocumento);
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
		/// Método para generar un archivo de imagen en un arreglo de bytes a través de una cadena de caracter con formato XML de entrada previamente definido.
		/// </summary>
		/// <param name="strXmlDatos">Cadena de caracter en formato XML.</param>
		/// <param name="IdDocumento">Identificador único (ID) del documento.</param>
		/// <returns>Archivo en mapa de bytes</returns>
		public byte[] ObtenerPNG(string strXmlDatos, int IdDocumento)
		{
			try
			{
				return GeneraPNG.GenerarArchivoPng(strXmlDatos, IdDocumento);
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