using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Personal.Entidades.Canciones
{
    public class entFolder
    {
        public entFolder()
        {
            nNivel = -1;
            nFolder = -1;
            sFolder = "";
            sDescripcion = "";
            lstSubFolder = new List<entFolder>();
        }
        public entFolder(int Nivel, int Folder, string DesFolder, string Descripcion)
        {
            nNivel = Nivel;
            nFolder = Folder;
            sFolder = DesFolder;
            sDescripcion = Descripcion;
            lstSubFolder = new List<entFolder>();
        }
        public entFolder(entFolder itemFolder)
        {
            nNivel = itemFolder.nNivel;
            nFolder = itemFolder.nFolder;
            sFolder = itemFolder.sFolder;
            sDescripcion = itemFolder.sDescripcion;
            lstSubFolder = itemFolder.lstSubFolder;
        }

        public override string ToString()
        {
            return string.Format("{0} :: {1} :: {2}", nNivel, lstSubFolder.Count, sFolder);
        }
        public int nNivel { get; set; }
        public int nFolder { get; set; }
        public string sFolder { get; set; }
        public string sDescripcion { get; set; }

        public List<entFolder> lstSubFolder { get; set; }
    }
}
