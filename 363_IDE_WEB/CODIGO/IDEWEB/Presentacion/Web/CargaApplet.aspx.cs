using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAT.DyP.Presentacion.Seguridad.Membresia;
using System.Configuration;
using SAT.DyP.Util.Web.WebServiceConfiguracion;

namespace Sat.Scade.Net.IDE.Presentacion.Web
{
    public partial class CargaApplet : PaginaBase
    {
        private IDCMembershipUser usuario;


        protected void Page_Load(object sender, EventArgs e)
        {
            this.usuario = base.GetCurrentMembershipUser();
            string sSendParamApplet = "<param name='java_arguments' value= '{2}' /><param name='Rfclogeo' value= '{0}' /><param name='pruebaLab' value= '{1}' />";
            string banderaLab = "false", sAppletMemory = "";
            try
            {
                sAppletMemory = ConfigurationManager.AppSettings["AppletMemory"].ToString().Trim();
                banderaLab = ConfigurationManager.AppSettings["EnLaboratorio"].ToString();
            }
            catch (Exception ex)
            {
                Utilerias.GenerarMensaje("Error con el parametro EnLaboratorio en el Web.config", ex);
                appFEA.InnerHtml = "<param name='pruebaLab' value= '" + "false" + "' />";
            }

            appFEA.InnerHtml = string.Format(sSendParamApplet, usuario.RFC.Trim().ToUpper(), banderaLab, sAppletMemory);
            if (!Page.IsPostBack)
            {
                rfcLabel.Text = usuario.RFC;
                razonSocialLabel.Text = usuario.RazonSocial;
                Session[Parametros.Sesion] = null;
                Response.Cookies[Parametros.Sesion]["FechaRecepcion"] = null;
            }
        }
        internal IDCMembershipUser getUsuario()
        {
            this.usuario = base.GetCurrentMembershipUser();
            return usuario;
        }

        protected void EnviarDeclaracion_Click(object sender, EventArgs e)
        {
            Response.Redirect(Urls.NotificacionEncrip);
        }

        protected void satSesionLoginStatus_LoggedOut(object sender, EventArgs e)
        {
            string direccion = "";
            try
            {
                Configuracion servicioConfiguracion = new Configuracion();
                direccion = servicioConfiguracion.ReadSetting("Sat.Scade.Net.UrlNovellLogout");
            }
            catch (Exception ex)
            {
                Sat.Scade.Net.IDE.Presentacion.Web.Utilerias.RegistrarLogEventos(ex);
                throw ex;
            }

            if (Convert.ToBoolean(ConfigurationManager.AppSettings["AutenticacionNovell"]))
            {
                Session.Abandon();
                HttpContext.Current.Request.Cookies.Clear();
                Response.Redirect(direccion);
            }
            else
            {
                Session.Abandon();
                HttpContext.Current.Request.Cookies.Clear();
                Response.Redirect(@"~\Login.aspx");
            }
        }
    }
}