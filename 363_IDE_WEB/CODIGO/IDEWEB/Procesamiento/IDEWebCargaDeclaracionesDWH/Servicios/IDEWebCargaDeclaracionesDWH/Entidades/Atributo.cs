using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDEWebCargaDeclaracionesDWH
{
    class Atributo
    {
        public Atributo(string nom, string val)
        {
            nombre = nom;
            valor = val;
        }
        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        private string valor;

        public string Valor
        {
            get { return valor; }
            set { valor = value; }
        }

    }
}
