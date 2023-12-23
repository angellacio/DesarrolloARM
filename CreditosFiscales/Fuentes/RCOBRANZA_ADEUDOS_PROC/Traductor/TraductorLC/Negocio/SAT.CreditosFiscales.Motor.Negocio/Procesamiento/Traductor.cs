
//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.Negocio:SAT.CreditosFiscales.Motor.Negocio.Traductor:1:12/07/2012[Assembly:1.0:12/07/2013])




#region Namespaces
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Xml.Xsl;
using System.Xml.Linq;

using SAT.CreditosFiscales.Motor.Entidades.Procesamiento;
using SAT.CreditosFiscales.Motor.Entidades.Esquemas;
using SAT.CreditosFiscales.Motor.Entidades.AdmonRegla;
using SAT.CreditosFiscales.Motor.Entidades.ManejoErrores;

using SAT.CreditosFiscales.Motor.AccesoDatos.AdmonReglas;
using SAT.CreditosFiscales.Motor.AccesoDatos.Esquemas;
using SAT.CreditosFiscales.Motor.AccesoDatos.Procesamiento;
using SAT.CreditosFiscales.Motor.AccesoDatos.ManejoErrores;

using Enumeraciones = SAT.CreditosFiscales.Motor.Entidades.Enumeraciones;
using DatosProcesamiento = SAT.CreditosFiscales.Motor.Entidades.DatosProcesamiento;

using SAT.CreditosFiscales.Motor.Negocio.AdmonReglas;
using SAT.CreditosFiscales.Motor.Negocio.Esquemas;
using System.Xml.Serialization;
using SAT.CreditosFiscales.Motor.Entidades.Herramientas;
using SAT.CreditosFiscales.Motor.Negocio.RecepcionLC;
using SAT.CreditosFiscales.Motor.AccesoDatos.Informacion;
using SAT.CreditosFiscales.Motor.Negocio.Mensajes;
using SAT.CreditosFiscales.Motor.Utilidades;
using SAT.CreditosFiscales.Motor.Entidades.Catalogos;
using SAT.CreditosFiscales.Motor.AccesoDatos.Catalogos;
using System.Diagnostics;
using SAT.CreditosFiscales.Motor.Entidades.DatosProcesamiento;
using SAT.CreditosFiscales.Motor.Entidades.CodigosError;
using SAT.CreditosFiscales.Motor.Negocio.DatosCache;
using SAT.CreditosFiscales.Motor.Negocio.AccesoLogEventos;
using SAT.CreditosFiscales.Motor.AccesoDatos.Busquedas;
#endregion

namespace SAT.CreditosFiscales.Motor.Negocio.Procesamiento
{
    public class Traductor : IDisposable
    {
        /// <summary>
        /// Método principal del procesamiento que realiza transformaciones, validaciones, generación de línea de captura
        /// y generación de pdf
        /// </summary>
        /// <param name="idAplicacion">Id de la aplicación</param>
        /// <param name="idTipoDoc">Id del tipo de documento</param>
        /// <param name="documento">Documento XML en string</param>
        /// <param name="generaPDF">true si desea generar PDF</param>
        /// <returns>string de RespuestaLC serializado</returns>
        public string EjecutaMotor(short idAplicacion, short idTipoDoc, string documento, bool generaPDF)
        {
            string respuestaGeneral = string.Empty;            

            try
            {
                RespuestaLC respuestaLC = new RespuestaLC();
                Respuesta<ResultadoReglas> resultadoValidacion = GeneraLineaCaptura(idAplicacion, idTipoDoc, documento, ref respuestaLC);
                respuestaLC.IdProcesamiento = resultadoValidacion.Entidad.IdProcesamiento.ToString();

                #region Genera PDF linea captura si es requerido

                if (generaPDF && resultadoValidacion.EsExitoso)
                {
                    Stopwatch stopWatch = new Stopwatch();
                    stopWatch.Start();
                    Respuesta<RespuestaLC> respuestaPDF = new Respuesta<RespuestaLC>();

                    respuestaPDF = ObtienePDF(respuestaLC, idAplicacion, idTipoDoc, documento);
                    //File.WriteAllBytes("C:\\Temp\\prueba.pdf", respuestaPDF.Entidad.PDF);
                    respuestaLC = respuestaPDF.Entidad;
                    stopWatch.Stop();

                    BitacoraProcesamiento.GuardaBitacora(
                        resultadoValidacion.Entidad,
                        Enumeraciones.PasosTraductor.GeneraPDF,
                        escribirMensaje: false,
                        observaciones: string.Format("La generación del PDF fue: {0}", respuestaPDF.EsExitoso == true ? "Exitoso" : "No Exitoso"),
                        listaErrores: respuestaPDF.ListaErrores,
                        duracion: Convert.ToDecimal(stopWatch.Elapsed.TotalSeconds));

                    if (!respuestaPDF.EsExitoso)
                    {
                        resultadoValidacion.Entidad.PasoProceso = Enumeraciones.PasosTraductor.GeneraPDF;
                        resultadoValidacion.EsExitoso = false;
                        respuestaLC.TipoRespuesta = 0;
                        resultadoValidacion.ListaErrores.AddRange(respuestaPDF.ListaErrores);
                    }
                }

                #endregion

                //Construye mensaje de salida
                respuestaGeneral = ConstruyeMensajeSalida(resultadoValidacion, respuestaLC);
                //resultadoValidacion.Dispose();
            }
            catch (Exception ex)
            {
                respuestaGeneral = "<RespuestaLC><ListaErrores><Error>" + ex.ToString() + "</Error></ListaErrores><TipoRespuesta>0</TipoRespuesta><IdProcesamiento></IdProcesamiento></RespuestaLC>";
            }

            return respuestaGeneral;
        }

        public string ObtienePDF(string LineaCaptura)
        {
            //Respuesta<RespuestaLC> resultadoPDF = new Respuesta<RespuestaLC>();
            RespuestaLC resultadoPDF = new RespuestaLC(); 
            RespuestaLCDatosLinea[] lineas= new RespuestaLCDatosLinea[1];
            RespuestaLCDatosLinea linea= new RespuestaLCDatosLinea();
                List<string> Folios = new List<string>();
                Folios.Add(BusquedaLineaCaptura.ConsultaFolioDyPPorLineaCaptura(LineaCaptura));
                ServicioImpresion.EnumTemplate e = (ServicioImpresion.EnumTemplate)1;

                using (ServicioImpresion.ServicioGeneraFormatoImpresionClient servImpresion = new ServicioImpresion.ServicioGeneraFormatoImpresionClient())
                {
                    resultadoPDF.PDF = servImpresion.GeneraArchivo(e, Folios.ToArray());
                }
                 linea.Folio=Folios.First();
                 linea.LineaCaptura=LineaCaptura;
                 resultadoPDF.TipoRespuesta = 1;
                 lineas[0]=linea;
                 resultadoPDF.LineasCaptura = lineas;
                //LogEventos.EscribirEntradaLog((int)EnumErrorMotor.ErrorCrearConceptoDyP, "", EventLogEntryType.Error);
                //throw new Exception("");
            return MetodosComunes.SerializaRespuestaLC(resultadoPDF);
        }

        public string ObtienePDFXFolio(string Folio, string LineaCaptura)
        {
            //Respuesta<RespuestaLC> resultadoPDF = new Respuesta<RespuestaLC>();
            RespuestaLC resultadoPDF = new RespuestaLC();
            RespuestaLCDatosLinea[] lineas = new RespuestaLCDatosLinea[1];
            RespuestaLCDatosLinea linea = new RespuestaLCDatosLinea();
            List<string> Folios = new List<string>();
            Folios.Add(Folio);
            ServicioImpresion.EnumTemplate e = (ServicioImpresion.EnumTemplate)1;

            using (ServicioImpresion.ServicioGeneraFormatoImpresionClient servImpresion = new ServicioImpresion.ServicioGeneraFormatoImpresionClient())
            {
                resultadoPDF.PDF = servImpresion.GeneraArchivo(e, Folios.ToArray());
            }
            linea.Folio = Folios.First();
            linea.LineaCaptura = LineaCaptura;
            resultadoPDF.TipoRespuesta = 1;
            lineas[0] = linea;
            resultadoPDF.LineasCaptura = lineas;
            //LogEventos.EscribirEntradaLog((int)EnumErrorMotor.ErrorCrearConceptoDyP, "", EventLogEntryType.Error);
            //throw new Exception("");
            return MetodosComunes.SerializaRespuestaLC(resultadoPDF);
        }

        public string ObtieneFormatos(int IdADR, string RFC, string Folio, string LineaDeCaptura, string No_de_Resolucion, int rango_de_emision_FechaPago, DateTime fecha_ini, DateTime fecha_fin)
        {
            //Respuesta<RespuestaLC> resultadoPDF = new Respuesta<RespuestaLC>();

            BusquedaLineaCaptura BusquedaForm = new BusquedaLineaCaptura();

            List<RespuestaListaFormatos> Formatos = BusquedaLineaCaptura.ConsultaFormatosXFiltros(IdADR,RFC, Folio, LineaDeCaptura, No_de_Resolucion, rango_de_emision_FechaPago, fecha_ini, fecha_fin);
            
            string salida = "<RespuestaLF><ListaFromatos>";
            for (int x = 0; x < Formatos.Count; x++)
            {
                salida += "<Formato>";
                string IdConsecutivo = "<IDCONS>" + x.ToString() + "</IDCONS>";
                string ADR = "<ADR>" + Formatos[x].ADR + "</ADR>";
                string RFCVal = "<RFC>" + Formatos[x].RFC + "</RFC>";
                string RazonSocial = "<RazonSocial>" + Formatos[x].RazonSocial + "</RazonSocial>";
                string FolioVal = "<Folio>" + Formatos[x].Folio + "</Folio>";
                string fechaEmision = "<fechaEmision>" + Formatos[x].fechaEmision + "</fechaEmision>";
                string IdResolucion = "<IdResolucion>" + Formatos[x].IdResolucion + "</IdResolucion>";
                string NumResolucion = "<NumResolucion>" + Formatos[x].NumResolucion + "</NumResolucion>";
                string LineaDeCapturaVal = "<LineaDeCapturaVal>" + Formatos[x].LineaDeCaptura + "</LineaDeCapturaVal>";
                string Importe = "<Importe>" + Formatos[x].Importe + "</Importe>";
                string FechaDeVigencia = "<FechaDeVigencia>" + Formatos[x].FechaDeVigencia + "</FechaDeVigencia>";
                string FormaDePago = "<FormaDePago>" + Formatos[x].FormaDePago + "</FormaDePago>";
                string FechaDePago = "<FechaDePago>" + Formatos[x].FechaDePago + "</FechaDePago>";
                string TipoDePago = "<TipoDePago>" + Formatos[x].TipoDePago + "</TipoDePago>";
                string Aplicativo = "<Aplicativo>" + Formatos[x].Aplicativo + "</Aplicativo>";
                string Modulo = "<Modulo>" + Formatos[x].Modulo + "</Modulo>";
                string RFC_Usr_Generador = "<RFC_Usr_Generador>" + Formatos[x].RFC_Usr_Generador + "</RFC_Usr_Generador>";
                string pDFFieldVal = "<pDFFieldVal>" + " " + "</pDFFieldVal>";
                string XML_Gen = "<XML_Gen>" + Formatos[x].XML + "</XML_Gen>";
                salida += IdConsecutivo+ADR + RFCVal + RazonSocial 
                    + FolioVal + fechaEmision + IdResolucion + NumResolucion 
                    + LineaDeCapturaVal
                    + Importe + FechaDeVigencia+FormaDePago + FechaDePago + TipoDePago 
                    + Aplicativo + Modulo + RFC_Usr_Generador
                    + pDFFieldVal + XML_Gen;
                salida += "</Formato>";
            }
            salida += "</ListaFromatos></RespuestaLF>";
            return salida;
        }

        /// <summary>
        /// Lógica de validaciones, transformaciones y generación de línea de captura
        /// </summary>
        /// <param name="idAplicacion">Id de la aplicación</param>
        /// <param name="idTipoDoc">Id del tipo de documento</param>
        /// <param name="documento">documento XML en string</param>
        /// <param name="respuestaLC">respuesta LC inicializada</param>
        /// <returns></returns>
        private Respuesta<ResultadoReglas> GeneraLineaCaptura(short idAplicacion, short idTipoDoc, string documento, ref RespuestaLC respuestaLC)
        {
            Respuesta<ResultadoReglas> resultadoValidacion = new Respuesta<ResultadoReglas> { Entidad = new ResultadoReglas(documento, idAplicacion, idTipoDoc) };
            Respuesta<ResultadoReglas> resultadoValidacionSIAT = new Respuesta<ResultadoReglas> { Entidad = new ResultadoReglas(documento, idAplicacion, idTipoDoc) };
            resultadoValidacionSIAT.Entidad.IdProcesamiento = resultadoValidacion.Entidad.IdProcesamiento;
            Contrato contratoActual = null;
            StringBuilder observaciones = new StringBuilder();
            Ejecucion reglasNegocio = new Ejecucion();
            SolicitudGeneracionLC solicitud = new SolicitudGeneracionLC();
            string respuestaGeneral = string.Empty;
            Stopwatch stopWatch = new Stopwatch();

            //Valida que el mensaje no venga vacío.
            if (documento.Trim().Length <= 0)
            {
                resultadoValidacion.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.ErrorMensajeVacio));
            }

            #region Registra Mensaje en Bitácora

            BitacoraProcesamiento.GuardaBitacora(
                resultadoValidacion.Entidad,
                Enumeraciones.PasosTraductor.RecibePeticion,
                observaciones: "Mensaje de entrada al traductor.",
                listaErrores: resultadoValidacion.ListaErrores);

            #endregion

            if (documento.Trim().Length <= 0)
            {
                resultadoValidacion.Entidad.PasoProceso = Enumeraciones.PasosTraductor.RecibePeticion;
                return resultadoValidacion;
            }

            #region Obtiene recursos de validación antes de realizar inserción a base de datos
            stopWatch.Start();
            Respuesta<ReglasPorDocumento> respuesta = reglasNegocio.ConsultaReglas(idAplicacion, idTipoDoc, true,false);
            Respuesta<ReglasPorDocumento> respuestaSIAT = reglasNegocio.ConsultaReglas(idAplicacion, idTipoDoc, true, true);
            stopWatch.Stop();

            BitacoraProcesamiento.GuardaBitacora(
               resultadoValidacion.Entidad,
               Enumeraciones.PasosTraductor.ObtieneReglas,
               escribirMensaje: false,
               observaciones: null,
               listaErrores: respuesta.ListaErrores,
               duracion: Convert.ToDecimal(stopWatch.Elapsed.TotalSeconds));
            
            BitacoraProcesamiento.GuardaBitacora(
                resultadoValidacionSIAT.Entidad,
                Enumeraciones.PasosTraductor.ObtieneReglasSIAT,
                escribirMensaje: false,
                observaciones: null,
                listaErrores: respuestaSIAT.ListaErrores,
                duracion: Convert.ToDecimal(stopWatch.Elapsed.TotalSeconds));

            if (!respuesta.EsExitoso)
            {
                resultadoValidacion.Entidad.PasoProceso = Enumeraciones.PasosTraductor.ObtieneReglas;
                resultadoValidacion.ListaErrores.AddRange(respuesta.ListaErrores);
                return resultadoValidacion;
            }
            #endregion

            #region Verifica si existe XSD de validación
            contratoActual = respuesta.Entidad.ListaContratos.Single(c => c.DireccionContrato == Enumeraciones.DireccionTraductor.Entrada);

            if (contratoActual == null)
            {
                resultadoValidacion.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.ContratoEntrada));
                return resultadoValidacion;
            }

            #endregion

            #region Valida mensaje de entrada
            stopWatch.Restart();
            // Valida estructura del mensaje de entrada.
            ValidaXSD oValidacionXSD = new ValidaXSD();
            RespuestaGenerica validacionXSD = oValidacionXSD.ValidaEsquemaXSD(documento, contratoActual.Xsd, contratoActual.TargetNamespace);
            stopWatch.Stop();

            resultadoValidacion.Duracion = Convert.ToDecimal(stopWatch.Elapsed.TotalSeconds);
            #region Registra Mensaje en Bitácora

            observaciones.Clear();
            observaciones.AppendFormat("Se validó el mensaje de entrada al motor con el esquema {0}: {1}", contratoActual.IdEsquema, contratoActual.Descripcion);

            BitacoraProcesamiento.GuardaBitacora(
                resultadoValidacion.Entidad,
                Enumeraciones.PasosTraductor.ValidaXSDEntrada,
                escribirMensaje: false,
                observaciones: observaciones.ToString(),
                listaErrores: validacionXSD.ListaErrores,
                duracion: Convert.ToDecimal(stopWatch.Elapsed.TotalSeconds));

            #endregion

            if (!validacionXSD.EsExitoso)
            {
                resultadoValidacion.Entidad.PasoProceso = Enumeraciones.PasosTraductor.ValidaXSDEntrada;
                resultadoValidacion.ListaErrores.AddRange(validacionXSD.ListaErrores);                
                return resultadoValidacion;
            }
            #endregion

            #region 


            #endregion

            #region Aplica reglas de validación y transformación antes de canonico
            stopWatch.Restart();
            if (respuesta.Entidad != null && respuesta.Entidad.ListaReglas != null && respuesta.Entidad.ListaReglas.Count > 0)
            {
                // Aplica reglas de validación
                reglasNegocio.AplicaReglas(documento, respuesta.Entidad, resultadoValidacion);
                //Transformacion para SIAT
                reglasNegocio.AplicaReglas(documento, respuestaSIAT.Entidad, resultadoValidacionSIAT);
                observaciones.Clear();
                observaciones.Append("Regla(s) aplicada(s): ");

                resultadoValidacion.Entidad.Reglas.Select(
                    r =>
                    {
                        observaciones.AppendFormat("{0}. {1} ({2} {3}). ", r.IdRegla, r.Descripcion, r.EsValidacion ? "Validación" : "Transformación", r.FueExitosa ? "sin errores" : "con errores");

                        return r;
                    }
                    ).ToList();

                stopWatch.Stop();
                
                BitacoraProcesamiento.GuardaBitacora(
                    resultadoValidacion.Entidad,
                    Enumeraciones.PasosTraductor.AplicaReglasAntesCanonico,
                    observaciones: observaciones.ToString(),
                    listaErrores: resultadoValidacion.ListaErrores,
                    duracion: Convert.ToDecimal(stopWatch.Elapsed.TotalSeconds));
                observaciones.Clear();
                observaciones.Append("Regla(s) aplicada(s): ");
                resultadoValidacionSIAT.Entidad.Reglas.Select(
                    r =>
                    {
                        observaciones.AppendFormat("{0}. {1} ({2} {3}). ", r.IdRegla, r.Descripcion, r.EsValidacion ? "Validación" : "Transformación", r.FueExitosa ? "sin errores" : "con errores");

                        return r;
                    }
                    ).ToList();

                BitacoraProcesamiento.GuardaBitacora(
                   resultadoValidacionSIAT.Entidad,
                   Enumeraciones.PasosTraductor.AplicaReglasAntesCanonicoSIAT,
                   observaciones: observaciones.ToString(),
                   listaErrores: resultadoValidacionSIAT.ListaErrores,
                   duracion: Convert.ToDecimal(stopWatch.Elapsed.TotalSeconds));
                
                if (!resultadoValidacion.EsExitoso)
                {
                    resultadoValidacion.Entidad.PasoProceso = Enumeraciones.PasosTraductor.AplicaReglasAntesCanonico;
                    resultadoValidacion.EsExitoso = false;
                    return resultadoValidacion;
                }

                if (!resultadoValidacionSIAT.EsExitoso)
                {
                    resultadoValidacionSIAT.Entidad.PasoProceso = Enumeraciones.PasosTraductor.AplicaReglasAntesCanonico;
                    resultadoValidacionSIAT.EsExitoso = false;
                    return resultadoValidacionSIAT;
                }
            }

            #endregion

            #region Completa transformación Xml Canonico
            stopWatch.Restart();
            DatosProcesamiento.CreditosFiscales oCreditosFiscales = new DatosProcesamiento.CreditosFiscales();
            var respuestaCompletaCanonico = CompletaCanonico(resultadoValidacion.Entidad.Mensaje);
            var respuestaCompletaCanonicoSIAT = CompletaCanonicoSIAT(resultadoValidacionSIAT.Entidad.Mensaje);
            stopWatch.Stop();

            observaciones.Clear();
            BitacoraProcesamiento.GuardaBitacora(
                resultadoValidacion.Entidad,
                Enumeraciones.PasosTraductor.CompletaTransformacionCanonica,
                observaciones: observaciones.AppendFormat("Completa mensaje canonico. Resultado: {0}", respuestaCompletaCanonico.EsExitoso == true ? "Exitoso" : "Falló").ToString(),
                listaErrores: respuestaCompletaCanonico.ListaErrores,
                duracion: Convert.ToDecimal(stopWatch.Elapsed.TotalSeconds));
            observaciones.Clear();
            BitacoraProcesamiento.GuardaBitacora(
                resultadoValidacionSIAT.Entidad,
                Enumeraciones.PasosTraductor.CompletaTransformacionCanonicaSIAT,
                observaciones: observaciones.AppendFormat("Completa mensaje canonico SIAT. Resultado: {0}", respuestaCompletaCanonico.EsExitoso == true ? "Exitoso" : "Falló").ToString(),
                listaErrores: respuestaCompletaCanonicoSIAT.ListaErrores,
                duracion: Convert.ToDecimal(stopWatch.Elapsed.TotalSeconds));

            if (!respuestaCompletaCanonico.EsExitoso)
            {
                resultadoValidacion.Entidad.PasoProceso = Enumeraciones.PasosTraductor.CompletaTransformacionCanonica;
                resultadoValidacion.ListaErrores.AddRange(respuestaCompletaCanonico.ListaErrores);
                resultadoValidacion.EsExitoso = false;
                return resultadoValidacion;
            }

            resultadoValidacion.Entidad.Mensaje = MetodosComunes.SerlializaCreditosFiscales(respuestaCompletaCanonico.Entidad);

            #endregion

            #region Valida la lista de conceptos

            if (respuestaCompletaCanonico.Entidad.DatosGenerales.LineasCaptura.DatosLineaCaptura[0].PagoObligadoInternet != 0)
            {
                stopWatch.Restart();
                var respuestaValidaTipoPersonaPeriodicidad = ValidaConceptoYPeriodicidadParaTipoPersona(idAplicacion, idTipoDoc, resultadoValidacion.Entidad.IdProcesamiento, respuestaCompletaCanonico);
                stopWatch.Stop();

                observaciones.Clear();
                BitacoraProcesamiento.GuardaBitacora(
                    resultadoValidacion.Entidad,
                    Enumeraciones.PasosTraductor.ValidaConceptoPeriodicidadPorTipoPersona,
                    escribirMensaje: false,
                    observaciones: observaciones.AppendFormat("Completa validación concepto y periodicidad para TipoPersona. Resultado: {0}", respuestaValidaTipoPersonaPeriodicidad.EsExitoso == true ? "Exitoso" : "Falló").ToString(),
                    listaErrores: respuestaValidaTipoPersonaPeriodicidad.ListaErrores,
                    duracion: Convert.ToDecimal(stopWatch.Elapsed.TotalSeconds));

                if (!respuestaValidaTipoPersonaPeriodicidad.EsExitoso)
                {
                    resultadoValidacion.Entidad.PasoProceso = Enumeraciones.PasosTraductor.ValidaConceptoPeriodicidadPorTipoPersona;
                    resultadoValidacion.EsExitoso = false;
                    resultadoValidacion.ListaErrores.AddRange(respuestaValidaTipoPersonaPeriodicidad.ListaErrores);
                    return resultadoValidacion;
                }
            }

            #endregion

            #region Agrupacion de Conceptos DyP
            stopWatch.Restart();
            //CreditosFiscalesConcepto[] conceptosSIAT = new CreditosFiscalesConcepto[respuestaCompletaCanonico.Entidad.Conceptos.Count()];
            //Array.(respuestaCompletaCanonico.Entidad.Conceptos, conceptosSIAT, conceptosSIAT.Count());
            CreditosFiscalesConcepto[] conceptosSIAT = Utilidades.GenericCopier<CreditosFiscalesConcepto[]>.DeepCopy(respuestaCompletaCanonico.Entidad.Conceptos);

            var respuestaAgrupacion = GeneraAgrupacionConceptosDyP(respuestaCompletaCanonico.Entidad);

            resultadoValidacion.Entidad.Mensaje = MetodosComunes.SerlializaCreditosFiscales(respuestaAgrupacion.Entidad);
            stopWatch.Stop();

            observaciones.Clear();
            BitacoraProcesamiento.GuardaBitacora(
                resultadoValidacion.Entidad,
                Enumeraciones.PasosTraductor.AgrupacionConceptos,
                observaciones: observaciones.AppendFormat("Agrupa mensaje canonico. Resultado: {0}", respuestaAgrupacion.EsExitoso == true ? "Exitoso" : "Falló").ToString(),
                listaErrores: respuestaAgrupacion.ListaErrores,
                duracion: Convert.ToDecimal(stopWatch.Elapsed.TotalSeconds));


            if (!resultadoValidacion.EsExitoso)
            {
                resultadoValidacion.ListaErrores.AddRange(respuestaAgrupacion.ListaErrores);
                return resultadoValidacion;
            }

            #endregion

            #region Busqueda si existe una línea previamente generada
            stopWatch.Restart();
            var respuestaExisteLC = BusquedaLineas.BusquedaLC(respuestaCompletaCanonico.Entidad);
           // respuestaCompletaCanonico.Entidad.DatosGenerales.NumeroParcialidad

            stopWatch.Stop();

            observaciones.Clear();
            BitacoraProcesamiento.GuardaBitacora(
                resultadoValidacion.Entidad,
                Enumeraciones.PasosTraductor.BuscaLineaExistenteBD,
                false,
                observaciones: observaciones.AppendFormat("Búsqueda de Línea de captura previamente generada, resultado de la búsqueda: {0} {1}", respuestaExisteLC.ExisteLineaCaptura == true ? "Existe Línea de Captura" : "No existe Línea de Captura", respuestaExisteLC.ExisteLineaCaptura == true && respuestaExisteLC.Entidad!=null ? "Folio: " + respuestaExisteLC.Entidad.LineasCaptura[0].Folio : string.Empty).ToString(),
                listaErrores: respuestaExisteLC.ListaErrores,
                duracion: Convert.ToDecimal(stopWatch.Elapsed.TotalSeconds));

            if (!respuestaExisteLC.EsExitoso)
            {
                resultadoValidacion.ListaErrores.AddRange(respuestaExisteLC.ListaErrores);
                resultadoValidacion.EsExitoso = false;
                return resultadoValidacion;
            }

            if (respuestaExisteLC.ExisteLineaCaptura)
            {
                resultadoValidacion.Entidad.PasoProceso = Enumeraciones.PasosTraductor.BuscaLineaExistenteBD;
                respuestaLC = respuestaExisteLC.Entidad;
                return resultadoValidacion;
            }

            #endregion

            #region Inserta datos de XML Canonico a BD

            stopWatch.Restart();
            LogicaAlmacena oLogAlmacena = new LogicaAlmacena(resultadoValidacion.Entidad.Mensaje, resultadoValidacion.Entidad.IdAplicacion, resultadoValidacion.Entidad.IdProcesamiento);  
            var resultadoInsersion = oLogAlmacena.AlmacenaDatosBD(respuestaCompletaCanonico.Entidad);
            stopWatch.Stop();
            observaciones.Clear();
            BitacoraProcesamiento.GuardaBitacora(
                resultadoValidacion.Entidad,
                Enumeraciones.PasosTraductor.InsertaDatosCanonicoDB,
                false,
                observaciones: observaciones.AppendFormat("Se inserta mensaje canonico por campos a BD : {0}", resultadoInsersion.EsExitoso.ToString()).ToString(),
                listaErrores: resultadoInsersion.ListaErrores,
                duracion: Convert.ToDecimal(stopWatch.Elapsed.TotalSeconds));
            if (!resultadoInsersion.EsExitoso)
            {
                resultadoValidacion.ListaErrores.AddRange(resultadoInsersion.ListaErrores);
                resultadoValidacion.EsExitoso = false;
                return resultadoValidacion;
            }
            //SIAT
           if (respuestaCompletaCanonicoSIAT.Entidad.DatosGenerales.EsSIAT)
            {
              stopWatch.Restart();
              var resultadoSIATInsersion = oLogAlmacena.AlmacenaDatosBDSIAT(respuestaCompletaCanonicoSIAT.Entidad, conceptosSIAT);
              stopWatch.Stop();
              observaciones.Clear();
              BitacoraProcesamiento.GuardaBitacora(
                    resultadoValidacionSIAT.Entidad,
                    Enumeraciones.PasosTraductor.InsertaDatosCanonicoDBSIAT,
                    false,
                    observaciones: observaciones.AppendFormat("Se inserta mensaje canonico por campos a BD para SIAT: {0}", resultadoInsersion.EsExitoso.ToString()).ToString(),
                    listaErrores: resultadoSIATInsersion.ListaErrores,
                    duracion: Convert.ToDecimal(stopWatch.Elapsed.TotalSeconds));

               if (!resultadoSIATInsersion.EsExitoso)
                {
                    resultadoValidacionSIAT.ListaErrores.AddRange(resultadoSIATInsersion.ListaErrores);
                    resultadoValidacionSIAT.EsExitoso = false;
                    return resultadoValidacionSIAT;
                }
            }
            #endregion

            #region Obtiene recursos de validación después de realizar inserción a base de datos
            Respuesta<ReglasPorDocumento> respuestaDespues = reglasNegocio.ConsultaReglas(idAplicacion, idTipoDoc, false,false);

            if (!respuesta.EsExitoso)
            {
                resultadoValidacion.ListaErrores.AddRange(respuesta.ListaErrores);
                return resultadoValidacion;
            }
            #endregion

            #region Aplica reglas de validación y transformación después de inserción
            if (respuesta.Entidad != null && respuesta.Entidad.ListaReglas != null && respuesta.Entidad.ListaReglas.Count > 0)
            {
                stopWatch.Restart();
                // Aplica reglas de validación
                reglasNegocio.AplicaReglas(resultadoValidacion.Entidad.Mensaje, respuestaDespues.Entidad, resultadoValidacion);

                observaciones.Clear();
                observaciones.Append("Regla(s) aplicada(s): ");

                resultadoValidacion.Entidad.Reglas.Select(
                    r =>
                    {
                        observaciones.AppendFormat("{0}. {1} ({2} {3}). ", r.IdRegla, r.Descripcion, r.EsValidacion ? "Validación" : "Transformación", r.FueExitosa ? "sin errores" : "con errores");

                        return r;
                    }
                    ).ToList();
                stopWatch.Stop();

                BitacoraProcesamiento.GuardaBitacora(
                    resultadoValidacion.Entidad,
                    Enumeraciones.PasosTraductor.AplicaReglasDespuesCanonico,
                    observaciones: observaciones.ToString(),
                    listaErrores: resultadoValidacion.ListaErrores,
                    duracion: Convert.ToDecimal(stopWatch.Elapsed.TotalSeconds));

            }

            if (!resultadoValidacion.EsExitoso)
            {
                resultadoValidacion.Entidad.PasoProceso = Enumeraciones.PasosTraductor.AplicaReglasDespuesCanonico;
                resultadoValidacion.EsExitoso = false;
                return resultadoValidacion;
            }

            #endregion

            #region CompletarEsquemaDyP

            stopWatch.Restart();
            solicitud = CompletarEsquemaDyP(ref resultadoValidacion, respuestaCompletaCanonico.Entidad.DatosGenerales.ALR);
            resultadoValidacion.Entidad.Mensaje = SerializaSolicitudDyP(solicitud);
            stopWatch.Stop();

            observaciones.Clear();
            BitacoraProcesamiento.GuardaBitacora(
               resultadoValidacion.Entidad,
               Enumeraciones.PasosTraductor.CompletaEsquemaDyP,
               true,
               observaciones: observaciones.AppendFormat("Se completa mensaje DyP para generación de Línea de Captura : {0}", resultadoValidacion.EsExitoso.ToString()).ToString(),
               listaErrores: resultadoValidacion.ListaErrores,
               duracion: Convert.ToDecimal(stopWatch.Elapsed.TotalSeconds));

            if (!resultadoValidacion.EsExitoso)
            {

                resultadoValidacion.Entidad.PasoProceso = Enumeraciones.PasosTraductor.CompletaEsquemaDyP;
                resultadoValidacion.EsExitoso = false;
                return resultadoValidacion;
            }

            #endregion

            #region Valida mensaje que se va a enviar a DyP

            stopWatch.Restart();
            contratoActual = respuesta.Entidad.ListaContratos.Single(c => c.DireccionContrato == Enumeraciones.DireccionTraductor.Salida);

            if (contratoActual == null)
            {
                resultadoValidacion.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.ContratoDyP));
                return resultadoValidacion;
            }


            // Valida estructura del mensaje de entrada a DyP.
            var validacionSalida = oValidacionXSD.ValidaEsquemaXSD(resultadoValidacion.Entidad.Mensaje,
                contratoActual.Xsd,
                contratoActual.TargetNamespace);
            stopWatch.Stop();

            observaciones.Clear();
            observaciones.AppendFormat("Se validó el mensaje de salida al motor con el esquema {0}: {1} - Exitoso: {2}", contratoActual.IdEsquema, contratoActual.Descripcion, validacionSalida.EsExitoso.ToString());

            BitacoraProcesamiento.GuardaBitacora(
                resultadoValidacion.Entidad,
                Enumeraciones.PasosTraductor.ValidaXSDSalida,
                observaciones: observaciones.ToString(),
                listaErrores: validacionSalida.ListaErrores,
                duracion: Convert.ToDecimal(stopWatch.Elapsed.TotalSeconds));

            if (!validacionSalida.EsExitoso)
            {
                resultadoValidacion.Entidad.PasoProceso = Enumeraciones.PasosTraductor.ValidaXSDSalida;
                resultadoValidacion.EsExitoso = false;
                resultadoValidacion.ListaErrores.AddRange(validacionSalida.ListaErrores);
                return resultadoValidacion;
            }

            #endregion

            #region OrdenaTransacciones
            //Ordenamiento de Transacciones DyP
            stopWatch.Restart();
            var respuestaOrdenamiento = OrdenamientoTransacciones(solicitud, resultadoValidacion.Entidad.IdAplicacion, resultadoValidacion.Entidad.IdTipoDocPago);
            resultadoValidacion.Entidad.Mensaje = SerializaSolicitudDyP(respuestaOrdenamiento.Entidad);
            stopWatch.Stop();

            observaciones.Clear();
            BitacoraProcesamiento.GuardaBitacora(
                resultadoValidacion.Entidad,
                Enumeraciones.PasosTraductor.OrdenamientoTransacciones,
                escribirMensaje: true,
                observaciones: observaciones.AppendFormat("Se ordenan transacciones resultado: {0}", respuestaOrdenamiento.EsExitoso.ToString()).ToString(),
                listaErrores: respuestaOrdenamiento.ListaErrores,
                duracion: Convert.ToDecimal(stopWatch.Elapsed.TotalSeconds));

            if (!respuestaOrdenamiento.EsExitoso)
            {
                resultadoValidacion.ListaErrores.AddRange(respuestaOrdenamiento.ListaErrores);
                return resultadoValidacion;
            }

            #endregion

            #region Envia mensaje a DyP
            stopWatch.Restart();
            //Envia Mensaje a DyP
            InvocaServicios oInvocarServicios = new InvocaServicios();
            respuestaLC = oInvocarServicios.InvocaServicioLCDyP(ref resultadoValidacion, respuestaOrdenamiento.Entidad);
            stopWatch.Stop();

            observaciones.Clear();
            BitacoraProcesamiento.GuardaBitacora(
                resultadoValidacion.Entidad,
                Enumeraciones.PasosTraductor.EnviadoDyP,
                escribirMensaje: true,
                observaciones: observaciones.AppendFormat("Se envía mensaje a DyP resultado: {0}", respuestaLC.TipoRespuesta == 1 ? "ACEPTADO" : "NO ACEPTADO").ToString(),
                listaErrores: resultadoValidacion.ListaErrores,
                duracion: Convert.ToDecimal(stopWatch.Elapsed.TotalSeconds));

            if (respuestaLC.ListaErrores != null)
            {
                resultadoValidacion.EsExitoso = false;
                return resultadoValidacion;
            }

            #endregion

            //ActualizaDatos Respuesta en BD
            actualizaDatosGenerales(resultadoValidacion.Entidad.IdProcesamiento,
            solicitud.DatosGenerales == null ? string.Empty : solicitud.DatosGenerales.Solicitud,
            respuestaLC == null ? string.Empty : (respuestaLC.LineasCaptura == null ? string.Empty : respuestaLC.LineasCaptura[0].LineaCaptura), resultadoValidacion.EsExitoso, resultadoValidacion.Duracion);
            
            return resultadoValidacion;
        }

        private Respuesta<SolicitudGeneracionLC> OrdenamientoTransacciones(SolicitudGeneracionLC solicitud, short IdAplicacion, short IdTipoDocumento)
        {
            Respuesta<SolicitudGeneracionLC> respuesta = new Respuesta<SolicitudGeneracionLC>();

            try
            {
                List<CatOrdenamientoTransacciones> lstOrdenamiento = DalCatOrdenamientoTransacciones.ConsultaOrdenamiento(IdAplicacion, IdTipoDocumento);
                if (lstOrdenamiento.Count > 0)
                {
                    Parallel.For(0, solicitud.Conceptos.Count(), new ParallelOptions{MaxDegreeOfParallelism = 4}, i =>
                    {
                        List<Transaccion> oTransaccionTemp = new List<Transaccion>();
                        List<Transaccion> oTransaccionOriginal = solicitud.Conceptos[i].Transacciones.ToList();
                        foreach (CatOrdenamientoTransacciones ordenamiento in lstOrdenamiento)
                        {
                            //Agregar transacciones a Temporal segun secuencia
                            oTransaccionTemp.AddRange(oTransaccionOriginal.Where(t => t.Tipo.ToLower() == ordenamiento.Ordenamiento.ToLower() || t.Descripcion.ToLower() == ordenamiento.Ordenamiento.ToLower()));

                            //Eliminar transacciones de original
                            var index = oTransaccionOriginal.FindAll(t => t.Tipo.ToLower() == ordenamiento.Ordenamiento.ToLower() || t.Descripcion.ToLower() == ordenamiento.Ordenamiento.ToLower());
                            if (index.Count >= 0)
                            {
                                foreach (Transaccion tran in index)
                                {
                                    // ensure item found
                                    oTransaccionOriginal.Remove(tran);
                                }
                            }
                        }

                        solicitud.Conceptos[i].Transacciones = oTransaccionTemp.ToArray();
                    });
                }

                respuesta.EsExitoso = true;
            }
            catch (Exception ex)
            {
                respuesta.EsExitoso = false;
                respuesta.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.OrdenamientoTransaccciones), ex);
            }

            respuesta.Entidad = solicitud;
            return respuesta;
        }


        private static Respuesta<bool> ValidaConceptoYPeriodicidadParaTipoPersona(short idAplicacion, short idTipoDoc, Guid IdProcesamiento, Respuesta<DatosProcesamiento.CreditosFiscales> respuestaCompletaCanonico)//, ref Respuesta<ResultadoReglas> resultadoValidacion)
        {
            Respuesta<bool> respuestaValidaEquivalencias = new Respuesta<bool>();

            try
            {
                //PersonaFisica(1|TB:DatosGenerales=2), Persona Moral(1|TB:DatosGenerales=4)
                int tipoPersona = respuestaCompletaCanonico.Entidad.DatosGenerales.LineasCaptura.DatosLineaCaptura[0].PagoObligadoInternet;

                List<CatConceptoPersonaPeriodicidad> catConceptoPersonaPeriodicidad = DalConceptoPersonaPeriodicidad.ObtenerCatConceptoPersonaPeriodicidad(tipoPersona);

                if (catConceptoPersonaPeriodicidad.Count() != 0)
                {

                    foreach (var concepto in respuestaCompletaCanonico.Entidad.Conceptos)
                    {
                        if (!catConceptoPersonaPeriodicidad.Exists(m => m.ConceptoDyP == concepto.Clave && m.IdPeriodicidadDyP == concepto.TipoPeriodicidad && m.IdTipoPersona == tipoPersona))
                        {
                            respuestaValidaEquivalencias.EsExitoso = false;
                            respuestaValidaEquivalencias.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.ConceptoPersonaPeriodicidad), concepto.Clave, concepto.Periodicidad, tipoPersona);
                        }
                    }
                }
                else
                {
                    respuestaValidaEquivalencias.EsExitoso = false;
                    respuestaValidaEquivalencias.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.RecuperaConceptoPersonaPeriodicidad), tipoPersona);
                }

                if (respuestaValidaEquivalencias.ListaErrores.Count == 0)
                {
                    respuestaValidaEquivalencias.EsExitoso = true;
                }
                else
                {
                    respuestaCompletaCanonico.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.ErrorValidaConceptoPeriodicidadPorTipoPersona));
                    respuestaValidaEquivalencias.EsExitoso = false;
                }
            }
            catch (Exception ex)
            {
                respuestaCompletaCanonico.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.ErrorValidaConceptoPeriodicidadPorTipoPersona), ex);
                respuestaValidaEquivalencias.EsExitoso = false;
            }

            return respuestaValidaEquivalencias;
        }


        /// <summary>
        /// Obtiene un documento PDF generado por el servicio de Impresión de PDF
        /// </summary>
        /// <param name="datosLC">Datos de la línea de captura</param>
        /// <returns></returns>
        private Respuesta<RespuestaLC> ObtienePDF(RespuestaLC datosLC, int IdAplicacion, int IdTipoDocumento, string documento)
        {
            Respuesta<RespuestaLC> resultadoPDF = new Respuesta<RespuestaLC>();

            try
            {
                resultadoPDF.Entidad = datosLC;

                List<string> Folios = new List<string>();

               // Folios.Add("44721500000044");

                foreach (RespuestaLCDatosLinea datosFolio in resultadoPDF.Entidad.LineasCaptura)
                {
                    Folios.Add(datosFolio.Folio);
                }

                //Obtiene Formato de impresión
                int formatoImpresion = GeneracionEquivalenciaFormato.GeneraEquivalenciaFormato(IdAplicacion, IdTipoDocumento, documento);
                ServicioImpresion.EnumTemplate e = (ServicioImpresion.EnumTemplate)formatoImpresion;

                using (ServicioImpresion.ServicioGeneraFormatoImpresionClient servImpresion = new ServicioImpresion.ServicioGeneraFormatoImpresionClient())
                {
                    resultadoPDF.Entidad.PDF = servImpresion.GeneraArchivo(e, Folios.ToArray());
                }

                resultadoPDF.EsExitoso = true;
            }
            catch (Exception ex)
            {
                resultadoPDF.EsExitoso = false;
                resultadoPDF.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.GeneracionPDF), ex);
            }

            return resultadoPDF;
        }

        /// <summary>
        /// Genera un formato correcto para el mensaje de salida
        /// </summary>
        /// <param name="resultadoProcesamiento">resultado total del procesamiento</param>
        /// <param name="respuestaLC">respuesta de la línea de captura</param>
        /// <returns>string serializado de la respuesta del servicio</returns>
        private string ConstruyeMensajeSalida(Respuesta<ResultadoReglas> resultadoProcesamiento, RespuestaLC respuestaLC = null)
        {
            string respuestaGeneral = string.Empty;

            try
            {
                if (respuestaLC == null)
                {
                    respuestaLC = new RespuestaLC();
                    respuestaLC.TipoRespuesta = 0;
                    respuestaLC.ListaErrores = new string[resultadoProcesamiento.ListaErrores.Count];

                    for (int i = 0; i < resultadoProcesamiento.ListaErrores.Count; i++)
                    {
                        string ErrorAplicacion = string.Empty;
                        if (resultadoProcesamiento.ListaErrores[i].ErrorAplicacion != null)
                            ErrorAplicacion = resultadoProcesamiento.ListaErrores[i].ErrorAplicacion.Message;
                        respuestaLC.ListaErrores[i] = resultadoProcesamiento.ListaErrores[i].Codigo + "|" + resultadoProcesamiento.ListaErrores[i].ErrorUsuario + "|" + ErrorAplicacion;
                    }
                }
                else if (respuestaLC != null && respuestaLC.ListaErrores == null && resultadoProcesamiento.ListaErrores.Count == 0)
                    respuestaLC.TipoRespuesta = 1;
                else if (respuestaLC != null && respuestaLC.ListaErrores == null && resultadoProcesamiento.ListaErrores.Count > 0)
                {
                    respuestaLC.TipoRespuesta = 0;
                    respuestaLC.ListaErrores = new string[resultadoProcesamiento.ListaErrores.Count];

                    for (int i = 0; i < resultadoProcesamiento.ListaErrores.Count; i++)
                    {
                        string ErrorAplicacion = string.Empty;
                        if (resultadoProcesamiento.ListaErrores[i].ErrorAplicacion != null)
                            ErrorAplicacion = resultadoProcesamiento.ListaErrores[i].ErrorAplicacion.Message;
                        respuestaLC.ListaErrores[i] = resultadoProcesamiento.ListaErrores[i].Codigo + "|" + resultadoProcesamiento.ListaErrores[i].ErrorUsuario + "|" + ErrorAplicacion;
                    }
                }
                else if (respuestaLC != null && respuestaLC.LineasCaptura == null)
                    respuestaLC.TipoRespuesta = 0;
                else
                    respuestaLC.TipoRespuesta = 0;

                respuestaGeneral = MetodosComunes.SerializaRespuestaLC(respuestaLC);
            }
            catch (Exception ex)
            {
                respuestaGeneral = "<RespuestaLC><ListaErrores><Error>" + ex.Message + "</Error></ListaErrores><TipoRespuesta>0</TipoRespuesta><IdProcesamiento></IdProcesamiento></RespuestaLC>";
            }

            return respuestaGeneral;
        }

        /// <summary>
        /// Completa el esquema que se enviará a DyP
        /// </summary>
        /// <param name="resultadoValidacion">resultado total del procesamiento</param>
        /// <param name="IdALR">Id del ALR</param>
        /// <returns></returns>
        private SolicitudGeneracionLC CompletarEsquemaDyP(ref Respuesta<ResultadoReglas> resultadoValidacion, sbyte IdALR)
        {

            //Deserializar objeto de solicitud de generación de LC
            SolicitudGeneracionLC solicitud = new SolicitudGeneracionLC();
            try
            {
                solicitud = DeserializaSolicitudDyP(resultadoValidacion.Entidad.Mensaje);

                #region Folio

                var respuestaFolio = this.obtieneNumeroFolio(resultadoValidacion.Entidad.IdAplicacion, (byte)IdALR);

                if (respuestaFolio.EsExitoso)
                {
                    solicitud.DatosGenerales.Solicitud = respuestaFolio.Entidad;

                    #region FechaEmisión y FechaRecepción

                    var respuestaFechas = obtieneFechasSolicitud(resultadoValidacion.Entidad.IdProcesamiento);

                    if (respuestaFechas.EsExitoso)
                    {
                        solicitud.DatosGenerales.FechaEmision = respuestaFechas.Entidad["FechaEmision"];
                        solicitud.DatosGenerales.FechaRecepcion = respuestaFechas.Entidad["FechaRecepcion"];
                    }
                    else
                    {
                        resultadoValidacion.EsExitoso = false;
                        resultadoValidacion.ListaErrores.AddRange(respuestaFechas.ListaErrores);
                        return solicitud;
                    }

                    #region ImporteTotal

                    var respuestaImporte = obtieneImporteTotalSolicitud(solicitud.Conceptos);

                    if (respuestaFechas.EsExitoso)
                    {
                        solicitud.DatosGenerales.ImporteTotal = respuestaImporte.Entidad;
                    }
                    else
                    {
                        resultadoValidacion.EsExitoso = false;
                        resultadoValidacion.ListaErrores.AddRange(respuestaImporte.ListaErrores);
                        return solicitud;
                    }

                    #endregion

                    #endregion

                    #region ImportesPorConcepto

                    var resultadoImportes = obtieneImportesConceptos(solicitud.Conceptos, resultadoValidacion.Entidad.IdTipoDocPago);

                    if (!resultadoImportes.EsExitoso)
                    {
                        resultadoValidacion.EsExitoso = false;
                        resultadoValidacion.ListaErrores.AddRange(respuestaImporte.ListaErrores);
                        return solicitud;
                    }

                    #endregion
                }
                else
                {
                    resultadoValidacion.EsExitoso = false;
                    resultadoValidacion.ListaErrores.AddRange(respuestaFolio.ListaErrores);
                    return solicitud;
                }

                #endregion

            }
            catch (Exception ex)
            {
                resultadoValidacion.EsExitoso = false;
                resultadoValidacion.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.GeneracionSolicitudDyP), ex);
            }

            return solicitud;
        }

        /// <summary>
        /// Método para completar mensaje canonico
        /// </summary>
        /// <param name="documentoCanonico">documento canonico xml</param>
        /// <returns></returns>
        private Respuesta<DatosProcesamiento.CreditosFiscales> CompletaCanonico(string documentoCanonico)
        {
            Respuesta<DatosProcesamiento.CreditosFiscales> respuestaCompletaCanonico = new Respuesta<DatosProcesamiento.CreditosFiscales>();

            try
            {
                respuestaCompletaCanonico.Entidad = GeneracionEquivalenciaConceptos.GeneraMensajeLineaCaptura(documentoCanonico);
                respuestaCompletaCanonico.EsExitoso = true;

                if (respuestaCompletaCanonico.Entidad == null)
                    respuestaCompletaCanonico.EsExitoso = false;
                else
                {
                    if (respuestaCompletaCanonico.Entidad.DatosGenerales.DeudorPuro == 1)
                    {
                        respuestaCompletaCanonico.Entidad.DatosGenerales.RFCGenerico = Catalogos.AplicationSettings.ConsultaConfiguracion(Constantes.RFCGenerico).ToString();
                        respuestaCompletaCanonico.Entidad.DatosGenerales.IdContribuyente = Catalogos.AplicationSettings.ConsultaConfiguracion(Constantes.BOIDGenerico).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                respuestaCompletaCanonico.EsExitoso = false;
                if (ex.InnerException != null)
                    respuestaCompletaCanonico.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.CompletaCanonico), new Exception(ex.InnerException.Message));
                else
                    respuestaCompletaCanonico.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.CompletaCanonico), ex);
            }

            return respuestaCompletaCanonico;
        }

        /// <summary>
        /// Método para completar mensaje canonico para SIAT
        /// </summary>
        /// <param name="documentoCanonico">documento canonico xml</param>
        /// <returns></returns>
        private Respuesta<DatosProcesamiento.CreditosFiscalesSIAT> CompletaCanonicoSIAT(string documentoCanonico)
        {
            Respuesta<DatosProcesamiento.CreditosFiscalesSIAT> respuestaCompletaCanonico = new Respuesta<DatosProcesamiento.CreditosFiscalesSIAT>();

            try
            {
                respuestaCompletaCanonico.Entidad = MetodosComunes.DeserializaXmlSIAT(documentoCanonico);
                respuestaCompletaCanonico.EsExitoso = true;

                if (respuestaCompletaCanonico.Entidad == null)
                {
                    respuestaCompletaCanonico.EsExitoso = false;
                }
            }
            catch (Exception ex)
            {
                respuestaCompletaCanonico.EsExitoso = false;
                if (ex.InnerException != null)
                    respuestaCompletaCanonico.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.CompletaCanonico), new Exception(ex.InnerException.Message));
                else
                    respuestaCompletaCanonico.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.CompletaCanonico), ex);
            }

            return respuestaCompletaCanonico;
        }

        /// <summary>
        /// Obtiene el número de folio por aplicación y ALR.
        /// </summary>
        /// <param name="idAplicacion">Identificador de la aplicación.</param>
        /// <param name="idALR">Identificador del ALR.</param>
        /// <returns>Folio obtenido o el error generado en su caso.</returns>
        private Respuesta<string> obtieneNumeroFolio(short idAplicacion, byte idALR)
        {
            Respuesta<string> resultadoFolio = new Respuesta<string>();

            try
            {
                resultadoFolio.Entidad = DalProcesamiento.ObtieneFolioPorAplicacion(idAplicacion, idALR);
                resultadoFolio.EsExitoso = true;
            }
            catch (Exception ex)
            {
                resultadoFolio.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.ObtenerFolio), ex);
            }

            return resultadoFolio;
        }

        /// <summary>
        /// Obtiene Importe Total de la solicitud
        /// </summary>
        /// <param name="conceptos">Clase de conceptos a completar</param>
        /// <returns>Importe total</returns>
        private Respuesta<decimal> obtieneImporteTotalSolicitud(Concepto[] conceptos)
        {
            Respuesta<decimal> resultadoImporte = new Respuesta<decimal>();

            try
            {
                resultadoImporte.Entidad = Convert.ToDecimal(conceptos.Sum(r => r.CantidadPagar));
                resultadoImporte.EsExitoso = true;
            }
            catch (Exception ex)
            {
                resultadoImporte.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.ObtenerFolio), ex);
            }

            return resultadoImporte;
        }

        /// <summary>
        /// Obtiene las fechas de solicitud de la base de datos
        /// </summary>
        /// <param name="IdProcesamiento">Id del procesamiento</param>
        /// <returns></returns>
        private Respuesta<Dictionary<string, string>> obtieneFechasSolicitud(Guid IdProcesamiento)
        {
            Respuesta<Dictionary<string, string>> resultadoFechas = new Respuesta<Dictionary<string, string>>();

            try
            {
                resultadoFechas.Entidad = DalProcesamiento.ObtieneFechasProceso(IdProcesamiento);
                resultadoFechas.EsExitoso = true;
            }
            catch (Exception ex)
            {
                resultadoFechas.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.ObtenerFolio), ex);
            }

            return resultadoFechas;
        }

        private Respuesta<DatosProcesamiento.CreditosFiscales> GeneraAgrupacionConceptosDyP(DatosProcesamiento.CreditosFiscales solicitud)
        {
            Respuesta<DatosProcesamiento.CreditosFiscales> resultadoAgrupacion = new Respuesta<DatosProcesamiento.CreditosFiscales>();

            try
            {
                List<DatosProcesamiento.CreditosFiscalesConcepto> conceptosAgrupados = new List<DatosProcesamiento.CreditosFiscalesConcepto>();

                short secuencia = 1;
                for (int i = 0; i < solicitud.Conceptos.Count(); i++)
                {
                    solicitud.Conceptos[i].NoSecuencia = secuencia;
                    secuencia++;
                }

                foreach (DatosProcesamiento.CreditosFiscalesConcepto conceptoDyP in solicitud.Conceptos)
                {
                    //Buscar si el concepto ya existe en el ConceptoDyPAgrupado     
                    DatosProcesamiento.CreditosFiscalesConcepto conceptoTemp = ExisteConcepto(conceptoDyP.Clave, conceptoDyP.Ejercicio, conceptoDyP.FechaCausacion, conceptoDyP.Periodo, conceptoDyP.TipoPeriodicidad, conceptoDyP.EsCorrecto, ref conceptosAgrupados);
                    //Si el concepto existe buscarlo , sumar la cantidad a pagar y agregar todas las transacciones
                    if (conceptoTemp != null)
                    {
                        //Agrupar Transacciones
                        conceptosAgrupados.Find(c => c.NoSecuencia == conceptoTemp.NoSecuencia).Transacciones = AgruparTransaccion(conceptosAgrupados.Find(c => c.NoSecuencia == conceptoTemp.NoSecuencia).Transacciones.ToList(), conceptoDyP.Transacciones.ToList());

                        //Sumar Importe a Pagar
                        conceptosAgrupados.Find(c => c.NoSecuencia == conceptoTemp.NoSecuencia).ImportePagar += conceptoDyP.ImportePagar;
                    }
                    else // si no existe agregarlo
                    {
                        //Agrupar Transacciones
                        conceptoDyP.Transacciones = AgruparTransaccion(conceptoDyP.Transacciones.ToList(), null);
                        conceptosAgrupados.Add(conceptoDyP);
                    }
                }

                solicitud.Conceptos = conceptosAgrupados.ToArray();
                resultadoAgrupacion.Entidad = solicitud;
                resultadoAgrupacion.EsExitoso = true;
            }
            catch (Exception ex)
            {
                resultadoAgrupacion.EsExitoso = false;
                resultadoAgrupacion.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.AgrupacionConceptosDyP), ex);
            }

            return resultadoAgrupacion;
        }

        private DatosProcesamiento.CreditosFiscalesConceptoTransaccion[] AgruparTransaccion(List<DatosProcesamiento.CreditosFiscalesConceptoTransaccion> transaccionesPivote, List<DatosProcesamiento.CreditosFiscalesConceptoTransaccion> transaccionesNuevas = null)
        {
            List<DatosProcesamiento.CreditosFiscalesConceptoTransaccion> transaccionesAgrupadas = new List<DatosProcesamiento.CreditosFiscalesConceptoTransaccion>();

            //Realizar agrupacion de las transacciones privote
            foreach (DatosProcesamiento.CreditosFiscalesConceptoTransaccion transaccionPivote in transaccionesPivote)
            {
                DatosProcesamiento.CreditosFiscalesConceptoTransaccion transaccion = ExisteTransaccion(transaccionPivote.Clave, transaccionPivote.Tipo, ref transaccionesAgrupadas);

                if (transaccion != null)
                {
                    transaccionesAgrupadas.Find(tr => tr.Clave == transaccion.Clave && tr.Tipo == transaccion.Tipo).Importe += transaccionPivote.Importe;
                }
                else
                    transaccionesAgrupadas.Add(transaccionPivote);
            }

            if (transaccionesNuevas != null)
            {
                //Realizar agrupacion de las transacciones nuevas
                foreach (DatosProcesamiento.CreditosFiscalesConceptoTransaccion transaccionNueva in transaccionesNuevas)
                {
                    DatosProcesamiento.CreditosFiscalesConceptoTransaccion transaccion = ExisteTransaccion(transaccionNueva.Clave, transaccionNueva.Tipo, ref transaccionesAgrupadas);

                    if (transaccion != null)
                    {
                        transaccionesAgrupadas.Find(tr => tr.Clave == transaccion.Clave && tr.Tipo == transaccion.Tipo).Importe += transaccionNueva.Importe;
                    }
                    else
                        transaccionesAgrupadas.Add(transaccionNueva);
                }
            }

            return transaccionesAgrupadas.ToArray();
        }

        private DatosProcesamiento.CreditosFiscalesConceptoTransaccion ExisteTransaccion(string clave, string tipo, ref List<DatosProcesamiento.CreditosFiscalesConceptoTransaccion> transaccionesAgrupadas)
        {
            return transaccionesAgrupadas.Find(tr => tr.Clave == clave && tr.Tipo == tipo);
        }

        private DatosProcesamiento.CreditosFiscalesConcepto ExisteConcepto(string clave, int ejercicio, string fechaCausacion, string periodo, string periodicidad, bool esCorrecto, ref List<DatosProcesamiento.CreditosFiscalesConcepto> conceptosDyPAgrupados)
        {
            return conceptosDyPAgrupados.Find(c => c.Clave == clave && c.Periodo == periodo && c.TipoPeriodicidad == periodicidad && c.Ejercicio == ejercicio && c.FechaCausacion == fechaCausacion && c.EsCorrecto == esCorrecto);
        }

        /// <summary>
        /// Obtiene los importes de los conceptos
        /// </summary>
        /// <param name="conceptos">Colección de conceptos a completar</param>
        /// <returns></returns>
        private Respuesta<Concepto[]> obtieneImportesConceptos(Concepto[] conceptos, int IdTipoDocumento)
        {
            Respuesta<Concepto[]> resultadoImportes = new Respuesta<Concepto[]>();

            try
            {
                foreach (Concepto concepto in conceptos)
                {
                    concepto.ImporteTotalAbonos = concepto.Transacciones.Where(ab => ab.Tipo.ToUpper() == "ABONO").Sum(ab => ab.Valor);
                    concepto.ImporteTotalCargos = concepto.Transacciones.Where(ab => ab.Tipo.ToUpper() == "CARGO").Sum(ab => ab.Valor);

                    if (concepto.ImporteTotalAbonos == 0 && IdTipoDocumento == 22)
                        concepto.ImporteTotalAbonos = null;

                    if (concepto.DatosIcep.Ejercicio == "0")
                        concepto.DatosIcep.Ejercicio = null;
                }

                resultadoImportes.Entidad = conceptos;
                resultadoImportes.EsExitoso = true;
            }
            catch (Exception ex)
            {
                resultadoImportes.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.ObtenerFolio), ex);
            }

            return resultadoImportes;
        }

        /// <summary>
        /// Deserializa solicitud DyP
        /// </summary>
        /// <param name="documento">Documento a deserializar</param>
        /// <returns></returns>
        private SolicitudGeneracionLC DeserializaSolicitudDyP(string documento)
        {
            SolicitudGeneracionLC oSolicitud = new SolicitudGeneracionLC();

            try
            {
                UTF8Encoding encoding = new UTF8Encoding();
                using (MemoryStream stream = new MemoryStream(encoding.GetBytes(documento)))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(SolicitudGeneracionLC));
                    oSolicitud = (SolicitudGeneracionLC)serializer.Deserialize(stream);
                }
            }
            catch
            {
                throw;
            }

            return oSolicitud;
        }

        /// <summary>
        /// Serializa una solicitud a DyP
        /// </summary>
        /// <param name="solicitud">Documento a deserializar</param>
        /// <returns></returns>
        private string SerializaSolicitudDyP(SolicitudGeneracionLC solicitud)
        {
            string sRespuesta = string.Empty;

            try
            {
                using (MemoryStream streamReporte = new MemoryStream())
                {
                    XmlSerializer serializadorReporte = new XmlSerializer(solicitud.GetType());

                    serializadorReporte.Serialize(streamReporte, solicitud);
                    streamReporte.Seek(0, SeekOrigin.Begin);

                    XmlDocument docRespuesta = new XmlDocument();
                    docRespuesta.Load(streamReporte);

                    sRespuesta = docRespuesta.InnerXml;
                }
            }
            catch
            {
                throw;
            }

            return sRespuesta;
        }

        /// <summary>
        /// Actualiza los datos generales de un procesamiento
        /// </summary>
        /// <param name="IdProcesamiento">Id del procesamiento</param>
        /// <param name="FolioDyP">Folio DyP</param>
        /// <param name="LineaCaptura">Línea de captura</param>
        /// <param name="Exito">true si el proceso fue exitoso</param>
        /// <returns></returns>
        private RespuestaGenerica actualizaDatosGenerales(Guid IdProcesamiento, string FolioDyP, string LineaCaptura, bool Exito, decimal TiempoServicio)
        {
            RespuestaGenerica resultadoRegistro = new RespuestaGenerica();

            try
            {
                resultadoRegistro.EsExitoso = DalDatosGenerales.ActualizaDatosGenerales(IdProcesamiento, FolioDyP, LineaCaptura, Exito, TiempoServicio);
            }
            catch (Exception ex)
            {
                resultadoRegistro.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.RegistraProcesamiento), ex);
            }

            return resultadoRegistro;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

       
    }
}
