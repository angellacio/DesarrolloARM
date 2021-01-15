
//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Negocio.Comun.Procesos:AlmacenarDocumentoExcepcion:0:21/Mayo/2008[SAT.DyP.Negocio.Comun.Procesos:1.1:21/Mayo/2008])
	
using System;
using System.Collections.Generic;
using System.Text;
using SAT.DyP.Util.Types;

namespace SAT.DyP.Negocio.Comun.Procesos
{
    /// <summary>
    /// Excepción al generar un documento
    /// </summary>
    [Serializable]
    public class AlmacenarDocumentoExcepcion : BusinessException
    {
        public AlmacenarDocumentoExcepcion(string Message)
            : base(Message)
        {
        }

        public AlmacenarDocumentoExcepcion(string Message, Exception inner)
            : base(Message, inner)
        {
        }
    }
}
