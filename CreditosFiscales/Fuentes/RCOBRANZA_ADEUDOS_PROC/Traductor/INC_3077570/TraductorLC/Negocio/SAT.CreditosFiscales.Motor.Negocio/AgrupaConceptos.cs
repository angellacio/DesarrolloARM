// Referencias de sistema.
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SAT.CreditosFiscales.Motor.Negocio
{
	/// <summary>
	/// 
	/// </summary>
	public class AgrupaConceptos
	{
		int consecutivo = 0;
		int noConceptos = 0;
		string conceptos = string.Empty;
		string transacciones = string.Empty;
		bool escrito = false;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="numeroSecuencia"></param>
		/// <param name="clave"></param>
		/// <param name="ejercicio"></param>
		/// <param name="periodo"></param>
		/// <param name="periodicidad"></param>
		public void AgrupaData(string numeroSecuencia, string clave, string ejercicio, string periodo, string periodicidad)
		{
			string[] arregloConceptos = conceptos.Split('~');
			string key = string.Format("{0}|{1}|{2}|{3}|{4}", numeroSecuencia, clave, ejercicio, periodo, periodicidad);
			bool existeConcepto = false;

			if (conceptos.Length > 0)
			{
				foreach (string conceptoActual in arregloConceptos)
				{
					if (conceptoActual.Equals(key))
					{
						existeConcepto = true;
						break;
					}
				}

				if (!existeConcepto)
				{
					conceptos = string.Format("{0}~{1}", conceptos, key);
					noConceptos++;
				}
			}
			else
				conceptos = key; noConceptos++;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public int ObtieneNoTotalConceptos()
		{
			return noConceptos;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public string ObtieneConceptos() { return conceptos; }

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public string ObtieneConceptoActual()
		{
			string[] arregloConceptos = conceptos.Split('~');
			string valor = string.Empty;
			if (arregloConceptos != null && arregloConceptos.Length > 0)
			{
				if (consecutivo < arregloConceptos.Length)
					valor = arregloConceptos[consecutivo]; consecutivo++;
			}

			return valor;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public bool YaSeEscribio() { return escrito; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="valor"></param>
		public void InicializaEscribio(string valor) { escrito = bool.Parse(valor); }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="clave"></param>
		/// <param name="descripcion"></param>
		/// <param name="importe"></param>
		/// <param name="tipo"></param>
		public void GeneraTransacciones(string clave, string descripcion, string importe, string tipo)
		{
			string[] arregloTransacciones = null;
			string[] transaccion = null;
			long nuevoImporte = 0;
			bool transaccionExistente = false;

			if (transacciones.Length > 0)
			{
				arregloTransacciones = transacciones.Split('~');

				foreach (string transaccionActual in arregloTransacciones)
				{
					transaccion = transaccionActual.Split('|');

					if (transaccion[0].Equals(clave))
					{
						nuevoImporte = long.Parse(transaccion[2]) + long.Parse(importe);

						transaccionExistente = true;
						transacciones = transacciones.Replace(transaccionActual, string.Format("{0}|{1}|{2}|{3}", clave, descripcion, nuevoImporte.ToString(), tipo));
						break;
					}
				}

				if (!transaccionExistente)
					transacciones = string.Format("{0}~{1}|{2}|{3}|{4}", transacciones, clave, descripcion, importe, tipo);
			}
			else
				transacciones = string.Format("{0}|{1}|{2}|{3}", clave, descripcion, importe, tipo);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public string DevuelveTransacciones()
		{
			string[] arregloTransacciones = null;
			string[] transaccion = null;
			string xml = string.Empty;
			string formatoTransaccion = "<Transaccion><Clave>{0}</Clave><Descripcion>{1}</Descripcion><Valor>{2}</Valor><Tipo>{3}</Tipo></Transaccion>";
			string formatoTransacciones = "<Transacciones>{0}</Transacciones>";
			arregloTransacciones = transacciones.Split('~');

			foreach (string transaccionActual in arregloTransacciones)
			{
				transaccion = transaccionActual.Split('|');
				xml = string.Format("{0}{1}", xml, string.Format(formatoTransaccion, transaccion[0], transaccion[1], transaccion[2], transaccion[3]));
			}
			return string.Format(formatoTransacciones, xml);
		}

		/// <summary>
		/// 
		/// </summary>
		public void InicializaTransacciones() { transacciones = string.Empty; }

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public int ObtieneNoSecuencia() { return consecutivo; }
	}
}