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
    public partial class Configuracion : Form
    {
        public Configuracion()
        {
            InitializeComponent();
        }

        private void Configuracion_Load(object sender, EventArgs e)
        {
            Negocio v_config = new Negocio();
            dgvConfiguracion.DataSource = v_config.Datos_Coniguracion();
            dgvConfiguracion.Refresh();
        }
    }
}
