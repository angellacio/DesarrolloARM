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

namespace MonitorPaqueteServices
{
    class Datos
    {
        private SqlCommand cmd;
        private SqlConnection cn;
        private SqlConnection cnremota;
        private DataTable resultado;
        public string error = string.Empty;
        private string fecha = DateTime.Now.ToString("dd-MM-yyyy hh mm ss");

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
                        valor= reader.GetString(0);
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

        public DataTable consulta_monitor(string paquete)
        {
            ConexionRemota();
            resultado = new DataTable();
            SqlDataAdapter Adp = new SqlDataAdapter();

            string comando = Parametros("C_ESTADOS");
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
                Errores(ex.ToString() + "  --  -- Store Consultar_Paquete_Monitor");
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

        public void Actualiza_Estado(string paquete, int rdl, int estado, DateTime dtInicio)
        {
            ConexionLocal();
            SqlDataAdapter Adp = new SqlDataAdapter();
            cmd = new SqlCommand("Actualiza_Paquete", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@paquete", SqlDbType.NVarChar).Value = paquete;
            cmd.Parameters.Add("@rdl", SqlDbType.Int).Value = rdl;
            cmd.Parameters.Add("@estado", SqlDbType.Int).Value = estado;
            cmd.Parameters.Add("@fechainicio", SqlDbType.DateTime).Value = dtInicio;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                error = ex.Message;
                Errores(ex.ToString() + "  --  -- Store Actualiza_Paquete");
            }
            finally
            {
                cn.Close();
                cn.Dispose();
            }
        }

        public DataTable Consulta_Cerrados()
        {
            ConexionLocal();
            resultado = new DataTable();
            SqlDataAdapter Adp = new SqlDataAdapter();
            cmd = new SqlCommand("ConsultarPaquetes_Cerrados", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                Adp.SelectCommand = cmd;
                Adp.Fill(resultado);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                Errores(ex.ToString() + "  --  -- Store ConsultarPaquetes_Cerrados");
            }
            finally
            {
                resultado.Dispose();
                cn.Close();
                cn.Dispose();
            }
            return resultado;
        }

        public DataTable Consulta_IncidentesRela(string paquete)
        {
            ConexionLocal();
            resultado = new DataTable();
            SqlDataAdapter Adp = new SqlDataAdapter();
            cmd = new SqlCommand("Incidentes_Rela", cn);
            cmd.Parameters.Add("@paquete", SqlDbType.NVarChar).Value = paquete;
            cmd.CommandType = CommandType.StoredProcedure;
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

        public DataTable Consulta_PaquetesRela(string paquete)
        {
            ConexionLocal();
            resultado = new DataTable();
            SqlDataAdapter Adp = new SqlDataAdapter();
            cmd = new SqlCommand("Paquetes_Rela", cn);
            cmd.Parameters.Add("@paquete", SqlDbType.NVarChar).Value = paquete;
            cmd.CommandType = CommandType.StoredProcedure;
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

        public DataTable Consulta_Rechazados()
        {
            ConexionLocal();
            resultado = new DataTable();
            SqlDataAdapter Adp = new SqlDataAdapter();
            cmd = new SqlCommand("ConsultarPaquetes_Rechazados", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                Adp.SelectCommand = cmd;
                Adp.Fill(resultado);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                Errores(ex.ToString() + "  --  -- Store ConsultarPaquetes_Rechazados");
            }
            finally
            {
                resultado.Dispose();
                cn.Close();
                cn.Dispose();
            }
            return resultado;
        }

        public void Notificado(string paquete,string archivo)
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

        public DataTable Consulta_Reingresos()
        {
            ConexionLocal();
            resultado = new DataTable();
            SqlDataAdapter Adp = new SqlDataAdapter();
            cmd = new SqlCommand("ConsultarPaquetes_Reingreso", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                Adp.SelectCommand = cmd;
                Adp.Fill(resultado);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                Errores(ex.ToString() + "  --  -- Store ConsultarPaquetes_Reingreso");
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
            string s_enc = Environment.NewLine + "------------------------------ " + DateTime.Now.ToString() + " ------------------------------" + Environment.NewLine;
            path = path.Replace("@Log", "Log_Error_ServicioPaquetesDatos");

            StreamWriter WriteReportFile = File.AppendText(path);
            WriteReportFile.WriteLine(s_enc + error);
            WriteReportFile.Close();
            //File.WriteAllText(path, error, Encoding.UTF8);
        }
    }
}
