using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sat.CreditosFiscales.Comunes.Entidades.Pagos;
using System.Configuration;
using System.IO;
using Sat.CreditosFiscales.Comunes.Herramientas.Archivo;
using Sat.CreditosFiscales.Procesamiento.LogicaNegocio.AccesoLogEventos;
using Sat.CreditosFiscales.Comunes.Entidades.CodigosError;

namespace Sat.CreditosFiscales.Procesamiento.PagosV.NegocioPagos
{
   public class ContribuyenteCumplido
    {
       private static List<TblLineaCaptura> RFCs = new List<TblLineaCaptura>();
       public static String nombreNuevoArchivo { get; set; }
      /* public static void AddRFC( List<InfoArchivoSIR> creditoSIR)
       {
           foreach (InfoArchivoSIR sir in creditoSIR.GroupBy(g=>g.RFC).Select(group=>group.First()))
           {
               if (!RFCs.Exists(e => e.Equals(new StringBuilder(sir.RFC))))
               {
                   InfoArchivoSIR nuevo = new InfoArchivoSIR();
                   nuevo.RFC = sir.RFC;
                   nuevo.ImportePagado = sir.ImportePagado;
                   RFCs.Add(nuevo);
               }
                  
           }
       }*/

       public static void AddRFC(List<TblLineaCaptura> lcs)
       {
           foreach (TblLineaCaptura lc in lcs)
           {

                   TblLineaCaptura nuevo = new TblLineaCaptura();
                   nuevo.Rfc = lc.Rfc;
                   nuevo.LineaCaptura = lc.LineaCaptura;
                   nuevo.MontoLineaCaptura = lc.MontoLineaCaptura;
                   nuevo.FechaPago = lc.FechaPago;
                   RFCs.Add(nuevo);
           }
       }
       
       public static void CreaArchivoRFCs()
       {
           try
           {
               String formatoFechaLinea = ConfigurationManager.AppSettings["ForFechaLinea"];
               String separador = ConfigurationManager.AppSettings["Separador"];
               String vacio = ConfigurationManager.AppSettings["Nulo"];
               String nomBase=ConfigurationManager.AppSettings["NomBaseLstRFC"];
               String formatoFecha=ConfigurationManager.AppSettings["ForFechaLstRFC"];
               String extension=ConfigurationManager.AppSettings["ExteFechaLstRFC"];
               var rutaArchivosSIR = ConfigurationManager.AppSettings["RutaArchivosParaSIR"];
               
               nombreNuevoArchivo = nomBase + DateTime.Now.ToString(formatoFecha) + extension;

               if (RFCs.Count() != 0)
               {
                   ManejadorArchivos.WriteMessage("\n Llenando archivo de contribuyentes cumplidos: " + nombreNuevoArchivo);
                   Console.WriteLine("Llenando archivo de contribuyentes cumplidos:" + nombreNuevoArchivo);
               }

               foreach (TblLineaCaptura lc in RFCs.OrderBy(x => x.ToString()))
               {
                   ManejadorArchivos.EscribeArchivo(generaLinea(lc, formatoFechaLinea, vacio, separador), rutaArchivosSIR, nombreNuevoArchivo);
               }
           }
           catch (Exception exception)
           {
               LogEventos.EscribirEntradaLog((int)EnumErroresPagos.ErrorMoverArchivoProcExitoso, exception);
           }
       }

       private static StringBuilder generaLinea(TblLineaCaptura lc,String formatoFecha,String vacio,String separador)
       {
           StringBuilder sBuilder = new StringBuilder();
           String sFechaPago;
           String lineaCaptura;
           
           lineaCaptura = (lc.LineaCaptura == null) ? vacio : lc.LineaCaptura;

           sFechaPago = (lc.FechaPago == null) ? vacio : DateTime.Parse(lc.FechaPago.ToString()).ToString(formatoFecha);
 
           sBuilder.Append(lc.Rfc);
           sBuilder.Append(separador);
           sBuilder.Append(lineaCaptura);
           sBuilder.Append(separador);
           sBuilder.Append(lc.MontoLineaCaptura);
           sBuilder.Append(separador);
           sBuilder.Append(sFechaPago);
           sBuilder.Append(Environment.NewLine);
           return sBuilder;
       }

    }
}
