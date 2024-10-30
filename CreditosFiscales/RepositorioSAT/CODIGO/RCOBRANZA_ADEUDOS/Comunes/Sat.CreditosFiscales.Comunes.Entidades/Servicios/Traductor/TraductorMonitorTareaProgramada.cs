using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sat.CreditosFiscales.Comunes.Entidades.Servicios.Traductor
{
    public class TraductorMonitorTareaProgramada
    {
        public TraductorMonitorTareaProgramada()
        {
            DatosBusqueda = new List<TraductorMonitorTareaProgramadaBusqueda>();
        }
        public int IdTipoPago { get; set; }
        public int IdEstatus { get; set; }
        public string FechaInicio { set; get; }
        public string FechaFin { set; get; }
        public List<TraductorMonitorTareaProgramadaBusqueda> DatosBusqueda { get; set; }
    }
}
