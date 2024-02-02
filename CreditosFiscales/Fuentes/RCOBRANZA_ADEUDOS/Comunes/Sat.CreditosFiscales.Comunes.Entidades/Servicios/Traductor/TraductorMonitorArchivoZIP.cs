using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sat.CreditosFiscales.Comunes.Entidades.Servicios.Traductor
{
    public class TraductorMonitorArchivoZIP
    {
        public TraductorMonitorArchivoZIP()
        {
            DatosBusqueda = new List<TraductorMonitorArchivoZIPBusqueda>();
        }
        public int IdTipoPago { get; set; }
        public string ArchivoZIP { get; set; }
        public string FechaInicio { set; get; }
        public string FechaFin { set; get; }
        public List<TraductorMonitorArchivoZIPBusqueda> DatosBusqueda { get; set; }
    }
}
