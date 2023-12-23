
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Comunes.Entidades.GeneraFormato:Sat.CreditosFiscales.Comunes.Entidades.GeneraFormato.DocumentoDeterminante:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Sat.CreditosFiscales.Comunes.Entidades.Catalogos;
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
        public DateTime FechaNotificacion { get; set; }
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
        public decimal ImportePagar { get; set; }
        [DataMember]
        public decimal ImporteRecargos { get; set; }
        //[DataMember]
        //public string Observaciones { get; set; }
        [DataMember]
        public List<CatMarca> Marcas { get; set; }
        [DataMember]
        public bool Marcado { get; set; }
        [DataMember]
        public int NumeroConceptos { get; set; }
        [DataMember]
        public List<DocumentoDeterminanteConcepto> Conceptos { get; set; }
        [DataMember]
        public List<LineaCaptura> LineasCaptura { get; set; }
        [DataMember]
        public string FormaPago { get; set; }
        [DataMember]
        public decimal SaldoInformativo { get; set; }
        [DataMember]
        public string IdResolucionMAT { get; set; }
        [DataMember]
        public byte IdTipoLineaMAT { get; set; }
        [DataMember]
        public string IdPersonaMAT { get; set; }


        #endregion 

        #region Constructor
        public DocumentoDeterminante()
        {
            Conceptos = new List<DocumentoDeterminanteConcepto>();
            LineasCaptura = new List<LineaCaptura>();
            Marcas = new List<CatMarca>();
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

            [E.NombreCampo("FechaNotificacion")]
            [E.NombreParametroSql("@FechaNotificacion")]
            FechaNotificacion,

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
            

            [E.NombreCampo("ImporteDescuentos")]
            [E.NombreParametroSql("@ImporteDescuentos")]
            ImporteDescuentos,

            [E.NombreCampo("ImportePagar")]
            [E.NombreParametroSql("@ImportePagar")]
            ImportePagar,

            [E.NombreCampo("ImporteRecargos")]
            [E.NombreParametroSql("@ImporteRecargos")]
            ImporteRecargos,

            [E.NombreCampo("Observaciones")]
            [E.NombreParametroSql("@Observaciones")]
            Observaciones,

            [E.NombreCampo("FormaPago")]
            [E.NombreParametroSql("@FormaPago")]
            FormaPago,

            [E.NombreCampo("SaldoInformativo")]
            [E.NombreParametroSql("@SaldoInformativo")]
            SaldoInformativo,

            [E.NombreCampo("IdResolucionMAT")]
            [E.NombreParametroSql("@IdResolucionMAT")]
            IdResolucionMAT       
        }
    }

}
