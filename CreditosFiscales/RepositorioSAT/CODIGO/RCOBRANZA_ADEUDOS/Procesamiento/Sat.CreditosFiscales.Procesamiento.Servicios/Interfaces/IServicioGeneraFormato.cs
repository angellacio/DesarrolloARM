
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Procesamiento.Servicios.Interfaces:Sat.CreditosFiscales.Procesamiento.Servicios.Interfaces.IServicioGeneraFormato:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Sat.CreditosFiscales.Comunes.Entidades.AccesoLogEventos;
using Sat.CreditosFiscales.Comunes.Entidades.GeneraFormato;
using Sat.CreditosFiscales.Comunes.Entidades.Servicios;


namespace Sat.CreditosFiscales.Procesamiento.Servicios.Interfaces
{
 
    [ServiceContract]      
    public interface IServicioGeneraFormato
    {
        [OperationContract]
        [FaultContract(typeof(ReporteErrorWcf))]
        Int64  GuardaSolicitud(Solicitud solicitud);

        [OperationContract]
        [FaultContract(typeof(ReporteErrorWcf))]
        List<DocumentoDeterminante> ObtenerInformacionContribuyente(string rfc, List<string> rfcs);

        [OperationContract]
        [FaultContract(typeof(ReporteErrorWcf))]
        DocumentosContribuyentePuro ObtenerInformacionContribuyentePuro(string rfc, string documentoDeterminante, int idAutoridad, int claveAlr, DateTime fechaDocumento);
       
    }
}
