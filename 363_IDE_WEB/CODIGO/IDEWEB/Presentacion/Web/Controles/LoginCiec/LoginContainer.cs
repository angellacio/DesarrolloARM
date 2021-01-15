//@(#)SCADE2(W:SKDN09071PC4:Sat.Scade.Net.IDE.Presentacion.Web:LoginContainer:0:11/Febrero/2009[Sat.Scade.Net.IDE.Presentacion.Web:0:11/Febrero/2009]) 
using System;
using System.Collections.Generic;
using System.Text;

using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sat.Scade.Net.IDE.Presentacion.Web
{
    internal sealed class LoginContainer: GenericContainer<LoginCiec>
    {
        //private Label fielLabel;
        private Label titleLabel;        
        private Label userNameLabel;        
        private Label failureLabel;
        private Label passwordLabel;
        private TextBox userNameTextBox;
        private TextBox passwordTextBox;
        private Button loggInButton;
        private Button fielButton;
        //private HyperLink ciecFLink;
        private CheckBox mailCheckBox;        
        private RequiredFieldValidator passwordRequired;
        private RequiredFieldValidator userNameRequired;

        // R2. Declaramos un Objeto de Tipo Link
        private HyperLink mailUpdateLink;


        public LoginContainer(LoginCiec owner):base(owner)
        {            
        }

        protected override bool ConvertingToTemplate
        {
            get { return base.Owner.ConvertingToTemplate; }
        }

        internal CheckBox MailCheckBox
        {
            get { return mailCheckBox; }
            set { mailCheckBox = value; }
        }

        //internal Label FielLabel
        //{
        //    get { return fielLabel; }
        //    set { fielLabel = value; }
        //}

        internal Label TitleLabel
        {
            get { return titleLabel; }
            set { titleLabel = value; }
        }

        internal Button FielButton
        {
            get { return fielButton; }
            set { fielButton = value; }
        }

        //internal HyperLink CiecFLink
        //{
        //    get { return ciecFLink; }
        //    set { ciecFLink = value; }
        //}

        internal Label UserNameLabel
        {
            get { return userNameLabel; }
            set { userNameLabel = value; }
        }

        internal Label FailureLabel
        {
            get { return failureLabel; }
            set { failureLabel = value; }
        }

        internal Label PasswordLabel
        {
            get { return passwordLabel; }
            set { passwordLabel = value; }
        }

        internal TextBox UserNameTextBox
        {
            get { return userNameTextBox; }
            set { userNameTextBox = value; }
        }

        internal TextBox PasswordTextBox
        {
            get { return passwordTextBox; }
            set { passwordTextBox = value; }
        }

        internal Button LoggInButton
        {
            get { return loggInButton; }
            set { loggInButton = value; }
        }                

        internal RequiredFieldValidator PasswordRequired
        {
            get { return passwordRequired; }
            set { passwordRequired = value; }
        }

        internal RequiredFieldValidator UserNameRequired
        {
            get { return userNameRequired; }
            set { userNameRequired = value; }
        }

        // R2. Agregamos la Propiedad MailUpdateLink al Objeto LoginContainer
        internal HyperLink MailUpdateLink
        {
            get { return mailUpdateLink; }
            set { mailUpdateLink = value; }
        }
    }
}