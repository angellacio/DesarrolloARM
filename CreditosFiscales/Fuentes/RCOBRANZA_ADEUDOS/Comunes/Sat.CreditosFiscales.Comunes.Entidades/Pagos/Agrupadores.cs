
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Comunes.Entidades.Pagos:Sat.CreditosFiscales.Comunes.Entidades.Pagos.Agrupadores:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sat.CreditosFiscales.Comunes.Entidades.Pagos
{
    /// <summary>
    /// Clase para que representa la colección de agrupadores.
    /// </summary>
    public class Agrupadores
    {
        public Agrupadores()
        {
            Conceptos = new List<ConceptoOriginal>();
        }
        /// <summary>
        /// Identificador del agrupador.
        /// </summary>
        public int IdAgrupador { get; set; }
        
        /// <summary>
        /// Identificador de procesamiento que realizó la solicitud.
        /// </summary>
        public string IdProcesamiento { get; set; }

        /// <summary>
        /// Cadena que representa la llave de la información de el documento determinante.
        /// </summary>
        public string ValorAgrupador { get; set; }

        /// <summary>
        /// Importe a pagar por documento.
        /// </summary>
        public decimal ImportePagar { get; set; }

        /// <summary>
        /// Saldo informativo con el total a pagar.
        /// </summary>
        public decimal? SaldoInformativo { get; set; }

        /// <summary>
        /// Lista de conceptos padres e hijos.
        /// </summary>
        public List<ConceptoOriginal> Conceptos { get; set; } 
    }
}
