using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonitorPaquetes
{
    public partial class Vista_Correos : Form
    {
        public string t_correo = string.Empty;
        private Negocio edit_mail = new Negocio();
        string path = string.Empty;

        public Vista_Correos()
        {
            InitializeComponent();
        }

        private void btnVPrevia_Click(object sender, EventArgs e)
        {
            edit_mail.guardar_temp(rtxtCodCorreo.Text, t_correo, out path);
            System.Diagnostics.Process.Start(@""+path);
        }

        private void Vista_Correos_Load(object sender, EventArgs e)
        {
            rtxtCodCorreo.Text =  edit_mail.config_parametro(t_correo);
        }
    }
}
