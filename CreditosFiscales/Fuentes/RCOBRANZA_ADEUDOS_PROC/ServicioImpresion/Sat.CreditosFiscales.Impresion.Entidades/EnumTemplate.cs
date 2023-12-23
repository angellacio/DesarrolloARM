//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Impresion.Entidades:Sat.CreditosFiscales.Impresion.Entidades.EnumTemplate:1:12/07/2013[Assembly:1.0:12/07/2013])
/*
 * Actualizado por: Mario Escarpulli
 * Fecha actualización: 13/Junio/2019
*/
namespace Sat.CreditosFiscales.Impresion.Entidades
{
	/// <summary>
	/// Enumeración para el tipo de template
	/// </summary>
	public enum EnumTemplate
	{
		/// <summary>
		/// Formato para pago total o a cuenta
		/// </summary>
		FormatoParaPago = 1,
		/// <summary>
		/// Formato de confirmación de transacciones virtuales totales y parciales
		/// </summary>
		FormatoConfirmacionDeTransaccionesVirtuales = 2,
		/// <summary>
		/// Formato de confirmación de movimientos contables
		/// </summary>
		FormatoConfirmacionDeMovimientosContables = 3,
		/// <summary>
		/// Formato de confirmación para rectificaciones de cancelación o reclasificación
		/// </summary>
		FormatoConfirmacionDeRectificacionesContables = 4,
	}
}