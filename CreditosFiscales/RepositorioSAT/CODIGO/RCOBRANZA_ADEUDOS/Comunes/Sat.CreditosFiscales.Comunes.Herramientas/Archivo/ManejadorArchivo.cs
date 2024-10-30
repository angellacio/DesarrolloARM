
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Comunes.Herramientas.Archivo:Sat.CreditosFiscales.Comunes.Herramientas.Archivo.ManejadorArchivos:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace Sat.CreditosFiscales.Comunes.Herramientas.Archivo
{
    /// <summary>
    /// CLase que se utiliza para el manejo de archivos.
    /// </summary>
    public static class ManejadorArchivos
    {
        /// <summary>
        /// Método que crea mensaje de texto para ser procesado.
        /// </summary>
        /// <param name="items">Lista de elementos para ser procesados.</param>
        /// <returns>Arreglo de líneas de texto que formarán el archivo.</returns>
        public static StringBuilder CrearMensaje(IEnumerable<string> items)
        {
            var mensaje = new StringBuilder();
            foreach (string item in items)
            {
                mensaje.Append(item);
                if (item != Constantes.EOF)
                    mensaje.Append(Environment.NewLine);
            }
            return mensaje;
        }

      
        /// <summary>
        /// Adjunta el conteido del archivo existente a uno nuevo.
        /// </summary>
        /// <param name="mensaje">Mensaje creado durante la ejecución actual.</param>
        /// <param name="contenido">Líneas de texto con el contenido del archivo creado en un proceso previo.</param>
        public static void AppendContenido(ref StringBuilder mensaje, string[] contenido)
        {
            foreach (var lineaTrim in contenido.Select(linea => linea.Trim()))
            {
                mensaje.Append(lineaTrim + "\n");
            }
        }

       

        /// <summary>
        /// Escribir archivo en el file system.
        /// </summary>
        /// <param name="mensaje">Arreglo de líneas de texto para conformar el archivo.</param>
        /// <param name="rutaArchivo">Ruta de archivo a escribir.</param>
        /// <param name="nombreArchivo">Nombre del archivo que se va a generar.</param>
        public static void EscribeArchivo(StringBuilder mensaje, string rutaArchivo, string nombreArchivo)
        {
            if (string.IsNullOrEmpty(rutaArchivo)) return;
            rutaArchivo = Path.Combine(rutaArchivo, nombreArchivo);
            Escribe(rutaArchivo, mensaje);
        }

        /// <summary>
        /// Método para escribir el archivo dentro del file share.
        /// </summary>
        /// <param name="mensaje">Mensaje de texto para ser guardado en el archivo.</param>
        /// <param name="rutaArchivo">Ruta de red dentro del sistema de archivos donde será depositado el archivo.</param>
        /// <param name="nombreArchivo">Nombre del archivo que será creado en la ruta especificada.</param>
        public static void EscribeArchivo(string[] mensaje, string rutaArchivo, string nombreArchivo)
        {
            if (string.IsNullOrEmpty(rutaArchivo)) return;
            rutaArchivo = Path.Combine(rutaArchivo, nombreArchivo);
            Escribe(rutaArchivo, mensaje);
        }

        private static void Escribe(string rutaArchivo, string[] mensaje)
        {
            try
            {
                File.WriteAllLines(rutaArchivo, mensaje, Encoding.Default);
            }
            catch (Exception exception)
            {
                //LogEventos.EscribirEntradaError(exception, 2051);

            }
        }

        private static void Escribe(string rutaArchivo, StringBuilder mensaje)
        {
          
                //bool existeArchivo = !File.Exists(rutaArchivo);
                using (var outFile = new StreamWriter(rutaArchivo, true, Encoding.Default))
                {
                    outFile.Write(mensaje);
                }  
        }

        /// <summary>
        /// Método que se utiliza para mover archivos.
        /// </summary>
        /// <param name="sourcePath">Ruta origen del archivo que se desea mover. Debe inlcuir el nombre del archivo.</param>
        /// <param name="targetDirectory">Ruta destino donde será colocado el archivo inlcuido en la ruta de origen.</param>
        public static void MueveArchivo(string sourcePath, string targetDirectory)
        {
            try
            {
                var fileName = Path.GetFileName(sourcePath);
                var targetPath = Path.Combine(targetDirectory, fileName);

                //if (File.Exists(targetPath))
                 // File.Delete(targetPath);
                File.Move(sourcePath, targetPath);
            }
            catch (Exception exception)
            {
                //AccesoLogEventos.EscribirEntradaError(exception, 2053);
            }
        }

        /// <summary>
        /// Método que se utiliza para mover archivos.
        /// </summary>
        /// <param name="sourcePath">Ruta de orgen, debe inlcuir el nombre del archivo.</param>
        /// <param name="targetDirectory">Ruta destino donde será colocado el nuevo archivo.</param>
        /// <param name="newFileName">Si existe el nombre del archivo en la carpeta destino, se puede especificar un nombre en particular.</param>
        public static void MueveArchivo(string sourcePath, string targetDirectory, string newFileName)
        {
            try
            {
                var targetPath = Path.Combine(targetDirectory, newFileName);

                if (File.Exists(targetPath))
                    File.Delete(targetPath);

                File.Move(sourcePath, targetPath);
            }
            catch (Exception exception)
            {
                //AccesoLogEventos.EscribirEntradaError(exception, 2054);
            }
        }

        /// <summary>
        /// Método utilizada para borrar archivos contenidos dentro de un directorio.
        /// </summary>
        /// <param name="rutaArchivo">Ruta física dentro del sistema, donde se cuenta contenido el archivo.</param>
        /// <param name="nombreArchivo">Nombre del archivo a ser borrado.</param>
        public static void BorraArchivo(string rutaArchivo, string nombreArchivo)
        {
            try
            {
                rutaArchivo = Path.Combine(rutaArchivo, nombreArchivo);
                if (File.Exists(rutaArchivo))
                {
                    File.Delete(rutaArchivo);
                }
            }
            catch (Exception exception)
            {
                //AccesoLogEventos.EscribirEntradaError(exception, 2055);
            }
        }

        /// <summary>
        /// Método que crea mensajes para archivos de cifras de control.
        /// </summary>
        /// <param name="bytesSize">Número en bytes del archivo de detalles</param>
        /// <param name="numLineas">Número de líneas que contiene el archivo de detalles.</param>
        /// <param name="alr">Clave de alr a la que pertenece el archivo.</param>
        /// <param name="fecha">Fecha de operación.</param>
        /// <param name="hora">Hora de ejecución.</param>
        /// <param name="sumaImportes">Suma de el total de línea de detalles.</param>
        /// <returns>Regresa el mensaje armado.</returns>
        public static StringBuilder CrearMensaje(string bytesSize, string numLineas, string alr, string fecha, string hora, string sumaImportes)
        {
            var mensaje = new StringBuilder();
            string item = string.Concat(bytesSize,
                                        "|",
                                        numLineas,
                                        "|",
                                        alr,
                                        "|",
                                        fecha,
                                        "|",
                                        hora,
                                        "|",
                                        sumaImportes.PadLeft(14, '0'),
                                        "|");
            mensaje.Append(item);
            return mensaje;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytesSize"></param>
        /// <param name="numLineas"></param>
        /// <param name="alr"></param>
        /// <param name="fecha"></param>
        /// <param name="hora"></param>
        /// <param name="sumaImportes"></param>
        /// <returns></returns>
        public static string[] CrearMensajeLineas(string bytesSize, string numLineas, string alr, string fecha, string hora, string sumaImportes)
        {
            var mensaje = new string[1];
            string item = string.Concat(bytesSize,
                                        "|",
                                        numLineas,
                                        "|",
                                        alr,
                                        "|",
                                        fecha,
                                        "|",
                                        hora,
                                        "|",
                                        sumaImportes.PadLeft(14, '0'),
                                        "|");
            mensaje[0] = item;
            return mensaje;
        }

        
        public static string[] LeerArchivo(string rutaArchivo)
        {
            return File.ReadAllLines(rutaArchivo);
        }

        public static void WriteMessage(string mensaje)
        {
            try
            {
                if (ConfigurationManager.AppSettings["LogActivo"] == "1")
                {
                    string ruta = ConfigurationManager.AppSettings["RutaLog"];
                    string nombreArchivo = string.Concat("Log", "_", DateTime.Now.ToString("yyyyMMdd") + ".txt");
                    string rutaEscribe = Path.Combine(ruta, nombreArchivo);
                    mensaje = "--|" + DateTime.Now.ToString() + "| " + mensaje;

                    var existeArchivo = File.Exists(rutaEscribe);
                    using (var outFile = new StreamWriter(rutaEscribe, true, Encoding.Default))
                    {
                        if (!existeArchivo)
                            outFile.Write(mensaje);
                        else
                            outFile.Write(mensaje + Environment.NewLine);
                    }
                }
            }
            catch (Exception exception)
            {
                //Sat.Condonaciones.Iu.Comun.AccesoLogEventos.EscribirEntradaError(exception, 2050);

            }
        }
    }
}
