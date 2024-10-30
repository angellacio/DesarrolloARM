// Referencias de sistema.
using System;
using System.Linq;
using System.Text;

// Referencias personalizadas.
using SAT.CreditosFiscales.Motor.Entidades.CodigosError;
using SAT.CreditosFiscales.Motor.Entidades.ManejoErrores;
using SAT.CreditosFiscales.Motor.Entidades.Procesamiento;
using SAT.CreditosFiscales.Motor.Negocio.AccesoLogEventos;
using SAT.CreditosFiscales.Motor.AccesoDatos.Procesamiento;
using SAT.CreditosFiscales.Motor.Entidades.DatosProcesamiento;

namespace SAT.CreditosFiscales.Motor.Negocio.Procesamiento
{
	/// <summary>
	/// 
	/// </summary>
	public static class BitacoraProcesamiento
	{
		/// <summary>
		/// Tamño máximo de mensaje de error
		/// </summary>
		public static int MaxMessageSize
		{
			get
			{
				return 20000;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="bitacora"></param>
		delegate void DelegadoGuardaBitacoraAsinc(Bitacora bitacora);

		/// <summary>
		/// Método que invoca al guardado asincrono de una petición en base de datos.
		/// </summary>
		/// <param name="entidad"></param>
		/// <param name="paso"></param>
		/// <param name="escribirMensaje"></param>
		/// <param name="observaciones"></param>
		/// <param name="listaErrores"></param>
		/// <param name="duracion"></param>
		public static void GuardaBitacora	(
												ResultadoReglas entidad
												, Entidades.Enumeraciones.PasosTraductor paso
												, bool escribirMensaje = true
												, string observaciones = null
												, Errores listaErrores = null
												, decimal duracion = 0
											)
		{
			var vBitacora = new Bitacora()
			{
				entidad = entidad
				, paso = paso
				, escribirMensaje = escribirMensaje
				, observaciones = observaciones
				, listaErrores = listaErrores
				, duracion = duracion
			};

			var opAsinc = new DelegadoGuardaBitacoraAsinc(GuardaProcesamientAsinc);
			opAsinc.BeginInvoke(vBitacora, null, null);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="bitacora"></param>
		private static void GuardaProcesamientAsinc(Bitacora bitacora)
		{
			var vErrores = new StringBuilder();

			try
			{
				if (bitacora.listaErrores != null)
				{
					bitacora.listaErrores.Select(
													e =>
														{
															if (!string.IsNullOrEmpty(e.ErrorUsuario))
																vErrores.Append(e.ErrorUsuario);

															if (e.ErrorAplicacion != null)
																vErrores.Append(FormatearException(e.ErrorAplicacion));

															return e;
														}
												).ToList();
				}

				DalProcesamiento.RegistraProcesamiento	(
															bitacora.entidad.IdAplicacion
															, bitacora.entidad.IdTipoDocPago
															, bitacora.entidad.IdProcesamiento
															, bitacora.paso
															, (bitacora.escribirMensaje ? bitacora.entidad.Mensaje : null)
															, bitacora.observaciones
															, vErrores.ToString()
															, bitacora.duracion
														);
			}
			catch (Exception ex)
			{
				LogEventos.EscribirEntradaLog((int) EnumErrorMotor.ErrorGuardarBitacora, ex);
			}
		}

		/// <summary>
		/// Formatea una excepción a mensaje para base de datos
		/// </summary>
		/// <param name="e">Excepción a formatear</param>
		/// <returns>mensaje formateado</returns>
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