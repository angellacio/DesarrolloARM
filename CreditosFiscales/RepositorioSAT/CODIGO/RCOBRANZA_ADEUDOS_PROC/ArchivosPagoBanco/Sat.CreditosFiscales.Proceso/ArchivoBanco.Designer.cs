namespace ArchivoBanco
{
    partial class FormPrincipal
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
            this.background = new System.ComponentModel.BackgroundWorker();
            this.barra = new System.Windows.Forms.ProgressBar();
            this.lbl_estatus = new System.Windows.Forms.Label();
            this.lnk_error = new System.Windows.Forms.LinkLabel();
            this.btn_cargar = new System.Windows.Forms.Button();
            this.openfile = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // background
            // 
            this.background.WorkerReportsProgress = true;
            this.background.DoWork += new System.ComponentModel.DoWorkEventHandler(this.background_DoWork);
            this.background.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.background_ProgressChanged);
            this.background.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.background_RunWorkerCompleted);
            // 
            // barra
            // 
            this.barra.Location = new System.Drawing.Point(12, 100);
            this.barra.Name = "barra";
            this.barra.Size = new System.Drawing.Size(293, 23);
            this.barra.TabIndex = 0;
            // 
            // lbl_estatus
            // 
            this.lbl_estatus.AutoSize = true;
            this.lbl_estatus.Location = new System.Drawing.Point(12, 126);
            this.lbl_estatus.Name = "lbl_estatus";
            this.lbl_estatus.Size = new System.Drawing.Size(100, 13);
            this.lbl_estatus.TabIndex = 2;
            this.lbl_estatus.Text = "Cargando archivo...";
            // 
            // lnk_error
            // 
            this.lnk_error.AutoSize = true;
            this.lnk_error.Location = new System.Drawing.Point(15, 208);
            this.lnk_error.Name = "lnk_error";
            this.lnk_error.Size = new System.Drawing.Size(206, 13);
            this.lnk_error.TabIndex = 3;
            this.lnk_error.TabStop = true;
            this.lnk_error.Text = "Ocurrio una excepción clic para abrir log...";
            this.lnk_error.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnk_error_LinkClicked);
            // 
            // btn_cargar
            // 
            this.btn_cargar.Location = new System.Drawing.Point(198, 161);
            this.btn_cargar.Name = "btn_cargar";
            this.btn_cargar.Size = new System.Drawing.Size(90, 23);
            this.btn_cargar.TabIndex = 4;
            this.btn_cargar.Text = "Cargar Insumo";
            this.btn_cargar.UseVisualStyleBackColor = true;
            this.btn_cargar.Click += new System.EventHandler(this.btn_cargar_Click);
            // 
            // FormPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(322, 262);
            this.Controls.Add(this.btn_cargar);
            this.Controls.Add(this.lnk_error);
            this.Controls.Add(this.lbl_estatus);
            this.Controls.Add(this.barra);
            this.Name = "FormPrincipal";
            this.Text = "Archivos Banco";
            this.Load += new System.EventHandler(this.FormPrincipal_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker background;
        private System.Windows.Forms.ProgressBar barra;
        private System.Windows.Forms.Label lbl_estatus;
        private System.Windows.Forms.LinkLabel lnk_error;
        private System.Windows.Forms.OpenFileDialog openfile;
        private System.Windows.Forms.Button btn_cargar;
    }
}

