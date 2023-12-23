
//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.AccesoDatos.Informacion:SAT.CreditosFiscales.Motor.AccesoDatos.Informacion.DalProcesamiento:1:12/07/2012[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data;

using Microsoft.Practices.EnterpriseLibrary.Data;

using Enumeraciones = SAT.CreditosFiscales.Motor.Entidades.Enumeraciones;

namespace SAT.CreditosFiscales.Motor.AccesoDatos.Procesamiento
{
    /// <summary>
    /// Clase de acceso a datos durante el procesamiento.
    /// </summary>
    public class DalProcesamiento
    {       
        private const string SP_OBTIENEFOLIOPORAPLICACION = "pObtenerFolioPorAplicacion";
        private const string SP_OBTIENEFECHASPROCESO = "pObtenerFechasProceso";
        private const string SP_INSERTAPROCESAMIENTO = "pInsertaProcesamiento";
        private const string SP_OBTIENEIMPORTETOTAL = "pObtenerImporteTotal";
        private const string SP_OBTIENEIMPORTESPORCONCEPTO = "pObtenerImporteTotalCargosPorConcepto"; 

        /// <summary>
        /// Genera un folio nuevo por aplicación y alr
        /// </summary>
        /// <param name="idAplicacion">Id de la aplicación</param>
        /// <param name="idALR">Id ALR</param>
        /// <returns>Folio generado</returns>
        public static string ObtieneFolioPorAplicacion(short idAplicacion, short idALR)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand(SP_OBTIENEFOLIOPORAPLICACION);
            
            db.AddInParameter(cmd, "@pIdAplicacion", System.Data.DbType.Int16, idAplicacion);
            db.AddInParameter(cmd, "@pIdALR", System.Data.DbType.Int16, idALR);            

            return db.ExecuteScalar(cmd).ToString();
        }

        /// <summary>
        /// Obtiene el importe total del procesamiento
        /// </summary>
        /// <param name="IdProcesamiento">Id procesamiento</param>
        /// <returns>importe total</returns>
        public static string ObtieneImporteTotal(Guid IdProcesamiento)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand(SP_OBTIENEIMPORTETOTAL);

            db.AddInParameter(cmd, "@pIdProcesamiento", System.Data.DbType.Guid, IdProcesamiento);            

            return db.ExecuteScalar(cmd).ToString();
        }

        /// <summary>
        /// Obtiene importes totales por concepto
        /// </summary>
        /// <param name="IdProcesamiento"></param>
        /// <param name="ClaveConcepto"></param>
        /// <param name="ClaveTransaccion"></param>
        /// <returns>Importe Total Abonos e Importe Total Cargos</returns>
        public static Dictionary<string, string> ObtieneImportesPorConcepto(Guid IdProcesamiento, int ClaveConcepto, int ClaveTransaccion)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand(SP_OBTIENEIMPORTESPORCONCEPTO);
            var DatosImporte = new Dictionary<string, string>();

            db.AddInParameter(cmd, "@pIdProcesamiento", System.Data.DbType.Guid, IdProcesamiento);
            db.AddInParameter(cmd, "@pClaveConcepto", System.Data.DbType.Int32, ClaveConcepto);
            db.AddInParameter(cmd, "@pClaveTransaccion", System.Data.DbType.Int32, ClaveTransaccion);            

            using (var rdReglas = db.ExecuteReader(cmd))
            {
                while (rdReglas.Read())
                {
                    DatosImporte.Add("ImporteTotalCargos", rdReglas["ImporteTotalCargos"].ToString());
                    DatosImporte.Add("ImporteTotalAbonos", rdReglas["ImporteTotalAbonos"].ToString());
                    DatosImporte.Add("ImportePagar", rdReglas["ImportePagar"].ToString());
                }
            }

            return DatosImporte;
        }

        /// <summary>
        /// Obtiene las fechas del proceso
        /// </summary>
        /// <param name="IdProcesamiento">Id procesamiento</param>
        /// <returns>Fecha Emisión y Fecha Recepción</returns>
        public static Dictionary<string, string> ObtieneFechasProceso(Guid IdProcesamiento)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand(SP_OBTIENEFECHASPROCESO);
            var DatosFechas = new Dictionary<string, string>();
            db.AddInParameter(cmd, "@pIdProcesamiento", System.Data.DbType.Guid, IdProcesamiento);           
            using (var rdReglas = db.ExecuteReader(cmd))
            {
                while (rdReglas.Read())
                {
                    DatosFechas.Add("FechaEmision", rdReglas["FechaEmision"].ToString());
                    DatosFechas.Add("FechaRecepcion", rdReglas["FechaRecepcion"].ToString());
                }
            }
            return DatosFechas;
        }

        /// <summary>
        /// Registra los datos del procesamiento
        /// </summary>
        /// <param name="idAplicacion">Id de la aplicación</param>
        /// <param name="idTipoDocPago">Id tipo documento</param>
        /// <param name="idProcesamiento">Id procesamiento</param>
        /// <param name="paso">Paso del proceso</param>
        /// <param name="mensaje">mensaje xml procesando</param>
        /// <param name="observaciones">observaciones del proceso</param>
        /// <param name="errores">Lista de errores generados</param>
        /// <returns>true si el registro fue exitoso</returns>
        public static bool RegistraProcesamiento(
            short idAplicacion,
            short idTipoDocPago,
            Guid idProcesamiento,
            Enumeraciones.PasosTraductor paso,
            string mensaje,
            string observaciones,
            string errores,
            decimal duracion)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand(SP_INSERTAPROCESAMIENTO);

            db.AddInParameter(cmd, "@pIdAplicacion", System.Data.DbType.Int16, idAplicacion);
            db.AddInParameter(cmd, "@pIdTipoDocPago", System.Data.DbType.Int16, idTipoDocPago);
            db.AddInParameter(cmd, "@pIdProcesamiento", System.Data.DbType.Guid, idProcesamiento);
            db.AddInParameter(cmd, "@pPaso", System.Data.DbType.Byte, (byte)paso);
            db.AddInParameter(cmd, "@pMensaje", System.Data.DbType.Xml, mensaje == null ? DBNull.Value : (object)mensaje);
            db.AddInParameter(cmd, "@pObservaciones", System.Data.DbType.String, observaciones == null ? DBNull.Value : (object)observaciones);
            db.AddInParameter(cmd, "@pErrores", System.Data.DbType.String, (errores == null? errores = string.Empty : errores).Length == 0 ? DBNull.Value : (object)errores);
            db.AddInParameter(cmd, "@pDuracion", DbType.Decimal, duracion);            
            int registrosAfectados = Convert.ToInt32(db.ExecuteNonQuery(cmd));            
            return registrosAfectados > 0;
        }
    }
}
