// Referencias de sistema.
using System;
using System.Windows.Forms;

// Referebcias personalizadas.
using CargaDeCatalogos.Motor;
using CargaDeCatalogos.Catalogos;

namespace CargaDeCatalogos
{
	/// <summary>
	/// 
	/// </summary>
	public partial class CargarCatalogos : Form
	{
		/// <summary>
		/// 
		/// </summary>
		public CargarCatalogos()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender">Origen del evento.</param>
		/// <param name="e">Argumentos del evento.</param>
		private void btnTransacciones_Click(object sender, EventArgs e)
		{
			var pantallaTransacciones = new Transacciones();
			pantallaTransacciones.ShowDialog();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender">Origen del evento.</param>
		/// <param name="e">Argumentos del evento.</param>
		private void btnReglasMotor_Click(object sender, EventArgs e)
		{
			var pantallaReglas = new ReglasMotor();
			pantallaReglas.ShowDialog();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender">Origen del evento.</param>
		/// <param name="e">Argumentos del evento.</param>
		private void btnPeriodos_Click(object sender, EventArgs e)
		{
			var pantallaPeriodos = new Periodos();
			pantallaPeriodos.ShowDialog();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender">Origen del evento.</param>
		/// <param name="e">Argumentos del evento.</param>
		private void btnPeriodicidad_Click(object sender, EventArgs e)
		{
			var pantallaPeriodicidad = new Perioridicidad();
			pantallaPeriodicidad.ShowDialog();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender">Origen del evento.</param>
		/// <param name="e">Argumentos del evento.</param>
		private void btnConceptos_Click(object sender, EventArgs e)
		{
			var pantallaConcepto = new ConceptoPago();
			pantallaConcepto.ShowDialog();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender">Origen del evento.</param>
		/// <param name="e">Argumentos del evento.</param>
		private void btnAlr_Click(object sender, EventArgs e)
		{
			var pantallaALR = new ALR();
			pantallaALR.ShowDialog();
		}
	}
}