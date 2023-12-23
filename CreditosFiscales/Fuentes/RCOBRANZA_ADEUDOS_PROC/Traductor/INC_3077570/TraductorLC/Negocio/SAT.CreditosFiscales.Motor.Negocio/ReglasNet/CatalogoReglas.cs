//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.Negocio:SAT.CreditosFiscales.Motor.Negocio.CatalogoReglas:1:12/07/2012[Assembly:1.0:12/07/2013])

// Referencias de sistema.
using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;

namespace SAT.CreditosFiscales.Motor.Negocio.ReglasNet
{
	/// <summary>
	/// 
	/// </summary>
	public class CatalogoReglas
	{
		/// <summary>
		/// 
		/// </summary>
		private string directory = string.Empty;

		private Dictionary<string, ReglaBaseNet> _reglasPorNombreClase = null;
		private List<ReglaBaseNet> _listaReglas = null;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="directory"></param>
		public CatalogoReglas(string directory)
		{
			this.directory = directory;

			Cargar();
		}

		/// <summary>
		/// Carga el contenido de un archivo de ensamblado(s) de la ruta de acceso especificada. 
		/// </summary>
		private void Cargar()
		{
			string[] archivos = Directory.GetFiles(directory, "*.dll");
			_reglasPorNombreClase = new Dictionary<string, ReglaBaseNet>();
			_listaReglas = new List<ReglaBaseNet>();

			foreach (string archivo in archivos)
			{
				Assembly assembly = null;
				try
				{
					assembly = Assembly.LoadFile(archivo);
				}
				catch
				{ }

				if (null != assembly)
				{
					Type[] types = assembly.GetTypes();

					foreach (Type type in types)
					{
						if (type.IsSubclassOf(typeof(ReglaBaseNet)))
						{
							ReglaBaseNet regla = null;
							try
							{
								regla = Activator.CreateInstance(type) as ReglaBaseNet;
							}
							catch
							{
								throw;
							}

							if (null != regla)
							{
								_reglasPorNombreClase[regla.NombreClase] = regla;
								_listaReglas.Add(regla);
							}
						}
					}
				}

			}

			// Se ordenan para proposito de visualización.
			//_listaReglas.Sort();
		}

		/// <summary>
		/// 
		/// </summary>
		public List<ReglaBaseNet> ListaReglas
		{
			get { return _listaReglas; }
		}

		/// <summary>
		/// Obtiene una regla
		/// </summary>
		/// <param name="className">Identificador de la clase</param>
		/// <returns>Regla </returns>
		public ReglaBaseNet ObtenerRegla(string className)
		{
			if (_reglasPorNombreClase.ContainsKey(className))
				return _reglasPorNombreClase[className];
			else
				throw new Exception(string.Format("No existe una regla para el nombre de clase {0}", className));
		}
	}
}