// Referemcias de sistema.
using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;

namespace Sat.CreditosFiscales.Impresion.ConsoleTest
{
	/// <summary>
	/// 
	/// </summary>
	class Program
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="args">Argumentos.</param>
		static void Main(string[] args)
		{
			try
			{
				byte[] archivo = null;
				using (var servicio = new ServicioGeneraFormatoImpresion.ServicioGeneraFormatoImpresionClient())
				{
					// 44011300000334   44551300000002  44121300000037
					//var listaDeFolios = new List<string>() { "44111300003010" };
					//archivo = servicio.GeneraArchivo(ServicioGeneraFormatoImpresion.EnumTemplate.FormatoParaPago, listaDeFolios);

					// 44461300000030
					//var listaDeFolios = new List<string>() { "44121300000112" };
					//archivo = servicio.GeneraArchivo(ServicioGeneraFormatoImpresion.EnumTemplate.FormatoConfirmacionDeMovimientosContables, listaDeFolios);

					//
					var listaDeFolios = new List<string>() { "44141300003033" };
					archivo = servicio.GeneraArchivo(ServicioGeneraFormatoImpresion.EnumTemplate.FormatoConfirmacionDeRectificacionesContables, listaDeFolios);

					//var listaDeFolios = new List<string>() { "44141300003007", "44141300003006" };
					//archivo = servicio.GeneraArchivo(ServicioGeneraFormatoImpresion.EnumTemplate.FormatoConfirmacionDeTransaccionesVirtuales, listaDeFolios);

					verArchivo(archivo);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				Console.Read();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="archivo"></param>
		private static void verArchivo(byte[] archivo)
		{
			string path = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");
			File.WriteAllBytes(path, archivo);

			// open in default PDF application
			Process process = Process.Start(path);
			process.WaitForExit();
			File.Delete(path);
		}
	}
}