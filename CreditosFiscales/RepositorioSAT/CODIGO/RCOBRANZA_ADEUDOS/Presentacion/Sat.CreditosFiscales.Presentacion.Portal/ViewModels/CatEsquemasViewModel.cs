using System;
using System.Xml;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Sat.CreditosFiscales.Comunes.Entidades.Catalogos;

namespace Sat.CreditosFiscales.Presentacion.Portal.ViewModels
{

    public class CatEsquemasViewModel
    {
        public List<CatEsquemas> Lista { set; get; }
        public string IdEsquema { set; get; }
        public string Descripcion { set; get; }

    }

    public class CatEsquemasCatalogoViewModel
    {
        public Int16 IdEsquema { set; get; }
        [Required]
        public string Descripcion { set; get; }
        [Required]
        public string Esquema { set; get; }
        public string TargetNamespace { set; get; }
        [Required]
        public int Accion { set; get; }
    }

}

