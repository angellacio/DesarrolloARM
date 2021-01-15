using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class IDE_Default : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["usuario"] == null)
        {

            Response.Redirect("Login.aspx");

        }

        else
        {

            DateTime fecha = DateTime.Now; //captura la fecha y la hora del sistema
            int ANO = fecha.Year;

            for (int an = 2008; an <= ANO; an++)
            {
                string te = Convert.ToString(an);
                if (Ejercicio.Items.FindByValue(te) != null)
                { }
                else
                {
                    Ejercicio.Items.Add(te);
                }
            }

        }

    }

    protected void GridView1_SelectedIndexChanged1(object sender, EventArgs e)
    {

    }
    protected void DropDownList5_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void DropDownList6_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void DropDownList7_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {


        ServiceMonitor.ServicioMonitorClient SC = new ServiceMonitor.ServicioMonitorClient();
        ServiceMonitor.DeclaracionEntrada ren = new ServiceMonitor.DeclaracionEntrada();
        List<ServiceMonitor.DeclaracionSalida> sa = new List<ServiceMonitor.DeclaracionSalida>();

        ren.Estatus = Estatus.SelectedValue;
        ren.Folio = Folio.Text == "" ? 0 : Int32.Parse(Folio.Text);
        ren.NombreArchivo = NombreArchivo.Text;
        ren.Rfc = Rfc.Text;
        //ren.Banco = Banco.Text;
        //ren.TipoArchivo = TipoArchivo.SelectedValue;
        ren.MedioRecepcion = MedioRecepcion.SelectedValue;
        ren.fechaInicio = FechaInicio.Text;
        ren.fechaFin = FechaFin.Text;
        //ren.UltimoEstado = UltimoEstado.SelectedValue;
        ren.Sector = Sector.SelectedValue;
        ren.Formato = Formato.Text;
        ren.MotivoRechazo = MotivoRechazo.Text;
        ren.RazonSocial = RazonSocial.Text;
        ren.Periodo = Int32.Parse(Periodo.SelectedValue);
        ren.Ejercicio = Ejercicio.SelectedValue == "Todos" ? 0 : Int32.Parse(Ejercicio.SelectedValue);
        ren.Operaciones = Operaciones.Text == "" ? 0 : Int32.Parse(Operaciones.Text);
        ren.FormaDeclaracion = FormaDeclaracion.SelectedValue;
        ren.EstadoDeclaracion = EstadoDeclaracion.SelectedValue;
        ren.Bloque = 0;
        ren.Inicio = 0;

        sa = SC.TraerDeclaraciones(ren).ToList();

        SC.Close();


        sa.ForEach(item =>
            {
                item.MotivoRechazo = Server.HtmlDecode(item.MotivoRechazo);
            });


        GridView1.DataSource = sa;
        GridView1.DataBind();

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Folio.Text = string.Empty;
        FechaInicio.Text = string.Empty;
        FechaFin.Text = string.Empty;
        Estatus.SelectedValue = "0";
        NombreArchivo.Text = string.Empty;
        Rfc.Text = string.Empty;
        RazonSocial.Text = string.Empty;
        Sector.SelectedIndex = -1;
        MedioRecepcion.SelectedIndex = -1;
        FormaDeclaracion.SelectedValue = "2";
        EstadoDeclaracion.SelectedValue = "2";
        Ejercicio.SelectedIndex = -1;
        Periodo.SelectedValue = "-1";
        Operaciones.Text = string.Empty;
        Formato.Text = string.Empty;
        MotivoRechazo.Text = string.Empty;

        GridView1.DataSource = null;
        GridView1.DataBind();
    }

    protected void FechaInicioC_SelectionChanged1(object sender, EventArgs e)
    {
        FechaInicio.Text = FechaInicioC.SelectedDate.ToShortDateString().ToString();
        FechaInicioC.Visible = false;
    }
    protected void FechaFinC_SelectionChanged(object sender, EventArgs e)
    {
        FechaFin.Text = FechaFinC.SelectedDate.ToShortDateString().ToString();
        FechaFinC.Visible = false;
    }
    protected void BCalen1_Click(object sender, ImageClickEventArgs e)
    {
        FechaInicioC.Visible = true;
    }
    protected void BCalen2_Click(object sender, ImageClickEventArgs e)
    {
        FechaFinC.Visible = true;
    }

    protected void Periodo_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Ejercicio_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Periodo_SelectedIndexChanged1(object sender, EventArgs e)
    {

    }
}