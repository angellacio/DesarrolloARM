//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.Entidades:SAT.CreditosFiscales.Motor.Entidades.RespuestaXslt:1:12/07/2012[Assembly:1.0:12/07/2013])

// Referencias de sistema.
using System.Linq;
using System.Text;

// Referencias personalizadas.
using SAT.CreditosFiscales.Motor.Entidades.ManejoErrores;

namespace SAT.CreditosFiscales.Motor.Entidades.Procesamiento
{
	/// <summary>
	/// Objeto que indica la respuesta en la ejecución de un XSLT
	/// </summary>
	public class RespuestaXslt
	{
		/// <summary>
		/// Indica si la ejecución de la transformación o validación ha sido exitoso.
		/// </summary>
		public bool EsExitoso { get; set; }

		/// <summary>
		/// Error generado en la ejecución.
		/// </summary>
		public Errores ListaErrores { get; set; }

		/// <summary>
		/// Mensaje obtenido en la ejecución de la transformación.
		/// </summary>
		public string Mensaje { get; set; }

		public string ErroresEncontrados
		{
			get
			{
				var data = new StringBuilder();

				ListaErrores.Select
				(
					e =>
					{
						data.Append(e.ErrorAplicacion != null ? e.ErrorAplicacion.Message : e.ErrorUsuario);

						return e;
					}
				 ).ToList();

				return data.ToString();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public RespuestaXslt()
		{
			ListaErrores = new Errores();
		}

		/// <summary>
		/// Agrega un error a la lista.
		/// </summary>
		/// <param name="mensaje">Mensaje de error.</param>
		/// <param name="codigo">Código de error.</param>
		public void AgregaError(string mensaje, int codigo = 0)
		{
			ListaErrores.Add(new Error { ErrorUsuario = mensaje, Codigo = codigo });
		}
	}
}