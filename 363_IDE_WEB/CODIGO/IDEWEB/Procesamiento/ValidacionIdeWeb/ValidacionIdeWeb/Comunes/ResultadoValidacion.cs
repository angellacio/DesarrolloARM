using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ValidacionIdeWeb
{
    public class ResultadoValidacion
    {
        private bool esCorreca;
        private List<String> listaErrores;
        private bool sinErroresEsquema;
        private List<String> listaErroresEsquema;
        private bool validacionConcluida;

        public bool ValidacionConcluida
        {
            get { return validacionConcluida; }
            set { validacionConcluida = value; }
        }


        public bool SinErroresEsquema
        {
            get { return sinErroresEsquema; }
            set { sinErroresEsquema = value; }
        }

        public List<String> ListaErroresEsquema
        {
            get { return listaErroresEsquema; }
            set { listaErroresEsquema = value; }
        }

        public List<String> ListaErrores
        {
            get { return listaErrores; }
            set { listaErrores = value; }
        }


        public bool EsCorreca
        {
            get { return esCorreca; }
            set { esCorreca = value; }
        }
        


    }
}