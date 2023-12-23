
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Presentacion.Portal.ViewModels:Sat.CreditosFiscales.Presentacion.Portal.ViewModels.GeneraAccesoLogViewModel:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using Sat.CreditosFiscales.Comunes.Entidades.AccesoLogEventos;
using System.ComponentModel.DataAnnotations;

namespace Sat.CreditosFiscales.Presentacion.Portal.ViewModels
{

    public class GeneraAccesoLogViewModel
    {
        [Required]
        public string Usuario { set; get; }
        [Required]
        public string Contraseña { set; get; }
    }

    public class CambiaPasswordViewModel
    {
        [Required]
        public string Usuario { set; get; }
        [Required]
        public string Contraseña { set; get; }
        [Required]
        public string NuevaContraseña { set; get; }
        [Required]
        public string ConfirmarContraseña { set; get; }
    }

    public class GeneraLogEventosViewModel
    {
        public List<LogEvento> listaEventos { set; get; }

        public string Ticket { set; get; }
        public string FechaInicio { set; get; }

        
        public string FechaFin { set; get; }

    }
}