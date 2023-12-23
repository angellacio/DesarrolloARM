
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Procesamiento.Servicios.Interfaces:Sat.CreditosFiscales.Procesamiento.Servicios.Interfaces.IServicioCatalogos:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Sat.CreditosFiscales.Comunes.Entidades.Catalogos;

namespace Sat.CreditosFiscales.Procesamiento.Servicios.Interfaces
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IServicioCatalogos" in both code and config file together.
    [ServiceContract]
    public interface IServicioCatalogos
    {
        //[OperationContract]
        //List<CatMarca> ObtenerCatalogoMarca();

        [OperationContract]
        List<Autoridad> ObtenerCatalogoAutoridad();

        [OperationContract]
        ALR ObtenerAlrXCveCorta(string CveCorta);

        [OperationContract]
        List<ALR> ObtenerCatalogoAlr();

        [OperationContract]
        DescripcionConceptoPeriodicidad ObtenerDescripcionConceptoPeriodicidad(int idConceptoLey, int idPeriodo, int idPeriodicidad);

        [OperationContract]
        object ConsultaConfiguracion(string settingName);


        #region Clave Computo

        [OperationContract]
        List<CatClaveComputo> ListaClaveComputo(string claveComputo = "");
        [OperationContract]
        void GuardarClaveComputo(CatClaveComputo.Accion accion, CatClaveComputo ClaveComputo);
        [OperationContract]
        void EliminarClaveComputo(string claveComputo);

        #endregion

        #region Catalogo de Reglas
        [OperationContract]
        List<CatReglas> ListaCatReglas(string idRegla, string descripcion = "");
        [OperationContract]
        void GuardarRegla(CatReglas.Accion accion, CatReglas Regla);
        [OperationContract]
        void EliminarRegla(Guid idRegla);
        #endregion


        #region Catalogo de Esquemas
        [OperationContract]
        List<CatEsquemas> ListaCatEsquemas(string idEsquema, string descripcion = "");
        [OperationContract]
        void GuardarEsquema(CatEsquemas.Accion accion, CatEsquemas Esquema);
        [OperationContract]
        void EliminarEsquema(Int16 idEsquema);
        #endregion


    }
}
