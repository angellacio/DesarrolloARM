
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Comunes.Entidades.AccesoLogEventos:Sat.CreditosFiscales.Comunes.Entidades.AccesoLogEventos.ExcepcionTipificada:1:12/07/2013[Assembly:1.0:12/07/2013])
	
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ServiceModel;

namespace Sat.CreditosFiscales.Comunes.Entidades.AccesoLogEventos
{
  
    public class ExcepcionTipificada : Exception
    {
        public readonly string Ticket;

        public readonly string Mensaje;

        
      

        public ExcepcionTipificada(string mensaje, Exception ex, string ticket)
            : base(mensaje, ex)
        {
            this.Ticket = ticket;
            //string msgTicket = string.IsNullOrWhiteSpace(ticket) ? string.Empty : string.Format("Para darle seguimiento por favor conserve el siguiente número:{0}.", ticket);
            //this.Mensaje = string.Format("{0} {1}", mensaje, msgTicket);            

            this.Mensaje = string.Format("{0}<br />({1})", mensaje, ticket);            
        }
        
    }
}
