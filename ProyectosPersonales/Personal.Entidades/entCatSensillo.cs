using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Personal.Entidades
{
    public class entCatSensillo
    {
        public entCatSensillo() {
            nId = -1;
            sAcronimo = "";
            sDescripcion = "";
            bEstado = false;
        }
        public int nId { get; set; }
        public string sAcronimo { get; set; }
        public string sDescripcion { get; set; }
        public Boolean bEstado { get; set; }
    }
}
