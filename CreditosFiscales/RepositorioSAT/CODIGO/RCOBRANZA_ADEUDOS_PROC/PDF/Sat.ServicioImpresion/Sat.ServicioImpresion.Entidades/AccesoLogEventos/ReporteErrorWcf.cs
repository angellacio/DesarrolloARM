//@(#)CREDITOSFISCALES(W:71950:Sat.ServicioImpresion.Entidades.AccesoLogEventos:Sat.ServicioImpresion.Entidades.AccesoLogEventos.ReporteErrorWcf:1:12/07/2013[Assembly:1.0:12/07/2013])

// Referncias de sistema.
using System;
using System.Runtime.Serialization;

namespace Sat.ServicioImpresion.Entidades.AccesoLogEventos
{
	/// <summary>
	/// Clase para regresar una excepción desde el servicio de impresión
	/// </summary>
	[DataContract]
	[Serializable]
	public class ReporteErrorWcf
	{
		/// <summary>
		/// Mensaje definido
		/// </summary>
		[DataMember]
		public string Mensaje { get; set; }

		/// <summary>
		/// Mensaje de la Excepción
		/// </summary>
		[DataMember]
		public string InnerException { get; set; }

		/// <summary>
		/// Contructor de la clase
		/// </summary>
		/// <param name="mensaje">Mensaje de texto</param>
		public ReporteErrorWcf(string mensaje)
		{
			this.Mensaje = mensaje;
		}
	}
}