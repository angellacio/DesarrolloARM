using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sat.CreditosFiscales.Archivos;
using System.Configuration;
using System.Diagnostics;

namespace ArchivoBanco
{
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
        }
        private Boolean huboError = false;
        private List<DyP> lineasBanco;
        private void background_DoWork(object sender, DoWorkEventArgs e)
        {
            Negocio.escribeLog(ConfigurationManager.AppSettings["RutaLog"].ToString(), new Exception("Inicia proceso"));
            try
            {
                huboError = false;
                background.ReportProgress(-1);
                String rutaArchivosProcesar=ConfigurationManager.AppSettings["RutaArchivosProcesar"].ToString();
                Sat.CreditosFiscales.Archivos.Control avance = new Sat.CreditosFiscales.Archivos.Control(lineasBanco.Count());
                background.ReportProgress(-2, avance);
                var lineasBancoOrdenado = lineasBanco.GroupBy(x => new { x.ClaveBanco, x.FechaDePago });
                String FechaDePagoAnt = String.Empty;
                String nombreArchivoBancoE = String.Empty;
                Negocio reglas = new Negocio(rutaArchivosProcesar);
                foreach (var grupo in lineasBancoOrdenado)
                {
                    if (huboError)
                    {
                        break;
                    }
                    reglas.banco= grupo.First();
                    reglas.creaNombreArchivoBanco(TipoArchivoBanco.E);
                    reglas.creaPrimeraLineaE();
                    reglas.reIniciaConsecutivo();
                    foreach (var banco in grupo)
                    {
                        reglas.banco = banco;
                        reglas.creaLineaE();
                        avance.actual++;
                        background.ReportProgress(0, avance);
                    }
                    reglas.creaUltimaLineaE();
                    reglas.creaNombreArchivoBanco(TipoArchivoBanco.Q);
                    reglas.creaLineasQ();
                }
                Negocio.escribeLog(ConfigurationManager.AppSettings["RutaLog"].ToString(), new Exception("Termina proceso"));
            }    
            catch (Exception ex)
            {
    
                Console.WriteLine(ex.Message, ex.InnerException, ex.StackTrace, ex.Source);
                Negocio.escribeLog(ConfigurationManager.AppSettings["RutaLog"].ToString(),ex);             
                huboError = true;
            }
        }

        private void WriteToEventLog(string message)
        {
            string cs = "Application";
            string logName = "Application";
            EventLog elog = new EventLog();

            if (!EventLog.SourceExists(cs))
            {
                EventLog.CreateEventSource(cs, logName);
            }

            elog.Log = logName;
            elog.Source = cs;
            elog.EnableRaisingEvents = true;
            elog.WriteEntry(message);
        }

        private void background_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Sat.CreditosFiscales.Archivos.Control avance = (Sat.CreditosFiscales.Archivos.Control)e.UserState;
            switch (e.ProgressPercentage)
            {
                case -1:
                    this.Text = "Cargando archivo excel";
                    break;
                case -2:
                    this.Text = "Archivo excel cargado: " + avance.totalElementos + " lineas.";
                    break;
                default:
                    barra.Value = avance.Porcentaje;
                    lbl_estatus.Text = "Generando archivo: " + avance.actual + " de  " + avance.totalElementos;
                    break;
            }


        }

        private void background_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btn_cargar.Enabled = true;
            
            lbl_estatus.Visible = !huboError;
            lnk_error.Visible = huboError;


            lbl_estatus.Text = "Completado, correr proceso de pagos.";
        }



        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            lbl_estatus.Visible = false;
            lnk_error.Visible = false;
        }

        private void lnk_error_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Negocio.abrirLog(ConfigurationManager.AppSettings["RutaLog"].ToString());
        }

        private void btn_cargar_Click(object sender, EventArgs e)
        {
            openfile.Filter = "Archivos Excel|*.xlsx";
            openfile.Title = "Selecciona Archivo de flujos";
            openfile.InitialDirectory = ConfigurationManager.AppSettings["RutaInsumo"].ToString();
            String rutaArchivo;
            if (openfile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                rutaArchivo = openfile.FileName;
                lbl_estatus.Visible = true;
                lineasBanco = Negocio.cargaExcel(rutaArchivo);
                btn_cargar.Enabled = false;
                background.RunWorkerAsync();
            }
          
        }
    }
}
