using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConectBaseDatos.Ent
{
    [Serializable]
    public class entParametro
    {
        

        public string p_Nombre { get; set; }
        public Array.ListaComponentes.BD_TipoParametro p_Tipo { get; set; }
        public Nullable<int> p_Tamaño { get; set; }
        public object p_Valor { get; set; }
    }
}
