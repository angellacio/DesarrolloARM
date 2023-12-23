using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using SAT.CreditosFiscales.Motor.AccesoDatos.AdmonReglas;
using SAT.CreditosFiscales.Motor.AccesoDatos.Catalogos;
using Entidades = SAT.CreditosFiscales.Motor.Entidades.Catalogos;
using EntidadRegla = SAT.CreditosFiscales.Motor.Entidades.AdmonRegla;

namespace CargaDeCatalogos.Motor
{
    public partial class ReglasMotor : Form
    {
        public ReglasMotor()
        {
            InitializeComponent();
        }

        private void ReglasMotor_Load(object sender, EventArgs e)
        {
            try
            {
                List<Entidades.CatTipoDocumento> tipodocumento = DalTipoDocumento.ConsultaTipoDocumentos();
                List<Entidades.Aplicacion> aplicacion = Aplicacion.ConsultaAplicaciones();

                cmbTipoDocumento.DataSource = tipodocumento;
                cmbTipoDocumento.DisplayMember = "Nombre";
                cmbTipoDocumento.ValueMember = "IdTipoDocumento";

                cmbAplicacion.DataSource = aplicacion;
                cmbAplicacion.DisplayMember = "Nombre";
                cmbAplicacion.ValueMember = "IdAplicacion";

                CargarReglas(null, null);

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnExaminar_Click(object sender, EventArgs e)
        {
            FileDialogExaminar.Filter = "Xml files (*.xml, *.xslt, *.xsl) | *.xml; *.xslt; *.xsl;";
            FileDialogExaminar.ShowDialog();

            if (FileDialogExaminar.FileName != string.Empty)
                txtArchivoRegla.Text = FileDialogExaminar.FileName;
        }

        private void CargarReglas(short? idAplicacion = null, short? idTipoDocPago = null)
        {

            dgReglas.DataSource = DalReglas.ConsultaReglas(idAplicacion, idTipoDocPago).ListaReglas;            
        }

        private void btnBuscarReglas_Click(object sender, EventArgs e)
        {
            try
            {   
                CargarReglas(Convert.ToInt16(cmbAplicacion.SelectedValue), Convert.ToInt16(cmbTipoDocumento.SelectedValue));
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void dgReglas_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Asignar los datos en lugar correspondiente
            try
            {
                lblIDRegla.Text = dgReglas.Rows[e.RowIndex].Cells["IdRegla"].FormattedValue.ToString();
                txtDescripcion.Text = dgReglas.Rows[e.RowIndex].Cells["Descripcion"].FormattedValue.ToString();
                ckEsValidacion.Checked = Convert.ToBoolean(dgReglas.Rows[e.RowIndex].Cells["EsValidacion"].FormattedValue);
                ckAntesInsersion.Checked = Convert.ToBoolean(dgReglas.Rows[e.RowIndex].Cells["AntesDeInsercion"].FormattedValue);
                txtSecuencia.Text = dgReglas.Rows[e.RowIndex].Cells["Secuencia"].FormattedValue.ToString();
                txtArchivoRegla.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNuevaRegla_Click(object sender, EventArgs e)
        {
            lblIDRegla.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            ckEsValidacion.Checked = false;
            ckEsValidacion.Checked = false;
            txtArchivoRegla.Text = string.Empty;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {            
            try
            {
                //Guardar Regla
                XmlDocument documentoRegla = new XmlDocument();
                documentoRegla.Load(txtArchivoRegla.Text);


                Guid IdRegla = DalReglas.InsertaRegla(txtDescripcion.Text.Trim(), documentoRegla.InnerXml, ckEsValidacion.Checked);

                //Guardar Admon Regla
                DalReglas.InsertaAdmonRegla(IdRegla, Convert.ToInt32(cmbAplicacion.SelectedValue), Convert.ToInt32(cmbTipoDocumento.SelectedValue),
                    Convert.ToInt32(txtSecuencia.Text), ckAntesInsersion.Checked);

                MessageBox.Show("La regla se almacenó correctamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
