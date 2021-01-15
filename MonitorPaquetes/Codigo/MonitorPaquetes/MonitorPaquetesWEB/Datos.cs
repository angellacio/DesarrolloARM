using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net.NetworkInformation;
using System.Configuration;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace MonitorPaquetesWEB
{
    public class Datos
    {
        private SqlCommand cmd;
        private SqlConnection cn = new SqlConnection();
        private SqlConnection cnremota;
        private DataTable resultado;
        public string error = string.Empty;

        private void Conexion()
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

        private void ConexionRemota()
        {
            try
            {
                cnremota = new SqlConnection();
                string cadena = Parametros("BD_MONITOR");
                cnremota.ConnectionString = cadena;

                if (cnremota.State == ConnectionState.Open)
                {
                    cnremota.Close();
                }
                cnremota.Open();
            }
            catch (Exception ex)
            {
                error = ex.Message;
                Errores(ex.ToString() + "  --  -- ConexionMonitor");
            }
        }


        public DataTable consulta()
        {
            Conexion();
            resultado = new DataTable();
            SqlDataAdapter Adp = new SqlDataAdapter();
            cmd = new SqlCommand("Consultar_Paquetes", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                Adp.SelectCommand = cmd;
                Adp.Fill(resultado);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                Errores(ex.ToString() + "  --  -- Store Consultar_Paquetes");
            }
            finally
            {
                resultado.Dispose();
                cn.Close();
                cn.Dispose();
            }
            return resultado;
        }

        public string Parametros(string param)
        {
            Conexion();
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

        public DataTable Consulta_Notificados()
        {
            Conexion();
            resultado = new DataTable();
            SqlDataAdapter Adp = new SqlDataAdapter();
            cmd = new SqlCommand("Consultar_PaquetesNoti", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                Adp.SelectCommand = cmd;
                Adp.Fill(resultado);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                Errores(ex.ToString() + "  --  -- Store Consultar_PaquetesNoti");
            }
            finally
            {
                resultado.Dispose();
                cn.Close();
                cn.Dispose();
            }
            return resultado;
        }

        public DataTable Det_Paquete(string Paquete)
        {
            Conexion();
            resultado = new DataTable();
            SqlDataAdapter Adp = new SqlDataAdapter();
            cmd = new SqlCommand("Detalle_Paquete", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Paquete", SqlDbType.NVarChar).Value = Paquete;
            try
            {
                Adp.SelectCommand = cmd;
                Adp.Fill(resultado);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                Errores(ex.ToString() + "  --  -- Store Detalle_Paquete");
            }
            finally
            {
                resultado.Dispose();
                cn.Close();
                cn.Dispose();
            }
            return resultado;
        }

        public DataTable Det_Paquete_INC(string Paquete)
        {
            Conexion();
            resultado = new DataTable();
            SqlDataAdapter Adp = new SqlDataAdapter();
            cmd = new SqlCommand("Incidentes_Rela", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Paquete", SqlDbType.NVarChar).Value = Paquete;
            try
            {
                Adp.SelectCommand = cmd;
                Adp.Fill(resultado);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                Errores(ex.ToString() + "  --  -- Store Incidentes_Rela");
            }
            finally
            {
                resultado.Dispose();
                cn.Close();
                cn.Dispose();
            }
            return resultado;
        }

        public DataTable Det_Paquete_RPaq(string Paquete)
        {
            Conexion();
            resultado = new DataTable();
            SqlDataAdapter Adp = new SqlDataAdapter();
            cmd = new SqlCommand("Paquetes_Rela", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Paquete", SqlDbType.NVarChar).Value = Paquete;
            try
            {
                Adp.SelectCommand = cmd;
                Adp.Fill(resultado);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                Errores(ex.ToString() + "  --  -- Store Paquetes_Rela");
            }
            finally
            {
                resultado.Dispose();
                cn.Close();
                cn.Dispose();
            }
            return resultado;
        }

        public DataTable Det_Desarrollador(string Paquete)
        {
            Conexion();
            resultado = new DataTable();
            SqlDataAdapter Adp = new SqlDataAdapter();
            cmd = new SqlCommand("Detalle_Desarrollador", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@paquete", SqlDbType.NVarChar).Value = Paquete;
            try
            {
                Adp.SelectCommand = cmd;
                Adp.Fill(resultado);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                Errores(ex.ToString() + "  --  -- Store Detalle_Desarrollador");
            }
            finally
            {
                resultado.Dispose();
                cn.Close();
                cn.Dispose();
            }
            return resultado;
        }

        public DataTable detalle_monitor(string paquete)
        {
            ConexionRemota();
            resultado = new DataTable();
            SqlDataAdapter Adp = new SqlDataAdapter();

            string comando = Parametros("C_RECHAZOS");
            comando = comando.Replace("@paquete", paquete);

            cmd = new SqlCommand(comando, cnremota);
            try
            {
                Adp.SelectCommand = cmd;
                Adp.Fill(resultado);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                Errores(ex.ToString() + "  --  -- Store Consultar_Rechazo_Monitor");
            }
            finally
            {
                resultado.Dispose();
                cn.Close();
                cn.Dispose();
                cnremota.Close();
                cnremota.Dispose();
            }
            return resultado;
        }

        public DataTable consulta_incidentes()
        {
            Conexion();
            resultado = new DataTable();
            SqlDataAdapter Adp = new SqlDataAdapter();
            cmd = new SqlCommand("Consultar_IncidentesSin", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                Adp.SelectCommand = cmd;
                Adp.Fill(resultado);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                Errores(ex.ToString() + "  --  -- Store Consultar_IncidentesSin");
            }
            finally
            {
                resultado.Dispose();
                cn.Close();
                cn.Dispose();
            }
            return resultado;
        }

        public DataTable consulta_incidentesPaquete()
        {
            Conexion();
            resultado = new DataTable();
            SqlDataAdapter Adp = new SqlDataAdapter();
            cmd = new SqlCommand("Consultar_IncidentesCon", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                Adp.SelectCommand = cmd;
                Adp.Fill(resultado);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                Errores(ex.ToString() + "  --  -- Store Consultar_IncidentesCon");
            }
            finally
            {
                resultado.Dispose();
                cn.Close();
                cn.Dispose();
            }
            return resultado;
        }

        //------------------------------------------------------------------------------------------//
        //------------------------------------------------------------------------------------------//
        //------------------------------------------------------------------------------------------//
        //------------------------------------------------------------------------------------------//

        public void Errores(string error)
        {
            string path = Parametros("RUTA_LOG");
            path = path.Replace("@Log", "Log_Error_Monitor_Paquetes");
            File.WriteAllText(path, error, Encoding.UTF8);
        }
    }
}