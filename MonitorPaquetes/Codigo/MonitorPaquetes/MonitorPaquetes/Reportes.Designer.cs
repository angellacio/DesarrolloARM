namespace MonitorPaquetes
{
    partial class Reportes
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
            this.gpReporte = new System.Windows.Forms.GroupBox();
            this.dtgReportes = new System.Windows.Forms.DataGridView();
            this.btnGenerar = new System.Windows.Forms.Button();
            this.cbMeses = new System.Windows.Forms.ComboBox();
            this.lblMes = new System.Windows.Forms.Label();
            this.btnAuditoria = new System.Windows.Forms.Button();
            this.txtFechaAuditoria = new System.Windows.Forms.TextBox();
            this.lblFecha = new System.Windows.Forms.Label();
            this.lblToolTip = new System.Windows.Forms.Label();
            this.gpReporte.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgReportes)).BeginInit();
            this.SuspendLayout();
            // 
            // gpReporte
            // 
            this.gpReporte.Controls.Add(this.dtgReportes);
            this.gpReporte.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpReporte.Location = new System.Drawing.Point(13, 35);
            this.gpReporte.Name = "gpReporte";
            this.gpReporte.Size = new System.Drawing.Size(860, 409);
            this.gpReporte.TabIndex = 0;
            this.gpReporte.TabStop = false;
            this.gpReporte.Text = "Reporte";
            // 
            // dtgReportes
            // 
            this.dtgReportes.AllowUserToAddRows = false;
            this.dtgReportes.AllowUserToDeleteRows = false;
            this.dtgReportes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dtgReportes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgReportes.Location = new System.Drawing.Point(7, 20);
            this.dtgReportes.Name = "dtgReportes";
            this.dtgReportes.ReadOnly = true;
            this.dtgReportes.Size = new System.Drawing.Size(847, 383);
            this.dtgReportes.TabIndex = 0;
            // 
            // btnGenerar
            // 
            this.btnGenerar.Enabled = false;
            this.btnGenerar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerar.Location = new System.Drawing.Point(396, 444);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(144, 39);
            this.btnGenerar.TabIndex = 1;
            this.btnGenerar.Text = "Generar Excel";
            this.btnGenerar.UseMnemonic = false;
            this.btnGenerar.UseVisualStyleBackColor = true;
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
            // 
            // cbMeses
            // 
            this.cbMeses.FormattingEnabled = true;
            this.cbMeses.Items.AddRange(new object[] {
            "",
            "Enero",
            "Febrero",
            "Marzo",
            "Abril",
            "Mayo",
            "Junio",
            "Julio",
            "Agosto",
            "Septiembre",
            "Octubre",
            "Nobiembre",
            "Diciembre"});
            this.cbMeses.Location = new System.Drawing.Point(752, 12);
            this.cbMeses.Name = "cbMeses";
            this.cbMeses.Size = new System.Drawing.Size(121, 21);
            this.cbMeses.TabIndex = 2;
            this.cbMeses.SelectedIndexChanged += new System.EventHandler(this.cbMeses_SelectedIndexChanged);
            // 
            // lblMes
            // 
            this.lblMes.AutoSize = true;
            this.lblMes.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMes.Location = new System.Drawing.Point(711, 14);
            this.lblMes.Name = "lblMes";
            this.lblMes.Size = new System.Drawing.Size(38, 17);
            this.lblMes.TabIndex = 3;
            this.lblMes.Text = "Mes:";
            // 
            // btnAuditoria
            // 
            this.btnAuditoria.Enabled = false;
            this.btnAuditoria.Location = new System.Drawing.Point(232, 4);
            this.btnAuditoria.Name = "btnAuditoria";
            this.btnAuditoria.Size = new System.Drawing.Size(75, 23);
            this.btnAuditoria.TabIndex = 4;
            this.btnAuditoria.Text = "Buscar";
            this.btnAuditoria.UseVisualStyleBackColor = true;
            this.btnAuditoria.Click += new System.EventHandler(this.btnAuditoria_Click);
            // 
            // txtFechaAuditoria
            // 
            this.txtFechaAuditoria.Enabled = false;
            this.txtFechaAuditoria.Location = new System.Drawing.Point(126, 6);
            this.txtFechaAuditoria.Name = "txtFechaAuditoria";
            this.txtFechaAuditoria.Size = new System.Drawing.Size(100, 20);
            this.txtFechaAuditoria.TabIndex = 5;
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Enabled = false;
            this.lblFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFecha.Location = new System.Drawing.Point(17, 8);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(109, 15);
            this.lblFecha.TabIndex = 6;
            this.lblFecha.Text = "Fecha de Inicio:";
            // 
            // lblToolTip
            // 
            this.lblToolTip.AutoSize = true;
            this.lblToolTip.Enabled = false;
            this.lblToolTip.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToolTip.Location = new System.Drawing.Point(123, 23);
            this.lblToolTip.Name = "lblToolTip";
            this.lblToolTip.Size = new System.Drawing.Size(106, 13);
            this.lblToolTip.TabIndex = 7;
            this.lblToolTip.Text = "Formato aaaa/mm/dd";
            // 
            // Reportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(885, 491);
            this.Controls.Add(this.lblToolTip);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.txtFechaAuditoria);
            this.Controls.Add(this.btnAuditoria);
            this.Controls.Add(this.lblMes);
            this.Controls.Add(this.cbMeses);
            this.Controls.Add(this.btnGenerar);
            this.Controls.Add(this.gpReporte);
            this.Name = "Reportes";
            this.Text = "Reportes";
            this.Load += new System.EventHandler(this.Reportes_Load);
            this.gpReporte.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgReportes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gpReporte;
        private System.Windows.Forms.DataGridView dtgReportes;
        private System.Windows.Forms.Button btnGenerar;
        private System.Windows.Forms.ComboBox cbMeses;
        private System.Windows.Forms.Label lblMes;
        private System.Windows.Forms.Button btnAuditoria;
        private System.Windows.Forms.TextBox txtFechaAuditoria;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Label lblToolTip;
    }
}