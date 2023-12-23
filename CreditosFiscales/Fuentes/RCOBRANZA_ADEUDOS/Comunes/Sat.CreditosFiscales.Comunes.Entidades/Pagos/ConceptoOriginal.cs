
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Comunes.Entidades.Pagos:Sat.CreditosFiscales.Comunes.Entidades.Pagos.ConceptoOriginal:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sat.CreditosFiscales.Comunes.Entidades.Pagos
{
    /// <summary>
    /// Entidad que se utiliza para almacenar el valor de los conceptos.
    /// </summary>
    public class ConceptoOriginal
    {
        private bool _esPadre;

        /// <summary>
        /// Identificador del agrupador del documento al que pertenece el concepto.
        /// </summary>
        public int IdAgrupador { get; set; }

        /// <summary>
        /// Identificador de la solicitud en la que el aplicativo solicitó su línea de captura.
        /// </summary>
        public string IdSolicitud { get; set; }

        /// <summary>
        /// Clave de concepto ley origen.
        /// </summary>
        public string ClaveOrigen { get; set; }

        /// <summary>
        /// Descripción de concepto ley origen.
        /// </summary>
        public string DescripcionOrigen { get; set; }

        /// <summary>
        /// Ejercicio fiscal del concepto.
        /// </summary>
        public int Ejercicio { get; set; }

        /// <summary>
        /// Periodicidad origen dedl cocepto ley.
        /// </summary>
        public string PeridicidadOrigen { get; set; }

        /// <summary>
        /// Periodo origen del concepto ley.
        /// </summary>
        public string PeriodoOrigen { get; set; }

        /// <summary>
        /// Fecha de causación en caso de que no venga el ejercicio.
        /// </summary>
        public DateTime FechaCausacion { get; set; }

        /// <summary>
        /// Número de credito SIR.
        /// </summary>
        public int CreditoSir { get; set; }

        /// <summary>
        /// Importe histórico del crédito SIR.
        /// </summary>
        public decimal ImporteHistorico { get; set; }

        /// <summary>
        /// Importe parte actualizada del concepto ley.
        /// </summary>
        public decimal ImporteParteActualizada { get; set; }

        /// <summary>
        /// Importe pagar por concepto.
        /// </summary>
        public decimal ImportePagar { get; set; }

        /// <summary>
        /// Bandera que indica si el concepto se liquida al 100%.
        /// </summary>
        public bool Liquidar { get; set; }

        /// <summary>
        /// Indica si el concepto es padre.
        /// </summary>
        public bool EsPadre { get; set; }

        /// <summary>
        /// Identificador del concepto original.
        /// </summary>
        public int IdConceptoOriginal { get; set; }

        /// <summary>
        /// Importe de descuentos.
        /// </summary>
        public decimal ImporteDescuentos { get; set; }

        /// <summary>
        /// Motivo id para identificar el concepto.
        /// </summary>
        public string MotivoId { get; set; }
    }
}
