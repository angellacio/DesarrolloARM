using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MonitorPaquetesWEB
{
    public partial class Incidentes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Datos_Incidentes();
        }

        protected void lnkInicio_Click(object sender, EventArgs e)
        {
            Response.Redirect("Inicio.aspx");
        }

        protected void btnIncidentes_Click(object sender, EventArgs e)
        {
            Response.Redirect("Incidentes.aspx");
        }

        private void Datos_Incidentes()
        {
            Negocio incidentes = new Negocio();
            GdvIncidentesCurso.DataSource = incidentes.IncidentesSinPaquete();
            GdvIncidentesCurso.DataBind();

            GdvIncidentesPaquete.DataSource = incidentes.IncidentesConPaquete();
            GdvIncidentesPaquete.DataBind();
        }

    }
}