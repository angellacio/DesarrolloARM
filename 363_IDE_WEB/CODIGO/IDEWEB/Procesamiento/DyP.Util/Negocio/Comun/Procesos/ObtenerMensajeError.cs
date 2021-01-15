//@(#)SCADE2(W:SKDN08251CO6:SAT.DyP.Negocio.Comun.Procesos:0:16/Junio/2008[SAT.DyP.Negocio.Comun.Procesos:1.2:16/Junio/2008])
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
    /// Clase que obtiene los mensajes de error.
    /// </summary>
    [Serializable]
    public class ObtenerMensajeError
    {
        public string Execute(int iErrorCode, ContextoError eCtxError)
        {
            string _contenido = string.Empty;
            DALError _daoError = new DALError();
            _contenido = _daoError.ObtenerMensajeError(iErrorCode, eCtxError);
            return _contenido;
        }
    }
}
