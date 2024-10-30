
namespace ManejoCanciones.uControl
{
    partial class cDatoCancion
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblRutaE = new System.Windows.Forms.Label();
            this.txtRuta = new System.Windows.Forms.TextBox();
            this.ckbFolder = new System.Windows.Forms.CheckBox();
            this.btnContraer = new System.Windows.Forms.Button();
            this.pnlCanciones = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblArchivo = new System.Windows.Forms.Label();
            this.lblArtista = new System.Windows.Forms.Label();
            this.pnlCanciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblRutaE
            // 
            this.lblRutaE.AutoSize = true;
            this.lblRutaE.Location = new System.Drawing.Point(4, 4);
            this.lblRutaE.Name = "lblRutaE";
            this.lblRutaE.Size = new System.Drawing.Size(33, 13);
            this.lblRutaE.TabIndex = 0;
            this.lblRutaE.Text = "Ruta:";
            // 
            // txtRuta
            // 
            this.txtRuta.Location = new System.Drawing.Point(44, 4);
            this.txtRuta.Name = "txtRuta";
            this.txtRuta.ReadOnly = true;
            this.txtRuta.Size = new System.Drawing.Size(389, 20);
            this.txtRuta.TabIndex = 1;
            // 
            // ckbFolder
            // 
            this.ckbFolder.AutoSize = true;
            this.ckbFolder.Location = new System.Drawing.Point(439, 11);
            this.ckbFolder.Name = "ckbFolder";
            this.ckbFolder.Size = new System.Drawing.Size(15, 14);
            this.ckbFolder.TabIndex = 2;
            this.ckbFolder.UseVisualStyleBackColor = true;
            // 
            // btnContraer
            // 
            this.btnContraer.Location = new System.Drawing.Point(460, 4);
            this.btnContraer.Name = "btnContraer";
            this.btnContraer.Size = new System.Drawing.Size(34, 23);
            this.btnContraer.TabIndex = 3;
            this.btnContraer.Text = "C";
            this.btnContraer.UseVisualStyleBackColor = true;
            // 
            // pnlCanciones
            // 
            this.pnlCanciones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCanciones.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCanciones.Controls.Add(this.lblArtista);
            this.pnlCanciones.Controls.Add(this.lblTitulo);
            this.pnlCanciones.Controls.Add(this.lblArchivo);
            this.pnlCanciones.Location = new System.Drawing.Point(0, 31);
            this.pnlCanciones.Name = "pnlCanciones";
            this.pnlCanciones.Size = new System.Drawing.Size(494, 56);
            this.pnlCanciones.TabIndex = 4;
            // 
            // lblTitulo
            // 
            this.lblTitulo.Location = new System.Drawing.Point(111, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(101, 25);
            this.lblTitulo.TabIndex = 3;
            this.lblTitulo.Text = "Titulo";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblArchivo
            // 
            this.lblArchivo.Location = new System.Drawing.Point(0, 0);
            this.lblArchivo.Name = "lblArchivo";
            this.lblArchivo.Size = new System.Drawing.Size(101, 25);
            this.lblArchivo.TabIndex = 2;
            this.lblArchivo.Text = "Archivo";
            this.lblArchivo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblArtista
            // 
            this.lblArtista.Location = new System.Drawing.Point(228, 0);
            this.lblArtista.Name = "lblArtista";
            this.lblArtista.Size = new System.Drawing.Size(101, 25);
            this.lblArtista.TabIndex = 6;
            this.lblArtista.Text = "Artista";
            this.lblArtista.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cDatoCancion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.btnContraer);
            this.Controls.Add(this.ckbFolder);
            this.Controls.Add(this.txtRuta);
            this.Controls.Add(this.lblRutaE);
            this.Controls.Add(this.pnlCanciones);
            this.Name = "cDatoCancion";
            this.Size = new System.Drawing.Size(500, 90);
            this.pnlCanciones.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblRutaE;
        private System.Windows.Forms.TextBox txtRuta;
        private System.Windows.Forms.CheckBox ckbFolder;
        private System.Windows.Forms.Button btnContraer;
        private System.Windows.Forms.Panel pnlCanciones;
        private System.Windows.Forms.Label lblArchivo;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblArtista;
    }
}
