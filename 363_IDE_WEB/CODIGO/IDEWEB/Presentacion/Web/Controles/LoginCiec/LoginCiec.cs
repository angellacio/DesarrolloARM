//@(#)SCADE2(W:SKDN09071PC4:Sat.Scade.Net.IDE.Presentacion.Web:LoginCiec:0:11/Febrero/2009[Sat.Scade.Net.IDE.Presentacion.Web:0:11/Febrero/2009]) 
using System;
using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Design;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sat.Scade.Net.IDE.Presentacion.Web;
using System.Web.Security;

[assembly: System.Web.UI.WebResource(Utility.Resource.ImagenNewLogin,Utility.mimeType.image)]
[assembly: System.Web.UI.WebResource(Utility.Resource.RecursosFiel, Utility.mimeType.javascript)]
namespace Sat.Scade.Net.IDE.Presentacion.Web
{
    [System.Drawing.ToolboxBitmap(typeof(resfinder), Utility.Resource.ImagenloginCiecIcono)]    
    [Themeable(true), ToolboxData(Utility.ToolBoxData.LoginCiec)]
    public class LoginCiec : System.Web.UI.WebControls.Login
    {       
        private bool renderDesignerRegion = true;
        private bool convertingToTemplate = true;
        private string urlImage;
        private string password = String.Empty;
        private string userName = String.Empty;
        private Sat.Scade.Net.IDE.Presentacion.Web.LoginContainer templateContainer;
        private Sat.Scade.Net.IDE.Presentacion.Web.LoginNewTemplate templateLayout;

        // R2. Se Agrega el Servicio de Actualización de Correo Electrónico
        [Category(Utility.Category.EDSCiec), Description("Url aplicación actualización de correo(s)."), UrlProperty, Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]

        public string DestinationMailUpdateUrl
        {
            get { return (String)ViewState["DestinationMailUpdateUrl"] ?? string.Empty; }
            set { ViewState["DestinationMailUpdateUrl"] = value; }
        }


        [Category(Utility.Category.EDSCiec), Description(Utility.Description.ChangeLoginType)]
        public event CommandEventHandler OnChangeLoginType;               

        public bool ConvertingToTemplate
        {
            get
            {
                return this.DesignMode && this.convertingToTemplate;
            }
        }

        private Sat.Scade.Net.IDE.Presentacion.Web.LoginContainer TemplateContainer
        {
            get
            {
                this.EnsureChildControls();
                return this.templateContainer;
            }
        }

        public Sat.Scade.Net.IDE.Presentacion.Web.LoginNewTemplate TemplateLayout
        {
            get
            {
                return this.templateLayout;
            }
            set
            {
                this.templateLayout = value;
                this.ChildControlsCreated = false;
            }
        }

        [ReadOnly(true)]
        public string LoginUserName
        {
            get
            {
                if ((this.templateContainer != null) && (this.templateContainer.UserNameTextBox != null))
                {
                    ITextControl userNameTextBox = (ITextControl)this.templateContainer.UserNameTextBox;
                    if ((userNameTextBox != null) && (userNameTextBox.Text != null))
                    {
                        return userNameTextBox.Text;
                    }
                }
                return String.Empty;
            }
        }

        [ReadOnly(true)]
        public string LoginPassword
        {
            get
            {
                if (this.password != null)
                {
                    return this.password;
                }
                return string.Empty;
            }
        }
 
        [Category(Utility.Category.EDSCiec), Description(Utility.Description.Tema)]
        public ScadeTheme Theme
        {
            get
            {                
                object obj = this.ViewState[Utility.ViewSate.ThemeLogin];                
                if (obj != null)
                {
                    return (ScadeTheme)obj;
                }
                return ScadeTheme.Default;
            }
            set
            {
                this.ViewState[Utility.ViewSate.ThemeLogin] = value;
            }
        }

        [Category(Utility.Category.EDSCiec), Description(Utility.Description.MedioPresentacion)]
        public MedioPresentacion MedioPresentacion
        {
            get
            {
                string str = this.GetSetting(Utility.AppSettings.MedioPresentacion);
                if (str != null)
                {
                    int medio = 0;
                    Int32.TryParse(str, out medio);
                    return (MedioPresentacion)medio;
                }
                object obj = this.ViewState[Utility.ViewSate.MedioPresentacion];
                if (obj != null)
                {
                    return (MedioPresentacion)obj;
                }
                return MedioPresentacion.Internet;
            }
            set
            {
                this.ViewState[Utility.ViewSate.MedioPresentacion] = value;
            }
        }

        [Category(Utility.Category.EDSCiec), Description(Utility.Description.UserNameRequiredErrorText)]
        public string UserNameRequiredErrorText
        {
            get
            {
                string str = this.GetSetting(Utility.AppSettings.UserNameRequiredErrorLogin);
                if (str != null)
                {
                    return str;
                }
                object obj = this.ViewState[Utility.ViewSate.UserNameRequiredErrorTextLogin];
                if (obj != null)
                {
                    return (string)obj;
                }
                return String.Empty;
            }
            set
            {
                this.ViewState[Utility.ViewSate.UserNameRequiredErrorTextLogin] = value;
            }
        }

        [Category(Utility.Category.EDSCiec), Description(Utility.Description.PasswordRequerido)]
        public string PasswordRequiredErrorText
        {
            get
            {
                string str = this.GetSetting(Utility.AppSettings.PasswordRequiredErrorLogin);
                if (str != null)
                {
                    return str;
                }
                object obj = this.ViewState[Utility.ViewSate.passwordRequiredErrorTextLogin];
                if (obj != null)
                {
                    return (string)obj;
                }
                return String.Empty;
            }
            set
            {
                this.ViewState[Utility.ViewSate.passwordRequiredErrorTextLogin] = value;
            }
        }
        
        [Category(Utility.Category.EDSCiec), Description(Utility.Description.UserNameTextLabel)]
        public string UserNameLabel
        {
            get
            {
                string str = this.GetSetting(Utility.AppSettings.UserNameLogin);
                if (str != null)
                {
                    return str;
                }
                object obj = this.ViewState[Utility.ViewSate.UserNameLabelLogin];
                if (obj != null)
                {
                    return (string)obj;
                }
                return String.Empty;
            }
            set
            {
                this.ViewState[Utility.ViewSate.UserNameLabelLogin] = value;
            }
        }

        [Category(Utility.Category.EDSCiec), Description(Utility.Description.PasswordTextLabel)]
        public string PasswordLabel
        {
            get
            {
                string str = this.GetSetting(Utility.AppSettings.PasswordLogin);
                if (str != null)
                {
                    return str;
                }
                object obj = this.ViewState[Utility.ViewSate.PasswordLabelLogin];
                if (obj != null)
                {
                    return (string)obj;
                }
                return String.Empty;
            }
            set
            {
                this.ViewState[Utility.ViewSate.PasswordLabelLogin] = value;
            }
        }

        [Category(Utility.Category.EDSCiec), Description(Utility.Description.DisplayMail)]
        public virtual bool DisplayMail
        {
            get
            {
                object obj = this.ViewState[Utility.ViewSate.DisplayMailLogin];
                if (obj != null)
                {
                    return (bool)obj;
                }
                return true;
            }
            set
            {
                this.ViewState[Utility.ViewSate.DisplayMailLogin] = value;
            }
        }

        [Category(Utility.Category.EDSCiec), Description(Utility.Description.Titulo)]
        public virtual string Titulo
        {
            get
            {
                string str = this.GetSetting(Utility.AppSettings.TitleLogin);
                if (str != null)
                {
                    return str;
                }
                object obj = this.ViewState[Utility.ViewSate.TitleLogin];
                if (obj != null)
                {
                    return (string)obj;
                }
                return String.Empty;
            }
            set
            {
                this.ViewState[Utility.ViewSate.TitleLogin] = value;
            }
        }

        [Category(Utility.Category.EDSCiec), Description(Utility.Description.ButtonText)]
        public virtual string ButtonText
        {
            get
            {
                string str = this.GetSetting(Utility.AppSettings.ButtonTextLogin);
                if (str != null)
                {
                    return str;
                }
                object obj = this.ViewState[Utility.ViewSate.ButtonTextLogin];
                if (obj != null)
                {
                    return (string)obj;
                }
                return String.Empty;
            }
            set
            {
                this.ViewState[Utility.ViewSate.ButtonTextLogin] = value;
            }
        }

        [Category(Utility.Category.EDSCiec), Description(Utility.Description.MailText)]
        public virtual string MailText
        {
            get
            {
                object obj = this.ViewState[Utility.ViewSate.MailTextLogin];
                if (obj != null)
                {
                    return (string)obj;
                }
                return String.Empty;
            }
            set
            {
                this.ViewState[Utility.ViewSate.MailTextLogin] = value;
            }
        }

        [Category(Utility.Category.EDSCiec),Description(Utility.Description.FielTitle)]
        public virtual string FielTitle
        {
            get
            {
                object obj = this.ViewState[Utility.ViewSate.FielTitleLogin];
                if (obj != null)
                {
                    return (string)obj;
                }
                return String.Empty;
            }
            set
            {
                this.ViewState[Utility.ViewSate.FielTitleLogin] = value;
            }
        }               

        [Category(Utility.Category.EDSCiec), Description(Utility.Description.FielTextUrl)]
        public virtual string FielTextUrl
        {
            get
            {
                object obj = this.ViewState[Utility.ViewSate.FielTextUrlLogin];
                if (obj != null)
                {
                    return (string)obj;
                }
                return String.Empty;
            }
            set
            {
                this.ViewState[Utility.ViewSate.FielTextUrlLogin] = value;
            }
        }

        [Category(Utility.Category.EDSCiec), Description(Utility.Description.CiecFNavigateUrl), UrlProperty, Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public virtual string CiecFNavigateUrl
        {
            get
            {
                object obj = this.ViewState[Utility.ViewSate.CiecFNavigateUrlLogin];
                if (obj != null)
                {
                    return (string)obj;
                }
                return String.Empty;
            }
            set
            {
                this.ViewState[Utility.ViewSate.CiecFNavigateUrlLogin] = value;
            }
        }

        [Category(Utility.Category.EDSCiec), Description(Utility.Description.CiecFTextUrl)]
        public virtual string CiecFTextUrl
        {
            get
            {
                object obj = this.ViewState[Utility.ViewSate.CiecFTextUrlLogin];
                if (obj != null)
                {
                    return (string)obj;
                }
                return String.Empty;
            }
            set
            {
                this.ViewState[Utility.ViewSate.CiecFTextUrlLogin] = value;
            }
        }

        [Category(Utility.Category.EDSCiec), Description(Utility.Description.ErrorAutenticacion)]
        public virtual string FailureMessage
        {
            get
            {
                string str = this.GetSetting(Utility.AppSettings.FailureTextLogin);
                if (str != null)
                {
                    return str;
                }
                object obj = this.ViewState[Utility.ViewSate.FailureMessageLogin];
                if (obj != null)
                {
                    return (string)obj;
                }
                return Utility.Message.ErrorAutenticacion;
            }
            set
            {
                this.ViewState[Utility.ViewSate.FailureMessageLogin] = value;
            }
        }

        [Category(Utility.Category.EDSCiec), Description(Utility.Description.MailPageUrl)]
        public virtual string MailPageUrl
        {
            get
            {
                return (string)this.ViewState[Utility.ViewSate.MailPageUrl]?? String.Empty;                
            }
            set
            {
                this.ViewState[Utility.ViewSate.MailPageUrl] = value;
            }
        }

        [Category(Utility.Category.EDSCiec), Description(Utility.Description.VerificaCorreo)]
        public bool UpdateMail
        {
            get
            {
                if ((this.templateContainer != null))
                {
                    ICheckBoxControl mailCheckBox = (ICheckBoxControl)this.templateContainer.MailCheckBox;
                    if ((mailCheckBox != null))
                    {
                        return mailCheckBox.Checked;
                    }
                }
                return false;
            }
        }         

        [Category(Utility.Category.EDSCiec), Description(Utility.Description.PasswordTecleado)]
        public override string Password
        {
            get
            {
                return this.password;
            }
        }

        private string UserNameInternal
        {
            get
            {
                string userName = this.userName;
                if ((this.templateContainer != null) && String.IsNullOrEmpty(userName))
                {
                    ITextControl userNameTextBox = (ITextControl)this.templateContainer.UserNameTextBox;
                    if ((userNameTextBox != null) && (userNameTextBox.Text != null))
                    {
                        return userNameTextBox.Text;
                    }
                }
                return userName;
            }
        }

        private string PasswordInternal
        {
            get
            {
                string password = this.password;
                if ((this.templateContainer != null) && String.IsNullOrEmpty(password))
                {
                    ITextControl passwordTextBox = (ITextControl)this.templateContainer.PasswordTextBox;
                    if ((passwordTextBox != null) && (passwordTextBox.Text != null))
                    {
                        return passwordTextBox.Text;
                    }
                }
                return password;
            }
        }

        public LoginCiec()
        {
            this.urlImage = String.Empty;
        }
        
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            this.SetEditableChildProperties();
            this.TemplateContainer.Visible = (this.VisibleWhenLoggedIn || !this.Page.Request.IsAuthenticated);

            string script = this.Page.ClientScript.GetWebResourceUrl(this.GetType(), Utility.Resource.RecursosFiel);
            this.Page.ClientScript.RegisterClientScriptInclude(Utility.Resource.WebControls, script);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (this.Page != null)
            {
                this.Page.VerifyRenderingInServerForm(this);
            }

            if (base.DesignMode)
            {
                this.ChildControlsCreated = false;
                this.EnsureChildControls();
            }
            if (this.TemplateContainer.Visible)
            {
                if (this.Page != null && this.TemplateContainer != null && this.Theme == ScadeTheme.New)
                {
                    Table tabla = this.TemplateContainer.FindControl(Utility.ID.Principal) as Table;
                    if (tabla != null)
                        tabla.BackImageUrl = this.Page.ClientScript.GetWebResourceUrl(this.GetType(), Utility.Resource.ImagenNewLogin);
                }

                this.SetChildProperties();
                this.RenderContents(writer);
            }
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            this.Controls.Clear();
            this.templateContainer = new Sat.Scade.Net.IDE.Presentacion.Web.LoginContainer(this);
            this.templateContainer.RenderDesignerRegion = this.renderDesignerRegion;
            ITemplate layoutTemplate = this.TemplateLayout;
            if (layoutTemplate == null)
            {
                this.templateContainer.EnableViewState = false;
                this.templateContainer.EnableTheming = false;
                layoutTemplate = new Sat.Scade.Net.IDE.Presentacion.Web.LoginDefaultTemplate(this);
                if (this.Theme == ScadeTheme.New)
                {
                    layoutTemplate = new Sat.Scade.Net.IDE.Presentacion.Web.LoginNewTemplate(this);
                }                
            }
            layoutTemplate.InstantiateIn(this.templateContainer);

            this.templateContainer.Visible = true;
            this.Controls.Add(this.templateContainer);

            if (this.MedioPresentacion == Web.MedioPresentacion.Internet) this.Controls.Add(new LiteralControl("<br /><br /><label style='font-family: Arial, Helvetica, sans-serif; font-size: 13px; color: #FF0000;'>IMPORTANTE:</label><label style='font-family: Arial, Helvetica, sans-serif; font-size: 13px'> Para un correcto funcionamiento del aplicativo utilizar Internet Explorer y realizar la siguiente </label><a href='AyudaConfigura.html' target='_blank' style='font-family: Arial, Helvetica, sans-serif; font-size: 13px; color: #0000FF;'>Configuraciones</a>"));

            this.SetEditableChildProperties();
            IEditableTextControl userNameTextBox = this.templateContainer.UserNameTextBox as IEditableTextControl;
            if (userNameTextBox != null) 
            {
                userNameTextBox.TextChanged += new EventHandler(this.UserNameTextChanged);
            }
            IEditableTextControl passwordTextBox = this.templateContainer.PasswordTextBox as IEditableTextControl;
            if (passwordTextBox != null)
            {
                passwordTextBox.TextChanged += new EventHandler(this.PasswordTextChanged);
            }
            Button fielLinkbutton = this.templateContainer.FielButton;
            if (fielLinkbutton != null)
            {
                fielLinkbutton.Command += new CommandEventHandler(fielLink_Command);
            }
        }

        private void SetDefaultTemplateChildProperties()
        {
            #region Configuracion de Controles para Medio Modulo

            LoginContainer templateContainer = this.TemplateContainer;

            //Label Titulo
            Label titleLabel = this.templateContainer.TitleLabel;
            string titleText = this.Titulo;
            if (titleText.Length > 0)
            {
                titleLabel.Text = titleText;
                this.SetTableCellVisible(titleLabel, true);
            }
            else
            {
                this.SetTableCellVisible(titleLabel, false);
            }
            if (this.TitleTextStyle != null)
            {
                this.SetTableCellStyle(titleLabel, this.TitleTextStyle);
            }

            //Label Nombre de Usuario                      
            Control userNameLabel = this.templateContainer.UserNameLabel;
            string userNameTextLabel = this.UserNameLabel;
            if (userNameTextLabel.Length > 0)
            {
                ((ITextControl)userNameLabel).Text = userNameTextLabel;
                userNameLabel.Visible = true;
            }
            if (this.LabelStyle != null)
            {
                this.SetTableCellStyle(userNameLabel, this.LabelStyle);
            }

            //TextBox Nombre de Usuario
            WebControl userNameTextBox = (WebControl)this.templateContainer.UserNameTextBox;
            RequiredFieldValidator userNameRequired = this.templateContainer.UserNameRequired;
            userNameTextBox.TabIndex = this.TabIndex;
            userNameTextBox.AccessKey = this.AccessKey;

            string userNameRequiredText = this.UserNameRequiredErrorText;
            if (userNameRequiredText.Length > 0)
            {
                userNameRequired.Text = this.UserNameRequiredErrorText;
                userNameRequired.ToolTip = this.UserNameRequiredErrorText;
                this.SetTableCellVisible(userNameRequired, true);
            }
            if (this.TextBoxStyle != null)
            {
                userNameTextBox.ApplyStyle(this.TextBoxStyle);
            }
            if (this.ValidatorTextStyle != null)
            {
                userNameRequired.ApplyStyle(this.ValidatorTextStyle);
            }

            //Label Password
            Control passwordLabel = this.templateContainer.PasswordLabel;
            string passwordLabelText = this.PasswordLabel;
            if (passwordLabelText.Length > 0)
            {
                ((ITextControl)passwordLabel).Text = passwordLabelText;
                passwordLabel.Visible = true;
            }
            if (this.LabelStyle != null)
            {
                this.SetTableCellStyle(passwordLabel, this.LabelStyle);
            }

            //TextBox Password
            WebControl passwordTextBox = (WebControl)this.templateContainer.PasswordTextBox;
            RequiredFieldValidator passwordRequired = this.templateContainer.PasswordRequired;
            passwordTextBox.TabIndex = this.TabIndex;

            string passwordRequiredText = this.PasswordRequiredErrorText;
            if (passwordRequiredText.Length > 0)
            {
                passwordRequired.Text = this.PasswordRequiredErrorText;
                passwordRequired.ToolTip = this.PasswordRequiredErrorText;
                this.SetTableCellVisible(passwordRequired, true);
            }
            if (this.TextBoxStyle != null)
            {
                passwordTextBox.ApplyStyle(this.TextBoxStyle);
            }
            if (this.ValidatorTextStyle != null)
            {
                passwordRequired.ApplyStyle(this.ValidatorTextStyle);
            }

            //Button Acceso Login
            Button loginButton = (Button)this.templateContainer.LoggInButton;
            string loginButtonText = this.ButtonText;
            if (loginButtonText.Length > 0)
            {
                loginButton.Visible = true;
                loginButton.Text = this.ButtonText;
                loginButton.TabIndex = this.TabIndex;
            }
            if (this.LoginButtonStyle != null)
            {
                loginButton.ApplyStyle(this.LoginButtonStyle);
            }

            ////Label Texto de Error de autenticacion
            //Label failureLabel = this.templateContainer.FailureLabel;
            //if (failureLabel.Text.Length > 0)
            //{
            //    if (this.FailureTextStyle != null)
            //    {
            //        failureLabel.ApplyStyle(this.FailureTextStyle);
            //        this.SetTableCellVisible(failureLabel, true);
            //    }
            //}
            //else
            //{
            //    this.SetTableCellVisible(failureLabel, false);
            //}

            #endregion

            #region Configuracion de Controles para Medio Internet

            CheckBox mailCheckBox = this.templateContainer.MailCheckBox;
            //Label fielTitle = this.templateContainer.FielLabel;
            Button fielLink = this.templateContainer.FielButton;
            //HyperLink ciecFLink = this.templateContainer.CiecFLink;

            // R2. Agregamos a la Configuración el Link para Agregar el Email
            HyperLink mailUpdateLink = this.templateContainer.MailUpdateLink;


            bool Internet = this.MedioPresentacion == MedioPresentacion.Internet;
            if (Internet)
            {
                //CheckBox Configuracion de Texto de Correo Electrónico.
                string mailCheckBoxText = this.MailText;
                if (mailCheckBoxText.Length > 0)
                {
                    mailCheckBox.Text = this.MailText;
                    mailCheckBox.TabIndex = this.TabIndex;
                    this.SetTableCellVisible(mailCheckBox, true);
                }

                // R2. Establecemos la Visibilidad del Checkbox para Añadir Emails
                this.SetTableCellVisible(mailCheckBox, this.DisplayMail);


                if (this.CheckBoxStyle != null)
                {
                    this.SetTableCellStyle(mailCheckBox, this.CheckBoxStyle);
                }

                // R2. Establecemos la Dirección del Url
                mailUpdateLink.NavigateUrl = this.DestinationMailUpdateUrl;


                /////Label Configuracion de la etiqueta para la liga de Fiel.
                //string fielTitleText = this.FielTitle;
                //if (fielTitleText.Length > 0)
                //{
                //    fielTitle.Text = this.FielTitle;
                //    fielTitle.TabIndex = this.TabIndex;
                //}
                //if (this.LabelStyle != null)
                //{
                //    this.SetTableCellStyle(fielTitle, this.LabelStyle);
                //}

                /////LinkConfiguracion del texto para la liga de Fiel.
                //string fielLinkText = this.FielTextUrl;
                //if (fielLinkText.Length > 0)
                //{
                //    fielLink.Text = this.FielTextUrl;
                //}
                //this.SetTableCellVisible(fielLink, true);


                /////Configuracion del Texto para la liga de CiecF
                //string ciecFLinkText = this.CiecFTextUrl;
                //if (ciecFLinkText.Length > 0)
                //{
                //    ciecFLink.Text = this.CiecFTextUrl;
                //}

                /////Configuracion del Url para la liga de CiecF
                //string ciecFLinkNavigateUrl = this.CiecFNavigateUrl;
                //if (ciecFLinkNavigateUrl.Length > 0)
                //{
                //    ciecFLink.NavigateUrl = this.CiecFNavigateUrl;
                //}

                //ciecFLink.TabIndex = this.TabIndex;
                //if (this.HyperLinkStyle != null)
                //{
                //    this.SetTableCellStyle(ciecFLink, this.HyperLinkStyle);
                //}
                //this.SetTableCellVisible(ciecFLink, true);
            }
            else
            {
                this.SetTableCellVisible(mailCheckBox, false);
				this.SetTableCellVisible(mailUpdateLink, false);

                //fielTitle.Visible = false;
                //this.SetTableCellVisible(fielTitle, false);

                fielLink.Visible = false;
                this.SetTableCellVisible(fielLink, false);

                //ciecFLink.Visible = false;
                //this.SetTableCellVisible(ciecFLink, false);
            }
            #endregion
        }

        private string GetSetting(string appSetting)
        {
            if (!this.DesignMode)
            {
                return ConfigurationManager.AppSettings[appSetting];
            }
            return null;
        }

        protected void UserNameTextChanged(object sender, EventArgs e)
        {
            this.userName = ((ITextControl)sender).Text;
            base.UserName = this.userName;
        }

        protected void PasswordTextChanged(object sender, EventArgs e)
        {
            this.password = ((ITextControl)sender).Text;
        }                         

        protected void SetEditableChildProperties()
        {
            LoginContainer templateContainer = this.TemplateContainer;
            string userNameInternal = this.UserNameInternal;
            if (!string.IsNullOrEmpty(userNameInternal))
            {
                ITextControl userNameTextBox = (ITextControl)templateContainer.UserNameTextBox;
                if (userNameTextBox != null)
                {
                    userNameTextBox.Text = userNameInternal;
                }
            }            
        }

        protected void SetChildProperties()
        {
            this.SetCommonChildProperties();
            if (this.TemplateLayout == null)
            {
                this.SetDefaultTemplateChildProperties();
            }
        }

        protected void SetCommonChildProperties()
        {
            Sat.Scade.Net.IDE.Presentacion.Web.LoginContainer templateContainer = this.TemplateContainer;
            this.CopyBaseAttributesToInnerControl(this, templateContainer);
            templateContainer.ApplyStyle(base.ControlStyle);

            ITextControl failureTextLabel = (ITextControl)templateContainer.FailureLabel;
            string failureText = this.FailureMessage;

            if (((failureTextLabel != null) && (failureText.Length > 0)) && this.RedirectedFromFailedLogin())
            {
                failureTextLabel.Text = failureText;
            }            
        }

        protected override void OnAuthenticate(AuthenticateEventArgs e)
        {
            base.OnAuthenticate(e);

            if (!e.Authenticated)
            {
                Page.Response.Redirect(String.Format("{0}?loginfailure=1", FormsAuthentication.LoginUrl));
            }
        }

        protected bool RedirectedFromFailedLogin()
        {
            return ((!base.DesignMode && (this.Page != null)) && (!this.Page.IsPostBack && (this.Page.Request.QueryString["loginfailure"] != null)));
        }              

        protected void fielLink_Command(object sender, CommandEventArgs e)
        {
            Page.Response.Redirect("LoginFEA.asp");
            //if (OnChangeLoginType != null)
            //{
            //    this.OnChangeLoginType(this, e);
            //} 
        }

        internal void SetTableCellStyle(Control control, Style style)
        {
            Control parent = control.Parent;
            if (parent != null)
            {
                ((TableCell)parent).ApplyStyle(style);
            }
        }

        internal void SetTableCellVisible(Control control, bool visible)
        {
            Control parent = control.Parent;
            if (parent != null)
            {
                parent.Visible = visible;
            }
        }

        internal void CopyBaseAttributesToInnerControl(WebControl control, WebControl child)
        {
            short tabIndex = control.TabIndex;
            string accessKey = control.AccessKey;
            try
            {
                control.AccessKey = string.Empty;
                control.TabIndex = 0;
                child.CopyBaseAttributes(control);
            }
            finally
            {
                control.TabIndex = tabIndex;
                control.AccessKey = accessKey;
            }
        }
    }
}