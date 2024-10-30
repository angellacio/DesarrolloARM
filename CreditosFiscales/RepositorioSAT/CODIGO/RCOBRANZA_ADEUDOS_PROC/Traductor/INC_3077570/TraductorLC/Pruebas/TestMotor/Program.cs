// Decompiled with JetBrains decompiler
// Type: TestMotor.Program
// Assembly: TestMotor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 50D74495-5863-43DD-A49E-C6D45E76437E
// Assembly location: C:\Respaldar 2\SAT-Softek\Consulta de adeudos\PPMC 102552 agregar MAT\pruebas\TestMotorTraductor\TestMotor.exe

/*
 * Actualizó: Mario Escarpulli
 * Fecha: 10/Jun/2019
*/
// Referencias de sistema.

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace TestMotor
{
	/// <summary>
	/// 
	/// </summary>
	internal class Program
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="args"></param>
		[STAThreadAttribute]
		private static void Main(string[] args)
		{
			try
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run((Form) new GeneraPDF());
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			Console.ReadLine();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sTextPdf"></param>
		/// <param name="name"></param>
		public static void SaveText(string sTextPdf, string name)
		{
			File.WriteAllText(name, sTextPdf);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static RespuestaLC DeserializaResp(string text)
		{
			var respuestaLc = new RespuestaLC();
			try
			{
				return (RespuestaLC) new XmlSerializer(typeof(RespuestaLC)).Deserialize((Stream) new MemoryStream(new UTF8Encoding().GetBytes(text)));
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="inFileName"></param>
		/// <param name="outFileName"></param>
		public static void Decode(string inFileName, string outFileName)
		{
			var transform = (ICryptoTransform) new FromBase64Transform();
			using (FileStream fileStream1 = File.OpenRead(inFileName))
			{
				using (FileStream fileStream2 = File.Create(outFileName))
				{
					using (CryptoStream cryptoStream = new CryptoStream((Stream) fileStream1, transform, CryptoStreamMode.Read))
					{
						var buffer = new byte[4096];
						int count;
						while ((count = cryptoStream.Read(buffer, 0, buffer.Length)) > 0)
							fileStream2.Write(buffer, 0, count);

						fileStream2.Flush();
					}
				}
			}
		}
	}
}