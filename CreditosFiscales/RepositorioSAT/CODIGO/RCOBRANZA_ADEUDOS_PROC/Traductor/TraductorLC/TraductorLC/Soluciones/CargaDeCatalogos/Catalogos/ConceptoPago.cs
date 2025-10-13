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
	public partial class ConceptoPago : Form
	{
		/// <summary>
		/// 
		/// </summary>
		public ConceptoPago()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender">Origen del evento.</param>
		/// <param name="e">Argumentos del evento.</param>
		private void btnBuscarConcepto_Click(object sender, EventArgs e)
		{
			try
			{
				var client = new ConsultaCatalogosClient();
				var filtro = new CatalogoFiltro();

				filtro.IdOrigen = Convert.ToInt32(txtIDOrigen.Text);
				CatalogosDyP.ConceptoPago[] conceptos = client.ConsultarConceptosPagos(filtro);

				dgConceptos.DataSource = conceptos;
			}
			catch (Exception ex)
			{ MessageBox.Show(ex.Message); }
		}
	}
}