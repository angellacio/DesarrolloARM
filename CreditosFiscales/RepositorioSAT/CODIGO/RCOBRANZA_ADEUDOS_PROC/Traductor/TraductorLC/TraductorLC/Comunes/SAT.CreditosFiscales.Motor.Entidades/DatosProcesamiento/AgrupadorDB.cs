//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.Entidades:SAT.CreditosFiscales.Motor.Entidades.AgrupadorDB:1:12/07/2012[Assembly:1.0:12/07/2013])

// Referencias de sistema.
using System;

namespace SAT.CreditosFiscales.Motor.Entidades.DatosProcesamiento
{
	/// <summary>
	/// Entidad de Agrupador
	/// </summary>
	public class AgrupadorDB
	{
		/// <summary>
		/// Identificador del agrupador
		/// </summary>
		public int IdAgrupador { get; set; }

		/// <summary>
		/// Identificador del documento
		/// </summary>
		public Guid IdProcesamiento { get; set; }

		/// <summary>
		/// Valor del agrupador
		/// </summary>
		public string ValorAgrupador { get; set; }

		/// <summary>
		/// Importe total a pagar
		/// </summary>
		public decimal ImporteTotalPagar { get; set; }
	}
}