using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sat.CreditosFiscales.Comunes.Entidades.Servicios.Traductor
{
    public class TraductorMonitorArchivoZIPBusqueda
    {
        public TraductorMonitorArchivoZIPBusqueda() { }
        public string TipoPago { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string Nombre { get; set; }
        public long? Importe { set; get; }
        public short? NumeroPagos { set; get; }
    }
}
