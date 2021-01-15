//@(#)SCADE2(W:SKDN09071PC4:Sat.Scade.Net.IDE.Presentacion.Web:LoginNewTemplate:0:11/Febrero/2009[Sat.Scade.Net.IDE.Presentacion.Web:0:11/Febrero/2009]) 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sat.Scade.Net.IDE.Presentacion.Web
{
    public class LoginNewTemplate : ITemplate
    {
        private Table ContainerExternal;
        private Table container;
        private LoginCiec owner;

        public System.Drawing.Color ColorFont
        {
            get { return this.container.ForeColor; }
            set { this.container.ForeColor = value; }
        }

        public string UlrImage
        {            
            set { this.ContainerExternal.BackImageUrl = value; }
        }

        public LoginNewTemplate(LoginCiec owner)
        {
            this.owner = owner;
            this.ContainerExternal = new Table();
            this.container = new Table();
        }

        void CreateControls(Sat.Scade.Net.IDE.Presentacion.Web.LoginContainer loginContainer)
        {
            string uniqueID = this.owner.UniqueID;
            bool flag = true;

            loginContainer.TitleLabel = new Label();
            loginContainer.TitleLabel.ID = Utility.ID.Title;
            loginContainer.TitleLabel.Text = Utility.Text.Title;
            loginContainer.TitleLabel.Font.Size = new FontUnit(14, UnitType.Pixel);
            loginContainer.TitleLabel.Font.Bold = true;

            loginContainer.UserNameTextBox = new TextBox();
            loginContainer.UserNameTextBox.ID = Utility.ID.UserName;

            loginContainer.PasswordTextBox = new TextBox();
            loginContainer.PasswordTextBox.ID = Utility.ID.Password;
            loginContainer.PasswordTextBox.TextMode = TextBoxMode.Password;
            
            loginContainer.UserNameLabel = new Label();
            loginContainer.UserNameLabel.ID = Utility.ID.UserNameLabel;
            loginContainer.UserNameLabel.Text = Utility.Text.UserName;
            loginContainer.UserNameLabel.AssociatedControlID = loginContainer.UserNameTextBox.ID;

            loginContainer.PasswordLabel = new Label();
            loginContainer.PasswordLabel.ID = Utility.ID.PasswordLabel;
            loginContainer.PasswordLabel.Text = Utility.Text.Password;
            loginContainer.PasswordLabel.AssociatedControlID = loginContainer.PasswordTextBox.ID;            
            
            loginContainer.UserNameRequired = new RequiredFieldValidator();
            loginContainer.UserNameRequired.ID = Utility.ID.UserNameRequired;
            loginContainer.UserNameRequired.ValidationGroup = uniqueID;
            loginContainer.UserNameRequired.ControlToValidate = loginContainer.UserNameTextBox.ID;
            loginContainer.UserNameRequired.Text = Utility.Text.Required;
            loginContainer.UserNameRequired.Font.Size = new FontUnit(11, UnitType.Pixel);
            loginContainer.UserNameRequired.Display = ValidatorDisplay.Dynamic;            
            loginContainer.UserNameRequired.Enabled = flag;
            loginContainer.UserNameRequired.Visible = flag;

            loginContainer.PasswordRequired = new RequiredFieldValidator();
            loginContainer.PasswordRequired.ID = Utility.ID.PasswordRequired;
            loginContainer.PasswordRequired.ValidationGroup = uniqueID;
            loginContainer.PasswordRequired.ControlToValidate = loginContainer.PasswordTextBox.ID;
            loginContainer.PasswordRequired.Text = Utility.Text.Required;
            loginContainer.PasswordRequired.Font.Size = new FontUnit(11, UnitType.Pixel);
            loginContainer.PasswordRequired.Display = ValidatorDisplay.Dynamic;
            loginContainer.PasswordRequired.Enabled = flag;
            loginContainer.PasswordRequired.Visible = flag;
                            
            loginContainer.LoggInButton = new Button();
            loginContainer.LoggInButton.ID = Utility.ID.Loggin;
            loginContainer.LoggInButton.CommandName = System.Web.UI.WebControls.Login.LoginButtonCommandName;
            loginContainer.LoggInButton.Text = Utility.Text.Entrar;
            loginContainer.LoggInButton.ValidationGroup = uniqueID;

            loginContainer.FielButton = new Button();
            loginContainer.FielButton.ID = Utility.ID.FielLinkButton;
            loginContainer.FielButton.Text = Utility.Text.Fiel;
            loginContainer.FielButton.CommandName = Utility.CommandName.LoginTypeCiec;            

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
            this.container.Width = new Unit(520);
            this.container.Height = new Unit(220);                       

            TableCell CellHeaderContainer = this.CreateCell(3,100,HorizontalAlign.Center,loginContainer.TitleLabel);
            TableRow RowHeaderContainer = CreateRow(CellHeaderContainer);

            TableCell CellUserContainer1 = this.CreateCell(1, 32, HorizontalAlign.Right, null);
            TableCell CellUserContainer2 = this.CreateCell(1, 28, HorizontalAlign.Right, loginContainer.UserNameLabel);
            TableCell CellUserContainer3 = this.CreateCell(1, 40, HorizontalAlign.Right, loginContainer.UserNameTextBox);
            TableRow RowUserContainer = this.CreateRow(CellUserContainer1, CellUserContainer2, CellUserContainer3);
            
            TableCell CellPasswordContainer1 = this.CreateCell(1, 32, HorizontalAlign.Center, null);
            TableCell CellPasswordContainer2 = this.CreateCell(1,28,HorizontalAlign.Right, loginContainer.PasswordLabel);            
            TableCell CellPasswordContainer3 = this.CreateCell(1,40,HorizontalAlign.Left,loginContainer.PasswordTextBox);
            TableRow RowPasswordContainer = this.CreateRow(CellPasswordContainer1, CellPasswordContainer2, CellPasswordContainer3);
            
            TableCell CellRequiredContainer3 = this.CreateCell(3,100,HorizontalAlign.Right,loginContainer.UserNameRequired, new LiteralControl("<br/>"), loginContainer.PasswordRequired);
            TableRow RowRequiredContainer = this.CreateRow(CellRequiredContainer3);

            TableCell CellFailureContainer3 = this.CreateCell(3, 100, HorizontalAlign.Right,loginContainer.FailureLabel);
            TableRow RowFailureContainer = this.CreateRow(CellFailureContainer3);

            TableCell CellLoginContainer2 = this.CreateCell(1,40,HorizontalAlign.Center, null);
            TableCell CellLoginContainer4 = this.CreateCell(1, 40,HorizontalAlign.Right,null);
            TableCell CellLoginContainer3 = this.CreateCell(1, 20, HorizontalAlign.Right, loginContainer.FielButton, new LiteralControl("&nbsp;"), loginContainer.LoggInButton);
            TableRow RowLoginContainer = this.CreateRow(CellLoginContainer2, CellLoginContainer4, CellLoginContainer3);

            //TableCell CellCiecContainer2 = this.CreateCell(1, 40, HorizontalAlign.Center, loginContainer.CiecFLink);
            //TableCell CellCiecContainer3 = this.CreateCell(2, 60, HorizontalAlign.Right, null);
            //TableRow RowCiecContainer = this.CreateRow(CellCiecContainer2, CellCiecContainer3);

            //TableCell CellMailContainer2 = this.CreateCell(1, 40, HorizontalAlign.Center, loginContainer.FielLabel);
            //TableCell CellMailContainer3 = this.CreateCell(2, 60, HorizontalAlign.Center, loginContainer.MailCheckBox);
            //TableRow RowMailContainer = this.CreateRow(CellMailContainer2, CellMailContainer3);

            // R2. Se crean los Elementos para Incorporar el Email Adicional
            TableCell CellMailUpdateLink3 = this.CreateCell(4, 100, HorizontalAlign.Center, new LiteralControl("<br/><br/>"), loginContainer.MailUpdateLink);
            TableRow RowMailUpdateLink = this.CreateRow(CellMailUpdateLink3);


            //TableCell CellFielContainer2 = this.CreateCell(1, 40, HorizontalAlign.Center, loginContainer.FielLabel, loginContainer.FielLinkButton);
            //TableCell CellFielContainer3 = this.CreateCell(2, 60, HorizontalAlign.Center, null);
            //TableRow RowFielContainer = this.CreateRow(CellFielContainer2, CellFielContainer3);
            
            this.container.Rows.Add(RowHeaderContainer);
            this.container.Rows.Add(RowUserContainer);
            this.container.Rows.Add(RowPasswordContainer);
            this.container.Rows.Add(RowRequiredContainer);
            this.container.Rows.Add(RowFailureContainer);
            //this.container.Rows.Add(RowMailContainer);
            this.container.Rows.Add(RowLoginContainer);
			
			// R2. Se Agrega al Contenedor el Email Adicional
            this.container.Rows.Add(RowMailUpdateLink);

			
            //this.container.Rows.Add(RowCiecContainer);
            //this.container.Rows.Add(RowFielContainer);
                       
            this.ContainerExternal.ID = Utility.ID.Principal;
            this.ContainerExternal.Style[Utility.Style.background_repeat] = Utility.Style.no_repeat;
            this.ContainerExternal.Style[Utility.Style.background_position] = Utility.Style.top;
            this.ContainerExternal.Width = new Unit(572);
            this.ContainerExternal.Height = new Unit(310);
            this.container.ForeColor = System.Drawing.Color.White;
            this.ContainerExternal.Font.Name = Utility.Style.font_name;
            this.ContainerExternal.Font.Size = new FontUnit(12, UnitType.Pixel);

            //this.ContainerExternal = CreateChildTable(this.owner.ConvertingToTemplate);
            TableCell CellPrincipal = this.CreateCell(1, 100, HorizontalAlign.Center, this.container);
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