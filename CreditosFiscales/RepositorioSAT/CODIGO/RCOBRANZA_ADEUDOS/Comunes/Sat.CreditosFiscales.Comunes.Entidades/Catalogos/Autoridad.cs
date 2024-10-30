
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Comunes.Entidades.Catalogos:Sat.CreditosFiscales.Comunes.Entidades.Catalogos.Autoridad:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Sat.CreditosFiscales.Comunes.Entidades.Catalogos
{
    [DataContract]
    public class Autoridad
    {
        [DataMember]
        public int IdAutoridad { set; get; }
        [DataMember]
        public string Descripcion { set; get; }
    }
}
