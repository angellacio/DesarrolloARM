using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
namespace Sat.CreditosFiscales.Archivos
{
    public class Log
    {
        public static void escribeLinea(String ruta,String texto)
        {
            using (var outFile = new StreamWriter(ruta, true, Encoding.Default))
            {
                                outFile.Write(texto+"\n");
            }
        }

        public static void borraArchivoAnterior(String archivo)
        {
            if (File.Exists(archivo))
            {
                File.Delete(archivo);
            }
        }

        public static void abreArchivo(String archivo)
        {
            Process.Start(archivo);
 
        }


    }
}
