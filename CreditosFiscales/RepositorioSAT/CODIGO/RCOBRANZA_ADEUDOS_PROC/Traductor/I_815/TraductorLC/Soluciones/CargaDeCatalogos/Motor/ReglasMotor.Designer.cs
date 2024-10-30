namespace CargaDeCatalogos.Motor
{
    partial class ReglasMotor
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
			this.label1 = new System.Windows.Forms.Label();
			this.txtDescripcion = new System.Windows.Forms.TextBox();
			this.ckEsValidacion = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtArchivoRegla = new System.Windows.Forms.TextBox();
			this.btnExaminar = new System.Windows.Forms.Button();
			this.btnGuardar = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.lblIDRegla = new System.Windows.Forms.Label();
			this.dgReglas = new System.Windows.Forms.DataGridView();
			this.FileDialogExaminar = new System.Windows.Forms.OpenFileDialog();
			this.cmbAplicacion = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.cmbTipoDocumento = new System.Windows.Forms.ComboBox();
			this.txtTipoDocumento = new System.Windows.Forms.Label();
			this.btnBuscarReglas = new System.Windows.Forms.Button();
			this.ckAntesInsersion = new System.Windows.Forms.CheckBox();
			this.label5 = new System.Windows.Forms.Label();
			this.txtSecuencia = new System.Windows.Forms.MaskedTextBox();
			this.btnNuevaRegla = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dgReglas)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 35);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(63, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Descripcion";
			// 
			// txtDescripcion
			// 
			this.txtDescripcion.Location = new System.Drawing.Point(82, 32);
			this.txtDescripcion.Name = "txtDescripcion";
			this.txtDescripcion.Size = new System.Drawing.Size(728, 20);
			this.txtDescripcion.TabIndex = 1;
			// 
			// ckEsValidacion
			// 
			this.ckEsValidacion.AutoSize = true;
			this.ckEsValidacion.Location = new System.Drawing.Point(16, 86);
			this.ckEsValidacion.Name = "ckEsValidacion";
			this.ckEsValidacion.Size = new System.Drawing.Size(90, 17);
			this.ckEsValidacion.TabIndex = 2;
			this.ckEsValidacion.Text = "Es Validacion";
			this.ckEsValidacion.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(13, 60);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(35, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Regla";
			// 
			// txtArchivoRegla
			// 
			this.txtArchivoRegla.Location = new System.Drawing.Point(82, 60);
			this.txtArchivoRegla.Name = "txtArchivoRegla";
			this.txtArchivoRegla.Size = new System.Drawing.Size(538, 20);
			this.txtArchivoRegla.TabIndex = 4;
			// 
			// btnExaminar
			// 
			this.btnExaminar.Location = new System.Drawing.Point(626, 58);
			this.btnExaminar.Name = "btnExaminar";
			this.btnExaminar.Size = new System.Drawing.Size(75, 23);
			this.btnExaminar.TabIndex = 5;
			this.btnExaminar.Text = "Examinar...";
			this.btnExaminar.UseVisualStyleBackColor = true;
			this.btnExaminar.Click += new System.EventHandler(this.btnExaminar_Click);
			// 
			// btnGuardar
			// 
			this.btnGuardar.Location = new System.Drawing.Point(816, 38);
			this.btnGuardar.Name = "btnGuardar";
			this.btnGuardar.Size = new System.Drawing.Size(103, 23);
			this.btnGuardar.TabIndex = 6;
			this.btnGuardar.Text = "Guardar en BD";
			this.btnGuardar.UseVisualStyleBackColor = true;
			this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(13, 9);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(44, 13);
			this.label3.TabIndex = 7;
			this.label3.Text = "IdRegla";
			// 
			// lblIDRegla
			// 
			this.lblIDRegla.AutoSize = true;
			this.lblIDRegla.Location = new System.Drawing.Point(82, 9);
			this.lblIDRegla.Name = "lblIDRegla";
			this.lblIDRegla.Size = new System.Drawing.Size(0, 13);
			this.lblIDRegla.TabIndex = 8;
			// 
			// dgReglas
			// 
			this.dgReglas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgReglas.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			this.dgReglas.Location = new System.Drawing.Point(16, 146);
			this.dgReglas.Name = "dgReglas";
			this.dgReglas.Size = new System.Drawing.Size(902, 263);
			this.dgReglas.TabIndex = 9;
			this.dgReglas.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgReglas_CellContentDoubleClick);
			// 
			// FileDialogExaminar
			// 
			this.FileDialogExaminar.FileName = "openFileDialog1";
			// 
			// cmbAplicacion
			// 
			this.cmbAplicacion.FormattingEnabled = true;
			this.cmbAplicacion.Location = new System.Drawing.Point(504, 86);
			this.cmbAplicacion.Name = "cmbAplicacion";
			this.cmbAplicacion.Size = new System.Drawing.Size(158, 21);
			this.cmbAplicacion.TabIndex = 20;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(396, 89);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(102, 13);
			this.label4.TabIndex = 19;
			this.label4.Text = "Aplicación a asignar";
			// 
			// cmbTipoDocumento
			// 
			this.cmbTipoDocumento.FormattingEnabled = true;
			this.cmbTipoDocumento.Location = new System.Drawing.Point(219, 86);
			this.cmbTipoDocumento.Name = "cmbTipoDocumento";
			this.cmbTipoDocumento.Size = new System.Drawing.Size(158, 21);
			this.cmbTipoDocumento.TabIndex = 18;
			// 
			// txtTipoDocumento
			// 
			this.txtTipoDocumento.AutoSize = true;
			this.txtTipoDocumento.Location = new System.Drawing.Point(127, 89);
			this.txtTipoDocumento.Name = "txtTipoDocumento";
			this.txtTipoDocumento.Size = new System.Drawing.Size(86, 13);
			this.txtTipoDocumento.TabIndex = 17;
			this.txtTipoDocumento.Text = "Tipo Documento";
			// 
			// btnBuscarReglas
			// 
			this.btnBuscarReglas.Location = new System.Drawing.Point(816, 9);
			this.btnBuscarReglas.Name = "btnBuscarReglas";
			this.btnBuscarReglas.Size = new System.Drawing.Size(102, 23);
			this.btnBuscarReglas.TabIndex = 21;
			this.btnBuscarReglas.Text = "Buscar Reglas";
			this.btnBuscarReglas.UseVisualStyleBackColor = true;
			this.btnBuscarReglas.Click += new System.EventHandler(this.btnBuscarReglas_Click);
			// 
			// ckAntesInsersion
			// 
			this.ckAntesInsersion.AutoSize = true;
			this.ckAntesInsersion.Location = new System.Drawing.Point(15, 115);
			this.ckAntesInsersion.Name = "ckAntesInsersion";
			this.ckAntesInsersion.Size = new System.Drawing.Size(154, 17);
			this.ckAntesInsersion.TabIndex = 22;
			this.ckAntesInsersion.Text = "Antes de Insertar Canónico";
			this.ckAntesInsersion.UseVisualStyleBackColor = true;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(175, 116);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(58, 13);
			this.label5.TabIndex = 23;
			this.label5.Text = "Secuencia";
			// 
			// txtSecuencia
			// 
			this.txtSecuencia.Location = new System.Drawing.Point(239, 113);
			this.txtSecuencia.Mask = "99999";
			this.txtSecuencia.Name = "txtSecuencia";
			this.txtSecuencia.Size = new System.Drawing.Size(39, 20);
			this.txtSecuencia.TabIndex = 24;
			this.txtSecuencia.ValidatingType = typeof(int);
			// 
			// btnNuevaRegla
			// 
			this.btnNuevaRegla.Location = new System.Drawing.Point(816, 68);
			this.btnNuevaRegla.Name = "btnNuevaRegla";
			this.btnNuevaRegla.Size = new System.Drawing.Size(102, 23);
			this.btnNuevaRegla.TabIndex = 25;
			this.btnNuevaRegla.Text = "Nueva Regla";
			this.btnNuevaRegla.UseVisualStyleBackColor = true;
			this.btnNuevaRegla.Click += new System.EventHandler(this.btnNuevaRegla_Click);
			// 
			// ReglasMotor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(930, 421);
			this.Controls.Add(this.btnNuevaRegla);
			this.Controls.Add(this.txtSecuencia);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.ckAntesInsersion);
			this.Controls.Add(this.btnBuscarReglas);
			this.Controls.Add(this.cmbAplicacion);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.cmbTipoDocumento);
			this.Controls.Add(this.txtTipoDocumento);
			this.Controls.Add(this.dgReglas);
			this.Controls.Add(this.lblIDRegla);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.btnGuardar);
			this.Controls.Add(this.btnExaminar);
			this.Controls.Add(this.txtArchivoRegla);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.ckEsValidacion);
			this.Controls.Add(this.txtDescripcion);
			this.Controls.Add(this.label1);
			this.Name = "ReglasMotor";
			this.Text = "Reglas del Motor Traductor";
			this.Load += new System.EventHandler(this.ReglasMotor_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgReglas)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.CheckBox ckEsValidacion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtArchivoRegla;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnExaminar;
        private System.Windows.Forms.Label lblIDRegla;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgReglas;
        private System.Windows.Forms.OpenFileDialog FileDialogExaminar;
        private System.Windows.Forms.ComboBox cmbAplicacion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbTipoDocumento;
        private System.Windows.Forms.Label txtTipoDocumento;
        private System.Windows.Forms.Button btnBuscarReglas;
        private System.Windows.Forms.CheckBox ckAntesInsersion;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.MaskedTextBox txtSecuencia;
        private System.Windows.Forms.Button btnNuevaRegla;
    }
}