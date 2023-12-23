namespace CargaDeCatalogos.Catalogos
{
    partial class Transacciones
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
			this.btnBuscarDyP = new System.Windows.Forms.Button();
			this.btnGuardarDBMotor = new System.Windows.Forms.Button();
			this.btnBuscarDBMotor = new System.Windows.Forms.Button();
			this.dgListaResultados = new System.Windows.Forms.DataGridView();
			this.txtTipoDocumento = new System.Windows.Forms.Label();
			this.cmbTipoDocumento = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.cmbAplicacion = new System.Windows.Forms.ComboBox();
			((System.ComponentModel.ISupportInitialize)(this.dgListaResultados)).BeginInit();
			this.SuspendLayout();
			// 
			// btnBuscarDyP
			// 
			this.btnBuscarDyP.Location = new System.Drawing.Point(12, 12);
			this.btnBuscarDyP.Name = "btnBuscarDyP";
			this.btnBuscarDyP.Size = new System.Drawing.Size(114, 23);
			this.btnBuscarDyP.TabIndex = 0;
			this.btnBuscarDyP.Text = "Buscar en DyP";
			this.btnBuscarDyP.UseVisualStyleBackColor = true;
			this.btnBuscarDyP.Click += new System.EventHandler(this.btnBuscarDyP_Click);
			// 
			// btnGuardarDBMotor
			// 
			this.btnGuardarDBMotor.Location = new System.Drawing.Point(594, 12);
			this.btnGuardarDBMotor.Name = "btnGuardarDBMotor";
			this.btnGuardarDBMotor.Size = new System.Drawing.Size(111, 23);
			this.btnGuardarDBMotor.TabIndex = 1;
			this.btnGuardarDBMotor.Text = "Guardar BD Motor";
			this.btnGuardarDBMotor.UseVisualStyleBackColor = true;
			this.btnGuardarDBMotor.Click += new System.EventHandler(this.btnGuardarDBMotor_Click);
			// 
			// btnBuscarDBMotor
			// 
			this.btnBuscarDBMotor.Location = new System.Drawing.Point(132, 12);
			this.btnBuscarDBMotor.Name = "btnBuscarDBMotor";
			this.btnBuscarDBMotor.Size = new System.Drawing.Size(130, 23);
			this.btnBuscarDBMotor.TabIndex = 10;
			this.btnBuscarDBMotor.Text = "Buscar en DB Motor";
			this.btnBuscarDBMotor.UseVisualStyleBackColor = true;
			// 
			// dgListaResultados
			// 
			this.dgListaResultados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgListaResultados.Location = new System.Drawing.Point(12, 88);
			this.dgListaResultados.Name = "dgListaResultados";
			this.dgListaResultados.Size = new System.Drawing.Size(693, 211);
			this.dgListaResultados.TabIndex = 11;
			// 
			// txtTipoDocumento
			// 
			this.txtTipoDocumento.AutoSize = true;
			this.txtTipoDocumento.Location = new System.Drawing.Point(9, 55);
			this.txtTipoDocumento.Name = "txtTipoDocumento";
			this.txtTipoDocumento.Size = new System.Drawing.Size(89, 13);
			this.txtTipoDocumento.TabIndex = 13;
			this.txtTipoDocumento.Text = "Tipo Documento:";
			// 
			// cmbTipoDocumento
			// 
			this.cmbTipoDocumento.FormattingEnabled = true;
			this.cmbTipoDocumento.Location = new System.Drawing.Point(101, 52);
			this.cmbTipoDocumento.Name = "cmbTipoDocumento";
			this.cmbTipoDocumento.Size = new System.Drawing.Size(199, 21);
			this.cmbTipoDocumento.TabIndex = 14;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(320, 55);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(105, 13);
			this.label1.TabIndex = 15;
			this.label1.Text = "Aplicación a asignar:";
			// 
			// cmbAplicacion
			// 
			this.cmbAplicacion.FormattingEnabled = true;
			this.cmbAplicacion.Location = new System.Drawing.Point(426, 52);
			this.cmbAplicacion.Name = "cmbAplicacion";
			this.cmbAplicacion.Size = new System.Drawing.Size(199, 21);
			this.cmbAplicacion.TabIndex = 16;
			// 
			// Transacciones
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(717, 311);
			this.Controls.Add(this.cmbAplicacion);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cmbTipoDocumento);
			this.Controls.Add(this.txtTipoDocumento);
			this.Controls.Add(this.dgListaResultados);
			this.Controls.Add(this.btnBuscarDBMotor);
			this.Controls.Add(this.btnGuardarDBMotor);
			this.Controls.Add(this.btnBuscarDyP);
			this.Name = "Transacciones";
			this.Text = "Transacciones";
			this.Load += new System.EventHandler(this.Transacciones_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgListaResultados)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBuscarDyP;
        private System.Windows.Forms.Button btnGuardarDBMotor;
        private System.Windows.Forms.Button btnBuscarDBMotor;
        private System.Windows.Forms.DataGridView dgListaResultados;
        private System.Windows.Forms.Label txtTipoDocumento;
        private System.Windows.Forms.ComboBox cmbTipoDocumento;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbAplicacion;
    }
}