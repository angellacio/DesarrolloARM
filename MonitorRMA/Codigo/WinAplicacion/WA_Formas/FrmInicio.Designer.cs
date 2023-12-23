namespace WA_Formas
{
    partial class FrmInicio
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.TsmiProblematicas = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiManIncidentes = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiManPaquetes = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiManMasiva = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiCatalogos = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiAreas = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiAplicaciones = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiSalir = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.TtlUsuario = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.TsmiCatEmpleados = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiCatTablas = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiCatCatalogo = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmiCatAreaAplicacion = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsmiProblematicas,
            this.TsmiCatalogos,
            this.TsmiAreas,
            this.TsmiAplicaciones,
            this.TsmiSalir});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(632, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            // 
            // TsmiProblematicas
            // 
            this.TsmiProblematicas.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsmiManIncidentes,
            this.TsmiManPaquetes,
            this.TsmiManMasiva});
            this.TsmiProblematicas.ImageTransparentColor = System.Drawing.SystemColors.ActiveBorder;
            this.TsmiProblematicas.Name = "TsmiProblematicas";
            this.TsmiProblematicas.Size = new System.Drawing.Size(136, 20);
            this.TsmiProblematicas.Text = "&Administra Problemas";
            // 
            // TsmiManIncidentes
            // 
            this.TsmiManIncidentes.Name = "TsmiManIncidentes";
            this.TsmiManIncidentes.Size = new System.Drawing.Size(180, 22);
            this.TsmiManIncidentes.Text = "Manejo &Incidentes";
            this.TsmiManIncidentes.Click += new System.EventHandler(this.TsmiManIncidentes_Click);
            // 
            // TsmiManPaquetes
            // 
            this.TsmiManPaquetes.Name = "TsmiManPaquetes";
            this.TsmiManPaquetes.Size = new System.Drawing.Size(180, 22);
            this.TsmiManPaquetes.Text = "Manejo Pa&quetes";
            this.TsmiManPaquetes.Click += new System.EventHandler(this.TsmiManPaquetes_Click);
            // 
            // TsmiManMasiva
            // 
            this.TsmiManMasiva.Name = "TsmiManMasiva";
            this.TsmiManMasiva.Size = new System.Drawing.Size(180, 22);
            this.TsmiManMasiva.Text = "Altama &Masiva";
            this.TsmiManMasiva.Click += new System.EventHandler(this.TsmiManMasiva_Click);
            // 
            // TsmiCatalogos
            // 
            this.TsmiCatalogos.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsmiCatEmpleados,
            this.TsmiCatTablas,
            this.TsmiCatCatalogo,
            this.TsmiCatAreaAplicacion});
            this.TsmiCatalogos.Name = "TsmiCatalogos";
            this.TsmiCatalogos.Size = new System.Drawing.Size(72, 20);
            this.TsmiCatalogos.Text = "&Catalogos";
            // 
            // TsmiAreas
            // 
            this.TsmiAreas.Image = global::WA_Formas.Properties.Resources.Filtros;
            this.TsmiAreas.Name = "TsmiAreas";
            this.TsmiAreas.Size = new System.Drawing.Size(64, 20);
            this.TsmiAreas.Text = "A&reas";
            // 
            // TsmiAplicaciones
            // 
            this.TsmiAplicaciones.Image = global::WA_Formas.Properties.Resources.Filtros;
            this.TsmiAplicaciones.Name = "TsmiAplicaciones";
            this.TsmiAplicaciones.Size = new System.Drawing.Size(102, 20);
            this.TsmiAplicaciones.Text = "A&plicaciones";
            // 
            // TsmiSalir
            // 
            this.TsmiSalir.Image = global::WA_Formas.Properties.Resources.salir;
            this.TsmiSalir.Name = "TsmiSalir";
            this.TsmiSalir.Size = new System.Drawing.Size(57, 20);
            this.TsmiSalir.Text = "&Salir";
            this.TsmiSalir.Click += new System.EventHandler(this.TsmiSalir_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TtlUsuario});
            this.statusStrip.Location = new System.Drawing.Point(0, 431);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(632, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "StatusStrip";
            // 
            // TtlUsuario
            // 
            this.TtlUsuario.Name = "TtlUsuario";
            this.TtlUsuario.Size = new System.Drawing.Size(72, 17);
            this.TtlUsuario.Text = "DatoUsuario";
            // 
            // TsmiCatEmpleados
            // 
            this.TsmiCatEmpleados.Name = "TsmiCatEmpleados";
            this.TsmiCatEmpleados.Size = new System.Drawing.Size(180, 22);
            this.TsmiCatEmpleados.Text = "&Empleados";
            // 
            // TsmiCatTablas
            // 
            this.TsmiCatTablas.Name = "TsmiCatTablas";
            this.TsmiCatTablas.Size = new System.Drawing.Size(180, 22);
            this.TsmiCatTablas.Text = "&Tablas";
            // 
            // TsmiCatCatalogo
            // 
            this.TsmiCatCatalogo.Name = "TsmiCatCatalogo";
            this.TsmiCatCatalogo.Size = new System.Drawing.Size(180, 22);
            this.TsmiCatCatalogo.Text = "Ca&talogos";
            // 
            // TsmiCatAreaAplicacion
            // 
            this.TsmiCatAreaAplicacion.Name = "TsmiCatAreaAplicacion";
            this.TsmiCatAreaAplicacion.Size = new System.Drawing.Size(180, 22);
            this.TsmiCatAreaAplicacion.Text = "Areas/Aplicacion";
            // 
            // FrmInicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 453);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "FrmInicio";
            this.Text = "frmInicio";
            this.Load += new System.EventHandler(this.FrmInicio_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripMenuItem TsmiProblematicas;
        private System.Windows.Forms.ToolStripMenuItem TsmiCatalogos;
        private System.Windows.Forms.ToolStripMenuItem TsmiAplicaciones;
        private System.Windows.Forms.ToolStripMenuItem TsmiAreas;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripStatusLabel TtlUsuario;
        private System.Windows.Forms.ToolStripMenuItem TsmiSalir;
        private System.Windows.Forms.ToolStripMenuItem TsmiManIncidentes;
        private System.Windows.Forms.ToolStripMenuItem TsmiManPaquetes;
        private System.Windows.Forms.ToolStripMenuItem TsmiManMasiva;
        private System.Windows.Forms.ToolStripMenuItem TsmiCatEmpleados;
        private System.Windows.Forms.ToolStripMenuItem TsmiCatTablas;
        private System.Windows.Forms.ToolStripMenuItem TsmiCatCatalogo;
        private System.Windows.Forms.ToolStripMenuItem TsmiCatAreaAplicacion;
    }
}



