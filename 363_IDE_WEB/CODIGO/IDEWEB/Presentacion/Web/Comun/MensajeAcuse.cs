//@(#)SCADE2(W:SKDN08515FF4:Sat.Scade.Net.IDE.Presentacion.Web:MensajeAcuse:0:23/Diciembre/2008[Sat.Scade.Net.IDE.Presentacion.Web:0:23/Diciembre/2008]) 
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Sat.Scade.Net.IDE.Presentacion.Web
{
    /// <summary>
    /// Clase que contiene los campos del Acuse que se mostrara al contribuyente
    /// </summary>
    [Serializable, XmlRoot("mensajeAcuse")]
    public class MensajeAcuse
    {
        private string mensajeId;
        private string rfcAutenticacion;
        private string rfcContribuyente;
        private string mensaje;
        private string folio;
        private string selloDigital;
        private string fechaRecepcion;
        private string horaRecepcion;
        private string numeroOperacion;
        private string nombreArchivo;
        private int estatus;
        private int medioPresentacion;
        private int tamañoArchivo;


        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string MensajeID
        {
            get
            {
                return this.mensajeId;
            }
            set
            {
                this.mensajeId = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
        public String FechaRecepcion
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

        [System.Xml.Serialization.XmlElementAttribute(Order = 8)]
        public String HoraRecepcion
        {
            get
            {
                return this.horaRecepcion;
            }
            set
            {
                this.horaRecepcion = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 9)]
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

        [System.Xml.Serialization.XmlElementAttribute(Order = 10)]
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

        [System.Xml.Serialization.XmlElementAttribute(Order = 11)]
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

        [System.Xml.Serialization.XmlElementAttribute(Order = 12)]
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
        /// Serializa el objeto
        /// </summary>
        /// <returns>Regresa el documento XML como cadena de Texto</returns>
        public override string ToString()
        {
            MemoryStream output = new MemoryStream();
            XmlSerializer serializer = new XmlSerializer(typeof(MensajeAcuse));
            serializer.Serialize(output, this);

            output.Position = 0;

            StreamReader reader = new StreamReader(output);
            string xmlString = reader.ReadToEnd();

            return xmlString;
        }

        /// <summary>
        /// Deserializa el objeto
        /// </summary>
        /// <param name="xmlString">Cadena del mensaje xml</param>
        /// <returns>regresa un mensaje XML</returns>
        public static MensajeAcuse Deserialize(string xmlString)
        {
            MemoryStream stream = new MemoryStream(System.Text.UnicodeEncoding.Unicode.GetBytes(xmlString));
            XmlSerializer ser = new XmlSerializer(typeof(MensajeAcuse));

            return ser.Deserialize(stream) as MensajeAcuse;
        }
    }
}
