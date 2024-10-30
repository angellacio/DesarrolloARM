
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Procesamiento.Servicios.Interfaces:Sat.CreditosFiscales.Procesamiento.Servicios.Interfaces.IServicioSeguridad:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Sat.CreditosFiscales.Comunes.Entidades.AccesoLogEventos;
using Sat.CreditosFiscales.Comunes.Entidades.Seguridad;

namespace Sat.CreditosFiscales.Procesamiento.Servicios.Interfaces
{
    [ServiceContract]     
    public interface IServicioSeguridad
    {
        [OperationContract]
        [FaultContract(typeof(ReporteErrorWcf))]
        InfoContribuyente ObtenerInfoContribuyente(string RfcContribuyente);

        [OperationContract]
        [FaultContract(typeof(ReporteErrorWcf))]
         EmpleadoLdap ObtieneInfoEmpleado(string rfc);
    }
}
