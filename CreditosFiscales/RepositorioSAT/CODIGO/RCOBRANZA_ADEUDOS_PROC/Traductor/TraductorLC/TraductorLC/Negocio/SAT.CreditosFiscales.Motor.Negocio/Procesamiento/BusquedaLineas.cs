//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.Negocio:SAT.CreditosFiscales.Motor.Negocio.BusquedaLineas:1:12/07/2012[Assembly:1.0:12/07/2013])

// Referencias de sistema.
using System;
using System.Linq;
using System.Collections.Generic;

// Referencias personalizadas.
using SAT.CreditosFiscales.Motor.Utilidades;
using SAT.CreditosFiscales.Motor.Negocio.Mensajes;
using SAT.CreditosFiscales.Motor.AccesoDatos.Busquedas;
using SAT.CreditosFiscales.Motor.Entidades.Procesamiento;
using DatosProcesamiento = SAT.CreditosFiscales.Motor.Entidades.DatosProcesamiento;

namespace SAT.CreditosFiscales.Motor.Negocio.Procesamiento
{
	/// <summary>
	/// 
	/// </summary>
	public static class BusquedaLineas
	{
		/// <summary>
		/// Busca en base de datos si existe una línea previamente generada
		/// </summary>
		/// <param name="solicitudCanonica">Objeto creditos fiscales con los datos de la solicitud</param>
		/// <returns>Respuesta</returns>
		public static Respuesta<RespuestaLC> BusquedaLC(DatosProcesamiento.CreditosFiscales solicitudCanonica)
		{
			var respuestaBusqueda = new Respuesta<RespuestaLC>();
			bool continuar;

			try
			{
				//Obtiene procesamientos con el mismo tipo de concepto
				List<string> ProcesamientosBD = BusquedaLineaCaptura.ConsultaProcesamientPorDatosGenerales
				(
					solicitudCanonica.DatosGenerales.RFC
					, solicitudCanonica.DatosGenerales.ALR, solicitudCanonica.DatosGenerales.TipoDocumento
					, solicitudCanonica.DatosGenerales.LineasCaptura.DatosLineaCaptura[0].FechaVigencia
					, solicitudCanonica.Agrupadores[0].FormaPago
					, solicitudCanonica.DatosGenerales.LineasCaptura.DatosLineaCaptura[0].Importe
					, solicitudCanonica.DatosGenerales.NumeroParcialidad
					, solicitudCanonica.DatosGenerales.IdAplicacion
				);
				respuestaBusqueda.EsExitoso = true;

				if (ProcesamientosBD.Count > 0)
				{
					foreach (string procesamientoDB in ProcesamientosBD)
					{
						// Obtener agrupadores por datos generales
						List<DatosProcesamiento.AgrupadorDB> AgrupadoresBD = BusquedaLineaCaptura.ConsultaAgrupadoresPorDatosGenerales(procesamientoDB);

						// Comparación de Agrupadores
						continuar = ComparaAgrupadores(AgrupadoresBD, solicitudCanonica.Agrupadores);

						// Buscar DatosLineaCaptura
						if (continuar)
						{
							respuestaBusqueda.ExisteLineaCaptura = continuar;
							respuestaBusqueda.Entidad = BuscaDatosLC(Guid.Parse(procesamientoDB));
							if (respuestaBusqueda.Entidad.ListaErrores != null)
							{
								respuestaBusqueda.AgregaError(respuestaBusqueda.Entidad.ListaErrores[0]);
								respuestaBusqueda.EsExitoso = false;
							}
							break;
						}
					}
				}
				else
					respuestaBusqueda.ExisteLineaCaptura = false;
			}
			catch (Exception ex)
			{
				respuestaBusqueda.EsExitoso = false;
				respuestaBusqueda.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.BusquedaLCGenerada), ex);
			}

			return respuestaBusqueda;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="AgrupadoresBD"></param>
		/// <param name="creditosFiscalesAgrupador"></param>
		/// <returns></returns>
		private static bool ComparaAgrupadores(List<DatosProcesamiento.AgrupadorDB> AgrupadoresBD, DatosProcesamiento.CreditosFiscalesAgrupador[] creditosFiscalesAgrupador)
		{
			bool bExisteAgrupador = true;

			if (AgrupadoresBD.Count() > 0)
			{
				if (AgrupadoresBD.Count == creditosFiscalesAgrupador.Count())
				{
					//Comparar los datos para saber si son iguales
					foreach (DatosProcesamiento.CreditosFiscalesAgrupador agrupadorSol in creditosFiscalesAgrupador)
					{
						string sValorAgrupador = agrupadorSol.NumeroAgrupador + "|" + agrupadorSol.Fecha + "|" + agrupadorSol.AutoridadId.ToString() + "|" + agrupadorSol.ALR + "|" + agrupadorSol.RFC;
						//Revisar si existen agrupadores iguales                                                
						DatosProcesamiento.AgrupadorDB oAgrupador = AgrupadoresBD.FindAll(a => a.ValorAgrupador.ToLower() == sValorAgrupador.ToLower()).FirstOrDefault();
						if (oAgrupador != null)
						{
							bExisteAgrupador = ComparaConceptos(agrupadorSol, oAgrupador);
							if (bExisteAgrupador)
							{
								//Eliminar transacciones de original
								AgrupadoresBD.Remove(oAgrupador);
							}
							else
							{
								bExisteAgrupador = false;
								break;
							}
						}
						else
						{
							bExisteAgrupador = false;
							break;
						}
					}
				}
				else
					bExisteAgrupador = false;
			}
			else
				bExisteAgrupador = false;

			return bExisteAgrupador;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="agrupadorSol"></param>
		/// <param name="Agrupador"></param>
		/// <returns>Valor de tipo Lógico.</returns>
		private static bool ComparaConceptos(DatosProcesamiento.CreditosFiscalesAgrupador agrupadorSol, DatosProcesamiento.AgrupadorDB Agrupador)
		{
			bool bExisteConcepto = true;

			List<DatosProcesamiento.ConceptoOriginalDB> ConceptosOriginalDB = BusquedaLineaCaptura.ConsultaConceptosPorAgrupador(Agrupador.IdAgrupador, Agrupador.IdProcesamiento);

			if (ConceptosOriginalDB.Count == agrupadorSol.ConceptosOriginal.Count())
			{
				foreach (DatosProcesamiento.CreditosFiscalesAgrupadorConceptos conceptoSol in agrupadorSol.ConceptosOriginal)
				{
					//Comparación ConceptoOrigen, Ejercicio, FechaCausacion, CreditoSIR, ImportePagar
					DatosProcesamiento.ConceptoOriginalDB oConcepto = ConceptosOriginalDB.FindAll(c => conceptoSol.Clave == c.ConceptoOrigen &&
						conceptoSol.Ejercicio == c.Ejercicio &&
						conceptoSol.FechaCausacion == c.FechaCausacion &&
						conceptoSol.CreditoSir == c.CreditoSir &&
						conceptoSol.ImportePagar == c.ImportePagar).FirstOrDefault();

					if (oConcepto != null)
					{
						bExisteConcepto = ComparaDescuentos(conceptoSol, oConcepto);
						if (bExisteConcepto)
						{
							bExisteConcepto = ComparaHijos(conceptoSol, oConcepto);
							if (bExisteConcepto)
							{
								//Eliminar transacciones de original
								ConceptosOriginalDB.Remove(oConcepto);
							}
							else
							{
								bExisteConcepto = false;
								break;
							}
						}
					}
					else
					{
						bExisteConcepto = false;
						break;
					}

				}
			}
			else
				bExisteConcepto = false;

			return bExisteConcepto;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="conceptoSol"></param>
		/// <param name="Concepto"></param>
		/// <returns></returns>
		private static bool ComparaHijos(DatosProcesamiento.CreditosFiscalesAgrupadorConceptos conceptoSol, DatosProcesamiento.ConceptoOriginalDB Concepto)
		{
			bool ExisteHijo = true;

			if (conceptoSol.Hijos != null)
			{
				// Consulta todos los conceptos hijos del concepto original
				List<DatosProcesamiento.ConceptoOriginalHijo> ConceptosOriginalHijoDB = BusquedaLineaCaptura.ConsultaConceptosHijoPorAgrupador(Concepto.IdAgrupador, Concepto.IdProcesamiento, Concepto.ConceptoOrigen, Concepto.IdConceptoOriginal);

				if (conceptoSol.Hijos.Count() == ConceptosOriginalHijoDB.Count)
				{
					foreach (DatosProcesamiento.CreditosFiscalesAgrupadorConceptosHijo HijoSol in conceptoSol.Hijos)
					{
						//ConceptoOriginalPadre, ConceptoOriginalHijo, Ejercicio, ImportePagar    
						DatosProcesamiento.ConceptoOriginalHijo ConceptoHijo = ConceptosOriginalHijoDB.Where
						(
							c => conceptoSol.Clave == c.ConceptoOrigen
								&& HijoSol.Clave == c.ConceptoOrigenHijo 
								&& HijoSol.Ejercicio == c.Ejercicio 
								&& (HijoSol.FechaCausacion == string.Empty ? null : HijoSol.FechaCausacion) == c.FechaCausacion
								&& HijoSol.ImportePagar == c.ImportePagar
						).FirstOrDefault();

						if (ConceptoHijo != null)
						{
							ExisteHijo = ComparaDescuentosHijo(HijoSol, ConceptoHijo, Concepto.ConceptoOrigen, Concepto.IdConceptoOriginal);
							if (ExisteHijo)
							{
								// Eliminar transacciones de original
								ConceptosOriginalHijoDB.Remove(ConceptoHijo);
							}
							else
							{
								ExisteHijo = false;
								break;
							}
						}
						else
						{
							ExisteHijo = false;
							break;
						}
					}
				}
				else
					ExisteHijo = false;
			}
			else
				ExisteHijo = false;

			return ExisteHijo;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="HijoSol"></param>
		/// <param name="ConceptoHijo"></param>
		/// <param name="ClaveConceptoOriginal"></param>
		/// <param name="IdConceptoOriginal"></param>
		/// <returns></returns>
		private static bool ComparaDescuentosHijo(DatosProcesamiento.CreditosFiscalesAgrupadorConceptosHijo HijoSol, DatosProcesamiento.ConceptoOriginalHijo ConceptoHijo, string ClaveConceptoOriginal, int IdConceptoOriginal)
		{
			bool bExisteDescuentoHijo = true;

			if (HijoSol.Descuentos != null)
			{
				//Consulta todos los descuentos del concepto
				List<DatosProcesamiento.DescuentoOriginalHijo> descuentosHijoDB = BusquedaLineaCaptura.ConsultaDescuentosPorConceptoHijo(ConceptoHijo.IdAgrupador, ConceptoHijo.IdProcesamiento, IdConceptoOriginal, ConceptoHijo.IdConceptoOriginalHijo);

				if (HijoSol.Descuentos.Count() == descuentosHijoDB.Count)
				{
					foreach (DatosProcesamiento.CreditosFiscalesAgrupadorConceptosHijoDescuento descuentoHijoSol in HijoSol.Descuentos)
					{
						//ConceptoOriginal, ConceptoOriginalHijo, IdDescuento, ImporteDescuento  
						DatosProcesamiento.DescuentoOriginalHijo DescuentoHijo = descuentosHijoDB.Where(d => ClaveConceptoOriginal == d.ConceptoOriginal &&
								HijoSol.Clave == d.ConceptoOriginalHijo &&
								descuentoHijoSol.IdDescuento == d.IdDescuento &&
								descuentoHijoSol.ImporteDescuento == d.ImporteDescuento).FirstOrDefault();

						if (DescuentoHijo != null)
						{
							//Eliminar descuento de original
							descuentosHijoDB.Remove(DescuentoHijo);
						}
						else
						{
							bExisteDescuentoHijo = false;
							break;
						}
					}
				}
				else
					bExisteDescuentoHijo = false;
			}
			else
				bExisteDescuentoHijo = true;

			return bExisteDescuentoHijo;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="conceptoSol"></param>
		/// <param name="Concepto"></param>
		/// <returns></returns>
		private static bool ComparaDescuentos(DatosProcesamiento.CreditosFiscalesAgrupadorConceptos conceptoSol, DatosProcesamiento.ConceptoOriginalDB Concepto)
		{
			bool ExisteDescuento = true;

			if (conceptoSol.Descuentos != null)
			{
				//Consulta todos los descuentos del concepto
				List<DatosProcesamiento.DescuentosOriginal> DescuentosOriginalDB = BusquedaLineaCaptura.ConsultaDescuentosPorConcepto(Concepto.IdAgrupador, Concepto.IdProcesamiento, Concepto.IdConceptoOriginal);

				if (conceptoSol.Descuentos.Count() == DescuentosOriginalDB.Count)
				{
					foreach (DatosProcesamiento.CreditosFiscalesAgrupadorConceptosDescuento descuentoOriginalSol in conceptoSol.Descuentos)
					{
						// ConceptoOriginal, IdDescuento, ImporteDescuento
						DatosProcesamiento.DescuentosOriginal Descuento = DescuentosOriginalDB.Where(d => conceptoSol.Clave == d.ConceptoOriginal &&
							descuentoOriginalSol.IdDescuento == d.IdDescuento &&
							descuentoOriginalSol.ImporteDescuento == d.ImporteDescuento).FirstOrDefault();

						if (Descuento != null)
						{
							//Eliminar descuento de original
							DescuentosOriginalDB.Remove(Descuento);
						}
						else
						{
							ExisteDescuento = false;
							break;
						}
					}
				}
				else
					ExisteDescuento = false;
			}
			else
				ExisteDescuento = false;

			return ExisteDescuento;
		}

		/// <summary>
		/// Busca los datos de una línea de captura por procesamiento
		/// </summary>
		/// <param name="IdProcesamiento">Id del procesamiento a buscar</param>
		/// <returns>RespuestaLC datos de línea de captura</returns>
		private static RespuestaLC BuscaDatosLC(Guid IdProcesamiento)
		{
			var respuestaLC = new RespuestaLC();

			try
			{
				respuestaLC = BusquedaLineaCaptura.ConsultaLineaCapturaPorProcesamiento(IdProcesamiento);
			}
			catch
			{
				throw;
			}

			return respuestaLC;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="Folio"></param>
		/// <returns></returns>
		public static RespuestaSolicitudOriginal BuscaXMLOriginal(string Folio)
		{
			var vRespuestaXmlOriginal = new RespuestaSolicitudOriginal();

			try
			{
				vRespuestaXmlOriginal = BusquedaLineaCaptura.ConsultaXMLOriginal(Folio);
			}
			catch
			{
				throw;
			}

			return vRespuestaXmlOriginal;
		}

		/// <summary>
		/// Busca los datos de resoluciones de una línea de captura
		/// </summary>
		/// <param name="lineaCaptura">Línea de captura</param>
		/// <returns>RespuestaResolucionesEnLC datos de resolucion determinante</returns>
		public static RespuestaResolucionesEnLC BuscaResolucionesEnLC(string lineaCaptura)
		{
			var respuestaResolucionesEnLC = new RespuestaResolucionesEnLC();

			try
			{
				respuestaResolucionesEnLC = BusquedaLineaCaptura.ConsultaResolucionesEnLineaCaptura(lineaCaptura);
			}
			catch
			{
				throw;
			}

			return respuestaResolucionesEnLC;
		}

		/// <summary>
		/// Busqueda de lineas captura existentes
		/// </summary>
		/// <param name="solicitud">solicitud de lineas de captura</param>
		/// <returns>RespuestaLineasCapturaExistentes</returns>
		public static RespuestaLineasCapturaExistentes BuscaLineasCapturaExistentes(SolicitudLineasCapturaExistentes solicitud)
		{
			var respuestaLcExistentes = new RespuestaLineasCapturaExistentes();
			var lineasCaptura = new List<RespuestaLineasCapturaExistentesDatosLinea>();
			try
			{
				foreach (SolicitudLineasCapturaExistentesResolucion resolucion in solicitud.Resoluciones)
				{
					List<RespuestaLineasCapturaExistentesDatosLinea> lineasCapturaPorResolucion = BusquedaLineaCaptura.ConsultaLineasDeCapturaExistentes(resolucion.NumeroResolucion, resolucion.FechaDocumento, Convert.ToInt32(resolucion.Autoridad), resolucion.ALR, resolucion.RFC);
					lineasCaptura.AddRange(lineasCapturaPorResolucion);
				}
				respuestaLcExistentes.LineasCaptura = lineasCaptura.ToArray();

				return respuestaLcExistentes;
			}
			catch
			{
				throw;
			}
		}
	}
}