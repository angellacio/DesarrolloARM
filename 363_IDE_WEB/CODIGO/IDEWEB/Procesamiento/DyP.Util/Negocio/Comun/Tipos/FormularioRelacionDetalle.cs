
//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Negocio.Comun.Tipos:FormularioRelacionDetalle:0:21/Mayo/2008[SAT.DyP.Negocio.Comun.Tipos:1.0:21/Mayo/2008])
	
using System;
using System.Collections.Generic;
using System.Text;

namespace SAT.DyP.Negocio.Comun.Tipos
{
    /// <summary>
    /// Clase que representa los registros válidos del formulario
    /// </summary>
    [Serializable]
    public class FormularioRelacionDetalle
    {
        private int _clave;
        public int Clave
        {
            get { return this._clave; }
            set { this._clave = value; }
        }

        private char _tipoDato;
        public char TipoDato
        {
            get { return this._tipoDato; }
            set { this._tipoDato = value; }
        }
    }
}
