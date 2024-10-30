//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.Negocio:SAT.CreditosFiscales.Motor.Negocio.ValidaXSD:1:12/07/2012[Assembly:1.0:12/07/2013])

// Referencias de sistema.
using System;
using System.IO;
using System.Xml;
using System.Linq;
using System.Text;
using System.Xml.Schema;
using System.Collections.Generic;

// Referencias personalizadas.
using SAT.CreditosFiscales.Motor.Entidades.Procesamiento;
using SAT.CreditosFiscales.Motor.Negocio.Mensajes;
using SAT.CreditosFiscales.Motor.Utilidades;

namespace SAT.CreditosFiscales.Motor.Negocio.Procesamiento
{
	/// <summary>
	/// Clase de validación XSD
	/// </summary>
	[Serializable]
	public class ValidaXSD
	{
		/// <summary>
		/// Lista de errores generados en la validación del esquema
		/// </summary>
		public List<string> ListaErroresEsquema { get; set; }

		/// <summary>
		/// Devuelve el número de Errores de un esquema
		/// </summary>
		/// <returns></returns>
		public int ErroresTotales()
		{
			return ListaErroresEsquema.Count();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="documento"></param>
		/// <param name="xsd"></param>
		/// <param name="NameSpace"></param>
		/// <returns></returns>
		public RespuestaGenerica ValidaEsquemaXSD(string documento, string xsd, string NameSpace)
		{
			RespuestaGenerica oRespuestaValidaXsd;
			try
			{
				oRespuestaValidaXsd = new RespuestaGenerica();
				var vXmlMensaje = new XmlDocument();
				vXmlMensaje.LoadXml(documento);
				this.ValidaEsquema(vXmlMensaje, xsd, ref oRespuestaValidaXsd);

				if (ErroresTotales() > 0)
				{
					oRespuestaValidaXsd.EsExitoso = false;
					//Asignar Errores a Respuesta Genérica
					foreach (string errorEsquema in ListaErroresEsquema)
						oRespuestaValidaXsd.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.ValidacionEsquema), new Exception(errorEsquema));
				}
				else
					oRespuestaValidaXsd.EsExitoso = true;
			}
			catch (Exception eError)
			{
				oRespuestaValidaXsd = new RespuestaGenerica();
				oRespuestaValidaXsd.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.ValidacionEsquema), eError);
			}
			return oRespuestaValidaXsd;
		}

		/// <summary>
		/// Valida documento xml contra esquema xsd
		/// </summary>
		/// <param name="Xml">Xml a validar</param>
		/// <param name="Xsd">Esquema Xsd</param>
		/// <param name="respuestaValidaXsd">respuesta de validación</param>
		private void ValidaEsquema(XmlDocument Xml, string Xsd, ref RespuestaGenerica respuestaValidaXsd)
		{
			try
			{
				if (Xml.FirstChild.NodeType == XmlNodeType.XmlDeclaration)
				{
					string sEncoding = ((XmlDeclaration) Xml.FirstChild).Encoding;
					if (!string.IsNullOrEmpty(sEncoding))
						Xml.InnerXml = Xml.InnerXml.Replace(sEncoding, Constantes.ValidEncondingXML);
				}

				var encoding = new UTF8Encoding();
				using (MemoryStream stream = new MemoryStream(encoding.GetBytes(Xml.InnerXml)))
				{
					// Se carga el esquema a utilizar               
					XmlReaderSettings settings = CargaEsquemas(Xsd);
					settings.ValidationEventHandler += new ValidationEventHandler(this.ValidacionEsquema);

					// Se crea la lista que contendrá los errores de esquema
					this.ListaErroresEsquema = new List<string>();

					var documento = new XmlDocument();
					documento.Load(XmlReader.Create(stream, settings));
				}

			}
			catch (Exception err)
			{
				respuestaValidaXsd.EsExitoso = false;
				respuestaValidaXsd.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.ValidacionEsquema), err);
			}
		}

		/// <summary>
		/// Validación de esquema xsd
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ValidacionEsquema(object sender, ValidationEventArgs e)
		{
			this.ListaErroresEsquema.Add(string.Format("{0} Línea: {1}", e.Message, e.Exception.LineNumber));
		}

		/// <summary>
		/// Carga esquema xsd
		/// </summary>
		/// <param name="xsd"></param>
		/// <returns></returns>
		private static XmlReaderSettings CargaEsquemas(string xsd)
		{
			// Se obtiene el nombre del esquema desde la tabla de configuraciones.
			try
			{
				using (StringReader osrLeerEsquema = new StringReader(xsd))
				{
					var voEsquemaSet = new XmlSchemaSet();

					voEsquemaSet.Add(null, XmlReader.Create(osrLeerEsquema));
					var settings = new XmlReaderSettings()
									{
										ValidationType = ValidationType.Schema
										, Schemas = voEsquemaSet
										, DtdProcessing = DtdProcessing.Parse
									};

					return settings;
				}
			}
			catch
			{
				throw;
			}
		}
	}
}