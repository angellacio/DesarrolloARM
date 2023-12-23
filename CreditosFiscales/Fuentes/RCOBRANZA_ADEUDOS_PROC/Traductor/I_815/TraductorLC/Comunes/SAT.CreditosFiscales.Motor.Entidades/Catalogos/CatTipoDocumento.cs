//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.Entidades:SAT.CreditosFiscales.Motor.Entidades.CatTipoDocumento:1:12/07/2012[Assembly:1.0:12/07/2013])

namespace SAT.CreditosFiscales.Motor.Entidades.Catalogos
{
	/// <summary>
	/// Identificador de tipo de documento que genera cuando se realiza una petición de línea de captura.
	/// </summary>
	public class CatTipoDocumento
	{
		/// <summary>
		/// Identificador de tipo de documento.
		/// </summary>
		public int IdTipoDocumento { get; set; }

		/// <summary>
		/// Nombre del documento.
		/// </summary>
		public string Nombre { get; set; }
	}
}