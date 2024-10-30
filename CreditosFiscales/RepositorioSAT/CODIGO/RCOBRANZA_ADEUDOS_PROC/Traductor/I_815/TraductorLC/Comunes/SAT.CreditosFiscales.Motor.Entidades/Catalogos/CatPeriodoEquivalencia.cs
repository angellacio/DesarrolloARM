//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.Entidades:SAT.CreditosFiscales.Motor.Entidades.CatPeriodoEquivalencia:1:12/07/2012[Assembly:1.0:12/07/2013])

namespace SAT.CreditosFiscales.Motor.Entidades.Catalogos
{
	/// <summary>
	/// Clase que representa el catálogo de periodo.
	/// </summary>
	public class CatPeriodoEquivalencia
	{
		/// <summary>
		/// Id de la aplicación a consultar.
		/// </summary>
		public int IdAplicacion { get; set; }

		/// <summary>
		/// Periodo Origen. 
		/// </summary>
		public string PeriodoOrigen { get; set; }

		/// <summary>
		/// Descripción del período origen.
		/// </summary>
		public string DescripcionOrigen { get; set; }

		/// <summary>
		/// Id periodo DyP.
		/// </summary>
		public string IdPeriodoDyP { get; set; }

		/// <summary>
		/// Descripción de periodo DyP.
		/// </summary>
		public string DescripcionDyP { get; set; }

		/// <summary>
		/// Elemento activo dentro del catálogo.
		/// </summary>
		public bool Activo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int PeriodicidadOrigen { get; set; }
	}
}