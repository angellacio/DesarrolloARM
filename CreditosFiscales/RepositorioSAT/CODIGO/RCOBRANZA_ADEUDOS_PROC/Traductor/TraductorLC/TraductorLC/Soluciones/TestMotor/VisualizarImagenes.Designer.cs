namespace TestMotor
{
	partial class VisualizarImagenes
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.GbGenerarPdf = new System.Windows.Forms.GroupBox();
			this.KivPngLC = new KaiwaProjects.KpImageViewer();
			this.BtnCerrar = new System.Windows.Forms.Button();
			this.GbGenerarPdf.SuspendLayout();
			this.SuspendLayout();
			// 
			// GbGenerarPdf
			// 
			this.GbGenerarPdf.Controls.Add(this.KivPngLC);
			this.GbGenerarPdf.Location = new System.Drawing.Point(12, 12);
			this.GbGenerarPdf.Name = "GbGenerarPdf";
			this.GbGenerarPdf.Size = new System.Drawing.Size(787, 283);
			this.GbGenerarPdf.TabIndex = 2;
			this.GbGenerarPdf.TabStop = false;
			this.GbGenerarPdf.Text = "Línea de Captura";
			// 
			// KivPngLC
			// 
			this.KivPngLC.AllowDrop = true;
			this.KivPngLC.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.KivPngLC.BackgroundColor = System.Drawing.SystemColors.ControlLight;
			this.KivPngLC.ForeColor = System.Drawing.SystemColors.ControlText;
			this.KivPngLC.GifAnimation = false;
			this.KivPngLC.GifFPS = 15D;
			this.KivPngLC.Image = null;
			this.KivPngLC.Location = new System.Drawing.Point(11, 19);
			this.KivPngLC.MenuColor = System.Drawing.Color.LightSteelBlue;
			this.KivPngLC.MenuPanelColor = System.Drawing.Color.LightSteelBlue;
			this.KivPngLC.MinimumSize = new System.Drawing.Size(454, 157);
			this.KivPngLC.Name = "KivPngLC";
			this.KivPngLC.NavigationPanelColor = System.Drawing.Color.LightSteelBlue;
			this.KivPngLC.NavigationTextColor = System.Drawing.SystemColors.ButtonHighlight;
			this.KivPngLC.PreviewButton = true;
			this.KivPngLC.PreviewPanelColor = System.Drawing.Color.LightSteelBlue;
			this.KivPngLC.PreviewText = "Previsualizar";
			this.KivPngLC.PreviewTextColor = System.Drawing.SystemColors.ButtonHighlight;
			this.KivPngLC.Rotation = 0;
			this.KivPngLC.Scrollbars = true;
			this.KivPngLC.ShowPreview = true;
			this.KivPngLC.Size = new System.Drawing.Size(757, 248);
			this.KivPngLC.TabIndex = 20;
			this.KivPngLC.TextColor = System.Drawing.SystemColors.ButtonHighlight;
			this.KivPngLC.Zoom = 100D;
			// 
			// BtnCerrar
			// 
			this.BtnCerrar.Location = new System.Drawing.Point(12, 301);
			this.BtnCerrar.Name = "BtnCerrar";
			this.BtnCerrar.Size = new System.Drawing.Size(79, 23);
			this.BtnCerrar.TabIndex = 20;
			this.BtnCerrar.Text = "&Cerrar";
			this.BtnCerrar.UseVisualStyleBackColor = true;
			this.BtnCerrar.Click += new System.EventHandler(this.BtnCerrar_Click);
			// 
			// VisualizarImagenes
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(810, 332);
			this.Controls.Add(this.BtnCerrar);
			this.Controls.Add(this.GbGenerarPdf);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "VisualizarImagenes";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Visualizador de Imágenes...";
			this.Load += new System.EventHandler(this.VisualizarImagenes_Load);
			this.GbGenerarPdf.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox GbGenerarPdf;
		private KaiwaProjects.KpImageViewer KivPngLC;
		private System.Windows.Forms.Button BtnCerrar;
	}
}