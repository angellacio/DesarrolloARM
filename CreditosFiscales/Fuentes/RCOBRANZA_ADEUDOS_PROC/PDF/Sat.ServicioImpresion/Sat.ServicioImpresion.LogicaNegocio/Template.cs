//@(#)CREDITOSFISCALES(W:71950:Sat.ServicioImpresion.LogicaNegocio:Sat.ServicioImpresion.LogicaNegocio.Template:1:12/07/2013[Assembly:1.0:12/07/2013])

// Referencias de sistemas.
using System;
using System.Xml;
using System.Text;
using System.Collections.Generic;

// Referencias personalizadas.
using Sat.ServicioImpresion.AccesoDatos;

namespace Sat.ServicioImpresion.LogicaNegocio
{
	/// <summary>
	/// Clase con los métodos requeridos para poblar un template
	/// </summary>
	public static class Template
	{
		/// <summary>
		/// Método para poblar un template con los datos de un xml
		/// </summary>
		/// <param name="datos">Xml con los datos requeridos para poblar el template</param>
		/// <param name="IdDocumento">Identificador del documento o template</param>
		/// <returns>Archivo generado</returns>
		public static string LlenaTemplate(XmlDocument datos, int IdDocumento)
		{
			XmlNode root = datos.DocumentElement;
			var template = new StringBuilder(LlenaSeccionConfig(root, IdDocumento));
			string carpetaImagenes = root.Attributes["template"].InnerXml;
			string pathImages = string.Format("{0}\\Images\\{1}", System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath, carpetaImagenes);
			template.Replace("$RutaLogos", pathImages);

			return template.ToString();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="seccion"></param>
		/// <param name="IdDocumento"></param>
		/// <returns></returns>
		private static string LlenaSeccion(XmlNode seccion, int IdDocumento)
		{
			string nombreTemplate = seccion.Attributes["template"].InnerXml;
			int version = Convert.ToInt32(seccion.Attributes["version"].InnerXml);
			var sbTemplate = new StringBuilder(RecuperaTemplate(IdDocumento, nombreTemplate, version));
			var detalles = new Dictionary<string, StringBuilder>();
			foreach (XmlNode elemento in seccion.ChildNodes)
			{
				if (elemento.ChildNodes.Count > 0)
				{
					if (elemento.ChildNodes.Count == 1 && elemento.FirstChild.NodeType == XmlNodeType.Text)
						sbTemplate.Replace(string.Format("${0}$", elemento.Name), elemento.InnerText);
					else
					{
						string detalle = LlenaSeccion(elemento, IdDocumento).Replace("NumPDF", string.Format("NumPDF{0}", Guid.NewGuid()));
						if (detalles.ContainsKey(elemento.Name))
							detalles[elemento.Name] = detalles[elemento.Name].Append(detalle);
						else
							detalles.Add(elemento.Name, new StringBuilder(detalle));
					}
				}
				else
				{
					if (elemento.Attributes.Count > 0)  // <RectificacionDecir />
						sbTemplate.Replace(string.Format("<{0} />", elemento.Name), string.Empty);
					else
						sbTemplate.Replace(string.Format("${0}$", elemento.Name), string.Empty);
				}
			}
			foreach (string key in detalles.Keys)
			{
				string keyTemplate = string.Format("<{0} />", key);
				sbTemplate.Replace(keyTemplate, detalles[key].ToString());
			}
			return sbTemplate.ToString();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="seccion"></param>
		/// <param name="IdDocumento"></param>
		/// <returns></returns>
		private static string LlenaSeccionConfig(XmlNode seccion, int IdDocumento)
		{
			string nombreTemplate = seccion.Attributes["template"].InnerXml;

			var sbTemplate = new StringBuilder(RecuperaTemplate(IdDocumento, nombreTemplate, 1));
			var detalles = new Dictionary<string, StringBuilder>();
			foreach (XmlNode elemento in seccion.ChildNodes)
			{
				if (elemento.ChildNodes.Count > 0)
				{
					if (elemento.ChildNodes.Count == 1 && elemento.FirstChild.NodeType == XmlNodeType.Text)
						sbTemplate.Replace(string.Format("${0}$", elemento.Name), elemento.InnerText);
					else
					{
						string detalle = LlenaSeccion(elemento, IdDocumento).Replace("NumPDF", string.Format("NumPDF{0}", Guid.NewGuid()));
						if (detalles.ContainsKey(elemento.Name))
							detalles[elemento.Name] = detalles[elemento.Name].Append(detalle);
						else
							detalles.Add(elemento.Name, new StringBuilder(detalle));
					}
				}
				else
					sbTemplate.Replace(string.Format("${0}$", elemento.Name), string.Empty);
			}

			foreach (string key in detalles.Keys)
			{
				string keyTemplate = string.Format("<{0} />", key);
				sbTemplate.Replace(keyTemplate, detalles[key].ToString());
			}

			return sbTemplate.ToString();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdDocumento"></param>
		/// <param name="nombreTemplate"></param>
		/// <param name="version"></param>
		/// <returns></returns>
		private static string RecuperaTemplate(int IdDocumento, string nombreTemplate, int version)
		{
			var dalTemplate = new DalTemplate();
			return dalTemplate.ConsultaTemplate(IdDocumento, nombreTemplate, version);
		}

		/// <summary>
		/// Método que obtiene el archivo en formato XSD para la validación de un XML de entrada.
		/// </summary>
		/// <param name="piIdDocumento">Identificador único del documento o template.</param>
		/// <returns>Cadena con XSD de validación</returns>
		public static string ObtieneTemplateValidacion(int piIdDocumento)
		{
			return new DalTemplate().ObtieneTemplateValidacion(piIdDocumento);
		}
	}
}