
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Procesamiento.Servicios:Sat.CreditosFiscales.Procesamiento.Servicios.ServicioLineasCaptura:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using Sat.CreditosFiscales.Comunes.Entidades.AccesoLogEventos;
using Sat.CreditosFiscales.Procesamiento.Servicios.Interfaces;
using Sat.CreditosFiscales.Comunes.Entidades.GeneraFormato;
using Sat.CreditosFiscales.Comunes.Entidades;
using Sat.CreditosFiscales.Procesamiento.LogicaNegocio.GeneraLineasCaptura;


namespace Sat.CreditosFiscales.Procesamiento.Servicios
{
    /// <summary>
    /// Clase para el manejo de los metodos publicados por el servicio de líneas de captura
    /// </summary>
    public class ServicioLineasCaptura : IServicioLineasCaptura
    {

        /// <summary>
        /// Genera las líneas de captura de los documentos seleccionados por el usuario
        /// </summary>
        /// <param name="idSolicitud">Identificador de la solicitud</param>
        /// <param name="usuario"><see cref="Usuario"/></param>
        /// <returns><see cref="LineasCapturaRespuesta"/></returns>
        public LineasCapturaRespuesta GenerarLineasCaptura(Int64 idSolicitud, Usuario usuario)
        {
            var lineasCaptura = new LineasCapturaRespuesta();
            ExcepcionTipificada excepcion = null;
            try
            {
                lineasCaptura.LineasCaptura = LineasCaptura.GenerarLineasDeCaptura(idSolicitud, usuario, ref excepcion);
                if (excepcion != null)
                    lineasCaptura.Excepcion = new ReporteErrorWcf(excepcion.Mensaje);
            }
            catch (ExcepcionTipificada ex)
            {
                ReporteErrorWcf reporteError = new ReporteErrorWcf(ex.Mensaje);
                if (ex.InnerException != null)
                {
                    reporteError.InnerException = ex.InnerException.Message;
                }

                throw new FaultException<ReporteErrorWcf>(reporteError, new FaultReason(reporteError.Mensaje));
            }
            return lineasCaptura;
        }

        /// <summary>
        /// Genera un archivo en mapa de bytes apartir de una lista de folios
        /// </summary>
        /// <param name="listaDeFolios">Lista de folios</param>
        /// <returns>Mapa de bytes</returns>
        public byte[] GeneraArchivos(List<string> listaDeFolios)
        {
            try
            {
                return LineasCaptura.GeneraArchivos(listaDeFolios);
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

        /// <summary>
        /// Obtenemos los documentos determinantes asociados a un folio de línea de captura
        /// </summary>
        /// <param name="lineaCaptura">Folio de línea de captura</param>
        /// <param name="rfcContribuyente">Rfc del contribuyente</param>
        /// <returns>Lista de documentos</returns>
        public List<string> ObtenerDocumentosAsociados(string lineaCaptura, string rfcContribuyente)
        {
            List<string> listaDocumentosDeterminantes = null;
            try
            {
                listaDocumentosDeterminantes = LineasCaptura.ObtenerDocumentosAsociados(lineaCaptura, rfcContribuyente);
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

            return listaDocumentosDeterminantes;
        }

        /// <summary>
        /// Obtenemos la información personal del deudor puro
        /// </summary>
        /// <param name="idSolicitud">Identificador de la solicitud</param>
        /// <returns><see cref="Usuario"/></returns>
        public Usuario ObtenerInfoUsuarioDeudorPuro(Int64 idSolicitud)
        {
            var usuario = new Usuario();
            try
            {
                usuario = LineasCaptura.ObtenerInfoUsuarioDeudorPuro(idSolicitud);
            }
            catch (ExcepcionTipificada ex)
            {
                ReporteErrorWcf reporteError = new ReporteErrorWcf(ex.Mensaje);
                if (ex.InnerException != null)
                {
                    reporteError.InnerException = ex.InnerException.Message;
                }

                throw new FaultException<ReporteErrorWcf>(reporteError, new FaultReason(reporteError.Mensaje));
            }
            return usuario;
        }

    }
}
