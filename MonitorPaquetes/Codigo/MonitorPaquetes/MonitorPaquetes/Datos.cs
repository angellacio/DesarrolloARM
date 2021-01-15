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

namespace MonitorPaquetes
{
    class Datos
    {
        private SqlCommand cmd;
        private SqlConnection cn = new SqlConnection();
        private DataTable resultado;
        public string error = string.Empty;
        private bool testcon = true;
        //public bool estado_conL = false;
        //public bool estado_conE = false;

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
                testcon = false;
                error = ex.Message;
                Errores(ex.ToString() + "  --  -- Conexion");
            }
        }

        private void ConexionM()
        {
            try
            {
                cn = new SqlConnection();
                string cadena = Parametros("BD_MONITOR");
                cn.ConnectionString = cadena;

                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
                cn.Open();

            }
            catch (Exception ex)
            {
                testcon = false;
                error = ex.Message;
                Errores(ex.ToString() + "  --  -- ConexionM");
            }
        }
        public bool TestConexion(int tipo)
        {
            switch (tipo)
            {
                case 1:
                    return testcon;
                case 2:
                    return testcon;
            } 
             
            
            return true;
        }

        public DataTable consulta()
        {
            //SqlConnection cn = 
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

        public DataTable Lista_Desa()
        {
            Conexion();
            resultado = new DataTable();
            SqlDataAdapter Adp = new SqlDataAdapter();
            cmd = new SqlCommand("Consulta_Desarrolladores", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                Adp.SelectCommand = cmd;
                Adp.Fill(resultado);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                Errores(ex.ToString() + "  --  -- Store Consulta_Desarrolladores");
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

        public bool registro(string Paquete,int Desarrollador,int PPMC)
        {
            Conexion();
            cmd = new SqlCommand("Registro_Paquete", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Paquete", SqlDbType.NVarChar).Value = Paquete;
            cmd.Parameters.Add("@Id_Desa", SqlDbType.Int).Value = Desarrollador;
            cmd.Parameters.Add("@ppmc", SqlDbType.Int).Value = PPMC;
            try
            {
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                Errores(ex.ToString() + "  --  -- Store Registro_Paquete");
                return false;
            }
        }

        public bool registro_inc(string Paquete, string Incidente)
        {
            Conexion();
            cmd = new SqlCommand("Registra_Incidentes", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Paquete", SqlDbType.NVarChar).Value = Paquete;
            cmd.Parameters.Add("@Incidente", SqlDbType.NVarChar).Value = Incidente;
            try
            {
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                Errores(ex.ToString() + "  --  -- Store Registra_Incidentes");
                return false;
            }
        }

        public bool borrar_paq(string Paquete)
        {
            Conexion();
            cmd = new SqlCommand("Borrar_Paquete", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@paquete", SqlDbType.NVarChar).Value = Paquete;
            try
            {
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                Errores(ex.ToString() + "  --  -- Store Borrar_Paquete");
                return false;
            }
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

        public DataTable rep_paq_cer(int mes)
        {
            Conexion();
            resultado = new DataTable();
            SqlDataAdapter Adp = new SqlDataAdapter();
            cmd = new SqlCommand("ReportePaq_Cerrados", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Mes", SqlDbType.Int).Value = mes;
            try
            {
                Adp.SelectCommand = cmd;
                Adp.Fill(resultado);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                Errores(ex.ToString() + "  --  -- Store ReportePaq_Cerrados");
            }
            finally
            {
                resultado.Dispose();
                cn.Close();
                cn.Dispose();
            }
            return resultado;
        }

        public DataTable rep_paq_auditoria(string fecha)
        {
            Conexion();
            resultado = new DataTable();
            SqlDataAdapter Adp = new SqlDataAdapter();
            cmd = new SqlCommand("ReportePaq_Auditoria", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FechaAuditoria", SqlDbType.DateTime).Value = fecha;
            try
            {
                Adp.SelectCommand = cmd;
                Adp.Fill(resultado);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                Errores(ex.ToString() + "  --  -- Store ReportePaq_Cerrados");
            }
            finally
            {
                resultado.Dispose();
                cn.Close();
                cn.Dispose();
            }
            return resultado;
        }

        public DataTable rep_paq_rec(int mes)
        {
            Conexion();
            resultado = new DataTable();
            SqlDataAdapter Adp = new SqlDataAdapter();
            cmd = new SqlCommand("ReportePaq_Rechazados", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Mes", SqlDbType.Int).Value = mes;
            try
            {
                Adp.SelectCommand = cmd;
                Adp.Fill(resultado);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                Errores(ex.ToString() + "  --  -- Store ReportePaq_Rechazados");
            }
            finally
            {
                resultado.Dispose();
                cn.Close();
                cn.Dispose();
            }
            return resultado;
        }

        public DataTable rep_paq_mes(int mes)
        {
            Conexion();
            resultado = new DataTable();
            SqlDataAdapter Adp = new SqlDataAdapter();
            cmd = new SqlCommand("ReportePaq_Mensual", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Mes", SqlDbType.Int).Value = mes;
            try
            {
                Adp.SelectCommand = cmd;
                Adp.Fill(resultado);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                Errores(ex.ToString() + "  --  -- Store ReportePaq_Mensual");
            }
            finally
            {
                resultado.Dispose();
                cn.Close();
                cn.Dispose();
            }
            return resultado;
        }

        public DataTable rep_edo_monitor()
        {
            Conexion();
            resultado = new DataTable();
            SqlDataAdapter Adp = new SqlDataAdapter();
            cmd = new SqlCommand("Reporte_Estados", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                Adp.SelectCommand = cmd;
                Adp.Fill(resultado);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                Errores(ex.ToString() + "  --  -- Store Reporte_Estados");
            }
            finally
            {
                resultado.Dispose();
                cn.Close();
                cn.Dispose();
            }
            return resultado;
        }

        public DataTable rep_desas()
        {
            Conexion();
            resultado = new DataTable();
            SqlDataAdapter Adp = new SqlDataAdapter();
            cmd = new SqlCommand("Reporte_Desarrolladores", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                Adp.SelectCommand = cmd;
                Adp.Fill(resultado);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                Errores(ex.ToString() + "  --  -- Store Reporte_Desarrolladores");
            }
            finally
            {
                resultado.Dispose();
                cn.Close();
                cn.Dispose();
            }
            return resultado;
        }

        public bool registro_edo(int id, string descripcion, string area)
        {
            Conexion();
            cmd = new SqlCommand("Registra_Estado", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            cmd.Parameters.Add("@descripcion", SqlDbType.NVarChar).Value = descripcion;
            cmd.Parameters.Add("@area", SqlDbType.NVarChar).Value = area;
            try
            {
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                Errores(ex.ToString() + "  --  -- Store Registra_Estado");
                return false;
            }
        }

        public bool registro_desa(string nombre, string correo)
        {
            Conexion();
            cmd = new SqlCommand("Registra_Desarrollador", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@nombre", SqlDbType.NVarChar).Value = nombre;
            cmd.Parameters.Add("@correo", SqlDbType.NVarChar).Value = correo;
            try
            {
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                Errores(ex.ToString() + "  --  -- Store Registra_Desarrollador");
                return false;
            }
        }

        public bool actualiza_desa(string paquete, int IdDesa)
        {
            Conexion();
            cmd = new SqlCommand("Actualiza_PaqueteDesa", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@paquete", SqlDbType.NVarChar).Value = paquete;
            cmd.Parameters.Add("@idDesa", SqlDbType.Int).Value = IdDesa;
            try
            {
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                Errores(ex.ToString() + "  --  -- Store Actualiza_PaqueteDesa");
                return false;
            }
        }
        public bool actualiza_desa(string incidente, string paquete, Boolean retroalimentado, Boolean DOC_PRU, string identificador, string observaciones)
        {
            Conexion();
            cmd = new SqlCommand("Actualiza_Seguimiento", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Incidente", SqlDbType.NVarChar).Value = incidente;
            cmd.Parameters.Add("@Paquete", SqlDbType.NVarChar).Value = paquete;
            cmd.Parameters.Add("@Retroalimentado", SqlDbType.Bit).Value = retroalimentado;
            cmd.Parameters.Add("@DocPru", SqlDbType.Bit).Value = DOC_PRU;
            cmd.Parameters.Add("@Identificador", SqlDbType.NVarChar).Value = identificador;
            cmd.Parameters.Add("@Observaciones", SqlDbType.NVarChar).Value = observaciones;
            cmd.Parameters.Add("@NotLiberar", SqlDbType.NVarChar).Value = "|N/A|";
            try
            {
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                Errores(ex.ToString() + "  --  -- Store Actualiza_PaqueteDesa");
                return false;
            }
        }
        public bool actualiza_desa(string incidente, string paquete, string NotLibera)
        {
            Conexion();
            cmd = new SqlCommand("Actualiza_Seguimiento", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Incidente", SqlDbType.NVarChar).Value = incidente;
            cmd.Parameters.Add("@Paquete", SqlDbType.NVarChar).Value = paquete;
            cmd.Parameters.Add("@Retroalimentado", SqlDbType.Bit).Value = false;
            cmd.Parameters.Add("@DocPru", SqlDbType.Bit).Value = false;
            cmd.Parameters.Add("@Identificador", SqlDbType.NVarChar).Value = "";
            cmd.Parameters.Add("@Observaciones", SqlDbType.NVarChar).Value = "";
            cmd.Parameters.Add("@NotLiberar", SqlDbType.NVarChar).Value = NotLibera;
            try
            {
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                Errores(ex.ToString() + "  --  -- Store Actualiza_PaqueteDesa");
                return false;
            }
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

        public DataTable Consulta_Configuracion()
        {
            Conexion();
            resultado = new DataTable();
            SqlDataAdapter Adp = new SqlDataAdapter();
            cmd = new SqlCommand("Consultar_Configuracion", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                Adp.SelectCommand = cmd;
                Adp.Fill(resultado);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                Errores(ex.ToString() + "  --  -- Store Consultar_Configuracion");
            }
            finally
            {
                resultado.Dispose();
                cn.Close();
                cn.Dispose();
            }
            return resultado;
        }

        public bool registro_inc(string incidente, string estado, string fecha, string paquetes,string descripcion)
        {
            if (cn.State == ConnectionState.Closed)
            {
                string cadena = ConfigurationManager.AppSettings["Conexion"].ToString();
                cn.ConnectionString = cadena;
                cn.Open();
            }

            DateTime fecha_null = new DateTime();
            cmd = new SqlCommand("RegistroArchivo_Incidentes", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@incidente", SqlDbType.NVarChar).Value = incidente;
            cmd.Parameters.Add("@estado", SqlDbType.NVarChar).Value = estado;
            
            if (fecha == "0" || fecha == null || fecha =="")
            {
                fecha_null = Convert.ToDateTime("01/01/2000 00:00");
                cmd.Parameters.Add("@fecha", SqlDbType.DateTime).Value = fecha_null;
            }
            else
            {
                cmd.Parameters.Add("@fecha", SqlDbType.DateTime).Value = fecha;
            }

            cmd.Parameters.Add("@paquetes", SqlDbType.NVarChar).Value = paquetes;
            cmd.Parameters.Add("@descripcion", SqlDbType.NVarChar).Value = descripcion;
            try
            {
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                Errores(ex.ToString() + "  --  -- Store RegistroArchivo_Incidentes");
                return false;
            }
        }

        public void Dispose()
        {
            cn.Close();
            cmd.Dispose();
            resultado.Dispose();
            error = null;
        }



        //------------------------------------------------------------------------------------------//
        //------------------------------------------------------------------------------------------//
        //------------------------------------------------------------------------------------------//
        //------------------------------------------------------------------------------------------//

        public void Errores(string error)
        {
            string path = Parametros("RUTA_LOG");
            string s_enc = Environment.NewLine + "------------------------------ " + DateTime.Now.ToString() + " ------------------------------" + Environment.NewLine;
            path = path.Replace("@Log", "Log_Error_Monitor_Paquetes");

            StreamWriter WriteReportFile = File.AppendText(path);
            WriteReportFile.WriteLine(s_enc + error);
            WriteReportFile.Close();

            //File.WriteAllText(path, error, Encoding.UTF8);
        }

    }
}
