using SAT.CreditosFiscales.Motor.Negocio.ServicioImpresion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Windows.Forms;
using TestMotor.ServicioMotor;
namespace TestMotor
{
    public partial class GeneraPDF : Form
    {
        private string rutaPdf = string.Empty;
        public GeneraPDF()
        {
            InitializeComponent();
        }

        private void GeneraPDF_Load(object sender, EventArgs e)
        {
            this.lbl_pdf.Visible = false;
            this.lnk_pdf_formato.Visible = false;
            this.lnk_PDFLC.Visible = false;
            this.tbx_logPdf.Visible = false;
            this.cbx_formatos.DataSource = (object)Enum.GetValues(typeof(EnumTemplate));
        }

        private void btn_generPDFServImp_Click(object sender, EventArgs e)
        {
            EnumTemplate template = (EnumTemplate)this.cbx_formatos.SelectedItem;
            this.tbx_logPdf.Text = string.Empty;
            using (ServicioGeneraFormatoImpresionClient formatoImpresionClient = new ServicioGeneraFormatoImpresionClient())
            {
                try
                {
                    this.lnk_pdf_formato.Visible = false;
                    List<string> list = new List<string>();
                    list.Add(this.tbx_folio.Text);
                    this.rutaPdf = string.Format(ConfigurationManager.AppSettings["ArchivoPDF"], (object)this.tbx_folio.Text);
                    File.WriteAllBytes(this.rutaPdf, formatoImpresionClient.GeneraArchivo(template, list.ToArray()));
                    this.lnk_pdf_formato.Text = "PDF generado clic para abrir...";
                    this.lnk_pdf_formato.Visible = true;
                }
                catch (Exception ex)
                {
                    this.tbx_logPdf.Text = ex.Message;
                    this.tbx_logPdf.Text += ex.StackTrace;
                }
            }
        }

        private void btn_nuevo_Click(object sender, EventArgs e)
        {
            this.tbx_msjEntrada.Text = string.Empty;
            this.tbx_log.Visible = false;
            this.lbl_pdf.Visible = false;
        }

        private void btn_enviar_Click(object sender, EventArgs e)
        {
            this.btn_enviar.Enabled = false;
            this.lbl_pdf.Visible = false;
            this.tbx_log.Visible = false;
            this.tbx_log.Text = string.Empty;
            try
            {
                using (CreditosFiscalesClient creditosFiscalesClient = new CreditosFiscalesClient())
                {
                    using (new OperationContextScope((IContextChannel)creditosFiscalesClient.InnerChannel))
                    {
                        HttpRequestMessageProperty requestMessageProperty = new HttpRequestMessageProperty();
                        requestMessageProperty.Headers.Add("Authorization", "CRFIApp0001|CRFIApp0001");
                        OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = (object)requestMessageProperty;
                        string text1 = this.tbx_msjEntrada.Text;
                        Stopwatch stopwatch = new Stopwatch();
                        stopwatch.Start();
                        string text2 = creditosFiscalesClient.ObtieneLineaCapturaConPDF(short.Parse(this.tbx_idAplicacion.Text), short.Parse(this.tbx_IdDocumento.Text), text1);
                        stopwatch.Stop();
                        RespuestaLC respuestaLc = Program.deserializaResp(text2);
                        Console.WriteLine(string.Format("Resultado: {0}", respuestaLc.TipoRespuesta == 0 ? (object)"No Exitoso" : (object)"Exitoso"));
                        if (respuestaLc.LineasCaptura != null)
                        {
                            if (respuestaLc.PDF != null && respuestaLc.PDF.Length > 0)
                            {
                                this.rutaPdf = string.Format(ConfigurationManager.AppSettings["ArchivoPDF"], (object)respuestaLc.LineasCaptura[0].Folio);
                                File.WriteAllBytes(this.rutaPdf, respuestaLc.PDF);
                                this.lbl_pdf.Text = "PDF generado clic para abrir...";
                                this.lbl_pdf.Visible = true;
                            }
                            foreach (RespuestaLCDatosLinea respuestaLcDatosLinea in respuestaLc.LineasCaptura)
                            {
                                Console.WriteLine(string.Format("Folio: {0}", (object)respuestaLcDatosLinea.Folio));
                                Console.WriteLine(string.Format("LineaCaptura: {0}", (object)respuestaLcDatosLinea.LineaCaptura));
                                Console.WriteLine("*****************");
                            }
                        }
                        if (respuestaLc.ListaErrores != null)
                        {
                            foreach (string str1 in respuestaLc.ListaErrores)
                            {
                                TextBox textBox = this.tbx_log;
                                string str2 = textBox.Text + str1 + " IdProcesamiento:" + respuestaLc.IdProcesamiento;
                                textBox.Text = str2;
                                this.tbx_log.Visible = true;
                                Console.WriteLine(string.Format("Error: {0}", (object)str1));
                                Console.WriteLine("*****************");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.btn_enviar.Enabled = true;
                this.tbx_log.Visible = true;
                this.tbx_log.Text = ex.Message + ex.StackTrace;
            }
            this.btn_enviar.Enabled = true;
        }

        private void lbl_pdf_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            Process.Start(this.rutaPdf);
        }

        private void lnk_pdf_formato_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            Process.Start(this.rutaPdf);
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamReader sr = new
                   System.IO.StreamReader(openFileDialog.FileName);
                tbx_msjEntrada.Text=sr.ReadToEnd();
                sr.Close();
            }
        }

        private void btn_enviarLinea_Click(object sender, EventArgs e)
        {

            this.tbx_logLC.Text = String.Empty;
            this.lnk_PDFLC.Visible = false;
            this.btn_enviarLinea.Enabled = false;
            try
            {
               using (CreditosFiscalesClient creditosFiscalesClient = new CreditosFiscalesClient())
                {
                    using (new OperationContextScope((IContextChannel)creditosFiscalesClient.InnerChannel))
                    {
                        HttpRequestMessageProperty requestMessageProperty = new HttpRequestMessageProperty();
                        requestMessageProperty.Headers.Add("Authorization", "CRFIApp0001|CRFIApp0001");
                        OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = (object)requestMessageProperty;
                        Stopwatch stopwatch = new Stopwatch();
                        stopwatch.Start();
                        string text2 = creditosFiscalesClient.ObtienePDF(tbx_lineaCaptura.Text);
                        stopwatch.Stop();
                        RespuestaLC respuestaLc = Program.deserializaResp(text2);
                        Console.WriteLine(string.Format("Resultado: {0}", respuestaLc.TipoRespuesta == 0 ? (object)"No Exitoso" : (object)"Exitoso"));
                        if (respuestaLc.LineasCaptura != null)
                        {
                            if (respuestaLc.PDF != null && respuestaLc.PDF.Length > 0)
                            {
                                this.rutaPdf = string.Format(ConfigurationManager.AppSettings["ArchivoPDF"], (object)respuestaLc.LineasCaptura[0].Folio);
                                File.WriteAllBytes(this.rutaPdf, respuestaLc.PDF);
                                this.lnk_PDFLC.Text = "PDF generado clic para abrir...";
                                this.lnk_PDFLC.Visible = true;
                            }
                            foreach (RespuestaLCDatosLinea respuestaLcDatosLinea in respuestaLc.LineasCaptura)
                            {
                                Console.WriteLine(string.Format("Folio: {0}", (object)respuestaLcDatosLinea.Folio));
                                Console.WriteLine(string.Format("LineaCaptura: {0}", (object)respuestaLcDatosLinea.LineaCaptura));
                                Console.WriteLine("*****************");
                            }
                        }
                        if (respuestaLc.ListaErrores != null)
                        {
                            foreach (string str1 in respuestaLc.ListaErrores)
                            {
                                TextBox textBox = this.tbx_logLC;
                                string str2 = textBox.Text + str1 + " IdProcesamiento:" + respuestaLc.IdProcesamiento;
                                textBox.Text = str2;
                                this.tbx_logLC.Visible = true;
                                Console.WriteLine(string.Format("Error: {0}", (object)str1));
                                Console.WriteLine("*****************");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.btn_enviarLinea.Enabled = true;
                this.tbx_logLC.Visible = true;
                this.tbx_logLC.Text = ex.Message + ex.StackTrace;
            }
            this.btn_enviarLinea.Enabled = true;
        }
    }
}
