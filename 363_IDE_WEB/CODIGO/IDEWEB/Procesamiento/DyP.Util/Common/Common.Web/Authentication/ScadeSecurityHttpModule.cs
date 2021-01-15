
//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Util.Web.Authentication:ScadeSecurityHttpModule:0:21/May/2008[SAT.DyP.Util.Web:1.0:21/May/2008])
	
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Diagnostics;
using SAT.DyP.Presentacion.Seguridad.Membresia;
using SAT.DyP.Presentacion.Seguridad.Membresia.wsIDCValidate;

namespace SAT.DyP.Util.Web.Authentication
{
    /// <summary>
    /// HttpModule responsable de asignar el IDC membership de forma
    /// correcta para todas las aplicacions Web
    /// </summary>
    public class ScadeSecurityHttpModule:IHttpModule
    {
        #region IHttpModule Members

        public void Dispose()
        {
                       
        }

        public void Init(HttpApplication context)
        {            
            context.PostAuthenticateRequest += new EventHandler(OnPostAuthenticateRequest);
            context.AuthenticateRequest += new EventHandler(OnAuthenticateRequest);            
        }

        /// <summary>
        /// Obtiene la información del perfil que le corresponde a un usuario
        /// en SPED
        /// </summary>
        /// <param name="userName">RFC</param>
        /// <returns></returns>
        private string GetUserProfile(string userName)
        {
            string profileInfo = string.Empty;

            try
            {
                AppSettingsReader reader=new AppSettingsReader();
                string url=reader.GetValue("autenticacion",typeof(string)).ToString();

                idcValidation webServiceInvoke = new idcValidation();
                webServiceInvoke.Url = url;

                profileInfo = webServiceInvoke.ObtenerPerfil(userName);
            }
            catch (Exception ex)
            {
                Logging.EventLogHelper.WriteErrorEntry(ex.Message, Logging.CoreLogEventIdentifier.AUTHENTICATION_EVENT_ID);
            }

            return profileInfo;
        }

        void OnAuthenticateRequest(object sender, EventArgs e)
        {
            HttpApplication app = sender as HttpApplication;
            HttpContext ctx = app.Context;

            if (ctx.Request.IsAuthenticated)
            {
                if (ctx.User != null)
                {
                    if (!string.IsNullOrEmpty(ctx.User.Identity.Name))
                    {
                        string userName=ctx.User.Identity.Name;

                        string profile = GetUserProfile(userName);

                        if (string.IsNullOrEmpty(profile))
                        {
                            ctx.User = new IDCPrincipal(userName);
                        }
                        else
                        {
                            ctx.User = new IDCPrincipal(userName, profile);
                        }
                    }
                }
            }
        }

        void OnPostAuthenticateRequest(object sender, EventArgs e)
        {
            HttpApplication app = sender as HttpApplication;
            HttpContext ctx = app.Context;
            string userName = ctx.User.Identity.Name;
                            
            if (!string.IsNullOrEmpty(userName))
            {               
                if (!ctx.User.Identity.IsAuthenticated)
                {
                    ctx.Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
                    ctx.Response.Status = "401";
                    ctx.Response.End();
                    ctx.Response.Redirect(FormsAuthentication.LoginUrl);
                }                
            }
        }

        #endregion
    }
}
