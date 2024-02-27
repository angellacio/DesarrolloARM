
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Procesamiento.Servicios.Interfaces:Sat.CreditosFiscales.Procesamiento.Servicios.Interfaces.IServicioConsultaEventos:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Sat.CreditosFiscales.Comunes.Entidades.AccesoLogEventos;
using Sat.CreditosFiscales.Comunes.Entidades.Servicios;
using Sat.CreditosFiscales.Comunes.Entidades.Servicios.Traductor;

namespace Sat.CreditosFiscales.Procesamiento.Servicios.Interfaces
{
    /// <summary>
    /// Interface que contiene los metodos publicos del servicio para la consulta de eventos de logs y bitacoras
    /// </summary>
    [ServiceContract]
    public interface IServicioConsultaEventos
    {
        /// <summary>
        /// Método para la búsqueda de eventos generados en el log de una aplicación por diversos filtros
        /// </summary>
        /// <param name="aplicacion">Nombre de la aplicación</param>
        /// <param name="porTicket">Número de ticket</param>
        /// <param name="porFechaInicio">Fecha inicio</param>
        /// <param name="porFechaFin">Fecha fin</param>
        /// <returns><see cref="LogEvento"/>></returns>
        [OperationContract]
        List<LogEvento> BuscarEventos(string aplicacion, string porTicket, string porFechaInicio, string porFechaFin);

        /// <summary>
        /// Método para la validación del acceso en el modulo de administración
        /// </summary>
        /// <param name="usuario">Clave del usuario</param>
        /// <param name="contraseña">Contraseña de acceso</param>
        /// <returns>Usuario válido</returns>
        [OperationContract]
        bool VerificaAcceso(string usuario, string contraseña);

        [OperationContract]
        bool CambiarPassword(string usuario, string contraseña);

        /// <summary>
        /// Método para la búsqueda de peticiones realizadas al traductor por diversos filtros
        /// </summary>
        /// <param name="porRfc">Rfc del contribuyente</param>
        /// <param name="porFechaInicio">Fecha incio</param>
        /// <param name="porFechaFin">Fecha final</param>
        /// <param name="conError">Peticiones realizadas con error, exito o todas</param>
        /// <returns><see cref="Peticion"/></returns>
        [OperationContract]
        List<Peticion> BuscarPeticiones(string porRfc, string porFechaInicio, string porFechaFin, int conError);

        /// <summary>
        /// Método para la búsqueda en bitácora del traductor por diversos filtros
        /// </summary>
        /// <param name="porIdAplicacion">Identificador de la aplicación</param>
        /// <param name="porIdTipoDocumento">Identificador del tipo de documento</param>
        /// <param name="porFechaInicio">Fecha inico</param>
        /// <param name="porFechaFin">Fecha fin</param>
        /// <param name="conError">Acciones realizadas con error, exito o todas</param>
        /// <returns><see cref="TraductorBitacora"/></returns>
        [OperationContract]
        List<TraductorBitacora> BuscarEnBitacora(int porIdAplicacion, int porIdTipoDocumento, DateTime porFechaInicio, DateTime porFechaFin, int conError, string porIdProcesamiento, string porRfc, int porIdPaso);
        /// <summary>
        /// Método para obtener los eventos registrados en el monitor de pago detalle.
        /// </summary>
        /// <param name="cadenaDeConexion">Cadena de conexión</param>
        /// <param name="porIdTipoPago">Identificador del tipo de pago</param>
        /// <param name="porIdEstatus">Identificador estatus</param>
        /// <param name="porIdBanco">Ientificar del tipo de banco</param>
        /// <param name="porFechaInicio">Fecha inicio pago</param>
        /// <param name="porFechaFin">Fecha fin pago</param>
        /// <param name="porLineaCaptura">Identifcar la linea de captura</param>
        /// <returns><see cref="TraductorMonitorPagoDetalleBusqueda"/></returns>
        [OperationContract]
        List<TraductorMonitorPagoDetalleBusqueda> BuscarEnMonitorPagoDetalle(int porIdTipoPago, int porIdEstatus, int porIdBanco, DateTime? porFechaInicio, DateTime? porFechaFin, string porLineaCaptura);
        /// <summary>
        /// Método para obtener los eventos registrados en el monitor de pago detalle.
        /// </summary>
        /// <param name="porIdTipoPago">Identificador del tipo de pago</param>
        /// <param name="porIdEstatus">Identificador estatus</param>
        /// <param name="porFechaInicio">Fecha inicio pago</param>
        /// <param name="porFechaFin">Fecha fin pago</param>
        /// <returns>Lista del tipo <see cref="TraductorMonitorArchivoZIPBusqueda"/></returns>
        [OperationContract]
        List<TraductorMonitorArchivoZIPBusqueda> BuscarEnMonitorArchivoZIP(int porIdTipoPago, string porArchivoZIP, DateTime? porFechaInicio, DateTime? porFechaFin);
        /// <summary>
        /// Método para obtener los eventos registrados en el monitor de tareas programadas.
        /// </summary>
        /// <param name="porIdTipoPago">Identificador del tipo de pago</param>
        /// <param name="porIdEstatus">Identificador estatus</param>
        /// <param name="porFechaInicio">Fecha inicio proceso</param>
        /// <param name="porFechaFin">Fecha fin proceso</param>
        /// <returns>Lista del tipo <see cref="TraductorMonitorTareaProgramadaBusqueda"/></returns>
        [OperationContract]
        List<TraductorMonitorTareaProgramadaBusqueda> BuscarEnMonitorTareaProgramada(int porIdTipoPago, int porIdEstatus, DateTime? porFechaInicio, DateTime? porFechaFin);

        /// <summary>
        /// Método para la obtención de catalogos
        /// </summary>
        /// <param name="idCatalogo">Identificador del catalogo</param>
        /// <returns>Diccionario con valor y texto del catalogo</returns>
        [OperationContract]
        Dictionary<int, string> ObtenerCatalogo(int idCatalogo);


    }
}
