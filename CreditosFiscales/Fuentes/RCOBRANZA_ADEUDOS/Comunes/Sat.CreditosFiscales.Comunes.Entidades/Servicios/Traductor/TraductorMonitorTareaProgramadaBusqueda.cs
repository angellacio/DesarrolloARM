using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sat.CreditosFiscales.Comunes.Entidades.Servicios.Traductor
{
    public class TraductorMonitorTareaProgramadaBusqueda
    {
        public TraductorMonitorTareaProgramadaBusqueda() { }
        public string TipoPago { get; set; }
        public long? IdProceso { get; set; }
        public string NombreArchivo { get; set; }
        public string Estatus { get; set; }
        public string FechaPresentacion { get; set; }
        public DateTime? FechaProceso { get; set; }
        public string NombreArchivoZIP { get; set; }
        public long? Importe { set; get; }
        public short? NumeroPagos { set; get; }
        public string Error { get; set; }
    }
}
