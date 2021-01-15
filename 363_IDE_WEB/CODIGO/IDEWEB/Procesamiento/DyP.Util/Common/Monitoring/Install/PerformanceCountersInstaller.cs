//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Util.Monitoring.Install:PerformanceCountersInstaller:0:21/May/2008[SAT.DyP.Util.Monitoring:1.0:21/May/2008])
	
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;

namespace SAT.DyP.Util.Monitoring.Install
{
    [RunInstaller(true)]
    public class PerformanceCountersInstaller : Installer
    {
        public override void Install(System.Collections.IDictionary stateSaver)
        {
            base.Install(stateSaver);

            try
            {
                CreatePerformanceCounters();
            }
            catch(Exception ex)
            {
                this.Context.LogMessage(String.Format("Error installing DyP performance counters: '{0}'", ex));
                throw;
            }
        }

        public override void Rollback(System.Collections.IDictionary savedState)
        {
            try
            {
                RemovePerformanceCounters();
            }
            catch(Exception ex)
            {
                this.Context.LogMessage(String.Format("Warning: an error has been ignored while rolling back installation of DyP performance counters: '{0}'", ex));
                /* esta operación no debe relanzar excepciones */ 
            }

            base.Rollback(savedState);
        }

        public override void Uninstall(System.Collections.IDictionary savedState)
        {
            try
            {
                RemovePerformanceCounters();
            }
            catch (Exception ex)
            {
                this.Context.LogMessage(String.Format("Warning: an error has been ignored while uninstalling DyP performance counters: '{0}'", ex));
                /* esta operación no debe relanzar excepciones */
            }

            base.Uninstall(savedState);
        }

        private void CreatePerformanceCounters()
        {
            if (PerformanceCounterCategory.Exists(ScadePerfCounterDefinition.CategoryName))
            {
                RemovePerformanceCounters();
            }

            this.Context.LogMessage(String.Format("Creating performance counters category '{0}' and counters...", ScadePerfCounterDefinition.CategoryName));

            CounterCreationDataCollection installerCollection = new CounterCreationDataCollection();

            foreach (ScadePerfCounterDefinition counterDefinition in ScadePerfCounterDefinition.CounterDefinitions.Values)
            {
                this.Context.LogMessage(String.Format("Creating performance counter '{0}'.", counterDefinition.CounterName));
                installerCollection.Add(new CounterCreationData(counterDefinition.CounterName, counterDefinition.CounterHelp, counterDefinition.CounterType));
            }

            PerformanceCounterCategory.Create(ScadePerfCounterDefinition.CategoryName,
                ScadePerfCounterDefinition.CategoryHelp,
                PerformanceCounterCategoryType.SingleInstance, installerCollection);
        }

        private void RemovePerformanceCounters()
        {
            if (PerformanceCounterCategory.Exists(ScadePerfCounterDefinition.CategoryName))
            {
                this.Context.LogMessage(String.Format("Deleting existing performance counters category '{0}'.", ScadePerfCounterDefinition.CategoryName));
                PerformanceCounterCategory.Delete(ScadePerfCounterDefinition.CategoryName);
            }
        }
    }
}
