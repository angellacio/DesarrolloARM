
//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Negocio.Comun.Procesos:EliminarFolioExcepcion:0:21/Mayo/2008[SAT.DyP.Negocio.Comun.Procesos:1.1:21/Mayo/2008])
	
using System;
using System.Collections.Generic;
using System.Text;
using SAT.DyP.Util.Types;

namespace SAT.DyP.Negocio.Comun.Procesos
{
    /// <summary>
    /// Excepción por Eliminiación de Folio
    /// </summary>
    [Serializable]
    public class EliminarFolioExcepcion : BusinessException
    {
        public EliminarFolioExcepcion(string Message)
            : base(Message)
        {
        }

        public EliminarFolioExcepcion(string Message, Exception inner)
            : base(Message, inner)
        {
        }
    }
}
