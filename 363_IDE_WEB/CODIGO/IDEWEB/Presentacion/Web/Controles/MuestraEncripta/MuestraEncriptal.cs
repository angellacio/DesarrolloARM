//@(#)SCADE2(W:SKDN09071PC4:Sat.Scade.Net.IDE.Presentacion.Web:LoginFiel:0:11/Febrero/2009[Sat.Scade.Net.IDE.Presentacion.Web:0:11/Febrero/2009]) 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Drawing.Design;
using System.Collections;
using System.IO;
using System.Security.Permissions;
using Sat.Scade.Net.IDE.Presentacion.Web;

[assembly: TagPrefix(Utility.Resource.fullType, Utility.Resource.Sat)]
[assembly: System.Web.UI.WebResource(Utility.Resource.RecursosFiel, Utility.mimeType.javascript)]
[assembly: System.Web.UI.WebResource(Utility.Resource.ImagenFiel, System.Net.Mime.MediaTypeNames.Image.Jpeg)]
namespace Sat.Scade.Net.IDE.Presentacion.Web
{
    [System.Drawing.ToolboxBitmap(typeof(resfinder), Utility.Resource.ImagenloginFielIcono)]
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal), 
    AspNetHostingPermission(SecurityAction.InheritanceDemand, Level=AspNetHostingPermissionLevel.Minimal),
    ToolboxData(Utility.ToolBoxData.MuestraEncripta)]
    public class MuestraEncripta : CompositeControl
    {
        public event CommandEventHandler OnChangeLoginType;
        private FontUnit sizeFont = new FontUnit(13, UnitType.Pixel);
        private static readonly object EventAuthenticate = new object();
        private static readonly object EventLoggedIn = new object();
        private static readonly object EventLoggingIn = new object();
        private static readonly object EventLoginError = new object();        
        private string backImageUrl = String.Empty;
        //private CheckBox CheckBoxUpdateMail;
        private HiddenField IsValidCertificateHiddenField;
        private LinkButton CiecLinkButton;
        private Label LabelFailure;
        private Button LoginButton;
        private Image FielImage;

        // R2. Declaramos el objeto mailUpdateLink y las propiedades del Link
        //private HyperLink mailUpdateLink;

        [Category(Utility.Category.EDSFiel), Description("Url Aplicacion Actualización de Correo(s)."), UrlProperty, Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string DestinationMailUpdateUrl
        {
            get { return (String)ViewState["DestinationMailUpdateUrl"] ?? String.Empty; }
            set { ViewState["DestinationMailUpdateUrl"] = value; }
        }


        [Category(Utility.Category.EDSFiel), DefaultValue(0), Description("Accion a realizar despues de que suceda un error en la autenticacion.")]
        public virtual LoginFailureAction FailureAction
        {
            get
            {
                object obj2 = this.ViewState["FailureAction"];
                if (obj2 != null)
                {
                    return (LoginFailureAction)obj2;
                }
                return LoginFailureAction.Refresh;
            }
            set
            {
                if ((value < LoginFailureAction.Refresh) || (value > LoginFailureAction.RedirectToLoginPage))
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                this.ViewState["FailureAction"] = value;
            }
        }

        [Category(Utility.Category.EDSApplet), Description("Ancho en pixeles del applet.")]
        public new string Width
        {
            get
            {
                string s = (string)ViewState["HeaderWidth"];
                return String.IsNullOrEmpty(s) ? "640" : s;
            }
            set
            {
                ViewState["HeaderWidth"] = value;
            }
        }

        [Category(Utility.Category.EDSApplet), Description("Alto en pixeles del login para FIEL.")]
        public new string Height
        {
            get
            {
                string s = (string)ViewState["HeaderHeight"];
                return String.IsNullOrEmpty(s) ? "250" : s;
            }
            set
            {
                ViewState["HeaderHeight"] = value;
            }
        }

        [Category(Utility.Category.EDSApplet), Description("Archivo donde se encuentra el applet de java.")]
        public string JavaArchive
        {
            get
            {
                string s = (string)ViewState["HeaderJavaArchive"];
                return String.IsNullOrEmpty(s) ? "Recursos/Applet/Internet/EncriptorIDE.jar" : s;
            }
            set
            {
                ViewState["HeaderJavaArchive"] = value;
            }
        }

        [Category(Utility.Category.EDSApplet), Description("Codio del applet de java.")]
        public string JavaCode
        {
            get
            {
                string s = (string)ViewState["HeaderJavaCode"];
                return String.IsNullOrEmpty(s) ? "Firma.class" : s;
            }
            set
            {
                ViewState["HeaderJavaCode"] = value;
            }
        }

        [Category(Utility.Category.EDSApplet), Description("Url donde se ejecutara el applet de java.")]
        public string UrlOriginal
        {
            get
            {
                string s = (string)ViewState["HeaderUrlOriginal"];
                return String.IsNullOrEmpty(s) ? "~/CargarArchivo.aspx".ToUpper() : s;
            }
            set
            {
                ViewState["HeaderUrlOriginal"] = value;
            }
        }

        [Category(Utility.Category.EDSApplet), Description("Pagina donde se encuentra el applet de java.")]
        public string AppPage
        {
            get
            {
                string s = (string)ViewState["HeaderAppPage"];
                return String.IsNullOrEmpty(s) ? "firma" : s;
            }
            set
            {
                ViewState["HeaderAppPage"] = value;
            }
        }

        [Category(Utility.Category.EDSApplet), Description("Plug in con el que se ejeuctara el applet de java")]
        public string JavaPlugIn
        {
            get
            {
                string s = (string)ViewState["HeaderJavaPlugIn"];
                return String.IsNullOrEmpty(s) ? "http://java.sun.com/products/plugin/1.3/jinstall-13-win32.cab#Version=1,3,0,mn" : s;
            }
            set
            {
                ViewState["HeaderJavaPlugIn"] = value;
            }
        }                       

        [Category(Utility.Category.EDSInformacion), Description("Rfc obtenido por la autenticación FIEL.") ,ReadOnly(true)]
        public string Rfc
        {
            get
            {
                if (this.Page.Request != null)
                    return Page.Request["RFC"];
                return String.Empty;
            }
        }

        [Category(Utility.Category.EDSInformacion), Description("Serie del certificado obtenido por la autenticación FIEL."), ReadOnly(true)]
        public string Serie
        {
            get
            {
                if (Page.Request != null)
                    return Page.Request["Serie"];
                return String.Empty;
            }
        }

        [Category(Utility.Category.EDSInformacion), Description("Firma obtenida por la autenticación FIEL."), ReadOnly(true)]
        public string Firma
        {
            get
            {
                if (Page.Request != null)
                    return Page.Request["Firma"];
                return String.Empty;
            }
        }

        [Category(Utility.Category.EDSInformacion), Description("Cadena Original obtenido por la autenticación FIEL."), ReadOnly(true)]
        public string CadenaO
        {
            get
            {
                if (Page.Request != null)
                    return Page.Request["CadenaO"];
                return String.Empty;
            }
        }

        [Category(Utility.Category.EDSInformacion), Description("Url Original obtenida por la autenticación FIEL."), ReadOnly(true)]
        public string URLoriginal
        {
            get
            {
                if (Page.Request != null)
                    return Page.Request["URLoriginal"];
                return String.Empty;
            }
        }                               

        [Category(Utility.Category.EDSFiel), Description("Texto que sera mostrado en el checkbox de actualizar correo electrónico.")]
        public string MailText
        {
            get
            {
                return (String)ViewState["MailTextFiel"] ?? Utility.Text.Mail;
            }
            set
            {
                ViewState["MailTextFiel"] = value;
            }
        }

        [Category(Utility.Category.EDSFiel), Description("Texto que sera mostrado en el boton de login.")]
        public string LoginText
        {
            get
            {
                return (String)ViewState["LoginTextFiel"] ?? Utility.Text.Continuar;
            }
            set
            {
                ViewState["LoginTextFiel"] = value;
            }
        }

        [Category(Utility.Category.EDSFiel), Description("Texto que sera en cuando ocurre un error en la autenticacion.")]
        public string FailureText
        {
            get
            {
                return (String)ViewState["FailureTextFiel"] ?? String.Empty;
            }
            set
            {
                ViewState["FailureTextFiel"] = value;
            }
        }

        

        [Category(Utility.Category.EDSFiel), Description("Indica si tiene que validar el certificado.")]
        public bool IsValidCertificate
        {
            get
            {
                bool flag = true;
                if (ViewState["IsValidCertificate"] != null)
                {
                    bool.TryParse((string)ViewState["IsValidCertificate"], out flag);
                }
                return flag;
            }

            set
            {
                ViewState["IsValidCertificate"] = value.ToString();
            }
        }

        

        

        [Category(Utility.Category.EDSFiel), Description("Url destino, despues de autenticar correctamente al usuario."), UrlProperty, Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string DestinationPageUrl
        {
            get
            {
                String s = (String)ViewState["DestinationPageUrl"];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState["DestinationPageUrl"] = value;
            }
        }

        

        [Category(Utility.Category.EDSFiel), Description("Codigo javascript que sera ejecutado en el navegador cliente.")]
        public string OnClientClick
        {
            get
            {
                string script = (string)ViewState["ClientScript"];
                return script == null ? string.Empty : script;
            }
            set { ViewState["ClientScript"] = value; }
        }
        
        [Category(Utility.Category.EDSFiel), Description("Proveedor de membresia ocupado para autenticar al usuario.")]
        public string MembershipProvider
        {
            get
            {
                String s = (String)ViewState[Utility.ViewSate.MembershipProviderLogin];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState[Utility.ViewSate.MembershipProviderLogin] = value;
            }
        }                                
        
        [Category(Utility.Category.EDSFiel), Description("Evento que sustituye la autenticacion por defualt(proveedor de membresia asignado).")]
        public event AuthenticateEventHandler Authenticate
        {
            add
            {
                base.Events.AddHandler(EventAuthenticate, value);
            }
            remove
            {
                base.Events.RemoveHandler(EventAuthenticate, value);
            }
        }

        [Category(Utility.Category.EDSFiel), Description("Evento que sucede despues de realizar la autenticacion de forma satisfactoria.")]
        public event EventHandler LoggedIn
        {
            add
            {
                base.Events.AddHandler(EventLoggedIn, value);
            }
            remove
            {
                base.Events.RemoveHandler(EventLoggedIn, value);
            }
        }

        [Category(Utility.Category.EDSFiel), Description("Evento que sucede justo antes de realizar la autenticacion.")]
        public event LoginCancelEventHandler LoggingIn
        {
            add
            {
                base.Events.AddHandler(EventLoggingIn, value);
            }
            remove
            {
                base.Events.RemoveHandler(EventLoggingIn, value);
            }
        }

        [Category(Utility.Category.EDSFiel), Description("Evento que sucede despues de realizar la autenticacion de forma fracasada.")]
        public event EventHandler LoginError
        {
            add
            {
                base.Events.AddHandler(EventLoginError, value);
            }
            remove
            {
                base.Events.RemoveHandler(EventLoginError, value);
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            string script = this.Page.ClientScript.GetWebResourceUrl(this.GetType(), Utility.Resource.RecursosFiel);
            this.Page.ClientScript.RegisterClientScriptInclude(Utility.Resource.WebControls, script);
        }
        
        protected override void CreateChildControls()
        {
            Controls.Clear();

            this.CiecLinkButton = new LinkButton();          
            this.LabelFailure = new Label();
            this.IsValidCertificateHiddenField = new HiddenField();
            this.LoginButton = new Button();
            this.FielImage = new Image();


            this.CiecLinkButton.ID = Utility.ID.CiecLinkButton;
            this.LabelFailure.ID = Utility.ID.FailureText;     
            this.IsValidCertificateHiddenField.ID = Utility.ID.IsValidCertificateHiddenField;
            this.LoginButton.ID = Utility.ID.Loggin;            
            this.FielImage.ID = Utility.ID.FielImage;

           


            this.Controls.Add(new LiteralControl(String.Format("<center>")));
            string info = string.Empty;

            if (!this.DesignMode)
            {
                info = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"];

                if (this.IsValidCertificate)
                {
                    this.Controls.Add(new LiteralControl("<table align=\"center\"><tr><td>"));
                    this.Controls.Add(new LiteralControl(String.Format("<input id='{0}' name='{1}' type='{2}' value='{3}' />",Utility.ID.RFC, Utility.ID.RFC, Utility.ID.HiddenType, Utility.Text.Required)));
                    this.Controls.Add(new LiteralControl(String.Format("<input id='{0}' name='{1}' type='{2}' value='{3}' />", Utility.ID.Firma, Utility.ID.Firma, Utility.ID.HiddenType, Utility.Text.Required)));
                    this.Controls.Add(new LiteralControl(String.Format("<input id='{0}' name='{1}' type='{2}' value='{3}' />", Utility.ID.Serie, Utility.ID.Serie, Utility.ID.HiddenType, Utility.Text.Required)));
                    this.Controls.Add(new LiteralControl(String.Format("<input id='{0}' name='{1}' type='{2}' value='{3}' />", Utility.ID.CadenaO, Utility.ID.CadenaO, Utility.ID.HiddenType, Utility.Text.Required)));
                    this.Controls.Add(new LiteralControl(String.Format("<input id='{0}' name='{1}' type='{2}' value='{3}' />", Utility.ID.UrlOriginal, Utility.ID.UrlOriginal, Utility.ID.HiddenType, Utility.Text.Required)));

                    if (info.IndexOf("MSIE") > 0 && info.IndexOf("Win") > 0 && info.IndexOf("Windows 3.1") <= 0 && info.IndexOf("Opera") <= 0)
                    {
                        this.Controls.Add(new LiteralControl(String.Format("<object id='{0}' name='{1}' classid='{2}' codebase='{3}' width='{4}' height='{5}'>", "AppFEA", "AppFEA", "clsid:8AD9C840-044E-11D1-B3E9-00805F499D93", this.JavaPlugIn, this.Width, this.Height)));
                        this.Controls.Add(new LiteralControl(String.Format("<param name='{0}' value='{1}'/>", "d", "AppFEA")));
                        this.Controls.Add(new LiteralControl(String.Format("<param name='{0}' value='{1}'/>", "name", "AppFEA")));
                        this.Controls.Add(new LiteralControl(String.Format("<param name='{0}' value='{1}'/>", "mayscript", "true")));
                        this.Controls.Add(new LiteralControl(String.Format("<param name='{0}' value='{1}'/>", "scriptable", "true")));
                        this.Controls.Add(new LiteralControl(String.Format("<param name='{0}' value='{1}'/>", "java_type", "application/x-java-applet;jpi-version=1.3")));
                        this.Controls.Add(new LiteralControl(String.Format("<param name='{0}' value='{1}'/>", "java_archive", this.JavaArchive)));
                        this.Controls.Add(new LiteralControl(String.Format("<param name='{0}' value='{1}'/>", "java_code", this.JavaCode)));
                        this.Controls.Add(new LiteralControl(String.Format("<param name='{0}' value='{1}'/>", "UrlOriginal", this.UrlOriginal)));
                        this.Controls.Add(new LiteralControl(String.Format("<param name='{0}' value='{1}'/>", "Pagina", this.AppPage)));
                        this.Controls.Add(new LiteralControl("<br/><b>[ Se requiere Java Plug-in 1.3.1_15 o superior para ejecutar el Applet ]</b><br/><a href='http://www.java.com/es/' target='_blank'>http://www.java.com/es/</a><br/>"));
                        this.Controls.Add(new LiteralControl(String.Format("</object>")));            
                    }
                    else if (info.IndexOf("Mozilla") > 0 || info.IndexOf("Opera") > 0)
                    {
                        this.Controls.Add(new LiteralControl(String.Format("<embed id='{0}' name='{1}' type='{2}' pluginspage='{3}' mayscript='{4}' scriptable='{5}' archive='{6}' code='{7}' width='{8}' height='{9}' UrlOriginal='{10}' Pagina='{11}'>", "AppFEA", "AppFEA", "application/x-java-applet", "http://www.java.com/es/", "true", "true", this.JavaArchive, this.JavaCode, this.Width, this.Height, this.UrlOriginal, this.AppPage)));
                        this.Controls.Add(new LiteralControl(String.Format("<noembed>")));
                        this.Controls.Add(new LiteralControl("<br/><b>[ Se requiere Java Plug-in 1.3.1_15 o superior para ejecutar el Applet ]</b><br/><a href='http://www.java.com/es/' target='_blank'>http://www.java.com/es/</a><br/>"));
                        this.Controls.Add(new LiteralControl(String.Format("</noembed>")));
                        this.Controls.Add(new LiteralControl(String.Format("</embed>")));                        
                    }
                    else
                    {
                        this.Controls.Add(new LiteralControl(String.Format("<applet id='APPLET1' name='AppFEA' MAYSCRIPT archive='{0}' code='{1}' width='{2}' height='{3}'>", this.JavaArchive, this.JavaCode, this.Width, this.Height)));
                        this.Controls.Add(new LiteralControl(String.Format("<param name='UrlOriginal' value='{0}' />", this.UrlOriginal)));
                        this.Controls.Add(new LiteralControl(String.Format("<param name='Pagina' value='{0}' />", this.AppPage)));
                        this.Controls.Add(new LiteralControl("<br/><b>[ Se requiere Java Plug-in 1.3.1_15 o superior para ejecutar el Applet ]</b><br/><a href='http://www.java.com/es/' target='_blank'>http://www.java.com/es/</a><br/>"));
                        this.Controls.Add(new LiteralControl(String.Format("</applet>")));            
                    }
                    this.Controls.Add(new LiteralControl("</td></tr></table>"));
                }
            }
            else
            {                
                FielImage = new Image();
                FielImage.ID = Utility.ID.FielImage;
                FielImage.ImageUrl = Page.ClientScript.GetWebResourceUrl(this.GetType(), Utility.Resource.ImagenFiel);
                Controls.Add(FielImage);
                this.Controls.Add(this.FielImage);
            }

            this.LabelFailure.ForeColor = System.Drawing.Color.Red;
            this.LabelFailure.Font.Name = Utility.Style.font_ArialSans;
            this.LabelFailure.Font.Size = sizeFont;
            this.LabelFailure.Width = new Unit(400);
            this.Controls.Add(this.LabelFailure);
            this.Controls.Add(new LiteralControl("<div id='divContent' style='visibility:hidden'>"));

            

            // R2. Definimos los Atributos de mailUpdateLink y lo Agregamos al Contenedor.            
            this.LoginButton.Font.Name = Utility.Style.font_ArialSans;
            this.LoginButton.Font.Size = sizeFont;
            this.LoginButton.Font.Bold = true;
            this.LoginButton.BackColor = System.Drawing.Color.FromArgb(212, 226, 239);
            this.LoginButton.BorderColor = System.Drawing.Color.FromArgb(122, 138, 153);
            this.LoginButton.BorderStyle = BorderStyle.Solid;
            this.LoginButton.Text = this.LoginText;
            this.LoginButton.CommandName = Utility.CommandName.LoginButtonCommandName;
            this.LoginButton.Attributes.Add("onclick", "javascript:return Validate();");            
            this.Controls.Add(this.LoginButton);
            this.Controls.Add(new LiteralControl(String.Format("</div>")));

            //MGFM
            this.Controls.Add(new LiteralControl(String.Format("<br/><br/>")));

            
            

            this.IsValidCertificateHiddenField.Value = this.IsValidCertificate.ToString();
            this.Controls.Add(this.IsValidCertificateHiddenField);
            this.Controls.Add(new LiteralControl(String.Format("</center>")));
            base.CreateChildControls();
        }
                
        private bool OnLoginPage()
        {
            return AuthenticationConfig.AccessingLoginPage(this.Context, FormsAuthentication.LoginUrl);
        }

        private bool RedirectedFromFailedLogin()
        {
            return ((!base.DesignMode && (this.Page != null)) && (!this.Page.IsPostBack && (this.Page.Request.QueryString["loginfailure"] != null)));
        }
        
        private void AttemptLogin()
        {
            if ((this.Page == null) || this.Page.IsValid)
            {
                LoginCancelEventArgs e = new LoginCancelEventArgs();
                this.OnLoggingIn(e);
                if (!e.Cancel)
                {
                    AuthenticateEventArgs args2 = new AuthenticateEventArgs();
                    this.OnAuthenticate(args2);
                    if (args2.Authenticated)
                    {
                        FormsAuthentication.SetAuthCookie(this.Rfc, false);
                        this.OnLoggedIn(EventArgs.Empty);
                        //this.Page.Response.Redirect(this.GetRedirectUrl(), false);
                    }
                    else
                    {
                        this.OnLoginError(EventArgs.Empty);
                        if (this.FailureAction == LoginFailureAction.RedirectToLoginPage)
                        {
                            FormsAuthentication.RedirectToLoginPage("loginfailure=1");
                        }
                        ITextControl failureTextLabel = (ITextControl)this.FindControl(Utility.ID.FailureText);
                        if (failureTextLabel != null)
                        {
                            failureTextLabel.Text = this.GetProvider(this.MembershipProvider).PasswordStrengthRegularExpression;//this.FailureText;
                        }
                    }
                }
            }
        }

        

        private void AuthenticateUsingMembershipProvider(AuthenticateEventArgs e)
        {
            e.Authenticated = this.GetProvider(this.MembershipProvider).ValidateUser(this.Rfc, this.Serie);
        }

        protected void CiecLinkButton_Command(object sender, CommandEventArgs e)
        {
            if (this.OnChangeLoginType != null)
            {
                this.OnChangeLoginType(this, e);
            }
        }

        protected virtual void OnAuthenticate(AuthenticateEventArgs e)
        {
            AuthenticateEventHandler handler = (AuthenticateEventHandler)base.Events[EventAuthenticate];
            if (handler != null)
            {
                handler(this, e);
            }
            else
            {
                this.AuthenticateUsingMembershipProvider(e);
            }
        }

        protected override bool OnBubbleEvent(object source, EventArgs e)
        {
            bool flag = false;
            if (e is CommandEventArgs)
            {
                CommandEventArgs args = (CommandEventArgs)e;
                if (string.Equals(args.CommandName, Utility.CommandName.LoginButtonCommandName, StringComparison.OrdinalIgnoreCase))
                {
                    this.AttemptLogin();
                    flag = true;
                }
            }
            return flag;
        }

        protected virtual void OnLoggedIn(EventArgs e)
        {
            EventHandler handler = (EventHandler)base.Events[EventLoggedIn];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnLoggingIn(LoginCancelEventArgs e)
        {
            LoginCancelEventHandler handler = (LoginCancelEventHandler)base.Events[EventLoggingIn];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnLoginError(EventArgs e)
        {
            EventHandler handler = (EventHandler)base.Events[EventLoginError];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        internal MembershipProvider GetProvider(string providerName)
        {
            if (string.IsNullOrEmpty(providerName))
            {
                return Membership.Provider;
            }
            MembershipProvider provider = Membership.Providers[providerName];
            if (provider == null)
            {
                throw new HttpException(Utility.Message.ProveedorNoEncontrado);
            }
            return provider;
        }
    }
}