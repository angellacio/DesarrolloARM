namespace ManejoCanciones.uControl
{
    partial class frmReproducirCancion
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReproducirCancion));
            this.wmpMusica = new AxWMPLib.AxWindowsMediaPlayer();
            this.mtbCancion = new XComponent.SliderBar.MACTrackBar();
            this.txtCancion = new System.Windows.Forms.TextBox();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnAdelantar = new System.Windows.Forms.Button();
            this.btnRegresar = new System.Windows.Forms.Button();
            this.mtbVolumen = new XComponent.SliderBar.MACTrackBar();
            this.txtCancionMinutos = new System.Windows.Forms.TextBox();
            this.txtCancionMinutosDuracion = new System.Windows.Forms.TextBox();
            this.btnSilencio = new System.Windows.Forms.Button();
            this.btnRepetir = new System.Windows.Forms.Button();
            this.txtNivelVolumen = new System.Windows.Forms.TextBox();
            this.tmrMusicaProgres = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.wmpMusica)).BeginInit();
            this.SuspendLayout();
            // 
            // wmpMusica
            // 
            this.wmpMusica.Enabled = true;
            this.wmpMusica.Location = new System.Drawing.Point(342, 164);
            this.wmpMusica.Name = "wmpMusica";
            this.wmpMusica.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("wmpMusica.OcxState")));
            this.wmpMusica.Size = new System.Drawing.Size(27, 16);
            this.wmpMusica.TabIndex = 0;
            // 
            // mtbCancion
            // 
            this.mtbCancion.BackColor = System.Drawing.Color.Transparent;
            this.mtbCancion.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.mtbCancion.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtbCancion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(125)))), ((int)(((byte)(123)))));
            this.mtbCancion.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mtbCancion.IndentHeight = 6;
            this.mtbCancion.Location = new System.Drawing.Point(12, 87);
            this.mtbCancion.Maximum = 10;
            this.mtbCancion.Minimum = 0;
            this.mtbCancion.Name = "mtbCancion";
            this.mtbCancion.Size = new System.Drawing.Size(315, 28);
            this.mtbCancion.TabIndex = 1;
            this.mtbCancion.TextTickStyle = System.Windows.Forms.TickStyle.None;
            this.mtbCancion.TickColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(146)))), ((int)(((byte)(148)))));
            this.mtbCancion.TickHeight = 4;
            this.mtbCancion.TickStyle = System.Windows.Forms.TickStyle.None;
            this.mtbCancion.TrackerColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(130)))), ((int)(((byte)(198)))));
            this.mtbCancion.TrackerSize = new System.Drawing.Size(16, 16);
            this.mtbCancion.TrackLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(93)))), ((int)(((byte)(90)))));
            this.mtbCancion.TrackLineHeight = 3;
            this.mtbCancion.TrackLineSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(93)))), ((int)(((byte)(90)))));
            this.mtbCancion.Value = 5;
            this.mtbCancion.ValueChanged += new XComponent.SliderBar.ValueChangedHandler(this.mtbCancion_ValueChanged);
            // 
            // txtCancion
            // 
            this.txtCancion.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtCancion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCancion.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCancion.Location = new System.Drawing.Point(12, 12);
            this.txtCancion.Multiline = true;
            this.txtCancion.Name = "txtCancion";
            this.txtCancion.Size = new System.Drawing.Size(315, 80);
            this.txtCancion.TabIndex = 2;
            this.txtCancion.Text = "Aasdaccd sdasdasd asd\r\nYasdasdasdasda sd asdasd";
            this.txtCancion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.SystemColors.Control;
            this.btnCerrar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnCerrar.FlatAppearance.BorderSize = 0;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.Font = new System.Drawing.Font("Blackadder ITC", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.Location = new System.Drawing.Point(318, -1);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(22, 23);
            this.btnCerrar.TabIndex = 3;
            this.btnCerrar.Text = "X";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.BackColor = System.Drawing.SystemColors.Control;
            this.btnPlay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnPlay.FlatAppearance.BorderSize = 0;
            this.btnPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlay.Font = new System.Drawing.Font("Webdings", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btnPlay.Location = new System.Drawing.Point(176, 121);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(32, 32);
            this.btnPlay.TabIndex = 4;
            this.btnPlay.Text = ";";
            this.btnPlay.UseVisualStyleBackColor = false;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.SystemColors.Control;
            this.btnStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnStop.FlatAppearance.BorderSize = 0;
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.Font = new System.Drawing.Font("Webdings", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btnStop.Location = new System.Drawing.Point(138, 121);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(32, 32);
            this.btnStop.TabIndex = 5;
            this.btnStop.Text = "<";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnAdelantar
            // 
            this.btnAdelantar.BackColor = System.Drawing.SystemColors.Control;
            this.btnAdelantar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnAdelantar.FlatAppearance.BorderSize = 0;
            this.btnAdelantar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdelantar.Font = new System.Drawing.Font("Webdings", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btnAdelantar.Location = new System.Drawing.Point(214, 121);
            this.btnAdelantar.Name = "btnAdelantar";
            this.btnAdelantar.Size = new System.Drawing.Size(32, 32);
            this.btnAdelantar.TabIndex = 6;
            this.btnAdelantar.Text = "8";
            this.btnAdelantar.UseVisualStyleBackColor = false;
            this.btnAdelantar.Click += new System.EventHandler(this.btnAdelantar_Click);
            // 
            // btnRegresar
            // 
            this.btnRegresar.BackColor = System.Drawing.SystemColors.Control;
            this.btnRegresar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnRegresar.FlatAppearance.BorderSize = 0;
            this.btnRegresar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegresar.Font = new System.Drawing.Font("Webdings", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btnRegresar.Location = new System.Drawing.Point(100, 121);
            this.btnRegresar.Name = "btnRegresar";
            this.btnRegresar.Size = new System.Drawing.Size(32, 32);
            this.btnRegresar.TabIndex = 7;
            this.btnRegresar.Text = "7";
            this.btnRegresar.UseVisualStyleBackColor = false;
            this.btnRegresar.Click += new System.EventHandler(this.btnRegresar_Click);
            // 
            // mtbVolumen
            // 
            this.mtbVolumen.BackColor = System.Drawing.Color.Transparent;
            this.mtbVolumen.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.mtbVolumen.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtbVolumen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(125)))), ((int)(((byte)(123)))));
            this.mtbVolumen.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mtbVolumen.IndentHeight = 6;
            this.mtbVolumen.Location = new System.Drawing.Point(239, 154);
            this.mtbVolumen.Maximum = 100;
            this.mtbVolumen.Minimum = 0;
            this.mtbVolumen.Name = "mtbVolumen";
            this.mtbVolumen.Size = new System.Drawing.Size(88, 22);
            this.mtbVolumen.TabIndex = 8;
            this.mtbVolumen.TextTickStyle = System.Windows.Forms.TickStyle.None;
            this.mtbVolumen.TickColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(146)))), ((int)(((byte)(148)))));
            this.mtbVolumen.TickHeight = 4;
            this.mtbVolumen.TickStyle = System.Windows.Forms.TickStyle.None;
            this.mtbVolumen.TrackerColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(130)))), ((int)(((byte)(198)))));
            this.mtbVolumen.TrackerSize = new System.Drawing.Size(10, 10);
            this.mtbVolumen.TrackLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(93)))), ((int)(((byte)(90)))));
            this.mtbVolumen.TrackLineHeight = 3;
            this.mtbVolumen.TrackLineSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(93)))), ((int)(((byte)(90)))));
            this.mtbVolumen.Value = 50;
            this.mtbVolumen.ValueChanged += new XComponent.SliderBar.ValueChangedHandler(this.mtbVolumen_ValueChanged);
            // 
            // txtCancionMinutos
            // 
            this.txtCancionMinutos.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtCancionMinutos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCancionMinutos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCancionMinutos.Location = new System.Drawing.Point(12, 113);
            this.txtCancionMinutos.Name = "txtCancionMinutos";
            this.txtCancionMinutos.Size = new System.Drawing.Size(27, 13);
            this.txtCancionMinutos.TabIndex = 9;
            this.txtCancionMinutos.Text = "--:--";
            this.txtCancionMinutos.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCancionMinutosDuracion
            // 
            this.txtCancionMinutosDuracion.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtCancionMinutosDuracion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCancionMinutosDuracion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCancionMinutosDuracion.Location = new System.Drawing.Point(287, 113);
            this.txtCancionMinutosDuracion.Name = "txtCancionMinutosDuracion";
            this.txtCancionMinutosDuracion.Size = new System.Drawing.Size(27, 13);
            this.txtCancionMinutosDuracion.TabIndex = 10;
            this.txtCancionMinutosDuracion.Text = "--:--";
            this.txtCancionMinutosDuracion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnSilencio
            // 
            this.btnSilencio.BackColor = System.Drawing.SystemColors.Control;
            this.btnSilencio.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSilencio.FlatAppearance.BorderSize = 0;
            this.btnSilencio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSilencio.Font = new System.Drawing.Font("Webdings", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btnSilencio.Location = new System.Drawing.Point(223, 153);
            this.btnSilencio.Name = "btnSilencio";
            this.btnSilencio.Size = new System.Drawing.Size(23, 27);
            this.btnSilencio.TabIndex = 11;
            this.btnSilencio.Text = "X";
            this.btnSilencio.UseVisualStyleBackColor = false;
            // 
            // btnRepetir
            // 
            this.btnRepetir.BackColor = System.Drawing.Color.Transparent;
            this.btnRepetir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnRepetir.FlatAppearance.BorderSize = 0;
            this.btnRepetir.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnRepetir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRepetir.Font = new System.Drawing.Font("Wingdings 3", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btnRepetir.Location = new System.Drawing.Point(191, 154);
            this.btnRepetir.Name = "btnRepetir";
            this.btnRepetir.Size = new System.Drawing.Size(26, 26);
            this.btnRepetir.TabIndex = 12;
            this.btnRepetir.Text = "Q";
            this.btnRepetir.UseVisualStyleBackColor = false;
            this.btnRepetir.Click += new System.EventHandler(this.btnRepetir_Click);
            // 
            // txtNivelVolumen
            // 
            this.txtNivelVolumen.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtNivelVolumen.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNivelVolumen.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNivelVolumen.Location = new System.Drawing.Point(269, 173);
            this.txtNivelVolumen.Name = "txtNivelVolumen";
            this.txtNivelVolumen.Size = new System.Drawing.Size(27, 11);
            this.txtNivelVolumen.TabIndex = 13;
            this.txtNivelVolumen.Text = "50";
            this.txtNivelVolumen.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tmrMusicaProgres
            // 
            this.tmrMusicaProgres.Interval = 1;
            this.tmrMusicaProgres.Tick += new System.EventHandler(this.tmrMusicaProgres_Tick);
            // 
            // frmReproducirCancion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 189);
            this.ControlBox = false;
            this.Controls.Add(this.txtCancionMinutosDuracion);
            this.Controls.Add(this.txtCancionMinutos);
            this.Controls.Add(this.mtbVolumen);
            this.Controls.Add(this.btnRegresar);
            this.Controls.Add(this.btnAdelantar);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.txtCancion);
            this.Controls.Add(this.mtbCancion);
            this.Controls.Add(this.wmpMusica);
            this.Controls.Add(this.btnSilencio);
            this.Controls.Add(this.btnRepetir);
            this.Controls.Add(this.txtNivelVolumen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmReproducirCancion";
            this.Load += new System.EventHandler(this.frmReproducirCancion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.wmpMusica)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AxWMPLib.AxWindowsMediaPlayer wmpMusica;
        private XComponent.SliderBar.MACTrackBar mtbCancion;
        private System.Windows.Forms.TextBox txtCancion;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnAdelantar;
        private System.Windows.Forms.Button btnRegresar;
        private XComponent.SliderBar.MACTrackBar mtbVolumen;
        private System.Windows.Forms.TextBox txtCancionMinutos;
        private System.Windows.Forms.TextBox txtCancionMinutosDuracion;
        private System.Windows.Forms.Button btnSilencio;
        private System.Windows.Forms.Button btnRepetir;
        private System.Windows.Forms.TextBox txtNivelVolumen;
        private System.Windows.Forms.Timer tmrMusicaProgres;
    }
}