using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Sat.CreditosFiscales.Procesamiento.LogicaNegocio.ReglasEquivalencia;
using Sat.CreditosFiscales.Procesamiento.Servicios.Interfaces;

namespace Sat.CreditosFiscales.Procesamiento.Servicios
{    
    public class ServicioReglasEquivalencia : IServicioReglasEquivalencia
    {
        public List<Comunes.Entidades.Catalogos.ReglaEquivalencia> ObtieneReglas(int idAplicacion, int idTipoDocumento, int idTipoObjeto, string valorObjeto)
        {
            return LogicaReglas.ObtieneReglas(idAplicacion, idTipoDocumento, idTipoObjeto, valorObjeto);
        }

        public List<Comunes.Entidades.Catalogos.ConceptoEquivalencia> ObtieneConceptos(int idAplicacion, string conceptoOrigen, string conceptoDyP)
        {
            return LogicaReglas.ObtieneConceptos(idAplicacion, conceptoOrigen, conceptoDyP);
        }
        
        public List<Comunes.Entidades.Catalogos.CatalogoGenerico> ObtieneCatAplicacion()
        {
            return LogicaReglas.ObtieneCatAplicacion();
        }

        public List<Comunes.Entidades.Catalogos.CatalogoGenerico> ObtieneCatTipoDocumento()
        {
            return LogicaReglas.ObtieneCatTipoDocumento();
        }

        public List<Comunes.Entidades.Catalogos.CatalogoGenerico> ObtieneCatTipoObjeto()
        {
            return LogicaReglas.ObtieneCatTipoObjeto();
        }

        public List<Comunes.Entidades.Catalogos.CatalogoGenerico> ObtieneCatConceptoDyP()
        {
            return LogicaReglas.ObtieneCatConceptoDyP();
        }

        public void ActualizaRegla(Comunes.Entidades.Catalogos.ReglaEquivalencia regla)
        {
            LogicaReglas.ActualizaRegla(regla);
        }

        public void AgregaRegla(Comunes.Entidades.Catalogos.ReglaEquivalencia regla)
        {
            LogicaReglas.AgregaRegla(regla);
        }
        
        public void ActualizaConcepto(Comunes.Entidades.Catalogos.ConceptoEquivalencia concepto, Comunes.Entidades.Catalogos.ConceptoEquivalencia conceptoOriginal)
        {
            LogicaReglas.ActualizaConcepto(concepto, conceptoOriginal);
        }

        public void AgregaConcepto(Comunes.Entidades.Catalogos.ConceptoEquivalencia concepto)
        {
            LogicaReglas.AgregaConcepto(concepto);
        }
    }
}
