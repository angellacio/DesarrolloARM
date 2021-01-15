using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MonitorPaquetes
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Default());
            //Application.Run(new FrmPrincipal());
            Application.Run(new PrincipalNew());
            //Application.Run(new ManejoIncidentes.datTrabajo());
        }
    }
}
