using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sat.CreditosFiscales.Comunes.Entidades.Catalogos;

namespace Sat.CreditosFiscales.Presentacion.Portal.ViewModels
{
    public class ReglasEquivalenciaViewModel
    {
        public List<ReglaEquivalencia> Reglas {get; set;}

        public ReglaEquivalenciaParametroViewModel Parametros { get; set; }

        public List<CatalogoGenerico> CatAplicacion { get; set; }

        public List<CatalogoGenerico> CatTipoObjeto { get; set; }

        public List<CatalogoGenerico> CatTipoDocumento { get; set; }

        public int Accion { get; set; }

        public ReglaEquivalencia Regla { get; set; }
    }
}