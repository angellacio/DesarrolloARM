
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Datos.AccesoDatos.GeneraFormato:Sat.CreditosFiscales.Datos.AccesoDatos.GeneraFormato.DalGeneracionFormato:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Sat.CreditosFiscales.Comunes.Entidades;
using Sat.CreditosFiscales.Comunes.Entidades.Catalogos;
using Sat.CreditosFiscales.Comunes.Entidades.GeneraFormato;
using Sat.CreditosFiscales.Comunes.Herramientas;


namespace Sat.CreditosFiscales.Datos.AccesoDatos.GeneraFormato
{
    public class DalGeneracionFormato : IDisposable
    {
        public Int64 GuardaSolicitud(Solicitud solicitud)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbConnection conexion = db.CreateConnection();

            DbTransaction transaccion = null;
            try
            {

                if (solicitud.Documentos != null && solicitud.Documentos.Count > 0)
                {
                    conexion.Open();

                    ////Se genera la solicitud
                    DbCommand cmdSolicitud = PreparaComandoInsercionSolicitud(solicitud, db);
                    transaccion = conexion.BeginTransaction();
                    db.ExecuteNonQuery(cmdSolicitud, transaccion);
                    Int64 idSolicitud = Convert.ToInt64(db.GetParameterValue(cmdSolicitud, MetodosComunes.ObtieneNombreParametroSql(Solicitud.Campos.IdSolicitud)));

                    foreach (var rfc in solicitud.Usuario.Rfcs)
                    {
                        DbCommand cmdRfc = PreparaComandoInsercionRfc(idSolicitud, rfc, db);
                        db.ExecuteNonQuery(cmdRfc, transaccion);
                    }

                    ////Se inserta cada uno de los documentos
                    foreach (var doc in solicitud.Documentos)
                    {
                        ////Se asigna el id de la solicitud, obtenido de la ejecución de cmdSolicitud.
                        doc.IdSolicitud = idSolicitud;
                        doc.IdDocumento = solicitud.Documentos.IndexOf(doc);
                        DbCommand cmdDeterminante = PreparaComandoInsercionDeterminante(doc, db);
                        db.ExecuteNonQuery(cmdDeterminante, transaccion);

                        InsertaMarcas(db, transaccion, doc);

                        InsertaConceptos(db, transaccion, idSolicitud, doc);

                    }
                    transaccion.Commit();
                    return idSolicitud;
                }
                else ////La lista de documentos viene vacia o nula
                {
                    ////TODO: Tipificar y almacenar la excepcion.
                    throw new Exception("La lista de documentos esta vacía o nula");
                }
            }          
            catch (Exception err)
            {                
                if (transaccion != null)
                {
                    transaccion.Rollback();
                }
                throw (err);
            }
            finally
            {
                if (conexion.State.Equals(ConnectionState.Open))
                {
                    conexion.Close();
                }
            }
        }

        /// <summary>
        /// Función que obtiene todas las marcas del documento de una solicitud
        /// </summary>
        /// <returns>Lista de <see cref="CatMarca"/></returns>
        public List<CatMarca> ObtenerMarcas(long idSolicitud, int idDocumento)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("pMarcasPorDocumentoConsultar");
            db.AddInParameter(dbCommand, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminante.Campos.IdSolicitud), DbType.Int64, idSolicitud);
            db.AddInParameter(dbCommand, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminante.Campos.IdDocumento), DbType.Int32, idDocumento);
            using (var dataReader = db.ExecuteReader(dbCommand))
            {
                List<CatMarca> marcas = new List<CatMarca>();
                while (dataReader.Read())
                {
                    int claveMarca = Convert.ToInt32(dataReader[CatMarca.Campos.CveMarca.ToString()].ToString()[0]) - 64;
                    
                    CatMarca marca = new CatMarca()
                    {
                        CveMarca = claveMarca,
                        Descripcion = (string)dataReader[CatMarca.Campos.Descripcion.ToString()],
                        GenerarLC = bool.Parse(dataReader[CatMarca.Campos.GenerarLC.ToString()].ToString()),
                        MostrarImportes = bool.Parse(dataReader[CatMarca.Campos.MostrarImportes.ToString()].ToString()),
                        Observacion = (string)dataReader[CatMarca.Campos.Observacion.ToString()]
                    };
                    marcas.Add(marca);
                }
                return marcas;
            }

        }

        #region Métodos privados

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="transaccion"></param>
        /// <param name="idSolicitud"></param>
        /// <param name="doc"></param>
        private void InsertaConceptos(Database db, DbTransaction transaccion, Int64 idSolicitud, DocumentoDeterminante doc)
        {
            if (doc.Conceptos != null && doc.Conceptos.Count > 0)
            {
                ////Se almacena cada uno de los conceptos en el documento determinante.
                foreach (var concepto in doc.Conceptos)
                {

                    ////Se asigna el id del determinante, obtenido de la ejecucion de cmdDeterminante.
                    concepto.IdDocumento = doc.IdDocumento;
                    concepto.IdSolicitud = idSolicitud;
                    concepto.IdDocumentoConcepto = doc.Conceptos.IndexOf(concepto);

                    DbCommand cmdInsertaConcepto = PreparaComandoInsercionConceptos(concepto, db);
                    db.ExecuteNonQuery(cmdInsertaConcepto, transaccion);

                    InsertaDescuentos(db, transaccion, concepto);

                    InsertaConceptosHijo(db, transaccion, concepto);
                }
            }
            else////La lista de conceptos esta vacía o nula
            {
                ////TODO: Tipificar y almacenar la excepcion.
                throw new Exception("La lista de conceptos esta vacía o nula");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="transaccion"></param>
        /// <param name="doc"></param>
        private void InsertaMarcas(Database db, DbTransaction transaccion, DocumentoDeterminante doc)
        {
            if (doc.Marcas != null)
            {
                foreach (var m in doc.Marcas)
                {
                    DbCommand cmdInsertaMarca = PreparaComandoInsercionMarca(doc.IdSolicitud, doc.IdDocumento, m, db);
                    db.ExecuteNonQuery(cmdInsertaMarca, transaccion);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="transaccion"></param>
        /// <param name="concepto"></param>
        private void InsertaConceptosHijo(Database db, DbTransaction transaccion, DocumentoDeterminanteConcepto concepto)
        {
            foreach (var cHijo in concepto.ConceptosHijo)
            {
                cHijo.IdDocumentoConcepto = concepto.IdDocumentoConcepto;
                cHijo.IdSolicitud = concepto.IdSolicitud;
                cHijo.IdDocumento = concepto.IdDocumento;
                cHijo.IdDocumentoConceptoHijo = concepto.ConceptosHijo.IndexOf(cHijo);
                DbCommand cmdInsertaConceptoHijo = PrepataComandoInsercionConceptoHijo(cHijo, db);
                db.ExecuteNonQuery(cmdInsertaConceptoHijo, transaccion);

                InsertaDescuentosHijo(db, transaccion, cHijo);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="transaccion"></param>
        /// <param name="cHijo"></param>
        private void InsertaDescuentosHijo(Database db, DbTransaction transaccion, DocumentoDeterminanteConceptoHijo cHijo)
        {
            foreach (var descuentoHijo in cHijo.Descuentos)
            {
                descuentoHijo.IdDescuento = descuentoHijo.IdDescuento; //cHijo.Descuentos.IndexOf(descuentoHijo);
                DbCommand cmdInsertaDescuentoHijo = PreparaComandoInsercionDescuentoHijo(cHijo, descuentoHijo, db);

                db.ExecuteNonQuery(cmdInsertaDescuentoHijo, transaccion);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="transaccion"></param>
        /// <param name="concepto"></param>
        private void InsertaDescuentos(Database db, DbTransaction transaccion, DocumentoDeterminanteConcepto concepto)
        {
            foreach (var descuento in concepto.Descuentos)
            {
                descuento.IdDescuento = descuento.IdDescuento;//concepto.Descuentos.IndexOf(descuento);
                DbCommand cmdInsertaDescuento = PreparaComandoInsercionDescuento(concepto, descuento, db);
                db.ExecuteNonQuery(cmdInsertaDescuento, transaccion);
            }
        }

        private DbCommand PreparaComandoInsercionRfc(Int64 idSolicitud, string rfc, Database db)
        {
            DbCommand cmd = db.GetStoredProcCommand("pRfcInserta");
            db.AddInParameter(cmd, "@IdSolicitud", DbType.Int64, idSolicitud);
            db.AddInParameter(cmd, "@RFC", DbType.String, rfc);
            return cmd;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="concepto"></param>
        /// <param name="descuento"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        private DbCommand PreparaComandoInsercionDescuento(DocumentoDeterminanteConcepto concepto, DescuentoConcepto descuento, Database db)
        {
            DbCommand cmd = db.GetStoredProcCommand("pDescuentoConceptoInserta");
            db.AddInParameter(cmd,MetodosComunes.ObtieneNombreParametroSql(DescuentoConcepto.Campos.IdDocumentoConcepto),DbType.Int32, concepto.IdDocumentoConcepto);
            db.AddInParameter(cmd,MetodosComunes.ObtieneNombreParametroSql(DescuentoConcepto.Campos.IdDocumento),DbType.Int32, concepto.IdDocumento);
            db.AddInParameter(cmd,MetodosComunes.ObtieneNombreParametroSql(DescuentoConcepto.Campos.IdSolicitud),DbType.Int64, concepto.IdSolicitud);
            db.AddInParameter(cmd,MetodosComunes.ObtieneNombreParametroSql(DescuentoConcepto.Campos.IdDescuento),DbType.String, descuento.IdDescuento);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DescuentoConcepto.Campos.ImporteDescuento), DbType.Decimal, descuento.ImporteDescuento);

            return cmd;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="concepto"></param>
        /// <param name="descuento"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        private DbCommand PreparaComandoInsercionDescuentoHijo(DocumentoDeterminanteConceptoHijo concepto, DescuentoConceptoHijo descuento, Database db)
        {
            DbCommand cmd = db.GetStoredProcCommand("pDescuentoConceptoHijoInserta");
            
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DescuentoConceptoHijo.Campos.IdDocumentoConcepto), DbType.Int32, concepto.IdDocumentoConcepto);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DescuentoConceptoHijo.Campos.IdDocumento), DbType.Int32, concepto.IdDocumento);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DescuentoConceptoHijo.Campos.IdSolicitud), DbType.Int64, concepto.IdSolicitud);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DescuentoConceptoHijo.Campos.IdDescuento), DbType.String, descuento.IdDescuento);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DescuentoConceptoHijo.Campos.IdConcepto), DbType.Int32, concepto.IdDocumentoConceptoHijo);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DescuentoConceptoHijo.Campos.ImporteDescuento), DbType.Decimal, descuento.ImporteDescuento);
            
            return cmd;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        /// <param name="IdDocumento"></param>
        /// <param name="marca"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        private DbCommand PreparaComandoInsercionMarca(Int64 IdSolicitud, int IdDocumento,CatMarca marca, Database db)
        {
            string cveMarca = Convert.ToChar(marca.CveMarca + 64).ToString();
            DbCommand cmd = db.GetStoredProcCommand("pDocumentoMarcaInserta");
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(CatMarca.Campos.CveMarca), DbType.String, cveMarca.ToString());
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(CatMarca.Campos.Observacion), DbType.String, marca.Observacion);
            db.AddInParameter(cmd, "@IdSolicitud", DbType.Int64, IdSolicitud);
            db.AddInParameter(cmd, "@IdDocumento", DbType.Int32, IdDocumento);

            return cmd;
        }

        /// <summary>
        /// Método que prepara el comando de inserción a la Tabla TblSolicitud
        /// </summary>
        /// <param name="solicitud">Objeto del tipo <see cref="SolicitudGeneracion"/></param>
        /// <param name="db">Conexion a la base de datos</param>
        /// <returns><see cref="DbCommand"/></returns>
        private DbCommand PreparaComandoInsercionSolicitud(Solicitud solicitud, Database db)
        {
            DbCommand cmd = db.GetStoredProcCommand("pSolicitudInserta");
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(Usuario.Campos.ApellidoMaterno), DbType.String, solicitud.Usuario.ApellidoMaterno);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(Usuario.Campos.ApellidoPaterno), DbType.String, solicitud.Usuario.ApellidoPaterno);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(Usuario.Campos.IdALR), DbType.Byte, solicitud.Usuario.IdALR);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(Usuario.Campos.Nombres), DbType.String, solicitud.Usuario.Nombres);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(Usuario.Campos.RazonSocial), DbType.String, solicitud.Usuario.RazonSocial);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(Usuario.Campos.Rfc), DbType.String, solicitud.Usuario.Rfc);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(Usuario.Campos.PersonaFisica), DbType.Boolean, solicitud.Usuario.PersonaFisica);
            db.AddOutParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(Solicitud.Campos.IdSolicitud), System.Data.DbType.Int64, 0);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(Usuario.Campos.IdTipoLineaMAT), DbType.Byte, solicitud.Usuario.IdTipoLineaMAT);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(Usuario.Campos.IdPersonaMAT), DbType.String, solicitud.Usuario.IdPersonaMAT);

            return cmd;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        private DbCommand PreparaComandoInsercionDeterminante(DocumentoDeterminante doc, Database db)
        {
            DateTime fechaDocumento = DateTime.MinValue.CompareTo(doc.FechaDocumento) == 0 ? SqlDateTime.MinValue.Value: doc.FechaDocumento;
            

            DbCommand cmd = db.GetStoredProcCommand("pDocumentoDeterminanteInserta");
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminante.Campos.NumDocumento), DbType.String, doc.NumDocumento);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminante.Campos.FechaDocumento), DbType.DateTime, fechaDocumento);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminante.Campos.IdSolicitud), DbType.Int64, doc.IdSolicitud);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminante.Campos.IdALR), DbType.Byte, doc.IdALR);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminante.Campos.IdAutoridad), DbType.Int32, doc.IdAutoridad);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminante.Campos.ImporteActualizacion), DbType.Decimal, doc.ImporteActualizacion);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminante.Campos.ImporteDescuentos), DbType.Decimal, doc.ImporteDescuentos);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminante.Campos.ImporteHistorico), DbType.Decimal, doc.ImporteHistorico);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminante.Campos.SaldoInformativo), DbType.Decimal, doc.SaldoInformativo);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminante.Campos.FormaPago), DbType.String, doc.FormaPago);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminante.Campos.Rfc), DbType.String, doc.Rfc);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminante.Campos.IdDocumento), DbType.Int32, doc.IdDocumento);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminante.Campos.IdResolucionMAT), DbType.String, doc.IdResolucionMAT);

            if (DateTime.MinValue.CompareTo(doc.FechaNotificacion) != 0)
                db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminante.Campos.FechaNotificacion), DbType.DateTime, doc.FechaNotificacion);
            //else
            //    Console.WriteLine("Fecha de notificación igual a 01/01/1900 que se transformo en DateTime.Min");
            

            return cmd;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="concepto">Objeto del tipo <see cref="DocumentoDeterminanteConcepto"/></param>
        /// <param name="db"></param>
        /// <returns></returns>
        private DbCommand PreparaComandoInsercionConceptos(DocumentoDeterminanteConcepto concepto, Database db)
        {
            DbCommand cmd = db.GetStoredProcCommand("pDocumentoDeterminanteConceptoInserta");
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminanteConcepto.Campos.IdDocumento), DbType.Int32, concepto.IdDocumento);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminanteConcepto.Campos.IdSolicitud), DbType.Int64, concepto.IdSolicitud);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminanteConcepto.Campos.CreditoARCA), DbType.String, concepto.CreditoARCA);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminanteConcepto.Campos.CreditoSIR), DbType.Int32, concepto.CreditoSIR);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminanteConcepto.Campos.IdConcepto), DbType.Int32, concepto.IdConcepto);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminanteConcepto.Campos.idTipoConcepto), DbType.Byte, concepto.IdTipoConcepto);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminanteConcepto.Campos.idPeriodicidad), DbType.Byte, concepto.IdPeriodicidad);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminanteConcepto.Campos.IdPeriodo), DbType.Byte, concepto.IdPeriodo);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminanteConcepto.Campos.Ejercicio), DbType.Int32, concepto.Ejercicio);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminanteConcepto.Campos.ImporteParteActualizada), DbType.Decimal, concepto.ImporteParteActualizada);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminanteConcepto.Campos.ImporteDescuentos), DbType.Decimal, concepto.ImporteDescuentos);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminanteConcepto.Campos.ImporteHistorico), DbType.Decimal, concepto.ImporteHistorico);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminanteConcepto.Campos.ImportePagar), DbType.Decimal, concepto.ImportePagar);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminanteConcepto.Campos.ImporteCondonacion), DbType.Decimal, concepto.ImporteCondonacion);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminanteConcepto.Campos.IdConceptoMAT), DbType.String, concepto.IdConceptoMAT);            
            if (concepto.FechaCausacion.HasValue)
            {
                db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminanteConcepto.Campos.FechaCausacion), DbType.DateTime, concepto.FechaCausacion.Value);
            }
            else
            {
                db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminanteConcepto.Campos.FechaCausacion), DbType.DateTime, SqlDateTime.Null);
            }
            if (concepto.FechaNotificacion.HasValue)
            {
                db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminanteConcepto.Campos.FechaNotificacion), DbType.DateTime, concepto.FechaNotificacion.Value);
            }
            else
            {
                db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminanteConcepto.Campos.FechaNotificacion), DbType.DateTime, SqlDateTime.Null);
            }
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminanteConcepto.Campos.IdDocumentoConcepto), DbType.Int32, concepto.IdDocumentoConcepto);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminanteConcepto.Campos.IdMotivo), DbType.String, concepto.IdMotivo);

            return cmd;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="concepto"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        private DbCommand PrepataComandoInsercionConceptoHijo(DocumentoDeterminanteConceptoHijo concepto, Database db)
        {
           
            DbCommand cmd = db.GetStoredProcCommand("pDocumentoDeterminanteConceptoHijoInserta");
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminanteConceptoHijo.Campos.IdSolicitud), DbType.Int64, concepto.IdSolicitud);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminanteConceptoHijo.Campos.IdDocumento), DbType.Int32, concepto.IdDocumento);            
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminanteConceptoHijo.Campos.IdDocumentoConcepto), DbType.Int32, concepto.IdDocumentoConcepto);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminanteConceptoHijo.Campos.IdDocumentoConceptoHijo), DbType.Int32, concepto.IdDocumentoConceptoHijo);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminanteConceptoHijo.Campos.IdMotivo), DbType.String, concepto.IdMotivo);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminanteConceptoHijo.Campos.IdConcepto), DbType.Int32, concepto.IdConcepto);           
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminanteConceptoHijo.Campos.CreditoSIR), DbType.Int32, concepto.CreditoSIR);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminanteConceptoHijo.Campos.ImporteDescuentos), DbType.Decimal, concepto.ImporteDescuentos);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminanteConceptoHijo.Campos.ImporteHistorico), DbType.Decimal, concepto.ImporteHistorico);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminanteConceptoHijo.Campos.ImportePagar), DbType.Decimal, concepto.ImportePagar);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminanteConceptoHijo.Campos.ImporteCondonacion), DbType.Decimal, concepto.ImporteCondonacion);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminanteConceptoHijo.Campos.ImporteParteActualizada), DbType.Decimal, concepto.ImporteParteActualizada);
            //RV
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminanteConceptoHijo.Campos.IdPeriodicidad), DbType.Decimal, concepto.IdPeriodicidad);
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminanteConceptoHijo.Campos.IdPeriodo), DbType.Decimal, concepto.IdPeriodo);



            if (concepto.FechaCausacion.HasValue)
            {
                db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminanteConceptoHijo.Campos.FechaCausacion), DbType.DateTime, concepto.FechaCausacion.Value);
            }
            else
            {
                db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminanteConceptoHijo.Campos.FechaCausacion), DbType.DateTime, SqlDateTime.Null);
            }
            if (concepto.FechaNotificacion.HasValue)
            {
                db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminanteConceptoHijo.Campos.FechaNotificacion), DbType.DateTime, concepto.FechaNotificacion.Value);
            }
            else
            {
                db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(DocumentoDeterminanteConceptoHijo.Campos.FechaNotificacion), DbType.DateTime, SqlDateTime.Null);
            }

            return cmd;
        }

        
        #endregion

        #region Implementación Dispose
        private bool _isDisposed;

        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            _isDisposed = true;
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}



