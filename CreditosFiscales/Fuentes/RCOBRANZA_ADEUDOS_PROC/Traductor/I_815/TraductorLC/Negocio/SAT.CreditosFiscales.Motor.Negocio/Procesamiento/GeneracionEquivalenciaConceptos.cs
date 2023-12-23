//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.Negocio:SAT.CreditosFiscales.Motor.Negocio.GeneracionEquivalenciaConceptos:1:12/07/2012[Assembly:1.0:12/07/2013])

// Referencias de sistema.
using System;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.Xml.XPath;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Concurrent;

// Referencias personalidades.
using SAT.CreditosFiscales.Motor.Utilidades;
using SAT.CreditosFiscales.Motor.AccesoDatos.Procesamiento;
using SAT.CreditosFiscales.Motor.Entidades.Catalogos;
using SAT.CreditosFiscales.Motor.Entidades.CodigosError;
using SAT.CreditosFiscales.Motor.Entidades.Enumeradores;
using SAT.CreditosFiscales.Motor.Entidades.Herramientas;
using SAT.CreditosFiscales.Motor.Entidades.Procesamiento;
using SAT.CreditosFiscales.Motor.Entidades.DatosProcesamiento;
using SAT.CreditosFiscales.Motor.Negocio.AccesoLogEventos;

namespace SAT.CreditosFiscales.Motor.Negocio.Procesamiento
{
	/// <summary>
	/// Clase que se utiliza para hacer la conversión de conceptos Ley a conceptos DyP.
	/// </summary>
	public static class GeneracionEquivalenciaConceptos
	{
		/// <summary>
		/// Método que inicia el proceso de transformación de conceptos originales a conceptos D y P.
		/// </summary>
		/// <param name="xmlMedioCanonico">Xml adaptado al esquema de canónico de salida.</param>
		/// <returns>Entidad de créditos fiscales con las propiedades requeridas llenas.</returns>
		public static Entidades.DatosProcesamiento.CreditosFiscales GeneraMensajeLineaCaptura(string xmlMedioCanonico)
		{
			try
			{
				var lstConceptosDypFinal = new List<CreditosFiscalesConcepto>();
				Entidades.DatosProcesamiento.CreditosFiscales creditosFiscales = MetodosComunes.DeserializaObjectoPorXml(xmlMedioCanonico);
				if (Catalogos.AplicationSettings.ConsultaConfiguracion<string>(Constantes.ActivaNeteo) == "1")
				{
					var xmlDocument = new XmlDocument();
					xmlDocument.LoadXml(xmlMedioCanonico);
					Entidades.DatosProcesamiento.CreditosFiscales respuesta = EliminaDescuento(xmlDocument);
					creditosFiscales.Conceptos = ObtieneConceptosFinal(respuesta).ToArray();
					creditosFiscales.Agrupadores = respuesta.Agrupadores;
					// string s = MetodosComunes.SerlializaObjeto(respuesta);
				}
				else
					creditosFiscales.Conceptos = ObtieneConceptosFinal(creditosFiscales).ToArray();
/*
				short secuencia = 1;
				for (int i = 0; i < creditosFiscales.Conceptos.Count(); i++)
				{
					creditosFiscales.Conceptos[i].NoSecuencia = secuencia;
					secuencia++;
				}
*/
				return creditosFiscales;
			}
			catch (Exception exception)
			{
				LogEventos.EscribirEntradaLog((int) EnumErrorMotor.ErrorCrearConceptoDyP, exception);
				throw;
			}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="documento"></param>
		/// <returns></returns>
		public static Entidades.DatosProcesamiento.CreditosFiscales EliminaDescuento(XmlDocument documento)
		{
			Entidades.DatosProcesamiento.CreditosFiscales creditosFiscales = MetodosComunes.DeserializaObjectoPorXml(documento.InnerXml);

			// Conceptos
			foreach (CreditosFiscalesAgrupador agrupador in creditosFiscales.Agrupadores)
			{
				foreach (CreditosFiscalesAgrupadorConceptos concepto in agrupador.ConceptosOriginal)
				{
					if (concepto.Descuentos != null && concepto.Descuentos.Any())
					{
						foreach (CreditosFiscalesAgrupadorConceptosDescuento descuento in concepto.Descuentos)
						{
							concepto.ImporteHistorico -= descuento.ImporteDescuento;
							concepto.ImporteTotalCargos -= descuento.ImporteDescuento;
							concepto.ImporteTotalAbonos -= descuento.ImporteDescuento;
						}

						concepto.Descuentos = new CreditosFiscalesAgrupadorConceptosDescuento[0];
					}

					if (concepto.Hijos != null)
					{
						foreach (var conceptoHijo in concepto.Hijos)
						{
							if (conceptoHijo.Descuentos != null && conceptoHijo.Descuentos.Any())
							{
								foreach (var descuentoHijo in conceptoHijo.Descuentos)
								{
									conceptoHijo.ImporteHistorico -= descuentoHijo.ImporteDescuento;
									conceptoHijo.ImporteTotalCargos -= descuentoHijo.ImporteDescuento;
									conceptoHijo.ImporteTotalAbonos -= descuentoHijo.ImporteDescuento;
								}
							}

							conceptoHijo.Descuentos = new CreditosFiscalesAgrupadorConceptosHijoDescuento[0];
						}
					}
				}
			}

			return creditosFiscales;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="creditosFiscales"></param>
		/// <returns></returns>
		private static List<CreditosFiscalesConcepto> ObtieneConceptosFinal(Entidades.DatosProcesamiento.CreditosFiscales creditosFiscales)
		{
			var lstConceptosDypFinal = new BlockingCollection<CreditosFiscalesConcepto>();
			try
			{
				Parallel.For
				(
					0
					, creditosFiscales.Agrupadores.Count()
					, new ParallelOptions {MaxDegreeOfParallelism = 4 }
					, i =>
					{
						for (int j = 0; j < creditosFiscales.Agrupadores[i].ConceptosOriginal.Count(); j++)
						{
							var concepto = new CreditosFiscalesConcepto();
							concepto.IdAgrupador = i + 1;
							concepto.NoSecuencia = (short) (j + 1);
							concepto.ImportePagar += creditosFiscales.Agrupadores[i].ConceptosOriginal[j].ImportePagar;
							concepto.EsCorrecto = creditosFiscales.Agrupadores[i].ConceptosOriginal[j].EsCorrecto;
							concepto.Ejercicio = creditosFiscales.Agrupadores[i].ConceptosOriginal[j].Ejercicio;

							// El ejericicio se pasa tal cual del concepto original.
							concepto.FechaCausacion = creditosFiscales.Agrupadores[i].ConceptosOriginal[j].FechaCausacion;

							// Igual al fecha de causación, tendría que aplicarse alguna validación.
							concepto.ClaveOrigen = creditosFiscales.Agrupadores[i].ConceptosOriginal[j].Clave;

							ObtenerConceptoEquivalencia(creditosFiscales.DatosGenerales.IdAplicacion, creditosFiscales.Agrupadores[i].ConceptosOriginal[j], ref concepto, creditosFiscales.DatosGenerales.TipoDocumento);
							ObtenerPeriodicidadEquivalencia(creditosFiscales.DatosGenerales.IdAplicacion, ref concepto, creditosFiscales.Agrupadores[i].ConceptosOriginal[j]); // se obtiene la periodicidad equivalencia.
							ObtenerPeriodoEquivalencia(creditosFiscales.DatosGenerales.IdAplicacion, ref concepto, creditosFiscales.Agrupadores[i].ConceptosOriginal[j]);

							var listaTransacciones = new List<CreditosFiscalesConceptoTransaccion>();
							string objetoSerializado = MetodosComunes.SerlializaObjeto(creditosFiscales.Agrupadores[i].ConceptosOriginal[j]);
							if (creditosFiscales.Agrupadores[i].ConceptosOriginal[j].ImporteHistorico >= 0)
							{
								listaTransacciones.Add	(
															GeneradorTransaccion.Instancia.GeneraTransaccion
															(
																creditosFiscales.DatosGenerales.IdAplicacion
																, creditosFiscales.DatosGenerales.TipoDocumento
																, Constantes.ImporteHistoricoPadre
																, creditosFiscales.Agrupadores[i].ConceptosOriginal[j].ImporteHistorico
																, objetoSerializado
															)
														);
							}

							//En este caso no se toma en cuenta el concepto serializado.
							if (creditosFiscales.Agrupadores[i].ConceptosOriginal[j].ImportePagar > 0)
							{
								//var formaPago = MetodosComunes.ObtenerMarcaFormaPago(conceptoOriginal);
								string formaPago = creditosFiscales.Agrupadores[i].ConceptosOriginal[j].FormaPago;
								listaTransacciones.Add
								(
									GeneradorTransaccion.Instancia.GeneraTransaccion
									(
										creditosFiscales.DatosGenerales.IdAplicacion
										, creditosFiscales.DatosGenerales.TipoDocumento
										, formaPago
										, creditosFiscales.Agrupadores[i].ConceptosOriginal[j].ImportePagar
										, objetoSerializado
									)
								);
							}

							if (creditosFiscales.Agrupadores[i].ConceptosOriginal[j].ImporteParteActualizada > 0)
							{
								listaTransacciones.Add
								(
									GeneradorTransaccion.Instancia.GeneraTransaccion
									(
										creditosFiscales.DatosGenerales.IdAplicacion
										, creditosFiscales.DatosGenerales.TipoDocumento
										, Constantes.ImporteParteActualizadaPadre
										, creditosFiscales.Agrupadores[i].ConceptosOriginal[j].ImporteParteActualizada
										, objetoSerializado
									)
								);
							}

							listaTransacciones.AddRange
							(
								GeneradorTransaccion.Instancia.GeneraTransaccion
								(
									creditosFiscales.DatosGenerales.IdAplicacion
									, creditosFiscales.DatosGenerales.TipoDocumento
									, objetoSerializado
								)
							);

							// Se agregan transacciones de total monto cargo y total monto abono y algunas más como ImporteSinPagoInicial
							if (creditosFiscales.Agrupadores[i].ConceptosOriginal[j].Descuentos != null)
							{
								foreach (var descuento in creditosFiscales.Agrupadores[i].ConceptosOriginal[j].Descuentos)
								{
									listaTransacciones.Add
									(
												GeneradorTransaccion.Instancia.GeneraTransaccion
												(
													creditosFiscales.DatosGenerales.IdAplicacion
													, creditosFiscales.DatosGenerales.TipoDocumento
													, Constantes.ImporteDescuentoPadre
													, descuento
												)
									);
								}
							}

							if (creditosFiscales.Agrupadores[i].ConceptosOriginal[j].Hijos != null)
							{
								foreach (CreditosFiscalesAgrupadorConceptosHijo conceptoHijo in creditosFiscales.Agrupadores[i].ConceptosOriginal[j].Hijos)
								{
									string objeSerializadoHijo = MetodosComunes.SerlializaObjeto(conceptoHijo);
									concepto.ImportePagar += conceptoHijo.ImportePagar;
									if (conceptoHijo.ImportePagar > 0)
									{
										//var formaPago = MetodosComunes.ObtenerMarcaFormaPago(conceptoHijo);
										string formaPago = conceptoHijo.FormaPago;
										CreditosFiscalesConceptoTransaccion transaccion = GeneradorTransaccion.Instancia.GeneraTransaccion
															(
																creditosFiscales.DatosGenerales.IdAplicacion
																, creditosFiscales.DatosGenerales.TipoDocumento
																, formaPago
																, conceptoHijo.ImportePagar
																, objeSerializadoHijo
															);
										conceptoHijo.TipoTransaccion = transaccion.Tipo;
										listaTransacciones.Add(transaccion);
									}

									if (conceptoHijo.ImporteParteActualizada > 0)
									{
										CreditosFiscalesConceptoTransaccion transaccion = GeneradorTransaccion.Instancia.GeneraTransaccion
															(
																creditosFiscales.DatosGenerales.IdAplicacion
																, creditosFiscales.DatosGenerales.TipoDocumento
																, Constantes.ImporteParteActualizadaHijo
																, conceptoHijo.ImporteParteActualizada
																, objeSerializadoHijo
															);
										conceptoHijo.TipoTransaccion = transaccion.Tipo;
										listaTransacciones.Add(transaccion);
									}

									listaTransacciones.AddRange(
										GeneradorTransaccion.Instancia.GeneraTransaccion(
																							creditosFiscales.DatosGenerales.IdAplicacion
																							, creditosFiscales.DatosGenerales.TipoDocumento
																							, objeSerializadoHijo
																						)
																);

									// Se agregan total de transacciones monto cargo y abono para hijos y algunas más como ImporteSinPagoInicial
									if (conceptoHijo.ImporteHistorico >= 0)
									{
										string objetoSerializadoHijo = MetodosComunes.SerlializaObjeto(conceptoHijo);
										CreditosFiscalesConceptoTransaccion transaccion = GeneradorTransaccion.Instancia.GeneraTransaccion
																							(
																								creditosFiscales.DatosGenerales.IdAplicacion
																								, creditosFiscales.DatosGenerales.TipoDocumento
																								, conceptoHijo.Clave
																								, objetoSerializadoHijo
																								, conceptoHijo.ImporteHistorico
																							);
										conceptoHijo.TipoTransaccion = transaccion.Tipo;
										listaTransacciones.Add(transaccion);
									}

									if (conceptoHijo.Descuentos != null)
									{
										foreach (var descuentoHijo in conceptoHijo.Descuentos)
										{
											listaTransacciones.Add	(
																		GeneradorTransaccion.Instancia.GeneraTransaccion
																		(
																			creditosFiscales.DatosGenerales.IdAplicacion
																			, creditosFiscales.DatosGenerales.TipoDocumento
																			, Constantes.ImporteDescuentoHijo
																			, descuentoHijo
																		)
																	);
										}
									}
								}
							}

							ValidarYCompletarTransaccionesRequeridas(
																		creditosFiscales.DatosGenerales.IdAplicacion
																		, creditosFiscales.DatosGenerales.TipoDocumento
																		, ref listaTransacciones
																	);

							// Se eliminan las transacciones de total monto cargo y total monto abono que no apliquen
							List<CreditosFiscalesConceptoTransaccion> listEliminar = listaTransacciones.Where(transaccion => transaccion.Clave == "-1").ToList();
							foreach (var transaccion in listEliminar)
								listaTransacciones.Remove(transaccion);

							concepto.Transacciones = listaTransacciones.ToArray();
							lstConceptosDypFinal.Add(concepto);
						}
					}
				);
			}
			catch (Exception exception)
			{
				LogEventos.EscribirEntradaLog((int) EnumErrorMotor.ErrorCrearConceptoDyP, exception);
				throw;
			}
			return lstConceptosDypFinal.ToList();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="idAplicacion"></param>
		/// <param name="idDocumento"></param>
		/// <param name="listaTransacciones"></param>
		private static void ValidarYCompletarTransaccionesRequeridas(int idAplicacion, sbyte idDocumento, ref List<CreditosFiscalesConceptoTransaccion> listaTransacciones)
		{
			try
			{
				List<CatTransaccion> transaccionesRequeridas = ObtenerTransaccionesRequeridas(idAplicacion, idDocumento);
				foreach (var transaccionRequerida in transaccionesRequeridas)
				{
					bool existe = false;
					foreach (
							var transaccion in
								listaTransacciones.Where(
									transaccion => transaccionRequerida.IdTransaccion == transaccion.Clave)
							)
					{
						existe = true;
					}

					if (!existe)
						listaTransacciones.Add	(
													new CreditosFiscalesConceptoTransaccion
														{
															Clave = transaccionRequerida.IdTransaccion
															, Descripcion = transaccionRequerida.Descripcion
															, Importe = 0
															, Tipo = transaccionRequerida.TipoTransaccion
														}
												);
				}
			}
			catch (Exception exception)
			{
				LogEventos.EscribirEntradaLog((int) EnumErrorMotor.ErrorCrearConceptoDyP, exception);
				throw;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="idAplicacion"></param>
		/// <param name="idDocumento"></param>
		/// <returns></returns>
		private static List<CatTransaccion> ObtenerTransaccionesRequeridas(int idAplicacion, sbyte idDocumento)
		{
			try
			{
				return DalConceptosEquivalencia.ObtenerTransaccionesRequeridasPorDocumento(idAplicacion, idDocumento);
			}
			catch (Exception exception)
			{
				LogEventos.EscribirEntradaLog((int) EnumErrorMotor.ErrorAccesoBaseDeDatos, exception);
				throw;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="idAplicacion"></param>
		/// <param name="concepto"></param>
		/// <param name="conceptoOriginal"></param>
		private static void ObtenerPeriodoEquivalencia(int idAplicacion, ref CreditosFiscalesConcepto concepto, CreditosFiscalesAgrupadorConceptos conceptoOriginal)
		{
			bool esAplicarRegla = false;
			var lstPeriodoEquivalencia = new List<CatPeriodoEquivalencia>();
			try
			{
				//lstPeriodoEquivalencia = DalConceptosEquivalencia.ObtenerPeriodoEquivalencia(idAplicacion, conceptoOriginal.PeriodoId, conceptoOriginal.PeriodicidadId);
				lstPeriodoEquivalencia = DatosCache.CacheRepository.PeriodoEquivalencia.Where(
										  p => p.IdAplicacion == idAplicacion && p.PeriodoOrigen == conceptoOriginal.PeriodoId.ToString()
										  && p.PeriodicidadOrigen == conceptoOriginal.PeriodicidadId).ToList();
			}
			catch (Exception exception)
			{
				string mensaje = string.Format("Error al obtener periodicidad equivalencia con IdAplicacion {0}, periodoId {1}, periodicidadId {2}", idAplicacion, conceptoOriginal.PeriodoId, concepto.Periodicidad);
				LogEventos.EscribirEntradaLog((int) EnumErrorMotor.ErrorAccesoBaseDeDatos, mensaje, exception);
			}

			if (lstPeriodoEquivalencia == null || lstPeriodoEquivalencia.Count > 1)
				esAplicarRegla = true;

			if (esAplicarRegla)
			{
				var doc = new XmlDocument();
				doc.InnerXml = MetodosComunes.SerlializaObjeto(conceptoOriginal);
				string idPeriodo = GenerarPeriodoDyP(idAplicacion, doc.InnerXml, conceptoOriginal.PeriodoId);
				if (idPeriodo == "-1")
				{
					string mensaje = string.Format("No existe periodo dyp para el periodoId origen {0}, periodicidad origen {1} en la aplicaciónId {2}", conceptoOriginal.PeriodoId, conceptoOriginal.PeriodicidadId, idAplicacion);
					LogEventos.EscribirEntradaLog((int) EnumErrorMotor.ErrorCrearConceptoDyP, mensaje, EventLogEntryType.Error);
					throw new Exception(mensaje);
				}

				CatPeriodoEquivalencia periodoDyP = DalConceptosEquivalencia.ObtenerPeriodoPorId(idPeriodo);
				if (periodoDyP != null)
				{
					concepto.ClavePeriodo = periodoDyP.IdPeriodoDyP;
					concepto.Periodo = periodoDyP.DescripcionDyP;
				}
			}
			else
			{
				CatPeriodoEquivalencia periodoEquivalencia = lstPeriodoEquivalencia.FirstOrDefault();
				if (periodoEquivalencia != null)
				{
					concepto.ClavePeriodo = periodoEquivalencia.IdPeriodoDyP;
					concepto.Periodo = periodoEquivalencia.DescripcionDyP;
				}
				else
				{
					string mensaje = string.Format("No existe periodo dyp para la periodicidadId origen: {0} y el periodoId origen {1} en la aplicaciónId {2}", conceptoOriginal.PeriodicidadId, conceptoOriginal.PeriodoId, idAplicacion);
					LogEventos.EscribirEntradaLog((int) EnumErrorMotor.ErrorCrearConceptoDyP, mensaje, EventLogEntryType.Error);
					throw new Exception(mensaje);
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="idAplicacion"></param>
		/// <param name="concepto"></param>
		/// <param name="conceptoOriginal"></param>
		private static void ObtenerPeriodicidadEquivalencia(int idAplicacion, ref CreditosFiscalesConcepto concepto, CreditosFiscalesAgrupadorConceptos conceptoOriginal)
		{
			bool esAplicarRegla = false;
			var lstPeriodicidadEquivalencia = new List<CatPeriodicidadEquivalencia>();
			try
			{
				//lstPeriodicidadEquivalencia = DalConceptosEquivalencia.ObtenerPeriodicidadEquivalencia(idAplicacion,conceptoOriginal.PeriodicidadId);
				lstPeriodicidadEquivalencia = DatosCache.CacheRepository.PeriodicidadEquivalencia.Where(p => p.IdAplicacion == idAplicacion && p.PeriodicidadOrigen == conceptoOriginal.PeriodicidadId.ToString()).ToList();
			}
			catch (Exception exception)
			{
				string mensaje = string.Format("Error al obtener periodicidad equivalencia con IdAplicación {0}, periodicidadId {1}", idAplicacion, conceptoOriginal.PeriodicidadId);
				LogEventos.EscribirEntradaLog((int) EnumErrorMotor.ErrorAccesoBaseDeDatos, mensaje, exception);
			}

			if (lstPeriodicidadEquivalencia == null || lstPeriodicidadEquivalencia.Count > 1)
				esAplicarRegla = true;

			if (esAplicarRegla)
			{
				var doc = new XmlDocument();
				doc.InnerXml = MetodosComunes.SerlializaObjeto(conceptoOriginal); // Serializa el concepto original.
				string idPeriodicidad = GenerarPeriodicidadDyP(idAplicacion, doc.InnerXml, conceptoOriginal.PeriodicidadId);
				if (idPeriodicidad == "-1")
				{
					string mensaje = string.Format("No existe periodicidad dyp para la peridicidadId origen {0} en la aplicaciónId {1}", conceptoOriginal.PeriodicidadId, idAplicacion);
					LogEventos.EscribirEntradaLog((int) EnumErrorMotor.ErrorCrearConceptoDyP, mensaje, EventLogEntryType.Error);
					throw new Exception(mensaje);
				}

				CatPeriodicidadEquivalencia periodicidadDyP = DalConceptosEquivalencia.ObtenerPeriodicidadEquivalencia(idPeriodicidad);
				if (periodicidadDyP != null)
				{
					concepto.TipoPeriodicidad = periodicidadDyP.IdPeriodicidadDyP;
					concepto.Periodicidad = periodicidadDyP.DescripcionPeriodicidadDyP;
				}
			}
			else
			{
				CatPeriodicidadEquivalencia catPeriodicidadEquivalencia = lstPeriodicidadEquivalencia.FirstOrDefault();
				if (catPeriodicidadEquivalencia != null)
				{
					concepto.TipoPeriodicidad = catPeriodicidadEquivalencia.IdPeriodicidadDyP;
					concepto.Periodicidad = catPeriodicidadEquivalencia.DescripcionPeriodicidadDyP;
				}
				else
				{
					string mensaje = string.Format("No existe periodicidad dyp para la peridicidadId {0} en la aplicaciónId {1} ", conceptoOriginal.PeriodicidadId, idAplicacion);
					LogEventos.EscribirEntradaLog((int) EnumErrorMotor.ErrorCrearConceptoDyP, mensaje, EventLogEntryType.Error);
					throw new Exception(mensaje);
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="idAplicacion"></param>
		/// <param name="conceptoOriginal"></param>
		/// <param name="concepto"></param>
		/// <param name="IdTipoDocumento"></param>
		private static void ObtenerConceptoEquivalencia(int idAplicacion, CreditosFiscalesAgrupadorConceptos conceptoOriginal, ref CreditosFiscalesConcepto concepto, int IdTipoDocumento)
		{
			try
			{
				bool esAplicarRegla = false;
				IEnumerable<CreditosFiscalesConcepto> listaConceptosDyp = DatosCache.CacheRepository.ConceptosEquivalencia.Where(c => c.IdAplicacion == idAplicacion && c.ClaveOrigen == conceptoOriginal.Clave);

				//Se busca en el catálogo de equivalencias
				if (listaConceptosDyp != null && listaConceptosDyp.Count() > 0)
				{
					// Si hay más de una o no se encuentra la equivalencia se debe buscar en el catálogo de reglas con el tipo.
					if (listaConceptosDyp.Count() > 1)
						esAplicarRegla = true;
					else
					{
						concepto.Clave = listaConceptosDyp.FirstOrDefault().Clave;
						concepto.Descripcion = listaConceptosDyp.FirstOrDefault().Descripcion;
					}
				}
				else
					esAplicarRegla = true; // si no encontro ningún concepto, buscar en las reglas.

				if (esAplicarRegla)
				{
					var doc = new XmlDocument();
					doc.InnerXml = MetodosComunes.SerlializaObjeto(conceptoOriginal); // serializa el concepto original.
					int idConcepto = GenerarConceptoEquivalencia(idAplicacion, doc.InnerXml, conceptoOriginal.Clave, IdTipoDocumento);
					if (idConcepto == -1)
					{
						string mensaje = string.Format("No existe concepto DyP para el concepto original {0} en la aplicaciónId {1}", conceptoOriginal.Clave, idAplicacion);
						LogEventos.EscribirEntradaLog((int) EnumErrorMotor.ErrorCrearConceptoDyP, mensaje, EventLogEntryType.Error);
						throw new Exception(mensaje);
					}

					CreditosFiscalesConcepto conceptoDyP = DalConceptosEquivalencia.ObtenerConceptoDyPorId(idAplicacion, idConcepto, conceptoOriginal.Clave);
					//ObtenerPeriodicidadEquivalencia(idAplicacion, ref conceptoDyP, conceptoOriginal);
					//lstConceptosDypFinal.Add(conceptoDyP);
					concepto.Clave = conceptoDyP.Clave;
					concepto.Descripcion = conceptoDyP.Descripcion;
				}
			}
			catch (Exception exception)
			{
				LogEventos.EscribirEntradaLog((int) EnumErrorMotor.ErrorCrearConceptoDyP, exception);
				throw;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="idAplicacion"></param>
		/// <param name="objetoSerializable"></param>
		/// <param name="periodoId"></param>
		/// <returns></returns>
		private static string GenerarPeriodoDyP(int idAplicacion, string objetoSerializable, int periodoId)
		{
			try
			{
				//var lstReglasEequivalencia = DalConceptosEquivalencia.ObtenerReglasEquivalencia(idAplicacion, EnumTipoObjeto.Periodo);
				List<CatReglasEquivalencia> lstReglasEequivalencia = HerramientasReglasEquivalencia.ObtenerReglasEquivalencia(idAplicacion, EnumTipoObjeto.Periodo);
				XDocument objetoFiltro = XDocument.Parse(objetoSerializable);
				string idPeriodo = HerramientasReglasEquivalencia.AplicarReglasEquivalencia(objetoFiltro, lstReglasEequivalencia);
				return idPeriodo;
			}
			catch (SqlException sqlEx)
			{
				string mensaje = string.Format("Error al obtener Periodo de DyP con el periodo original {0} en la aplicaciónId {1}", periodoId, idAplicacion);
				LogEventos.EscribirEntradaLog((int) EnumErrorMotor.ErrorAccesoBaseDeDatos, mensaje, sqlEx);
				throw;
			}
			catch (Exception exception)
			{
				string mensaje = string.Format("Error al obtener periodo de DyP con el periodo original {0} en la aplicaciónId {1}", periodoId, idAplicacion);
				LogEventos.EscribirEntradaLog((int) EnumErrorMotor.ErrorCrearConceptoDyP, mensaje, exception);
				throw;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="idAplicacion"></param>
		/// <param name="objetoSerializado"></param>
		/// <param name="periodicidadId"></param>
		/// <returns></returns>
		private static string GenerarPeriodicidadDyP(int idAplicacion, string objetoSerializado, int periodicidadId)
		{
			try
			{
				//var lstReglasEquivalencia = DalConceptosEquivalencia.ObtenerReglasEquivalencia(idAplicacion, EnumTipoObjeto.Periodicidad);

				List<CatReglasEquivalencia> lstReglasEquivalencia = HerramientasReglasEquivalencia.ObtenerReglasEquivalencia(idAplicacion, EnumTipoObjeto.Periodicidad);
				var objetoFiltro = XDocument.Parse(objetoSerializado);
				string idPeriodicidad = HerramientasReglasEquivalencia.AplicarReglasEquivalencia(objetoFiltro, lstReglasEquivalencia);
				return idPeriodicidad;
			}
			catch (SqlException sqlEx)
			{
				string mensaje = string.Format("Error al obtener la periodicidad de DyP con la periodicidad original {0} en la aplicaciónId {1}", periodicidadId, idAplicacion);
				LogEventos.EscribirEntradaLog((int) EnumErrorMotor.ErrorAccesoBaseDeDatos, mensaje, sqlEx);
				throw;
			}
			catch (Exception exception)
			{
				string mensaje = string.Format("Error al obtener la periodicidad de DyP con la periodicidad original {0} en la aplicaciónId {1}", periodicidadId, idAplicacion);
				LogEventos.EscribirEntradaLog((int) EnumErrorMotor.ErrorCrearConceptoDyP, mensaje, exception);
				throw;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="idAplicacion"></param>
		/// <param name="objetoSerializado"></param>
		/// <param name="claveOriginal"></param>
		/// <param name="IdTipoDocumento"></param>
		/// <returns></returns>
		private static int GenerarConceptoEquivalencia(int idAplicacion, string objetoSerializado, string claveOriginal, int IdTipoDocumento)
		{
			try
			{
				//var lstReglasEquivalencia = DalConceptosEquivalencia.ObtenerReglasPorConcepto(idAplicacion, EnumTipoObjeto.Concepto, claveOriginal);
				List<CatReglasEquivalencia> lstReglasEquivalencia = HerramientasReglasEquivalencia.ObtenerReglasEquivalencia(idAplicacion, EnumTipoObjeto.Concepto, claveOriginal, IdTipoDocumento);

				XDocument objetoFiltro = XDocument.Parse(objetoSerializado);
				int idConcepto = Convert.ToInt32(HerramientasReglasEquivalencia.AplicarReglasEquivalencia(objetoFiltro, lstReglasEquivalencia));
				return idConcepto;
			}
			catch (SqlException sqlEx)
			{
				string mensaje = string.Format("Error al obtener el concepto DyP para el concepto original {0} y la aplicación {1}", claveOriginal, idAplicacion);
				LogEventos.EscribirEntradaLog((int) EnumErrorMotor.ErrorAccesoBaseDeDatos, mensaje, sqlEx);
				throw;
			}
			catch (Exception exception)
			{
				string mensaje = string.Format("Error al obtener el concepto DyP para el concepto original {0} y la aplicación {1}", claveOriginal, idAplicacion);
				LogEventos.EscribirEntradaLog((int) EnumErrorMotor.ErrorCrearConceptoDyP, mensaje, exception);
				throw;
			}
		}
	}
}