using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SAT.SUS.ServicioMicrosoft
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] { new DatosServicio.SustitutivasMicrosoft() };
            ServiceBase.Run(ServicesToRun);

            // DatosServicio.SustitutivasMicrosoft test0 = new DatosServicio.SustitutivasMicrosoft();
            // test0.IniciaServicio();

        }
    }
}
