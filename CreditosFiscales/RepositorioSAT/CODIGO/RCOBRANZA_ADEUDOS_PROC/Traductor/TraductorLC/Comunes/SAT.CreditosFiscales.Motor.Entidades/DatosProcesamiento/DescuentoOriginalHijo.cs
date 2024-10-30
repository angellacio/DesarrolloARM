
//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.Entidades:SAT.CreditosFiscales.Motor.Entidades.DescuentoOriginalHijo:1:12/07/2012[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAT.CreditosFiscales.Motor.Entidades.DatosProcesamiento
{
    /// <summary>
    /// Entidad del descuento hijo
    /// </summary>
    public class DescuentoOriginalHijo
    {
        /// <summary>
        /// Identificador del agrupador
        /// </summary>
        public int IdAgrupador { get; set; }

        /// <summary>
        /// Identificador del concepto original
        /// </summary>
        public int IdConceptoOriginal { get; set; }

        /// <summary>
        /// Identificador del procesamiento
        /// </summary>
        public Guid IdProcesamiento { get; set; }

        /// <summary>
        /// Id del descuento
        /// </summary>
        public string IdDescuento { get; set; }

        /// <summary>
        /// Clave de Concepto Original
        /// </summary>
        public string ConceptoOriginal { get; set; }

        /// <summary>
        /// Clave del Concepto Hijo
        /// </summary>
        public string ConceptoOriginalHijo { get; set; }

        /// <summary>
        /// Importe del descuento
        /// </summary>
        public decimal ImporteDescuento { get; set; }
    }
}
