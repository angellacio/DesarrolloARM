//@(#)SCADE2(W:SKDN09071PC4:Sat.Scade.Net.IDE.Presentacion.Comun:Email:0:11/Febrero/2009[Sat.Scade.Net.IDE.Presentacion.Comun:0:11/Febrero/2009]) 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing.Design;
using System.Drawing;
using System.Web.Security;

/// <summary>
/// Esta clase se creo para solucionar el bug o fixture del compilador de .NET ya que al no referenciar esta clase
/// en el atributo ToolboxBitmap el recurso(imagen) asignado como icono del control no puede ser encontrado.
/// </summary>
internal class resfinder
{
}

namespace Sat.Scade.Net.IDE.Presentacion.Web
{
    
    [ToolboxBitmap(typeof(resfinder), Utility.Resource.ImagenMailIcono)]    
    [Themeable(true), ToolboxData(Utility.ToolBoxData.Email)]
    public class Email : CompositeControl
    {
        private Button continuosButton;
        private Button updateButton;
        private Button cancelButton;
        private Label titleLabel;
        private Label CharErrorLabel;
        private Label userCaptionLabel;
        private Label mailCaptionLabel;
        private Label userTextLabel;
        private Label razonSocialTextLabel;
        private Label mailLabel;
        private Label errorLabel;
        private Label footerLabel;
        private TextBox mailTextBox;                        
        private RegularExpressionValidator validatorRegex;
        private RequiredFieldValidator validatorRequire;

        public event CancelEventHandler Cancelled;
        public event UpdateEventHander Continued;
        public event CommandEventHandler Accepted;

        private TableItemStyle titleTextStyle;
        private TableItemStyle footerTextStyle;
        private TableItemStyle buttonTextStyle;
        private TableItemStyle errorTextStyle;
        private TableItemStyle labelTextStyle;
        private TableItemStyle texBoxTextStyle;

        public delegate void UpdateEventHander(object sender, UpdateEventArgs e);

        [Category(Utility.Category.EDSEmail), Description(Utility.Description.TitleTextStyleEmail), DefaultValue((string)null), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), NotifyParentProperty(true), PersistenceMode(PersistenceMode.InnerProperty)]
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

        [Category(Utility.Category.EDSEmail), Description(Utility.Description.LabelTextStyleEmail), DefaultValue((string)null), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), NotifyParentProperty(true), PersistenceMode(PersistenceMode.InnerProperty)]
        public TableItemStyle LabelTextStyle
        {
            get
            {
                if (this.labelTextStyle == null)
                {
                    this.labelTextStyle = new TableItemStyle();
                    if (base.IsTrackingViewState)
                    {
                        ((IStateManager)this.labelTextStyle).TrackViewState();
                    }
                }
                return this.labelTextStyle;
            }
        }

        [Category(Utility.Category.EDSEmail), Description(Utility.Description.ErrorTextStyleEmail), DefaultValue((string)null), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), NotifyParentProperty(true), PersistenceMode(PersistenceMode.InnerProperty)]
        public TableItemStyle ErrorTextStyle
        {
            get
            {
                if (this.errorTextStyle == null)
                {
                    this.errorTextStyle = new TableItemStyle();
                    if (base.IsTrackingViewState)
                    {
                        ((IStateManager)this.errorTextStyle).TrackViewState();
                    }
                }
                return this.errorTextStyle;
            }
        }

        [Category(Utility.Category.EDSEmail), Description(Utility.Description.ButtonTextStyleEmail), DefaultValue((string)null), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), NotifyParentProperty(true), PersistenceMode(PersistenceMode.InnerProperty)]
        public TableItemStyle ButtonTextStyle
        {
            get
            {
                if (this.buttonTextStyle == null)
                {
                    this.buttonTextStyle = new TableItemStyle();
                    if (base.IsTrackingViewState)
                    {
                        ((IStateManager)this.buttonTextStyle).TrackViewState();
                    }
                }
                return this.buttonTextStyle;
            }
        }

        [Category(Utility.Category.EDSEmail), Description(Utility.Description.FooterTextStyleEmail), DefaultValue((string)null), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), NotifyParentProperty(true), PersistenceMode(PersistenceMode.InnerProperty)]
        public TableItemStyle FooterTextStyle
        {
            get
            {
                if (this.footerTextStyle == null)
                {
                    this.footerTextStyle = new TableItemStyle();
                    if (base.IsTrackingViewState)
                    {
                        ((IStateManager)this.footerTextStyle).TrackViewState();
                    }
                }
                return this.footerTextStyle;
            }
        }

        [Category(Utility.Category.EDSEmail), Description(Utility.Description.TexBoxTextStyleEmail), DefaultValue((string)null), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), NotifyParentProperty(true), PersistenceMode(PersistenceMode.InnerProperty)]
        public TableItemStyle TexBoxTextStyle
        {
            get
            {
                if (this.texBoxTextStyle == null)
                {
                    this.texBoxTextStyle = new TableItemStyle();
                    if (base.IsTrackingViewState)
                    {
                        ((IStateManager)this.texBoxTextStyle).TrackViewState();
                    }
                }
                return this.texBoxTextStyle;
            }
        }

        [Category(Utility.Category.EDSEmail), Description(Utility.Description.TitleEmail)]
        public string TitleEmail
        {
            get
            {
                return (String)ViewState[Utility.ViewSate.TitleEmail] ?? Utility.Text.TitleEMail;
            }

            set
            {
                ViewState[Utility.ViewSate.TitleEmail] = value;
            }
        }

        [Category(Utility.Category.EDSEmail), Description(Utility.Description.RfcEmail)]
        public string RfcText
        {
            get
            {
                return (String)ViewState[Utility.ViewSate.RfcTextEmail] ?? String.Empty;
            }

            set
            {
                ViewState[Utility.ViewSate.RfcTextEmail] = value;
            }
        }

        [Category(Utility.Category.EDSEmail), Description(Utility.Description.RazonSocialEmail)]
        public string RazonSocialText
        {
            get
            {
                return (String)ViewState[Utility.ViewSate.RazonSocialTextEmail] ?? String.Empty;
            }

            set
            {
                ViewState[Utility.ViewSate.RazonSocialTextEmail] = value;
            }
        }

        [Category(Utility.Category.EDSEmail), Description(Utility.Description.UserLabelEmail)]
        public string UserLabelText
        {
            get
            {
                return (String)ViewState[Utility.ViewSate.UserLabelTextEmail] ?? Utility.Text.RfcEmail;
            }

            set
            {
                ViewState[Utility.ViewSate.UserLabelTextEmail] = value;
            }
        }

        [Category(Utility.Category.EDSEmail), Description(Utility.Description.MailLabelEmail)]
        public string MailLabelText
        {
            get
            {
                return (String)ViewState[Utility.ViewSate.MailLabelTextEmail] ?? Utility.Text.MailLabelEmail;
            }

            set
            {
                ViewState[Utility.ViewSate.MailLabelTextEmail] = value;
            }
        }
       
        [Category(Utility.Category.EDSEmail), Description(Utility.Description.ActualEmail)]
        public string CurrentEmail
        {
            get
            {
                return (String)ViewState[Utility.ViewSate.CurrentEmail] ?? String.Empty;
            }

            set
            {
                ViewState[Utility.ViewSate.CurrentEmail] = value;
            }
        }

        [Category(Utility.Category.EDSEmail), ReadOnly(true), Description(Utility.Description.NewEmail)]
        public string NewEmail
        {
            get
            {
                return this.NewMailHidden;
            }
        }

        [Category(Utility.Category.EDSEmail)]
        private string NewMailHidden
        {
            get
            {
                return (String)ViewState[Utility.ViewSate.NewEmailHidden] ?? String.Empty;
            }
            set
            {
                ViewState[Utility.ViewSate.NewEmailHidden] = value;
            }
        }

        [Category(Utility.Category.EDSEmail), Description(Utility.Description.ContinuosButtonEmail)]
        public string ContinuosButtonText
        {
            get
            {
                return (String)ViewState[Utility.ViewSate.ContinuosButtonTextEmail] ?? Utility.Text.Continuar;
            }

            set
            {
                ViewState[Utility.ViewSate.ContinuosButtonTextEmail] = value;
            }
        }

        [Category(Utility.Category.EDSEmail), Description(Utility.Description.UpdateButtonEmail)]
        public string UpdateButtonText
        {
            get
            {
                return (String)ViewState[Utility.ViewSate.UpdateButtonTextEmail] ?? Utility.Text.ActualizarEmail;
            }

            set
            {
                ViewState[Utility.ViewSate.UpdateButtonTextEmail] = value;
            }
        }

        [Category(Utility.Category.EDSEmail), Description(Utility.Description.CancelButtonEmail)]
        public string CancelButtonText
        {
            get
            {
                return (String)ViewState[Utility.ViewSate.CancelButtonTextEmail] ?? Utility.Text.CancelarEmail;
            }

            set
            {
                ViewState[Utility.ViewSate.CancelButtonTextEmail] = value;
            }
        }

        [Category(Utility.Category.EDSEmail), Description(Utility.Description.FooterTextEmail)]
        public string FooterText
        {
            get
            {
                return (String)ViewState[Utility.ViewSate.FooterTextEmail] ?? Utility.Text.FooterTextEmail;
            }

            set
            {
                ViewState[Utility.ViewSate.FooterTextEmail] = value;
            }
        }

        [Category(Utility.Category.EDSEmail), Description(Utility.Description.FailureEmail)]
        public string FailureText
        {
            get
            {
                return (String)ViewState[Utility.ViewSate.FailureTextEmail] ?? Utility.Text.FailureTextEmail;
            }

            set
            {
                ViewState[Utility.ViewSate.FailureTextEmail] = value;
            }
        }

        [Category(Utility.Category.EDSEmail), Description(Utility.Description.FailureRequireEmail)]
        public string FailureRequireText
        {
            get
            {
                return (String)ViewState[Utility.ViewSate.FailureRequireTextEmail] ?? Utility.Text.FailureRequireTextEmail;
            }

            set
            {
                ViewState[Utility.ViewSate.FailureRequireTextEmail] = value;
            }
        }

        [Category(Utility.Category.EDSEmail), Description(Utility.Description.FailureReqCaracter)]
        public string FailureRequireChar
        {
            get
            {
                return (String)ViewState[Utility.ViewSate.FailureRequireCharEmail]?? Utility.Text.FailureRequireChar ;
            }

            set
            {
                ViewState[Utility.ViewSate.FailureRequireCharEmail] = value;
            }
        }

        [Category(Utility.Category.EDSEmail), Description(Utility.Description.SuccessfullTextEmail)]
        public string SuccesfullText
        {
            get
            {
                return (String)ViewState[Utility.ViewSate.SuccesfullTextEmail] ?? Utility.Text.SuccesfullTextEmail;
            }

            set
            {
                ViewState[Utility.ViewSate.SuccesfullTextEmail] = value;
            }
        }

        [Category(Utility.Category.EDSEmail), Description(Utility.Description.ExceptionTextEmail)]
        public string ExceptionText
        {
            get
            {
                return (String)ViewState[Utility.ViewSate.ExceptionTextEmail] ?? Utility.Text.ExceptionTextEmail;
            }

            set
            {
                ViewState[Utility.ViewSate.ExceptionTextEmail] = value;
            }
        }
        
        [Category(Utility.Category.EDSEmail), Description(Utility.Description.RegExEmail)]
        public string RegEx
        {
            get
            {
                return (String)ViewState[Utility.ViewSate.RegExEmail] ?? Utility.Text.RegExEmail;
            }

            set
            {
                ViewState[Utility.ViewSate.RegExEmail] = value;
            }
        }

        [Category(Utility.Category.EDSEmail), Description(Utility.Description.DestinationPageUrlEmail), UrlProperty, Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string DestinationPageUrl
        {
            get
            {
                return (String)ViewState[Utility.ViewSate.DestinationPageUrlEMail] ?? String.Empty;
            }

            set
            {
                ViewState[Utility.ViewSate.DestinationPageUrlEMail] = value;
            }
        }
        
        [Category(Utility.Category.EDSEmail), Description(Utility.Description.MembershipProviderEmail)]
        public string MembershipProvider
        {
            get
            {
                return (String)ViewState[Utility.ViewSate.MembershipProviderEMail] ?? String.Empty;
            }

            set
            {
                ViewState[Utility.ViewSate.MembershipProviderEMail] = value;
            }
        }

        [Category(Utility.Category.EDSEmail), Description(Utility.Description.HasChangedEmail), ReadOnly(true)]
        public bool MailChanged
        {
            get { return MailChangedHidden; }
        }

        [Category(Utility.Category.EDSEmail)]
        private bool MailChangedHidden
        {
            get
            {
                bool visible = false;
                if (ViewState[Utility.ViewSate.MailChangedHiddenEmail] != null)
                {
                    return (bool)ViewState[Utility.ViewSate.MailChangedHiddenEmail];
                }
                return visible;
            }

            set
            {
                ViewState[Utility.ViewSate.MailChangedHiddenEmail] = value;
            }
        }

        [Category(Utility.Category.EDSEmail), Description(Utility.Description.errorOcurrEmail)]
        private bool ErrorOcurr
        {
            get
            {
                bool errorOcurr = false;
                if (ViewState[Utility.ViewSate.ErrorOcurrEmail] != null)
                {
                    return (bool)ViewState[Utility.ViewSate.ErrorOcurrEmail];
                }
                return errorOcurr;
            }

            set
            {
                ViewState[Utility.ViewSate.ErrorOcurrEmail] = value;
            }
        }

        protected override void CreateChildControls()
        {
            this.Controls.Clear();

            string IDValidation = Guid.NewGuid().ToString();

            TableCell titleCell = this.CreateCell(1,100, HorizontalAlign.Center);
            TableCell rfcCell = this.CreateCell(1, 100, HorizontalAlign.Center);
            TableCell razonSocialCell = this.CreateCell(1, 100, HorizontalAlign.Center);
            TableCell mailCell = this.CreateCell(1, 100, HorizontalAlign.Center);
            TableCell updateCell = this.CreateCell(1, 100, HorizontalAlign.Center);
            TableCell footerCell = this.CreateCell(1, 100, HorizontalAlign.Center);
            TableCell failureCell = this.CreateCell(1, 100, HorizontalAlign.Center);            
                                    
            this.titleLabel = new Label();
            this.titleLabel.Text = this.TitleEmail;
            this.titleLabel.Font.Bold = true;
            titleCell.Controls.Add(this.titleLabel);
            titleCell.Controls.Add(new LiteralControl("<br/><br/>"));            
            
            this.userCaptionLabel = new Label();
            this.userCaptionLabel.Text = this.UserLabelText;            
            this.userTextLabel = new Label();
            this.userTextLabel.Text = this.RfcText;
            rfcCell.Controls.Add(this.userCaptionLabel);
            rfcCell.Controls.Add(this.userTextLabel);
            rfcCell.Controls.Add(new LiteralControl("<br/><br/>"));
                               
            this.razonSocialTextLabel = new Label();
            this.razonSocialTextLabel.Text = this.RazonSocialText;
            rfcCell.Controls.Add(this.razonSocialTextLabel);      
            rfcCell.Controls.Add(new LiteralControl("<br/><br/>"));
                      
            this.mailCaptionLabel = new Label();
            this.mailCaptionLabel.Text = this.MailLabelText;
            this.mailCaptionLabel.Font.Bold = true;
            mailCell.Controls.Add(this.mailCaptionLabel);
                              
            this.footerLabel = new Label();
            this.footerLabel.Text = this.FooterText;
            footerCell.Controls.Add(this.footerLabel);            
                        
            this.errorLabel = new Label();
            this.errorLabel.Text = this.FailureText;
            failureCell.Controls.Add(this.errorLabel);
        
            ///Si el correo electronico cambio, solo debe mostrar un label con el nuevo correo y un boton 'Continuar'
            if (this.MailChanged)
            {                
                this.mailLabel = new Label();
                this.mailLabel.Text = this.NewEmail;
                mailCell.Controls.Add(this.mailLabel);
                mailCell.Controls.Add(new LiteralControl("<br/><br/>"));

                updateCell.Controls.Clear();                               

                this.continuosButton = new Button();
                this.continuosButton.Font.Name = Utility.Style.font_ArialSans;
                this.continuosButton.Font.Size = new FontUnit(13, UnitType.Pixel);
                this.continuosButton.Font.Bold = true;
                this.continuosButton.BorderColor = Color.FromArgb(122, 138, 153);
                this.continuosButton.BorderStyle = BorderStyle.Solid;
                this.continuosButton.Command += new CommandEventHandler(continuosButton_Command);
                this.continuosButton.Text = this.ContinuosButtonText;
                updateCell.Controls.Add(this.continuosButton);
                updateCell.Controls.Add(new LiteralControl("<br/><br/>"));
            }
            else
            {
                ///Si el correo electronico no ha cambiado, entonces debe mostrar un TextBox en modo de edicio y los botones 'Actualizar' y 'Cancelar'
                this.mailTextBox = new TextBox();
                this.mailTextBox.ID = Utility.ID.CurrentMailTextBoxEmail;
                this.mailTextBox.TextChanged += new EventHandler(mailTextBox_TextChanged);
                this.mailTextBox.Text = this.CurrentEmail;
                this.mailTextBox.Width = new Unit(200, UnitType.Pixel);
                this.mailTextBox.ValidationGroup = IDValidation;                
                mailCell.Controls.Add(this.mailTextBox);

                CharErrorLabel = new Label();
                CharErrorLabel.ForeColor = Color.Red;
                CharErrorLabel.Text = this.FailureRequireChar;
                mailCell.Controls.Add(CharErrorLabel);                
                mailCell.Controls.Add(new LiteralControl("<br/>"));
                
                this.validatorRegex = new RegularExpressionValidator();
                this.validatorRegex.ControlToValidate = Utility.ID.CurrentMailTextBoxEmail;
                this.validatorRegex.Display = ValidatorDisplay.Dynamic;
                this.validatorRegex.ErrorMessage = this.FailureText;
                this.validatorRegex.Text = this.FailureText;
                this.validatorRegex.ValidationExpression = this.RegEx;
                this.validatorRegex.ValidationGroup = IDValidation;                
                mailCell.Controls.Add(this.validatorRegex);
                
                this.validatorRequire = new RequiredFieldValidator();
                this.validatorRequire.ControlToValidate = Utility.ID.CurrentMailTextBoxEmail;
                this.validatorRequire.Display = ValidatorDisplay.Dynamic;
                this.validatorRequire.ErrorMessage = this.FailureRequireText;
                this.validatorRequire.Text = this.FailureRequireText;
                this.validatorRequire.ValidationGroup = IDValidation;                
                mailCell.Controls.Add(this.validatorRequire);
                mailCell.Controls.Add(new LiteralControl("<br/><br/>"));
                                               
                updateCell.Controls.Clear();                               

                this.updateButton = new Button();
                this.updateButton.Text = this.UpdateButtonText;
                this.updateButton.ValidationGroup = IDValidation;
                this.updateButton.CausesValidation = true;
                this.updateButton.Font.Name = Utility.Style.font_ArialSans;
                this.updateButton.Font.Size = new FontUnit(13, UnitType.Pixel);
                this.updateButton.Font.Bold = true;
                this.updateButton.BorderColor = Color.FromArgb(122, 138, 153);
                this.updateButton.BorderStyle = BorderStyle.Solid;
                this.updateButton.Command += new CommandEventHandler(updateButton_Command);
                updateCell.Controls.Add(this.updateButton);                               
                
                this.cancelButton = new Button();
                this.cancelButton.Text = CancelButtonText;
                this.cancelButton.Font.Name = Utility.Style.font_ArialSans;
                this.cancelButton.Font.Size = new FontUnit(13, UnitType.Pixel);
                this.cancelButton.Font.Bold = true;
                this.cancelButton.BorderColor = Color.FromArgb(122, 138, 153);
                this.cancelButton.BorderStyle = BorderStyle.Solid;
                this.cancelButton.Command += new CommandEventHandler(cancelButton_Command);                                
                updateCell.Controls.Add(this.cancelButton);
                updateCell.Controls.Add(new LiteralControl("<br/><br/>"));
            }

            TableRow titleRow = CreateRow(titleCell);
            TableRow rfcRow = CreateRow(rfcCell);
            TableRow RazonSocialRow = CreateRow(razonSocialCell);
            TableRow mailRow = CreateRow(mailCell);
            TableRow failureRow = CreateRow(failureCell);
            TableRow updateRow = CreateRow(updateCell);
            TableRow footerRow = CreateRow(footerCell);
            
            Table container = new Table();
            container.ID = Utility.ID.ContainerMail;
            container.Font.Name = Utility.Style.font_name;
            container.Font.Size = new FontUnit(13, UnitType.Pixel);

            container.Rows.Add(titleRow);
            container.Rows.Add(rfcRow);
            container.Rows.Add(RazonSocialRow);
            container.Rows.Add(mailRow);
            container.Rows.Add(failureRow);
            container.Rows.Add(updateRow);
            container.Rows.Add(footerRow);

            this.Controls.Add(container);
        }        

        protected override void Render(HtmlTextWriter writer)
        {
            this.SetChildProperties();
            this.RenderContents(writer);
        }

        private void UpdateMail(UpdateEventArgs e)
        {
            try
            {
                MembershipProvider provider = this.GetProvider(this.MembershipProvider);
                MembershipUser user = provider.GetUser(this.RfcText, false) as MembershipUser;
                user.Email = this.NewEmail;

                provider.UpdateUser(user);
                e.Updated = true;                
            }
            catch (Exception ex)
            {
                e.Updated = false;
                this.FailureText = this.ExceptionText;
                e.ErrorMessage = ex.Message;
                System.Diagnostics.EventLog.WriteEntry(ex.Source, ex.Message);
                throw ex;
            }
        }

        protected void mailTextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox NewMailTextBox = sender as TextBox;
            string newMailText = String.Empty;
            if (NewMailTextBox != null)
            {
                newMailText = NewMailTextBox.Text;
            }
            NewMailHidden = newMailText;
        }
        
        protected void updateButton_Command(object sender, CommandEventArgs e)
        {
            if (this.Accepted != null)
            {
                this.Accepted(this, e);
            }
            else
            {
                this.MailChangedHidden = true;
                this.FailureText = this.SuccesfullText;
                this.ErrorOcurr = true;
            }
            this.CreateChildControls();
        }

        protected void continuosButton_Command(object sender, CommandEventArgs e)
        {
            UpdateEventArgs eUpdate = new UpdateEventArgs();
            if (this.Continued != null)
            {
                this.Continued(this, eUpdate);
            }
            else
            {
                this.ErrorOcurr = true;
                if ((this.Page == null) || this.Page.IsValid)
                {
                    this.UpdateMail(eUpdate);
                    if (eUpdate.Updated)
                    {
                        Page.Response.Redirect(this.DestinationPageUrl);                        
                    }
                    else
                    {                        
                        this.CreateChildControls();
                    }
                }
            }
        }        

        protected void cancelButton_Command(object sender, CommandEventArgs e)
        {
            CancelEventArgs eCancel = new CancelEventArgs();
            if (this.Cancelled != null)
            {
                this.Cancelled(this, eCancel);
            }
            else
            {
                Page.Response.Redirect(this.DestinationPageUrl);
            }
        }

        /// <summary>
        /// Asignación y configuración de las propiedades de los controles
        /// </summary>
        protected void SetChildProperties()
        {
            ///Si el control no se encuentra en modo de diseño entonces obtenemos el proveedor de membresias
            ///para asignar el Rfc, correo electrónico actual y la razon social.
            if (!DesignMode)
            {
                try
                {
                    MembershipProvider provider = this.GetProvider(this.MembershipProvider);
                    MembershipUser user = provider.GetUser(this.RfcText, false) as MembershipUser;
                    this.RfcText = user.ProviderUserKey.ToString();
                    this.CurrentEmail = user.Email;
                    this.RazonSocialText = user.UserName;
                }
                catch(Exception ex)
                {
                    System.Diagnostics.EventLog.WriteEntry(ex.Source, ex.Message);
                    throw ex;
                }
            }

            Label titleLabel = this.titleLabel;
            string titleText = this.TitleEmail;
            if (titleText.Length > 0)
            {
                titleLabel.Text = titleText;
                this.SetTableCellVisible(titleLabel, true);
            }
            if (this.TitleTextStyle != null)
            {
                this.SetTableCellStyle(titleLabel, this.TitleTextStyle);
            }

            Label rfclabel = this.userCaptionLabel;
            string rfcLabelText = this.UserLabelText;
            if (rfcLabelText.Length > 0)
            {
                rfclabel.Text = rfcLabelText;
                this.SetTableCellVisible(rfclabel, true);
            }
            if (this.LabelTextStyle != null)
            {
                this.SetTableCellStyle(rfclabel, this.LabelTextStyle);
            }                       

            Label razonSocialTextlabel = this.razonSocialTextLabel;
            string razonSocialText = this.RazonSocialText;
            if (razonSocialText.Length > 0)
            {
                razonSocialTextlabel.Text = razonSocialText;
                this.SetTableCellVisible(razonSocialTextlabel, true);
            }
            if (this.LabelTextStyle != null)
            {
                this.SetTableCellStyle(razonSocialTextlabel, this.LabelTextStyle);
            }
            
            Label userlabel = this.userCaptionLabel;
            string userLabelText = this.UserLabelText;
            if (userLabelText.Length > 0)
            {
                userlabel.Text = userLabelText;
                this.SetTableCellVisible(userlabel, true);
            }
            if (this.LabelTextStyle != null)
            {
                this.SetTableCellStyle(userlabel, this.LabelTextStyle);
            }
                        
            Label mensajeLabel = this.errorLabel;
            string mensajeText = this.FailureText;
            if (mensajeText.Length > 0 && ErrorOcurr)
            {
                mensajeLabel.Text = mensajeText;
                this.SetTableCellVisible(mensajeLabel, true);
            }
            else
            {
                this.SetTableCellVisible(mensajeLabel, false);
            }
            if (this.LabelTextStyle != null)
            {
                this.SetTableCellStyle(mensajeLabel, this.LabelTextStyle);
            }

            Label footLabel = this.footerLabel;
            string footText = this.FooterText;
            if (footText.Length > 0)
            {
                footLabel.Text = footText;
                this.SetTableCellVisible(footLabel, true);
            }
            if (this.FooterTextStyle != null)
            {
                this.SetTableCellStyle(footLabel, this.FooterTextStyle);
            }

            if (this.MailChanged)
            {
                Label maillabel = this.mailLabel;
                string newMailText = this.NewEmail;
                if (newMailText.Length > 0)
                {
                    maillabel.Text = newMailText;
                    this.SetTableCellVisible(maillabel, true);
                }
                if (this.LabelTextStyle != null)
                {
                    this.SetTableCellStyle(maillabel, this.LabelTextStyle);
                }

                Button contButton = this.continuosButton;
                string contText = this.ContinuosButtonText;
                if (contText.Length > 0)
                {
                    contButton.Text = contText;
                    this.SetTableCellVisible(contButton, true);
                }
                if (this.ButtonTextStyle != null)
                {
                    this.SetTableCellStyle(contButton, this.ButtonTextStyle);
                }
            }
            else
            {
                TextBox mailtextbox = this.mailTextBox;
                string currentMailText = this.CurrentEmail;
                if (currentMailText.Length > 0)
                {
                    mailtextbox.Text = currentMailText;
                    this.SetTableCellVisible(mailtextbox, true);
                }
                if (this.TexBoxTextStyle != null)
                {
                    this.SetTableCellStyle(mailtextbox, this.TexBoxTextStyle);
                }
                this.SetTableCellVisible(CharErrorLabel, true);
                if (this.ErrorTextStyle != null)
                {
                    this.SetTableCellStyle(CharErrorLabel, this.ErrorTextStyle);
                }

                Button cancButton = this.cancelButton;
                string cancText = this.CancelButtonText;
                if (cancText.Length > 0)
                {
                    cancButton.Text = cancText;
                    this.SetTableCellVisible(cancButton, true);
                }
                if (this.ButtonTextStyle != null)
                {
                    this.SetTableCellStyle(cancButton, this.ButtonTextStyle);
                }

                Button updaButton = this.updateButton;
                string updaText = this.UpdateButtonText;
                if (updaText.Length > 0)
                {
                    updaButton.Text = updaText;
                    this.SetTableCellVisible(updaButton, true);
                }
                if (this.ButtonTextStyle != null)
                {
                    this.SetTableCellStyle(updaButton, this.ButtonTextStyle);
                }

            }

        }

        /// <summary>
        /// Obtiene el proveedor de membresias configurado.
        /// </summary>
        /// <param name="providerName">Nombre del proveedor que se encuentra en el archivo de configuracion Web.Config</param>
        /// <returns></returns>
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

        /// <summary>
        /// Asigna el estilo de fuente a la celda
        /// </summary>
        /// <param name="control">control que pertenece a la celda deseada</param>
        /// <param name="style">Estilo a configuar</param>
        internal void SetTableCellStyle(Control control, Style style)
        {
            Control parent = control.Parent;
            if (parent != null)
            {
                ((TableCell)parent).ApplyStyle(style);
            }
        }

        /// <summary>
        /// Habilita la visibilidad de la celda que contiene al control.
        /// </summary>
        /// <param name="control">Control contenido en la celda a ocultar o aparecer.</param>
        /// <param name="visible">True si el control y la celda seran visibles, en otro caso False.</param>
        internal void SetTableCellVisible(Control control, bool visible)
        {
            Control parent = control.Parent;
            if (parent != null)
            {
                parent.Visible = visible;
            }
        }

        /// <summary>
        /// Crea una fila con la celda indicada.
        /// </summary>
        /// <param name="cell">Celda a agregar.</param>
        /// <returns>Retorna una nueva fila con una celda.</returns>
        internal TableRow CreateRow(TableCell cell)
        {
            TableRow fieldRow = new TableRow();
            fieldRow.Cells.Add(cell);
            return fieldRow;
        }

        /// <summary>
        /// Crea una celda con algunos atributos configurados
        /// </summary>
        /// <param name="columSpan">Numero de columnas que abarcara la celda.</param>
        /// <param name="width">Tamaño en porcentaje que medira la celda con respecto a la fila.</param>
        /// <param name="hAlign">Alinacion de la celda dentro de la fila.</param>
        /// <returns>Retorna una celda configurada.</returns>
        internal TableCell CreateCell(int columSpan, int width, HorizontalAlign hAlign)
        {
            TableCell fieldCell = new TableCell();
            fieldCell.ColumnSpan = columSpan;
            fieldCell.Width = new Unit(width, UnitType.Percentage);
            fieldCell.HorizontalAlign = hAlign;
            return fieldCell;
        }
        
    }
}
