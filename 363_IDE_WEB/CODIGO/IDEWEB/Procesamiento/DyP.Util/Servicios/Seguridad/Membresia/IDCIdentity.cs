
//@(#)SCADE2(W:SKDN08221CO2:SAT.DyP.Presentacion.Seguridad.Membresia:IDCIdentity:0:21/Mayo/2008[SAT.DyP.Presentacion.Seguridad.Membresia:1.1:21/Mayo/2008])
	
using System;
using System.Collections.Generic;
using System.Text;
using SAT.DyP.Presentacion.Seguridad.Membresia.wsIDCValidate;
using System.Configuration;

namespace SAT.DyP.Presentacion.Seguridad.Membresia
{
    public class IDCIdentity : System.Security.Principal.IIdentity
    {
        private string _rfc;
        private string _nombreRazonSocial;

        public IDCIdentity(string _rfc)
        {
            this._rfc = _rfc;
            CargarDatosUsuario();
        }
        public string AuthenticationType
        {
            get {return "SAT.DyP Authentication"; }
        }

        public bool IsAuthenticated
        {
            get { return true ; }
        }

        public string Name
        {
            get { return _nombreRazonSocial; }
        }

        public string UserID
        {
            get { return _rfc; }
        }

        private void CargarDatosUsuario()
        {
            idcValidation _user = new idcValidation();
            _user.Url = ConfigurationManager.AppSettings["autenticacion"];

            _nombreRazonSocial = _user.ObtenNombreRazonSocial(_rfc);
        }
    }
}
