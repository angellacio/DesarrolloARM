// Referencias de sistema.
using System;
using System.Diagnostics;
using System.ComponentModel;
using System.Xml.Serialization;

[Serializable()]
[DebuggerStepThrough()]
[Designer("code")]
[XmlType(AnonymousType = true)]
[XmlRoot(Namespace = "", IsNullable = false)]
public partial class RespuestaListaFormatos
{
	public byte[] pDFField;

	#region Propiedades
		public string ADR { get; set; }
		public string RFC { get; set; }
		public string RazonSocial { get; set; }
		public string Folio { get; set; }
		public string fechaEmision { get; set; }
		public string IdResolucion { get; set; }
		public string NumResolucion { get; set; }
		public string LineaDeCaptura { get; set; }
		public string Importe { get; set; }
		public string FechaDeVigencia { get; set; }
		public string FormaDePago { get; set; }
		public string FechaDePago { get; set; }
		public string TipoDePago { get; set; }
		public string Aplicativo { get; set; }
		public string Modulo { get; set; }
		public string RFC_Usr_Generador { get; set; }
		public string XML { get; set; }
		public string Error { get; set; }
	#endregion

	/// <summary>
	/// 
	/// </summary>
	public RespuestaListaFormatos()
	{
		ADR = string.Empty;
		RFC = string.Empty;
		RazonSocial = string.Empty;
		Folio = string.Empty;
		fechaEmision = string.Empty;
		IdResolucion = string.Empty;
		NumResolucion = string.Empty;
		LineaDeCaptura = string.Empty;
		Importe = string.Empty;
		FormaDePago = string.Empty;
		FechaDePago = string.Empty;
		TipoDePago = string.Empty;
		Aplicativo = string.Empty;
		Modulo = string.Empty;
		RFC_Usr_Generador = string.Empty;
		pDFField = null;
		XML = string.Empty;
		Error = string.Empty;
	}
}
