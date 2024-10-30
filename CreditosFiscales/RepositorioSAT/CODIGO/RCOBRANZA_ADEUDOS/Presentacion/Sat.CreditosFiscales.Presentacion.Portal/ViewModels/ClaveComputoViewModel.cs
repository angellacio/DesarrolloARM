using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Sat.CreditosFiscales.Comunes.Entidades.Catalogos;

namespace Sat.CreditosFiscales.Presentacion.Portal.ViewModels
{
    public class ClaveComputoViewModel
    {
        public List<CatClaveComputo> Lista { set; get; }
        public string claveComputo { set; get; }

    }

    public class ClaveComputoCatalogoViewModel
    {
        [Required]
        public string ClaveComputo { set; get; }
        [Required]
        public string Descripcion { set; get; }
        [Required]
        public int Accion { set; get; }
    }
}