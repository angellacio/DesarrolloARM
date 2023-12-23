using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Sat.CreditosFiscales.Comunes.Entidades.Servicios;
using E = Sat.CreditosFiscales.Comunes.Herramientas.Enumeradores.CustomAttributes;

namespace Sat.CreditosFiscales.Comunes.Entidades.GeneraFormato
{
    /// <summary>
    /// Clase que representa a la tabla TblDocumentoDeterminante.
    /// </summary>    
    [DataContract]
    public class DocumentoDeterminante
    {
        #region Campos
        [DataMember]
        public int IdDocumento { get; set; }
        [DataMember]
        public Int64 IdSolicitud { get; set; }
        [DataMember]
        public string NumDocumento { get; set; }
        [DataMember]
        public DateTime FechaDocumento { get; set; }
        [DataMember]
        public byte IdALR { get; set; }
        [DataMember]
        public int IdAutoridad { get; set; }
        [DataMember]
        public string Rfc { get; set; }
        [DataMember]
        public decimal ImporteHistorico { get; set; }
        [DataMember]
        public decimal ImporteActualizacion { get; set; }
        [DataMember]
        public decimal ImporteDescuentos { get; set; }
        [DataMember]
        public string Observaciones { get; set; }
        [DataMember]
        public bool Marcado { get; set; }
        [DataMember]
        public List<DocumentoDeterminanteConcepto> Conceptos { get; set; }

        #endregion 

        #region Constructor
        public DocumentoDeterminante()
        {
            Conceptos = new List<DocumentoDeterminanteConcepto>();
        }       
        #endregion

        /// <summary>
        /// Enumerador que representa a los campos de  la tabla TblDocumentoDeterminante, 
        /// así como los respectivos nombres de campo y parametro de SQL.
        /// </summary>
        public enum Campos
        {
            [E.NombreCampo("IdDocumento")]
            [E.NombreParametroSql("@IdDocumento")]
            IdDocumento = 1,

            [E.NombreCampo("NumDocumento")]
            [E.NombreParametroSql("@NumDocumento")]
            NumDocumento,

            [E.NombreCampo("FechaDocumento")]
            [E.NombreParametroSql("@FechaDocumento")]
            FechaDocumento,

            [E.NombreCampo("IdSolicitud")]
            [E.NombreParametroSql("@IdSolicitud")]
            IdSolicitud,

            [E.NombreCampo("IdALR")]
            [E.NombreParametroSql("@IdALR")]
            IdALR,

            [E.NombreCampo("IdAutoridad")]
            [E.NombreParametroSql("@IdAutoridad")]
            IdAutoridad,

            [E.NombreCampo("Autoridad")]
            [E.NombreParametroSql("@Autoridad")]
            Autoridad,

            [E.NombreCampo("Rfc")]
            [E.NombreParametroSql("@Rfc")]
            Rfc,

            [E.NombreCampo("ImporteHistorico")]
            [E.NombreParametroSql("@ImporteHistorico")]
            ImporteHistorico,

            [E.NombreCampo("ImporteActualizacion")]
            [E.NombreParametroSql("@ImporteActualizacion")]
            ImporteActualizacion,

            //[E.NombreCampo("ImporteRecargos")]
            //[E.NombreParametroSql("@ImporteRecargos")]
            //ImporteRecargos,

            [E.NombreCampo("ImporteDescuentos")]
            [E.NombreParametroSql("@ImporteDescuentos")]
            ImporteDescuentos,

            //[E.NombreCampo("ImporteSaldoActualizado")]
            //[E.NombreParametroSql("@ImporteSaldoActualizado")]
            //ImporteSaldoActualizado,

            //[E.NombreCampo("ImportePagos")]
            //[E.NombreParametroSql("@ImportePagos")]
            //ImportePagos,

            [E.NombreCampo("Observaciones")]
            [E.NombreParametroSql("@Observaciones")]
            Observaciones
        }
    }

}
