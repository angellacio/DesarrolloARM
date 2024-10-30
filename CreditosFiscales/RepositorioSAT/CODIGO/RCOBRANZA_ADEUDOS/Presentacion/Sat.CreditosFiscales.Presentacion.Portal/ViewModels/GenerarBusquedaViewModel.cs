
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Presentacion.Portal.ViewModels:Sat.CreditosFiscales.Presentacion.Portal.ViewModels.GenerarBusquedaViewModel:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Sat.CreditosFiscales.Comunes.Entidades.Catalogos;

namespace Sat.CreditosFiscales.Presentacion.Portal.ViewModels
{
    public class GenerarBusquedaViewModel
    {

        [Required]
        public string Rfc { set; get; }

        [Required]
        public string DocumentoDeterminante { set; get; }

        [Required]
        public int IdAutoridad { set; get; }

        [Required]
        public int IdALR { set; get; }

        [Required]
        public string FechaDocumento { set; get; }


        public List<ALR> listaALR { set; get; }

        public List<Autoridad> listaAutoridad { set; get; }

    }
}