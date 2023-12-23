using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using entSeg = WA_Entidades.Seguridad;

namespace WA_Formas
{
    public partial class FrmInicio : Form
    {
        private entSeg.EntDatosAutentificacion DatSeguridad { get; set; }

        public FrmInicio(entSeg.EntDatosAutentificacion datSeguridad)
        {
            InitializeComponent();
            DatSeguridad = datSeguridad;

            TtlUsuario.Text = $"{DatSeguridad.Usuario}-{DatSeguridad.Empelado} {DatSeguridad.Apellido_Uno} {DatSeguridad.Apellido_Dos}";

            DatSeguridad.Areas.ForEach(item =>
            {
                System.Windows.Forms.ToolStripMenuItem TsmiDatoArea = new System.Windows.Forms.ToolStripMenuItem()
                {
                    CheckState = CheckState.Checked,
                    Name = $"TsmiArea_{item.IdCatalogo}",
                    Size = new System.Drawing.Size(180, 22),
                    Text = $"{item.Catalogo}",
                    Checked = item.Seleccion
                };
                TsmiDatoArea.Click += new System.EventHandler(this.SellecionaFiltros_Aerea_Click);
                TsmiAreas.DropDownItems.Add(TsmiDatoArea);
            });


            DatSeguridad.Aplicativos.ForEach(item =>
            {
                System.Windows.Forms.ToolStripMenuItem TsmiDatoAplicativo = new System.Windows.Forms.ToolStripMenuItem()
                {
                    CheckState = CheckState.Checked,
                    Name = $"TsmiAplicativo_{item.IdCatalogo}",
                    Size = new System.Drawing.Size(180, 22),
                    Text = $"{item.Orden} :: {item.Catalogo}",
                    Checked = item.Seleccion
                };
                TsmiDatoAplicativo.Click += new System.EventHandler(this.SellecionaFiltros_Aplicacion_Click);
                TsmiAplicaciones.DropDownItems.Add(TsmiDatoAplicativo);
            });
        }

        private void FrmInicio_Load(object sender, EventArgs e)
        {
            LlenaDatos_RMAs();
        }

        private void TsmiSalir_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        public void LlenaDatos_RMAs()
        {

        }

        private void SellecionaFiltros_Aerea_Click(object sender, EventArgs e)
        {
            int nIdArea = 0;
            System.Windows.Forms.ToolStripMenuItem TsiItem = (System.Windows.Forms.ToolStripMenuItem)sender;
            TsiItem.Checked = !TsiItem.Checked;
            nIdArea = int.Parse(TsiItem.Name.Replace("TsmiArea_", ""));

            DatSeguridad.Areas.Find(item => item.IdCatalogo == nIdArea).Seleccion = TsiItem.Checked;
            LlenaDatos_RMAs();
        }
        private void SellecionaFiltros_Aplicacion_Click(object sender, EventArgs e)
        {
            int nIdAplicacion = 0;
            System.Windows.Forms.ToolStripMenuItem TsiItem = (System.Windows.Forms.ToolStripMenuItem)sender;
            TsiItem.Checked = !TsiItem.Checked;
            nIdAplicacion = int.Parse(TsiItem.Name.Replace("TsmiAplicativo_", ""));

            DatSeguridad.Aplicativos.Find(item => item.IdCatalogo == nIdAplicacion).Seleccion = TsiItem.Checked;
            LlenaDatos_RMAs();
        }

        private void TsmiManIncidentes_Click(object sender, EventArgs e)
        {
            ManejoRequerimiento.FrmManejoRequerimeinto frmReq = new ManejoRequerimiento.FrmManejoRequerimeinto();
            frmReq.ShowDialog();
        }

        private void TsmiManPaquetes_Click(object sender, EventArgs e)
        {

        }

        private void TsmiManMasiva_Click(object sender, EventArgs e)
        {

        }
    }
}
