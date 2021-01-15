using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorEntidades
{
    [Serializable]
    public class ent_Incidentes
    {
        public override string ToString()
        {
            return string.Format("{0} - Area: {1} - Paquetes: {2}", D010_sIncidente, D001_sIDArea, lstPaquetes.Count);
        }

        public string D001_sIDArea { get; set; }
        public string D002_sArea { get; set; }
        public string D010_sIncidente { get; set; }
        public DateTime D011_fAlta { get; set; }
        public string D012_sDescripcion { get; set; }
        public Boolean D013_bRetroalimentado { get; set; }
        public string D014_sNotaLibera { get; set; }
        public Boolean D015_bPPMC { get; set; }
        public string D020_sPaquetes
        {
            get
            {
                string result = "";

                lstPaquetes.ForEach(itemP =>
                result = string.Format("{0} {1} {2:dd/MM/yyyy HH:mm} {3} \r \n", result, itemP.D010_sPaquete, itemP.D020_fAlta, itemP.D034_sEstatus)
                );

                return result;
            }
        }

        public List<ent_Paquetes> lstPaquetes { get; set; }
        public List<ent_Observaciones> lstObservaciones { get; set; }
    }
}
