using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WA_Entidades
{
    public class EntTabla
    {
        public EntTabla() {
            IdTabla = null;
            Tabla = null;
            EstadoTabla = null;
        }
        public int? IdTabla { get; set; }
        public string Tabla { get; set; }
        public Boolean? EstadoTabla { get; set; }
    }
}
