//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.Entidades:SAT.CreditosFiscales.Motor.Entidades.LogEvento:1:12/07/2012[Assembly:1.0:12/07/2013])

// Referencias de sistema.
using System;

namespace Sat.CreditosFiscales.Motor.Entidades.AccesoLogEventos
{
	/// <summary>
	/// Clase de entidades para el log de eventos
	/// </summary>
	public class LogEvento
	{
		/// <summary>
		/// 
		/// </summary>
		public Guid Id { set; get; }
		/// <summary>
		/// 
		/// </summary>
		public string Aplicacion { set; get; }
		/// <summary>
		/// 
		/// </summary>
		public string Evento { set; get; }
		/// <summary>
		/// 
		/// </summary>
		public string Mensaje { set; get; }
		/// <summary>
		/// 
		/// </summary>
		public int IdTipoEvento { set; get; }
		/// <summary>
		/// 
		/// </summary>
		public string TipoEvento
		{
			get
			{
				return ((EnumTipoEvento) IdTipoEvento).ToString();
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime FechaOrigen { set; get; }
	}
}