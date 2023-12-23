//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Impresion.Entidades:Sat.CreditosFiscales.Impresion.Entidades.FormatoConfirmacionDeRectificacionesContables:1:12/07/2013[Assembly:1.0:12/07/2013])

// Referencias de sistema.
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Sat.CreditosFiscales.Impresion.Entidades
{
	/// <summary>
	/// Clase para el formato de rectificaciones contables de cancelación o reclasificación - Encabezado
	/// </summary>
	[Serializable]
	[XmlRoot(ElementName = "Documentos")]
	public class FormatoConfirmacionDeRectificacionesContables
	{
		/// <summary>
		/// 
		/// </summary>
		[XmlAttribute]
		public string template { set { } get { return "Configuracion"; } }

		/// <summary>
		/// 
		/// </summary>
		[XmlElement("LC")]
		public List<FCRCLC> LC { set; get; }

		/// <summary>
		/// 
		/// </summary>
		public FormatoConfirmacionDeRectificacionesContables()
		{
			LC = new List<FCRCLC>();
		}
	}

	/// <summary>
	/// Clase para el formato de rectificaciones contables de cancelación o reclasificación - MovimientosRectificaciones
	/// </summary>
	[Serializable]
	public class FCRCLC
	{
		/// <summary>
		/// 
		/// </summary>
		[XmlAttribute]
		public string template { set { } get { return "Rectificaciones"; } }
		/// <summary>
		/// 
		/// </summary>
		[XmlAttribute]
		public int version { set; get; }
		/// <summary>
		/// 
		/// </summary>
		public string ALR { set; get; }
		/// <summary>
		/// 
		/// </summary>
		public string RFC { set; get; }
		/// <summary>
		/// 
		/// </summary>
		public string FolioLC { set; get; }
		/// <summary>
		/// 
		/// </summary>
		public string Nombre { set; get; }
		/// <summary>
		/// 
		/// </summary>
		public string FechaYHora { set; get; }
		/// <summary>
		/// 
		/// </summary>
		public string TipoOperacion { set; get; }
		/// <summary>
		/// 
		/// </summary>
		public string TipoRectificacion { set; get; }
		/// <summary>
		/// 
		/// </summary>
		public string ImporteOperacion { set; get; }
		/// <summary>
		/// 
		/// </summary>
		public string DocumentoRectificar { set; get; }
		/// <summary>
		/// 
		/// </summary>
		public string FechaEmision { set; get; }
		/// <summary>
		/// 
		/// </summary>
		public string ClaveAbono { set; get; }
		/// <summary>
		/// 
		/// </summary>
		public string Convenio { set; get; }
		/// <summary>
		/// 
		/// </summary>
		public string RFCDice { set; get; }
		/// <summary>
		/// 
		/// </summary>
		public string RFCDecir { set; get; }
		/// <summary>
		/// Lista de abonos Dice
		/// </summary>
		[XmlElement("RectificacionDiceR")]
		public List<FCRDiceR> DiceRenglon1 { set; get; }
		/// <summary>
		/// Lista de cargos Dice
		/// </summary>
		[XmlElement("RectificacionDice")]
		public List<FCRDice> Dice { set; get; }

		/// <summary>
		/// Lista de abonos Debe Decir
		/// </summary>
		[XmlElement("RectificacionDecirR")]
		public List<FCRDebeDecirR> DebeDecirRenglon1 { set; get; }
		/// <summary>
		/// Lista de cargos Debe Decir
		/// </summary>
		[XmlElement("RectificacionDecir")]
		public List<FCRDebeDecir> DebeDecir { set; get; }

		/// <summary>
		/// 
		/// </summary>
		public string TotalDice { set; get; }
		/// <summary>
		/// 
		/// </summary>
		public string TotalDecir { set; get; }
		/// <summary>
		/// 
		/// </summary>
		public string MotivoRectificacion { set; get; }
	}

	/// <summary>
	/// Clase para el formato de rectificaciones contables de cancelación o reclasificación - Primer renglón Dice rowSpan
	/// </summary>
	public class FCRDiceR
	{
		/// <summary>
		/// 
		/// </summary>
		[XmlAttribute]
		public string template { set { } get { return "RectificacionDiceRowsp"; } }
		/// <summary>
		/// 
		/// </summary>
		[XmlAttribute]
		public int version { set; get; }

		/// <summary>
		/// 
		/// </summary>
		public string Credito { set; get; }
		/// <summary>
		/// 
		/// </summary>
		public string ClaveCargo { set; get; }
		/// <summary>
		/// 
		/// </summary>
		public string DescripcionCargo { set; get; }

		/// <summary>
		/// 
		/// </summary>
		public string ClaveAbono { set; get; }
		/// <summary>
		/// 
		/// </summary>
		public string DescripcionAbono { set; get; }
		/// <summary>
		/// 
		/// </summary>
		public string Importe { set; get; }
		/// <summary>
		/// 
		/// </summary>
		public int NumCreditos { set; get; }
	}

	/// <summary>
	/// Clase para el formato de rectificaciones contables de cancelación o reclasificación - Dice
	/// </summary>
	[Serializable]
	public class FCRDice
	{
		/// <summary>
		/// 
		/// </summary>
		[XmlAttribute]
		/// <summary>
		/// 
		/// </summary>
		public string template { set { } get { return "RectificacionDice"; } }
		/// <summary>
		/// 
		/// </summary>
		[XmlAttribute]
		public int version { set; get; }

		/// <summary>
		/// 
		/// </summary>
		public string Credito { set; get; }
		/// <summary>
		/// 
		/// </summary>
		public string ClaveCargo { set; get; }
		/// <summary>
		/// 
		/// </summary>
		public string DescripcionCargo { set; get; }
		/// <summary>
		/// 
		/// </summary>
		public string Importe { set; get; }
		/// <summary>
		/// 
		/// </summary>
		public string Nivel { set; get; }
	}

	/// <summary>
	/// Clase para el formato de rectificaciones contables de cancelación o reclasificación - Primer renglón Debe Decir RowSpan
	/// </summary>
	public class FCRDebeDecirR
	{
		/// <summary>
		/// 
		/// </summary>
		[XmlAttribute]
		/// <summary>
		/// 
		/// </summary>
		public string template { set { } get { return "RectificacionDecirRowsp"; } }
		/// <summary>
		/// 
		/// </summary>
		[XmlAttribute]
		public int version { set; get; }

		/// <summary>
		/// 
		/// </summary>
		public string Credito { set; get; }
		/// <summary>
		/// 
		/// </summary>
		public string ClaveCargo { set; get; }
		/// <summary>
		/// 
		/// </summary>
		public string DescripcionCargo { set; get; }

		/// <summary>
		/// 
		/// </summary>
		public string ClaveAbono { set; get; }
		/// <summary>
		/// 
		/// </summary>
		public string DescripcionAbono { set; get; }
		/// <summary>
		/// 
		/// </summary>
		public string Importe { set; get; }
		/// <summary>
		/// 
		/// </summary>
		public int NumCreditos { set; get; }
	}

	/// <summary>
	/// Clase para el formato de rectificaciones contables de cancelación o reclasificación - Debe Decir
	/// </summary>
	[Serializable]
	public class FCRDebeDecir
	{
		/// <summary>
		/// 
		/// </summary>
		[XmlAttribute]
		public string template { set { } get { return "RectificacionDecir"; } }
		/// <summary>
		/// 
		/// </summary>
		[XmlAttribute]
		public int version { set; get; }
		/// <summary>
		/// 
		/// </summary>
		public string Credito { set; get; }
		/// <summary>
		/// 
		/// </summary>
		public string ClaveCargo { set; get; }
		/// <summary>
		/// 
		/// </summary>
		public string DescripcionCargo { set; get; }
		/// <summary>
		/// 
		/// </summary>
		public string Importe { set; get; }
		/// <summary>
		/// 
		/// </summary>
		public string Nivel { set; get; }
	}
}