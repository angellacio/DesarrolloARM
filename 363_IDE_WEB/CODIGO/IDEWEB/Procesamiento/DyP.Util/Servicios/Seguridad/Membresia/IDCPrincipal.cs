
//@(#)SCADE2(W:SKDN08221CO2:SAT.DyP.Presentacion.Seguridad.Membresia:IDCPrincipal:0:21/Mayo/2008[SAT.DyP.Presentacion.Seguridad.Membresia:1.1:21/Mayo/2008])
	
using System;
using System.Collections.Generic;
using System.Text;

namespace SAT.DyP.Presentacion.Seguridad.Membresia
{
    public class IDCPrincipal : System.Security.Principal.IPrincipal
    {
        protected System.Security.Principal.IIdentity _identity;
        List<string> rolList = new List<string>();

        public bool IsInRole(string _role)
        {            
            return rolList.Contains(_role);
        }

        public IDCPrincipal(string _rfc)
        {
            _identity = new IDCIdentity(_rfc.ToUpper());
        }

        public IDCPrincipal(string rfc, string roles):this(rfc)
        {
            if (!string.IsNullOrEmpty(roles))
            {
                string[] roleArray = roles.Split(',');
                foreach (string rol in roleArray)
                {
                    if (!string.IsNullOrEmpty(rol))
                    {
                        rolList.Add(rol);
                    }
                }
            }
        }

        public System.Security.Principal.IIdentity Identity
        {
            get { return _identity; }
            set { _identity = value; }
        }
    }
}
