
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Presentacion.Portal.ViewModels:Sat.CreditosFiscales.Presentacion.Portal.ViewModels.DeudoresPurosViewModel:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sat.CreditosFiscales.Comunes.Entidades.Catalogos;
using Sat.CreditosFiscales.Presentacion.Portal.Models;

namespace Sat.CreditosFiscales.Presentacion.Portal.ViewModels
{
    public class DeudoresPurosViewModel
    {
        public ParametroDeudorPuroModel Parametros { get; set; }

        public List<DocDetermintanteModel> Determinantes { get; set; }

        public List<string> IdsRecargos { get; set; }

        public List<ALR> ListaAlr { get; set; }

        public List<Autoridad> ListaAutoridad { get; set; }

        public string MensajeError { get; set; }

        public int Accion { get; set; }
    }
}