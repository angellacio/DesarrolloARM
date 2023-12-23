using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sat.CreditosFiscales.Archivos
{
    public class Control
    {
        public int totalElementos { get; set; }
        public int actual { get; set; }

        public Control(int totalElementos)
        {
            this.totalElementos = totalElementos;
            actual = 0;
        }

        public int Porcentaje     
        {
            get { return actual * 100 / totalElementos; }
        }
    }
}
