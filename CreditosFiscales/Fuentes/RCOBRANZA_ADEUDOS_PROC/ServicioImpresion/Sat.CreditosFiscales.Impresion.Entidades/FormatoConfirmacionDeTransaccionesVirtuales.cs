//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Impresion.Entidades:Sat.CreditosFiscales.Impresion.Entidades.FormatoConfirmacionDeTransaccionesVirtuales:1:12/07/2013[Assembly:1.0:12/07/2013])

// Referencias de sistema.
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Sat.CreditosFiscales.Impresion.Entidades
{
	/// <summary>
	/// Clase para el formato de confirmación de transacciones virtuales totales y parciales - Encabezado
	/// </summary>
	[XmlRoot(ElementName = "Documentos")]
	public class FormatoConfirmacionDeTransaccionesVirtuales
	{
		/// <summary>
		/// Tipo de template
		/// </summary>
		[XmlAttribute]
		public string template { set { } get { return "Configuracion"; } }

		/// <summary>
		/// Lista de transacciones
		/// </summary>
		[XmlElement("LC")]
		public List<FCTLC> LC { set; get; }
		/// <summary>
		/// Constructor
		/// </summary>
		public FormatoConfirmacionDeTransaccionesVirtuales()
		{
			LC = new List<FCTLC>();
		}
	}

	/// <summary>
	/// Clase para el formato de confirmación de transacciones virtuales totales y parciales - Transacciones
	/// </summary>
	public class FCTLC
	{
		/// <summary>
		/// Tipo de template
		/// </summary>
		[XmlAttribute]
		public string template { set { } get { return "Transacciones"; } }
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
		/// Fecha y Hora de emisión
		/// </summary>
		public string FechaYHora { set; get; }
		/// <summary>
		/// Fecha de importe actualizado al
		/// </summary>
		public string Fecha { set; get; }
		/// <summary>
		/// Importe actualizado total
		/// </summary>
		public string ImporteActualizadoTotal { set; get; }
		/// <summary>
		/// Importe pagar total
		/// </summary>
		public string ImportePagarTotal { set; get; }
		/// <summary>
		/// Lista de referencias
		/// </summary>
		[XmlElement("Referencias")]
		public List<FCTReferencias> Referencias { set; get; }
		/// <summary>
		/// Lista de detalles de las referencias
		/// </summary>
		[XmlElement("ReferenciaDetalles")]
		public List<FCTReferenciaDetalles> ReferenciaDetalles { set; get; }
		/// <summary>
		/// Observaciones
		/// </summary>
		public string Observaciones { set; get; }
		/// <summary>
		/// Constructor
		/// </summary>
		public FCTLC()
		{
			Referencias = new List<FCTReferencias>();
			ReferenciaDetalles = new List<FCTReferenciaDetalles>();
		}
	}

	/// <summary>
	/// Clase para el formato de confirmación de transacciones virtuales totales y parciales - Transacciones - Referencias
	/// </summary>
	public class FCTReferencias
	{
		/// <summary>
		/// Tipo de template
		/// </summary>
		[XmlAttribute]
		public string template { set { } get { return "Referencias"; } }
		/// <summary>
		///  Número de versión del template
		/// </summary>
		[XmlAttribute]
		public int version { set; get; }
		/// <summary>
		/// Número de resolución o referencia
		/// </summary>
		public string Referencia { set; get; }
		/// <summary>
		/// Importe actualizado
		/// </summary>
		public string Actualizado { set; get; }
		/// <summary>
		/// Importe aplicado
		/// </summary>
		public string Aplicado { set; get; }
	}

	/// <summary>
	/// Clase para el formato de confirmación de transacciones virtuales totales y parciales - Detalles
	/// </summary>
	public class FCTReferenciaDetalles
	{
		/// <summary>
		/// Tipo de template
		/// </summary>
		[XmlAttribute]
		public string template { set { } get { return "ReferenciaDetalle"; } }
		/// <summary>
		///  Número de versión del template
		/// </summary>
		[XmlAttribute]
		public int version { set; get; }
		/// <summary>
		/// Número de resolución o referencia
		/// </summary>
		public string Referencia { set; get; }
		/// <summary>
		/// Lista de cargos
		/// </summary>
		[XmlElement("TransacCargos")]
		public List<FCTCargos> Cargos { set; get; }
		/// <summary>
		/// Lista de abonos
		/// </summary>
		[XmlElement("TransacAbonos")]
		public List<FCTAbonos> Abonos { set; get; }
		/// <summary>
		/// Importe total de cargos
		/// </summary>
		public string TotalCargos { set; get; }
		/// <summary>
		/// Importe total de abonos
		/// </summary>
		public string TotalAbonos { set; get; }

		/// <summary>
		/// Constructor
		/// </summary>
		public FCTReferenciaDetalles()
		{
			Cargos = new List<FCTCargos>();
			Abonos = new List<FCTAbonos>();
		}
	}

	/// <summary>
	/// Clase para el formato de confirmación de transacciones virtuales totales y parciales - Cargos
	/// </summary>
	public class FCTCargos
	{
		/// <summary>
		/// Tipo de template
		/// </summary>
		[XmlAttribute]
		public string template { set { } get { return "TransacCargos"; } }
		/// <summary>
		///  Número de versión del template
		/// </summary>
		[XmlAttribute]
		public int version { set; get; }
		/// <summary>
		/// Número de crédito del cargo
		/// </summary>
		public string Credito { set; get; }
		/// <summary>
		/// Clave del cargo
		/// </summary>
		public string Clave { set; get; }
		/// <summary>
		/// Descripción del cargo
		/// </summary>
		public string Descripcion { set; get; }
		/// <summary>
		/// Importe del cargo
		/// </summary>
		public string Importe { set; get; }
		/// <summary>
		/// Nivel 1 Padre o 2 Hijo
		/// </summary>
		public int Nivel { set; get; }
	}

	/// <summary>
	/// Clase para el formato de confirmación de transacciones virtuales totales y parciales - Abonos
	/// </summary>
	public class FCTAbonos
	{
		/// <summary>
		/// Tipo de template
		/// </summary>
		[XmlAttribute]
		public string template { set { } get { return "TransacAbonos"; } }
		/// <summary>
		///  Número de versión del template
		/// </summary>
		[XmlAttribute]
		public int version { set; get; }
		/// <summary>
		/// Clave del abono
		/// </summary>
		public string Clave { set; get; }
		/// <summary>
		/// Descripción del abono
		/// </summary>
		public string Descripcion { set; get; }
		/// <summary>
		/// Importe del abono
		/// </summary>
		public string Importe { set; get; }
	}
}