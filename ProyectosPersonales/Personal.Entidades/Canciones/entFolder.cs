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
            //nFolder = -1;
            sFolder = "";
            //ArrayFolder = null;
            //sDescripcion = "";
            lstSubFolder = new List<entFolder>();
        }
        public entFolder(string Folder, int Nivel)
        {
            nNivel = Nivel;
            //nFolder = Folder;
            sFolder = Folder;
            //ArrayFolder = ArrayF;
            //sDescripcion = Descripcion;
            lstSubFolder = new List<entFolder>();
        }
        public entFolder(entFolder itemFolder)
        {
            nNivel = itemFolder.nNivel;
            //nFolder = itemFolder.nFolder;
            sFolder = itemFolder.sFolder;
            //ArrayFolder = itemFolder.ArrayFolder;
            //sDescripcion = itemFolder.sDescripcion;
            lstSubFolder = itemFolder.lstSubFolder;
        }

        public override string ToString()
        {
            //return string.Format("{0} :: {1} :: {2}", nNivel, lstSubFolder.Count, sFolder);
            return string.Format("{0} :: {1}", nNivel, sFolder);
        }

        public string sFolder { get; set; }
        //public string[] ArrayFolder { get; set; }
        public int nNivel { get;set; }

        public List<entFolder> lstSubFolder { get; set; }
    }
}
