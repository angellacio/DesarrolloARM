
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Presentacion.Portal.ViewModels:Sat.CreditosFiscales.Presentacion.Portal.ViewModels.GeneraLineasCapturaViewModel:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using Sat.CreditosFiscales.Comunes.Herramientas;
using Sat.CreditosFiscales.Comunes.Entidades.GeneraFormato;

namespace Sat.CreditosFiscales.Presentacion.Portal.ViewModels
{
    public class GeneraLineasCapturaViewModel
    {
        public List<LineaCaptura> listaLineaCaptura { set; get; }
        public bool deudorPuro { set; get; }
    }
}