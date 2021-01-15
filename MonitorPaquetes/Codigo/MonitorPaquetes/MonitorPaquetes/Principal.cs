using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MonitorPaquetes
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void TimerPaquete_Tick(object sender, EventArgs e)
        {
            Actualizar();
        }

        private void catalogoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Reportes vtnReporte = new Reportes();
            vtnReporte.reporte = 6;
            vtnReporte.ShowDialog();
            vtnReporte.Dispose();
        }

        private void soloUnoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AltaPaquete vtnPaquete = new AltaPaquete();
            vtnPaquete.ShowDialog();
            vtnPaquete.Dispose();
            Actualizar();
        }

        private void altaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AltaDesarrollador vtnDesarrollador = new AltaDesarrollador();
            vtnDesarrollador.ShowDialog();
            vtnDesarrollador.Dispose();
            Actualizar();
        }

        private void altaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AltaEstado vtnEstado = new AltaEstado();
            vtnEstado.ShowDialog();
            vtnEstado.Dispose();
            Actualizar();
        }

        private void masivaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AltaMasivaPaquetes vtnMasiva = new AltaMasivaPaquetes();
            vtnMasiva.ShowDialog();
            vtnMasiva.Dispose();
            Actualizar();
        }

        private void rechazadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reportes vtnReporte = new Reportes();
            vtnReporte.reporte = 2;
            vtnReporte.ShowDialog();
            vtnReporte.Dispose();
        }

        private void cerradosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reportes vtnReporte = new Reportes();
            vtnReporte.reporte = 1;
            vtnReporte.ShowDialog();
            vtnReporte.Dispose();
        }

        private void mensualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reportes vtnReporte = new Reportes();
            vtnReporte.reporte = 3;
            vtnReporte.ShowDialog();
            vtnReporte.Dispose();
        }

        private void mensualToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Reportes vtnReporte = new Reportes();
            vtnReporte.reporte = 5;
            vtnReporte.ShowDialog();
            vtnReporte.Dispose();
        }

        private void semanalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reportes vtnReporte = new Reportes();
            vtnReporte.reporte = 4;
            vtnReporte.ShowDialog();
            vtnReporte.Dispose();
        }

        private void catalogoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reportes vtnReporte = new Reportes();
            vtnReporte.reporte = 7;
            vtnReporte.ShowDialog();
            vtnReporte.Dispose();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

            if (txtDesarrollador.Visible == true)
            {
                txtDesarrollador.Visible = false;
                txtDesarrollador.Enabled = false;
                cbDesarrollador.Visible = true;
                cbDesarrollador.Enabled = true;
                btnEditar.Text = "Guardar";
            }
            else
            {
                Negocio ActDesa = new Negocio();

                if (ActDesa.Actualiza_Paquete(txtPaquete.Text, Convert.ToInt32(cbDesarrollador.SelectedValue)))
                {
                    MessageBox.Show("Desarrollador Actualizado");
                }
                else
                {
                    MessageBox.Show("Ocurrio un error al tratar de cambiar al Desarrollador: " + ActDesa.error, "Actualización de Paquete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                btnEditar.Text = "Editar";
                Limpiar_Campos();

            }
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            try
            {
                Negocio DataDesa = new Negocio();
                cbDesarrollador.DisplayMember = "Nombre";
                cbDesarrollador.ValueMember = "Id_Desa";
                cbDesarrollador.DataSource = DataDesa.Lista_Desarrolladores();
                Actualizar();
            }
            catch //(Exception ex)
            { }
            
        }

        private void dgvPaquetes_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1)
                {
                    Limpiar_Campos();
                    Detalle_Paquete(dgvPaquetes.Rows[e.RowIndex].Cells[1].Value.ToString());
                }
            }
            catch //(Exception ex)
            { }
        }

        private void Detalle_Paquete(string paquete)
        {
            Negocio Detalle = new Negocio();
            string[] datos = null;
            datos = Detalle.DatosPaquete(paquete);

            txtRdl.Text = datos.GetValue(0).ToString();
            txtPaquete.Text = datos.GetValue(1).ToString();
            txtEstado.Text = datos.GetValue(2).ToString();
            txtDesarrollador.Text = datos.GetValue(3).ToString();
            txtIncidentes.Text = datos.GetValue(4).ToString();
            txtPaquetes.Text = datos.GetValue(5).ToString();
            txtecha.Text = datos.GetValue(6).ToString();
            btnEditar.Enabled = true;

            if (Convert.ToInt32(txtRdl.Text) == 0)
            {
                btnBorrar.Enabled = true;
            }
            else
            {
                btnBorrar.Enabled = false;
            }
        }

        private void Actualizar()
        {

            try
            {
                Negocio DataPaquetes = new Negocio();
                dgvPaquetes.DataSource = DataPaquetes.Monitoreo_Paquetes();
                dgvPaquetes.Refresh();

                dgvAvisos.DataSource = DataPaquetes.Avisos();
                dgvAvisos.Refresh();
            }
            catch //(Exception ex)
            {
                dgvPaquetes.DataSource = null;
                dgvPaquetes.Refresh();

                dgvAvisos.DataSource = null;
                dgvAvisos.Refresh();
            }

        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //// ABOUT
        }

        private void tipoDeEnvioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Config v_config = new Config();
            v_config.ShowDialog();
            v_config.Dispose();
        }

        private void cargarExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Incidentes v_incidente = new Incidentes();
            v_incidente.ShowDialog();
            v_incidente.Dispose();
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            Negocio borrar_paq = new Negocio();

            if (borrar_paq.Borrar_Paquete(txtPaquete.Text))
            {
                MessageBox.Show("El Paquete: " + txtPaquete.Text + ", fue borrado satisfactoriamente", "Borrado de Paquete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Limpiar_Campos();
                Actualizar();
            }
            else
            {
                MessageBox.Show("Ocurrio un error al tratar de borrar el Paquete: " + txtPaquete.Text, "Borrado de Paquete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Limpiar_Campos();
                Actualizar();
            }
        }
        private void Limpiar_Campos()
        {
            txtRdl.Text = "";
            txtPaquete.Text = "";
            txtEstado.Text = "";
            txtDesarrollador.Text = "";
            txtIncidentes.Text = "";
            txtPaquetes.Text = "";
            txtecha.Text = "";

            btnBorrar.Enabled = false;
            btnEditar.Enabled = false;

            txtDesarrollador.Visible = true;
            txtDesarrollador.Enabled = false;

            cbDesarrollador.SelectedIndex = 0;
            cbDesarrollador.Visible = false;
            cbDesarrollador.Enabled = false;
            btnEditar.Text = "Editar";
        }

        private void auditoriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reportes vtnReporte = new Reportes();
            vtnReporte.reporte = 8;
            vtnReporte.ShowDialog();
        }

        private void dgvPaquetes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1)
                {
                    Limpiar_Campos();
                    Detalle_Paquete(dgvPaquetes.Rows[e.RowIndex].Cells[1].Value.ToString());
                }
            }
            catch //(Exception ex)
            {

            }
        }

        private void dgvAvisos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    Limpiar_Campos();
                    Detalle_Paquete(dgvAvisos.Rows[e.RowIndex].Cells[0].Value.ToString());
                }
            }
            catch //(Exception ex)
            {

            }
        }
    }
}
