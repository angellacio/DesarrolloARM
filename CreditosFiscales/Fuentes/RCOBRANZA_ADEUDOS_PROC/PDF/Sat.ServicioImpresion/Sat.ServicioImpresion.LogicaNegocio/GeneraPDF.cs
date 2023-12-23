//@(#)CREDITOSFISCALES(W:71950:Sat.ServicioImpresion.LogicaNegocio:Sat.ServicioImpresion.LogicaNegocio.GeneraPDF:1:12/07/2013[Assembly:1.0:12/07/2013])
/*
 * Modificado por: Mario Escarpulli
 * Modificado el: 18/Junio/2019
 */

// Referencias de sistemas.
using System;
using System.IO;
using System.Text;

// Referencias externas.
using Fonet;
using Fonet.Render.Pdf;

// Referencias personalizadas.
using Sat.ServicioImpresion.Entidades.CodigosError;
using Sat.ServicioImpresion.Entidades.AccesoLogEventos;
using Sat.ServicioImpresion.LogicaNegocio.AccesoLogEventos;

namespace Sat.ServicioImpresion.LogicaNegocio
{
	/// <summary>
	/// Clase que contiene los metodos necesarios para generar un archivo representado en una arreglo de bytes
	/// </summary>
	public static class GeneraPDF
	{
		/// <summary>
		/// Método para generar un archivo representado en un arreglo de bytes de acuerdo a un template definido.
		/// </summary>
		/// <param name="strXmlDatos">Cadena de caracter en formato XML.</param>
		/// <param name="IdDocumento">Identificador único (ID) del documento o template a utilizar.</param>
		/// <returns>Arreglo de bytes</returns>
		public static byte[] GenerarArchivoPDF(string strXmlDatos, int IdDocumento)
		{
			try
			{
				string sRutaTemporal = Compartidos.MetodosCompartidos.ObtenerPathTemporal();
				string sNombrePdfTmp = Guid.NewGuid().ToString();
				string sRutaPdfTmp = string.Format("{0}{1}.pdf", sRutaTemporal, sNombrePdfTmp);

				Compartidos.MetodosCompartidos.ValidaXml(strXmlDatos, IdDocumento);
				Compartidos.MetodosCompartidos.VerificaNodoCodigoBarras(ref strXmlDatos, sRutaTemporal, sNombrePdfTmp);

				string sXmlFoPdf = Compartidos.MetodosCompartidos.GenerarCadenaFo(strXmlDatos, IdDocumento);
				CrearArchivoPdf(sXmlFoPdf, sRutaPdfTmp);
				return Compartidos.MetodosCompartidos.LeerArchivo(sRutaTemporal, sNombrePdfTmp, sRutaPdfTmp);
			}
			catch (ExcepcionTipificada etError)
			{
				throw etError;
			}
			catch (Exception eError)
			{
				string ticket = LogEventos.EscribirEntradaLog((int) EnumErroresImpresion.ErrorGenerarArchivo, eError, strXmlDatos);
				throw new ExcepcionTipificada("No se logró generar el archivo .pdf, por favor intentelo más tarde.", eError, ticket);
			}
		}

		/// <summary>
		/// Permite crear un archivo en formato PDF.
		/// </summary>
		/// <param name="psXmlFoPdf">Cadena de caracter con formato XML que contiene la información de la Línea de Captura.</param>
		/// <param name="psRutaArchivo">Ruta física donde se guardará el archivo generado.</param>
		private static void CrearArchivoPdf(string psXmlFoPdf, string psRutaArchivo)
		{
			try
			{
				if (File.Exists(psRutaArchivo))
					File.Delete(psRutaArchivo);

				using (FileStream outStream = File.Create(psRutaArchivo))
				{
					FonetDriver driver = FonetDriver.Make();
					var voOpciones = new PdfRendererOptions();
					var voFuente = new System.IO.FileInfo(System.Web.Hosting.HostingEnvironment.MapPath("~/Template/Menlo-Regular.ttf"));
					voOpciones.AddPrivateFont(voFuente);
					voOpciones.FontType = FontType.Subset;
					driver.Options = voOpciones;
					driver.Render(new MemoryStream(Encoding.UTF8.GetBytes(psXmlFoPdf.ToString())), outStream);
					outStream.Close();
				}
			}
			catch (Exception eError)
			{
				string sNumeroTicket = LogEventos.EscribirEntradaLog((int) EnumErroresImpresion.ErrorGuardarArchivo, eError, psXmlFoPdf);
				throw new ExcepcionTipificada("No se logró guardar el archivo en el directorio temporal.", eError, sNumeroTicket);
			}
		}
	}
}