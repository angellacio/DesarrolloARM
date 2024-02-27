using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;


namespace Sat.CreditosFiscales.Datos.AccesoDatos.Pagos
{
    using Sat.CreditosFiscales.Comunes.Entidades.Pagos;
    using Sat.CreditosFiscales.Comunes.Herramientas;
    using System.Globalization;
    public static class SIATDALPagos
    {
        /// <summary>
        /// Método que permite obtener los datos generales.
        /// </summary>
        /// <param name="idSolicitud">Id de la solicitud de línea de captura.</param>
        /// <returns>Lista de documento y conceptos originales que crearon la línea de captura.</returns>
        public static CreditosFiscalesResolucion[] ObtenerResoluciones(string idSolicitud, Database db)
        {
            var cmdDeterminantes = db.GetStoredProcCommand("pPagosSIATDoctosDeterminantesObtener");
            db.AddInParameter(cmdDeterminantes, "@pIdProcesamiento", DbType.Guid, Guid.Parse(idSolicitud));
            List<CreditosFiscalesResolucion> resoluciones = new List<CreditosFiscalesResolucion>();
            using (var dataReader = db.ExecuteReader(cmdDeterminantes))
            {
                while (dataReader.Read())
                {
                    var resolucion = new CreditosFiscalesResolucion();
                    resolucion.IdResolucion = (string)dataReader["IdResolucion"];
                    Int16 idAgrupador = Int16.Parse(dataReader["IdAgrupador"].ToString());
                    resolucion.Conceptos = SIATDALPagos.ObtenerDocumentosConceptos(idSolicitud, idAgrupador, db);
                    resoluciones.Add(resolucion);
                }
            }
            return resoluciones.ToArray();

        }

        /// <summary>
        /// Método que permite obtener los conceptos que conforman el documento determinante.
        /// </summary>
        /// <param name="idSolicitud">Id de la solicitud de línea de captura.</param>
        /// <param name="idAgrupador">Id del agrupador .</param>
        /// <returns>Lista de conceptos originales que crearon la línea de captura.</returns>
        public static CreditosFiscalesResolucionConcepto[] ObtenerDocumentosConceptos(string idSolicitud, int idAgrupador, Database db)
        {
            var cmdConceptos = db.GetStoredProcCommand("pPagosSIATConceptosPorDeterminanteObtener");
            db.AddInParameter(cmdConceptos, "@pIdProcesamiento", DbType.Guid, Guid.Parse(idSolicitud));
            db.AddInParameter(cmdConceptos, "@pIdAgrupador", DbType.Int32, idAgrupador);
            List<CreditosFiscalesResolucionConcepto> conceptos = new List<CreditosFiscalesResolucionConcepto>();
            using (var dataReader = db.ExecuteReader(cmdConceptos))
            {
                while (dataReader.Read())
                {
                    var concepto = new CreditosFiscalesResolucionConcepto();
                    concepto.IdConcepto = (string)dataReader["IdConcepto"];
                    concepto.TransaccionBaja = pPagosSIATTransaccionesObtener(idSolicitud, idAgrupador, int.Parse(dataReader["IdConceptoOriginal"].ToString()), db);
                    conceptos.Add(concepto);
                }
            }
            return conceptos.ToArray();
        }

        /// <summary>
        /// Método que permite obtener las transacciones que conforman el concepto original.
        /// </summary>
        /// <param name="idSolicitud">Id de la solicitud de línea de captura.</param>
        /// <param name="idAgrupador">Id del agrupador .</param>
        /// <returns>Lista de conceptos originales que crearon la línea de captura.</returns>
        public static CreditosFiscalesResolucionConceptoTransaccionBaja[] pPagosSIATTransaccionesObtener(string idSolicitud, int idAgrupador, int idConceptoOriginal, Database db)
        {
            var cmdConceptos = db.GetStoredProcCommand("pPagosSIATTransaccionesObtener");
            db.AddInParameter(cmdConceptos, "@pIdProcesamiento", DbType.Guid, Guid.Parse(idSolicitud));
            db.AddInParameter(cmdConceptos, "@pIdAgrupador", DbType.Int32, idAgrupador);
            db.AddInParameter(cmdConceptos, "@pIdConceptoOriginal", DbType.Int32, idConceptoOriginal);
            List<CreditosFiscalesResolucionConceptoTransaccionBaja> transacciones = new List<CreditosFiscalesResolucionConceptoTransaccionBaja>();
            using (var dataReader = db.ExecuteReader(cmdConceptos))
            {
                while (dataReader.Read())
                {
                    CreditosFiscalesResolucionConceptoTransaccionBaja transaccion = new CreditosFiscalesResolucionConceptoTransaccionBaja();
                    transaccion.Value = (string)dataReader["TransaccionBaja"];
                    transacciones.Add(transaccion);
                }
            }
            return transacciones.ToArray(); ;
        }

        /// <summary>
        /// Método que permite obtener los documentos que conforman la línea de captura por solicitud.
        /// </summary>
        /// <param name="idSolicitud">Id de la solicitud de línea de captura.</param>
        /// <returns>Lista de documento y conceptos originales que crearon la línea de captura.</returns>
        public static CreditosFiscalesDatosGenerales ObtenerDatosGenerales(TblLineaCaptura tblLC, string formatoFecha, Database db)
        {
            CreditosFiscalesDatosGenerales generales = new CreditosFiscalesDatosGenerales();
            generales.TipoLinea = -1;
            DbCommand dbCommand = db.GetStoredProcCommand("pPagosSIATGeneralesObtener");
            db.AddInParameter(dbCommand, "@pIdProcesamiento", DbType.Guid, Guid.Parse(tblLC.IdSolicitud));
            using (var dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    generales.Convenio = new CreditosFiscalesDatosGeneralesConvenio();
                    generales.PagoEfectivo = new CreditosFiscalesDatosGeneralesPagoEfectivo();
                    generales.PagoVirtual = new CreditosFiscalesDatosGeneralesPagoVirtual();
                    generales.Convenio.ID = dataReader["NumeroConvenio"].ToString();
                    generales.NumeroDocumento = dataReader["FolioDyP"].ToString();
                    generales.TipoPago = int.Parse(dataReader["TipoPago"].ToString());
                    generales.TipoDocumento = int.Parse(dataReader["IdTipoDocumento"].ToString());
                    generales.TipoLinea = int.Parse(dataReader["IdTipoLinea"].ToString());
                    generales.RFC = dataReader["RFC"].ToString();
                    generales.ALR = int.Parse(dataReader["ClaveAlr"].ToString());
                    generales.LineaCaptura = dataReader["LineaCaptura"].ToString();
                    generales.PagoVirtual.Importe = long.Parse(dataReader["PagoVirtualImporte"].ToString());
                    generales.PagoVirtual.FechaPago = DateTime.Parse(tblLC.FechaPago.ToString()).ToString(formatoFecha);
                    generales.PagoEfectivo.Importe = long.Parse(dataReader["PagoEfectivoImporte"].ToString());
                    generales.PagoEfectivo.FechaPago = DateTime.Parse(tblLC.FechaPago.ToString()).ToString(formatoFecha);
                }
                dataReader.NextResult();

                while (dataReader.Read())
                {
                    generales.Transaccion = dataReader["TransaccionBaja"].ToString();
                }
            }

            return generales;
        }

        public static CreditosFiscales ObtenerPagoSIAT(TblLineaCaptura tblLinea, string formatoFecha)
        {
            Database db = DatabaseFactory.CreateDatabase();
            CreditosFiscales pagoSIAT = new CreditosFiscales();
            if (tblLinea.IdSolicitud != null)
            {
                pagoSIAT.DatosGenerales = SIATDALPagos.ObtenerDatosGenerales(tblLinea, formatoFecha, db);
                pagoSIAT.Resoluciones = SIATDALPagos.ObtenerResoluciones(tblLinea.IdSolicitud, db);


            }
            else
            {
                CreditosFiscalesDatosGenerales datosGenerales = new CreditosFiscalesDatosGenerales();
                CreditosFiscalesDatosGeneralesPagoEfectivo pagoEfectivo = new CreditosFiscalesDatosGeneralesPagoEfectivo();
                string tipoDyPPuro = tblLinea.LineaCaptura.Substring(10, 2);
                datosGenerales.LineaCaptura = tblLinea.LineaCaptura;
                datosGenerales.TipoLinea = -2;
                datosGenerales.TipoDocumento = 22;
                if (tipoDyPPuro == "55")
                {
                    datosGenerales.TipoDocumento = 10;
                    datosGenerales.TipoLinea = 1;
                }
                if (tipoDyPPuro == "56")
                {
                    datosGenerales.TipoDocumento = 22;
                    datosGenerales.TipoLinea = 7;
                }
                pagoEfectivo.Importe = tblLinea.MontoLineaCaptura;
                pagoEfectivo.FechaPago = DateTime.Parse(tblLinea.FechaPago.ToString()).ToString(formatoFecha);
                datosGenerales.PagoEfectivo = pagoEfectivo;
                pagoSIAT.DatosGenerales = datosGenerales;
            }

            return pagoSIAT;
        }

        /// <summary>
        /// Método que almacena el detalle de los pagos para SIAT de contribuyentes.
        /// </summary>
        /// <param name="idProceso">Identificador del proceso de pagos.</param>
        /// <param name="archivo">Informacion y contenido del archivo SIAT.</param>
        public static void InsertarDetallePagos(long idProceso, InfoArchivoSIAT archivo)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("pPagosSIATDetalleInsertar");
            db.AddInParameter(dbCommand, "@pIdProceso", DbType.Int64, idProceso);
            db.AddInParameter(dbCommand, "@pNombreArchivo", DbType.String, archivo.NomArchivoDetalle);
            if (archivo.Generales.IdSolicitud != null)
            {
                db.AddInParameter(dbCommand, "@pIdProcesamiento", DbType.Guid, Guid.Parse(archivo.Generales.IdSolicitud));
            }
            else
            {
                db.AddInParameter(dbCommand, "@pIdProcesamiento", DbType.Guid, null);
            }
            db.AddInParameter(dbCommand, "@pContenido", DbType.String, archivo.Contenido);
            var result = db.ExecuteNonQuery(dbCommand);
        }

        public static void EliminaRegistroExistente(long idProceso)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("pPagosSIATDetalleEliminar");
            db.AddInParameter(dbCommand, "@pIdProceso", DbType.Int64, idProceso);
            var result = db.ExecuteNonQuery(dbCommand);
        }

        public static void EliminaRegistroExistenteVirtual(long idProceso, string detalleArchivo)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("pPagosSIATDetalleEliminarVirtual");
            db.AddInParameter(dbCommand, "@pIdProceso", DbType.Int64, idProceso);
            db.AddInParameter(dbCommand, "@pNombreArchivo", DbType.String, detalleArchivo);
            var result = db.ExecuteNonQuery(dbCommand);
        }

        public static List<InfoArchivoSIAT> ObtenerInformacionArchivosDeSalida(long idProceso, int pag, int reg, Int16 TipoArchivo)
        {
            var listaArchivos = new List<InfoArchivoSIAT>();
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("pPagosSIATInfoArchivosObtener");
            db.AddInParameter(dbCommand, "@pIdProceso", DbType.Int64, idProceso);
            db.AddInParameter(dbCommand, "@pagina", DbType.Int32, pag);
            db.AddInParameter(dbCommand, "@registros", DbType.Int32, reg);
            db.AddInParameter(dbCommand, "@tipoArchivo", DbType.Int16, TipoArchivo);
            using (var dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    var infoArchivo = new InfoArchivoSIAT();
                    infoArchivo.NomArchivoDetalle = (string)dataReader["NombreArchivo"];
                    infoArchivo.Contenido = (string)dataReader["Contenido"];
                    infoArchivo.setGenerales(MetodosComunes.Deserializa<CreditosFiscales>(infoArchivo.Contenido));
                    listaArchivos.Add(infoArchivo);
                }
            }
            return listaArchivos;
        }

        public static void InsertarNoProcesados(long idProceso, string NomArchivoDetalle, string IdSolicitud)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("pPagosSIATDetalleInsertarNoProcesados");
            db.AddInParameter(dbCommand, "@pIdProceso", DbType.Int64, idProceso);
            db.AddInParameter(dbCommand, "@pNombreArchivo", DbType.String, NomArchivoDetalle);
            db.AddInParameter(dbCommand, "@pIdProcesamiento", DbType.Guid, Guid.Parse(IdSolicitud));
            var result = db.ExecuteNonQuery(dbCommand);
        }


        #region Atención POL-8241 

        //  public static void RegistraArchivos(long IdProceso, )

        /// <summary>
        /// Registra los datos del archivo Q en la tabla de control tblControlPagosHead
        /// </summary>
        /// <param name="idProceso"></param>
        /// <param name="archivo"></param>
        /// <returns></returns>
        public static long RegistraArchivoQ(long idProceso, TblControlPagosHead archivo)
        {
            long idArchivo = 0;
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("pRegistraControlPagosH");
            db.AddInParameter(dbCommand, "@pIdProceso", DbType.Int64, idProceso);
            db.AddInParameter(dbCommand, "@pNombreArchivo", DbType.String, archivo.NombreArchivo);
            db.AddInParameter(dbCommand, "@pIdBanco", DbType.Int32, archivo.IdBanco);
            db.AddInParameter(dbCommand, "@pFechaPago", DbType.String, archivo.FechaPago);
            db.AddInParameter(dbCommand, "@pNumRegistros", DbType.Int32, archivo.NumRegistros);
            db.AddInParameter(dbCommand, "@pimporte", DbType.Int64, archivo.Importe);
            //          var result = db.ExecuteReader(dbCommand);

            using (var dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    idArchivo = Convert.ToInt64(dataReader[0]);
                }
            }

            return idArchivo;
        }


        /// <summary>
        /// Registra los datos del archivo E en la tabla de control tblControlPagosDet
        /// </summary>
        /// <param name="archivo"></param>
        public static void RegistraArchivoE(TblControlPagosDet archivo, bool esVirtual)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("pRegistraControlPagosDet");

            db.AddInParameter(dbCommand, "@IdArchivo", DbType.Int64, archivo.IdArchivo);
            db.AddInParameter(dbCommand, "@NumLinea", DbType.Int32, archivo.NumLinea);
            db.AddInParameter(dbCommand, "@Consecutivo", DbType.Int32, archivo.Consecutivo);
            db.AddInParameter(dbCommand, "@LineaCaptura", DbType.String, archivo.LineaCaptura);
            db.AddInParameter(dbCommand, "@FechaPago", DbType.String, archivo.FechaPago);
            db.AddInParameter(dbCommand, "@HoraPago", DbType.String, archivo.HoraPago);
            db.AddInParameter(dbCommand, "@Importe", DbType.Int64, archivo.Importe);
            db.AddInParameter(dbCommand, "@NumOperacion", DbType.String, archivo.NumOperacion);
            db.AddInParameter(dbCommand, "@MedioRecepcion", DbType.Int32, archivo.MedioRecepcion);
            db.AddInParameter(dbCommand, "@Version", DbType.Int32, archivo.Version);
            db.AddInParameter(dbCommand, "@TipoPago", DbType.Int32, archivo.TipoPago);
            db.AddInParameter(dbCommand, "@IdEstado", DbType.Int32, archivo.IdEstado);
            db.AddInParameter(dbCommand, "@IdZip", DbType.Int64, archivo.IdZip);
            db.AddInParameter(dbCommand, "@NombreXML", DbType.String, archivo.NombreXML);

            if (esVirtual)
                db.AddInParameter(dbCommand, "@idProcesamiento", DbType.String, archivo.idProcesamiento);


            var result = db.ExecuteNonQuery(dbCommand);
        }








        /// <summary>
        /// Obtine el el nombre de un archivo Q con estado de registrado para comenzar a procesarlo
        /// </summary>
        /// <param name="IdProceso">Identificador del proceso Actual</param>
        /// <param name="Equipo">Nomvre del equipo que procesara el archivo</param>
        /// <returns></returns>
        public static TblControlPagosHead ObtieneNombreArchivoaProcesar(long IdProceso, string Equipo)
        {
            TblControlPagosHead archivoq = new TblControlPagosHead();
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("pObtenerArchivoQProcesar");
            db.AddInParameter(dbCommand, "@pIdProceso", DbType.Int64, IdProceso);
            db.AddInParameter(dbCommand, "@NombreEquipo", DbType.String, Equipo);

            using (var dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    archivoq.idArchivo = (long)dataReader["idarchivo"];
                    archivoq.NombreArchivo = (string)dataReader["NombreArchivo"];
                }
            }


            return archivoq;
        }



        public static void ActualizaEstadoProceso(long IdProceso, long Archivo, int Estado)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("pActualizaEstadoProceso");
            db.AddInParameter(dbCommand, "@pidProceso", DbType.Int64, IdProceso);
            db.AddInParameter(dbCommand, "@pidArchivo", DbType.Int64, Archivo);
            db.AddInParameter(dbCommand, "@pEstado", DbType.Int32, Estado);
            var result = db.ExecuteNonQuery(dbCommand);

        }

        //public static void ActualizaEstadoProcesoLinea(long idProceso, int IdArchivo, string LC, int Estado)
        //{
        //    Database db = DatabaseFactory.CreateDatabase();
        //    DbCommand dbCommand = db.GetStoredProcCommand("pActualizaEstadoProcesoLC");
        //    db.AddInParameter(dbCommand, "@pIdProceso", DbType.Int64, idProceso);
        //    db.AddInParameter(dbCommand, "@pidArchivo", DbType.Int64, IdArchivo);
        //    db.AddInParameter(dbCommand, "@pLineaCaptura", DbType.String, LC);
        //    db.AddInParameter(dbCommand, "@pEstado", DbType.Int32, Estado);
        //    var result = db.ExecuteNonQuery(dbCommand);
        //}


        /// <summary>
        /// Actualiza el campo de error al momento de procesar el archivo Q
        /// </summary>
        /// <param name="IdProceso">Id Proceso </param>
        /// <param name="Archivo">Id Archivo con el error</param>
        /// <param name="Error">Id Error ocurrido a nuvel archivo</param>
        public static void ActualizaErrorProceso(long IdProceso, long IdArchivo, int IdError)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("pActualizaEstadoError");
            db.AddInParameter(dbCommand, "@pidArchivo", DbType.Int64, IdArchivo);
            db.AddInParameter(dbCommand, "@pEstado", DbType.Int32, IdError);

            if (IdArchivo < 0)
                db.AddInParameter(dbCommand, "@idProceso", DbType.Int64, IdProceso);

            var result = db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// Actualiza el campo Error y el motivo al momento de procesar el pago de la linea de captura
        /// </summary>
        /// <param name="IdProceso"></param>
        /// <param name="Archivo"></param>
        /// <param name="LC"></param>
        /// <param name="Error"></param>
        public static void ActualizaErrorProcesoLC(long IdProceso, long idArchivo, string LC, int IdError)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("pActualizaEstadoErrorLC");
            db.AddInParameter(dbCommand, "@pIdProceso", DbType.Int64, IdProceso);
            db.AddInParameter(dbCommand, "@pidArchivo", DbType.Int64, idArchivo);
            db.AddInParameter(dbCommand, "@pLC", DbType.String, LC);
            db.AddInParameter(dbCommand, "@pIdError", DbType.Int32, IdError);
            var result = db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// Actualiza el nombre del archivo XML generado para el archivo zip
        /// </summary>
        /// <param name="IdProceso"></param>
        /// <param name="NombreXML"></param>
        /// <param name="LC"></param>
        /// <param name="IdProcesamiento"></param>
        public static void ActualizaNombreXML(long IdProceso, string NombreXML, string LC, string IdProcesamiento)
        {

        }

        /// <summary>
        /// Actualiza el Identificador de procesamiento en la tabla de controlDet
        /// </summary>
        /// <param name="IdProceso"></param>
        /// <param name="LC"></param>
        /// <param name="IdProcesamiento"></param>
        public static void ActualizaIdProcesamiento(long IdProceso, string LC, string IdProcesamiento)
        {

        }

        /// <summary>
        /// Inserta el archivo Zip en tblControlZIp y actualiza ControlDetalle con el IdZip
        /// </summary>
        /// <param name="idProceso"></param>
        /// <param name="LC"></param>
        /// <param name="NombreXML"></param>
        /// <param name="NombreZip"></param>
        public static void ActualizaNombreZip(long idProceso, string NombreZip, List<TblControlPagosDet> lstDetalles, Int16 TipoPago)
        {

            int idArchivo = 0;
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("pRegistraArchivoZip");
            db.AddInParameter(dbCommand, "@pNombreArchivo", DbType.String, NombreZip);

            using (var dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    idArchivo = Convert.ToInt32(dataReader["IdZip"]);
                }
            }


            int reg = 1;

            DbCommand dbCommandZip = db.GetStoredProcCommand("pActualizaIdArchivoZip");
            foreach (TblControlPagosDet detalle in lstDetalles)
            {
                dbCommandZip.Parameters.Clear();
                db.AddInParameter(dbCommandZip, "@IdZip", DbType.Int32, idArchivo);
                db.AddInParameter(dbCommandZip, "@LC", DbType.String, detalle.LineaCaptura);
                db.AddInParameter(dbCommandZip, "@NombreXML", DbType.String, detalle.NombreXML);
                db.AddInParameter(dbCommandZip, "@idProceso", DbType.Int64, idProceso);

                if (reg == lstDetalles.Count())
                    db.AddInParameter(dbCommandZip, "@final", DbType.Boolean, 1);
                else
                    db.AddInParameter(dbCommandZip, "@final", DbType.Boolean, 0);


                db.AddInParameter(dbCommandZip, "@TipoPago", DbType.Int16, TipoPago);


                if (TipoPago == 2)
                    db.AddInParameter(dbCommandZip, "@NumOperacion", DbType.String, detalle.NumOperacion);




                var result = db.ExecuteNonQuery(dbCommandZip);

                reg++;

            }

        }




        #endregion
    }
}
