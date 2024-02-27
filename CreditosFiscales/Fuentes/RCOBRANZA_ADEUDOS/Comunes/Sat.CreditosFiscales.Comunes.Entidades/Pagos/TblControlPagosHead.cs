using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sat.CreditosFiscales.Comunes.Entidades.Pagos
{
    public class TblControlPagosHead
    {
        public long idArchivo { get; set; }
        public long IdProceso { get; set; }
        public string NombreArchivo { get; set; }
        public int IdBanco { get; set; }
        public string FechaPago { get; set; }
        public int NumRegistros { get; set; }
        public long Importe { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaProceso { get; set; }
        public int Estado { get; set; }

        public string NombreEquipo { get; set; }

        public int IdError { get; set; }











        









    }

}
