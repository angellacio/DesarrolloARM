using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sat.CreditosFiscales.Comunes.Entidades.Servicios.Traductor
{
    public class TraductorMonitorPagoDetalleBusqueda
    {
        public TraductorMonitorPagoDetalleBusqueda() { }
        public string TipoPago { get; set; }
        public string FechaPago { get; set; }
        public string HoraPago { get; set; }
        public string Importe { set; get; }
        public string NumeroOperaciones { set; get; }
        public string LineaCaptura { get; set; }
        public string MedioRecepcion { get; set; }
        public string EstatusPago { get; set; }
        public DateTime? FechaProceso { get; set; }
        public string XML { get; set; }
        public string ZIP { get; set; }
        public string Error { get; set; }
    }
}
