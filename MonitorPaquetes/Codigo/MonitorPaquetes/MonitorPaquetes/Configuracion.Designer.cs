namespace MonitorPaquetes
{
    partial class Configuracion
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
            this.dgvConfiguracion = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConfiguracion)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvConfiguracion
            // 
            this.dgvConfiguracion.AllowUserToAddRows = false;
            this.dgvConfiguracion.AllowUserToDeleteRows = false;
            this.dgvConfiguracion.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvConfiguracion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvConfiguracion.Location = new System.Drawing.Point(13, 13);
            this.dgvConfiguracion.Name = "dgvConfiguracion";
            this.dgvConfiguracion.ReadOnly = true;
            this.dgvConfiguracion.Size = new System.Drawing.Size(499, 412);
            this.dgvConfiguracion.TabIndex = 0;
            // 
            // Configuracion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 437);
            this.Controls.Add(this.dgvConfiguracion);
            this.Name = "Configuracion";
            this.Text = "Configuracion";
            this.Load += new System.EventHandler(this.Configuracion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvConfiguracion)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvConfiguracion;
    }
}