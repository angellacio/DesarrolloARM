// Referencias de sistemas.
using System;
using System.Xml;
using System.Windows.Forms;
using System.Collections.Generic;

// Referencias personalizadas.
using SAT.CreditosFiscales.Motor.AccesoDatos.AdmonReglas;
using SAT.CreditosFiscales.Motor.AccesoDatos.Catalogos;
using Entidades = SAT.CreditosFiscales.Motor.Entidades.Catalogos;
using EntidadRegla = SAT.CreditosFiscales.Motor.Entidades.AdmonRegla;

namespace CargaDeCatalogos.Motor
{
	/// <summary>
	/// 
	/// </summary>
	public partial class ReglasMotor : Form
	{
		/// <summary>
		/// 
		/// </summary>
		public ReglasMotor()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender">Origen del evento.</param>
		/// <param name="e">Argumentos del evento.</param>
		private void ReglasMotor_Load(object sender, EventArgs e)
		{
			try
			{
				List<Entidades.CatTipoDocumento> tipodocumento = DalTipoDocumento.ConsultaTipoDocumentos();
				List<Entidades.Aplicacion> aplicacion = Aplicacion.ConsultaAplicaciones();

				cmbTipoDocumento.DataSource = tipodocumento;
				cmbTipoDocumento.DisplayMember = "Nombre";
				cmbTipoDocumento.ValueMember = "IdTipoDocumento";

				cmbAplicacion.DataSource = aplicacion;
				cmbAplicacion.DisplayMember = "Nombre";
				cmbAplicacion.ValueMember = "IdAplicacion";

				CargarReglas(null, null);

			}
			catch (Exception ex) { MessageBox.Show(ex.Message); }
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender">Origen del evento.</param>
		/// <param name="e">Argumentos del evento.</param>
		private void btnExaminar_Click(object sender, EventArgs e)
		{
			FileDialogExaminar.Filter = "Xml files (*.xml, *.xslt, *.xsl) | *.xml; *.xslt; *.xsl;";
			FileDialogExaminar.ShowDialog();

			if (FileDialogExaminar.FileName != string.Empty)
				txtArchivoRegla.Text = FileDialogExaminar.FileName;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="idAplicacion"></param>
		/// <param name="idTipoDocPago"></param>
		private void CargarReglas(short? idAplicacion = null, short? idTipoDocPago = null)
		{
			dgReglas.DataSource = DalReglas.ConsultaReglas(idAplicacion, idTipoDocPago).ListaReglas;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender">Origen del evento.</param>
		/// <param name="e">Argumentos del evento.</param>
		private void btnBuscarReglas_Click(object sender, EventArgs e)
		{
			try
			{
				CargarReglas(Convert.ToInt16(cmbAplicacion.SelectedValue), Convert.ToInt16(cmbTipoDocumento.SelectedValue));
			}
			catch (Exception ex) { MessageBox.Show(ex.Message); }
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender">Origen del evento.</param>
		/// <param name="e">Argumentos del evento.</param>
		private void dgReglas_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			//Asignar los datos en lugar correspondiente
			try
			{
				lblIDRegla.Text = dgReglas.Rows[e.RowIndex].Cells["IdRegla"].FormattedValue.ToString();
				txtDescripcion.Text = dgReglas.Rows[e.RowIndex].Cells["Descripcion"].FormattedValue.ToString();
				ckEsValidacion.Checked = Convert.ToBoolean(dgReglas.Rows[e.RowIndex].Cells["EsValidacion"].FormattedValue);
				ckAntesInsersion.Checked = Convert.ToBoolean(dgReglas.Rows[e.RowIndex].Cells["AntesDeInsercion"].FormattedValue);
				txtSecuencia.Text = dgReglas.Rows[e.RowIndex].Cells["Secuencia"].FormattedValue.ToString();
				txtArchivoRegla.Text = string.Empty;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender">Origen del evento.</param>
		/// <param name="e">Argumentos del evento.</param>
		private void btnNuevaRegla_Click(object sender, EventArgs e)
		{
			this.lblIDRegla.Text = string.Empty;
			this.txtDescripcion.Text = string.Empty;
			this.ckEsValidacion.Checked = false;
			this.ckEsValidacion.Checked = false;
			this.txtArchivoRegla.Text = string.Empty;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender">Origen del evento.</param>
		/// <param name="e">Argumentos del evento.</param>
		private void btnGuardar_Click(object sender, EventArgs e)
		{
			try
			{
				// Guardar Regla
				var documentoRegla = new XmlDocument();
				documentoRegla.Load(txtArchivoRegla.Text);
				Guid gIdRegla = DalReglas.InsertaRegla(txtDescripcion.Text.Trim(), documentoRegla.InnerXml, ckEsValidacion.Checked);

				// Guardar Admon Regla
				DalReglas.InsertaAdmonRegla
										(
											gIdRegla
											, Convert.ToInt32(cmbAplicacion.SelectedValue)
											, Convert.ToInt32(cmbTipoDocumento.SelectedValue)
											, Convert.ToInt32(txtSecuencia.Text)
											, ckAntesInsersion.Checked
										);

				MessageBox.Show("La regla se almacenó correctamente");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}
}