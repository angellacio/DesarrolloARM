
//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.Entidades:SAT.CreditosFiscales.Motor.Entidades.ConceptoOriginalDB:1:12/07/2012[Assembly:1.0:12/07/2013])


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAT.CreditosFiscales.Motor.Entidades.DatosProcesamiento
{
    /// <summary>
    /// Entidad de Concepto Original
    /// </summary>
    public class ConceptoOriginalDB
    {
        /// <summary>
        /// Identificador del concepto original
        /// </summary>
        public int IdConceptoOriginal { get; set; }

        /// <summary>
        /// Identificador del agrupador
        /// </summary>
        public int IdAgrupador { get; set; }

        /// <summary>
        /// Identificador del procesamiento
        /// </summary>
        public Guid IdProcesamiento { get; set; }

        /// <summary>
        /// Concepto Origen
        /// </summary>
        public string ConceptoOrigen { get; set; }

        /// <summary>
        /// Ejercicio Fiscal
        /// </summary>
        public int Ejercicio { get; set; }

        /// <summary>
        /// Fecha de Causación
        /// </summary>
        public string FechaCausacion { get; set; }

        /// <summary>
        /// número del Credito SIR
        /// </summary>
        public int CreditoSir { get; set; }

        /// <summary>
        /// Importe a pagar
        /// </summary>
        public decimal ImportePagar { get; set; }
    }
}
