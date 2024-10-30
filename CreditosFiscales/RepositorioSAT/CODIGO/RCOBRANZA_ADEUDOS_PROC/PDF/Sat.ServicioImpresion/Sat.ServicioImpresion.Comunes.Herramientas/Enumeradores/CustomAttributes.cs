//@(#)CREDITOSFISCALES(W:71950:Sat.ServicioImpresion.Comunes.Herramientas.Enumeradores:Sat.ServicioImpresion.Comunes.Herramientas.Enumeradores.CustomAttributes:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;

namespace Sat.ServicioImpresion.Comunes.Herramientas.Enumeradores
{
	/// <summary>
	/// 
	/// </summary>
	public class CustomAttributes
	{
		/// <summary>
		/// 
		/// </summary>
		public sealed class NombreCampo : Attribute, IAtributoPersonalizado<string>
		{
			private readonly string valor;

			/// <summary>
			/// 
			/// </summary>
			/// <param name="valorAtributo"></param>
			public NombreCampo(string valorAtributo)
			{
				this.valor = valorAtributo;
			}

			/// <summary>
			/// 
			/// </summary>
			public string Value
			{
				get { return this.valor; }
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public sealed class NombreParametroSql : Attribute, IAtributoPersonalizado<string>
		{
			private readonly string valor;

			/// <summary>
			/// 
			/// </summary>
			/// <param name="valorAtributo"></param>
			public NombreParametroSql(string valorAtributo)
			{
				this.valor = valorAtributo;
			}

			/// <summary>
			/// 
			/// </summary>
			public string Value
			{
				get { return this.valor; }
			}
		}
	}
}