using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlMensajes.Errores
{
    public class exSalidaAll : Exception
    {
        public exSalidaAll()
        {
        }

        public exSalidaAll(string message) : base(message)
        {
        }

        public exSalidaAll(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
