using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ValidacionIdeWeb.Comunes.Exepciones
{
    [Serializable]
    public class ecAplicacion : Exception
    {
        public ecAplicacion()
        {
        }

        public ecAplicacion(string message)
            : base(message)
        {
        }

        public ecAplicacion(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}