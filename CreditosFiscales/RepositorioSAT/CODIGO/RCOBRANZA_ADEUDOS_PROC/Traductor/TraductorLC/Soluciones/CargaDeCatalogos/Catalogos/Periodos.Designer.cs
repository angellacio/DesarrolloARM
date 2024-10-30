namespace CargaDeCatalogos.Catalogos
{
    partial class Periodos
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
            this.txtIDOrigen = new System.Windows.Forms.TextBox();
            this.btnBuscarPeriodo = new System.Windows.Forms.Button();
            this.dgPeriodos = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgPeriodos)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID Origen";
            // 
            // txtIDOrigen
            // 
            this.txtIDOrigen.Location = new System.Drawing.Point(70, 6);
            this.txtIDOrigen.Name = "txtIDOrigen";
            this.txtIDOrigen.Size = new System.Drawing.Size(71, 20);
            this.txtIDOrigen.TabIndex = 1;
            // 
            // btnBuscarPeriodo
            // 
            this.btnBuscarPeriodo.Location = new System.Drawing.Point(169, 4);
            this.btnBuscarPeriodo.Name = "btnBuscarPeriodo";
            this.btnBuscarPeriodo.Size = new System.Drawing.Size(119, 23);
            this.btnBuscarPeriodo.TabIndex = 2;
            this.btnBuscarPeriodo.Text = "Buscar Periodos";
            this.btnBuscarPeriodo.UseVisualStyleBackColor = true;
            this.btnBuscarPeriodo.Click += new System.EventHandler(this.btnBuscarPeriodo_Click);
            // 
            // dgPeriodos
            // 
            this.dgPeriodos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPeriodos.Location = new System.Drawing.Point(15, 54);
            this.dgPeriodos.Name = "dgPeriodos";
            this.dgPeriodos.Size = new System.Drawing.Size(845, 312);
            this.dgPeriodos.TabIndex = 3;
            // 
            // Periodos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(872, 378);
            this.Controls.Add(this.dgPeriodos);
            this.Controls.Add(this.btnBuscarPeriodo);
            this.Controls.Add(this.txtIDOrigen);
            this.Controls.Add(this.label1);
            this.Name = "Periodos";
            this.Text = "Periodos";
            ((System.ComponentModel.ISupportInitialize)(this.dgPeriodos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtIDOrigen;
        private System.Windows.Forms.Button btnBuscarPeriodo;
        private System.Windows.Forms.DataGridView dgPeriodos;
    }
}