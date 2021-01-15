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
    public partial class Reportes : Form
    {
        public Reportes()
        {
            InitializeComponent();
        }
        public int reporte = 0;
        private DataTable rep_datos = new DataTable();

        private void Reportes_Load(object sender, EventArgs e)
        {
            int mes = DateTime.Now.Month;
            switch (reporte)
            {
                case 1:
                    gpReporte.Text = gpReporte.Text + " de Paquetes Cerrados";
                    cbMeses.SelectedIndex = mes;
                    break;

                case 2:
                    gpReporte.Text = gpReporte.Text + " de Paquetes Rechazados";
                    cbMeses.SelectedIndex = mes;
                    break;

                case 3:
                    gpReporte.Text = gpReporte.Text + " de Paquetes Mensual";
                    cbMeses.SelectedIndex = mes;
                    break;

                case 4:
                    gpReporte.Text = gpReporte.Text + " de Incidentes Semanal";
                    break;

                case 5:
                    gpReporte.Text = gpReporte.Text + " de Incidentes Mensual";
                    cbMeses.SelectedIndex = mes;
                    break;

                case 6:
                    gpReporte.Text = "Catalogo de Estados de Monitor";
                    cbMeses.Enabled = false;
                    break;

                case 7:
                    gpReporte.Text = "Catalogo de Desarrolladores";
                    cbMeses.Enabled = false;
                    break;
                case 8:
                    gpReporte.Text = gpReporte.Text + " de Paquetes para Auditoria";
                    lblFecha.Enabled = true;
                    txtFechaAuditoria.Enabled = true;
                    btnAuditoria.Enabled = true;
                    lblToolTip.Enabled = true;
                    cbMeses.Enabled = false;
                    break;
            }
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            Negocio Export = new Negocio();
            Export.ExportExcel(rep_datos, gpReporte.Text, cbMeses.Text);
            if (Export.ExportExcel(rep_datos, gpReporte.Text, cbMeses.Text))
            {
                MessageBox.Show("Se genero correctamente el reporte : " + Export.Archivo,"Reportes",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error al generear el reporte: " + Export.error, "Reportes", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbMeses_SelectedIndexChanged(object sender, EventArgs e)
        {
            Negocio Reportes_datos = new Negocio();
            rep_datos = new DataTable();
            //MessageBox.Show(cbMeses.SelectedIndex + "--" + cbMeses.Text);

            rep_datos = Reportes_datos.Reportes(reporte, cbMeses.SelectedIndex.ToString());
            
            dtgReportes.DataSource = rep_datos;
            dtgReportes.Refresh();
            if (dtgReportes.RowCount > 0)
            { btnGenerar.Enabled = true; }
            else { btnGenerar.Enabled = false; }
        }

        private void btnAuditoria_Click(object sender, EventArgs e)
        {
            Negocio Reportes_datos = new Negocio();
            rep_datos = new DataTable();

            rep_datos = Reportes_datos.Reportes(reporte, txtFechaAuditoria.Text);

            dtgReportes.DataSource = rep_datos;
            dtgReportes.Refresh();
            if (dtgReportes.RowCount > 0)
            { btnGenerar.Enabled = true; }
            else { btnGenerar.Enabled = false; }
        }
    }
}
