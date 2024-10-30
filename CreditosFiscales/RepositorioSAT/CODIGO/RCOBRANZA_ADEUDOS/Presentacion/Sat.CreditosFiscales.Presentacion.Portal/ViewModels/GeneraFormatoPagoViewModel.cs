
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Presentacion.Portal.ViewModels:Sat.CreditosFiscales.Presentacion.Portal.ViewModels.GeneraFormatoPagoViewModel:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sat.CreditosFiscales.Presentacion.Portal.Models;

namespace Sat.CreditosFiscales.Presentacion.Portal.ViewModels
{
    public class GeneraFormatoPagoViewModel
    {
        public List<DocDetermintanteModel> Determinantes { get; set; }

        public int AccionPantalla { get; set; }

        public List<string> IdsRecargos { get; set; }
    }
}