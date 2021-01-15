
//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Negocio.Comun.Tipos:EntidadReceptora:0:21/Mayo/2008[SAT.DyP.Negocio.Comun.Tipos:1.0:21/Mayo/2008])
	
using System;
using System.Collections.Generic;
using System.Text;

namespace SAT.DyP.Negocio.Comun.Tipos
{
    public class EntidadReceptora
    {
        #region Instance members
        private int _idEntidad;
        public int IdEntidad
        {
            get { return _idEntidad; }
        }
        private string _nombre;

        public string Nombre
        {
            get { return _nombre; }
        }

        public EntidadReceptora(int idEntidad, string nombre)
        {
            _idEntidad = idEntidad;
            _nombre = nombre;
        }
        #endregion

        public static EntidadReceptora Internet = new EntidadReceptora(19080, "INTERNET");
    }
}
