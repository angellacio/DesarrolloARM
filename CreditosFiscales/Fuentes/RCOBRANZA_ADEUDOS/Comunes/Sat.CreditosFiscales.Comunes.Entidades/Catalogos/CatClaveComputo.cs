//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Comunes.Entidades.Catalogos:Sat.CreditosFiscales.Comunes.Entidades.Catalogos.CatClaveComputo:1:27/08/2013[Assembly:1.0:27/08/2013])


using System;
using E = Sat.CreditosFiscales.Comunes.Herramientas.Enumeradores.CustomAttributes;

namespace Sat.CreditosFiscales.Comunes.Entidades.Catalogos
{
    public class CatClaveComputo
    {
        public string ClaveComputo { set; get; }
        public string Descripcion { set; get; }
        public enum Accion { 
            Guardar = 1,
            Actualizar = 2
        }


        public CatClaveComputo()
        {
        }

        public enum Campos
        {
            [E.NombreCampo("ClaveComputo")]
            [E.NombreParametroSql("@ClaveComputo")]
            ClaveComputo,

            [E.NombreCampo("Descripcion")]
            [E.NombreParametroSql("@Descripcion")]
            Descripcion
        }
    }
}
