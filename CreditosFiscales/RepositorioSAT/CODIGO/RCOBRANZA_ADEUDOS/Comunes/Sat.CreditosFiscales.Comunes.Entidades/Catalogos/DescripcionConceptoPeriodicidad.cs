
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Comunes.Entidades.Catalogos:Sat.CreditosFiscales.Comunes.Entidades.Catalogos.DescripcionConceptoPeriodicidad:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E = Sat.CreditosFiscales.Comunes.Herramientas.Enumeradores.CustomAttributes;

namespace Sat.CreditosFiscales.Comunes.Entidades.Catalogos
{
    public class DescripcionConceptoPeriodicidad
    {
        public string DescripcionPeriodicidad { get; set; }

        public string DescripcionPeriodo { get; set; }

        public string DescripcionConcepto { get; set; }

        public enum Campos
        {
            [E.NombreCampo("DesPeriodicidad")]
            [E.NombreParametroSql("@IdPeriodicidad")]
            DescripcionPeriodicidad = 1,

            [E.NombreCampo("DesPeriodo")]
            [E.NombreParametroSql("@IdPeriodo")]
            DescripcionPeriodo,

            [E.NombreCampo("DesConcepto")]
            [E.NombreParametroSql("@IdConcepto")]
            DescripcionConcepto
            
        }
    }
}
