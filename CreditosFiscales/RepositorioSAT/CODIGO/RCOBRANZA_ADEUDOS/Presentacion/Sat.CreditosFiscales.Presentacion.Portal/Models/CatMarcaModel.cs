
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Presentacion.Portal.Models:Sat.CreditosFiscales.Presentacion.Portal.Models.CatMarcaModel:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sat.CreditosFiscales.Comunes.Entidades.Catalogos;

namespace Sat.CreditosFiscales.Presentacion.Portal.Models
{
    public class CatMarcaModel:CatMarca
    {
        public string Prefijo { get; set; }
        public string Entidad { get; set; }
       
    }
}