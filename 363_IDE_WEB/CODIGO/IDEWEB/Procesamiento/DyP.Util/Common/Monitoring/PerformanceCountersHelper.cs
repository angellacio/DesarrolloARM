//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Util.Monitoring:PerformanceCountersHelper:0:21/May/2008[SAT.DyP.Util.Monitoring:1.0:21/May/2008])
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace SAT.DyP.Util.Monitoring
{
    public static class PerformanceCountersHelper
    {
        /// <summary>
        /// Estructura de datos en la que se cachean los performance counters activos para que no se creen en cada llamada.
        /// </summary>
        private static Dictionary<string, PerformanceCounter> _activeCounters = new Dictionary<string, PerformanceCounter>();

        /// <summary>
        /// Objeto de sincronizacion para el acceso al cache de contadores activos.
        /// </summary>
        private static object _activeCountersMutex = new object();

        public static void PublishValue(string performanceCounter, long milliseconds)
        {
            UpdateCounter(performanceCounter, milliseconds);
        }

        private static void UpdateCounter(string counterName, long milliseconds)
        {
            PerformanceCounter counter = GetActiveCounter(counterName);

            if (counter == null)
            {
                counter = CreatePerformanceCounter(counterName);
            }

            counter.RawValue = milliseconds;
        }

        private static PerformanceCounter GetActiveCounter(string counterName)
        {
            PerformanceCounter activeCounter = null;
            
            lock (_activeCountersMutex)
            {
                if (_activeCounters.ContainsKey(counterName))
                {
                    activeCounter = _activeCounters[counterName];
                }
                else
                {
                    activeCounter = null;
                }
            }

            return activeCounter;
        }

        private static PerformanceCounter CreatePerformanceCounter(string counterName)
        {
            PerformanceCounter counter = new PerformanceCounter(
                    ScadePerfCounterDefinition.CategoryName,
                    ScadePerfCounterDefinition.CounterDefinitions[counterName].CounterName,
                    false);

            lock (_activeCountersMutex)
            {
                _activeCounters.Add(counterName, counter);
            }

            return counter;
        }
    }
}
