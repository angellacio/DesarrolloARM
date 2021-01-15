using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.Net.NetworkInformation;
using Microsoft.Win32;
using System.IO;


namespace MonitorIncidentesServices
{
    class Datos
    {
        private SqlCommand cmd;
        private SqlConnection cn;
        //private SqlConnection cnremota;
        private DataTable resultado;
        public string error = string.Empty;

        private void ConexionLocal()
        {
            try
            {
                cn = new SqlConnection();
                string cadena = ConfigurationManager.AppSettings["Conexion"].ToString();
                cn.ConnectionString = cadena;

                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
                cn.Open();
            }
            catch (Exception ex)
            {
                error = ex.Message;
                Errores(ex.ToString() + "  --  -- Conexion");
            }
        }

        public string Parametros(string param)
        {
            ConexionLocal();
            string valor = string.Empty;
            cmd = new SqlCommand("Consulta_Config", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@parametro", SqlDbType.NVarChar).Value = param;
            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        valor = reader.GetString(0);
                    }
                }
                else
                {
                    return valor;
                }
                reader.Close();
                reader.Dispose();
            }
            catch (Exception ex)
            {
                error = ex.Message;
                Errores(ex.ToString() + "  --  -- Store Consulta_Config -" + param);
            }
            finally
            {
                cn.Close();
                cn.Dispose();
            }
            return valor;
        }

        public DataTable consulta()
        {
            ConexionLocal();
            resultado = new DataTable();
            SqlDataAdapter Adp = new SqlDataAdapter();
            cmd = new SqlCommand("Notificar_Incidentes", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                Adp.SelectCommand = cmd;
                Adp.Fill(resultado);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                Errores(ex.ToString() + "  --  -- Store Notificar_Incidentes");
            }
            finally
            {
                resultado.Dispose();
                cn.Close();
                cn.Dispose();
            }
            return resultado;
        }

        public void Notificado(string paquete, string archivo)
        {
            ConexionLocal();
            SqlDataAdapter Adp = new SqlDataAdapter();
            cmd = new SqlCommand("Actualiza_Notificado", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@paquete", SqlDbType.NVarChar).Value = paquete;
            cmd.Parameters.Add("@archivo", SqlDbType.NVarChar).Value = archivo;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                error = ex.Message;
                Errores(ex.ToString() + "  --  -- Store Actualiza_Notificado");
            }
            finally
            {
                cn.Close();
                cn.Dispose();
            }
        }

        //------------------------------------------------------------------------------------------//
        //------------------------------------------------------------------------------------------//
        //------------------------------------------------------------------------------------------//
        //------------------------------------------------------------------------------------------//

        public void Errores(string error)
        {
            string path = Parametros("RUTA_LOG");
            path = path.Replace("@Log", "Log_Error_ServicioIncidentesDatos");
            File.WriteAllText(path, error, Encoding.UTF8);
        }

        public void Dispose()
        {
            resultado.Dispose();
            cn.Close();
            cn.Dispose();
            //cnremota.Close();
            //cnremota.Dispose();
            error = string.Empty;
        }
    }
}
