
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Comunes.Herramientas:Sat.CreditosFiscales.Comunes.Herramientas.Constantes:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sat.CreditosFiscales.Comunes.Herramientas
{
    /// <summary>
    /// Clase que contiene valores constantes utilizados dentro de el programa en todas las capas.
    /// </summary>
    public static class Constantes
    {
        /// <summary>
        /// Fin de archivo.
        /// </summary>
        public static string EOF = "EOF";

        /// <summary>
        /// Tipo de forumulario para líneas de captura de pagos.
        /// </summary>
        public static string TipoFormulario = "PRLC";

        /// <summary>
        /// Versión de la reglas que definen la línea de captura.
        /// </summary>
        public static string Version = "01";

        /// <summary>
        /// Identificador del tipo de documento.
        /// </summary>
        public static short PagoParcialidades = 22;

        /// <summary>
        /// Tipo de formulario de pago para SIR
        /// </summary>
        public static string PLCE01 = "PLCE01";

        /// <summary>
        /// Tipo de formulario de pago para SIR
        /// </summary>
        public static string PLCE02 = "PLCE02";

        /// <summary>
        /// Tipo de formulario de pago para SIR
        /// </summary>
        public static string PLCE03 = "PLCE03";

        /// <summary>
        /// Tipo de formulario de pago para SIR
        /// </summary>
        public static string PLCP01 = "PLCP01";

        /// <summary>
        /// Tipo de formulario de pago para SIR
        /// </summary>
        public static string PLCP02 = "PLCP02";

        /// <summary>
        /// Tipo de formulario de pago para SIR
        /// </summary>
        public static string PLCP03 = "PLCP03";

        /// <summary>
        /// Constante para pagos virtuales.
        /// </summary>
        public static string Virtual = "Virtual";
    }
}
