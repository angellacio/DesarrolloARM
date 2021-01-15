using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorEntidades
{
    [Serializable]
    public class ent_Observaciones
    {
        public override string ToString()
        {
            return string.Format("{0:dd/MM/yyyy hh:mm tt} * {1}", D010_dMensaje, D020_sMensaje);
        }
        public int D000_nID { get; set; }
        public string D001_sIdIncidentes { get; set; }
        public string D002_sIdPaquete { get; set; }
        public DateTime D010_dMensaje { get; set; }
        public string D020_sMensaje { get; set; }
    }
}
