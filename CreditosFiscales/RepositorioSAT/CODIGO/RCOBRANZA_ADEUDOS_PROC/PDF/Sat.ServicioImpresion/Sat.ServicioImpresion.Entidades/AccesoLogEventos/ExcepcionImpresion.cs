//@(#)CREDITOSFISCALES(W:71950:Sat.ServicioImpresion.Entidades.AccesoLogEventos:Sat.ServicioImpresion.Entidades.AccesoLogEventos.ExcepcionImpresion:1:12/07/2013[Assembly:1.0:12/07/2013])

// Referencias de sistema.
using System;

namespace Sat.ServicioImpresion.Entidades.AccesoLogEventos
{
	/// <summary>
	/// Clase para manipular las de excepciones generadas por el servicio de impresión.
	/// </summary>
	public class ExcepcionImpresion : Exception
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="mensaje"></param>
		public ExcepcionImpresion(string mensaje)
			: base(mensaje)
		{ }
	}
}