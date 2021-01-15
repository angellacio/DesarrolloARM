
//@(#)SCADE2(W:SKDN08221CO2:SAT.DyP.Presentacion.Seguridad.Membresia:WebAcessMethod:0:21/Mayo/2008[SAT.DyP.Presentacion.Seguridad.Membresia:1.1:21/Mayo/2008])
	
using System;
using System.Collections.Generic;
using System.Text;

namespace SAT.DyP.Presentacion.Seguridad.Membresia
{
    /// <summary>
    /// Enumeración que representa los metodos de acceso disponibles
    /// para el servicio de autenticación
    /// </summary>
    public enum WebAccessMethod : int
    {
        Internet = 1,
        Modulo = 2
    }
}
