namespace MonitorPaquetes
{
    partial class Vista_Correos
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
            this.PanelEdit = new System.Windows.Forms.Panel();
            this.btnVPrevia = new System.Windows.Forms.Button();
            this.rtxtCodCorreo = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.PanelEdit.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelEdit
            // 
            this.PanelEdit.Controls.Add(this.btnVPrevia);
            this.PanelEdit.Controls.Add(this.rtxtCodCorreo);
            this.PanelEdit.Controls.Add(this.button1);
            this.PanelEdit.Location = new System.Drawing.Point(12, 29);
            this.PanelEdit.Name = "PanelEdit";
            this.PanelEdit.Size = new System.Drawing.Size(596, 586);
            this.PanelEdit.TabIndex = 5;
            // 
            // btnVPrevia
            // 
            this.btnVPrevia.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVPrevia.Location = new System.Drawing.Point(3, 552);
            this.btnVPrevia.Name = "btnVPrevia";
            this.btnVPrevia.Size = new System.Drawing.Size(90, 23);
            this.btnVPrevia.TabIndex = 3;
            this.btnVPrevia.Text = "Vista Previa";
            this.btnVPrevia.UseVisualStyleBackColor = true;
            this.btnVPrevia.Click += new System.EventHandler(this.btnVPrevia_Click);
            // 
            // rtxtCodCorreo
            // 
            this.rtxtCodCorreo.Location = new System.Drawing.Point(3, 7);
            this.rtxtCodCorreo.Name = "rtxtCodCorreo";
            this.rtxtCodCorreo.Size = new System.Drawing.Size(590, 543);
            this.rtxtCodCorreo.TabIndex = 1;
            this.rtxtCodCorreo.Text = "";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(503, 552);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Guardar";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(214, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(197, 18);
            this.label1.TabIndex = 6;
            this.label1.Text = "Codigo HTML del Correo";
            // 
            // Vista_Correos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 626);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PanelEdit);
            this.Name = "Vista_Correos";
            this.Text = "Vista_Correos";
            this.Load += new System.EventHandler(this.Vista_Correos_Load);
            this.PanelEdit.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel PanelEdit;
        private System.Windows.Forms.Button btnVPrevia;
        private System.Windows.Forms.RichTextBox rtxtCodCorreo;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
    }
}