using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CargaDeCatalogos.Catalogos;
using CargaDeCatalogos.Motor;

namespace CargaDeCatalogos
{
    public partial class CargarCatalogos : Form
    {
        public CargarCatalogos()
        {
            InitializeComponent();
        }

        private void btnTransacciones_Click(object sender, EventArgs e)
        {
            Transacciones pantallaTransacciones = new Transacciones();
            pantallaTransacciones.ShowDialog();
        }

        private void btnReglasMotor_Click(object sender, EventArgs e)
        {
            ReglasMotor pantallaReglas = new ReglasMotor();
            pantallaReglas.ShowDialog();
        }

        private void btnPeriodos_Click(object sender, EventArgs e)
        {
            Periodos pantallaPeriodos = new Periodos();
            pantallaPeriodos.ShowDialog();
        }

        private void btnPeriodicidad_Click(object sender, EventArgs e)
        {
            Perioridicidad pantallaPeriodicidad = new Perioridicidad();
            pantallaPeriodicidad.ShowDialog();
        }

        private void btnConceptos_Click(object sender, EventArgs e)
        {
            ConceptoPago pantallaConcepto = new ConceptoPago();
            pantallaConcepto.ShowDialog();
        }

        private void btnAlr_Click(object sender, EventArgs e)
        {
            ALR pantallaALR = new ALR();
            pantallaALR.ShowDialog();
        }
    }
}
