//@(#)SCADE2(W:SKDN08515FF4:Sat.Scade.Net.IDE.Presentacion.Proxy:DocumentResponse:0:23/Diciembre/2008[Sat.Scade.Net.IDE.Presentacion.Proxy:0:23/Diciembre/2008]) 
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Scade.Net.IDE.Presentacion.Proxy
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "3.0.4506.30")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/scade.net/2007_10_07/Response")]
    public partial class DocumentResponse
    {
        private string mensajeID;
        private string rfcAutenticacion;
        private string rfcContribuyente;
        private int estatus;
        private string mensaje;
        private string folio;
        private string selloDigital;
        private System.DateTime fechaRecepcion;
        private string numeroOperacion;

        /// <summary>
        /// Identificador �nico del mensaje.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("MessageID", Order = 0)]
        public string MensajeID
        {
            get
            {
                return this.mensajeID;
            }
            set
            {
                this.mensajeID = value;
            }
        }

        /// <summary>
        /// Registro Federal del Contribuyente del usuario que se autentica en la aplicaci�n.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("RFCPostedBy", Order = 1)]
        public string RfcAutenticacion
        {
            get
            {
                return this.rfcAutenticacion;
            }
            set
            {
                this.rfcAutenticacion = value;
            }
        }

        /// <summary>
        /// Registro Federal del Contribuyente que presenta la declaraci�n.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("RFC", Order = 2)]
        public string RfcContribuyente
        {
            get
            {
                return this.rfcContribuyente;
            }
            set
            {
                this.rfcContribuyente = value;
            }
        }

        /// <summary>
        /// Estatus de la respuesta de la recepci�n de la declaraci�n.
        /// <para>
        /// <remarks>
        /// El valor 1 indica que la recepci�n de la declaraci�n fu� satisfactoria y el valor 0 indica que hubo un error en la recepci�n, en la propiedad Mensaje puede ver la descripci�n del motivo de error en la recepci�n.
        /// </remarks>
        /// </para>
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("Status", Order = 3)]
        public int Estatus
        {
            get
            {
                return this.estatus;
            }
            set
            {
                this.estatus = value;
            }
        }

        /// <summary>
        /// Descripci�n o mensajes de la respuesta de la recepci�n de la declaraci�n de acuerdo a su estatus, puede consultar el estatus de la recepci�n de la declaraci�n en la propiedad Estatus.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("Message", Order = 4)]
        public string Mensaje
        {
            get
            {
                return this.mensaje;
            }
            set
            {
                this.mensaje = value;
            }
        }

        /// <summary>
        /// N�mero de folio generado por la recepci�n de la declaraci�n en SCADE. <remarks>Este valor puede ser vac�o o nulo si la recepci�n de la declaraci�n no fu� satisfactoria.</remarks>
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("BusinessDocumentID", Order = 5)]
        public string Folio
        {
            get
            {
                return this.folio;
            }
            set
            {
                this.folio = value;
            }
        }

        /// <summary>
        /// Contiene el sello digital de la declaraci�n presentada por el contribuyente.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("DigitalReceiptSeal", Order = 6)]
        public string SelloDigital
        {
            get
            {
                return this.selloDigital;
            }
            set
            {
                this.selloDigital = value;
            }
        }

        /// <summary>
        /// Fecha de recepci�n de la declaraci�n en SCADE.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("ReceivedDate", Order = 7)]
        public System.DateTime FechaRecepcion
        {
            get
            {
                return this.fechaRecepcion;
            }
            set
            {
                this.fechaRecepcion = value;
            }
        }

        /// <summary>
        /// Contiene el n�mero de operaci�n.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("OperationId", Order = 8)]
        public string NumeroOperacion
        {
            get
            {
                return this.numeroOperacion;
            }
            set
            {
                this.numeroOperacion = value;
            }
        }
    }
}
