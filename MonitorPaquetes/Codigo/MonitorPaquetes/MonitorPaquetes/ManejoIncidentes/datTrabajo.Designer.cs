namespace MonitorPaquetes.ManejoIncidentes
{
    partial class datTrabajo
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gbTrabajo = new System.Windows.Forms.GroupBox();
            this.gbDatos = new System.Windows.Forms.GroupBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.dgvPaquetes = new System.Windows.Forms.DataGridView();
            this.dciCabecera = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dvtb_Area = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcInsidente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcIFechaAlta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcIdentificador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcRetroalimentado = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dicPPMC = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dclNotificado = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dciDOC_PRU = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dciPaquete = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dciAltaPaquete = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dciDesarrollador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcpRDL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcpFechaMov = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dciEstatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcpObservaciones = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rdbTodos = new System.Windows.Forms.RadioButton();
            this.rdbActivos = new System.Windows.Forms.RadioButton();
            this.gbPQT = new System.Windows.Forms.GroupBox();
            this.gbRDL = new System.Windows.Forms.GroupBox();
            this.dtgvRDL = new System.Windows.Forms.DataGridView();
            this.numPaquete = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtgRDL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvFechaRDL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvRDLIdEstatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtgRDLEstatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ckbNotificado = new System.Windows.Forms.CheckBox();
            this.gbPQT_FAlta = new System.Windows.Forms.GroupBox();
            this.lblPQT_fAlta = new System.Windows.Forms.Label();
            this.gbPQT_Desarrollador = new System.Windows.Forms.GroupBox();
            this.cmbPQT_Desarrollador = new System.Windows.Forms.ComboBox();
            this.btnPQT_Observaciones = new System.Windows.Forms.Button();
            this.gbPQT_Identificador = new System.Windows.Forms.GroupBox();
            this.txtPQT_Identificador = new System.Windows.Forms.TextBox();
            this.ckbDOC_PRU = new System.Windows.Forms.CheckBox();
            this.txtPaquete = new System.Windows.Forms.TextBox();
            this.gbRMA = new System.Windows.Forms.GroupBox();
            this.btnRMA_Observaciones = new System.Windows.Forms.Button();
            this.gbRMA_NotaLibera = new System.Windows.Forms.GroupBox();
            this.txtRMA_NotaLibera = new System.Windows.Forms.TextBox();
            this.gbRMA_Identificador = new System.Windows.Forms.GroupBox();
            this.txtRMA_Identificador = new System.Windows.Forms.TextBox();
            this.gbRMA_fAlta = new System.Windows.Forms.GroupBox();
            this.lblRMA_fAlta = new System.Windows.Forms.Label();
            this.gbRMA_Area = new System.Windows.Forms.GroupBox();
            this.cmbRMA_Area = new System.Windows.Forms.ComboBox();
            this.ckbRMA_Retroalimentado = new System.Windows.Forms.CheckBox();
            this.ckbRMA_PPMC = new System.Windows.Forms.CheckBox();
            this.txtRMA = new System.Windows.Forms.TextBox();
            this.tltMensajes = new System.Windows.Forms.ToolTip(this.components);
            this.erpManejoIncidentes = new System.Windows.Forms.ErrorProvider(this.components);
            this.gbTrabajo.SuspendLayout();
            this.gbDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPaquetes)).BeginInit();
            this.gbPQT.SuspendLayout();
            this.gbRDL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvRDL)).BeginInit();
            this.gbPQT_FAlta.SuspendLayout();
            this.gbPQT_Desarrollador.SuspendLayout();
            this.gbPQT_Identificador.SuspendLayout();
            this.gbRMA.SuspendLayout();
            this.gbRMA_NotaLibera.SuspendLayout();
            this.gbRMA_Identificador.SuspendLayout();
            this.gbRMA_fAlta.SuspendLayout();
            this.gbRMA_Area.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.erpManejoIncidentes)).BeginInit();
            this.SuspendLayout();
            // 
            // gbTrabajo
            // 
            this.gbTrabajo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbTrabajo.Controls.Add(this.gbDatos);
            this.gbTrabajo.Controls.Add(this.gbPQT);
            this.gbTrabajo.Controls.Add(this.gbRMA);
            this.gbTrabajo.Location = new System.Drawing.Point(12, 12);
            this.gbTrabajo.Name = "gbTrabajo";
            this.gbTrabajo.Size = new System.Drawing.Size(1030, 565);
            this.gbTrabajo.TabIndex = 0;
            this.gbTrabajo.TabStop = false;
            // 
            // gbDatos
            // 
            this.gbDatos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDatos.Controls.Add(this.btnBuscar);
            this.gbDatos.Controls.Add(this.dgvPaquetes);
            this.gbDatos.Controls.Add(this.rdbTodos);
            this.gbDatos.Controls.Add(this.rdbActivos);
            this.gbDatos.Location = new System.Drawing.Point(366, 0);
            this.gbDatos.Name = "gbDatos";
            this.gbDatos.Size = new System.Drawing.Size(664, 565);
            this.gbDatos.TabIndex = 6;
            this.gbDatos.TabStop = false;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuscar.Location = new System.Drawing.Point(604, 11);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(54, 23);
            this.btnBuscar.TabIndex = 5;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // dgvPaquetes
            // 
            this.dgvPaquetes.AllowUserToAddRows = false;
            this.dgvPaquetes.AllowUserToDeleteRows = false;
            this.dgvPaquetes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPaquetes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvPaquetes.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
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
            this.dciCabecera,
            this.dvtb_Area,
            this.dcInsidente,
            this.dcIFechaAlta,
            this.dcIdentificador,
            this.dcRetroalimentado,
            this.dicPPMC,
            this.dclNotificado,
            this.dciDOC_PRU,
            this.dciPaquete,
            this.dciAltaPaquete,
            this.dciDesarrollador,
            this.dcpRDL,
            this.dcpFechaMov,
            this.dciEstatus,
            this.dcpObservaciones});
            this.dgvPaquetes.EnableHeadersVisualStyles = false;
            this.dgvPaquetes.Location = new System.Drawing.Point(6, 37);
            this.dgvPaquetes.MultiSelect = false;
            this.dgvPaquetes.Name = "dgvPaquetes";
            this.dgvPaquetes.RowHeadersVisible = false;
            this.dgvPaquetes.Size = new System.Drawing.Size(652, 522);
            this.dgvPaquetes.TabIndex = 3;
            this.dgvPaquetes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPaquetes_CellClick);
            this.dgvPaquetes.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvPaquetes_CellFormatting);
            this.dgvPaquetes.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvPaquetes_CellPainting);
            // 
            // dciCabecera
            // 
            this.dciCabecera.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dciCabecera.DataPropertyName = "I001_sCabecera";
            dataGridViewCellStyle2.Format = "dd/MM/yyyy HH:mm";
            dataGridViewCellStyle2.NullValue = null;
            this.dciCabecera.DefaultCellStyle = dataGridViewCellStyle2;
            this.dciCabecera.HeaderText = "Cabecera";
            this.dciCabecera.Name = "dciCabecera";
            this.dciCabecera.ReadOnly = true;
            this.dciCabecera.Width = 77;
            // 
            // dvtb_Area
            // 
            this.dvtb_Area.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dvtb_Area.DataPropertyName = "I002_sArea";
            this.dvtb_Area.HeaderText = "Area";
            this.dvtb_Area.Name = "dvtb_Area";
            this.dvtb_Area.ReadOnly = true;
            this.dvtb_Area.Visible = false;
            // 
            // dcInsidente
            // 
            this.dcInsidente.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dcInsidente.DataPropertyName = "I010_sIncidente";
            this.dcInsidente.HeaderText = "Id Incidente";
            this.dcInsidente.Name = "dcInsidente";
            this.dcInsidente.Visible = false;
            // 
            // dcIFechaAlta
            // 
            this.dcIFechaAlta.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dcIFechaAlta.DataPropertyName = "I011_fAlta";
            this.dcIFechaAlta.HeaderText = "Fecha Alta Incidente";
            this.dcIFechaAlta.Name = "dcIFechaAlta";
            this.dcIFechaAlta.Visible = false;
            // 
            // dcIdentificador
            // 
            this.dcIdentificador.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dcIdentificador.DataPropertyName = "I012_sDescripcion";
            this.dcIdentificador.HeaderText = "Identificador Incidente";
            this.dcIdentificador.Name = "dcIdentificador";
            this.dcIdentificador.Visible = false;
            // 
            // dcRetroalimentado
            // 
            this.dcRetroalimentado.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.dcRetroalimentado.DataPropertyName = "I013_bRetroalimentado";
            this.dcRetroalimentado.HeaderText = "Retroalimentado";
            this.dcRetroalimentado.Name = "dcRetroalimentado";
            this.dcRetroalimentado.ReadOnly = true;
            this.dcRetroalimentado.Visible = false;
            // 
            // dicPPMC
            // 
            this.dicPPMC.DataPropertyName = "I020_bPPMC";
            this.dicPPMC.HeaderText = "PPMC";
            this.dicPPMC.Name = "dicPPMC";
            this.dicPPMC.ReadOnly = true;
            this.dicPPMC.Visible = false;
            this.dicPPMC.Width = 42;
            // 
            // dclNotificado
            // 
            this.dclNotificado.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dclNotificado.DataPropertyName = "P033_bNotificado";
            this.dclNotificado.HeaderText = "Notificado";
            this.dclNotificado.Name = "dclNotificado";
            this.dclNotificado.ReadOnly = true;
            this.dclNotificado.Visible = false;
            // 
            // dciDOC_PRU
            // 
            this.dciDOC_PRU.DataPropertyName = "P031_bDOC_PRU";
            this.dciDOC_PRU.HeaderText = "DOC PRU";
            this.dciDOC_PRU.Name = "dciDOC_PRU";
            this.dciDOC_PRU.ReadOnly = true;
            this.dciDOC_PRU.Width = 55;
            // 
            // dciPaquete
            // 
            this.dciPaquete.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dciPaquete.DataPropertyName = "P010_sPaquete";
            this.dciPaquete.HeaderText = "Paquete";
            this.dciPaquete.Name = "dciPaquete";
            this.dciPaquete.ReadOnly = true;
            this.dciPaquete.Width = 71;
            // 
            // dciAltaPaquete
            // 
            this.dciAltaPaquete.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dciAltaPaquete.DataPropertyName = "P020_fAlta";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "dd/MM/yyyy HH:mm";
            this.dciAltaPaquete.DefaultCellStyle = dataGridViewCellStyle3;
            this.dciAltaPaquete.HeaderText = "Fecha Alta Paquete";
            this.dciAltaPaquete.Name = "dciAltaPaquete";
            this.dciAltaPaquete.ReadOnly = true;
            this.dciAltaPaquete.Width = 114;
            // 
            // dciDesarrollador
            // 
            this.dciDesarrollador.DataPropertyName = "P036_sDesarrollador";
            this.dciDesarrollador.HeaderText = "Desarrollador";
            this.dciDesarrollador.Name = "dciDesarrollador";
            this.dciDesarrollador.Visible = false;
            this.dciDesarrollador.Width = 93;
            // 
            // dcpRDL
            // 
            this.dcpRDL.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dcpRDL.DataPropertyName = "P40_nUltimaRDL";
            this.dcpRDL.HeaderText = "Ultimo RDL";
            this.dcpRDL.Name = "dcpRDL";
            this.dcpRDL.ReadOnly = true;
            this.dcpRDL.Width = 78;
            // 
            // dcpFechaMov
            // 
            this.dcpFechaMov.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dcpFechaMov.DataPropertyName = "P41_fUltimoMoviminetoMoni";
            this.dcpFechaMov.HeaderText = "Fecha Ultimo Movimiento";
            this.dcpFechaMov.Name = "dcpFechaMov";
            this.dcpFechaMov.ReadOnly = true;
            this.dcpFechaMov.Width = 137;
            // 
            // dciEstatus
            // 
            this.dciEstatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dciEstatus.DataPropertyName = "P42_Estatus";
            this.dciEstatus.HeaderText = "Ultimo Estatus";
            this.dciEstatus.Name = "dciEstatus";
            this.dciEstatus.Width = 90;
            // 
            // dcpObservaciones
            // 
            this.dcpObservaciones.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dcpObservaciones.DataPropertyName = "P032_sObservaciones";
            this.dcpObservaciones.HeaderText = "Observaciones";
            this.dcpObservaciones.Name = "dcpObservaciones";
            this.dcpObservaciones.ReadOnly = true;
            this.dcpObservaciones.Width = 102;
            // 
            // rdbTodos
            // 
            this.rdbTodos.AutoSize = true;
            this.rdbTodos.Location = new System.Drawing.Point(72, 14);
            this.rdbTodos.Name = "rdbTodos";
            this.rdbTodos.Size = new System.Drawing.Size(55, 17);
            this.rdbTodos.TabIndex = 4;
            this.rdbTodos.Text = "Todos";
            this.rdbTodos.UseVisualStyleBackColor = true;
            this.rdbTodos.CheckedChanged += new System.EventHandler(this.rdbTodos_CheckedChanged);
            // 
            // rdbActivos
            // 
            this.rdbActivos.AutoSize = true;
            this.rdbActivos.Checked = true;
            this.rdbActivos.Location = new System.Drawing.Point(6, 14);
            this.rdbActivos.Name = "rdbActivos";
            this.rdbActivos.Size = new System.Drawing.Size(60, 17);
            this.rdbActivos.TabIndex = 2;
            this.rdbActivos.TabStop = true;
            this.rdbActivos.Text = "Activos";
            this.rdbActivos.UseVisualStyleBackColor = true;
            this.rdbActivos.CheckedChanged += new System.EventHandler(this.rdbActivos_CheckedChanged);
            // 
            // gbPQT
            // 
            this.gbPQT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gbPQT.Controls.Add(this.gbRDL);
            this.gbPQT.Controls.Add(this.ckbNotificado);
            this.gbPQT.Controls.Add(this.gbPQT_FAlta);
            this.gbPQT.Controls.Add(this.gbPQT_Desarrollador);
            this.gbPQT.Controls.Add(this.btnPQT_Observaciones);
            this.gbPQT.Controls.Add(this.gbPQT_Identificador);
            this.gbPQT.Controls.Add(this.ckbDOC_PRU);
            this.gbPQT.Controls.Add(this.txtPaquete);
            this.gbPQT.Location = new System.Drawing.Point(6, 292);
            this.gbPQT.Name = "gbPQT";
            this.gbPQT.Size = new System.Drawing.Size(353, 267);
            this.gbPQT.TabIndex = 1;
            this.gbPQT.TabStop = false;
            this.gbPQT.Text = "Paquete";
            // 
            // gbRDL
            // 
            this.gbRDL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gbRDL.Controls.Add(this.dtgvRDL);
            this.gbRDL.Location = new System.Drawing.Point(7, 163);
            this.gbRDL.Name = "gbRDL";
            this.gbRDL.Size = new System.Drawing.Size(340, 98);
            this.gbRDL.TabIndex = 8;
            this.gbRDL.TabStop = false;
            this.gbRDL.Text = "RDL";
            // 
            // dtgvRDL
            // 
            this.dtgvRDL.AllowUserToAddRows = false;
            this.dtgvRDL.AllowUserToDeleteRows = false;
            this.dtgvRDL.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgvRDL.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dtgvRDL.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dtgvRDL.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvRDL.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.numPaquete,
            this.dtgRDL,
            this.dgvFechaRDL,
            this.dgvRDLIdEstatus,
            this.dtgRDLEstatus});
            this.dtgvRDL.EnableHeadersVisualStyles = false;
            this.dtgvRDL.Location = new System.Drawing.Point(6, 19);
            this.dtgvRDL.MultiSelect = false;
            this.dtgvRDL.Name = "dtgvRDL";
            this.dtgvRDL.RowHeadersVisible = false;
            this.dtgvRDL.Size = new System.Drawing.Size(328, 73);
            this.dtgvRDL.TabIndex = 0;
            // 
            // numPaquete
            // 
            this.numPaquete.DataPropertyName = "D011_sPaquete";
            this.numPaquete.HeaderText = "Paquete";
            this.numPaquete.Name = "numPaquete";
            this.numPaquete.Visible = false;
            this.numPaquete.Width = 52;
            // 
            // dtgRDL
            // 
            this.dtgRDL.DataPropertyName = "D010_nRDL";
            this.dtgRDL.HeaderText = "RDL";
            this.dtgRDL.Name = "dtgRDL";
            this.dtgRDL.ReadOnly = true;
            this.dtgRDL.Width = 53;
            // 
            // dgvFechaRDL
            // 
            this.dgvFechaRDL.DataPropertyName = "D012_fMovimiento";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "dd/MM/yyyy HH:mm";
            this.dgvFechaRDL.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvFechaRDL.HeaderText = "Fecha Movimiento";
            this.dgvFechaRDL.Name = "dgvFechaRDL";
            this.dgvFechaRDL.ReadOnly = true;
            this.dgvFechaRDL.Width = 108;
            // 
            // dgvRDLIdEstatus
            // 
            this.dgvRDLIdEstatus.DataPropertyName = "D013_nEstatus";
            this.dgvRDLIdEstatus.HeaderText = "Estatus";
            this.dgvRDLIdEstatus.Name = "dgvRDLIdEstatus";
            this.dgvRDLIdEstatus.ReadOnly = true;
            this.dgvRDLIdEstatus.Visible = false;
            this.dgvRDLIdEstatus.Width = 66;
            // 
            // dtgRDLEstatus
            // 
            this.dtgRDLEstatus.DataPropertyName = "D020_sEstatus";
            this.dtgRDLEstatus.HeaderText = "Estatus";
            this.dtgRDLEstatus.Name = "dtgRDLEstatus";
            this.dtgRDLEstatus.ReadOnly = true;
            this.dtgRDLEstatus.Width = 66;
            // 
            // ckbNotificado
            // 
            this.ckbNotificado.AutoSize = true;
            this.ckbNotificado.Enabled = false;
            this.ckbNotificado.Location = new System.Drawing.Point(194, 22);
            this.ckbNotificado.Name = "ckbNotificado";
            this.ckbNotificado.Size = new System.Drawing.Size(74, 17);
            this.ckbNotificado.TabIndex = 7;
            this.ckbNotificado.Text = "Notificado";
            this.ckbNotificado.UseVisualStyleBackColor = true;
            this.ckbNotificado.Click += new System.EventHandler(this.ckbNotificado_Click);
            // 
            // gbPQT_FAlta
            // 
            this.gbPQT_FAlta.Controls.Add(this.lblPQT_fAlta);
            this.gbPQT_FAlta.Location = new System.Drawing.Point(7, 45);
            this.gbPQT_FAlta.Name = "gbPQT_FAlta";
            this.gbPQT_FAlta.Size = new System.Drawing.Size(167, 46);
            this.gbPQT_FAlta.TabIndex = 3;
            this.gbPQT_FAlta.TabStop = false;
            this.gbPQT_FAlta.Text = "Fecha alta";
            // 
            // lblPQT_fAlta
            // 
            this.lblPQT_fAlta.AutoSize = true;
            this.lblPQT_fAlta.Location = new System.Drawing.Point(7, 20);
            this.lblPQT_fAlta.Name = "lblPQT_fAlta";
            this.lblPQT_fAlta.Size = new System.Drawing.Size(149, 13);
            this.lblPQT_fAlta.TabIndex = 0;
            this.lblPQT_fAlta.Text = "01 Diciembre 2018 12:59 a.m.";
            // 
            // gbPQT_Desarrollador
            // 
            this.gbPQT_Desarrollador.Controls.Add(this.cmbPQT_Desarrollador);
            this.gbPQT_Desarrollador.Location = new System.Drawing.Point(179, 46);
            this.gbPQT_Desarrollador.Name = "gbPQT_Desarrollador";
            this.gbPQT_Desarrollador.Size = new System.Drawing.Size(168, 46);
            this.gbPQT_Desarrollador.TabIndex = 5;
            this.gbPQT_Desarrollador.TabStop = false;
            this.gbPQT_Desarrollador.Text = "Desarrollador";
            // 
            // cmbPQT_Desarrollador
            // 
            this.cmbPQT_Desarrollador.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPQT_Desarrollador.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPQT_Desarrollador.Enabled = false;
            this.cmbPQT_Desarrollador.FormattingEnabled = true;
            this.cmbPQT_Desarrollador.Items.AddRange(new object[] {
            "Seleccione una opcion.",
            "ACSI",
            "APE4 DyP",
            "APE4 FATCA",
            "APE4 Formulario",
            "APE4 Juridica"});
            this.cmbPQT_Desarrollador.Location = new System.Drawing.Point(7, 20);
            this.cmbPQT_Desarrollador.Name = "cmbPQT_Desarrollador";
            this.cmbPQT_Desarrollador.Size = new System.Drawing.Size(155, 21);
            this.cmbPQT_Desarrollador.TabIndex = 1;
            this.cmbPQT_Desarrollador.SelectionChangeCommitted += new System.EventHandler(this.cmbPQT_Desarrollador_SelectionChangeCommitted);
            // 
            // btnPQT_Observaciones
            // 
            this.btnPQT_Observaciones.BackgroundImage = global::MonitorPaquetes.Properties.Resources.Observaciones;
            this.btnPQT_Observaciones.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPQT_Observaciones.Enabled = false;
            this.btnPQT_Observaciones.Location = new System.Drawing.Point(296, 10);
            this.btnPQT_Observaciones.Name = "btnPQT_Observaciones";
            this.btnPQT_Observaciones.Size = new System.Drawing.Size(40, 38);
            this.btnPQT_Observaciones.TabIndex = 6;
            this.btnPQT_Observaciones.UseVisualStyleBackColor = true;
            this.btnPQT_Observaciones.Click += new System.EventHandler(this.btnPQT_Observaciones_Click);
            // 
            // gbPQT_Identificador
            // 
            this.gbPQT_Identificador.Controls.Add(this.txtPQT_Identificador);
            this.gbPQT_Identificador.Location = new System.Drawing.Point(7, 93);
            this.gbPQT_Identificador.Name = "gbPQT_Identificador";
            this.gbPQT_Identificador.Size = new System.Drawing.Size(340, 63);
            this.gbPQT_Identificador.TabIndex = 2;
            this.gbPQT_Identificador.TabStop = false;
            this.gbPQT_Identificador.Text = "Identificador";
            // 
            // txtPQT_Identificador
            // 
            this.txtPQT_Identificador.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPQT_Identificador.Location = new System.Drawing.Point(6, 19);
            this.txtPQT_Identificador.Multiline = true;
            this.txtPQT_Identificador.Name = "txtPQT_Identificador";
            this.txtPQT_Identificador.ReadOnly = true;
            this.txtPQT_Identificador.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPQT_Identificador.Size = new System.Drawing.Size(328, 38);
            this.txtPQT_Identificador.TabIndex = 2;
            this.txtPQT_Identificador.Leave += new System.EventHandler(this.txtPQT_Identificador_Leave);
            // 
            // ckbDOC_PRU
            // 
            this.ckbDOC_PRU.AutoSize = true;
            this.ckbDOC_PRU.Enabled = false;
            this.ckbDOC_PRU.Location = new System.Drawing.Point(113, 22);
            this.ckbDOC_PRU.Name = "ckbDOC_PRU";
            this.ckbDOC_PRU.Size = new System.Drawing.Size(75, 17);
            this.ckbDOC_PRU.TabIndex = 1;
            this.ckbDOC_PRU.Text = "DOC PRU";
            this.ckbDOC_PRU.UseVisualStyleBackColor = true;
            this.ckbDOC_PRU.Click += new System.EventHandler(this.ckbDOC_PRU_Click);
            // 
            // txtPaquete
            // 
            this.txtPaquete.Location = new System.Drawing.Point(7, 20);
            this.txtPaquete.Name = "txtPaquete";
            this.txtPaquete.ReadOnly = true;
            this.txtPaquete.Size = new System.Drawing.Size(99, 20);
            this.txtPaquete.TabIndex = 0;
            this.txtPaquete.Leave += new System.EventHandler(this.txtPaquete_Leave);
            // 
            // gbRMA
            // 
            this.gbRMA.Controls.Add(this.btnRMA_Observaciones);
            this.gbRMA.Controls.Add(this.gbRMA_NotaLibera);
            this.gbRMA.Controls.Add(this.gbRMA_Identificador);
            this.gbRMA.Controls.Add(this.gbRMA_fAlta);
            this.gbRMA.Controls.Add(this.gbRMA_Area);
            this.gbRMA.Controls.Add(this.ckbRMA_Retroalimentado);
            this.gbRMA.Controls.Add(this.ckbRMA_PPMC);
            this.gbRMA.Controls.Add(this.txtRMA);
            this.gbRMA.Location = new System.Drawing.Point(6, 11);
            this.gbRMA.Name = "gbRMA";
            this.gbRMA.Size = new System.Drawing.Size(353, 275);
            this.gbRMA.TabIndex = 0;
            this.gbRMA.TabStop = false;
            this.gbRMA.Text = "Incidentes";
            // 
            // btnRMA_Observaciones
            // 
            this.btnRMA_Observaciones.BackgroundImage = global::MonitorPaquetes.Properties.Resources.Observaciones;
            this.btnRMA_Observaciones.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRMA_Observaciones.Enabled = false;
            this.btnRMA_Observaciones.Location = new System.Drawing.Point(297, 9);
            this.btnRMA_Observaciones.Name = "btnRMA_Observaciones";
            this.btnRMA_Observaciones.Size = new System.Drawing.Size(40, 38);
            this.btnRMA_Observaciones.TabIndex = 5;
            this.btnRMA_Observaciones.UseVisualStyleBackColor = true;
            this.btnRMA_Observaciones.Click += new System.EventHandler(this.btnRMA_Observaciones_Click);
            // 
            // gbRMA_NotaLibera
            // 
            this.gbRMA_NotaLibera.Controls.Add(this.txtRMA_NotaLibera);
            this.gbRMA_NotaLibera.Location = new System.Drawing.Point(6, 154);
            this.gbRMA_NotaLibera.Name = "gbRMA_NotaLibera";
            this.gbRMA_NotaLibera.Size = new System.Drawing.Size(341, 115);
            this.gbRMA_NotaLibera.TabIndex = 3;
            this.gbRMA_NotaLibera.TabStop = false;
            this.gbRMA_NotaLibera.Text = "Nota de liberación";
            // 
            // txtRMA_NotaLibera
            // 
            this.txtRMA_NotaLibera.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRMA_NotaLibera.Location = new System.Drawing.Point(6, 19);
            this.txtRMA_NotaLibera.Multiline = true;
            this.txtRMA_NotaLibera.Name = "txtRMA_NotaLibera";
            this.txtRMA_NotaLibera.ReadOnly = true;
            this.txtRMA_NotaLibera.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRMA_NotaLibera.Size = new System.Drawing.Size(329, 90);
            this.txtRMA_NotaLibera.TabIndex = 2;
            this.txtRMA_NotaLibera.Leave += new System.EventHandler(this.txtRMA_NotaLibera_Leave);
            // 
            // gbRMA_Identificador
            // 
            this.gbRMA_Identificador.Controls.Add(this.txtRMA_Identificador);
            this.gbRMA_Identificador.Location = new System.Drawing.Point(6, 91);
            this.gbRMA_Identificador.Name = "gbRMA_Identificador";
            this.gbRMA_Identificador.Size = new System.Drawing.Size(341, 63);
            this.gbRMA_Identificador.TabIndex = 3;
            this.gbRMA_Identificador.TabStop = false;
            this.gbRMA_Identificador.Text = "Identificador";
            // 
            // txtRMA_Identificador
            // 
            this.txtRMA_Identificador.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRMA_Identificador.Location = new System.Drawing.Point(6, 19);
            this.txtRMA_Identificador.Multiline = true;
            this.txtRMA_Identificador.Name = "txtRMA_Identificador";
            this.txtRMA_Identificador.ReadOnly = true;
            this.txtRMA_Identificador.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRMA_Identificador.Size = new System.Drawing.Size(329, 38);
            this.txtRMA_Identificador.TabIndex = 2;
            this.txtRMA_Identificador.Leave += new System.EventHandler(this.txtRMA_Identificador_Leave);
            // 
            // gbRMA_fAlta
            // 
            this.gbRMA_fAlta.Controls.Add(this.lblRMA_fAlta);
            this.gbRMA_fAlta.Location = new System.Drawing.Point(6, 45);
            this.gbRMA_fAlta.Name = "gbRMA_fAlta";
            this.gbRMA_fAlta.Size = new System.Drawing.Size(167, 46);
            this.gbRMA_fAlta.TabIndex = 2;
            this.gbRMA_fAlta.TabStop = false;
            this.gbRMA_fAlta.Text = "Fecha alta";
            // 
            // lblRMA_fAlta
            // 
            this.lblRMA_fAlta.AutoSize = true;
            this.lblRMA_fAlta.Location = new System.Drawing.Point(7, 20);
            this.lblRMA_fAlta.Name = "lblRMA_fAlta";
            this.lblRMA_fAlta.Size = new System.Drawing.Size(149, 13);
            this.lblRMA_fAlta.TabIndex = 0;
            this.lblRMA_fAlta.Text = "01 Diciembre 2018 12:59 a.m.";
            // 
            // gbRMA_Area
            // 
            this.gbRMA_Area.Controls.Add(this.cmbRMA_Area);
            this.gbRMA_Area.Location = new System.Drawing.Point(179, 45);
            this.gbRMA_Area.Name = "gbRMA_Area";
            this.gbRMA_Area.Size = new System.Drawing.Size(168, 46);
            this.gbRMA_Area.TabIndex = 4;
            this.gbRMA_Area.TabStop = false;
            this.gbRMA_Area.Text = "Area";
            // 
            // cmbRMA_Area
            // 
            this.cmbRMA_Area.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbRMA_Area.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRMA_Area.Enabled = false;
            this.cmbRMA_Area.FormattingEnabled = true;
            this.cmbRMA_Area.Items.AddRange(new object[] {
            "Seleccione una opcion.",
            "ACSI",
            "APE4 DyP",
            "APE4 FATCA",
            "APE4 Formulario",
            "APE4 Juridica"});
            this.cmbRMA_Area.Location = new System.Drawing.Point(7, 20);
            this.cmbRMA_Area.Name = "cmbRMA_Area";
            this.cmbRMA_Area.Size = new System.Drawing.Size(155, 21);
            this.cmbRMA_Area.TabIndex = 1;
            this.cmbRMA_Area.SelectionChangeCommitted += new System.EventHandler(this.cmbRMA_Area_SelectionChangeCommitted);
            // 
            // ckbRMA_Retroalimentado
            // 
            this.ckbRMA_Retroalimentado.AutoSize = true;
            this.ckbRMA_Retroalimentado.Enabled = false;
            this.ckbRMA_Retroalimentado.Location = new System.Drawing.Point(190, 20);
            this.ckbRMA_Retroalimentado.Name = "ckbRMA_Retroalimentado";
            this.ckbRMA_Retroalimentado.Size = new System.Drawing.Size(103, 17);
            this.ckbRMA_Retroalimentado.TabIndex = 3;
            this.ckbRMA_Retroalimentado.Text = "Retroalimentado";
            this.ckbRMA_Retroalimentado.UseVisualStyleBackColor = true;
            this.ckbRMA_Retroalimentado.Click += new System.EventHandler(this.ckbRMA_Retroalimentado_Click);
            // 
            // ckbRMA_PPMC
            // 
            this.ckbRMA_PPMC.AutoSize = true;
            this.ckbRMA_PPMC.Enabled = false;
            this.ckbRMA_PPMC.Location = new System.Drawing.Point(113, 21);
            this.ckbRMA_PPMC.Name = "ckbRMA_PPMC";
            this.ckbRMA_PPMC.Size = new System.Drawing.Size(70, 17);
            this.ckbRMA_PPMC.TabIndex = 1;
            this.ckbRMA_PPMC.Text = "es PPMC";
            this.ckbRMA_PPMC.UseVisualStyleBackColor = true;
            this.ckbRMA_PPMC.Click += new System.EventHandler(this.ckbRMA_PPMC_Click);
            // 
            // txtRMA
            // 
            this.txtRMA.Location = new System.Drawing.Point(6, 19);
            this.txtRMA.Name = "txtRMA";
            this.txtRMA.ReadOnly = true;
            this.txtRMA.Size = new System.Drawing.Size(100, 20);
            this.txtRMA.TabIndex = 0;
            this.txtRMA.Leave += new System.EventHandler(this.txtRMA_Leave);
            // 
            // erpManejoIncidentes
            // 
            this.erpManejoIncidentes.ContainerControl = this;
            // 
            // datTrabajo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1054, 589);
            this.Controls.Add(this.gbTrabajo);
            this.Name = "datTrabajo";
            this.Text = "datTrabajo";
            this.Load += new System.EventHandler(this.datTrabajo_Load);
            this.gbTrabajo.ResumeLayout(false);
            this.gbDatos.ResumeLayout(false);
            this.gbDatos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPaquetes)).EndInit();
            this.gbPQT.ResumeLayout(false);
            this.gbPQT.PerformLayout();
            this.gbRDL.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvRDL)).EndInit();
            this.gbPQT_FAlta.ResumeLayout(false);
            this.gbPQT_FAlta.PerformLayout();
            this.gbPQT_Desarrollador.ResumeLayout(false);
            this.gbPQT_Identificador.ResumeLayout(false);
            this.gbPQT_Identificador.PerformLayout();
            this.gbRMA.ResumeLayout(false);
            this.gbRMA.PerformLayout();
            this.gbRMA_NotaLibera.ResumeLayout(false);
            this.gbRMA_NotaLibera.PerformLayout();
            this.gbRMA_Identificador.ResumeLayout(false);
            this.gbRMA_Identificador.PerformLayout();
            this.gbRMA_fAlta.ResumeLayout(false);
            this.gbRMA_fAlta.PerformLayout();
            this.gbRMA_Area.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.erpManejoIncidentes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbTrabajo;
        private System.Windows.Forms.GroupBox gbRMA;
        private System.Windows.Forms.CheckBox ckbRMA_PPMC;
        private System.Windows.Forms.TextBox txtRMA;
        private System.Windows.Forms.GroupBox gbRMA_Area;
        private System.Windows.Forms.ComboBox cmbRMA_Area;
        private System.Windows.Forms.CheckBox ckbRMA_Retroalimentado;
        private System.Windows.Forms.GroupBox gbPQT;
        private System.Windows.Forms.GroupBox gbRMA_fAlta;
        private System.Windows.Forms.Label lblRMA_fAlta;
        private System.Windows.Forms.TextBox txtPaquete;
        private System.Windows.Forms.TextBox txtPQT_Identificador;
        private System.Windows.Forms.CheckBox ckbDOC_PRU;
        private System.Windows.Forms.GroupBox gbPQT_Identificador;
        private System.Windows.Forms.GroupBox gbRMA_Identificador;
        private System.Windows.Forms.TextBox txtRMA_Identificador;
        private System.Windows.Forms.GroupBox gbRMA_NotaLibera;
        private System.Windows.Forms.TextBox txtRMA_NotaLibera;
        private System.Windows.Forms.Button btnRMA_Observaciones;
        private System.Windows.Forms.Button btnPQT_Observaciones;
        private System.Windows.Forms.ToolTip tltMensajes;
        private System.Windows.Forms.RadioButton rdbActivos;
        private System.Windows.Forms.DataGridView dgvPaquetes;
        private System.Windows.Forms.RadioButton rdbTodos;
        private System.Windows.Forms.GroupBox gbPQT_Desarrollador;
        private System.Windows.Forms.ComboBox cmbPQT_Desarrollador;
        private System.Windows.Forms.GroupBox gbPQT_FAlta;
        private System.Windows.Forms.Label lblPQT_fAlta;
        private System.Windows.Forms.GroupBox gbDatos;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.ErrorProvider erpManejoIncidentes;
        private System.Windows.Forms.CheckBox ckbNotificado;
        private System.Windows.Forms.GroupBox gbRDL;
        private System.Windows.Forms.DataGridView dtgvRDL;
        private System.Windows.Forms.DataGridViewTextBoxColumn dciCabecera;
        private System.Windows.Forms.DataGridViewTextBoxColumn dvtb_Area;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcInsidente;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcIFechaAlta;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcIdentificador;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dcRetroalimentado;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dicPPMC;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dclNotificado;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dciDOC_PRU;
        private System.Windows.Forms.DataGridViewTextBoxColumn dciPaquete;
        private System.Windows.Forms.DataGridViewTextBoxColumn dciAltaPaquete;
        private System.Windows.Forms.DataGridViewTextBoxColumn dciDesarrollador;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcpRDL;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcpFechaMov;
        private System.Windows.Forms.DataGridViewTextBoxColumn dciEstatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcpObservaciones;
        private System.Windows.Forms.DataGridViewTextBoxColumn numPaquete;
        private System.Windows.Forms.DataGridViewTextBoxColumn dtgRDL;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvFechaRDL;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvRDLIdEstatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn dtgRDLEstatus;
    }
}