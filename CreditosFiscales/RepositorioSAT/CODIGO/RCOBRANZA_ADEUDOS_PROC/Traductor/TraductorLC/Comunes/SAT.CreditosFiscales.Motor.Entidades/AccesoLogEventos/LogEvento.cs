
//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.Entidades:SAT.CreditosFiscales.Motor.Entidades.LogEvento:1:12/07/2012[Assembly:1.0:12/07/2013])

using System;


namespace Sat.CreditosFiscales.Motor.Entidades.AccesoLogEventos
{
    /// <summary>
    /// Clase de entidades para el log de eventos
    /// </summary>
    public class LogEvento
    {
        public Guid Id { set; get; }
        public string Aplicacion { set; get; }
        public string Evento { set; get; }
        public string Mensaje { set; get; }
        public int IdTipoEvento { set; get; }
        public string TipoEvento
        {
            get
            {
                return ((EnumTipoEvento)IdTipoEvento).ToString();
            }
        }
        public DateTime FechaOrigen { set; get; }
    }
}
