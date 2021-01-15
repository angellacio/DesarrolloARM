using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ValidacionIdeWeb
{
    public class DatosArchivo
    {
        private bool esAnual;
        private string rfc;
        private int ejercicio;
        private int periodo;
        private bool esNormal;
        

        public bool EsAnual
        {
            get { return esAnual; }
            set { esAnual = value; }
        }
        public string Rfc
        {
            get { return rfc; }
            set { rfc = value; }
        }

        public int Ejercicio
        {
            get { return ejercicio; }
            set { ejercicio = value; }
        }

        public int Periodo
        {
            get { return periodo; }
            set { periodo = value; }
        }
        public bool EsNormal
        {
            get { return esNormal; }
            set { esNormal = value; }
        }
        

        
    }
}