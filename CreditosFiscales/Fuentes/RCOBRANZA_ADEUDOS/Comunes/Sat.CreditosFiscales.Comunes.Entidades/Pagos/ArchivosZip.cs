using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sat.CreditosFiscales.Comunes.Entidades.Pagos
{
    /// <summary>
    /// Clase con los campos para actualziar los datos en las tablas de Control de Archivos ZIP y Detallel
    /// </summary>
    public class ArchivosZip
    {
        TblArchivoZip ArchivoZip { get; set; }
        List<TblControlPagosDet> Detalle { get; set; }
    }
}
