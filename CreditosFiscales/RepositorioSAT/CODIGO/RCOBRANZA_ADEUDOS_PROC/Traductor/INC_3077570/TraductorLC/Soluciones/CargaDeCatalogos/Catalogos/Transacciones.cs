// Referencias de sistemas.
using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

// Referencias personalizadas.
using CargaDeCatalogos.CatalogosDyP;
using SAT.CreditosFiscales.Motor.AccesoDatos.Catalogos;
using Entidades = SAT.CreditosFiscales.Motor.Entidades.Catalogos;

namespace CargaDeCatalogos.Catalogos
{
	/// <summary>
	/// 
	/// </summary>
	public partial class Transacciones : Form
	{
		List<Entidades.CatTransaccion> nuevasTransacciones = new List<Entidades.CatTransaccion>();

		/// <summary>
		/// 
		/// </summary>
		public Transacciones()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender">Origen del evento.</param>
		/// <param name="e">Argumentos del evento.</param>
		private void label1_Click(object sender, EventArgs e)
		{ }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender">Origen del evento.</param>
		/// <param name="e">Argumentos del evento.</param>
		private void btnBuscarDyP_Click(object sender, EventArgs e)
		{
			try
			{
				var client = new ConsultaCatalogosClient();
				var filtro = new CatalogoFiltro();

				filtro.IdOrigen = 5;
				filtro.TipoDocumento = new TipoDocumento();
				filtro.TipoDocumento.IdTipoDocumento = Convert.ToInt32(this.cmbTipoDocumento.SelectedValue);

				Transaccion[] transacciones = client.ConsultarTransaccionesXTipoDocumento(filtro);

				if (transacciones.Count() > 0)
				{
					foreach (CatalogosDyP.Transaccion transaccion in transacciones)
					{
						nuevasTransacciones.Add
						(
							new Entidades.CatTransaccion
								{
									IdAplicacion = Convert.ToInt16(cmbAplicacion.SelectedValue),
									IdTransaccion = transaccion.IdTransaccion.ToString(),
									Descripcion = transaccion.Descripcion,
									IdTipoTransaccion = transaccion.TipoTransaccion.IdTipoTransaccion,
									TipoTransaccion = transaccion.TipoTransaccion.Descripcion,
									IdTipoDocumento = Convert.ToInt16(cmbTipoDocumento.SelectedValue),
									EsRequerido = TransaccionesObligatorias.TransaccionesObligatorias.ObtieneTransaccionObligatoria(Convert.ToInt16(cmbTipoDocumento.SelectedValue), transaccion.IdTransaccion.ToString())
								}
						);
					}

					this.dgListaResultados.DataSource = nuevasTransacciones;
				}
				else
					MessageBox.Show("No existen Datos");

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
		private void btnGuardarDBMotor_Click(object sender, EventArgs e)
		{
			try
			{
				foreach (Entidades.CatTransaccion transaccion in nuevasTransacciones)
				{
					DalCatTransaccion.InsertaCatTrasaccion
						(
							transaccion.IdTransaccion
							, transaccion.Descripcion
							, transaccion.IdTipoTransaccion
							, transaccion.TipoTransaccion
							, transaccion.IdTipoDocumento
							, transaccion.IdAplicacion
							, transaccion.EsRequerido
							, true
						);
				}

				MessageBox.Show("El registro se almacenó correctamente");
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
		private void Transacciones_Load(object sender, EventArgs e)
		{
			try
			{
				List<Entidades.CatTipoDocumento> tipodocumento = DalTipoDocumento.ConsultaTipoDocumentos();
				List<Entidades.Aplicacion> aplicacion = Aplicacion.ConsultaAplicaciones();

				this.cmbTipoDocumento.DataSource = tipodocumento;
				this.cmbTipoDocumento.DisplayMember = "Nombre";
				this.cmbTipoDocumento.ValueMember = "IdTipoDocumento";

				this.cmbAplicacion.DataSource = aplicacion;
				this.cmbAplicacion.DisplayMember = "Nombre";
				this.cmbAplicacion.ValueMember = "IdAplicacion";
			}
			catch (Exception ex) { MessageBox.Show(ex.Message); }
		}
	}
}