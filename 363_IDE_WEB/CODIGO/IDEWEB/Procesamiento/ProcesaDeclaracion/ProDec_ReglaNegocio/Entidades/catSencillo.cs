using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProDec_ReglaNegocio.Entidades
{
    [Serializable]
    public class catSencillo
    {
        public int nID { get; set; }
        public string sID { get; set; }
        public string sAcronimo { get; set; }
        public string sDescripcion { get; set; }
        public Boolean bEstatus { get; set; }
    }
}
