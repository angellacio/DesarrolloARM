using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace SAT.DyP.Presentacion.Seguridad.Autenticacion
{
    [Serializable]
    public class UserResponse
    {
        private string mail;

        public string Mail
        {
            get { return mail; }
            set { mail = value; }
        }
        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        private string razonSocial;

        public string RazonSocial
        {
            get { return razonSocial; }
            set { razonSocial = value; }
        }

        public UserResponse(string mail, string password, string razonSocial)
        {
            this.mail = mail;
            this.password = password;
            this.razonSocial = razonSocial;
        }
        public UserResponse()
        { }
    }
}