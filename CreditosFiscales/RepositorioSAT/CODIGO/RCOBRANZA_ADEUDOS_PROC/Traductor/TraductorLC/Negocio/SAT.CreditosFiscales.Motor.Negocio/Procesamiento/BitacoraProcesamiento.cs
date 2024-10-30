using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAT.CreditosFiscales.Motor.AccesoDatos.Procesamiento;
using SAT.CreditosFiscales.Motor.Entidades.CodigosError;
using SAT.CreditosFiscales.Motor.Entidades.DatosProcesamiento;
using SAT.CreditosFiscales.Motor.Entidades.ManejoErrores;
using SAT.CreditosFiscales.Motor.Entidades.Procesamiento;
using SAT.CreditosFiscales.Motor.Negocio.AccesoLogEventos;

namespace SAT.CreditosFiscales.Motor.Negocio.Procesamiento
{
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

        delegate void DelegadoGuardaBitacoraAsinc(Bitacora bitacora);
        /// <summary>
        /// Método que invoca al guradado asincrono de una petición en base de datos.
        /// </summary>
        /// <param name="peticion">Información del request y response realizado a los servicios con los que convive la aplicaición.</param>
        public static void GuardaBitacora(ResultadoReglas entidad,
            SAT.CreditosFiscales.Motor.Entidades.Enumeraciones.PasosTraductor paso,
            bool escribirMensaje = true,
            string observaciones = null,
            Errores listaErrores = null,
            decimal duracion = 0)
        {
            Bitacora bitacora = new Bitacora()
            {
                entidad = entidad,
                paso = paso,
                escribirMensaje = escribirMensaje,
                observaciones = observaciones,
                listaErrores = listaErrores,
                duracion = duracion
            };

            DelegadoGuardaBitacoraAsinc opAsinc = new DelegadoGuardaBitacoraAsinc(GuardaProcesamientAsinc);
            opAsinc.BeginInvoke(bitacora, null, null);
        }

        private static void GuardaProcesamientAsinc(Bitacora bitacora)
        {
            StringBuilder errores = new StringBuilder();           

            try
            {
                if (bitacora.listaErrores != null)
                {
                    bitacora.listaErrores.Select(
                    e =>
                    {
                        if (!string.IsNullOrEmpty(e.ErrorUsuario))
                        {
                            errores.Append(e.ErrorUsuario);
                        }

                        if (e.ErrorAplicacion != null)
                        {
                            errores.Append(FormatearException(e.ErrorAplicacion));
                        }

                        return e;
                    }
                    ).ToList();
                }

                DalProcesamiento.RegistraProcesamiento(
                    bitacora.entidad.IdAplicacion,
                    bitacora.entidad.IdTipoDocPago,
                    bitacora.entidad.IdProcesamiento,
                    bitacora.paso,
                    bitacora.escribirMensaje ? bitacora.entidad.Mensaje : null,
                    bitacora.observaciones, errores.ToString(), bitacora.duracion);
            }
            catch (Exception ex)
            {
                LogEventos.EscribirEntradaLog((int)EnumErrorMotor.ErrorGuardarBitacora, ex);
            }            
        }

        /// <summary>
        /// Formatea una excepción a mensaje para base de datos
        /// </summary>
        /// <param name="e">Excepción a formatear</param>
        /// <returns>mensaje formateado</returns>
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
