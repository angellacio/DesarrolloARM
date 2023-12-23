
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Comunes.Entidades.GeneraFormato:Sat.CreditosFiscales.Comunes.Entidades.GeneraFormato.DocumentosContribuyentePuro:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Sat.CreditosFiscales.Comunes.Entidades.GeneraFormato
{
    public class DocumentosContribuyentePuro
    {
        [DataMember]
        public Usuario Usuario { get; set; }
        [DataMember]
        public List<DocumentoDeterminante> Documentos { get; set; }
    }
}
