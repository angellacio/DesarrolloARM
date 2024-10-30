//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Comunes.Entidades.Catalogos:Sat.CreditosFiscales.Comunes.Entidades.Catalogos.CatReglas:1:27/08/2013[Assembly:1.0:27/08/2013])

using System;
using System.Xml;
using E = Sat.CreditosFiscales.Comunes.Herramientas.Enumeradores.CustomAttributes;

namespace Sat.CreditosFiscales.Comunes.Entidades.Catalogos
{
    public class CatReglas
    {
        public Guid? IdRegla { set; get; }
        public string Descripcion { set; get; }
        public string Regla { set; get; }
        public bool EsValidacion { set; get; }
        public enum Accion
        {
            Guardar = 1,
            Actualizar = 2,
            Eliminar = 3
        }

        public enum Campos
        {
            [E.NombreCampo("IdRegla")]
            [E.NombreParametroSql("@IdRegla")]
            IdRegla,

            [E.NombreCampo("Descripcion")]
            [E.NombreParametroSql("@Descripcion")]
            Descripcion,


            [E.NombreCampo("Regla")]
            [E.NombreParametroSql("@Regla")]
            Regla,


            [E.NombreCampo("EsValidacion")]
            [E.NombreParametroSql("@EsValidacion")]
            EsValidacion
        }
    }
}
