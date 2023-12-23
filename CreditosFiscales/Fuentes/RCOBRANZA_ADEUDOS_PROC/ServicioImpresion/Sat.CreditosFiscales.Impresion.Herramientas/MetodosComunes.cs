//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Impresion.Herramientas:Sat.CreditosFiscales.Impresion.Herramientas.MetodosComunes:1:12/07/2013[Assembly:1.0:12/07/2013])

// Referencias de sistema.
using System;
using System.Xml;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Sat.CreditosFiscales.Impresion.Herramientas
{
	/// <summary>
	/// Clase para la generación de métodos comunes utilizados en la solución
	/// </summary>
	public class MetodosComunes
	{
		/// <summary>
		/// Arreglo que contiene los meses del año en texto a 3 caracteres Ene,Feb, ...
		/// </summary>
		private static string[] Mes = { "Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic" };

		/// <summary>
		/// Método que serializa un objeto y lo regresa en una cadena con formato XML.
		/// </summary>
		/// <param name="value">Tipo de objeto</param>
		/// <returns>Cadena en formato XML</returns>
		public static string Serializa(Object value)
		{
			var xmlFuncionario = new StringBuilder();
			var serilizador = new XmlSerializer(value.GetType());
			var xns = new XmlSerializerNamespaces();
			xns.Add(string.Empty, string.Empty);
			serilizador.Serialize(XmlTextWriter.Create(xmlFuncionario), value, xns);
			return xmlFuncionario.ToString();
		}

		/// <summary>
		/// Método para formatear una cantidad decimal en tipo moneda
		/// </summary>
		/// <param name="cantidad">Cantidad</param>
		/// <returns>Cadena con formato $ 9,999</returns>
		public static string FormatoMoneda(decimal cantidad)
		{
			return string.Format("$ {0:#,0}", cantidad);
		}
		/// <summary>
		/// Método que muestra una cantidad sin formato
		/// </summary>
		/// <param name="cantidad">Cantidad</param>
		/// <returns>9999</returns>
		public static string SinFormatoMoneda(decimal cantidad)
		{
			return string.Format("{0:0}", cantidad);
		}

		/// <summary>
		/// Enumeración para el tipo de fecha
		/// </summary>
		public enum TipoFecha
		{
			/// <summary>
			/// Formato 01/Ene/2013
			/// </summary>
			FechaMesTexto,
			/// <summary>
			/// Formato 01/01/2013
			/// </summary>
			FechaMesNumero
		}
		/// <summary>
		/// Método para formatear una fecha en tipo 01/Ene/2013 o 01/01/2013
		/// </summary>
		/// <param name="fecha">Fecha</param>
		/// <param name="tipo"><see cref="TipoFecha"/>Enumeración del tipo de formato de salida</param>
		/// <returns>Cadena con el formato definido 01/Ene/2013 o 01/01/2013</returns>
		public static string FormatoFecha(DateTime fecha, TipoFecha tipo)
		{
			switch (tipo)
			{
				case TipoFecha.FechaMesNumero:
					return string.Format("{0:dd/MM/yyyy}", fecha);
				default:
					return string.Format("{0:dd}", fecha) + "/" + Mes[fecha.Month - 1] + "/" + string.Format("{0:yyyy}", fecha);
			}
		}

		/// <summary>
		/// Método para formatear una fecha en tipo 01/Ene/2013 11:00
		/// </summary>
		/// <param name="fechahora">Fecha y Hora</param>
		/// <returns>Cadena con formato 01/Ene/2013 11:00</returns>
		public static string FormatoFechaHora(DateTime fechahora)
		{
			return FormatoFecha(fechahora, TipoFecha.FechaMesTexto) + string.Format(" {0:HH:mm}", fechahora);
		}
		/// <summary>
		/// Método para formatear una línea de captura en tipo 0000 0000 0000 0000 0000
		/// </summary>
		/// <param name="lineaCaptura">Linea de captura</param>
		/// <returns>Cadena con formato 0000 0000 0000 0000 0000</returns>
		public static string FormatoLineaCaptura(string lineaCaptura)
		{
			return lineaCaptura.ToCharArray().Aggregate("", (result, c) => result += ((!string.IsNullOrEmpty(result) && (result.Length + 1) % 5 == 0) ? " " : "") + c.ToString());
		}

		/// <summary>
		/// Formatea el folio de línea de captura en tipo 00-000000000
		/// </summary>
		/// <param name="folio">Folio</param>
		/// <returns>Cadena con formato 00-000000000</returns>
		public static string FormatoFolioLineaCaptura(string folio)
		{
			return folio.Insert(2, "-");
		}
	}
}