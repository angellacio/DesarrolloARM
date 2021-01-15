using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAT.DyP.Util.Data;
using System.Data;
using SAT.DyP.Util.Logging;
using System.Data.SqlClient;

namespace IDEWebCargaDeclaracionesDWH
{
    class AccesoDatos
    {
        private static DataAccessHelper CrearConexion()
        {
            return new DataAccessHelper(Constantes.SettingNames.BaseDatos, DataProviderType.SqlServer);
        }

        public static DatosDeclaracion ObtenerDeclaracionAProcesar()
        {
            DatosDeclaracion datosDeclaracion = null;           
            DataAccessHelper acceso = null;

            string sql = "sp_ObtenerDeclaracionAProcesarDWH";
            
            try
            {
                acceso = CrearConexion();

                using (IDataReader reader = acceso.ExecuteReaderStoreProcedure(sql,null))
                {
                    if (reader.Read())
                    {                       

                        if (reader.GetInt32(0) > 0)
                        {
                            datosDeclaracion = new DatosDeclaracion();
                            datosDeclaracion.Folio = reader.GetInt32(0);
                            datosDeclaracion.IdEntidadReceptora = reader.GetInt32(1);
                            datosDeclaracion.ArchivoFisico = reader.GetString(2);
                            datosDeclaracion.EsNormal = reader.GetBoolean(3);
                            datosDeclaracion.EsAnual = reader.GetBoolean(4);
                        }

                    }
                }
            }
            catch (Exception e)
            {
                EventLogHelper.WriteErrorEntry(Constantes.ErrorIDEWebCargaDeclaracionesDWH + e.Message, Constantes.Materia);

            }
            finally
            {
                if (acceso != null)
                    acceso.CloseConnection();
            }

            return datosDeclaracion;
        }

        public static void ActualizarControlCargaDWH(int folio, int estatus, string observaciones)
        {
            string sql = "sp_ActualizarControlCargaDWH";
            DataAccessHelper acceso = null;

            IDataParameter[] parametros = new IDataParameter[3];

            parametros[0] = new SqlParameter("@Folio", folio);
            parametros[1] = new SqlParameter("@IdEstatusDWH ", estatus);
            parametros[2] = new SqlParameter("@Observaciones ", observaciones);

            try
            {
                acceso = CrearConexion();
                acceso.ExecuteReaderStoreProcedure(sql, parametros);
            }
            catch (Exception e)
            {
                EventLogHelper.WriteErrorEntry(Constantes.ErrorIDEWebCargaDeclaracionesDWH + e.Message, Constantes.Materia);
            }
            finally
            {
                if (acceso != null)
                    acceso.CloseConnection();
            }
        }
    }
}
