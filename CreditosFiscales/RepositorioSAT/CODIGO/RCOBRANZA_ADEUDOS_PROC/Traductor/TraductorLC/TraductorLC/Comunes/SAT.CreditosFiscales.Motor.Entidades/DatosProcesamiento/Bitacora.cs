// Referencias personalizadas.
using SAT.CreditosFiscales.Motor.Entidades.ManejoErrores;
using SAT.CreditosFiscales.Motor.Entidades.Procesamiento;

namespace SAT.CreditosFiscales.Motor.Entidades.DatosProcesamiento
{
	/// <summary>
	/// 
	/// </summary>
	public class Bitacora
	{
		/// <summary>
		/// 
		/// </summary>
		public ResultadoReglas entidad { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Enumeraciones.PasosTraductor paso { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public bool escribirMensaje { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string observaciones { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Errores listaErrores { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public decimal duracion { get; set; }
	}
}