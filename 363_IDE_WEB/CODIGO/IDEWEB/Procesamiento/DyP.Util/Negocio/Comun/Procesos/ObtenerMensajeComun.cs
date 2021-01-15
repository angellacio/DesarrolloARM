
//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Negocio.Comun.Procesos:ObtenerMensajeComun:0:21/Mayo/2008[SAT.DyP.Negocio.Comun.Procesos:1.1:21/Mayo/2008])
	
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using SAT.DyP.Util.Types;
using SAT.DyP.Negocio.Comun.Procesos.Datos;
using SAT.DyP.Util.ExceptionHandling;

namespace SAT.DyP.Negocio.Comun.Procesos
{
    /// <summary>
    /// Clase que obtiene el contenido de un mensaje común
    /// </summary>
    /// 
    [Serializable]
    public class ObtenerMensajeComun
    {
        /// <summary>
        /// Devuelve el contenido de un mensaje
        /// </summary>
        /// <param name="constanteMensaje">Constante del Mensaje</param>
        /// <returns>Contenido</returns>
        public static string Execute(string constanteMensaje)
        {
            string _contenido = string.Empty;

            DALMensaje _daoMensaje = new DALMensaje();
            _contenido = _daoMensaje.ObtenerMensajeComun(constanteMensaje);
            return _contenido;
        }

        public static string Execute(int idMensaje, string entidadMensaje)
        {
            string _contenido = string.Empty;

            try
            {
                DALMensaje _daoMensaje = new DALMensaje();
                _contenido = _daoMensaje.ObtenerMensaje(idMensaje, entidadMensaje);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message, ex);
            }

            return _contenido;
        }
    }
}
