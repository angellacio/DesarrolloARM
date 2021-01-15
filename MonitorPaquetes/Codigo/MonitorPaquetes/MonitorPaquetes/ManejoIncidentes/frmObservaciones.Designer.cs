namespace MonitorPaquetes.ManejoIncidentes
{
    partial class frmObservaciones
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dtgObservaciones = new System.Windows.Forms.DataGridView();
            this.gbObservacion = new System.Windows.Forms.GroupBox();
            this.dgcID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcIncidente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcPaquete = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcMensaje = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtObservaciones = new System.Windows.Forms.TextBox();
            this.btnAccion = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.lblID = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dtgObservaciones)).BeginInit();
            this.gbObservacion.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtgObservaciones
            // 
            this.dtgObservaciones.AllowUserToAddRows = false;
            this.dtgObservaciones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgObservaciones.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dtgObservaciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgObservaciones.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcID,
            this.dgcIncidente,
            this.dgcPaquete,
            this.dgcFecha,
            this.dgcMensaje});
            this.dtgObservaciones.Location = new System.Drawing.Point(12, 172);
            this.dtgObservaciones.Name = "dtgObservaciones";
            this.dtgObservaciones.Size = new System.Drawing.Size(443, 170);
            this.dtgObservaciones.TabIndex = 200;
            this.dtgObservaciones.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgObservaciones_CellClick);
            // 
            // gbObservacion
            // 
            this.gbObservacion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbObservacion.Controls.Add(this.lblID);
            this.gbObservacion.Controls.Add(this.btnLimpiar);
            this.gbObservacion.Controls.Add(this.btnAccion);
            this.gbObservacion.Controls.Add(this.txtObservaciones);
            this.gbObservacion.Location = new System.Drawing.Point(13, 13);
            this.gbObservacion.Name = "gbObservacion";
            this.gbObservacion.Size = new System.Drawing.Size(442, 153);
            this.gbObservacion.TabIndex = 1;
            this.gbObservacion.TabStop = false;
            this.gbObservacion.Text = "Observación";
            // 
            // dgcID
            // 
            this.dgcID.DataPropertyName = "D000_nID";
            this.dgcID.HeaderText = "ID";
            this.dgcID.Name = "dgcID";
            this.dgcID.Width = 43;
            // 
            // dgcIncidente
            // 
            this.dgcIncidente.DataPropertyName = "D001_sIdIncidentes";
            this.dgcIncidente.HeaderText = "Incidente";
            this.dgcIncidente.Name = "dgcIncidente";
            this.dgcIncidente.Visible = false;
            this.dgcIncidente.Width = 76;
            // 
            // dgcPaquete
            // 
            this.dgcPaquete.DataPropertyName = "D002_sIdPaquete";
            this.dgcPaquete.HeaderText = "Paquete";
            this.dgcPaquete.Name = "dgcPaquete";
            this.dgcPaquete.Visible = false;
            this.dgcPaquete.Width = 72;
            // 
            // dgcFecha
            // 
            this.dgcFecha.DataPropertyName = "D010_dMensaje";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Format = "dd/MM/yyyy HH:mm";
            this.dgcFecha.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgcFecha.HeaderText = "Fecha";
            this.dgcFecha.Name = "dgcFecha";
            this.dgcFecha.Width = 62;
            // 
            // dgcMensaje
            // 
            this.dgcMensaje.DataPropertyName = "D020_sMensaje";
            this.dgcMensaje.HeaderText = "Observaciones";
            this.dgcMensaje.Name = "dgcMensaje";
            this.dgcMensaje.Width = 103;
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtObservaciones.Location = new System.Drawing.Point(7, 20);
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.Size = new System.Drawing.Size(347, 127);
            this.txtObservaciones.TabIndex = 50;
            // 
            // btnAccion
            // 
            this.btnAccion.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnAccion.Location = new System.Drawing.Point(361, 37);
            this.btnAccion.Name = "btnAccion";
            this.btnAccion.Size = new System.Drawing.Size(75, 69);
            this.btnAccion.TabIndex = 100;
            this.btnAccion.Text = "Accion";
            this.btnAccion.UseVisualStyleBackColor = true;
            this.btnAccion.Click += new System.EventHandler(this.btnAccion_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnLimpiar.Location = new System.Drawing.Point(360, 112);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(75, 23);
            this.btnLimpiar.TabIndex = 150;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(360, 16);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(0, 13);
            this.lblID.TabIndex = 151;
            // 
            // frmObservaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 354);
            this.Controls.Add(this.gbObservacion);
            this.Controls.Add(this.dtgObservaciones);
            this.Name = "frmObservaciones";
            this.Text = "frmObservaciones";
            this.Load += new System.EventHandler(this.frmObservaciones_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgObservaciones)).EndInit();
            this.gbObservacion.ResumeLayout(false);
            this.gbObservacion.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dtgObservaciones;
        private System.Windows.Forms.GroupBox gbObservacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcID;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcIncidente;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcPaquete;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcFecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcMensaje;
        private System.Windows.Forms.Button btnAccion;
        private System.Windows.Forms.TextBox txtObservaciones;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Label lblID;
    }
}