using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ValidacionIdeWeb
{
    public class Mensaje
    {
        private int idMensaje;
        private string descripcion;
        private int idTipoMensaje;

        public int IdMensaje
        {
            get { return idMensaje; }
            set { idMensaje = value; }
        }
        
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        
        public int IdTipoMensaje
        {
            get { return idTipoMensaje; }
            set { idTipoMensaje = value; }
        }
        

    }
}