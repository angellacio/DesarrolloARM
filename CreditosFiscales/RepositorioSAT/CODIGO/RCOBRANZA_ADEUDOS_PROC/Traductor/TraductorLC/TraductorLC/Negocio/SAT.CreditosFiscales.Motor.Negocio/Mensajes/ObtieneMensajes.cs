//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.Negocio:SAT.CreditosFiscales.Motor.Negocio.ObtieneMensajes:1:12/07/2012[Assembly:1.0:12/07/2013])

// Referencias de sistema.
using System;

// Referencias personalizadas.
using SAT.CreditosFiscales.Motor.AccesoDatos.Catalogos;
using SAT.CreditosFiscales.Motor.Entidades.Catalogos;

namespace SAT.CreditosFiscales.Motor.Negocio.Mensajes
{
	/// <summary>
	/// Permite obtener mensajes a través de los catálogos en la base de datos.
	/// </summary>
	public class ObtieneMensajes
	{
		/// <summary>
		/// Obtiene mensaje de error de la base de datos
		/// </summary>
		/// <param name="psParametroError">Parámetro a buscar en el catálogo de mensajes.</param>
		/// <returns>CatMensajes encontrado</returns>
		public static CatMensajes ObtieneMensajeError(string psParametroError)
		{
			var mensaje = new CatMensajes();
			try
			{
				mensaje = DalCatMensaje.ConsultaMensaje(psParametroError);
			}
			catch (Exception)
			{
				mensaje.Codigo = 0;
				mensaje.Parametro = string.Empty;
				mensaje.Valor = string.Empty;
			}

			return mensaje;
		}
	}
}