// Referencias de sistema.
using System;
using System.Diagnostics;
using System.ComponentModel;
using System.Xml.Serialization;

[Serializable()]
[DebuggerStepThrough()]
[DesignerCategory("code")]
[XmlType(AnonymousType = true)]
[XmlRoot(Namespace = "", IsNullable = false)]
public partial class RespuestaListaFolios
{
	private string folio = null;
	private string idTipoDocumento = null;
	private string fechaEmision = null;

	/// <summary>
	/// 
	/// </summary>
	public string Folio
	{
		get { return this.folio; }
		set { this.folio = value; }
	}

	/// <summary>
	/// 
	/// </summary>
	public string IdTipoDocumento
	{
		get { return this.idTipoDocumento; }
		set { this.idTipoDocumento = value; }
	}

	/// <summary>
	/// 
	/// </summary>
	public string FechaEmision
	{
		get { return this.fechaEmision; }
		set { this.fechaEmision = value; }
	}
}