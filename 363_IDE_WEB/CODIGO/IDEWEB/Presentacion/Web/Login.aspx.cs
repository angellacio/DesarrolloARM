using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SAT.DyP.Presentacion.Seguridad.Membresia;
using SAT.DyP.Util.Web.WebServiceConfiguracion;
using Sat.Scade.Net.IDE.Presentacion.Web;

namespace Sat.Scade.Net.IDE.Presentacion.Web
{
    public partial class Login : PaginaBase
    {
        private bool TieneFiel
        {
            get
            { 
                IDCMembershipUser usuarioIdc = base.GetCurrentMembershipUser(LoginScade1.NombreUsuario);
                return usuarioIdc.TieneFIEL;
            }
        }

        // R2. Agregamos un método para obtener la Url de Actualización
        protected string GetUpdateMailDestinationUrl()
        {
            try
            {
                Configuracion servicioConfiguracion = new Configuracion();
                servicioConfiguracion.Url = Parametros.UrlServicioConfiguracion;
                return servicioConfiguracion.ReadSetting(Configuraciones.UrlServicioActualizacionCorreo);
            }
            catch { return string.Empty; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            // R2. Ocultamos los Checkbox
            LoginScade1.VisualizarMailCiec = LoginScade1.VisualizarMailFiel = false;

            // LoginScade1.UrlPaginaDestinoUpdateMail = GetUpdateMailDestinationUrl();
            LoginScade1.UrlPaginaDestinoUpdateMail = string.Format(GetUpdateMailDestinationUrl(), Server.UrlEncode((HttpContext.Current.Request.Url).AbsoluteUri));
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["AutenticacionNovell"]))
            {
              AutenticaSAML();
            }
            else
            {

              if (Parametros.ForzarServicioNoDisponible)
              {
                Response.Redirect(Urls.ServicioNodisponible);
              }
            }
        }
        protected void AutenticaSAML()
        {
          String header_RFC = base.Request.Headers[ConfigurationManager.AppSettings["HeaderRFC"]];
          String header_PSW = base.Request.Headers[ConfigurationManager.AppSettings["HeaderPassword"]];
          String header_CER = base.Request.Headers[ConfigurationManager.AppSettings["HeaderCertificado"]];

          if (!String.IsNullOrEmpty(header_RFC) && !String.IsNullOrEmpty(header_PSW))
          {

            FormsAuthentication.SetAuthCookie(header_RFC, true);            
            Response.Redirect(@"~\CargaApplet.aspx");
          }
          else
          {
            ServicioNoDisponible("Los valores de las credenciales(Headers) son nulos");
          }
        }

        protected void ServicioNoDisponible(string error)
        {
          this.pnlLogin.Visible = false;
          this.lnkInicioSesionNovell.Text = ConfigurationManager.AppSettings["TextoUrlInicioSesiónNovell"];
          Configuracion servicioConfiguracion = new Configuracion();
          this.lnkInicioSesionNovell.NavigateUrl = servicioConfiguracion.ReadSetting("Sat.DyP.UrlNovellLogout");
          this.pnlErrorLogin.Visible = true;
        }

        protected void LoginScade1_LoggedInCiec(object sender, EventArgs e)
        {            
            LoginScade1.TieneFiel = TieneFiel;
            if (LoginScade1.TieneFiel)
            {
                HeaderLogin.ForzarMedioInternet = true;
            }
        }

        protected void LoginScade1_CambiandoModo(object sender, CommandEventArgs e)
        {
            HeaderLogin.ForzarMedioInternet = false;
            if (LoginScade1.Modo == Sat.Scade.Net.IDE.Presentacion.Web.LoginType.Fiel)
            {
                HeaderLogin.ForzarMedioInternet = true;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            HeaderLogin.ForzarMedioInternet = false;
            if (LoginScade1.Modo == Sat.Scade.Net.IDE.Presentacion.Web.LoginType.Fiel)
            {
                HeaderLogin.ForzarMedioInternet = true;
            }
        }
    }
}