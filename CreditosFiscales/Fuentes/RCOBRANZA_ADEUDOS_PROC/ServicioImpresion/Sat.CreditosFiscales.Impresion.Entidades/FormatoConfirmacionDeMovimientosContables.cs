//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Impresion.Entidades:Sat.CreditosFiscales.Impresion.Entidades.FormatoConfirmacionDeMovimientosContables:1:12/07/2013[Assembly:1.0:12/07/2013])

// Referencias de sistema.
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Sat.CreditosFiscales.Impresion.Entidades
{
	/// <summary>
	/// Clase para el formato de confirmación de movimientos contables - Encabezado
	/// </summary>
	[XmlRoot(ElementName = "Documentos")]
	public class FormatoConfirmacionDeMovimientosContables
	{
		/// <summary>
		/// Tipo de template
		/// </summary>
		[XmlAttribute]
		public string template { set { } get { return "Configuracion"; } }

		/// <summary>
		/// Lista de movimientos contables
		/// </summary>
		[XmlElement("LC")]
		public List<FCMCLC> LC { set; get; }

		/// <summary>
		/// Constructor.
		/// </summary>
		public FormatoConfirmacionDeMovimientosContables()
		{
			LC = new List<FCMCLC>();
		}
	}

	/// <summary>
	/// Clase para el formato de confirmación de movimientos contables - Movimientos Contables
	/// </summary>
	public class FCMCLC
	{
		/// <summary>
		/// Tipo de template
		/// </summary>
		[XmlAttribute]
		public string template { set { } get { return "MovimientosContables"; } }
		/// <summary>
		/// Versión del template
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
		/// Folio de línea de captura
		/// </summary>
		public string FolioLC { set; get; }
		/// <summary>
		/// Nombre o razón social del contribuyente
		/// </summary>
		public string Nombre { set; get; }
		/// <summary>
		/// Fecha y hora de emisión
		/// </summary>
		public string FechaYHora { set; get; }
		/// <summary>
		/// Referencia
		/// </summary>
		public string Referencia { set; get; }
		/// <summary>
		/// Importe
		/// </summary>
		public string Importe { set; get; }
		/// <summary>
		/// Lista de abonos
		/// </summary>
		[XmlElement("Abonos")]
		public List<FCMCAbonos> Abonos { set; get; }
		/// <summary>
		/// Lista de cargos
		/// </summary>
		[XmlElement("Cargos")]
		public List<FCMCCargos> Cargos { set; get; }
		/// <summary>
		/// Importe total de abonos
		/// </summary>
		public string TotalA { set; get; }
		/// <summary>
		/// Importe total de cargos
		/// </summary>
		public string TotalC { set; get; }
		/// <summary>
		/// Observaciones
		/// </summary>
		public string Observaciones { set; get; }

		/// <summary>
		/// Constructor
		/// </summary>
		public FCMCLC()
		{
			Abonos = new List<FCMCAbonos>();
			Cargos = new List<FCMCCargos>();
		}
	}

	/// <summary>
	/// Clase para el formato de confirmación de movimientos contables - Abonos
	/// </summary>
	public class FCMCAbonos
	{
		/// <summary>
		/// Tipo de template
		/// </summary>
		[XmlAttribute]
		public string template { set { } get { return "Abonos"; } }
		/// <summary>
		/// Número de versión del template
		/// </summary>
		[XmlAttribute]
		public int version { set; get; }
		/// <summary>
		/// Clave del abono
		/// </summary>
		public string ClaveA { set; get; }
		/// <summary>
		/// Descripción del abono
		/// </summary>
		public string DescripcionA { set; get; }
		/// <summary>
		/// Importe del abono
		/// </summary>
		public string ImporteA { set; get; }
	}

	/// <summary>
	/// Clase para el formato de confirmación de movimientos contables - Cargos
	/// </summary>
	public class FCMCCargos
	{
		/// <summary>
		/// Tipo de template
		/// </summary>
		[XmlAttribute]
		public string template { set { } get { return "Cargos"; } }
		/// <summary>
		/// Número de versión del template
		/// </summary>
		[XmlAttribute]
		public int version { set; get; }
		/// <summary>
		/// Clave del cargo
		/// </summary>
		public string ClaveC { set; get; }
		/// <summary>
		/// Descripción de cargo
		/// </summary>
		public string DescripcionC { set; get; }
		/// <summary>
		/// Importe del cargo
		/// </summary>
		public string ImporteC { set; get; }
	}
}