using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ManejoCanciones.uControl
{
    public partial class cDatoCancion : UserControl
    {
        public string rutaCancion
        {
            get
            {
                return txtRuta.Text.Trim();
            }
            set
            {
                txtRuta.Text = value.Trim();
            }
        }

        public cDatoCancion()
        {
            InitializeComponent();
        }
    }
}
