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
	public partial class Periodos : Form
	{
		/// <summary>
		/// 
		/// </summary>
		public Periodos()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender">Origen del evento.</param>
		/// <param name="e">Argumentos del evento.</param>
		private void btnBuscarPeriodo_Click(object sender, EventArgs e)
		{
			try
			{
				var client = new ConsultaCatalogosClient();
				var filtro = new CatalogoFiltro();

				filtro.IdOrigen = Convert.ToInt32(txtIDOrigen.Text);
				Periodo[] periodos = client.ConsultarPeriodos(filtro);

				dgPeriodos.DataSource = periodos;
			}
			catch (Exception ex)
			{ MessageBox.Show(ex.Message); }
		}
	}
}