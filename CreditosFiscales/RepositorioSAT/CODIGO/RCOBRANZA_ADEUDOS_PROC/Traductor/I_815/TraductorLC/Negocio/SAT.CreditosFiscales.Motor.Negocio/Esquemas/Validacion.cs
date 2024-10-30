//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.Negocio:SAT.CreditosFiscales.Motor.Negocio.Validacion:1:12/07/2012[Assembly:1.0:12/07/2013])

// Referencias de sistema.
using System;
using System.Xml;
using System.IO;

// Referencias personalizadas.
using SAT.CreditosFiscales.Motor.Utilidades;
using SAT.CreditosFiscales.Motor.Negocio.Mensajes;
using SAT.CreditosFiscales.Motor.AccesoDatos.Esquemas;
using SAT.CreditosFiscales.Motor.Entidades.Procesamiento;

namespace SAT.CreditosFiscales.Motor.Negocio.Esquemas
{
	public class Validacion
	{
		/// <summary>
		/// Valida el mensaje por medio de un XSD.
		/// </summary>
		/// <param name="documento">Documento a validar.</param>
		/// <param name="xsd">XSD con el que se validará el mensaje.</param>
		/// <param name="targetNamespace">Valor del targetnamespace del esquema.</param>
		/// <returns>Resultado de la validación.</returns>
		public RespuestaGenerica ValidaXSD(string documento, string xsd, string targetNamespace)
		{
			var settings = new XmlReaderSettings();
			var respuesta = new RespuestaGenerica();

			bool seEjecuto = false;

			if (xsd != null && xsd.Length > 0)
			{
				settings.ValidationType = ValidationType.Schema;

				try
				{
					using (var sReader = new StringReader(xsd))
					{
						settings.Schemas.Add(targetNamespace, XmlReader.Create(sReader));
						seEjecuto = true;
					}
				}
				catch (Exception ex)
				{
					respuesta.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.InicializaEsquema), ex);
				}

				if (seEjecuto)
				{
					using (var strReader = new StringReader(documento))
					{
						try
						{
							using (var reader = XmlReader.Create(strReader, settings))
							{
								reader.MoveToContent();

								while (reader.Read());
								respuesta.EsExitoso = true;
							}
						}
						catch (Exception ex)
						{
							respuesta.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.ValidacionEsquema), ex);
						}
					}
				}
			}
			else
				respuesta.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.EsquemaNulo));

			return respuesta;
		}

		/// <summary>
		/// Inserta una regla relacionada a una aplicación y a un tipo de documento de pago.
		/// </summary>
		/// <param name="descripcion">Descripción del esquema.</param>
		/// <param name="esquema">Esquema.</param>
		/// <param name="targetNamespace">Namespace.</param>
		public int InsertaEsquema(string descripcion, string esquema, string targetNamespace)
		{
			return DalEsquemas.InsertaEsquema(descripcion, esquema, targetNamespace);
		}
	}
}