using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorEntidades
{
    [Serializable]
    public class ent_IncidentesPaquetes
    {
        public override string ToString()
        {
            return string.Format("{3}_{0} - {1} - {2}", I010_sIncidente, P010_sPaquete, lstRDL.Count, I002_sArea);
        }

        string sEstatus;

        public string I001_sCabecera
        {
            get
            {
                string sTexto = "";

                sTexto = string.Format("{0} :: {1:dd/MM/yyyy} :: {2} :: {3}", I002_sArea, I011_fAlta, I010_sIncidente, I012_sDescripcion);

                return sTexto;
            }
        }
        public string I002_sArea { get; set; }
        public string I010_sIncidente { get; set; }
        public DateTime I011_fAlta { get; set; }
        public string I012_sDescripcion { get; set; }
        public Boolean I013_bRetroalimentado { get; set; }
        public Boolean I020_bPPMC { get; set; }

        public string P010_sPaquete { get; set; }
        public string P011_sPaquete
        {
            get
            {
                string sResult = "";

                if (P020_fAlta != null)
                sResult = string.Format("{0:dd/MM/yyyy} :: {1} :: {2} :: {3:dd/MM/yyyy} :: {4}", P020_fAlta, P010_sPaquete, P40_nUltimaRDL, P41_fUltimoMoviminetoMoni, P42_Estatus);
                else
                    sResult = string.Format("{0} :: {1} :: {2:dd/MM/yyyy} :: {3}", P010_sPaquete, P40_nUltimaRDL, P41_fUltimoMoviminetoMoni, P42_Estatus);

                return sResult;
            }
        }
        public Nullable<DateTime> P020_fAlta { get; set; }
        public Boolean P031_bDOC_PRU { get; set; }
        public string P032_sObservaciones { get; set; }
        public Boolean P033_bNotificado { get; set; }
        public string P036_sDesarrollador { get; set; }
        public Nullable<int> P40_nUltimaRDL
        {
            get
            {
                Nullable<int> nResult = null;
                ent_RDLs itemRDL = null;
                Nullable<DateTime> fMax = null;

                if (lstRDL.Count > 0)
                {
                    fMax = lstRDL.Max(item => (item.D012_fMovimiento)).Value;
                    itemRDL = lstRDL.Find(item => item.D012_fMovimiento == fMax);
                    nResult = itemRDL.D010_nRDL;
                }

                return nResult;
            }
        }
        public Nullable<DateTime> P41_fUltimoMoviminetoMoni
        {
            get
            {
                Nullable<DateTime> fMax = null;

                if (lstRDL.Count > 0)
                    fMax = lstRDL.Max(item => (item.D012_fMovimiento)).Value;

                return fMax;
            }
        }
        public string P42_Estatus
        {
            get
            {
                ent_RDLs itemRDL = null;
                Nullable<DateTime> fMax = null;

                if (lstRDL.Count > 0)
                {
                    fMax = lstRDL.Max(item => (item.D012_fMovimiento)).Value;
                    itemRDL = lstRDL.Find(item => item.D012_fMovimiento == fMax);

                    if (sEstatus != itemRDL.D020_sEstatus.Trim()) sEstatus = itemRDL.D020_sEstatus.Trim();
                }

                return sEstatus;
            }
            set
            {
                sEstatus = value;
            }
        }
        public List<ent_RDLs> lstRDL { get; set; }
    }
}
