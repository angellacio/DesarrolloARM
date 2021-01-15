using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Configuration;

namespace MonitorReglaNegocios.Log
{
    public static class escribeMensaje
    {

        public enum AccionProceso { Inicio, Final };
        private static string sRutaArchivo
        {
            get
            {
                string result = "";
                try
                {
                    result = ConfigurationManager.AppSettings["Log"].ToString();
                }
                catch { }

                if (result == "") result = string.Format(@"{0}\Log\@Log.txt", Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Replace(@"file:\", ""));

                result = result.Replace("@Log", "MenError.log");
                if (!Directory.Exists(Path.GetDirectoryName(result))) Directory.CreateDirectory(Path.GetDirectoryName(result));

                return result;
            }
        }
        private static Boolean bolAuditoria
        {
            get
            {
                string sTransac = ConfigurationManager.AppSettings["Auditoria"].ToString().Trim().ToUpper();

                return sTransac == "SI";
            }
        }

        public static void mensajeStarFin_Servicio(AccionProceso aProceso)
        {
            string sMensaje = "";

            sMensaje = string.Format("****************************** {0:dd/MM/yyyy HH:mm:ss} * {1}", DateTime.Now, (aProceso == AccionProceso.Inicio ? "Inicio Servicio." : "Fin Servicio."));

            EscribeLog(sMensaje);
        }
        public static void mensajeStarFin_Proceso(AccionProceso aProceso, int nRenglones)
        {
            string sMensaje = "";

            if (nRenglones > 0)
            {
                sMensaje = string.Format("--- {0:dd/MM/yyyy HH:mm:ss} - {1}", DateTime.Now, (aProceso == AccionProceso.Inicio ? "Inicio proceso." : "Fin proceso.."));
            }
            else
            {
                if (bolAuditoria) sMensaje = string.Format("--- {0:dd/MM/yyyy HH:mm:ss} - {1}", DateTime.Now, (aProceso == AccionProceso.Inicio ? "Inicio proceso." : "Fin proceso.."));
                else sMensaje = string.Format("--- {0}", (aProceso == AccionProceso.Inicio ? "." : ".."));
            }

            EscribeLog(sMensaje);
        }

        public static void mensajeAuditoria(string sMensaje)
        {
            if (bolAuditoria)
                EscribeLog(string.Format("+++ {0:dd/MM/yyyy HH:mm:ss} + {1}", DateTime.Now, sMensaje));
        }

        public static void mensajeNormal(string sLog)
        {
            string sMensaje = "";

            sMensaje = string.Format("+ {0:dd/MM/yyyy HH:mm:ss} + {1}", DateTime.Now, sLog);

            EscribeLog(sMensaje);
        }

        public static void mensajeError(string sError)
        {
            string sMensaje = "";

            sMensaje = string.Format("------------------------------ {1:dd/MM/yyyy HH:mm:ss} ------------------------------ {0}{2}", Environment.NewLine, DateTime.Now, sError);

            EscribeLog(sMensaje);
        }

        private static void EscribeLog(string sMensaje)
        {
            StreamWriter WriteReportFile = File.AppendText(sRutaArchivo);
            WriteReportFile.WriteLine(sMensaje);
            WriteReportFile.Close();
        }
    }
}
