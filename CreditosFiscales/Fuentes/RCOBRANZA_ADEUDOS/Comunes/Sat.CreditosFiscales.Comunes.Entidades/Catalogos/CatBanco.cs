
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Comunes.Entidades.Catalogos:Sat.CreditosFiscales.Comunes.Entidades.Catalogos.CatBanco:1:12/07/2013[Assembly:1.0:12/07/2013])

namespace Sat.CreditosFiscales.Comunes.Entidades.Catalogos
{
    /// <summary>
    /// Catálogo de bancos
    /// </summary>
    public class CatBanco
    {
        /// <summary>
        /// Identificador del banco.
        /// </summary>
        public int IdBanco { get; set; }

        /// <summary>
        /// Descripción del banco.
        /// </summary>
        public string Descripcion { get; set; }
    }
}
