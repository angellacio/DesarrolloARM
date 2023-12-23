
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Comunes.Entidades.GeneraFormato:Sat.CreditosFiscales.Comunes.Entidades.GeneraFormato.LineaCaptura:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using Sat.CreditosFiscales.Comunes.Entidades.GeneraFormato;
using E = Sat.CreditosFiscales.Comunes.Herramientas.Enumeradores.CustomAttributes;


namespace Sat.CreditosFiscales.Comunes.Entidades.GeneraFormato
{
    /// <summary>
    /// Clase que define una línea de captura
    /// </summary>
    public class LineaCaptura
    {
        /// <summary>
        /// Identificador de la línea de captura
        /// </summary>
        public Int64 IdLineaCaptura { set; get; }
        /// <summary>
        /// Línea de captura
        /// </summary>
        public string Linea { set; get; }
        /// <summary>
        /// Folio de la línea de captura
        /// </summary>
        public string Folio { set; get; }
        /// <summary>
        /// Fecha de vencimiento para el pago de la línea de captura
        /// </summary>
        public string FechaVencimiento { set; get; }
        /// <summary>
        /// Importe total a pagar de la línea de captura
        /// </summary>
        public decimal ImporteTotal { set; get; }
        /// <summary>
        /// Fecha de pago de la línea de captura
        /// </summary>
        public string FechaPago { set; get; }
        /// <summary>
        /// Fecha de emisión de la línea de captura
        /// </summary>
        public string FechaEmision { set; get; }
        
        /// <summary>
        /// Lista de documentos asociados a la línea de captura
        /// </summary>
        public List<IdentificadorDocumento> Documentos { get; set; }

        #region Constructor
        public LineaCaptura()
        {
            Documentos = new List<IdentificadorDocumento>();
        }
        #endregion


        /// <summary>
        /// Enumerador que representa a los campos de  la tabla TblLineaCaptura, 
        /// así como los respectivos nombres de campo y parametro de SQL.
        /// </summary>
        public enum Campos
        {
            [E.NombreCampo("IdLineaCaptura")]
            [E.NombreParametroSql("@IdLineaCaptura")]
            IdLineaCaptura = 1,

            [E.NombreCampo("Linea")]
            [E.NombreParametroSql("@LineaCaptura")]
            Linea,

            [E.NombreCampo("Folio")]
            [E.NombreParametroSql("@Folio")]
            Folio,

            [E.NombreCampo("FechaVencimiento")]
            [E.NombreParametroSql("@FechaVencimiento")]
            FechaVencimiento,

            [E.NombreCampo("ImporteTotal")]
            [E.NombreParametroSql("@ImporteTotal")]
            ImporteTotal,

            [E.NombreCampo("FechaPago")]
            [E.NombreParametroSql("@FechaPago")]
            FechaPago,

            [E.NombreCampo("FechaEmision")]
            [E.NombreParametroSql("@FechaEmision")]
            FechaEmision
        }

    }

    /// <summary>
    /// Clase para identificar el documento asociado a la línea de captura
    /// </summary>
    public class IdentificadorDocumento
    {
        /// <summary>
        /// Identificador del documento
        /// </summary>
        public long IdDocumento { get; set; }
        /// <summary>
        /// Número de documento
        /// </summary>
        public string NumDocumento { get; set; }
    }
}
