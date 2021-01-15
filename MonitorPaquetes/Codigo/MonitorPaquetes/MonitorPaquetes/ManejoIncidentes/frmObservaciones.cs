using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MonitorEntidades;
using MonitorReglaNegocios;

namespace MonitorPaquetes.ManejoIncidentes
{
    public partial class frmObservaciones : Form
    {
        public frmObservaciones(string sIncidente, string sPaquete, List<ent_Observaciones> lstObservaciones)
        {
            sInc = sIncidente;
            sPaq = sPaquete;
            lstObser = lstObservaciones;

            InitializeComponent();
        }

        private string sInc { get; set; }
        private string sPaq { get; set; }
        public List<ent_Observaciones> lstObser { get; set; }

        private void frmObservaciones_Load(object sender, EventArgs e)
        {
            if (sPaq == null) this.Text = string.Format("Observaciones para {0}", sInc);
            else this.Text = string.Format("Observaciones para {0} / {1}", sInc, sPaq);

            dtgObservaciones.DataSource = lstObser;

            btnAccion.Text = "Agregar";
            lblID.Text = "-1";
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            btnAccion.Text = "Agregar";
            txtObservaciones.Text = "";
            lblID.Text = "-1";
        }

        private void dtgObservaciones_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    btnAccion.Text = "Actualizar";
                    lblID.Text = dtgObservaciones.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();
                    txtObservaciones.Text = dtgObservaciones.Rows[e.RowIndex].Cells[4].Value.ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnAccion_Click(object sender, EventArgs e)
        {
            int nObservacion = -1;
            string sDato = "";
            try
            {
                nObservacion = int.Parse(lblID.Text.Trim());
                sDato = txtObservaciones.Text.Trim();
                
                if (btnAccion.Text.Trim() == "Agregar")
                    lstObser = ClienteWindows.Incs_Act_IncObservaciones(nObservacion, sInc, sPaq, sDato, 1);
                else
                    lstObser = ClienteWindows.Incs_Act_IncObservaciones(nObservacion, sInc, sPaq, sDato, 2);

                lblID.Text = "-1";
                txtObservaciones.Text = "";
                btnAccion.Text = "Agregar";

                dtgObservaciones.DataSource = lstObser;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
