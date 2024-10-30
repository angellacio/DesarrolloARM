using System.Drawing;
using System.Windows.Forms;

namespace TestMotor
{
	partial class GeneraPDF
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
			this.BtnSalir = new System.Windows.Forms.Button();
			this.TcMotorTraductor = new System.Windows.Forms.TabControl();
			this.TpGeneraPdf = new System.Windows.Forms.TabPage();
			this.GbGenerarPdfVisualizar = new System.Windows.Forms.GroupBox();
			this.LnkPdfFormato = new System.Windows.Forms.LinkLabel();
			this.TxtLogPdf = new System.Windows.Forms.TextBox();
			this.GbGenerarPdf = new System.Windows.Forms.GroupBox();
			this.BtnLimpiarPdfServImp = new System.Windows.Forms.Button();
			this.LblFormatoPdf = new System.Windows.Forms.Label();
			this.CbFormatosPdf = new System.Windows.Forms.ComboBox();
			this.LblFolioPdf = new System.Windows.Forms.Label();
			this.TxtFolioPdf = new System.Windows.Forms.TextBox();
			this.BtnGenerarPdfServImp = new System.Windows.Forms.Button();
			this.TpGeneraPng = new System.Windows.Forms.TabPage();
			this.GbGenerarPngVisualizar = new System.Windows.Forms.GroupBox();
			this.KivPngLC = new KaiwaProjects.KpImageViewer();
			this.TxtLogPng = new System.Windows.Forms.TextBox();
			this.GbGenerarPng = new System.Windows.Forms.GroupBox();
			this.BtnLimpiarPngServImp = new System.Windows.Forms.Button();
			this.LblFormatoPng = new System.Windows.Forms.Label();
			this.CbFormatosPng = new System.Windows.Forms.ComboBox();
			this.LblFolioPng = new System.Windows.Forms.Label();
			this.TxtFolioPng = new System.Windows.Forms.TextBox();
			this.BtnGeneraPngServImp = new System.Windows.Forms.Button();
			this.TpMotorTraductor = new System.Windows.Forms.TabPage();
			this.GbMT = new System.Windows.Forms.GroupBox();
			this.GbMTGenera = new System.Windows.Forms.GroupBox();
			this.RbArchivoPng = new System.Windows.Forms.RadioButton();
			this.RbArchivoPdf = new System.Windows.Forms.RadioButton();
			this.TxtLogMT = new System.Windows.Forms.TextBox();
			this.LnkArchivoMT = new System.Windows.Forms.LinkLabel();
			this.BtnEnviarMensajeMT = new System.Windows.Forms.Button();
			this.TxtMensajeEntradaMT = new System.Windows.Forms.TextBox();
			this.LblMensajeEntradaMT = new System.Windows.Forms.Label();
			this.LblIdAplicacionMT = new System.Windows.Forms.Label();
			this.LblIdDocumentoMT = new System.Windows.Forms.Label();
			this.TxtIdAplicacionMT = new System.Windows.Forms.TextBox();
			this.TxtIdDocumentoMT = new System.Windows.Forms.TextBox();
			this.BtnLimpiarMT = new System.Windows.Forms.Button();
			this.BtnAbrirMensajeMT = new System.Windows.Forms.Button();
			this.TpPdfLineaCaptura = new System.Windows.Forms.TabPage();
			this.GbPdfLCVisualizar = new System.Windows.Forms.GroupBox();
			this.LnkPdfLC = new System.Windows.Forms.LinkLabel();
			this.TxtLogLC = new System.Windows.Forms.TextBox();
			this.GbPdfLC = new System.Windows.Forms.GroupBox();
			this.BtnLimpiarLineaCaptura = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.TxtLineaCaptura = new System.Windows.Forms.TextBox();
			this.BtnEnviarLineaCaptura = new System.Windows.Forms.Button();
			this.TcMotorTraductor.SuspendLayout();
			this.TpGeneraPdf.SuspendLayout();
			this.GbGenerarPdfVisualizar.SuspendLayout();
			this.GbGenerarPdf.SuspendLayout();
			this.TpGeneraPng.SuspendLayout();
			this.GbGenerarPngVisualizar.SuspendLayout();
			this.GbGenerarPng.SuspendLayout();
			this.TpMotorTraductor.SuspendLayout();
			this.GbMT.SuspendLayout();
			this.GbMTGenera.SuspendLayout();
			this.TpPdfLineaCaptura.SuspendLayout();
			this.GbPdfLCVisualizar.SuspendLayout();
			this.GbPdfLC.SuspendLayout();
			this.SuspendLayout();
			// 
			// BtnSalir
			// 
			this.BtnSalir.Location = new System.Drawing.Point(6, 494);
			this.BtnSalir.Name = "BtnSalir";
			this.BtnSalir.Size = new System.Drawing.Size(79, 23);
			this.BtnSalir.TabIndex = 43;
			this.BtnSalir.Text = "&Salir";
			this.BtnSalir.UseVisualStyleBackColor = true;
			this.BtnSalir.Click += new System.EventHandler(this.BtnSalir_Click);
			// 
			// TcMotorTraductor
			// 
			this.TcMotorTraductor.Controls.Add(this.TpGeneraPdf);
			this.TcMotorTraductor.Controls.Add(this.TpGeneraPng);
			this.TcMotorTraductor.Controls.Add(this.TpMotorTraductor);
			this.TcMotorTraductor.Controls.Add(this.TpPdfLineaCaptura);
			this.TcMotorTraductor.Location = new System.Drawing.Point(6, 5);
			this.TcMotorTraductor.Name = "TcMotorTraductor";
			this.TcMotorTraductor.SelectedIndex = 0;
			this.TcMotorTraductor.Size = new System.Drawing.Size(797, 483);
			this.TcMotorTraductor.TabIndex = 0;
			// 
			// TpGeneraPdf
			// 
			this.TpGeneraPdf.Controls.Add(this.GbGenerarPdfVisualizar);
			this.TpGeneraPdf.Controls.Add(this.GbGenerarPdf);
			this.TpGeneraPdf.Location = new System.Drawing.Point(4, 22);
			this.TpGeneraPdf.Name = "TpGeneraPdf";
			this.TpGeneraPdf.Padding = new System.Windows.Forms.Padding(3);
			this.TpGeneraPdf.Size = new System.Drawing.Size(789, 457);
			this.TpGeneraPdf.TabIndex = 0;
			this.TpGeneraPdf.Text = "Generador PDF";
			this.TpGeneraPdf.UseVisualStyleBackColor = true;
			// 
			// GbGenerarPdfVisualizar
			// 
			this.GbGenerarPdfVisualizar.Controls.Add(this.LnkPdfFormato);
			this.GbGenerarPdfVisualizar.Controls.Add(this.TxtLogPdf);
			this.GbGenerarPdfVisualizar.Location = new System.Drawing.Point(13, 131);
			this.GbGenerarPdfVisualizar.Name = "GbGenerarPdfVisualizar";
			this.GbGenerarPdfVisualizar.Size = new System.Drawing.Size(763, 310);
			this.GbGenerarPdfVisualizar.TabIndex = 8;
			this.GbGenerarPdfVisualizar.TabStop = false;
			this.GbGenerarPdfVisualizar.Text = "Visualizar contenido";
			this.GbGenerarPdfVisualizar.Visible = false;
			// 
			// LnkPdfFormato
			// 
			this.LnkPdfFormato.AutoSize = true;
			this.LnkPdfFormato.Location = new System.Drawing.Point(17, 15);
			this.LnkPdfFormato.Name = "LnkPdfFormato";
			this.LnkPdfFormato.Size = new System.Drawing.Size(16, 13);
			this.LnkPdfFormato.TabIndex = 9;
			this.LnkPdfFormato.TabStop = true;
			this.LnkPdfFormato.Text = "...";
			this.LnkPdfFormato.Visible = false;
			this.LnkPdfFormato.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkPdfFormato_LinkClicked);
			// 
			// TxtLogPdf
			// 
			this.TxtLogPdf.Location = new System.Drawing.Point(16, 33);
			this.TxtLogPdf.Multiline = true;
			this.TxtLogPdf.Name = "TxtLogPdf";
			this.TxtLogPdf.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.TxtLogPdf.Size = new System.Drawing.Size(730, 267);
			this.TxtLogPdf.TabIndex = 10;
			this.TxtLogPdf.Visible = false;
			// 
			// GbGenerarPdf
			// 
			this.GbGenerarPdf.Controls.Add(this.BtnLimpiarPdfServImp);
			this.GbGenerarPdf.Controls.Add(this.LblFormatoPdf);
			this.GbGenerarPdf.Controls.Add(this.CbFormatosPdf);
			this.GbGenerarPdf.Controls.Add(this.LblFolioPdf);
			this.GbGenerarPdf.Controls.Add(this.TxtFolioPdf);
			this.GbGenerarPdf.Controls.Add(this.BtnGenerarPdfServImp);
			this.GbGenerarPdf.Location = new System.Drawing.Point(13, 10);
			this.GbGenerarPdf.Name = "GbGenerarPdf";
			this.GbGenerarPdf.Size = new System.Drawing.Size(763, 115);
			this.GbGenerarPdf.TabIndex = 1;
			this.GbGenerarPdf.TabStop = false;
			this.GbGenerarPdf.Text = "Capturar Datos";
			// 
			// BtnLimpiarPdfServImp
			// 
			this.BtnLimpiarPdfServImp.Location = new System.Drawing.Point(178, 78);
			this.BtnLimpiarPdfServImp.Name = "BtnLimpiarPdfServImp";
			this.BtnLimpiarPdfServImp.Size = new System.Drawing.Size(97, 23);
			this.BtnLimpiarPdfServImp.TabIndex = 7;
			this.BtnLimpiarPdfServImp.Text = "Li&mpiar";
			this.BtnLimpiarPdfServImp.UseVisualStyleBackColor = true;
			this.BtnLimpiarPdfServImp.Click += new System.EventHandler(this.BtnLimpiarPdfServImp_Click);
			// 
			// LblFormatoPdf
			// 
			this.LblFormatoPdf.AutoSize = true;
			this.LblFormatoPdf.Location = new System.Drawing.Point(13, 24);
			this.LblFormatoPdf.Name = "LblFormatoPdf";
			this.LblFormatoPdf.Size = new System.Drawing.Size(48, 13);
			this.LblFormatoPdf.TabIndex = 2;
			this.LblFormatoPdf.Text = "F&ormato:";
			this.LblFormatoPdf.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// CbFormatosPdf
			// 
			this.CbFormatosPdf.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CbFormatosPdf.FormattingEnabled = true;
			this.CbFormatosPdf.Location = new System.Drawing.Point(64, 24);
			this.CbFormatosPdf.Name = "CbFormatosPdf";
			this.CbFormatosPdf.Size = new System.Drawing.Size(297, 21);
			this.CbFormatosPdf.TabIndex = 3;
			// 
			// LblFolioPdf
			// 
			this.LblFolioPdf.AutoSize = true;
			this.LblFolioPdf.Location = new System.Drawing.Point(13, 51);
			this.LblFolioPdf.Name = "LblFolioPdf";
			this.LblFolioPdf.Size = new System.Drawing.Size(32, 13);
			this.LblFolioPdf.TabIndex = 4;
			this.LblFolioPdf.Text = "&Folio:";
			// 
			// TxtFolioPdf
			// 
			this.TxtFolioPdf.Location = new System.Drawing.Point(64, 51);
			this.TxtFolioPdf.Name = "TxtFolioPdf";
			this.TxtFolioPdf.Size = new System.Drawing.Size(297, 20);
			this.TxtFolioPdf.TabIndex = 5;
			// 
			// BtnGenerarPdfServImp
			// 
			this.BtnGenerarPdfServImp.Location = new System.Drawing.Point(64, 78);
			this.BtnGenerarPdfServImp.Name = "BtnGenerarPdfServImp";
			this.BtnGenerarPdfServImp.Size = new System.Drawing.Size(97, 23);
			this.BtnGenerarPdfServImp.TabIndex = 6;
			this.BtnGenerarPdfServImp.Text = "Generar P&DF";
			this.BtnGenerarPdfServImp.UseVisualStyleBackColor = true;
			this.BtnGenerarPdfServImp.Click += new System.EventHandler(this.BtnGenerarPdfServImp_Click);
			// 
			// TpGeneraPng
			// 
			this.TpGeneraPng.Controls.Add(this.GbGenerarPngVisualizar);
			this.TpGeneraPng.Controls.Add(this.GbGenerarPng);
			this.TpGeneraPng.Location = new System.Drawing.Point(4, 22);
			this.TpGeneraPng.Name = "TpGeneraPng";
			this.TpGeneraPng.Size = new System.Drawing.Size(789, 457);
			this.TpGeneraPng.TabIndex = 3;
			this.TpGeneraPng.Text = "Generador PNG";
			this.TpGeneraPng.UseVisualStyleBackColor = true;
			// 
			// GbGenerarPngVisualizar
			// 
			this.GbGenerarPngVisualizar.Controls.Add(this.KivPngLC);
			this.GbGenerarPngVisualizar.Controls.Add(this.TxtLogPng);
			this.GbGenerarPngVisualizar.Location = new System.Drawing.Point(13, 131);
			this.GbGenerarPngVisualizar.Name = "GbGenerarPngVisualizar";
			this.GbGenerarPngVisualizar.Size = new System.Drawing.Size(763, 310);
			this.GbGenerarPngVisualizar.TabIndex = 18;
			this.GbGenerarPngVisualizar.TabStop = false;
			this.GbGenerarPngVisualizar.Text = "Visualizar imagen";
			this.GbGenerarPngVisualizar.Visible = false;
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
			this.KivPngLC.Location = new System.Drawing.Point(16, 33);
			this.KivPngLC.MenuColor = System.Drawing.Color.LightSteelBlue;
			this.KivPngLC.MenuPanelColor = System.Drawing.Color.LightSteelBlue;
			this.KivPngLC.MinimumSize = new System.Drawing.Size(454, 157);
			this.KivPngLC.Name = "KivPngLC";
			this.KivPngLC.NavigationPanelColor = System.Drawing.Color.LightSteelBlue;
			this.KivPngLC.NavigationTextColor = System.Drawing.SystemColors.ButtonHighlight;
			this.KivPngLC.PreviewButton = false;
			this.KivPngLC.PreviewPanelColor = System.Drawing.Color.LightSteelBlue;
			this.KivPngLC.PreviewText = "Previsualizar";
			this.KivPngLC.PreviewTextColor = System.Drawing.SystemColors.ButtonHighlight;
			this.KivPngLC.Rotation = 0;
			this.KivPngLC.Scrollbars = true;
			this.KivPngLC.ShowPreview = true;
			this.KivPngLC.Size = new System.Drawing.Size(740, 267);
			this.KivPngLC.TabIndex = 19;
			this.KivPngLC.TextColor = System.Drawing.SystemColors.ButtonHighlight;
			this.KivPngLC.Zoom = 100D;
			// 
			// TxtLogPng
			// 
			this.TxtLogPng.Location = new System.Drawing.Point(16, 33);
			this.TxtLogPng.Multiline = true;
			this.TxtLogPng.Name = "TxtLogPng";
			this.TxtLogPng.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.TxtLogPng.Size = new System.Drawing.Size(730, 267);
			this.TxtLogPng.TabIndex = 20;
			this.TxtLogPng.Visible = false;
			// 
			// GbGenerarPng
			// 
			this.GbGenerarPng.Controls.Add(this.BtnLimpiarPngServImp);
			this.GbGenerarPng.Controls.Add(this.LblFormatoPng);
			this.GbGenerarPng.Controls.Add(this.CbFormatosPng);
			this.GbGenerarPng.Controls.Add(this.LblFolioPng);
			this.GbGenerarPng.Controls.Add(this.TxtFolioPng);
			this.GbGenerarPng.Controls.Add(this.BtnGeneraPngServImp);
			this.GbGenerarPng.Location = new System.Drawing.Point(13, 10);
			this.GbGenerarPng.Name = "GbGenerarPng";
			this.GbGenerarPng.Size = new System.Drawing.Size(763, 115);
			this.GbGenerarPng.TabIndex = 11;
			this.GbGenerarPng.TabStop = false;
			this.GbGenerarPng.Text = "Capturar Datos ";
			// 
			// BtnLimpiarPngServImp
			// 
			this.BtnLimpiarPngServImp.Location = new System.Drawing.Point(178, 78);
			this.BtnLimpiarPngServImp.Name = "BtnLimpiarPngServImp";
			this.BtnLimpiarPngServImp.Size = new System.Drawing.Size(97, 23);
			this.BtnLimpiarPngServImp.TabIndex = 17;
			this.BtnLimpiarPngServImp.Text = "Li&mpiar";
			this.BtnLimpiarPngServImp.UseVisualStyleBackColor = true;
			this.BtnLimpiarPngServImp.Click += new System.EventHandler(this.BtnLimpiarPngServImp_Click);
			// 
			// LblFormatoPng
			// 
			this.LblFormatoPng.AutoSize = true;
			this.LblFormatoPng.Location = new System.Drawing.Point(13, 24);
			this.LblFormatoPng.Name = "LblFormatoPng";
			this.LblFormatoPng.Size = new System.Drawing.Size(48, 13);
			this.LblFormatoPng.TabIndex = 12;
			this.LblFormatoPng.Text = "For&mato:";
			this.LblFormatoPng.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// CbFormatosPng
			// 
			this.CbFormatosPng.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CbFormatosPng.FormattingEnabled = true;
			this.CbFormatosPng.Location = new System.Drawing.Point(64, 24);
			this.CbFormatosPng.Name = "CbFormatosPng";
			this.CbFormatosPng.Size = new System.Drawing.Size(297, 21);
			this.CbFormatosPng.TabIndex = 13;
			// 
			// LblFolioPng
			// 
			this.LblFolioPng.AutoSize = true;
			this.LblFolioPng.Location = new System.Drawing.Point(13, 51);
			this.LblFolioPng.Name = "LblFolioPng";
			this.LblFolioPng.Size = new System.Drawing.Size(32, 13);
			this.LblFolioPng.TabIndex = 14;
			this.LblFolioPng.Text = "Fo&lio:";
			// 
			// TxtFolioPng
			// 
			this.TxtFolioPng.Location = new System.Drawing.Point(64, 51);
			this.TxtFolioPng.Name = "TxtFolioPng";
			this.TxtFolioPng.Size = new System.Drawing.Size(297, 20);
			this.TxtFolioPng.TabIndex = 15;
			// 
			// BtnGeneraPngServImp
			// 
			this.BtnGeneraPngServImp.Location = new System.Drawing.Point(64, 78);
			this.BtnGeneraPngServImp.Name = "BtnGeneraPngServImp";
			this.BtnGeneraPngServImp.Size = new System.Drawing.Size(97, 23);
			this.BtnGeneraPngServImp.TabIndex = 16;
			this.BtnGeneraPngServImp.Text = "Generar P&NG";
			this.BtnGeneraPngServImp.UseVisualStyleBackColor = true;
			this.BtnGeneraPngServImp.Click += new System.EventHandler(this.BtnGeneraPngServImp_Click);
			// 
			// TpMotorTraductor
			// 
			this.TpMotorTraductor.Controls.Add(this.GbMT);
			this.TpMotorTraductor.Location = new System.Drawing.Point(4, 22);
			this.TpMotorTraductor.Name = "TpMotorTraductor";
			this.TpMotorTraductor.Padding = new System.Windows.Forms.Padding(3);
			this.TpMotorTraductor.Size = new System.Drawing.Size(789, 457);
			this.TpMotorTraductor.TabIndex = 1;
			this.TpMotorTraductor.Text = "Motor Traductor";
			this.TpMotorTraductor.UseVisualStyleBackColor = true;
			// 
			// GbMT
			// 
			this.GbMT.Controls.Add(this.GbMTGenera);
			this.GbMT.Controls.Add(this.TxtLogMT);
			this.GbMT.Controls.Add(this.LnkArchivoMT);
			this.GbMT.Controls.Add(this.BtnEnviarMensajeMT);
			this.GbMT.Controls.Add(this.TxtMensajeEntradaMT);
			this.GbMT.Controls.Add(this.LblMensajeEntradaMT);
			this.GbMT.Controls.Add(this.LblIdAplicacionMT);
			this.GbMT.Controls.Add(this.LblIdDocumentoMT);
			this.GbMT.Controls.Add(this.TxtIdAplicacionMT);
			this.GbMT.Controls.Add(this.TxtIdDocumentoMT);
			this.GbMT.Controls.Add(this.BtnLimpiarMT);
			this.GbMT.Controls.Add(this.BtnAbrirMensajeMT);
			this.GbMT.Location = new System.Drawing.Point(13, 10);
			this.GbMT.Name = "GbMT";
			this.GbMT.Size = new System.Drawing.Size(763, 436);
			this.GbMT.TabIndex = 20;
			this.GbMT.TabStop = false;
			this.GbMT.Text = "Capturar Datos ";
			// 
			// GbMTGenera
			// 
			this.GbMTGenera.Controls.Add(this.RbArchivoPng);
			this.GbMTGenera.Controls.Add(this.RbArchivoPdf);
			this.GbMTGenera.Location = new System.Drawing.Point(208, 14);
			this.GbMTGenera.Name = "GbMTGenera";
			this.GbMTGenera.Size = new System.Drawing.Size(280, 58);
			this.GbMTGenera.TabIndex = 25;
			this.GbMTGenera.TabStop = false;
			this.GbMTGenera.Text = "Salida por:";
			// 
			// RbArchivoPng
			// 
			this.RbArchivoPng.AutoSize = true;
			this.RbArchivoPng.Location = new System.Drawing.Point(143, 25);
			this.RbArchivoPng.Name = "RbArchivoPng";
			this.RbArchivoPng.Size = new System.Drawing.Size(113, 17);
			this.RbArchivoPng.TabIndex = 27;
			this.RbArchivoPng.TabStop = true;
			this.RbArchivoPng.Text = "Archivo de imagen";
			this.RbArchivoPng.UseVisualStyleBackColor = true;
			// 
			// RbArchivoPdf
			// 
			this.RbArchivoPdf.AutoSize = true;
			this.RbArchivoPdf.Location = new System.Drawing.Point(16, 25);
			this.RbArchivoPdf.Name = "RbArchivoPdf";
			this.RbArchivoPdf.Size = new System.Drawing.Size(85, 17);
			this.RbArchivoPdf.TabIndex = 26;
			this.RbArchivoPdf.TabStop = true;
			this.RbArchivoPdf.Text = "Archivo PDF";
			this.RbArchivoPdf.UseVisualStyleBackColor = true;
			// 
			// TxtLogMT
			// 
			this.TxtLogMT.Location = new System.Drawing.Point(16, 348);
			this.TxtLogMT.Multiline = true;
			this.TxtLogMT.Name = "TxtLogMT";
			this.TxtLogMT.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.TxtLogMT.Size = new System.Drawing.Size(730, 79);
			this.TxtLogMT.TabIndex = 34;
			this.TxtLogMT.Visible = false;
			// 
			// LnkArchivoMT
			// 
			this.LnkArchivoMT.AutoSize = true;
			this.LnkArchivoMT.Location = new System.Drawing.Point(13, 327);
			this.LnkArchivoMT.Name = "LnkArchivoMT";
			this.LnkArchivoMT.Size = new System.Drawing.Size(16, 13);
			this.LnkArchivoMT.TabIndex = 33;
			this.LnkArchivoMT.TabStop = true;
			this.LnkArchivoMT.Text = "...";
			this.LnkArchivoMT.Visible = false;
			this.LnkArchivoMT.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkArchivoMT_LinkClicked);
			// 
			// BtnEnviarMensajeMT
			// 
			this.BtnEnviarMensajeMT.Enabled = false;
			this.BtnEnviarMensajeMT.Location = new System.Drawing.Point(16, 298);
			this.BtnEnviarMensajeMT.Name = "BtnEnviarMensajeMT";
			this.BtnEnviarMensajeMT.Size = new System.Drawing.Size(75, 23);
			this.BtnEnviarMensajeMT.TabIndex = 31;
			this.BtnEnviarMensajeMT.Text = "&Enviar";
			this.BtnEnviarMensajeMT.UseVisualStyleBackColor = true;
			this.BtnEnviarMensajeMT.Click += new System.EventHandler(this.BtnEnviarMensajeMT_Click);
			// 
			// TxtMensajeEntradaMT
			// 
			this.TxtMensajeEntradaMT.Location = new System.Drawing.Point(16, 92);
			this.TxtMensajeEntradaMT.MaxLength = 200000;
			this.TxtMensajeEntradaMT.Multiline = true;
			this.TxtMensajeEntradaMT.Name = "TxtMensajeEntradaMT";
			this.TxtMensajeEntradaMT.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.TxtMensajeEntradaMT.Size = new System.Drawing.Size(730, 200);
			this.TxtMensajeEntradaMT.TabIndex = 30;
			this.TxtMensajeEntradaMT.TextChanged += new System.EventHandler(this.TxtMensajeEntradaMT_TextChanged);
			// 
			// LblMensajeEntradaMT
			// 
			this.LblMensajeEntradaMT.AutoSize = true;
			this.LblMensajeEntradaMT.Location = new System.Drawing.Point(13, 77);
			this.LblMensajeEntradaMT.Name = "LblMensajeEntradaMT";
			this.LblMensajeEntradaMT.Size = new System.Drawing.Size(72, 13);
			this.LblMensajeEntradaMT.TabIndex = 29;
			this.LblMensajeEntradaMT.Text = "Entrada XML:";
			// 
			// LblIdAplicacionMT
			// 
			this.LblIdAplicacionMT.AutoSize = true;
			this.LblIdAplicacionMT.Location = new System.Drawing.Point(13, 24);
			this.LblIdAplicacionMT.Name = "LblIdAplicacionMT";
			this.LblIdAplicacionMT.Size = new System.Drawing.Size(73, 13);
			this.LblIdAplicacionMT.TabIndex = 21;
			this.LblIdAplicacionMT.Text = "ID &Aplicación:";
			// 
			// LblIdDocumentoMT
			// 
			this.LblIdDocumentoMT.AutoSize = true;
			this.LblIdDocumentoMT.Location = new System.Drawing.Point(13, 55);
			this.LblIdDocumentoMT.Name = "LblIdDocumentoMT";
			this.LblIdDocumentoMT.Size = new System.Drawing.Size(79, 13);
			this.LblIdDocumentoMT.TabIndex = 23;
			this.LblIdDocumentoMT.Text = "ID &Documento:";
			// 
			// TxtIdAplicacionMT
			// 
			this.TxtIdAplicacionMT.Location = new System.Drawing.Point(97, 21);
			this.TxtIdAplicacionMT.Name = "TxtIdAplicacionMT";
			this.TxtIdAplicacionMT.Size = new System.Drawing.Size(97, 20);
			this.TxtIdAplicacionMT.TabIndex = 22;
			// 
			// TxtIdDocumentoMT
			// 
			this.TxtIdDocumentoMT.Location = new System.Drawing.Point(97, 52);
			this.TxtIdDocumentoMT.Name = "TxtIdDocumentoMT";
			this.TxtIdDocumentoMT.Size = new System.Drawing.Size(97, 20);
			this.TxtIdDocumentoMT.TabIndex = 24;
			// 
			// BtnLimpiarMT
			// 
			this.BtnLimpiarMT.Location = new System.Drawing.Point(97, 298);
			this.BtnLimpiarMT.Name = "BtnLimpiarMT";
			this.BtnLimpiarMT.Size = new System.Drawing.Size(97, 23);
			this.BtnLimpiarMT.TabIndex = 32;
			this.BtnLimpiarMT.Text = "Li&mpiar";
			this.BtnLimpiarMT.UseVisualStyleBackColor = true;
			this.BtnLimpiarMT.Click += new System.EventHandler(this.BtnLimpiarMT_Click);
			// 
			// BtnAbrirMensajeMT
			// 
			this.BtnAbrirMensajeMT.Location = new System.Drawing.Point(649, 63);
			this.BtnAbrirMensajeMT.Name = "BtnAbrirMensajeMT";
			this.BtnAbrirMensajeMT.Size = new System.Drawing.Size(97, 23);
			this.BtnAbrirMensajeMT.TabIndex = 28;
			this.BtnAbrirMensajeMT.Text = "A&brir mensaje";
			this.BtnAbrirMensajeMT.UseVisualStyleBackColor = true;
			this.BtnAbrirMensajeMT.Click += new System.EventHandler(this.BtnAbrirMensajeMT_Click);
			// 
			// TpPdfLineaCaptura
			// 
			this.TpPdfLineaCaptura.Controls.Add(this.GbPdfLCVisualizar);
			this.TpPdfLineaCaptura.Controls.Add(this.GbPdfLC);
			this.TpPdfLineaCaptura.Location = new System.Drawing.Point(4, 22);
			this.TpPdfLineaCaptura.Name = "TpPdfLineaCaptura";
			this.TpPdfLineaCaptura.Padding = new System.Windows.Forms.Padding(3);
			this.TpPdfLineaCaptura.Size = new System.Drawing.Size(789, 457);
			this.TpPdfLineaCaptura.TabIndex = 2;
			this.TpPdfLineaCaptura.Text = "PDF-LineaCaptura";
			this.TpPdfLineaCaptura.UseVisualStyleBackColor = true;
			// 
			// GbPdfLCVisualizar
			// 
			this.GbPdfLCVisualizar.Controls.Add(this.LnkPdfLC);
			this.GbPdfLCVisualizar.Controls.Add(this.TxtLogLC);
			this.GbPdfLCVisualizar.Location = new System.Drawing.Point(13, 131);
			this.GbPdfLCVisualizar.Name = "GbPdfLCVisualizar";
			this.GbPdfLCVisualizar.Size = new System.Drawing.Size(763, 310);
			this.GbPdfLCVisualizar.TabIndex = 40;
			this.GbPdfLCVisualizar.TabStop = false;
			this.GbPdfLCVisualizar.Text = "Visualizar contenido";
			// 
			// LnkPdfLC
			// 
			this.LnkPdfLC.AutoSize = true;
			this.LnkPdfLC.Location = new System.Drawing.Point(17, 15);
			this.LnkPdfLC.Name = "LnkPdfLC";
			this.LnkPdfLC.Size = new System.Drawing.Size(16, 13);
			this.LnkPdfLC.TabIndex = 41;
			this.LnkPdfLC.TabStop = true;
			this.LnkPdfLC.Text = "...";
			this.LnkPdfLC.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkPdfLC_LinkClicked);
			// 
			// TxtLogLC
			// 
			this.TxtLogLC.Location = new System.Drawing.Point(16, 33);
			this.TxtLogLC.Multiline = true;
			this.TxtLogLC.Name = "TxtLogLC";
			this.TxtLogLC.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.TxtLogLC.Size = new System.Drawing.Size(730, 267);
			this.TxtLogLC.TabIndex = 42;
			// 
			// GbPdfLC
			// 
			this.GbPdfLC.Controls.Add(this.BtnLimpiarLineaCaptura);
			this.GbPdfLC.Controls.Add(this.label3);
			this.GbPdfLC.Controls.Add(this.TxtLineaCaptura);
			this.GbPdfLC.Controls.Add(this.BtnEnviarLineaCaptura);
			this.GbPdfLC.Location = new System.Drawing.Point(13, 10);
			this.GbPdfLC.Name = "GbPdfLC";
			this.GbPdfLC.Size = new System.Drawing.Size(763, 115);
			this.GbPdfLC.TabIndex = 35;
			this.GbPdfLC.TabStop = false;
			this.GbPdfLC.Text = "Capturar Datos";
			// 
			// BtnLimpiarLineaCaptura
			// 
			this.BtnLimpiarLineaCaptura.Location = new System.Drawing.Point(223, 52);
			this.BtnLimpiarLineaCaptura.Name = "BtnLimpiarLineaCaptura";
			this.BtnLimpiarLineaCaptura.Size = new System.Drawing.Size(97, 23);
			this.BtnLimpiarLineaCaptura.TabIndex = 39;
			this.BtnLimpiarLineaCaptura.Text = "Li&mpiar";
			this.BtnLimpiarLineaCaptura.UseVisualStyleBackColor = true;
			this.BtnLimpiarLineaCaptura.Click += new System.EventHandler(this.BtnLimpiarLineaCaptura_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(13, 24);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(93, 13);
			this.label3.TabIndex = 36;
			this.label3.Text = "Línea de Captura:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// TxtLineaCaptura
			// 
			this.TxtLineaCaptura.Location = new System.Drawing.Point(108, 24);
			this.TxtLineaCaptura.Name = "TxtLineaCaptura";
			this.TxtLineaCaptura.Size = new System.Drawing.Size(297, 20);
			this.TxtLineaCaptura.TabIndex = 37;
			// 
			// BtnEnviarLineaCaptura
			// 
			this.BtnEnviarLineaCaptura.Location = new System.Drawing.Point(108, 52);
			this.BtnEnviarLineaCaptura.Name = "BtnEnviarLineaCaptura";
			this.BtnEnviarLineaCaptura.Size = new System.Drawing.Size(97, 23);
			this.BtnEnviarLineaCaptura.TabIndex = 38;
			this.BtnEnviarLineaCaptura.Text = "&Enviar";
			this.BtnEnviarLineaCaptura.UseVisualStyleBackColor = true;
			this.BtnEnviarLineaCaptura.Click += new System.EventHandler(this.BtnEnviarLineaCaptura_Click);
			// 
			// GeneradorLC
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.ClientSize = new System.Drawing.Size(806, 521);
			this.Controls.Add(this.BtnSalir);
			this.Controls.Add(this.TcMotorTraductor);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "GeneradorLC";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Generador de Líneas de Captura...";
			this.Load += new System.EventHandler(this.GeneradorLC_Load);
			this.TcMotorTraductor.ResumeLayout(false);
			this.TpGeneraPdf.ResumeLayout(false);
			this.GbGenerarPdfVisualizar.ResumeLayout(false);
			this.GbGenerarPdfVisualizar.PerformLayout();
			this.GbGenerarPdf.ResumeLayout(false);
			this.GbGenerarPdf.PerformLayout();
			this.TpGeneraPng.ResumeLayout(false);
			this.GbGenerarPngVisualizar.ResumeLayout(false);
			this.GbGenerarPngVisualizar.PerformLayout();
			this.GbGenerarPng.ResumeLayout(false);
			this.GbGenerarPng.PerformLayout();
			this.TpMotorTraductor.ResumeLayout(false);
			this.GbMT.ResumeLayout(false);
			this.GbMT.PerformLayout();
			this.GbMTGenera.ResumeLayout(false);
			this.GbMTGenera.PerformLayout();
			this.TpPdfLineaCaptura.ResumeLayout(false);
			this.GbPdfLCVisualizar.ResumeLayout(false);
			this.GbPdfLCVisualizar.PerformLayout();
			this.GbPdfLC.ResumeLayout(false);
			this.GbPdfLC.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button BtnSalir;
		private System.Windows.Forms.TabControl TcMotorTraductor;
		private System.Windows.Forms.TabPage TpGeneraPdf;
		private System.Windows.Forms.GroupBox GbGenerarPdfVisualizar;
		private System.Windows.Forms.TextBox TxtLogPdf;
		private System.Windows.Forms.GroupBox GbGenerarPdf;
		private System.Windows.Forms.Label LblFormatoPdf;
		private System.Windows.Forms.ComboBox CbFormatosPdf;
		private System.Windows.Forms.Label LblFolioPdf;
		private System.Windows.Forms.TextBox TxtFolioPdf;
		private System.Windows.Forms.Button BtnGenerarPdfServImp;
		private System.Windows.Forms.TabPage TpGeneraPng;
		private System.Windows.Forms.GroupBox GbGenerarPngVisualizar;
		private System.Windows.Forms.GroupBox GbGenerarPng;
		private System.Windows.Forms.Label LblFormatoPng;
		private System.Windows.Forms.ComboBox CbFormatosPng;
		private System.Windows.Forms.Label LblFolioPng;
		private System.Windows.Forms.TextBox TxtFolioPng;
		private System.Windows.Forms.Button BtnGeneraPngServImp;
		private System.Windows.Forms.TabPage TpMotorTraductor;
		private System.Windows.Forms.TabPage TpPdfLineaCaptura;
		private System.Windows.Forms.GroupBox GbPdfLC;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox TxtLineaCaptura;
		private System.Windows.Forms.Button BtnEnviarLineaCaptura;
		private System.Windows.Forms.GroupBox GbPdfLCVisualizar;
		private System.Windows.Forms.TextBox TxtLogLC;
		private System.Windows.Forms.LinkLabel LnkPdfLC;
		private System.Windows.Forms.LinkLabel LnkPdfFormato;
		private System.Windows.Forms.TextBox TxtLogPng;
		private System.Windows.Forms.Button BtnLimpiarPngServImp;
		private System.Windows.Forms.Button BtnLimpiarPdfServImp;
		private System.Windows.Forms.Button BtnLimpiarLineaCaptura;
		private System.Windows.Forms.GroupBox GbMT;
		private System.Windows.Forms.TextBox TxtLogMT;
		private System.Windows.Forms.LinkLabel LnkArchivoMT;
		private System.Windows.Forms.Button BtnEnviarMensajeMT;
		private System.Windows.Forms.TextBox TxtMensajeEntradaMT;
		private System.Windows.Forms.Label LblMensajeEntradaMT;
		private System.Windows.Forms.Label LblIdAplicacionMT;
		private System.Windows.Forms.Label LblIdDocumentoMT;
		private System.Windows.Forms.TextBox TxtIdAplicacionMT;
		private System.Windows.Forms.TextBox TxtIdDocumentoMT;
		private System.Windows.Forms.Button BtnLimpiarMT;
		private System.Windows.Forms.Button BtnAbrirMensajeMT;
		private KaiwaProjects.KpImageViewer KivPngLC;
		private GroupBox GbMTGenera;
		internal RadioButton RbArchivoPng;
		internal RadioButton RbArchivoPdf;
	}
}