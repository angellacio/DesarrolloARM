//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Impresion.Entidades.AccesoLogEventos:Sat.CreditosFiscales.Impresion.Entidades.AccesoLogEventos.ExcepcionGeneracionFormatoImpresion:1:12/07/2013[Assembly:1.0:12/07/2013])

// Referencias de sistema.
using System;

namespace Sat.CreditosFiscales.Impresion.Entidades.AccesoLogEventos
{
	/// <summary>
	/// Clase para manipular las de excepciones generadas por el servicio de impresión.
	/// </summary>
	public class ExcepcionGeneracionFormatoImpresion : Exception
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="mensaje"></param>
		public ExcepcionGeneracionFormatoImpresion(string mensaje)
			: base(mensaje)
		{ }
	}
}