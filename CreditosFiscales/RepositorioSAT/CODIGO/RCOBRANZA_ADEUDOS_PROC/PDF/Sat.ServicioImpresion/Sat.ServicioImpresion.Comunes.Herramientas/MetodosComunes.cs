//@(#)CREDITOSFISCALES(W:71950:Sat.ServicioImpresion.Comunes.Herramientas:Sat.ServicioImpresion.Comunes.Herramientas.MetodosComunes:1:12/07/2013[Assembly:1.0:12/07/2013])

// Referencias de sistema.
using System;
using System.Reflection;
using System.ComponentModel;

namespace Sat.ServicioImpresion.Comunes.Herramientas
{
	/// <summary>
	/// Clase dónde se definen los métodos requeridos en los diversos proyectos de la solución
	/// </summary>
	public class MetodosComunes
	{
		/// <summary>
		/// Obtiene la descripción de un enumerado.
		/// </summary>
		/// <param name="enumerador">Enumerado.</param>
		/// <returns>Descripción del enumerado.</returns>
		public static string ObtieneDescripcionEnum(Enum enumerador)
		{
			Type type = enumerador.GetType();
			MemberInfo[] memInfo = type.GetMember(enumerador.ToString());

			if (memInfo != null && memInfo.Length > 0)
			{
				var atributos = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

				if (atributos != null && atributos.Length > 0)
				{
					return ((DescriptionAttribute) atributos[0]).Description;
				}
			}

			// Si no cuenta con el atributo se regresa el nombre.
			return enumerador.ToString();
		}

		/// <summary>
		/// Obtiene el nombre del campo de un enumerador.
		/// </summary>
		/// <param name="enumerador">Enumerador.</param>
		/// <returns>Nombre del campo.</returns>
		public static string ObtieneNombreCampo(Enum enumerador)
		{
			Type type = enumerador.GetType();
			MemberInfo[] memInfo = type.GetMember(enumerador.ToString());

			if (memInfo != null && memInfo.Length > 0)
			{
				object[] atributos = memInfo[0].GetCustomAttributes(typeof(Enumeradores.CustomAttributes.NombreCampo), false);

				if (atributos != null && atributos.Length > 0)
				{
					string x = (atributos[0] as Enumeradores.CustomAttributes.NombreCampo).Value;
					return x;
				}
			}

			// Si no cuenta con el atributo se regresa el nombre.
			return enumerador.ToString();
		}

		/// <summary>
		/// Obtiene el nombre del parametro de sql de un enumerador.
		/// </summary>
		/// <param name="enumerador">Enumerador.</param>
		/// <returns>Nombre del campo.</returns>
		public static string ObtieneNombreParametroSql(Enum enumerador)
		{
			Type type = enumerador.GetType();
			MemberInfo[] memInfo = type.GetMember(enumerador.ToString());

			if (memInfo != null && memInfo.Length > 0)
			{
				object[] atributos = memInfo[0].GetCustomAttributes(typeof(Enumeradores.CustomAttributes.NombreParametroSql), false);

				if (atributos != null && atributos.Length > 0)
				{
					string x = (atributos[0] as Enumeradores.CustomAttributes.NombreParametroSql).Value;
					return x;
				}
			}

			// Si no cuenta con el atributo se regresa el nombre.
			return enumerador.ToString();
		}
	}
}