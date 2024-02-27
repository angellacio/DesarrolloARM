using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sat.CreditosFiscales.Comunes.Entidades.Pagos
{
    public class TblArchivoZip
    {
        public long IdZip { get; set; }
        public string NombreZip { get; set; }
        public int NumPagos { get; set; }
        public long Importe { get; set; }

        public DateTime FechaCreacion { get; set; }
    }
}
