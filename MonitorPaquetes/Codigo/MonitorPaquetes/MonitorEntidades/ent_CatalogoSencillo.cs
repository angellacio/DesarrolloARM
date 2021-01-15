using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorEntidades
{
    [Serializable]
    public class ent_CatalogoSencillo
    {
        public override string ToString()
        {
            return string.Format("{0}|{1}, {4}, {2}, {3}", D000_nId, D000_sId, D010_sAcronimo, D020_sDescripcion, D030_bEstatus);
        }

        public int D000_nId { get; set;}
        public string D000_sId { get; set; }
        public string D010_sAcronimo { get; set; }
        public string D020_sDescripcion { get; set; }
        public Boolean D030_bEstatus { get; set; }
    }
}
