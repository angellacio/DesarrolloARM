//@(#)SCADE2(W:SKDN08455AK4:Sat.Scade.Net.IDE.Presentacion.Web:PaginaBase:0:25/Noviembre/2008[Sat.Scade.Net.IDE.Presentacion.Web:0:25/Noviembre/2008]) 
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SAT.DyP.Presentacion.Seguridad.Membresia;
using System.Text;
using System.Diagnostics;
using System.Globalization;
using SAT.DyP.Util.Web.WebServiceConfiguracion;

namespace Sat.Scade.Net.IDE.Presentacion.Web
{
    public class PaginaBase : System.Web.UI.Page
    {
        protected override void OnLoad(EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //se controla la expiración de la sesión actual
                this.PreparePageHeaders();
            }

            base.OnLoad(e);
        }

        protected void PreparePageHeaders()
        {
            Response.Expires = 0;
            Response.Cache.SetNoStore();
            Response.AppendHeader("Pragma", "no-cache");
        }

        protected IDCMembershipUser GetCurrentMembershipUser()
        {
            IDCMembershipUser usuarioIdc = null;

            IDCMembershipProvider proveedor = Membership.Provider as IDCMembershipProvider;

            IDCIdentity identity = (IDCIdentity)User.Identity;
            usuarioIdc = (IDCMembershipUser)proveedor.GetUser(identity.UserID, true);

            if (usuarioIdc == null)
            {
                throw new ArgumentException(String.Format(Mensajes.UsuarioNoEncontrado, identity.UserID));
            }
            
            return usuarioIdc;
        }

        protected IDCMembershipUser GetCurrentMembershipUser(string nombreUsuario)
        {
            IDCMembershipUser usuarioIdc = null;

            IDCMembershipProvider proveedor = Membership.Provider as IDCMembershipProvider;

            if (!String.IsNullOrEmpty(nombreUsuario))
            {
                usuarioIdc = proveedor.GetUser(nombreUsuario, true) as IDCMembershipUser;

                if (usuarioIdc == null)
                {
                    throw new ArgumentException(String.Format(Mensajes.UsuarioNoEncontrado, nombreUsuario));
                }
            }

            return usuarioIdc;
        }

        
    }
}
