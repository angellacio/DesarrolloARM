
//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.Entidades:SAT.CreditosFiscales.Motor.Entidades.Errores:1:12/07/2012[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAT.CreditosFiscales.Motor.Entidades.ManejoErrores
{
    /// <summary>
    /// Representa la lista de errores.
    /// </summary>
    public class Errores : List<Error>
    { }

    /// <summary>
    /// Objeto que contiene las propiedades que puede contener un Error.
    /// </summary>
    public class Error
    {
        /// <summary>
        /// Excepción generada en la ejecución de la aplicación.
        /// </summary>
        public Exception ErrorAplicacion { get; set; }

        /// <summary>
        /// Error que se mostrará al usuario.
        /// </summary>
        public string ErrorUsuario { get; set; }

        /// <summary>
        /// Código de error.
        /// </summary>
        public int Codigo { get; set; }        
    }
}
