
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Comunes.Entidades.Pagos:Sat.CreditosFiscales.Comunes.Entidades.Pagos.InfoArchivoSIR:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;

namespace Sat.CreditosFiscales.Comunes.Entidades.Pagos
{
    /// <summary>
    /// Clase que define las lineas de captura para el archivo de descargo en SIR.
    /// </summary>
    public class InfoArchivoSIR
    {
        /// <summary>
        /// Id de la solicitud actualizada.
        /// </summary>
        public string IdSolicitud { get; set; }
        /// <summary>
        /// Clave de la local de recaudación donde se realizó la actualización
        /// </summary>
        public short? Alr { get; set; }
        /// <summary>
        /// Número de documento determinante.
        /// </summary>
        public string Derterminante { get; set; }
        /// <summary>
        /// Caracteres que definen la línea de captura que fue pagada en el banco.
        /// </summary>
        public string LineaDeCaptura { get; set; }
        /// <summary>
        /// RFC del contribuyente que generó la línea de captura.
        /// </summary>
        public string RFC { get; set; }
        /// <summary>
        /// Número de ejercicio.
        /// </summary>
        public int Ejercicio { get; set; }
        /// <summary>
        /// Importe pagado de la línea de captura.
        /// </summary>
        public long ImportePagado { get; set; }
        /// <summary>
        /// Período pagado.
        /// </summary>
        public int Periodo { get; set; }
        /// <summary>
        /// Número de crédito a actualizar en el sistema SIR.
        /// </summary>
        public int? NumeroDeCreditoSir { get; set; }
        /// <summary>
        /// Importe actualizado del documento de terminante.
        /// </summary>
        public decimal ImporteActualizado { get; set; }
        /// <summary>
        /// Monto a pagar por crédito perteneciente al documento determinate.
        /// </summary>
        public int MontoPagar { get; set; }
        /// <summary>
        /// Monto a condonar por crédito perteneciente.
        /// </summary>
        public long MontoCondonar { get; set; }
        /// <summary>
        /// Fecha de operación y Fecha de pago.
        /// </summary>
        public DateTime FechaPago { get; set; }
        /// <summary>
        /// Información de archivo de pagos de DYP, que se utiliza para generar el nuevo archivo hacia SIR.
        /// </summary>
        public string DetalleArchivo { get; set; }
        /// <summary>
        /// Identificador de Líneas de Captura Masiva.
        /// </summary>
        public bool EsLineaCapturaMasiva { get; set; }

        /// <summary>
        /// Tipo de formulario.
        /// </summary>
        public string TipoDeFormulario { get; set; }

        /// <summary>
        /// Número de parcialidad para pago en parcialidades.
        /// </summary>
        public int? NumeroDeParcialidad { get; set; }

        /// <summary>
        /// Identificador del tipo de documento en el que se generó la línea de captura.
        /// </summary>
        public int IdTipoDocumento { get; set; }
    }
}
