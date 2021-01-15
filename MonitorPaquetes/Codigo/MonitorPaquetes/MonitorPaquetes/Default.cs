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
using MonitorRN = MonitorReglaNegocios;

namespace MonitorPaquetes
{
    public partial class Default : Form
    {
        public Default()
        {
            InitializeComponent();
        }
        public List<ent_CatalogoSencillo> lstAreas { get; set; }
        public List<ent_CatalogoSencillo> lstDesarrolladores { get; set; }
        public List<ent_Incidentes> lstIncidentesActivos { get; set; }
        public List<ent_Incidentes> lstIncidentesAvisos { get; set; }
        public List<ent_Incidentes> lstIncidentes { get; set; }

        public void CargarCatalogos()
        {
            try
            {
                lstAreas = MonitorRN.ClienteWindows.Lista_TrabajoAreas();
                lstDesarrolladores = MonitorRN.ClienteWindows.Lista_Desarrolladores();
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CargarIncidentes()
        {
            try
            {
               
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            try
            {
                CargarCatalogos();

                cbDesarrollador.ValueMember = "D000_nId";
                cbDesarrollador.DisplayMember = "D020_sDescripcion";
                cbDesarrollador.DataSource = lstDesarrolladores.FindAll(item => { return item.D030_bEstatus == true; });

                Actualizar();
            }
            catch (InvalidOperationException ex)
            {
                DialogResult dr = MessageBox.Show(ex.Message, " ! ! ALERTA ¡ ¡ ", MessageBoxButtons.RetryCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.Retry)
                {
                    FrmPrincipal_Load(sender, e);
                }
                if (dr == DialogResult.Cancel)
                {
                    Application.Exit();
                }
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message, " ! ! ALERTA ¡ ¡ ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, " ¡ ¡ ERROR ! ! ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
        private void auditoriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reportes vtnReporte = new Reportes();
            vtnReporte.reporte = 8;
            vtnReporte.ShowDialog();
        }

        #region SegimientoPaquetes
        string sValorModifica { get; set; }

        private void Limpiar_Campos()
        {
            txtRdl.Text = "";
            txtPaquete.Text = "";
            txtEstado.Text = "";
            txtDesarrollador.Text = "";
            txtIncidentes.Text = "";
            txtPaquetes.Text = "";
            txtecha.Text = "";
            txtNotaLibera.Text = "";
            ckbRetro.Checked = false;
            ckbDocPru.Checked = false;

            btnBorrar.Enabled = false;
            btnEditar.Enabled = false;
            ckbRetro.Enabled = false;
            ckbDocPru.Enabled = false;

            cbDesarrollador.Visible = false;
            txtDesarrollador.Visible = true;

            txtRdl.ReadOnly = true;
            txtPaquete.ReadOnly = true;
            txtEstado.ReadOnly = true;
            txtDesarrollador.ReadOnly = true;
            txtIncidentes.ReadOnly = true;
            txtPaquetes.ReadOnly = true;
            txtecha.ReadOnly = true;
            txtNotaLibera.ReadOnly = true;
            txtDesarrollador.ReadOnly = true;

            cbDesarrollador.SelectedIndex = 0;

            btnEditar.Text = "Editar";
        }
        private void Detalle_Paquete(string paquete, int nOrigen)
        {
            Negocio Detalle = new Negocio();
            string[] datos = null;
            datos = Detalle.DatosPaquete(paquete);

            txtRdl.Text = datos.GetValue(0).ToString();
            txtPaquete.Text = datos.GetValue(1).ToString();
            txtEstado.Text = datos.GetValue(2).ToString();
            txtDesarrollador.Text = datos.GetValue(3).ToString();
            txtIncidentes.Text = datos.GetValue(4).ToString();
            gbDetalle.Text = string.Format("Detalle: {0} - {1}", datos.GetValue(0).ToString(), datos.GetValue(1).ToString());
            txtPaquetes.Text = datos.GetValue(5).ToString();
            txtecha.Text = datos.GetValue(6).ToString();
            ckbDocPru.Checked = bool.Parse(datos.GetValue(7).ToString());
            txtObservaciones.Text = datos.GetValue(8).ToString();
            txtIdentificador.Text = datos.GetValue(9).ToString();
            ckbRetro.Checked = bool.Parse(datos.GetValue(10).ToString());
            txtNotaLibera.Text = datos.GetValue(11).ToString();

            if (nOrigen == 1)
            {
                btnEditar.Enabled = true;
                if (Convert.ToInt32(txtRdl.Text) == 0) btnBorrar.Enabled = true; else btnBorrar.Enabled = false;
                txtNotaLibera.ReadOnly = false;
            }
        }
        public void Actualizar()
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
                dgvPaquetes.Rows.Clear();
                dgvPaquetes.Refresh();

                dgvAvisos.Rows.Clear();
                dgvAvisos.Refresh();
            }
        }

        private void TimerPaquete_Tick(object sender, EventArgs e)
        {
            Actualizar();
        }

        private void txtNotaLibera_Leave(object sender, EventArgs e)
        {
            Negocio neg = null;
            string sIncendente = "", sPaquete = "", sNotLib = "";
            try
            {
                neg = new Negocio();

                sIncendente = txtIncidentes.Text.Trim();
                sPaquete = txtPaquete.Text.Trim();
                sNotLib = txtNotaLibera.Text.Trim();

                neg.Actualiza_Paquete(sIncendente, sPaquete, sNotLib);
            }
            catch //(Exception ex)
            {

            }
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

        private void dgvPaquetes_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewCellStyle dtgStyle = e.CellStyle;

            if (dgvPaquetes.Rows[e.RowIndex].Cells[2].Value.ToString().Trim().ToUpper() == "PRUEBAS FUNCIONALES SN") dtgStyle.BackColor = Color.Aqua;
            if (dgvPaquetes.Rows[e.RowIndex].Cells[2].Value.ToString().Trim().ToUpper() == "VALIDACIÓN RECHAZO (FSW)") dtgStyle.BackColor = Color.Red;
            if (dgvPaquetes.Rows[e.RowIndex].Cells[2].Value.ToString().Trim().ToUpper() == "RECHAZO A RAPE") dtgStyle.BackColor = Color.Orange;
            if (dgvPaquetes.Rows[e.RowIndex].Cells[2].Value.ToString().Trim().ToUpper() == "VALIDACIÓN RECHAZO (FSW)(RECHAZO OPERACIONES)") dtgStyle.BackColor = Color.Orange;
            if (dgvPaquetes.Rows[e.RowIndex].Cells[2].Value.ToString().Trim().ToUpper() == "CERRADO (CANCELADO)") dtgStyle.Font = new Font("Microsoft Sans Serif", float.Parse("8.25"), FontStyle.Bold);
            if (dgvPaquetes.Rows[e.RowIndex].Cells[2].Value.ToString().Trim().ToUpper() == "INSTALACIÓN CONCLUIDA") dtgStyle.BackColor = Color.GreenYellow;
            if (dgvPaquetes.Rows[e.RowIndex].Cells[2].Value.ToString().Trim().ToUpper() == "CERRADO LIBERACIÓN NORMAL(INSTALACION CONCLUIDA)") dtgStyle.BackColor = Color.Green;

            e.CellStyle = dtgStyle;
        }
        private void dgvPaquetes_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            sValorModifica = dgvPaquetes.Rows[e.RowIndex].Cells[1].Value.ToString().Trim();
        }
        private void dgvPaquetes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    Limpiar_Campos();
                    Detalle_Paquete(dgvPaquetes.Rows[e.RowIndex].Cells[1].Value.ToString(), 1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void dgvPaquetes_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Negocio neg = null;
            string sIncendente = "", sPaquete = "", sIdentificador = "", sObservaciones = "";
            Boolean bRetroalimentado = false, bDoc_Pru = false;
            if (e.ColumnIndex == 1) dgvPaquetes.Rows[e.RowIndex].Cells[1].Value = sValorModifica;
            if (e.ColumnIndex > 3)
            {
                try
                {
                    neg = new Negocio();

                    sIncendente = dgvPaquetes.Rows[e.RowIndex].Cells[3].Value.ToString().Trim();
                    sPaquete = dgvPaquetes.Rows[e.RowIndex].Cells[1].Value.ToString().Trim();
                    bRetroalimentado = Boolean.Parse(dgvPaquetes.Rows[e.RowIndex].Cells[4].Value.ToString().Trim());
                    bDoc_Pru = Boolean.Parse(dgvPaquetes.Rows[e.RowIndex].Cells[5].Value.ToString().Trim());
                    sIdentificador = dgvPaquetes.Rows[e.RowIndex].Cells[7].Value.ToString().Trim();
                    sObservaciones = dgvPaquetes.Rows[e.RowIndex].Cells[8].Value.ToString().Trim();

                    neg.Actualiza_Paquete(sIncendente, sPaquete, bRetroalimentado, bDoc_Pru, sIdentificador, sObservaciones);
                }
                catch //(Exception ex)
                {

                }
            }
        }

        private void dgvAvisos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    Limpiar_Campos();
                    Detalle_Paquete(dgvAvisos.Rows[e.RowIndex].Cells[0].Value.ToString(), 2);
                }
            }
            catch //(Exception ex)
            {

            }
        }
        private void dgvAvisos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewCellStyle dtgStyle = e.CellStyle;

            if (dgvAvisos.Rows[e.RowIndex].Cells[4].Value.ToString().Trim().ToUpper() == "PRUEBAS FUNCIONALES SN") dtgStyle.BackColor = Color.Aqua;
            if (dgvAvisos.Rows[e.RowIndex].Cells[4].Value.ToString().Trim().ToUpper() == "VALIDACIÓN RECHAZO (FSW)") dtgStyle.BackColor = Color.Red;
            if (dgvAvisos.Rows[e.RowIndex].Cells[4].Value.ToString().Trim().ToUpper() == "RECHAZO A RAPE") dtgStyle.BackColor = Color.Orange;
            if (dgvAvisos.Rows[e.RowIndex].Cells[4].Value.ToString().Trim().ToUpper() == "VALIDACIÓN RECHAZO (FSW)(RECHAZO OPERACIONES)") dtgStyle.BackColor = Color.Orange;
            if (dgvAvisos.Rows[e.RowIndex].Cells[4].Value.ToString().Trim().ToUpper() == "CERRADO (CANCELADO)") dtgStyle.Font = new Font("Microsoft Sans Serif", float.Parse("8.25"), FontStyle.Bold);
            if (dgvAvisos.Rows[e.RowIndex].Cells[4].Value.ToString().Trim().ToUpper() == "INSTALACIÓN CONCLUIDA") dtgStyle.BackColor = Color.GreenYellow;
            if (dgvAvisos.Rows[e.RowIndex].Cells[4].Value.ToString().Trim().ToUpper() == "CERRADO LIBERACIÓN NORMAL (INSTALACION CONCLUIDA)") dtgStyle.BackColor = Color.Green;

            e.CellStyle = dtgStyle;
        }
        #endregion



    }
}
