
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Procesamiento.LogicaNegocio.Servicios:Sat.CreditosFiscales.Procesamiento.LogicaNegocio.Servicios.ProxyManagerImpresion:1:12/07/2013[Assembly:1.0:12/07/2013])

using Sat.CreditosFiscales.Procesamiento.LogicaNegocio.ServicioGeneraFormatoImpresion;
using System;
using System.Collections.Generic;
using Sat.CreditosFiscales.Comunes.Herramientas;
using System.Diagnostics;


namespace Sat.CreditosFiscales.Procesamiento.LogicaNegocio.Servicios
{
    /// <summary>
    /// Clase para el manejo del proxy del servicio de impresión
    /// </summary>
    public class ProxyManagerImpresion
    {
        /// <summary>
        /// Método proxy para la generación de un archivo en mapa de bytes de acuerdo a una lista de folios y un template requerido,
        /// invocando al servicio de impresión
        /// </summary>
        /// <param name="template">Enumeración del tipo de template<see cref="EnumTemplate"/></param>
        /// <param name="listaDeFolios">Lista de folios</param>
        /// <returns>Mapa de bytes</returns>
        public static byte[] GeneraArchivos(EnumTemplate template, List<string> listaDeFolios)
        {
            // Invocación al servicio de impresión Sat.CreditosFiscales.Impresion
            InterceptorMensajes interceptor = new InterceptorMensajes();
            Stopwatch duracion = new Stopwatch();
            try
            {
                using (var servicio = new ServicioGeneraFormatoImpresion.ServicioGeneraFormatoImpresionClient())
                {
                    duracion.Start();
                    return servicio.GeneraArchivo(template, listaDeFolios);
                }
            }
            catch (Exception ex)
            {
                if (duracion.IsRunning)
                    duracion.Stop();
            }
            finally
            {
                if (duracion.IsRunning)
                    duracion.Stop();

            }
            return null;

        }

    }
}
