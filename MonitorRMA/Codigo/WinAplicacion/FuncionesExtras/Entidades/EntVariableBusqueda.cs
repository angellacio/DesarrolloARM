using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuncionesExtras.Entidades
{
    public class EntVariableBusqueda
    {
        public EntVariableBusqueda() 
        {
            Variable = "";
            Procesado = false;
        }
        public string Variable {  get; set; }
        public Boolean Procesado { get; set; }
    }
}
