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
    public partial class Perioridicidad : Form
    {
        public Perioridicidad()
        {
            InitializeComponent();
        }

        private void btnBuscarPeriodicidad_Click(object sender, EventArgs e)
        {
            try
            {
                CatalogosDyP.ConsultaCatalogosClient client = new CatalogosDyP.ConsultaCatalogosClient();
                CatalogosDyP.CatalogoFiltro filtro = new CatalogosDyP.CatalogoFiltro();

                filtro.IdOrigen = Convert.ToInt32(txtIDOrigen.Text);
                CatalogosDyP.Periodicidad[] periodicidad = client.ConsultarPeriodicidades(filtro);

                dgPeriodicidad.DataSource = periodicidad;
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
    }
}
