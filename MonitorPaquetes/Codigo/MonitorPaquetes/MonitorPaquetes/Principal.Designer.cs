namespace MonitorPaquetes
{
    partial class FrmPrincipal
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
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
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrincipal));
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
            this.dgvPaquetes = new System.Windows.Forms.DataGridView();
            this.gbDetalle = new System.Windows.Forms.GroupBox();
            this.btnBorrar = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.txtEstado = new System.Windows.Forms.TextBox();
            this.lbEstado = new System.Windows.Forms.Label();
            this.txtecha = new System.Windows.Forms.TextBox();
            this.lbFecha = new System.Windows.Forms.Label();
            this.txtPaquetes = new System.Windows.Forms.RichTextBox();
            this.lbPaquetes = new System.Windows.Forms.Label();
            this.txtIncidentes = new System.Windows.Forms.RichTextBox();
            this.lbIncidentes = new System.Windows.Forms.Label();
            this.txtDesarrollador = new System.Windows.Forms.TextBox();
            this.lbDesarrollador = new System.Windows.Forms.Label();
            this.txtPaquete = new System.Windows.Forms.TextBox();
            this.lbPaquete = new System.Windows.Forms.Label();
            this.txtRdl = new System.Windows.Forms.TextBox();
            this.lbRdl = new System.Windows.Forms.Label();
            this.cbDesarrollador = new System.Windows.Forms.ComboBox();
            this.gpAvisos = new System.Windows.Forms.GroupBox();
            this.dgvAvisos = new System.Windows.Forms.DataGridView();
            this.MenuPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPaquetes)).BeginInit();
            this.gbDetalle.SuspendLayout();
            this.gpAvisos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAvisos)).BeginInit();
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
            // dgvPaquetes
            // 
            this.dgvPaquetes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPaquetes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvPaquetes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPaquetes.Location = new System.Drawing.Point(12, 27);
            this.dgvPaquetes.Name = "dgvPaquetes";
            this.dgvPaquetes.Size = new System.Drawing.Size(350, 415);
            this.dgvPaquetes.TabIndex = 1;
            this.dgvPaquetes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPaquetes_CellClick);
            this.dgvPaquetes.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPaquetes_CellContentDoubleClick);
            // 
            // gbDetalle
            // 
            this.gbDetalle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDetalle.Controls.Add(this.btnBorrar);
            this.gbDetalle.Controls.Add(this.btnEditar);
            this.gbDetalle.Controls.Add(this.txtEstado);
            this.gbDetalle.Controls.Add(this.lbEstado);
            this.gbDetalle.Controls.Add(this.txtecha);
            this.gbDetalle.Controls.Add(this.lbFecha);
            this.gbDetalle.Controls.Add(this.txtPaquetes);
            this.gbDetalle.Controls.Add(this.lbPaquetes);
            this.gbDetalle.Controls.Add(this.txtIncidentes);
            this.gbDetalle.Controls.Add(this.lbIncidentes);
            this.gbDetalle.Controls.Add(this.txtDesarrollador);
            this.gbDetalle.Controls.Add(this.lbDesarrollador);
            this.gbDetalle.Controls.Add(this.txtPaquete);
            this.gbDetalle.Controls.Add(this.lbPaquete);
            this.gbDetalle.Controls.Add(this.txtRdl);
            this.gbDetalle.Controls.Add(this.lbRdl);
            this.gbDetalle.Controls.Add(this.cbDesarrollador);
            this.gbDetalle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbDetalle.Location = new System.Drawing.Point(367, 32);
            this.gbDetalle.Name = "gbDetalle";
            this.gbDetalle.Size = new System.Drawing.Size(546, 246);
            this.gbDetalle.TabIndex = 2;
            this.gbDetalle.TabStop = false;
            this.gbDetalle.Text = "Detalle";
            // 
            // btnBorrar
            // 
            this.btnBorrar.Enabled = false;
            this.btnBorrar.Location = new System.Drawing.Point(11, 213);
            this.btnBorrar.Name = "btnBorrar";
            this.btnBorrar.Size = new System.Drawing.Size(86, 28);
            this.btnBorrar.TabIndex = 17;
            this.btnBorrar.Text = "Borrar";
            this.btnBorrar.UseVisualStyleBackColor = true;
            this.btnBorrar.Click += new System.EventHandler(this.btnBorrar_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.Enabled = false;
            this.btnEditar.Location = new System.Drawing.Point(454, 212);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(86, 28);
            this.btnEditar.TabIndex = 15;
            this.btnEditar.Text = "Editar";
            this.btnEditar.UseVisualStyleBackColor = true;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // txtEstado
            // 
            this.txtEstado.Enabled = false;
            this.txtEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEstado.Location = new System.Drawing.Point(294, 52);
            this.txtEstado.Name = "txtEstado";
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
            this.txtecha.Enabled = false;
            this.txtecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtecha.Location = new System.Drawing.Point(87, 52);
            this.txtecha.Name = "txtecha";
            this.txtecha.Size = new System.Drawing.Size(164, 20);
            this.txtecha.TabIndex = 11;
            // 
            // lbFecha
            // 
            this.lbFecha.AutoSize = true;
            this.lbFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFecha.Location = new System.Drawing.Point(8, 55);
            this.lbFecha.Name = "lbFecha";
            this.lbFecha.Size = new System.Drawing.Size(82, 13);
            this.lbFecha.TabIndex = 10;
            this.lbFecha.Text = "Fecha Registro:";
            // 
            // txtPaquetes
            // 
            this.txtPaquetes.Enabled = false;
            this.txtPaquetes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPaquetes.Location = new System.Drawing.Point(284, 95);
            this.txtPaquetes.Name = "txtPaquetes";
            this.txtPaquetes.Size = new System.Drawing.Size(256, 111);
            this.txtPaquetes.TabIndex = 9;
            this.txtPaquetes.Text = "";
            // 
            // lbPaquetes
            // 
            this.lbPaquetes.AutoSize = true;
            this.lbPaquetes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPaquetes.Location = new System.Drawing.Point(281, 79);
            this.lbPaquetes.Name = "lbPaquetes";
            this.lbPaquetes.Size = new System.Drawing.Size(135, 13);
            this.lbPaquetes.TabIndex = 8;
            this.lbPaquetes.Text = "Paquete(s) Relacionado(s):";
            // 
            // txtIncidentes
            // 
            this.txtIncidentes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIncidentes.Location = new System.Drawing.Point(9, 95);
            this.txtIncidentes.Name = "txtIncidentes";
            this.txtIncidentes.ReadOnly = true;
            this.txtIncidentes.Size = new System.Drawing.Size(238, 111);
            this.txtIncidentes.TabIndex = 7;
            this.txtIncidentes.Text = "";
            // 
            // lbIncidentes
            // 
            this.lbIncidentes.AutoSize = true;
            this.lbIncidentes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbIncidentes.Location = new System.Drawing.Point(6, 79);
            this.lbIncidentes.Name = "lbIncidentes";
            this.lbIncidentes.Size = new System.Drawing.Size(139, 13);
            this.lbIncidentes.TabIndex = 6;
            this.lbIncidentes.Text = "Incidente(s) Relacionado(s):";
            // 
            // txtDesarrollador
            // 
            this.txtDesarrollador.Enabled = false;
            this.txtDesarrollador.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDesarrollador.Location = new System.Drawing.Point(351, 24);
            this.txtDesarrollador.Name = "txtDesarrollador";
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
            this.txtPaquete.Enabled = false;
            this.txtPaquete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPaquete.Location = new System.Drawing.Point(134, 23);
            this.txtPaquete.Name = "txtPaquete";
            this.txtPaquete.Size = new System.Drawing.Size(132, 20);
            this.txtPaquete.TabIndex = 3;
            // 
            // lbPaquete
            // 
            this.lbPaquete.AutoSize = true;
            this.lbPaquete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPaquete.Location = new System.Drawing.Point(84, 26);
            this.lbPaquete.Name = "lbPaquete";
            this.lbPaquete.Size = new System.Drawing.Size(50, 13);
            this.lbPaquete.TabIndex = 2;
            this.lbPaquete.Text = "Paquete:";
            // 
            // txtRdl
            // 
            this.txtRdl.Enabled = false;
            this.txtRdl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRdl.Location = new System.Drawing.Point(32, 23);
            this.txtRdl.Name = "txtRdl";
            this.txtRdl.Size = new System.Drawing.Size(47, 20);
            this.txtRdl.TabIndex = 1;
            // 
            // lbRdl
            // 
            this.lbRdl.AutoSize = true;
            this.lbRdl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRdl.Location = new System.Drawing.Point(6, 26);
            this.lbRdl.Name = "lbRdl";
            this.lbRdl.Size = new System.Drawing.Size(26, 13);
            this.lbRdl.TabIndex = 0;
            this.lbRdl.Text = "Rdl:";
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
            // gpAvisos
            // 
            this.gpAvisos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gpAvisos.Controls.Add(this.dgvAvisos);
            this.gpAvisos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpAvisos.Location = new System.Drawing.Point(368, 284);
            this.gpAvisos.Name = "gpAvisos";
            this.gpAvisos.Size = new System.Drawing.Size(546, 158);
            this.gpAvisos.TabIndex = 3;
            this.gpAvisos.TabStop = false;
            this.gpAvisos.Text = "Avisos";
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
            this.dgvAvisos.Location = new System.Drawing.Point(6, 19);
            this.dgvAvisos.Name = "dgvAvisos";
            this.dgvAvisos.ReadOnly = true;
            this.dgvAvisos.Size = new System.Drawing.Size(534, 133);
            this.dgvAvisos.TabIndex = 0;
            this.dgvAvisos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAvisos_CellContentClick);
            // 
            // FrmPrincipal
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(926, 456);
            this.Controls.Add(this.gpAvisos);
            this.Controls.Add(this.gbDetalle);
            this.Controls.Add(this.dgvPaquetes);
            this.Controls.Add(this.MenuPrincipal);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MenuPrincipal;
            this.Name = "FrmPrincipal";
            this.Text = "Principal";
            this.Load += new System.EventHandler(this.FrmPrincipal_Load);
            this.MenuPrincipal.ResumeLayout(false);
            this.MenuPrincipal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPaquetes)).EndInit();
            this.gbDetalle.ResumeLayout(false);
            this.gbDetalle.PerformLayout();
            this.gpAvisos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAvisos)).EndInit();
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
        private System.Windows.Forms.Timer TimerPaquete;
        private System.Windows.Forms.DataGridView dgvPaquetes;
        private System.Windows.Forms.ToolStripMenuItem opcionesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tipoDeEnvioToolStripMenuItem;
        private System.Windows.Forms.GroupBox gbDetalle;
        private System.Windows.Forms.TextBox txtRdl;
        private System.Windows.Forms.Label lbRdl;
        private System.Windows.Forms.GroupBox gpAvisos;
        private System.Windows.Forms.DataGridView dgvAvisos;
        private System.Windows.Forms.RichTextBox txtPaquetes;
        private System.Windows.Forms.Label lbPaquetes;
        private System.Windows.Forms.RichTextBox txtIncidentes;
        private System.Windows.Forms.Label lbIncidentes;
        private System.Windows.Forms.TextBox txtDesarrollador;
        private System.Windows.Forms.Label lbDesarrollador;
        private System.Windows.Forms.TextBox txtPaquete;
        private System.Windows.Forms.Label lbPaquete;
        private System.Windows.Forms.TextBox txtEstado;
        private System.Windows.Forms.Label lbEstado;
        private System.Windows.Forms.TextBox txtecha;
        private System.Windows.Forms.Label lbFecha;
        private System.Windows.Forms.ToolStripMenuItem acercaDeToolStripMenuItem;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.ComboBox cbDesarrollador;
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
        private System.Windows.Forms.Button btnBorrar;
        private System.Windows.Forms.ToolStripMenuItem auditoriaToolStripMenuItem;
    }
}

