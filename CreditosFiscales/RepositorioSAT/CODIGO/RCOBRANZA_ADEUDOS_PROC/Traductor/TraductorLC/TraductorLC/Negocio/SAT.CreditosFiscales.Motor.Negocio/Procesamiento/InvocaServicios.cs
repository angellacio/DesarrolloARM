//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.Negocio:SAT.CreditosFiscales.Motor.Negocio.InvocaServicios:1:12/07/2012[Assembly:1.0:12/07/2013])

// Referencias de sistema.
using System;
using System.Linq;
using System.Diagnostics;
using System.ServiceModel;
using System.Collections.Generic;

// Referencias personalizadas.
using SAT.CreditosFiscales.Motor.Entidades.Catalogos;
using SAT.CreditosFiscales.Motor.Entidades.Procesamiento;
using SAT.CreditosFiscales.Motor.Negocio.Mensajes;
using SAT.CreditosFiscales.Motor.Negocio.RecepcionLC;
using SAT.CreditosFiscales.Motor.Utilidades;
using Sat.CreditosFiscales.Comunes.Herramientas;

namespace SAT.CreditosFiscales.Motor.Negocio.Procesamiento
{
	/// <summary>
	/// Clase de negocio que invoca los servicios de DyP
	/// </summary>
	public class InvocaServicios
	{
		/// <summary>
		/// Invoca los servicios DyP
		/// </summary>
		/// <param name="resultadoValidaciones">Clase Respuesta</param>
		/// <param name="solicitud">solicitud DyP de la línea de captura</param>
		/// <returns>Datos de RespuestaLC</returns>
		public RespuestaLC InvocaServicioLCDyP(ref Respuesta<ResultadoReglas> resultadoValidaciones, SolicitudGeneracionLC solicitud)
		{
			var respuesta = new RespuestaLC();
			var respuestaWcfLC = new RespuestaGeneracionLC();
			var datosLinea = new List<string>();
			var interceptor = new InterceptorMensajes();
			try
			{
				//Ordenar los conceptos de acuerdo a los iceps correctos para cuando el documento lo requiera                
				solicitud.Conceptos = (from t in solicitud.Conceptos select t).OrderBy(c => c.DatosIcep.IcepCorrecto).ToArray();

				var stopWatch = new Stopwatch();
				stopWatch.Start();
					using (RecepcionClient oClientLC = new RecepcionClient())
					{
						oClientLC.Endpoint.Binding.CloseTimeout = new TimeSpan(0, 10, 0);
						oClientLC.Endpoint.Behaviors.Add(interceptor);

						//Invocar servicio   
						respuestaWcfLC = oClientLC.GenerarLineaCaptura(solicitud);
					}
				stopWatch.Stop();

				// Format and display the TimeSpan value.
				resultadoValidaciones.Duracion = Convert.ToDecimal(stopWatch.Elapsed.TotalSeconds);

				//Configurar resultado                
				if (respuestaWcfLC.RespuestaDatosGenerales.Resultado == RespuestaResultado.Aceptado)
				{
					resultadoValidaciones.EsExitoso = true;

					if (respuestaWcfLC.RespuestaDatosGenerales.LineasCaptura.Count() > 0)
					{
						for (int i = 0; i < respuestaWcfLC.RespuestaDatosGenerales.LineasCaptura.Count(); i++)
							datosLinea.Add(respuestaWcfLC.RespuestaDatosGenerales.LineasCaptura[i].LineaCaptura);
					}
				}
				else
				{
					resultadoValidaciones.EsExitoso = false;
					respuesta.TipoRespuesta = 0;
					CatMensajes mensajeError = ObtieneMensajes.ObtieneMensajeError(Constantes.ServicioWebDyP);

					foreach (RespuestaError error in respuestaWcfLC.RespuestaErrores)
					{
						resultadoValidaciones.AgregaError(mensajeError, new Exception(error.Descripcion));
					}
				}

				if (respuestaWcfLC.RespuestaDatosGenerales.Resultado == RespuestaResultado.Aceptado)
				{
					respuesta.LineasCaptura = new RespuestaLCDatosLinea[1];
					var voDatosLineaCaptura = new RespuestaLCDatosLinea();

					voDatosLineaCaptura.Folio = respuestaWcfLC.RespuestaDatosGenerales.Solicitud;

					if (datosLinea.Count > 0)
						voDatosLineaCaptura.LineaCaptura = datosLinea[0];

					respuesta.LineasCaptura[0] = voDatosLineaCaptura;
					respuesta.TipoRespuesta = 1;
				}

			}
			catch (CommunicationException exComp)
			{
				CatMensajes mensajeError = ObtieneMensajes.ObtieneMensajeError(Constantes.ErrorComunicacionServicioWebDyP);
				resultadoValidaciones.AgregaError(mensajeError, exComp);
				resultadoValidaciones.EsExitoso = false;
				respuesta.TipoRespuesta = 0;
			}
			catch (TimeoutException timeEx)
			{
				CatMensajes mensajeError = ObtieneMensajes.ObtieneMensajeError(Constantes.ErrorComunicacionServicioWebDyP);
				resultadoValidaciones.AgregaError(mensajeError, timeEx);
				resultadoValidaciones.EsExitoso = false;
				respuesta.TipoRespuesta = 0;
			}
			catch (Exception ex)
			{
				CatMensajes mensajeError = ObtieneMensajes.ObtieneMensajeError(Constantes.ServicioWebDyP);
				resultadoValidaciones.AgregaError(mensajeError, ex);
				resultadoValidaciones.EsExitoso = false;
				respuesta.TipoRespuesta = 0;
			}

			finally
			{

				string s = interceptor.XMLPeticion;
				s = interceptor.XMLRespuesta;
			}

			return respuesta;
		}
	}
}