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
        }

        private void FrmInicio_Load(object sender, EventArgs e)
        {
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
                TsmiAreas.DropDownItems.Add(TsmiDatoArea);
            });


            DatSeguridad.Aplicativos.ForEach(item =>
            {
                System.Windows.Forms.ToolStripMenuItem TsmiDatoAplicativo = new System.Windows.Forms.ToolStripMenuItem()
                {
                    CheckState = CheckState.Checked,
                    Name = $"TsmiAplicativo_{item.IdCatalogo}",
                    Size = new System.Drawing.Size(180, 22),
                    Text = $"{item.IdCatalogo}-{item.Catalogo}",
                    Checked = item.Seleccion
                };
                TsmiAplicaciones.DropDownItems.Add(TsmiDatoAplicativo);
            });
        }

        private void TsmiSalir_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void TsmiManIncidentes_Click(object sender, EventArgs e)
        {

        }

        private void TsmiManPaquetes_Click(object sender, EventArgs e)
        {

        }

        private void TsmiManMasiva_Click(object sender, EventArgs e)
        {

        }
    }
}
