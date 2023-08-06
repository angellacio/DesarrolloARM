using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WA_Entidades
{
    public class EntCatalogoSensillo
    {
        public EntCatalogoSensillo() {
            IdTabla = null;
            Tabla = null;
            EstadoTabla = null;
            IdCatalogo = null;
            Acronimo = null;
            Orden = null;
            Observaciones = null;
            Catalogo = null;
            EstadaCatalogo = null;
        } 
        public int? IdTabla { get; set; }
        public string Tabla { get; set; }
        public Boolean? EstadoTabla { get; set; }
        public int? IdCatalogo { get; set; }
        public string Acronimo { get; set; }
        public int? Orden { get; set; }
        public string Observaciones { get; set; }
        public string Catalogo { get; set; }
        public Boolean? EstadaCatalogo { get; set; }
    }
}
