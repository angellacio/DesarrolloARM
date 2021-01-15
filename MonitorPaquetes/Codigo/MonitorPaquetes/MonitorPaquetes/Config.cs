using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace MonitorPaquetes
{
    public partial class Config : Form
    {
        public Config()
        {
            InitializeComponent();
        }

        private void btnExaminaRepo_Click(object sender, EventArgs e)
        {
            fbDialogRutas.ShowDialog();
            PATH_REPORTES.Enabled = true;
            PATH_REPORTES.Text = fbDialogRutas.SelectedPath;
        }

        private void btnExaminaLog_Click(object sender, EventArgs e)
        {
            fbDialogRutas.ShowDialog();
            RUTA_LOG.Enabled = true;
            RUTA_LOG.Text = fbDialogRutas.SelectedPath;
        }

        private void btnRutaMacro_Click(object sender, EventArgs e)
        {
            fbDialogRutas.ShowDialog();
            RUTA_CORREOS.Enabled = true;
            RUTA_CORREOS.Text = fbDialogRutas.SelectedPath;
        }

        private void btnConexionLocal_Click(object sender, EventArgs e)
        {
            Negocio PruebaConLocal = new Negocio();
            if (PruebaConLocal.Test_Conexion(1))
            {
                MessageBox.Show("Conexion BD Local Satisfactoria");
            }
            else
            {
                MessageBox.Show("No se pudo establecer conexion a la Base de Datos Local", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnConexionRem_Click(object sender, EventArgs e)
        {
            Negocio PruebaConLocal = new Negocio();
            if (PruebaConLocal.Test_Conexion(2))
            {
                MessageBox.Show("Conexion BD Remota Satisfactoria");
            }
            else
            {
                MessageBox.Show("No se pudo establecer conexion a la Base de Datos Remota", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            foreach (Control ctr in Panel_Config.Controls)
            {
                System.Windows.Forms.TabPage tb = new TabPage();
                System.Windows.Forms.GroupBox gb = new GroupBox();

                if (ctr.GetType().Equals(tb.GetType()))
                {
                    foreach (Control ctrtab in ctr.Controls)
                    {
                        if (ctrtab.GetType().Equals(gb.GetType()))
                        {
                            foreach (Control ctrgb in ctrtab.Controls)
                            {
                                MessageBox.Show(ctrtab.Name.ToString() + " - " + ctrgb.Name.ToString());
                            }
                        }
                    }
                }
                
            }
        }

        private void CERRADO_0_Click(object sender, EventArgs e)
        {
            Vista_Correos v_editarCorreos = new MonitorPaquetes.Vista_Correos();
            v_editarCorreos.t_correo = CERRADO_0.Name;
            v_editarCorreos.ShowDialog();
            v_editarCorreos.Dispose();
        }

        private void RECHAZADO_0_Click(object sender, EventArgs e)
        {
            Vista_Correos v_editarCorreos = new MonitorPaquetes.Vista_Correos();
            v_editarCorreos.t_correo = RECHAZADO_0.Name;
            v_editarCorreos.ShowDialog();
            v_editarCorreos.Dispose();
        }

        private void CERRADO_1_Click(object sender, EventArgs e)
        {
            Vista_Correos v_editarCorreos = new MonitorPaquetes.Vista_Correos();
            v_editarCorreos.t_correo = CERRADO_1.Name;
            v_editarCorreos.ShowDialog();
            v_editarCorreos.Dispose();
        }

        private void RECHAZADO_1_Click(object sender, EventArgs e)
        {
            Vista_Correos v_editarCorreos = new MonitorPaquetes.Vista_Correos();
            v_editarCorreos.t_correo = RECHAZADO_1.Name;
            v_editarCorreos.ShowDialog();
            v_editarCorreos.Dispose();
        }

        private void Config_Load(object sender, EventArgs e)
        {
            Negocio valores = new Negocio();
            DataTable Result = new DataTable();
            Result = valores.Datos_Coniguracion();
            Carga_Datos(Result);

            txtBDLocal.Text = ConfigurationManager.AppSettings["Conexion"].ToString();
        }
        private void Carga_Datos(DataTable datos)
        {
            foreach (DataRow dr in datos.Rows)
            {
                Control[] ctrdatos = this.Controls.Find(dr[0].ToString(),true);
                if (ctrdatos != null && ctrdatos.Length > 0)
                {
                    if (ctrdatos[0].GetType().Equals(txtBDLocal.GetType()))
                    {
                        ctrdatos[0].Text = dr[1].ToString();
                    }
                }
            }

            for (int i = Convert.ToInt16(TIPO_ENVIO.Text); i <= Convert.ToInt16(TIPO_ENVIO.Text); i++)
            {
                Control[] ctrdatosi = this.Controls.Find("TIPO_ENVIO"+ TIPO_ENVIO.Text, true);
                ((ctrdatosi[0]) as RadioButton).Checked = true;
            }

            for (int x = Convert.ToInt16(TIPO_ENVIO_INC.Text); x <= Convert.ToInt16(TIPO_ENVIO_INC.Text); x++)
            {
                Control[] ctrdatosx = this.Controls.Find("TIPO_ENVIO_INC" + TIPO_ENVIO_INC.Text, true);
                ((ctrdatosx[0]) as RadioButton).Checked = true;
            }
        }

    }
}
