//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Util.Monitoring.Install:WmiInstaller:0:21/May/2008[SAT.DyP.Util.Monitoring:1.0:21/May/2008])
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Reflection;
using System.Management.Instrumentation;

namespace SAT.DyP.Util.Monitoring.Install
{
    [RunInstaller(true)]
    public partial class WmiInstaller : Installer
    {
        public override void Install(System.Collections.IDictionary stateSaver)
        {
            base.Install(stateSaver);

            try
            {
                CreateWMISchemas();
            }
            catch { /* Como estos esquemas no se pueden borrar, no hay manera de hacer rollback y por eso no se lanzan excepciones. */}
        }

        private void CreateWMISchemas()
        {
            Assembly instrumentedAssembly = Assembly.GetExecutingAssembly();

            if (!Instrumentation.IsAssemblyRegistered(instrumentedAssembly))
            {
                this.Context.LogMessage(String.Format("Registrando schemas de WMI para assembly '{0}'...", instrumentedAssembly.FullName));
                System.Management.Instrumentation.Instrumentation.RegisterAssembly(instrumentedAssembly);
            }
        }
    }
}