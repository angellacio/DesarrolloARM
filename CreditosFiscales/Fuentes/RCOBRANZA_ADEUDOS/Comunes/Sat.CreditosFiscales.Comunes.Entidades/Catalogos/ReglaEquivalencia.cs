using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E = Sat.CreditosFiscales.Comunes.Herramientas.Enumeradores.CustomAttributes;

namespace Sat.CreditosFiscales.Comunes.Entidades.Catalogos
{
    public class ReglaEquivalencia
    {
        public ReglaEquivalencia()
        {
        }

        public int IdReglaEquivalencia { get; set; }
        public int IdAplicacion { get; set; }
        public int IdTipoDocumento { get; set; }
        public string Descripcion { get; set; }
        public int IdTipoObjeto { get; set; }
        public string ValorObjeto { get; set; }
        public string ValorRetorno { get; set; }
        public string XPathEvaluacion { get; set; }
        public string ValorEvaluacion { get; set; }
        public bool Activo { get; set; }
        public int Secuencia { get; set; }
        public int IdPadre { get; set; }
        public string NombreAplicacion { get; set; }
        public string NombreDocumento { get; set; }
        public string NombreTipoObjeto { get; set; }
        public string Excepcion { get; set; }

        public enum Campos
        {
            [E.NombreCampo("IdReglaEquivalencia")]
            [E.NombreParametroSql("@IdReglaEquivalencia")]
            IdReglaEquivalencia,

            [E.NombreCampo("IdAplicacion")]
            [E.NombreParametroSql("@IdAplicacion")]
            IdAplicacion,

            [E.NombreCampo("IdTipoDocumento")]
            [E.NombreParametroSql("@IdTipoDocumento")]
            IdTipoDocumento,

            [E.NombreCampo("Descripcion")]
            [E.NombreParametroSql("@Descripcion")]
            Descripcion,

            [E.NombreCampo("IdTipoObjeto")]
            [E.NombreParametroSql("@IdTipoObjeto")]
            IdTipoObjeto,

            [E.NombreCampo("ValorObjeto")]
            [E.NombreParametroSql("@ValorObjeto")]
            ValorObjeto,

            [E.NombreCampo("ValorRetorno")]
            [E.NombreParametroSql("@ValorRetorno")]
            ValorRetorno,

            [E.NombreCampo("XPathEvaluacion")]
            [E.NombreParametroSql("@XPathEvaluacion")]
            XPathEvaluacion,

            [E.NombreCampo("ValorEvaluacion")]
            [E.NombreParametroSql("@ValorEvaluacion")]
            ValorEvaluacion,

            [E.NombreCampo("Activo")]
            [E.NombreParametroSql("@Activo")]
            Activo,

            [E.NombreCampo("Secuencia")]
            [E.NombreParametroSql("@Secuencia")]
            Secuencia,

            [E.NombreCampo("IdPadre")]
            [E.NombreParametroSql("@IdPadre")]
            IdPadre,

            [E.NombreCampo("NombreAplicacion")]            
            NombreAplicacion,

            [E.NombreCampo("NombreDocumento")]            
            NombreDocumento ,

            [E.NombreCampo("NombreTipoObjeto")]
            NombreTipoObjeto
        }
    }
}
