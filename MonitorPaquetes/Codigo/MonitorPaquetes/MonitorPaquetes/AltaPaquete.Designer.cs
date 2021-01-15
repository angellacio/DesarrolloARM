namespace MonitorPaquetes
{
    partial class AltaPaquete
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
            this.gbDatosPaq = new System.Windows.Forms.GroupBox();
            this.btnRegistra = new System.Windows.Forms.Button();
            this.chkPPMC = new System.Windows.Forms.CheckBox();
            this.cbDesarrollador = new System.Windows.Forms.ComboBox();
            this.lbDesarrollador = new System.Windows.Forms.Label();
            this.txtPaquetesRela = new System.Windows.Forms.TextBox();
            this.lbPaquetesRela = new System.Windows.Forms.Label();
            this.txtIncidentes = new System.Windows.Forms.TextBox();
            this.lbIncidentes = new System.Windows.Forms.Label();
            this.txtPaquete = new System.Windows.Forms.TextBox();
            this.lbPaquete = new System.Windows.Forms.Label();
            this.gbDatosPaq.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbDatosPaq
            // 
            this.gbDatosPaq.Controls.Add(this.btnRegistra);
            this.gbDatosPaq.Controls.Add(this.chkPPMC);
            this.gbDatosPaq.Controls.Add(this.cbDesarrollador);
            this.gbDatosPaq.Controls.Add(this.lbDesarrollador);
            this.gbDatosPaq.Controls.Add(this.txtPaquetesRela);
            this.gbDatosPaq.Controls.Add(this.lbPaquetesRela);
            this.gbDatosPaq.Controls.Add(this.txtIncidentes);
            this.gbDatosPaq.Controls.Add(this.lbIncidentes);
            this.gbDatosPaq.Controls.Add(this.txtPaquete);
            this.gbDatosPaq.Controls.Add(this.lbPaquete);
            this.gbDatosPaq.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbDatosPaq.Location = new System.Drawing.Point(12, 12);
            this.gbDatosPaq.Name = "gbDatosPaq";
            this.gbDatosPaq.Size = new System.Drawing.Size(337, 288);
            this.gbDatosPaq.TabIndex = 0;
            this.gbDatosPaq.TabStop = false;
            this.gbDatosPaq.Text = "Datos del Paquete:";
            // 
            // btnRegistra
            // 
            this.btnRegistra.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegistra.Location = new System.Drawing.Point(135, 249);
            this.btnRegistra.Name = "btnRegistra";
            this.btnRegistra.Size = new System.Drawing.Size(75, 33);
            this.btnRegistra.TabIndex = 9;
            this.btnRegistra.Text = "Registrar";
            this.btnRegistra.UseVisualStyleBackColor = true;
            this.btnRegistra.Click += new System.EventHandler(this.btnRegistra_Click);
            // 
            // chkPPMC
            // 
            this.chkPPMC.AutoSize = true;
            this.chkPPMC.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkPPMC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPPMC.Location = new System.Drawing.Point(247, 22);
            this.chkPPMC.Name = "chkPPMC";
            this.chkPPMC.Size = new System.Drawing.Size(78, 19);
            this.chkPPMC.TabIndex = 8;
            this.chkPPMC.Text = "Es PPMC";
            this.chkPPMC.UseVisualStyleBackColor = true;
            // 
            // cbDesarrollador
            // 
            this.cbDesarrollador.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDesarrollador.FormattingEnabled = true;
            this.cbDesarrollador.Location = new System.Drawing.Point(126, 45);
            this.cbDesarrollador.Name = "cbDesarrollador";
            this.cbDesarrollador.Size = new System.Drawing.Size(206, 23);
            this.cbDesarrollador.TabIndex = 7;
            // 
            // lbDesarrollador
            // 
            this.lbDesarrollador.AutoSize = true;
            this.lbDesarrollador.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDesarrollador.Location = new System.Drawing.Point(7, 48);
            this.lbDesarrollador.Name = "lbDesarrollador";
            this.lbDesarrollador.Size = new System.Drawing.Size(99, 15);
            this.lbDesarrollador.TabIndex = 6;
            this.lbDesarrollador.Text = "Desarrollador:";
            // 
            // txtPaquetesRela
            // 
            this.txtPaquetesRela.Enabled = false;
            this.txtPaquetesRela.Location = new System.Drawing.Point(126, 158);
            this.txtPaquetesRela.Multiline = true;
            this.txtPaquetesRela.Name = "txtPaquetesRela";
            this.txtPaquetesRela.Size = new System.Drawing.Size(206, 81);
            this.txtPaquetesRela.TabIndex = 5;
            // 
            // lbPaquetesRela
            // 
            this.lbPaquetesRela.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPaquetesRela.Location = new System.Drawing.Point(7, 158);
            this.lbPaquetesRela.Name = "lbPaquetesRela";
            this.lbPaquetesRela.Size = new System.Drawing.Size(105, 47);
            this.lbPaquetesRela.TabIndex = 4;
            this.lbPaquetesRela.Text = "Paquete(s) Relacionado(s):";
            // 
            // txtIncidentes
            // 
            this.txtIncidentes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIncidentes.Location = new System.Drawing.Point(126, 71);
            this.txtIncidentes.Multiline = true;
            this.txtIncidentes.Name = "txtIncidentes";
            this.txtIncidentes.Size = new System.Drawing.Size(206, 81);
            this.txtIncidentes.TabIndex = 3;
            // 
            // lbIncidentes
            // 
            this.lbIncidentes.AutoSize = true;
            this.lbIncidentes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbIncidentes.Location = new System.Drawing.Point(7, 75);
            this.lbIncidentes.Name = "lbIncidentes";
            this.lbIncidentes.Size = new System.Drawing.Size(87, 15);
            this.lbIncidentes.TabIndex = 2;
            this.lbIncidentes.Text = "Incidente(s):";
            // 
            // txtPaquete
            // 
            this.txtPaquete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPaquete.Location = new System.Drawing.Point(126, 20);
            this.txtPaquete.MaxLength = 12;
            this.txtPaquete.Name = "txtPaquete";
            this.txtPaquete.Size = new System.Drawing.Size(113, 21);
            this.txtPaquete.TabIndex = 1;
            // 
            // lbPaquete
            // 
            this.lbPaquete.AutoSize = true;
            this.lbPaquete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPaquete.Location = new System.Drawing.Point(7, 20);
            this.lbPaquete.Name = "lbPaquete";
            this.lbPaquete.Size = new System.Drawing.Size(119, 15);
            this.lbPaquete.TabIndex = 0;
            this.lbPaquete.Text = "Nombre Paquete:";
            // 
            // AltaPaquete
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(361, 311);
            this.Controls.Add(this.gbDatosPaq);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AltaPaquete";
            this.Text = "Alta Paquete";
            this.Load += new System.EventHandler(this.AltaPaquete_Load);
            this.gbDatosPaq.ResumeLayout(false);
            this.gbDatosPaq.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbDatosPaq;
        private System.Windows.Forms.ComboBox cbDesarrollador;
        private System.Windows.Forms.Label lbDesarrollador;
        private System.Windows.Forms.TextBox txtPaquetesRela;
        private System.Windows.Forms.Label lbPaquetesRela;
        private System.Windows.Forms.TextBox txtIncidentes;
        private System.Windows.Forms.Label lbIncidentes;
        private System.Windows.Forms.TextBox txtPaquete;
        private System.Windows.Forms.Label lbPaquete;
        private System.Windows.Forms.Button btnRegistra;
        private System.Windows.Forms.CheckBox chkPPMC;
    }
}