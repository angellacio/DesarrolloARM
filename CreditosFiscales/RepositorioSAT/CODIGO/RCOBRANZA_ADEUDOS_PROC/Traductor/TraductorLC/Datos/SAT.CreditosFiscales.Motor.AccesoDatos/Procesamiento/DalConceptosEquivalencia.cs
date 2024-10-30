
//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.AccesoDatos.Informacion:SAT.CreditosFiscales.Motor.AccesoDatos.Informacion.DalConceptosEquivalencia:1:12/07/2012[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SAT.CreditosFiscales.Motor.Entidades.Catalogos;
using SAT.CreditosFiscales.Motor.Entidades.DatosProcesamiento;
using SAT.CreditosFiscales.Motor.Entidades.Enumeradores;

namespace SAT.CreditosFiscales.Motor.AccesoDatos.Procesamiento
{
    /// <summary>
    /// Clase de acceso a datos durante la transformación de conceptos.
    /// </summary>
    public class DalConceptosEquivalencia
    {
        /// <summary>
        /// Método que obtiene el catálogo de coceptos de DyP.
        /// </summary>
        /// <param name="idAplicacion">Tipo de aplicativo que consulta.</param>
        /// <param name="clave">Clave de concepto origen.</param>
        /// <returns>Lista de conceptos y descripciones de DyP.</returns>
        public static List<CreditosFiscalesConcepto> ObtenerConceptoEquivalencia(int idAplicacion, string clave)
        {
            var lstConceptoDyP = new List<CreditosFiscalesConcepto>();
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("pTraductorConceptosEquivObtener");
            db.AddInParameter(dbCommand, "@pIdAplicacion", DbType.Int32, idAplicacion);
            db.AddInParameter(dbCommand, "@pConceptoOriginal", DbType.String, clave);
            using (var dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    var conceptoDyP = new CreditosFiscalesConcepto();
                    conceptoDyP.Clave = dataReader["ConceptoDyP"].ToString();
                    conceptoDyP.Descripcion = (string)dataReader["Descripcion"];
                    conceptoDyP.ClaveOrigen = clave;
                    lstConceptoDyP.Add(conceptoDyP);
                }
            }

            return lstConceptoDyP;
        }

        public static List<CreditosFiscalesConcepto> ObtenerConceptoEquivalencia()
        {
            var lstConceptoDyP = new List<CreditosFiscalesConcepto>();
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("pConsultaConceptosEquivalenciaCompleto");
            
            using (var dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    var conceptoDyP = new CreditosFiscalesConcepto();
                    conceptoDyP.Clave = dataReader["ConceptoDyP"].ToString();
                    conceptoDyP.Descripcion = (string)dataReader["Descripcion"];
                    conceptoDyP.ClaveOrigen = dataReader["ConceptoOrigen"].ToString();
                    conceptoDyP.IdAplicacion = Convert.ToInt32(dataReader["IdAplicacion"]);
                    lstConceptoDyP.Add(conceptoDyP);
                }
            }

            return lstConceptoDyP;
        }

        /// <summary>
        /// Método que obtiene el catálogo de reglas concepto.
        /// </summary>
        /// <param name="idAplicacion">Id del aplicativo para el que aplican las reglas.</param>
        /// <param name="concepto">Objeto para el cual están definididas las reglas.</param>
        /// <param name="claveOriginal">Concepto original.</param>
        /// <returns>Lista de reglas para defiirnir.</returns>
        public static List<CatReglasEquivalencia> ObtenerReglasPorConcepto(int idAplicacion, EnumTipoObjeto concepto, string claveOriginal)
        {
            var lstReglas = new List<CatReglasEquivalencia>();
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("pTraductorReglasEquivObtener");
            db.AddInParameter(dbCommand, "@pIdAplicacion", DbType.Int32, idAplicacion);
            db.AddInParameter(dbCommand, "@pIdTipoObjeto", DbType.Int16, (short)concepto);
            db.AddInParameter(dbCommand, "@pValorObjecto", DbType.String, claveOriginal);
            using (var dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    var catReglasEquivalencia = new CatReglasEquivalencia();
                    catReglasEquivalencia.IdAplicacion = (short)dataReader["IdAplicacion"];
                    catReglasEquivalencia.Descripcion = (string)dataReader["Descripcion"];
                    catReglasEquivalencia.IdTipoObjecto = Convert.ToInt32(dataReader["IdTipoObjeto"]);
                    catReglasEquivalencia.ValorObjeto = (string)dataReader["ValorObjeto"];
                    catReglasEquivalencia.ValorRetorno = (string)dataReader["ValorRetorno"];
                    catReglasEquivalencia.XPathEvaluacion = (string)dataReader["XPathEvaluacion"];
                    catReglasEquivalencia.ValorEvaluacion = (string)dataReader["ValorEvaluacion"];
                    catReglasEquivalencia.Activo = (bool)dataReader["Activo"];
                    lstReglas.Add(catReglasEquivalencia);
                }
            }
            return lstReglas;
        }

        public static List<CatReglasEquivalencia> ObtenerReglasEquivalencia()
        {
            var lstReglas = new List<CatReglasEquivalencia>();
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("pObtieneReglasEquivalencia");
  
            using (var dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    var catReglasEquivalencia = new CatReglasEquivalencia();
                    catReglasEquivalencia.IdAplicacion = (short)dataReader["IdAplicacion"];
                    catReglasEquivalencia.Descripcion = (string)dataReader["Descripcion"];
                    catReglasEquivalencia.IdTipoObjecto = Convert.ToInt32(dataReader["IdTipoObjeto"]);
                    catReglasEquivalencia.ValorObjeto = (string)dataReader["ValorObjeto"];
                    catReglasEquivalencia.ValorRetorno = (string)dataReader["ValorRetorno"];
                    catReglasEquivalencia.XPathEvaluacion = (string)dataReader["XPathEvaluacion"];
                    catReglasEquivalencia.ValorEvaluacion = (string)dataReader["ValorEvaluacion"];
                    catReglasEquivalencia.Activo = (bool)dataReader["Activo"];
                    catReglasEquivalencia.IdTipoDocumento = Convert.ToInt32(dataReader["IdTipoDocumento"]);
                    catReglasEquivalencia.Secuencia = Convert.ToInt32(dataReader["Secuencia"]);
                    catReglasEquivalencia.IdPadre = dataReader["idPadre"] == DBNull.Value ? null:(int?)dataReader["idPadre"];
                    lstReglas.Add(catReglasEquivalencia);
                }
            }
            return lstReglas;
        }

        ///// <summary>
        ///// Método que obtiene el catálogo de reglas concepto.
        ///// </summary>
        ///// <param name="idAplicacion">Id del aplicativo para el que aplican las reglas.</param>
        ///// <param name="concepto">Objeto para el cual están definididas las reglas.</param>        
        ///// <returns>Lista de reglas para defiirnir.</returns>
        //public static List<CatReglasEquivalencia> ObtenerReglasEquivalencia(int idAplicacion, EnumTipoObjeto concepto)
        //{
        //    var lstReglas = new List<CatReglasEquivalencia>();
        //    Database db = DatabaseFactory.CreateDatabase();
        //    DbCommand dbCommand = db.GetStoredProcCommand("pTraductorReglasEquivObtener");
        //    db.AddInParameter(dbCommand, "@pIdAplicacion", DbType.Int32, idAplicacion);
        //    db.AddInParameter(dbCommand, "@pIdTipoObjeto", DbType.Int16, (short)concepto);
        //    using (var dataReader = db.ExecuteReader(dbCommand))
        //    {
        //        while (dataReader.Read())
        //        {
        //            var catReglasEquivalencia = new CatReglasEquivalencia();
        //            catReglasEquivalencia.IdAplicacion = (short)dataReader["IdAplicacion"];
        //            catReglasEquivalencia.Descripcion = (string)dataReader["Descripcion"];
        //            catReglasEquivalencia.IdTipoObjecto = (int)dataReader["IdTipoObjeto"];
        //            catReglasEquivalencia.ValorObjeto = (string)dataReader["ValorObjeto"];
        //            catReglasEquivalencia.ValorRetorno = (string)dataReader["ValorRetorno"];
        //            catReglasEquivalencia.XPathEvaluacion = (string)dataReader["XPathEvaluacion"];
        //            catReglasEquivalencia.ValorEvaluacion = (string)dataReader["ValorEvaluacion"];
        //            catReglasEquivalencia.Activo = (bool)dataReader["Activo"];
        //            lstReglas.Add(catReglasEquivalencia);
        //        }
        //    }
        //    return lstReglas;
        //}

        /// <summary>
        /// Método que obtiene el concepto DyP por Id.
        /// </summary>
        /// <param name="idAplicacion">Aplicativo dado de alta.</param>
        /// <param name="idConcepto">Identificador del concepto DyP.</param>
        /// <param name="clave">Clave valor del concepto.</param>
        /// <returns>Concepto DyP.</returns>
        public static CreditosFiscalesConcepto ObtenerConceptoDyPorId(int idAplicacion, int idConcepto, string clave)
        {
            var concepto = new CreditosFiscalesConcepto();
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("pMotorConceptoDyPObtener");
            db.AddInParameter(dbCommand, "@pIdAplicacion", DbType.Int32, idAplicacion);
            db.AddInParameter(dbCommand, "@pIdCondepto", DbType.Int32, idConcepto);
            using (var dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    concepto.Clave = dataReader["ConceptoDyP"].ToString();
                    concepto.Descripcion = (string) dataReader["Descripcion"];
                    concepto.ClaveOrigen = clave;
                }
            }

            return concepto;
        }

        /// <summary>
        /// Método que obtienen la periodicidad Equivalencia.
        /// </summary>
        /// <param name="idAplicacion">Id de la aplicación.</param>
        /// <param name="periodicidadId">Id de la periodicidad.</param>
        /// <returns>Lista de periodicidades equivalencia.</returns>
        public static List<CatPeriodicidadEquivalencia> ObtenerPeriodicidadEquivalencia(int idAplicacion, int periodicidadId)
        {
            var lstPeridicidadEquivalencia = new List<CatPeriodicidadEquivalencia>();
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("pTraductorPeriodicidadObtener");
            db.AddInParameter(dbCommand, "@pIdTipoAplicacion", DbType.Int32, idAplicacion);
            db.AddInParameter(dbCommand, "@pPeriodicidadOrigen", DbType.String, periodicidadId.ToString());
            using (var dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    var periodicidad = new CatPeriodicidadEquivalencia();
                    periodicidad.IdPeriodicidadDyP = (string)dataReader["IdPeriodicidadDyP"];
                    periodicidad.DescripcionPeriodicidadDyP = (string)dataReader["Descripcion"];
                    lstPeridicidadEquivalencia.Add(periodicidad);
                }
            }

            return lstPeridicidadEquivalencia;
        }

        public static List<CatPeriodicidadEquivalencia> ObtenerPeriodicidadEquivalencia()
        {
            var lstPeridicidadEquivalencia = new List<CatPeriodicidadEquivalencia>();
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("pObtenerPeriodicidad");
            
            using (var dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    var periodicidad = new CatPeriodicidadEquivalencia();
                    periodicidad.IdPeriodicidadDyP = (string)dataReader["IdPeriodicidadDyP"];
                    periodicidad.DescripcionPeriodicidadDyP = (string)dataReader["Descripcion"];
                    periodicidad.IdAplicacion = (short)dataReader["IdAplicacion"];
                    periodicidad.PeriodicidadOrigen = (string)dataReader["PeriodicidadOrigen"];
                    lstPeridicidadEquivalencia.Add(periodicidad);
                }
            }

            return lstPeridicidadEquivalencia;
        }

        /// <summary>
        /// Obtener peridocidad equivalencia por id de periodicidad.
        /// </summary>
        /// <param name="idPeriodicidadDyp">Peridodicidad de DyP.</param>
        /// <returns>Periodicidad equivalencia.</returns>
        public static CatPeriodicidadEquivalencia ObtenerPeriodicidadEquivalencia(string idPeriodicidadDyp)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("pMotorConceptoPeriodicidadObtener");
            db.AddInParameter(dbCommand, "@pIdPeriodicidad", DbType.String, idPeriodicidadDyp);

            CatPeriodicidadEquivalencia periodicidad = null;
            using (var dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    periodicidad = new CatPeriodicidadEquivalencia();
                    periodicidad.IdPeriodicidadDyP = (string)dataReader["IdPeriodicidadDyP"];
                    periodicidad.DescripcionPeriodicidadDyP = (string)dataReader["Descripcion"];
                }
            }
            return periodicidad;
        }

        /// <summary>
        /// Obtener Periodo equivalencia.
        /// </summary>
        /// <param name="idAplicacion">Id de la aplicación que realiza la petición.</param>
        /// <param name="periodoId">Periodo id de origen.</param>
        /// <param name="periodicidadId">Periodicidad ID de origen.</param>
        /// <returns>Lista de periodos equivalencia.</returns>
        public static List<CatPeriodoEquivalencia> ObtenerPeriodoEquivalencia(int idAplicacion, int periodoId, int periodicidadId)
        {
            var lstPeriodoEquivalencia = new List<CatPeriodoEquivalencia>();
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("pMotorPeriodoEquivObtener");
            db.AddInParameter(dbCommand, "@pIdAplicacion", DbType.Int32, idAplicacion);
            db.AddInParameter(dbCommand, "@pPeriodoOrigen",DbType.String,periodoId.ToString());
            db.AddInParameter(dbCommand, "@pPeriodicidadOrigen",DbType.String,periodicidadId.ToString());
            using(var dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    var periodo = new CatPeriodoEquivalencia();
                    periodo.IdPeriodoDyP = (string) dataReader["IdPeriodoDyP"];
                    periodo.DescripcionDyP = (string) dataReader["Descripcion"];
                    lstPeriodoEquivalencia.Add(periodo);
                }
            }

            return lstPeriodoEquivalencia;
        }

        public static List<CatPeriodoEquivalencia> ObtenerPeriodoEquivalencia()
        {
            var lstPeriodoEquivalencia = new List<CatPeriodoEquivalencia>();
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("pMotorObtenerPeriodoEquivalencia");
            
            using (var dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    var periodo = new CatPeriodoEquivalencia();
                    periodo.IdPeriodoDyP = (string)dataReader["IdPeriodoDyP"];
                    periodo.DescripcionDyP = (string)dataReader["Descripcion"];
                    periodo.IdAplicacion = (short)dataReader["IdAplicacion"];
                    periodo.PeriodoOrigen = dataReader["PeriodoOrigen"].ToString();
                    periodo.PeriodicidadOrigen = Convert.ToInt32(dataReader["PeriodicidadOrigen"]);
                    lstPeriodoEquivalencia.Add(periodo);
                }
            }

            return lstPeriodoEquivalencia;
        }

        /// <summary>
        /// Periodo de equivalencia por periodo origen.
        /// </summary>
        /// <param name="idPeriodoDyP">Id del período origen.</param>
        /// <returns>El perido de equivalencia.</returns>
        public static CatPeriodoEquivalencia ObtenerPeriodoPorId(string idPeriodoDyP)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("pMotorPeriodoPorIdObtener");
            db.AddInParameter(dbCommand, "@pIdPeriodo", DbType.String, idPeriodoDyP);
            
            CatPeriodoEquivalencia periodo = null;
            using (var dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    periodo = new CatPeriodoEquivalencia();
                    periodo.IdPeriodoDyP = (string) dataReader["IdPeriodoDyP"];
                    periodo.DescripcionDyP = (string) dataReader["Descripcion"];
                }
            }
            return periodo;
        }

        /// <summary>
        /// Obtener las reglas de equivalencia por tipo de objeto.
        /// </summary>
        /// <param name="idAplicacion">Identificador de la aplicación.</param>
        /// <param name="transaccion">Tipo de objeto que se busca.</param>
        /// <param name="tipodocumento">Tipo de documento.</param>
        /// <param name="valorObjeto">Valor del objeto a buscar.</param>
        /// <returns>Lista de reglas para obtener el valor deseado.</returns>
        public static List<CatReglasEquivalencia> ObtenerReglasEquivalencia(int idAplicacion, EnumTipoObjeto transaccion, int tipodocumento, string valorObjeto)
        {
            var lstReglas = new List<CatReglasEquivalencia>();
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("pTraductorReglasEquivObtener");
            db.AddInParameter(dbCommand, "@pIdAplicacion", DbType.Int32, idAplicacion);           
            db.AddInParameter(dbCommand, "@pIdTipoObjeto", DbType.Int16, (short)transaccion);
            db.AddInParameter(dbCommand, "@pValorObjecto", DbType.String, valorObjeto);
            db.AddInParameter(dbCommand, "@pIdTipoDocumento", DbType.Int16, tipodocumento);
            using (var dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    var catReglasEquivalencia = new CatReglasEquivalencia();
                    catReglasEquivalencia.IdAplicacion = (short)dataReader["IdAplicacion"];
                    catReglasEquivalencia.Descripcion = (string)dataReader["Descripcion"];
                    catReglasEquivalencia.IdTipoObjecto = Convert.ToInt32(dataReader["IdTipoObjeto"]);
                    catReglasEquivalencia.ValorObjeto = (string)dataReader["ValorObjeto"];
                    catReglasEquivalencia.ValorRetorno = (string)dataReader["ValorRetorno"];
                    catReglasEquivalencia.XPathEvaluacion = (string)dataReader["XPathEvaluacion"];
                    catReglasEquivalencia.ValorEvaluacion = (string)dataReader["ValorEvaluacion"];
                    catReglasEquivalencia.Activo = (bool)dataReader["Activo"];
                    lstReglas.Add(catReglasEquivalencia);
                }
            }
            return lstReglas;
        }

        /// <summary>
        /// Método que obtiene las transacciones por su identificador.
        /// </summary>
        /// <param name="idTransaccion">Id de transacción a obtener.</param>
        /// <param name="idAplicacion">Id de aplicación que realiza la petición.</param>
        /// <param name="idTipodocumento">Tipo de documento.</param>
        /// <returns>Transacción que corresponde a los parámetros de búsqueda asignados.</returns>
        public static CatTransaccion ObtenerTransaccionPorId(string idTransaccion, int idAplicacion, int idTipodocumento)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("pMotorTransaccionObtener");
            db.AddInParameter(dbCommand, "@pIdTransaccion", DbType.String, idTransaccion);
            db.AddInParameter(dbCommand, "@pIdAplicacion", DbType.Int32, idAplicacion);
            db.AddInParameter(dbCommand, "@pIdTipoDocumento", DbType.Int32, idTipodocumento);

            CatTransaccion transaccion = null;
            using (var dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    transaccion = new CatTransaccion();
                    transaccion.IdTransaccion = dataReader["CveTransaccion"].ToString();
                    transaccion.Descripcion = (string)dataReader["Descripcion"];
                    transaccion.TipoTransaccion = (string) dataReader["TipoTransaccion"];
                }
            }
            return transaccion;
        }

        /// <summary>
        /// Método que obtiene las transaccciones requeridas por documento.
        /// </summary>
        /// <param name="idAplicacion">Identificador de la aplicación que realiza la petición.</param>
        /// <param name="idDocumento">Identificador de documento.</param>
        /// <returns>Lista de transacciónes requeridas.</returns>
        public static List<CatTransaccion> ObtenerTransaccionesRequeridasPorDocumento(int idAplicacion, sbyte idDocumento)
        {
            var listaTransacciones = new List<CatTransaccion>();
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("pMotorTransaccionesPorDocObtener");
            db.AddInParameter(dbCommand, "@pIdAplicacion", DbType.Int32, idAplicacion);
            db.AddInParameter(dbCommand, "@pIdDocumento", DbType.Int16, idDocumento);

            using (var dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    var transaccion = new CatTransaccion();
                    transaccion.IdTransaccion = (string) dataReader["CveTransaccion"];
                    transaccion.Descripcion = (string) dataReader["Descripcion"];
                    transaccion.IdTipoTransaccion = (int) dataReader["IdTipoTransaccion"];
                    transaccion.TipoTransaccion = (string) dataReader["TipoTransaccion"];
                    transaccion.IdTipoDocumento = (short) dataReader["IdTipoDocumento"];
                    transaccion.IdAplicacion = (short) dataReader["IdAplicacion"];
                    transaccion.EsRequerido = (bool) dataReader["EsObligatorio"];
                    listaTransacciones.Add(transaccion);
                }
            }

            return listaTransacciones;
        }

        /// <summary>
        /// Método que obtiene las transaccciones requeridas por documento.
        /// </summary>
        /// <returns>Lista de transacciónes de baja requeridas por SIAT.</returns>
        public static List<CatTransaccion> ObtenerCatSIATTransaccionesBaja()
        {
            var listaTransacciones = new List<CatTransaccion>();
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("pMotorCatSIATTransaccionesBajaObtener");

            using (var dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    var transaccion = new CatTransaccion();
                    transaccion.IdTransaccion = (string)dataReader["CveTransaccion"];
                    listaTransacciones.Add(transaccion);
                }
            }

            return listaTransacciones;
        }
    }



}
