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
    public partial class AltaEstado : Form
    {
        public AltaEstado()
        {
            InitializeComponent();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (cbArea.SelectedIndex > 0 && txtIdEstado.Text != "" && txtDescripcion.Text != "")
            {
                Negocio Alta_edos = new Negocio();
                if (Alta_edos.Alta_Estados(Convert.ToInt32(txtIdEstado.Text), txtDescripcion.Text,cbArea.Text))
                {
                    MessageBox.Show("Se agrego correctamente el estado", "Alta de Estados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Ocurrio un error al registrar el estado: " + Alta_edos.error, "Alta de Estados", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Todos los campos son obligatorios", "Alta de Estados", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AltaEstado_Load(object sender, EventArgs e)
        {
            cbArea.SelectedIndex = 0;
        }
    }
}
