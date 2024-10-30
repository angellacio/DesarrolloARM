using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAT.CreditosFiscales.Motor.Entidades.Catalogos
{
    public class CatOrdenamientoTransacciones
    {
        public int IdAplicacion { get; set; }

        public int TipoDocumento { get; set; }

        public string Ordenamiento { get; set; }

        public int Secuencia { get; set; }
    }
}
