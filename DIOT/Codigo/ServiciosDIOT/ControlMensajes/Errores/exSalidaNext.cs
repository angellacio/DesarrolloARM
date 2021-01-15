using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlMensajes.Errores
{
    public class exSalidaNext : Exception
    {
        public exSalidaNext()
        {
        }

        public exSalidaNext(string message) : base(message)
        {
        }

        public exSalidaNext(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
