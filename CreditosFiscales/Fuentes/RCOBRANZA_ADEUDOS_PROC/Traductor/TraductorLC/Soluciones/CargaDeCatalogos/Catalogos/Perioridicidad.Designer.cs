namespace CargaDeCatalogos.Catalogos
{
    partial class Perioridicidad
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
            this.dgPeriodicidad = new System.Windows.Forms.DataGridView();
            this.btnBuscarPeriodicidad = new System.Windows.Forms.Button();
            this.txtIDOrigen = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgPeriodicidad)).BeginInit();
            this.SuspendLayout();
            // 
            // dgPeriodicidad
            // 
            this.dgPeriodicidad.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPeriodicidad.Location = new System.Drawing.Point(12, 56);
            this.dgPeriodicidad.Name = "dgPeriodicidad";
            this.dgPeriodicidad.Size = new System.Drawing.Size(845, 312);
            this.dgPeriodicidad.TabIndex = 7;
            // 
            // btnBuscarPeriodicidad
            // 
            this.btnBuscarPeriodicidad.Location = new System.Drawing.Point(166, 6);
            this.btnBuscarPeriodicidad.Name = "btnBuscarPeriodicidad";
            this.btnBuscarPeriodicidad.Size = new System.Drawing.Size(119, 23);
            this.btnBuscarPeriodicidad.TabIndex = 6;
            this.btnBuscarPeriodicidad.Text = "Buscar Periodicidades";
            this.btnBuscarPeriodicidad.UseVisualStyleBackColor = true;
            this.btnBuscarPeriodicidad.Click += new System.EventHandler(this.btnBuscarPeriodicidad_Click);
            // 
            // txtIDOrigen
            // 
            this.txtIDOrigen.Location = new System.Drawing.Point(67, 8);
            this.txtIDOrigen.Name = "txtIDOrigen";
            this.txtIDOrigen.Size = new System.Drawing.Size(71, 20);
            this.txtIDOrigen.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "ID Origen";
            // 
            // Perioridicidad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(874, 384);
            this.Controls.Add(this.dgPeriodicidad);
            this.Controls.Add(this.btnBuscarPeriodicidad);
            this.Controls.Add(this.txtIDOrigen);
            this.Controls.Add(this.label1);
            this.Name = "Perioridicidad";
            this.Text = "Perioridicidad";
            ((System.ComponentModel.ISupportInitialize)(this.dgPeriodicidad)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgPeriodicidad;
        private System.Windows.Forms.Button btnBuscarPeriodicidad;
        private System.Windows.Forms.TextBox txtIDOrigen;
        private System.Windows.Forms.Label label1;
    }
}