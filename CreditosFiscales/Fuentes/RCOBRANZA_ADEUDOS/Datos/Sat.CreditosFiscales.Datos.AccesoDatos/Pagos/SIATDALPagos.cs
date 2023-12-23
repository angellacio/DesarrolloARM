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
                    generales.Convenio  = new CreditosFiscalesDatosGeneralesConvenio();
                    generales.PagoEfectivo = new CreditosFiscalesDatosGeneralesPagoEfectivo();
                    generales.PagoVirtual = new CreditosFiscalesDatosGeneralesPagoVirtual();
                    generales.Convenio.ID = dataReader["NumeroConvenio"].ToString();
                    generales.NumeroDocumento = dataReader["FolioDyP"].ToString();
                    generales.TipoPago =int.Parse(dataReader["TipoPago"].ToString());
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
          CreditosFiscales pagoSIAT= new CreditosFiscales();
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
              if(tipoDyPPuro == "56")
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

       public static List<InfoArchivoSIAT> ObtenerInformacionArchivosDeSalida(long idProceso)
       {
           var listaArchivos = new List<InfoArchivoSIAT>();
           Database db = DatabaseFactory.CreateDatabase();
           DbCommand dbCommand = db.GetStoredProcCommand("pPagosSIATInfoArchivosObtener");
           db.AddInParameter(dbCommand, "@pIdProceso", DbType.Int64, idProceso);
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
    }
}
