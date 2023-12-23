using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Sat.CreditosFiscales.Impresion.Entidades;
using Sat.CreditosFiscales.Impresion.LogicaNegocio;

namespace Sat.CreditosFiscales.Impresion.Portal.Servicios
{
    
    
    public class ServicioGeneraFormato : IServicioGeneraFormato
    {
        public string GenerarXML(EnumTemplate tipoFormato, List<string> listaLineasCaptura)
        {
            return new GeneraFormato().GenerarXML(tipoFormato, listaLineasCaptura);
        }
    }
}
