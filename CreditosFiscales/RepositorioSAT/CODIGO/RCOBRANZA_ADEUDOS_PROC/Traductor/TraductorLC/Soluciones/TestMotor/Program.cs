// Decompiled with JetBrains decompiler
// Type: TestMotor.Program
// Assembly: TestMotor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 50D74495-5863-43DD-A49E-C6D45E76437E
// Assembly location: C:\Respaldar 2\SAT-Softek\Consulta de adeudos\PPMC 102552 agregar MAT\pruebas\TestMotorTraductor\TestMotor.exe

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace TestMotor
{
    internal class Program
    {
        [STAThreadAttribute]  
        private static void Main(string[] args)
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run((Form)new GeneraPDF());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }

        public static void SaveText(string TextPDF, string name)
        {
            File.WriteAllText(name, TextPDF);
        }

        public static RespuestaLC deserializaResp(string text)
        {
            RespuestaLC respuestaLc = new RespuestaLC();
            try
            {
                return (RespuestaLC)new XmlSerializer(typeof(RespuestaLC)).Deserialize((Stream)new MemoryStream(new UTF8Encoding().GetBytes(text)));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Decode(string inFileName, string outFileName)
        {
            ICryptoTransform transform = (ICryptoTransform)new FromBase64Transform();
            using (FileStream fileStream1 = File.OpenRead(inFileName))
            {
                using (FileStream fileStream2 = File.Create(outFileName))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)fileStream1, transform, CryptoStreamMode.Read))
                    {
                        byte[] buffer = new byte[4096];
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
