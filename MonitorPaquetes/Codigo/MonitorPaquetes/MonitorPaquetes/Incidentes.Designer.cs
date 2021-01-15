namespace MonitorPaquetes
{
    partial class Incidentes
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
            this.ofdArchivo = new System.Windows.Forms.OpenFileDialog();
            this.btnExaminar = new System.Windows.Forms.Button();
            this.lblRuta = new System.Windows.Forms.Label();
            this.txtRuta = new System.Windows.Forms.TextBox();
            this.pgrBar = new System.Windows.Forms.ProgressBar();
            this.btnCargar = new System.Windows.Forms.Button();
            this.bkgWorker = new System.ComponentModel.BackgroundWorker();
            this.lblProgress = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ofdArchivo
            // 
            this.ofdArchivo.Filter = "*.txt|";
            this.ofdArchivo.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // btnExaminar
            // 
            this.btnExaminar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExaminar.Location = new System.Drawing.Point(430, 11);
            this.btnExaminar.Name = "btnExaminar";
            this.btnExaminar.Size = new System.Drawing.Size(84, 23);
            this.btnExaminar.TabIndex = 0;
            this.btnExaminar.Text = "Examinar";
            this.btnExaminar.UseVisualStyleBackColor = true;
            this.btnExaminar.Click += new System.EventHandler(this.btnCargar_Click);
            // 
            // lblRuta
            // 
            this.lblRuta.AutoSize = true;
            this.lblRuta.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRuta.Location = new System.Drawing.Point(9, 15);
            this.lblRuta.Name = "lblRuta";
            this.lblRuta.Size = new System.Drawing.Size(138, 17);
            this.lblRuta.TabIndex = 1;
            this.lblRuta.Text = "Ruta del Archivo: ";
            // 
            // txtRuta
            // 
            this.txtRuta.Location = new System.Drawing.Point(137, 14);
            this.txtRuta.Name = "txtRuta";
            this.txtRuta.Size = new System.Drawing.Size(287, 20);
            this.txtRuta.TabIndex = 2;
            // 
            // pgrBar
            // 
            this.pgrBar.Location = new System.Drawing.Point(12, 76);
            this.pgrBar.Name = "pgrBar";
            this.pgrBar.Size = new System.Drawing.Size(502, 23);
            this.pgrBar.TabIndex = 3;
            // 
            // btnCargar
            // 
            this.btnCargar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCargar.Location = new System.Drawing.Point(226, 40);
            this.btnCargar.Name = "btnCargar";
            this.btnCargar.Size = new System.Drawing.Size(117, 30);
            this.btnCargar.TabIndex = 4;
            this.btnCargar.Text = "Cargar Archivo";
            this.btnCargar.UseVisualStyleBackColor = true;
            this.btnCargar.Click += new System.EventHandler(this.btnCargar_Click_1);
            // 
            // bkgWorker
            // 
            this.bkgWorker.WorkerReportsProgress = true;
            this.bkgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bkgWorker_DoWork);
            this.bkgWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bkgWorker_ProgressChanged);
            this.bkgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bkgWorker_RunWorkerCompleted);
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(12, 57);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(0, 13);
            this.lblProgress.TabIndex = 5;
            // 
            // Incidentes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 112);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.btnCargar);
            this.Controls.Add(this.pgrBar);
            this.Controls.Add(this.txtRuta);
            this.Controls.Add(this.lblRuta);
            this.Controls.Add(this.btnExaminar);
            this.Name = "Incidentes";
            this.Text = "Incidentes";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog ofdArchivo;
        private System.Windows.Forms.Button btnExaminar;
        private System.Windows.Forms.Label lblRuta;
        private System.Windows.Forms.TextBox txtRuta;
        private System.Windows.Forms.ProgressBar pgrBar;
        private System.Windows.Forms.Button btnCargar;
        private System.ComponentModel.BackgroundWorker bkgWorker;
        private System.Windows.Forms.Label lblProgress;
    }
}