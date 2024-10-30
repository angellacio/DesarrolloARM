namespace TestMotor
{
    partial class GeneraPDF
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.cbx_formatos = new System.Windows.Forms.ComboBox();
            this.tbx_folio = new System.Windows.Forms.TextBox();
            this.tbx_logPdf = new System.Windows.Forms.TextBox();
            this.lnk_pdf_formato = new System.Windows.Forms.LinkLabel();
            this.lbl_formato = new System.Windows.Forms.Label();
            this.lbl_folio = new System.Windows.Forms.Label();
            this.btn_generPDFServImp = new System.Windows.Forms.Button();
            this.lbl_idAplicacion = new System.Windows.Forms.Label();
            this.lbl_idDocumento = new System.Windows.Forms.Label();
            this.tbx_idAplicacion = new System.Windows.Forms.TextBox();
            this.tbx_IdDocumento = new System.Windows.Forms.TextBox();
            this.lbl_entrada = new System.Windows.Forms.Label();
            this.lbl_pdf = new System.Windows.Forms.LinkLabel();
            this.tbx_msjEntrada = new System.Windows.Forms.TextBox();
            this.btn_nuevo = new System.Windows.Forms.Button();
            this.btn_enviar = new System.Windows.Forms.Button();
            this.tbx_log = new System.Windows.Forms.TextBox();
            this.btnAbrir = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.lbl_LineaCaptura = new System.Windows.Forms.Label();
            this.tbx_lineaCaptura = new System.Windows.Forms.TextBox();
            this.btn_enviarLinea = new System.Windows.Forms.Button();
            this.tbx_logLC = new System.Windows.Forms.TextBox();
            this.lnk_PDFLC = new System.Windows.Forms.LinkLabel();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 26);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(682, 381);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btn_generPDFServImp);
            this.tabPage1.Controls.Add(this.lbl_folio);
            this.tabPage1.Controls.Add(this.lbl_formato);
            this.tabPage1.Controls.Add(this.lnk_pdf_formato);
            this.tabPage1.Controls.Add(this.tbx_logPdf);
            this.tabPage1.Controls.Add(this.tbx_folio);
            this.tabPage1.Controls.Add(this.cbx_formatos);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(674, 355);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "PDF-Folio";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnAbrir);
            this.tabPage2.Controls.Add(this.tbx_log);
            this.tabPage2.Controls.Add(this.btn_enviar);
            this.tabPage2.Controls.Add(this.btn_nuevo);
            this.tabPage2.Controls.Add(this.tbx_msjEntrada);
            this.tabPage2.Controls.Add(this.lbl_pdf);
            this.tabPage2.Controls.Add(this.lbl_entrada);
            this.tabPage2.Controls.Add(this.tbx_IdDocumento);
            this.tabPage2.Controls.Add(this.tbx_idAplicacion);
            this.tabPage2.Controls.Add(this.lbl_idDocumento);
            this.tabPage2.Controls.Add(this.lbl_idAplicacion);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(674, 355);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Motor Traductor";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // cbx_formatos
            // 
            this.cbx_formatos.FormattingEnabled = true;
            this.cbx_formatos.Location = new System.Drawing.Point(61, 25);
            this.cbx_formatos.Name = "cbx_formatos";
            this.cbx_formatos.Size = new System.Drawing.Size(260, 21);
            this.cbx_formatos.TabIndex = 0;
            // 
            // tbx_folio
            // 
            this.tbx_folio.Location = new System.Drawing.Point(61, 66);
            this.tbx_folio.Name = "tbx_folio";
            this.tbx_folio.Size = new System.Drawing.Size(100, 20);
            this.tbx_folio.TabIndex = 1;
            // 
            // tbx_logPdf
            // 
            this.tbx_logPdf.Location = new System.Drawing.Point(29, 125);
            this.tbx_logPdf.Multiline = true;
            this.tbx_logPdf.Name = "tbx_logPdf";
            this.tbx_logPdf.Size = new System.Drawing.Size(573, 155);
            this.tbx_logPdf.TabIndex = 2;
            // 
            // lnk_pdf_formato
            // 
            this.lnk_pdf_formato.AutoSize = true;
            this.lnk_pdf_formato.Location = new System.Drawing.Point(58, 100);
            this.lnk_pdf_formato.Name = "lnk_pdf_formato";
            this.lnk_pdf_formato.Size = new System.Drawing.Size(83, 13);
            this.lnk_pdf_formato.TabIndex = 3;
            this.lnk_pdf_formato.TabStop = true;
            this.lnk_pdf_formato.Text = "lnk_pdf_formato";
            this.lnk_pdf_formato.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnk_pdf_formato_LinkClicked);
            // 
            // lbl_formato
            // 
            this.lbl_formato.AutoSize = true;
            this.lbl_formato.Location = new System.Drawing.Point(10, 28);
            this.lbl_formato.Name = "lbl_formato";
            this.lbl_formato.Size = new System.Drawing.Size(45, 13);
            this.lbl_formato.TabIndex = 5;
            this.lbl_formato.Text = "Formato";
            // 
            // lbl_folio
            // 
            this.lbl_folio.AutoSize = true;
            this.lbl_folio.Location = new System.Drawing.Point(26, 69);
            this.lbl_folio.Name = "lbl_folio";
            this.lbl_folio.Size = new System.Drawing.Size(29, 13);
            this.lbl_folio.TabIndex = 6;
            this.lbl_folio.Text = "Folio";
            // 
            // btn_generPDFServImp
            // 
            this.btn_generPDFServImp.Location = new System.Drawing.Point(184, 63);
            this.btn_generPDFServImp.Name = "btn_generPDFServImp";
            this.btn_generPDFServImp.Size = new System.Drawing.Size(75, 23);
            this.btn_generPDFServImp.TabIndex = 7;
            this.btn_generPDFServImp.Text = "GeneraPDF";
            this.btn_generPDFServImp.UseVisualStyleBackColor = true;
            this.btn_generPDFServImp.Click += new System.EventHandler(this.btn_generPDFServImp_Click);
            // 
            // lbl_idAplicacion
            // 
            this.lbl_idAplicacion.AutoSize = true;
            this.lbl_idAplicacion.Location = new System.Drawing.Point(31, 24);
            this.lbl_idAplicacion.Name = "lbl_idAplicacion";
            this.lbl_idAplicacion.Size = new System.Drawing.Size(70, 13);
            this.lbl_idAplicacion.TabIndex = 0;
            this.lbl_idAplicacion.Text = "ID Aplicación";
            // 
            // lbl_idDocumento
            // 
            this.lbl_idDocumento.AutoSize = true;
            this.lbl_idDocumento.Location = new System.Drawing.Point(31, 55);
            this.lbl_idDocumento.Name = "lbl_idDocumento";
            this.lbl_idDocumento.Size = new System.Drawing.Size(76, 13);
            this.lbl_idDocumento.TabIndex = 1;
            this.lbl_idDocumento.Text = "ID Documento";
            // 
            // tbx_idAplicacion
            // 
            this.tbx_idAplicacion.Location = new System.Drawing.Point(113, 21);
            this.tbx_idAplicacion.Name = "tbx_idAplicacion";
            this.tbx_idAplicacion.Size = new System.Drawing.Size(18, 20);
            this.tbx_idAplicacion.TabIndex = 2;
            // 
            // tbx_IdDocumento
            // 
            this.tbx_IdDocumento.Location = new System.Drawing.Point(113, 55);
            this.tbx_IdDocumento.Name = "tbx_IdDocumento";
            this.tbx_IdDocumento.Size = new System.Drawing.Size(18, 20);
            this.tbx_IdDocumento.TabIndex = 3;
            // 
            // lbl_entrada
            // 
            this.lbl_entrada.AutoSize = true;
            this.lbl_entrada.Location = new System.Drawing.Point(31, 77);
            this.lbl_entrada.Name = "lbl_entrada";
            this.lbl_entrada.Size = new System.Drawing.Size(69, 13);
            this.lbl_entrada.TabIndex = 4;
            this.lbl_entrada.Text = "XML Entrada";
            // 
            // lbl_pdf
            // 
            this.lbl_pdf.AutoSize = true;
            this.lbl_pdf.Location = new System.Drawing.Point(31, 225);
            this.lbl_pdf.Name = "lbl_pdf";
            this.lbl_pdf.Size = new System.Drawing.Size(38, 13);
            this.lbl_pdf.TabIndex = 5;
            this.lbl_pdf.TabStop = true;
            this.lbl_pdf.Text = "lbl_pdf";
            this.lbl_pdf.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbl_pdf_LinkClicked);
            // 
            // tbx_msjEntrada
            // 
            this.tbx_msjEntrada.Location = new System.Drawing.Point(34, 93);
            this.tbx_msjEntrada.Multiline = true;
            this.tbx_msjEntrada.Name = "tbx_msjEntrada";
            this.tbx_msjEntrada.Size = new System.Drawing.Size(606, 126);
            this.tbx_msjEntrada.TabIndex = 6;
            // 
            // btn_nuevo
            // 
            this.btn_nuevo.Location = new System.Drawing.Point(484, 225);
            this.btn_nuevo.Name = "btn_nuevo";
            this.btn_nuevo.Size = new System.Drawing.Size(75, 23);
            this.btn_nuevo.TabIndex = 7;
            this.btn_nuevo.Text = "Borrar";
            this.btn_nuevo.UseVisualStyleBackColor = true;
            this.btn_nuevo.Click += new System.EventHandler(this.btn_nuevo_Click);
            // 
            // btn_enviar
            // 
            this.btn_enviar.Location = new System.Drawing.Point(565, 225);
            this.btn_enviar.Name = "btn_enviar";
            this.btn_enviar.Size = new System.Drawing.Size(75, 23);
            this.btn_enviar.TabIndex = 8;
            this.btn_enviar.Text = "Enviar";
            this.btn_enviar.UseVisualStyleBackColor = true;
            this.btn_enviar.Click += new System.EventHandler(this.btn_enviar_Click);
            // 
            // tbx_log
            // 
            this.tbx_log.Location = new System.Drawing.Point(34, 254);
            this.tbx_log.Multiline = true;
            this.tbx_log.Name = "tbx_log";
            this.tbx_log.Size = new System.Drawing.Size(606, 71);
            this.tbx_log.TabIndex = 9;
            // 
            // btnAbrir
            // 
            this.btnAbrir.Location = new System.Drawing.Point(565, 64);
            this.btnAbrir.Name = "btnAbrir";
            this.btnAbrir.Size = new System.Drawing.Size(75, 23);
            this.btnAbrir.TabIndex = 10;
            this.btnAbrir.Text = "Abrir";
            this.btnAbrir.UseVisualStyleBackColor = true;
            this.btnAbrir.Click += new System.EventHandler(this.btnAbrir_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.lnk_PDFLC);
            this.tabPage3.Controls.Add(this.tbx_logLC);
            this.tabPage3.Controls.Add(this.btn_enviarLinea);
            this.tabPage3.Controls.Add(this.tbx_lineaCaptura);
            this.tabPage3.Controls.Add(this.lbl_LineaCaptura);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(674, 355);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "PDF-LineaCaptura";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // lbl_LineaCaptura
            // 
            this.lbl_LineaCaptura.AutoSize = true;
            this.lbl_LineaCaptura.Location = new System.Drawing.Point(26, 41);
            this.lbl_LineaCaptura.Name = "lbl_LineaCaptura";
            this.lbl_LineaCaptura.Size = new System.Drawing.Size(88, 13);
            this.lbl_LineaCaptura.TabIndex = 0;
            this.lbl_LineaCaptura.Text = "Linea de Captura";
            // 
            // tbx_lineaCaptura
            // 
            this.tbx_lineaCaptura.Location = new System.Drawing.Point(120, 38);
            this.tbx_lineaCaptura.Name = "tbx_lineaCaptura";
            this.tbx_lineaCaptura.Size = new System.Drawing.Size(244, 20);
            this.tbx_lineaCaptura.TabIndex = 1;
            // 
            // btn_enviarLinea
            // 
            this.btn_enviarLinea.Location = new System.Drawing.Point(289, 64);
            this.btn_enviarLinea.Name = "btn_enviarLinea";
            this.btn_enviarLinea.Size = new System.Drawing.Size(75, 23);
            this.btn_enviarLinea.TabIndex = 2;
            this.btn_enviarLinea.Text = "Enviar";
            this.btn_enviarLinea.UseVisualStyleBackColor = true;
            this.btn_enviarLinea.Click += new System.EventHandler(this.btn_enviarLinea_Click);
            // 
            // tbx_logLC
            // 
            this.tbx_logLC.Location = new System.Drawing.Point(29, 116);
            this.tbx_logLC.Multiline = true;
            this.tbx_logLC.Name = "tbx_logLC";
            this.tbx_logLC.Size = new System.Drawing.Size(597, 138);
            this.tbx_logLC.TabIndex = 3;
            // 
            // lnk_PDFLC
            // 
            this.lnk_PDFLC.AutoSize = true;
            this.lnk_PDFLC.Location = new System.Drawing.Point(26, 87);
            this.lnk_PDFLC.Name = "lnk_PDFLC";
            this.lnk_PDFLC.Size = new System.Drawing.Size(55, 13);
            this.lnk_PDFLC.TabIndex = 4;
            this.lnk_PDFLC.TabStop = true;
            this.lnk_PDFLC.Text = "linkLabel1";
            // 
            // GeneraPDF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(706, 408);
            this.Controls.Add(this.tabControl1);
            this.Name = "GeneraPDF";
            this.Text = "GeneraPDF";
            this.Load += new System.EventHandler(this.GeneraPDF_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ComboBox cbx_formatos;
        private System.Windows.Forms.TextBox tbx_folio;
        private System.Windows.Forms.TextBox tbx_logPdf;
        private System.Windows.Forms.LinkLabel lnk_pdf_formato;
        private System.Windows.Forms.Button btn_generPDFServImp;
        private System.Windows.Forms.Label lbl_folio;
        private System.Windows.Forms.Label lbl_formato;
        private System.Windows.Forms.Button btn_enviar;
        private System.Windows.Forms.Button btn_nuevo;
        private System.Windows.Forms.TextBox tbx_msjEntrada;
        private System.Windows.Forms.LinkLabel lbl_pdf;
        private System.Windows.Forms.Label lbl_entrada;
        private System.Windows.Forms.TextBox tbx_IdDocumento;
        private System.Windows.Forms.TextBox tbx_idAplicacion;
        private System.Windows.Forms.Label lbl_idDocumento;
        private System.Windows.Forms.Label lbl_idAplicacion;
        private System.Windows.Forms.TextBox tbx_log;
        private System.Windows.Forms.Button btnAbrir;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox tbx_lineaCaptura;
        private System.Windows.Forms.Label lbl_LineaCaptura;
        private System.Windows.Forms.Button btn_enviarLinea;
        private System.Windows.Forms.LinkLabel lnk_PDFLC;
        private System.Windows.Forms.TextBox tbx_logLC;
    }
}