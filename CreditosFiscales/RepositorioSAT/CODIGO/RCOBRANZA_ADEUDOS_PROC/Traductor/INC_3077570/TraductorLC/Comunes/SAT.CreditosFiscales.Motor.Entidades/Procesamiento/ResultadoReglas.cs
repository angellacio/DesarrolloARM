//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.Entidades:SAT.CreditosFiscales.Motor.Entidades.ResultadoReglas:1:12/07/2012[Assembly:1.0:12/07/2013])

// Referencias de sistema.
using System;

// Referencias personalizadas.
using SAT.CreditosFiscales.Motor.Entidades.AdmonRegla;

namespace SAT.CreditosFiscales.Motor.Entidades.Procesamiento
{
	/// <summary>
	/// 
	/// </summary>
	[Serializable]
	public class ResultadoReglas
	{
		#region Propiedades
			/// <summary>
			/// Id del procesamiento.
			/// </summary>
			public Guid IdProcesamiento { get; set; }

			/// <summary>
			/// Identificador de la aplicación.
			/// </summary>
			public short IdAplicacion { get; set; }

			/// <summary>
			/// Identificador del tipo de documento de pago.
			/// </summary>
			public short IdTipoDocPago { get; set; }

			/// <summary>
			/// Documento que se está procesando.
			/// </summary>
			public string Mensaje { get; set; }

			/// <summary>
			/// 
			/// </summary>
			public Enumeraciones.PasosTraductor PasoProceso { get; set; }

			/// <summary>
			/// Reglas que fueron ejecutadas sobre el documento.
			/// </summary>
			public ReglasEjecutadas Reglas { get; set; }
		#endregion

		#region Constructores
			/// <summary>
			/// Constructor predeterminado de la clase.
			/// </summary>
			public ResultadoReglas()
			{
				this.Reglas = new ReglasEjecutadas();
				this.IdProcesamiento = Guid.NewGuid();
			}

			/// <summary>
			///  Constructor predeterminado de la clase.
			/// </summary>
			/// <param name="mensaje">Documento a procesar.</param>
			/// <param name="idAplicacion">Identificador de la aplicación.</param>
			/// <param name="idTipoDoc">Idenditificador del tipo de documento de pago.</param>
			public ResultadoReglas(string mensaje, short idAplicacion, short idTipoDoc)
			{
				this.Mensaje = mensaje;
				this.Reglas = new ReglasEjecutadas();
				this.IdProcesamiento = Guid.NewGuid();
				this.IdAplicacion = idAplicacion;
				this.IdTipoDocPago = idTipoDoc;
			}
		#endregion

		#region Métodos
			/// <summary>
			/// Agrega una nueva regla a la lista.
			/// </summary>
			/// <param name="idRegla">Identificador de la regla.</param>
			/// <param name="descripcion">Descripción de la regla.</param>
			/// <param name="esValidacion">Indica si la regla es una validación o una transformación.</param>
			/// <param name="fueExitosa">Indica si fue exitosa o no al ejecutarse.</param>
			public void AgregaRegla(Guid idRegla, string descripcion, bool esValidacion, bool fueExitosa = false)
			{
				Reglas.Add(new ReglaEjecutada { IdRegla = idRegla, Descripcion = descripcion, FueExitosa = fueExitosa, EsValidacion = esValidacion });
			}
		#endregion
	}
}