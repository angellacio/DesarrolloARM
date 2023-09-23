using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using rnSeg = WA_RN.Seguridad;
using entSeg = WA_Entidades.Seguridad;
using exM = UtileriasComunes.ManejoErrores;

namespace WA_Formas
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void BtnEntrar_Click(object sender, EventArgs e)
        {
            entSeg.EntDatosAutentificacion datSeguridad;
            try
            {

                rnSeg.IManejoSeguridad ManejoSeg = new rnSeg.ManejoSeguridad();

                datSeguridad = ManejoSeg.ValidaUsuario(TxtUsuario.Text.Trim(), TxtContraseña.Text.Trim(), true);

                FrmInicio frmInicia = new FrmInicio(datSeguridad);
                frmInicia.Show();

                this.Close();
            }
            catch (exM.ErroresAplicacion ex)
            {
                MessageBox.Show(ex.MensajeUsuario);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
