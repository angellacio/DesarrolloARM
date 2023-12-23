//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Impresion.LogicaNegocio:Sat.CreditosFiscales.Impresion.LogicaNegocio.GeneraFormato:1:12/07/2013[Assembly:1.0:12/07/2013])

/*
 * Actualizado por: Mario Escarpulli
 * Actualizado el: 16/Junio/2019
*/

// Referencia de sistema.
using System;
using System.Collections.Generic;

// Referencias personalizadas.
using Sat.CreditosFiscales.Impresion.Entidades;
using Sat.CreditosFiscales.Impresion.AccesoDatos;
using Sat.CreditosFiscales.Impresion.Herramientas;
using Sat.CreditosFiscales.Impresion.Entidades.CodigosError;
using Sat.CreditosFiscales.Impresion.Entidades.AccesoLogEventos;
using Sat.CreditosFiscales.Impresion.LogicaNegocio.AccesoLogEventos;

namespace Sat.CreditosFiscales.Impresion.LogicaNegocio
{
	/// <summary>
	/// Clase para generar los diversos archivos (templates)
	/// </summary>
	public class GeneraFormato
	{
		/// <summary>
		/// Genera un archivo en arreglo de bytes el cual recibe una lista de folios de líneas de captura
		/// </summary>
		/// <param name="template"><see cref=""/>Enumaración para el tipo de documento (Template)</param>
		/// <param name="listaDeFolios">Lista de folios</param>
		/// <returns>Mapa de bytes del archivo</returns>
		public byte[] GeneraArchivo(EnumTemplate template, List<string> listaDeFolios)
		{
			try
			{
				string sXml = this.RemplazaCaracteresEspeciales(this.GenerarXML(template, listaDeFolios));
				using (var servicio = new ServicioImpresion.ServicioImpresionClient())
				{
					return servicio.ObtenerPDF(sXml, (int) template);
				}
			}
			catch (ExcepcionTipificada ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				string ticket = LogEventos.EscribirEntradaLog((int) EnumErroresGeneracionFormatoImpresion.ErrorGenerarArchivo, ex);
				throw new ExcepcionTipificada("No se logró generar el archivo, por favor intentelo más tarde", ex, ticket);
			}
		}

		/// <summary>
		/// Genera un archivo de imagen, en arreglo de bytes, el cúal recibe una lista de folios de Líneas de Captura.
		/// </summary>
		/// <param name="template">Enumaración para el tipo de documento (Template)</param>
		/// <param name="listaDeFolios">Lista de folios</param>
		/// <returns>Arreglo de bytes del archivo de imagen.</returns>
		public byte[] GeneraArchivoImagen(EnumTemplate template, List<string> listaDeFolios)
		{
			try
			{
				string sXml = this.RemplazaCaracteresEspeciales(this.GenerarXML(template, listaDeFolios));
				using (var vsweServicio = new ServicioImpresion.ServicioImpresionClient())
				{
					return vsweServicio.ObtenerPNG(sXml, (int) template);
				}
			}
			catch (ExcepcionTipificada etError)
			{
				throw etError;
			}
			catch (Exception eError)
			{
				string sNumeroTicket = LogEventos.EscribirEntradaLog((int) EnumErroresGeneracionFormatoImpresion.ErrorGenerarArchivo, eError);
				throw new ExcepcionTipificada("No se logró generar el archivo de imagen, por favor intentelo más tarde", eError, sNumeroTicket);
			}
		}

		/// <summary>
		/// Genera un XML con el formato requerido para la generaración de un archivo
		/// </summary>
		/// <param name="template"><see cref="EnumTemplate"/>Enumeración para el tipo de template</param>
		/// <param name="listaDeFolios">Lista de folios</param>
		/// <returns>Cadena en formato XML</returns>
		public string GenerarXML(EnumTemplate template, List<string> listaDeFolios)
		{
			string sContenidoXml = string.Empty;
			try
			{
				switch (template)
				{
					case EnumTemplate.FormatoParaPago:
						sContenidoXml = this.ObtenerFormatoPago(listaDeFolios);
						break;

					case EnumTemplate.FormatoConfirmacionDeTransaccionesVirtuales:
						sContenidoXml = this.ObtenerFormatoConfirmacionDeTransaccionesVirtuales(listaDeFolios);
						break;

					case EnumTemplate.FormatoConfirmacionDeRectificacionesContables:
						sContenidoXml = this.ObtenerFormatoConfirmacionDeRectificacionesContables(listaDeFolios);
						break;

					case EnumTemplate.FormatoConfirmacionDeMovimientosContables:
						sContenidoXml = this.ObtenerFormatoConfirmacionDeMovimientosContables(listaDeFolios);
						break;
				}
			}
			catch (Exception eError)
			{
				string sTicket = LogEventos.EscribirEntradaLog((int) EnumErroresGeneracionFormatoImpresion.ErrorGenerarXML, eError);
				throw new ExcepcionTipificada("Error al generar el archivo XML para esa lista de folios.", eError, sTicket);
			}

			return sContenidoXml;
		}

		/// <summary>
		/// Genera una cadena en formato XML para la generación del documento de formato de pago
		/// </summary>
		/// <param name="listaDeFolios">Lista de folios</param>
		/// <returns>Cadena en formato XML</returns>
		private string ObtenerFormatoPago(List<string> listaDeFolios)
		{
			string sContenidoXml = string.Empty;
			FormatoPago documentos = new DalGeneraTemplate().ObtenerFormatoPago(listaDeFolios);
			sContenidoXml = MetodosComunes.Serializa(documentos);
			return sContenidoXml;
		}

		/// <summary>
		/// Genera una cadena en formato XML para la generación del documento de confirmación de transacciones virtuales
		/// </summary>
		/// <param name="listaDeFolios">Lista de folios</param>
		/// <returns>Cadena en formato XML</returns>
		private string ObtenerFormatoConfirmacionDeTransaccionesVirtuales(List<string> listaDeFolios)
		{
			string sXml = string.Empty;
			FormatoConfirmacionDeTransaccionesVirtuales documentos = new DalGeneraTemplate().ObtenerFormatoConfirmacionDeTransaccionesVirtuales(listaDeFolios);
			sXml = MetodosComunes.Serializa(documentos);
			return sXml;
		}

		/// <summary>
		/// nera una cadena en formato XML para la generación del documento de confirmación de movimientos contables
		/// </summary>
		/// <param name="listaDeFolios">Lista de folios</param>
		/// <returns>Cadena en formato XML</returns>
		private string ObtenerFormatoConfirmacionDeMovimientosContables(List<string> listaDeFolios)
		{
			string sXml = string.Empty;
			FormatoConfirmacionDeMovimientosContables documentos = new DalGeneraTemplate().ObtenerFormatoConfirmacionDeMovimientosContables(listaDeFolios);
			sXml = MetodosComunes.Serializa(documentos);
			return sXml;
		}

		/// <summary>
		/// nera una cadena en formato XML para la generación del documento de confirmación de rectificaciones contables
		/// </summary>
		/// <param name="listaDeFolios">Lista de folios</param>
		/// <returns>Cadena en formato XML</returns>
		private string ObtenerFormatoConfirmacionDeRectificacionesContables(List<string> listaDeFolios)
		{
			string sXml = string.Empty;
			FormatoConfirmacionDeRectificacionesContables documentos = new DalGeneraTemplate().ObtenerFormatoConfirmacionDeRectificacionesContables(listaDeFolios);
			sXml = MetodosComunes.Serializa(documentos);
			return sXml;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="cadena"></param>
		/// <returns></returns>
		private string RemplazaCaracteresEspeciales(string cadena)
		{
			return cadena.Replace("&amp;", "&amp;#038;");
		}
	}
}