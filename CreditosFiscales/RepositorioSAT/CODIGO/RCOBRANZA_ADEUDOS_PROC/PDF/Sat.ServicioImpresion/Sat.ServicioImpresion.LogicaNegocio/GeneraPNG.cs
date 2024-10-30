/*
 * ----------------------------------------
 * Creado por: Mario Escarpulli 
 * Creado: 15/Junio/2019
 * NOTAS: Esta clase se creó con base al Requerimiento de Servicio (RES)
 *        CZA_CREDFIS: Nuevo Servicio en Motor Traductor para la entrega de sección de línea de captura.
 * ----- Historial de actualizaciones -----
 * Actualizado por: Mario Escarpulli
 * Actualizado el: 20/Junio/2019
 * ----------------------------------------
*/

// Referencias de sistemas.
using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;
using System.Drawing.Text;
using System.Web.Hosting;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

// Referencias personalizadas.
using Sat.ServicioImpresion.Entidades.CodigosError;
using Sat.ServicioImpresion.Entidades.AccesoLogEventos;
using Sat.ServicioImpresion.LogicaNegocio.AccesoLogEventos;
using Sat.ServicioImpresion.LogicaNegocio.Compartidos;

namespace Sat.ServicioImpresion.LogicaNegocio
{
	/// <summary>
	/// Clase que contiene los métodos necesarios para generar un archivo de imagen representado en una arreglo de bytes.
	/// </summary>
	public static class GeneraPNG
	{
		/// <summary>
		/// Método para generar un archivo representado en un arreglo de bytes de acuerdo a un template definido.
		/// </summary>
		/// <param name="psXmlDatos">Cadena de caracter en formato XML.</param>
		/// <param name="piIdDocumento">Identificador único (ID) del documento o template a utilizar.</param>
		/// <returns>Arreglo de bytes.</returns>
		public static byte[] GenerarArchivoPng(string psXmlDatos, int piIdDocumento)
		{
			try
			{
				// Validando la información de la cadena de caracter en formato XMl.
				Compartidos.MetodosCompartidos.ValidaXml(psXmlDatos, piIdDocumento);
				Compartidos.MetodosCompartidos.VerificaNodoCodigoBarras(ref psXmlDatos, Compartidos.MetodosCompartidos.ObtenerPathTemporal(), Guid.NewGuid().ToString());

				return CrearArchivoPng(psXmlDatos, piIdDocumento);
			}
			catch (ExcepcionTipificada etError)
			{
				throw etError;
			}
			catch (Exception eError)
			{
				string sNumeroDeTicket = LogEventos.EscribirEntradaLog((int) EnumErroresImpresion.ErrorGenerarArchivo, eError, psXmlDatos);
				throw new ExcepcionTipificada("No se logró generar el archivo de imagen, por favor intentelo más tarde.", eError, sNumeroDeTicket);
			}
		}

		/// <summary>
		/// Permite crear un archivo de imagen en formato PNG.
		/// </summary>
		/// <param name="psXmlDatos">Cadena de caracter con formato XML que contiene la información de la Línea de Captura.</param>
		/// <param name="piIdDocumento">Identificador único (ID) del documento o template a utilizar.</param>
		/// <returns>Arreglo de bytes que contiene la imagen de la Línea de Captura.</returns>
		private static byte[] CrearArchivoPng(string psXmlDatos, int piIdDocumento)
		{
			try
			{
				// Ruta y nombre del archivo.
				string sRutaImagenTemporal = Compartidos.MetodosCompartidos.ObtenerPathTemporal();

				// Extrayendo los valores requeridos para generar el archivo de imagen.
				var vxdDocumentoXml = new XmlDocument();
				vxdDocumentoXml.LoadXml(psXmlDatos);
				// Obteniendo los valores requeridos.
				string sRfcContribuyente = vxdDocumentoXml.SelectSingleNode(xpath: "Documentos/LC/RFC").InnerText.Trim();
				string sFolioLineaCaptura = vxdDocumentoXml.SelectSingleNode(xpath: "Documentos/LC/FolioLC").InnerText.Replace(" ", string.Empty).Trim();
				string sFechaDeEmision = vxdDocumentoXml.SelectSingleNode(xpath: "Documentos/LC/FechaYHora").InnerText.Replace(" / ", "/");
				string sNumeroLineaCaptura = vxdDocumentoXml.SelectSingleNode(xpath: "Documentos/LC/NumLC").InnerText.Trim();
				string sImporteTotalAPagar = vxdDocumentoXml.SelectSingleNode(xpath: "Documentos/LC/ImporteAPagarT").InnerText.Trim();
				string sFechaVigencia = vxdDocumentoXml.SelectSingleNode(xpath: "Documentos/LC/FechaVigencia").InnerText.Replace(" / ", "/").Trim();
				string sLeyenda = vxdDocumentoXml.SelectSingleNode(xpath: "Documentos/LC/Leyenda").InnerText.Trim();
				string sNumeroCodigoBarra = vxdDocumentoXml.SelectSingleNode(xpath: "Documentos/LC/CodigoBarrasDetalle/CodigoBarras").InnerText.Trim();
				string sCodigoBarraRutaImagen = vxdDocumentoXml.SelectSingleNode(xpath: "Documentos/LC/CodigoBarrasDetalle/ImagenCB").InnerText.Trim();
				string sCodigoQRRutaImagen = vxdDocumentoXml.SelectSingleNode(xpath: "Documentos/LC/CodigoBarrasDetalle/ImagenQR").InnerText.Trim();

				// Ruta y nombre del archivo.
				string sNombreImagen = sRfcContribuyente + "_" + sNumeroLineaCaptura.Replace(" ", string.Empty).Trim();
				string sRutaCompletaImagenLC = string.Format("{0}{1}.png", sRutaImagenTemporal, sNombreImagen);

				var bmArchivoImagen = new Bitmap(700, 235);
				bmArchivoImagen.SetResolution(95.0f, 100.0f);
				using (Graphics gpArchivo = Graphics.FromImage(bmArchivoImagen))
				{
					// Configuración de Fuentes
					var vffArial = new FontFamily("Arial");
					var vffTRoman = new FontFamily("Times New Roman");
					var vpfMenlo = new PrivateFontCollection();
					vpfMenlo.AddFontFile(HostingEnvironment.MapPath("~/Template/Menlo-Regular.ttf"));

					var vfFuenteArialRegular = new Font(vffArial, 7f, FontStyle.Regular);
					var vfFuenteArialRegularNegrita = new Font(vffArial, 7f, FontStyle.Bold);
					var vfFuenteArialMediana = new Font(vffArial, 8, FontStyle.Regular);
					var vfFuenteArialNormal = new Font(vffArial, 10f, FontStyle.Regular);
					var vfFuenteArialNormalNegrita = new Font(vffArial, 10f, FontStyle.Bold);
					var vfFuenteArialGrande = new Font(vffArial, 12f, FontStyle.Regular);
					var vfFuenteArialGrandeNegrita = new Font(vffArial, 12f, FontStyle.Bold);
					var vfFuenteTRomanNormal = new Font(vffArial, 10f, FontStyle.Regular);
					var vfFuenteTRomanNegrita = new Font(vffArial, 12f, FontStyle.Bold);
					var vfFuenteMenloNormalNegrita = new Font((FontFamily) vpfMenlo.Families[0], 12f, FontStyle.Bold);

					// Color de fuente.
					var vsbColorFuente = new SolidBrush(Color.FromArgb(0, 0, 0));

					// Estableciendo los formatos para la alineación de textos.
					var sfFormatearTexto = new StringFormat();
					sfFormatearTexto.Trimming = StringTrimming.Word;
					var sfFormatearTextoCentrado = new StringFormat();
					sfFormatearTextoCentrado.Alignment = StringAlignment.Center;
					var sfFormatearTextoDerechaIzquierda = new StringFormat();
					sfFormatearTextoDerechaIzquierda.Alignment = StringAlignment.Far;

					// Configuración del gráfico.
					gpArchivo.SmoothingMode = SmoothingMode.HighQuality;
					gpArchivo.TextRenderingHint = TextRenderingHint.AntiAlias;
					gpArchivo.CompositingQuality = CompositingQuality.HighQuality;
					gpArchivo.InterpolationMode = InterpolationMode.HighQualityBilinear;
					gpArchivo.PixelOffsetMode = PixelOffsetMode.HighQuality;
					gpArchivo.Clear(Color.Transparent);

					// Pintando la información que debe mostrarse en el gráfico.
					gpArchivo.DrawString("Número de documento de la LC:", vfFuenteArialRegular, vsbColorFuente, new PointF(0f, 0f));
					gpArchivo.DrawString(sFolioLineaCaptura, vfFuenteArialRegularNegrita, vsbColorFuente, new PointF(142f, 0f));
					gpArchivo.DrawString("Fecha y hora de emisión:", vfFuenteArialRegular, Brushes.Black, new PointF(505f, 0f));
					gpArchivo.DrawString(sFechaDeEmision, vfFuenteArialRegularNegrita, Brushes.Black, new RectangleF(0f, 0f, 700f, 20f), sfFormatearTextoDerechaIzquierda);
					gpArchivo.DrawString("SECCIÓN LÍNEA DE CAPTURA", vfFuenteTRomanNegrita, Brushes.Black, new RectangleF(0f, 20f, 690f, 30f), sfFormatearTextoCentrado);
					gpArchivo.DrawString("El importe deberá ser pagado en las Instituciones de Crédito autorizadas, utilizando para tal efecto la línea de captura que se indica a continuación:", vfFuenteArialNormal, vsbColorFuente, new RectangleF(10f, 50f, 690f, 30f), sfFormatearTexto);
					gpArchivo.DrawString("Línea de Captura:", vfFuenteArialNormal, vsbColorFuente, new RectangleF(10f, 100f, 60f, 30f), sfFormatearTexto);
					gpArchivo.DrawString(sNumeroLineaCaptura, vfFuenteMenloNormalNegrita, Brushes.Black, new PointF(103f, 108f));
					gpArchivo.DrawString("Importe total a pagar:", vfFuenteArialNormal, vsbColorFuente, new RectangleF(420f, 100f, 90f, 30f), sfFormatearTexto);
					gpArchivo.DrawString(sImporteTotalAPagar, vfFuenteArialGrandeNegrita, Brushes.Black, new RectangleF(0f, 108f, 690f, 40f), sfFormatearTextoDerechaIzquierda);
					gpArchivo.DrawString("Vigente hasta:", vfFuenteTRomanNormal, vsbColorFuente, new PointF(10f, 150f));
					gpArchivo.DrawString(sFechaVigencia, vfFuenteArialNormalNegrita, vsbColorFuente, new PointF(103f, 150f));
					gpArchivo.DrawString(sLeyenda, vfFuenteArialMediana, vsbColorFuente, new RectangleF(0f, 150f, 690f, 30f), sfFormatearTextoDerechaIzquierda);

					// Marco
					gpArchivo.DrawRectangle(new Pen(Color.Black, 1), new Rectangle(0, 11, 699, 160));

					// Incrustrando el Código de Barra y Código QR 
					Image imgCodigoBarra = Image.FromFile(sCodigoBarraRutaImagen);
					gpArchivo.DrawImage(imgCodigoBarra, 10f, 180f, 300f, 40f);
					gpArchivo.DrawString(sNumeroCodigoBarra, vfFuenteArialMediana, vsbColorFuente, new RectangleF(10f, 220f, 300f, 30f), sfFormatearTextoCentrado);

					Image imgCodigoQR = Image.FromFile(sCodigoQRRutaImagen);
					gpArchivo.DrawImage(imgCodigoQR, 640f, 180f, 50f, 50f);

					// Cerrando objetos
					vsbColorFuente.Dispose();
				}

				// Guardando en memoria el archivo de imagen en formato .PNG.
				byte[] byArchivo = null;
				using (MemoryStream msArchivo = new MemoryStream())
				{
					bmArchivoImagen.Save(msArchivo, ImageFormat.Png);
					byArchivo = msArchivo.ToArray();
				}

				// Liberando de memoria
				bmArchivoImagen.Dispose();

				return byArchivo;
			}
			catch (Exception eError)
			{
				string sNumeroDeTicket = LogEventos.EscribirEntradaLog((int) EnumErroresImpresion.ErrorGuardarArchivo, eError, psXmlDatos);
				throw new ExcepcionTipificada("No se logró guardar el archivo de imagen en el directorio temporal.", eError, sNumeroDeTicket);
			}
		}
	}
}