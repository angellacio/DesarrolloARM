
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Comunes.Entidades.Catalogos:Sat.CreditosFiscales.Comunes.Entidades.Catalogos.ALR:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Runtime.Serialization;
using E=Sat.CreditosFiscales.Comunes.Herramientas.Enumeradores.CustomAttributes;

namespace Sat.CreditosFiscales.Comunes.Entidades.Catalogos
{
    [DataContract]
    public class ALR
    {
        [DataMember]
        public int IdAlr { get; set; }
        [DataMember]
        public string ClaveCorta { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public string Estado { get; set; }
        [DataMember]
        public string Prefijo { get; set; }
        [DataMember]
        public string Telefono { get; set; }
        [DataMember]
        public string RutaInternet { get; set; }

        public enum Campos
        {
            [E.NombreCampo("IdALR")]
            [E.NombreParametroSql("@IdALR")]
            IdAlr = 1,

            [E.NombreCampo("CveCorta")]
            [E.NombreParametroSql("@CveCorta")]
            ClaveCorta,

            [E.NombreCampo("Descripcion")]
            [E.NombreParametroSql("@Descripcion")]
            Descripcion,

            [E.NombreCampo("Estado")]
            [E.NombreParametroSql("@Estado")]
            Estado,

            [E.NombreCampo("Prefijo")]
            [E.NombreParametroSql("@Prefijo")]
            Prefijo,

            [E.NombreCampo("Telefono")]
            [E.NombreParametroSql("@Telefono")]
            Telefono,

            [E.NombreCampo("RutaInternet")]
            [E.NombreParametroSql("@RutaInternet")]
            RutaInternet
        }
    }
}
