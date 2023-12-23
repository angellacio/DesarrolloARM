
//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.AccesoDatos.Informacion:SAT.CreditosFiscales.Motor.AccesoDatos.Informacion.DalConceptos:1:12/07/2012[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace SAT.CreditosFiscales.Motor.AccesoDatos.Informacion
{
    /// <summary>
    /// Clase de acceso a datos para obtener información de conceptos
    /// </summary>
    public class DalConceptos
    {
        private const string SP_INSERTACONCEPTOPADRE = "pInsertaConceptoOrignal";
        private const string SP_INSERTACONCEPTOHIJO = "pInsertaConceptosHijo";
        private const string SP_INSERTACONCEPTODYP = "pInsertaConceptosDyP";
        private const string SP_INSERTASIATCONCEPTOORIGINAL = "pInsertaSIATConceptoOrignal";

        /// <summary>
        /// Inserta un registro con los datos correspondientes al concepto original
        /// </summary>
        /// <param name="idProcesamiento">Id del procesamiento a consultar</param>
        /// <param name="IdAgrupador">Id del agrupador</param>
        /// <param name="ConceptoOrigen">Concepto Origen</param>
        /// <param name="Ejercicio">Ejercicio</param>
        /// <param name="PerioricidadOrigen">Periodicidad</param>
        /// <param name="PeriodoOrigen">Periodo origen</param>
        /// <param name="FechaCausacion">Fecha de causación</param>
        /// <param name="CreditoSIR">número de crédito sir</param>
        /// <param name="IHistorico">Importe historico</param>
        /// <param name="IParteActualizada">Importe parte actualizada</param>
        /// <param name="IPagar">Importe a pagar</param>
        /// <param name="Liquidar">Indica si el concepto se va a liquidar</param>
        /// <param name="MotivoID">Motivo ID</param>
        /// <returns>Id del concepto padre almacenado</returns>
        public static string InsertaConceptoOriginal(Guid idProcesamiento, int IdAgrupador, string ConceptoOrigen
                                            ,int Ejercicio, string PerioricidadOrigen
                                            ,string PeriodoOrigen, string FechaCausacion, int CreditoSIR
                                            ,decimal IHistorico, decimal IParteActualizada, decimal IPagar, bool Liquidar
                                            ,string MotivoID, bool Correcto)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand(SP_INSERTACONCEPTOPADRE);
            IDataReader rdConcepto = null;
            string registrosAfectados = string.Empty;

            db.AddInParameter(cmd, "@IdAgrupador", System.Data.DbType.Int32, IdAgrupador);
            db.AddInParameter(cmd, "@IdProcesamiento", System.Data.DbType.Guid, idProcesamiento);
            db.AddInParameter(cmd, "@ConceptoOrigen", System.Data.DbType.String, ConceptoOrigen);
            db.AddInParameter(cmd, "@Ejercicio", System.Data.DbType.Int16, Ejercicio == 0 ? DBNull.Value : (object)Ejercicio);
            db.AddInParameter(cmd, "@PeriodicidadOrigen", System.Data.DbType.String, PerioricidadOrigen);
            db.AddInParameter(cmd, "@PeriodoOrigen", System.Data.DbType.String, PeriodoOrigen);
            db.AddInParameter(cmd, "@FechaCausacion", System.Data.DbType.DateTime, (FechaCausacion == null || FechaCausacion == string.Empty) ? DBNull.Value : (object)FechaCausacion);
            db.AddInParameter(cmd, "@CreditoSIR", System.Data.DbType.Int32, CreditoSIR);
            db.AddInParameter(cmd, "@ImporteHistorico", System.Data.DbType.Decimal, IHistorico);
            db.AddInParameter(cmd, "@ImporteParteActualizada", System.Data.DbType.Decimal, IParteActualizada);            
            db.AddInParameter(cmd, "@ImportePagar", System.Data.DbType.Decimal, IPagar);
            db.AddInParameter(cmd, "@Liquidar", System.Data.DbType.Boolean, Liquidar);
            db.AddInParameter(cmd, "@MotivoID", System.Data.DbType.String, MotivoID);
            db.AddInParameter(cmd, "@Correcto", DbType.Boolean, Correcto);

           
            using (rdConcepto = db.ExecuteReader(cmd))
            {
                while (rdConcepto.Read())
                {
                    registrosAfectados = rdConcepto["IdConceptoOriginal"].ToString();
                }
            }

            return registrosAfectados;
        }

        /// <summary>
        /// Inserta un registro con los datos correspondientes al concepto hijo
        /// </summary>
        /// <param name="IdProcesamiento">Id procesamiento</param>
        /// <param name="IdAgrupador">Id agrupador</param>
        /// <param name="IdConceptoOriginalPadre">Id concepto padre</param>
        /// <param name="ConceptoOriginalHijo">Id concepto hijo</param>
        /// <param name="ImporteHistorico">Importe histórico</param>
        /// <param name="ImporteParteActualizada">Importe de la parte actualizada</param>
        /// <param name="ImportePagar">Importe a pagar</param>
        /// <param name="Ejercicio">Ejercicio</param>
        /// <param name="FechaCausacion">Fecha de causación</param>
        /// <param name="PeriodicidadOrigen">Periodicidad origen</param>
        /// <param name="PeriodoOrigen">periodo origen</param>
        /// <param name="Liquidar">Indica si el concepto se va a liquidar</param>
        /// <param name="MotivoID">Motivo ID</param>
        /// <returns>Id del concepto hijo almacenado</returns>
        public static string InsertaConceptoHijo(Guid IdProcesamiento, int IdAgrupador, int IdConceptoOriginalPadre
                                               , string ConceptoOriginalHijo                                               
                                               , decimal ImporteHistorico
                                               , decimal ImporteParteActualizada
                                               , decimal ImportePagar
                                               , int Ejercicio
                                               , string FechaCausacion
                                               , int PeriodicidadOrigen
                                               , int PeriodoOrigen
                                               , bool Liquidar
                                               , string MotivoID
                                               , string TipoTransaccion
                                               , int CreditoSIR)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand(SP_INSERTACONCEPTOHIJO);
            IDataReader rdConcepto = null;
            string registrosAfectados = string.Empty;

            db.AddInParameter(cmd, "@IdProcesamiento", System.Data.DbType.Guid, IdProcesamiento);
            db.AddInParameter(cmd, "@IdAgrupador", System.Data.DbType.Int32, IdAgrupador);
            db.AddInParameter(cmd, "@IdConceptoOriginal", System.Data.DbType.Int32, IdConceptoOriginalPadre);
            db.AddInParameter(cmd, "@ConceptoOriginalHijo", System.Data.DbType.String, ConceptoOriginalHijo);            
            db.AddInParameter(cmd, "@ImporteHistorico", System.Data.DbType.Decimal, ImporteHistorico);
            db.AddInParameter(cmd, "@ImporteParteActualizada", System.Data.DbType.Decimal, ImporteParteActualizada);           
            db.AddInParameter(cmd, "@ImportePagar", System.Data.DbType.Decimal, ImportePagar);
            db.AddInParameter(cmd, "@Ejercicio", System.Data.DbType.Int32, Ejercicio == 0 ? DBNull.Value : (object)Ejercicio);
            db.AddInParameter(cmd, "@FechaCausacion", System.Data.DbType.DateTime, (FechaCausacion == null || FechaCausacion == string.Empty) ? DBNull.Value : (object)FechaCausacion);
            db.AddInParameter(cmd, "@PeriodicidadOrigen", System.Data.DbType.Int32, PeriodicidadOrigen);
            db.AddInParameter(cmd, "@PeriodoOrigen", System.Data.DbType.Int32, PeriodoOrigen);
            db.AddInParameter(cmd, "@Liquidar", System.Data.DbType.Boolean, Liquidar);
            db.AddInParameter(cmd, "@MotivoID", System.Data.DbType.String, MotivoID);
            db.AddInParameter(cmd, "@TipoTransaccion", System.Data.DbType.String, TipoTransaccion);
            db.AddInParameter(cmd, "@CreditoSIR", System.Data.DbType.Int32, CreditoSIR); 


            using (rdConcepto = db.ExecuteReader(cmd))
            {
                while (rdConcepto.Read())
                {
                    registrosAfectados = rdConcepto["IdConceptoOriginalHijo"].ToString();
                }
            }

            return registrosAfectados;            
        }

        /// <summary>
        /// Inserta un registro con los datos correspondientes al concepto DyP
        /// </summary>
        /// <param name="IdProcesamiento">Id procesamiento</param>
        /// <param name="Clave">Clave del concepto</param>
        /// <param name="Ejercicio">Ejercicio</param>
        /// <param name="ClaveOrigen">Clave origen</param>
        /// <param name="ClavePeriodicidad">Clave periodicidad</param>
        /// <param name="ClavePeriodo">Clave Periodo</param>
        /// <param name="NumeroCredito">Número del crédito</param>
        /// <param name="FechaCausacion">Fecha de causación</param>
        /// <returns>Id del concepto DyP almacenado</returns>
        public static string InsertaConceptoDyP(Guid IdProcesamiento 
                                               ,string Clave
                                               ,int Ejercicio
                                               ,string ClaveOrigen
                                               ,string ClavePeriodicidad
                                               ,string ClavePeriodo
                                               ,int NumeroCredito
                                               ,string FechaCausacion)
        {
            Database db = DatabaseFactory.CreateDatabase();
            IDataReader rdConcepto = null;
            DbCommand cmd = db.GetStoredProcCommand(SP_INSERTACONCEPTODYP);
            string registrosAfectados = string.Empty;
            
            db.AddInParameter(cmd, "@IdProcesamiento", System.Data.DbType.Guid, IdProcesamiento);
            db.AddInParameter(cmd, "@ConceptoDyP", System.Data.DbType.String, Clave);
            db.AddInParameter(cmd, "@Ejercicio", System.Data.DbType.Int32, Ejercicio == 0 ? DBNull.Value : (object)Ejercicio);
            db.AddInParameter(cmd, "@ClaveOrigen", System.Data.DbType.String, ClaveOrigen);
            db.AddInParameter(cmd, "@ClavePeriodicidad", System.Data.DbType.String, ClavePeriodicidad);
            db.AddInParameter(cmd, "@ClavePeriodo", System.Data.DbType.String, ClavePeriodo);
            db.AddInParameter(cmd, "@NumeroCredito", System.Data.DbType.Int32, NumeroCredito);
            db.AddInParameter(cmd, "@FechaCausacion", System.Data.DbType.Date, (FechaCausacion == null || FechaCausacion == string.Empty) ? DBNull.Value : (object)FechaCausacion);

            using (rdConcepto = db.ExecuteReader(cmd))
            {
                while (rdConcepto.Read())
                {
                    registrosAfectados = rdConcepto["ConceptoDyP"].ToString();
                }
            }

            return registrosAfectados;
        }

        public static string InsertaSIATConceptoOriginal(Guid IdProcesamiento, int idAgrupador, Entidades.DatosProcesamiento.CreditosFiscalesSIATAgrupadorConceptos conceptoPadre)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand(SP_INSERTASIATCONCEPTOORIGINAL);
            IDataReader rdConcepto = null;
            string registrosAfectados = string.Empty;

            db.AddInParameter(cmd, "@IdAgrupador", System.Data.DbType.Int32, idAgrupador);
            db.AddInParameter(cmd, "@IdProcesamiento", System.Data.DbType.Guid, IdProcesamiento);
            db.AddInParameter(cmd, "@IdConcepto", System.Data.DbType.String, conceptoPadre.IdConcepto);

            using (rdConcepto = db.ExecuteReader(cmd))
            {
                while (rdConcepto.Read())
                {
                    registrosAfectados = rdConcepto["IdConceptoOriginal"].ToString();
                }
            }

            return registrosAfectados;
        }
    }
}
