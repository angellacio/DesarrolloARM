using Sat.CreditosFiscales.Comunes.Entidades.CodigosError;
using Sat.CreditosFiscales.Procesamiento.LogicaNegocio.AccesoLogEventos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Ionic.Zip;

namespace Sat.CreditosFiscales.Procesamiento.Pagos.NegocioPagos
{
    using Sat.CreditosFiscales.Comunes.Entidades.Pagos;
    using Sat.CreditosFiscales.Comunes.Herramientas;
    using Sat.CreditosFiscales.Comunes.Herramientas.Archivo;
    using Sat.CreditosFiscales.Datos.AccesoDatos.Pagos;
    using System.IO;
    using System.Threading.Tasks;
     public class SIAT
    {

        bool bitacoriza = Boolean.Parse(ConfigurationManager.AppSettings["Bitacoriza"]);

        public bool GeneracionLineasDePago(long idProceso, Dictionary<string, object> archivosProcesar, bool esReproceso)
         {
             //var listArchivosGral = new Dictionary<byte, List<InfoArchivoSIR>>();
             var listaArchivos = new List<InfoArchivoSIAT>();
             try
             {
                 if (archivosProcesar != null && archivosProcesar.Count > 0)
                 {
                     var cifraControl = archivosProcesar.FirstOrDefault(x => x.Key.StartsWith("Q"));
                     var detalle = archivosProcesar.FirstOrDefault(x => x.Key.StartsWith("E"));
                     Console.WriteLine("Procesando MAT archivo: " + detalle.Key);
                     ManejadorArchivos.WriteMessage("\nProcesando archivo MAT : " + detalle.Key);
                     listaArchivos = ProcesaArchivo(idProceso, cifraControl, detalle);                             
                     AlmacenarInformacionPorArchivo(idProceso, listaArchivos, esReproceso);
                 }
             }
             catch (Exception exception)
             {
                 if (archivosProcesar != null)
                     LogEventos.EscribirEntradaLog((int)EnumErroresPagos.ErrorProcesandoArchivos,
                                                   archivosProcesar.FirstOrDefault(x => x.Key.StartsWith("Q")).Key, exception);
                 else
                 {
                     LogEventos.EscribirEntradaLog((int)EnumErroresPagos.ErrorProcesandoArchivos, exception);
                 }
                 return false;
             }
             return true;
         }
        
         public bool GeneracionLineasDePagoVirtuales(long idProceso, bool esReproceso,List<TblLineaCaptura> lineas)
         {
             //var listArchivosGral = new Dictionary<byte, List<InfoArchivoSIR>>();
             var listaArchivos = new List<InfoArchivoSIAT>();
             try
             {
                 listaArchivos = ProcesaArchivo(idProceso, lineas); //FGR pensar en los diferentes layouts por tipo de crédito.        
                      AlmacenarInformacionPorArchivoVirtual(idProceso, listaArchivos, esReproceso);
             }
             catch (Exception exception)
             {
               
                     LogEventos.EscribirEntradaLog((int)EnumErroresPagos.ErrorProcesandoArchivos, exception);
                 return false;
             }
             return true;
         }
         private List<InfoArchivoSIAT> ProcesaArchivo(long idProceso, KeyValuePair<string, object> cifraControl, KeyValuePair<string, object> detalle)
         {
             ProcesamientoArchivos procApoyo = new ProcesamientoArchivos();
             var contenidoArchivos = new List<InfoArchivoSIAT>();
             try
             {
                 String formatoFecha = ConfigurationManager.AppSettings["ForFechaPagoSIAT"].ToString();
                    var lineasDeCaptura= procApoyo.ObtenerLineasCaptura(detalle);
                 
                 if (lineasDeCaptura != null)
                 {
                     lineasDeCaptura = procApoyo.ValidarLineasDeCaptura(lineasDeCaptura, detalle, false);

                     foreach (TblLineaCaptura lineaCaptura in lineasDeCaptura) //Base de datos
                     {
                         CreditosFiscales pago = SIATDALPagos.ObtenerPagoSIAT(lineaCaptura, formatoFecha);
                         if (pago.DatosGenerales.TipoLinea != -1 && pago.DatosGenerales.TipoLinea != 0 && pago.DatosGenerales.TipoLinea != 4)//Si es SIAT/MAT
                         {
                             InfoArchivoSIAT archivo = new InfoArchivoSIAT();
                             archivo.Contenido = MetodosComunes.Serializa(pago);
                             archivo.Generales = lineaCaptura;
                             archivo.NomArchivoDetalle = detalle.Key;
                             contenidoArchivos.Add(archivo);
                         }
                         else
                         {
                             SIATDALPagos.InsertarNoProcesados(idProceso, detalle.Key, lineaCaptura.IdSolicitud);
                         }
                     }
                  }
             }
             catch (Exception exception)
             {
                 LogEventos.EscribirEntradaLog((int)EnumErroresPagos.ErrorProcesandoArchivos, exception);
                 throw;
             }
             return contenidoArchivos;
         }
         private List<InfoArchivoSIAT> ProcesaArchivo(long idProceso,List<TblLineaCaptura> lineasDeCaptura)
         {
             ProcesamientoArchivos procApoyo = new ProcesamientoArchivos();
             var contenidoArchivos = new List<InfoArchivoSIAT>();
             try
             {
                 String formatoFecha = ConfigurationManager.AppSettings["ForFechaPagoSIAT"].ToString();
                 if (lineasDeCaptura != null)
                 {
                     if (lineasDeCaptura.Count() != 0)
                     {
                         Console.WriteLine("Procesando Transacciones Virtuales MAT");
                         ManejadorArchivos.WriteMessage("\nProcesando Transacciones Virtuales MAT");
                     }
                     foreach (TblLineaCaptura lineaCaptura in lineasDeCaptura) //Base de datos
                     {
                         CreditosFiscales pago = SIATDALPagos.ObtenerPagoSIAT(lineaCaptura, formatoFecha);

                         if (pago.DatosGenerales.TipoLinea != -1 && pago.DatosGenerales.TipoLinea != 0 && pago.DatosGenerales.TipoLinea != 4)//Si es SIAT/MAT
                         {
                             InfoArchivoSIAT archivo = new InfoArchivoSIAT();
                             archivo.Contenido = MetodosComunes.Serializa(pago);
                             archivo.Generales = lineaCaptura;
                             archivo.NomArchivoDetalle = Constantes.Virtual;
                             contenidoArchivos.Add(archivo);
                         }
                         else
                         {
                             SIATDALPagos.InsertarNoProcesados(idProceso, "Virtual", lineaCaptura.IdSolicitud);
                         }
                     }
                 }
             }
             catch (Exception exception)
             {
                 LogEventos.EscribirEntradaLog((int)EnumErroresPagos.ErrorProcesandoArchivos, exception);
                 throw;
             }
             return contenidoArchivos;
         }
         private void AlmacenarInformacionPorArchivo(long idProceso, List<InfoArchivoSIAT>listArchivosGral, bool esReproceso)
         {
             if (esReproceso)
                 SIATDALPagos.EliminaRegistroExistente(idProceso);

             try
             {
                 foreach ( InfoArchivoSIAT archivoLocal in listArchivosGral)
                 {
                         SIATDALPagos.InsertarDetallePagos(idProceso, archivoLocal);
                 }

             }
             catch (Exception exception)
             {
                 LogEventos.EscribirEntradaLog((int)EnumErroresPagos.ErrorInsertarDetallePagos, exception);
                 throw;
             }

         }
         private void AlmacenarInformacionPorArchivoVirtual(long idProceso, List<InfoArchivoSIAT> listArchivosGral, bool esReproceso)
         {
             try
             {
                 if (esReproceso)
                     DalPagos.EliminaRegistroExistenteVirtual(idProceso, Constantes.Virtual);

                 foreach (InfoArchivoSIAT archivoLocal in listArchivosGral)
                 {

                     SIATDALPagos.InsertarDetallePagos(idProceso, archivoLocal);
                 }
             }
             catch (Exception exception)
             {
                 LogEventos.EscribirEntradaLog((int)EnumErroresPagos.ErrorInsertarDetallePagos, exception);
                 throw;
             }

         }
         public void CrearArchivosSalida(long idProceso)
         {
             var rutaArchivos = ConfigurationManager.AppSettings["RutaArchivosMAT"];
             var rutaRespaldo = ConfigurationManager.AppSettings["RutaArchivosProcesados"];
             String directorioZIP = ConfigurationManager.AppSettings["DirectorioZIPSIAT"];


            //AJGG
            if (bitacoriza)
                ManejadorArchivos.WriteMessage("Rutas Archivos Salida: rutaArchivos|rutaRespaldo|directorioZIP " + rutaArchivos + "|" + rutaRespaldo + "|" + directorioZIP );




            NombreSIAT nombres = new NombreSIAT();

             String nomArchivoCifras = nombres.getCifras();
             try
             {
                 var listArchivos = SIATDALPagos.ObtenerInformacionArchivosDeSalida(idProceso);

                 if (listArchivos.Count() != 0)
                 {
                     ManejadorArchivos.WriteMessage("\n Creando Archivos de salida MAT:" + rutaArchivos);
                     Console.WriteLine("Creando Archivos de salida MAT:" + rutaArchivos);
                 }

                 using (ZipFile zip = new ZipFile())
                 {
                     List<String> consecutivos = new List<String>();
                     foreach(InfoArchivoSIAT archivoLocal in listArchivos )
                     {
                         int consecutivo = 0;
                         var nombreArchivo = nombres.getArchivoXML(archivoLocal,consecutivo);
                         var nombreArchivoPuro = archivoLocal.Generales.FolioSolicitud == null ? archivoLocal.Generales.LineaCaptura : archivoLocal.Generales.FolioSolicitud;
                         if (consecutivos.Contains(nombreArchivoPuro))
                          {
                              consecutivo = consecutivos.Count(n => n == nombreArchivoPuro);
                             nombreArchivo = nombres.getArchivoXML(archivoLocal, consecutivo);
                          } 
                          this.ReemplazaArchivo(new StringBuilder(archivoLocal.Contenido), rutaArchivos, nombreArchivo);
                          zip.AddFile(Path.Combine(rutaArchivos, nombreArchivo), directorioZIP);
                          ManejadorArchivos.EscribeArchivo(nombres.GeneraLineaCifras(archivoLocal, consecutivo), rutaArchivos, nomArchivoCifras);

                          consecutivos.Add(nombreArchivoPuro);   
                     }
                     if (listArchivos.Count() != 0)
                     {
                         zip.AddFile(Path.Combine(rutaArchivos, nomArchivoCifras), directorioZIP);
                         zip.Save(nombres.ObtenerRutaZIP(rutaArchivos));
                         zip.Save(nombres.ObtenerRutaZIP(rutaRespaldo));
                     
                    
                     foreach (ZipEntry entry in zip )
                     {
                         ManejadorArchivos.BorraArchivo(rutaArchivos, entry.FileName );
                     }
                     /*foreach(InfoArchivoSIAT archivoLocal in listArchivos )
                     {
                         var nombreArchivo = nombres.getArchivoXML(archivoLocal,0);
                          ManejadorArchivos.BorraArchivo(rutaArchivos, nombreArchivo);
                     }*/
                     ManejadorArchivos.BorraArchivo(rutaArchivos, nomArchivoCifras);
                     }
                 }
             }
             catch (Exception exception)
             {


                //AJGG
                if (bitacoriza)
                    ManejadorArchivos.WriteMessage("Error al crear ZIP " + exception.Message + "|" + exception.StackTrace);



                LogEventos.EscribirEntradaLog((int)EnumErroresPagos.ErrorCrearProcArchivoSalida, exception);
                 throw;
             }
         }

         public void ReemplazaArchivo(StringBuilder mensaje, string rutaArchivo, string nombreArchivo)
         {
             if (string.IsNullOrEmpty(rutaArchivo)) return;
             rutaArchivo = Path.Combine(rutaArchivo, nombreArchivo);
             if (File.Exists(rutaArchivo))
             {
                 File.Delete(rutaArchivo);
             }
             Escribe(rutaArchivo, mensaje);
         }
         
         private void Escribe(string rutaArchivo, StringBuilder mensaje)
         {

             //bool existeArchivo = !File.Exists(rutaArchivo);
             using (var outFile = new StreamWriter(rutaArchivo, true, Encoding.Default))
             {
                 outFile.Write(mensaje);
             }
         }

        
    }
}
