
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Comunes.Entidades.Pagos:Sat.CreditosFiscales.Comunes.Entidades.Pagos.ConceptoOriginalHijo:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sat.CreditosFiscales.Comunes.Entidades.Pagos
{
    /// <summary>
    /// Clase que contiene las propiedades de el concepto original hijo.
    /// </summary>
    public class ConceptoOriginalHijo
    {
        /// <summary>
        /// Identificador de el procesamiento de la solicitud.
        /// </summary>
        public string IdProcesamiento { get; set; }

        /// <summary>
        /// Identificador del agrupador.
        /// </summary>
        public int IdAgrupador { get; set; }

        /// <summary>
        /// Identificador del concepto original padre.
        /// </summary>
        public string ConceptoOriginalPadre { get; set; }

        /// <summary>
        /// Indentificador del concepto hijo.
        /// </summary>
        public string ConceptoHijo { get; set; }

        /// <summary>
        /// Importe Histórico.
        /// </summary>
        public decimal ImporteHistorico { get; set; }

        /// <summary>
        /// Importe Pagar.
        /// </summary>
        public decimal ImportePagar { get; set; }

        /// <summary>
        /// Ejercicio fiscal del concepto.
        /// </summary>
        public int Ejercicio { get; set; }
    }
}
