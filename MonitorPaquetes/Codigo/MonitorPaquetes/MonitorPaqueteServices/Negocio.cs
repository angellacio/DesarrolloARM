using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;

namespace MonitorPaqueteServices
{
    class Negocio
    {
        private string fecha = DateTime.Now.ToString("dd-MM-yyyy hh mm ss");

        public void Execute()
        {
            DataTable l_paq = new DataTable();
            Datos Paquetes = new Datos();
            try
            {
                l_paq = Paquetes.consulta();

                foreach (DataRow dr in l_paq.Rows)
                {
                    Actualiza_Paquete(dr[1].ToString());
                }

                Notificar_Cerrados();

                Notificar_Rechazados();

                Reingresos();
            }
            catch (Exception ex)
            { 
                Errores(ex.ToString()); 
            }
            finally
            {
                l_paq.Dispose();
            }
        }
        
        private void Actualiza_Paquete(string paquete)
        {
            DataTable datos_monitor = new DataTable();
            Datos Act_Paquete = new Datos();
            datos_monitor = Act_Paquete.consulta_monitor(paquete);

            foreach (DataRow dr in datos_monitor.Rows)
            {
                Act_Paquete.Actualiza_Estado(paquete, Convert.ToInt32(dr[0]), Convert.ToInt32(dr[2]), Convert.ToDateTime(dr[1]));
            }
        }
        
        private void Notificar_Cerrados()
        {
            Datos Datos_Notificacion = new Datos();
            DataTable Paquetes = new DataTable();
            string plantilla = string.Empty;
            Paquetes = Datos_Notificacion.Consulta_Cerrados();


            foreach (DataRow dr in Paquetes.Rows)
            {
                Correo Notificar_Cerrado = new Correo();
                string asunto = string.Empty;
                
                plantilla = Datos_Notificacion.Parametros("CERRADO_" + dr[2].ToString());
                plantilla = plantilla.Replace("@Paquete", dr[0].ToString());
                plantilla = plantilla.Replace("@Incidentes",Incidentes_Relacionados(dr[0].ToString()));
                plantilla = plantilla.Replace("@PaqRe", Paquetes_Relacionados(dr[0].ToString()));

                asunto = Datos_Notificacion.Parametros("ASUNTO_0");
                Notificar_Cerrado.Enviar(plantilla, dr[0].ToString(), Convert.ToInt32(dr[2].ToString()), dr[1].ToString(),asunto,"C");
            }
        }
        
        private void Notificar_Rechazados()
        {
            Datos Datos_Notificacion = new Datos();
            DataTable Paquetes = new DataTable();
            string plantilla = string.Empty;
            Paquetes = Datos_Notificacion.Consulta_Rechazados();

            foreach (DataRow dr in Paquetes.Rows)
            {
                string asunto = string.Empty;
                Correo Notificar_Cerrado = new Correo();
                
                plantilla = Datos_Notificacion.Parametros("RECHAZADO_" + dr[2].ToString());
                plantilla = plantilla.Replace("@Paquete", dr[0].ToString());
                plantilla = plantilla.Replace("@Incidentes", Incidentes_Relacionados(dr[0].ToString()));
                plantilla = plantilla.Replace("@Detalle", Detalle_Rechazo(dr[0].ToString()));

                asunto = Datos_Notificacion.Parametros("ASUNTO_1");

                Notificar_Cerrado.Enviar(plantilla, dr[0].ToString(), Convert.ToInt32(dr[2].ToString()), dr[1].ToString(),asunto,"R");
                
            }
        }

        private string Incidentes_Relacionados(string paquete)
        {
            Datos inc_rela = new Datos();
            string inc_temp = string.Empty;

            foreach (DataRow dr in inc_rela.Consulta_IncidentesRela(paquete).Rows)
            {
                inc_temp=inc_temp+dr[0].ToString()+"<BR>";
            }

            return inc_temp;
        }

        private string Paquetes_Relacionados(string paquete)
        {
            Datos paq_rela = new Datos();
            string paq_temp = string.Empty;

            foreach (DataRow dr in paq_rela.Consulta_PaquetesRela(paquete).Rows)
            {
                paq_temp = paq_temp + dr[0].ToString() + "<BR>";
            }

            if (paq_temp == string.Empty)
            {
                paq_temp = "No existe relacion con otros Paquetes";
            }
            return paq_temp;
        }

        private string Detalle_Rechazo(string paquete)
        {
            Datos det_rechazo = new Datos();
            string paq_temp = string.Empty;

            foreach (DataRow dr in det_rechazo.detalle_monitor(paquete).Rows)
            {
                paq_temp = paq_temp + dr[0].ToString() + "<BR>";
            }

            if (paq_temp == string.Empty)
            {
                paq_temp = "No se pudo obtener detalle del rechazo";
            }

            return paq_temp;
        }

        public void Errores(string error)
        {
            Datos config = new Datos();
            string path = config.Parametros("RUTA_LOG");
            string s_enc = Environment.NewLine + "------------------------------ " + DateTime.Now.ToString() + " ------------------------------" + Environment.NewLine;
            path = path.Replace("@Log", "Log_Error_ServicioPaquetes");

            StreamWriter WriteReportFile = File.AppendText(path);
            WriteReportFile.WriteLine(s_enc + error);
            WriteReportFile.Close();
            //File.WriteAllText(path, error, Encoding.UTF8);
        }

        private void Reingresos()
        {
            Datos Datos_Reingresos = new Datos();
            DataTable Paquetes_Re = new DataTable();
            Paquetes_Re = Datos_Reingresos.Consulta_Reingresos();

            foreach (DataRow dr in Paquetes_Re.Rows)
            {
                DataTable datos_monitor = new DataTable();
                Datos Act_Paquete = new Datos();
                datos_monitor = Act_Paquete.consulta_monitor(dr[1].ToString());

                foreach (DataRow drm in datos_monitor.Rows)
                {
                    //Errores("Consulta Monitor: " + Convert.ToInt32(drm[0].ToString()) + "  Consulta Local: " + Convert.ToInt32(dr[0].ToString());    
                    if (Convert.ToInt32(drm[0].ToString()) != Convert.ToInt32(dr[0].ToString()))
                    {
                        Act_Paquete.Actualiza_Estado(dr[1].ToString(), Convert.ToInt32(drm[0].ToString()), Convert.ToInt32(drm[2].ToString()), Convert.ToDateTime(drm[1].ToString()));
                    }
                }

            }
        }
    }
}
