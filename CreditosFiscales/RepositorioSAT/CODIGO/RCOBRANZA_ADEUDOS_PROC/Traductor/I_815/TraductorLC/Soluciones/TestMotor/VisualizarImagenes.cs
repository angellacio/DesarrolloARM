/*
 * ----------------------------------------
 * Creado por: Mario Escarpulli 
 * Creado: 15/Junio/2019
 * NOTAS: Esta clase se creó con base al Requerimiento de Servicio (RES)
 *        CZA_CREDFIS: Nuevo Servicio en Motor Traductor para la entrega de sección de línea de captura.
 * ----- Historial de actualizaciones -----
 * Actualizado por: Mario Escarpulli
 * Actualizado el: 21/Junio/2019
 * ----------------------------------------
*/

// Referencias personalizadas.
using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace TestMotor
{
	/// <summary>
	/// Visualizador de imágenes.
	/// </summary>
	public partial class VisualizarImagenes : Form
	{
		/// <summary>
		/// Formulario que permite visualizar un archivo de imagen.
		/// </summary>
		public VisualizarImagenes()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Inicializa el formulario.
		/// </summary>
		/// <param name="sender">Origen del evento.</param>
		/// <param name="e">Argumentos del evento.</param>
		private void VisualizarImagenes_Load(object sender, EventArgs e)
		{
			try
			{
				string sRutaArchivo = ((GeneraPDF) Owner).RutaArchivoBinario;

				if (File.Exists(sRutaArchivo))
				{
					// Construyendo un objeto de imagen a partir de un archivo en físico.
					Image imgLineaDeCaptura = Image.FromFile(sRutaArchivo);
					this.KivPngLC.Scrollbars = true;
					this.KivPngLC.ImagePath = sRutaArchivo;
					this.KivPngLC.Image = (Bitmap) imgLineaDeCaptura;
					this.KivPngLC.Width = 740;
					this.KivPngLC.Height = 267;
					this.KivPngLC.Zoom = 85;
					this.KivPngLC.Refresh();
					this.KivPngLC.Visible = true;
				}
				else
				{
					MessageBox.Show("El archivo de imagen que intenta visualizar no existe o fue eliminado.", "Sin archivo existente", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					this.BtnCerrar_Click(sender, e);
					return;
				}
			}
			catch (IOException)
			{
				MessageBox.Show("Ocurrió un error al intentar visualizar la Línea de Captura a través de un archivo de imagen.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				this.BtnCerrar_Click(sender, e);
				return;
			}
		}

		/// <summary>
		/// Cierra el formulario.
		/// </summary>
		/// <param name="sender">Origen del evento.</param>
		/// <param name="e">Argumentos del evento.</param>
		private void BtnCerrar_Click(object sender, EventArgs e)
		{
			if (this.KivPngLC.Image != null)
			{
				this.KivPngLC.Image = null;
				this.KivPngLC.Image.Dispose();
			}
			this.Hide();
		}
	}
}