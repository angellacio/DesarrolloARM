
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Procesamiento.Servicios.Interfaces:Sat.CreditosFiscales.Procesamiento.Servicios.Interfaces.IServicioLog:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Web;
using Sat.CreditosFiscales.Comunes.Entidades.AccesoLogEventos;

namespace Sat.CreditosFiscales.Procesamiento.Servicios.Interfaces
{
    [ServiceContract]
    public interface IServicioLog
    {
        [OperationContract]
        string EscribirEntradaLog(int eventoID, string mensaje, System.Diagnostics.EventLogEntryType eventoLogTipo, string aplicacion);

        [OperationContract]
        void Test();
    }
}