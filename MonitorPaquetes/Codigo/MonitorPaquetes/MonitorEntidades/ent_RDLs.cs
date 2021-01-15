using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorEntidades
{
    [Serializable]
    public class ent_RDLs
    {
        public override string ToString()
        {
            return string.Format("{0}, {1:dd/MM/yyyy HH:mm}, {2}", D010_nRDL, D012_fMovimiento, D020_sEstatus);
        }

        public int D010_nRDL { get; set; }
        public string D011_sPaquete { get; set; }
        public Nullable<DateTime> D012_fMovimiento { get; set; }
        public int D013_nEstatus { get; set; }
        public string D020_sEstatus { get; set; }
    }
}
