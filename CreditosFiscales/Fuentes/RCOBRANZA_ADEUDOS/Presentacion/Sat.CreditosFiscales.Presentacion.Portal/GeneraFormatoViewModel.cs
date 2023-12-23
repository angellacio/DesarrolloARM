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
    }
}