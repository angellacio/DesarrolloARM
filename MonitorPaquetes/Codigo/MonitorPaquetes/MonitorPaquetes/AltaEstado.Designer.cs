namespace MonitorPaquetes
{
    partial class AltaEstado
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
            this.gpEstado = new System.Windows.Forms.GroupBox();
            this.btnRegistrar = new System.Windows.Forms.Button();
            this.cbArea = new System.Windows.Forms.ComboBox();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.txtIdEstado = new System.Windows.Forms.TextBox();
            this.lbArea = new System.Windows.Forms.Label();
            this.lbDescripcion = new System.Windows.Forms.Label();
            this.lbIdEstado = new System.Windows.Forms.Label();
            this.gpEstado.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpEstado
            // 
            this.gpEstado.Controls.Add(this.btnRegistrar);
            this.gpEstado.Controls.Add(this.cbArea);
            this.gpEstado.Controls.Add(this.txtDescripcion);
            this.gpEstado.Controls.Add(this.txtIdEstado);
            this.gpEstado.Controls.Add(this.lbArea);
            this.gpEstado.Controls.Add(this.lbDescripcion);
            this.gpEstado.Controls.Add(this.lbIdEstado);
            this.gpEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpEstado.Location = new System.Drawing.Point(13, 13);
            this.gpEstado.Name = "gpEstado";
            this.gpEstado.Size = new System.Drawing.Size(381, 117);
            this.gpEstado.TabIndex = 0;
            this.gpEstado.TabStop = false;
            this.gpEstado.Text = "Datos Estado:";
            // 
            // btnRegistrar
            // 
            this.btnRegistrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegistrar.Location = new System.Drawing.Point(154, 78);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new System.Drawing.Size(75, 29);
            this.btnRegistrar.TabIndex = 6;
            this.btnRegistrar.Text = "Registrar";
            this.btnRegistrar.UseVisualStyleBackColor = true;
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrar_Click);
            // 
            // cbArea
            // 
            this.cbArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbArea.FormattingEnabled = true;
            this.cbArea.Items.AddRange(new object[] {
            "Seleccionar ...",
            "Desarrollo",
            "AVL",
            "Operaciones",
            "RAPE"});
            this.cbArea.Location = new System.Drawing.Point(209, 19);
            this.cbArea.Name = "cbArea";
            this.cbArea.Size = new System.Drawing.Size(166, 23);
            this.cbArea.TabIndex = 5;
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescripcion.Location = new System.Drawing.Point(90, 49);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(285, 21);
            this.txtDescripcion.TabIndex = 4;
            // 
            // txtIdEstado
            // 
            this.txtIdEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIdEstado.Location = new System.Drawing.Point(90, 20);
            this.txtIdEstado.Name = "txtIdEstado";
            this.txtIdEstado.Size = new System.Drawing.Size(73, 21);
            this.txtIdEstado.TabIndex = 3;
            // 
            // lbArea
            // 
            this.lbArea.AutoSize = true;
            this.lbArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbArea.Location = new System.Drawing.Point(170, 23);
            this.lbArea.Name = "lbArea";
            this.lbArea.Size = new System.Drawing.Size(35, 15);
            this.lbArea.TabIndex = 2;
            this.lbArea.Text = "Area:";
            // 
            // lbDescripcion
            // 
            this.lbDescripcion.AutoSize = true;
            this.lbDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDescripcion.Location = new System.Drawing.Point(7, 49);
            this.lbDescripcion.Name = "lbDescripcion";
            this.lbDescripcion.Size = new System.Drawing.Size(75, 15);
            this.lbDescripcion.TabIndex = 1;
            this.lbDescripcion.Text = "Descripcion:";
            // 
            // lbIdEstado
            // 
            this.lbIdEstado.AutoSize = true;
            this.lbIdEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbIdEstado.Location = new System.Drawing.Point(7, 23);
            this.lbIdEstado.Name = "lbIdEstado";
            this.lbIdEstado.Size = new System.Drawing.Size(63, 15);
            this.lbIdEstado.TabIndex = 0;
            this.lbIdEstado.Text = "ID Estado:";
            // 
            // AltaEstado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(406, 141);
            this.Controls.Add(this.gpEstado);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AltaEstado";
            this.Text = "Alta Estado";
            this.Load += new System.EventHandler(this.AltaEstado_Load);
            this.gpEstado.ResumeLayout(false);
            this.gpEstado.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gpEstado;
        private System.Windows.Forms.Button btnRegistrar;
        private System.Windows.Forms.ComboBox cbArea;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.TextBox txtIdEstado;
        private System.Windows.Forms.Label lbArea;
        private System.Windows.Forms.Label lbDescripcion;
        private System.Windows.Forms.Label lbIdEstado;
    }
}