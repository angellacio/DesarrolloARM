using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProDec_ReglaNegocio.Entidades
{
    [Serializable]
    public class catConsultaDeclas
    {
        public int nFolio { get; set; }
        public int nMedioRecepcion { get; set;}
        public string sNombreArchivo { get; set; }
        public string sNombreFisico { get; set; }

    }
}
