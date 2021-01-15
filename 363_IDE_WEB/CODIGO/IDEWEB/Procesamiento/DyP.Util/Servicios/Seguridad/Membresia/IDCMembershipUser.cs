
//@(#)SCADE2(W:SKDN08221CO2:SAT.DyP.Presentacion.Seguridad.Membresia:IDCMembershipUser:0:21/Mayo/2008[SAT.DyP.Presentacion.Seguridad.Membresia:1.1:21/Mayo/2008])
	
using System;
using System.Collections.Generic;
using System.Text;
using SAT.DyP.Presentacion.Seguridad.Membresia.wsIDCValidate;

namespace SAT.DyP.Presentacion.Seguridad.Membresia
{
    public class IDCMembershipUser:System.Web.Security.MembershipUser 
    {
        private string razonSocial;
        private string password;
        private string rfc;
        private bool _tieneFiel;
        private string _perfil;

        public string RFC
        {
            get { return rfc.ToUpper(); }
            set { rfc = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string RazonSocial
        {
            get { return razonSocial; }
            set { razonSocial = value; }
        }

        public bool TieneFIEL
        {
            get { return _tieneFiel; }
            set { _tieneFiel = value; }
        }

        public string Perfil
        {
            get { return _perfil; }
            set { _perfil = value; }
        }


        public IDCMembershipUser(string providername, string rfc, string razonSocial, string mail, string password):
            base(providername, razonSocial, rfc, mail,string.Empty, string.Empty, true,false, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue,DateTime.MinValue, DateTime.MinValue)
        {
            
            this.razonSocial =   razonSocial;
            this.password = password;
            this.rfc = rfc;
            base.Email = mail;
        }
    }
}
