using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.IO;

namespace MonitorPaquetesWEB
{
    public partial class Inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Actualiza();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Actualiza();
        }

        private void Actualiza()
        {
            DataTable Paquetes = new DataTable();
            Negocio Consultas = new Negocio();
            Paquetes = Consultas.Monitoreo_Paquetes();
            GdvPaquetes.DataSource = Paquetes;
            GdvPaquetes.DataBind();

            Paquetes = Consultas.Avisos();
            GdvNotificaciones.DataSource = Paquetes;
            GdvNotificaciones.DataBind();
        }

        protected void btnIncidentes_Click(object sender, EventArgs e)
        {
            Response.Redirect("Incidentes.aspx");
        }

        protected void lnkInicio_Click(object sender, EventArgs e)
        {
            Response.Redirect("Inicio.aspx");
        }

        protected void GdvPaquetes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Detalle")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                Session["Paquete"] = GdvPaquetes.Rows[rowIndex].Cells[2].Text;
                Response.Redirect("Detalle_Paquete.aspx");
            }
        }
    }
}