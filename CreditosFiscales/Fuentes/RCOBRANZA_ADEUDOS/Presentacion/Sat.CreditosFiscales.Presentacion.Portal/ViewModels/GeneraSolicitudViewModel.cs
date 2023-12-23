
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Presentacion.Portal.ViewModels:Sat.CreditosFiscales.Presentacion.Portal.ViewModels.GeneraSolicitudViewModel:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using Sat.CreditosFiscales.Comunes.Entidades.GeneraFormato;
using Sat.CreditosFiscales.Comunes.Entidades.Servicios;

namespace Sat.CreditosFiscales.Presentacion.Portal.ViewModels
{
    public class GeneraSolicitudViewModel
    {
        public Solicitud Solicitud { set; get; }

        public List<DocumentoDeterminante> listaDocumentos { get { return Solicitud.Documentos; } }


    }
}