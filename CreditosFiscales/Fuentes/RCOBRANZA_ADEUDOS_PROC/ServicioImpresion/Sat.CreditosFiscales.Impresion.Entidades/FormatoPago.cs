//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Impresion.Entidades:Sat.CreditosFiscales.Impresion.Entidades.FormatoPago:1:12/07/2013[Assembly:1.0:12/07/2013])

// Referencias de sistema.
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Sat.CreditosFiscales.Impresion.Entidades
{
	/// <summary>
	/// Clase para el formato de tipo de pago total y a cuenta - Encabezado
	/// </summary>
	[XmlRoot(ElementName = "Documentos")]
	public class FormatoPago
	{
		/// <summary>
		/// Tipo de template
		/// </summary>
		[XmlAttribute]
		public string template { set { } get { return "Configuracion"; } }

		/// <summary>
		/// Lista de líneas de captura
		/// </summary>
		[XmlElement("LC")]
		public List<FPLC> LC { set; get; }

		/// <summary>
		/// Constructor
		/// </summary>
		public FormatoPago()
		{
			LC = new List<FPLC>();
		}

	}

	/// <summary>
	/// Clase para el formato de tipo de pago total y a cuenta - LineaCaptura
	/// </summary>
	public class FPLC
	{
		/// <summary>
		/// Tipo de template
		/// </summary>
		[XmlAttribute]
		public string template { set { } get { return "LineaCaptura"; } }
		/// <summary>
		/// Número de versión del template
		/// </summary>
		[XmlAttribute]
		public int version { set; get; }
		/// <summary>
		/// ALR
		/// </summary>
		public string ALR { set; get; }
		/// <summary>
		/// Rfc del contribuyente
		/// </summary>
		public string RFC { set; get; }
		/// <summary>
		/// Folio de la línea de captura
		/// </summary>
		public string FolioLC { set; get; }
		/// <summary>
		/// Nombre o razón social del contribuyente
		/// </summary>
		public string Nombre { set; get; }

		/// <summary>
		/// Lista de documentos o resoluciones
		/// </summary>
		[XmlElement("Documento")]
		public List<FPDocumento> Documento { set; get; }
		/// <summary>
		/// Total de importe actualizado
		/// </summary>
		public string ImporteActualizadoT { set; get; }
		/// <summary>
		/// Total de importe a pagar
		/// </summary>
		public string ImporteAPagarT { set; get; }
		/// <summary>
		/// Fecha y hora de emisión
		/// </summary>
		public string FechaYHora { set; get; }
		/// <summary>
		/// Fecha de importe actualizado al
		/// </summary>
		public string Fecha { set; get; }
		/// <summary>
		/// Observaciones
		/// </summary>
		public string Observaciones { set; get; }
		/// <summary>
		/// Número de línea de captura
		/// </summary>
		public string NumLC { set; get; }
		/// <summary>
		/// Código de barras
		/// </summary>
		[XmlElement("CodigoBarrasDetalle")]
		public FCodigoBarras CodigoBarras { set; get; }

		/// <summary>
		/// Fecha de vigencia hasta
		/// </summary>
		public string FechaVigencia { set; get; }
		/// <summary>
		/// Texto de leyenda
		/// </summary>
		public string Leyenda { set; get; }

		public FPLC()
		{
			Documento = new List<FPDocumento>();
			CodigoBarras = new FCodigoBarras();
		}
	}

	/// <summary>
	/// Clase para el formato de tipo de pago total y a cuenta - Documento
	/// </summary>
	public class FPDocumento
	{
		/// <summary>
		/// Tipo de template
		/// </summary>
		[XmlAttribute]
		public string template { set { } get { return "LineaCapturaDocumento"; } }
		/// <summary>
		/// Versión del template
		/// </summary>
		[XmlAttribute]
		public int version { set; get; }
		/// <summary>
		/// Número de documento o resolución
		/// </summary>
		public string NumDocumento { set; get; }
		/// <summary>
		/// Importe actualizado
		/// </summary>
		public string ImporteActualizado { set; get; }
		/// <summary>
		/// Importe a pagar
		/// </summary>
		public string ImporteAPagar { set; get; }
	}

	/// <summary>
	/// Código de Barras
	/// </summary>
	public class FCodigoBarras
	{
		/// <summary>
		/// Tipo de template
		/// </summary>
		[XmlAttribute]
		public string template { set { } get { return "CodigoBarrasDetalle"; } }
		/// <summary>
		/// Versión del template
		/// </summary>
		[XmlAttribute]
		public int version { set; get; }
		/// <summary>
		/// Código a transformar
		/// </summary>
		public string CodigoBarras { set; get; }
		/// <summary>
		/// Ruta imagen código de barras formato Code 128
		/// </summary>
		public string ImagenCB { set; get; }
		/// <summary>
		/// Ruta imagen código QR
		/// </summary>
		public string ImagenQR { set; get; }
	}
}