
//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Negocio.Comun.Procesos:DALDatoAcceso:0:21/Mayo/2008[SAT.DyP.Negocio.Comun.Procesos:1.1:21/Mayo/2008])
	

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using SAT.DyP.Util.Data;
using SAT.DyP.Util.Configuration;
using SAT.DyP.Negocio.Comun.Tipos;
using SAT.DyP.Util.Types;

namespace SAT.DyP.Negocio.Comun.Procesos.Datos
{
    

    internal class DALDatosAcceso
    {
        /// <summary>
        /// Obtiene la datos del acceso de la tabla TControlAcceso
        /// de SPED de un usuario
        /// </summary>
        public ControlAcceso ObtenerDatosUsuario(string rfc)
        {
            DataAccessHelper helper = new DataAccessHelper(newConfigurationConstants.DATABASE_SPED, DataProviderType.SqlServer);

            string sql = "SCADENET_ValidarAcceso";

            IDataParameter[] parameters ={ new SqlParameter("@usuario", SqlDbType.VarChar) };
            parameters[0].Value = rfc;
            ControlAcceso datosAcceso = null;

            try
            {
                SqlDataReader reader = (SqlDataReader)helper.ExecuteReaderStoreProcedure(sql, parameters);
                if (!reader.IsClosed)
                {
                    if (reader.Read())
                    {
                        datosAcceso = new ControlAcceso();

                        datosAcceso.RFC = reader.GetString(0);
                        datosAcceso.Llave = reader.GetString(1);
                        datosAcceso.Usuario = reader.GetString(2);
                        datosAcceso.Clave = reader.GetString(3);
                        datosAcceso.Perfil = reader.GetString(4);
                    }

                    reader.Close();
                }

                reader.Dispose();
                reader = null;
            }
            catch (Exception ex)
            {
                throw new PlatformException(string.Format("Error al validar Acceso en Modulo para usuario '{0}'", rfc), ex);
            }
            finally
            {
                if (helper != null)
                {
                    helper.CloseConnection();
                }
            }

            return datosAcceso;
        }
    }
}
