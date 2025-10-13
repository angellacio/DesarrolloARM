//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.Entidades:SAT.CreditosFiscales.Motor.Entidades.ExcepcionTipificada:1:12/07/2012[Assembly:1.0:12/07/2013])

// Referencias de sistema.
using System;

namespace Sat.CreditosFiscales.Motor.Entidades.AccesoLogEventos
{
	/// <summary>
	/// 
	/// </summary>
	public class ExcepcionTipificada : Exception
	{
		private string ticket;

		public readonly string Mensaje;

		/// <summary>
		/// Crea una excepción tipificada
		/// </summary>
		/// <param name="mensaje"></param>
		/// <param name="ex"></param>
		/// <param name="ticket"></param>
		public ExcepcionTipificada(string mensaje, Exception ex, string ticket)
			: base(mensaje, ex)
		{
			this.ticket = ticket;
			string msgTicket = string.IsNullOrWhiteSpace(ticket) ? string.Empty : string.Format("Para darle seguimiento por favor conserve el siguiente número:{0}.", ticket);
			this.Mensaje = string.Format("{0} {1}", mensaje, msgTicket);
		}

	}
}