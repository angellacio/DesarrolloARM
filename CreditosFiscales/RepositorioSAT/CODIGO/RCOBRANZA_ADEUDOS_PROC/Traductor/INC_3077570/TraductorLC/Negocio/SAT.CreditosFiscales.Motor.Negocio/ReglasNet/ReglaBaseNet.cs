//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.Negocio:SAT.CreditosFiscales.Motor.Negocio.ReglaBaseNet:1:12/07/2012[Assembly:1.0:12/07/2013])

// Referencias de sistema.
using System;
using System.Xml;

// Referencias personalizadas.
using SAT.CreditosFiscales.Motor.Utilidades;
using SAT.CreditosFiscales.Motor.Negocio.Mensajes;
using SAT.CreditosFiscales.Motor.Entidades.Procesamiento;

namespace SAT.CreditosFiscales.Motor.Negocio.ReglasNet
{
	/// <summary>
	/// Clase de reglas .net para aplicar al procesamiento.
	/// </summary>
	public class ReglaBaseNet
	{
		/// <summary>
		/// 
		/// </summary>
		public virtual string NombreClase { get { return this.GetType().AssemblyQualifiedName; } }

		/// <summary>
		/// Ejecuta regla NET
		/// </summary>
		/// <param name="documento"></param>
		/// <returns></returns>
		public virtual RespuestaGenerica Ejecuta(XmlDocument documento)
		{
			return new RespuestaGenerica();
		}

		/// <summary>
		/// Ejecuta regla .NET
		/// </summary>
		/// <param name="documento"></param>
		/// <returns></returns>
		public virtual RespuestaGenerica EjecutaRegla(XmlDocument documento)
		{
			var respuesta = new RespuestaGenerica();

			try
			{
				respuesta = Ejecuta(documento);
			}
			catch (Exception ex)
			{
				respuesta.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.EjecucionReglaNet), ex, this.NombreClase);
			}

			return respuesta;
		}
	}
}