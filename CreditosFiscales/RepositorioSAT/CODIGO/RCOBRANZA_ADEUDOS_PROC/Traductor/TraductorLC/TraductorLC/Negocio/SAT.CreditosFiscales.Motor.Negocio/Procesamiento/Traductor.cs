//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.Negocio:SAT.CreditosFiscales.Motor.Negocio.Traductor:1:12/07/2012[Assembly:1.0:12/07/2013])

#region Referencias de sistema
using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Collections.Generic;
#endregion

#region Referencias personalizadas.
using SAT.CreditosFiscales.Motor.Entidades.Esquemas;
using SAT.CreditosFiscales.Motor.Entidades.Catalogos;
using SAT.CreditosFiscales.Motor.Entidades.AdmonRegla;
using SAT.CreditosFiscales.Motor.Entidades.Herramientas;
using SAT.CreditosFiscales.Motor.Entidades.Procesamiento;
using SAT.CreditosFiscales.Motor.Entidades.ManejoErrores;
using SAT.CreditosFiscales.Motor.Entidades.CodigosError;
using SAT.CreditosFiscales.Motor.Entidades.DatosProcesamiento;

using SAT.CreditosFiscales.Motor.AccesoDatos.Esquemas;
using SAT.CreditosFiscales.Motor.AccesoDatos.AdmonReglas;
using SAT.CreditosFiscales.Motor.AccesoDatos.Catalogos;
using SAT.CreditosFiscales.Motor.AccesoDatos.Busquedas;
using SAT.CreditosFiscales.Motor.AccesoDatos.Procesamiento;
using SAT.CreditosFiscales.Motor.AccesoDatos.Informacion;
using SAT.CreditosFiscales.Motor.AccesoDatos.ManejoErrores;

using SAT.CreditosFiscales.Motor.Negocio.Esquemas;
using SAT.CreditosFiscales.Motor.Negocio.Mensajes;
using SAT.CreditosFiscales.Motor.Negocio.AdmonReglas;
using SAT.CreditosFiscales.Motor.Negocio.RecepcionLC;
using SAT.CreditosFiscales.Motor.Negocio.DatosCache;
using SAT.CreditosFiscales.Motor.Negocio.AccesoLogEventos;

using SAT.CreditosFiscales.Motor.Utilidades;

using Enumeraciones = SAT.CreditosFiscales.Motor.Entidades.Enumeraciones;
using DatosProcesamiento = SAT.CreditosFiscales.Motor.Entidades.DatosProcesamiento;
#endregion

namespace SAT.CreditosFiscales.Motor.Negocio.Procesamiento
{
	/// <summary>
	/// 
	/// </summary>
	public class Traductor : IDisposable
	{
		/// <summary>
		/// Método principal del procesamiento que realiza transformaciones, validaciones, generación de línea de captura y generación de PDF.
		/// </summary>
		/// <param name="idAplicacion">Identificador único (ID) de la Aplicación que envía la petición.</param>
		/// <param name="idTipoDoc">Identificador único (ID) del Tipo de Documento que se envía.</param>
		/// <param name="documento">Documento en formato XML con los datos necesarios para generar la Línea de Captura.</param>
		/// <param name="generaPDF">True si se desea generar el archivo de imagen con formato PDF.</param>
		/// <returns>Cadena de caracter de RespuestaLC serializado</returns>
		public string EjecutaMotor(short idAplicacion, short idTipoDoc, string documento, bool generaPDF)
		{
			string sRespuestaGeneral = string.Empty;

			try
			{
				var vRespuestaLC = new RespuestaLC();
				Respuesta<ResultadoReglas> oResultadoValidacion = this.GeneraLineaCaptura(idAplicacion, idTipoDoc, documento, ref vRespuestaLC);
				vRespuestaLC.IdProcesamiento = oResultadoValidacion.Entidad.IdProcesamiento.ToString();

				#region Genera PDF línea captura si es requerido
					if (generaPDF && oResultadoValidacion.EsExitoso)
					{
						var stopWatch = new Stopwatch();
						stopWatch.Start();
							var vRespuestaPdf = new Respuesta<RespuestaLC>();
							vRespuestaPdf = this.ObtienePDF(vRespuestaLC, idAplicacion, idTipoDoc, documento);
							//File.WriteAllBytes("C:\\Temp\\prueba.pdf", vRespuestaPdf.Entidad.PDF);
							vRespuestaLC = vRespuestaPdf.Entidad;
						stopWatch.Stop();

						BitacoraProcesamiento.GuardaBitacora(
																oResultadoValidacion.Entidad
																, Enumeraciones.PasosTraductor.GeneraPDF
																, escribirMensaje: false
																, observaciones: string.Format("La generación del PDF fue: {0}", vRespuestaPdf.EsExitoso == true ? "Exitoso" : "No Exitoso")
																, listaErrores: vRespuestaPdf.ListaErrores
																, duracion: Convert.ToDecimal(stopWatch.Elapsed.TotalSeconds)
															);

						if (!vRespuestaPdf.EsExitoso)
						{
							oResultadoValidacion.Entidad.PasoProceso = Enumeraciones.PasosTraductor.GeneraPDF;
							oResultadoValidacion.EsExitoso = false;
							vRespuestaLC.TipoRespuesta = 0;
							oResultadoValidacion.ListaErrores.AddRange(vRespuestaPdf.ListaErrores);
						}
					}
				#endregion

				//Construye mensaje de salida
				sRespuestaGeneral = ConstruyeMensajeSalida(oResultadoValidacion, vRespuestaLC);
				//oResultadoValidacion.Dispose();
			}
			catch (Exception eError)
			{
				sRespuestaGeneral = "<RespuestaLC><ListaErrores><Error>" + eError.ToString() + "</Error></ListaErrores><TipoRespuesta>0</TipoRespuesta><IdProcesamiento></IdProcesamiento></RespuestaLC>";
			}

			return sRespuestaGeneral;
		}

		/// <summary>
		/// Método principal del procesamiento que realiza transformaciones, validaciones, generación de línea de captura y generación de PDF.
		/// </summary>
		/// <param name="pshIdAplicacion">Identificador único (ID) de la Aplicación que envía la petición.</param>
		/// <param name="pshIdTipoDoc">Identificador único (ID) del Tipo de Documento que se envía.</param>
		/// <param name="psDocumento">Documento en formato XML con los datos necesarios para generar la Línea de Captura.</param>
		/// <param name="pboGeneraPng">True si se desea generar el archivo de imagen con formato PNG.</param>
		/// <returns>Cadena de caracter de RespuestaLC serializado</returns>
		public string EjecutaMotorImagen(short pshIdAplicacion, short pshIdTipoDoc, string psDocumento, bool pboGeneraPng)
		{
			/*
			 * ----------------------------------------
			 * Creado por: Mario Escarpulli 
			 * Creado: 15/Junio/2019
			 * NOTAS: Esta clase se creó con base al Requerimiento de Servicio (RES)
			 *        CZA_CREDFIS: Nuevo Servicio en Motor Traductor para la entrega de sección de línea de captura.
			 * ----- Historial de actualizaciones -----
			 * Actualizado por: Mario Escarpulli
			 * Actualizado el: 20/Junio/2019
			 * ----------------------------------------
			*/
			string sRespuestaGeneral = string.Empty;

			try
			{
				var voRespuestaLC = new RespuestaLC();
				Respuesta<ResultadoReglas> oResultadoValidacion = this.GeneraLineaCaptura(pshIdAplicacion, pshIdTipoDoc, psDocumento, ref voRespuestaLC);
				voRespuestaLC.IdProcesamiento = oResultadoValidacion.Entidad.IdProcesamiento.ToString();

				#region Genera PNG Línea de Captura si es requerido.
					if (pboGeneraPng && oResultadoValidacion.EsExitoso)
					{
						var vswMedicionDeTiempo = new Stopwatch();
						vswMedicionDeTiempo.Start();
							var vRespuestaPng = new Respuesta<RespuestaLC>();
							vRespuestaPng = this.ObtienePNG(voRespuestaLC, pshIdAplicacion, pshIdTipoDoc, psDocumento);
							//File.WriteAllBytes("C:\\Temp\\MAAM710815_44331900000001.png", vRespuestaPng.Entidad.PNG);
							voRespuestaLC = vRespuestaPng.Entidad;
						vswMedicionDeTiempo.Stop();

						BitacoraProcesamiento.GuardaBitacora(
																oResultadoValidacion.Entidad
																, Enumeraciones.PasosTraductor.GeneraPNG
																, escribirMensaje: false
																, observaciones: string.Format("La generación del PNG fue: {0}", vRespuestaPng.EsExitoso == true ? "Exitoso" : "No Exitoso")
																, listaErrores: vRespuestaPng.ListaErrores
																, duracion: Convert.ToDecimal(vswMedicionDeTiempo.Elapsed.TotalSeconds)
															);

						if (!vRespuestaPng.EsExitoso)
						{
							oResultadoValidacion.Entidad.PasoProceso = Enumeraciones.PasosTraductor.GeneraPNG;
							oResultadoValidacion.EsExitoso = false;
							voRespuestaLC.TipoRespuesta = 0;
							oResultadoValidacion.ListaErrores.AddRange(vRespuestaPng.ListaErrores);
						}
					}
				#endregion

				// Construye mensaje de salida
				sRespuestaGeneral = this.ConstruyeMensajeSalida(oResultadoValidacion, voRespuestaLC);
				//oResultadoValidacion.Dispose();
			}
			catch (Exception eError)
			{
				sRespuestaGeneral = "<RespuestaLC><ListaErrores><Error>" + eError.ToString() + "</Error></ListaErrores><TipoRespuesta>0</TipoRespuesta><IdProcesamiento></IdProcesamiento></RespuestaLC>";
			}

			return sRespuestaGeneral;
		}

		/// <summary>
		/// Lógica de validaciones, transformaciones y generación de Línea de Captura.
		/// </summary>
		/// <param name="pshIdAplicacion">Identificador único de la Aplicación que envía la petición.</param>
		/// <param name="pshIdTipoDoc">Identificador único del Tipo de Documento que se envía.</param>
		/// <param name="psDocumento">Documento en formato XML con los datos necesarios para generar la Línea de Captura.</param>
		/// <param name="respuestaLC">Objeto de respuesta LC inicializada.</param>
		/// <returns>Objeto con sus propiedades y valores.</returns>
		private Respuesta<ResultadoReglas> GeneraLineaCaptura(short pshIdAplicacion, short pshIdTipoDoc, string psDocumento, ref RespuestaLC respuestaLC)
		{
			var voResultadoValidacion     = new Respuesta<ResultadoReglas> { Entidad = new ResultadoReglas(psDocumento, pshIdAplicacion, pshIdTipoDoc) };
			var voResultadoValidacionSiat = new Respuesta<ResultadoReglas> { Entidad = new ResultadoReglas(psDocumento, pshIdAplicacion, pshIdTipoDoc) };
			voResultadoValidacionSiat.Entidad.IdProcesamiento = voResultadoValidacion.Entidad.IdProcesamiento;

			Contrato oContratoActual = null;
			var vsbObservaciones = new StringBuilder();
			var voReglasNegocio = new Ejecucion();
			var voSolicitud = new SolicitudGeneracionLC();
			string sRespuestaGeneral = string.Empty;
			var vswMedicionDeTiempo = new Stopwatch();

			// Validando que el contenido del mensaje no este vacío.
			if (psDocumento.Trim().Length <= 0)
				voResultadoValidacion.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.ErrorMensajeVacio));

			#region Registra Mensaje en Bitácora
				BitacoraProcesamiento.GuardaBitacora(
														voResultadoValidacion.Entidad
														, Enumeraciones.PasosTraductor.RecibePeticion
														, observaciones: "Mensaje de entrada al traductor."
														, listaErrores: voResultadoValidacion.ListaErrores
													);
			#endregion

			if (psDocumento.Trim().Length <= 0)
			{
				voResultadoValidacion.Entidad.PasoProceso = Enumeraciones.PasosTraductor.RecibePeticion;
				return voResultadoValidacion;
			}

			#region Obtiene recursos de validación antes de realizar inserción a base de datos
				vswMedicionDeTiempo.Start();
					Respuesta<ReglasPorDocumento> oRespuesta = voReglasNegocio.ConsultaReglas(pshIdAplicacion, pshIdTipoDoc, true, false);
					Respuesta<ReglasPorDocumento> oRespuestaSiat = voReglasNegocio.ConsultaReglas(pshIdAplicacion, pshIdTipoDoc, true, true);
				vswMedicionDeTiempo.Stop();

				// Guardando la información procesada en la Bitácora.
				BitacoraProcesamiento.GuardaBitacora(
														voResultadoValidacion.Entidad
														, Enumeraciones.PasosTraductor.ObtieneReglas
														, escribirMensaje: false
														, observaciones: null
														, listaErrores: oRespuesta.ListaErrores
														, duracion: Convert.ToDecimal(vswMedicionDeTiempo.Elapsed.TotalSeconds)
													);

				BitacoraProcesamiento.GuardaBitacora(
														voResultadoValidacionSiat.Entidad
														, Enumeraciones.PasosTraductor.ObtieneReglasSIAT
														, escribirMensaje: false
														, observaciones: null
														, listaErrores: oRespuestaSiat.ListaErrores
														, duracion: Convert.ToDecimal(vswMedicionDeTiempo.Elapsed.TotalSeconds)
													);

				if (!oRespuesta.EsExitoso)
				{
					voResultadoValidacion.Entidad.PasoProceso = Enumeraciones.PasosTraductor.ObtieneReglas;
					voResultadoValidacion.ListaErrores.AddRange(oRespuesta.ListaErrores);
					return voResultadoValidacion;
				}
			#endregion

			#region Verifica si existe XSD de validación
				oContratoActual = oRespuesta.Entidad.ListaContratos.Single(c => c.DireccionContrato == Enumeraciones.DireccionTraductor.Entrada);

				if (oContratoActual == null)
				{
					voResultadoValidacion.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.ContratoEntrada));
					return voResultadoValidacion;
				}
			#endregion

			#region Valida mensaje de entrada
				vswMedicionDeTiempo.Restart();
					// Valida estructura del mensaje de entrada.
					var voValidacionXsd = new ValidaXSD();
					RespuestaGenerica voRespuestaValidacionXsd = voValidacionXsd.ValidaEsquemaXSD(psDocumento, oContratoActual.Xsd, oContratoActual.TargetNamespace);
				vswMedicionDeTiempo.Stop();

				voResultadoValidacion.Duracion = Convert.ToDecimal(vswMedicionDeTiempo.Elapsed.TotalSeconds);

				#region Registra Mensaje en Bitácora
					vsbObservaciones.Clear();
					vsbObservaciones.AppendFormat("Se validó el mensaje de entrada al motor con el esquema {0}: {1}", oContratoActual.IdEsquema, oContratoActual.Descripcion);

					BitacoraProcesamiento.GuardaBitacora(
															voResultadoValidacion.Entidad
															, Enumeraciones.PasosTraductor.ValidaXSDEntrada
															, escribirMensaje: false
															, observaciones: vsbObservaciones.ToString()
															, listaErrores: voRespuestaValidacionXsd.ListaErrores
															, duracion: Convert.ToDecimal(vswMedicionDeTiempo.Elapsed.TotalSeconds)
														);
				#endregion

				if (!voRespuestaValidacionXsd.EsExitoso)
				{
					voResultadoValidacion.Entidad.PasoProceso = Enumeraciones.PasosTraductor.ValidaXSDEntrada;
					voResultadoValidacion.ListaErrores.AddRange(voRespuestaValidacionXsd.ListaErrores);
					return voResultadoValidacion;
				}
			#endregion

			#region Aplica reglas de validación y transformación antes de canónico
				if (oRespuesta.Entidad != null && oRespuesta.Entidad.ListaReglas != null && oRespuesta.Entidad.ListaReglas.Count > 0)
				{
					vswMedicionDeTiempo.Restart();
						// Aplica reglas de validación
						voReglasNegocio.AplicaReglas(psDocumento, oRespuesta.Entidad, voResultadoValidacion);
						//Transformacion para SIAT
						voReglasNegocio.AplicaReglas(psDocumento, oRespuestaSiat.Entidad, voResultadoValidacionSiat);
						vsbObservaciones.Clear();
						vsbObservaciones.Append("Regla(s) aplicada(s): ");

						voResultadoValidacion.Entidad.Reglas.Select
						(
							r =>
								{
									vsbObservaciones.AppendFormat("{0}. {1} ({2} {3}). ", r.IdRegla, r.Descripcion, r.EsValidacion ? "Validación" : "Transformación", r.FueExitosa ? "sin errores" : "con errores");
									return r;
								}
						).ToList();
					vswMedicionDeTiempo.Stop();

					BitacoraProcesamiento.GuardaBitacora
														(
															voResultadoValidacion.Entidad
															, Enumeraciones.PasosTraductor.AplicaReglasAntesCanonico
															, observaciones: vsbObservaciones.ToString()
															, listaErrores: voResultadoValidacion.ListaErrores
															, duracion: Convert.ToDecimal(vswMedicionDeTiempo.Elapsed.TotalSeconds)
														);

					vsbObservaciones.Clear();
					vsbObservaciones.Append("Regla(s) aplicada(s): ");
					voResultadoValidacionSiat.Entidad.Reglas.Select
					(
						r =>
							{
								vsbObservaciones.AppendFormat("{0}. {1} ({2} {3}). ", r.IdRegla, r.Descripcion, r.EsValidacion ? "Validación" : "Transformación", r.FueExitosa ? "sin errores" : "con errores");
								return r;
							}
					).ToList();

					BitacoraProcesamiento.GuardaBitacora
														(
															voResultadoValidacionSiat.Entidad
															, Enumeraciones.PasosTraductor.AplicaReglasAntesCanonicoSIAT
															, observaciones: vsbObservaciones.ToString()
															, listaErrores: voResultadoValidacionSiat.ListaErrores
															, duracion: Convert.ToDecimal(vswMedicionDeTiempo.Elapsed.TotalSeconds)
														);

					if (!voResultadoValidacion.EsExitoso)
					{
						voResultadoValidacion.Entidad.PasoProceso = Enumeraciones.PasosTraductor.AplicaReglasAntesCanonico;
						voResultadoValidacion.EsExitoso = false;
						return voResultadoValidacion;
					}

					if (!voResultadoValidacionSiat.EsExitoso)
					{
						voResultadoValidacionSiat.Entidad.PasoProceso = Enumeraciones.PasosTraductor.AplicaReglasAntesCanonico;
						voResultadoValidacionSiat.EsExitoso = false;
						return voResultadoValidacionSiat;
					}
				}
			#endregion

			#region Completa transformación Xml Canónico
				vswMedicionDeTiempo.Restart();
					var oCreditosFiscales = new DatosProcesamiento.CreditosFiscales();
					Respuesta<DatosProcesamiento.CreditosFiscales> oRespuestaCompletaCanonico = this.CompletaCanonico(voResultadoValidacion.Entidad.Mensaje);
					Respuesta<DatosProcesamiento.CreditosFiscalesSIAT> oRespuestaCompletaCanonicoSiat = this.CompletaCanonicoSIAT(voResultadoValidacionSiat.Entidad.Mensaje);
				vswMedicionDeTiempo.Stop();

				vsbObservaciones.Clear();
				BitacoraProcesamiento.GuardaBitacora(
														voResultadoValidacion.Entidad
														, Enumeraciones.PasosTraductor.CompletaTransformacionCanonica
														, observaciones: vsbObservaciones.AppendFormat("Completa mensaje canónico. Resultado: {0}", oRespuestaCompletaCanonico.EsExitoso == true ? "Exitoso" : "Falló").ToString()
														, listaErrores: oRespuestaCompletaCanonico.ListaErrores
														, duracion: Convert.ToDecimal(vswMedicionDeTiempo.Elapsed.TotalSeconds)
													);

				vsbObservaciones.Clear();
				BitacoraProcesamiento.GuardaBitacora
													(
														voResultadoValidacionSiat.Entidad
														, Enumeraciones.PasosTraductor.CompletaTransformacionCanonicaSIAT
														, observaciones: vsbObservaciones.AppendFormat("Completa mensaje canónico SIAT. Resultado: {0}", oRespuestaCompletaCanonico.EsExitoso == true ? "Exitoso" : "Falló").ToString()
														, listaErrores: oRespuestaCompletaCanonicoSiat.ListaErrores
														, duracion: Convert.ToDecimal(vswMedicionDeTiempo.Elapsed.TotalSeconds)
													);

				if (!oRespuestaCompletaCanonico.EsExitoso)
				{
					voResultadoValidacion.Entidad.PasoProceso = Enumeraciones.PasosTraductor.CompletaTransformacionCanonica;
					voResultadoValidacion.ListaErrores.AddRange(oRespuestaCompletaCanonico.ListaErrores);
					voResultadoValidacion.EsExitoso = false;
					return voResultadoValidacion;
				}

				voResultadoValidacion.Entidad.Mensaje = MetodosComunes.SerlializaCreditosFiscales(oRespuestaCompletaCanonico.Entidad);
			#endregion

			#region Valida la lista de conceptos
				if (oRespuestaCompletaCanonico.Entidad.DatosGenerales.LineasCaptura.DatosLineaCaptura[0].PagoObligadoInternet != 0)
				{
					vswMedicionDeTiempo.Restart();
					Respuesta<bool> oRespuestaValidaTipoPersonaPeriodicidad = ValidaConceptoYPeriodicidadParaTipoPersona(pshIdAplicacion, pshIdTipoDoc, voResultadoValidacion.Entidad.IdProcesamiento, oRespuestaCompletaCanonico);
					vswMedicionDeTiempo.Stop();

					vsbObservaciones.Clear();
					BitacoraProcesamiento.GuardaBitacora
														(
															voResultadoValidacion.Entidad
															, Enumeraciones.PasosTraductor.ValidaConceptoPeriodicidadPorTipoPersona
															, escribirMensaje: false
															, observaciones: vsbObservaciones.AppendFormat("Completa validación concepto y periodicidad para TipoPersona. Resultado: {0}", oRespuestaValidaTipoPersonaPeriodicidad.EsExitoso == true ? "Exitoso" : "Falló").ToString()
															, listaErrores: oRespuestaValidaTipoPersonaPeriodicidad.ListaErrores
															, duracion: Convert.ToDecimal(vswMedicionDeTiempo.Elapsed.TotalSeconds)
														);

					if (!oRespuestaValidaTipoPersonaPeriodicidad.EsExitoso)
					{
						voResultadoValidacion.Entidad.PasoProceso = Enumeraciones.PasosTraductor.ValidaConceptoPeriodicidadPorTipoPersona;
						voResultadoValidacion.EsExitoso = false;
						voResultadoValidacion.ListaErrores.AddRange(oRespuestaValidaTipoPersonaPeriodicidad.ListaErrores);
						return voResultadoValidacion;
					}
				}
			#endregion

			#region Agrupación de Conceptos DyP
				vswMedicionDeTiempo.Restart();
					//CreditosFiscalesConcepto[] conceptosSIAT = new CreditosFiscalesConcepto[respuestaCompletaCanonico.Entidad.Conceptos.Count()];
					//Array.(respuestaCompletaCanonico.Entidad.Conceptos, conceptosSIAT, conceptosSIAT.Count());

					DatosProcesamiento.CreditosFiscalesConcepto[] oConceptosSiat = Utilidades.GenericCopier<DatosProcesamiento.CreditosFiscalesConcepto[]>.DeepCopy(oRespuestaCompletaCanonico.Entidad.Conceptos);
					Respuesta<DatosProcesamiento.CreditosFiscales> respuestaAgrupacion = this.GeneraAgrupacionConceptosDyP(oRespuestaCompletaCanonico.Entidad);
					voResultadoValidacion.Entidad.Mensaje = MetodosComunes.SerlializaCreditosFiscales(respuestaAgrupacion.Entidad);
				vswMedicionDeTiempo.Stop();

				vsbObservaciones.Clear();
				BitacoraProcesamiento.GuardaBitacora(
														voResultadoValidacion.Entidad
														, Enumeraciones.PasosTraductor.AgrupacionConceptos
														, observaciones: vsbObservaciones.AppendFormat("Agrupa mensaje canónico. Resultado: {0}", respuestaAgrupacion.EsExitoso == true ? "Exitoso" : "Falló").ToString()
														, listaErrores: respuestaAgrupacion.ListaErrores
														, duracion: Convert.ToDecimal(vswMedicionDeTiempo.Elapsed.TotalSeconds)
													);

				if (!voResultadoValidacion.EsExitoso)
				{
					voResultadoValidacion.ListaErrores.AddRange(respuestaAgrupacion.ListaErrores);
					return voResultadoValidacion;
				}
			#endregion

			#region Búsqueda si existe una línea previamente generada
				vswMedicionDeTiempo.Restart();
					Respuesta<RespuestaLC> oRespuestaExisteLC = BusquedaLineas.BusquedaLC(oRespuestaCompletaCanonico.Entidad);
					//respuestaCompletaCanonico.Entidad.DatosGenerales.NumeroParcialidad
				vswMedicionDeTiempo.Stop();

				vsbObservaciones.Clear();
				BitacoraProcesamiento.GuardaBitacora(
														voResultadoValidacion.Entidad
														, Enumeraciones.PasosTraductor.BuscaLineaExistenteBD
														, false
														, observaciones: vsbObservaciones.AppendFormat("Búsqueda de Línea de captura previamente generada, resultado de la búsqueda: {0} {1}", oRespuestaExisteLC.ExisteLineaCaptura == true ? "Existe Línea de Captura" : "No existe Línea de Captura", oRespuestaExisteLC.ExisteLineaCaptura == true && oRespuestaExisteLC.Entidad != null ? "Folio: " + oRespuestaExisteLC.Entidad.LineasCaptura[0].Folio : string.Empty).ToString()
														, listaErrores: oRespuestaExisteLC.ListaErrores
														, duracion: Convert.ToDecimal(vswMedicionDeTiempo.Elapsed.TotalSeconds)
													);

				if (!oRespuestaExisteLC.EsExitoso)
				{
					voResultadoValidacion.ListaErrores.AddRange(oRespuestaExisteLC.ListaErrores);
					voResultadoValidacion.EsExitoso = false;
					return voResultadoValidacion;
				}

				if (oRespuestaExisteLC.ExisteLineaCaptura)
				{
					voResultadoValidacion.Entidad.PasoProceso = Enumeraciones.PasosTraductor.BuscaLineaExistenteBD;
					respuestaLC = oRespuestaExisteLC.Entidad;
					return voResultadoValidacion;
				}
			#endregion

			#region Inserta datos de XML Canónico a BD
				vswMedicionDeTiempo.Restart();
					var oLogAlmacena = new LogicaAlmacena(voResultadoValidacion.Entidad.Mensaje, voResultadoValidacion.Entidad.IdAplicacion, voResultadoValidacion.Entidad.IdProcesamiento);
					Respuesta<string> oResultadoInsersion = oLogAlmacena.AlmacenaDatosBD(oRespuestaCompletaCanonico.Entidad);
				vswMedicionDeTiempo.Stop();

				vsbObservaciones.Clear();
				BitacoraProcesamiento.GuardaBitacora
													(
														voResultadoValidacion.Entidad
														, Enumeraciones.PasosTraductor.InsertaDatosCanonicoDB
														, false
														, observaciones: vsbObservaciones.AppendFormat("Se inserta mensaje canónico por campos a BD : {0}", oResultadoInsersion.EsExitoso.ToString()).ToString()
														, listaErrores: oResultadoInsersion.ListaErrores
														, duracion: Convert.ToDecimal(vswMedicionDeTiempo.Elapsed.TotalSeconds)
													);

				if (!oResultadoInsersion.EsExitoso)
				{
					voResultadoValidacion.ListaErrores.AddRange(oResultadoInsersion.ListaErrores);
					voResultadoValidacion.EsExitoso = false;
					return voResultadoValidacion;
				}

				// SIAT
				if (oRespuestaCompletaCanonicoSiat.Entidad.DatosGenerales.EsSIAT)
				{
					vswMedicionDeTiempo.Restart();
						Respuesta<string> oResultadoSIATInsersion = oLogAlmacena.AlmacenaDatosBDSIAT(oRespuestaCompletaCanonicoSiat.Entidad, oConceptosSiat);
					vswMedicionDeTiempo.Stop();

					vsbObservaciones.Clear();
					BitacoraProcesamiento.GuardaBitacora
														(
															voResultadoValidacionSiat.Entidad
															, Enumeraciones.PasosTraductor.InsertaDatosCanonicoDBSIAT
															, false
															, observaciones: vsbObservaciones.AppendFormat("Se inserta mensaje canónico por campos a BD para SIAT: {0}"
															, oResultadoInsersion.EsExitoso.ToString()).ToString()
															, listaErrores: oResultadoSIATInsersion.ListaErrores
															, duracion: Convert.ToDecimal(vswMedicionDeTiempo.Elapsed.TotalSeconds)
														);

					if (!oResultadoSIATInsersion.EsExitoso)
					{
						voResultadoValidacionSiat.ListaErrores.AddRange(oResultadoSIATInsersion.ListaErrores);
						voResultadoValidacionSiat.EsExitoso = false;
						return voResultadoValidacionSiat;
					}
				}
			#endregion

			#region Obtiene recursos de validación después de realizar inserción a base de datos
				Respuesta<ReglasPorDocumento> oRespuestaDespues = voReglasNegocio.ConsultaReglas(pshIdAplicacion, pshIdTipoDoc, false, false);

				if (!oRespuesta.EsExitoso)
				{
					voResultadoValidacion.ListaErrores.AddRange(oRespuesta.ListaErrores);
					return voResultadoValidacion;
				}
			#endregion

			#region Aplica reglas de validación y transformación después de inserción
				if (oRespuesta.Entidad != null && oRespuesta.Entidad.ListaReglas != null && oRespuesta.Entidad.ListaReglas.Count > 0)
				{
					vswMedicionDeTiempo.Restart();
						// Aplica reglas de validación
						voReglasNegocio.AplicaReglas(voResultadoValidacion.Entidad.Mensaje, oRespuestaDespues.Entidad, voResultadoValidacion);

						vsbObservaciones.Clear();
						vsbObservaciones.Append("Regla(s) aplicada(s): ");

						voResultadoValidacion.Entidad.Reglas.Select
							(
								r =>
									{
										vsbObservaciones.AppendFormat("{0}. {1} ({2} {3}). ", r.IdRegla, r.Descripcion, r.EsValidacion ? "Validación" : "Transformación", r.FueExitosa ? "sin errores" : "con errores");
										return r;
									}
							).ToList();
					vswMedicionDeTiempo.Stop();

					BitacoraProcesamiento.GuardaBitacora
														(
															voResultadoValidacion.Entidad
															, Enumeraciones.PasosTraductor.AplicaReglasDespuesCanonico
															, observaciones: vsbObservaciones.ToString()
															, listaErrores: voResultadoValidacion.ListaErrores
															, duracion: Convert.ToDecimal(vswMedicionDeTiempo.Elapsed.TotalSeconds)
														);
				}

				if (!voResultadoValidacion.EsExitoso)
				{
					voResultadoValidacion.Entidad.PasoProceso = Enumeraciones.PasosTraductor.AplicaReglasDespuesCanonico;
					voResultadoValidacion.EsExitoso = false;
					return voResultadoValidacion;
				}
			#endregion

			#region CompletarEsquemaDyP
				vswMedicionDeTiempo.Restart();
					voSolicitud = this.CompletarEsquemaDyP(ref voResultadoValidacion, oRespuestaCompletaCanonico.Entidad.DatosGenerales.ALR);
					voResultadoValidacion.Entidad.Mensaje = this.SerializaSolicitudDyP(voSolicitud);
				vswMedicionDeTiempo.Stop();

				vsbObservaciones.Clear();
				BitacoraProcesamiento.GuardaBitacora(
														voResultadoValidacion.Entidad
														, Enumeraciones.PasosTraductor.CompletaEsquemaDyP
														, true
														, observaciones: vsbObservaciones.AppendFormat("Se completa mensaje DyP para generación de Línea de Captura : {0}", voResultadoValidacion.EsExitoso.ToString()).ToString()
														, listaErrores: voResultadoValidacion.ListaErrores
														, duracion: Convert.ToDecimal(vswMedicionDeTiempo.Elapsed.TotalSeconds)
													);

				if (!voResultadoValidacion.EsExitoso)
				{
					voResultadoValidacion.Entidad.PasoProceso = Enumeraciones.PasosTraductor.CompletaEsquemaDyP;
					voResultadoValidacion.EsExitoso = false;
					return voResultadoValidacion;
				}
			#endregion

			#region Valida mensaje que se va a enviar a DyP
				vswMedicionDeTiempo.Restart();
					oContratoActual = oRespuesta.Entidad.ListaContratos.Single(c => c.DireccionContrato == Enumeraciones.DireccionTraductor.Salida);
					if (oContratoActual == null)
					{
						voResultadoValidacion.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.ContratoDyP));
						return voResultadoValidacion;
					}

					// Valida estructura del mensaje de entrada a DyP.
					RespuestaGenerica oValidacionSalida = voValidacionXsd.ValidaEsquemaXSD(voResultadoValidacion.Entidad.Mensaje, oContratoActual.Xsd, oContratoActual.TargetNamespace);
				vswMedicionDeTiempo.Stop();

				vsbObservaciones.Clear();
				vsbObservaciones.AppendFormat("Se validó el mensaje de salida al motor con el esquema {0}: {1} - Exitoso: {2}", oContratoActual.IdEsquema, oContratoActual.Descripcion, oValidacionSalida.EsExitoso.ToString());

				BitacoraProcesamiento.GuardaBitacora
													(
														voResultadoValidacion.Entidad
														, Enumeraciones.PasosTraductor.ValidaXSDSalida
														, observaciones: vsbObservaciones.ToString()
														, listaErrores: oValidacionSalida.ListaErrores
														, duracion: Convert.ToDecimal(vswMedicionDeTiempo.Elapsed.TotalSeconds)
													);

				if (!oValidacionSalida.EsExitoso)
				{
					voResultadoValidacion.Entidad.PasoProceso = Enumeraciones.PasosTraductor.ValidaXSDSalida;
					voResultadoValidacion.EsExitoso = false;
					voResultadoValidacion.ListaErrores.AddRange(oValidacionSalida.ListaErrores);
					return voResultadoValidacion;
				}
			#endregion

			#region Ordena Transacciones
				//Ordenamiento de Transacciones DyP
				vswMedicionDeTiempo.Restart();
					Respuesta<SolicitudGeneracionLC> oRespuestaOrdenamiento = this.OrdenamientoTransacciones(voSolicitud, voResultadoValidacion.Entidad.IdAplicacion, voResultadoValidacion.Entidad.IdTipoDocPago);
					voResultadoValidacion.Entidad.Mensaje = this.SerializaSolicitudDyP(oRespuestaOrdenamiento.Entidad);
				vswMedicionDeTiempo.Stop();

				vsbObservaciones.Clear();
				BitacoraProcesamiento.GuardaBitacora
													(
														voResultadoValidacion.Entidad
														, Enumeraciones.PasosTraductor.OrdenamientoTransacciones
														, escribirMensaje: true
														, observaciones: vsbObservaciones.AppendFormat("Se ordenan transacciones resultado: {0}", oRespuestaOrdenamiento.EsExitoso.ToString()).ToString()
														, listaErrores: oRespuestaOrdenamiento.ListaErrores
														, duracion: Convert.ToDecimal(vswMedicionDeTiempo.Elapsed.TotalSeconds)
													);

				if (!oRespuestaOrdenamiento.EsExitoso)
				{
					voResultadoValidacion.ListaErrores.AddRange(oRespuestaOrdenamiento.ListaErrores);
					return voResultadoValidacion;
				}
			#endregion

			#region Envia mensaje a DyP
				vswMedicionDeTiempo.Restart();
					// Envía Mensaje a DyP
					var oInvocarServicios = new InvocaServicios();
					respuestaLC = oInvocarServicios.InvocaServicioLCDyP(ref voResultadoValidacion, oRespuestaOrdenamiento.Entidad);
				vswMedicionDeTiempo.Stop();

				vsbObservaciones.Clear();
				BitacoraProcesamiento.GuardaBitacora(
														voResultadoValidacion.Entidad
														, Enumeraciones.PasosTraductor.EnviadoDyP
														, escribirMensaje: true
														, observaciones: vsbObservaciones.AppendFormat("Se envía mensaje a DyP resultado: {0}", respuestaLC.TipoRespuesta == 1 ? "ACEPTADO" : "NO ACEPTADO").ToString()
														, listaErrores: voResultadoValidacion.ListaErrores
														, duracion: Convert.ToDecimal(vswMedicionDeTiempo.Elapsed.TotalSeconds)
													);

				if (respuestaLC.ListaErrores != null)
				{
					voResultadoValidacion.EsExitoso = false;
					return voResultadoValidacion;
				}
			#endregion

			// ActualizaDatos Respuesta en BD
			this.actualizaDatosGenerales
										(
											voResultadoValidacion.Entidad.IdProcesamiento
											, voSolicitud.DatosGenerales == null ? string.Empty : voSolicitud.DatosGenerales.Solicitud
											, respuestaLC == null ? string.Empty : (respuestaLC.LineasCaptura == null ? string.Empty : respuestaLC.LineasCaptura[0].LineaCaptura), voResultadoValidacion.EsExitoso, voResultadoValidacion.Duracion
										);

			return voResultadoValidacion;
		}

		/// <summary>
		/// Método para completar mensaje canónico.
		/// </summary>
		/// <param name="documentoCanonico">Documento canónico XML.</param>
		/// <returns>Objeto con propiedades y sus valores.</returns>
		private Respuesta<DatosProcesamiento.CreditosFiscales> CompletaCanonico(string documentoCanonico)
		{
			var voRespuestaCompletaCanonico = new Respuesta<DatosProcesamiento.CreditosFiscales>();

			try
			{
				voRespuestaCompletaCanonico.Entidad = GeneracionEquivalenciaConceptos.GeneraMensajeLineaCaptura(documentoCanonico);
				voRespuestaCompletaCanonico.EsExitoso = true;

				if (voRespuestaCompletaCanonico.Entidad == null)
					voRespuestaCompletaCanonico.EsExitoso = false;
				else
				{
					if (voRespuestaCompletaCanonico.Entidad.DatosGenerales.DeudorPuro == 1)
					{
						voRespuestaCompletaCanonico.Entidad.DatosGenerales.RFCGenerico = Catalogos.AplicationSettings.ConsultaConfiguracion(Constantes.RFCGenerico).ToString();
						voRespuestaCompletaCanonico.Entidad.DatosGenerales.IdContribuyente = Catalogos.AplicationSettings.ConsultaConfiguracion(Constantes.BOIDGenerico).ToString();
					}
				}
			}
			catch (Exception ex)
			{
				voRespuestaCompletaCanonico.EsExitoso = false;
				if (ex.InnerException != null)
					voRespuestaCompletaCanonico.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.CompletaCanonico), new Exception(ex.InnerException.Message));
				else
					voRespuestaCompletaCanonico.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.CompletaCanonico), ex);
			}

			return voRespuestaCompletaCanonico;
		}

		/// <summary>
		/// Método para completar mensaje canónico para SIAT.
		/// </summary>
		/// <param name="psDocumentoCanonico">Documento canónico Xml.</param>
		/// <returns>Objeto con propiedades y valores.</returns>
		private Respuesta<DatosProcesamiento.CreditosFiscalesSIAT> CompletaCanonicoSIAT(string psDocumentoCanonico)
		{
			var voRespuestaCompletaCanonico = new Respuesta<DatosProcesamiento.CreditosFiscalesSIAT>();

			try
			{
				voRespuestaCompletaCanonico.Entidad = MetodosComunes.DeserializaXmlSIAT(psDocumentoCanonico);
				if (voRespuestaCompletaCanonico.Entidad == null)
					voRespuestaCompletaCanonico.EsExitoso = false;
				else
					voRespuestaCompletaCanonico.EsExitoso = true;
			}
			catch (Exception eError)
			{
				voRespuestaCompletaCanonico.EsExitoso = false;
				if (eError.InnerException != null)
					voRespuestaCompletaCanonico.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.CompletaCanonico), new Exception(eError.InnerException.Message));
				else
					voRespuestaCompletaCanonico.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.CompletaCanonico), eError);
			}

			return voRespuestaCompletaCanonico;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="idAplicacion">Identificador único (ID) de la Aplicación que envía la petición.</param>
		/// <param name="idTipoDoc">Identificador único (ID) del Tipo de Documento que se envía.</param>
		/// <param name="IdProcesamiento">Identificador único (ID) del Procesamiento.</param>
		/// <param name="respuestaCompletaCanonico"></param>
		/// <returns>Objeto con propiedades y valores.</returns>
		private static Respuesta<bool> ValidaConceptoYPeriodicidadParaTipoPersona(short idAplicacion, short idTipoDoc, Guid IdProcesamiento, Respuesta<DatosProcesamiento.CreditosFiscales> respuestaCompletaCanonico)//, ref Respuesta<ResultadoReglas> resultadoValidacion)
		{
			var voRespuestaValidaEquivalencias = new Respuesta<bool>();

			try
			{
				//PersonaFisica(1|TB:DatosGenerales=2), Persona Moral(1|TB:DatosGenerales=4)
				int iTipoPersona = respuestaCompletaCanonico.Entidad.DatosGenerales.LineasCaptura.DatosLineaCaptura[0].PagoObligadoInternet;

				List<CatConceptoPersonaPeriodicidad> lCatConceptoPersonaPeriodicidad = DalConceptoPersonaPeriodicidad.ObtenerCatConceptoPersonaPeriodicidad(iTipoPersona);

				if (lCatConceptoPersonaPeriodicidad.Count() != 0)
				{
					foreach (var concepto in respuestaCompletaCanonico.Entidad.Conceptos)
					{
						if (!lCatConceptoPersonaPeriodicidad.Exists(m => m.ConceptoDyP == concepto.Clave && m.IdPeriodicidadDyP == concepto.TipoPeriodicidad && m.IdTipoPersona == iTipoPersona))
						{
							voRespuestaValidaEquivalencias.EsExitoso = false;
							voRespuestaValidaEquivalencias.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.ConceptoPersonaPeriodicidad), concepto.Clave, concepto.Periodicidad, iTipoPersona);
						}
					}
				}
				else
				{
					voRespuestaValidaEquivalencias.EsExitoso = false;
					voRespuestaValidaEquivalencias.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.RecuperaConceptoPersonaPeriodicidad), iTipoPersona);
				}

				if (voRespuestaValidaEquivalencias.ListaErrores.Count == 0)
				{
					voRespuestaValidaEquivalencias.EsExitoso = true;
				}
				else
				{
					respuestaCompletaCanonico.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.ErrorValidaConceptoPeriodicidadPorTipoPersona));
					voRespuestaValidaEquivalencias.EsExitoso = false;
				}
			}
			catch (Exception eError)
			{
				respuestaCompletaCanonico.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.ErrorValidaConceptoPeriodicidadPorTipoPersona), eError);
				voRespuestaValidaEquivalencias.EsExitoso = false;
			}

			return voRespuestaValidaEquivalencias;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="solicitud"></param>
		/// <returns>Objeto con propiedades y valores.</returns>
		private Respuesta<DatosProcesamiento.CreditosFiscales> GeneraAgrupacionConceptosDyP(DatosProcesamiento.CreditosFiscales solicitud)
		{
			var voResultadoAgrupacion = new Respuesta<DatosProcesamiento.CreditosFiscales>();

			try
			{
				var vlConceptosAgrupados = new List<DatosProcesamiento.CreditosFiscalesConcepto>();

				short shSecuencia = 1;
				for (int i = 0; i < solicitud.Conceptos.Count(); i++)
				{
					solicitud.Conceptos[i].NoSecuencia = shSecuencia;
					shSecuencia++;
				}

				foreach (DatosProcesamiento.CreditosFiscalesConcepto conceptoDyP in solicitud.Conceptos)
				{
					// Buscar si el concepto ya existe en el ConceptoDyPAgrupado     
					DatosProcesamiento.CreditosFiscalesConcepto conceptoTemp = this.ExisteConcepto(conceptoDyP.Clave, conceptoDyP.Ejercicio, conceptoDyP.FechaCausacion, conceptoDyP.Periodo, conceptoDyP.TipoPeriodicidad, conceptoDyP.EsCorrecto, ref vlConceptosAgrupados);

					// Si el concepto existe buscarlo , sumar la cantidad a pagar y agregar todas las transacciones.
					if (conceptoTemp != null)
					{
						// Agrupar Transacciones
						vlConceptosAgrupados.Find(c => c.NoSecuencia == conceptoTemp.NoSecuencia).Transacciones = AgruparTransaccion(vlConceptosAgrupados.Find(c => c.NoSecuencia == conceptoTemp.NoSecuencia).Transacciones.ToList(), conceptoDyP.Transacciones.ToList());

						// Sumar Importe a Pagar
						vlConceptosAgrupados.Find(c => c.NoSecuencia == conceptoTemp.NoSecuencia).ImportePagar += conceptoDyP.ImportePagar;
					}
					// Si no existe agregarlo.
					else
					{
						//Agrupar Transacciones
						conceptoDyP.Transacciones = this.AgruparTransaccion(conceptoDyP.Transacciones.ToList(), null);
						vlConceptosAgrupados.Add(conceptoDyP);
					}
				}

				solicitud.Conceptos = vlConceptosAgrupados.ToArray();
				voResultadoAgrupacion.Entidad = solicitud;
				voResultadoAgrupacion.EsExitoso = true;
			}
			catch (Exception ex)
			{
				voResultadoAgrupacion.EsExitoso = false;
				voResultadoAgrupacion.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.AgrupacionConceptosDyP), ex);
			}

			return voResultadoAgrupacion;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="clave"></param>
		/// <param name="ejercicio"></param>
		/// <param name="fechaCausacion"></param>
		/// <param name="periodo"></param>
		/// <param name="periodicidad"></param>
		/// <param name="esCorrecto"></param>
		/// <param name="conceptosDyPAgrupados"></param>
		/// <returns>Objeto con propiedades y valores.</returns>
		private DatosProcesamiento.CreditosFiscalesConcepto ExisteConcepto(string clave, int ejercicio, string fechaCausacion, string periodo, string periodicidad, bool esCorrecto, ref List<DatosProcesamiento.CreditosFiscalesConcepto> conceptosDyPAgrupados)
		{
			return conceptosDyPAgrupados.Find(c => c.Clave == clave && c.Periodo == periodo && c.TipoPeriodicidad == periodicidad && c.Ejercicio == ejercicio && c.FechaCausacion == fechaCausacion && c.EsCorrecto == esCorrecto);
		}

		/// <summary>
		/// Agrupación de Transacciones.
		/// </summary>
		/// <param name="transaccionesPivote"></param>
		/// <param name="transaccionesNuevas"></param>
		/// <returns>Objeto con propiedades y valores.</returns>
		private DatosProcesamiento.CreditosFiscalesConceptoTransaccion[] AgruparTransaccion(List<DatosProcesamiento.CreditosFiscalesConceptoTransaccion> transaccionesPivote, List<DatosProcesamiento.CreditosFiscalesConceptoTransaccion> transaccionesNuevas = null)
		{
			var vlTransaccionesAgrupadas = new List<DatosProcesamiento.CreditosFiscalesConceptoTransaccion>();

			//Realizar agrupacion de las transacciones privote
			foreach (DatosProcesamiento.CreditosFiscalesConceptoTransaccion transaccionPivote in transaccionesPivote)
			{
				DatosProcesamiento.CreditosFiscalesConceptoTransaccion transaccion = ExisteTransaccion(transaccionPivote.Clave, transaccionPivote.Tipo, ref vlTransaccionesAgrupadas);

				if (transaccion != null)
					vlTransaccionesAgrupadas.Find(tr => tr.Clave == transaccion.Clave && tr.Tipo == transaccion.Tipo).Importe += transaccionPivote.Importe;
				else
					vlTransaccionesAgrupadas.Add(transaccionPivote);
			}

			if (transaccionesNuevas != null)
			{
				// Realizar agrupacion de las transacciones nuevas.
				foreach (DatosProcesamiento.CreditosFiscalesConceptoTransaccion oTransaccionNueva in transaccionesNuevas)
				{
					DatosProcesamiento.CreditosFiscalesConceptoTransaccion oTransaccion = this.ExisteTransaccion(oTransaccionNueva.Clave, oTransaccionNueva.Tipo, ref vlTransaccionesAgrupadas);

					if (oTransaccion != null)
						vlTransaccionesAgrupadas.Find(tr => tr.Clave == oTransaccion.Clave && tr.Tipo == oTransaccion.Tipo).Importe += oTransaccionNueva.Importe;
					else
						vlTransaccionesAgrupadas.Add(oTransaccionNueva);
				}
			}

			return vlTransaccionesAgrupadas.ToArray();
		}

		/// <summary>
		/// Verifica si existe o no una transacción en un grupo de transacciones.
		/// </summary>
		/// <param name="clave"></param>
		/// <param name="tipo"></param>
		/// <param name="transaccionesAgrupadas"></param>
		/// <returns></returns>
		private DatosProcesamiento.CreditosFiscalesConceptoTransaccion ExisteTransaccion(string clave, string tipo, ref List<DatosProcesamiento.CreditosFiscalesConceptoTransaccion> transaccionesAgrupadas)
		{
			return transaccionesAgrupadas.Find(tr => tr.Clave == clave && tr.Tipo == tipo);
		}

		/// <summary>
		/// Completa el esquema que se enviará a DyP
		/// </summary>
		/// <param name="resultadoValidacion">resultado total del procesamiento</param>
		/// <param name="IdALR">Id del ALR</param>
		/// <returns>Objeto con propiedades y valores.</returns>
		private SolicitudGeneracionLC CompletarEsquemaDyP(ref Respuesta<ResultadoReglas> resultadoValidacion, sbyte IdALR)
		{
			//Deserializar objeto de solicitud de generación de LC
			var voSolicitud = new SolicitudGeneracionLC();
			try
			{
				voSolicitud = this.DeserializaSolicitudDyP(resultadoValidacion.Entidad.Mensaje);

				#region Folio
					Respuesta<string> oRespuestaFolio = this.obtieneNumeroFolio(resultadoValidacion.Entidad.IdAplicacion, (byte) IdALR);

					if (oRespuestaFolio.EsExitoso)
					{
						voSolicitud.DatosGenerales.Solicitud = oRespuestaFolio.Entidad;

						#region FechaEmisión y FechaRecepción
							Respuesta<Dictionary<string, string>> oRespuestaFechas = this.obtieneFechasSolicitud(resultadoValidacion.Entidad.IdProcesamiento);

							if (oRespuestaFechas.EsExitoso)
							{
								voSolicitud.DatosGenerales.FechaEmision = oRespuestaFechas.Entidad["FechaEmision"];
								voSolicitud.DatosGenerales.FechaRecepcion = oRespuestaFechas.Entidad["FechaRecepcion"];
							}
							else
							{
								resultadoValidacion.EsExitoso = false;
								resultadoValidacion.ListaErrores.AddRange(oRespuestaFechas.ListaErrores);
								return voSolicitud;
							}

							#region ImporteTotal
								Respuesta<decimal> oRespuestaImporte = this.obtieneImporteTotalSolicitud(voSolicitud.Conceptos);

								if (oRespuestaFechas.EsExitoso)
									voSolicitud.DatosGenerales.ImporteTotal = oRespuestaImporte.Entidad;
								else
								{
									resultadoValidacion.EsExitoso = false;
									resultadoValidacion.ListaErrores.AddRange(oRespuestaImporte.ListaErrores);
									return voSolicitud;
								}
							#endregion
						#endregion

						#region ImportesPorConcepto
							Respuesta<Concepto[]> voResultadoImportes = this.obtieneImportesConceptos(voSolicitud.Conceptos, resultadoValidacion.Entidad.IdTipoDocPago);

							if (!voResultadoImportes.EsExitoso)
							{
								resultadoValidacion.EsExitoso = false;
								resultadoValidacion.ListaErrores.AddRange(oRespuestaImporte.ListaErrores);
								return voSolicitud;
							}
						#endregion
					}
					else
					{
						resultadoValidacion.EsExitoso = false;
						resultadoValidacion.ListaErrores.AddRange(oRespuestaFolio.ListaErrores);
						return voSolicitud;
					}
				#endregion
			}
			catch (Exception eError)
			{
				resultadoValidacion.EsExitoso = false;
				resultadoValidacion.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.GeneracionSolicitudDyP), eError);
			}

			return voSolicitud;
		}

		/// <summary>
		/// Deserializa solicitud DyP
		/// </summary>
		/// <param name="documento">Documento a deserializar.</param>
		/// <returns>Objeto con propiedades y valores.</returns>
		private SolicitudGeneracionLC DeserializaSolicitudDyP(string documento)
		{
			var oSolicitud = new SolicitudGeneracionLC();

			try
			{
				var vCodificacion = new UTF8Encoding();
				using (MemoryStream stream = new MemoryStream(vCodificacion.GetBytes(documento)))
				{
					var vSerializador = new XmlSerializer(typeof(SolicitudGeneracionLC));
					oSolicitud = (SolicitudGeneracionLC) vSerializador.Deserialize(stream);
				}
			}
			catch
			{
				throw;
			}

			return oSolicitud;
		}

		/// <summary>
		/// Obtiene las fechas de solicitud de la base de datos
		/// </summary>
		/// <param name="IdProcesamiento">Id del procesamiento</param>
		/// <returns></returns>
		private Respuesta<Dictionary<string, string>> obtieneFechasSolicitud(Guid IdProcesamiento)
		{
			var voResultadoFechas = new Respuesta<Dictionary<string, string>>();

			try
			{
				voResultadoFechas.Entidad = DalProcesamiento.ObtieneFechasProceso(IdProcesamiento);
				voResultadoFechas.EsExitoso = true;
			}
			catch (Exception ex)
			{
				voResultadoFechas.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.ObtenerFolio), ex);
			}

			return voResultadoFechas;
		}

		/// <summary>
		/// Obtiene Importe Total de la solicitud
		/// </summary>
		/// <param name="conceptos">Clase de conceptos a completar</param>
		/// <returns>Importe total</returns>
		private Respuesta<decimal> obtieneImporteTotalSolicitud(Concepto[] conceptos)
		{
			var voResultadoImporte = new Respuesta<decimal>();

			try
			{
				voResultadoImporte.Entidad = Convert.ToDecimal(conceptos.Sum(r => r.CantidadPagar));
				voResultadoImporte.EsExitoso = true;
			}
			catch (Exception ex)
			{
				voResultadoImporte.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.ObtenerFolio), ex);
			}

			return voResultadoImporte;
		}

		/// <summary>
		/// Obtiene los importes de los conceptos.
		/// </summary>
		/// <param name="conceptos">Colección de conceptos a completar</param>
		/// <returns>Objeto con propiedades y valores.</returns>
		private Respuesta<Concepto[]> obtieneImportesConceptos(Concepto[] conceptos, int IdTipoDocumento)
		{
			var voResultadoImportes = new Respuesta<Concepto[]>();

			try
			{
				foreach (Concepto oConcepto in conceptos)
				{
					oConcepto.ImporteTotalAbonos = oConcepto.Transacciones.Where(ab => ab.Tipo.ToUpper() == "ABONO").Sum(ab => ab.Valor);
					oConcepto.ImporteTotalCargos = oConcepto.Transacciones.Where(ab => ab.Tipo.ToUpper() == "CARGO").Sum(ab => ab.Valor);

					if (oConcepto.ImporteTotalAbonos == 0 && IdTipoDocumento == 22)
						oConcepto.ImporteTotalAbonos = null;

					if (oConcepto.DatosIcep.Ejercicio == "0")
						oConcepto.DatosIcep.Ejercicio = null;
				}

				voResultadoImportes.Entidad = conceptos;
				voResultadoImportes.EsExitoso = true;
			}
			catch (Exception eError)
			{
				voResultadoImportes.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.ObtenerFolio), eError);
			}

			return voResultadoImportes;
		}

		/// <summary>
		/// Serializa una solicitud a DyP
		/// </summary>
		/// <param name="solicitud">Documento a deserializar</param>
		/// <returns>Cadena de caracter</returns>
		private string SerializaSolicitudDyP(SolicitudGeneracionLC solicitud)
		{
			string sRespuesta = string.Empty;

			try
			{
				using (MemoryStream omsReporte = new MemoryStream())
				{
					var vxsSerializadorReporte = new XmlSerializer(solicitud.GetType());

					vxsSerializadorReporte.Serialize(omsReporte, solicitud);
					omsReporte.Seek(0, SeekOrigin.Begin);

					var vxdRespuesta = new XmlDocument();
					vxdRespuesta.Load(omsReporte);

					sRespuesta = vxdRespuesta.InnerXml;
				}
			}
			catch
			{
				throw;
			}

			return sRespuesta;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="solicitud"></param>
		/// <param name="IdAplicacion">Identificador único (ID) de la Aplicación que envía la petición.</param>
		/// <param name="IdTipoDocumento">Identificador único (ID) del Tipo de Documento que se envía.</param>
		/// <returns>Objeto con propiedades y valores.</returns>
		private Respuesta<SolicitudGeneracionLC> OrdenamientoTransacciones(SolicitudGeneracionLC solicitud, short IdAplicacion, short IdTipoDocumento)
		{
			var voRespuesta = new Respuesta<SolicitudGeneracionLC>();

			try
			{
				List<CatOrdenamientoTransacciones> lstOrdenamiento = DalCatOrdenamientoTransacciones.ConsultaOrdenamiento(IdAplicacion, IdTipoDocumento);
				if (lstOrdenamiento.Count > 0)
				{
					Parallel.For(0, solicitud.Conceptos.Count(), new ParallelOptions { MaxDegreeOfParallelism = 4 }, i =>
					{
						var oTransaccionTemp = new List<Transaccion>();
						List<Transaccion> oTransaccionOriginal = solicitud.Conceptos[i].Transacciones.ToList();
						foreach (CatOrdenamientoTransacciones oOrdenamiento in lstOrdenamiento)
						{
							// Agregar transacciones a Temporal según secuencia
							oTransaccionTemp.AddRange(oTransaccionOriginal.Where(t => t.Tipo.ToLower() == oOrdenamiento.Ordenamiento.ToLower() || t.Descripcion.ToLower() == oOrdenamiento.Ordenamiento.ToLower()));

							// Eliminar transacciones de original
							List<Transaccion> index = oTransaccionOriginal.FindAll(t => t.Tipo.ToLower() == oOrdenamiento.Ordenamiento.ToLower() || t.Descripcion.ToLower() == oOrdenamiento.Ordenamiento.ToLower());
							if (index.Count >= 0)
							{
								foreach (Transaccion oTransaccion in index)
								{
									// ensure item found
									oTransaccionOriginal.Remove(oTransaccion);
								}
							}
						}

						solicitud.Conceptos[i].Transacciones = oTransaccionTemp.ToArray();
					});
				}

				voRespuesta.EsExitoso = true;
			}
			catch (Exception ex)
			{
				voRespuesta.EsExitoso = false;
				voRespuesta.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.OrdenamientoTransaccciones), ex);
			}

			voRespuesta.Entidad = solicitud;
			return voRespuesta;
		}

		/// <summary>
		/// Actualiza los datos generales de un procesamiento
		/// </summary>
		/// <param name="IdProcesamiento">Id del procesamiento</param>
		/// <param name="FolioDyP">Folio DyP</param>
		/// <param name="LineaCaptura">Línea de captura</param>
		/// <param name="Exito">true si el proceso fue exitoso</param>
		/// <returns>Objeto con propiedades y valores.</returns>
		private RespuestaGenerica actualizaDatosGenerales(Guid IdProcesamiento, string FolioDyP, string LineaCaptura, bool Exito, decimal TiempoServicio)
		{
			var voResultadoRegistro = new RespuestaGenerica();

			try
			{
				voResultadoRegistro.EsExitoso = DalDatosGenerales.ActualizaDatosGenerales(IdProcesamiento, FolioDyP, LineaCaptura, Exito, TiempoServicio);
			}
			catch (Exception ex)
			{
				voResultadoRegistro.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.RegistraProcesamiento), ex);
			}

			return voResultadoRegistro;
		}

		/// <summary>
		/// Obtiene un documento PDF generado por el servicio de Impresión de PDF
		/// </summary>
		/// <param name="datosLC">Datos de la línea de captura</param>
		/// <returns>Objeto con propiedades y valores.</returns>
		private Respuesta<RespuestaLC> ObtienePDF(RespuestaLC datosLC, int IdAplicacion, int IdTipoDocumento, string documento)
		{
			var voResultadoPdf = new Respuesta<RespuestaLC>();

			try
			{
				voResultadoPdf.Entidad = datosLC;

				var voFolios = new List<string>();
				// Folios.Add("44721500000044");
				foreach (RespuestaLCDatosLinea datosFolio in voResultadoPdf.Entidad.LineasCaptura)
					voFolios.Add(datosFolio.Folio);

				// Obtiene Formato de impresión
				int iIdFormatoImpresion = GeneracionEquivalenciaFormato.GeneraEquivalenciaFormato(IdAplicacion, IdTipoDocumento, documento);
				var vEnumeracion = (ServicioImpresion.EnumTemplate) iIdFormatoImpresion;

				using (ServicioImpresion.ServicioGeneraFormatoImpresionClient osweServicioImpresion = new ServicioImpresion.ServicioGeneraFormatoImpresionClient())
				{
					voResultadoPdf.Entidad.PDF = osweServicioImpresion.GeneraArchivo(vEnumeracion, voFolios.ToArray());
				}

				voResultadoPdf.EsExitoso = true;
			}
			catch (Exception eError)
			{
				voResultadoPdf.EsExitoso = false;
				voResultadoPdf.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.GeneracionPDF), eError);
			}

			return voResultadoPdf;
		}

		/// <summary>
		/// Obtiene un documento PNG generado por el servicio de Impresión de PNG
		/// </summary>
		/// <param name="poDatosLC">Datos de la línea de captura</param>
		/// <param name="piIdAplicacion">Identificador único (ID) de la Aplicación que envía la petición.</param>
		/// <param name="piIdTipoDocumento">Identificador único (ID) del Tipo de Documento que se envía.</param>
		/// <param name="psDocumento">Documento en formato XML con los datos necesarios para generar la Línea de Captura.</param>
		/// <returns>Objeto con propiedades y valores.</returns>
		private Respuesta<RespuestaLC> ObtienePNG(RespuestaLC poDatosLC, int piIdAplicacion, int piIdTipoDocumento, string psDocumento)
		{
			/*
			 * ----------------------------------------
			 * Creado por: Mario Escarpulli 
			 * Creado: 15/Junio/2019
			 * NOTAS: Esta clase se creó con base al Requerimiento de Servicio (RES)
			 *        CZA_CREDFIS: Nuevo Servicio en Motor Traductor para la entrega de sección de línea de captura.
			 * ----- Historial de actualizaciones -----
			 * Actualizado por: Mario Escarpulli
			 * Actualizado el: 20/Junio/2019
			 * ----------------------------------------
			*/
			var voResultadoPng = new Respuesta<RespuestaLC>();

			try
			{
				voResultadoPng.Entidad = poDatosLC;

				var voFolios = new List<string>();
				// Folios.Add("44721500000044");
				foreach (RespuestaLCDatosLinea datosFolio in voResultadoPng.Entidad.LineasCaptura)
					voFolios.Add(datosFolio.Folio);

				// Obtiene Formato de impresión
				int iIdFormatoImpresion = GeneracionEquivalenciaFormato.GeneraEquivalenciaFormato(piIdAplicacion, piIdTipoDocumento, psDocumento);
				var vEnumeracion = (ServicioImpresion.EnumTemplate) iIdFormatoImpresion;

				using (ServicioImpresion.ServicioGeneraFormatoImpresionClient osweServicioImpresion = new ServicioImpresion.ServicioGeneraFormatoImpresionClient())
				{
					voResultadoPng.Entidad.PNG = osweServicioImpresion.GeneraArchivoImagen(vEnumeracion, voFolios.ToArray());
				}

				voResultadoPng.EsExitoso = true;
			}
			catch (Exception eError)
			{
				voResultadoPng.EsExitoso = false;
				voResultadoPng.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.GeneracionPNG), eError);
			}

			return voResultadoPng;
		}

		/// <summary>
		/// Genera un formato correcto para el mensaje de salida.
		/// </summary>
		/// <param name="resultadoProcesamiento">Resultado total del procesamiento</param>
		/// <param name="respuestaLC">Respuesta de la línea de captura</param>
		/// <returns>Cadena de caracter serializado de la respuesta del servicio.</returns>
		private string ConstruyeMensajeSalida(Respuesta<ResultadoReglas> resultadoProcesamiento, RespuestaLC respuestaLC = null)
		{
			string sRespuestaGeneral = string.Empty;

			try
			{
				if (respuestaLC == null)
				{
					respuestaLC = new RespuestaLC();
					respuestaLC.TipoRespuesta = 0;
					respuestaLC.ListaErrores = new string[resultadoProcesamiento.ListaErrores.Count];

					for (int i = 0; i < resultadoProcesamiento.ListaErrores.Count; i++)
					{
						string sErrorAplicacion = string.Empty;
						if (resultadoProcesamiento.ListaErrores[i].ErrorAplicacion != null)
							sErrorAplicacion = resultadoProcesamiento.ListaErrores[i].ErrorAplicacion.Message;
						respuestaLC.ListaErrores[i] = resultadoProcesamiento.ListaErrores[i].Codigo + "|" + resultadoProcesamiento.ListaErrores[i].ErrorUsuario + "|" + sErrorAplicacion;
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
						string sErrorAplicacion = string.Empty;
						if (resultadoProcesamiento.ListaErrores[i].ErrorAplicacion != null)
							sErrorAplicacion = resultadoProcesamiento.ListaErrores[i].ErrorAplicacion.Message;

						respuestaLC.ListaErrores[i] = resultadoProcesamiento.ListaErrores[i].Codigo + "|" + resultadoProcesamiento.ListaErrores[i].ErrorUsuario + "|" + sErrorAplicacion;
					}
				}
				else if (respuestaLC != null && respuestaLC.LineasCaptura == null)
					respuestaLC.TipoRespuesta = 0;
				else
					respuestaLC.TipoRespuesta = 0;

				sRespuestaGeneral = MetodosComunes.SerializaRespuestaLC(respuestaLC);
			}
			catch (Exception eError)
			{
				sRespuestaGeneral = "<RespuestaLC><ListaErrores><Error>" + eError.Message + "</Error></ListaErrores><TipoRespuesta>0</TipoRespuesta><IdProcesamiento></IdProcesamiento></RespuestaLC>";
			}

			return sRespuestaGeneral;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="LineaCaptura"></param>
		/// <returns></returns>
		public string ObtienePDF(string LineaCaptura)
		{
			//Respuesta<RespuestaLC> vResultadoPdf = new Respuesta<RespuestaLC>();
			var vResultadoPdf = new RespuestaLC();
			var lineas = new RespuestaLCDatosLinea[1];
			var linea = new RespuestaLCDatosLinea();

			var vFolios = new List<string>();
			vFolios.Add(BusquedaLineaCaptura.ConsultaFolioDyPPorLineaCaptura(LineaCaptura));
			var e = (ServicioImpresion.EnumTemplate) 1;

			using (ServicioImpresion.ServicioGeneraFormatoImpresionClient servImpresion = new ServicioImpresion.ServicioGeneraFormatoImpresionClient())
			{
				vResultadoPdf.PDF = servImpresion.GeneraArchivo(e, vFolios.ToArray());
			}

			linea.Folio = vFolios.First();
			linea.LineaCaptura = LineaCaptura;
			vResultadoPdf.TipoRespuesta = 1;
			lineas[0] = linea;
			vResultadoPdf.LineasCaptura = lineas;
			//LogEventos.EscribirEntradaLog((int)EnumErrorMotor.ErrorCrearConceptoDyP, "", EventLogEntryType.Error);
			//throw new Exception("");
			return MetodosComunes.SerializaRespuestaLC(vResultadoPdf);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="Folio"></param>
		/// <param name="LineaCaptura"></param>
		/// <returns></returns>
		public string ObtienePDFXFolio(string Folio, string LineaCaptura)
		{
			//var resultadoPDF = new Respuesta<RespuestaLC>();
			var vResultadoPdf = new RespuestaLC();
			var lineas = new RespuestaLCDatosLinea[1];
			var linea = new RespuestaLCDatosLinea();
			var vFolios = new List<string>();
			vFolios.Add(Folio);
			var e = (ServicioImpresion.EnumTemplate) 1;

			using (ServicioImpresion.ServicioGeneraFormatoImpresionClient servImpresion = new ServicioImpresion.ServicioGeneraFormatoImpresionClient())
			{
				vResultadoPdf.PDF = servImpresion.GeneraArchivo(e, vFolios.ToArray());
			}

			linea.Folio = vFolios.First();
			linea.LineaCaptura = LineaCaptura;
			vResultadoPdf.TipoRespuesta = 1;
			lineas[0] = linea;
			vResultadoPdf.LineasCaptura = lineas;
			//LogEventos.EscribirEntradaLog((int)EnumErrorMotor.ErrorCrearConceptoDyP, "", EventLogEntryType.Error);
			//throw new Exception("");
			return MetodosComunes.SerializaRespuestaLC(vResultadoPdf);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdADR"></param>
		/// <param name="RFC"></param>
		/// <param name="Folio"></param>
		/// <param name="LineaDeCaptura"></param>
		/// <param name="No_de_Resolucion"></param>
		/// <param name="rango_de_emision_FechaPago"></param>
		/// <param name="fecha_ini"></param>
		/// <param name="fecha_fin"></param>
		/// <returns></returns>
		public string ObtieneFormatos(int IdADR, string RFC, string Folio, string LineaDeCaptura, string No_de_Resolucion, int rango_de_emision_FechaPago, DateTime fecha_ini, DateTime fecha_fin)
		{
			//var resultadoPDF = new Respuesta<RespuestaLC>();
			var vBusquedaForm = new BusquedaLineaCaptura();
			
			List<RespuestaListaFormatos> oFormatos = BusquedaLineaCaptura.ConsultaFormatosXFiltros(IdADR, RFC, Folio, LineaDeCaptura, No_de_Resolucion, rango_de_emision_FechaPago, fecha_ini, fecha_fin);

			string salida = "<RespuestaLF><ListaFromatos>";
			for (int x = 0; x < oFormatos.Count; x++)
			{
				salida += "<Formato>";
				string IdConsecutivo = "<IDCONS>" + x.ToString() + "</IDCONS>";
				string ADR = "<ADR>" + oFormatos[x].ADR + "</ADR>";
				string RFCVal = "<RFC>" + oFormatos[x].RFC + "</RFC>";
				string RazonSocial = "<RazonSocial>" + oFormatos[x].RazonSocial + "</RazonSocial>";
				string FolioVal = "<Folio>" + oFormatos[x].Folio + "</Folio>";
				string fechaEmision = "<fechaEmision>" + oFormatos[x].fechaEmision + "</fechaEmision>";
				string IdResolucion = "<IdResolucion>" + oFormatos[x].IdResolucion + "</IdResolucion>";
				string NumResolucion = "<NumResolucion>" + oFormatos[x].NumResolucion + "</NumResolucion>";
				string LineaDeCapturaVal = "<LineaDeCapturaVal>" + oFormatos[x].LineaDeCaptura + "</LineaDeCapturaVal>";
				string Importe = "<Importe>" + oFormatos[x].Importe + "</Importe>";
				string FechaDeVigencia = "<FechaDeVigencia>" + oFormatos[x].FechaDeVigencia + "</FechaDeVigencia>";
				string FormaDePago = "<FormaDePago>" + oFormatos[x].FormaDePago + "</FormaDePago>";
				string FechaDePago = "<FechaDePago>" + oFormatos[x].FechaDePago + "</FechaDePago>";
				string TipoDePago = "<TipoDePago>" + oFormatos[x].TipoDePago + "</TipoDePago>";
				string Aplicativo = "<Aplicativo>" + oFormatos[x].Aplicativo + "</Aplicativo>";
				string Modulo = "<Modulo>" + oFormatos[x].Modulo + "</Modulo>";
				string RFC_Usr_Generador = "<RFC_Usr_Generador>" + oFormatos[x].RFC_Usr_Generador + "</RFC_Usr_Generador>";
				string pDFFieldVal = "<pDFFieldVal>" + " " + "</pDFFieldVal>";
				string XML_Gen = "<XML_Gen>" + oFormatos[x].XML + "</XML_Gen>";
				salida += IdConsecutivo + ADR + RFCVal + RazonSocial
					+ FolioVal + fechaEmision + IdResolucion + NumResolucion
					+ LineaDeCapturaVal
					+ Importe + FechaDeVigencia + FormaDePago + FechaDePago + TipoDePago
					+ Aplicativo + Modulo + RFC_Usr_Generador
					+ pDFFieldVal + XML_Gen;
				salida += "</Formato>";
			}
			salida += "</ListaFromatos></RespuestaLF>";
			return salida;
		}

		/// <summary>
		/// Obtiene el número de folio por aplicación y ALR.
		/// </summary>
		/// <param name="idAplicacion">Identificador de la aplicación.</param>
		/// <param name="idALR">Identificador del ALR.</param>
		/// <returns>Folio obtenido o el error generado en su caso.</returns>
		private Respuesta<string> obtieneNumeroFolio(short idAplicacion, byte idALR)
		{
			var voResultadoFolio = new Respuesta<string>();

			try
			{
				voResultadoFolio.Entidad = DalProcesamiento.ObtieneFolioPorAplicacion(idAplicacion, idALR);
				voResultadoFolio.EsExitoso = true;
			}
			catch (Exception ex)
			{
				voResultadoFolio.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.ObtenerFolio), ex);
			}

			return voResultadoFolio;
		}

		/// <summary>
		/// Liberando de memoria los objetos utilizados por la clase.
		/// </summary>
		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}
	}
}