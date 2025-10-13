
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("SN_Text", 2, 2);
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Nodo_Text", 0, 1, new System.Windows.Forms.TreeNode[] {
            treeNode1});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCanciones));
            this.fbdUbicacion = new System.Windows.Forms.FolderBrowserDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRutaOrigen = new System.Windows.Forms.TextBox();
            this.btnRuta = new System.Windows.Forms.Button();
            this.pgbCargando = new System.Windows.Forms.ProgressBar();
            this.scFolders = new System.Windows.Forms.SplitContainer();
            this.tvCanciones = new System.Windows.Forms.TreeView();
            this.imlTreeVew = new System.Windows.Forms.ImageList(this.components);
            this.scCanciones = new System.Windows.Forms.SplitContainer();
            this.gbListaCanciones = new System.Windows.Forms.GroupBox();
            this.dtgMusica = new System.Windows.Forms.DataGridView();
            this.tcID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cRuta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cArchivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cExtencion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cTamaño = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmModifica = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clmID3v1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clmID3v2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cNumPista = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cArtista = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cArtistaC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cAlbum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cAño = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdGenero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cGenero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cComentario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tcEstadoCancion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.scFolders)).BeginInit();
            this.scFolders.Panel1.SuspendLayout();
            this.scFolders.Panel2.SuspendLayout();
            this.scFolders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scCanciones)).BeginInit();
            this.scCanciones.Panel1.SuspendLayout();
            this.scCanciones.SuspendLayout();
            this.gbListaCanciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgMusica)).BeginInit();
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
            this.btnRuta.Location = new System.Drawing.Point(481, 10);
            this.btnRuta.Name = "btnRuta";
            this.btnRuta.Size = new System.Drawing.Size(75, 23);
            this.btnRuta.TabIndex = 2;
            this.btnRuta.Text = "Esp Ruta";
            this.btnRuta.UseVisualStyleBackColor = true;
            this.btnRuta.Click += new System.EventHandler(this.btnRuta_Click);
            // 
            // pgbCargando
            // 
            this.pgbCargando.Location = new System.Drawing.Point(562, 10);
            this.pgbCargando.Name = "pgbCargando";
            this.pgbCargando.Size = new System.Drawing.Size(225, 23);
            this.pgbCargando.TabIndex = 6;
            // 
            // scFolders
            // 
            this.scFolders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scFolders.Location = new System.Drawing.Point(5, 39);
            this.scFolders.Name = "scFolders";
            // 
            // scFolders.Panel1
            // 
            this.scFolders.Panel1.Controls.Add(this.tvCanciones);
            // 
            // scFolders.Panel2
            // 
            this.scFolders.Panel2.Controls.Add(this.scCanciones);
            this.scFolders.Size = new System.Drawing.Size(783, 399);
            this.scFolders.SplitterDistance = 215;
            this.scFolders.TabIndex = 8;
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
            treeNode1.ImageIndex = 2;
            treeNode1.Name = "SN_Name";
            treeNode1.SelectedImageIndex = 2;
            treeNode1.Text = "SN_Text";
            treeNode1.ToolTipText = "SN_ToolTipText";
            treeNode2.ImageIndex = 0;
            treeNode2.Name = "Nodo_Name";
            treeNode2.SelectedImageIndex = 1;
            treeNode2.Text = "Nodo_Text";
            treeNode2.ToolTipText = "Nodo_ToolTipText";
            this.tvCanciones.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2});
            this.tvCanciones.SelectedImageIndex = 1;
            this.tvCanciones.Size = new System.Drawing.Size(209, 393);
            this.tvCanciones.StateImageList = this.imlTreeVew;
            this.tvCanciones.TabIndex = 0;
            this.tvCanciones.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvCanciones_NodeMouseClick);
            // 
            // imlTreeVew
            // 
            this.imlTreeVew.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlTreeVew.ImageStream")));
            this.imlTreeVew.TransparentColor = System.Drawing.Color.Transparent;
            this.imlTreeVew.Images.SetKeyName(0, "FCerrado.jpg");
            this.imlTreeVew.Images.SetKeyName(1, "FAbierto.jpg");
            this.imlTreeVew.Images.SetKeyName(2, "AMusica.png");
            // 
            // scCanciones
            // 
            this.scCanciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scCanciones.Location = new System.Drawing.Point(0, 0);
            this.scCanciones.Name = "scCanciones";
            this.scCanciones.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scCanciones.Panel1
            // 
            this.scCanciones.Panel1.Controls.Add(this.gbListaCanciones);
            this.scCanciones.Size = new System.Drawing.Size(564, 399);
            this.scCanciones.SplitterDistance = 126;
            this.scCanciones.TabIndex = 1;
            // 
            // gbListaCanciones
            // 
            this.gbListaCanciones.Controls.Add(this.dtgMusica);
            this.gbListaCanciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbListaCanciones.Location = new System.Drawing.Point(0, 0);
            this.gbListaCanciones.Name = "gbListaCanciones";
            this.gbListaCanciones.Size = new System.Drawing.Size(564, 126);
            this.gbListaCanciones.TabIndex = 1;
            this.gbListaCanciones.TabStop = false;
            this.gbListaCanciones.Text = "groupBox1";
            // 
            // dtgMusica
            // 
            this.dtgMusica.AllowUserToAddRows = false;
            this.dtgMusica.AllowUserToDeleteRows = false;
            this.dtgMusica.AllowUserToOrderColumns = true;
            this.dtgMusica.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgMusica.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgMusica.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tcID,
            this.cRuta,
            this.cArchivo,
            this.cExtencion,
            this.cTamaño,
            this.cmModifica,
            this.clmID3v1,
            this.clmID3v2,
            this.cNumPista,
            this.cNombre,
            this.cArtista,
            this.cArtistaC,
            this.cAlbum,
            this.cAño,
            this.IdGenero,
            this.cGenero,
            this.cComentario,
            this.tcEstadoCancion});
            this.dtgMusica.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgMusica.Location = new System.Drawing.Point(3, 16);
            this.dtgMusica.Name = "dtgMusica";
            this.dtgMusica.RowHeadersVisible = false;
            this.dtgMusica.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgMusica.ShowEditingIcon = false;
            this.dtgMusica.ShowRowErrors = false;
            this.dtgMusica.Size = new System.Drawing.Size(558, 107);
            this.dtgMusica.TabIndex = 0;
            this.dtgMusica.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dtgMusica_ColumnHeaderMouseClick);
            this.dtgMusica.SelectionChanged += new System.EventHandler(this.dtgMusica_SelectionChanged);
            // 
            // tcID
            // 
            this.tcID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.tcID.DataPropertyName = "RutaID";
            this.tcID.HeaderText = "Id";
            this.tcID.Name = "tcID";
            this.tcID.ReadOnly = true;
            this.tcID.Width = 41;
            // 
            // cRuta
            // 
            this.cRuta.DataPropertyName = "Ruta";
            this.cRuta.HeaderText = "Ruta";
            this.cRuta.Name = "cRuta";
            this.cRuta.ReadOnly = true;
            // 
            // cArchivo
            // 
            this.cArchivo.DataPropertyName = "NombreArchivo";
            this.cArchivo.HeaderText = "Archivo";
            this.cArchivo.Name = "cArchivo";
            this.cArchivo.ReadOnly = true;
            // 
            // cExtencion
            // 
            this.cExtencion.DataPropertyName = "Extencion";
            this.cExtencion.HeaderText = "Extencion";
            this.cExtencion.Name = "cExtencion";
            this.cExtencion.Visible = false;
            // 
            // cTamaño
            // 
            this.cTamaño.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cTamaño.DataPropertyName = "Tamanio";
            this.cTamaño.HeaderText = "Tamaño";
            this.cTamaño.Name = "cTamaño";
            this.cTamaño.ReadOnly = true;
            this.cTamaño.Width = 71;
            // 
            // cmModifica
            // 
            this.cmModifica.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cmModifica.DataPropertyName = "bDatCan_Mod";
            this.cmModifica.HeaderText = "Modifica";
            this.cmModifica.Name = "cmModifica";
            this.cmModifica.ReadOnly = true;
            this.cmModifica.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cmModifica.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.cmModifica.Width = 72;
            // 
            // clmID3v1
            // 
            this.clmID3v1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.clmID3v1.DataPropertyName = "bEstadoID3v1";
            this.clmID3v1.HeaderText = "ID3v1";
            this.clmID3v1.Name = "clmID3v1";
            this.clmID3v1.ReadOnly = true;
            this.clmID3v1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmID3v1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.clmID3v1.Width = 61;
            // 
            // clmID3v2
            // 
            this.clmID3v2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.clmID3v2.DataPropertyName = "bEstadoID3v2";
            this.clmID3v2.HeaderText = "ID3v2";
            this.clmID3v2.Name = "clmID3v2";
            this.clmID3v2.ReadOnly = true;
            this.clmID3v2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmID3v2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.clmID3v2.Width = 61;
            // 
            // cNumPista
            // 
            this.cNumPista.DataPropertyName = "sMusica_NumPista";
            this.cNumPista.HeaderText = "Número Pista";
            this.cNumPista.Name = "cNumPista";
            this.cNumPista.Visible = false;
            // 
            // cNombre
            // 
            this.cNombre.DataPropertyName = "sMusica_Nombre";
            this.cNombre.HeaderText = "Nombre";
            this.cNombre.Name = "cNombre";
            this.cNombre.Visible = false;
            // 
            // cArtista
            // 
            this.cArtista.DataPropertyName = "sMusica_Artista";
            this.cArtista.HeaderText = "Artista";
            this.cArtista.Name = "cArtista";
            this.cArtista.Visible = false;
            // 
            // cArtistaC
            // 
            this.cArtistaC.DataPropertyName = "sMusica_ArtistaC";
            this.cArtistaC.HeaderText = "Artista Complementario";
            this.cArtistaC.Name = "cArtistaC";
            this.cArtistaC.Visible = false;
            // 
            // cAlbum
            // 
            this.cAlbum.DataPropertyName = "sMusica_Album";
            this.cAlbum.HeaderText = "Album";
            this.cAlbum.Name = "cAlbum";
            this.cAlbum.Visible = false;
            // 
            // cAño
            // 
            this.cAño.DataPropertyName = "sMusica_Año";
            this.cAño.HeaderText = "Año";
            this.cAño.Name = "cAño";
            this.cAño.Visible = false;
            // 
            // IdGenero
            // 
            this.IdGenero.DataPropertyName = "sMusica_IdGenero";
            this.IdGenero.HeaderText = "Id Genero";
            this.IdGenero.Name = "IdGenero";
            this.IdGenero.Visible = false;
            // 
            // cGenero
            // 
            this.cGenero.DataPropertyName = "sMusica_Genero";
            this.cGenero.HeaderText = "Genero";
            this.cGenero.Name = "cGenero";
            this.cGenero.Visible = false;
            // 
            // cComentario
            // 
            this.cComentario.DataPropertyName = "sMusica_Comentario";
            this.cComentario.HeaderText = "Comentario";
            this.cComentario.Name = "cComentario";
            this.cComentario.Visible = false;
            // 
            // tcEstadoCancion
            // 
            this.tcEstadoCancion.DataPropertyName = "EstCancion";
            this.tcEstadoCancion.HeaderText = "Estado Cancion";
            this.tcEstadoCancion.Name = "tcEstadoCancion";
            this.tcEstadoCancion.Visible = false;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(712, 11);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 23);
            this.btnBuscar.TabIndex = 9;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Visible = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // FrmCanciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.scFolders);
            this.Controls.Add(this.pgbCargando);
            this.Controls.Add(this.btnRuta);
            this.Controls.Add(this.txtRutaOrigen);
            this.Controls.Add(this.label1);
            this.Name = "FrmCanciones";
            this.Text = "Aplicacion de Canciones";
            this.scFolders.Panel1.ResumeLayout(false);
            this.scFolders.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scFolders)).EndInit();
            this.scFolders.ResumeLayout(false);
            this.scCanciones.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scCanciones)).EndInit();
            this.scCanciones.ResumeLayout(false);
            this.gbListaCanciones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgMusica)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog fbdUbicacion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRutaOrigen;
        private System.Windows.Forms.Button btnRuta;
        private System.Windows.Forms.ProgressBar pgbCargando;
        private System.Windows.Forms.SplitContainer scFolders;
        private System.Windows.Forms.TreeView tvCanciones;
        private System.Windows.Forms.ImageList imlTreeVew;
        private System.Windows.Forms.DataGridView dtgMusica;
        private System.Windows.Forms.SplitContainer scCanciones;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.GroupBox gbListaCanciones;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.DataGridViewTextBoxColumn tcID;
        private System.Windows.Forms.DataGridViewTextBoxColumn cRuta;
        private System.Windows.Forms.DataGridViewTextBoxColumn cArchivo;
        private System.Windows.Forms.DataGridViewTextBoxColumn cExtencion;
        private System.Windows.Forms.DataGridViewTextBoxColumn cTamaño;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cmModifica;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clmID3v1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clmID3v2;
        private System.Windows.Forms.DataGridViewTextBoxColumn cNumPista;
        private System.Windows.Forms.DataGridViewTextBoxColumn cNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn cArtista;
        private System.Windows.Forms.DataGridViewTextBoxColumn cArtistaC;
        private System.Windows.Forms.DataGridViewTextBoxColumn cAlbum;
        private System.Windows.Forms.DataGridViewTextBoxColumn cAño;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdGenero;
        private System.Windows.Forms.DataGridViewTextBoxColumn cGenero;
        private System.Windows.Forms.DataGridViewTextBoxColumn cComentario;
        private System.Windows.Forms.DataGridViewTextBoxColumn tcEstadoCancion;
    }
}

