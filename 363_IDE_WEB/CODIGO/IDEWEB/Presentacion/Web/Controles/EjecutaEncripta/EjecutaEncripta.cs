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
    [Themeable(true), ToolboxData(Utility.ToolBoxData.EjecutaEncripta)]
    public class EjecutaEncripta : System.Web.UI.WebControls.Login
    {       
        private bool renderDesignerRegion = true;
        private bool convertingToTemplate = true;
        private string urlImage;
        //private string password = String.Empty;
        //private string userName = String.Empty;
        private Sat.Scade.Net.IDE.Presentacion.Web.EncriptaContainer templateContainer;
        private Sat.Scade.Net.IDE.Presentacion.Web.EncriptaNewTemplate templateLayout;

        


        [Category(Utility.Category.EDSCiec), Description(Utility.Description.ChangeLoginType)]
        public event CommandEventHandler OnChangeLoginType;               

        public bool ConvertingToTemplate
        {
            get
            {
                return this.DesignMode && this.convertingToTemplate;
            }
        }

        private Sat.Scade.Net.IDE.Presentacion.Web.EncriptaContainer TemplateContainer
        {
            get
            {
                this.EnsureChildControls();
                return this.templateContainer;
            }
        }

        public Sat.Scade.Net.IDE.Presentacion.Web.EncriptaNewTemplate TemplateLayout
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

        

        public EjecutaEncripta()
        {
            this.urlImage = String.Empty;
        }
        
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            //this.SetEditableChildProperties();
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
            this.templateContainer = new Sat.Scade.Net.IDE.Presentacion.Web.EncriptaContainer(this);
            this.templateContainer.RenderDesignerRegion = this.renderDesignerRegion;
            ITemplate layoutTemplate = this.TemplateLayout;
            if (layoutTemplate == null)
            {
                this.templateContainer.EnableViewState = false;
                this.templateContainer.EnableTheming = false;
                layoutTemplate = new Sat.Scade.Net.IDE.Presentacion.Web.EncriptaDefaultTemplate(this);
                if (this.Theme == ScadeTheme.New)
                {
                    layoutTemplate = new Sat.Scade.Net.IDE.Presentacion.Web.EncriptaNewTemplate(this);
                }                
            }
            layoutTemplate.InstantiateIn(this.templateContainer);

            this.templateContainer.Visible = true;
            this.Controls.Add(this.templateContainer);
            //this.SetEditableChildProperties();
            
            LinkButton fielLinkbutton = this.templateContainer.FielLinkButton;
            if (fielLinkbutton != null)
            {
                fielLinkbutton.Command += new CommandEventHandler(fielLink_Command);
            }
        }

        private void SetDefaultTemplateChildProperties()
        {
            #region Configuracion de Controles para Medio Modulo

            EncriptaContainer templateContainer = this.TemplateContainer;

            

            ////Button Acceso Login
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

            

            #endregion

            #region Configuracion de Controles para Medio Internet

            
            Label fielTitle = this.templateContainer.FielLabel;
            LinkButton fielLink = this.templateContainer.FielLinkButton;
            //HyperLink ciecFLink = this.templateContainer.CiecFLink;

            // R2. Agregamos a la Configuración el Link para Agregar el Email
            //HyperLink mailUpdateLink = this.templateContainer.MailUpdateLink;


            bool Internet = this.MedioPresentacion == MedioPresentacion.Internet;
            if (Internet)
            {
                


                ///Label Configuracion de la etiqueta para la liga de Fiel.
                string fielTitleText = this.FielTitle;
                if (fielTitleText.Length > 0)
                {
                    fielTitle.Text = this.FielTitle;
                    fielTitle.TabIndex = this.TabIndex;
                }
                if (this.LabelStyle != null)
                {
                    this.SetTableCellStyle(fielTitle, this.LabelStyle);
                }

                ///LinkConfiguracion del texto para la liga de Fiel.
                string fielLinkText = this.FielTextUrl;
                if (fielLinkText.Length > 0)
                {
                    fielLink.Text = this.FielTextUrl;
                }
                this.SetTableCellVisible(fielLink, true);
                

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
                

                fielTitle.Visible = false;
                this.SetTableCellVisible(fielTitle, false);

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

        //protected void UserNameTextChanged(object sender, EventArgs e)
        //{
        //    this.userName = ((ITextControl)sender).Text;
        //    base.UserName = this.userName;
        //}

        //protected void PasswordTextChanged(object sender, EventArgs e)
        //{
        //    this.password = ((ITextControl)sender).Text;
        //}                         

        //protected void SetEditableChildProperties()
        //{
        //    EncriptaContainer templateContainer = this.TemplateContainer;
        //    string userNameInternal = this.UserNameInternal;
        //    if (!string.IsNullOrEmpty(userNameInternal))
        //    {
        //        ITextControl userNameTextBox = (ITextControl)templateContainer.UserNameTextBox;
        //        if (userNameTextBox != null)
        //        {
        //            userNameTextBox.Text = userNameInternal;
        //        }
        //    }            
        //}

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
            Sat.Scade.Net.IDE.Presentacion.Web.EncriptaContainer templateContainer = this.TemplateContainer;
            this.CopyBaseAttributesToInnerControl(this, templateContainer);
            templateContainer.ApplyStyle(base.ControlStyle);

                 
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
            if (OnChangeLoginType != null)
            {
                this.OnChangeLoginType(this, e);
            } 
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