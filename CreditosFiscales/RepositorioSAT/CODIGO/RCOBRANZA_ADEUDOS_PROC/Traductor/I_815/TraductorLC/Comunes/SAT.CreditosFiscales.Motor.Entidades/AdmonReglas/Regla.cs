//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.Entidades:SAT.CreditosFiscales.Motor.Entidades.ReglasPorDocumento:1:12/07/2012[Assembly:1.0:12/07/2013])

// Referencias de sistema.
using System;
using System.Collections.Generic;

// Referencias personalizadas.
using SAT.CreditosFiscales.Motor.Entidades.Esquemas;
using SAT.CreditosFiscales.Motor.Entidades.ManejoErrores;

namespace SAT.CreditosFiscales.Motor.Entidades.AdmonRegla
{
	/// <summary>
	///  Clase de entidades para las reglas del motor
	/// </summary>
	public class ReglasPorDocumento
	{
		/// <summary>
		/// Id de la aplicación.
		/// </summary>
		public short IdAplicacion { get; set; }

		/// <summary>
		/// Id del tipo de documento de pago.
		/// </summary>
		public short IdTipoDocPago { get; set; }

		/// <summary>
		/// Lista de reglas configuradas para la aplicación.
		/// </summary>
		public Reglas ListaReglas { get; set; }

		/// <summary>
		/// Lista de contratos asignados a la aplicación.
		/// </summary>
		public Contratos ListaContratos { get; set; }

		/// <summary>
		/// Lista de directorios en los que se encuentran las reglas en .NET
		/// </summary>
		public string DirectorioReglasNet { get; set; }

		/// <summary>
		/// Constructor predeterminado de la clase.
		/// </summary>
		public ReglasPorDocumento()
		{
			ListaContratos = new Contratos();
			ListaReglas = new Reglas();
		}
	}

	/// <summary>
	/// Objeto que representa a la lista de reglas.
	/// </summary>
	public class Reglas : List<Regla>
	{ }

	/// <summary>
	/// Objeto Regla que contiene las propiedades relacionadas a una regla.
	/// </summary>
	public class Regla : ReglaBase
	{
		public string Xslt { get; set; }
	}

	/// <summary>
	/// Clase base para una regla.
	/// </summary>
	public class ReglaBase
	{
		public Guid IdRegla { get; set; }
		public string Descripcion { get; set; }
		public bool EsValidacion { get; set; }
		public bool EsNET { get; set; }
		public byte Secuencia { get; set; }
		public bool AntesDeInsercion { get; set; }
	}

	/// <summary>
	/// Lista de reglas que se ejecutaron.
	/// </summary>
	public class ReglasEjecutadas : List<ReglaEjecutada>
	{ }

	/// <summary>
	/// Objeto que representa a una regla ejecutada para un mensaje.
	/// </summary>
	public class ReglaEjecutada : ReglaBase
	{
		public bool FueExitosa { get; set; }
		public Errores Errores { get; set; }
	}
}