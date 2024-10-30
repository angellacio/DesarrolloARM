using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SAT.CreditosFiscales.Motor.AccesoDatos.Catalogos;
using Entidades = SAT.CreditosFiscales.Motor.Entidades.Catalogos;

namespace CargaDeCatalogos.Catalogos
{
    public partial class Transacciones : Form
    {
        List<Entidades.CatTransaccion> nuevasTransacciones = new List<Entidades.CatTransaccion>();

        public Transacciones()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnBuscarDyP_Click(object sender, EventArgs e)
        {
            try
            {
                CatalogosDyP.ConsultaCatalogosClient client = new CatalogosDyP.ConsultaCatalogosClient();
                CatalogosDyP.CatalogoFiltro filtro = new CatalogosDyP.CatalogoFiltro();
                
                filtro.IdOrigen = 5;
                filtro.TipoDocumento = new CatalogosDyP.TipoDocumento();
                filtro.TipoDocumento.IdTipoDocumento = Convert.ToInt32(cmbTipoDocumento.SelectedValue);          

                CatalogosDyP.Transaccion[] transacciones = client.ConsultarTransaccionesXTipoDocumento(filtro);                

                if (transacciones.Count() > 0)
                {
                    foreach (CatalogosDyP.Transaccion transaccion in transacciones)
                    {
                        nuevasTransacciones.Add(
                            new Entidades.CatTransaccion
                            {
                                IdAplicacion = Convert.ToInt16(cmbAplicacion.SelectedValue),
                                IdTransaccion = transaccion.IdTransaccion.ToString(),
                                Descripcion = transaccion.Descripcion,
                                IdTipoTransaccion = transaccion.TipoTransaccion.IdTipoTransaccion,
                                TipoTransaccion = transaccion.TipoTransaccion.Descripcion,
                                IdTipoDocumento = Convert.ToInt16(cmbTipoDocumento.SelectedValue),
                                EsRequerido = TransaccionesObligatorias.TransaccionesObligatorias.ObtieneTransaccionObligatoria(Convert.ToInt16(cmbTipoDocumento.SelectedValue),transaccion.IdTransaccion.ToString())
                            }
                            );
                    }

                    dgListaResultados.DataSource = nuevasTransacciones;
                }
                else
                    MessageBox.Show("No existen Datos");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnGuardarDBMotor_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Entidades.CatTransaccion transaccion in nuevasTransacciones)
                {
                    DalCatTransaccion.InsertaCatTrasaccion(transaccion.IdTransaccion, transaccion.Descripcion,
                                                            transaccion.IdTipoTransaccion, transaccion.TipoTransaccion,
                                                            transaccion.IdTipoDocumento, transaccion.IdAplicacion,
                                                           transaccion.EsRequerido, true);
                }


                MessageBox.Show("El registro se almacenó correctamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Transacciones_Load(object sender, EventArgs e)
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

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
