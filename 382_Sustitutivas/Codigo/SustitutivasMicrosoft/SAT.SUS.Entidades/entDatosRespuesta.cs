using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAT.SUS.Entidades
{
    [Serializable]
    public class entDatosRespuesta
    {
        public override string ToString()
        {
            return string.Format("Renglones encontrados: {2} ;; nError: {0} ;; sMensajeError: {1} ;;", nEstatus, sMensaje, lstPagos.Count);
        }
        public entDatosRespuesta()
        {
            lstPagos = new List<entDatosPago>();
            nerror = 0;
            smensajeerror = "";
        }

        private int nerror;
        private string smensajeerror;

        public int nEstatus
        {
            get
            {
                int nResult = nerror;

                if (lstPagos != null && lstPagos.Count > 0)
                {
                    lstPagos.ForEach(item =>
                        {
                            if (item.nEstatus == 3)
                            {
                                nResult = 3;
                            }
                        });
                }

                return nResult;
            }
            set
            {
                nerror = value;
            }
        }
        public string sMensaje
        {
            get
            {
                string sResult = smensajeerror;

                if (lstPagos != null && lstPagos.Count > 0)
                {
                    lstPagos.ForEach(item =>
                    {
                        if (item.nEstatus == 3)
                        {
                            sResult = string.Format("{0}\r\n{1}", sResult, item.sMensaje);
                        }
                    });
                }

                return sResult;
            }
            set
            {
                smensajeerror = value;
            }
        }

        public List<entDatosPago> lstPagos { get; set; }
    }
}
