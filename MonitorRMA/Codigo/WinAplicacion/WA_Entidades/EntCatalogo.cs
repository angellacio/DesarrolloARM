using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WA_Entidades
{
    public class EntCatalogo : EntTabla
    {
        public EntCatalogo() {
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
       
        public int? IdCatalogo { get; set; }
        public string Acronimo { get; set; }
        public int? Orden { get; set; }
        public string Observaciones { get; set; }
        public string Catalogo { get; set; }
        public Boolean? EstadaCatalogo { get; set; }
    }
}
