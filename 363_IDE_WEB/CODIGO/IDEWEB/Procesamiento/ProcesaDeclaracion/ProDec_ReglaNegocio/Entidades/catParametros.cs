using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ProDec_ReglaNegocio.Entidades
{
    [Serializable]
    public class catParametros
    {
        public string sNombre { get; set; }
        public object oDato { get; set; }
        public SqlDbType sqlTipo { get; set; }
    }
}
