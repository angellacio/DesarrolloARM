using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MonitorIncidentesServices
{
    class Correo
    {

        public void Enviar(string correo,string asunto)
        {
            Datos Configuracion = new Datos();

            switch (Convert.ToInt32(Configuracion.Parametros("TIPO_ENVIO_INC")))
            {
                case 1: // Envio por Outlook Cuenta Local
                    break;
                case 2: // Envio por Gmail 
                    break;
                case 3: // Envio por Macro Outlook C:\Paquetes_Monitor_Macro_Outlook\*.txt
                    Envio_Macro(correo, asunto);
                    break;
                case 4: //Envio Prueba Ruta RUTA_PRUEBA
                    Prueba_Correo(correo, asunto);
                    break;
            }
        }
        private void Envio_Macro(string correo, string asunto)
        {
            Datos Macro = new Datos();

            string Correo_To = Macro.Parametros("CORREOS_INC");
            string Correo_From = Macro.Parametros("CORREO_ALTERNO");
            string Correo_Asunto = Macro.Parametros("ASUNTO_INC");
            string Correo_CC = Macro.Parametros("CORREOS_CC_INC");

            string path = Macro.Parametros("RUTA_CORREOS");
            string nombre = string.Empty;

            nombre = "Incidentes -" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString();
            path = path.Replace("@Notificacion", nombre);

            Correo_Asunto = Correo_Asunto.Replace("@Fecha", DateTime.Now.ToShortDateString());

            File.WriteAllText(path, Correo_From + "|" + Correo_To + "|" + Correo_CC + "|" + Correo_Asunto + "|" + correo, Encoding.UTF8);
            
            //Actualiza a Notificado
            Macro.Notificado("Incidentes", nombre);
        }

        private void Prueba_Correo(string correo, string asunto)
        {
            Datos Prueba_Macro = new Datos();

            string Correo_To = Prueba_Macro.Parametros("CORREOS_INC");
            string Correo_From = Prueba_Macro.Parametros("CORREO_ALTERNO");
            string Correo_Asunto = Prueba_Macro.Parametros("ASUNTO_INC");
            string Correo_CC = Prueba_Macro.Parametros("CORREOS_CC_INC");

            string path = Prueba_Macro.Parametros("RUTA_PRUEBA");
            string nombre = string.Empty;

            nombre = "Incidentes -" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString();
            path = path.Replace("@Prueba", nombre);

            Correo_Asunto = Correo_Asunto.Replace("@Fecha", DateTime.Now.ToShortDateString());

            File.WriteAllText(path, Correo_From + "|" + Correo_To + "|" + Correo_CC + "|" + Correo_Asunto + "|" + correo, Encoding.UTF8);

            //Actualiza a Notificado
            Prueba_Macro.Notificado("Incidentes", nombre);
        }
    }
}
