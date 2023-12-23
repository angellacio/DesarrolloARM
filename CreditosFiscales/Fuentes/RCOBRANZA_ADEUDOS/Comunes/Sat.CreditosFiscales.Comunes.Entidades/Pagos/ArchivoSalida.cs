
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Comunes.Entidades.Pagos:Sat.CreditosFiscales.Comunes.Entidades.Pagos.ArchivoSalida:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sat.CreditosFiscales.Comunes.Entidades.Pagos
{
    /// <summary>
    /// Clase que contiene la información de los archivos de salida para descargo de pagos.
    /// </summary>
    public class ArchivoSalida
    {
        /// <summary>
        /// Id de proceso de pagos.
        /// </summary>
        public long IdProceso { get; set; }
        /// <summary>
        /// Locarl de recaudación.
        /// </summary>
        public int IdAlr { get; set; }

        /// <summary>
        /// Importe pagado por el crédito.
        /// </summary>
        public long ImportePagado { get; set; }
        /// <summary>
        /// Contenido del archivo de pagos.
        /// </summary>
        public string Contenido { get; set; }
    }
}
