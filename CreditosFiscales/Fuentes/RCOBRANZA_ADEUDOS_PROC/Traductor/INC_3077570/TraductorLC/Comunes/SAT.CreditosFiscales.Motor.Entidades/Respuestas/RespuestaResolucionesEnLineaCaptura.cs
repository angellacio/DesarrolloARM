// Referencias de sistema.
using System;
using System.Diagnostics;
using System.ComponentModel;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

/// <summary>
/// 
/// </summary>
[GeneratedCode("xsd", "4.0.30319.17929")]
[Serializable()]
[DebuggerStepThrough()]
[DesignerCategory("code")]
[XmlType(AnonymousType = true)]
[XmlRoot(Namespace = "", IsNullable = false)]
public partial class RespuestaResolucionesEnLC
{
	private RespuestaResolucionesEnLCResolucion[] resolucionesField;
	private string[] listaErroresField;

	/// <summary>
	/// 
	/// </summary>
	[XmlArray(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
	[XmlArrayItem("Resolucion", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
	public RespuestaResolucionesEnLCResolucion[] Resoluciones
	{
		get
		{
			return this.resolucionesField;
		}
		set
		{
			this.resolucionesField = value;
		}
	}

	/// <summary>
	/// 
	/// </summary>
	[XmlArray(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
	[XmlArrayItem("Error", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
	public string[] ListaErrores
	{
		get
		{
			return this.listaErroresField;
		}
		set
		{
			this.listaErroresField = value;
		}
	}
}

/// <summary>
/// 
/// </summary>
[GeneratedCode("xsd", "4.0.30319.17929")]
[Serializable()]
[DebuggerStepThrough()]
[DesignerCategory("code")]
[XmlType(AnonymousType = true)]
public partial class RespuestaResolucionesEnLCResolucion
{
	private string numeroResolucionField;
	private string canonicoField;

	/// <summary>
	/// 
	/// </summary>
	[XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
	public string NumeroResolucion
	{
		get
		{
			return this.numeroResolucionField;
		}
		set
		{
			this.numeroResolucionField = value;
		}
	}

	/// <summary>
	/// 
	/// </summary>
	[XmlElement(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
	public string Canonico
	{
		get
		{
			return this.canonicoField;
		}
		set
		{
			this.canonicoField = value;
		}
	}
}