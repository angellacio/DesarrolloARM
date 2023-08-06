using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtileriasBaseDatos.EnumTextos
{
    public class EnumComponentes
    {
        public enum BD_TipoParametro { EnteroNor = 1, EnteroCor = 2, EnteroLar = 3, 
                                       TextoNor = 4, TextoCor = 5, TextoLar = 6,
                                       Fecha = 7, FechaHora = 8, SiNo = 9 }
        public enum BD_TipoComando { StoreProcedure = 1, Text = 2 }

    }
}
