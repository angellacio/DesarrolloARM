
//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.Entidades:SAT.CreditosFiscales.Motor.Entidades.CatTransaccion:1:12/07/2012[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAT.CreditosFiscales.Motor.Entidades.Catalogos
{
    /// <summary>
    /// Clase que contiene la información de las transacciones enviadas a D y P.
    /// </summary>
    public class CatTransaccion
    {
        /// <summary>
        /// Identificador de la transacción.        
        /// </summary>
        public string IdTransaccion { get; set; }

        /// <summary>
        /// Descripción dela transacción de D y P.
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Identificador del tipo de transacción.
        /// </summary>
        public int IdTipoTransaccion { get; set; }

        /// <summary>
        /// Descripción del tipo de transacción.
        /// </summary>
        public string TipoTransaccion { get; set; }

        /// <summary>
        /// Identificador del tipo de documento generado por la solicitud de línea de captura.
        /// </summary>
        public short IdTipoDocumento { get; set; }

        /// <summary>
        /// Identificador de la aplicación que realizó la petición de línea de captura.
        /// </summary>
        public short IdAplicacion { get; set; }

        /// <summary>
        /// Valida si la transacción es requerida en el documento y aplicación que realizan la solicitud de línea de captura.
        /// </summary>
        public bool EsRequerido { get; set; }
    }
}
