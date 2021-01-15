
//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Negocio.Comun.Tipos:MensajesBitacora:0:21/Mayo/2008[SAT.DyP.Negocio.Comun.Tipos:1.0:21/Mayo/2008])
	
using System;
using System.Collections.Generic;
using System.Text;

namespace SAT.DyP.Negocio.Comun.Tipos
{
    /// <summary>
    /// Enumeración que representa el Identificador de mensaje
    /// que se registra en la bitacora
    /// </summary>
    [Serializable]
    public enum MensajesBitacora:int
    {
        ArchivoRecibidoEnProceso=0,
        ArchivoDesencriptadoAlmacenadoXML=1,
        DeclaracionValidacionSintaxis=2,
        AlmacenadaEnLocal=3,
        AlmacenadaEnCentral=4,
        ArchivoNoProcesado=5,
        AlmacenadaEnHistorico=6,
        DeclaracionDePrueba=9
    }
}
