using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Configuration;

namespace ProDec_ReglaNegocio
{
    public static class ManejoErrores
    {
        private static string sLog
        {
            get
            {
                return ConfigurationManager.AppSettings["LogEvent_Log"].ToString();
            }
        }
        private static string sSource
        {
            get
            {
                return ConfigurationManager.AppSettings["LogEvent_Source"].ToString();
            }
        }
        private static int nEvento
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["LogEvent_NumEvent"].ToString());
            }
        }
        private static Boolean bolAuditoria
        {
            get
            {
                return ConfigurationManager.AppSettings["LogEvent_Audit"].ToString().ToUpper().Trim() == "SI";
            }
        }

        public static void IniciaTermina(Boolean bolInicio, Boolean bolServicio)
        {
            string sMensaje = "Termino ";

            if (bolAuditoria)
            {
                if (bolInicio)
                {
                    sMensaje = "Inicio ";
                }
                if (bolServicio)
                {
                    sMensaje = string.Format("{0} servicio.", sMensaje);
                }
                else
                {
                    sMensaje = string.Format("{0} proceso.", sMensaje);
                }
            }
            else
            {
                if (bolServicio)
                {
                    sMensaje = " **** ";
                }
                else
                {
                    sMensaje = " --- ";
                }
            }

            sMensaje = string.Format("{1:dd/MM/yyyy HH:mm:ss} - {0}", sMensaje, DateTime.Now);

            EscribeMensaje(sMensaje, EventLogEntryType.Information);
        }

        public static void Mensaje(string sMensaje)
        {
            EscribeMensaje(sMensaje, EventLogEntryType.Information);
        }
        public static void MensajeAuditoria(string sMensaje)
        {
            if (bolAuditoria) EscribeMensaje(sMensaje, EventLogEntryType.SuccessAudit);
        }
        public static void MensajeWarning(string sMensaje, ApplicationException ex)
        {
            StringBuilder error = new StringBuilder();
            if (!String.IsNullOrEmpty(sMensaje))
            {
                error.AppendLine(String.Format("Mensaje\t: {0}", sMensaje));
            }
            error.AppendLine(String.Format("{0}{1}", Environment.NewLine, ex.Message.Trim()));

            EscribeMensaje(error.ToString(), EventLogEntryType.Warning);
        }
        public static void MensajeError(string sMensaje, Exception ex)
        {
            StringBuilder error = new StringBuilder();
            error.AppendLine(String.Format("Origen\t: {0}", ex.Source));
            error.AppendLine(String.Format("Clase\t: {0}", ex.TargetSite.DeclaringType.FullName));
            error.AppendLine(String.Format("Método\t: {0}", ex.TargetSite.Name));
            if (!String.IsNullOrEmpty(sMensaje))
            {
                error.AppendLine(String.Format("Mensaje\t: {0}", sMensaje));
            }
            error.AppendLine(String.Format("{0}{1}", Environment.NewLine, ex.ToString()));

            EscribeMensaje(error.ToString(), EventLogEntryType.Error);
        }

        private static void EscribeMensaje(string sMensaje, EventLogEntryType TypeLog)
        {
            EventLog escribeLog = null;

            if (EventLog.SourceExists(sSource) == false) EventLog.CreateEventSource(sSource, sLog);
            escribeLog = new EventLog(sLog);
            escribeLog.Source = sSource;

            ((System.ComponentModel.ISupportInitialize)(escribeLog)).BeginInit();


            escribeLog.WriteEntry(sMensaje, TypeLog, nEvento);
        }
    }
}
