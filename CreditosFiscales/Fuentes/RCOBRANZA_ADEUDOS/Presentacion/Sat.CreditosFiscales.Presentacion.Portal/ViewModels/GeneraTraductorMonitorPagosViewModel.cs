using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sat.CreditosFiscales.Comunes.Entidades.Servicios.Traductor;

namespace Sat.CreditosFiscales.Presentacion.Portal.ViewModels
{
    public class GeneraTraductorMonitorPagosViewMode
    {
        public GeneraTraductorMonitorPagosViewMode()
        {
            MonitorPagoDetalle = new TraductorMonitorPagoDetalle();
            MonitorArchivoZIP = new TraductorMonitorArchivoZIP();
            MonitorTareaProgramada = new TraductorMonitorTareaProgramada();
        }
        public int IdMonitor { get; set; }
        public TraductorMonitorPagoDetalle MonitorPagoDetalle { get; set; }
        public TraductorMonitorArchivoZIP MonitorArchivoZIP { get; set; }
        public TraductorMonitorTareaProgramada MonitorTareaProgramada { get; set; }
        public Dictionary<int, string> listaMonitoreo
        {
            get
            {
                return new Dictionary<int, string>() {
                    { -1, "Seleccione una opción" },
                    { 1, "Detalle de pagos" },
                    { 2, "Archivos ZIP" },
                    { 3, "Tarea Programada" }
                };
            }
        }
        public Dictionary<int, string> listaTipoPago
        {
            get
            {
                return new Dictionary<int, string>()
                {
                    { -1, "Todos" },
                    { 1, "Pagos Físicos" },
                    { 2, "Pagos Virtuales" }
                };
            }
        }
        public Dictionary<int, string> listaEstatus { get; set; }
        public Dictionary<int, string> listaBanco { get; set; }
    }
}