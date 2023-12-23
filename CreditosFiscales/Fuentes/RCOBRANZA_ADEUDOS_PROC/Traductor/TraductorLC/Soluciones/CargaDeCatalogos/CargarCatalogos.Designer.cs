namespace CargaDeCatalogos
{
    partial class CargarCatalogos
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
            this.btnTransacciones = new System.Windows.Forms.Button();
            this.btnPeriodos = new System.Windows.Forms.Button();
            this.btnPeriodicidad = new System.Windows.Forms.Button();
            this.btnAlr = new System.Windows.Forms.Button();
            this.btnConceptos = new System.Windows.Forms.Button();
            this.btnReglasMotor = new System.Windows.Forms.Button();
            this.btnReglasEquivalencia = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnTransacciones
            // 
            this.btnTransacciones.Location = new System.Drawing.Point(12, 12);
            this.btnTransacciones.Name = "btnTransacciones";
            this.btnTransacciones.Size = new System.Drawing.Size(117, 23);
            this.btnTransacciones.TabIndex = 0;
            this.btnTransacciones.Text = "Transacciones";
            this.btnTransacciones.UseVisualStyleBackColor = true;
            this.btnTransacciones.Click += new System.EventHandler(this.btnTransacciones_Click);
            // 
            // btnPeriodos
            // 
            this.btnPeriodos.Location = new System.Drawing.Point(12, 41);
            this.btnPeriodos.Name = "btnPeriodos";
            this.btnPeriodos.Size = new System.Drawing.Size(117, 23);
            this.btnPeriodos.TabIndex = 1;
            this.btnPeriodos.Text = "Periodos";
            this.btnPeriodos.UseVisualStyleBackColor = true;
            this.btnPeriodos.Click += new System.EventHandler(this.btnPeriodos_Click);
            // 
            // btnPeriodicidad
            // 
            this.btnPeriodicidad.Location = new System.Drawing.Point(12, 70);
            this.btnPeriodicidad.Name = "btnPeriodicidad";
            this.btnPeriodicidad.Size = new System.Drawing.Size(117, 23);
            this.btnPeriodicidad.TabIndex = 2;
            this.btnPeriodicidad.Text = "Periodicidad";
            this.btnPeriodicidad.UseVisualStyleBackColor = true;
            this.btnPeriodicidad.Click += new System.EventHandler(this.btnPeriodicidad_Click);
            // 
            // btnAlr
            // 
            this.btnAlr.Location = new System.Drawing.Point(12, 128);
            this.btnAlr.Name = "btnAlr";
            this.btnAlr.Size = new System.Drawing.Size(117, 23);
            this.btnAlr.TabIndex = 4;
            this.btnAlr.Text = "Alr";
            this.btnAlr.UseVisualStyleBackColor = true;
            this.btnAlr.Click += new System.EventHandler(this.btnAlr_Click);
            // 
            // btnConceptos
            // 
            this.btnConceptos.Location = new System.Drawing.Point(12, 99);
            this.btnConceptos.Name = "btnConceptos";
            this.btnConceptos.Size = new System.Drawing.Size(117, 23);
            this.btnConceptos.TabIndex = 5;
            this.btnConceptos.Text = "Conceptos Pago";
            this.btnConceptos.UseVisualStyleBackColor = true;
            this.btnConceptos.Click += new System.EventHandler(this.btnConceptos_Click);
            // 
            // btnReglasMotor
            // 
            this.btnReglasMotor.Location = new System.Drawing.Point(148, 12);
            this.btnReglasMotor.Name = "btnReglasMotor";
            this.btnReglasMotor.Size = new System.Drawing.Size(138, 23);
            this.btnReglasMotor.TabIndex = 6;
            this.btnReglasMotor.Text = "Reglas Motor Traductor";
            this.btnReglasMotor.UseVisualStyleBackColor = true;
            this.btnReglasMotor.Click += new System.EventHandler(this.btnReglasMotor_Click);
            // 
            // btnReglasEquivalencia
            // 
            this.btnReglasEquivalencia.Location = new System.Drawing.Point(148, 41);
            this.btnReglasEquivalencia.Name = "btnReglasEquivalencia";
            this.btnReglasEquivalencia.Size = new System.Drawing.Size(138, 23);
            this.btnReglasEquivalencia.TabIndex = 7;
            this.btnReglasEquivalencia.Text = "Reglas Equivalencia";
            this.btnReglasEquivalencia.UseVisualStyleBackColor = true;
            // 
            // CargarCatalogos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 376);
            this.Controls.Add(this.btnReglasEquivalencia);
            this.Controls.Add(this.btnReglasMotor);
            this.Controls.Add(this.btnConceptos);
            this.Controls.Add(this.btnAlr);
            this.Controls.Add(this.btnPeriodicidad);
            this.Controls.Add(this.btnPeriodos);
            this.Controls.Add(this.btnTransacciones);
            this.Name = "CargarCatalogos";
            this.Text = "Cargar Catalogos";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnTransacciones;
        private System.Windows.Forms.Button btnPeriodos;
        private System.Windows.Forms.Button btnPeriodicidad;
        private System.Windows.Forms.Button btnAlr;
        private System.Windows.Forms.Button btnConceptos;
        private System.Windows.Forms.Button btnReglasMotor;
        private System.Windows.Forms.Button btnReglasEquivalencia;
    }
}

