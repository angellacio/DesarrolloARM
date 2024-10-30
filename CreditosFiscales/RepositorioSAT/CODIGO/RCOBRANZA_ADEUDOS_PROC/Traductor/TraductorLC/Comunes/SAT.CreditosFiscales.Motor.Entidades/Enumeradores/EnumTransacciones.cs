
//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.Entidades:SAT.CreditosFiscales.Motor.Entidades.EnumTransacciones:1:12/07/2012[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAT.CreditosFiscales.Motor.Entidades.Enumeradores
{
    /// <summary>
    /// Enumerador de tipo de trasacciones
    /// </summary>
    public enum EnumTransacciones
    {
        ImporteHistorico =1,
        ImporteActualizado = 2,
        ImporteRecargos = 3,
        ImporteMulta = 4,
        Descuentos = 5,
        MontoPagar = 6
    }
}
