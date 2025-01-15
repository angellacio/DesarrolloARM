namespace CargaDeCatalogos.Catalogos
{
    partial class ConceptoPago
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
			this.dgConceptos = new System.Windows.Forms.DataGridView();
			this.btnBuscarConcepto = new System.Windows.Forms.Button();
			this.txtIDOrigen = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.dgConceptos)).BeginInit();
			this.SuspendLayout();
			// 
			// dgConceptos
			// 
			this.dgConceptos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgConceptos.Location = new System.Drawing.Point(12, 56);
			this.dgConceptos.Name = "dgConceptos";
			this.dgConceptos.Size = new System.Drawing.Size(845, 312);
			this.dgConceptos.TabIndex = 11;
			// 
			// btnBuscarConcepto
			// 
			this.btnBuscarConcepto.Location = new System.Drawing.Point(166, 6);
			this.btnBuscarConcepto.Name = "btnBuscarConcepto";
			this.btnBuscarConcepto.Size = new System.Drawing.Size(119, 23);
			this.btnBuscarConcepto.TabIndex = 10;
			this.btnBuscarConcepto.Text = "Buscar Conceptos";
			this.btnBuscarConcepto.UseVisualStyleBackColor = true;
			this.btnBuscarConcepto.Click += new System.EventHandler(this.btnBuscarConcepto_Click);
			// 
			// txtIDOrigen
			// 
			this.txtIDOrigen.Location = new System.Drawing.Point(67, 8);
			this.txtIDOrigen.Name = "txtIDOrigen";
			this.txtIDOrigen.Size = new System.Drawing.Size(93, 20);
			this.txtIDOrigen.TabIndex = 9;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 11);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(52, 13);
			this.label1.TabIndex = 8;
			this.label1.Text = "ID Origen";
			// 
			// ConceptoPago
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(874, 391);
			this.Controls.Add(this.dgConceptos);
			this.Controls.Add(this.btnBuscarConcepto);
			this.Controls.Add(this.txtIDOrigen);
			this.Controls.Add(this.label1);
			this.Name = "ConceptoPago";
			this.Text = "ConceptoPago";
			((System.ComponentModel.ISupportInitialize)(this.dgConceptos)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgConceptos;
        private System.Windows.Forms.Button btnBuscarConcepto;
        private System.Windows.Forms.TextBox txtIDOrigen;
        private System.Windows.Forms.Label label1;

    }
}