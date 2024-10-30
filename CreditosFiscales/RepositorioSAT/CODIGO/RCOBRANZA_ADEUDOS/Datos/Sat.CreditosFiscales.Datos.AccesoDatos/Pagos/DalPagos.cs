
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Datos.AccesoDatos.Pagos:Sat.CreditosFiscales.Datos.AccesoDatos.Pagos.DalPagos:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Sat.CreditosFiscales.Comunes.Entidades.GeneraFormato;
using Sat.CreditosFiscales.Comunes.Entidades.Pagos;
using Sat.CreditosFiscales.Comunes.Entidades.Servicios;

namespace Sat.CreditosFiscales.Datos.AccesoDatos.Pagos
{
    /// <summary>
    /// Clase que se utiliza para acceder a la base de datos durante el proceso de ejecución de pagos.
    /// </summary>
    public static class DalPagos
    {
        /// <summary>
        /// Obteiene los diferentes repositorios habilitados para obtener 
        /// </summary>
        /// <returns></returns>
        public static List<RutaArchivos> ObtenerRepositorioPagos()
        {
            var listaRepositorios = new List<RutaArchivos>();
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("pPagosRepositorioObtener");
            using (var dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    var repositorio = new RutaArchivos();
                    repositorio.IdRepositorio = (int)dataReader["IdRepositorio"];
                    repositorio.Nombre = (string)dataReader["Nombre"];
                    repositorio.RutaRepositorio = (string)dataReader["RutaRepositorio"];
                    repositorio.IdEstatus = (bool)dataReader["Idestatus"];
                    listaRepositorios.Add(repositorio);
                }
            }

            return listaRepositorios;
        }

        /// <summary>
        /// Método que obtiene las línes de captura extraidas del archivo de bancos.
        /// </summary>
        /// <param name="lineasDeCaptura">Cadena que contiene las líneas de captura.</param>
        /// <returns>Lista de información de las líneas de captura encontradas en la base de datos.</returns>
        public static List<TblLineaCaptura> ObtenerLineasDeCaptura(string lineasDeCaptura)
        {
            var listaLineasCaptura = new List<TblLineaCaptura>();
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("pPagosLineasDeCapturaObtener");
            db.AddInParameter(dbCommand, "@pLineasCaptura", DbType.String, lineasDeCaptura);
            using (var dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    var lineaCaptura = new TblLineaCaptura();
                    lineaCaptura.IdAplicacion = (short)dataReader["IdAplicacion"];
                    lineaCaptura.LineaCaptura = (string)dataReader["LineaCaptura"];
                    lineaCaptura.MontoLineaCaptura = (long)dataReader["ImporteTotal"];
                    lineaCaptura.IdSolicitud = dataReader.GetGuid(0).ToString();
                    lineaCaptura.IdEstadoSolicitud = Convert.ToInt32(dataReader["IdEstadoSolicitud"]);
                    lineaCaptura.Rfc = (string)dataReader["Rfc"];
                    lineaCaptura.IdTipoPersona = (byte?)dataReader["IdTipoPersona"];
                    lineaCaptura.FechaSolicitud = (DateTime)dataReader["FechaSolicitud"];
                    lineaCaptura.IdALR = (short?)dataReader["IdALR"];
                    lineaCaptura.IdTipoDocumento = (short)dataReader["IdTipoDocumento"];
                    lineaCaptura.IdAplicacion = (short)dataReader["IdAplicacion"];
                    lineaCaptura.IdTipoPersona = (byte?)dataReader["IdTipoPersona"];
                    if (dataReader["NumeroParcialidad"] == DBNull.Value)
                        lineaCaptura.NumeroParcialidad = null;
                    else
                    {
                        lineaCaptura.NumeroParcialidad = (byte?)dataReader["NumeroParcialidad"];
                    }
                    listaLineasCaptura.Add(lineaCaptura);
                }
            }

            return listaLineasCaptura;
        }

        /// <summary>
        /// Método que permite agregar las líneas de captura con inconsistencia de pago a la base de datos.
        /// </summary>
        /// <param name="listLineasInvalidas">Líneas de captura con diferencia de pago.</param>
        public static void AgregarRegistrosIncosistenciasPago(List<ReporteDiferencias> listLineasInvalidas)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("pPagosDiferenciasAgregar");
            foreach (ReporteDiferencias diferencia in listLineasInvalidas)
            {
                dbCommand.Parameters.Clear();
                db.AddInParameter(dbCommand, "@pIdSolicitud", DbType.Int64, diferencia.IdSolicitud);
                db.AddInParameter(dbCommand, "@pLineaCaptura", DbType.String, diferencia.LineaDeCaptura);
                db.AddInParameter(dbCommand, "@pIdALR", DbType.Int16, diferencia.Alr);
                db.AddInParameter(dbCommand, "@pRfc", DbType.String, diferencia.Rfc);
                db.AddInParameter(dbCommand, "@pMontoLineaCaptura", DbType.Decimal, diferencia.MontoPagarAplicativo);
                db.AddInParameter(dbCommand, "@pMontoPagado", DbType.Decimal, diferencia.MontoPagadoLinea);
                db.AddInParameter(dbCommand, "@pFechaPago", DbType.DateTime, diferencia.FechaOperacion);
                db.AddInParameter(dbCommand, "@pFechaProcesado", DbType.DateTime, DateTime.Now);
                db.AddInParameter(dbCommand, "@pNombreArchivo", DbType.String, diferencia.NombreArchivo);
                var result = db.ExecuteNonQuery(dbCommand);
            }
        }

        /// <summary>
        /// Método que permite obtener los documentos que conforman la línea de captura por solicitud.
        /// </summary>
        /// <param name="idSolicitud">Id de la solicitud de línea de captura.</param>
        /// <returns>Lista de documento y conceptos originales que crearon la línea de captura.</returns>
        public static List<Agrupadores> ObtenerDocumentosDeterminantes(string idSolicitud)
        {
            var listaDocumentoDeterminante = new List<Agrupadores>();
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("pPagosDoctosDeterminantesObtener");
            var dbSubCmd = db.GetStoredProcCommand("pPagosConceptosPorDeterminanteObtener");
            db.AddInParameter(dbCommand, "@pIdsolicitud", DbType.Guid, Guid.Parse(idSolicitud));
            using (var dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    var agrupador = new Agrupadores();
                    agrupador.IdProcesamiento = dataReader.GetGuid(1).ToString();
                    agrupador.IdAgrupador = Convert.ToInt32(dataReader["IdAgrupador"]);
                    agrupador.ValorAgrupador = (string)dataReader["ValorAgrupador"];
                    agrupador.ImportePagar = (decimal)dataReader["ImporteTotalPagar"];
                    agrupador.SaldoInformativo = (decimal?)dataReader["SaldoInformativo"];
                    listaDocumentoDeterminante.Add(agrupador);

                    dbSubCmd.Parameters.Clear();
                    db.AddInParameter(dbSubCmd, "@pIdSolicitud", DbType.Guid, Guid.Parse(idSolicitud));
                    db.AddInParameter(dbSubCmd, "@pIdAgrupador", DbType.Int32, agrupador.IdAgrupador);
                    using (var anotherReader = db.ExecuteReader(dbSubCmd))
                    {
                        while (anotherReader.Read())
                        {
                            var concepto = new ConceptoOriginal();
                            concepto.IdAgrupador = Convert.ToInt32(anotherReader["IdAgrupador"]);
                            concepto.IdSolicitud = anotherReader.GetGuid(1).ToString();
                            concepto.ClaveOrigen = (string)anotherReader["ConceptoOrigen"];
                            concepto.CreditoSir = Convert.ToInt32(anotherReader["CreditoSIR"]);
                            concepto.ImporteHistorico = (decimal)anotherReader["ImporteHistorico"];
                            concepto.ImportePagar = (decimal)anotherReader["ImportePagar"];
                            concepto.ImporteParteActualizada = (decimal)anotherReader["ImporteParteActualizada"];
                            concepto.ImporteDescuentos = (decimal)anotherReader["ImporteDescuento"];
                            concepto.Liquidar = (bool)anotherReader["Liquidar"];
                            concepto.IdConceptoOriginal = (int)anotherReader["IdConceptoOriginal"];
                            concepto.EsPadre = (bool)anotherReader["EsPadre"];
                            if (anotherReader["MotivoId"] == DBNull.Value)
                                concepto.MotivoId = string.Empty;
                            else
                            {
                                concepto.MotivoId = (string)anotherReader["MotivoId"];
                            }
                            agrupador.Conceptos.Add(concepto);
                        }
                    }
                }
            }
            return listaDocumentoDeterminante;
        }

        /// <summary>
        /// Método que actualiza a pagada la línea de captura.
        /// </summary>
        /// <param name="listaArchivosTemp">Lista de solicitudes para ser actualzadas como pagadas.</param>
        public static void ActualizarLineasDeCaptura(List<InfoArchivoSIR> listaArchivosTemp)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("pPagosSolicitudActualizar");
            var solicitudes = new Dictionary<Guid, DateTime>();
            foreach (InfoArchivoSIR infoArchivoSIR in listaArchivosTemp.Where(infoArchivoSIR => !solicitudes.ContainsKey(Guid.Parse(infoArchivoSIR.IdSolicitud))))
            {
                solicitudes.Add(Guid.Parse(infoArchivoSIR.IdSolicitud), infoArchivoSIR.FechaPago);
            }

            foreach (var solicitud in solicitudes)
            {
                dbCommand.Parameters.Clear();
                db.AddInParameter(dbCommand, "@pIdSolicitud", DbType.Guid, solicitud.Key);
                db.AddInParameter(dbCommand, "@pFechaPago", DbType.DateTime, solicitud.Value);
                var result = db.ExecuteNonQuery(dbCommand);
            }
        }

        /// <summary>
        /// Método que obtiene el identificador del proceso de pagos.
        /// </summary>
        /// <param name="esReproceso">Activa si es un reproceso.</param>
        /// <returns>Número de proceso.</returns>
        public static long ObtenerIdProcesoPagos(ref bool esReproceso)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("pPagosIdProcesoObtener");
            long idProceso = 0;
            using (var dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    idProceso = (long)dataReader["IdProceso"];
                    esReproceso = (bool)dataReader["EsReproceso"];
                }
            }
            return idProceso;
        }

        /// <summary>
        /// Método que almacena el detalle de los pagos de contribuyentes.
        /// </summary>
        /// <param name="idProceso">Identificador del proceso de pagos.</param>
        /// <param name="archivo">Información del detalle de los pagos.</param>
        /// <param name="contenido">Nombre del atrchivo de pago de bancos.</param>
        public static void InsertarDetallePagos(long idProceso, InfoArchivoSIR archivo, string contenido)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("pPagosDetalleInsertar");
            db.AddInParameter(dbCommand, "@pIdProceso", DbType.Int64, idProceso);
            db.AddInParameter(dbCommand, "@pNombreArchivo", DbType.String, archivo.DetalleArchivo);
            db.AddInParameter(dbCommand, "@pIdAlr", DbType.Int32, archivo.Alr);
            db.AddInParameter(dbCommand, "@pNumeroCredito", DbType.Int32, archivo.NumeroDeCreditoSir);
            db.AddInParameter(dbCommand, "@pImportePagado", DbType.Int64, archivo.ImportePagado);
            db.AddInParameter(dbCommand, "@pContenido", DbType.String, contenido);
            var result = db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// Método que actualiza la fecha de fin del proceso de pagos.
        /// </summary>
        /// <param name="idProceso">Proceso a ser actualizado como finalizado.</param>
        public static void ActualizaFechaFin(long idProceso)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("pPagosFechaFinActualizar");
            db.AddInParameter(dbCommand, "@pIdProceso", DbType.Int64, idProceso);
            var result = db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// Método que recupera la información de los archivos de  salida para SIR.
        /// </summary>
        /// <param name="idProceso">Número de proceso.</param>
        /// <returns>Lista de archivos de salida.</returns>
        public static List<ArchivoSalida> ObtenerInformacionArchivosDeSalida(long idProceso)
        {
            var listaArchivos = new List<ArchivoSalida>();
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("pPagosInfoArchivosObtener");
            db.AddInParameter(dbCommand, "@pIdProceso", DbType.Int64, idProceso);
            using (var dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    var infoArchivo = new ArchivoSalida();
                    infoArchivo.IdProceso = (long)dataReader["IdProceso"];
                    infoArchivo.IdAlr = (int)dataReader["IdAlr"];
                    infoArchivo.Contenido = (string)dataReader["Contenido"];
                    infoArchivo.ImportePagado = Convert.ToInt64((decimal)dataReader["ImportePagado"]);
                    listaArchivos.Add(infoArchivo);
                }
            }
            return listaArchivos;
        }

        /// <summary>
        /// Elimina registros existentes dentro de un reproceso.
        /// </summary>
        /// <param name="idProceso">Número de procesamiento actual.</param>
        public static void EliminaRegistroExistente(long idProceso)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("pPagosDetalleEliminar");
            db.AddInParameter(dbCommand, "@pIdProceso", DbType.Int64, idProceso);
            //db.AddInParameter(dbCommand, "@pNombreArchivo", DbType.String, infoArchivoSIR.DetalleArchivo);
            //db.AddInParameter(dbCommand, "@pIdAlr", DbType.Int32, idAlr);
            //db.AddInParameter(dbCommand, "@pNumeroCredito", DbType.Int32, infoArchivoSIR.NumeroDeCreditoSir);
            var result = db.ExecuteNonQuery(dbCommand);
        }

        public static List<TblLineaCaptura> ObtenerLineasDeCapturaVirtuales()
        {
            var listaLineasCaptura = new List<TblLineaCaptura>();
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("pPagosLineasDeCapturaVirtualesObtener");
            using (var dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    var lineaCaptura = new TblLineaCaptura();
                    lineaCaptura.IdAplicacion = (short)dataReader["IdAplicacion"];
                    lineaCaptura.LineaCaptura = dataReader["LineaCaptura"] == DBNull.Value ? null : (string)dataReader["LineaCaptura"];
                    lineaCaptura.MontoLineaCaptura = (long)dataReader["ImporteTotal"];
                    lineaCaptura.IdSolicitud = dataReader.GetGuid(0).ToString();
                    lineaCaptura.IdEstadoSolicitud = Convert.ToInt32(dataReader["IdEstadoSolicitud"]);
                    lineaCaptura.Rfc = (string)dataReader["Rfc"];
                    lineaCaptura.IdTipoPersona = (byte?)dataReader["IdTipoPersona"];
                    lineaCaptura.FechaSolicitud = (DateTime)dataReader["FechaSolicitud"];
                    lineaCaptura.IdALR = (short?)dataReader["IdALR"];
                    lineaCaptura.IdTipoDocumento = (short)dataReader["IdTipoDocumento"];
                    lineaCaptura.IdAplicacion = (short)dataReader["IdAplicacion"];
                    lineaCaptura.IdTipoPersona = (byte?)dataReader["IdTipoPersona"];
                    if (dataReader["NumeroParcialidad"] == DBNull.Value)
                        lineaCaptura.NumeroParcialidad = null;
                    else
                    {
                        lineaCaptura.NumeroParcialidad = (byte?)dataReader["NumeroParcialidad"];
                    }
                    listaLineasCaptura.Add(lineaCaptura);
                }
            }

            return listaLineasCaptura;
        }

        public static void EliminaRegistroExistenteVirtual(long idProceso, string detalleArchivo)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("pPagosDetalleEliminarVirtual");
            db.AddInParameter(dbCommand, "@pIdProceso", DbType.Int64, idProceso);
            db.AddInParameter(dbCommand, "@pNombreArchivo", DbType.String, detalleArchivo);
            //db.AddInParameter(dbCommand, "@pIdAlr", DbType.Int32, idAlr);
            //db.AddInParameter(dbCommand, "@pNumeroCredito", DbType.Int32, infoArchivoSIR.NumeroDeCreditoSir);
            var result = db.ExecuteNonQuery(dbCommand);
        }
    }
}
