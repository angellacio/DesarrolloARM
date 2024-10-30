
using System.Xml.Serialization;

    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class RespuestaListaFolios
    {

        private string folio = null;
        private string idTipoDocumento = null;
        private string fechaEmision=null;


        public string Folio
        {
            get { return this.folio; }
            set { this.folio = value; }
        }
        public string IdTipoDocumento
        {
            get { return this.IdTipoDocumento; }
            set { this.IdTipoDocumento = value; }
        }
        public string FechaEmision
        {
            get { return this.fechaEmision; }
            set { this.fechaEmision = value; }
        }


    }

