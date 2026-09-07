
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
            this.txtPista = new System.Windows.Forms.TextBox();
            this.txtRuta = new System.Windows.Forms.TextBox();
            this.lblRuta = new System.Windows.Forms.Label();
            this.lblPista = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.lblArtista = new System.Windows.Forms.Label();
            this.lblAlbum = new System.Windows.Forms.Label();
            this.lblGenero = new System.Windows.Forms.Label();
            this.cmbGenero = new System.Windows.Forms.ComboBox();
            this.lblComentario = new System.Windows.Forms.Label();
            this.lblAño = new System.Windows.Forms.Label();
            this.txtAño = new System.Windows.Forms.TextBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.txtArtista = new System.Windows.Forms.TextBox();
            this.txtAlbum = new System.Windows.Forms.TextBox();
            this.txtComentario = new System.Windows.Forms.TextBox();
            this.gbModificado = new System.Windows.Forms.GroupBox();
            this.scDetalleCancion = new System.Windows.Forms.SplitContainer();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.scDatosArchivo = new System.Windows.Forms.SplitContainer();
            this.gbID3v1 = new System.Windows.Forms.GroupBox();
            this.txtID3v1_Genero = new System.Windows.Forms.TextBox();
            this.txtID3v1_Comentario = new System.Windows.Forms.TextBox();
            this.txtID3v1_Album = new System.Windows.Forms.TextBox();
            this.lblID3v1_NumPista = new System.Windows.Forms.Label();
            this.txtID3v1_Artista = new System.Windows.Forms.TextBox();
            this.lblID3v1_Nombre = new System.Windows.Forms.Label();
            this.txtID3v1_Nombre = new System.Windows.Forms.TextBox();
            this.lblID3v1_Artista = new System.Windows.Forms.Label();
            this.txtID3v1_Año = new System.Windows.Forms.TextBox();
            this.lblID3v1_Album = new System.Windows.Forms.Label();
            this.lblID3v1_Año = new System.Windows.Forms.Label();
            this.lblID3v1_Comentario = new System.Windows.Forms.Label();
            this.lblID3v1_Genero = new System.Windows.Forms.Label();
            this.txtID3v1_NumPista = new System.Windows.Forms.TextBox();
            this.gbID3v2 = new System.Windows.Forms.GroupBox();
            this.txtID3v2_Genero = new System.Windows.Forms.TextBox();
            this.txtID3v2_Comentario = new System.Windows.Forms.TextBox();
            this.txtID3v2_Album = new System.Windows.Forms.TextBox();
            this.lblID3v2_NumPista = new System.Windows.Forms.Label();
            this.txtID3v2_Artista = new System.Windows.Forms.TextBox();
            this.lblID3v2_Nombre = new System.Windows.Forms.Label();
            this.txtID3v2_Nombre = new System.Windows.Forms.TextBox();
            this.lblID3v2_Artista = new System.Windows.Forms.Label();
            this.txtID3v2_Año = new System.Windows.Forms.TextBox();
            this.lblID3v2_Album = new System.Windows.Forms.Label();
            this.lblID3v2_Año = new System.Windows.Forms.Label();
            this.lblID3v2_Comentario = new System.Windows.Forms.Label();
            this.lblID3v2_Genero = new System.Windows.Forms.Label();
            this.txtID3v2_NumPista = new System.Windows.Forms.TextBox();
            this.gbModificado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scDetalleCancion)).BeginInit();
            this.scDetalleCancion.Panel1.SuspendLayout();
            this.scDetalleCancion.Panel2.SuspendLayout();
            this.scDetalleCancion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scDatosArchivo)).BeginInit();
            this.scDatosArchivo.Panel1.SuspendLayout();
            this.scDatosArchivo.Panel2.SuspendLayout();
            this.scDatosArchivo.SuspendLayout();
            this.gbID3v1.SuspendLayout();
            this.gbID3v2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtPista
            // 
            this.txtPista.Location = new System.Drawing.Point(70, 45);
            this.txtPista.MaxLength = 9;
            this.txtPista.Name = "txtPista";
            this.txtPista.Size = new System.Drawing.Size(89, 20);
            this.txtPista.TabIndex = 0;
            this.txtPista.TextChanged += new System.EventHandler(this.txtPista_TextChanged);
            // 
            // txtRuta
            // 
            this.txtRuta.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRuta.Location = new System.Drawing.Point(70, 19);
            this.txtRuta.Name = "txtRuta";
            this.txtRuta.Size = new System.Drawing.Size(722, 20);
            this.txtRuta.TabIndex = 1;
            // 
            // lblRuta
            // 
            this.lblRuta.AutoSize = true;
            this.lblRuta.Location = new System.Drawing.Point(6, 22);
            this.lblRuta.Name = "lblRuta";
            this.lblRuta.Size = new System.Drawing.Size(33, 13);
            this.lblRuta.TabIndex = 2;
            this.lblRuta.Text = "Ruta:";
            // 
            // lblPista
            // 
            this.lblPista.AutoSize = true;
            this.lblPista.Location = new System.Drawing.Point(6, 48);
            this.lblPista.Name = "lblPista";
            this.lblPista.Size = new System.Drawing.Size(58, 13);
            this.lblPista.TabIndex = 3;
            this.lblPista.Text = "Num Pista:";
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(6, 74);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(47, 13);
            this.lblNombre.TabIndex = 4;
            this.lblNombre.Text = "Nombre:";
            // 
            // lblArtista
            // 
            this.lblArtista.AutoSize = true;
            this.lblArtista.Location = new System.Drawing.Point(392, 74);
            this.lblArtista.Name = "lblArtista";
            this.lblArtista.Size = new System.Drawing.Size(39, 13);
            this.lblArtista.TabIndex = 5;
            this.lblArtista.Text = "Artista:";
            // 
            // lblAlbum
            // 
            this.lblAlbum.AutoSize = true;
            this.lblAlbum.Location = new System.Drawing.Point(6, 100);
            this.lblAlbum.Name = "lblAlbum";
            this.lblAlbum.Size = new System.Drawing.Size(39, 13);
            this.lblAlbum.TabIndex = 6;
            this.lblAlbum.Text = "Album:";
            // 
            // lblGenero
            // 
            this.lblGenero.AutoSize = true;
            this.lblGenero.Location = new System.Drawing.Point(243, 48);
            this.lblGenero.Name = "lblGenero";
            this.lblGenero.Size = new System.Drawing.Size(45, 13);
            this.lblGenero.TabIndex = 7;
            this.lblGenero.Text = "Genero:";
            // 
            // cmbGenero
            // 
            this.cmbGenero.FormattingEnabled = true;
            this.cmbGenero.Location = new System.Drawing.Point(294, 45);
            this.cmbGenero.Name = "cmbGenero";
            this.cmbGenero.Size = new System.Drawing.Size(230, 21);
            this.cmbGenero.TabIndex = 8;
            this.cmbGenero.SelectionChangeCommitted += new System.EventHandler(this.cmbGenero_SelectionChangeCommitted);
            // 
            // lblComentario
            // 
            this.lblComentario.AutoSize = true;
            this.lblComentario.Location = new System.Drawing.Point(6, 133);
            this.lblComentario.Name = "lblComentario";
            this.lblComentario.Size = new System.Drawing.Size(63, 13);
            this.lblComentario.TabIndex = 9;
            this.lblComentario.Text = "Comentario:";
            // 
            // lblAño
            // 
            this.lblAño.AutoSize = true;
            this.lblAño.Location = new System.Drawing.Point(165, 48);
            this.lblAño.Name = "lblAño";
            this.lblAño.Size = new System.Drawing.Size(29, 13);
            this.lblAño.TabIndex = 10;
            this.lblAño.Text = "Año:";
            // 
            // txtAño
            // 
            this.txtAño.Location = new System.Drawing.Point(200, 45);
            this.txtAño.MaxLength = 4;
            this.txtAño.Name = "txtAño";
            this.txtAño.Size = new System.Drawing.Size(37, 20);
            this.txtAño.TabIndex = 11;
            this.txtAño.TextChanged += new System.EventHandler(this.txtAño_TextChanged);
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(69, 71);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(317, 20);
            this.txtNombre.TabIndex = 12;
            this.txtNombre.TextChanged += new System.EventHandler(this.txtNombre_TextChanged);
            // 
            // txtArtista
            // 
            this.txtArtista.Location = new System.Drawing.Point(437, 71);
            this.txtArtista.Name = "txtArtista";
            this.txtArtista.Size = new System.Drawing.Size(353, 20);
            this.txtArtista.TabIndex = 13;
            this.txtArtista.TextChanged += new System.EventHandler(this.txtArtista_TextChanged);
            // 
            // txtAlbum
            // 
            this.txtAlbum.Location = new System.Drawing.Point(70, 97);
            this.txtAlbum.Name = "txtAlbum";
            this.txtAlbum.Size = new System.Drawing.Size(316, 20);
            this.txtAlbum.TabIndex = 14;
            this.txtAlbum.TextChanged += new System.EventHandler(this.txtAlbum_TextChanged);
            // 
            // txtComentario
            // 
            this.txtComentario.Location = new System.Drawing.Point(70, 123);
            this.txtComentario.Multiline = true;
            this.txtComentario.Name = "txtComentario";
            this.txtComentario.Size = new System.Drawing.Size(723, 33);
            this.txtComentario.TabIndex = 15;
            this.txtComentario.TextChanged += new System.EventHandler(this.txtComentario_TextChanged);
            // 
            // gbModificado
            // 
            this.gbModificado.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbModificado.Controls.Add(this.lblRuta);
            this.gbModificado.Controls.Add(this.txtComentario);
            this.gbModificado.Controls.Add(this.txtRuta);
            this.gbModificado.Controls.Add(this.txtAlbum);
            this.gbModificado.Controls.Add(this.lblPista);
            this.gbModificado.Controls.Add(this.txtArtista);
            this.gbModificado.Controls.Add(this.lblNombre);
            this.gbModificado.Controls.Add(this.txtNombre);
            this.gbModificado.Controls.Add(this.lblArtista);
            this.gbModificado.Controls.Add(this.txtAño);
            this.gbModificado.Controls.Add(this.lblAlbum);
            this.gbModificado.Controls.Add(this.lblAño);
            this.gbModificado.Controls.Add(this.cmbGenero);
            this.gbModificado.Controls.Add(this.lblComentario);
            this.gbModificado.Controls.Add(this.lblGenero);
            this.gbModificado.Controls.Add(this.txtPista);
            this.gbModificado.Location = new System.Drawing.Point(3, 4);
            this.gbModificado.Name = "gbModificado";
            this.gbModificado.Size = new System.Drawing.Size(798, 162);
            this.gbModificado.TabIndex = 16;
            this.gbModificado.TabStop = false;
            this.gbModificado.Text = "groupBox1";
            // 
            // scDetalleCancion
            // 
            this.scDetalleCancion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scDetalleCancion.Location = new System.Drawing.Point(0, 0);
            this.scDetalleCancion.Name = "scDetalleCancion";
            this.scDetalleCancion.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scDetalleCancion.Panel1
            // 
            this.scDetalleCancion.Panel1.Controls.Add(this.btnActualizar);
            this.scDetalleCancion.Panel1.Controls.Add(this.btnLimpiar);
            this.scDetalleCancion.Panel1.Controls.Add(this.gbModificado);
            this.scDetalleCancion.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // scDetalleCancion.Panel2
            // 
            this.scDetalleCancion.Panel2.Controls.Add(this.scDatosArchivo);
            this.scDetalleCancion.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.scDetalleCancion.Size = new System.Drawing.Size(885, 381);
            this.scDetalleCancion.SplitterDistance = 169;
            this.scDetalleCancion.TabIndex = 17;
            // 
            // btnActualizar
            // 
            this.btnActualizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnActualizar.Location = new System.Drawing.Point(807, 4);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(75, 23);
            this.btnActualizar.TabIndex = 18;
            this.btnActualizar.Text = "Act. Lista";
            this.btnActualizar.UseVisualStyleBackColor = true;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLimpiar.Location = new System.Drawing.Point(807, 33);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(75, 23);
            this.btnLimpiar.TabIndex = 17;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // scDatosArchivo
            // 
            this.scDatosArchivo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scDatosArchivo.Location = new System.Drawing.Point(0, 0);
            this.scDatosArchivo.Name = "scDatosArchivo";
            // 
            // scDatosArchivo.Panel1
            // 
            this.scDatosArchivo.Panel1.Controls.Add(this.gbID3v1);
            // 
            // scDatosArchivo.Panel2
            // 
            this.scDatosArchivo.Panel2.Controls.Add(this.gbID3v2);
            this.scDatosArchivo.Size = new System.Drawing.Size(885, 208);
            this.scDatosArchivo.SplitterDistance = 418;
            this.scDatosArchivo.TabIndex = 17;
            // 
            // gbID3v1
            // 
            this.gbID3v1.Controls.Add(this.txtID3v1_Genero);
            this.gbID3v1.Controls.Add(this.txtID3v1_Comentario);
            this.gbID3v1.Controls.Add(this.txtID3v1_Album);
            this.gbID3v1.Controls.Add(this.lblID3v1_NumPista);
            this.gbID3v1.Controls.Add(this.txtID3v1_Artista);
            this.gbID3v1.Controls.Add(this.lblID3v1_Nombre);
            this.gbID3v1.Controls.Add(this.txtID3v1_Nombre);
            this.gbID3v1.Controls.Add(this.lblID3v1_Artista);
            this.gbID3v1.Controls.Add(this.txtID3v1_Año);
            this.gbID3v1.Controls.Add(this.lblID3v1_Album);
            this.gbID3v1.Controls.Add(this.lblID3v1_Año);
            this.gbID3v1.Controls.Add(this.lblID3v1_Comentario);
            this.gbID3v1.Controls.Add(this.lblID3v1_Genero);
            this.gbID3v1.Controls.Add(this.txtID3v1_NumPista);
            this.gbID3v1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbID3v1.Location = new System.Drawing.Point(0, 0);
            this.gbID3v1.Name = "gbID3v1";
            this.gbID3v1.Size = new System.Drawing.Size(418, 208);
            this.gbID3v1.TabIndex = 16;
            this.gbID3v1.TabStop = false;
            this.gbID3v1.Text = "ID3v1";
            // 
            // txtID3v1_Genero
            // 
            this.txtID3v1_Genero.Location = new System.Drawing.Point(284, 13);
            this.txtID3v1_Genero.Name = "txtID3v1_Genero";
            this.txtID3v1_Genero.ReadOnly = true;
            this.txtID3v1_Genero.Size = new System.Drawing.Size(128, 20);
            this.txtID3v1_Genero.TabIndex = 16;
            // 
            // txtID3v1_Comentario
            // 
            this.txtID3v1_Comentario.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtID3v1_Comentario.Location = new System.Drawing.Point(69, 117);
            this.txtID3v1_Comentario.Multiline = true;
            this.txtID3v1_Comentario.Name = "txtID3v1_Comentario";
            this.txtID3v1_Comentario.ReadOnly = true;
            this.txtID3v1_Comentario.Size = new System.Drawing.Size(344, 33);
            this.txtID3v1_Comentario.TabIndex = 15;
            // 
            // txtID3v1_Album
            // 
            this.txtID3v1_Album.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtID3v1_Album.Location = new System.Drawing.Point(69, 91);
            this.txtID3v1_Album.Name = "txtID3v1_Album";
            this.txtID3v1_Album.ReadOnly = true;
            this.txtID3v1_Album.Size = new System.Drawing.Size(344, 20);
            this.txtID3v1_Album.TabIndex = 14;
            // 
            // lblID3v1_NumPista
            // 
            this.lblID3v1_NumPista.AutoSize = true;
            this.lblID3v1_NumPista.Location = new System.Drawing.Point(6, 16);
            this.lblID3v1_NumPista.Name = "lblID3v1_NumPista";
            this.lblID3v1_NumPista.Size = new System.Drawing.Size(58, 13);
            this.lblID3v1_NumPista.TabIndex = 3;
            this.lblID3v1_NumPista.Text = "Num Pista:";
            // 
            // txtID3v1_Artista
            // 
            this.txtID3v1_Artista.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtID3v1_Artista.Location = new System.Drawing.Point(69, 65);
            this.txtID3v1_Artista.Name = "txtID3v1_Artista";
            this.txtID3v1_Artista.ReadOnly = true;
            this.txtID3v1_Artista.Size = new System.Drawing.Size(344, 20);
            this.txtID3v1_Artista.TabIndex = 13;
            // 
            // lblID3v1_Nombre
            // 
            this.lblID3v1_Nombre.AutoSize = true;
            this.lblID3v1_Nombre.Location = new System.Drawing.Point(6, 42);
            this.lblID3v1_Nombre.Name = "lblID3v1_Nombre";
            this.lblID3v1_Nombre.Size = new System.Drawing.Size(47, 13);
            this.lblID3v1_Nombre.TabIndex = 4;
            this.lblID3v1_Nombre.Text = "Nombre:";
            // 
            // txtID3v1_Nombre
            // 
            this.txtID3v1_Nombre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtID3v1_Nombre.Location = new System.Drawing.Point(69, 39);
            this.txtID3v1_Nombre.Name = "txtID3v1_Nombre";
            this.txtID3v1_Nombre.ReadOnly = true;
            this.txtID3v1_Nombre.Size = new System.Drawing.Size(344, 20);
            this.txtID3v1_Nombre.TabIndex = 12;
            // 
            // lblID3v1_Artista
            // 
            this.lblID3v1_Artista.AutoSize = true;
            this.lblID3v1_Artista.Location = new System.Drawing.Point(6, 68);
            this.lblID3v1_Artista.Name = "lblID3v1_Artista";
            this.lblID3v1_Artista.Size = new System.Drawing.Size(39, 13);
            this.lblID3v1_Artista.TabIndex = 5;
            this.lblID3v1_Artista.Text = "Artista:";
            // 
            // txtID3v1_Año
            // 
            this.txtID3v1_Año.Location = new System.Drawing.Point(200, 13);
            this.txtID3v1_Año.MaxLength = 4;
            this.txtID3v1_Año.Name = "txtID3v1_Año";
            this.txtID3v1_Año.ReadOnly = true;
            this.txtID3v1_Año.Size = new System.Drawing.Size(37, 20);
            this.txtID3v1_Año.TabIndex = 11;
            // 
            // lblID3v1_Album
            // 
            this.lblID3v1_Album.AutoSize = true;
            this.lblID3v1_Album.Location = new System.Drawing.Point(6, 94);
            this.lblID3v1_Album.Name = "lblID3v1_Album";
            this.lblID3v1_Album.Size = new System.Drawing.Size(39, 13);
            this.lblID3v1_Album.TabIndex = 6;
            this.lblID3v1_Album.Text = "Album:";
            // 
            // lblID3v1_Año
            // 
            this.lblID3v1_Año.AutoSize = true;
            this.lblID3v1_Año.Location = new System.Drawing.Point(165, 16);
            this.lblID3v1_Año.Name = "lblID3v1_Año";
            this.lblID3v1_Año.Size = new System.Drawing.Size(29, 13);
            this.lblID3v1_Año.TabIndex = 10;
            this.lblID3v1_Año.Text = "Año:";
            // 
            // lblID3v1_Comentario
            // 
            this.lblID3v1_Comentario.AutoSize = true;
            this.lblID3v1_Comentario.Location = new System.Drawing.Point(6, 120);
            this.lblID3v1_Comentario.Name = "lblID3v1_Comentario";
            this.lblID3v1_Comentario.Size = new System.Drawing.Size(63, 13);
            this.lblID3v1_Comentario.TabIndex = 9;
            this.lblID3v1_Comentario.Text = "Comentario:";
            // 
            // lblID3v1_Genero
            // 
            this.lblID3v1_Genero.AutoSize = true;
            this.lblID3v1_Genero.Location = new System.Drawing.Point(243, 16);
            this.lblID3v1_Genero.Name = "lblID3v1_Genero";
            this.lblID3v1_Genero.Size = new System.Drawing.Size(45, 13);
            this.lblID3v1_Genero.TabIndex = 7;
            this.lblID3v1_Genero.Text = "Genero:";
            // 
            // txtID3v1_NumPista
            // 
            this.txtID3v1_NumPista.Location = new System.Drawing.Point(70, 13);
            this.txtID3v1_NumPista.MaxLength = 9;
            this.txtID3v1_NumPista.Name = "txtID3v1_NumPista";
            this.txtID3v1_NumPista.ReadOnly = true;
            this.txtID3v1_NumPista.Size = new System.Drawing.Size(89, 20);
            this.txtID3v1_NumPista.TabIndex = 0;
            // 
            // gbID3v2
            // 
            this.gbID3v2.Controls.Add(this.txtID3v2_Genero);
            this.gbID3v2.Controls.Add(this.txtID3v2_Comentario);
            this.gbID3v2.Controls.Add(this.txtID3v2_Album);
            this.gbID3v2.Controls.Add(this.lblID3v2_NumPista);
            this.gbID3v2.Controls.Add(this.txtID3v2_Artista);
            this.gbID3v2.Controls.Add(this.lblID3v2_Nombre);
            this.gbID3v2.Controls.Add(this.txtID3v2_Nombre);
            this.gbID3v2.Controls.Add(this.lblID3v2_Artista);
            this.gbID3v2.Controls.Add(this.txtID3v2_Año);
            this.gbID3v2.Controls.Add(this.lblID3v2_Album);
            this.gbID3v2.Controls.Add(this.lblID3v2_Año);
            this.gbID3v2.Controls.Add(this.lblID3v2_Comentario);
            this.gbID3v2.Controls.Add(this.lblID3v2_Genero);
            this.gbID3v2.Controls.Add(this.txtID3v2_NumPista);
            this.gbID3v2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbID3v2.Location = new System.Drawing.Point(0, 0);
            this.gbID3v2.Name = "gbID3v2";
            this.gbID3v2.Size = new System.Drawing.Size(463, 208);
            this.gbID3v2.TabIndex = 17;
            this.gbID3v2.TabStop = false;
            this.gbID3v2.Text = "ID3v2";
            // 
            // txtID3v2_Genero
            // 
            this.txtID3v2_Genero.Location = new System.Drawing.Point(285, 13);
            this.txtID3v2_Genero.Name = "txtID3v2_Genero";
            this.txtID3v2_Genero.ReadOnly = true;
            this.txtID3v2_Genero.Size = new System.Drawing.Size(128, 20);
            this.txtID3v2_Genero.TabIndex = 17;
            // 
            // txtID3v2_Comentario
            // 
            this.txtID3v2_Comentario.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtID3v2_Comentario.Location = new System.Drawing.Point(69, 117);
            this.txtID3v2_Comentario.Multiline = true;
            this.txtID3v2_Comentario.Name = "txtID3v2_Comentario";
            this.txtID3v2_Comentario.ReadOnly = true;
            this.txtID3v2_Comentario.Size = new System.Drawing.Size(387, 33);
            this.txtID3v2_Comentario.TabIndex = 15;
            // 
            // txtID3v2_Album
            // 
            this.txtID3v2_Album.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtID3v2_Album.Location = new System.Drawing.Point(69, 91);
            this.txtID3v2_Album.Name = "txtID3v2_Album";
            this.txtID3v2_Album.ReadOnly = true;
            this.txtID3v2_Album.Size = new System.Drawing.Size(387, 20);
            this.txtID3v2_Album.TabIndex = 14;
            // 
            // lblID3v2_NumPista
            // 
            this.lblID3v2_NumPista.AutoSize = true;
            this.lblID3v2_NumPista.Location = new System.Drawing.Point(6, 16);
            this.lblID3v2_NumPista.Name = "lblID3v2_NumPista";
            this.lblID3v2_NumPista.Size = new System.Drawing.Size(58, 13);
            this.lblID3v2_NumPista.TabIndex = 3;
            this.lblID3v2_NumPista.Text = "Num Pista:";
            // 
            // txtID3v2_Artista
            // 
            this.txtID3v2_Artista.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtID3v2_Artista.Location = new System.Drawing.Point(69, 65);
            this.txtID3v2_Artista.Name = "txtID3v2_Artista";
            this.txtID3v2_Artista.ReadOnly = true;
            this.txtID3v2_Artista.Size = new System.Drawing.Size(387, 20);
            this.txtID3v2_Artista.TabIndex = 13;
            // 
            // lblID3v2_Nombre
            // 
            this.lblID3v2_Nombre.AutoSize = true;
            this.lblID3v2_Nombre.Location = new System.Drawing.Point(6, 42);
            this.lblID3v2_Nombre.Name = "lblID3v2_Nombre";
            this.lblID3v2_Nombre.Size = new System.Drawing.Size(47, 13);
            this.lblID3v2_Nombre.TabIndex = 4;
            this.lblID3v2_Nombre.Text = "Nombre:";
            // 
            // txtID3v2_Nombre
            // 
            this.txtID3v2_Nombre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtID3v2_Nombre.Location = new System.Drawing.Point(69, 39);
            this.txtID3v2_Nombre.Name = "txtID3v2_Nombre";
            this.txtID3v2_Nombre.ReadOnly = true;
            this.txtID3v2_Nombre.Size = new System.Drawing.Size(387, 20);
            this.txtID3v2_Nombre.TabIndex = 12;
            // 
            // lblID3v2_Artista
            // 
            this.lblID3v2_Artista.AutoSize = true;
            this.lblID3v2_Artista.Location = new System.Drawing.Point(6, 68);
            this.lblID3v2_Artista.Name = "lblID3v2_Artista";
            this.lblID3v2_Artista.Size = new System.Drawing.Size(39, 13);
            this.lblID3v2_Artista.TabIndex = 5;
            this.lblID3v2_Artista.Text = "Artista:";
            // 
            // txtID3v2_Año
            // 
            this.txtID3v2_Año.Location = new System.Drawing.Point(200, 13);
            this.txtID3v2_Año.MaxLength = 4;
            this.txtID3v2_Año.Name = "txtID3v2_Año";
            this.txtID3v2_Año.ReadOnly = true;
            this.txtID3v2_Año.Size = new System.Drawing.Size(37, 20);
            this.txtID3v2_Año.TabIndex = 11;
            // 
            // lblID3v2_Album
            // 
            this.lblID3v2_Album.AutoSize = true;
            this.lblID3v2_Album.Location = new System.Drawing.Point(6, 94);
            this.lblID3v2_Album.Name = "lblID3v2_Album";
            this.lblID3v2_Album.Size = new System.Drawing.Size(39, 13);
            this.lblID3v2_Album.TabIndex = 6;
            this.lblID3v2_Album.Text = "Album:";
            // 
            // lblID3v2_Año
            // 
            this.lblID3v2_Año.AutoSize = true;
            this.lblID3v2_Año.Location = new System.Drawing.Point(165, 16);
            this.lblID3v2_Año.Name = "lblID3v2_Año";
            this.lblID3v2_Año.Size = new System.Drawing.Size(29, 13);
            this.lblID3v2_Año.TabIndex = 10;
            this.lblID3v2_Año.Text = "Año:";
            // 
            // lblID3v2_Comentario
            // 
            this.lblID3v2_Comentario.AutoSize = true;
            this.lblID3v2_Comentario.Location = new System.Drawing.Point(6, 120);
            this.lblID3v2_Comentario.Name = "lblID3v2_Comentario";
            this.lblID3v2_Comentario.Size = new System.Drawing.Size(63, 13);
            this.lblID3v2_Comentario.TabIndex = 9;
            this.lblID3v2_Comentario.Text = "Comentario:";
            // 
            // lblID3v2_Genero
            // 
            this.lblID3v2_Genero.AutoSize = true;
            this.lblID3v2_Genero.Location = new System.Drawing.Point(243, 16);
            this.lblID3v2_Genero.Name = "lblID3v2_Genero";
            this.lblID3v2_Genero.Size = new System.Drawing.Size(45, 13);
            this.lblID3v2_Genero.TabIndex = 7;
            this.lblID3v2_Genero.Text = "Genero:";
            // 
            // txtID3v2_NumPista
            // 
            this.txtID3v2_NumPista.Location = new System.Drawing.Point(70, 13);
            this.txtID3v2_NumPista.MaxLength = 9;
            this.txtID3v2_NumPista.Name = "txtID3v2_NumPista";
            this.txtID3v2_NumPista.ReadOnly = true;
            this.txtID3v2_NumPista.Size = new System.Drawing.Size(89, 20);
            this.txtID3v2_NumPista.TabIndex = 0;
            // 
            // cDatoCancion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.scDetalleCancion);
            this.Name = "cDatoCancion";
            this.Size = new System.Drawing.Size(885, 381);
            this.Load += new System.EventHandler(this.cDatoCancion_Load);
            this.gbModificado.ResumeLayout(false);
            this.gbModificado.PerformLayout();
            this.scDetalleCancion.Panel1.ResumeLayout(false);
            this.scDetalleCancion.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scDetalleCancion)).EndInit();
            this.scDetalleCancion.ResumeLayout(false);
            this.scDatosArchivo.Panel1.ResumeLayout(false);
            this.scDatosArchivo.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scDatosArchivo)).EndInit();
            this.scDatosArchivo.ResumeLayout(false);
            this.gbID3v1.ResumeLayout(false);
            this.gbID3v1.PerformLayout();
            this.gbID3v2.ResumeLayout(false);
            this.gbID3v2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtPista;
        private System.Windows.Forms.TextBox txtRuta;
        private System.Windows.Forms.Label lblRuta;
        private System.Windows.Forms.Label lblPista;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label lblArtista;
        private System.Windows.Forms.Label lblAlbum;
        private System.Windows.Forms.Label lblGenero;
        private System.Windows.Forms.ComboBox cmbGenero;
        private System.Windows.Forms.Label lblComentario;
        private System.Windows.Forms.Label lblAño;
        private System.Windows.Forms.TextBox txtAño;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtArtista;
        private System.Windows.Forms.TextBox txtAlbum;
        private System.Windows.Forms.TextBox txtComentario;
        private System.Windows.Forms.GroupBox gbModificado;
        private System.Windows.Forms.SplitContainer scDetalleCancion;
        private System.Windows.Forms.GroupBox gbID3v1;
        private System.Windows.Forms.TextBox txtID3v1_Comentario;
        private System.Windows.Forms.TextBox txtID3v1_Album;
        private System.Windows.Forms.Label lblID3v1_NumPista;
        private System.Windows.Forms.TextBox txtID3v1_Artista;
        private System.Windows.Forms.Label lblID3v1_Nombre;
        private System.Windows.Forms.TextBox txtID3v1_Nombre;
        private System.Windows.Forms.Label lblID3v1_Artista;
        private System.Windows.Forms.TextBox txtID3v1_Año;
        private System.Windows.Forms.Label lblID3v1_Album;
        private System.Windows.Forms.Label lblID3v1_Año;
        private System.Windows.Forms.Label lblID3v1_Comentario;
        private System.Windows.Forms.Label lblID3v1_Genero;
        private System.Windows.Forms.TextBox txtID3v1_NumPista;
        private System.Windows.Forms.SplitContainer scDatosArchivo;
        private System.Windows.Forms.GroupBox gbID3v2;
        private System.Windows.Forms.TextBox txtID3v2_Comentario;
        private System.Windows.Forms.TextBox txtID3v2_Album;
        private System.Windows.Forms.Label lblID3v2_NumPista;
        private System.Windows.Forms.TextBox txtID3v2_Artista;
        private System.Windows.Forms.Label lblID3v2_Nombre;
        private System.Windows.Forms.TextBox txtID3v2_Nombre;
        private System.Windows.Forms.Label lblID3v2_Artista;
        private System.Windows.Forms.TextBox txtID3v2_Año;
        private System.Windows.Forms.Label lblID3v2_Album;
        private System.Windows.Forms.Label lblID3v2_Año;
        private System.Windows.Forms.Label lblID3v2_Comentario;
        private System.Windows.Forms.Label lblID3v2_Genero;
        private System.Windows.Forms.TextBox txtID3v2_NumPista;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.TextBox txtID3v2_Genero;
        private System.Windows.Forms.TextBox txtID3v1_Genero;
    }
}
