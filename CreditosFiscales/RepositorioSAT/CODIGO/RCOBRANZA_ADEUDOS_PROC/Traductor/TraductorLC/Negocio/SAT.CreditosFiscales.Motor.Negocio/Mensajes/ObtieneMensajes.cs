

//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.Negocio:SAT.CreditosFiscales.Motor.Negocio.ObtieneMensajes:1:12/07/2012[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAT.CreditosFiscales.Motor.AccesoDatos.Catalogos;
using SAT.CreditosFiscales.Motor.Entidades.Catalogos;

namespace SAT.CreditosFiscales.Motor.Negocio.Mensajes
{
    public class ObtieneMensajes
    {
        /// <summary>
        /// Obtiene mensaje de error de la base de datos
        /// </summary>
        /// <param name="parametroError">parámetro a buscar</param>
        /// <returns>CatMensajes encontrado</returns>
        public static CatMensajes ObtieneMensajeError(string parametroError)
        {
            CatMensajes mensaje = new CatMensajes();
            try
            {
                mensaje = DalCatMensaje.ConsultaMensaje(parametroError);
            }
            catch (Exception)
            {
                mensaje.Codigo = 0;
                mensaje.Parametro = "";
                mensaje.Valor = "";
            }

            return mensaje;
        }
    }
}
