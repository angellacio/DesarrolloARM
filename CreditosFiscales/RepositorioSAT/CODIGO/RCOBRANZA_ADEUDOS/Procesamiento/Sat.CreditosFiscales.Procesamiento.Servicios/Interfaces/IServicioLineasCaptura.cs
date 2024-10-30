
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Procesamiento.Servicios.Interfaces:Sat.CreditosFiscales.Procesamiento.Servicios.Interfaces.IServicioLineasCaptura:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using Sat.CreditosFiscales.Comunes.Entidades.AccesoLogEventos;
using Sat.CreditosFiscales.Comunes.Entidades.GeneraFormato;
using Sat.CreditosFiscales.Comunes.Entidades;

namespace Sat.CreditosFiscales.Procesamiento.Servicios.Interfaces
{
    /// <summary>
    /// Interface para el manejo de los metodos publicados por el servicio de líneas de captura
    /// </summary>
    [ServiceContract]
    public interface IServicioLineasCaptura
    {
        /// <summary>
        /// Genera las líneas de captura de los documentos seleccionados por el usuario
        /// </summary>
        /// <param name="idSolicitud">Identificador de la solicitud</param>
        /// <param name="usuario"><see cref="Usuario"/></param>
        /// <returns><see cref="LineasCapturaRespuesta"/></returns>
        [OperationContract]
        LineasCapturaRespuesta GenerarLineasCaptura(Int64 idSolicitud, Usuario usuario);

        /// <summary>
        /// Genera un archivo en mapa de bytes apartir de una lista de folios
        /// </summary>
        /// <param name="listaDeFolios">Lista de folios</param>
        /// <returns>Mapa de bytes</returns>
        [OperationContract]
        byte[] GeneraArchivos(List<string> listaDeFolios);

        /// <summary>
        /// Obtenemos los documentos determinantes asociados a un folio de línea de captura
        /// </summary>
        /// <param name="lineaCaptura">Folio de línea de captura</param>
        /// <param name="rfcContribuyente">Rfc del contribuyente</param>
        /// <returns>Lista de documentos</returns>
        [OperationContract]
        List<string> ObtenerDocumentosAsociados(string lineaCaptura, string rfcContribuyente);

        /// <summary>
        /// Obtenemos la información personal del deudor puro
        /// </summary>
        /// <param name="idSolicitud">Identificador de la solicitud</param>
        /// <returns><see cref="Usuario"/></returns>
        [OperationContract]
        Usuario ObtenerInfoUsuarioDeudorPuro(Int64 idSolicitud);
    }

    /// <summary>
    /// Clase para regresar la respuesta del servicio en la generación de las líneas de captura
    /// </summary>
    [DataContract]
    public class LineasCapturaRespuesta
    {
        [DataMember]
        public List<LineaCaptura> LineasCaptura { set; get; }

        [DataMember]
        public ReporteErrorWcf Excepcion { set; get; }


    }
}
