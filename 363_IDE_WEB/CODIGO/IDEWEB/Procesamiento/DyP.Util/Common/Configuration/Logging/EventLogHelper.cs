//@(#)SCADE2(W:SKDN08251CO6:SAT.DyP.Util.Logging:0:16/Junio/2008[SAT.DyP.Util:1.1:16/Junio/2008])
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.ComponentModel;
using SAT.DyP.Util.Types;

namespace SAT.DyP.Util.Logging
{
    public static class EventLogHelper
    {
        private static string LOG_SOURCE = "DyP";
        private static int GENERIC_EVENT_ID = 10000;

        /// <summary>
        /// Agrega una entrada al registro con valores por default
        /// </summary>
        /// <param name="message"></param>
        /// <param name="eventType"></param>
        public static void WriteEntry(string message, EventLogEntryType eventType)
        {
            WriteEntry(LOG_SOURCE, message, eventType,GENERIC_EVENT_ID);                 
        }

        /// <summary>
        /// Agrega una entrada al registro especificando el tipo de mensaje y ID de evento
        /// </summary>
        /// <param name="message"></param>
        /// <param name="eventType"></param>
        /// <param name="eventID"></param>
        public static void WriteEntry(string message,EventLogEntryType eventType, int eventID)
        {
            WriteEntry(LOG_SOURCE, message, eventType, eventID);
        }
        
        /// <summary>
        /// Escribe una entrada de Error al log con un ID de evento
        /// </summary>
        /// <param name="message"></param>
        /// <param name="eventID"></param>
        public static void WriteErrorEntry(string message,int eventID)
        {
            WriteEntry(message, EventLogEntryType.Error, eventID);
        }


        /// <summary>
        /// Escribe una entrada de Error al log con un ID de evento.
        /// </summary>
        /// <param name="e">Excepción a bajar al Event Viewer.</param>
        /// <param name="eventID"></param>
        public static void WriteErrorEntry(Exception e, int eventID)
        {
            WriteEntry(FormatException(e), EventLogEntryType.Error, eventID);
        }

        private static string FormatException(Exception e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Message: ");
            sb.AppendLine(e.Message);
            sb.Append("StackTrace: ");
            sb.AppendLine(e.StackTrace);
            sb.Append("Source: ");
            sb.AppendLine(e.Source);
            sb.AppendLine("------------------------------------------------------------------");
            if (e.InnerException != null)
            {
                sb.AppendLine("InnerException");
                sb.AppendLine(FormatException(e.InnerException));
            }

            return sb.ToString();
        }


        public static void WriteWarningEntry(string message,int eventID)
        {
            WriteEntry(message, EventLogEntryType.Warning, eventID);
        }

        public static void WriteInformationEntry(string message, int eventID)
        {
            WriteEntry(message, EventLogEntryType.Information, eventID);
        }

        public static void WriteEntry(string Source, string message, EventLogEntryType eventType,int eventID)
        {
            try
            {                               
                EventLog.WriteEntry(Source, message, eventType,eventID); 
            }
            catch (InvalidOperationException ivEx)
            {
                //No se ha podido abrir la clave del Registro para el registro de eventos.
                throw new PlatformException("No se ha podido abrir la clave del Registro para el registro de eventos.", ivEx);
            }
            catch (Win32Exception winEx)
            {
                //El sistema operativo ha notificado un error al escribir la entrada de evento en el Registro
                throw new PlatformException("El sistema operativo ha notificado un error al escribir la entrada de evento en el Registro", winEx);
            }
            
        }
    }
}
