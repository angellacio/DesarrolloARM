using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml;
using Sat.CreditosFiscales.Impresion.Entidades;

namespace Sat.CreditosFiscales.Impresion.Portal.Servicios
{
    [ServiceContract(Name = "GeneraFormato")]
    public interface IServicioGeneraFormato
    {
        [OperationContract]
        string GenerarXML(EnumTemplate tipoFormato, List<string> listaLineasCaptura);
    }
}
