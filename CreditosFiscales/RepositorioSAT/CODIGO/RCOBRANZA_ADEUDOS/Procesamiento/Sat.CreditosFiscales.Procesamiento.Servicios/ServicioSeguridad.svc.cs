
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Procesamiento.Servicios:Sat.CreditosFiscales.Procesamiento.Servicios.ServicioSeguridad:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Sat.CreditosFiscales.Comunes.Entidades.AccesoLogEventos;
using Sat.CreditosFiscales.Comunes.Entidades.Seguridad;
using Sat.CreditosFiscales.Procesamiento.LogicaNegocio.Seguridad;
using Sat.CreditosFiscales.Procesamiento.Servicios.Interfaces;

namespace Sat.CreditosFiscales.Procesamiento.Servicios
{    
    public class ServicioSeguridad : IServicioSeguridad
    {
        public InfoContribuyente ObtenerInfoContribuyente(string RfcContribuyente)
        {
            try
            {
                return new AccesoIdc().ObtenerInfoContribuyente(RfcContribuyente);
            }
            catch (ExcepcionTipificada err)
            {

                ReporteErrorWcf reporteError = new ReporteErrorWcf(err.Mensaje);
                if (err.InnerException != null)
                {
                    reporteError.InnerException = err.InnerException.Message;
                }

                throw new FaultException<ReporteErrorWcf>(reporteError, new FaultReason(reporteError.Mensaje));

            }          
        }


        public EmpleadoLdap ObtieneInfoEmpleado(string rfc)
        {
            return new AutenticacionEmpleados().ObtieneInfoEmpleado(rfc);
        }
    }
}
