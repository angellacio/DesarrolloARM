// Referencias de sistemas.
using System;
using System.Windows.Forms;

// Referencias personalizadas.
using CargaDeCatalogos.CatalogosDyP;

namespace CargaDeCatalogos.Catalogos
{
	/// <summary>
	/// 
	/// </summary>
	public partial class Perioridicidad : Form
	{
		/// <summary>
		/// 
		/// </summary>
		public Perioridicidad()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender">Origen del evento.</param>
		/// <param name="e">Argumentos del evento.</param>
		private void btnBuscarPeriodicidad_Click(object sender, EventArgs e)
		{
			try
			{
				var client = new ConsultaCatalogosClient();
				var filtro = new CatalogoFiltro();

				filtro.IdOrigen = Convert.ToInt32(txtIDOrigen.Text);
				Periodicidad[] periodicidad = client.ConsultarPeriodicidades(filtro);

				dgPeriodicidad.DataSource = periodicidad;
			}
			catch (Exception ex)
			{ MessageBox.Show(ex.Message); }
		}
	}
}