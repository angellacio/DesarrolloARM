using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E = Sat.CreditosFiscales.Comunes.Herramientas.Enumeradores.CustomAttributes;

namespace Sat.CreditosFiscales.Comunes.Entidades.Catalogos
{
    public class ConceptoEquivalencia
    {
        public int IdAplicacion { get; set; }
        public string ConceptoOrigen { get; set; }
        public string DescripcionOrigen { get; set; }
        public string ConceptoDyP { get; set; }
        public bool Activo { get; set; }
        public string DescripcionDyP { get; set; }
        public string DescripcionAplicacion { get; set; }

        public enum Campos
        {
            [E.NombreCampo("IdAplicacion")]
            [E.NombreParametroSql("@IdAplicacion")]
            IdAplicacion = 1,

            [E.NombreCampo("ConceptoOrigen")]
            [E.NombreParametroSql("@ConceptoOrigen")]
            ConceptoOrigen,

            [E.NombreCampo("DescripcionOrigen")]
            [E.NombreParametroSql("@DescripcionOrigen")]
            DescripcionOrigen,

            [E.NombreCampo("ConceptoDyP")]
            [E.NombreParametroSql("@ConceptoDyP")]
            ConceptoDyP,

            [E.NombreCampo("Activo")]
            [E.NombreParametroSql("@Activo")]
            Activo,

            [E.NombreCampo("DescripcionDyP")]
            DescripcionDyP,

            [E.NombreCampo("DescripcionAplicacion")]
            DescripcionAplicacion

        }

    }
}
