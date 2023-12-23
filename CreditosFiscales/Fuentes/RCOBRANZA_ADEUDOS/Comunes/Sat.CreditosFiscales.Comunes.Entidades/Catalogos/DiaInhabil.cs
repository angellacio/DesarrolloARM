
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Comunes.Entidades.Catalogos:Sat.CreditosFiscales.Comunes.Entidades.Catalogos.CustomAttributes:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using E = Sat.CreditosFiscales.Comunes.Herramientas.Enumeradores.CustomAttributes;

namespace Sat.CreditosFiscales.Comunes.Entidades.Catalogos
{
    [DataContract]
    public class DiaInhabil
    {
        [DataMember]
        public int IdFecha { get; set; }
        [DataMember]
        public DateTime Fecha { get; set; }

        public enum Campos
        {
            [E.NombreCampo("IdFecha")]
            [E.NombreParametroSql("@IdFecha")]
            IdFecha = 1,

            [E.NombreCampo("Fecha")]
            [E.NombreParametroSql("@Fecha")]
            Fecha,
        }
    }
}
