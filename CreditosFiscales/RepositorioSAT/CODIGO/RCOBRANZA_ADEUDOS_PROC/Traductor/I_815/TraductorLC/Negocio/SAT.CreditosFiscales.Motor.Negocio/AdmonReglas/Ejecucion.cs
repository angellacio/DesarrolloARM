//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.Negocio:SAT.CreditosFiscales.Motor.Negocio.Ejecucion:1:12/07/2012[Assembly:1.0:12/07/2013])

// Referencias de sistema.
using System;
using System.IO;
using System.Xml;
using System.Linq;
using System.Xml.Xsl;

// Referencias personalizadas.
using SAT.CreditosFiscales.Motor.Utilidades;
using SAT.CreditosFiscales.Motor.Negocio.Mensajes;
using SAT.CreditosFiscales.Motor.Negocio.ReglasNet;
using SAT.CreditosFiscales.Motor.Entidades.AdmonRegla;
using SAT.CreditosFiscales.Motor.Entidades.Procesamiento;
using SAT.CreditosFiscales.Motor.AccesoDatos.AdmonReglas;

namespace SAT.CreditosFiscales.Motor.Negocio.AdmonReglas
{
	/// <summary>
	/// 
	/// </summary>
	public class Ejecucion
	{
		/// <summary>
		/// Aplica las reglas al documento de entrada.
		/// </summary>
		/// <param name="documentoEntrada">Documento de entrada.</param>
		/// <param name="reglas">Reglas que se aplicarán al documento.</param>        
		/// <param name="resultadoValidacion">Resultado de las validaciones.</param>
		public void AplicaReglas(string documentoEntrada, ReglasPorDocumento reglas, Respuesta<ResultadoReglas> resultadoValidacion)
		{
			CatalogoReglas catalogoReglas = null;
			ReglaBaseNet reglaBase = null;
			var documentoXml = new XmlDocument();

			if (reglas.ListaReglas.Where(rN => rN.EsNET == true).Count() > 0)
				catalogoReglas = new CatalogoReglas(reglas.DirectorioReglasNet);

			var voRespuestaNet = new RespuestaGenerica();

			reglas.ListaReglas.Select
			(
				r =>
					{
						if (resultadoValidacion.ListaErrores.Count == 0)
						{
							resultadoValidacion.Entidad.AgregaRegla(r.IdRegla, r.Descripcion, r.EsValidacion);

							if (r.EsNET)
							{
								reglaBase = catalogoReglas.ObtenerRegla(r.Xslt);

								documentoXml.LoadXml(resultadoValidacion.Entidad.Mensaje);
								voRespuestaNet = reglaBase.EjecutaRegla(documentoXml);
								resultadoValidacion.Entidad.Reglas[resultadoValidacion.Entidad.Reglas.Count - 1].FueExitosa = voRespuestaNet.EsExitoso;

								if (!voRespuestaNet.EsExitoso)
									resultadoValidacion.ListaErrores.AddRange(voRespuestaNet.ListaErrores);
							}
							else
								ejecutaXSLT(r, resultadoValidacion);
						}

						return r;
					}
			).ToList();

			resultadoValidacion.EsExitoso = resultadoValidacion.ListaErrores.Count == 0;
		}

		/// <summary>
		/// Ejecuta la transformación.
		/// </summary>
		/// <param name="reglaActual">Regla actual.</param>
		/// <param name="resultado">Resultado.</param>
		private void ejecutaXSLT(Regla reglaActual, Respuesta<ResultadoReglas> resultado)
		{
			var settings = new XsltSettings();
			settings.EnableScript = true;
			var respuestaXslt = new RespuestaXslt();
			bool seTransformo = false;

			try
			{
				respuestaXslt.Mensaje = TransformaXml(resultado.Entidad.Mensaje, reglaActual.Xslt);

				if (respuestaXslt.Mensaje != string.Empty)
				{
					seTransformo = true;
					resultado.Entidad.Reglas[resultado.Entidad.Reglas.Count - 1].FueExitosa = true;
				}
				else
				{
					seTransformo = false;
					resultado.Entidad.Reglas[resultado.Entidad.Reglas.Count - 1].FueExitosa = false;
					respuestaXslt.AgregaError("Transformación nula");
				}

				if (!reglaActual.EsValidacion)
					resultado.Entidad.Mensaje = respuestaXslt.Mensaje;
			}
			catch (Exception ex)
			{
				resultado.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.InicializaXSLT), ex, reglaActual.IdRegla);
			}

			if (!seTransformo)
			{
				resultado.Entidad.Reglas[resultado.Entidad.Reglas.Count - 1].Errores = respuestaXslt.ListaErrores;
				resultado.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.EjecutaTransformacion), reglaActual.IdRegla, respuestaXslt.ErroresEncontrados);
			}
		}


		/// <summary>
		/// Transforma un documento xml en base a un xslt
		/// </summary>
		/// <param name="xml">Documento a transformar o validar</param>
		/// <param name="xslt">Documento xslt a aplicar</param>
		/// <returns>Mensaje transformado</returns>
		public static string TransformaXml(string xml, string xslt)
		{
			// Create required readers for working with xml and xslt
			using (StringReader xsltInput = new StringReader(xslt))
			{
				var xmlInput = new StringReader(xml);
				var xsltReader = new XmlTextReader(xsltInput);
				var xmlReader = new XmlTextReader(xmlInput);

				// Create required writer for output
				var stringWriter = new StringWriter();
				var transformedXml = new XmlTextWriter(stringWriter);

				// Configura la transformación
				var settings = new XsltSettings();
				settings.EnableScript = true;

				// Create a XslCompiledTransform to perform transformation
				var xsltTransform = new XslCompiledTransform();

				try
				{
					xsltTransform.Load(xsltReader, settings, null);
					xsltTransform.Transform(xmlReader, transformedXml);
				}
				catch
				{
					throw;
				}

				return stringWriter.ToString();
			}
		}

		/// <summary>
		/// Lee el resultado de la transformación y lo asigna al objeto RespuestaXslt.
		/// </summary>
		/// <param name="tranformacion">Resultado de la transformación.</param>
		/// <returns>Respuesta de la transformación.</returns>
		private RespuestaXslt leeResultadoTransformacion(string tranformacion)
		{
			using (XmlReader xrLector = XmlReader.Create(new StringReader(tranformacion)))
			{
				var respuesta = new RespuestaXslt();
				xrLector.MoveToContent();

				while (!xrLector.EOF)
				{
					if (xrLector.NodeType == XmlNodeType.Element)
					{
						switch (xrLector.LocalName)
						{
							case "Documento":
								respuesta.Mensaje = xrLector.ReadInnerXml();
								break;

							case "EsExitoso":
								respuesta.EsExitoso = bool.Parse(xrLector.ReadInnerXml());
								break;

							case "Error":
								while (xrLector.Read() && !xrLector.LocalName.Equals("Descripcion")) ;
								respuesta.AgregaError(xrLector.ReadInnerXml());
								break;

							default:
								xrLector.Read();
								break;
						}
					}
					else
						xrLector.Read();
				}

				return respuesta;
			}
		}


		/// <summary>
		/// Obtiene todas las reglas relacionadas a un tipo de documento de pago y una aplicación.
		/// </summary>
		/// <param name="idAplicacion">Identificador único (ID) de la Aplicación que envía la petición.</param>
		/// <param name="idTipoDocPago">Identificador único (ID) del Tipo de Documento de Pago que se envía.</param>
		/// <param name="bAntesInsercion">True si las reglas son antes de aplicar la inserción a base de datos.</param>
		/// <param name="bEsSIAT"></param>
		/// <returns>Objeto de respuesta con las reglas encontradas.</returns>
		public Respuesta<ReglasPorDocumento> ConsultaReglas(short? idAplicacion, short? idTipoDocPago, bool bAntesInsercion, bool bEsSIAT)
		{
			var voRespuesta = new Respuesta<ReglasPorDocumento>();

			try
			{
				voRespuesta.Entidad = DalReglas.ConsultaRecursosValidacion(idAplicacion.Value, idTipoDocPago.Value, bAntesInsercion, bEsSIAT);

				if (voRespuesta.Entidad.ListaContratos == null || voRespuesta.Entidad.ListaContratos.Count == 0)
					voRespuesta.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.SinContratos));

				if (voRespuesta.Entidad.ListaReglas == null || voRespuesta.Entidad.ListaReglas.Count == 0)
					voRespuesta.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.SinReglas));
			}
			catch (Exception ex)
			{
				voRespuesta.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.ObtenerConfiguracion), ex);
			}

			voRespuesta.EsExitoso = voRespuesta.ListaErrores.Count == 0;

			return voRespuesta;
		}

		/// <summary>
		/// Inserta una regla relacionada a una aplicación y a un tipo de documento de pago.
		/// </summary>
		/// <param name="regla">Regla que se desea insertar.</param>
		public Guid InsertaRegla(Regla regla)
		{
			return DalReglas.InsertaRegla(regla.Descripcion, regla.Xslt, regla.EsValidacion);
		}
	}
}