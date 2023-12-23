
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Comunes.Entidades.Pagos:Sat.CreditosFiscales.Comunes.Entidades.Pagos.RutaArchivos:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sat.CreditosFiscales.Comunes.Entidades.Pagos
{
    /// <summary>
    /// Clase que sirve para obtener la información del repositorio rutas de archivos
    /// </summary>
    public class RutaArchivos
    {
        /// <summary>
        /// Identificador del repositorrio.
        /// </summary>
        public int IdRepositorio { get; set; }

        /// <summary>
        /// Nombre del repositorio.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Ruta del física del servidor donde se encuentra el repositorio.
        /// </summary>
        public string RutaRepositorio { get; set; }

        /// <summary>
        /// Estado del registro en base de datos.
        /// </summary>
        public bool IdEstatus { get; set; }
    }
}
