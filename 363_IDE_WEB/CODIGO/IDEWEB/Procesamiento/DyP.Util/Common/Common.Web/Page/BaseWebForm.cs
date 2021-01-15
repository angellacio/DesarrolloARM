//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Util.Web.Page:BaseWebForm:0:21/May/2008[SAT.DyP.Util.Web:1.0:21/May/2008])
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Configuration;
using SAT.DyP.Presentacion.Seguridad.Membresia;

namespace SAT.DyP.Util.Web.Page
{
    /// <summary>
    /// Clase base para las páginas Web
    /// </summary>
    public class BaseWebForm:System.Web.UI.Page
    {       
        private const string KEY_MEDIO_PRESENTACION = "MedioDePresentacion";
        private const string KEY_ENTIDAD_RECEPTORA = "EntidadReceptora";

        #region ASP.NET Methods

        protected override void OnLoad(EventArgs e)
        {
            //se controla la expiración de la sesión actual
            this.PreparePageHeaders();

            base.OnLoad(e);
        }

        #endregion

        /// <summary>
        /// Devuelve el Medio de Presentación configurado en la aplicación
        /// </summary>
        public int ReceiveChanel
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings[KEY_MEDIO_PRESENTACION]); }
        }

        /// <summary>
        /// Devuelve la clave de la Entidad Receptora
        /// </summary>
        public int BranchId
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings[KEY_ENTIDAD_RECEPTORA]); }
        }

        /// <summary>
        /// Devuelve la identidad del usuario autenticado
        /// </summary>
        protected IDCIdentity CurrentIdentity
        {
            get { return (IDCIdentity)User.Identity; }
        }

        /// <summary>
        /// Inicializa las directivas que permiten controlar
        /// la expiración la sesión
        /// </summary>
        protected void PreparePageHeaders()
        {
            Response.Expires = 0;
            Response.Cache.SetNoStore();
            Response.AppendHeader("Pragma", "no-cache");
        }

        /// <summary>
        /// Devuelve la información de la sesión actual
        /// </summary>
        /// <returns></returns>
        protected IDCMembershipUser GetCurrentMembershipUser()
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

        /// <summary>
        /// Ejecuta el cierra de la sesión actual, regresando a la
        /// página de Login
        /// </summary>
        protected void ExecuteSignOut(bool redirectToLoginUrl)
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            HttpContext.Current.Request.Cookies.Clear();

            if (redirectToLoginUrl)
            {
                Response.Redirect(FormsAuthentication.LoginUrl, true);
            }
        }

        protected IDCMembershipProvider CurrentMembershipProvider
        {
            get { return (IDCMembershipProvider)Membership.Provider; }
        }

        /// <summary>
        /// Registra un script que presentará un alert() al regreso del postback actual.
        /// </summary>
        /// <param name="message">El mensaje a mostrar</param>
        protected void ShowErrorMessage(string message)
        {
            string script = String.Format("alert('{0}');", message.Replace("'", "''"));
            this.ClientScript.RegisterStartupScript(typeof(string), "ERROR_MESSAGE", script, true);
        }
    }
}
