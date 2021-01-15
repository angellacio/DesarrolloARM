//@(#)SCADE2(W:SKDN08251CO6:SAT.DyP.Negocio.Comun.Procesos.Datos:0:16/Junio/2008[SAT.DyP.Negocio.Comun.Procesos:1.2:16/Junio/2008])
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using SAT.DyP.Util.Data;
using SAT.DyP.Util.Configuration;
using SAT.DyP.Negocio.Comun.Tipos;
using SAT.DyP.Util.Types;

namespace SAT.DyP.Negocio.Comun.Procesos.Datos
{
    /// <summary>
    /// Capa de acceso a datos para errores.
    /// </summary>
    internal class DALError
    {
        /// <summary>
        /// Regresa el mensaje de error para el IDError = iErrorCode e IDContexto = eCtxError.
        /// </summary>
        /// <param name="iErrorCode">Si el id del error no se encuentra en la BD se regresará el valor genérico que se encuentra en el ID (0).</param>
        /// <param name="eCtxError">ID del ContextoError que debe existir en la BD SCADENET.CContextos</param>
        /// <returns></returns>
        public string ObtenerMensajeError(int iErrorCode, ContextoError eCtxError)
        {
            string _result = string.Empty;
            string _sql = "SCADENET_ObtenerMensajeError";

            if (!ValidarContextoExiste(eCtxError))
                throw new SAT.DyP.Util.Types.PlatformException(string.Format("El ID de Contexto '{0}' no existe en la BD SCADENET.CContextos", eCtxError));

            IDataParameter[] _parameters ={ new SqlParameter("@IDError", SqlDbType.Int), new SqlParameter("@IDContexto", SqlDbType.TinyInt) };
            _parameters[0].Value = iErrorCode;
            _parameters[1].Value = eCtxError;

            DataAccessHelper _helper = null;
            try
            {
                _helper = new DataAccessHelper(newConfigurationConstants.DATABASE_SCADE_NET,
                                                            DataProviderType.SqlServer);
                object _contenido = _helper.ExecuteScalar(_sql, CommandType.StoredProcedure, _parameters);
                _result = _contenido != null ? _contenido.ToString() : ObtenerMensajeError(0, eCtxError);
            }
            catch (Exception ex)
            {
                throw new PlatformException(string.Format("Error al obtener mensaje de error 'IDError {0}', 'IDContexto{1}'", iErrorCode, eCtxError), ex);
            }
            return _result;
        }

        /// <summary>
        /// Valida que el Contexto de Error exista en la base de datos.
        /// </summary>
        /// <param name="iCtxError">El contexto de error a buscar.</param>
        /// <returns></returns>
        private bool ValidarContextoExiste(ContextoError eCtxError)
        {
            bool _result = false;
            string _sql = "sp_DALError_Obtiene_Contexto";
            IDataParameter[] _parameters = { new SqlParameter("@ctxError", SqlDbType.TinyInt) };
            _parameters[0].Value = eCtxError;

            DataAccessHelper _helper = new DataAccessHelper(newConfigurationConstants.DATABASE_SCADE_NET, DataProviderType.SqlServer);

            try
            {
                object _contenido = _helper.ExecuteScalar(_sql, CommandType.StoredProcedure, _parameters);
                _result = (int)_contenido > 0;
            }
            catch (Exception e)
            {
                throw new SAT.DyP.Util.Types.PlatformException(string.Format("Errro al validar existencia del contexto '{0}'",eCtxError), e);
            }
            return _result;
        }
    }
}
