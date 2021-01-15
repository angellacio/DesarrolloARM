
//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Negocio.Comun.Tipos:DocumentResponse:0:21/Mayo/2008[SAT.DyP.Negocio.Comun.Tipos:1.0:21/Mayo/2008])
	
using System.Xml.Serialization;

namespace SAT.DyP.Negocio.Comun.Tipos
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/scade.net/2007_10_07/Response")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.sat.gob.mx/scade.net/2007_10_07/Response", IsNullable = false)]
    public partial class DocumentResponse
    {

        private string messageIDField;

        private string rFCPostedByField;

        private string rFCField;

        private int statusField;

        private string messageField;

        private string businessDocumentIDField;

        private string digitalReceiptSealField;

        private System.DateTime receivedDateField;

        /// <remarks/>
        public string MessageID
        {
            get
            {
                return this.messageIDField;
            }
            set
            {
                this.messageIDField = value;
            }
        }

        /// <remarks/>
        public string RFCPostedBy
        {
            get
            {
                return this.rFCPostedByField;
            }
            set
            {
                this.rFCPostedByField = value;
            }
        }

        /// <remarks/>
        public string RFC
        {
            get
            {
                return this.rFCField;
            }
            set
            {
                this.rFCField = value;
            }
        }

        /// <remarks/>
        public int Status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }

        /// <remarks/>
        public string Message
        {
            get
            {
                return this.messageField;
            }
            set
            {
                this.messageField = value;
            }
        }

        /// <remarks/>
        public string BusinessDocumentID
        {
            get
            {
                return this.businessDocumentIDField;
            }
            set
            {
                this.businessDocumentIDField = value;
            }
        }

        /// <remarks/>
        public string DigitalReceiptSeal
        {
            get
            {
                return this.digitalReceiptSealField;
            }
            set
            {
                this.digitalReceiptSealField = value;
            }
        }

        /// <remarks/>
        public System.DateTime ReceivedDate
        {
            get
            {
                return this.receivedDateField;
            }
            set
            {
                this.receivedDateField = value;
            }
        }
    }
}