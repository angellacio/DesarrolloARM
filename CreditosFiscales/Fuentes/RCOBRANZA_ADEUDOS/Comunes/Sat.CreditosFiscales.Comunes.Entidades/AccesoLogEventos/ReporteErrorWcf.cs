
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Comunes.Entidades.AccesoLogEventos:Sat.CreditosFiscales.Comunes.Entidades.AccesoLogEventos.ReporteErrorWcf:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Sat.CreditosFiscales.Comunes.Entidades.AccesoLogEventos
{
    [DataContract]
    [Serializable]
    public class ReporteErrorWcf
    {
        [DataMember]
        public string Mensaje {get; set;}

        [DataMember]
        public string InnerException { get; set; }
        

        public ReporteErrorWcf(string mensaje)
        {
            this.Mensaje = mensaje;
           
            //this.InnerException = innerException;
        }
    }
}
