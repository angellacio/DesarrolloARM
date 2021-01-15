using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class IDE_Acuses : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usuario"] == null)
        {

            Response.Redirect("Login.aspx");

        }
        int Folio =  int.Parse(Request["Folio"]);
        ServiceMonitor.ServicioMonitorClient SC = new ServiceMonitor.ServicioMonitorClient();
        try
        {
            Label1.Text = Server.HtmlDecode(SC.TraerAcuse(Folio));
                

        }
        catch (Exception se)
        {
            Label1.Text = se.ToString();
        }
    }
    
}