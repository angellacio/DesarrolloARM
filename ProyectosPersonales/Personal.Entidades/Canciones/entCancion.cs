using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using TagClass;
using TagClass.ID3.ID3v2F;
using TagClass.ASF;
using System.CodeDom;
using mTexC = Personal.Entidades.Canciones.ManejoTextos;

namespace Personal.Entidades.Canciones
{
    public class entCancion
    {
        public entCancion()
        {
            EstCancion = mTexC.EstadoCancion.Espera;
            RutaID = -1;
            Ruta = "";
            NombreArchivo = "";
            Extencion = "";
            Tamanio = 0;
            bDatCan_Mod = false;
            bEstadoID3v1 = false;
            bEstadoID3v2 = false;
            DatCan_Mod = new entPropiedadesCancion();
            ID3v1 = new entPropiedadesCancion();
            ID3v2 = new entPropiedadesCancion();
        }
        public entCancion(string sRutaArchivo)
        {
            FileInfo fiCancion = null;
            bDatCan_Mod = false;
            bEstadoID3v1 = false;
            bEstadoID3v2 = false;
            DatCan_Mod = new entPropiedadesCancion();
            ID3v1 = new entPropiedadesCancion();
            ID3v2 = new entPropiedadesCancion();

            try
            {
                fiCancion = new FileInfo(sRutaArchivo);

                RutaID = 0;
                Ruta = fiCancion.DirectoryName.Trim();
                NombreArchivo = fiCancion.FullName.Replace(string.Format(@"{0}\", Ruta), "").Trim();
                Extencion = fiCancion.Extension.Trim();
                Tamanio = fiCancion.Length;
                EstCancion = mTexC.EstadoCancion.Agregado;
            }
            catch (Exception ex)
            {
                EstCancion = mTexC.EstadoCancion.Error;
                RutaID = -1;
                Ruta = sRutaArchivo;
                NombreArchivo = ex.Message;
            }
            finally { fiCancion = null; }
        }
        public entCancion(entCancion itemC)
        {
            EstCancion = itemC.EstCancion;
            RutaID = itemC.RutaID;
            Ruta = itemC.Ruta;
            NombreArchivo = itemC.NombreArchivo;
            Extencion = itemC.Extencion;
            Tamanio = itemC.Tamanio;
            bDatCan_Mod = itemC.bDatCan_Mod;
            bEstadoID3v1 = itemC.bEstadoID3v1;
            bEstadoID3v2 = itemC.bEstadoID3v2;
            DatCan_Mod = new entPropiedadesCancion(itemC.DatCan_Mod);
            ID3v1 = new entPropiedadesCancion(itemC.ID3v1);
            ID3v2 = new entPropiedadesCancion(itemC.ID3v2);
        }

        public override string ToString()
        {
            return string.Format("{0} :: {1}", NombreArchivo, Ruta);
        }


        public mTexC.EstadoCancion EstCancion { get; set; }
        public int RutaID { get; set; }
        //public string[] RutaAray { get { return Ruta.Split('\\'); } }
        public string Ruta { get; set; }
        public string NombreArchivo { get; set; }
        //public string NombreCompleto { get { return string.Format(@"{0}\{1}", Ruta, NombreArchivo); } }
        public string Extencion { get; set; }
        public long Tamanio { get; set; }
        public Boolean bDatCan_Mod { get; set; }
        public Boolean bEstadoID3v1 { get; set; }
        public Boolean bEstadoID3v2 { get; set; }

        public entPropiedadesCancion DatCan_Mod { get; set; }
        public entPropiedadesCancion ID3v1 { get; set; }
        public entPropiedadesCancion ID3v2 { get; set; }
    }
}
