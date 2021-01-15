using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MonitorPaquetes
{
    public partial class Incidentes : Form
    {
        public Incidentes()
        {
            InitializeComponent();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            txtRuta.Text = ofdArchivo.FileName;
            ofdArchivo.Dispose();
        }
        private void btnCargar_Click(object sender, EventArgs e)
        {
            ofdArchivo.ShowDialog();
        }

        private void btnCargar_Click_1(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            ruta = txtRuta.Text;
            Validar_Archivo();
            bkgWorker.RunWorkerAsync();
        }


        private void CargarArchivo_light(string ruta)
        {
            Negocio Carga_Incidnetes = new Negocio();
            int limitemax = 0;
            string[] datos_inc = Carga_Incidnetes.Archivo_Incidentes_Light(ruta, out limitemax);

            try
            {
                for (int i = 0; i < limitemax; i++)
                {
                    string[] temp = datos_inc[i].Split('|');
                    Carga_Incidnetes.Registra_Incidentes_light(temp[0], temp[1], temp[2], temp[3], temp[4]);
                    bkgWorker.ReportProgress(i+1);
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                Carga_Incidnetes.Dispose();
            }
            

        }

        private string ruta = string.Empty;
        private int limitemax = 0;
        private void Validar_Archivo()
        {
            Negocio Carga_Incidnetes = new Negocio();
            string[] datos_inc = Carga_Incidnetes.Archivo_Incidentes_Light(ruta, out limitemax);
            pgrBar.Maximum = limitemax;
            pgrBar.Step = 1;
        }

        private void bkgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            CargarArchivo_light(ruta);
        }

        private void bkgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Cursor = Cursors.Default;
            MessageBox.Show("Termino el Proceso de Carga, Total de Registros Cargados:" + pgrBar.Value, "Tracker", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void bkgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pgrBar.Value = e.ProgressPercentage;

            lblProgress.Text = "Registrando ...." + Convert.ToString(((pgrBar.Value * 100) / pgrBar.Maximum)) + "%";
            lblProgress.Refresh();
        }

        //------------------------------------------------------------------------------------------//
        //------------------------------------------------------------------------------------------//
        //------------------------------------------------------------------------------------------//
        //------------------------------------------------------------------------------------------//

        public void Errores(string error)
        {
            Negocio Log_errores = new Negocio();
            string path = Log_errores.config_errores();
            path = path.Replace("@Log", "Log_Error_Monitor_Paquetes");
            File.WriteAllText(path, error, Encoding.UTF8);
        }

    }
}
