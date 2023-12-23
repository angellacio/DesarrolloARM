
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Comunes.Entidades.GeneraFormato:Sat.CreditosFiscales.Comunes.Entidades.GeneraFormato.DocumentoDeterminanteConceptoHijo:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using E = Sat.CreditosFiscales.Comunes.Herramientas.Enumeradores.CustomAttributes;

namespace Sat.CreditosFiscales.Comunes.Entidades.GeneraFormato
{
    [DataContract]
    public class DocumentoDeterminanteConceptoHijo
    {
        #region Campos
        [DataMember]
        public int IdDocumentoConcepto { get; set; }
        [DataMember]
        public int IdDocumentoConceptoHijo { get; set; }
        [DataMember]
        public int IdDocumento { get; set; }
        [DataMember]
        public Int64 IdSolicitud { get; set; }
        [DataMember]
        public int CreditoSIR { get; set; }               
        [DataMember]
        public int IdConcepto { get; set; }
        [DataMember]
        public string IdMotivo { get; set; }
        [DataMember]
        public byte IdPeriodicidad { get; set; }
        [DataMember]
        public byte IdPeriodo { get; set; }
        [DataMember]
        public int Ejercicio { get; set; }
        [DataMember]
        public decimal ImporteHistorico { get; set; }
        [DataMember]
        public decimal ImporteParteActualizada { get; set; }
        [DataMember]
        public decimal ImporteDescuentos { get; set; }
        [DataMember]
        public decimal ImporteCondonacion { get; set; }
        [DataMember]
        public decimal ImportePagar { get; set; }
        [DataMember]
        public decimal ImporteRecargos { get; set; }
        [DataMember]
        public DateTime? FechaCausacion { get; set; }
        [DataMember]
        public DateTime? FechaNotificacion { get; set; }

        [DataMember]
        public List<DescuentoConceptoHijo> Descuentos { get; set; }

        #endregion

        #region Constructor
        public DocumentoDeterminanteConceptoHijo()
        {
            Descuentos = new List<DescuentoConceptoHijo>();
        }
        #endregion

        /// <summary>
        /// Enumerador que representa a los campos de  la tabla TblDocumentoDeterminanteConcepto, 
        /// así como los respectivos nombres de campo y parametro de SQL.
        /// </summary>
        public enum Campos
        {
            [E.NombreCampo("IdDocumentoConcepto")]
            [E.NombreParametroSql("@IdDocumentoConcepto")]
            IdDocumentoConcepto =1,

            [E.NombreCampo("IdDocumentoConceptoHijo")]
            [E.NombreParametroSql("@IdDocumentoConceptoHijo")]
            IdDocumentoConceptoHijo,

            [E.NombreCampo("IdDocumento")]
            [E.NombreParametroSql("@IdDocumento")]
            IdDocumento,

            [E.NombreCampo("IdSolicitud")]
            [E.NombreParametroSql("@IdSolicitud")]
            IdSolicitud,

            [E.NombreCampo("CreditoSIR")]
            [E.NombreParametroSql("@CreditoSIR")]
            CreditoSIR,

            [E.NombreCampo("idConcepto")]
            [E.NombreParametroSql("@IdConceptoLey")]
            IdConcepto ,

            [E.NombreCampo("IdMotivo")]
            [E.NombreParametroSql("@IdMotivo")]
            IdMotivo,

            [E.NombreCampo("IdPeriodicidad")]
            [E.NombreParametroSql("@IdPeriodicidad")]
            IdPeriodicidad,

            [E.NombreCampo("IdPeriodo")]
            [E.NombreParametroSql("@IdPeriodo")]
            IdPeriodo,

            [E.NombreCampo("Ejercicio")]
            [E.NombreParametroSql("@Ejercicio")]
            Ejercicio,

            [E.NombreCampo("ImporteDescuentos")]
            [E.NombreParametroSql("@ImporteDescuentos")]
            ImporteDescuentos,          

            [E.NombreCampo("ImporteHistorico")]
            [E.NombreParametroSql("@ImporteHistorico")]
            ImporteHistorico,

            [E.NombreCampo("ImporteParteActualizada")]
            [E.NombreParametroSql("@ImporteParteActualizada")]
            ImporteParteActualizada,          

            [E.NombreCampo("ImporteCondonacion")]
            [E.NombreParametroSql("@ImporteCondonacion")]
            ImporteCondonacion,

            [E.NombreCampo("ImportePagar")]
            [E.NombreParametroSql("@ImportePagar")]
            ImportePagar,

            [E.NombreCampo("ImporteRecargos")]
            [E.NombreParametroSql("@ImporteRecargos")]
            ImporteRecargos,

    
            [E.NombreCampo("FechaCausacion")]
            [E.NombreParametroSql("@FechaCausacion")]
            FechaCausacion,

            [E.NombreCampo("FechaNotificacion")]
            [E.NombreParametroSql("@FechaNotificacion")]
            FechaNotificacion
        }
    }


}

