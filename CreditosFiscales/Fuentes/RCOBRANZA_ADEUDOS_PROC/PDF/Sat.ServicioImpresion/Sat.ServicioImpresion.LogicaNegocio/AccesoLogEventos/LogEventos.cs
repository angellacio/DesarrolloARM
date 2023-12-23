//@(#)CREDITOSFISCALES(W:71950:Sat.ServicioImpresion.LogicaNegocio.AccesoLogEventos:Sat.ServicioImpresion.LogicaNegocio.AccesoLogEventos.LogEventos:1:12/07/2013[Assembly:1.0:12/07/2013])

// Referrencias de sistema.
using System;
using System.Text;
using System.Diagnostics;
using System.Configuration;
using System.ComponentModel;
using System.Data.SqlClient;

// Referemcias personalizas.
using Sat.ServicioImpresion.Entidades.AccesoLogEventos;
using Sat.ServicioImpresion.AccesoDatos.AccesoLogEventos;

namespace Sat.ServicioImpresion.LogicaNegocio.AccesoLogEventos
{
	/// <summary>
	/// Clase para escribir entradas en el visor de eventos del sistema.
	/// </summary>
	public static class LogEventos
	{
		/// <summary>
		/// 
		/// </summary>
		private static string ApplicationName
		{
			get
			{
				string appName = ConfigurationManager.AppSettings["ApplicationName"];
				return string.IsNullOrWhiteSpace(appName) ? "SAT.ServicioImpresion.Impresion" : appName;
			}
		}

		/// <summary>
		/// 
		/// </summary>
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

		/// <summary>
		/// 
		/// </summary>
		public static int MaxMessageSize
		{
			get
			{
				return 20000;
			}
		}

		#region Public
			/// <summary>
			/// Registra un evento en el Log
			/// </summary>
			/// <param name="eventoID">Identificador del evento</param>
			/// <param name="mensaje">Mensaje</param>
			/// <param name="eventoLogTipo">Tipo de evento<see cref="EventLogEntryType"/></param>
			/// <param name="aplicacion">Nombre de la aplicación</param>
			/// <param name="xml">Archivo xml de entrada</param>
			public static string EscribirEntradaLog(int eventoID, string mensaje, EventLogEntryType eventoLogTipo, string aplicacion, string xml = "")
			{
				try
				{
					var dal = new DalLogEventos();
					switch (eventoLogTipo)
					{
						case EventLogEntryType.Error:
							return dal.GuardaEvento(aplicacion, mensaje, eventoID, EnumTipoEvento.Error, xml);
						case EventLogEntryType.Warning:
							return dal.GuardaEvento(aplicacion, mensaje, eventoID, EnumTipoEvento.Alerta, xml);
						default:
							return dal.GuardaEvento(aplicacion, mensaje, eventoID, EnumTipoEvento.Informacion, xml);
					}
				}
				catch (Exception ex)
				{
					try
					{
						// Guardamos el error del SP
						EventLog.WriteEntry(aplicacion, FormatearException(ex), EventLogEntryType.Error, eventoID);
						// Guardamos el error generado por la aplicación
						EventLog.WriteEntry(aplicacion, mensaje, eventoLogTipo, eventoID);
						//Regresamos un ticket vacio porque no se podrá consultar en BD
						return string.Empty;
					}
					catch (InvalidOperationException ivEx)
					{
						//No se ha podido abrir la clave del Registro para el registro de eventos.
						throw new Exception("No se ha podido abrir la clave del Registro para el registro de eventos.", ivEx);
					}
					catch (Win32Exception winEx)
					{
						//El sistema operativo ha notificado un error al escribir la entrada de evento en el Registro
						throw new Exception("El sistema operativo ha notificado un error al escribir la entrada de evento en el Registro", winEx);
					}
				}
			}

			/// <summary>
			/// Registra un evento de tipo Error en el Log
			/// </summary>
			/// <param name="eventoID">Identificador del evento</param>
			/// <param name="mensaje">Mensaje</param>
			/// <param name="excepcion">Excepcion</param>
			/// <param name="xml">Archivo xml de entrada</param>
			public static string EscribirEntradaLog(int eventoID, string mensaje, Exception excepcion, string xml = "")
			{
				mensaje = mensaje + " : " + FormatearException(excepcion);
				mensaje = mensaje.Length > MaxMessageSize ? mensaje.Remove(MaxMessageSize) : mensaje;
				return EscribirEntrada(eventoID, mensaje, EventLogEntryType.Error, xml);
			}

			/// <summary>
			/// Registra un evento de tipo Error en el Log
			/// </summary>
			/// <param name="eventoID">Identificador del evento</param>
			/// <param name="mensaje">Mensaje</param>
			/// <param name="excepcion">Excepcion</param>
			/// <param name="xml">Archivo xml de entrada</param>
			public static string EscribirEntradaLog(int eventoID, string mensaje, SqlException excepcion, string xml = "")
			{
				mensaje = mensaje + " : " + FormatearException(excepcion);
				mensaje = mensaje.Length > MaxMessageSize ? mensaje.Remove(MaxMessageSize) : mensaje;
				return EscribirEntrada(eventoID, mensaje, EventLogEntryType.Error, xml);
			}

			/// <summary>
			/// Registra un evento de tipo Error en el Log
			/// </summary>
			/// <param name="eventoID">Identificador del evento</param>
			/// <param name="excepcion">Excepcion</param>
			/// <param name="xml">Archivo xml de entrada</param>
			public static string EscribirEntradaLog(int eventoID, Exception excepcion, string xml = "")
			{
				return EscribirEntrada(eventoID, FormatearException(excepcion), EventLogEntryType.Error, xml);
			}

			/// <summary>
			/// Registra un evento de tipo Error en el Log
			/// </summary>
			/// <param name="eventoID">Identificador del evento</param>
			/// <param name="excepcion">Excepcion</param>
			/// <param name="xml">Archivo xml de entrada</param>
			public static string EscribirEntradaLog(int eventoID, SqlException excepcion, string xml = "")
			{
				return EscribirEntrada(eventoID, FormatearException(excepcion), EventLogEntryType.Error, xml);
			}

			/// <summary>
			/// Registra un evento en el Log
			/// </summary>
			/// <param name="eventoID">Identificador del evento</param>
			/// <param name="mensaje">Mensaje</param>
			/// <param name="eventoTipo">Tipo de Evento <see cref="EventLogEntryType"/> </param>
			/// <param name="xml">Archivo xml de entrada</param>
			public static string EscribirEntradaLog(int eventoID, string mensaje, EventLogEntryType eventoTipo, string xml = "")
			{
				string ticket = string.Empty;
				switch (eventoTipo)
				{

					case EventLogEntryType.Warning:
						if (LogLevel > 1)
							ticket = EscribirEntrada(eventoID, mensaje, eventoTipo, xml);

						break;
					case EventLogEntryType.Information:
						if (LogLevel > 2)
							ticket = EscribirEntrada(eventoID, mensaje, EventLogEntryType.Information, xml);

						break;
					default:
						ticket = EscribirEntrada(eventoID, mensaje, eventoTipo, xml);
						break;
				}
				return ticket;
			}
		#endregion

		/// <summary>
		/// Registra un evento en el Log
		/// </summary>
		/// <param name="eventoID">Identificador del evento</param>
		/// <param name="mensaje">Mensaje</param>
		/// <param name="eventoTipo">Tipo de Evento <see cref="EventLogEntryType"/> </param>
		/// <param name="xml">Archivo xml de entrada</param>
		private static string EscribirEntrada(int eventoID, string mensaje, EventLogEntryType eventoTipo, string xml = "")
		{
			return EscribirEntradaLog(eventoID, mensaje, eventoTipo, ApplicationName, xml);
		}

		/// <summary>
		/// Formatea una excepción de la forma que será publicada
		/// </summary>
		/// <param name="e">Excepción a ser formateada</param>
		/// <returns>Mensaje formateado</returns>
		private static string FormatearException(Exception e)
		{
			var sb = new StringBuilder();
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