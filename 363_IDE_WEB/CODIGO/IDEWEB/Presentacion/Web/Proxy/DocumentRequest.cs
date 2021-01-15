//@(#)SCADE2(W:SKDN08515FF4:Sat.Scade.Net.IDE.Presentacion.Proxy:DocumentRequest:0:23/Diciembre/2008[Sat.Scade.Net.IDE.Presentacion.Proxy:0:23/Diciembre/2008]) 
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Scade.Net.IDE.Presentacion.Proxy
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "3.0.4506.30")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.sat.gob.mx/scade.net/2007_10_07/Request")]
    public partial class DocumentRequest
    {
        private string mensajeID;
        private string rfcContribuyente;
        private DateTime fechaPresentacion;
        private string rfcAutenticacion;
        private string contenidoDeclaracion;
        private string nombreArchivo;
        private int medioPresentacion;
        private string direccionIP;
        private int tipoPersona;
        private int materia;
        private int entidadReceptora;
        private string numeroSerie;
        private string rutaArchivo;
        private int tamañoArchivo;

        /// <summary>
        /// Identificador único del mensaje.
        /// <remarks>Se recomienda utilizar un valor GUID, ver <see cref="System.Guid"/>.</remarks>
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
        /// Registro Federal del Contribuyente que presenta la declaración.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("RFC", Order = 1)]
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
        /// Fecha en que el contribuyente presenta la declaración.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("ReceivedDate", Order = 2)]
        public System.DateTime FechaPresentacion
        {
            get
            {
                return this.fechaPresentacion;
            }
            set
            {
                this.fechaPresentacion = value;
            }
        }

        /// <summary>
        /// Registro Federal del Contribuyente del usuario que se autentica en la aplicación.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("PostedByRFC", Order = 3)]
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
        /// Contenido de la declaración. <remarks>El contenido de la declaración debe de estar en Base64.</remarks>
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("DocumentData", Order = 4)]
        public string Contenido
        {
            get
            {
                return this.contenidoDeclaracion;
            }
            set
            {
                this.contenidoDeclaracion = value;
            }
        }

        /// <summary>
        /// Nombre del archivo de la declaración. <remarks>El nombre del archivo puede ser vacío mas no nulo.</remarks>
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("FileName", Order = 5)]
        public string NombreArchivo
        {
            get
            {
                return this.nombreArchivo;
            }
            set
            {
                this.nombreArchivo = value;
            }
        }


        [System.Xml.Serialization.XmlElementAttribute("ReceiveChanel", Order = 6)]
        public int MedioPresentacion
        {
            get
            {
                return this.medioPresentacion;
            }
            set
            {
                this.medioPresentacion = value;
            }
        }

        /// <summary>
        /// Dirección IP del equipo del cual se envía la declaración.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("ClientIPAddress", Order = 7)]
        public string DireccionIP
        {
            get
            {
                return this.direccionIP;
            }
            set
            {
                this.direccionIP = value;
            }
        }

        /// <summary>
        /// Tipo de persona fiscal del contribuyente.
        /// <remarks>El valor 0 indica Persona Física y el valor 1 indica Persona Moral.
        /// </remarks>
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("TaxableEntityType", Order = 8)]
        public int TipoPersona
        {
            get
            {
                return this.tipoPersona;
            }
            set
            {
                this.tipoPersona = value;
            }
        }

        /// <summary>
        /// Identificador de la materia del trámite.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("TaxStatementType", Order = 9)]
        public int Materia
        {
            get
            {
                return this.materia;
            }
            set
            {
                this.materia = value;
            }
        }

        /// <summary>
        /// Identificador de la Entidad Receptora (Oficina Recaudadora del SAT).
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("BranchId", Order = 10)]
        public int EntidadReceptora
        {
            get
            {
                return this.entidadReceptora;
            }
            set
            {
                this.entidadReceptora = value;
            }
        }

        /// <summary>
        /// Número de serie del certificado del contribuyente que presenta la declaración.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("CertificateSerialNumber", Order = 11)]
        public string NumeroSerie
        {
            get
            {
                return this.numeroSerie;
            }
            set
            {
                this.numeroSerie = value;
            }
        }

        /// <summary>
        /// Ruta del archivo de la declaración.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("DocumentPath", Order = 12)]
        public string RutaArchivo
        {
            get
            {
                return this.rutaArchivo;
            }
            set
            {
                this.rutaArchivo = value;
            }
        }

        /// <summary>
        /// Tamaño del archivo de la declaración.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("FileSize", Order = 13)]
        public int TamañoArchivo
        {
            get
            {
                return this.tamañoArchivo;
            }
            set
            {
                this.tamañoArchivo = value;
            }
        }
    }
}
