using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAT.CreditosFiscales.Motor.Entidades.Catalogos
{
    public class CatFormatoImpresionEquivalencia
    {
        public int IdAplicacion { get; set; }

        public int IdTipoDocumento { get; set; }

        public int IdFormatoImpresion { get; set; }

        public string XPathEvaluacion { get; set; }

        public string ValorEvaluacion { get; set; }
    }
}
