
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Comunes.Entidades.AccesoLogEventos:Sat.CreditosFiscales.Comunes.Entidades.AccesoLogEventos.MensajesUsuario:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.ServiceModel;

namespace Sat.CreditosFiscales.Comunes.Entidades.AccesoLogEventos
{
  
    public class MensajesUsuario
    {
        public readonly string Ticket;
        public readonly int CodigoMensaje;

        public MensajesUsuario(int codigoMensaje, string ticket)
        {
            this.Ticket = ticket;
            this.CodigoMensaje = codigoMensaje;
        }

       
        
    }
}
