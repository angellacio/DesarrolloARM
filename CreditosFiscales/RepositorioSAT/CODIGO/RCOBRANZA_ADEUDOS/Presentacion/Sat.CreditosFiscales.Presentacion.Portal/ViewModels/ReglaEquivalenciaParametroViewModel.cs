using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sat.CreditosFiscales.Presentacion.Portal.ViewModels
{
    public class ReglaEquivalenciaParametroViewModel
    {
        public int IdAplicacion { get; set; }
        public int IdTipoDocumento { get; set; }
        public int IdTipoObjeto { get; set; }
        public string ValorObjeto { get; set; }
    }
}
