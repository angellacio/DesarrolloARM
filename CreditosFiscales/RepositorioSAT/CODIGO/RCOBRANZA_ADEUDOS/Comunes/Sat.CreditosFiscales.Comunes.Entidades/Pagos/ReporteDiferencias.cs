
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Comunes.Entidades.Pagos:Sat.CreditosFiscales.Comunes.Entidades.Pagos.ReporteDiferencias:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;

namespace Sat.CreditosFiscales.Comunes.Entidades.Pagos
{
    /// <summary>
    /// Clase que se utiliza para la generación del Log del proceso de pagos.
    /// </summary>
    public class ReporteDiferencias
    {
        /// <summary>
        /// Solicitud que generó la línea de captura.
        /// </summary>
        public string IdSolicitud { get; set; }
        /// <summary>
        /// Clave de local de recaudación.
        /// </summary>
        public int Alr { get; set; }
        /// <summary>
        /// Rfc del contribuyente.
        /// </summary>        
        public string Rfc { get; set; }
        /// <summary>
        /// Línea de captura actualizada.
        /// </summary>
        public string LineaDeCaptura { get; set; }
        /// <summary>
        /// Monto pagado de la línea de captura.
        /// </summary>        
        public decimal MontoPagadoLinea { get; set; }
        /// <summary>
        /// Monto a pagar que recibió el aplicativo.
        /// </summary>
        public decimal MontoPagarAplicativo { get; set; }

        /// <summary>
        /// Diferencia entre importe condonado e importe pagado.
        /// </summary>        
        public decimal ImporteDiferencia
        {
            get { return Math.Round((MontoPagarAplicativo - MontoPagadoLinea), 0); }
        }

        /// <summary>
        /// Fecha en que el pago fue procesado.
        /// </summary>
        public DateTime FechaProcesado { get; set; }
        /// <summary>
        /// Fecha en que fue realizada la operación.
        /// </summary>       
        public DateTime FechaOperacion { get; set; }
        /// <summary>
        /// Nombre del archivo en el que vienen los pagos procesados.
        /// </summary>
        public string NombreArchivo { get; set; }
        /// <summary>
        /// Descripción de ALR.
        /// </summary>
        public string DescripcionAlr { get; set; }

        /// <summary>
        /// Identificador para generar bitácora de líneas de captura masiva.
        /// </summary>
        public bool EsLineaCapturaMasiva { get; set; }
    }
}
