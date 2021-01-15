
//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Negocio.Comun.Procesos:AlmacenarBitacora:0:21/Mayo/2008[SAT.DyP.Negocio.Comun.Procesos:1.1:21/Mayo/2008])
	
using System;
using System.Collections.Generic;
using System.Text;
using SAT.DyP.Negocio.Comun.Tipos;
using SAT.DyP.Util.Types;

namespace SAT.DyP.Negocio.Comun.Procesos
{
    /// <summary>
    /// Clase abstracta del almacenamiento de bitácora
    /// </summary>
    [Serializable]
    public abstract class AlmacenarBitacora
    {
        public abstract bool Execute(Bitacora registroBitacora);        
    }
}
