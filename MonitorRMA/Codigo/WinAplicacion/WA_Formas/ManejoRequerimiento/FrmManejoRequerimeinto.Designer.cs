namespace WA_Formas.ManejoRequerimiento
{
    partial class FrmManejoRequerimeinto
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
            this.ScRequerimiento = new System.Windows.Forms.SplitContainer();
            this.gbRequerimeinto = new System.Windows.Forms.GroupBox();
            this.GbReqTipo = new System.Windows.Forms.GroupBox();
            this.RdbRT_RMA = new System.Windows.Forms.RadioButton();
            this.RdbRT_MantMay = new System.Windows.Forms.RadioButton();
            this.RdbRT_Servicio = new System.Windows.Forms.RadioButton();
            this.RdbRT_MantMen = new System.Windows.Forms.RadioButton();
            this.LblReqEstatus = new System.Windows.Forms.Label();
            this.CmbReqEstatus = new System.Windows.Forms.ComboBox();
            this.CmbAplicativo = new System.Windows.Forms.ComboBox();
            this.lblReqAplicativo = new System.Windows.Forms.Label();
            this.CmbResponsable = new System.Windows.Forms.ComboBox();
            this.LblResponsable = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.txtReqNumeroREQS = new System.Windows.Forms.TextBox();
            this.lblReqRequest = new System.Windows.Forms.Label();
            this.PnlReqRMA = new System.Windows.Forms.Panel();
            this.lblReqRFC = new System.Windows.Forms.Label();
            this.txtReqRFC = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.ScRequerimiento)).BeginInit();
            this.ScRequerimiento.Panel1.SuspendLayout();
            this.ScRequerimiento.SuspendLayout();
            this.gbRequerimeinto.SuspendLayout();
            this.GbReqTipo.SuspendLayout();
            this.PnlReqRMA.SuspendLayout();
            this.SuspendLayout();
            // 
            // ScRequerimiento
            // 
            this.ScRequerimiento.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ScRequerimiento.Location = new System.Drawing.Point(0, 0);
            this.ScRequerimiento.Name = "ScRequerimiento";
            // 
            // ScRequerimiento.Panel1
            // 
            this.ScRequerimiento.Panel1.AccessibleName = "PnRequerimeinto";
            this.ScRequerimiento.Panel1.Controls.Add(this.gbRequerimeinto);
            this.ScRequerimiento.Size = new System.Drawing.Size(800, 450);
            this.ScRequerimiento.SplitterDistance = 282;
            this.ScRequerimiento.TabIndex = 0;
            // 
            // gbRequerimeinto
            // 
            this.gbRequerimeinto.AutoSize = true;
            this.gbRequerimeinto.Controls.Add(this.PnlReqRMA);
            this.gbRequerimeinto.Controls.Add(this.textBox1);
            this.gbRequerimeinto.Controls.Add(this.label1);
            this.gbRequerimeinto.Controls.Add(this.CmbResponsable);
            this.gbRequerimeinto.Controls.Add(this.LblResponsable);
            this.gbRequerimeinto.Controls.Add(this.CmbAplicativo);
            this.gbRequerimeinto.Controls.Add(this.lblReqAplicativo);
            this.gbRequerimeinto.Controls.Add(this.CmbReqEstatus);
            this.gbRequerimeinto.Controls.Add(this.LblReqEstatus);
            this.gbRequerimeinto.Controls.Add(this.GbReqTipo);
            this.gbRequerimeinto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbRequerimeinto.Location = new System.Drawing.Point(0, 0);
            this.gbRequerimeinto.Name = "gbRequerimeinto";
            this.gbRequerimeinto.Size = new System.Drawing.Size(282, 450);
            this.gbRequerimeinto.TabIndex = 0;
            this.gbRequerimeinto.TabStop = false;
            this.gbRequerimeinto.Text = "ID: {0}";
            // 
            // GbReqTipo
            // 
            this.GbReqTipo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GbReqTipo.Controls.Add(this.RdbRT_Servicio);
            this.GbReqTipo.Controls.Add(this.RdbRT_MantMen);
            this.GbReqTipo.Controls.Add(this.RdbRT_MantMay);
            this.GbReqTipo.Controls.Add(this.RdbRT_RMA);
            this.GbReqTipo.Location = new System.Drawing.Point(6, 19);
            this.GbReqTipo.Name = "GbReqTipo";
            this.GbReqTipo.Size = new System.Drawing.Size(273, 52);
            this.GbReqTipo.TabIndex = 1;
            this.GbReqTipo.TabStop = false;
            this.GbReqTipo.Text = "Tipo Requerimiento";
            // 
            // RdbRT_RMA
            // 
            this.RdbRT_RMA.AutoSize = true;
            this.RdbRT_RMA.Checked = true;
            this.RdbRT_RMA.Location = new System.Drawing.Point(7, 19);
            this.RdbRT_RMA.Name = "RdbRT_RMA";
            this.RdbRT_RMA.Size = new System.Drawing.Size(49, 17);
            this.RdbRT_RMA.TabIndex = 1;
            this.RdbRT_RMA.TabStop = true;
            this.RdbRT_RMA.Text = "RMA";
            this.RdbRT_RMA.UseVisualStyleBackColor = true;
            // 
            // RdbRT_MantMay
            // 
            this.RdbRT_MantMay.Location = new System.Drawing.Point(203, 12);
            this.RdbRT_MantMay.Name = "RdbRT_MantMay";
            this.RdbRT_MantMay.Size = new System.Drawing.Size(64, 30);
            this.RdbRT_MantMay.TabIndex = 4;
            this.RdbRT_MantMay.Text = "Mant. Mayor";
            this.RdbRT_MantMay.UseVisualStyleBackColor = true;
            // 
            // RdbRT_Servicio
            // 
            this.RdbRT_Servicio.AutoSize = true;
            this.RdbRT_Servicio.Location = new System.Drawing.Point(62, 19);
            this.RdbRT_Servicio.Name = "RdbRT_Servicio";
            this.RdbRT_Servicio.Size = new System.Drawing.Size(63, 17);
            this.RdbRT_Servicio.TabIndex = 2;
            this.RdbRT_Servicio.Text = "Servicio";
            this.RdbRT_Servicio.UseVisualStyleBackColor = true;
            // 
            // RdbRT_MantMen
            // 
            this.RdbRT_MantMen.Location = new System.Drawing.Point(131, 12);
            this.RdbRT_MantMen.Name = "RdbRT_MantMen";
            this.RdbRT_MantMen.Size = new System.Drawing.Size(66, 30);
            this.RdbRT_MantMen.TabIndex = 3;
            this.RdbRT_MantMen.Text = "Man. Menor";
            this.RdbRT_MantMen.UseVisualStyleBackColor = true;
            // 
            // LblReqEstatus
            // 
            this.LblReqEstatus.AutoSize = true;
            this.LblReqEstatus.Location = new System.Drawing.Point(3, 76);
            this.LblReqEstatus.Name = "LblReqEstatus";
            this.LblReqEstatus.Size = new System.Drawing.Size(45, 13);
            this.LblReqEstatus.TabIndex = 2;
            this.LblReqEstatus.Text = "Estatus:";
            // 
            // CmbReqEstatus
            // 
            this.CmbReqEstatus.FormattingEnabled = true;
            this.CmbReqEstatus.Location = new System.Drawing.Point(54, 73);
            this.CmbReqEstatus.Name = "CmbReqEstatus";
            this.CmbReqEstatus.Size = new System.Drawing.Size(219, 21);
            this.CmbReqEstatus.TabIndex = 10;
            // 
            // CmbAplicativo
            // 
            this.CmbAplicativo.FormattingEnabled = true;
            this.CmbAplicativo.Location = new System.Drawing.Point(6, 113);
            this.CmbAplicativo.Name = "CmbAplicativo";
            this.CmbAplicativo.Size = new System.Drawing.Size(267, 21);
            this.CmbAplicativo.TabIndex = 11;
            // 
            // lblReqAplicativo
            // 
            this.lblReqAplicativo.AutoSize = true;
            this.lblReqAplicativo.Location = new System.Drawing.Point(3, 97);
            this.lblReqAplicativo.Name = "lblReqAplicativo";
            this.lblReqAplicativo.Size = new System.Drawing.Size(56, 13);
            this.lblReqAplicativo.TabIndex = 4;
            this.lblReqAplicativo.Text = "Aplicativo:";
            // 
            // CmbResponsable
            // 
            this.CmbResponsable.FormattingEnabled = true;
            this.CmbResponsable.Location = new System.Drawing.Point(6, 153);
            this.CmbResponsable.Name = "CmbResponsable";
            this.CmbResponsable.Size = new System.Drawing.Size(267, 21);
            this.CmbResponsable.TabIndex = 12;
            // 
            // LblResponsable
            // 
            this.LblResponsable.AutoSize = true;
            this.LblResponsable.Location = new System.Drawing.Point(3, 137);
            this.LblResponsable.Name = "LblResponsable";
            this.LblResponsable.Size = new System.Drawing.Size(72, 13);
            this.LblResponsable.TabIndex = 6;
            this.LblResponsable.Text = "Responsable:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 177);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Número Requerimiento:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(4, 194);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(127, 20);
            this.textBox1.TabIndex = 14;
            // 
            // txtReqNumeroREQS
            // 
            this.txtReqNumeroREQS.Location = new System.Drawing.Point(-3, 16);
            this.txtReqNumeroREQS.Name = "txtReqNumeroREQS";
            this.txtReqNumeroREQS.Size = new System.Drawing.Size(130, 20);
            this.txtReqNumeroREQS.TabIndex = 16;
            // 
            // lblReqRequest
            // 
            this.lblReqRequest.AutoSize = true;
            this.lblReqRequest.Location = new System.Drawing.Point(3, 0);
            this.lblReqRequest.Name = "lblReqRequest";
            this.lblReqRequest.Size = new System.Drawing.Size(80, 13);
            this.lblReqRequest.TabIndex = 15;
            this.lblReqRequest.Text = "Número REQS:";
            // 
            // PnlReqRMA
            // 
            this.PnlReqRMA.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PnlReqRMA.Controls.Add(this.txtReqRFC);
            this.PnlReqRMA.Controls.Add(this.lblReqRFC);
            this.PnlReqRMA.Controls.Add(this.lblReqRequest);
            this.PnlReqRMA.Controls.Add(this.txtReqNumeroREQS);
            this.PnlReqRMA.Location = new System.Drawing.Point(6, 220);
            this.PnlReqRMA.Name = "PnlReqRMA";
            this.PnlReqRMA.Size = new System.Drawing.Size(267, 218);
            this.PnlReqRMA.TabIndex = 17;
            // 
            // lblReqRFC
            // 
            this.lblReqRFC.AutoSize = true;
            this.lblReqRFC.Location = new System.Drawing.Point(134, 0);
            this.lblReqRFC.Name = "lblReqRFC";
            this.lblReqRFC.Size = new System.Drawing.Size(31, 13);
            this.lblReqRFC.TabIndex = 17;
            this.lblReqRFC.Text = "RFC:";
            // 
            // txtReqRFC
            // 
            this.txtReqRFC.Location = new System.Drawing.Point(137, 16);
            this.txtReqRFC.Name = "txtReqRFC";
            this.txtReqRFC.Size = new System.Drawing.Size(130, 20);
            this.txtReqRFC.TabIndex = 18;
            // 
            // FrmManejoRequerimeinto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ScRequerimiento);
            this.Name = "FrmManejoRequerimeinto";
            this.Text = "Manejo Requerimeinto :: Tipo {0} :: Num Req {1}";
            this.ScRequerimiento.Panel1.ResumeLayout(false);
            this.ScRequerimiento.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ScRequerimiento)).EndInit();
            this.ScRequerimiento.ResumeLayout(false);
            this.gbRequerimeinto.ResumeLayout(false);
            this.gbRequerimeinto.PerformLayout();
            this.GbReqTipo.ResumeLayout(false);
            this.GbReqTipo.PerformLayout();
            this.PnlReqRMA.ResumeLayout(false);
            this.PnlReqRMA.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer ScRequerimiento;
        private System.Windows.Forms.GroupBox gbRequerimeinto;
        private System.Windows.Forms.GroupBox GbReqTipo;
        private System.Windows.Forms.RadioButton RdbRT_MantMay;
        private System.Windows.Forms.RadioButton RdbRT_RMA;
        private System.Windows.Forms.RadioButton RdbRT_Servicio;
        private System.Windows.Forms.RadioButton RdbRT_MantMen;
        private System.Windows.Forms.Label LblReqEstatus;
        private System.Windows.Forms.ComboBox CmbReqEstatus;
        private System.Windows.Forms.ComboBox CmbAplicativo;
        private System.Windows.Forms.Label lblReqAplicativo;
        private System.Windows.Forms.ComboBox CmbResponsable;
        private System.Windows.Forms.Label LblResponsable;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtReqNumeroREQS;
        private System.Windows.Forms.Label lblReqRequest;
        private System.Windows.Forms.Panel PnlReqRMA;
        private System.Windows.Forms.TextBox txtReqRFC;
        private System.Windows.Forms.Label lblReqRFC;
    }
}