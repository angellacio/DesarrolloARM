namespace CargaDeCatalogos.Catalogos
{
    partial class ALR
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
			this.dgALR = new System.Windows.Forms.DataGridView();
			this.btnBuscarALR = new System.Windows.Forms.Button();
			this.txtIDOrigen = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.dgALR)).BeginInit();
			this.SuspendLayout();
			// 
			// dgALR
			// 
			this.dgALR.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgALR.Location = new System.Drawing.Point(12, 56);
			this.dgALR.Name = "dgALR";
			this.dgALR.Size = new System.Drawing.Size(845, 312);
			this.dgALR.TabIndex = 15;
			// 
			// btnBuscarALR
			// 
			this.btnBuscarALR.Location = new System.Drawing.Point(166, 6);
			this.btnBuscarALR.Name = "btnBuscarALR";
			this.btnBuscarALR.Size = new System.Drawing.Size(119, 23);
			this.btnBuscarALR.TabIndex = 14;
			this.btnBuscarALR.Text = "Buscar ALR";
			this.btnBuscarALR.UseVisualStyleBackColor = true;
			this.btnBuscarALR.Click += new System.EventHandler(this.btnBuscarALR_Click);
			// 
			// txtIDOrigen
			// 
			this.txtIDOrigen.Location = new System.Drawing.Point(67, 8);
			this.txtIDOrigen.Name = "txtIDOrigen";
			this.txtIDOrigen.Size = new System.Drawing.Size(93, 20);
			this.txtIDOrigen.TabIndex = 13;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 11);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(52, 13);
			this.label1.TabIndex = 12;
			this.label1.Text = "ID Origen";
			// 
			// ALR
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(877, 387);
			this.Controls.Add(this.dgALR);
			this.Controls.Add(this.btnBuscarALR);
			this.Controls.Add(this.txtIDOrigen);
			this.Controls.Add(this.label1);
			this.Name = "ALR";
			this.Text = "ALR";
			((System.ComponentModel.ISupportInitialize)(this.dgALR)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgALR;
        private System.Windows.Forms.Button btnBuscarALR;
        private System.Windows.Forms.TextBox txtIDOrigen;
        private System.Windows.Forms.Label label1;
    }
}