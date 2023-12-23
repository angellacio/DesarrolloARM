
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Comunes.Entidades:Sat.CreditosFiscales.Comunes.Entidades.Usuario:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using E = Sat.CreditosFiscales.Comunes.Herramientas.Enumeradores.CustomAttributes;

namespace Sat.CreditosFiscales.Comunes.Entidades
{
    [DataContract]
    public class Usuario
    {
        [DataMember]
        public string Rfc { get; set; }
        [DataMember]
        public List<string> Rfcs { get; set; }
        [DataMember]
        public string RazonSocial { get; set; }
        [DataMember]
        public string Nombres { get; set; }
        [DataMember]
        public string ApellidoPaterno { get; set; }
        [DataMember]
        public string ApellidoMaterno { get; set; }
        [DataMember]
        public byte IdALR { get; set; }
        [DataMember]
        public bool PersonaFisica { get; set; }
        [DataMember]
        public string BoId { get; set; }
        [DataMember]
        public bool EsContribuyentePuro { get; set; }
        [DataMember]
        public byte IdTipoLineaMAT { get; set; }
        [DataMember]
        public string IdPersonaMAT { get; set; }

        public Usuario()
        {
        }

        public enum Campos
        {           

            [E.NombreCampo("Rfc")]
            [E.NombreParametroSql("@Rfc")]
            Rfc,

            [E.NombreCampo("IdALR")]
            [E.NombreParametroSql("@IdALR")]
            IdALR,

            [E.NombreCampo("RazonSocial")]
            [E.NombreParametroSql("@RazonSocial")]
            RazonSocial,

            [E.NombreCampo("Nombres")]
            [E.NombreParametroSql("@Nombres")]
            Nombres,

            [E.NombreCampo("ApellidoPaterno")]
            [E.NombreParametroSql("@ApellidoPaterno")]
            ApellidoPaterno,

            [E.NombreCampo("ApellidoMaterno")]
            [E.NombreParametroSql("@ApellidoMaterno")]
            ApellidoMaterno,

            [E.NombreCampo("PersonaFisica")]
            [E.NombreParametroSql("@PersonaFisica")]
            PersonaFisica,

            [E.NombreCampo("IdTipoLineaMAT")]
            [E.NombreParametroSql("@IdTipoLineaMAT")]
            IdTipoLineaMAT,

            [E.NombreCampo("IdPersonaMAT")]
            [E.NombreParametroSql("@IdPersonaMAT")]
            IdPersonaMAT
        }
    }
}
