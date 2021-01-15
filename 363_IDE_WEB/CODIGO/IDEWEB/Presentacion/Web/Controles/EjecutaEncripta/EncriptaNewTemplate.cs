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
    public class EncriptaNewTemplate : ITemplate
    {
        private Table ContainerExternal;
        private Table container;
        private EjecutaEncripta owner;

        public System.Drawing.Color ColorFont
        {
            get { return this.container.ForeColor; }
            set { this.container.ForeColor = value; }
        }

        public string UlrImage
        {            
            set { this.ContainerExternal.BackImageUrl = value; }
        }

        public EncriptaNewTemplate(EjecutaEncripta owner)
        {
            this.owner = owner;
            this.ContainerExternal = new Table();
            this.container = new Table();
        }

        void CreateControls(Sat.Scade.Net.IDE.Presentacion.Web.EncriptaContainer encriptaContainer)
        {
            string uniqueID = this.owner.UniqueID;
            bool flag = true;

            //encriptaContainer.TitleLabel = new Label();
            //encriptaContainer.TitleLabel.ID = Utility.ID.Title;
            //encriptaContainer.TitleLabel.Text = Utility.Text.Title;
            //encriptaContainer.TitleLabel.Font.Size = new FontUnit(14, UnitType.Pixel);
            //encriptaContainer.TitleLabel.Font.Bold = true;

            //encriptaContainer.UserNameTextBox = new TextBox();
            //encriptaContainer.UserNameTextBox.ID = Utility.ID.UserName;

            //encriptaContainer.PasswordTextBox = new TextBox();
            //encriptaContainer.PasswordTextBox.ID = Utility.ID.Password;
            //encriptaContainer.PasswordTextBox.TextMode = TextBoxMode.Password;
            
            //encriptaContainer.UserNameLabel = new Label();
            //encriptaContainer.UserNameLabel.ID = Utility.ID.UserNameLabel;
            //encriptaContainer.UserNameLabel.Text = Utility.Text.UserName;
            //encriptaContainer.UserNameLabel.AssociatedControlID = encriptaContainer.UserNameTextBox.ID;

            //encriptaContainer.PasswordLabel = new Label();
            //encriptaContainer.PasswordLabel.ID = Utility.ID.PasswordLabel;
            //encriptaContainer.PasswordLabel.Text = Utility.Text.Password;
            //encriptaContainer.PasswordLabel.AssociatedControlID = encriptaContainer.PasswordTextBox.ID;            
            
            //encriptaContainer.UserNameRequired = new RequiredFieldValidator();
            //encriptaContainer.UserNameRequired.ID = Utility.ID.UserNameRequired;
            //encriptaContainer.UserNameRequired.ValidationGroup = uniqueID;
            //encriptaContainer.UserNameRequired.ControlToValidate = encriptaContainer.UserNameTextBox.ID;
            //encriptaContainer.UserNameRequired.Text = Utility.Text.Required;
            //encriptaContainer.UserNameRequired.Font.Size = new FontUnit(11, UnitType.Pixel);
            //encriptaContainer.UserNameRequired.Display = ValidatorDisplay.Dynamic;            
            //encriptaContainer.UserNameRequired.Enabled = flag;
            //encriptaContainer.UserNameRequired.Visible = flag;

            //encriptaContainer.PasswordRequired = new RequiredFieldValidator();
            //encriptaContainer.PasswordRequired.ID = Utility.ID.PasswordRequired;
            //encriptaContainer.PasswordRequired.ValidationGroup = uniqueID;
            //encriptaContainer.PasswordRequired.ControlToValidate = encriptaContainer.PasswordTextBox.ID;
            //encriptaContainer.PasswordRequired.Text = Utility.Text.Required;
            //encriptaContainer.PasswordRequired.Font.Size = new FontUnit(11, UnitType.Pixel);
            //encriptaContainer.PasswordRequired.Display = ValidatorDisplay.Dynamic;
            //encriptaContainer.PasswordRequired.Enabled = flag;
            //encriptaContainer.PasswordRequired.Visible = flag;

            encriptaContainer.LoggInButton = new Button();
            encriptaContainer.LoggInButton.ID = Utility.ID.Loggin;
            encriptaContainer.LoggInButton.CommandName = System.Web.UI.WebControls.Login.LoginButtonCommandName;
            encriptaContainer.LoggInButton.Text = Utility.Text.Entrar;
            encriptaContainer.LoggInButton.ValidationGroup = uniqueID;

            encriptaContainer.FielLinkButton = new LinkButton();
            encriptaContainer.FielLinkButton.ID = Utility.ID.FielLinkButton;
            encriptaContainer.FielLinkButton.Text = Utility.Text.Encriptar;
            encriptaContainer.FielLinkButton.CommandName = Utility.CommandName.LoginTypeCiec;            

            //encriptaContainer.CiecFLink = new HyperLink();
            //encriptaContainer.CiecFLink.ID = Utility.ID.CiecFLink;
            //encriptaContainer.CiecFLink.Text = Utility.Text.Ciecf;
            //encriptaContainer.CiecFLink.NavigateUrl = Utility.Url.CiecF;

            //encriptaContainer.MailCheckBox = new CheckBox();
            //encriptaContainer.MailCheckBox.ID = Utility.ID.Mail;
            //encriptaContainer.MailCheckBox.Text = Utility.Text.Mail;

            //// R2. Se Inicializan el Objeto MailUpdateLink
            //encriptaContainer.MailUpdateLink = new HyperLink();
            //encriptaContainer.MailUpdateLink.ID = Utility.ID.MailUpdateLink;
            //encriptaContainer.MailUpdateLink.Text = Utility.Text.Mail;


            encriptaContainer.FielLabel = new Label();
            encriptaContainer.FielLabel.ID = Utility.ID.FielTitle;
            encriptaContainer.FielLabel.Text = Utility.Text.EncriptarTitle;

            //encriptaContainer.FailureLabel = new Label();
            //encriptaContainer.FailureLabel.ID = Utility.ID.FailureText;
        }

        private void LayoutControls(Sat.Scade.Net.IDE.Presentacion.Web.EncriptaContainer encriptaContainer)
        {
            this.container.ID = Utility.ID.Container;
            this.container.Width = new Unit(520);
            this.container.Height = new Unit(220);                       

            //TableCell CellHeaderContainer = this.CreateCell(3,100,HorizontalAlign.Center,encriptaContainer.TitleLabel);
            //TableRow RowHeaderContainer = CreateRow(CellHeaderContainer);

            //TableCell CellUserContainer1 = this.CreateCell(1, 32, HorizontalAlign.Right, null);
            //TableCell CellUserContainer2 = this.CreateCell(1, 28, HorizontalAlign.Right, encriptaContainer.UserNameLabel);
            //TableCell CellUserContainer3 = this.CreateCell(1, 40, HorizontalAlign.Right, encriptaContainer.UserNameTextBox);
            //TableRow RowUserContainer = this.CreateRow(CellUserContainer1, CellUserContainer2, CellUserContainer3);
            
            //TableCell CellPasswordContainer1 = this.CreateCell(1, 32, HorizontalAlign.Center, null);
            //TableCell CellPasswordContainer2 = this.CreateCell(1,28,HorizontalAlign.Right, encriptaContainer.PasswordLabel);            
            //TableCell CellPasswordContainer3 = this.CreateCell(1,40,HorizontalAlign.Left,encriptaContainer.PasswordTextBox);
            //TableRow RowPasswordContainer = this.CreateRow(CellPasswordContainer1, CellPasswordContainer2, CellPasswordContainer3);
            
            //TableCell CellRequiredContainer3 = this.CreateCell(3,100,HorizontalAlign.Right,encriptaContainer.UserNameRequired, new LiteralControl("<br/>"), encriptaContainer.PasswordRequired);
            //TableRow RowRequiredContainer = this.CreateRow(CellRequiredContainer3);

            //TableCell CellFailureContainer3 = this.CreateCell(3, 100, HorizontalAlign.Right,encriptaContainer.FailureLabel);
            //TableRow RowFailureContainer = this.CreateRow(CellFailureContainer3);

            //TableCell CellencriptaContainer2 = this.CreateCell(1,40,HorizontalAlign.Center, null);
            //TableCell CellencriptaContainer4 = this.CreateCell(1, 40,HorizontalAlign.Right,null);
            TableCell CellencriptaContainer3 = this.CreateCell(1, 20, HorizontalAlign.Right, encriptaContainer.LoggInButton);
            TableRow RowencriptaContainer = this.CreateRow(CellencriptaContainer3);
            //TableRow RowencriptaContainer = this.CreateRow(CellencriptaContainer2, CellencriptaContainer4, CellencriptaContainer3);

            //TableCell CellCiecContainer2 = this.CreateCell(1, 40, HorizontalAlign.Center, encriptaContainer.CiecFLink);
            TableCell CellCiecContainer3 = this.CreateCell(2, 60, HorizontalAlign.Right, null);
            //TableRow RowCiecContainer = this.CreateRow(CellCiecContainer2, CellCiecContainer3);
            TableRow RowCiecContainer = this.CreateRow( CellCiecContainer3);

            TableCell CellMailContainer2 = this.CreateCell(1, 40, HorizontalAlign.Center, encriptaContainer.FielLabel);
            //TableCell CellMailContainer3 = this.CreateCell(2, 60, HorizontalAlign.Center, encriptaContainer.MailCheckBox);
            //TableRow RowMailContainer = this.CreateRow(CellMailContainer2,CellMailContainer3);
            TableRow RowMailContainer = this.CreateRow(CellMailContainer2);

            // R2. Se crean los Elementos para Incorporar el Email Adicional
            //TableCell CellMailUpdateLink3 = this.CreateCell(4, 100, HorizontalAlign.Center, new LiteralControl("<br/><br/>"), encriptaContainer.MailUpdateLink);
            //TableRow RowMailUpdateLink = this.CreateRow(CellMailUpdateLink3);


            TableCell CellFielContainer2 = this.CreateCell(1, 40, HorizontalAlign.Center, encriptaContainer.FielLabel, encriptaContainer.FielLinkButton);
            TableCell CellFielContainer3 = this.CreateCell(2, 60, HorizontalAlign.Center, null);
            TableRow RowFielContainer = this.CreateRow(CellFielContainer2, CellFielContainer3);
            
            //this.container.Rows.Add(RowHeaderContainer);
            //this.container.Rows.Add(RowUserContainer);
            //this.container.Rows.Add(RowPasswordContainer);
            //this.container.Rows.Add(RowRequiredContainer);
            //this.container.Rows.Add(RowFailureContainer);
            this.container.Rows.Add(RowMailContainer);
            //this.container.Rows.Add(RowencriptaContainer);
			
            //// R2. Se Agrega al Contenedor el Email Adicional
            //this.container.Rows.Add(RowMailUpdateLink);

			
            this.container.Rows.Add(RowCiecContainer);
            this.container.Rows.Add(RowFielContainer);
                       
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

            encriptaContainer.LayoutTable = this.container;
            encriptaContainer.BorderTable = this.ContainerExternal;
            encriptaContainer.Controls.Add(this.ContainerExternal);
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
            Sat.Scade.Net.IDE.Presentacion.Web.EncriptaContainer encriptaContainer = (Sat.Scade.Net.IDE.Presentacion.Web.EncriptaContainer)container;
            this.CreateControls(encriptaContainer);
            this.LayoutControls(encriptaContainer);
        }

        #endregion
    }
}