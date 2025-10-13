/*
 * Actualizado por: Mario Escarpulli
 * Fecha de actualización: 12/Junio/2019
*/
// Referencias de sistema.
using System;
using System.IO;
using System.Diagnostics;
using System.ServiceModel;
using System.Configuration;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ServiceModel.Channels;

// Referencias personalizas.
using TestMotor.ServicioMotor;
using SAT.CreditosFiscales.Motor.Negocio.ServicioImpresion;

namespace TestMotor
{
	/// <summary>
	/// Clase pública para realizar pruebas en la generación de Líneas de Capturas.
	/// </summary>
	public partial class GeneraPDF : Form
	{
		#region Propiedades
			/// <summary>
			/// Propiedad que obtiene o establece la ruta completa donde guardarán los archivos binarios.
			/// </summary>
			public string RutaArchivoBinario { get; set; }

			/// <summary>
			/// Propiedad que obtiene o establece el valor lógico para saber si se generó o no un archivo en formato PDF.
			/// </summary>
			private bool GeneroPdf { get; set; }
		#endregion

		#region Constructor
			/// <summary>
			/// Generador de Líneas de captura.
			/// </summary>
			public GeneraPDF()
			{
				InitializeComponent();
			}
		#endregion

		#region Eventos del formulario
			/// <summary>
			/// Inicializando el formulario.
			/// </summary>
			/// <param name="sender">Origen del evento.</param>
			/// <param name="e">Argumentos del evento.</param>
			private void GeneradorLC_Load(object sender, EventArgs e)
			{
				this.RutaArchivoBinario = string.Empty;
				this.LimpiarDatosFormularioPdf();
				this.LimpiarDatosFormularioPng();
				this.LimpiarDatosFormularioMotorTraductor();
				this.LimpiarDatosFormularioPdfLineaCaptura();
				this.TxtFolioPdf.Focus();
			}

			#region PDFs
				/// <summary>
				/// Genera el archivo .PDF de la Línea de Captura.
				/// </summary>
				/// <param name="sender">Origen del evento.</param>
				/// <param name="e">Argumentos del evento.</param>
				private void BtnGenerarPdfServImp_Click(object sender, EventArgs e)
				{
					if (this.TxtFolioPdf.Text.Trim().Length == 0)
					{ 
						MessageBox.Show("Escriba el número de Folio para generar un archivo PDF.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						this.TxtFolioPdf.Focus();
						return;
					}

					var vPlantilla = (EnumTemplate) this.CbFormatosPdf.SelectedItem;
					this.BtnGenerarPdfServImp.Enabled = false;

					this.GbGenerarPdfVisualizar.Visible = false;
					this.LnkPdfLC.Visible = false;
					this.TxtLogPdf.Text = string.Empty;
					this.TxtLogPdf.Visible = false;

					using (ServicioGeneraFormatoImpresionClient formatoImpresionClient = new ServicioGeneraFormatoImpresionClient())
					{
						try
						{
							// Generando y guardando el archivo en formato PDF.
							var vListado = new List<string>();
							vListado.Add(this.TxtFolioPdf.Text);
							this.RutaArchivoBinario = string.Format(ConfigurationManager.AppSettings["ArchivoPDF"], (object) this.TxtFolioPdf.Text);
							File.WriteAllBytes(this.RutaArchivoBinario, formatoImpresionClient.GeneraArchivo(vPlantilla, vListado.ToArray()));

							// Habilitando controles
							this.GbGenerarPdfVisualizar.Visible = true;
							this.LnkPdfLC.Text = "El archivo .PDF se generó correctamente, haga clic aquí para abrirlo...";
							this.LnkPdfLC.Visible = true;
							this.TxtLogPdf.Visible = true;
						}
						catch (Exception eError)
						{
							this.GbGenerarPdfVisualizar.Visible = true;
							this.LnkPdfLC.Visible = false;
							this.TxtLogPdf.Visible = true;
							this.TxtLogPdf.Text = eError.Message;
							this.TxtLogPdf.Text += eError.StackTrace;
						}
					}

					this.BtnGenerarPdfServImp.Enabled = true;
				}

				/// <summary>
				/// Abre el archivo .PDF de la Línea de Captura.
				/// </summary>
				/// <param name="sender">Origen del evento.</param>
				/// <param name="e">Argumentos del evento.</param>
				private void LnkPdfFormato_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
				{
					var openFileDialog = new OpenFileDialog();
					Process.Start(this.RutaArchivoBinario);
				}

				/// <summary>
				/// Limpia el tabulador que genera el archivo .PNG de la Línea de Captura.
				/// </summary>
				/// <param name="sender">Origen del evento.</param>
				/// <param name="e">Argumentos del evento.</param>
				private void BtnLimpiarPdfServImp_Click(object sender, EventArgs e)
				{
					this.LimpiarDatosFormularioPdf();
					this.TxtFolioPdf.Focus();
				}
			#endregion

			#region PNGs
				/// <summary>
				/// Genera el archivo .PNG de la Línea de Captura.
				/// </summary>
				/// <param name="sender">Origen del evento.</param>
				/// <param name="e">Argumentos del evento.</param>
				private void BtnGeneraPngServImp_Click(object sender, EventArgs e)
				{
					if (this.TxtFolioPng.Text.Trim().Length == 0)
					{
						MessageBox.Show("Escriba el número de Folio para generar un archivo PNG.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						this.TxtFolioPng.Focus();
						return;
					}

					this.BtnGeneraPngServImp.Enabled = false;
					this.GbGenerarPngVisualizar.Visible = true;
					this.TxtLogPng.Text = string.Empty;
					this.TxtLogPng.Visible = false;

					try
					{
					}
					catch(Exception iError)
					{
						this.KivPngLC.Visible = false;
						this.TxtLogPng.Visible = true;
						this.TxtLogPng.Text = iError.Message + iError.StackTrace;
					}

					this.BtnGeneraPngServImp.Enabled = true;
				}

				/// <summary>
				/// Limpia el tabulador que genera el archivo .PNG de la Línea de Captura.
				/// </summary>
				/// <param name="sender">Origen del evento.</param>
				/// <param name="e">Argumentos del evento.</param>
				private void BtnLimpiarPngServImp_Click(object sender, EventArgs e)
				{
					this.LimpiarDatosFormularioPng();
					this.TxtFolioPng.Focus();
				}
			#endregion

			#region Motor Traductor
				/// <summary>
				/// Evento que habilita o deshabilita un control si su contenido cambia.
				/// </summary>
				/// <param name="sender">Origen del evento.</param>
				/// <param name="e">Argumentos del evento.</param>
				private void TxtMensajeEntradaMT_TextChanged(object sender, EventArgs e)
				{
					this.BtnEnviarMensajeMT.Enabled = !string.IsNullOrWhiteSpace(this.TxtMensajeEntradaMT.Text.Trim());
				}

				/// <summary>
				/// Abrir mensaje para el Motor Traductor.
				/// </summary>
				/// <param name="sender">Origen del evento.</param>
				/// <param name="e">Argumentos del evento.</param>
				private void BtnAbrirMensajeMT_Click(object sender, EventArgs e)
				{
					var vofdAbrirArchico = new OpenFileDialog();
					vofdAbrirArchico.Filter = "Archivos con formato XML (XML)|*.xml;*.xsd;";
					if (vofdAbrirArchico.ShowDialog() == DialogResult.OK)
					{
						this.TxtMensajeEntradaMT.Text = string.Empty;
						var vsrContenido = new StreamReader(vofdAbrirArchico.FileName);
						this.TxtMensajeEntradaMT.Text = vsrContenido.ReadToEnd();
						vsrContenido.Close();
						this.TxtMensajeEntradaMT.Visible = true;
						this.BtnEnviarMensajeMT.Visible = true;
						this.LnkArchivoMT.Visible = false;
						this.TxtLogMT.Text = string.Empty;
						this.TxtLogMT.Visible = false;
						this.TxtMensajeEntradaMT.Focus();
					}
				}

				/// <summary>
				/// Limpiar datos para el Motor Traductor.
				/// </summary>
				/// <param name="sender">Origen del evento.</param>
				/// <param name="e">Argumentos del evento.</param>
				private void BtnLimpiarMT_Click(object sender, EventArgs e)
				{
					this.LimpiarDatosFormularioMotorTraductor();
					this.TxtIdAplicacionMT.Focus();
				}

				/// <summary>
				/// Enviar mensaje al Motor Traductor.
				/// </summary>
				/// <param name="sender">Origen del evento.</param>
				/// <param name="e">Argumentos del evento.</param>
				private void BtnEnviarMensajeMT_Click(object sender, EventArgs e)
				{
					if (this.TxtIdAplicacionMT.Text.Trim().Length == 0)
					{ 
						MessageBox.Show("Ingrese el Identificador único (ID) de la Aplicación para generar un archivo PDF.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						this.TxtIdAplicacionMT.Focus();
						return;
					}

					if (this.TxtIdDocumentoMT.Text.Trim().Length == 0)
					{ 
						MessageBox.Show("Ingrese el Identificador único (ID) del Documento para generar un archivo PDF.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						this.TxtIdDocumentoMT.Focus();
						return;
					}

					if (this.TxtMensajeEntradaMT.Text.Trim().Length == 0)
					{ 
						MessageBox.Show("Ingrese el contenido en formato XML para generar un archivo PDF.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						this.TxtMensajeEntradaMT.Focus();
						return;
					}

					if (!this.RbArchivoPdf.Checked && !this.RbArchivoPng.Checked)
					{ 
						MessageBox.Show("Seleccione el tipo de salida que debe tener el resultado (.PDF o .PNG).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}

					this.BtnEnviarMensajeMT.Enabled = false;
					this.LnkArchivoMT.Visible = false;
					this.TxtLogMT.Visible = false;
					this.TxtLogMT.Text = string.Empty;
					this.RutaArchivoBinario = string.Empty;
					try
					{
						using (CreditosFiscalesClient sweCreditosFiscalesClient = new CreditosFiscalesClient())
						{
							using (new OperationContextScope((IContextChannel) sweCreditosFiscalesClient.InnerChannel))
							{
								var requestMessageProperty = new HttpRequestMessageProperty();
								requestMessageProperty.Headers.Add("Authorization", "CRFIApp0001|CRFIApp0001");
								OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = (object) requestMessageProperty;

								var vswMedirTiempo = new Stopwatch();
								vswMedirTiempo.Start();
									string sMedirTiempo = string.Empty;
									if(this.RbArchivoPdf.Checked)
									{
										sMedirTiempo = sweCreditosFiscalesClient.ObtieneLineaCapturaConPDF(short.Parse(this.TxtIdAplicacionMT.Text), short.Parse(this.TxtIdDocumentoMT.Text), this.TxtMensajeEntradaMT.Text);
										this.GeneroPdf = true;
									}
									else
									{
										sMedirTiempo = sweCreditosFiscalesClient.ObtieneLineaCapturaConImagen(short.Parse(this.TxtIdAplicacionMT.Text), short.Parse(this.TxtIdDocumentoMT.Text), this.TxtMensajeEntradaMT.Text);
										this.GeneroPdf = false;
									}
								vswMedirTiempo.Stop();

								RespuestaLC oRespuestaLc = Program.DeserializaResp(sMedirTiempo);
								this.TxtLogMT.Visible = true;
								this.TxtLogMT.Text = string.Empty;
								this.TxtLogMT.Text = string.Format("Resultado: {0}", oRespuestaLc.TipoRespuesta == 0 ? (object) "No Exitoso" : (object) "Exitoso");

								if (oRespuestaLc.LineasCaptura != null)
								{
									if(this.RbArchivoPdf.Checked)
									{
										if (oRespuestaLc.PDF != null && oRespuestaLc.PDF.Length > 0)
										{
											this.RutaArchivoBinario = string.Format(ConfigurationManager.AppSettings["ArchivoPDF"], (object) oRespuestaLc.LineasCaptura[0].Folio);
											File.WriteAllBytes(this.RutaArchivoBinario, oRespuestaLc.PDF);
											this.LnkArchivoMT.Text = "Archivo PDF generado correctamente, haga clic aquí para abrirlo...";
											this.LnkArchivoMT.Visible = true;
										}
									}
									else
									{
										if (oRespuestaLc.PNG != null && oRespuestaLc.PNG.Length > 0)
										{
											this.RutaArchivoBinario = string.Format(ConfigurationManager.AppSettings["ArchivoPNG"], (object) oRespuestaLc.LineasCaptura[0].Folio);
											File.WriteAllBytes(this.RutaArchivoBinario, oRespuestaLc.PNG);
											this.LnkArchivoMT.Text = "Archivo PNG generado correctamente, haga clic aquí para abrirlo...";
											this.LnkArchivoMT.Visible = true;
										}
									}

									foreach (RespuestaLCDatosLinea respuestaLcDatosLinea in oRespuestaLc.LineasCaptura)
									{
										this.TxtLogMT.Text += string.Format("Folio: {0}", (object) respuestaLcDatosLinea.Folio) + "\n";
										this.TxtLogMT.Text += string.Format("LineaCaptura: {0}", (object) respuestaLcDatosLinea.LineaCaptura);
										this.TxtLogMT.Text += string.Format("*****************");
									}
								}

								if (oRespuestaLc.ListaErrores != null)
								{
									foreach (string sErrores in oRespuestaLc.ListaErrores)
									{
										TextBox tbLog = this.TxtLogMT;
										string sResultadoLog = tbLog.Text + sErrores + " IdProcesamiento:" + oRespuestaLc.IdProcesamiento;
										tbLog.Text = sResultadoLog;
										this.TxtLogMT.Visible = true;
										this.TxtLogMT.Text += string.Format("Error: {0}", (object) sErrores);
									}
								}
							}
						}
					}
					catch (Exception eError)
					{
						this.BtnEnviarMensajeMT.Enabled = true;
						this.TxtLogMT.Visible = true;
						this.TxtLogMT.Text = eError.Message + eError.StackTrace;
					}

					this.BtnEnviarMensajeMT.Enabled = true;
				}

				/// <summary>
				/// Permite visualizar un archivo de imagen generado por el Motor Traductor.
				/// </summary>
				/// <param name="sender">Origen del evento.</param>
				/// <param name="e">Argumentos del evento.</param>
				private void LnkArchivoMT_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
				{
					if (this.GeneroPdf)
					{
						var cofdArchivos = new OpenFileDialog();
						Process.Start(this.RutaArchivoBinario);
					}
					else
					{
						var vFormulario = new VisualizarImagenes();
						vFormulario.ShowDialog(this);
					}
				}
			#endregion

			#region Línea de Captura
				/// <summary>
				/// Genera el archivo .PDF a través la Línea de Captura.
				/// </summary>
				/// <param name="sender">Origen del evento.</param>
				/// <param name="e">Argumentos del evento.</param>
				private void BtnEnviarLineaCaptura_Click(object sender, EventArgs e)
				{
					if (this.TxtLineaCaptura.Text.Trim().Length == 0)
					{ 
						MessageBox.Show("Escriba la Línea de Captura para generar un archivo PDF.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						this.TxtLineaCaptura.Focus();
						return;
					}

					this.BtnEnviarLineaCaptura.Enabled = false;
					this.GbPdfLCVisualizar.Visible = false;
					this.LnkPdfLC.Visible = false;
					this.TxtLogLC.Text = string.Empty;

					try
					{
						using (CreditosFiscalesClient sweCreditosFiscalesClient = new CreditosFiscalesClient())
						{
							using (new OperationContextScope((IContextChannel) sweCreditosFiscalesClient.InnerChannel))
							{
								var vRequestMessageProperty = new HttpRequestMessageProperty();
								vRequestMessageProperty.Headers.Add("Authorization", "CRFIApp0001|CRFIApp0001");
								OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = (object) vRequestMessageProperty;

								var swMedicionDeTiempo = new Stopwatch();
								swMedicionDeTiempo.Start();
									string sLineaDeCaptura = sweCreditosFiscalesClient.ObtienePDF(this.TxtLineaCaptura.Text);
								swMedicionDeTiempo.Stop();

								RespuestaLC oRespuestaLc = Program.DeserializaResp(sLineaDeCaptura);
								Console.WriteLine(string.Format("Resultado: {0}", oRespuestaLc.TipoRespuesta == 0 ? (object) "No Exitoso" : (object) "Exitoso"));
								if (oRespuestaLc.LineasCaptura != null)
								{
									if (oRespuestaLc.PDF != null && oRespuestaLc.PDF.Length > 0)
									{
										this.RutaArchivoBinario = string.Format(ConfigurationManager.AppSettings["ArchivoPDF"], (object) oRespuestaLc.LineasCaptura[0].Folio);
										File.WriteAllBytes(this.RutaArchivoBinario, oRespuestaLc.PDF);
										this.LnkPdfLC.Text = "El archivo .PDF se generó correctamente, haga clic aquí para abrirlo...";
										this.LnkPdfLC.Visible = true;
									}

									foreach (RespuestaLCDatosLinea respuestaLcDatosLinea in oRespuestaLc.LineasCaptura)
									{
										this.TxtLogLC.Text += string.Format("Folio: {0}", (object) respuestaLcDatosLinea.Folio) + "\n";
										this.TxtLogLC.Text += string.Format("LineaCaptura: {0}", (object) respuestaLcDatosLinea.LineaCaptura);
										this.TxtLogLC.Text += string.Format("*****************");
									}
								}

								if (oRespuestaLc.ListaErrores != null)
								{
									foreach (string sError in oRespuestaLc.ListaErrores)
									{
										TextBox tbLog = this.TxtLogLC;
										string sResultadoError = tbLog.Text + sError + " IdProcesamiento:" + oRespuestaLc.IdProcesamiento;
										tbLog.Text = sResultadoError;
										this.TxtLogLC.Visible = true;
										this.TxtLogLC.Text += string.Format("Error: {0}", (object) sError) + "\n";
										this.TxtLogLC.Text += string.Format("*****************");
									}
								}
							}
						}
					}
					catch (Exception eError)
					{
						this.BtnEnviarLineaCaptura.Enabled = true;
						this.GbPdfLCVisualizar.Visible = true;
						this.LnkPdfLC.Visible = false;
						this.TxtLogLC.Text = eError.Message + eError.StackTrace;
					}

					this.BtnEnviarLineaCaptura.Enabled = true;
				}

				/// <summary>
				/// Abre el archivo .PDF de la Línea de Captura.
				/// </summary>
				/// <param name="sender">Origen del evento.</param>
				/// <param name="e">Argumentos del evento.</param>
				private void LnkPdfLC_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
				{
					var cofdArchivos = new OpenFileDialog();
					Process.Start(this.RutaArchivoBinario);
				}

				/// <summary>
				/// Limpia el tabulador que genera el archivo PDF a través de la Línea de Captura.
				/// </summary>
				/// <param name="sender">Origen del evento.</param>
				/// <param name="e">Argumentos del evento.</param>
				private void BtnLimpiarLineaCaptura_Click(object sender, EventArgs e)
				{
					this.LimpiarDatosFormularioPdfLineaCaptura();
				}
			#endregion

			/// <summary>
			/// Permite cerrar el formulario.
			/// </summary>
			/// <param name="sender">Origen del evento.</param>
			/// <param name="e">Argumentos del evento.</param>
			private void BtnSalir_Click(object sender, EventArgs e)
			{
				Application.Exit();
			}
		#endregion

		#region Métodos de apoyo
			/// <summary>
			/// Limpia los datos del formulario en la pestaña Generador PDF.
			/// </summary>
			private void LimpiarDatosFormularioPdf()
			{
				this.CbFormatosPdf.DataSource = (object) Enum.GetValues(typeof(EnumTemplate));
				this.TxtFolioPdf.Text = string.Empty;
				this.BtnGenerarPdfServImp.Enabled = true;
				this.GbGenerarPdfVisualizar.Visible = false;
				this.LnkPdfFormato.Visible = false;
				this.TxtLogPdf.Text = string.Empty;
				this.TxtLogPdf.Visible = false;
				this.RutaArchivoBinario = string.Empty;
			}

			/// <summary>
			/// Limpia los datos del formulario en la pestaña Generador PNG.
			/// </summary>
			private void LimpiarDatosFormularioPng()
			{
				this.CbFormatosPng.DataSource = (object) Enum.GetValues(typeof(EnumTemplate));
				this.TxtFolioPng.Text = string.Empty;
				this.BtnGeneraPngServImp.Enabled = true;
				this.GbGenerarPngVisualizar.Visible = false;
				this.TxtLogPng.Text = string.Empty;
				this.TxtLogPng.Visible = false;

				this.RutaArchivoBinario = string.Empty;
				this.KivPngLC.Visible = false;
				this.KivPngLC.Zoom = 85;
				if (this.KivPngLC.Image != null)
				{
					this.KivPngLC.Image = null;
					this.KivPngLC.Image.Dispose();
				}
			}

			/// <summary>
			/// Limpia los datos del formulario en la pestaña  Motor Traductor.
			/// </summary>
			private void LimpiarDatosFormularioMotorTraductor()
			{
				this.TxtIdAplicacionMT.Text = string.Empty;
				this.TxtIdDocumentoMT.Text = string.Empty;
				this.RbArchivoPdf.Checked = false;
				this.RbArchivoPng.Checked = false;
				this.LblMensajeEntradaMT.Visible = false;
				this.TxtMensajeEntradaMT.Text = string.Empty;
				this.TxtMensajeEntradaMT.Visible = true;
				this.BtnEnviarMensajeMT.Enabled = false;
				this.LnkArchivoMT.Visible = false;
				this.TxtLogMT.Visible = false;
				this.TxtLogMT.Text = string.Empty;
				this.RutaArchivoBinario = string.Empty;
			}

			/// <summary>
			/// Limpia los datos del formulario en la pestaña  Motor Traductor.
			/// </summary>
			private void LimpiarDatosFormularioPdfLineaCaptura()
			{
				this.TxtLineaCaptura.Text = string.Empty;
				this.BtnEnviarLineaCaptura.Enabled = true;
				this.GbPdfLCVisualizar.Visible = false;
				this.LnkPdfLC.Visible = false;
				this.TxtLogLC.Text = string.Empty;
				this.TxtLogLC.Visible = false;
			}
		#endregion
	}
}