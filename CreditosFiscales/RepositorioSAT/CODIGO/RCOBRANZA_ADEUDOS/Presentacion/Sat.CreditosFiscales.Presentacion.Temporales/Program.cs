using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;

namespace Sat.CreditosFiscales.Presentacion.Temporales
{
    class Program
    {
        static void Main(string[] args)
        {
            string carpetas = ConfigurationManager.AppSettings["Carpetas"];;
            string ejecutarProceso =  ConfigurationManager.AppSettings["EjecutarProceso"];
            string horasDeAntiguedad = ConfigurationManager.AppSettings["HorasDeAntiguedad"];
           

            bool ejecuta;

            if(!string.IsNullOrWhiteSpace(ejecutarProceso))
            {
                if(bool.TryParse(ejecutarProceso, out ejecuta))
                {
                    if (ejecuta)
                    {
                        if (!string.IsNullOrWhiteSpace(carpetas))
                        {
                            depuraCarpetas(carpetas, horasDeAntiguedad);
                            Console.WriteLine("La ejecución del proceso termino...");
                        }
                        else
                            Console.WriteLine("Error::Las rutas de depuración no están definidas en al App.config");
                    }
                    else
                        Console.WriteLine("Información::La ejecución del proceso está deshabilitada en al App.config");

                }
                else
                    Console.WriteLine("Error::La ejecución del proceso no está definida correctamente debe tener debe tener los valores true/false en al App.config");
            }
            else
                Console.WriteLine("Error::La ejecución del proceso no está definida en al App.config");
        }
        static void depuraCarpetas(string rutas, string  horasDeAntiguedad)
        {
            string[] carpetas = rutas.Split('|');

            int horas = 1;
            int.TryParse(horasDeAntiguedad, out horas);

            foreach (string carpeta in carpetas)
            {
                eliminaContenido(carpeta, horas);
            }
        }
        static void eliminaContenido(string carpeta, int horas)
        {
            
                if (System.IO.Directory.Exists(carpeta))
                {
                    System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo(carpeta);
                    System.IO.FileInfo[] archivos = directoryInfo.GetFiles();
                   
                    foreach (System.IO.FileInfo fileInfo in archivos)
                    {
                        try
                        {
                            if (fileInfo.CreationTime < DateTime.Now.AddHours(-1 * horas))
                                fileInfo.Delete();
                        }   
                        catch (Exception ex)
                        {
                            Console.WriteLine(string.Format("Error::No se logró eliminar el archivo '{0}', Excepción = {1}", fileInfo.FullName,ex.Message));
                        }
                    }
                }
                else
                    Console.WriteLine("Error::No existe la carpeta " + carpeta);
            
        }

    }
}
