/*
 * Actualizó: Mario Escarpulli
 * Fecha: 10/Jun/2019
*/
// Referencias de sistema.
using System;

// Referencias personalizadas.
using SAT.CreditosFiscales.Motor.Negocio.DatosCache;

namespace SAT.CreditosFiscales.Motor.Servicios
{
	/// <summary>
	/// 
	/// </summary>
	public class Global : System.Web.HttpApplication
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender">Origen del evento.</param>
		/// <param name="e">Argumentos del evento.</param>
		protected void Application_Start(object sender, EventArgs e)
		{
			CacheRepository.RecargaCache();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender">Origen del evento.</param>
		/// <param name="e">Argumentos del evento.</param>
		protected void Session_Start(object sender, EventArgs e)
		{ }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender">Origen del evento.</param>
		/// <param name="e">Argumentos del evento.</param>
		protected void Application_BeginRequest(object sender, EventArgs e)
		{
			CacheRepository.RecargaCache();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender">Origen del evento.</param>
		/// <param name="e">Argumentos del evento.</param>
		protected void Application_AuthenticateRequest(object sender, EventArgs e)
		{ }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender">Origen del evento.</param>
		/// <param name="e">Argumentos del evento.</param>
		protected void Application_Error(object sender, EventArgs e)
		{ }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender">Origen del evento.</param>
		/// <param name="e">Argumentos del evento.</param>
		protected void Session_End(object sender, EventArgs e)
		{ }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender">Origen del evento.</param>
		/// <param name="e">Argumentos del evento.</param>
		protected void Application_End(object sender, EventArgs e)
		{ }
	}
}