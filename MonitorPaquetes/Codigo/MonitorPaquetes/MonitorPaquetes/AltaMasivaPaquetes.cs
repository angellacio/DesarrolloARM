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
    public partial class AltaMasivaPaquetes : Form
    {
        public AltaMasivaPaquetes()
        {
            InitializeComponent();
        }

        private void AltaMasivaPaquetes_Load(object sender, EventArgs e)
        {
            Negocio desa = new Negocio();
            cbDesarrollador.DisplayMember = "Nombre";
            cbDesarrollador.ValueMember = "Id_Desa";
            cbDesarrollador.DataSource = desa.Lista_Desarrolladores();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            Negocio registro = new Negocio();
            int ppmc = 0;
            bool resultado = false;
            string errores= string.Empty;
            if (chkPPMC.Checked)
            {
                ppmc = 1;
            }
            try
            {
                if (ValidaCampos())
                {
                    resultado = registro.RegistrarPaqueteMasivo(txtPaquetes.Text, Convert.ToInt32(cbDesarrollador.SelectedValue.ToString()), txtIncidentes.Text, ppmc, out errores);
                    if (resultado == true)
                    { MessageBox.Show("Los paquetes " + txtPaquetes.Text + " fueron regsitrados correctamente", "Registrar Paquete", MessageBoxButtons.OK, MessageBoxIcon.Information); this.Close(); }
                    else
                    { MessageBox.Show("Ocurrio un problema al registar los paquetes " + txtPaquetes.Text + " Error: " + registro.error, "Registrar Paquete", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                else
                {
                    MessageBox.Show("Todos los campos en negritas son obligatorios", "Registro Masivo de Paquetes", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un problema al registar los paquetes " + txtPaquetes.Text + " Error: " + ex.ToString());
            }
        }

        private bool ValidaCampos()
        {
            bool result = false;
            if (txtIncidentes.Text != "" && txtPaquetes.Text != "")
            {
                result = true;
            }
            return result;
        }
    }
}
