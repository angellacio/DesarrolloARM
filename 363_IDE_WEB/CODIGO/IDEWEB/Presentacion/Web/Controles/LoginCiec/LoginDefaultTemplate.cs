//@(#)SCADE2(W:SKDN09071PC4:Sat.Scade.Net.IDE.Presentacion.Web:LoginDefaultTemplate:0:11/Febrero/2009[Sat.Scade.Net.IDE.Presentacion.Web:0:11/Febrero/2009]) 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sat.Scade.Net.IDE.Presentacion.Web
{
    public class LoginDefaultTemplate: ITemplate
    {
        private LoginCiec owner;
        private Table container;
        private Table ContainerExternal;
        
        public Color ColorFont
        {
            get { return this.container.ForeColor; }
            set { this.container.ForeColor = value; }
        }

        public string UlrImage
        {
            set { string UrlImage = value; }
        }

        public LoginDefaultTemplate(LoginCiec owner)
        {
            this.owner = owner;
            this.container = new Table();
            this.ContainerExternal = new Table();            
        }

        void CreateControls(Sat.Scade.Net.IDE.Presentacion.Web.LoginContainer loginContainer)
        {
            bool flag = true;
            string uniqueID = this.owner.UniqueID;            

            loginContainer.TitleLabel = new Label();
            loginContainer.TitleLabel.ID = Utility.ID.Title;            
            loginContainer.TitleLabel.ForeColor = Color.White;                        

            loginContainer.UserNameTextBox = new TextBox();
            loginContainer.UserNameTextBox.ID = Utility.ID.UserName;
            loginContainer.UserNameTextBox.Width = new Unit(150,UnitType.Pixel);

            loginContainer.PasswordTextBox = new TextBox();
            loginContainer.PasswordTextBox.ID = Utility.ID.Password;
            loginContainer.PasswordTextBox.Width = new Unit(150, UnitType.Pixel);
            loginContainer.PasswordTextBox.TextMode = TextBoxMode.Password;

            loginContainer.UserNameLabel = new Label();
            loginContainer.UserNameLabel.ID = Utility.ID.UserNameLabel;
            loginContainer.UserNameLabel.Text = Utility.Text.UserName;
            loginContainer.UserNameLabel.Font.Bold = true;
            loginContainer.UserNameLabel.AssociatedControlID = loginContainer.UserNameTextBox.ID;

            loginContainer.PasswordLabel = new Label();
            loginContainer.PasswordLabel.ID = Utility.ID.PasswordLabel;
            loginContainer.PasswordLabel.Text = Utility.Text.Password;
            loginContainer.PasswordLabel.Font.Bold = true;
            loginContainer.PasswordLabel.AssociatedControlID = loginContainer.PasswordTextBox.ID;

            loginContainer.UserNameRequired = new RequiredFieldValidator();
            loginContainer.UserNameRequired.ID = Utility.ID.UserNameRequired;
            loginContainer.UserNameRequired.ValidationGroup = uniqueID;
            loginContainer.UserNameRequired.ControlToValidate = loginContainer.UserNameTextBox.ID;
            loginContainer.UserNameRequired.Text = Utility.Text.Required;
            loginContainer.UserNameRequired.Display = ValidatorDisplay.Dynamic;
            loginContainer.UserNameRequired.Enabled = flag;
            loginContainer.UserNameRequired.Visible = flag;

            loginContainer.PasswordRequired = new RequiredFieldValidator();
            loginContainer.PasswordRequired.ID = Utility.ID.PasswordRequired;
            loginContainer.PasswordRequired.ValidationGroup = uniqueID;
            loginContainer.PasswordRequired.ControlToValidate = loginContainer.PasswordTextBox.ID;
            loginContainer.PasswordRequired.Text = Utility.Text.Required;
            loginContainer.PasswordRequired.Display = ValidatorDisplay.Dynamic;
            loginContainer.PasswordRequired.Enabled = flag;
            loginContainer.PasswordRequired.Visible = flag;

            loginContainer.LoggInButton = new Button();
            loginContainer.LoggInButton.ID = Utility.ID.Loggin;
            loginContainer.LoggInButton.CommandName = System.Web.UI.WebControls.Login.LoginButtonCommandName;
            loginContainer.LoggInButton.Text = Utility.Text.Aceptar;            
            loginContainer.LoggInButton.Font.Name = Utility.Style.font_ArialSans;
            loginContainer.LoggInButton.Font.Size = new FontUnit(13, UnitType.Pixel);
            loginContainer.LoggInButton.Font.Bold = true;
            loginContainer.LoggInButton.BackColor = Color.FromArgb(212,226,239);
            loginContainer.LoggInButton.BorderColor = Color.FromArgb(122,138,153);
            loginContainer.LoggInButton.BorderStyle = BorderStyle.Solid;
            loginContainer.LoggInButton.ValidationGroup = uniqueID;
   
            loginContainer.FielButton = new Button();
            loginContainer.FielButton.ID = Utility.ID.FielLinkButton;
            loginContainer.FielButton.Text = Utility.Text.Fiel;
            loginContainer.FielButton.CommandName = Utility.CommandName.LoginTypeFiel;

            //loginContainer.CiecFLink = new HyperLink();
            //loginContainer.CiecFLink.ID = Utility.ID.CiecFLink;
            //loginContainer.CiecFLink.Text = Utility.Text.Ciecf;
            //loginContainer.CiecFLink.NavigateUrl = Utility.Url.CiecF;
            
            loginContainer.MailCheckBox = new CheckBox();
            loginContainer.MailCheckBox.ID = Utility.ID.Mail;
            loginContainer.MailCheckBox.Text = Utility.Text.Mail;

            // R2. Se Inicializan el Objeto MailUpdateLink
            loginContainer.MailUpdateLink = new HyperLink();
            loginContainer.MailUpdateLink.ID = Utility.ID.MailUpdateLink;
            loginContainer.MailUpdateLink.Text = Utility.Text.Mail;
            
            //loginContainer.FielLabel = new Label();
            //loginContainer.FielLabel.ID = Utility.ID.FielTitle;
            //loginContainer.FielLabel.Text = Utility.Text.FielTitle;

            loginContainer.FailureLabel = new Label();
            loginContainer.FailureLabel.ID = Utility.ID.FailureText;            
        }

        private void LayoutControls(Sat.Scade.Net.IDE.Presentacion.Web.LoginContainer loginContainer)
        {
            this.container.ID = Utility.ID.Container;
            this.container.Width = new Unit(380);
            this.container.Height = new Unit(200);

            TableCell CellHeaderContainer = this.CreateCell(4, 100, HorizontalAlign.Center, loginContainer.TitleLabel);
            CellHeaderContainer.Height = new Unit(5, UnitType.Pixel);
            CellHeaderContainer.BorderStyle = BorderStyle.None;
            CellHeaderContainer.BackColor = Color.FromArgb(0, 0, 192);
            TableRow RowHeaderContainer = this.CreateRow(CellHeaderContainer);

            TableCell CellUserContainer1 = this.CreateCell(1, 20, HorizontalAlign.Right, new LiteralControl("&nbsp;"));
            TableCell CellUserContainer2 = this.CreateCell(1, 30, HorizontalAlign.Right, loginContainer.UserNameLabel);            
            TableCell CellUserContainer3 = this.CreateCell(1, 30, HorizontalAlign.Left, loginContainer.UserNameTextBox);
            TableCell CellUserContainer4 = this.CreateCell(1, 20, HorizontalAlign.Right, null);
            TableRow RowUserContainer = this.CreateRow(CellUserContainer1, CellUserContainer2, CellUserContainer3, CellUserContainer4);

            TableCell CellPasswordContainer2 = this.CreateCell(2, 50, HorizontalAlign.Right, loginContainer.PasswordLabel);
            TableCell CellPasswordContainer3 = this.CreateCell(2, 50, HorizontalAlign.Left, loginContainer.PasswordTextBox);
            TableRow RowPasswordContainer = this.CreateRow(CellPasswordContainer2, CellPasswordContainer3);

            TableCell CellRequiredContainer4 = this.CreateCell(4, 100, HorizontalAlign.Center, loginContainer.UserNameRequired, new LiteralControl("<br/>"), loginContainer.PasswordRequired);            
            TableRow RowRequiredContainer = this.CreateRow(CellRequiredContainer4);

            TableCell CellFailureContainer4 = this.CreateCell(4, 100, HorizontalAlign.Center, loginContainer.FailureLabel);
            TableRow RowFailureContainer = this.CreateRow(CellFailureContainer4);

            TableCell CellLoginContainer3 = null;
            if (owner.MedioPresentacion == MedioPresentacion.Internet) CellLoginContainer3 = this.CreateCell(4, 100, HorizontalAlign.Center, loginContainer.FielButton, new LiteralControl("&nbsp;"), loginContainer.LoggInButton);
            else CellLoginContainer3 = this.CreateCell(4, 100, HorizontalAlign.Center, loginContainer.LoggInButton);
            TableRow RowLoginContainer = this.CreateRow(CellLoginContainer3);

            //TableCell CellCiecFContainer3 = this.CreateCell(4, 100, HorizontalAlign.Center, new LiteralControl("<br/><br/>"), loginContainer.FielButton);
            //TableRow RowCiecFContainer = this.CreateRow(CellCiecFContainer3);

            TableCell CellMailContainer3 = this.CreateCell(4, 100, HorizontalAlign.Center, loginContainer.MailCheckBox, new LiteralControl("<br/><br/>"));
            TableRow RowMailContainer = this.CreateRow(CellMailContainer3);

            // R2. Se crean los Elementos para Incorporar el Email Adicional
            TableCell CellMailUpdateLink3 = this.CreateCell(4, 100, HorizontalAlign.Center, new LiteralControl("<br/><br/>"), loginContainer.MailUpdateLink);
            TableRow RowMailUpdateLink = this.CreateRow(CellMailUpdateLink3);


            //TableCell CellFielContainer3 = this.CreateCell(4, 100, HorizontalAlign.Center, new LiteralControl("<br/><br/>"), loginContainer.FielLabel, loginContainer.FielLinkButton);
            //TableRow RowFielContainer = this.CreateRow(CellFielContainer3);

            this.container.Rows.Add(RowHeaderContainer);
            this.container.Rows.Add(RowUserContainer);
            this.container.Rows.Add(RowPasswordContainer);
            this.container.Rows.Add(RowRequiredContainer);
            this.container.Rows.Add(RowFailureContainer);
            this.container.Rows.Add(RowMailContainer);
            this.container.Rows.Add(RowLoginContainer);

            //this.container.Rows.Add(RowCiecFContainer);

            // R2. Se Agrega al Contenedor el Email Adicional
            this.container.Rows.Add(RowMailUpdateLink);

            //this.container.Rows.Add(RowFielContainer);

            this.ContainerExternal.ID = Utility.ID.Principal;
            this.ContainerExternal.Style[Utility.Style.background_repeat] = Utility.Style.no_repeat;
            this.ContainerExternal.Style[Utility.Style.background_position] = Utility.Style.top;
            this.ContainerExternal.Width = new Unit(480);
            //this.ContainerExternal.Height = new Unit(260);            
            this.ContainerExternal.Font.Name = Utility.Style.font_name;
            this.ContainerExternal.Font.Size = new FontUnit(13, UnitType.Pixel);

            //this.ContainerExternal = CreateChildTable(this.owner.ConvertingToTemplate);
            
            TableCell CellPrincipal = this.CreateCell(1,100,HorizontalAlign.Center, this.container);            
            TableRow RowPrincipal = this.CreateRow(CellPrincipal);
            RowPrincipal.VerticalAlign = VerticalAlign.Top;
            this.ContainerExternal.Rows.Add(RowPrincipal);

            loginContainer.LayoutTable = this.container;
            loginContainer.BorderTable = this.ContainerExternal;
            loginContainer.Controls.Add(this.ContainerExternal);
        }

        private TableRow CreateRow(params TableCell[] cells)
        {
            TableRow rowField = new TableRow();
            if (cells != null)
            {
                foreach (TableCell cell in cells)
                {
                    rowField.Cells.Add(cell);
                }
            }
            return rowField;
        }

        private TableCell CreateCell(int columnSpan, int width, HorizontalAlign hAlign, params Control[] controls)
        {
            TableCell cellField = new TableCell();
            cellField.ColumnSpan = columnSpan;
            cellField.HorizontalAlign = hAlign;
            cellField.Width = new Unit(width, UnitType.Percentage);

            if (controls != null)
            {
                foreach (Control ctrl in controls)
                {
                    cellField.Controls.Add(ctrl);
                }
            }
            return cellField;
        }

        #region ITemplate Members

        public void InstantiateIn(Control container)
        {
            Sat.Scade.Net.IDE.Presentacion.Web.LoginContainer loginContainer = (Sat.Scade.Net.IDE.Presentacion.Web.LoginContainer)container;
            this.CreateControls(loginContainer);
            this.LayoutControls(loginContainer);
        }

        #endregion
    }
}