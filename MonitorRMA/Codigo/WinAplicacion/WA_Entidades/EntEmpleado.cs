using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WA_Entidades
{
    public class EntEmpleado
    {
        public EntEmpleado() { }
        public int? IdEmpleado { get; set; }
        public string Empelado { get; set; }
        public string Apellido_Uno { get; set; }
        public string Apellido_Dos { get; set; }
        public string Usuario { get; set; }
        public int? nOrden { get; set; }
        public Boolean? Root { get; set; }
        public Boolean? Estado { get; set; }
    }
}
