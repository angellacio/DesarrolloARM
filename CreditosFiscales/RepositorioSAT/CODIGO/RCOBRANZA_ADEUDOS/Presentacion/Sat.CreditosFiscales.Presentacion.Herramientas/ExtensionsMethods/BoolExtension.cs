
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Presentacion.Herramientas.ExtensionsMethods:Sat.CreditosFiscales.Presentacion.Herramientas.ExtensionsMethods.BoolExtension:1:12/07/2013[Assembly:1.0:12/07/2013])

namespace Sat.CreditosFiscales.Presentacion.Herramientas.ExtensionsMethods
{
    /// <summary>
    /// Métodos de extensión para los Booleanos
    /// </summary>
    public static class BoolExtension
    {
        /// <summary>
        /// Regresa Sí si es verdadero y No si es falso
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public static string BoolToWord(this bool valor)
        {
            return valor ? "Sí" : "No";
        }

        /// <summary>
        /// Regresa Sí si es verdadero y No si es falso
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public static string BoolToWord(this bool? valor)
        {
            string palabra = "-";
            if (valor != null)
            {
                palabra = (bool)valor ? "Sí" : "No";
            }

            return palabra;
        }
    }
}
