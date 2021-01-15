using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MonitorPaquetesWEB
{
    public partial class Detalle_Paquete : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           ObtenDatos((string)HttpContext.Current.Session["Paquete"]);
        }

        protected void lnkInicio_Click(object sender, EventArgs e)
        {
            Response.Redirect("Inicio.aspx");
        }

        private void ObtenDatos(string paquete)
        {
            Negocio DetallePaquete = new Negocio();
            Paquete det_paquete = new Paquete(paquete);
            Desarrollador det_desa = new Desarrollador(paquete);

            txtRdl.Text = det_paquete.Rdl;
            txtPaquete.Text = det_paquete.Nombre;
            txtDesarrollador.Text = det_paquete.Desarrollador;
            txtEstado.Text = det_paquete.Estado;
            txtIncidentesRela.Text = det_paquete.IncidentesRela;
            txtPaquetesRela.Text = det_paquete.PaquetesRela;
            txtRechazo.Text = det_paquete.Rechazo;

            txtNombre.Text = det_desa.Nombre;
            txtCorreo.Text = det_desa.Correo;
            txtTelefono.Text = det_desa.Teloficina;
            txtMovil.Text = det_desa.Telmovil;
            

        }

        protected void btnIncidentes_Click(object sender, EventArgs e)
        {
            Response.Redirect("Incidentes.aspx");
        }
    }
}