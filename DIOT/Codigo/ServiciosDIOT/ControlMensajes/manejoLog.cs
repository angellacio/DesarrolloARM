using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Diagnostics;

namespace ControlMensajes
{
    public class ManejoLog
    {
        private string sLog { get { return ConfigurationManager.AppSettings["RegLog"].ToString().ToUpper().Trim(); } }
        private string sSource { get; set; }
        private int nEvento { get; set; }
        private Boolean bolAuditoria { get { return ConfigurationManager.AppSettings["RegLogAudit"].ToString().ToUpper().Trim() == "SI"; } }
        private EventLog escribeLog { get; set; }

        public ManejoLog(string nomSource, int numEvento)
        {
            sSource = nomSource;
            nEvento = numEvento;

            if (EventLog.SourceExists(sSource) == false)
            {
                EventSourceCreationData sCrear = new EventSourceCreationData(sSource, sLog);
                EventLog.CreateEventSource(sCrear);
            }
            escribeLog = new EventLog(sLog);
            escribeLog.Source = sSource;

            ((System.ComponentModel.ISupportInitialize)(escribeLog)).BeginInit();
        }

        public void Mensaje(string sMensaje)
        {
            EscribeMensaje(sMensaje, EventLogEntryType.Information);
        }
        public void MensajeAuditoria(string sMensaje)
        {
            if (bolAuditoria)
                EscribeMensaje(sMensaje, EventLogEntryType.SuccessAudit);
        }
        public void MensajeWarning(string sMensaje)
        {
            EscribeMensaje(sMensaje, EventLogEntryType.Warning);
        }
        public void MensajeError(string sMensaje)
        {
            EscribeMensaje(sMensaje, EventLogEntryType.Error);
        }

        private void EscribeMensaje(string sMensaje, EventLogEntryType TypeLog)
        {
            //if (escribeLog == null) CreaComponentes();
            escribeLog.WriteEntry(sMensaje, TypeLog, nEvento);
        }
    }
}
