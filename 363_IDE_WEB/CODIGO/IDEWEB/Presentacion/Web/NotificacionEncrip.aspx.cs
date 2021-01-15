using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sat.Scade.Net.IDE.Presentacion.Web;
using SAT.DyP.Util.Web.WebServiceConfiguracion;
using SAT.DyP.Presentacion.Seguridad.Membresia;
using System.Configuration;

namespace Sat.Scade.Net.IDE.Presentacion.Web
{
    public partial class NotificacionEncrip : PaginaBase
    {
        private IDCMembershipUser usuario;

        protected void Page_Load(object sender, EventArgs e)
        {
            MensajeAcuse acuse = null;

            if (!IsPostBack)
            {
                Utilerias.RegistrarLogEventosAuditoria(string.Format("\r\n NotificacionEncrip.Page_Load {0}: {1}. ", Parametros.Sesion, Session[Parametros.Sesion]));
                rfcLabel.Text = "";

                try
                {
                    usuario = base.GetCurrentMembershipUser();
                    rfcLabel.Text = usuario.RFC;
                }
                catch (Exception ex)
                {
                    Utilerias.RegistrarLogEventosAuditoria(string.Format("\r\n NotificacionEncrip.Page_Load Membership: {0}. ", ex.ToString()));
                }

                satSesionLoginStatus.LoginText = "Inicio";
                satSesionLoginStatus.LogoutText = "Inicio";

                if (Parametros.UsarCookies)
                {
                    acuse = new MensajeAcuse();
                    acuse.MedioPresentacion = Convert.ToInt32(Request.Cookies[Parametros.Sesion]["MedioPresentacion"]);
                    acuse.Estatus = Convert.ToInt32(Request.Cookies[Parametros.Sesion]["Estatus"]);
                    acuse.RfcContribuyente = Server.UrlDecode(Request.Cookies[Parametros.Sesion]["RfcContribuyente"]);
                    acuse.RfcAutenticacion = Server.UrlDecode(Request.Cookies[Parametros.Sesion]["RfcAutenticacion"]);
                    acuse.NombreArchivo = Server.UrlDecode(Request.Cookies[Parametros.Sesion]["NombreArchivo"]);
                    acuse.TamañoArchivo = Convert.ToInt32(Request.Cookies[Parametros.Sesion]["TamanoArchivo"]);
                    acuse.FechaRecepcion = Request.Cookies[Parametros.Sesion]["FechaRecepcion"];
                    acuse.HoraRecepcion = Request.Cookies[Parametros.Sesion]["HoraRecepcion"];
                    acuse.Folio = Request.Cookies[Parametros.Sesion]["Folio"];
                    acuse.Mensaje = Server.UrlDecode(Request.Cookies[Parametros.Sesion]["Mensaje"]);
                }
                else
                {
                    acuse = Session[Parametros.Sesion] as MensajeAcuse;
                }

                this.GenerarHtml(acuse);
            }
        }

        private void GenerarHtml(MensajeAcuse acuse)
        {
            HtmlFormaterXml.TransformSource = (TipoRespuesta)acuse.Estatus == TipoRespuesta.Aceptada ? Urls.DeclaracionAceptada : Urls.DeclaracionRechazada;
            HtmlFormaterXml.DocumentContent = acuse.ToString();
        }

        protected void satSesionLoginStatus_LoggedOut(object sender, EventArgs e)
        {
            string direccion = "";
            try
            {
                Configuracion servicioConfiguracion = new Configuracion();
                //direccion = servicioConfiguracion.ReadSetting("Sat.Scade.Net.UrlNovellLogout");
                direccion = servicioConfiguracion.ReadSetting("Sat.DyP.UrlNovellLogout");
                
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

        protected void enviarMasHyperLink_Click(object sender, EventArgs e)
        {
            Response.Cookies[Parametros.Sesion]["FechaRecepcion"] = null;
            Session[Parametros.Sesion] = null;
            Response.Redirect("~/CargaApplet.aspx");
        }
    }
}