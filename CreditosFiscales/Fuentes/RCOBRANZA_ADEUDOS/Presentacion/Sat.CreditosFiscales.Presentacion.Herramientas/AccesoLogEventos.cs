
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Presentacion.Herramientas:Sat.CreditosFiscales.Presentacion.Herramientas.ClienteServicioLog:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Sat.CreditosFiscales.Procesamiento.Servicios.Interfaces;

namespace Sat.CreditosFiscales.Presentacion.Herramientas
{
    public class ClienteServicioLog : System.ServiceModel.ChannelFactory<IServicioLog>
    {
        public ClienteServicioLog() : base("EndPoint_IServicioLog") { }
    }

    public static class AccesoLogEventos
    {
        private static string ApplicationName
        {
            get
            {
                string appName = ConfigurationManager.AppSettings["ApplicationName"];
                return string.IsNullOrWhiteSpace(appName) ? "SAT.CreditosFiscales.Cliente" : appName;
            }
        }

        private static int LogLevel
        {
            get
            {
                string logLevel = ConfigurationManager.AppSettings["LogLevel"];
                int level = 3;
                int.TryParse(logLevel, out level);
                return level;
            }
        }

        public static int MaxMessageSize
        {
            get
            {
                return 20000;
            }
        }

           


        #region Public

        /// <summary>
        /// Registra un evento de tipo Error en el Log
        /// </summary>
        /// <param name="eventoID">Identificador del evento</param>
        /// <param name="mensaje">Mensaje</param>
        /// <param name="excepcion">Excepcion</param>
        public static string EscribirEntradaLog(int eventoID, string mensaje, Exception excepcion)
        {
            mensaje = mensaje + " : " + FormatearException(excepcion);
            mensaje = mensaje.Length > MaxMessageSize ? mensaje.Remove(MaxMessageSize) : mensaje;
            return EscribirEntrada(eventoID, mensaje, EventLogEntryType.Error);
        }

        /// <summary>
        /// Registra un evento de tipo Error en el Log
        /// </summary>
        /// <param name="eventoID">Identificador del evento</param>
        /// <param name="excepcion">Excepcion</param>
        public static string EscribirEntradaLog(int eventoID, Exception excepcion)
        {
            return  EscribirEntrada(eventoID, FormatearException(excepcion), EventLogEntryType.Error);
        }

        /// <summary>
        /// Registra un evento en el Log
        /// </summary>
        /// <param name="eventoID">Identificador del evento</param>
        /// <param name="mensaje">Mensaje</param>
        /// <param name="eventoTipo">Tipo de Evento <see cref="EventLogEntryType"/> </param>
        public static string  EscribirEntradaLog(int eventoID, string mensaje, EventLogEntryType eventoTipo)
        {
            string entradaLog = string.Empty;
            switch (eventoTipo)
            {
                case EventLogEntryType.Warning:
                    if (LogLevel > 1)
                    {
                        entradaLog = EscribirEntrada(eventoID, mensaje, eventoTipo);
                    }
                    break;
                case EventLogEntryType.Information:
                    if (LogLevel > 2)
                    {
                        entradaLog = EscribirEntrada(eventoID, mensaje, EventLogEntryType.Information);
                    }
                    break;
                default:
                    entradaLog = EscribirEntrada(eventoID, mensaje, eventoTipo);
                    break;
            }

            return entradaLog;
        }


        #endregion
        /// <summary>
        /// Registra un evento en el Log
        /// </summary>
        /// <param name="eventoID">Identificador del evento</param>
        /// <param name="mensaje">Mensaje</param>
        /// <param name="eventoLogTipo">Tipo de evento<see cref="EventLogEntryType"/></param>
        /// <param name="aplicacion">Nombre de la aplicación</param>
        private static string EscribirEntradaLog(int eventoID, string mensaje, EventLogEntryType eventoLogTipo, string aplicacion)
        {
            try
            {
                using (ClienteServicioLog log = new ClienteServicioLog())
                {
                    return  log.CreateChannel().EscribirEntradaLog(eventoID, mensaje, eventoLogTipo, aplicacion);
                }
            }
            catch (Exception ex)
            {
                try
                {
                    // Guardamos el error provocado por el servicio
                    EventLog.WriteEntry(aplicacion, FormatearException(ex), EventLogEntryType.Error, eventoID);
                    // Guardamos el error provocado por la aplicación
                    EventLog.WriteEntry(aplicacion, mensaje, eventoLogTipo, eventoID);
                    //
                    return string.Empty;
                        
                }
                catch (InvalidOperationException ivEx)
                {
                    throw new Exception("No se ha podido abrir la clave del Registro para el registro de eventos.", ivEx);
                }
                catch (Win32Exception winEx)
                {
                    throw new Exception("El sistema operativo ha notificado un error al escribir la entrada de evento en el Registro", winEx);
                }
                finally { }
            }

        }

        private static string EscribirEntrada(int eventoID, string mensaje, EventLogEntryType eventoTipo)
        {
            return EscribirEntradaLog(eventoID, mensaje, eventoTipo, ApplicationName);
        }

        /// <summary>
        /// Formatea una excepción de la forma que será publicada
        /// </summary>
        /// <param name="e">Excepción a ser formateada</param>
        /// <returns>Mensaje formateado</returns>
        private static string FormatearException(Exception e)
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
                sb.AppendLine(FormatearException(e.InnerException));
            }

            if (sb.Length > MaxMessageSize)
                return sb.ToString().Remove(MaxMessageSize);
            else
                return sb.ToString();
        }

    }
}
