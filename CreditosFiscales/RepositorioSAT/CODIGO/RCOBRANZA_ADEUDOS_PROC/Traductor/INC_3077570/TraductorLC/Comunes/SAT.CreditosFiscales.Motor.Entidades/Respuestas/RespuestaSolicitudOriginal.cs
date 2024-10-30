// Referencias de sistema.
using System;
using System.Diagnostics;
using System.ComponentModel;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

[Serializable()]
[DebuggerStepThrough()]
[DesignerCategory("code")]
[XmlType(AnonymousType = true)]
[XmlRoot(Namespace = "", IsNullable = false)]
public partial class RespuestaSolicitudOriginal
{
	private string xmlOriginal = null;
	private string lineaCaptura = null;
	private string fechaPago = null;
	private string fechaEmision = null;

	/// <summary>
	/// 
	/// </summary>
	public string XmlOriginal
	{
		get { return this.xmlOriginal; }
		set { this.xmlOriginal = value; }
	}

	/// <summary>
	/// 
	/// </summary>
	public string LineaCaptura
	{
		get { return this.lineaCaptura; }
		set { this.lineaCaptura = value; }
	}

	/// <summary>
	/// 
	/// </summary>
	public string FechaPago
	{
		get { return this.fechaPago; }
		set { this.fechaPago = value; }
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