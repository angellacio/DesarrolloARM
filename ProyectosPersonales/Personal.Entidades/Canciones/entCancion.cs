using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using TagClass;
using TagClass.ID3.ID3v2F;
using TagClass.ASF;

namespace Personal.Entidades.Canciones
{
    public class entCancion
    {
        public entCancion()
        {
            RutaID = -1;
            Ruta = "";
            NombreArchivo = "";
            Extencion = "";
            Tamanio = 0;
            sMusica_iTag = null;
            sMusica_NumPista = null;
            sMusica_Artista = "";
            sMusica_ArtistaC = "";
            sMusica_Album = "";
            sMusica_IdGenero = null;
            sMusica_Genero = "";
        }
        public entCancion(string sRutaArchivo)
        {
            FileInfo fiCancion = new FileInfo(sRutaArchivo);

            RutaID = 0;
            Ruta = fiCancion.DirectoryName.Trim();
            NombreArchivo = fiCancion.FullName.Replace(string.Format(@"{0}\", Ruta), "").Trim();
            Extencion = fiCancion.Extension.Trim();
            Tamanio = fiCancion.Length;

            sMusica_iTag = null;
            sMusica_NumPista = null;
            sMusica_Artista = "";
            sMusica_ArtistaC = "";
            sMusica_Album = "";
            sMusica_IdGenero = null;
            sMusica_Genero = "";
        }
        public entCancion(entCancion itemC)
        {
            RutaID = 0;
            Ruta = itemC.Ruta;
            NombreArchivo = itemC.NombreArchivo;
            Extencion = itemC.Extencion;
            Tamanio = itemC.Tamanio;

            sMusica_iTag = itemC.sMusica_iTag;
            sMusica_NumPista = itemC.sMusica_NumPista;
            sMusica_Artista = itemC.sMusica_Artista;
            sMusica_ArtistaC = itemC.sMusica_ArtistaC;
            sMusica_Album = itemC.sMusica_Album;
            sMusica_IdGenero = itemC.sMusica_IdGenero;
            sMusica_Genero = itemC.sMusica_Genero;
        }

        public override string ToString()
        {
            return string.Format("{0} :: {1}", NombreArchivo, Ruta);
        }

        public int RutaID { get; set; }
        public string Ruta { get; set; }
        public string[] RutaAray { get {return Ruta.Split('\\'); } }
        public string NombreArchivo { get; set; }
        public string NombreCompleto { get { return string.Format(@"{0}\{1}", Ruta, NombreArchivo); } }
        public string Extencion { get; set; }
        public long Tamanio { get; set; }

        public ITagInfo sMusica_iTag { get; set; }
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
