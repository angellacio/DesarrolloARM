using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WA_Entidades.Seguridad
{
    public class EntDatosAutentificacion
    {
        public EntDatosAutentificacion()
        {
            EmpleadoAutentificado = new EntEmpleado();
            Areas = new List<EntCatalogoSensillo>();
            Aplicativos = new List<EntCatalogoSensillo>();
        }
        public EntEmpleado EmpleadoAutentificado { get; set; }

        public List<EntCatalogoSensillo> Areas { get; set; }
        public List<EntCatalogoSensillo> Aplicativos { get; set; }
    }
}
