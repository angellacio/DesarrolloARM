
//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.AccesoDatos.Informacion:SAT.CreditosFiscales.Motor.AccesoDatos.Informacion.DalDatosGenerales:1:12/07/2012[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAT.CreditosFiscales.Motor.Entidades.DatosProcesamiento;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace SAT.CreditosFiscales.Motor.AccesoDatos.Informacion
{     
    /// <summary>
    /// Clase de acceso a datos para obtener información de datos generales
    /// </summary>
    public class DalDatosGenerales
    {
        private const string SP_INSERTADATOSGENERALES = "pInsertaDatosGenerales";
        private const string SP_INSERTASIATDATOSGENERALES = "pInsertaSIATDatosGenerales";
        private const string SP_ACTUALIZADATOSGENERALES = "pActualizaDatosGenerales";

        /// <summary>
        /// Inserta un registro con los datos generales de un procesamiento
        /// </summary>
        /// <param name="idProcesamiento">Id procesamiento</param>
        /// <param name="IdAplicacion">Id aplicación</param>
        /// <param name="RFC">RFC</param>
        /// <param name="ALR">ALR</param>
        /// <param name="ImporteTotalPagar">Importe total a pagar</param>
        /// <param name="FechaVigencia">Fecha de vigencia</param>
        /// <param name="IdTipoDocumento">Id tipo de documento</param>
        /// <param name="TipoOperacion">Tipo de operación</param>
        /// <param name="FormaPago">Forma de pago</param>
        /// <param name="Nombre">Nombre</param>
        /// <param name="ApellidoMaterno">Apellido Materno</param>
        /// <param name="ApellidoPaterno">Apellido Paterno</param>
        /// <param name="RazonSocial">Razón Social</param>
        /// <param name="IdTipoPersona">Id tipo persona</param>
        /// <param name="DeudorPuro">Deudor puro</param>
        /// <param name="Observaciones">Observaciones</param>
        /// <returns>true si la inserción fue correcta</returns>
        public static bool InsertaDatosGenerales(Guid idProcesamiento, int IdAplicacion, 
                                                string RFC, int ALR, decimal ImporteTotalPagar, string FechaVigencia,
                                                int IdTipoDocumento, string TipoOperacion, string FormaPago,
                                                string Nombre,string ApellidoMaterno,string ApellidoPaterno,
			                                    string RazonSocial,int IdTipoPersona, int DeudorPuro, string Observaciones,
                                                string FolioRectificar)
        {
            	      
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand(SP_INSERTADATOSGENERALES);
            int registrosAfectados;

            db.AddInParameter(cmd, "@pIdProcesamiento", System.Data.DbType.Guid, idProcesamiento);
            db.AddInParameter(cmd, "@idAplicacion", System.Data.DbType.Int16, IdAplicacion);
            db.AddInParameter(cmd, "@RFC", System.Data.DbType.String, RFC);
            db.AddInParameter(cmd, "@ClaveAlr", System.Data.DbType.Int16,ALR);
            db.AddInParameter(cmd, "@ImporteTotalPagar", System.Data.DbType.Decimal, ImporteTotalPagar);
            db.AddInParameter(cmd, "@FechaVigencia", System.Data.DbType.String, FechaVigencia == null ? DBNull.Value : (object)FechaVigencia);
            db.AddInParameter(cmd, "@IdTipoDocumento", System.Data.DbType.Int16, IdTipoDocumento);
            db.AddInParameter(cmd, "@TipoOperacion", System.Data.DbType.String, TipoOperacion);
            db.AddInParameter(cmd, "@FormaPago", System.Data.DbType.String, FormaPago);
            db.AddInParameter(cmd, "@Nombre", System.Data.DbType.String, Nombre);
            db.AddInParameter(cmd, "@ApellidoMaterno", System.Data.DbType.String, ApellidoMaterno);
            db.AddInParameter(cmd, "@ApellidoPaterno", System.Data.DbType.String, ApellidoPaterno);
            db.AddInParameter(cmd, "@IdTipoPersona", System.Data.DbType.Int16, IdTipoPersona);
            db.AddInParameter(cmd, "@RazonSocial", System.Data.DbType.String, RazonSocial == null ? DBNull.Value : (object)RazonSocial);
            db.AddInParameter(cmd, "@DeudorPuro", System.Data.DbType.Int32, DeudorPuro);
            db.AddInParameter(cmd, "@Observaciones", System.Data.DbType.String, Observaciones == null ? DBNull.Value : (object)Observaciones);
            db.AddInParameter(cmd, "@FolioRectificar", System.Data.DbType.String, FolioRectificar == null ? DBNull.Value : (object)FolioRectificar);

            registrosAfectados = Convert.ToInt32(db.ExecuteNonQuery(cmd));

            return registrosAfectados > 0;
        }

        /// <summary>
        /// Inserta un registro con los datos generales de un procesamiento
        /// </summary>
        /// <param name="idProcesamiento">Id procesamiento</param>
        /// <param name="IdPersona">Id de persona </param>
        /// <param name="IdConvenio">Id de convenio</param>
        /// <param name="TipoConvenio">Tipo de convenio</param>
        /// <param name="NumeroConvenio">Numero de convenio</param>
        /// <param name="ImporteSaldoInsolutoInicial">Saldo insoluto inicial</param>
        /// <param name="ImporteSaldoInsoluto">Saldo insoluto</param>
        /// <param name="TotalDeParcialidades">Total de parcialidades</param>
        /// <param name="MuestraTotalDeParcialidades">Mostrar el numero de parcialidades</param>
        /// <param name="Separador">Separador</param>
        /// <returns>true si la inserción fue correcta</returns>
        public static bool InsertaSIATDatosGenerales(Guid idProcesamiento, CreditosFiscalesSIATDatosGenerales DatosGenerales)
        {
            if (DatosGenerales.Convenio == null)
            {
                DatosGenerales.Convenio = new CreditosFiscalesSIATDatosGeneralesConvenio();
            }
            if (DatosGenerales.Convenio.Parcialidad == null)
            {
                DatosGenerales.Convenio.Parcialidad = new CreditosFiscalesSIATDatosGeneralesConvenioParcialidad();
            }
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand(SP_INSERTASIATDATOSGENERALES);
            int registrosAfectados;

            db.AddInParameter(cmd, "@pIdProcesamiento", System.Data.DbType.Guid, idProcesamiento);
            db.AddInParameter(cmd, "@pIdPersona", System.Data.DbType.String, DatosGenerales.IdPersona);
            db.AddInParameter(cmd, "@pIdConvenio", System.Data.DbType.String,DatosGenerales.Convenio.IdConvenio);
            db.AddInParameter(cmd, "@pTipoConvenio", System.Data.DbType.Int16, DatosGenerales.Convenio.TipoConvenio);
            db.AddInParameter(cmd, "@pNumeroConvenio", System.Data.DbType.String, DatosGenerales.Convenio.NumeroConvenio);
            db.AddInParameter(cmd, "@pImporteSaldoInsolutoInicial", System.Data.DbType.Decimal, DatosGenerales.Convenio.ImporteSaldoInsolutoInicial);
            db.AddInParameter(cmd, "@pImporteSaldoInsoluto", System.Data.DbType.Decimal, DatosGenerales.Convenio.ImporteSaldoInsoluto);
            db.AddInParameter(cmd, "@pNumeroParcialidad", System.Data.DbType.Int16, DatosGenerales.Convenio.Parcialidad.NumeroParcialidad);
            db.AddInParameter(cmd, "@pTotalDeParcialidades", System.Data.DbType.Int16,DatosGenerales.Convenio.Parcialidad.TotalDeParcialidades);
            db.AddInParameter(cmd, "@pMuestraTotalDeParcialidades", System.Data.DbType.Boolean, DatosGenerales.Convenio.Parcialidad.MuestraTotalDeParcialidades);
            db.AddInParameter(cmd, "@pSeparador", System.Data.DbType.String, DatosGenerales.Convenio.Parcialidad.Separador);
            db.AddInParameter(cmd, "@pDescripcionParcialidad ", System.Data.DbType.String, DatosGenerales.Convenio.Parcialidad.DescripcionParcialidad);
            db.AddInParameter(cmd, "@pIdTipoLinea", System.Data.DbType.Int16, DatosGenerales.IdTipoLinea);
            registrosAfectados = Convert.ToInt32(db.ExecuteNonQuery(cmd));

            return registrosAfectados > 0;
        }

        /// <summary>
        /// Actualiza el registro de datos generales
        /// </summary>
        /// <param name="IdProcesamiento">Id de procesamiento</param>
        /// <param name="FolioDyP">Folio para DyP</param>
        /// <param name="LineaCaptura">Línea de captura</param>
        /// <param name="Exito">true si todo el procesamiento fue correcto</param>
        /// <returns>true si la actualización fue correcta.</returns>
        public static bool ActualizaDatosGenerales(Guid IdProcesamiento, string FolioDyP, string LineaCaptura, bool Exito, decimal TiempoServicio)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand(SP_ACTUALIZADATOSGENERALES);
            int registrosAfectados;

            db.AddInParameter(cmd, "@IdProcesamiento", System.Data.DbType.Guid, IdProcesamiento);
            db.AddInParameter(cmd, "@FolioDyP", System.Data.DbType.String, FolioDyP == string.Empty ? DBNull.Value : (object)FolioDyP);
            db.AddInParameter(cmd, "@LineaCaptura", System.Data.DbType.String, LineaCaptura == string.Empty ? DBNull.Value : (object)LineaCaptura);
            db.AddInParameter(cmd, "@Exito", System.Data.DbType.Boolean, Exito);
            db.AddInParameter(cmd, "@TiempoServicio", System.Data.DbType.Decimal, TiempoServicio);

            registrosAfectados = Convert.ToInt32(db.ExecuteNonQuery(cmd));

            return registrosAfectados > 0;
        }
    }
}
