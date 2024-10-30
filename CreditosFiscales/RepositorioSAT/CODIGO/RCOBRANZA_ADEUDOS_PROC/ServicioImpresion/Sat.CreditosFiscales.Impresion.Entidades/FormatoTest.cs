using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Sat.CreditosFiscales.Impresion.Entidades
{
     [XmlRoot(ElementName = "Documentos")]
    public class FormatoTest
    {

         /// <summary>
        /// Tipo de template
        /// </summary>
        [XmlAttribute]
        public string template { set { } get { return "Configuracion"; } }

        /// <summary>
        /// Lista de líneas de captura
        /// </summary>
        [XmlElement("LC")]
        public List<FPLCTest> LC { set; get; }

        /// <summary>
        /// Constructor
        /// </summary>
        public FormatoTest()
        {
            LC = new List<FPLCTest>();
        }

    }

     public class FPLCTest
     {
         /// <summary>
         /// Tipo de template
         /// </summary>
         [XmlAttribute]
         public string template { set { } get { return "LineaCaptura"; } }
         /// <summary>
         /// Número de versión del template
         /// </summary>
         [XmlAttribute]
         public int version { set; get; }
         /// <summary>
         /// ALR
         /// </summary>
         public string ALR { set; get; }
         /// <summary>
         /// Rfc del contribuyente
         /// </summary>
         public string RFC { set; get; }
         /// <summary>
         /// Folio de la línea de captura
         /// </summary>
         public string FolioLC { set; get; }
         /// <summary>
         /// Nombre o razón social del contribuyente
         /// </summary>
         public string Nombre { set; get; }

         /// <summary>
         /// Lista de documentos o resoluciones
         /// </summary>
         [XmlElement("Documento")]
         public List<FPDocumentoTest> Documento { set; get; }
         /// <summary>
         /// Total de importe actualizado
         /// </summary>
         public string ImporteActualizadoT { set; get; }
         /// <summary>
         /// Total de importe a pagar
         /// </summary>
         public string ImporteAPagarT { set; get; }
         /// <summary>
         /// Fecha y hora de emisión
         /// </summary>
         public string FechaYHora { set; get; }
         /// <summary>
         /// Fecha de importe actualizado al
         /// </summary>
         public string Fecha { set; get; }
         /// <summary>
         /// Observaciones
         /// </summary>
         public string Observaciones { set; get; }
         /// <summary>
         /// Número de línea de captura
         /// </summary>
         public string NumLC { set; get; }

         public string CodigoBarras { set; get; }

         /// <summary>
         /// Fecha de vigencia hasta
         /// </summary>
         public string FechaVigencia { set; get; }

         public FPLCTest()
         {
             Documento = new List<FPDocumentoTest>();
         }
     }

     public class FPDocumentoTest
     {
         /// <summary>
         /// Tipo de template
         /// </summary>
         [XmlAttribute]
         public string template { set { } get { return "LineaCapturaDocumento"; } }
         /// <summary>
         /// Versión del template
         /// </summary>
         [XmlAttribute]
         public int version { set; get; }
         /// <summary>
         /// Número de documento o resolución
         /// </summary>
         public string NumDocumento { set; get; }
         /// <summary>
         /// Importe actualizado
         /// </summary>
         public string ImporteActualizado { set; get; }
         /// <summary>
         /// Importe a pagar
         /// </summary>
         public string ImporteAPagar { set; get; }
     }
}
