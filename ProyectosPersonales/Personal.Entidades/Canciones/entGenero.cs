using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Personal.Entidades.Canciones
{
    public class entGenero
    {
        public entGenero()
        {
            Id = 0;
            Orden = 0;
            Origen = "";
            Genero = "";
            Descripcion = "";
        }
        public int Id { get; set; }
        public int Orden { get; set; }
        public string Origen { get; set; }
        public string Genero { get; set; }
        public string Descripcion { get; set; }
    }
}
