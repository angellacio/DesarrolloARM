using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecepcionEnt.DyPManejoDatosC
{
    [Serializable]
    public class entCatalogoServicios
    {
        public override string ToString()
        {
            return base.ToString();
        }

        public entCatalogoServicios(int nSer)
        {
            nServicio = nSer;
            sServicio = "";
            sServicioRuta = "";
            bolEstado = false;
        }
        public entCatalogoServicios()
        {
            nServicio = -1;
            sServicio = "";
            sServicioRuta = "";
            bolEstado = false;
        }

        public int nServicio { get; set; }
        public string sServicio { get; set; }
        public string sServicioRuta { get; set; }
        public Boolean bolEstado { get; set; }
    }
}
