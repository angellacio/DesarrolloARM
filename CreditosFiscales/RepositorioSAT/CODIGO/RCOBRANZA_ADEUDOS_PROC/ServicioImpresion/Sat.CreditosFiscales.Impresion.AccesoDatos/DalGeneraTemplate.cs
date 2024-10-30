//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Impresion.AccesoDatos:Sat.CreditosFiscales.Impresion.AccesoDatos.DalGeneraTemplate:1:12/07/2013[Assembly:1.0:12/07/2013])

// Referencias de sistema.
using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;

// Referencias personalizadas.
using Sat.CreditosFiscales.Impresion.Entidades;
using Sat.CreditosFiscales.Impresion.Herramientas;

namespace Sat.CreditosFiscales.Impresion.AccesoDatos
{
	/// <summary>
	/// Clase para el manejo de los metodos requeridos para la obtención de la información de los diversos documentos en base de datos
	/// </summary>
	public class DalGeneraTemplate
	{
		/// <summary>
		/// Obtiene la información del formato para pago total o a cuenta
		/// </summary>
		/// <param name="listaDeFolios">Lista de folios</param>
		/// <returns><see cref="FormatoPago"/></returns>
		public FormatoPago ObtenerFormatoPago(List<string> listaDeFolios)
		{
			var documentos = new FormatoPago();
			Database db = DatabaseFactory.CreateDatabase();

			foreach (var folio in listaDeFolios)
			{
				DbCommand cmd = db.GetStoredProcCommand("pObtenerFormatoPago");
				db.AddInParameter(cmd, "@Folio", DbType.String, folio.ToString());
				using (var ds = db.ExecuteDataSet(cmd))
				{
					if (ds != null)
					{
						if (ds.Tables.Count == 2)
						{
							if (ds.Tables[0].Rows.Count > 0)
							{
								DataRow dr = ds.Tables[0].Rows[0];
								int version = Convert.ToInt16(dr["version"]);
								var lc = new FPLC();
								lc.ALR = dr["ALR"].ToString();
								lc.Fecha = MetodosComunes.FormatoFecha(Convert.ToDateTime(dr["Fecha"]), MetodosComunes.TipoFecha.FechaMesTexto);
								lc.FechaVigencia = MetodosComunes.FormatoFecha(Convert.ToDateTime(dr["FechaVigencia"]), MetodosComunes.TipoFecha.FechaMesNumero);
								lc.FechaYHora = MetodosComunes.FormatoFechaHora(Convert.ToDateTime(dr["FechaYHora"]));
								lc.FolioLC = MetodosComunes.FormatoFolioLineaCaptura(dr["FolioLC"].ToString());
								lc.ImporteActualizadoT = MetodosComunes.FormatoMoneda(Convert.ToDecimal(dr["ImporteActualizadoT"]));
								lc.ImporteAPagarT = MetodosComunes.FormatoMoneda(Convert.ToDecimal(dr["ImporteAPagarT"]));
								lc.Nombre = dr["Nombre"].ToString();
								lc.NumLC = MetodosComunes.FormatoLineaCaptura(dr["NumLC"].ToString());
								lc.Observaciones = dr["Observaciones"].ToString();
								lc.RFC = dr["RFC"].ToString();
								lc.Leyenda = dr["Leyenda"].ToString();
								lc.version = version;
								lc.CodigoBarras = new FCodigoBarras()
								{
									CodigoBarras = dr["NumLC"].ToString() + " " + MetodosComunes.SinFormatoMoneda(Convert.ToDecimal(dr["ImporteAPagarT"]))
									, ImagenCB = string.Empty
									, ImagenQR = string.Empty
									, version = version
								};

								foreach (DataRow drDoc in ds.Tables[1].Rows)
								{
									lc.Documento.Add
										(
										new FPDocumento()
												{
													ImporteActualizado = MetodosComunes.FormatoMoneda(Convert.ToDecimal(drDoc["ImporteActualizado"]))
													, ImporteAPagar = MetodosComunes.FormatoMoneda(Convert.ToDecimal(drDoc["ImporteAPagar"]))
													, NumDocumento = drDoc["NumDocumento"].ToString()
													, version = version
												}
										);
								}

								documentos.LC.Add(lc);
							}
						}
					}
				}
			}

			return documentos;
		}

		/// <summary>
		/// Obtiene la información del formato para la confirmación de movimientos contables
		/// </summary>
		/// <param name="listaDeFolios">Lista de folios</param>
		/// <returns><see cref="FormatoConfirmacionDeMovimientosContables"/></returns>
		public FormatoConfirmacionDeMovimientosContables ObtenerFormatoConfirmacionDeMovimientosContables(List<string> listaDeFolios)
		{
			var documentos = new FormatoConfirmacionDeMovimientosContables();
			Database db = DatabaseFactory.CreateDatabase();
			foreach (var folio in listaDeFolios)
			{
				DbCommand cmd = db.GetStoredProcCommand("pObtenerFormatoConfirmacionDeMovimientosContables");
				db.AddInParameter(cmd, "@Folio", DbType.String, folio.ToString());
				using (var ds = db.ExecuteDataSet(cmd))
				{
					if (ds != null)
					{
						if (ds.Tables.Count == 3)
						{
							if (ds.Tables[0].Rows.Count > 0)
							{
								DataRow dr = ds.Tables[0].Rows[0];
								int version = Convert.ToInt16(dr["version"]);
								var lc = new FCMCLC();
								lc.ALR = dr["ALR"].ToString();
								lc.FechaYHora = MetodosComunes.FormatoFechaHora(Convert.ToDateTime(dr["FechaYHora"]));
								lc.FolioLC = MetodosComunes.FormatoFolioLineaCaptura(dr["FolioLC"].ToString());
								lc.Importe = MetodosComunes.FormatoMoneda(Convert.ToDecimal(dr["Importe"]));
								lc.Nombre = dr["Nombre"].ToString();
								lc.Observaciones = dr["Observaciones"].ToString();
								lc.Referencia = dr["Referencia"].ToString();
								lc.RFC = dr["RFC"].ToString();

								lc.version = version;

								decimal totalAbonos = 0;
								decimal totalCargos = 0;

								foreach (DataRow drCargo in ds.Tables[1].Rows)
								{
									decimal importeC = Convert.ToDecimal(drCargo["ImporteC"]);
									totalCargos += importeC;
									lc.Cargos.Add(new FCMCCargos()
									{
										ClaveC = drCargo["ClaveC"].ToString(),
										DescripcionC = drCargo["DescripcionC"].ToString(),
										ImporteC = MetodosComunes.FormatoMoneda(importeC),
										version = version
									});
								}

								foreach (DataRow drAbono in ds.Tables[2].Rows)
								{
									decimal importeA = Convert.ToDecimal(drAbono["ImporteA"]);
									totalAbonos += importeA;
									lc.Abonos.Add(new FCMCAbonos()
									{
										ClaveA = drAbono["ClaveA"].ToString(),
										DescripcionA = drAbono["DescripcionA"].ToString(),
										ImporteA = MetodosComunes.FormatoMoneda(importeA),
										version = version
									});
								}

								lc.TotalA = MetodosComunes.FormatoMoneda(totalAbonos);
								lc.TotalC = MetodosComunes.FormatoMoneda(totalCargos);

								documentos.LC.Add(lc);
							}
						}
					}
				}
			}

			return documentos;
		}

		/// <summary>
		/// Obtiene la información para el formato de confirmación de rectificaciones contables
		/// </summary>
		/// <param name="listaDeFolios">Lista de folios</param>
		/// <returns><see cref="FormatoConfirmacionDeRectificacionesContables"/></returns>
		public FormatoConfirmacionDeRectificacionesContables ObtenerFormatoConfirmacionDeRectificacionesContables(List<string> listaDeFolios)
		{
			var documentos = new FormatoConfirmacionDeRectificacionesContables();
			Database db = DatabaseFactory.CreateDatabase();
			foreach (var folio in listaDeFolios)
			{
				DbCommand cmd = db.GetStoredProcCommand("pObtenerFormatoConfirmacionDeRectificacionesContables");
				db.AddInParameter(cmd, "@Folio", DbType.String, folio.ToString());
				using (var ds = db.ExecuteDataSet(cmd))
				{
					if (ds != null)
					{
						if (ds.Tables.Count == 2)
						{
							if (ds.Tables[0].Rows.Count > 0)
							{
								DataRow dr = ds.Tables[1].Rows[0];
								int version = Convert.ToInt16(dr["version"]);

								var lc = new FCRCLC();
								lc.ALR = dr["ALR"].ToString();
								lc.Nombre = dr["Nombre"].ToString();
								lc.RFC = dr["RFC"].ToString();
								lc.FolioLC = MetodosComunes.FormatoFolioLineaCaptura(dr["FolioLC"].ToString());
								lc.FechaYHora = MetodosComunes.FormatoFechaHora(Convert.ToDateTime(dr["FechaYHora"]));
								lc.Convenio = dr["Convenio"].ToString();

								lc.TipoOperacion = dr["TipoOperacion"].ToString();
								lc.TipoRectificacion = dr["TipoRectificacion"].ToString();
								lc.ImporteOperacion = MetodosComunes.FormatoMoneda(Convert.ToDecimal(dr["ImporteOperacion"]));

								lc.DocumentoRectificar = MetodosComunes.FormatoFolioLineaCaptura(dr["DocumentoRectificar"].ToString());
								lc.FechaEmision = MetodosComunes.FormatoFecha(Convert.ToDateTime(dr["FechaEmision"]), MetodosComunes.TipoFecha.FechaMesTexto);

								lc.MotivoRectificacion = dr["MotivoRectificacion"].ToString();
								lc.version = version;

								decimal totalDice = 0;
								decimal totalDebeDecir = 0;
								decimal importeDice = 0;
								decimal importeDebeDecir = 0;

								// Dice
								lc.Dice = new List<FCRDice>();
								DataRow[] drDiceCreditos = ds.Tables[0].Select("Nivel <> 0 and Rectificacion = 'Dice'");
								int totalCreditosDice = drDiceCreditos.Length;
								if (totalCreditosDice > 0)
								{
									foreach (DataRow drDice in drDiceCreditos)
									{
										decimal importe = Convert.ToDecimal(drDice["Importe"]);
										lc.RFCDice = drDice["RFC"].ToString();
										lc.Dice.Add(new FCRDice()
										{
											Credito = drDice["Credito"].ToString(),
											ClaveCargo = drDice["ClaveCargo"].ToString(),
											DescripcionCargo = drDice["DescripcionCargo"].ToString(),
											Importe = MetodosComunes.FormatoMoneda(importe),
											Nivel = drDice["Nivel"].ToString(),
											version = version
										});
										importeDice += importe;
									}
								}
								else
									lc.Dice.Add(new FCRDice() { version = version });

								// Debe Decir
								lc.DebeDecir = new List<FCRDebeDecir>();
								DataRow[] drDebeDecirCreditos = ds.Tables[0].Select("Nivel <> 0 and Rectificacion = 'DebeDecir'");
								int totalCreditosDebeDecir = drDebeDecirCreditos.Length;
								if (totalCreditosDebeDecir > 0)
								{
									foreach (DataRow drDebeDecir in drDebeDecirCreditos)
									{
										lc.RFCDecir = drDebeDecir["RFC"].ToString();
										decimal importe = Convert.ToDecimal(drDebeDecir["Importe"]);
										lc.DebeDecir.Add(new FCRDebeDecir()
										{
											Credito = drDebeDecir["Credito"].ToString(),
											ClaveCargo = drDebeDecir["ClaveCargo"].ToString(),
											DescripcionCargo = drDebeDecir["DescripcionCargo"].ToString(),
											Importe = MetodosComunes.FormatoMoneda(importe),
											Nivel = drDebeDecir["Nivel"].ToString(),
											version = version
										});
										importeDebeDecir += importe;
									}
								}
								else
									lc.DebeDecir.Add(new FCRDebeDecir() { version = version });

								totalDice += importeDice;
								totalDebeDecir += importeDebeDecir;

								//--------------------------------------

								// --- Primer renglón
								// Dice
								lc.DiceRenglon1 = new List<FCRDiceR>();
								DataRow[] drDiceCreditoR1 = ds.Tables[0].Select("Nivel = 0 and Rectificacion = 'Dice'");
								DataRow drDiceR1 = drDiceCreditoR1[0];
								lc.RFCDice = drDiceR1["RFC"].ToString();
								importeDice = Convert.ToDecimal(drDiceR1["Importe"]);
								lc.DiceRenglon1.Add(new FCRDiceR()
								{
									Credito = drDiceR1["Credito"].ToString()
									, ClaveCargo = drDiceR1["ClaveCargo"].ToString()
									, DescripcionCargo = drDiceR1["DescripcionCargo"].ToString()
									, ClaveAbono = drDiceR1["ClaveAbono"].ToString()
									, DescripcionAbono = drDiceR1["DescripcionAbono"].ToString()
									, Importe = MetodosComunes.FormatoMoneda(importeDice)
									, NumCreditos = totalCreditosDice + 1
									, version = version

								});

								// Debe decir
								lc.DebeDecirRenglon1 = new List<FCRDebeDecirR>();
								DataRow[] drCreditoDebeDecirR1 = ds.Tables[0].Select("Nivel = 0 and Rectificacion = 'DebeDecir'");
								DataRow drDebeDecirR1 = drCreditoDebeDecirR1[0];
								lc.RFCDecir = drDebeDecirR1["RFC"].ToString();
								importeDebeDecir = Convert.ToDecimal(drDebeDecirR1["Importe"]);
								lc.DebeDecirRenglon1.Add(new FCRDebeDecirR()
								{
									Credito = drDebeDecirR1["Credito"].ToString()
									, ClaveCargo = drDebeDecirR1["ClaveCargo"].ToString()
									, DescripcionCargo = drDebeDecirR1["DescripcionCargo"].ToString()
									, ClaveAbono = drDebeDecirR1["ClaveAbono"].ToString()
									, DescripcionAbono = drDebeDecirR1["DescripcionAbono"].ToString()
									, Importe = MetodosComunes.FormatoMoneda(importeDebeDecir)
									, NumCreditos = totalCreditosDebeDecir + 1
									, version = version

								});

								totalDice += importeDice;
								totalDebeDecir += importeDebeDecir;
								//
								lc.TotalDice = MetodosComunes.FormatoMoneda(totalDice);
								lc.TotalDecir = MetodosComunes.FormatoMoneda(totalDebeDecir);

								documentos.LC.Add(lc);
							}
						}
					}
				}
			}

			return documentos;
		}

		/// <summary>
		/// Obtiene la información del formato de confirmación de transacciones virtuales totales o parciales
		/// </summary>
		/// <param name="listaDeFolios">Lista de folios</param>
		/// <returns><see cref="FormatoConfirmacionDeTransaccionesVirtuales"/></returns>
		public FormatoConfirmacionDeTransaccionesVirtuales ObtenerFormatoConfirmacionDeTransaccionesVirtuales(List<string> listaDeFolios)
		{
			var documentos = new FormatoConfirmacionDeTransaccionesVirtuales();
			Database db = DatabaseFactory.CreateDatabase();

			foreach (var folio in listaDeFolios)
			{
				DbCommand cmd = db.GetStoredProcCommand("pObtenerFormatoConfirmacionDeTransaccionesVirtuales");
				db.AddInParameter(cmd, "@Folio", DbType.String, folio.ToString());
				using (var ds = db.ExecuteDataSet(cmd))
				{
					if (ds != null)
					{
						if (ds.Tables.Count == 5)
						{
							DataRow dr = ds.Tables[0].Rows[0];
							int version = Convert.ToInt16(dr["version"]);

							var lc = new FCTLC();
							lc.ALR = dr["ALR"].ToString();
							lc.FechaYHora = MetodosComunes.FormatoFechaHora(Convert.ToDateTime(dr["FechaYHora"]));
							lc.Fecha = MetodosComunes.FormatoFecha(Convert.ToDateTime(dr["Fecha"]), MetodosComunes.TipoFecha.FechaMesTexto);
							lc.FolioLC = MetodosComunes.FormatoFolioLineaCaptura(dr["FolioLC"].ToString());
							lc.Nombre = dr["Nombre"].ToString();
							lc.Observaciones = dr["Observaciones"].ToString();
							lc.RFC = dr["RFC"].ToString();
							lc.version = version;
							lc.ImporteActualizadoTotal = MetodosComunes.FormatoMoneda(Convert.ToDecimal(dr["ImporteActualizadoT"]));
							lc.ImportePagarTotal = MetodosComunes.FormatoMoneda(Convert.ToDecimal(dr["ImporteAPagarT"].ToString()));

							// Referencias
							var listaReferencias = new List<string>();
							decimal importeAplicado = 0;
							foreach (DataRow drReferencia in ds.Tables[1].Rows)
							{
								string referencia = drReferencia["Referencia"].ToString();
								importeAplicado += Convert.ToDecimal(drReferencia["Aplicado"]);
								lc.Referencias.Add(new FCTReferencias()
								{
									Actualizado = MetodosComunes.FormatoMoneda(Convert.ToDecimal(drReferencia["Actualizado"]))
									, Referencia = referencia
									, Aplicado = MetodosComunes.FormatoMoneda(Convert.ToDecimal(drReferencia["Aplicado"]))
									, version = version
								});

								listaReferencias.Add(referencia);
							}

							//Referencias SIAT
							if (ds.Tables[4].Rows.Count > 0)
							{
								lc.Referencias.Clear();
								DataRow drSiat = ds.Tables[4].Rows[0];
								lc.Referencias.Add(new FCTReferencias()
								{
									Actualizado = MetodosComunes.FormatoMoneda(Convert.ToDecimal(drSiat["ImporteSaldoInsoluto"]))
									, Referencia = drSiat["NumeroConvenio"].ToString()
									, Aplicado = MetodosComunes.FormatoMoneda(importeAplicado)
									, version = version
								});
							}

							// Detalle de referencias
							foreach (string referencia in listaReferencias)
							{
								decimal totalCargos = 0;
								decimal totalAbonos = 0;

								var referenciaDetalle = new FCTReferenciaDetalles();
								referenciaDetalle.Referencia = referencia;
								referenciaDetalle.version = version;
								// Cargos

								DataRow[] drCargos = ds.Tables[2].Select("Referencia = '" + referencia + "'");
								foreach (DataRow drCargo in drCargos)
								{
									decimal importe = Convert.ToDecimal(drCargo["Importe"]);

									totalCargos += importe;
									referenciaDetalle.Cargos.Add(new FCTCargos()
									{
										Clave = drCargo["Clave"].ToString(),
										Credito = drCargo["Credito"].ToString(),
										Descripcion = drCargo["Descripcion"].ToString(),
										Importe = MetodosComunes.FormatoMoneda(importe),
										Nivel = Convert.ToInt16(drCargo["Nivel"]),
										version = version
									});
								}

								// ---
								// Abonos
								DataRow[] drAbonos = ds.Tables[3].Select("Referencia = '" + referencia + "'");
								foreach (DataRow drAbono in drAbonos)
								{
									decimal importe = Convert.ToDecimal(drAbono["Importe"]);
									totalAbonos += importe;
									referenciaDetalle.Abonos.Add(new FCTAbonos()
									{
										Clave = drAbono["Clave"].ToString(),
										Descripcion = drAbono["Descripcion"].ToString(),
										Importe = MetodosComunes.FormatoMoneda(importe),
										version = version
									});

								}
								// ---
								referenciaDetalle.TotalAbonos = MetodosComunes.FormatoMoneda(totalAbonos);
								referenciaDetalle.TotalCargos = MetodosComunes.FormatoMoneda(totalCargos);

								lc.ReferenciaDetalles.Add(referenciaDetalle);
							}

							documentos.LC.Add(lc);
						}
					}
				}
			}

			return documentos;
		}
	}
}