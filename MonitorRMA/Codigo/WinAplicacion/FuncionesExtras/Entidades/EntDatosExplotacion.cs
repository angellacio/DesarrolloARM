using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuncionesExtras.Entidades
{
    public class EntDatosExplotacion
    {
        public EntDatosExplotacion()
        {
            D10_DatosASP = new EntDatArchivo();
            D20_Proxy = new EntDatArchivo();
            D30_Procesamiento = new EntDatArchivo();
            D31_Procesamiento = new EntDatArchivo();
            D32_Procesamiento = new EntDatArchivo();
            D33_Procesamiento = new EntDatArchivo();
            D40_SP = new EntDatArchivo();

            NIdTipoComponente = -2;
        }
        public override string ToString()
        {
            string sDatoMostrar = $"{D10_DatosASP}°";
            sDatoMostrar = $"{sDatoMostrar}{D20_Proxy}°";
            sDatoMostrar = $"{sDatoMostrar}{D30_Procesamiento}°";
            sDatoMostrar = $"{sDatoMostrar}{D31_Procesamiento}°";
            sDatoMostrar = $"{sDatoMostrar}{D32_Procesamiento}°";
            sDatoMostrar = $"{sDatoMostrar}{D33_Procesamiento}°";
            sDatoMostrar = $"{sDatoMostrar}{D40_SP}°";
            sDatoMostrar = $"{sDatoMostrar}{NIdTipoComponente}";
            return sDatoMostrar;
        }
        public EntDatArchivo D10_DatosASP { get; set; }
        public EntDatArchivo D20_Proxy { get; set; }
        public EntDatArchivo D30_Procesamiento { get; set; }
        public EntDatArchivo D31_Procesamiento { get; set; }
        public EntDatArchivo D32_Procesamiento { get; set; }
        public EntDatArchivo D33_Procesamiento { get; set; }
        public EntDatArchivo D40_SP { get; set; }

        public int NIdTipoComponente { get; set; }
    }
}
