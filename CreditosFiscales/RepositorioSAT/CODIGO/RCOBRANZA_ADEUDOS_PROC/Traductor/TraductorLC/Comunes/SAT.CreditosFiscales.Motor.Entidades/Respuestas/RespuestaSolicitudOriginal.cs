
using System.Xml.Serialization;

    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class RespuestaSolicitudOriginal
    {




        private string xmlOriginal = null;
        private string lineaCaptura = null;
        private string fechaPago=null;
        private string fechaEmision=null;


        public string XmlOriginal
        {
            get { return this.xmlOriginal; }
            set { this.xmlOriginal = value; }
        }
        public string LineaCaptura
        {
            get { return this.lineaCaptura; }
            set { this.lineaCaptura = value; }
        }
        public string FechaPago
        {
            get { return this.fechaPago; }
            set { this.fechaPago = value; }
        }

        public string FechaEmision
        {
            get { return this.fechaEmision; }
            set { this.fechaEmision = value; }
        }

    }

