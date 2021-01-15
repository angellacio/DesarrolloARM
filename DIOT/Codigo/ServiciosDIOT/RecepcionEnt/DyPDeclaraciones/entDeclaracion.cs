using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecepcionEnt.DyPDeclaraciones
{
    [Serializable]
    public class entDeclaracion
    {
        public override string ToString()
        {
            return string.Format("{0} :: {1} :: {2}", nIdProceso, sNombreArchivo, sMedio);
        }

        public entDeclaracion() { }

        public int nIdProceso { get; set; }
        public string sNombreArchivo { get; set; }
        public string nMedio { get; set; }
        public string sMedio { get; set; }
        public int nEntRec { get; set; }
        public DateTime dPresentacion { get; set; }
        public string sRFCAut { get; set; }
        public string sRFC { get; set; }
        public int nMateria { get; set; }
        public int nMensaje { get; set; }
        public string sMensaje { get; set; }
    }
}
