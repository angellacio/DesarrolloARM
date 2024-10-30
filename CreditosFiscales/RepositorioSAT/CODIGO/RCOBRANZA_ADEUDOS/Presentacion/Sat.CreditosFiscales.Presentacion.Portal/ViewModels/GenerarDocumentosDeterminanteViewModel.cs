
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Presentacion.Portal.ViewModels:Sat.CreditosFiscales.Presentacion.Portal.ViewModels.GenerarDocumentosDeterminanteViewModel:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sat.CreditosFiscales.Comunes.Entidades.GeneraFormato;
using Sat.CreditosFiscales.Comunes.Entidades.Servicios;


namespace Sat.CreditosFiscales.Presentacion.Portal.ViewModels
{
    public class GenerarDocumentosDeterminanteViewModel
    {
        public List<DocumentoDeterminante> listaDocumentos { get; set; }
        public List<string> listaDocumentosAsociados { get; set; }
    }
}