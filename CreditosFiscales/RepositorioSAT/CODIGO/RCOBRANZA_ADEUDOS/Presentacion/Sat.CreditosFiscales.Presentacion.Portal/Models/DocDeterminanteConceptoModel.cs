
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Presentacion.Portal.Models:Sat.CreditosFiscales.Presentacion.Portal.Models.DocDeterminanteConceptoHijoModel:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sat.CreditosFiscales.Comunes.Entidades.GeneraFormato;

namespace Sat.CreditosFiscales.Presentacion.Portal.Models
{
    public class DocDeterminanteConceptoModel : DocumentoDeterminanteConcepto
    {
        public string DescripcionPerido { get; set; }

        public string Descripcion { get; set; }

        public List<DocDeterminanteConceptoHijoModel> ConceptosHijo { get; set; }
    }
}