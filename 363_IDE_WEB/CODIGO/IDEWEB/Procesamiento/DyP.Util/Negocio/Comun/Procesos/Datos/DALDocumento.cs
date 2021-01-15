
//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Negocio.Comun.Procesos:DALDocumento:0:21/Mayo/2008[SAT.DyP.Negocio.Comun.Procesos:1.1:21/Mayo/2008])
	

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using SAT.DyP.Util.Data;
using SAT.DyP.Util.Configuration;
using SAT.DyP.Negocio.Comun.Tipos;
using SAT.DyP.Util.Types;

namespace SAT.DyP.Negocio.Comun.Procesos.Datos
{
    /// <summary>
    /// Capa de acceso a datos para documento
    /// </summary>
    public class DALDocumento
    {
        /// <summary>
        /// Obtiene el Id del la entidad receptora
        /// </summary>
        /// <param name="User">RFC del usuario</param>
        /// <returns></returns>
        public static int GetEntidadReceptora(string User)
        {
            EntidadReceptora _entidad = GetEntidadReceptoraYDescripcion(User);

            return _entidad.IdEntidad;
        }
        public static EntidadReceptora GetEntidadReceptoraYDescripcion(string User)
        {
            

            string _sql = @"SCADENET_ObtenerEntidadReceptora";

            IDataParameter[] _parameters ={ new SqlParameter("@Usuario", SqlDbType.VarChar) };
            _parameters[0].Value = User;

            DataAccessHelper _helper = null;

            EntidadReceptora _entidad = new EntidadReceptora(0, string.Empty);

            try
            {
                _helper = new DataAccessHelper(newConfigurationConstants.DATABASE_SPED,
                                                            DataProviderType.SqlServer);
                SqlDataReader _reader = (SqlDataReader)_helper.ExecuteSQL(_sql, CommandType.StoredProcedure, _parameters);

                if (!_reader.IsClosed)
                {
                    int identidad = 0;
                    string descripcion = string.Empty;

                    if (_reader.Read())
                    {
                        identidad = Convert.ToInt32(_reader.GetValue(0).ToString());
                        descripcion = _reader.GetString(1);
                    }

                    _entidad = new EntidadReceptora(identidad, descripcion);

                    _reader.Close();

                }
                _reader.Dispose();
                _reader = null;


            }
            catch (Exception ex)
            {
                throw new PlatformException("Error al obtener entidad receptora", ex);
            }
            finally
            {
                if (_helper != null)
                {
                    _helper.CloseConnection();
                }
            }

            return _entidad;
        }
    }
}
