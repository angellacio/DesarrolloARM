using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sat.CreditosFiscales.Comunes.Entidades.Pagos
{
    public  class InfoArchivoSIAT
    {
        /// <summary>
        /// Nombre del archivo detalle
        /// </summary>
        public string NomArchivoDetalle { get; set; }

        /// <summary>
        /// Nombre del archivo detalle
        /// </summary>
        public string Contenido { get; set; }

        /// <summary>
        /// Informacion de la linea de captura
        /// </summary>
        public TblLineaCaptura Generales { get; set; }

        public String FechaPagoFormato { get; set; }

        public void setGenerales(CreditosFiscales creditosSIAT) 
        {
                TblLineaCaptura lc = new TblLineaCaptura();
                lc.FolioSolicitud = creditosSIAT.DatosGenerales.NumeroDocumento;
                lc.LineaCaptura = creditosSIAT.DatosGenerales.LineaCaptura;
                lc.IdTipoDocumento =(short)creditosSIAT.DatosGenerales.TipoDocumento;
                lc.Rfc = creditosSIAT.DatosGenerales.RFC;
                lc.IdALR = (short)creditosSIAT.DatosGenerales.ALR;
                if (creditosSIAT.DatosGenerales.PagoEfectivo.Importe == 0)//Es virtual
                {
                    lc.ImporteTotal = creditosSIAT.DatosGenerales.PagoVirtual.Importe;
                    this.FechaPagoFormato = creditosSIAT.DatosGenerales.PagoVirtual.FechaPago;
                }
                else//Es efectivo
                {
                    lc.ImporteTotal = creditosSIAT.DatosGenerales.PagoEfectivo.Importe;
                    this.FechaPagoFormato = creditosSIAT.DatosGenerales.PagoEfectivo.FechaPago;
                }
                this.Generales = lc;
        }         

    }
}
