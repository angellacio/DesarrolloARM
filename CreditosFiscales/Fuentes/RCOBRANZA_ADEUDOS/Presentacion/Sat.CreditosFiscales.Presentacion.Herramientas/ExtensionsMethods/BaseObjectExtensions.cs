
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Presentacion.Herramientas.ExtensionsMethods:Sat.CreditosFiscales.Presentacion.Herramientas.ExtensionsMethods.DecimalExtensions:1:12/07/2013[Assembly:1.0:12/07/2013])

namespace Sat.CreditosFiscales.Presentacion.Herramientas.ExtensionsMethods
{
    using System;
    using System.Globalization;
    using System.Web.Mvc;
    /// <summary>
    /// Métodos de extensión para los decimales
    /// </summary>
    public static class DecimalExtensions
    {
        /// <summary>
        /// Regresa un decimal en formato de moneda
        /// </summary>
        /// <param name="cantidad"></param>
        /// <returns></returns>
        public static MvcHtmlString ToCurrencyString(this decimal cantidad)
        {
            return MvcHtmlString.Create(cantidad.ToString("c"));
        }

        /// <summary>
        /// Regresa un decimal en formato de moneda
        /// </summary>
        /// <param name="cantidad"></param>
        /// <returns></returns>
        public static string ToCurrency(this decimal cantidad)
        {
            var cantidadConDecimales = cantidad.ToString("c");
            return cantidad.ToString("c").Substring(0, cantidadConDecimales.Length - 3);
        }

        /// <summary>
        /// Regresa un entero en formato de moneda
        /// </summary>
        /// <param name="cantidad"></param>
        /// <returns></returns>
        public static string ToCurrency(this int cantidad)
        {
            var cantidadConDecimales = cantidad.ToString("c");
            return cantidad.ToString("c").Substring(0, cantidadConDecimales.Length - 3);
        }

        /// <summary>
        /// Regresa una DateTime en formato dd-MMM-aaaa
        /// </summary>
        /// <param name="fecha">Fecha</param>
        /// <returns></returns>
        public static string ToStringCustom(this DateTime fecha)
        {
            return fecha.ToString("dd-MMM-yyyy", CultureInfo.CreateSpecificCulture("es-MX"));
        }

        /// <summary>
        /// Regresa la fecha y hora en formato ddMMyyyy_hhmmss.
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public static string ToFileString(this DateTime fecha)
        {
            return string.Format("{0}_{1}", fecha.ToString("ddMMyyyy"), fecha.TimeOfDay.ToString("hhmmss"));
        }

        /// <summary>
        /// Regresa el nombre de un número de mes
        /// </summary>
        /// <param name="mes">Número de mes</param>
        /// <returns></returns>
        public static string ToMonth(this byte mes)
        {
            var resultado = string.Empty;
            switch (mes)
            {
                case 1:
                    resultado = "Enero";
                    break;
                case 2:
                    resultado = "Febrero";
                    break;
                case 3:
                    resultado = "Marzo";
                    break;
                case 4:
                    resultado = "Abril";
                    break;
                case 5:
                    resultado = "Mayo";
                    break;
                case 6:
                    resultado = "Junio";
                    break;
                case 7:
                    resultado = "Julio";
                    break;
                case 8:
                    resultado = "Agosto";
                    break;
                case 9:
                    resultado = "Septiembre";
                    break;
                case 10:
                    resultado = "Octubre";
                    break;
                case 11:
                    resultado = "Noviembre";
                    break;
                case 12:
                    resultado = "Diciembre";
                    break;
            }
            return resultado;
        }

    }
}
