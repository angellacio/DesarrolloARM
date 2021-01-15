using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace ProDec_Servicio
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        static void Main()
        {
#if DEBUG
            // Sección de pruebas //////////////////////////////////////////
            RecepcionValidacionDeclaraciones test0 = new RecepcionValidacionDeclaraciones();
            test0.IniciarProceso();
#else
           ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
			{ 
				new RecepcionValidacionDeclaraciones() 
			};
            ServiceBase.Run(ServicesToRun);
#endif
        }
    }
}
