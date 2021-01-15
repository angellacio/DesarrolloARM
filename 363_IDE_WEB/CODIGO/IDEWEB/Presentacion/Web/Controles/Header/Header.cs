//@(#)SCADE2(W:SKDN09071PC4:Sat.Scade.Net.IDE.Presentacion.Web:Header:0:11/Febrero/2009[Sat.Scade.Net.IDE.Presentacion.Web:0:11/Febrero/2009]) 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Configuration;
using System.Drawing.Design;
using Sat.Scade.Net.IDE.Presentacion.Web;


[assembly: System.Web.UI.WebResource(Utility.Resource.ImagenSat, Utility.mimeType.image)]
[assembly: System.Web.UI.WebResource(Utility.Resource.ImagenSatInfo, Utility.mimeType.image)]
[assembly: System.Web.UI.WebResource(Utility.Resource.ImagenScade, Utility.mimeType.image)]
[assembly: System.Web.UI.WebResource(Utility.Resource.ImagenNewHeader, Utility.mimeType.image)]
namespace Sat.Scade.Net.IDE.Presentacion.Web
{
    [System.Drawing.ToolboxBitmap(typeof(resfinder), Utility.Resource.ImagenHeaderIcono)]    
    [Themeable(true), ToolboxData(Utility.ToolBoxData.Header)]
    public class Header : CompositeControl, INamingContainer
    {
        private const string PREFIEL_MODE = "PreFiel";
        private TableItemStyle titleTextStyle;        
        private ITemplate templateLayout;
        private HeaderContainer templateContainer;

        [Category(Utility.Category.EDSHeader)]
        private HeaderContainer TemplateContainer
        {
            get
            {
                this.EnsureChildControls();
                return this.templateContainer;

            }
        }

        [Category(Utility.Category.EDSHeader)]
        private ITemplate TemplateLayout
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

        [Category(Utility.Category.EDSHeader), Description("Estilo de texto del titulo."), DefaultValue((string)null), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), NotifyParentProperty(true), PersistenceMode(PersistenceMode.InnerProperty)]
        public TableItemStyle TitleTextStyle
        {
            get
            {
                if (this.titleTextStyle == null)
                {
                    this.titleTextStyle = new TableItemStyle();
                    if (base.IsTrackingViewState)
                    {
                        ((IStateManager)this.titleTextStyle).TrackViewState();
                    }
                }
                return this.titleTextStyle;
            }
        }        

        [Category(Utility.Category.EDSHeader), Description(Utility.Description.Tema)]
        public ScadeTheme Theme
        {
            get
            {
                object obj = this.ViewState[Utility.ViewSate.ThemeHeader];
                if (obj != null)
                {
                    return (ScadeTheme)obj;
                }
                return ScadeTheme.Default;
            }
            set
            {
                this.ViewState[Utility.ViewSate.ThemeHeader] = value;
            }
        }

        [Category(Utility.Category.EDSHeader), Description(Utility.Description.MedioPresentacion)]
        public MedioPresentacion MedioPresentacion
        {
            get
            {
                if (!ForzarMedioInternet)
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
                }
                return MedioPresentacion.Internet;
            }
            set
            {
                this.ViewState[Utility.ViewSate.MedioPresentacion] = value;
            }
        }

        [Category(Utility.Category.EDSHeader), Description(Utility.Description.ForzarInternet)]
        public bool ForzarMedioInternet
        {
            get
            {
                bool flag = false;
                if (ViewState[Utility.ViewSate.MedioInternetHeader] != null)
                {
                    bool.TryParse((string)ViewState[Utility.ViewSate.MedioInternetHeader], out flag);
                }
                return flag;
            }

            set
            {
                ViewState[Utility.ViewSate.MedioInternetHeader] = value.ToString();
            }
        }

        [Category(Utility.Category.EDSHeader), Description(Utility.Description.ModoFielHeader)]
        public bool ModoFiel
        {
            get
            {
                bool flag = false;
                if (ViewState[Utility.ViewSate.ModoFielHeader] != null)
                {
                    bool.TryParse((string)ViewState[Utility.ViewSate.ModoFielHeader], out flag);
                }
                return flag;
            }

            set
            {
                ViewState[Utility.ViewSate.ModoFielHeader] = value.ToString();
            }
        }

        [Category(Utility.Category.EDSHeader), Description(Utility.Description.Titulo)]
        public virtual string Titulo
        {
            get
            {
                string str = this.GetSetting(Utility.AppSettings.TitleHeader);
                if (str != null)
                {
                    return str.Replace("|","<br/>");
                }
                object obj = this.ViewState[Utility.ViewSate.TitleHeader];
                if (obj != null)
                {
                    return (string)obj;
                }
                return String.Empty;
            }
            set
            {
                this.ViewState[Utility.ViewSate.TitleHeader] = value;
            }
        }

        [Category(Utility.Category.EDSHeader), Description(Utility.Description.TituloRecepcion)]
        public virtual string TituloRecepcion
        {
            get
            {
                string str = this.GetSetting(Utility.AppSettings.TituloReceptionHeader);
                if (str != null)
                {
                    return str.Replace("|", "<br/>");
                }
                object obj = this.ViewState[Utility.ViewSate.TituloReceptionHeader];
                if (obj != null)
                {
                    return (string)obj;
                }
                return String.Empty;
            }
            set
            {
                this.ViewState[Utility.ViewSate.TituloReceptionHeader] = value;
            }
        }

        [Category(Utility.Category.EDSHeader), Description("Url de la imagen que sera mostrada en el lado izquierdo del encabezado."), UrlProperty, Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string UrlImageLeftModulo
        {
            get
            {
                String s = (String)ViewState[Utility.ViewSate.UrlImageLeftModuloHeader];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState[Utility.ViewSate.UrlImageLeftModuloHeader] = value;
            }
        }

        [Category(Utility.Category.EDSHeader), Description("Ancho de la imagen que sera mostrada en el lado izquierdo del encabezado, solo aplica para modulo."), UrlProperty, Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public int ImageLeftModuloWidth
        {
            get
            {
                String s = (String)ViewState[Utility.ViewSate.UrlImageLeftModuloHeaderWidth];
                return ((s == null) ? 0 : Int32.Parse(s));
            }

            set
            {
                ViewState[Utility.ViewSate.UrlImageLeftModuloHeaderWidth] = value.ToString();
            }
        }

        [Category(Utility.Category.EDSHeader), Description("Alto de la imagen que sera mostrada en el lado izquierdo del encabezado, solo aplica para modulo."), UrlProperty, Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public int ImageLeftModuloHeight
        {
            get
            {
                String s = (String)ViewState[Utility.ViewSate.UrlImageLeftModuloHeaderHeight];
                return ((s == null) ? 0 : Int32.Parse(s));
            }

            set
            {
                ViewState[Utility.ViewSate.UrlImageLeftModuloHeaderHeight] = value.ToString();
            }
        }

        [Category(Utility.Category.EDSHeader), Description("Url de la imagen que sera mostrada en el lado derecho del encabezado, solo aplica para modulo."), UrlProperty, Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string UrlImageRightModulo
        {
            get
            {
                String s = (String)ViewState[Utility.ViewSate.UrlImageRightModuloHeader];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState[Utility.ViewSate.UrlImageRightModuloHeader] = value;
            }
        }

        [Category(Utility.Category.EDSHeader), Description("Ancho de la imagen que sera mostrada en el lado derecho del encabezado, solo aplica para modulo."), UrlProperty, Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public int ImageRightModuloWidth
        {
            get
            {
                String s = (String)ViewState[Utility.ViewSate.UrlImageRightModuloHeaderWidth];
                return ((s == null) ? 0 : Int32.Parse(s));
            }

            set
            {
                ViewState[Utility.ViewSate.UrlImageRightModuloHeaderWidth] = value.ToString();
            }
        }

        [Category(Utility.Category.EDSHeader), Description("Alto de la imagen que sera mostrada en el lado derecho del encabezado, solo aplica para modulo."), UrlProperty, Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public int ImageRighttModuloHeight
        {
            get
            {
                String s = (String)ViewState[Utility.ViewSate.UrlImageRightModuloHeaderHeight];
                return ((s == null) ? 0 : Int32.Parse(s));
            }

            set
            {
                ViewState[Utility.ViewSate.UrlImageRightModuloHeaderHeight] = value.ToString();
            }
        }
        
        [Category(Utility.Category.EDSHeader), Description("Ancho de la imagen que sera mostrada en el centro del encabezado, solo aplica para internet."), UrlProperty, Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public int ImageInternetHeaderWidth
        {
            get
            {
                String s = (String)ViewState[Utility.ViewSate.UrlImageInternetHeaderWidth];
                return ((s == null) ? 0 : Int32.Parse(s));
            }

            set
            {
                ViewState[Utility.ViewSate.UrlImageInternetHeaderWidth] = value.ToString();
            }
        }

        [Category(Utility.Category.EDSHeader), Description("Alto de la imagen que sera mostrada en el centro del encabezado, solo aplica para internet."), UrlProperty, Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public int ImageInternetHeaderHeight
        {
            get
            {
                String s = (String)ViewState[Utility.ViewSate.UrlImageInternetHeaderHeight];
                return ((s == null) ? 0 : Int32.Parse(s));
            }

            set
            {
                ViewState[Utility.ViewSate.UrlImageInternetHeaderHeight] = value.ToString();
            }
        }

        [Category(Utility.Category.EDSHeader), Description("Url de la imagen que sera mostrada en el centro del encabezado, solo aplica para internet."), UrlProperty, Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string UrlImageInternet
        {
            get
            {
                String s = (String)ViewState[Utility.ViewSate.UrlImageInternetHeader];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState[Utility.ViewSate.UrlImageInternetHeader] = value;
            }
        }

        protected override void CreateChildControls()
        {
            this.Controls.Clear();
            this.templateContainer = new HeaderContainer();

            ITemplate layoutTemplate = this.TemplateLayout;

            if (layoutTemplate == null)
            {
                this.templateContainer.EnableViewState = false;
                this.templateContainer.EnableTheming = false;

                if (!this.DesignMode && !Page.IsPostBack && !String.IsNullOrEmpty(Page.Request.QueryString["mode"]) && Page.Request.QueryString["mode"].Equals(PREFIEL_MODE))
                {
                    this.ModoFiel = true;
                }
                if (this.Theme == ScadeTheme.New)
                {
                    layoutTemplate = CreateNewTemplate();
                }
                else
                {
                    layoutTemplate = CreateDefaultTemplate();
                }                
            }

            layoutTemplate.InstantiateIn(this.templateContainer);
            this.templateContainer.Visible = true;
            this.Controls.Add(this.templateContainer);
        }        

        protected override void Render(HtmlTextWriter writer)
        {
            if (this.Page != null)
            {
                this.Page.VerifyRenderingInServerForm(this);
            }
            if (base.DesignMode)
            {
                base.ChildControlsCreated = false;
                this.EnsureChildControls();
            }
            if (this.templateContainer.Visible)
            {
                this.SetChildProperties();
                this.RenderContents(writer);
            }
        }

        private void SetDefaultTemplateChildProperties()
        {
            HeaderContainer templateContainer = this.TemplateContainer;
            string titleHeaderSelected = this.Titulo;
            
            if (this.ForzarMedioInternet)
            {
                titleHeaderSelected = this.TituloRecepcion;
            }

            Label titleLabel = this.templateContainer.TitleLabel;
            string titleText = titleHeaderSelected;            
            titleLabel.Text = titleText;
            if (titleText.Length > 0)
            {
                this.SetTableCellVisible(titleLabel, true);
                if (this.TitleTextStyle != null)
                {
                    this.SetTableCellStyle(titleLabel, this.TitleTextStyle);
                }
            }
            else
            {
                this.SetTableCellVisible(titleLabel, false);
            }

            Image leftImage = this.templateContainer.LeftImage;
            string leftImageUrl = this.UrlImageLeftModulo;
            if (leftImageUrl.Length > 0)
            {
                leftImage.ImageUrl = leftImageUrl;

                int leftImageWidth = this.ImageLeftModuloWidth;
                if (leftImageWidth > 0)
                {
                    leftImage.Width = new Unit(leftImageWidth, UnitType.Pixel);
                }

                int leftImageHeight = this.ImageLeftModuloHeight;
                if (leftImageHeight > 0)
                {
                    leftImage.Height = new Unit(leftImageHeight, UnitType.Pixel);
                }

                this.SetTableCellVisible(leftImage, true);
            }


            Image internetImage = this.templateContainer.CenterImage;
            string internetImageUrl = this.UrlImageInternet;
            if (internetImageUrl.Length > 0)
            {
                internetImage.ImageUrl = internetImageUrl;

                int internetImageWidth = this.ImageInternetHeaderWidth;
                if (internetImageWidth > 0)
                {
                    internetImage.Width = new Unit(internetImageWidth, UnitType.Pixel);
                }
                int internetImageHeight = this.ImageInternetHeaderHeight;
                if (internetImageHeight > 0)
                {
                    internetImage.Height = new Unit(internetImageHeight, UnitType.Pixel);
                }

                this.SetTableCellVisible(internetImage, true);
            }

            Image rightImage = this.templateContainer.RighImage;
            string rightImageUrl = this.UrlImageRightModulo;
            if (rightImageUrl.Length > 0)
            {
                rightImage.ImageUrl = rightImageUrl;

                int rightImageWidth = this.ImageRightModuloWidth;
                if (rightImageWidth > 0)
                {
                    rightImage.Width = new Unit(rightImageWidth, UnitType.Pixel);
                }
                int rightImageHeight = this.ImageRighttModuloHeight;
                if (rightImageHeight > 0)
                {
                    rightImage.Height = new Unit(rightImageHeight, UnitType.Pixel);
                }

                this.SetTableCellVisible(rightImage, true);
            }
        }

        internal void SetChildProperties()
        {        
            if (this.TemplateLayout == null)
            {
                this.SetDefaultTemplateChildProperties();
            }

        }

        internal ITemplate CreateNewTemplate()
        {
            HeaderNewTemplate newTemplate = new HeaderNewTemplate();
            newTemplate.ImageUrlCenter = Page.ClientScript.GetWebResourceUrl(this.GetType(), Utility.Resource.ImagenNewHeader);
            newTemplate.MedioPresentacion = this.MedioPresentacion;
            newTemplate.ModoFiel= this.ModoFiel;
            return newTemplate;
        }

        internal ITemplate CreateDefaultTemplate()
        {
            HeaderDefaultTemplate defaultTemplate = new HeaderDefaultTemplate();
            defaultTemplate.ImageUrlLeft = Page.ClientScript.GetWebResourceUrl(this.GetType(), Utility.Resource.ImagenSat);
            defaultTemplate.ImageUrlCenter = Page.ClientScript.GetWebResourceUrl(this.GetType(), Utility.Resource.ImagenSatInfo);
            defaultTemplate.ImageUrlRight = Page.ClientScript.GetWebResourceUrl(this.GetType(), Utility.Resource.ImagenScade);
            defaultTemplate.MedioPresentacion = this.MedioPresentacion;
            defaultTemplate.ModoFiel= this.ModoFiel;
            return defaultTemplate;
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

        internal string GetSetting(string appSetting)
        {
            if (!this.DesignMode)
            {
                return ConfigurationManager.AppSettings[appSetting];
            }
            return null;
        }

    }
}

