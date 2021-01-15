
//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Negocio.Comun.Procesos:ObtenerNotificacion:0:21/Mayo/2008[SAT.DyP.Negocio.Comun.Procesos:1.1:21/Mayo/2008])
	
using System;
using System.Collections.Generic;
using System.Text;
using SAT.DyP.Negocio.Comun.Procesos.Datos;
using SAT.DyP.Util.Types;
using SAT.DyP.Util.ExceptionHandling;

namespace SAT.DyP.Negocio.Comun.Procesos
{
    /// <summary>
    /// Obtención de la notificación
    /// </summary>
    [Serializable]
    public class ObtenerNotificacion
    {
        /// <summary>
        /// Obtener la notificación
        /// </summary>
        /// <param name="IdMensaje">Identificador de mensaje</param>
        /// <param name="entidadNotificadora">Nombre de la entidad notificadora</param>
        /// <returns>Notificación en candena</returns>
        public static string Execute(int IdMensaje, string entidadNotificadora)
        {
            string _contenido = string.Empty;


            DALNotificacion _daoMensaje = new DALNotificacion();
            _contenido = _daoMensaje.ObtenerTextoNotificacion(IdMensaje, entidadNotificadora);
            return _contenido;
        }
    }
}
