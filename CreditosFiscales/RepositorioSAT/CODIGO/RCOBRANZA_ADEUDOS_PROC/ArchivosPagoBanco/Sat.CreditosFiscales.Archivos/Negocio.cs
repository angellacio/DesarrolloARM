using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using log4net;
using System.ComponentModel; 
namespace Sat.CreditosFiscales.Archivos
{
    public class Negocio
    {
        public int consecutivo { get; set; }
        public long sumatoria { get; set; }
        private String rutaArchivosProcesar;
        public String nombreArchivoBanco { get; set; }
        public DyP banco { get; set; }
        public Negocio(String rutaArchivosProcesar)
        {
            consecutivo = 2;
            sumatoria = 0;
            this.rutaArchivosProcesar = rutaArchivosProcesar;
        }
        public void reIniciaConsecutivo()
        {
            sumatoria = 0;
            consecutivo = 2;
        }

        public static List<DyP> cargaExcel(String rutaArchivo)
        {
            var book = new Excel();
            return book.ToEntidadHojaExcelList(rutaArchivo);
        }

        public void creaNombreArchivoBanco(TipoArchivoBanco tipoArchivo)
        {
            String fechaformato = DateTime.Parse(banco.FechaDePago).ToString("yyMMdd");
            nombreArchivoBanco= tipoArchivo + banco.ClaveBanco + fechaformato;
        }

        public void creaPrimeraLineaE()
        {
            Log.borraArchivoAnterior(rutaArchivosProcesar + nombreArchivoBanco);
            Log.escribeLinea(rutaArchivosProcesar + nombreArchivoBanco, "1|1|" + banco.ClaveBanco +"|"
                + DateTime.Parse(banco.FechaDePago).ToString("yyyyMMdd")+"|");
        }
        public void creaLineasQ()
        {
            consecutivo -= 2;
            Log.borraArchivoAnterior(rutaArchivosProcesar + nombreArchivoBanco);
            Log.escribeLinea(rutaArchivosProcesar + nombreArchivoBanco, banco.ClaveBanco + "|"
                + DateTime.Parse(banco.FechaDePago).ToString("yyyyMMdd") + "|" + consecutivo + "|" + sumatoria + "|");
            Log.escribeLinea(rutaArchivosProcesar + nombreArchivoBanco, "EOF");
        }
        public void creaUltimaLineaE()
        {
            Log.escribeLinea(rutaArchivosProcesar + nombreArchivoBanco, "EOF");
        }

        public void creaLineaE()
        {
            StringBuilder lineaE = new StringBuilder();
            lineaE.Append("2|");
            lineaE.Append(consecutivo);
            lineaE.Append("|");
            lineaE.Append(banco.LineaCaptura); 
            lineaE.Append("|");
            lineaE.Append(DateTime.Parse(banco.FechaDePago).ToString("yyyyMMdd"));
            lineaE.Append("|");
            lineaE.Append(banco.Hora);
            lineaE.Append("|");
            lineaE.Append(banco.Importe);
            lineaE.Append("|");
            lineaE.Append(banco.NumeroOperacionBanco);
            lineaE.Append("|");
            lineaE.Append(banco.MedioRecepcion);
            lineaE.Append("|");
            lineaE.Append(banco.Version);
            lineaE.Append("|");
            consecutivo++;
            sumatoria += long.Parse(banco.Importe);
            Log.escribeLinea(rutaArchivosProcesar + nombreArchivoBanco, lineaE.ToString());
        }

        public static String creaLineaBancoE(DyP linea)
        {
            String fechaformato = DateTime.Parse(linea.FechaDePago).ToString("yyMMdd");
            return linea.ClaveBanco + fechaformato;
        }
         public static void escribeLog(String rutaLog, Exception ex)
       {
             rutaLog+="CILOG_"+DateTime.Now.ToString("yyMMdd");
             Log.escribeLinea(rutaLog+".txt", ex.Message+" "+ex.InnerException+" "+ex.StackTrace);
       }

         public static void abrirLog(String rutaLog)
         {
             rutaLog += "CILOG_" + DateTime.Now.ToString("yyMMdd");
             Log.abreArchivo(rutaLog + ".txt");
         }
    }

    public enum TipoArchivoBanco
    {
        [Description("Encabezado")]
        Q,
        [Description("Detalle")]
        E
    }
   
}
