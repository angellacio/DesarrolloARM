
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Comunes.Entidades.AccesoLogEventos:Sat.CreditosFiscales.Comunes.Entidades.AccesoLogEventos.LogEvento:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;


namespace Sat.CreditosFiscales.Comunes.Entidades.AccesoLogEventos
{
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
        public string Xml { set; get; }
    }
}
