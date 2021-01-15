using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Net.Mail;

namespace MonitorPaquetes
{
    class Negocio
    {
        DataTable resultado = new DataTable();
        
        private Datos inc_datos = new Datos();
        public string error = string.Empty;
        public string Archivo = string.Empty;

        public DataTable Monitoreo_Paquetes()
        {
            Datos grid_consulta = new Datos();
            return  grid_consulta.consulta();
        }

        public DataTable Datos_Coniguracion()
        {
            Datos grid_consulta_config = new Datos();
            return grid_consulta_config.Consulta_Configuracion();
        }

        public DataTable Avisos()
        {
            Datos grid_notificados = new Datos();
            return grid_notificados.Consulta_Notificados();
        }

        public bool RegistrarPaquete(string paquete, int desarrollador,string incidente,int pmmc)
        {
            bool resultado = false;
            Datos RegistroPaquete = new Datos();
            string[] l_inc = null;
            
            if(RegistroPaquete.registro(paquete, desarrollador, pmmc))
            {
                l_inc = incidente.Split(',');

                foreach (string inc in l_inc)
                {
                    RegistroPaquete.registro_inc(paquete, inc);
                }
                resultado = true;
            }

            return resultado;
        }

        public bool RegistrarPaqueteMasivo(string paquete, int desarrollador, string incidente, int pmmc, out string errores)
        {
            bool resultado = false;
            Datos RegistroPaquete = new Datos();
            string[] l_inc = null;
            string[] l_paq = null;
            errores = string.Empty;

            l_paq = paquete.Split(',');
            l_inc = incidente.Split(',');

            foreach (string paq in l_paq)
            {
                if (RegistroPaquete.registro(paq, desarrollador, pmmc))
                {
                    foreach (string inc in l_inc)
                    {
                        RegistroPaquete.registro_inc(paq, inc);
                    }
                }
                else
                {
                    errores = errores + "," + paquete;
                }
            }

            if (errores == string.Empty)
            {
                resultado = true;
            }

            return resultado;
        }


        public string[] DatosPaquete(string paquete)
        {
            Datos Detalle = new Datos();
            DataTable temp = new DataTable();
            string tmp = string.Empty;
            string[] resultado = { "", "", "", "", "", "", "", "", "", "", "", "" };

            temp = Detalle.Det_Paquete(paquete);

            foreach (DataRow dr in temp.Rows)
            {
                resultado.SetValue(dr[0].ToString(), 0);
                resultado.SetValue(dr[1].ToString(), 1);
                resultado.SetValue(dr[2].ToString(), 2);
                resultado.SetValue(dr[3].ToString(), 3);

                resultado.SetValue(dr[4].ToString(), 6);

                resultado.SetValue(dr[5].ToString(), 7);
                resultado.SetValue(dr[6].ToString(), 8);
            }
            temp.Clear();
            temp = Detalle.Det_Paquete_INC(paquete);

            foreach (DataRow dr in temp.Rows)
            {
                //tmp = tmp + dr[0].ToString() + Environment.NewLine;
                resultado.SetValue(dr[0].ToString(), 4);

                resultado.SetValue(dr[1].ToString(), 9);
                resultado.SetValue(dr[2].ToString(), 10);
                resultado.SetValue(dr[3].ToString(), 11);
            }
            //resultado.SetValue(tmp, 4);

            tmp = string.Empty;
            temp.Clear();
            temp = Detalle.Det_Paquete_RPaq(paquete);

            foreach (DataRow dr in temp.Rows)
            {
                tmp = tmp + dr[0].ToString() + Environment.NewLine;
            }
            resultado.SetValue(tmp, 5);

            return resultado;
        }

        public bool Actualiza_Paquete(string paquete,int IdDesa)
        {
            try
            {
                Datos ActPaquete = new Datos();
                if (ActPaquete.actualiza_desa(paquete, IdDesa))
                {
                    return true;
                }
                else
                {
                    error = ActPaquete.error;
                    return false;
                }

            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
        public bool Actualiza_Paquete(string incidente, string paquete, Boolean retroalimentado, Boolean DOC_PRU, string identificador, string observaciones)
        {
            try
            {
                Datos ActPaquete = new Datos();//Atualiza_Seguimiento
                if (ActPaquete.actualiza_desa(incidente, paquete, retroalimentado, DOC_PRU, identificador, observaciones))
                {
                    return true;
                }
                else
                {
                    error = ActPaquete.error;
                    return false;
                }

            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
            
        }
        public bool Actualiza_Paquete(string incidente, string paquete, string notLibera)
        {
            try
            {
                Datos ActPaquete = new Datos();//Atualiza_Seguimiento
                if (ActPaquete.actualiza_desa(incidente, paquete, notLibera))
                {
                    return true;
                }
                else
                {
                    error = ActPaquete.error;
                    return false;
                }

            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }

        }

        public DataTable Reportes(int tipo,string dato)
        {
            DataTable resultado = new DataTable();
            Datos data_reporte = new Datos();
            switch (tipo)
            {
                case 1:
                    resultado = data_reporte.rep_paq_cer(Convert.ToInt32(dato));
                    break;
                case 2:
                    resultado = data_reporte.rep_paq_rec(Convert.ToInt32(dato));
                    break;
                case 3:
                    resultado = data_reporte.rep_paq_mes(Convert.ToInt32(dato));
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    resultado = data_reporte.rep_edo_monitor();
                    break;
                case 7:
                    resultado = data_reporte.rep_desas();
                    break;
                case 8:
                    resultado = data_reporte.rep_paq_auditoria(dato);
                    break;
            }

            return resultado;
        }

        public bool ExportExcel(DataTable datos,string titulo,string mes)
        {

            try
            {
                string path = config_path(titulo, mes);
                var lines = new List<string>();

                string[] columnNames = datos.Columns.Cast<DataColumn>().
                                                  Select(column => column.ColumnName).
                                                  ToArray();

                var header = string.Join(",", columnNames);
                lines.Add(header);

                var valueLines = datos.AsEnumerable()
                                   .Select(row => string.Join(",", row.ItemArray));
                lines.AddRange(valueLines);

                File.WriteAllLines(path, lines, Encoding.UTF8);
                Archivo = path;
                return true;
            }
            catch (IOException ex)
            {
                error = ex.Message;
                return false;
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return false;
            }
        }

        public DataTable Lista_Desarrolladores()
        {
            Datos grid_consulta = new Datos();
            return grid_consulta.Lista_Desa();
        }

        public bool Alta_Estados(int id, string descripcion, string area)
        {
            try
            {
                Datos Estado = new Datos();
                if (Estado.registro_edo(id,descripcion,area))
                {
                    return true;
                }
                else
                {
                    error = Estado.error;
                    return false;
                }
                
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public bool Alta_Desarrollador(string Nombre, string Correo)
        {
            try
            {
                Datos Desarrollador = new Datos();
                if (Desarrollador.registro_desa(Nombre, Correo))
                {
                    return true;
                }
                else
                {
                    error = Desarrollador.error;
                    return false;
                }

            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        private string config_path(string titulo, string mes)
        {
            Datos Configuracion = new Datos();
            string path = string.Empty;
            path = Configuracion.Parametros("PATH_REPORTES");
            path = path.Replace("@titulo", (titulo + "_" + mes));
            return path;
        }

        public DataTable Archivo_Incidentes(string path, out int max)
        {
            int counter = 0;
            string line = string.Empty;
            DataTable resultado = new DataTable();

            resultado.Columns.Add("Numero_Incidente");
            resultado.Columns.Add("Estado");
            resultado.Columns.Add("Paquete");

            try
            {

                System.IO.StreamReader file = new System.IO.StreamReader(path);
                while ((line = file.ReadLine()) != null)
                {
                    string[] temp = line.Split('|');
                    DataRow dri = resultado.NewRow();

                    dri[0] = temp[0];
                    dri[1] = temp[0];
                    dri[2] = temp[0];
                    resultado.Rows.Add(dri);
                    counter++;
                }

                file.Close();
                max = counter;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                max = 0;
                return resultado;
            }
            return resultado;
        }

        public string[] Archivo_Incidentes_Light(string path, out int max)
        {
            int counter = 0;
            string line = string.Empty;
            List<string> lista = new List<string>();
            try
            {
                
                System.IO.StreamReader file = new System.IO.StreamReader(path);
                while ((line = file.ReadLine()) != null)
                {
                    lista.Add(line);
                    counter++;
                }

                file.Close();
                max = counter;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                max = 0;
                return lista.ToArray();
            }
            return lista.ToArray();
        }

        public bool Registra_Incidentes_light(string incidente, string estado, string fecha, string paquetes, string descripcion)
        {
            return inc_datos.registro_inc(incidente, estado, fecha, paquetes, descripcion);
        }

        public string config_errores()
        {
            Datos Configuracion = new Datos();
            string path = string.Empty;
            path = Configuracion.Parametros("RUTA_LOG");
            return path;
        }
        public bool Borrar_Paquete(string paquetes)
        {
            return inc_datos.borrar_paq(paquetes);
        }

        public void Dispose()
        {
            inc_datos.Dispose();
            resultado.Dispose();
            Archivo = null;
            error = null;
               
        }
        public string config_parametro(string parametro)
        {
            Datos Configuracion = new Datos();
            string path = string.Empty;
            path = Configuracion.Parametros(parametro);
            return path;
        }

        public void guardar_temp(string contenido,string nombre, out string path)
        {
            path = @"C:\\Temp\\" + nombre + ".html";
            File.WriteAllText(path, contenido, Encoding.UTF8);
        }

        public bool Test_Conexion(int tipo)
        {
            Datos test = new Datos();
            return test.TestConexion(tipo);
        }
    }
}