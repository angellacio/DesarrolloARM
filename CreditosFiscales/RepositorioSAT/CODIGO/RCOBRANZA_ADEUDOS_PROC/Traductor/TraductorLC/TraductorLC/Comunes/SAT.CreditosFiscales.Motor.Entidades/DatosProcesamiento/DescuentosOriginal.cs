//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.Entidades:SAT.CreditosFiscales.Motor.Entidades.DescuentosOriginal:1:12/07/2012[Assembly:1.0:12/07/2013])

// Referencias de sistema.
using System;

namespace SAT.CreditosFiscales.Motor.Entidades.DatosProcesamiento
{
	/// <summary>
	/// Entidad de Descuentos Originales
	/// </summary>
	public class DescuentosOriginal
	{
		/// <summary>
		/// Identificador del agrupador
		/// </summary>
		public int IdAgrupador { get; set; }

		/// <summary>
		/// Identificador del procesamiento
		/// </summary>
		public Guid IdProcesamiento { get; set; }

		/// <summary>
		/// Identificador del descuento
		/// </summary>
		public string IdDescuento { get; set; }

		/// <summary>
		/// Clave del concepto original
		/// </summary>
		public string ConceptoOriginal { get; set; }

		/// <summary>
		/// Importe del descuento
		/// </summary>
		public decimal ImporteDescuento { get; set; }
	}
}