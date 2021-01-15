namespace MonitorPaquetes
{
    partial class Default
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.MenuPrincipal = new System.Windows.Forms.MenuStrip();
            this.registrarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.soloUnoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.masivaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inicioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.desarrlladoresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.catalogoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.altaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bajaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.estadosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.catalogoToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.altaToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.reportesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.paquetesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.auditoriaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rechazadosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cerradosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mensualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.incidentesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mensualToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.semanalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cargaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.incidentesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.cargarExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.opcionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tipoDeEnvioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.acercaDeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TimerPaquete = new System.Windows.Forms.Timer(this.components);
            this.btnBorrar = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.tbcDatos = new System.Windows.Forms.TabControl();
            this.tapActivos = new System.Windows.Forms.TabPage();
            this.dgvPaquetes = new System.Windows.Forms.DataGridView();
            this.dcInsidente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcRDL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcPaquete = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcEstatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcRetroalimentado = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dcDocPru = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dcIdentificador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcObservaciones = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcNotaLibera = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tapAvisos = new System.Windows.Forms.TabPage();
            this.dgvAvisos = new System.Windows.Forms.DataGridView();
            this.gbDetalle = new System.Windows.Forms.GroupBox();
            this.txtPaquetes = new System.Windows.Forms.RichTextBox();
            this.txtObservaciones = new System.Windows.Forms.TextBox();
            this.txtIdentificador = new System.Windows.Forms.TextBox();
            this.ckbDocPru = new System.Windows.Forms.CheckBox();
            this.ckbRetro = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNotaLibera = new System.Windows.Forms.TextBox();
            this.txtEstado = new System.Windows.Forms.TextBox();
            this.lbEstado = new System.Windows.Forms.Label();
            this.txtecha = new System.Windows.Forms.TextBox();
            this.lbFecha = new System.Windows.Forms.Label();
            this.lbPaquetes = new System.Windows.Forms.Label();
            this.txtIncidentes = new System.Windows.Forms.RichTextBox();
            this.lbIncidentes = new System.Windows.Forms.Label();
            this.txtDesarrollador = new System.Windows.Forms.TextBox();
            this.lbDesarrollador = new System.Windows.Forms.Label();
            this.txtPaquete = new System.Windows.Forms.TextBox();
            this.txtRdl = new System.Windows.Forms.TextBox();
            this.cbDesarrollador = new System.Windows.Forms.ComboBox();
            this.dgvFecha_Notificacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcvIncidente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcPaquete = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtgArchivo_Notificacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcvDescripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MenuPrincipal.SuspendLayout();
            this.tbcDatos.SuspendLayout();
            this.tapActivos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPaquetes)).BeginInit();
            this.tapAvisos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAvisos)).BeginInit();
            this.gbDetalle.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuPrincipal
            // 
            this.MenuPrincipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.registrarToolStripMenuItem,
            this.inicioToolStripMenuItem,
            this.reportesToolStripMenuItem,
            this.incidentesToolStripMenuItem1,
            this.opcionesToolStripMenuItem,
            this.acercaDeToolStripMenuItem});
            this.MenuPrincipal.Location = new System.Drawing.Point(0, 0);
            this.MenuPrincipal.Name = "MenuPrincipal";
            this.MenuPrincipal.Size = new System.Drawing.Size(926, 24);
            this.MenuPrincipal.TabIndex = 0;
            this.MenuPrincipal.Text = "menuStrip1";
            // 
            // registrarToolStripMenuItem
            // 
            this.registrarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.soloUnoToolStripMenuItem,
            this.masivaToolStripMenuItem});
            this.registrarToolStripMenuItem.Name = "registrarToolStripMenuItem";
            this.registrarToolStripMenuItem.Size = new System.Drawing.Size(111, 20);
            this.registrarToolStripMenuItem.Text = "Registrar Paquete";
            // 
            // soloUnoToolStripMenuItem
            // 
            this.soloUnoToolStripMenuItem.Name = "soloUnoToolStripMenuItem";
            this.soloUnoToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.soloUnoToolStripMenuItem.Text = "Solo Uno";
            this.soloUnoToolStripMenuItem.Click += new System.EventHandler(this.soloUnoToolStripMenuItem_Click);
            // 
            // masivaToolStripMenuItem
            // 
            this.masivaToolStripMenuItem.Name = "masivaToolStripMenuItem";
            this.masivaToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.masivaToolStripMenuItem.Text = "Masiva";
            this.masivaToolStripMenuItem.Click += new System.EventHandler(this.masivaToolStripMenuItem_Click);
            // 
            // inicioToolStripMenuItem
            // 
            this.inicioToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.desarrlladoresToolStripMenuItem,
            this.estadosToolStripMenuItem});
            this.inicioToolStripMenuItem.Name = "inicioToolStripMenuItem";
            this.inicioToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.inicioToolStripMenuItem.Text = "Catalogos";
            // 
            // desarrlladoresToolStripMenuItem
            // 
            this.desarrlladoresToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.catalogoToolStripMenuItem,
            this.altaToolStripMenuItem,
            this.bajaToolStripMenuItem});
            this.desarrlladoresToolStripMenuItem.Name = "desarrlladoresToolStripMenuItem";
            this.desarrlladoresToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.desarrlladoresToolStripMenuItem.Text = "Desarrolladores";
            // 
            // catalogoToolStripMenuItem
            // 
            this.catalogoToolStripMenuItem.Name = "catalogoToolStripMenuItem";
            this.catalogoToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.catalogoToolStripMenuItem.Text = "Lista";
            this.catalogoToolStripMenuItem.Click += new System.EventHandler(this.catalogoToolStripMenuItem_Click);
            // 
            // altaToolStripMenuItem
            // 
            this.altaToolStripMenuItem.Name = "altaToolStripMenuItem";
            this.altaToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.altaToolStripMenuItem.Text = "Alta";
            this.altaToolStripMenuItem.Click += new System.EventHandler(this.altaToolStripMenuItem_Click);
            // 
            // bajaToolStripMenuItem
            // 
            this.bajaToolStripMenuItem.Name = "bajaToolStripMenuItem";
            this.bajaToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.bajaToolStripMenuItem.Text = "Baja";
            // 
            // estadosToolStripMenuItem
            // 
            this.estadosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.catalogoToolStripMenuItem1,
            this.altaToolStripMenuItem1});
            this.estadosToolStripMenuItem.Name = "estadosToolStripMenuItem";
            this.estadosToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.estadosToolStripMenuItem.Text = "Estados";
            // 
            // catalogoToolStripMenuItem1
            // 
            this.catalogoToolStripMenuItem1.Name = "catalogoToolStripMenuItem1";
            this.catalogoToolStripMenuItem1.Size = new System.Drawing.Size(98, 22);
            this.catalogoToolStripMenuItem1.Text = "Lista";
            this.catalogoToolStripMenuItem1.Click += new System.EventHandler(this.catalogoToolStripMenuItem1_Click);
            // 
            // altaToolStripMenuItem1
            // 
            this.altaToolStripMenuItem1.Name = "altaToolStripMenuItem1";
            this.altaToolStripMenuItem1.Size = new System.Drawing.Size(98, 22);
            this.altaToolStripMenuItem1.Text = "Alta";
            this.altaToolStripMenuItem1.Click += new System.EventHandler(this.altaToolStripMenuItem1_Click);
            // 
            // reportesToolStripMenuItem
            // 
            this.reportesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.paquetesToolStripMenuItem,
            this.incidentesToolStripMenuItem});
            this.reportesToolStripMenuItem.Name = "reportesToolStripMenuItem";
            this.reportesToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.reportesToolStripMenuItem.Text = "Reportes";
            // 
            // paquetesToolStripMenuItem
            // 
            this.paquetesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.auditoriaToolStripMenuItem,
            this.rechazadosToolStripMenuItem,
            this.cerradosToolStripMenuItem,
            this.mensualToolStripMenuItem});
            this.paquetesToolStripMenuItem.Name = "paquetesToolStripMenuItem";
            this.paquetesToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.paquetesToolStripMenuItem.Text = "Paquetes";
            // 
            // auditoriaToolStripMenuItem
            // 
            this.auditoriaToolStripMenuItem.Name = "auditoriaToolStripMenuItem";
            this.auditoriaToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.auditoriaToolStripMenuItem.Text = "Auditoria";
            this.auditoriaToolStripMenuItem.Click += new System.EventHandler(this.auditoriaToolStripMenuItem_Click);
            // 
            // rechazadosToolStripMenuItem
            // 
            this.rechazadosToolStripMenuItem.Name = "rechazadosToolStripMenuItem";
            this.rechazadosToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.rechazadosToolStripMenuItem.Text = "Rechazados";
            this.rechazadosToolStripMenuItem.Click += new System.EventHandler(this.rechazadosToolStripMenuItem_Click);
            // 
            // cerradosToolStripMenuItem
            // 
            this.cerradosToolStripMenuItem.Name = "cerradosToolStripMenuItem";
            this.cerradosToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.cerradosToolStripMenuItem.Text = "Cerrados";
            this.cerradosToolStripMenuItem.Click += new System.EventHandler(this.cerradosToolStripMenuItem_Click);
            // 
            // mensualToolStripMenuItem
            // 
            this.mensualToolStripMenuItem.Name = "mensualToolStripMenuItem";
            this.mensualToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.mensualToolStripMenuItem.Text = "Mensual";
            this.mensualToolStripMenuItem.Click += new System.EventHandler(this.mensualToolStripMenuItem_Click);
            // 
            // incidentesToolStripMenuItem
            // 
            this.incidentesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mensualToolStripMenuItem1,
            this.semanalToolStripMenuItem,
            this.cargaToolStripMenuItem});
            this.incidentesToolStripMenuItem.Name = "incidentesToolStripMenuItem";
            this.incidentesToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.incidentesToolStripMenuItem.Text = "Incidentes";
            // 
            // mensualToolStripMenuItem1
            // 
            this.mensualToolStripMenuItem1.Name = "mensualToolStripMenuItem1";
            this.mensualToolStripMenuItem1.Size = new System.Drawing.Size(119, 22);
            this.mensualToolStripMenuItem1.Text = "Mensual";
            this.mensualToolStripMenuItem1.Click += new System.EventHandler(this.mensualToolStripMenuItem1_Click);
            // 
            // semanalToolStripMenuItem
            // 
            this.semanalToolStripMenuItem.Name = "semanalToolStripMenuItem";
            this.semanalToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.semanalToolStripMenuItem.Text = "Semanal";
            this.semanalToolStripMenuItem.Click += new System.EventHandler(this.semanalToolStripMenuItem_Click);
            // 
            // cargaToolStripMenuItem
            // 
            this.cargaToolStripMenuItem.Name = "cargaToolStripMenuItem";
            this.cargaToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.cargaToolStripMenuItem.Text = "Carga";
            // 
            // incidentesToolStripMenuItem1
            // 
            this.incidentesToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cargarExcelToolStripMenuItem});
            this.incidentesToolStripMenuItem1.Name = "incidentesToolStripMenuItem1";
            this.incidentesToolStripMenuItem1.Size = new System.Drawing.Size(73, 20);
            this.incidentesToolStripMenuItem1.Text = "Incidentes";
            // 
            // cargarExcelToolStripMenuItem
            // 
            this.cargarExcelToolStripMenuItem.Name = "cargarExcelToolStripMenuItem";
            this.cargarExcelToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.cargarExcelToolStripMenuItem.Text = "Cargar Archivo";
            this.cargarExcelToolStripMenuItem.Click += new System.EventHandler(this.cargarExcelToolStripMenuItem_Click);
            // 
            // opcionesToolStripMenuItem
            // 
            this.opcionesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tipoDeEnvioToolStripMenuItem});
            this.opcionesToolStripMenuItem.Name = "opcionesToolStripMenuItem";
            this.opcionesToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.opcionesToolStripMenuItem.Text = "Opciones";
            // 
            // tipoDeEnvioToolStripMenuItem
            // 
            this.tipoDeEnvioToolStripMenuItem.Name = "tipoDeEnvioToolStripMenuItem";
            this.tipoDeEnvioToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.tipoDeEnvioToolStripMenuItem.Text = "Ver Configuraciones";
            this.tipoDeEnvioToolStripMenuItem.Click += new System.EventHandler(this.tipoDeEnvioToolStripMenuItem_Click);
            // 
            // acercaDeToolStripMenuItem
            // 
            this.acercaDeToolStripMenuItem.Name = "acercaDeToolStripMenuItem";
            this.acercaDeToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.acercaDeToolStripMenuItem.Text = "Acerca";
            this.acercaDeToolStripMenuItem.Click += new System.EventHandler(this.acercaDeToolStripMenuItem_Click);
            // 
            // TimerPaquete
            // 
            this.TimerPaquete.Enabled = true;
            this.TimerPaquete.Interval = 60000;
            this.TimerPaquete.Tick += new System.EventHandler(this.TimerPaquete_Tick);
            // 
            // btnBorrar
            // 
            this.btnBorrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBorrar.Enabled = false;
            this.btnBorrar.Location = new System.Drawing.Point(829, 163);
            this.btnBorrar.Name = "btnBorrar";
            this.btnBorrar.Size = new System.Drawing.Size(86, 28);
            this.btnBorrar.TabIndex = 23;
            this.btnBorrar.Text = "Borrar";
            this.btnBorrar.UseVisualStyleBackColor = true;
            this.btnBorrar.Click += new System.EventHandler(this.btnBorrar_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditar.Enabled = false;
            this.btnEditar.Location = new System.Drawing.Point(737, 163);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(86, 28);
            this.btnEditar.TabIndex = 22;
            this.btnEditar.Text = "Editar";
            this.btnEditar.UseVisualStyleBackColor = true;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // tbcDatos
            // 
            this.tbcDatos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbcDatos.Controls.Add(this.tapActivos);
            this.tbcDatos.Controls.Add(this.tapAvisos);
            this.tbcDatos.Location = new System.Drawing.Point(12, 173);
            this.tbcDatos.Name = "tbcDatos";
            this.tbcDatos.SelectedIndex = 0;
            this.tbcDatos.Size = new System.Drawing.Size(902, 205);
            this.tbcDatos.TabIndex = 21;
            // 
            // tapActivos
            // 
            this.tapActivos.Controls.Add(this.dgvPaquetes);
            this.tapActivos.Location = new System.Drawing.Point(4, 22);
            this.tapActivos.Name = "tapActivos";
            this.tapActivos.Padding = new System.Windows.Forms.Padding(3);
            this.tapActivos.Size = new System.Drawing.Size(894, 179);
            this.tapActivos.TabIndex = 0;
            this.tapActivos.Text = "Activos";
            // 
            // dgvPaquetes
            // 
            this.dgvPaquetes.AllowUserToAddRows = false;
            this.dgvPaquetes.AllowUserToDeleteRows = false;
            this.dgvPaquetes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPaquetes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPaquetes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPaquetes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPaquetes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dcInsidente,
            this.dcRDL,
            this.dcPaquete,
            this.dcEstatus,
            this.dcRetroalimentado,
            this.dcDocPru,
            this.dcIdentificador,
            this.dcObservaciones,
            this.dcNotaLibera});
            this.dgvPaquetes.Location = new System.Drawing.Point(6, 6);
            this.dgvPaquetes.Name = "dgvPaquetes";
            this.dgvPaquetes.Size = new System.Drawing.Size(882, 167);
            this.dgvPaquetes.TabIndex = 1;
            this.dgvPaquetes.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvPaquetes_CellBeginEdit);
            this.dgvPaquetes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPaquetes_CellClick);
            this.dgvPaquetes.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPaquetes_CellEndEdit);
            this.dgvPaquetes.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvPaquetes_CellFormatting);
            // 
            // dcInsidente
            // 
            this.dcInsidente.DataPropertyName = "Incidente";
            this.dcInsidente.HeaderText = "Id Incidente";
            this.dcInsidente.Name = "dcInsidente";
            this.dcInsidente.ReadOnly = true;
            this.dcInsidente.Width = 88;
            // 
            // dcRDL
            // 
            this.dcRDL.DataPropertyName = "Rdl";
            this.dcRDL.HeaderText = "RDL";
            this.dcRDL.Name = "dcRDL";
            this.dcRDL.ReadOnly = true;
            this.dcRDL.Width = 54;
            // 
            // dcPaquete
            // 
            this.dcPaquete.DataPropertyName = "Paquete";
            this.dcPaquete.HeaderText = "Paquete";
            this.dcPaquete.Name = "dcPaquete";
            this.dcPaquete.Width = 72;
            // 
            // dcEstatus
            // 
            this.dcEstatus.DataPropertyName = "Estado";
            this.dcEstatus.HeaderText = "Estatus";
            this.dcEstatus.Name = "dcEstatus";
            this.dcEstatus.ReadOnly = true;
            this.dcEstatus.Width = 67;
            // 
            // dcRetroalimentado
            // 
            this.dcRetroalimentado.DataPropertyName = "Retroalimentacion";
            this.dcRetroalimentado.HeaderText = "Retroalimentado";
            this.dcRetroalimentado.Name = "dcRetroalimentado";
            this.dcRetroalimentado.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dcRetroalimentado.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dcRetroalimentado.Width = 109;
            // 
            // dcDocPru
            // 
            this.dcDocPru.DataPropertyName = "DOC_PRU";
            this.dcDocPru.HeaderText = "DOC PRU";
            this.dcDocPru.Name = "dcDocPru";
            this.dcDocPru.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dcDocPru.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dcDocPru.Width = 81;
            // 
            // dcIdentificador
            // 
            this.dcIdentificador.DataPropertyName = "Descripcion";
            this.dcIdentificador.HeaderText = "Identificador";
            this.dcIdentificador.Name = "dcIdentificador";
            this.dcIdentificador.Width = 90;
            // 
            // dcObservaciones
            // 
            this.dcObservaciones.DataPropertyName = "Observaciones";
            this.dcObservaciones.HeaderText = "Observaciones";
            this.dcObservaciones.Name = "dcObservaciones";
            this.dcObservaciones.Width = 103;
            // 
            // dcNotaLibera
            // 
            this.dcNotaLibera.DataPropertyName = "NotaLibera";
            this.dcNotaLibera.HeaderText = "Nota para liberar";
            this.dcNotaLibera.Name = "dcNotaLibera";
            this.dcNotaLibera.Visible = false;
            this.dcNotaLibera.Width = 110;
            // 
            // tapAvisos
            // 
            this.tapAvisos.BackColor = System.Drawing.Color.Red;
            this.tapAvisos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tapAvisos.Controls.Add(this.dgvAvisos);
            this.tapAvisos.Location = new System.Drawing.Point(4, 22);
            this.tapAvisos.Name = "tapAvisos";
            this.tapAvisos.Padding = new System.Windows.Forms.Padding(3);
            this.tapAvisos.Size = new System.Drawing.Size(894, 179);
            this.tapAvisos.TabIndex = 1;
            this.tapAvisos.Text = "Avisos";
            // 
            // dgvAvisos
            // 
            this.dgvAvisos.AllowUserToAddRows = false;
            this.dgvAvisos.AllowUserToDeleteRows = false;
            this.dgvAvisos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvAvisos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvAvisos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAvisos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvFecha_Notificacion,
            this.dcvIncidente,
            this.dgcPaquete,
            this.dtgArchivo_Notificacion,
            this.dcvDescripcion});
            this.dgvAvisos.Location = new System.Drawing.Point(6, 6);
            this.dgvAvisos.Name = "dgvAvisos";
            this.dgvAvisos.ReadOnly = true;
            this.dgvAvisos.Size = new System.Drawing.Size(878, 163);
            this.dgvAvisos.TabIndex = 0;
            this.dgvAvisos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAvisos_CellClick);
            this.dgvAvisos.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvAvisos_CellFormatting);
            // 
            // gbDetalle
            // 
            this.gbDetalle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDetalle.Controls.Add(this.txtPaquetes);
            this.gbDetalle.Controls.Add(this.txtObservaciones);
            this.gbDetalle.Controls.Add(this.txtIdentificador);
            this.gbDetalle.Controls.Add(this.ckbDocPru);
            this.gbDetalle.Controls.Add(this.ckbRetro);
            this.gbDetalle.Controls.Add(this.label1);
            this.gbDetalle.Controls.Add(this.txtNotaLibera);
            this.gbDetalle.Controls.Add(this.txtEstado);
            this.gbDetalle.Controls.Add(this.lbEstado);
            this.gbDetalle.Controls.Add(this.txtecha);
            this.gbDetalle.Controls.Add(this.lbFecha);
            this.gbDetalle.Controls.Add(this.lbPaquetes);
            this.gbDetalle.Controls.Add(this.txtIncidentes);
            this.gbDetalle.Controls.Add(this.lbIncidentes);
            this.gbDetalle.Controls.Add(this.txtDesarrollador);
            this.gbDetalle.Controls.Add(this.lbDesarrollador);
            this.gbDetalle.Controls.Add(this.txtPaquete);
            this.gbDetalle.Controls.Add(this.txtRdl);
            this.gbDetalle.Controls.Add(this.cbDesarrollador);
            this.gbDetalle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbDetalle.Location = new System.Drawing.Point(12, 22);
            this.gbDetalle.Name = "gbDetalle";
            this.gbDetalle.Size = new System.Drawing.Size(902, 145);
            this.gbDetalle.TabIndex = 20;
            this.gbDetalle.TabStop = false;
            this.gbDetalle.Text = "Detalle";
            // 
            // txtPaquetes
            // 
            this.txtPaquetes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPaquetes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPaquetes.Location = new System.Drawing.Point(678, 26);
            this.txtPaquetes.Name = "txtPaquetes";
            this.txtPaquetes.ReadOnly = true;
            this.txtPaquetes.Size = new System.Drawing.Size(218, 111);
            this.txtPaquetes.TabIndex = 9;
            this.txtPaquetes.Text = "";
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtObservaciones.Location = new System.Drawing.Point(547, 68);
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.ReadOnly = true;
            this.txtObservaciones.Size = new System.Drawing.Size(171, 20);
            this.txtObservaciones.TabIndex = 22;
            this.txtObservaciones.Visible = false;
            // 
            // txtIdentificador
            // 
            this.txtIdentificador.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIdentificador.Location = new System.Drawing.Point(547, 42);
            this.txtIdentificador.Name = "txtIdentificador";
            this.txtIdentificador.ReadOnly = true;
            this.txtIdentificador.Size = new System.Drawing.Size(171, 20);
            this.txtIdentificador.TabIndex = 21;
            this.txtIdentificador.Visible = false;
            // 
            // ckbDocPru
            // 
            this.ckbDocPru.AutoSize = true;
            this.ckbDocPru.Enabled = false;
            this.ckbDocPru.Location = new System.Drawing.Point(576, 22);
            this.ckbDocPru.Name = "ckbDocPru";
            this.ckbDocPru.Size = new System.Drawing.Size(15, 14);
            this.ckbDocPru.TabIndex = 20;
            this.ckbDocPru.UseVisualStyleBackColor = true;
            this.ckbDocPru.Visible = false;
            // 
            // ckbRetro
            // 
            this.ckbRetro.AutoSize = true;
            this.ckbRetro.Enabled = false;
            this.ckbRetro.Location = new System.Drawing.Point(555, 22);
            this.ckbRetro.Name = "ckbRetro";
            this.ckbRetro.Size = new System.Drawing.Size(15, 14);
            this.ckbRetro.TabIndex = 19;
            this.ckbRetro.UseVisualStyleBackColor = true;
            this.ckbRetro.Visible = false;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 31);
            this.label1.TabIndex = 18;
            this.label1.Text = "Nota liberación:";
            // 
            // txtNotaLibera
            // 
            this.txtNotaLibera.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNotaLibera.Location = new System.Drawing.Point(87, 79);
            this.txtNotaLibera.Multiline = true;
            this.txtNotaLibera.Name = "txtNotaLibera";
            this.txtNotaLibera.ReadOnly = true;
            this.txtNotaLibera.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNotaLibera.Size = new System.Drawing.Size(453, 58);
            this.txtNotaLibera.TabIndex = 17;
            this.txtNotaLibera.Leave += new System.EventHandler(this.txtNotaLibera_Leave);
            // 
            // txtEstado
            // 
            this.txtEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEstado.Location = new System.Drawing.Point(294, 52);
            this.txtEstado.Name = "txtEstado";
            this.txtEstado.ReadOnly = true;
            this.txtEstado.Size = new System.Drawing.Size(244, 20);
            this.txtEstado.TabIndex = 13;
            // 
            // lbEstado
            // 
            this.lbEstado.AutoSize = true;
            this.lbEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEstado.Location = new System.Drawing.Point(254, 55);
            this.lbEstado.Name = "lbEstado";
            this.lbEstado.Size = new System.Drawing.Size(43, 13);
            this.lbEstado.TabIndex = 12;
            this.lbEstado.Text = "Estado:";
            // 
            // txtecha
            // 
            this.txtecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtecha.Location = new System.Drawing.Point(87, 52);
            this.txtecha.Name = "txtecha";
            this.txtecha.ReadOnly = true;
            this.txtecha.Size = new System.Drawing.Size(164, 20);
            this.txtecha.TabIndex = 11;
            // 
            // lbFecha
            // 
            this.lbFecha.AutoSize = true;
            this.lbFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFecha.Location = new System.Drawing.Point(8, 55);
            this.lbFecha.Name = "lbFecha";
            this.lbFecha.Size = new System.Drawing.Size(64, 13);
            this.lbFecha.TabIndex = 10;
            this.lbFecha.Text = "F. InstConc:";
            // 
            // lbPaquetes
            // 
            this.lbPaquetes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbPaquetes.AutoSize = true;
            this.lbPaquetes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPaquetes.Location = new System.Drawing.Point(721, 10);
            this.lbPaquetes.Name = "lbPaquetes";
            this.lbPaquetes.Size = new System.Drawing.Size(135, 13);
            this.lbPaquetes.TabIndex = 8;
            this.lbPaquetes.Text = "Paquete(s) Relacionado(s):";
            // 
            // txtIncidentes
            // 
            this.txtIncidentes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIncidentes.Location = new System.Drawing.Point(87, 26);
            this.txtIncidentes.Name = "txtIncidentes";
            this.txtIncidentes.ReadOnly = true;
            this.txtIncidentes.Size = new System.Drawing.Size(172, 22);
            this.txtIncidentes.TabIndex = 7;
            this.txtIncidentes.Text = "";
            // 
            // lbIncidentes
            // 
            this.lbIncidentes.AutoSize = true;
            this.lbIncidentes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbIncidentes.Location = new System.Drawing.Point(10, 26);
            this.lbIncidentes.Name = "lbIncidentes";
            this.lbIncidentes.Size = new System.Drawing.Size(54, 13);
            this.lbIncidentes.TabIndex = 6;
            this.lbIncidentes.Text = "Incidente:";
            // 
            // txtDesarrollador
            // 
            this.txtDesarrollador.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDesarrollador.Location = new System.Drawing.Point(351, 24);
            this.txtDesarrollador.Name = "txtDesarrollador";
            this.txtDesarrollador.ReadOnly = true;
            this.txtDesarrollador.Size = new System.Drawing.Size(189, 20);
            this.txtDesarrollador.TabIndex = 5;
            // 
            // lbDesarrollador
            // 
            this.lbDesarrollador.AutoSize = true;
            this.lbDesarrollador.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDesarrollador.Location = new System.Drawing.Point(273, 26);
            this.lbDesarrollador.Name = "lbDesarrollador";
            this.lbDesarrollador.Size = new System.Drawing.Size(72, 13);
            this.lbDesarrollador.TabIndex = 4;
            this.lbDesarrollador.Text = "Desarrollador:";
            // 
            // txtPaquete
            // 
            this.txtPaquete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPaquete.Location = new System.Drawing.Point(597, 93);
            this.txtPaquete.Name = "txtPaquete";
            this.txtPaquete.ReadOnly = true;
            this.txtPaquete.Size = new System.Drawing.Size(121, 20);
            this.txtPaquete.TabIndex = 3;
            this.txtPaquete.Visible = false;
            // 
            // txtRdl
            // 
            this.txtRdl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRdl.Location = new System.Drawing.Point(547, 94);
            this.txtRdl.Name = "txtRdl";
            this.txtRdl.ReadOnly = true;
            this.txtRdl.Size = new System.Drawing.Size(44, 20);
            this.txtRdl.TabIndex = 1;
            this.txtRdl.Visible = false;
            // 
            // cbDesarrollador
            // 
            this.cbDesarrollador.FormattingEnabled = true;
            this.cbDesarrollador.Location = new System.Drawing.Point(351, 24);
            this.cbDesarrollador.Name = "cbDesarrollador";
            this.cbDesarrollador.Size = new System.Drawing.Size(189, 24);
            this.cbDesarrollador.TabIndex = 16;
            this.cbDesarrollador.Visible = false;
            // 
            // dgvFecha_Notificacion
            // 
            this.dgvFecha_Notificacion.DataPropertyName = "Fecha_Notificacion";
            this.dgvFecha_Notificacion.HeaderText = "Fecha Notificacion";
            this.dgvFecha_Notificacion.Name = "dgvFecha_Notificacion";
            this.dgvFecha_Notificacion.ReadOnly = true;
            this.dgvFecha_Notificacion.Width = 111;
            // 
            // dcvIncidente
            // 
            this.dcvIncidente.DataPropertyName = "Incidente";
            this.dcvIncidente.HeaderText = "Incidente";
            this.dcvIncidente.Name = "dcvIncidente";
            this.dcvIncidente.ReadOnly = true;
            this.dcvIncidente.Width = 76;
            // 
            // dgcPaquete
            // 
            this.dgcPaquete.DataPropertyName = "Paquete";
            this.dgcPaquete.HeaderText = "Paquete";
            this.dgcPaquete.Name = "dgcPaquete";
            this.dgcPaquete.ReadOnly = true;
            this.dgcPaquete.Width = 72;
            // 
            // dtgArchivo_Notificacion
            // 
            this.dtgArchivo_Notificacion.DataPropertyName = "Archivo_Notificacion";
            this.dtgArchivo_Notificacion.HeaderText = "Archivo Notificacion";
            this.dtgArchivo_Notificacion.Name = "dtgArchivo_Notificacion";
            this.dtgArchivo_Notificacion.ReadOnly = true;
            this.dtgArchivo_Notificacion.Width = 116;
            // 
            // dcvDescripcion
            // 
            this.dcvDescripcion.DataPropertyName = "Descripcion";
            this.dcvDescripcion.HeaderText = "Estatus";
            this.dcvDescripcion.Name = "dcvDescripcion";
            this.dcvDescripcion.ReadOnly = true;
            this.dcvDescripcion.Width = 67;
            // 
            // Default
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(926, 375);
            this.Controls.Add(this.btnBorrar);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.tbcDatos);
            this.Controls.Add(this.gbDetalle);
            this.Controls.Add(this.MenuPrincipal);
            this.MainMenuStrip = this.MenuPrincipal;
            this.Name = "Default";
            this.Text = "Principal";
            this.Load += new System.EventHandler(this.FrmPrincipal_Load);
            this.MenuPrincipal.ResumeLayout(false);
            this.MenuPrincipal.PerformLayout();
            this.tbcDatos.ResumeLayout(false);
            this.tapActivos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPaquetes)).EndInit();
            this.tapAvisos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAvisos)).EndInit();
            this.gbDetalle.ResumeLayout(false);
            this.gbDetalle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MenuPrincipal;
        private System.Windows.Forms.ToolStripMenuItem inicioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem desarrlladoresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem catalogoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem altaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bajaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem estadosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem catalogoToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem altaToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem registrarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem paquetesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem incidentesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem opcionesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tipoDeEnvioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem acercaDeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem soloUnoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem masivaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rechazadosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cerradosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mensualToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mensualToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem semanalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cargaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem incidentesToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem cargarExcelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem auditoriaToolStripMenuItem;
        private System.Windows.Forms.Timer TimerPaquete;
        private System.Windows.Forms.Button btnBorrar;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.TabControl tbcDatos;
        private System.Windows.Forms.TabPage tapActivos;
        private System.Windows.Forms.DataGridView dgvPaquetes;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcInsidente;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcRDL;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcPaquete;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcEstatus;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dcRetroalimentado;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dcDocPru;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcIdentificador;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcObservaciones;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcNotaLibera;
        private System.Windows.Forms.TabPage tapAvisos;
        private System.Windows.Forms.GroupBox gbDetalle;
        private System.Windows.Forms.TextBox txtObservaciones;
        private System.Windows.Forms.TextBox txtIdentificador;
        private System.Windows.Forms.CheckBox ckbDocPru;
        private System.Windows.Forms.CheckBox ckbRetro;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNotaLibera;
        private System.Windows.Forms.TextBox txtEstado;
        private System.Windows.Forms.Label lbEstado;
        private System.Windows.Forms.TextBox txtecha;
        private System.Windows.Forms.Label lbFecha;
        private System.Windows.Forms.RichTextBox txtPaquetes;
        private System.Windows.Forms.Label lbPaquetes;
        private System.Windows.Forms.RichTextBox txtIncidentes;
        private System.Windows.Forms.Label lbIncidentes;
        private System.Windows.Forms.TextBox txtDesarrollador;
        private System.Windows.Forms.Label lbDesarrollador;
        private System.Windows.Forms.TextBox txtPaquete;
        private System.Windows.Forms.TextBox txtRdl;
        private System.Windows.Forms.ComboBox cbDesarrollador;
        private System.Windows.Forms.DataGridView dgvAvisos;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvFecha_Notificacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcvIncidente;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcPaquete;
        private System.Windows.Forms.DataGridViewTextBoxColumn dtgArchivo_Notificacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcvDescripcion;
    }
}