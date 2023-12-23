using System.Xml.Serialization;

    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]

public partial class RespuestaListaFormatos
{

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
    public byte[] pDFField;
    public string XML { get; set; }
    public string Error { get; set; }


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
      pDFField=null;
      XML = string.Empty;
      Error = string.Empty;

    }
   

   

}