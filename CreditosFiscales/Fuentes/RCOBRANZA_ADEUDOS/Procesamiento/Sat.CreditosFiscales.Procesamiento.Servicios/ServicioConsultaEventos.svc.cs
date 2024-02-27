
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Procesamiento.Servicios:Sat.CreditosFiscales.Procesamiento.Servicios.ServicioConsultaEventos:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Sat.CreditosFiscales.Procesamiento.LogicaNegocio.ConsultaEventos;
using Sat.CreditosFiscales.Comunes.Entidades.AccesoLogEventos;
using Sat.CreditosFiscales.Comunes.Entidades.Servicios;
using Sat.CreditosFiscales.Comunes.Entidades.Servicios.Traductor;
using Sat.CreditosFiscales.Procesamiento.Servicios.Interfaces;

namespace Sat.CreditosFiscales.Procesamiento.Servicios
{
    /// <summary>
    /// Clase que contiene los metodos publicos del servicio para la consulta de eventos de logs y bitacoras
    /// </summary>
    public class ServicioConsultaEventos : IServicioConsultaEventos
    {
        /// <summary>
        /// Método para la búsqueda de eventos generados en el log de una aplicación por diversos filtros
        /// </summary>
        /// <param name="aplicacion">Nombre de la aplicación</param>
        /// <param name="porTicket">Número de ticket</param>
        /// <param name="porFechaInicio">Fecha inicio</param>
        /// <param name="porFechaFin">Fecha fin</param>
        /// <returns><see cref="LogEvento"/>></returns>
        public List<LogEvento> BuscarEventos(string aplicacion, string porTicket, string porFechaInicio, string porFechaFin)
        {
            return LogEventos.BuscarEventos(aplicacion, porTicket, porFechaInicio, porFechaFin);
        }
        /// <summary>
        /// Método para la validación del acceso en el modulo de administración
        /// </summary>
        /// <param name="usuario">Clave del usuario</param>
        /// <param name="contraseña">Contraseña de acceso</param>
        /// <returns>Usuario válido</returns>
        public bool VerificaAcceso(string usuario, string contraseña)
        {
            return LogEventos.VerificaAcceso(usuario, contraseña);
        }
        public bool CambiarPassword(string usuario, string contraseña)
        {
            return LogEventos.CambiaPassword(usuario, contraseña);
        }
        /// <summary>
        /// Método para la búsqueda de peticiones realizadas al traductor por diversos filtros
        /// </summary>
        /// <param name="porRfc">Rfc del contribuyente</param>
        /// <param name="porFechaInicio">Fecha incio</param>
        /// <param name="porFechaFin">Fecha final</param>
        /// <param name="conError">Peticiones realizadas con error, exito o todas</param>
        /// <returns><see cref="Peticion"/></returns>
        public List<Peticion> BuscarPeticiones(string porRfc, string porFechaInicio, string porFechaFin, int conError)
        {
            return LogEventos.BuscarPeticiones(porRfc, porFechaInicio, porFechaFin, conError);
        }
        /// <summary>
        /// Método para la búsqueda en bitácora del traductor por diversos filtros
        /// </summary>
        /// <param name="porIdAplicacion">Identificador de la aplicación</param>
        /// <param name="porIdTipoDocumento">Identificador del tipo de documento</param>
        /// <param name="porFechaInicio">Fecha inico</param>
        /// <param name="porFechaFin">Fecha fin</param>
        /// <param name="conError">Acciones realizadas con error, exito o todas</param>
        /// <returns><see cref="TraductorBitacora"/></returns>
        public List<TraductorBitacora> BuscarEnBitacora(int porIdAplicacion, int porIdTipoDocumento, DateTime porFechaInicio, DateTime porFechaFin, int conError, string porIdProcesamiento, string porRfc, int porIdPaso)
        {
            return LogEventos.BuscarEnBitacora(porIdAplicacion, porIdTipoDocumento, porFechaInicio, porFechaFin, conError, porIdProcesamiento, porRfc, porIdPaso);
        }
        /// <summary>
        /// Método para la búsqueda en monitor pago detalle.
        /// </summary>
        /// <param name="porIdTipoPago">Identificador de la aplicación</param>
        /// <param name="porIdEstatus">Identificador del tipo de documento</param>
        /// <param name="porIdBanco">Fecha inico</param>
        /// <param name="porFechaInicio">Fecha fin</param>
        /// <param name="porFechaFin">Acciones realizadas con error, exito o todas</param>
        /// /// <param name="porLineaCaptura">Acciones realizadas con error, exito o todas</param>
        /// <returns><see cref="TraductorMonitorPagoDetalleBusqueda"/></returns>
        public List<TraductorMonitorPagoDetalleBusqueda> BuscarEnMonitorPagoDetalle(int porIdTipoPago, int porIdEstatus, int porIdBanco, DateTime? porFechaInicio, DateTime? porFechaFin, string porLineaCaptura)
        {
            return LogEventos.BuscarMonitorDetallePago(porIdTipoPago, porIdEstatus, porIdBanco, porFechaInicio, porFechaFin, porLineaCaptura);
        }
        /// <summary>
        /// Método para obtener los eventos registrados en el monitor de los archivos zip.
        /// </summary>
        /// <param name="porIdTipoPago">Identificador del tipo de pago</param>
        /// <param name="porIdEstatus">Identificador estatus</param>
        /// <param name="porFechaInicio">Fecha inicio pago</param>
        /// <param name="porFechaFin">Fecha fin pago</param>
        /// <returns>Lista del tipo <see cref="TraductorMonitorArchivoZIPBusqueda"/></returns>
        public List<TraductorMonitorArchivoZIPBusqueda> BuscarEnMonitorArchivoZIP(int porIdTipoPago, string porArchivoZIP, DateTime? porFechaInicio, DateTime? porFechaFin)
        {
            return LogEventos.BuscarMonitorArchivoZIP(porIdTipoPago, porArchivoZIP, porFechaInicio, porFechaFin);
        }
        /// <summary>
        /// Método para obtener los eventos registrados en el monitor de tareas programadas.
        /// </summary>
        /// <param name="porIdTipoPago">Identificador del tipo de pago</param>
        /// <param name="porIdEstatus">Identificador estatus</param>
        /// <param name="porFechaInicio">Fecha inicio proceso</param>
        /// <param name="porFechaFin">Fecha fin proceso</param>
        /// <returns>Lista del tipo <see cref="TraductorMonitorTareaProgramadaBusqueda"/></returns>
        public List<TraductorMonitorTareaProgramadaBusqueda> BuscarEnMonitorTareaProgramada(int porIdTipoPago, int porIdEstatus, DateTime? porFechaInicio, DateTime? porFechaFin)
        {
            return LogEventos.BuscarMonitorTareaProgramada(porIdTipoPago, porIdEstatus, porFechaInicio, porFechaFin);
        }

        /// <summary>
        /// Método para la obtención de catalogos
        /// </summary>
        /// <param name="idCatalogo">Identificador del catalogo</param>
        /// <returns>Diccionario con valor y texto del catalogo</returns>
        public Dictionary<int, string> ObtenerCatalogo(int idCatalogo)
        {
            return LogEventos.ObtenerCatalogo(idCatalogo);
        }
    }
}
