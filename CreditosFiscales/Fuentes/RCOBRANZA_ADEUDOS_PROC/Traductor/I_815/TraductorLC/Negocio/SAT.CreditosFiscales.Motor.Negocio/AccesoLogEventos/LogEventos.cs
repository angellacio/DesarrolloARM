﻿//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.Negocio:SAT.CreditosFiscales.Motor.Negocio.LogEventos:1:12/07/2012[Assembly:1.0:12/07/2013])

// Referencias de sistemas.
using System;
using System.Text;
using System.Diagnostics;
using System.Configuration;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Collections.Generic;

// Referencias personalizadas.
using Sat.CreditosFiscales.Motor.Entidades.AccesoLogEventos;
using SAT.CreditosFiscales.Motor.AccesoDatos.AccesoLogEventos;

namespace SAT.CreditosFiscales.Motor.Negocio.AccesoLogEventos
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
				return string.IsNullOrWhiteSpace(appName) ? "SAT.CreditosFiscales.Servicios" : appName;
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
			public static string EscribirEntradaLog(int eventoID, string mensaje, EventLogEntryType eventoLogTipo, string aplicacion)
			{
				try
				{
					var dal = new DalLogEventos();
					switch (eventoLogTipo)
					{
						case EventLogEntryType.Error:
							return dal.GuardaEvento(aplicacion, mensaje, eventoID, EnumTipoEvento.Error);
						case EventLogEntryType.Warning:
							return dal.GuardaEvento(aplicacion, mensaje, eventoID, EnumTipoEvento.Alerta);
						default:
							return dal.GuardaEvento(aplicacion, mensaje, eventoID, EnumTipoEvento.Informacion);
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
						// No se ha podido abrir la clave del Registro para el registro de eventos.
						throw new Exception("No se ha podido abrir la clave del Registro para el registro de eventos.", ivEx);
					}
					catch (Win32Exception winEx)
					{
						// El sistema operativo ha notificado un error al escribir la entrada de evento en el Registro
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
			/// <param name="mensaje">Mensaje</param>
			/// <param name="excepcion">Excepcion</param>
			public static string EscribirEntradaLog(int eventoID, string mensaje, SqlException excepcion)
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
				return EscribirEntrada(eventoID, FormatearException(excepcion), EventLogEntryType.Error);
			}

			/// <summary>
			/// Registra un evento de tipo Error en el Log
			/// </summary>
			/// <param name="eventoID">Identificador del evento</param>
			/// <param name="excepcion">Excepcion</param>
			public static string EscribirEntradaLog(int eventoID, SqlException excepcion)
			{
				return EscribirEntrada(eventoID, FormatearException(excepcion), EventLogEntryType.Error);
			}

			/// <summary>
			/// Registra un evento en el Log
			/// </summary>
			/// <param name="eventoID">Identificador del evento</param>
			/// <param name="mensaje">Mensaje</param>
			/// <param name="eventoTipo">Tipo de Evento <see cref="EventLogEntryType"/> </param>
			public static string EscribirEntradaLog(int eventoID, string mensaje, EventLogEntryType eventoTipo)
			{
				string ticket = string.Empty;
				switch (eventoTipo)
				{
					case EventLogEntryType.Warning:
						if (LogLevel > 1)
							ticket = EscribirEntrada(eventoID, mensaje, eventoTipo);

						break;
					case EventLogEntryType.Information:
						if (LogLevel > 2)
							ticket = EscribirEntrada(eventoID, mensaje, EventLogEntryType.Information);

						break;
					default:
						ticket = EscribirEntrada(eventoID, mensaje, eventoTipo);
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

		/// <summary>
		/// 
		/// </summary>
		/// <param name="porTicket"></param>
		/// <param name="porFechaInicio"></param>
		/// <param name="porFechaFin"></param>
		/// <returns></returns>
		public static List<LogEvento> BuscarEventos(string porTicket, string porFechaInicio, string porFechaFin)
		{
			return new DalLogEventos().BuscarEventos(porTicket, porFechaInicio, porFechaFin);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="usuario"></param>
		/// <param name="contraseña"></param>
		/// <returns></returns>
		public static bool VerificaAcceso(string usuario, string contraseña)
		{
			return new DalLogEventos().VerificaAcceso(usuario, contraseña);
		}
	}
}