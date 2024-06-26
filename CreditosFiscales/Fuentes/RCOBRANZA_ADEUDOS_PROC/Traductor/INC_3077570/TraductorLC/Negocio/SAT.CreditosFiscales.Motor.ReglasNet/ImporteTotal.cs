﻿//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.ReglasNet:SAT.CreditosFiscales.Motor.ReglasNet.ImporteTotal:1:12/07/2012[Assembly:1.0:12/07/2013])

// Referencias de sistema.
using System;
using System.Xml;

// Referencias personalizadas.
using SAT.CreditosFiscales.Motor.Negocio.Mensajes;
using Entidad = SAT.CreditosFiscales.Motor.Entidades;
using Negocio = SAT.CreditosFiscales.Motor.Negocio;

namespace SAT.CreditosFiscales.Motor.ReglasNet
{
	/// <summary>
	/// 
	/// </summary>
	public class ImporteTotal : Negocio.ReglasNet.ReglaBaseNet
	{
		/// <summary>
		/// Ejecuta la regla ImporteTotal
		/// </summary>
		/// <param name="documento">XML con la información del crédito fiscal.</param>
		/// <returns>Respuesta de la ejecución de la regla.</returns>
		public override Entidad.Procesamiento.RespuestaGenerica Ejecuta(XmlDocument documento)
		{
			var respuesta = new Entidad.Procesamiento.RespuestaGenerica();
			bool continuar = false;
			// Xpaths
			string sImporteTotalXpath = Entidad.ReglasNet.ConfiguracionXpath.IMPORTETOTAL;
			string sTransaccionesXpath = Entidad.ReglasNet.ConfiguracionXpath.TRANSACCION;
			XmlNodeList transacciones = null;
			string sTotal = string.Empty;

			try
			{
				sTotal = documento.SelectNodes(sImporteTotalXpath)[0].InnerText;
				continuar = true;
			}
			catch (Exception ex)
			{
				respuesta.AgregaError(ObtieneMensajes.ObtieneMensajeError("MotorCF:NodosReglaNet"), ex);
			}

			long sumatoria = 0;

			if (continuar)
			{
				continuar = false;

				try
				{
					transacciones = documento.SelectNodes(sTransaccionesXpath);
					continuar = true;
				}
				catch (Exception ex)
				{
					respuesta.AgregaError(ObtieneMensajes.ObtieneMensajeError("MotorCF:NodosReglaNet"), ex);
				}

				if (continuar)
				{
					if (transacciones != null)
					{
						foreach (XmlNode transaccionActual in transacciones)
						{
							if (transaccionActual.SelectNodes("Clave")[0].InnerText.Equals("4423"))
								sumatoria = sumatoria + long.Parse(transaccionActual.SelectNodes("Valor")[0].InnerText);
						}
					}

					if (long.Parse(sTotal).Equals(sumatoria))
						respuesta.EsExitoso = true;
					else
						respuesta.AgregaError("La suma de los conceptos no es igual al importe total del documento.");
				}
			}

			return respuesta;
		}
	}
}