using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class IDE_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack) return;
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid) return;
        ErrorMarcado.Text = "";
        Autenticacion.idcValidation SA = new Autenticacion.idcValidation();
        //bool s = SA.ValidateUserModule(txtUserName.Text, txtPassword.Text, "81");
        bool s = true;
        
        if (s == true)
        {
            Session["usuario"] = txtUserName.Text;
            Response.Redirect("Default.aspx", true);
        }
        else { ErrorMarcado.Text = "Las Credenciales son invalidas"; }
    }

    protected void valPage_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            if (string.IsNullOrEmpty(txtUserName.Text.Trim()))
                throw new Exception("El usuario es requerido");

            if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
                throw new Exception("La contraseña es requerida");

            System.Threading.Thread.Sleep(1000);

        }
        catch (Exception ex)
        {
            valPage.ErrorMessage = ex.Message;
            args.IsValid = false;
        }
    }
    protected void txtUserName_TextChanged(object sender, EventArgs e)
    {

    }
}