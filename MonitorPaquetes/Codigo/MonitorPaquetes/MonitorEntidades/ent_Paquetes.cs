using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorEntidades
{
    [Serializable]
    public class ent_Paquetes
    {
        public override string ToString()
        {
            return string.Format("{0}, {1:dd/MM/yyyy}, Ult.RDL {2}, Ult.Mov {3:dd/MM/yyyy HH:mm}, Est {4}", D010_sPaquete, D020_fAlta, D40_nUltimaRDL, D41_fUltimoMoviminetoMoni, D034_sEstatus);
        }

        string sEstatus;

        public string D000_sIncidente { get; set; }
        public string D010_sPaquete { get; set; }
        public DateTime D020_fAlta { get; set; }
        public Boolean D031_bDOC_PRU { get; set; }
        public string D032_sObservaciones { get; set; }
        public Boolean D033_bNotificado { get; set; }
        public string D034_sEstatus
        {
            get
            {
                return sEstatus;
            }
            set
            {
                sEstatus = value;
            }
        }
        public int D035_nDesarrollador { get; set; }
        public string D036_sDesarrollador { get; set; }
        public Nullable<int> D40_nUltimaRDL
        {
            get
            {
                Nullable<int> nResult = null;

                if (lstRDL.Count > 0) nResult = lstRDL.Max(item => item.D010_nRDL);

                return nResult;
            }
        }
        public Nullable<DateTime> D41_fUltimoMoviminetoMoni
        {
            get
            {
                Nullable<DateTime> nResult = null;

                if (lstRDL.Count > 0) nResult = lstRDL.Where(item => item.D010_nRDL == D40_nUltimaRDL.Value).Max(item => item.D012_fMovimiento);

                return nResult;
            }
        }
        public string D42_sUltimoEstatusMoni
        {
            get
            {
                string nResult = null;

                if (lstRDL.Count > 0)
                {
                    List<ent_RDLs> rdlFind = lstRDL.Where(item => item.D010_nRDL == D40_nUltimaRDL.Value && item.D012_fMovimiento == D41_fUltimoMoviminetoMoni.Value).ToList();
                    nResult = rdlFind[0].D020_sEstatus;
                }

                return nResult;
            }
        }
        public List<ent_RDLs> lstRDL { get; set; }
        public List<ent_Observaciones> lstObservaciones { get; set; }
    }
}
