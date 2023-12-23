//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Comunes.Entidades.Catalogos:Sat.CreditosFiscales.Comunes.Entidades.Catalogos.CatReglas:1:27/08/2013[Assembly:1.0:27/08/2013])

using System;
using System.Xml;
using E = Sat.CreditosFiscales.Comunes.Herramientas.Enumeradores.CustomAttributes;

namespace Sat.CreditosFiscales.Comunes.Entidades.Catalogos
{
    public class CatEsquemas
    {
        public Int16? IdEsquema { set; get; }
        public string Descripcion { set; get; }
        public string Esquema { set; get; }
        public string TargetNamespace { set; get; }
        public enum Accion
        {
            Guardar = 1,
            Actualizar = 2,
            Eliminar = 3
        }

        public enum Campos
        {
            [E.NombreCampo("IdEsquema")]
            [E.NombreParametroSql("@IdEsquema")]
            IdEsquema,

            [E.NombreCampo("Descripcion")]
            [E.NombreParametroSql("@Descripcion")]
            Descripcion,


            [E.NombreCampo("Esquema")]
            [E.NombreParametroSql("@Esquema")]
            Esquema,


            [E.NombreCampo("TargetNamespace")]
            [E.NombreParametroSql("@TargetNamespace")]
            TargetNamespace
        }
    }
}
