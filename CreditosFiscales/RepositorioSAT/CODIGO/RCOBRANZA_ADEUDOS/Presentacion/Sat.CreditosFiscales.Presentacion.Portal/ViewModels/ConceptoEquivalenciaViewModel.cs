using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sat.CreditosFiscales.Comunes.Entidades.Catalogos;

namespace Sat.CreditosFiscales.Presentacion.Portal.ViewModels
{
    public class ConceptoEquivalenciaViewModel
    {
        public List<ConceptoEquivalencia> Conceptos { get; set; }

        public ConceptoEquivalenciaParametroViewModel Parametros { get; set; }

        public List<CatalogoGenerico> CatConceptoDyP { get; set; }

        public List<CatalogoGenerico> CatAplicacion { get; set; }
     
        public int Accion { get; set; }

        public ConceptoEquivalencia Concepto { get; set; }

        public ConceptoEquivalencia ConceptoOriginal { get; set; }
    }
}