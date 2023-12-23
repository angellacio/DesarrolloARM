
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Presentacion.Portal.Models:Sat.CreditosFiscales.Presentacion.Portal.Models.DocDetermintanteModel:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sat.CreditosFiscales.Comunes.Entidades.GeneraFormato;

namespace Sat.CreditosFiscales.Presentacion.Portal.Models
{
    public class DocDetermintanteModel : DocumentoDeterminante
    {
        public bool Seleccionado { get; set; }

        public bool MostrarImportes { get; set; }

        public string DescripcionAutoridad { get; set; }        

        public List<CatMarcaModel> MarcasModel { get; set; }

        public List<DocDeterminanteConceptoModel> ConceptosPadre { get; set; }
    }
}