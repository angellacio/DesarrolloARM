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
using MonitorPaquetes.ManejoIncidentes;
using MonitorRN = MonitorReglaNegocios;
using LogMensaje = MonitorReglaNegocios.Log.escribeMensaje;

namespace MonitorPaquetes
{
    public partial class PrincipalNew : Form
    {
        public PrincipalNew()
        {
            InitializeComponent();
        }

        private List<ent_CatalogoSencillo> lstAreas { get; set; }
        private List<ent_CatalogoSencillo> lstDesarrolladores { get; set; }
        private ent_Incidentes itemInsidente { get; set; }

        public void ValidaInformacion()
        {
            if (txtRMA.Text.Trim() == "")
                erpManejoIncidentes.SetError(txtRMA, "Falta el numero del RMA.");
            else
                erpManejoIncidentes.SetError(txtRMA, "");

            if (cmbRMA_Area.SelectedValue != null)
            {
                if (cmbRMA_Area.SelectedValue.ToString() == "-1")
                    erpManejoIncidentes.SetError(cmbRMA_Area, "Falta el area.");
                else
                    erpManejoIncidentes.SetError(cmbRMA_Area, "");
            }

            if (txtRMA_Identificador.Text.Trim() == "")
                erpManejoIncidentes.SetError(txtRMA_Identificador, "Falta el identificador del RMA.");
            else
                erpManejoIncidentes.SetError(txtRMA_Identificador, "");


            if (cmbPQT_Desarrollador.SelectedValue != null)
            {
                if (cmbPQT_Desarrollador.SelectedValue.ToString() == "-1")
                    erpManejoIncidentes.SetError(cmbPQT_Desarrollador, "Falta el desarrollador.");
                else
                    erpManejoIncidentes.SetError(cmbPQT_Desarrollador, "");
            }


            if (txtPaquete.Text.Trim() == "")
                erpManejoIncidentes.SetError(txtPaquete, "Falta el identificador del paquete");
            else
                erpManejoIncidentes.SetError(txtPaquete, "");

        }

        public void CargarCatalogos()
        {
            try
            {
                lstAreas = MonitorRN.ClienteWindows.Lista_TrabajoAreas();
                lstDesarrolladores = MonitorRN.ClienteWindows.Lista_Desarrolladores();


            }
            catch (InvalidOperationException ex)
            {
                LogMensaje.mensajeError(ex.ToString());
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
        public void CargaReportes()
        {
            List<ent_IncidentesPaquetes> lstDatos = null;
            Boolean bolMuestraTodo = false;
            try
            {
                if (rdbTodos.Checked) bolMuestraTodo = true;

                lstDatos = MonitorReglaNegocios.ClienteWindows.Incs_ListaPaquetes(bolMuestraTodo);

                dgvPaquetes.DataSource = lstDatos;
                dgvPaquetes.AutoGenerateColumns = false;
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            { }
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            try
            {
                CargarCatalogos();

                cmbRMA_Area.ValueMember = "D000_sId";
                cmbRMA_Area.DisplayMember = "D020_sDescripcion";
                cmbRMA_Area.DataSource = lstAreas;

                cmbPQT_Desarrollador.ValueMember = "D000_nId";
                cmbPQT_Desarrollador.DisplayMember = "D020_sDescripcion";
                cmbPQT_Desarrollador.DataSource = lstDesarrolladores;

                CargaReportes();

                ValidaInformacion();
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

            CargaReportes();
        }
        private void altaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AltaDesarrollador vtnDesarrollador = new AltaDesarrollador();
            vtnDesarrollador.ShowDialog();
            vtnDesarrollador.Dispose();

            CargaReportes();
        }
        private void altaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AltaEstado vtnEstado = new AltaEstado();
            vtnEstado.ShowDialog();
            vtnEstado.Dispose();
        }
        private void masivaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AltaMasivaPaquetes vtnMasiva = new AltaMasivaPaquetes();
            vtnMasiva.ShowDialog();
            vtnMasiva.Dispose();
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

        private void TimerPaquete_Tick(object sender, EventArgs e)
        {
            CargaReportes();
        }

        private void rdbActivos_CheckedChanged(object sender, EventArgs e)
        {
            Limpiar_Campos();
            CargaReportes();
        }
        private void rdbTodos_CheckedChanged(object sender, EventArgs e)
        {
            Limpiar_Campos();
            CargaReportes();
        }

        private void btnRMA_Observaciones_Click(object sender, EventArgs e)
        {
            frmObservaciones frmObs = new frmObservaciones(txtRMA.Text.Trim(), null, itemInsidente.lstObservaciones);
            frmObs.ShowDialog();

            string sObservacionesInc = "";
            if (frmObs.lstObser != null && frmObs.lstObser.Count > 0)
                frmObs.lstObser.ForEach(item => { sObservacionesInc = string.Format("{0}{1}\r\n", sObservacionesInc, item.ToString()); });
            tltMensajes.SetToolTip(btnRMA_Observaciones, sObservacionesInc);

        }
        private void btnPQT_Observaciones_Click(object sender, EventArgs e)
        {
            frmObservaciones frmObs = new frmObservaciones(txtRMA.Text.Trim(), txtPaquete.Text.Trim(), itemInsidente.lstPaquetes[0].lstObservaciones);
            frmObs.ShowDialog();

            string sObservacionesInc = "";
            if (frmObs.lstObser != null && frmObs.lstObser.Count > 0)
                frmObs.lstObser.ForEach(item => { sObservacionesInc = string.Format("{0}{1}\r\n", sObservacionesInc, item.ToString()); });
            tltMensajes.SetToolTip(btnRMA_Observaciones, sObservacionesInc);

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Limpiar_Campos();
        }

        private void dgvPaquetes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    Limpiar_Campos();
                    Detalle_RMA(dgvPaquetes.Rows[e.RowIndex].Cells[2].Value.ToString().Trim(), dgvPaquetes.Rows[e.RowIndex].Cells[7].Value.ToString().Trim());
                    ValidaInformacion();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void dgvPaquetes_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewCellStyle dtgStyle = e.CellStyle;
            string sEstado = dgvPaquetes.Rows[e.RowIndex].Cells[16].Value.ToString().Trim();

            if (sEstado != "")
            {
                sEstado = sEstado.Trim().ToUpper();

                switch (sEstado)
                {
                    case "PRUEBAS FUNCIONALES SN":
                        dtgStyle.BackColor = Color.Aqua;
                        break;
                    case "VALIDACIÓN RECHAZO (FSW)":
                    case "VALIDACIÓN RECHAZO (FSW)(RECHAZO OPERACIONES)":
                        dtgStyle.BackColor = Color.Red;
                        break;
                    case "RECHAZO A RAPE":
                        dtgStyle.BackColor = Color.Orange;
                        break;
                    case "CERRADO (CANCELADO)":
                        dtgStyle.Font = new Font("Microsoft Sans Serif", float.Parse("8.25"), FontStyle.Bold);
                        break;
                    case "INSTALACIÓN CONCLUIDA":
                        dtgStyle.BackColor = Color.GreenYellow;
                        break;
                    case "CERRADO LIBERACIÓN NORMAL (INSTALACION CONCLUIDA)":
                        dtgStyle.BackColor = Color.Green;
                        break;
                }
            }

            e.CellStyle = dtgStyle;

            if (e.RowIndex > 0 && e.ColumnIndex == 0)
            {
                if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
                {
                    e.Value = "";
                    e.FormattingApplied = true;
                }
            }

        }

        public void Limpiar_Campos()
        {
            ckbRMA_PPMC.Enabled = false;
            ckbRMA_Retroalimentado.Enabled = false;
            btnRMA_Observaciones.Enabled = false;
            cmbRMA_Area.Enabled = false;
            txtRMA_Identificador.ReadOnly = true;
            txtRMA_NotaLibera.ReadOnly = true;

            gbRMA.Text = "Incidentes";
            txtRMA.Text = "";
            ckbRMA_PPMC.Checked = false;
            ckbRMA_Retroalimentado.Checked = false;
            tltMensajes.SetToolTip(btnRMA_Observaciones, "");
            cmbRMA_Area.SelectedIndex = 0;
            txtRMA_Identificador.Text = "";
            txtRMA_NotaLibera.Text = "";

            txtPaquete.ReadOnly = true;
            ckbDOC_PRU.Enabled = false;
            btnPQT_Observaciones.Enabled = false;
            cmbPQT_Desarrollador.Enabled = false;
            txtPQT_Identificador.ReadOnly = true;

            gbPQT.Text = "Paquete";
            txtPaquete.Text = "";
            txtPQT_Identificador.Text = "";
            ckbDOC_PRU.Checked = false;
            tltMensajes.SetToolTip(btnPQT_Observaciones, "");
            txtPQT_Identificador.Text = "";
        }
        public void Detalle_RMA(string sRMA, string sPaquete)
        {
            try
            {
                itemInsidente = MonitorReglaNegocios.ClienteWindows.Incs_Lista(sRMA, sPaquete);

                ckbRMA_PPMC.Enabled = true;
                ckbRMA_Retroalimentado.Enabled = true;
                btnRMA_Observaciones.Enabled = true;
                cmbRMA_Area.Enabled = true;
                txtRMA_Identificador.ReadOnly = false;
                txtRMA_NotaLibera.ReadOnly = false;
                txtRMA.ReadOnly = false;

                gbRMA.Text = itemInsidente.D010_sIncidente.Trim().ToUpper();

                txtRMA.Text = itemInsidente.D010_sIncidente.Trim().ToUpper();
                ckbRMA_Retroalimentado.Checked = itemInsidente.D013_bRetroalimentado;
                cmbRMA_Area.SelectedValue = itemInsidente.D001_sIDArea;
                lblRMA_fAlta.Text = string.Format("{0:dd/MM/yyyy hh:mm tt}", itemInsidente.D011_fAlta);
                txtRMA_Identificador.Text = itemInsidente.D012_sDescripcion;
                txtRMA_NotaLibera.Text = itemInsidente.D014_sNotaLibera;
                ckbRMA_PPMC.Checked = itemInsidente.D015_bPPMC;

                string sObservacionesInc = "";
                if (itemInsidente.lstObservaciones != null && itemInsidente.lstObservaciones.Count > 0)
                    itemInsidente.lstObservaciones.ForEach(item => { sObservacionesInc = string.Format("{0}{1}\r\n", sObservacionesInc, item.ToString()); });
                tltMensajes.SetToolTip(btnRMA_Observaciones, sObservacionesInc);

                txtPaquete.ReadOnly = false;
                ckbDOC_PRU.Enabled = true;
                ckbNotificado.Enabled = true;
                btnPQT_Observaciones.Enabled = true;
                cmbPQT_Desarrollador.Enabled = true;
                txtPQT_Identificador.ReadOnly = false;

                if (itemInsidente.lstPaquetes.Count > 0)
                {
                    gbPQT.Text = itemInsidente.lstPaquetes[0].D010_sPaquete;
                    txtPaquete.Text = itemInsidente.lstPaquetes[0].D010_sPaquete;
                    lblPQT_fAlta.Text = string.Format("{0:dd/MM/yyyy hh:mm tt}", itemInsidente.lstPaquetes[0].D020_fAlta);
                    ckbDOC_PRU.Checked = itemInsidente.lstPaquetes[0].D031_bDOC_PRU;
                    cmbPQT_Desarrollador.SelectedValue = itemInsidente.lstPaquetes[0].D035_nDesarrollador;
                    txtPQT_Identificador.Text = itemInsidente.lstPaquetes[0].D032_sObservaciones;

                    gbRDL.Text = string.Format("{0} :: {1}", itemInsidente.lstPaquetes[0].D40_nUltimaRDL, itemInsidente.lstPaquetes[0].D42_sUltimoEstatusMoni);
                    dtgvRDL.DataSource = itemInsidente.lstPaquetes[0].lstRDL.OrderByDescending(item => item.D012_fMovimiento).ToList();
                    dtgvRDL.AutoGenerateColumns = true;

                    string sObservacionesPaq = "";
                    if (itemInsidente.lstPaquetes[0].lstObservaciones.Count > 0)
                        itemInsidente.lstPaquetes[0].lstObservaciones.ForEach(item => { sObservacionesPaq = string.Format("{0}{1}\r\n", sObservacionesPaq, item.ToString()); });
                    tltMensajes.SetToolTip(btnPQT_Observaciones, sObservacionesPaq);
                }
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

        private void dgvPaquetes_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex > 0 && e.ColumnIndex == 0)
            {
                e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;
                if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
                {

                    e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
                }
                else e.AdvancedBorderStyle.Top = dgvPaquetes.AdvancedCellBorderStyle.Top;
            }
        }
        private Boolean IsTheSameCellValue(int column, int row)
        {
            Boolean bolResult = false;
            DataGridViewCell cell1 = dgvPaquetes[column, row];
            DataGridViewCell cell2 = dgvPaquetes[column, row - 1];
            if (cell1.Value != DBNull.Value && cell2.Value != DBNull.Value)
            {
                if (cell1.Value.ToString().Trim() == cell2.Value.ToString().Trim())
                {
                    bolResult = true;
                }
            }
            return bolResult;
        }

        private void cmbRMA_Area_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string sRMA = "", sArea = "";
            try
            {
                ValidaInformacion();
                if (cmbRMA_Area.SelectedValue != null)
                {
                    if (erpManejoIncidentes.GetError(cmbRMA_Area) == "")
                    {
                        sRMA = gbRMA.Text.Trim();
                        sArea = cmbRMA_Area.SelectedValue.ToString();
                        MonitorReglaNegocios.ClienteWindows.Incs_Act_Area(sRMA, sArea);

                        CargaReportes();
                    }
                }
            }
            catch (ApplicationException ex) { MessageBox.Show(ex.ToString()); }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }
        private void cmbPQT_Desarrollador_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string sRMA = "", sPaquete = "";
            int nDesarollador = -1;
            try
            {
                ValidaInformacion();
                if (cmbPQT_Desarrollador.SelectedValue != null)
                {
                    if (erpManejoIncidentes.GetError(cmbPQT_Desarrollador) == "")
                    {
                        sRMA = gbRMA.Text.Trim();
                        sPaquete = gbPQT.Text.Trim();
                        nDesarollador = int.Parse(cmbPQT_Desarrollador.SelectedValue.ToString());
                        MonitorReglaNegocios.ClienteWindows.Incs_Act_Desarrollador(sRMA, sPaquete, nDesarollador);

                        CargaReportes();
                    }
                }
            }
            catch (ApplicationException ex) { MessageBox.Show(ex.ToString()); }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }
        private void txtRMA_Leave(object sender, EventArgs e)
        {
            string sRMA = "", sRMANew = "";
            try
            {
                ValidaInformacion();

                if (erpManejoIncidentes.GetError(txtRMA).Trim() == "")
                {
                    sRMA = gbRMA.Text.Trim();
                    sRMANew = txtRMA.Text.Trim().ToUpper();
                    MonitorReglaNegocios.ClienteWindows.Incs_Act_RMA(sRMA, sRMANew);

                    CargaReportes();
                }
            }
            catch (ApplicationException ex) { MessageBox.Show(ex.ToString()); }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }
        private void ckbRMA_PPMC_Click(object sender, EventArgs e)
        {
            string sRMA = "", sPaquete = "";
            Boolean bChed = false;
            try
            {
                ValidaInformacion();

                sRMA = gbRMA.Text.Trim();
                sPaquete = gbPQT.Text.Trim();
                bChed = ckbRMA_PPMC.Checked;
                MonitorReglaNegocios.ClienteWindows.Incs_Act_PPMC(sRMA, bChed);

                CargaReportes();
            }
            catch (ApplicationException ex) { MessageBox.Show(ex.ToString()); }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }
        private void ckbRMA_Retroalimentado_Click(object sender, EventArgs e)
        {
            string sRMA = "", sPaquete = "";
            Boolean bChed = false;
            try
            {
                ValidaInformacion();

                sRMA = gbRMA.Text.Trim();
                sPaquete = gbPQT.Text.Trim();
                bChed = ckbRMA_Retroalimentado.Checked;
                MonitorReglaNegocios.ClienteWindows.Incs_Act_Retroalimentado(sRMA, bChed);

                CargaReportes();
            }
            catch (ApplicationException ex) { MessageBox.Show(ex.ToString()); }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }
        private void ckbDOC_PRU_Click(object sender, EventArgs e)
        {
            string sRMA = "", sPaquete = "";
            Boolean bChed = false;
            try
            {
                ValidaInformacion();

                sRMA = gbRMA.Text.Trim();
                sPaquete = gbPQT.Text.Trim();
                bChed = ckbDOC_PRU.Checked;
                MonitorReglaNegocios.ClienteWindows.Incs_Act_DocPru(sRMA, sPaquete, bChed);

                CargaReportes();
            }
            catch (ApplicationException ex) { MessageBox.Show(ex.ToString()); }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }
        private void txtRMA_Identificador_Leave(object sender, EventArgs e)
        {
            string sRMA = "", sIdentificador = "";
            try
            {
                ValidaInformacion();

                if (erpManejoIncidentes.GetError(txtRMA_Identificador).Trim() == "")
                {
                    sRMA = gbRMA.Text.Trim();
                    sIdentificador = txtRMA_Identificador.Text.Trim();
                    MonitorReglaNegocios.ClienteWindows.Incs_Act_Identificador(sRMA, sIdentificador);

                    CargaReportes();
                }
            }
            catch (ApplicationException ex) { MessageBox.Show(ex.ToString()); }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }
        private void txtRMA_NotaLibera_Leave(object sender, EventArgs e)
        {
            string sRMA = "", sPaquete = "", sNotaLibera = "";
            try
            {
                ValidaInformacion();

                sRMA = gbRMA.Text.Trim();
                sPaquete = gbPQT.Text.Trim();
                sNotaLibera = txtRMA_NotaLibera.Text.Trim();
                MonitorReglaNegocios.ClienteWindows.Incs_Act_NotaLibera(sRMA, sNotaLibera);

                CargaReportes();
            }
            catch (ApplicationException ex) { MessageBox.Show(ex.ToString()); }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }
        private void ckbNotificado_Click(object sender, EventArgs e)
        {
            string sRMA = "", sPaquete = "";
            Boolean bChed = false;
            try
            {
                ValidaInformacion();

                sRMA = gbRMA.Text.Trim();
                sPaquete = gbPQT.Text.Trim();
                bChed = ckbNotificado.Checked;
                MonitorReglaNegocios.ClienteWindows.Incs_Act_Notificado(sRMA, sPaquete, bChed);

                CargaReportes();
            }
            catch (ApplicationException ex) { MessageBox.Show(ex.ToString()); }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        private void txtPaquete_Leave(object sender, EventArgs e)
        {
            string sRMA = "", sPaquete = "";
            string sPaqueteNew = "";
            try
            {
                ValidaInformacion();

                sRMA = gbRMA.Text.Trim();
                sPaquete = gbPQT.Text.Trim();
                sPaqueteNew = txtPaquete.Text.Trim();
                MonitorReglaNegocios.ClienteWindows.Incs_Act_Paquete(sRMA, sPaquete, sPaqueteNew);

                CargaReportes();
            }
            catch (ApplicationException ex) { MessageBox.Show(ex.ToString()); }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }
        private void txtPQT_Identificador_Leave(object sender, EventArgs e)
        {
            string sRMA = "", sPaquete = "";
            string sIdentificador = "";
            try
            {
                ValidaInformacion();

                sRMA = gbRMA.Text.Trim();
                sPaquete = gbPQT.Text.Trim();
                sIdentificador = txtPQT_Identificador.Text.Trim();
                MonitorReglaNegocios.ClienteWindows.Incs_Act_Identificador(sRMA, sPaquete, sIdentificador);

                CargaReportes();
            }
            catch (ApplicationException ex) { MessageBox.Show(ex.ToString()); }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        #endregion

        private void btnPTrabajo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (btnPTrabajo.Text.Trim() == "Ocultar Panel Trabajo")
            {
                btnPTrabajo.Text = "Mostrar Panel Trabajo";
                //gbDatos.Location = new Point(0, 0);
                gbDatos.Dock = DockStyle.Fill;
            }
            else
            {
                btnPTrabajo.Text = "Ocultar Panel Trabajo";
                //gbDatos.Location = new Point(366, 0);
                gbDatos.Dock = DockStyle.None;
                gbDatos.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom);
            }
        }
    }
}
