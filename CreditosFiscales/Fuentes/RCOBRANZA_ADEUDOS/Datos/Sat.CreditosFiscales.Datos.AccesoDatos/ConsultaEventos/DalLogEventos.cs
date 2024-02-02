
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Datos.AccesoDatos.ConsultaEventos:Sat.CreditosFiscales.Datos.AccesoDatos.ConsultaEventos.DalLogEventos:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Sat.CreditosFiscales.Comunes.Herramientas;
using Sat.CreditosFiscales.Comunes.Entidades.AccesoLogEventos;
using Sat.CreditosFiscales.Comunes.Entidades.Servicios;
using Sat.CreditosFiscales.Comunes.Entidades.Servicios.Traductor;

namespace Sat.CreditosFiscales.Datos.AccesoDatos.ConsultaEventos
{
    /// <summary>
    /// Clase que contiene los metodos requeridos para la manipulación de la información en base de datos de los eventos generados
    /// por la aplicación y el traductos
    /// </summary>
    public class DalLogEventos
    {

        /// <summary>
        /// Método para obtener los eventos generados en el log por diversos filtros
        /// </summary>
        /// <param name="cadenaDeConexion">Cadena de conexión de la aplicación (Presentación, Traductor)</param>
        /// <param name="porTicket">Número de ticket</param>
        /// <param name="porFechaInicio">Fecha inicio</param>
        /// <param name="porFechaFin">Fecha fin</param>
        /// <returns>Lista del tipo <see cref="LogEvento"/></returns>
        public List<LogEvento> BuscarEventos(string cadenaDeConexion, string porTicket, string porFechaInicio, string porFechaFin)
        {
            var listaEventos = new List<LogEvento>();
            Database db = new SqlDatabase(cadenaDeConexion);
            DbCommand cmd = db.GetStoredProcCommand("pLogEventosBuscar");
            db.AddInParameter(cmd, "@pTicket", System.Data.DbType.String, porTicket);
            db.AddInParameter(cmd, "@pFechaInicio", System.Data.DbType.String, porFechaInicio);
            db.AddInParameter(cmd, "@pFechaFin", System.Data.DbType.String, porFechaFin);

            using (var dr = db.ExecuteReader(cmd))
            {
                DataTable dt = dr.GetSchemaTable();
                bool existe = false;
                foreach (DataRow col in dt.Rows)
                {
                    if (col.ItemArray[0].ToString() == "XmlSolicitud")
                    {
                        existe = true;
                    }
                }

                while (dr.Read())
                {
                    LogEvento evento = new LogEvento();
                    evento.Id = new Guid(dr["Id"].ToString());
                    evento.IdTipoEvento = Convert.ToInt16(dr["IdTipoEvento"]);
                    evento.Mensaje = dr["Mensaje"].ToString();
                    evento.Aplicacion = dr["Aplicacion"].ToString();
                    evento.Evento = dr["Evento"].ToString();
                    evento.FechaOrigen = Convert.ToDateTime(dr["FechaOrigen"]);
                    evento.Xml = existe ? dr["XmlSolicitud"].ToString() : string.Empty;

                    listaEventos.Add(evento);
                }
            }


            return listaEventos;
        }

        /// <summary>
        /// Método que verifica si el usuario que intenta registrarse es válido.
        /// </summary>
        /// <param name="usuario">Clave o nombre de usuario</param>
        /// <param name="contraseña">Contraseña de acceso</param>
        /// <returns>Verdadero o falso</returns>
        public bool VerificaAcceso(string usuario, string contraseña)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("pVerificaAcceso");
            string contMD5 = new Encripcion().ObtieneMD5(contraseña);
            db.AddInParameter(cmd, "@pUsuario", DbType.String, usuario);
            db.AddInParameter(cmd, "@pContrasena", DbType.String, contMD5);
            return (int)db.ExecuteScalar(cmd) == 1;
        }

        public bool CambiaPassword(string usuario, string contraseña)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("pCambiaPassword");
            string contMD5 = new Encripcion().ObtieneMD5(contraseña);
            db.AddInParameter(cmd, "@pUsuario", DbType.String, usuario);
            db.AddInParameter(cmd, "@pContrasena", DbType.String, contMD5);
            return (int)db.ExecuteScalar(cmd) == 1;
        }

        /// <summary>
        /// Método para obtener las peticiones realizadas al traductor por diversos filtros
        /// </summary>
        /// <param name="porRfc">Rfc del contribuyente</param>
        /// <param name="porFechaInicio">Fecha inicio</param>
        /// <param name="porFechaFin">Fecha fin</param>
        /// <param name="conError">Filtrar con error:1, sin error:0 o ninguna restricción:-1</param>
        /// <returns>Lista del tipo <see cref="Peticion"/></returns>
        public List<Peticion> BuscarPeticiones(string porRfc, string porFechaInicio, string porFechaFin, int conError)
        {
            var listaPeticiones = new List<Peticion>();
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("pPeticionesBuscar");
            db.AddInParameter(cmd, "@pRfc", DbType.String, porRfc);
            db.AddInParameter(cmd, "@pFechaInicio", DbType.String, porFechaInicio);
            db.AddInParameter(cmd, "@pFechaFin", DbType.String, porFechaFin);
            db.AddInParameter(cmd, "@pConError", DbType.Int16, conError);
            using (var dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    Peticion peticion = new Peticion();
                    peticion.Accion = Convert.ToInt16(dr["IdAccionOrigen"]);
                    peticion.Fecha = Convert.ToDateTime(dr["FechaPeticion"]);
                    peticion.HuboError = Convert.ToBoolean(dr["HuboError"]);
                    peticion.Observaciones = dr["Observaciones"].ToString();
                    peticion.TipoOrigen = Convert.ToInt16(dr["IdTipoOrigen"]);
                    peticion.XmlPeticion = dr["XmlPeticion"].ToString();
                    peticion.XmlRespuesta = dr["XmlRespuesta"].ToString();
                    peticion.RFC = dr["RFC"].ToString();
                    peticion.TipoOrigenDescripcion = dr["TipoOrigen"].ToString();
                    peticion.AccionDescripcion = dr["AccionOrigen"].ToString();
                    peticion.Duracion = dr["Duracion"] != System.DBNull.Value ? Convert.ToDecimal(dr["Duracion"]) : 0;

                    listaPeticiones.Add(peticion);
                }
            }


            return listaPeticiones;
        }

        /// <summary>
        /// Método para obtener los eventos registrados en la bitácora del traductor por diversos filtros
        /// </summary>
        /// <param name="cadenaDeConexion">Cadena de conexión</param>
        /// <param name="porIdAplicacion">Identificador de la aplicación</param>
        /// <param name="porIdTipoDocumento">Identificador del tipo de documento</param>
        /// <param name="porFechaInicio">Fecha inicio</param>
        /// <param name="porFechaFin">Fecha fin</param>
        /// <param name="conError">Filtrar con error:1, sin error:0 o ninguna restricción:-1</param>
        /// <returns>Lista del tipo <see cref="TraductorBitacora"/></returns>
        public List<TraductorBitacora> BuscarBitacora(string cadenaDeConexion, int porIdAplicacion, int porIdTipoDocumento, DateTime porFechaInicio, DateTime porFechaFin, int conError, string porIdProcesamiento, string porRfc, int porIdPaso)
        {
            var listaBitacora = new List<TraductorBitacora>();
            Database db = new SqlDatabase(cadenaDeConexion);
            DbCommand cmd = db.GetStoredProcCommand("pBitacoraEventosBuscar");
            db.AddInParameter(cmd, "@pIdAplicacion", DbType.Int16, porIdAplicacion);
            db.AddInParameter(cmd, "@pIdTipoDocPago", DbType.Int16, porIdTipoDocumento);
            if (porFechaInicio != DateTime.MinValue)
                db.AddInParameter(cmd, "@pFechaInicio", DbType.DateTime, porFechaInicio);
            if (porFechaFin != DateTime.MinValue)
                db.AddInParameter(cmd, "@pFechaFin", DbType.DateTime, porFechaFin);
            db.AddInParameter(cmd, "@pConError", DbType.Int16, conError);
            db.AddInParameter(cmd, "@pRFC", DbType.String, porRfc);
            db.AddInParameter(cmd, "@pIdPaso", DbType.Int32, porIdPaso);
            if (!string.IsNullOrWhiteSpace(porIdProcesamiento))
                db.AddInParameter(cmd, "@pIdProcesamiento", DbType.Guid, new Guid(porIdProcesamiento));

            using (var dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    TraductorBitacora bitacora = new TraductorBitacora();
                    bitacora.Aplicacion = dr["Aplicacion"].ToString();
                    bitacora.Errores = dr["Errores"].ToString();
                    bitacora.FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"]);
                    bitacora.IdPasoProceso = Convert.ToInt16(dr["idPasoProceso"]);
                    bitacora.IdProcesamiento = dr["IdProcesamiento"].ToString();
                    bitacora.Mensaje = dr["Mensaje"].ToString();
                    bitacora.Observaciones = dr["Observaciones"].ToString();
                    bitacora.TipoDocumento = dr["TipoDocumento"].ToString();
                    bitacora.Duracion = dr["Duracion"].ToString();

                    listaBitacora.Add(bitacora);

                }
            }

            return listaBitacora;
        }

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
        /// <returns>Lista del tipo <see cref="TraductorMonitorPagoDetalleBusqueda"/></returns>
        public List<TraductorMonitorPagoDetalleBusqueda> BuscarMonitorPagoDetalle(string cadenaDeConexion, int porIdTipoPago, int porIdEstatus, int porIdBanco, DateTime? porFechaInicio, DateTime? porFechaFin, string porLineaCaptura)
        {
            var listaMonitorPagoDet = new List<TraductorMonitorPagoDetalleBusqueda>();
            Database db = new SqlDatabase(cadenaDeConexion);
            DbCommand cmd = db.GetStoredProcCommand("pObtenerMonitorPagoDetalle");
            db.AddInParameter(cmd, "@pTipoPago", DbType.Int32, porIdTipoPago);
            db.AddInParameter(cmd, "@pIdEstado", DbType.Int32, porIdEstatus);
            db.AddInParameter(cmd, "@pNumLinea", DbType.String, porLineaCaptura);
            db.AddInParameter(cmd, "@pIdBanco", DbType.Int32, porIdBanco);
            db.AddInParameter(cmd, "@pFechaIni", DbType.DateTime, DBNull.Value);
            db.AddInParameter(cmd, "@pFechaFin", DbType.DateTime, DBNull.Value);
            if (porFechaInicio != null)
                db.SetParameterValue(cmd, "@pFechaIni", porFechaInicio);
            if (porFechaFin != null)
                db.SetParameterValue(cmd, "@pFechaFin", porFechaFin);

            using (var dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    string BusTipoPago = string.Empty, BusFechaPago = string.Empty, BusHoraPago = string.Empty,
                           BusLineaCaptuta = string.Empty, BusMedioRecepcion = string.Empty, BusDescEstado = string.Empty,
                           BusNombreXML = string.Empty, BusNombreZip = string.Empty, BusDescError = string.Empty,
                           BusImporte = string.Empty, BusNumOperaciones = string.Empty;
                    DateTime? BusFechaProceso = null;

                    if (dr["TipoPagoDesc"] != DBNull.Value) BusTipoPago = dr.GetString(dr.GetOrdinal("TipoPagoDesc"));
                    if (dr["FechaPago"] != DBNull.Value) BusFechaPago = dr.GetString(dr.GetOrdinal("FechaPago"));
                    if (dr["HoraPago"] != DBNull.Value) BusHoraPago = dr.GetString(dr.GetOrdinal("HoraPago"));
                    if (dr["Importe"] != DBNull.Value) BusImporte = dr.GetString(dr.GetOrdinal("Importe"));
                    if (dr["NumOperacion"] != DBNull.Value) BusNumOperaciones = dr.GetString(dr.GetOrdinal("NumOperacion"));
                    if (dr["LineaCaptura"] != DBNull.Value) BusLineaCaptuta = dr.GetString(dr.GetOrdinal("LineaCaptura"));
                    if (dr["DescMedio"] != DBNull.Value) BusMedioRecepcion = dr.GetString(dr.GetOrdinal("DescMedio"));
                    if (dr["DescEstado"] != DBNull.Value) BusDescEstado = dr.GetString(dr.GetOrdinal("DescEstado"));
                    if (dr["FechaProceso"] != DBNull.Value) BusFechaProceso = dr.GetDateTime(dr.GetOrdinal("FechaProceso"));
                    if (dr["NombreXML"] != DBNull.Value) BusNombreXML = dr.GetString(dr.GetOrdinal("NombreXML"));
                    if (dr["NombreZip"] != DBNull.Value) BusNombreZip = dr.GetString(dr.GetOrdinal("NombreZip"));
                    if (dr["DescError"] != DBNull.Value) BusDescError = dr.GetString(dr.GetOrdinal("DescError"));

                    listaMonitorPagoDet.Add(new TraductorMonitorPagoDetalleBusqueda()
                    {
                        TipoPago = BusTipoPago,
                        FechaPago = BusFechaPago,
                        HoraPago = BusHoraPago,
                        Importe = BusImporte,
                        NumeroOperaciones = BusNumOperaciones,
                        LineaCaptura = BusLineaCaptuta,
                        MedioRecepcion = BusMedioRecepcion,
                        EstatusPago = BusDescEstado,
                        FechaProceso = BusFechaProceso,
                        XML = BusNombreXML,
                        ZIP = BusNombreZip,
                        Error = BusDescError
                    });
                }
            }
            return listaMonitorPagoDet;
        }

        /// <summary>
        /// Método para obtener los eventos registrados en el monitor de archivo ZIP.
        /// </summary>
        /// <param name="cadenaDeConexion">Cadena de conexión</param>
        /// <param name="porIdTipoPago">Identificador del tipo de pago</param>
        /// <param name="porArchivoZIP">Nombre Archivo ZIP</param>
        /// <param name="porFechaInicio">Fecha inicio creación ZIP</param>
        /// <param name="porFechaFin">Fecha fin creación ZIP</param>
        /// <returns>Lista del tipo <see cref="TraductorMonitorArchivoZIPBusqueda"/></returns>
        public List<TraductorMonitorArchivoZIPBusqueda> BuscarMonitorArchivoZIP(string cadenaDeConexion, int porIdTipoPago, string porArchivoZIP, DateTime? porFechaInicio, DateTime? porFechaFin)
        {
            var listaMonitor = new List<TraductorMonitorArchivoZIPBusqueda>();
            Database db = new SqlDatabase(cadenaDeConexion);
            DbCommand cmd = db.GetStoredProcCommand("pObtenerMonitorArchivoZIP");
            db.AddInParameter(cmd, "@pTipoPago", DbType.Int32, porIdTipoPago);
            db.AddInParameter(cmd, "@pNombreZIP", DbType.String, porArchivoZIP);
            db.AddInParameter(cmd, "@pFechaIni", DbType.DateTime, DBNull.Value);
            db.AddInParameter(cmd, "@pFechaFin", DbType.DateTime, DBNull.Value);
            if (porFechaInicio != null)
                db.SetParameterValue(cmd, "@pFechaIni", porFechaInicio);
            if (porFechaFin != null)
                db.SetParameterValue(cmd, "@pFechaFin", porFechaFin);

            using (var dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    string BusTipoPago = string.Empty, BusNombreZip = string.Empty;
                    long? BusImporte = null;
                    short? BusNumOperaciones = null;
                    DateTime? BusFechaCreacion = null;

                    if (dr["TipoPagoDesc"] != DBNull.Value) BusTipoPago = dr.GetString(dr.GetOrdinal("TipoPagoDesc"));
                    if (dr["FechaCreacion"] != DBNull.Value) BusFechaCreacion = dr.GetDateTime(dr.GetOrdinal("FechaCreacion"));
                    if (dr["NombreZip"] != DBNull.Value) BusNombreZip = dr.GetString(dr.GetOrdinal("NombreZip"));
                    if (dr["Importe"] != DBNull.Value) BusImporte = dr.GetInt64(dr.GetOrdinal("Importe"));
                    if (dr["NumPagos"] != DBNull.Value) BusNumOperaciones = dr.GetInt16(dr.GetOrdinal("NumPagos"));

                    listaMonitor.Add(new TraductorMonitorArchivoZIPBusqueda()
                    {
                        TipoPago = BusTipoPago,
                        FechaCreacion = BusFechaCreacion,
                        Nombre = BusNombreZip,
                        Importe = BusImporte,
                        NumeroPagos = BusNumOperaciones
                    });
                }
            }
            return listaMonitor;
        }

        /// <summary>
        /// Método para obtener los eventos registrados en el monitor de tareas programadas.
        /// </summary>
        /// <param name="cadenaDeConexion">Cadena de conexión</param>
        /// <param name="porIdTipoPago">Identificador del tipo de pago</param>
        /// <param name="porIdEstatus">Identificador estatus</param>
        /// <param name="porFechaInicio">Fecha inicio proceso</param>
        /// <param name="porFechaFin">Fecha fin proceso</param>
        /// <returns>Lista del tipo <see cref="TraductorMonitorTareaProgramadaBusqueda"/></returns>
        public List<TraductorMonitorTareaProgramadaBusqueda> BuscarMonitorTareaProgramada(string cadenaDeConexion, int porIdTipoPago, int porIdEstatus, DateTime? porFechaInicio, DateTime? porFechaFin)
        {
            var listaMonitor = new List<TraductorMonitorTareaProgramadaBusqueda>();
            Database db = new SqlDatabase(cadenaDeConexion);
            DbCommand cmd = db.GetStoredProcCommand("pObtenerMonitorTProgramada");
            db.AddInParameter(cmd, "@pTipoPago", DbType.Int32, porIdTipoPago);
            db.AddInParameter(cmd, "@pIdEstado", DbType.Int32, porIdEstatus);
            db.AddInParameter(cmd, "@pFechaIni", DbType.DateTime, porFechaInicio);
            db.AddInParameter(cmd, "@pFechaFin", DbType.DateTime, porFechaFin);

            using (var dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    string BusTipoPago = string.Empty, BusFechaPresenacion = string.Empty, BusDescEstado = string.Empty,
                           BusNombreArchivo = string.Empty, BusNombreZip = string.Empty, BusDescError = string.Empty;
                    DateTime? BusFechaProceso = null;
                    long? BusIdProceso = null, BusImporte = null;
                    short? busNumPagos = null;

                    if (dr["TipoPagoDesc"] != DBNull.Value) BusTipoPago = dr.GetString(dr.GetOrdinal("TipoPagoDesc"));
                    if (dr["IdProceso"] != DBNull.Value) BusIdProceso = dr.GetInt64(dr.GetOrdinal("IdProceso"));
                    if (dr["NombreArchivo"] != DBNull.Value) BusNombreArchivo = dr.GetString(dr.GetOrdinal("NombreArchivo"));
                    if (dr["DescEstado"] != DBNull.Value) BusDescEstado = dr.GetString(dr.GetOrdinal("DescEstado"));
                    if (dr["FechaPago"] != DBNull.Value) BusFechaPresenacion = dr.GetString(dr.GetOrdinal("FechaPago"));
                    if (dr["FechaProceso"] != DBNull.Value) BusFechaProceso = dr.GetDateTime(dr.GetOrdinal("FechaProceso"));
                    if (dr["NombreZip"] != DBNull.Value) BusNombreZip = dr.GetString(dr.GetOrdinal("NombreZip"));
                    if (dr["Importe"] != DBNull.Value) BusImporte = dr.GetInt64(dr.GetOrdinal("Importe"));
                    if (dr["NumPagos"] != DBNull.Value) busNumPagos = dr.GetInt16(dr.GetOrdinal("NumPagos"));
                    if (dr["DescError"] != DBNull.Value) BusDescError = dr.GetString(dr.GetOrdinal("DescError"));


                    listaMonitor.Add(new TraductorMonitorTareaProgramadaBusqueda()
                    {
                        TipoPago = BusTipoPago,
                        IdProceso = BusIdProceso,
                        NombreArchivo = BusNombreArchivo,
                        Estatus = BusDescEstado,
                        FechaPresentacion = BusFechaPresenacion,
                        FechaProceso = BusFechaProceso,
                        NombreArchivoZIP = BusNombreZip,
                        Importe = BusImporte,
                        NumeroPagos = busNumPagos,
                        Error = BusDescError
                    });
                }
            }

            return listaMonitor;
        }

        /// <summary>
        /// Método para obtener un catalogo
        /// </summary>
        /// <param name="cadenaDeConexion">Cadena de conexión</param>
        /// <param name="idCatalogo">Identificador del catalogo</param>
        /// <returns>Diccionario de datos</returns>
        public Dictionary<int, string> ObtenerCatalogo(string cadenaDeConexion, int idCatalogo)
        {
            var listaCatalogo = new Dictionary<int, string>();
            Database db = new SqlDatabase(cadenaDeConexion);
            DbCommand cmd = db.GetStoredProcCommand("pBitacoraEventosCatalogo");
            db.AddInParameter(cmd, "@pIdCatalogo", DbType.Int16, idCatalogo);
            using (var dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    listaCatalogo.Add(Convert.ToInt32(dr["valor"]), dr["descripcion"].ToString());
                }
            }
            return listaCatalogo;
        }
    }
}
