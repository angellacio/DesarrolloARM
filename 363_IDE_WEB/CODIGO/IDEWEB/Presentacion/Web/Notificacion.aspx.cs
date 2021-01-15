using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sat.Scade.Net.IDE.Presentacion.Web;
using SAT.DyP.Presentacion.Seguridad.Membresia;

namespace Sat.Scade.Net.IDE.Presentacion.Web
{
    public partial class Notificacion : PaginaBase
    {
        private IDCMembershipUser usuario;

        //private MensajeAcuse acuse;
        private int Cook_MedioPresentacion
        {
            get
            {
                int result = -1;

                if (Parametros.UsarCookies)
                {
                    if (Request.Cookies[Parametros.Sesion] != null && Request.Cookies[Parametros.Sesion]["MedioPresentacion"] != null)
                        result = int.Parse(Request.Cookies[Parametros.Sesion]["MedioPresentacion"].Trim());
                }
                else
                {
                    if (Session[Parametros.Sesion] != null) result = ((MensajeAcuse)Session[Parametros.Sesion]).MedioPresentacion;
                }

                return result;
            }
            set
            {
                if (Parametros.UsarCookies)
                    Response.Cookies[Parametros.Sesion]["MedioPresentacion"] = value.ToString().Trim();
                else
                {
                    MensajeAcuse mAcuse = (MensajeAcuse)Session[Parametros.Sesion];
                    if (mAcuse == null) mAcuse = new MensajeAcuse();
                    mAcuse.MedioPresentacion = value;
                    Session[Parametros.Sesion] = mAcuse;
                }
            }
        }
        private int Cook_Estatus
        {
            get
            {
                int result = -1;

                if (Parametros.UsarCookies)
                {
                    if (Request.Cookies[Parametros.Sesion] != null && Request.Cookies[Parametros.Sesion]["Estatus"] != null)
                        result = int.Parse(Request.Cookies[Parametros.Sesion]["Estatus"].Trim());
                }
                else
                {
                    if (Session[Parametros.Sesion] != null) result = ((MensajeAcuse)Session[Parametros.Sesion]).Estatus;
                }

                return result;
            }
            set
            {
                if (Parametros.UsarCookies)
                    Response.Cookies[Parametros.Sesion]["Estatus"] = value.ToString().Trim();
                else
                {
                    MensajeAcuse mAcuse = (MensajeAcuse)Session[Parametros.Sesion];
                    if (mAcuse == null) mAcuse = new MensajeAcuse();
                    mAcuse.Estatus = value;
                    Session[Parametros.Sesion] = mAcuse;
                }
            }
        }
        private string Cook_RfcContribuyente
        {
            get
            {
                string result = "";

                if (Parametros.UsarCookies)
                {
                    if (Request.Cookies[Parametros.Sesion] != null && Request.Cookies[Parametros.Sesion]["RfcContribuyente"] != null)
                        result = Request.Cookies[Parametros.Sesion]["RfcContribuyente"].Trim();
                }
                else
                {
                    if (Session[Parametros.Sesion] != null) result = ((MensajeAcuse)Session[Parametros.Sesion]).RfcContribuyente;
                }
                return result;
            }
            set
            {
                if (Parametros.UsarCookies)
                    Response.Cookies[Parametros.Sesion]["RfcContribuyente"] = value.ToString().Trim();
                else
                {
                    MensajeAcuse mAcuse = (MensajeAcuse)Session[Parametros.Sesion];
                    if (mAcuse == null) mAcuse = new MensajeAcuse();
                    mAcuse.RfcContribuyente = value.ToString().Trim();
                    Session[Parametros.Sesion] = mAcuse;
                }
            }
        }
        private string Cook_RfcAutenticacion
        {
            get
            {
                string result = "";

                if (Parametros.UsarCookies)
                {
                    if (Request.Cookies[Parametros.Sesion] != null && Request.Cookies[Parametros.Sesion]["RfcAutenticacion"] != null)
                        result = Request.Cookies[Parametros.Sesion]["RfcAutenticacion"].Trim();
                }
                else
                {
                    if (Session[Parametros.Sesion] != null) result = ((MensajeAcuse)Session[Parametros.Sesion]).RfcAutenticacion;
                }

                return result;
            }
            set
            {
                if (Parametros.UsarCookies)
                    Response.Cookies[Parametros.Sesion]["RfcAutenticacion"] = value.ToString().Trim();
                else
                {
                    MensajeAcuse mAcuse = (MensajeAcuse)Session[Parametros.Sesion];
                    if (mAcuse == null) mAcuse = new MensajeAcuse();
                    mAcuse.RfcAutenticacion = value.ToString().Trim();
                    Session[Parametros.Sesion] = mAcuse;
                }
            }
        }
        private string Cook_NombreArchivo
        {
            get
            {
                string result = "";

                if (Parametros.UsarCookies)
                {
                    if (Request.Cookies[Parametros.Sesion] != null && Request.Cookies[Parametros.Sesion]["NombreArchivo"] != null)
                        result = Request.Cookies[Parametros.Sesion]["NombreArchivo"].Trim();
                }
                else
                {
                    if (Session[Parametros.Sesion] != null) result = ((MensajeAcuse)Session[Parametros.Sesion]).NombreArchivo;
                }

                return result;
            }
            set
            {
                if (Parametros.UsarCookies)
                    Response.Cookies[Parametros.Sesion]["NombreArchivo"] = value.ToString().Trim();
                else
                {
                    MensajeAcuse mAcuse = (MensajeAcuse)Session[Parametros.Sesion];
                    if (mAcuse == null) mAcuse = new MensajeAcuse();
                    mAcuse.NombreArchivo = value.ToString().Trim();
                    Session[Parametros.Sesion] = mAcuse;
                }
            }
        }
        private int Cook_TamanoArchivo
        {
            get
            {
                int result = -1;

                if (Parametros.UsarCookies)
                {
                    if (Request.Cookies[Parametros.Sesion] != null && Request.Cookies[Parametros.Sesion]["TamanoArchivo"] != null)
                        result = int.Parse(Request.Cookies[Parametros.Sesion]["TamanoArchivo"].Trim());
                }
                else
                {
                    if (Session[Parametros.Sesion] != null) result = ((MensajeAcuse)Session[Parametros.Sesion]).TamañoArchivo;
                }

                return result;
            }
            set
            {
                if (Parametros.UsarCookies)
                    Response.Cookies[Parametros.Sesion]["TamanoArchivo"] = value.ToString().Trim();
                else
                {
                    MensajeAcuse mAcuse = (MensajeAcuse)Session[Parametros.Sesion];
                    if (mAcuse == null) mAcuse = new MensajeAcuse();
                    mAcuse.TamañoArchivo = value;
                    Session[Parametros.Sesion] = mAcuse;
                }
            }
        }
        private string Cook_FechaRecepcion
        {
            get
            {
                string result = "";

                if (Parametros.UsarCookies)
                {
                    if (Request.Cookies[Parametros.Sesion] != null && Request.Cookies[Parametros.Sesion]["FechaRecepcion"] != null)
                        result = Request.Cookies[Parametros.Sesion]["FechaRecepcion"];
                }
                else
                {
                    if (Session[Parametros.Sesion] != null) result = ((MensajeAcuse)Session[Parametros.Sesion]).FechaRecepcion;
                }

                return result;
            }
            set
            {
                if (Parametros.UsarCookies)
                    Response.Cookies[Parametros.Sesion]["FechaRecepcion"] = value.ToString().Trim();
                else
                {
                    MensajeAcuse mAcuse = (MensajeAcuse)Session[Parametros.Sesion];
                    if (mAcuse == null) mAcuse = new MensajeAcuse();
                    mAcuse.FechaRecepcion = value.ToString().Trim();
                    Session[Parametros.Sesion] = mAcuse;
                }
            }
        }
        private string Cook_HoraRecepcion
        {
            get
            {
                string result = "";

                if (Parametros.UsarCookies)
                {
                    if (Request.Cookies[Parametros.Sesion] != null && Request.Cookies[Parametros.Sesion]["HoraRecepcion"] != null)
                        result = Request.Cookies[Parametros.Sesion]["HoraRecepcion"].Trim();
                }
                else
                {
                    if (Session[Parametros.Sesion] != null) result = ((MensajeAcuse)Session[Parametros.Sesion]).HoraRecepcion;
                }

                return result;
            }
            set
            {
                if (Parametros.UsarCookies)
                    Response.Cookies[Parametros.Sesion]["HoraRecepcion"] = value.ToString().Trim();
                else
                {
                    MensajeAcuse mAcuse = (MensajeAcuse)Session[Parametros.Sesion];
                    if (mAcuse == null) mAcuse = new MensajeAcuse();
                    mAcuse.HoraRecepcion = value.ToString().Trim();
                    Session[Parametros.Sesion] = mAcuse;
                }
            }
        }
        private string Cook_Folio
        {
            get
            {
                string result = "";

                if (Parametros.UsarCookies)
                {
                    if (Request.Cookies[Parametros.Sesion] != null && Request.Cookies[Parametros.Sesion]["Folio"] != null)
                        result = Request.Cookies[Parametros.Sesion]["Folio"].Trim();
                }
                else
                {
                    if (Session[Parametros.Sesion] != null) result = ((MensajeAcuse)Session[Parametros.Sesion]).Folio;
                }

                return result;
            }
            set
            {
                if (Parametros.UsarCookies)
                    Response.Cookies[Parametros.Sesion]["Folio"] = value.ToString().Trim();
                else
                {
                    MensajeAcuse mAcuse = (MensajeAcuse)Session[Parametros.Sesion];
                    if (mAcuse == null) mAcuse = new MensajeAcuse();
                    mAcuse.Folio = value.ToString().Trim();
                    Session[Parametros.Sesion] = mAcuse;
                }
            }
        }
        private string Cook_Mensaje
        {
            get
            {
                string result = "";

                if (Parametros.UsarCookies)
                {
                    if (Request.Cookies[Parametros.Sesion] != null && Request.Cookies[Parametros.Sesion]["Mensaje"] != null)
                        result = Request.Cookies[Parametros.Sesion]["Mensaje"].Trim();
                }
                else
                {
                    if (Session[Parametros.Sesion] != null) result = ((MensajeAcuse)Session[Parametros.Sesion]).Mensaje;
                }

                return Server.UrlDecode(result);
            }
            set
            {
                if (Parametros.UsarCookies)
                    Response.Cookies[Parametros.Sesion]["Mensaje"] = value.ToString().Trim();
                else
                {
                    MensajeAcuse mAcuse = (MensajeAcuse)Session[Parametros.Sesion];
                    if (mAcuse == null) mAcuse = new MensajeAcuse();
                    mAcuse.Mensaje = Server.UrlEncode(value.ToString().Trim());
                    Session[Parametros.Sesion] = mAcuse;
                }
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            this.usuario = base.GetCurrentMembershipUser();

            if (!IsPostBack)
            {
                Utilerias.RegistrarLogEventosAuditoria(string.Format("\r\n Notificacion.Page_Load {0}: {1}/{2}. ", Parametros.Sesion, Session[Parametros.Sesion], Request.Cookies[Parametros.Sesion]));

                rfcLabel.Text = usuario.RFC;

                if (Parametros.MedioPresentacion == MedioPresentacion.Internet)
                {
                    enviarMasHyperLink.Visible = false;
                }

                satSesionLoginStatus.LoginText = "Inicio";
                satSesionLoginStatus.LogoutText = "Inicio";

                MensajeAcuse acuse = null;
                acuse = new MensajeAcuse();
                acuse.MedioPresentacion = Cook_MedioPresentacion;
                acuse.Estatus = Cook_Estatus;
                acuse.RfcContribuyente = Cook_RfcContribuyente;
                acuse.RfcAutenticacion = Cook_RfcAutenticacion;
                acuse.NombreArchivo = Cook_NombreArchivo;
                acuse.TamañoArchivo = Cook_TamanoArchivo;
                acuse.FechaRecepcion = Cook_FechaRecepcion;
                acuse.HoraRecepcion = Cook_HoraRecepcion;
                acuse.Folio = Cook_Folio;
                acuse.Mensaje = Cook_Mensaje;

                this.GenerarHtml(acuse);
            }
        }

        private void GenerarHtml(MensajeAcuse acuse)
        {
            HtmlFormaterXml.TransformSource = (TipoRespuesta)acuse.Estatus == TipoRespuesta.Aceptada ? Urls.DeclaracionAceptada : Urls.DeclaracionRechazada;
            HtmlFormaterXml.DocumentContent = acuse.ToString();
        }

        protected void enviarMasHyperLink_Click(object sender, EventArgs e)
        {
            Response.Cookies[Parametros.Sesion]["FechaRecepcion"] = null;
            Session[Parametros.Sesion] = null;
            Response.Redirect("~/RecepcionDeclaracion.aspx", true);
        }

        protected void satSesionLoginStatus_LoggedOut(object sender, EventArgs e)
        {
            Session.Abandon();
            HttpContext.Current.Request.Cookies.Clear();
            Response.Redirect(@"~\Login.aspx");
        }

    }
}