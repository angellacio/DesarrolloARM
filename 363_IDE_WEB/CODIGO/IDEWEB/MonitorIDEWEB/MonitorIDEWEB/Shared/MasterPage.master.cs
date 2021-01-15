using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Shared_MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        
    }
    protected void btnSalir_Click(object sender, EventArgs e)
    {
       
    }
    protected void XmlDataSource1_Transforming(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Session["usuario"] = null;
        Response.Redirect("Login.aspx");
    }
}
