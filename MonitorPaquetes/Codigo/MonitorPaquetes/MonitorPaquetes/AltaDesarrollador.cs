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
    public partial class AltaDesarrollador : Form
    {
        public AltaDesarrollador()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text != "" && txtCorreo.Text != "")
            {
                Negocio Alta_desa = new Negocio();
                if (Alta_desa.Alta_Desarrollador(txtNombre.Text, txtCorreo.Text))
                {
                    MessageBox.Show("Se agrego correctamente el desarrollador", "Alta de Desarrollador", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Ocurrio un error al registrar el desarrollador: " + Alta_desa.error, "Alta de Desarrollador", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Todos los campos son obligatorios", "Alta de Desarrollador", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
