//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.Entidades:SAT.CreditosFiscales.Motor.Entidades.MetodosComunes:1:12/07/2012[Assembly:1.0:12/07/2013])

// Referencias de sistema.
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

// Referencias personalizadas.
using SAT.CreditosFiscales.Motor.Entidades.DatosProcesamiento;

namespace SAT.CreditosFiscales.Motor.Entidades.Herramientas
{
	/// <summary>
	/// Clase de métodos comunes utilizadas de toda la solución
	/// </summary>
	public static class MetodosComunes
	{
		/// <summary>
		/// Método que deserializa un objeto
		/// </summary>
		/// <typeparam name="T">Objeto de retorno</typeparam>
		/// <param name="strXml">string con XML del objeto serializado</param>
		/// <returns></returns>
		public static T Deserializa<T>(string strXml)
		{
			return (T) Deserializa(strXml, typeof(T));
		}

		/// <summary>
		/// Método que deserializa la información por tipo de objeto.
		/// </summary>
		/// <param name="strXml">Cadena xml de texto serializado.</param>
		/// <param name="type">Tipo de objeto al que será deserializado el xml de entrada.</param>
		/// <returns>Objeto deserializado.</returns>
		public static object Deserializa(string strXml, Type type)
		{
			var serializer = new XmlSerializer(type);
			using (StringReader rdr = new StringReader(strXml))
			{
				return serializer.Deserialize(rdr);
			}
		}

		/// <summary>
		/// Método que deserializa texto en archivos para crear una entidad de créditos fiscales.
		/// </summary>
		/// <param name="rutaArchivo">Ubicación del archivo dentro del disco.</param>
		/// <returns>Entidad de créditos fiscales deserializada.</returns>
		public static DatosProcesamiento.CreditosFiscales DeserializaObjectoPorArchivo(string rutaArchivo)
		{
			DatosProcesamiento.CreditosFiscales creditosFiscales;
			try
			{
				var serializer = new XmlSerializer(typeof(Entidades.DatosProcesamiento.CreditosFiscales));
				var fileStream = new FileStream(rutaArchivo, FileMode.Open);
				creditosFiscales = (DatosProcesamiento.CreditosFiscales) serializer.Deserialize(fileStream);
			}
			catch
			{
				throw;
			}
			return creditosFiscales;
		}

		/// <summary>
		/// Método que deserializa el objeto por Xml.
		/// </summary>
		/// <param name="psXml">Xml de objeto serializado.</param>
		/// <returns>Entidad de créditos fiscales.</returns>
		public static DatosProcesamiento.CreditosFiscales DeserializaObjectoPorXml(string psXml)
		{
			DatosProcesamiento.CreditosFiscales oCreditosFiscales;
			try
			{
				var vxsSerializador = new XmlSerializer(typeof(DatosProcesamiento.CreditosFiscales));
				using (var vsrXml = new StringReader(psXml))
				{
					oCreditosFiscales = (DatosProcesamiento.CreditosFiscales) vxsSerializador.Deserialize(vsrXml);
				}
			}
			catch
			{
				throw;
			}
			return oCreditosFiscales;
		}

		/// <summary>
		/// Metodo que deserializa el objeto por xml
		/// </summary>
		/// <param name="psXml">Xml de objeto serializado.</param>
		/// <returns>Entidad de créditos fiscales.</returns>
		public static DatosProcesamiento.CreditosFiscalesSIAT DeserializaXmlSIAT(string psXml)
		{
			DatosProcesamiento.CreditosFiscalesSIAT oCreditosFiscales;
			try
			{
				var vxsSerializador = new XmlSerializer(typeof(DatosProcesamiento.CreditosFiscalesSIAT));
				using (var vsrXml = new StringReader(psXml))
				{
					oCreditosFiscales = (DatosProcesamiento.CreditosFiscalesSIAT) vxsSerializador.Deserialize(vsrXml);
				}
			}
			catch
			{
				throw;
			}
			return oCreditosFiscales;
		}

		/// <summary>
		/// Serializa la respuesta de línea de captura hacia el aplicativo que realizó la petición.
		/// </summary>
		/// <param name="respuestaLC">Entidad que contiene la información de respuesta de el servicio de D y P.</param>
		/// <returns>Texto serializado de la entidad de respuesta.</returns>
		public static string SerializaRespuestaLC(RespuestaLC respuestaLC)
		{
			string sRespuesta;

			try
			{
				var serializadorReporte = new XmlSerializer(respuestaLC.GetType());
				using (var streamReporte = new MemoryStream())
				{
					serializadorReporte.Serialize(streamReporte, respuestaLC);
					streamReporte.Seek(0, SeekOrigin.Begin);

					var docRespuesta = new XmlDocument();
					docRespuesta.Load(streamReporte);

					sRespuesta = docRespuesta.InnerXml;
				}
			}
			catch
			{
				throw;
			}

			return sRespuesta;

		}

		/// <summary>
		/// Serializa un objeto que pueda ser serializable
		/// </summary>
		/// <param name="objetoSeralizable">Objeto a serializar</param>
		/// <returns>string serializada</returns>
		public static string SerlializaObjeto(object objetoSeralizable)
		{
			var stream = new MemoryStream();
			var serializer = new XmlSerializer(objetoSeralizable.GetType());
			serializer.Serialize(stream, objetoSeralizable);
			if (stream.Position > 0)
			{
				stream.Seek(0, SeekOrigin.Begin);
			}

			using (var streamReader = new StreamReader(stream, Encoding.UTF8))
			{
				return streamReader.ReadToEnd();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="conceptoHijo"></param>
		/// <returns></returns>
		public static string ObtenerMarcaRecargo(CreditosFiscalesAgrupadorConceptosHijo conceptoHijo)
		{
			string result = string.Empty;
			foreach (var marca in conceptoHijo.Marcas.Where(marca => marca.TipoMarca == "Recargo"))
			{
				result = marca.Text.ToString();
				break;
			}
			return result;
		}

		/// <summary>
		/// Serializa el objeto Créditos Fiscales.
		/// </summary>
		/// <param name="objetoSeralizable">Objeto Créditos Fiscales a serializar.</param>
		/// <returns>Cadena de caracter serializado.</returns>
		public static string SerlializaCreditosFiscales(DatosProcesamiento.CreditosFiscales objetoSeralizable)
		{
			var vsmMemoria = new MemoryStream();
			var serializer = new XmlSerializer(objetoSeralizable.GetType());
			serializer.Serialize(vsmMemoria, objetoSeralizable);

			if (vsmMemoria.Position > 0)
				vsmMemoria.Seek(0, SeekOrigin.Begin);

			using (var vsrLector = new StreamReader(vsmMemoria, Encoding.UTF8))
			{
				return vsrLector.ReadToEnd();
			}
		}
	}
}