//@(#)SCADE2(W:SKDN08221CO2:SAT.DyP.Routing.Configuration.Client:frmMain:0:21/May/2008[SAT.DyP.Routing.Configuration.Client:1.0:21/May/2008])
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SAT.DyP.Routing.Configuration.Client {
    public partial class frmMain : Form {
        public frmMain() {
            InitializeComponent();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e) {
            if (DialogResult.Yes == MessageBox.Show("¿Salir del cliente de configuración de ruteo?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)) {                
                e.Cancel = false;
            }
            else {
                e.Cancel = true;
            }
        }
        
        private void ruteoToolStripMenuItem_Click(object sender, EventArgs e) {
            this.ruteoToolStripMenuItem.Enabled = false;
            frmConfiguration configuracion = new frmConfiguration();
            configuracion.FormClosed += new FormClosedEventHandler(configuracion_FormClosed);
            configuracion.MdiParent = this;
            configuracion.Show();
        }

        void configuracion_FormClosed(object sender, FormClosedEventArgs e) {
            this.ruteoToolStripMenuItem.Enabled = true;
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e) {
            this.acercaDeToolStripMenuItem.Enabled = false;
            frmAbout about = new frmAbout();
            about.MdiParent = this;
            about.FormClosed += new FormClosedEventHandler(about_FormClosed);
            about.Show();
        }

        void about_FormClosed(object sender, FormClosedEventArgs e) {
            this.acercaDeToolStripMenuItem.Enabled = true;
        }
    }
}