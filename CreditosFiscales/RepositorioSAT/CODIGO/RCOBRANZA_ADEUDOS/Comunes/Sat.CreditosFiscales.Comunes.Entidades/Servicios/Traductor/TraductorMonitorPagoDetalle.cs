using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sat.CreditosFiscales.Comunes.Entidades.Servicios.Traductor
{
    public class TraductorMonitorPagoDetalle
    {
        public TraductorMonitorPagoDetalle()
        {
            DatosBusqueda = new List<TraductorMonitorPagoDetalleBusqueda>();
        }
        public int IdTipoPago { get; set; }
        public int IdEstatus { get; set; }
        public int IdBanco { get; set; }
        public string FechaInicio { set; get; }
        public string FechaFin { set; get; }
        public string LineaCaptura { get; set; }
        public List<TraductorMonitorPagoDetalleBusqueda> DatosBusqueda { get; set; }
    }
}
