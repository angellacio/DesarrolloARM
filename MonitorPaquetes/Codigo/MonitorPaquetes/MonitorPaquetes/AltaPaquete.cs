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
    public partial class AltaPaquete : Form
    {
        public AltaPaquete()
        {
            InitializeComponent();
        }

        private void btnRegistra_Click(object sender, EventArgs e)
        {
            Negocio registro = new Negocio();
            int ppmc = 0;
            bool resultado = false;
            if(chkPPMC.Checked)
            {
                ppmc = 1;
            }
            try
            {
                if(txtPaquete.Text!="" && cbDesarrollador.SelectedIndex > 0 && txtIncidentes.Text != "")
                {
                        resultado = registro.RegistrarPaquete(txtPaquete.Text, Convert.ToInt32(cbDesarrollador.SelectedValue.ToString()), txtIncidentes.Text, ppmc);
                        if (resultado == true)
                        { MessageBox.Show("El paquete " + txtPaquete.Text + " fue regsitrado correctamente","Registrar Paquete", MessageBoxButtons.OK, MessageBoxIcon.Information); this.Close(); }
                        else
                        { MessageBox.Show("Ocurrio un problema al registar el paquete " + txtPaquete.Text + " Error: " + registro.error,"Registrar Paquete", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                        }
                else
                {
                    MessageBox.Show("Todos los campos en negritas son obligatorios", "Registrar Paquete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un problema al registar el paquete " + txtPaquete.Text + " Error: " + ex.ToString());
            }
           
        }

        private void AltaPaquete_Load(object sender, EventArgs e)
        {
            Negocio desa = new Negocio();
            cbDesarrollador.DisplayMember = "Nombre";
            cbDesarrollador.ValueMember = "Id_Desa";
            cbDesarrollador.DataSource = desa.Lista_Desarrolladores();

        }
    }
}
