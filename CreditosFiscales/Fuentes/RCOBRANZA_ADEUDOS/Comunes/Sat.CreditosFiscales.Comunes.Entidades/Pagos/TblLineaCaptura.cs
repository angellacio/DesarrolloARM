
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Comunes.Entidades.Pagos:Sat.CreditosFiscales.Comunes.Entidades.Pagos.TblLineaCaptura:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sat.CreditosFiscales.Comunes.Entidades.Pagos
{
    /// <summary>
    /// Clase que contiene la información general de la línea de captura.
    /// </summary>
    public class TblLineaCaptura
    {
        /// <summary>
        /// Identificador de la aplicación que generó la línea de captura
        /// </summary>

        public short IdAplicacion { get; set; }

        //public long IdLineaCaptura { get; set; }
        /// <summary>
        /// Identificador de la solicitud de procesamiento del aplicativo.
        /// </summary>
        public string IdSolicitud { get; set; }

        /// <summary>
        /// Línea de captura.
        /// </summary>
        public string LineaCaptura { get; set; }

        /// <summary>
        /// Monto pagar de la línea de captura
        /// </summary>
        public long MontoLineaCaptura { get; set; }

        /// <summary>
        /// Fecha de vencimiento.
        /// </summary>
        public System.DateTime FechaVencimiento { get; set; }

        /// <summary>
        /// Monto de la condonación o las reducciónes.
        /// </summary>
        public decimal MontoCondonacion { get; set; }

        /// <summary>
        /// Importe total diferencia entre monto línea y monto condonación.
        /// </summary>
        public decimal ImporteTotal { get; set; }

        /// <summary>
        /// Fecha de pago de la línea de captura.
        /// </summary>
        public Nullable<System.DateTime> FechaPago { get; set; }

        /// <summary>
        /// Fecha de Emisión de la línea de captura.
        /// </summary>
        public Nullable<System.DateTime> FechaEmision { get; set; }        

        /// <summary>
        /// Estado de la solicitud de la línea de captura.
        /// </summary>
        public int IdEstadoSolicitud { get; set; }

        /// <summary>
        /// Folio de la solicitud de la línea de captura.
        /// </summary>
        public string FolioSolicitud { get; set; }

        /// <summary>
        /// Versión de la línea de captura.
        /// </summary>
        public Nullable<int> Version { get; set; }

        /// <summary>
        /// Rfc del contribuyente que generó la línea de captura.
        /// </summary>
        public string Rfc { get; set; }

        /// <summary>
        /// Tipo de persona que generó la línea de captura.
        /// </summary>
        public Nullable<byte> IdTipoPersona { get; set; }

        /// <summary>
        /// Fecha de solicitud de línea de captura.
        /// </summary>
        public System.DateTime FechaSolicitud { get; set; }

        /// <summary>
        /// Identificador de la local que contiene el aduedo.
        /// </summary>
        public Nullable<short> IdALR { get; set; }

        /// <summary>
        /// Razón social del tipo persona moral.
        /// </summary>
        public string RazonSocial { get; set; }

        /// <summary>
        /// Nombre del contribuyente.
        /// </summary>
        public string Nombres { get; set; }

        /// <summary>
        /// Apellido Paterno.
        /// </summary>
        public string ApellidoPaterno { get; set; }

        /// <summary>
        /// Apellio Materno.
        /// </summary>
        public string ApellidoMaterno { get; set; }

        /// <summary>
        /// Número de parcialidad.
        /// </summary>
        public int? NumeroParcialidad { get; set; }

        /// <summary>
        /// Tipo de documento.
        /// </summary>
        public short IdTipoDocumento { get; set; }
        
    }
}
