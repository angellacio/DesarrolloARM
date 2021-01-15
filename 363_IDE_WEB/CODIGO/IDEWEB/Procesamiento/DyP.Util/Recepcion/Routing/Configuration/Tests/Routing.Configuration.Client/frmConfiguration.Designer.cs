namespace SAT.DyP.Routing.Configuration.Client {
    partial class frmConfiguration {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.gridRuteo = new System.Windows.Forms.DataGridView();
            this.contextMenuGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteRowItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.txtSoapAction = new System.Windows.Forms.TextBox();
            this.txtDestino = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.id_route = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.soap_action = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.destination = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExtensionDAta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.editRowItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.gridRuteo)).BeginInit();
            this.contextMenuGrid.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridRuteo
            // 
            this.gridRuteo.AllowUserToAddRows = false;
            this.gridRuteo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridRuteo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id_route,
            this.soap_action,
            this.destination,
            this.description,
            this.ExtensionDAta});
            this.gridRuteo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridRuteo.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
            this.gridRuteo.Location = new System.Drawing.Point(3, 3);
            this.gridRuteo.MultiSelect = false;
            this.gridRuteo.Name = "gridRuteo";
            this.gridRuteo.RowTemplate.ContextMenuStrip = this.contextMenuGrid;
            this.gridRuteo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridRuteo.Size = new System.Drawing.Size(618, 341);
            this.gridRuteo.TabIndex = 1;
            // 
            // contextMenuGrid
            // 
            this.contextMenuGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteRowItem,
            this.editRowItem});
            this.contextMenuGrid.Name = "contextMenuGrid";
            this.contextMenuGrid.Size = new System.Drawing.Size(145, 48);
            // 
            // deleteRowItem
            // 
            this.deleteRowItem.Name = "deleteRowItem";
            this.deleteRowItem.Size = new System.Drawing.Size(144, 22);
            this.deleteRowItem.Text = "Eliminar ruta";
            this.deleteRowItem.ToolTipText = "Eliminar la columna actual";
            this.deleteRowItem.Click += new System.EventHandler(this.deleteRowItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(632, 373);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl1_Selecting);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.gridRuteo);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(624, 347);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Rutas";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnGuardar);
            this.tabPage2.Controls.Add(this.txtSoapAction);
            this.tabPage2.Controls.Add(this.txtDestino);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(624, 347);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Agregar";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(275, 266);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 23);
            this.btnGuardar.TabIndex = 6;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // txtSoapAction
            // 
            this.txtSoapAction.Location = new System.Drawing.Point(88, 7);
            this.txtSoapAction.Name = "txtSoapAction";
            this.txtSoapAction.Size = new System.Drawing.Size(519, 20);
            this.txtSoapAction.TabIndex = 1;
            // 
            // txtDestino
            // 
            this.txtDestino.Location = new System.Drawing.Point(88, 35);
            this.txtDestino.Name = "txtDestino";
            this.txtDestino.Size = new System.Drawing.Size(519, 20);
            this.txtDestino.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtDescripcion);
            this.groupBox1.Location = new System.Drawing.Point(9, 69);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(598, 177);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Descripción";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(6, 19);
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtDescripcion.Size = new System.Drawing.Size(586, 152);
            this.txtDescripcion.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Destino";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "SOAP Action";
            // 
            // id_route
            // 
            this.id_route.DataPropertyName = "IDRoute";
            this.id_route.HeaderText = "ID_Route";
            this.id_route.Name = "id_route";
            this.id_route.ReadOnly = true;
            // 
            // soap_action
            // 
            this.soap_action.DataPropertyName = "SoapAction";
            this.soap_action.HeaderText = "Soap Action";
            this.soap_action.Name = "soap_action";
            this.soap_action.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // destination
            // 
            this.destination.DataPropertyName = "Destination";
            this.destination.HeaderText = "Destination";
            this.destination.Name = "destination";
            this.destination.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // description
            // 
            this.description.DataPropertyName = "Description";
            this.description.HeaderText = "Description";
            this.description.Name = "description";
            this.description.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ExtensionDAta
            // 
            this.ExtensionDAta.DataPropertyName = "ExtensionData";
            this.ExtensionDAta.HeaderText = "ExtensionData";
            this.ExtensionDAta.Name = "ExtensionDAta";
            this.ExtensionDAta.ReadOnly = true;
            this.ExtensionDAta.Visible = false;
            // 
            // editRowItem
            // 
            this.editRowItem.Name = "editRowItem";
            this.editRowItem.Size = new System.Drawing.Size(144, 22);
            this.editRowItem.Text = "Editar ruta";
            this.editRowItem.Click += new System.EventHandler(this.editRowItem_Click);
            // 
            // frmConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 397);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmConfiguration";
            this.Text = "Configuración de ruteo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmConfiguration_FormClosing);
            this.Load += new System.EventHandler(this.frmConfiguration_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridRuteo)).EndInit();
            this.contextMenuGrid.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.TextBox txtSoapAction;
        private System.Windows.Forms.TextBox txtDestino;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.DataGridView gridRuteo;
        private System.Windows.Forms.ContextMenuStrip contextMenuGrid;
        private System.Windows.Forms.ToolStripMenuItem deleteRowItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_route;
        private System.Windows.Forms.DataGridViewTextBoxColumn soap_action;
        private System.Windows.Forms.DataGridViewTextBoxColumn destination;
        private System.Windows.Forms.DataGridViewTextBoxColumn description;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExtensionDAta;
        private System.Windows.Forms.ToolStripMenuItem editRowItem;

    }
}