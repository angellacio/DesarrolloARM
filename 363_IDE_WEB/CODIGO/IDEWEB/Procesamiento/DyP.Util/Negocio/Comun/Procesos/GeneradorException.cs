
//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Negocio.Comun.Procesos:GeneradorExcepcion:0:21/Mayo/2008[SAT.DyP.Negocio.Comun.Procesos:1.1:21/Mayo/2008])
	
using System;
using System.Collections.Generic;
using System.Text;
using SAT.DyP.Util.Types;

namespace SAT.DyP.Negocio.Comun.Procesos
{
    /// <summary>
    /// Generación de Excepciones de Negocio
    /// </summary>
    [Serializable]
    public class GeneradorException:BusinessException
    {
        public GeneradorException(string Message):base(Message)
        {
        }

        public GeneradorException(string Message, Exception innerException)
            : base(Message, innerException)
        {
        }
    }
}
