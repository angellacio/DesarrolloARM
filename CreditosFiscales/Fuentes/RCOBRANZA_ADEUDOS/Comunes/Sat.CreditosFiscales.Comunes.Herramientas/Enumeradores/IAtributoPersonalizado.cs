
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Comunes.Herramientas.Enumeradores:Sat.CreditosFiscales.Comunes.Herramientas.Enumeradores.IAtributoPersonalizado:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sat.CreditosFiscales.Comunes.Herramientas.Enumeradores
{
    public interface IAtributoPersonalizado<T>
    {
        T Value { get; }
    }
}
