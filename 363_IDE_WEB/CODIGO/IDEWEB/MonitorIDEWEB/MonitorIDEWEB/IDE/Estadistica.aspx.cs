using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;


public partial class IDE_Estadistica : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usuario"] == null)
        {

            Response.Redirect("Login.aspx");

        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        ReportViewer1.Visible = true;
        ServiceMonitor.EstadisticaEntrada EE = new ServiceMonitor.EstadisticaEntrada();
        List<ServiceMonitor.EstadisticaSalida> LES = new List<ServiceMonitor.EstadisticaSalida>();

        EE.MedioRecepcion = MedioRecepcion.SelectedValue;
        EE.TipoArchivo = TipoArchivo.SelectedValue;
        EE.Periodo = Periodo.SelectedValue == "0" ? "0" : Periodo.SelectedValue;
        EE.fechaInicio = FechaInicio.Text;
        EE.fechaFin = FechaFin.Text;
        EE.Rfc = Rfc.Text.ToString();

        ServiceMonitor.ServicioMonitorClient SM = new ServiceMonitor.ServicioMonitorClient();
        LES = SM.TraerEstadistica(EE).ToList();

        ReportViewer1.LocalReport.DataSources.Clear();

        ReportDataSource rds = new ReportDataSource("DataSet1", LES);
        ReportViewer1.LocalReport.DataSources.Clear();
        ReportViewer1.LocalReport.DataSources.Add(rds);
        
        ReportViewer1.LocalReport.Refresh();
        
        
    }
    protected void FechaInicioC_SelectionChanged(object sender, EventArgs e)
    {
        FechaInicio.Text = FechaInicioC.SelectedDate.ToShortDateString().ToString();
        FechaInicioC.Visible = false;
    }
    protected void FechaFinC_SelectionChanged(object sender, EventArgs e)
    {
        FechaFin.Text = FechaFinC.SelectedDate.ToShortDateString().ToString();
        FechaFinC.Visible = false;

    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        FechaInicioC.Visible = true;
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        FechaFinC.Visible = true;
    }
    protected void ImageButton2_Click1(object sender, ImageClickEventArgs e)
    {
        FechaFinC.Visible = true;
    }
}