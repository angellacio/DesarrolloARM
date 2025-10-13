using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Personal.Entidades.Canciones
{
    public class datCatalogos
    {
        public datCatalogos() 
        {
            lstGeneros = new List<entGenero>();
        }
        public List<entGenero> lstGeneros {  get; set; }
    }
}
