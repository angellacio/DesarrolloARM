using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Security;
using SAT.DyP.Presentacion.Seguridad.Membresia;
using SAT.DyP.Util.Web.Security;

namespace Sat.Scade.Net.IDE.Presentacion.Web
{
    public partial class LoginFEA : PaginaBase
    {
        private string sTitulo
        {
            get
            {
                string result = ConfigurationManager.AppSettings["TituloReceptionHeader"].ToString().Trim();
                result = result.Replace("|", "<br>");
                return result;
            }
        }
        //private string RFC_Login = string.Empty;
        //private string SERIE_Cert = string.Empty;
        //private string mensajeError = string.Empty;

        private string Autenti_RFC
        {
            get
            {
                return Request.Form["rfcEnvio"] == null ? "cin960904fq2" : Request.Form["rfcEnvio"].Trim();
            }
        }
        private string Autenti_Firma
        {
            get
            {
                return Request.Form["firmaDigital"] == null ? "" : Request.Form["firmaDigital"].Trim();
            }
        }
        private string Autenti_Serie
        {
            get
            {
                return Request.Form["numeroSerie"] == null ? "" : Request.Form["numeroSerie"].Trim();
            }
        }
        private string Autenti_Cadena
        {
            get
            {
                return Request.Form["cadenaOriginal"] == null ? "" : Request.Form["cadenaOriginal"].Trim();
            }
        }

        //protected bool ValidaFea(string Serie)
        //{
        //    bool resultado = CertificateHelper.IsValidAutenticationCertificate(Serie, out mensajeError);

        //    if (resultado)
        //    {
        //        this.InitializeFormsAuthentication(RFC_Login, 30);
        //        ExecuteSignOnFea(RFC_Login, true);
        //    }
        //    return resultado;
        //}
        //public void InitializeFormsAuthentication(string userName, int minutes)
        //{
        //    FormsAuthentication.Initialize();
        //    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(2,
        //                                                                     userName.Trim(),
        //                                                                     DateTime.Now,
        //                                                                     DateTime.Now.AddMinutes(minutes),
        //                                                                     false,
        //                                                                     FormsAuthentication.FormsCookieName,
        //                                                                     FormsAuthentication.FormsCookiePath);

        //    string securityData = FormsAuthentication.Encrypt(ticket);
        //    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, securityData);
        //    Response.Cookies.Add(cookie);
        //}
        //protected void ExecuteSignOnFea(string userName, bool validateFIEL)
        //{
        //    string controlStep = "";

        //    FormsAuthentication.SetAuthCookie(userName, false);
        //    controlStep = "ENABLE_AUTH";

        //    if (this.WebSiteAccessMethod == WebAccessMethod.Internet && validateFIEL)
        //    {

        //        try
        //        {
        //            IDCMembershipUser currentUser = this.GetCurrentMembershipUserFEA();
        //            controlStep = "GET_USER_INFO";

        //            if (currentUser != null)
        //            {
        //                if (currentUser.TieneFIEL && this.ForceFIELAuthentication)
        //                {
        //                    //se cierra la sesión
        //                    ExecuteSignOut(false);
        //                    controlStep = "CLOSE_SESSION";
        //                    FormsAuthentication.RedirectToLoginPage("mode=" + PREFIEL_LOGIN_MODE);
        //                    Response.End();
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            EventLogHelper.WriteErrorEntry("Control Error:" + controlStep, CoreLogEventIdentifier.COMMON_WEB_LOGIN_EVENT_ID);
        //            throw new SAT.SCADE.NET.Common.Types.PlatformException("Se presento un error en el proceso de autenticación del usuario", ex);
        //        }
        //    }
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                lblTitulo.Text = sTitulo;

                Loggin.Text = Utility.Text.Continuar;
                MailUpdateLink.Text = Utility.Text.Mail;
                CiecLinkButton.Text = Utility.Text.Ciec;

                lblTitulo.Text = string.Format("RFC: {0} FIRMA: {1} NUMERO: {2}", Autenti_RFC, Autenti_Firma, Autenti_Serie);

                if (this.IniciaSesion())
                    Response.Redirect("CargaApplet.aspx", true);
                else
                    Response.Redirect("LoginFEA.asp", true);
            }
        }

        private Boolean IniciaSesion()
        {
            Boolean bolResult = false;

            if (Autenti_RFC != "" && Autenti_Serie != "" && Autenti_Firma != "" && Autenti_Cadena != "")
            {
                IDCMembershipUser currentUser = this.GetCurrentMembershipUserFEA();

                FormsAuthentication.Initialize();
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(2, Autenti_RFC.Trim(), DateTime.Now, DateTime.Now.AddMinutes(80), false,
                                                                                 FormsAuthentication.FormsCookieName, FormsAuthentication.FormsCookiePath);
                string securityData = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, securityData);
                Response.Cookies.Add(cookie);

                bolResult = true;
            }
            return bolResult;
        }

        protected IDCMembershipUser GetCurrentMembershipUserFEA()
        {
            IDCMembershipUser user = null;
            IDCMembershipProvider prov = Membership.Provider as IDCMembershipProvider;

            if (!String.IsNullOrEmpty(User.Identity.Name))
            {
                IDCIdentity identity = (IDCIdentity)User.Identity;
                user = (IDCMembershipUser)prov.GetUser(identity.UserID, true);
            }

            return user;
        }

        protected void lkbAutContraseña_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}