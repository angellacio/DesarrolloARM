
//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Negocio.Comun.Tipos:DocumentRequest:0:21/Mayo/2008[SAT.DyP.Negocio.Comun.Tipos:1.0:21/Mayo/2008])
	
using System.Xml.Serialization;

namespace SAT.DyP.Negocio.Comun.Tipos
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/scade.net/2007_10_07/Request")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.sat.gob.mx/scade.net/2007_10_07/Request", IsNullable = false)]
    public partial class DocumentRequest
    {

        private string messageIDField;

        private string rFCField;

        private System.DateTime receivedDateField;

        private string postedByRFCField;

        private string documentDataField;

        private string fileNameField;

        private int receiveChanelField;

        private string clientIPAddressField;

        private int taxableEntityTypeField;

        private int taxStatementTypeField;

        private int branchIdField;

        private string certificateSerialNumberField;

        private string documentPathField;

        private int fileSizeField;

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

        /// <remarks/>
        public string PostedByRFC
        {
            get
            {
                return this.postedByRFCField;
            }
            set
            {
                this.postedByRFCField = value;
            }
        }

        /// <remarks/>
        public string DocumentData
        {
            get
            {
                return this.documentDataField;
            }
            set
            {
                this.documentDataField = value;
            }
        }

        /// <remarks/>
        public string FileName
        {
            get
            {
                return this.fileNameField;
            }
            set
            {
                this.fileNameField = value;
            }
        }

        /// <remarks/>
        public int ReceiveChanel
        {
            get
            {
                return this.receiveChanelField;
            }
            set
            {
                this.receiveChanelField = value;
            }
        }

        /// <remarks/>
        public string ClientIPAddress
        {
            get
            {
                return this.clientIPAddressField;
            }
            set
            {
                this.clientIPAddressField = value;
            }
        }

        /// <remarks/>
        public int TaxableEntityType
        {
            get
            {
                return this.taxableEntityTypeField;
            }
            set
            {
                this.taxableEntityTypeField = value;
            }
        }

        /// <remarks/>
        public int TaxStatementType
        {
            get
            {
                return this.taxStatementTypeField;
            }
            set
            {
                this.taxStatementTypeField = value;
            }
        }

        /// <remarks/>
        public int BranchId
        {
            get
            {
                return this.branchIdField;
            }
            set
            {
                this.branchIdField = value;
            }
        }

        /// <remarks/>
        public string CertificateSerialNumber
        {
            get
            {
                return this.certificateSerialNumberField;
            }
            set
            {
                this.certificateSerialNumberField = value;
            }
        }

        /// <remarks/>
        public string DocumentPath
        {
            get
            {
                return this.documentPathField;
            }
            set
            {
                this.documentPathField = value;
            }
        }

        /// <remarks/>
        public int FileSize
        {
            get
            {
                return this.fileSizeField;
            }
            set
            {
                this.fileSizeField = value;
            }
        }
    }
}