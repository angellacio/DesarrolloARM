
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Comunes.Entidades.GeneraFormato:Sat.CreditosFiscales.Comunes.Entidades.GeneraFormato.DescuentoConcepto:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using E = Sat.CreditosFiscales.Comunes.Herramientas.Enumeradores.CustomAttributes;

namespace Sat.CreditosFiscales.Comunes.Entidades.GeneraFormato
{
    [DataContract]
    public class DescuentoConcepto
    {
        [DataMember]
        public int IdDocumentoConcepto { get; set; }
        [DataMember]
        public int IdDocumento { get; set; }
        [DataMember]
        public Int64 IdSolicitud { get; set; }
        [DataMember]
        public string IdDescuento { get; set; }
        [DataMember]
        public decimal ImporteDescuento { get; set; }

        public enum Campos
        {
            [E.NombreCampo("IdDescuento")]
            [E.NombreParametroSql("@IdDescuento")]
            IdDescuento = 1,

            [E.NombreCampo("IdDocumentoConcepto")]
            [E.NombreParametroSql("@IdDocumentoConcepto")]
            IdDocumentoConcepto,

            [E.NombreCampo("IdDocumento")]
            [E.NombreParametroSql("@IdDocumento")]
            IdDocumento,

            [E.NombreCampo("IdSolicitud")]
            [E.NombreParametroSql("@IdSolicitud")]
            IdSolicitud,
            
            [E.NombreCampo("ImporteDescuento")]
            [E.NombreParametroSql("@ImporteDescuento")]
            ImporteDescuento,

        }
    }
}
