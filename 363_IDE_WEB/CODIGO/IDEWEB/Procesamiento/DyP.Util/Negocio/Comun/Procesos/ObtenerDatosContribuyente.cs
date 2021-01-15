
//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Negocio.Comun.Procesos:ObtenerDatosContribuyentes:0:21/Mayo/2008[SAT.DyP.Negocio.Comun.Procesos:1.1:21/Mayo/2008])
	
using System;
using System.Collections.Generic;
using System.Text;
using SAT.DyP.Negocio.Comun.Tipos;
using SAT.DyP.Negocio.Comun.Procesos.Datos;
using SAT.DyP.Util.ExceptionHandling;
using SAT.DyP.Util.Types;

namespace SAT.DyP.Negocio.Comun.Procesos
{
    /// <summary>
    /// Clase encargada de la obtención de los datos del Contribuyente
    /// </summary>
    [Serializable]
    public class ObtenerDatosContribuyente
    {
        /// <summary>
        /// Obtención de los datos del Contribuyente
        /// </summary>
        /// <param name="RFC">Cadena Registro Federal del Contribuyente</param>
        /// <returns>Tipo de dato Contribuyente</returns>
        public static Contribuyente Execute(string RFC)
        {
            Contribuyente _datos = null;

            DALContribuyente _dao = new DALContribuyente();
            _datos = _dao.Load(RFC);
            return _datos;
        }
    }
}
