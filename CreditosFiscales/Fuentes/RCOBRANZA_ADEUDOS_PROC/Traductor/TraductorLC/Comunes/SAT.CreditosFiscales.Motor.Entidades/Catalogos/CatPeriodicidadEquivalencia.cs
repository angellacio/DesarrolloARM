
//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.Entidades:SAT.CreditosFiscales.Motor.Entidades.CatPeriodicidadEquivalencia:1:12/07/2012[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAT.CreditosFiscales.Motor.Entidades.Catalogos
{
    /// <summary>
    /// Catálogo de periodicidad.
    /// </summary>
    public class CatPeriodicidadEquivalencia
    {
        /// <summary>
        /// Id Aplicación para administrar el catálogo.
        /// </summary>
        public short IdAplicacion { get; set; }

        /// <summary>
        /// Id de periodicidad del concepto origen.
        /// </summary>
        public string PeriodicidadOrigen { get; set; }

        /// <summary>
        /// Descripción de la periodicidad origen.
        /// </summary>
        public string DescripcionOrigen { get; set; }

        /// <summary>
        /// Periodicidad de dyP.
        /// </summary>
        public string IdPeriodicidadDyP { get; set; }

        /// <summary>
        /// Descripción de la periodicidad para DyP.
        /// </summary>
        public string DescripcionPeriodicidadDyP { get; set; }

        /// <summary>
        /// Estado del catálogo.
        /// </summary>
        public bool Activo { get; set; }
    }
}
