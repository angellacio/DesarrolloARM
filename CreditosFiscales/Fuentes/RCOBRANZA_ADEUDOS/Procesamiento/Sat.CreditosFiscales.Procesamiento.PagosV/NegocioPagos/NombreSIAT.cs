using Sat.CreditosFiscales.Comunes.Entidades.Pagos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace Sat.CreditosFiscales.Procesamiento.PagosV.NegocioPagos
{
    public class NombreSIAT
    {
        private int consecutivo { get; set; }
        private String nomArchivo { get; set; }
        private String nomArchivoCifras;
        private String formatoFecha;
        private String extCifrasSIAT;
        private String separadorZip;
        private String extXML;

        bool bitacoriza = Boolean.Parse(ConfigurationManager.AppSettings["Bitacoriza"]);

        public NombreSIAT()
        {
            String nomBase = ConfigurationManager.AppSettings["NomBaseZIPSIAT"].ToString();
            String fechaHoy = DateTime.Now.ToString(ConfigurationManager.AppSettings["ForFechaZIPSIAT"].ToString());
            String separador = ConfigurationManager.AppSettings["SeparadorNomZIPSIAT"].ToString();
            String ruta = ConfigurationManager.AppSettings["RutaArchivosMAT"].ToString();
            String ext = ConfigurationManager.AppSettings["ExtZIP"].ToString();
            nomArchivoCifras = ConfigurationManager.AppSettings["NomBaseCifrasSIAT"];
             formatoFecha = ConfigurationManager.AppSettings["ForNomFechaCifrasSIAT"];
             separadorZip = ConfigurationManager.AppSettings["SeparadorNomZIPSIAT"];
             consecutivo = int.Parse(ConfigurationManager.AppSettings["InicioConsecutivoZIPSIAT"]);
             extCifrasSIAT = ConfigurationManager.AppSettings["ExtCifrasSIAT"];
             extXML = ConfigurationManager.AppSettings["ExtArchivoSIAT"];
           
            while (true)
            {
                nomArchivo=nomBase + fechaHoy + separador + consecutivo+ext;
                String RutaZIP = Path.Combine(ruta, nomArchivo);
                if (!File.Exists(RutaZIP))
                {
                    break;
                }
                consecutivo++;
            }
        }

        public StringBuilder GeneraLineaCifras(InfoArchivoSIAT archivoLocal, int consecutivo)
        {
            String separador = ConfigurationManager.AppSettings["SeparadorCifrasSIAT"];
            String forFechaPagoSIAT = ConfigurationManager.AppSettings["forFechaPagoSIAT"];
            StringBuilder sBuilder = new StringBuilder();
            sBuilder.Append(archivoLocal.Generales.FolioSolicitud);
            sBuilder.Append(separador);
            sBuilder.Append(archivoLocal.FechaPagoFormato);
            sBuilder.Append(separador);
            sBuilder.Append(archivoLocal.Generales.Rfc);
            sBuilder.Append(separador);
            sBuilder.Append(archivoLocal.Generales.ImporteTotal);
            sBuilder.Append(separador);
            sBuilder.Append(archivoLocal.Generales.IdALR);
            sBuilder.Append(separador);
            sBuilder.Append(archivoLocal.Generales.LineaCaptura);
            sBuilder.Append(separador);
            sBuilder.Append(consecutivo.ToString("D2")); 
            sBuilder.Append(separador);
            sBuilder.Append(archivoLocal.Generales.IdTipoDocumento);
            sBuilder.Append("\n");
            return sBuilder;
        }
        public String ObtenerRutaZIP(String ruta)
        {
           return Path.Combine(ruta,nomArchivo);
        }

        public String getCifras() 
        {

            return nomArchivoCifras + DateTime.Now.ToString(formatoFecha) + this.separadorZip +this.consecutivo+ this.extCifrasSIAT;
        }

        public String getArchivoXML(InfoArchivoSIAT arch,int consecutivo)
        {
            if (arch.Generales.FolioSolicitud != null)
            {
                return arch.Generales.FolioSolicitud+"_"+consecutivo.ToString("D2") + extXML;
            }
            else
            {
                return arch.Generales.LineaCaptura + "_" + consecutivo.ToString("D2") + extXML;
            }
        }
    }
}
