//@(#)SCADE2(W:SKDN09071PC4:Sat.Scade.Net.IDE.Presentacion.Web:LoginScade:0:11/Febrero/2009[Sat.Scade.Net.IDE.Presentacion.Web:0:11/Febrero/2009]) 
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Web;
using System.Security.Permissions;
using System.Web.UI;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.Security;
using System.Configuration;

namespace Sat.Scade.Net.IDE.Presentacion.Web
{
    [System.Drawing.ToolboxBitmap(typeof(resfinder), Utility.Resource.ImagenloginScadeIcono)]
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal),
    AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal),
    ToolboxData(Utility.ToolBoxData.Encripta)]
    public class Encripta : CompositeControl
    {
        private const string PREFIEL_MODE = "PreFiel";
        [Category(Utility.Category.EDSScade)]
        public event CommandEventHandler CambiandoModo;

        //// R2.Agregamos el Servicio de la Url
        //[Bindable(true), Category(Utility.Category.EDSScade), UrlProperty, Description("Url aplicación de Actualización de Correo(s)"), Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]

        //public string UrlPaginaDestinoUpdateMail
        //{
        //    get { return (String)ViewState["UpdateMailUrl"] ?? string.Empty; }
        //    set { ViewState["UpdateMailUrl"] = value; }
        //}

        [Category(Utility.Category.EDSScade)]
        public LoginType Modo
        {
            get
            {
                if (ViewState["Scade_Mode"] != null && !String.IsNullOrEmpty(ViewState["Scade_Mode"].ToString()))
                    return (LoginType)((int)ViewState["Scade_Mode"]);
                return LoginType.Ciec;
            }
            set
            {
                ViewState["Scade_Mode"] = value;
            }
        }

        [Category(Utility.Category.EDSScade)]
        public MedioPresentacion MedioPresentacion
        {
            get
            {
                if (ViewState["Scade_MedioPresentacion"] != null && !String.IsNullOrEmpty(ViewState["Scade_MedioPresentacion"].ToString()))
                    return (MedioPresentacion)((int)ViewState["Scade_MedioPresentacion"]);
                return MedioPresentacion.Internet;
            }
            set
            {
                ViewState["Scade_MedioPresentacion"] = value;
            }
        }

        [Category(Utility.Category.EDSScade)]
        public ScadeTheme Tema
        {
            get
            {
                ScadeTheme tipo = ScadeTheme.Default;
                if (ViewState["Scade_Tema"] != null && !String.IsNullOrEmpty(ViewState["Scade_Tema"].ToString()))
                {
                    tipo = (ScadeTheme)((int)ViewState["Scade_Tema"]);
                }
                return tipo;
            }
            set
            {
                ViewState["Scade_Tema"] = value;
            }
        }


        private EjecutaEncripta ejeEncrip;
        [Category(Utility.Category.EDSCiec)]
        public event AuthenticateEventHandler AuthenticateCiec;
        [Category(Utility.Category.EDSCiec)]
        public event EventHandler LoggedInCiec;
        [Category(Utility.Category.EDSCiec)]
        public event LoginCancelEventHandler LoggingInCiec;
        [Category(Utility.Category.EDSCiec)]
        public event EventHandler LoginErrorCiec;

        //public TableItemStyle CheckBoxStyle
        //{
        //    get
        //    {
        //        TableItemStyle style = null;
        //        if (ViewState["CheckBoxStyle_Ciec"] != null && !String.IsNullOrEmpty(ViewState["CheckBoxStyle_Ciec"].ToString()))
        //        {
        //            style = (TableItemStyle)ViewState["CheckBoxStyle_Ciec"];
        //        }
        //        return style;
        //    }
        //    set
        //    {
        //        ViewState["CheckBoxStyle_Ciec"] = value;
        //    }
        //}

        //public TableItemStyle FailureTextStyle
        //{
        //    get
        //    {
        //        TableItemStyle style = null;
        //        if (ViewState["FailureTextStyle_Ciec"] != null && !String.IsNullOrEmpty(ViewState["FailureTextStyle_Ciec"].ToString()))
        //        {
        //            style = (TableItemStyle)ViewState["FailureTextStyle_Ciec"];
        //        }
        //        return style;
        //    }
        //    set
        //    {
        //        ViewState["FailureTextStyle_Ciec"] = value;
        //    }
        //}

        public TableItemStyle HyperLinkStyle
        {
            get
            {
                TableItemStyle style = null;
                if (ViewState["HyperLinkStyle_Ciec"] != null && !String.IsNullOrEmpty(ViewState["HyperLinkStyle_Ciec"].ToString()))
                {
                    style = (TableItemStyle)ViewState["HyperLinkStyle_Ciec"];
                }
                return style;
            }
            set
            {
                ViewState["HyperLinkStyle_Ciec"] = value;
            }
        }

        public TableItemStyle InstructionTextStyle
        {
            get
            {
                TableItemStyle style = null;
                if (ViewState["InstructionTextStyle_Ciec"] != null && !String.IsNullOrEmpty(ViewState["InstructionTextStyle_Ciec"].ToString()))
                {
                    style = (TableItemStyle)ViewState["InstructionTextStyle_Ciec"];
                }
                return style;
            }
            set
            {
                ViewState["InstructionTextStyle_Ciec"] = value;
            }
        }

        public TableItemStyle LabelStyle
        {
            get
            {
                TableItemStyle style = null;
                if (ViewState["LabelStyle_Ciec"] != null && !String.IsNullOrEmpty(ViewState["LabelStyle_Ciec"].ToString()))
                {
                    style = (TableItemStyle)ViewState["LabelStyle_Ciec"];
                }
                return style;
            }
            set
            {
                ViewState["LabelStyle_Ciec"] = value;
            }
        }

        public Style LoginButtonStyle
        {
            get
            {
                Style style = null;
                if (ViewState["LoginButtonStyle_Ciec"] != null && !String.IsNullOrEmpty(ViewState["LoginButtonStyle_Ciec"].ToString()))
                {
                    style = (Style)ViewState["LoginButtonStyle_Ciec"];
                }
                return style;
            }
            set
            {
                ViewState["LoginButtonStyle_Ciec"] = value;
            }
        }

        public Style TextBoxStyle
        {
            get
            {
                Style style = null;
                if (ViewState["TextBoxStyle_Ciec"] != null && !String.IsNullOrEmpty(ViewState["TextBoxStyle_Ciec"].ToString()))
                {
                    style = (Style)ViewState["TextBoxStyle_Ciec"];
                }
                return style;
            }
            set
            {
                ViewState["TextBoxStyle_Ciec"] = value;
            }
        }

        public TableItemStyle TitleTextStyle
        {
            get
            {
                TableItemStyle style = null;
                if (ViewState["TitleTextStyle_Ciec"] != null && !String.IsNullOrEmpty(ViewState["TitleTextStyle_Ciec"].ToString()))
                {
                    style = (TableItemStyle)ViewState["TitleTextStyle_Ciec"];
                }
                return style;
            }
            set
            {
                ViewState["TitleTextStyle_Ciec"] = value;
            }
        }

        public Style ValidatorTextStyle
        {
            get
            {
                Style style = null;
                if (ViewState["ValidatorTextStyle_Ciec"] != null && !String.IsNullOrEmpty(ViewState["ValidatorTextStyle_Ciec"].ToString()))
                {
                    style = (Style)ViewState["ValidatorTextStyle_Ciec"];
                }
                return style;
            }
            set
            {
                ViewState["ValidatorTextStyle_Ciec"] = value;
            }
        }

        [Category(Utility.Category.EDSCiec), ReadOnly(true)]
        public string NombreUsuario
        {
            get
            {
                if (this.ejeEncrip != null)
                {
                    return this.ejeEncrip.UserName;
                }
                return String.Empty;
            }
        }

        [Category(Utility.Category.EDSCiec)]
        public string LoginTitulo
        {
            get
            {
                return (String)ViewState["Scade_LoginTitulo"] ?? String.Empty;
            }
            set
            {
                ViewState["Scade_LoginTitulo"] = value;
            }
        }

        [Category(Utility.Category.EDSCiec)]
        public string NombreUsuarioEtiqueta
        {
            get
            {
                return (string)ViewState["Scade_NombreUsuarioEtiqueta"] ?? String.Empty;
            }
            set
            {
                ViewState["Scade_NombreUsuarioEtiqueta"] = value;
            }
        }

        [Category(Utility.Category.EDSCiec)]
        public string ContraseñaEtiqueta
        {
            get
            {
                return (string)ViewState["Scade_ContraseñaEtiqueta"] ?? String.Empty;
            }
            set
            {
                ViewState["Scade_ContraseñaEtiqueta"] = value;
            }
        }

        [Category(Utility.Category.EDSCiec)]
        public string ErrorNombreUsuarioRequerido
        {
            get
            {
                return (string)ViewState["Scade_ErrorNombreUsuarioRequerido"] ?? String.Empty;
            }
            set
            {
                ViewState["Scade_ErrorNombreUsuarioRequerido"] = value;
            }
        }

        [Category(Utility.Category.EDSCiec)]
        public string ErrorContraseñaRequerida
        {
            get
            {
                return (string)ViewState["Scade_ErrorContraseñaRequerida"] ?? String.Empty;
            }
            set
            {
                ViewState["Scade_ErrorContraseñaRequerida"] = value;
            }
        }

        [Category(Utility.Category.EDSCiec)]
        public bool VisualizarMailCiec
        {
            get
            {
                object obj = ViewState["Scade_VisualizarMailCiec"];
                if (obj != null)
                {
                    return (bool)obj;
                }
                return true;
            }
            set
            {
                ViewState["Scade_VisualizarMailCiec"] = value;
            }
        }

        [Category(Utility.Category.EDSCiec)]
        public string MailEtiquetaCiec
        {
            get
            {
                return (String)ViewState["Scade_MailEtiquetaCiec"] ?? String.Empty;
            }
            set
            {
                ViewState["Scade_MailEtiquetaCiec"] = value;
            }
        }

        

        [Category(Utility.Category.EDSCiec)]
        public string BotonLoginEtiquetaCiec
        {
            get
            {
                return (String)ViewState["Scade_BotonLoginEtiquetaCiec"] ?? String.Empty;
            }
            set
            {
                ViewState["Scade_BotonLoginEtiquetaCiec"] = value;
            }
        }

        [Bindable(true), Category(Utility.Category.EDSCiec)]
        public string FielTitulo
        {
            get
            {
                return (String)ViewState["Scade_FielTitulo"] ?? String.Empty;
            }
            set
            {
                ViewState["Scade_FielTitulo"] = value;
            }
        }

        [Category(Utility.Category.EDSCiec)]
        public string FielEtiqueta
        {
            get
            {
                return (String)ViewState["Scade_FielEtiqueta"] ?? String.Empty;
            }
            set
            {
                ViewState["Scade_FielEtiqueta"] = value;
            }
        }

        [Category(Utility.Category.EDSCiec), UrlProperty, Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string CiecFortalecidaUrl
        {
            get
            {
                return (String)ViewState["Scade_CiecFortalecidaUrl"] ?? String.Empty;
            }
            set
            {
                ViewState["Scade_CiecFortalecidaUrl"] = value;
            }
        }

        [Category(Utility.Category.EDSCiec)]
        public string CiecFortalecidaEtiqueta
        {
            get
            {
                return (String)ViewState["Scade_CiecFortalecidaEtiqueta"] ?? String.Empty;
            }
            set
            {
                ViewState["Scade_CiecFortalecidaEtiqueta"] = value;
            }
        }

        [Category(Utility.Category.EDSCiec)]
        public string ErrorAutenticacionCiec
        {
            get
            {
                return (String)ViewState["Scade_ErrorAutenticacionCiec"] ?? String.Empty;
            }
            set
            {
                ViewState["Scade_ErrorAutenticacionCiec"] = value;
            }
        }

        [Bindable(true), Category(Utility.Category.EDSCiec)]
        public string MembershipProviderCiec
        {
            get
            {
                return (String)ViewState["Scade_MembershipProviderCiec"] ?? String.Empty;
            }
            set
            {
                ViewState["Scade_MembershipProviderCiec"] = value;
            }
        }

        [Bindable(true), Category(Utility.Category.EDSCiec), UrlProperty, Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string UrlPaginaDestinoCiec
        {
            get
            {
                return (String)ViewState["Scade_UrlPaginaDestinoCiec"] ?? String.Empty;
            }
            set
            {
                ViewState["Scade_UrlPaginaDestinoCiec"] = value;
            }
        }

        [Bindable(true), Category(Utility.Category.EDSCiec), UrlProperty, Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string UrlPaginaCorreoCiec
        {
            get
            {
                return (String)ViewState["Scade_UrlPaginaCorreoCiec"] ?? String.Empty;
            }
            set
            {
                ViewState["Scade_UrlPaginaCorreoCiec"] = value;
            }
        }


        private MuestraEncripta muesEncrip;
        [Category(Utility.Category.EDSFiel)]
        public event AuthenticateEventHandler AuthenticateFiel;
        [Category(Utility.Category.EDSFiel)]
        public event EventHandler LoggedInFiel;
        [Category(Utility.Category.EDSFiel)]
        public event LoginCancelEventHandler LoggingInFiel;
        [Category(Utility.Category.EDSFiel)]
        public event EventHandler LoginErrorFiel;



        [Bindable(true), Category(Utility.Category.EDSFiel)]
        public string AnchoApplet
        {
            get
            {
                return (String)ViewState["Scade_AnchoApplet"] ?? String.Empty;
            }
            set
            {
                ViewState["Scade_AnchoApplet"] = value;
            }
        }

        [Bindable(true), Category(Utility.Category.EDSFiel)]
        public string AltoApplet
        {
            get
            {
                return (String)ViewState["Scade_AltoApplet"] ?? String.Empty;
            }
            set
            {
                ViewState["Scade_AltoApplet"] = value;
            }
        }

        [Bindable(true), Category(Utility.Category.EDSFiel)]
        public string JavaArchive
        {
            get
            {
                return (String)ViewState["Scade_JavaArchive"] ?? String.Empty;
            }
            set
            {
                ViewState["Scade_JavaArchive"] = value;
            }
        }

        [Bindable(true), Category(Utility.Category.EDSFiel)]
        public string JavaCode
        {
            get
            {
                return (String)ViewState["Scade_JavaCode"] ?? String.Empty;
            }
            set
            {
                ViewState["Scade_JavaCode"] = value;
            }
        }

        [Bindable(true), Category(Utility.Category.EDSFiel), UrlProperty, Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string UrlOriginal
        {
            get
            {
                return (String)ViewState["Scade_UrlOriginal"] ?? String.Empty;
            }
            set
            {
                ViewState["Scade_UrlOriginal"] = value;
            }
        }

        [Bindable(true), Category(Utility.Category.EDSFiel)]
        public string AppPage
        {
            get
            {
                return (String)ViewState["Scade_AppPage"] ?? String.Empty;
            }
            set
            {
                ViewState["Scade_AppPage"] = value;
            }
        }

        [Bindable(true), Category(Utility.Category.EDSFiel)]
        public string JavaPlugIn
        {
            get
            {
                return (String)ViewState["Scade_JavaPlugIn"] ?? String.Empty;
            }
            set
            {
                ViewState["Scade_JavaPlugIn"] = value;
            }
        }

        [Category(Utility.Category.EDSFiel), ReadOnly(true)]
        public string Rfc
        {
            get
            {
                return this.muesEncrip.Rfc;
            }
        }

        [Category(Utility.Category.EDSFiel), ReadOnly(true)]
        public string Serie
        {
            get
            {
                return this.muesEncrip.Serie;
            }
        }

        [Category(Utility.Category.EDSFiel), ReadOnly(true)]
        public string Firma
        {
            get
            {
                return this.muesEncrip.Firma;
            }
        }

        [Category(Utility.Category.EDSFiel), ReadOnly(true)]
        public string CadenaOriginal
        {
            get
            {
                return this.muesEncrip.CadenaO;
            }
        }

        [Category(Utility.Category.EDSFiel), ReadOnly(true)]
        public string URLOriginal
        {
            get
            {
                return this.muesEncrip.URLoriginal;
            }
        }

        [Bindable(true), Category(Utility.Category.EDSFiel)]
        public bool VisualizarMailFiel
        {
            get
            {
                object obj = ViewState["Scade_VisualizarMailFiel"];
                if (obj != null)
                {
                    return (bool)obj;
                }
                return true;
            }
            set
            {
                ViewState["Scade_VisualizarMailFiel"] = value;
            }
        }

        [Bindable(true), Category(Utility.Category.EDSFiel)]
        public string MailEtiquetaFiel
        {
            get
            {
                return (String)ViewState["Scade_MailEtiquetaFiel"] ?? String.Empty;
            }
            set
            {
                ViewState["Scade_MailEtiquetaFiel"] = value;
            }
        }

        //[Bindable(true), Category(Utility.Category.EDSFiel), ReadOnly(true)]
        //public bool ActualizarMailFiel
        //{
        //    get
        //    {
        //        if (this.muesEncrip != null)
        //        {
        //            return this.muesEncrip.MailChecked;
        //        }
        //        return false;
        //    }
        //}

        [Bindable(true), Category(Utility.Category.EDSFiel)]
        public string BotonLoginEtiquetaFiel
        {
            get
            {
                return (String)ViewState["Scade_BotonLoginEtiquetaFiel"] ?? String.Empty;
            }
            set
            {
                ViewState["Scade_BotonLoginEtiquetaFiel"] = value;
            }
        }

        [Bindable(true), Category(Utility.Category.EDSFiel)]
        public string ErrorAutenticacionFiel
        {
            get
            {
                return (String)ViewState["Scade_ErrorAutenticacionFiel"] ?? String.Empty;
            }
            set
            {
                ViewState["Scade_ErrorAutenticacionFiel"] = value;
            }
        }

        [Bindable(true), Category(Utility.Category.EDSFiel)]
        public string MembershipProviderFiel
        {
            get
            {
                return (String)ViewState["Scade_MembershipProviderFiel"] ?? String.Empty;
            }
            set
            {
                ViewState["Scade_MembershipProviderFiel"] = value;
            }
        }

        [Bindable(true), Category(Utility.Category.EDSFiel), UrlProperty, Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string UrlPaginaDestinoFiel
        {
            get
            {
                return (String)ViewState["Scade_UrlPaginaDestinoFiel"] ?? String.Empty;
            }
            set
            {
                ViewState["Scade_UrlPaginaDestinoFiel"] = value;
            }
        }

        [Bindable(true), Category(Utility.Category.EDSFiel), UrlProperty, Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string UrlPaginaCorreoFiel
        {
            get
            {
                return (String)ViewState["Scade_UrlPaginaCorreoFiel"] ?? String.Empty;
            }
            set
            {
                ViewState["Scade_UrlPaginaCorreoFiel"] = value;
            }
        }

        [Bindable(true), Category(Utility.Category.EDSFiel)]
        public bool VisualizarCiec
        {
            get
            {
                object obj = ViewState["Scade_VisualizarCiec"];
                if (obj != null)
                {
                    return (bool)obj;
                }
                return true;
            }
            set
            {
                ViewState["Scade_VisualizarCiec"] = value;
            }
        }

        [Bindable(true), Category(Utility.Category.EDSFiel)]
        public string CiecEtiqueta
        {
            get
            {
                return (String)ViewState["Scade_CiecEtiqueta"] ?? String.Empty;
            }
            set
            {
                ViewState["Scade_CiecEtiqueta"] = value;
            }
        }

        [Bindable(true), Category(Utility.Category.EDSFiel)]
        public bool IsValidCertificate
        {
            get
            {
                object obj = ViewState["Scade_IsValidCertificate"];
                if (obj != null)
                {
                    return (bool)obj;
                }
                return true;
            }
            set
            {
                ViewState["Scade_IsValidCertificate"] = value;
            }
        }

        [Category(Utility.Category.EDSFiel)]
        public bool TieneFiel
        {
            get
            {
                object obj = ViewState["Scade_TieneFiel"];
                if (obj != null)
                {
                    return (bool)obj;
                }
                return false;
            }
            set
            {
                ViewState["Scade_TieneFiel"] = value;
            }
        }

        [Bindable(true), Category(Utility.Category.EDSFiel)]
        public string MensajeForzarFiel
        {
            get
            {
                return (String)ViewState["Scade_MensajeForzarFiel"] ?? "Estimado contribuyente, usted ya cuenta con Firma Electrónica Avanzada (FIEL), por lo que se requiere que se autentifique con esta modalidad.";
            }
            set
            {
                ViewState["Scade_MensajeForzarFiel"] = value;
            }
        }

        protected override void CreateChildControls()
        {
            this.Controls.Clear();


            if (!this.DesignMode && !Page.IsPostBack && !String.IsNullOrEmpty(Page.Request.QueryString["mode"]) && Page.Request.QueryString["mode"].Equals(PREFIEL_MODE))
            {
                this.TieneFiel = false;

                this.Modo = Sat.Scade.Net.IDE.Presentacion.Web.LoginType.Fiel;
                ShowErrorMessage(this.MensajeForzarFiel);
            }

            this.muesEncrip = new MuestraEncripta();
            this.muesEncrip.OnChangeLoginType += new CommandEventHandler(Scade_OnChangeLoginType);

            if (this.LoggingInFiel != null)
            {
                this.muesEncrip.LoggingIn += new LoginCancelEventHandler(fiel_LoggingIn);
            }

            if (this.LoggedInFiel != null)
            {
                this.muesEncrip.LoggedIn += new EventHandler(fiel_LoggedIn);
            }

            if (this.LoginErrorFiel != null)
            {
                this.muesEncrip.LoginError += new EventHandler(fiel_LoginError);
            }

            if (this.AuthenticateFiel != null)
            {
                this.muesEncrip.Authenticate += new AuthenticateEventHandler(fiel_Authenticate);
            }

            this.muesEncrip.Width = this.AnchoApplet;
            this.muesEncrip.Height = this.AltoApplet;
            this.muesEncrip.JavaArchive = "EncriptorIDE.jar";
            //this.muesEncrip.JavaArchive = this.JavaArchive;
            this.muesEncrip.JavaCode = this.JavaCode;
            this.muesEncrip.UrlOriginal = this.UrlOriginal;
            this.muesEncrip.AppPage = this.AppPage;
            this.muesEncrip.JavaPlugIn = this.JavaPlugIn;

            //this.muesEncrip.MailVisible = this.VisualizarMailFiel;

            this.muesEncrip.MailText = this.MailEtiquetaFiel;
            if (this.MailEtiquetaFiel.Length == 0)
            {
                this.muesEncrip.MailText = null;
            }
            //this.muesEncrip.MailPageUrl = this.UrlPaginaCorreoFiel;

            //this.muesEncrip.CiecText = this.CiecEtiqueta;
            //if (this.CiecEtiqueta.Length == 0)
            //{
            //    this.muesEncrip.CiecText = null;
            //}

            //this.muesEncrip.CiecVisible = this.VisualizarCiec;

            this.muesEncrip.LoginText = this.BotonLoginEtiquetaFiel;
            if (this.BotonLoginEtiquetaFiel.Length == 0)
            {
                this.muesEncrip.LoginText = null;
            }

            this.muesEncrip.FailureText = this.ErrorAutenticacionFiel;
            this.muesEncrip.MembershipProvider = this.MembershipProviderFiel;
            this.muesEncrip.DestinationPageUrl = this.UrlPaginaDestinoFiel;
            this.muesEncrip.IsValidCertificate = this.IsValidCertificate;

            // R2. Agregamos la Url a la Fiel
            //this.muesEncrip.DestinationMailUpdateUrl = this.UrlPaginaDestinoUpdateMail;

            this.Controls.Add(this.muesEncrip);
            
            this.ejeEncrip = new EjecutaEncripta();
            this.ejeEncrip.OnChangeLoginType += new CommandEventHandler(Scade_OnChangeLoginType);

            //if (this.LoggingInCiec != null)
            //{
            //    this.ciec.LoggingIn += new LoginCancelEventHandler(ciec_LoggingIn);
            //}

            //if (this.LoggedInCiec != null)
            //{
            //    this.ciec.LoggedIn += new EventHandler(ciec_LoggedIn);
            //}

            //if (this.LoginErrorCiec != null)
            //{
            //    this.ciec.LoginError += new EventHandler(ciec_LoginError);
            //}

            //if (this.AuthenticateCiec != null)
            //{
            //    this.ciec.Authenticate += new AuthenticateEventHandler(ciec_Authenticate);
            //}

            //this.ciec.Theme = this.Tema;
            //this.ciec.MedioPresentacion = this.MedioPresentacion;
            ////this.ciec.Titulo = this.LoginTitulo;
            ////this.ciec.UserNameLabel = this.NombreUsuarioEtiqueta;
            ////this.ciec.PasswordLabel = this.ContraseñaEtiqueta;
            ////this.ciec.UserNameRequiredErrorMessage = this.ErrorNombreUsuarioRequerido;
            ////this.ciec.PasswordRequiredErrorMessage = this.ErrorContraseñaRequerida;
            ////this.ciec.DisplayMail = this.VisualizarMailCiec;
            ////this.ciec.MailText = this.MailEtiquetaCiec;
            this.ejeEncrip.ButtonText = this.BotonLoginEtiquetaCiec;
            this.ejeEncrip.FielTitle = this.FielTitulo;
            this.ejeEncrip.FielTextUrl = this.FielEtiqueta;
            //this.ciec.CiecFTextUrl = this.CiecFortalecidaEtiqueta;
            //this.ciec.CiecFNavigateUrl = this.CiecFortalecidaUrl;
            //this.ciec.MembershipProvider = this.MembershipProviderCiec;
            //this.ciec.DestinationPageUrl = this.UrlPaginaDestinoCiec;
            //this.ciec.FailureAction = LoginFailureAction.RedirectToLoginPage;

            //// R2. Agregamos la Url a la CIEC
            //this.ciec.DestinationMailUpdateUrl = this.UrlPaginaDestinoUpdateMail;

            this.Controls.Add(this.ejeEncrip);
            
            this.muesEncrip.Visible = this.Modo == LoginType.Fiel;
            this.ejeEncrip.Visible = this.Modo == LoginType.Ciec;
        }

        void ciec_Authenticate(object sender, AuthenticateEventArgs e)
        {
            if (this.AuthenticateCiec != null)
            {
                this.AuthenticateCiec(this, e);
            }
        }

        void fiel_Authenticate(object sender, AuthenticateEventArgs e)
        {
            if (this.AuthenticateFiel != null)
            {
                this.AuthenticateFiel(this, e);
            }
        }

        protected void fiel_LoggingIn(object sender, LoginCancelEventArgs e)
        {
            if (this.LoggingInFiel != null)
            {
                this.LoggingInFiel(this, e);
            }
        }

        protected void fiel_LoggedIn(object sender, EventArgs e)
        {
            if (this.LoggedInFiel != null)
            {
                this.LoggedInFiel(this, e);
            }
        }

        protected void fiel_LoginError(object sender, EventArgs e)
        {
            if (this.LoginErrorFiel != null)
            {
                this.LoginErrorFiel(this, e);
            }
        }

        protected void ciec_LoggingIn(object sender, LoginCancelEventArgs e)
        {
            if (this.LoggingInCiec != null)
            {
                this.LoggingInCiec(this, e);
            }
        }

        protected void ciec_LoggedIn(object sender, EventArgs e)
        {
            if (this.LoggedInCiec != null)
            {
                this.LoggedInCiec(this, e);
            }

            //if (this.TieneFiel)
            //{
            //    FormsAuthentication.SignOut();
            //    Page.Response.Clear();
            //    Page.Response.StatusCode = 200;
            //    System.Web.Security.FormsAuthentication.RedirectToLoginPage(String.Format("mode={0}", PREFIEL_MODE));
            //    Page.Response.End();
            //}
            //else
            //{
            //    if (this.ActualizarMailCiec)
            //    {
            //        Page.Response.Redirect(this.UrlPaginaCorreoCiec);
            //    }
            //}
        }

        protected void ciec_LoginError(object sender, EventArgs e)
        {
            if (this.LoginErrorCiec != null)
            {
                this.LoginErrorCiec(this, e);
            }
        }

        protected void Scade_OnChangeLoginType(object sender, CommandEventArgs e)
        {
            if (e.CommandName == Utility.CommandName.LoginTypeCiec)
            {
                this.Modo = LoginType.Fiel;
            }
            if (e.CommandName == Utility.CommandName.LoginTypeFiel)
            {
                this.Modo = LoginType.Ciec;
            }
            if (CambiandoModo != null)
            {
                this.CambiandoModo(this, e);
            }
            this.CreateChildControls();
        }

        protected void ShowErrorMessage(string message)
        {
            string script = String.Format("alert('{0}');", message.Replace("'", "''"));
            Page.ClientScript.RegisterStartupScript(typeof(string), "ScriptMessageError", script, true);
        }
    }
}
