/*
 * Modificado por: Mario Escarpulli
 * Modificado el: 18/Junio/2019
*/

// Referencias de sistemas.
using System;
using System.IO;
using System.Xml;
using System.Drawing;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Reflection;
using System.Drawing.Text;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

// Referencias externas.
using iTextSharp.text.pdf;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;

// Referencias personalizadas.
using Sat.ServicioImpresion.AccesoDatos.Catalogos;
using Sat.ServicioImpresion.Entidades.CodigosError;
using Sat.ServicioImpresion.Entidades.AccesoLogEventos;
using Sat.ServicioImpresion.LogicaNegocio.AccesoLogEventos;

namespace Sat.ServicioImpresion.LogicaNegocio.Compartidos
{
	/// <summary>
	/// Clase pública estática que permite compartir métodos para generar archivos de tipo PDF y archivos de tipo imagen.
	/// </summary>
	public static class MetodosCompartidos
	{
		/// <summary>
		/// Obtiene la ruta temporal donde se guardarán los archivos generados por la aplicación.
		/// </summary>
		/// <returns>Cadena de caracter con la ruta temporal.</returns>
		public static string ObtenerPathTemporal()
		{
			string sRutaTemporal = string.Empty;
			try
			{
				object objeto = new DalApplicationSettings().RecuperaConfiguracion("PathTemplateTemporal");
				sRutaTemporal = objeto.ToString();
				if (!Directory.Exists(sRutaTemporal))
					Directory.CreateDirectory(sRutaTemporal);
			}
			catch (Exception ex)
			{
				string ticket = LogEventos.EscribirEntradaLog((int) EnumErroresImpresion.ErrorDirectorioNoExiste, ex);
				throw new ExcepcionTipificada("La directorio temporal no existe.", ex, ticket);
			}

			return sRutaTemporal;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="strXmlDatos"></param>
		/// <param name="IdDocumento"></param>
		/// <returns></returns>
		public static string GenerarCadenaFo(string strXmlDatos, int IdDocumento)
		{
			var xmlDatos = new XmlDocument();
			xmlDatos.LoadXml(strXmlDatos);
			return Template.LlenaTemplate(xmlDatos, IdDocumento);
		}

		/// <summary>
		/// Permite leer el contenido de un archivo binario.
		/// </summary>
		/// <param name="psRutaTemporal"></param>
		/// <param name="psNombreArchivo"></param>
		/// <param name="psRutaArchivo"></param>
		/// <returns></returns>
		public static byte[] LeerArchivo(string psRutaTemporal, string psNombreArchivo, string psRutaArchivo)
		{
			byte[] byContenido = null;

			if (File.Exists(psRutaArchivo))
			{
				using (FileStream fsArchivo = new FileInfo(psRutaArchivo).OpenRead())
				{
					byContenido = new byte[fsArchivo.Length];
					fsArchivo.Read(byContenido, 0, Convert.ToInt32(fsArchivo.Length));
					fsArchivo.Close();
				}
			}
			else
				throw new ApplicationException("No se pudo tener acceso al archivo de resolución.");

			if (File.Exists(psRutaArchivo))
			{
				string sArchivos = string.Format("{0}*.*", psNombreArchivo);
				foreach (FileInfo fiArchivo in new DirectoryInfo(psRutaTemporal).GetFiles(sArchivos))
					fiArchivo.Delete();
			}

			return byContenido;
		}

		/// <summary>
		/// Valida una cadena de caracter del tipo XML mediante un XSD.
		/// </summary>
		/// <param name="psXml">Cadena de caracter de tipo XML.</param>
		/// <param name="piIdDocumento">Identificador del documento o template.</param>
		public static void ValidaXml(string psXml, int piIdDocumento)
		{
			try
			{
				string xsdValidacion = Template.ObtieneTemplateValidacion(piIdDocumento);
				if (string.IsNullOrEmpty(xsdValidacion))
					throw new Exception("No existe un esquema de validación para este documento.");

				var vsrLectorXsd = new StringReader(xsdValidacion);
				XmlReader xsdLector = XmlReader.Create(vsrLectorXsd);
				var vxsEsquema = new XmlSchemaSet();
				vxsEsquema.Add("", xsdLector);

				var vsrLectorXml = new StringReader(psXml);
				XDocument xdDocumento = XDocument.Load(vsrLectorXml);

				bool bConError = false;
				string sMensajeExcepcion = string.Empty;
				xdDocumento.Validate(vxsEsquema, (o, e) =>
				{
					sMensajeExcepcion = e.Message;
					bConError = true;
				});

				if (bConError)
					throw new Exception(sMensajeExcepcion);
			}
			catch (Exception eError)
			{
				string sTicket = LogEventos.EscribirEntradaLog((int) EnumErroresImpresion.ErrorXMLInvalido, eError, psXml);
				throw new ExcepcionTipificada("El XML de entrada no es válido para este documento.", eError, sTicket);
			}
		}

		/// <summary>
		/// Verifica si la cadena de caracter de tipo XML recibido contiene un elemento con el nombre Codigo de Barras, en caso de existir genera una imagen 
		/// del código de barras de su valor.
		/// </summary>
		/// <param name="psXml">Cadena de caracter con formato XML.</param>
		/// <param name="psRutaTemporal">Ruta Temporal dónde se guarda el resultado.</param>
		/// <param name="psNombreArchivoTmp">Nombre del archivo temporal.</param>
		public static void VerificaNodoCodigoBarras(ref string psXml, string psRutaTemporal, string psNombreArchivoTmp)
		{
			string sRutaImagenTemporal = String.Format("{0}{1}", psRutaTemporal, psNombreArchivoTmp);
			var vxdDocumento = new XmlDocument();
			vxdDocumento.LoadXml(psXml);
			XmlNodeList xnlElementos = vxdDocumento.GetElementsByTagName("CodigoBarrasDetalle");
			for (int i = 0; i < xnlElementos.Count; i++)
			{
				XmlNode nodo = xnlElementos[i].SelectSingleNode("CodigoBarras");
				// Obtenemos el codigo a convertir
				string sCodigo = nodo.InnerText.Trim();
				//sCodigo = System.Text.RegularExpressions.Regex.Replace(codigo, " ", @"\u0194");

				// Agregamos las rutas de los códigos a los nodos del XML de entrada
				XmlNode nodoImagenCB = xnlElementos[i].SelectSingleNode("ImagenCB");
				nodoImagenCB.InnerText = GeneraCodigoBarras(sCodigo, sRutaImagenTemporal + "-" + i);

				XmlNode nodoImagenQR = xnlElementos[i].SelectSingleNode("ImagenQR");
				nodoImagenQR.InnerText = GeneraCodigoQR(sCodigo, sRutaImagenTemporal + "-" + i);
			}

			psXml = vxdDocumento.InnerXml.ToString();
		}

		/// <summary>
		/// Genera una imagen de código de barras Code 128.
		/// </summary>
		/// <param name="psCodigo">Texto a incrustar en la imagen.</param>
		/// <param name="sRutaTemporal">Ruta y Nombre temporal de la imagen.</param>
		/// <returns>Cadena de caracter con la ruta dónde se guarda la imagen.</returns>
		private static string GeneraCodigoBarras(string psCodigo, string sRutaTemporal)
		{
			string sRutaImagen = string.Format("{0}CB.png", sRutaTemporal);
			var vbcCodigoBarra = new Barcode128();
			vbcCodigoBarra.CodeType = Barcode.CODE128;
			vbcCodigoBarra.ChecksumText = true;
			vbcCodigoBarra.GenerateChecksum = true;
			vbcCodigoBarra.Code = psCodigo;
			var vbmCodigoBarra = new System.Drawing.Bitmap(vbcCodigoBarra.CreateDrawingImage(System.Drawing.Color.Black, System.Drawing.Color.White));
			vbmCodigoBarra.Save(sRutaImagen, System.Drawing.Imaging.ImageFormat.Png);

			return sRutaImagen;
		}

		/// <summary>
		/// Genera una imagen de código QR.
		/// </summary>
		/// <param name="psCodigo">Texto a incrustar en la imagen.</param>
		/// <param name="sRutaTemporal">Ruta y Nombre temporal de la imagen.</param>
		/// <returns>Cadena de caracter con la ruta dónde se guarda la imagen.</returns>
		private static string GeneraCodigoQR(string psCodigo, string sRutaTemporal)
		{
			string sRutaImagen = String.Format("{0}QR.png", sRutaTemporal);
			var qrCodificador = new QrEncoder(ErrorCorrectionLevel.H);
			QrCode qrCodigo = qrCodificador.Encode(psCodigo);

			var vgrCrearGrafico = new GraphicsRenderer(new FixedModuleSize(1, QuietZoneModules.Two), Brushes.Black, Brushes.White);
			using (MemoryStream msCodigoQR = new MemoryStream())
			{
				vgrCrearGrafico.WriteToStream(qrCodigo.Matrix, ImageFormat.Png, msCodigoQR);
				var vbmCodigoQR = new Bitmap(msCodigoQR);
				vbmCodigoQR.Save(sRutaImagen, System.Drawing.Imaging.ImageFormat.Png);
			}
			return sRutaImagen;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="format"></param>
		/// <returns></returns>
		public static ImageCodecInfo GetEncoder(ImageFormat format)
		{
			ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
			foreach (var codec in codecs)
			{
				if (codec.FormatID == format.Guid)
					return codec;
			}
			return null;
		}
	}
}