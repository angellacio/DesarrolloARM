
namespace ManejoCanciones
{
    partial class FrmCanciones
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

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("SN_Text");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Nodo_Text", new System.Windows.Forms.TreeNode[] {
            treeNode1});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCanciones));
            this.fbdUbicacion = new System.Windows.Forms.FolderBrowserDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRutaOrigen = new System.Windows.Forms.TextBox();
            this.btnRuta = new System.Windows.Forms.Button();
            this.btnEtiquetas = new System.Windows.Forms.Button();
            this.pgbCargando = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.scMusica = new System.Windows.Forms.SplitContainer();
            this.tvCanciones = new System.Windows.Forms.TreeView();
            this.imlTreeVew = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.scMusica)).BeginInit();
            this.scMusica.Panel1.SuspendLayout();
            this.scMusica.SuspendLayout();
            this.SuspendLayout();
            // 
            // fbdUbicacion
            // 
            this.fbdUbicacion.ShowNewFolderButton = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ruta a escanear:";
            // 
            // txtRutaOrigen
            // 
            this.txtRutaOrigen.Location = new System.Drawing.Point(109, 13);
            this.txtRutaOrigen.Name = "txtRutaOrigen";
            this.txtRutaOrigen.ReadOnly = true;
            this.txtRutaOrigen.Size = new System.Drawing.Size(366, 20);
            this.txtRutaOrigen.TabIndex = 1;
            // 
            // btnRuta
            // 
            this.btnRuta.Location = new System.Drawing.Point(482, 13);
            this.btnRuta.Name = "btnRuta";
            this.btnRuta.Size = new System.Drawing.Size(75, 23);
            this.btnRuta.TabIndex = 2;
            this.btnRuta.Text = "Esp Ruta";
            this.btnRuta.UseVisualStyleBackColor = true;
            this.btnRuta.Click += new System.EventHandler(this.btnRuta_Click);
            // 
            // btnEtiquetas
            // 
            this.btnEtiquetas.Location = new System.Drawing.Point(563, 13);
            this.btnEtiquetas.Name = "btnEtiquetas";
            this.btnEtiquetas.Size = new System.Drawing.Size(75, 23);
            this.btnEtiquetas.TabIndex = 4;
            this.btnEtiquetas.Text = "Etiquetas";
            this.btnEtiquetas.UseVisualStyleBackColor = true;
            // 
            // pgbCargando
            // 
            this.pgbCargando.Location = new System.Drawing.Point(644, 9);
            this.pgbCargando.Name = "pgbCargando";
            this.pgbCargando.Size = new System.Drawing.Size(144, 23);
            this.pgbCargando.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(109, -6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "label2";
            // 
            // scMusica
            // 
            this.scMusica.Location = new System.Drawing.Point(5, 39);
            this.scMusica.Name = "scMusica";
            // 
            // scMusica.Panel1
            // 
            this.scMusica.Panel1.Controls.Add(this.tvCanciones);
            this.scMusica.Size = new System.Drawing.Size(783, 399);
            this.scMusica.SplitterDistance = 261;
            this.scMusica.TabIndex = 8;
            // 
            // tvCanciones
            // 
            this.tvCanciones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvCanciones.ImageIndex = 0;
            this.tvCanciones.ImageList = this.imlTreeVew;
            this.tvCanciones.Location = new System.Drawing.Point(3, 3);
            this.tvCanciones.Name = "tvCanciones";
            treeNode1.Name = "SN_Name";
            treeNode1.Text = "SN_Text";
            treeNode1.ToolTipText = "SN_ToolTipText";
            treeNode2.Name = "Nodo_Name";
            treeNode2.Text = "Nodo_Text";
            treeNode2.ToolTipText = "Nodo_ToolTipText";
            this.tvCanciones.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2});
            this.tvCanciones.SelectedImageIndex = 0;
            this.tvCanciones.Size = new System.Drawing.Size(255, 393);
            this.tvCanciones.TabIndex = 0;
            // 
            // imlTreeVew
            // 
            this.imlTreeVew.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlTreeVew.ImageStream")));
            this.imlTreeVew.TransparentColor = System.Drawing.Color.Transparent;
            this.imlTreeVew.Images.SetKeyName(0, "FCerrado.jpg");
            this.imlTreeVew.Images.SetKeyName(1, "FAbierto.jpg");
            // 
            // frmCanciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.scMusica);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pgbCargando);
            this.Controls.Add(this.btnEtiquetas);
            this.Controls.Add(this.btnRuta);
            this.Controls.Add(this.txtRutaOrigen);
            this.Controls.Add(this.label1);
            this.Name = "frmCanciones";
            this.Text = "Aplicacion de Canciones";
            this.scMusica.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scMusica)).EndInit();
            this.scMusica.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog fbdUbicacion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRutaOrigen;
        private System.Windows.Forms.Button btnRuta;
        private System.Windows.Forms.Button btnEtiquetas;
        private System.Windows.Forms.ProgressBar pgbCargando;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.SplitContainer scMusica;
        private System.Windows.Forms.TreeView tvCanciones;
        private System.Windows.Forms.ImageList imlTreeVew;
    }
}

