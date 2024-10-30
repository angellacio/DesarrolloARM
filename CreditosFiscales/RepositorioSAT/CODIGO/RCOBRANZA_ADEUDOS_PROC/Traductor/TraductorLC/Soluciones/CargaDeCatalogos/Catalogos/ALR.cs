using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CargaDeCatalogos.Catalogos
{
    public partial class ALR : Form
    {
        public ALR()
        {
            InitializeComponent();
        }

        private void btnBuscarALR_Click(object sender, EventArgs e)
        {
            try
            {
                CatalogosDyP.ConsultaCatalogosClient client = new CatalogosDyP.ConsultaCatalogosClient();
                CatalogosDyP.CatalogoFiltro filtro = new CatalogosDyP.CatalogoFiltro();

                filtro.IdOrigen = Convert.ToInt32(txtIDOrigen.Text);
                CatalogosDyP.Alr[] ALRs = client.ConsultarAlrs(filtro);

                dgALR.DataSource = ALRs;
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
    }
}
