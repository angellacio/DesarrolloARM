using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDEWebCargaDeclaracionesDWH
{
    class DatosDeclaracion
    {
        private int? folio;
        private int? idEntidadReceptora;
        private string archivoFisico;
        private bool esNormal;        
        private bool esAnual;        

        public int? Folio
        {
            get { return folio; }
            set { folio = value; }
        }

        public int? IdEntidadReceptora
        {
            get { return idEntidadReceptora; }
            set { idEntidadReceptora = value; }
        }

        public string ArchivoFisico
        {
            get { return archivoFisico; }
            set { archivoFisico = value; }
        }

        public bool EsNormal
        {
            get { return esNormal; }
            set { esNormal = value; }
        }

        public bool EsAnual
        {
            get { return esAnual; }
            set { esAnual = value; }
        }
    }
}
