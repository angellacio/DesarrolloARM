
//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Negocio.Comun.Tipos:IdentificadorEvento:0:21/Mayo/2008[SAT.DyP.Negocio.Comun.Tipos:1.0:21/Mayo/2008])
	
using System;
using System.Collections.Generic;
using System.Text;

namespace SAT.DyP.Negocio.Comun.Tipos
{
    /// <summary>
    /// Clase que define una constante que se empleara
    /// para definir el EVENT ID para escribir sucesos al
    /// Log del Sistema Operativo
    /// </summary>
    public class IdentificadorEvento
    {
        public const int NEGOCIO_COMUN_EVENT_ID = 2000;

        public const int DECLARASAT_EN_LINEA_EVENT_ID = 3000;

        public const int DECLARASAT_TRADICIONAL_EVENT_ID = 4000;

        public const int DECLARASAT_COMUN_EVENT_ID = 5000;

        public const int AVISO_CERO_EVENT_ID = 6000;

        public const int DIOT_EVENT_ID = 7000;

        public const int IETU_EVENT_ID = 8000;

        public const int DIM_EVENT_ID = 9000;
    }
}
