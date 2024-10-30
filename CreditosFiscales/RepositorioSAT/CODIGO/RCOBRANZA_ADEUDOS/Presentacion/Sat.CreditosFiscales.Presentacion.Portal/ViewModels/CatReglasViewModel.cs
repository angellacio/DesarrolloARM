using System;
using System.Xml;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Sat.CreditosFiscales.Comunes.Entidades.Catalogos;

namespace Sat.CreditosFiscales.Presentacion.Portal.ViewModels
{

    public class CatReglasViewModel
    {
        public List<CatReglas> Lista { set; get; }
        public string IdRegla { set; get; }
        public string Descripcion { set; get; }

    }

    public class CatReglasCatalogoViewModel
    {
        public Guid IdRegla { set; get; }
        [Required]
        public string Descripcion { set; get; }
        [Required]
        public string Regla { set; get; }
        public bool EsValidacion { set; get; }
        [Required]
        public int Accion { set; get; }
    }

}

