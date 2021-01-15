//@(#)SCADE2(W:SKDN08221CO2:SAT.DyP.Routing.Configuration.Client:Program:0:21/May/2008[SAT.DyP.Routing.Configuration.Client:1.0:21/May/2008])
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SAT.DyP.Routing.Configuration.Client {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.Run(new frmMain());
        }
    }
}