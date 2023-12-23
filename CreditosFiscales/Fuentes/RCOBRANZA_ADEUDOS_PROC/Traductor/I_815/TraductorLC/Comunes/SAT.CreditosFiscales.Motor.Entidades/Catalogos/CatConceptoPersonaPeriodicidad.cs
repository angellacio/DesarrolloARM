//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.Entidades:SAT.CreditosFiscales.Motor.Entidades.CatPeriodoEquivalencia:1:12/07/2012[Assembly:1.0:12/07/2013])

namespace SAT.CreditosFiscales.Motor.Entidades.Catalogos
{
	/// <summary>
	/// Clase que representa el catálogo de periodo.
	/// </summary>
	public class CatConceptoPersonaPeriodicidad
	{
		/// <summary>
		/// Concepto de DyP.
		/// </summary>
		public string ConceptoDyP { get; set; }

		/// <summary>
		/// Id del tipo de persona.
		/// </summary>
		public int IdTipoPersona { get; set; }

		/// <summary>
		/// Periodicidad Origen. 
		/// </summary>
		public string IdPeriodicidadDyP { get; set; }
	}
}