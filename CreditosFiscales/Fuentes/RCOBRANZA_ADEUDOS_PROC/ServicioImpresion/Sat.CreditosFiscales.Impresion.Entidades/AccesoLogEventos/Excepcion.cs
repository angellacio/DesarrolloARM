//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Impresion.Entidades.AccesoLogEventos:Sat.CreditosFiscales.Impresion.Entidades.AccesoLogEventos.ExcepcionTipificada:1:12/07/2013[Assembly:1.0:12/07/2013])

// Referencias de sistema.
using System;

namespace Sat.CreditosFiscales.Impresion.Entidades.AccesoLogEventos
{
	/// <summary>
	/// Clase utilizada para el manejo de una excepción tipificada
	/// </summary>
	public class ExcepcionTipificada : Exception
	{
		/// <summary>
		/// Ticket de la excepción
		/// </summary>
		private string ticket;

		/// <summary>
		/// Mensaje a mostrar
		/// </summary>
		public readonly string Mensaje;

		/// <summary>
		/// Método para generar el mensaje con una excepción tipificada 
		/// </summary>
		/// <param name="mensaje">Mensaje adicional</param>
		/// <param name="ex">Excepción</param>
		/// <param name="ticket">Número de ticket</param>
		public ExcepcionTipificada(string mensaje, Exception ex, string ticket)
			: base(mensaje, ex)
		{
			this.ticket = ticket;
			string msgTicket = string.IsNullOrWhiteSpace(ticket) ? string.Empty : string.Format("{0}", ticket);
			this.Mensaje = string.Format("{0} {1}", mensaje, msgTicket);
		}
	}
}