using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Sat.CreditosFiscales.Impresion.Entidades.AccesoLogEventos;

namespace Sat.CreditosFiscales.Impresion.Portal.ViewModels
{
    public class GeneraLogEventosViewModel
    {
        public List<LogEvento> listaEventos { set; get; }

        public string Ticket { set; get; }
        public string FechaInicio { set; get; }


        public string FechaFin { set; get; }
    }

    public class GeneraAccesoLogViewModel
    {
        [Required]
        public string Usuario { set; get; }
        [Required]
        public string Contraseña { set; get; }
    }
}