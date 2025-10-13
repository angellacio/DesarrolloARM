using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Personal.Entidades.Canciones
{
    public class entPropiedadesCancion
    {
        public entPropiedadesCancion()
        {
            //sMusica_NumPista = null;
            //sMusica_Nombre = "¡¡ Nuevo !!";
            //sMusica_Artista = "¡¡ Nuevo !!";
            //sMusica_ArtistaC = "¡¡ Nuevo !!";
            //sMusica_Album = "¡¡ Nuevo !!";
            //sMusica_Año = null;
            //sMusica_IdGenero = null;
            //sMusica_Genero = "¡¡ Nuevo !!";
            //sMusica_Comentario = "¡¡ Nuevo !!";
            sMusica_NumPista = null;
            sMusica_Nombre = "";
            sMusica_Artista = "";
            sMusica_ArtistaC = "";
            sMusica_Album = "";
            sMusica_Año = null;
            sMusica_IdGenero = null;
            sMusica_Genero = "";
            sMusica_Comentario = "";
        }
        public entPropiedadesCancion(entPropiedadesCancion itemReplica)
        {
            sMusica_NumPista = itemReplica.sMusica_NumPista;
            sMusica_Nombre = itemReplica.sMusica_Nombre;
            sMusica_Artista = itemReplica.sMusica_Artista;
            sMusica_ArtistaC = itemReplica.sMusica_ArtistaC;
            sMusica_Album = itemReplica.sMusica_Album;
            sMusica_Año = itemReplica.sMusica_Año;
            sMusica_IdGenero = itemReplica.sMusica_IdGenero;
            sMusica_Genero = itemReplica.sMusica_Genero;
            sMusica_Comentario = itemReplica.sMusica_Comentario;
        }

        //public ITagInfo sMusica_iTag { get; set; }
        public int? sMusica_NumPista { get; set; }
        public string sMusica_Nombre { get; set; }
        public string sMusica_Artista { get; set; }
        public string sMusica_ArtistaC { get; set; }
        public string sMusica_Album { get; set; }
        public int? sMusica_Año { get; set; }
        public int? sMusica_IdGenero { get; set; }
        public string sMusica_Genero { get; set; }
        public string sMusica_Comentario { get; set; }
    }
}
