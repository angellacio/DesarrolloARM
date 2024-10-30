
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Presentacion.Herramientas.ExtensionsMethods:Sat.CreditosFiscales.Presentacion.Herramientas.ExtensionsMethods.IntExtension:1:12/07/2013[Assembly:1.0:12/07/2013])

namespace Sat.CreditosFiscales.Presentacion.Herramientas.ExtensionsMethods
{
    /// <summary>
    /// Métodos de extensión para los HtmlHelper
    /// </summary>
    public static class IntExtension
    {
        /// <summary>
        /// Regresa Sí si es verdadero y No si es falso
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public static string BoolToWord(this int valor)
        {
            string palabra = valor.ToString();
            if (valor.Equals(0))
            {
                palabra = "No";
            }
            else if(valor.Equals(1))
            {
                palabra = "Sí";
            }
            return palabra;
        }
    }
}
