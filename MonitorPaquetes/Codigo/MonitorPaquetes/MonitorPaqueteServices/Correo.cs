using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MonitorPaqueteServices
{
    class Correo
    {
        public string Archivo_Notificacion = string.Empty;
        public string Motivo_Notificacion = string.Empty;

        public void Enviar(string correo,string paquete,int origen,string destinatario,string asunto,string motivo)
        {
            Datos Configuracion = new Datos();
            Motivo_Notificacion = motivo;
            
            switch (Convert.ToInt32(Configuracion.Parametros("TIPO_ENVIO")))
            {
                case 1: // Envio por Outlook Cuenta Local (Requiere permisos de Administrador para el Outlook)
                    break;
                case 2: // Envio por Gmail 
                    break;
                case 3: // Envio por Macro Outlook C:\Paquetes_Monitor_Macro_Outlook\*.txt
                    Envio_Macro(correo, paquete, origen, destinatario,asunto);
                    break;
                case 4: //Envio Prueba Ruta RUTA_PRUEBA
                    Prueba_Correo(correo, paquete, origen, destinatario,asunto);
                    break;
            }
        }

        private void Envio_Macro(string correo,string paquete, int origen, string destinatario,string asunto)
        {
            Datos Macro = new Datos();

            string Correo_To = destinatario;
            string Correo_From = Macro.Parametros("CORREO_ALTERNO");
            string Correo_CC = Macro.Parametros("CORREOS_CO");
            string Correo_Asunto = asunto;

            string path = Macro.Parametros("RUTA_CORREOS");
            string nombre = string.Empty;

            nombre = Motivo_Notificacion + "-" + paquete + "-" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString();
            path = path.Replace("@Notificacion", nombre);

            if (origen == 1)
            {
                Correo_CC = Correo_CC + Macro.Parametros("CORREOS_PPMC");
            }
            else
            {
                Correo_CC = Correo_CC + Macro.Parametros("CORREOS_MESA");
            }

            Correo_Asunto = Correo_Asunto.Replace("@Paquete",paquete);
            Correo_Asunto = Correo_Asunto.Replace("@Fecha",DateTime.Now.ToShortDateString());

            File.WriteAllText(path, Correo_From + "|" + Correo_To + "|" + Correo_CC + "|" + Correo_Asunto + "|" + correo, Encoding.UTF8);
            
            Archivo_Notificacion = path;

            //Actualiza a Notificado
            Macro.Notificado(paquete, nombre);
        }

        private void Prueba_Correo(string correo, string paquete, int origen, string destinatario,string asunto )
        {
            Datos Prueba = new Datos();

            string Correo_To = destinatario;
            string Correo_From = Prueba.Parametros("CORREO_ALTERNO");
            string Correo_CC = Prueba.Parametros("CORREOS_CO");
            string Correo_Asunto = asunto;

            string path = Prueba.Parametros("RUTA_PRUEBA");
            string nombre = string.Empty;

            nombre = paquete+"_Prueba";
            path = path.Replace("@Prueba", nombre);

            if (origen == 1)
            {
                Correo_CC = Correo_CC + Prueba.Parametros("CORREOS_PPMC");
            }
            else
            {
                Correo_CC = Correo_CC + Prueba.Parametros("CORREOS_MESA");
            }

            Correo_Asunto = Correo_Asunto.Replace("@Paquete", paquete);
            Correo_Asunto = Correo_Asunto.Replace("@Fecha", DateTime.Now.ToShortDateString());

            File.WriteAllText(path, Correo_From + "|" + Correo_To + "|" + Correo_CC + "|" + Correo_Asunto + "|" + correo, Encoding.UTF8);
            Archivo_Notificacion = path;

            //Actualiza a Notificado
            Prueba.Notificado(paquete, nombre);
        }        
    }
}
