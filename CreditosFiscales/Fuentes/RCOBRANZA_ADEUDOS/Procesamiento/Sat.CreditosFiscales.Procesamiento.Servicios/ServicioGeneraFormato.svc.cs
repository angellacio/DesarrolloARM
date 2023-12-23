
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Procesamiento.Servicios:Sat.CreditosFiscales.Procesamiento.Servicios.ServicioGeneraFormato:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Sat.CreditosFiscales.Comunes.Entidades.AccesoLogEventos;
using Sat.CreditosFiscales.Comunes.Entidades.GeneraFormato;
using Sat.CreditosFiscales.Comunes.Entidades.Servicios;
using Sat.CreditosFiscales.Procesamiento.LogicaNegocio.GeneraFormato;
using Sat.CreditosFiscales.Procesamiento.LogicaNegocio.Servicios;
using Sat.CreditosFiscales.Procesamiento.Servicios.Interfaces;

namespace Sat.CreditosFiscales.Procesamiento.Servicios
{

    public class ServicioGeneraFormato : IServicioGeneraFormato
    {
        public Int64 GuardaSolicitud(Solicitud solicitud)
        {
            try
            {
                return new GeneraFormato().GuardaSolicitud(solicitud);
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

        public List<DocumentoDeterminante> ObtenerInformacionContribuyente(string rfc, List<string> rfcs)
        {
            try
            {
                return ProxyManagerArca.ObtenerInformacionContribuyente(rfc, rfcs);
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

        public DocumentosContribuyentePuro ObtenerInformacionContribuyentePuro(string rfc, string documentoDeterminante, int idAutoridad, int claveAlr, DateTime fechaDocumento)
        {
            try
            {
                return ProxyManagerArca.ObtenerInformacionContribuyentePuro(rfc,documentoDeterminante,idAutoridad,claveAlr,fechaDocumento);
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
    }
}
