
//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Negocio.Comun.Tipos:Contribuyente:0:21/Mayo/2008[SAT.DyP.Negocio.Comun.Tipos:1.0:21/Mayo/2008])
	
using System;
using System.Collections.Generic;
using System.Text;

namespace SAT.DyP.Negocio.Comun.Tipos
{
    /// <summary>
    /// Clase que representa la entidad Contribuyente
    /// </summary>
    [Serializable]
    public class Contribuyente
    {
        private string _rfc;
        public string Rfc
        {
            get { return _rfc; }
            set { _rfc = value; }
        }

        private string _razonSocial;
        public string RazonSocial
        {
            get { return _razonSocial; }
            set { _razonSocial = value; }
        }

        private string _tipoContribuyente;
        public string TipoContribuyente
        {
            get { return _tipoContribuyente; }
            set { _tipoContribuyente = value; }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private string _rfcRepresentanteLegal;
        public string RfcRepresentanteLegal
        {
            get { return _rfcRepresentanteLegal; }
            set { _rfcRepresentanteLegal = value; }
        }

        private string _representanteLegal;
        public string RepresentanteLegal
        {
            get { return _representanteLegal; }
            set { _representanteLegal = value; }
        }

        private string _clave;
        public string Clave
        {
            get { return _clave; }
            set { _clave = value; }
        }

        private int _estadoClave;
        public int EstadoClave
        {
            get { return _estadoClave; }
            set { _estadoClave = value; }
        }
    }
}
