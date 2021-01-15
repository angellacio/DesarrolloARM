
//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Negocio.Comun.Tipos:MensajesNotificacion:0:21/Mayo/2008[SAT.DyP.Negocio.Comun.Tipos:1.0:21/Mayo/2008])
	
using System;
using System.Collections.Generic;
using System.Text;

namespace SAT.DyP.Negocio.Comun.Tipos
{
    /// <summary>
    /// Enumeracion que representa el identificador de mensaje
    /// que representa la plantilla de notificacion por correo
    /// </summary>
    [Serializable]
    public enum MensajesNotificacion : int
    {
        AnualDeclaracionRecibida = 0,
        AcuseReciboInsercionDucto = 1,//Indica que la informacion se recibio en el ducto
        ErrorDeclaracionGenerico = 2, //Indica que el archivo tuvo un error, que se reenvie
        ErrorFormatoIncorrectoDeclaracion = 3, //Indica que el formato no es correcto o no es una declaracion
        ErrorVersionFormatoDeclaracion = 4
    }
}
