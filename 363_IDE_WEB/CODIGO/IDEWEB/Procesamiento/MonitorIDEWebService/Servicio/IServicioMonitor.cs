using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using IdeMonitorService.Negocio.Contratos;
using System.Web.UI.WebControls;

namespace IdeMonitorService
{
    [ServiceContract]
    public interface IServicioMonitor
    {

        [OperationContract]
        List<DeclaracionSalida> TraerDeclaraciones(DeclaracionEntrada declaracion);

        [OperationContract]
        List<DeclaracionSalida> TraerBitacora(DeclaracionEntrada declaracion);

        [OperationContract]
        List<CatalogoSalida> TraerCatalogos(int id);

        [OperationContract]
        List<EstadisticaSalida> TraerEstadistica(EstadisticaEntrada estadistica);

        [OperationContract]
        String TraerAcuse(int Folio);

    }
}
