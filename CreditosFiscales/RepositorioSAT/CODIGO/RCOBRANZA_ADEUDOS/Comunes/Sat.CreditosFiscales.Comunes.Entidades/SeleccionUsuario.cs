
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Comunes.Entidades:Sat.CreditosFiscales.Comunes.Entidades.SeleccionUsuario:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sat.CreditosFiscales.Comunes.Entidades.GeneraFormato;

namespace Sat.CreditosFiscales.Comunes.Entidades
{
    public class SeleccionUsuario
    {
        public List<DocumentoDeterminante> DocumentosSeleccionados { get; set; }

        public string ArchivoTemporal { get; set; }

        public Usuario DeudorPuro { get; set; }

    }
}
