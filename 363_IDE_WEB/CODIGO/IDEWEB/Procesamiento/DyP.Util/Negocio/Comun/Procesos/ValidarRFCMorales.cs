
//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Negocio.Comun.Procesos:ValidarRFCMorales:0:21/Mayo/2008[SAT.DyP.Negocio.Comun.Procesos:1.1:21/Mayo/2008])
	
using System;
using System.Collections.Generic;
using System.Text;
using SAT.DyP.Negocio.Comun.Tipos;

namespace SAT.DyP.Negocio.Comun.Procesos
{
    [Serializable]
    public class ValidarRFCMorales
    {
        /// <summary>
        /// Valida RFC para personas Morales
        /// </summary>
        /// <param name="rfc">Cadena RFC</param>
        public RFC Execute(string rfc)
        {
            return new RFCPersonaMoral(rfc);
        }
    }
}
