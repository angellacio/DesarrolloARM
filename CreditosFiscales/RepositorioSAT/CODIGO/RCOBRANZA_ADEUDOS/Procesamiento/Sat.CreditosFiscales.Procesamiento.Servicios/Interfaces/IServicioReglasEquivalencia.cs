using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Sat.CreditosFiscales.Comunes.Entidades.Catalogos;

namespace Sat.CreditosFiscales.Procesamiento.Servicios.Interfaces
{
    [ServiceContract]
    public interface IServicioReglasEquivalencia
    {
        [OperationContract]
        List<ReglaEquivalencia> ObtieneReglas(int idAplicacion, int idTipoDocumento, int idTipoObjeto, string valorObjeto);

        [OperationContract]
        List<ConceptoEquivalencia> ObtieneConceptos(int idAplicacion, string conceptoOrigen, string conceptoDyP);

        [OperationContract]
        List<CatalogoGenerico> ObtieneCatAplicacion();

        [OperationContract]
        List<CatalogoGenerico> ObtieneCatTipoDocumento();

        [OperationContract]
        List<CatalogoGenerico> ObtieneCatConceptoDyP();

        [OperationContract]
        List<CatalogoGenerico> ObtieneCatTipoObjeto();

        [OperationContract]
        void ActualizaRegla(ReglaEquivalencia regla);

        [OperationContract]
        void AgregaRegla(ReglaEquivalencia regla);

        [OperationContract]
        void ActualizaConcepto(ConceptoEquivalencia concepto, ConceptoEquivalencia conceptoOriginal);

        [OperationContract]
        void AgregaConcepto(ConceptoEquivalencia concepto);
    }
}


