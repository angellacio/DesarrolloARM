
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Presentacion.Portal.ViewModels:Sat.CreditosFiscales.Presentacion.Portal.ViewModels.GeneraPeticionesViewModel:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using Sat.CreditosFiscales.Comunes.Entidades.Servicios;

namespace Sat.CreditosFiscales.Presentacion.Portal.ViewModels
{
    public class GeneraPeticionesViewModel
    {

        public List<Peticion> listaPeticiones { set; get; }


        public string Rfc { set; get; }
        public string FechaInicio { set; get; }
        public string FechaFin { set; get; }
        public int conError { set; get; }

        public Dictionary<int, string> listaFiltrarPor { get { return new Dictionary<int, string>() { { -1, "Ver Todos" }, { 0, "Sin Error" }, { 1, "Con Error" }}; } }

    }
}