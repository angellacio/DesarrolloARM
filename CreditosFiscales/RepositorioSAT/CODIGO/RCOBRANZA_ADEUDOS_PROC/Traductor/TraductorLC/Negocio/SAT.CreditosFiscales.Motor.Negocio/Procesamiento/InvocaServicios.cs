
//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.Negocio:SAT.CreditosFiscales.Motor.Negocio.InvocaServicios:1:12/07/2012[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
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
        /// <param name="resultadoValidaciones">Clase Respuesta<ResultadoReglas></param>
        /// <param name="solicitud">solicitud DyP de la línea de captura</param>
        /// <returns>Datos de RespuestaLC</returns>
        public RespuestaLC InvocaServicioLCDyP(ref Respuesta<ResultadoReglas> resultadoValidaciones, SolicitudGeneracionLC solicitud)
        {            
            RespuestaLC respuesta = new RespuestaLC();
            RespuestaGeneracionLC respuestaWCFLC = new RespuestaGeneracionLC();            
            List<string> datosLinea = new List<string>();
             InterceptorMensajes interceptor = new InterceptorMensajes();
            try
            {
                //Ordenar los conceptos de acuerdo a los iceps correctos para cuando el documento lo requiera                
                solicitud.Conceptos = (from t in solicitud.Conceptos
                                       select t).OrderBy(c => c.DatosIcep.IcepCorrecto).ToArray();

                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
               
                using (RecepcionClient oClientLC = new RecepcionClient())
                {
                    oClientLC.Endpoint.Binding.CloseTimeout = new TimeSpan(0, 10, 0);
                    oClientLC.Endpoint.Behaviors.Add(interceptor);
                   
                    //Invocar servicio   
                    respuestaWCFLC = oClientLC.GenerarLineaCaptura(solicitud);
                }

                stopWatch.Stop();

                // Format and display the TimeSpan value.
                resultadoValidaciones.Duracion = Convert.ToDecimal(stopWatch.Elapsed.TotalSeconds);

                //Configurar resultado                
                if (respuestaWCFLC.RespuestaDatosGenerales.Resultado == RespuestaResultado.Aceptado)
                {
                    resultadoValidaciones.EsExitoso = true;

                    if (respuestaWCFLC.RespuestaDatosGenerales.LineasCaptura.Count() > 0)
                    {
                        for (int i = 0; i < respuestaWCFLC.RespuestaDatosGenerales.LineasCaptura.Count(); i++)
                        {
                            datosLinea.Add(respuestaWCFLC.RespuestaDatosGenerales.LineasCaptura[i].LineaCaptura);
                        }
                    }
                }
                else
                {
                    resultadoValidaciones.EsExitoso = false;
                    respuesta.TipoRespuesta = 0;
                    CatMensajes mensajeError = ObtieneMensajes.ObtieneMensajeError(Constantes.ServicioWebDyP);

                    foreach (RespuestaError error in respuestaWCFLC.RespuestaErrores)
                    {
                        resultadoValidaciones.AgregaError(mensajeError, new Exception(error.Descripcion));
                    }
                }

                if (respuestaWCFLC.RespuestaDatosGenerales.Resultado == RespuestaResultado.Aceptado)
                {
                    respuesta.LineasCaptura = new RespuestaLCDatosLinea[1];
                    RespuestaLCDatosLinea DatosLineaCaptura = new RespuestaLCDatosLinea();

                    DatosLineaCaptura.Folio = respuestaWCFLC.RespuestaDatosGenerales.Solicitud;

                    if (datosLinea.Count > 0)
                        DatosLineaCaptura.LineaCaptura = datosLinea[0];

                    respuesta.LineasCaptura[0] = DatosLineaCaptura;
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