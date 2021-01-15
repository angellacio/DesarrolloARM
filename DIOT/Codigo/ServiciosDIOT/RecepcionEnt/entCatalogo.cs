using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecepcionEnt
{
    [Serializable]
    public class entCatalogo
    {
        public override string ToString()
        {
            return string.Format("{0}|{1} :: {2} :: {3} :: {4}", nId, sId, sAcronimo, sDescripcion, bEstatus);
        }

        public entCatalogo()
        {
            nId = -1;
            sId = "";
            sAcronimo = "";
            sDescripcion = "";
            bEstatus = false;
        }

        public int nId { get; set; }
        public string sId { get; set; }
        public string sAcronimo { get; set; }
        public string sDescripcion { get; set; }
        public Boolean bEstatus { get; set; }
    }
}
