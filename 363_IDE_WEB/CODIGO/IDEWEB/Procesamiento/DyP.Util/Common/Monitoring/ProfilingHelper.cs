//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Util.Monitoring:ProfilingHelper:0:21/May/2008[SAT.DyP.Util.Monitoring:1.0:21/May/2008])
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using SAT.DyP.Util.Logging;

namespace SAT.DyP.Util.Monitoring
{
    public sealed class ProfilingHelper
    {
        private const string DISABLE_INSTRUMENTATION_SETTING = "disableInstrumentation";

        private Stopwatch _stopwatch;
        private Guid _invokeID;
        private DateTime _startTimeStamp;
        private DateTime _endTimeStamp;
        private string _className;
        
        public ProfilingHelper(string className)
        {
            this._stopwatch = new Stopwatch();
            this._invokeID  = Guid.NewGuid();
            this._className = className;
        }

        public void Start()
        {
            this._startTimeStamp = DateTime.Now;
            this._stopwatch.Start();
        }
        
        public void Stop(string performanceCounterName)
        {
            if (IsInstrumentationDisabled) return;

            this._stopwatch.Stop();
            this._endTimeStamp = DateTime.Now;

            FireMethodCallEvent fmce = new FireMethodCallEvent();

            try
            {
                // Publicar evento de WMI
                fmce.Execute(this._invokeID, this._className, performanceCounterName, this._stopwatch.ElapsedMilliseconds, this._startTimeStamp, this._endTimeStamp);
            }
            catch(Exception ex)
            {
                EventLogHelper.WriteEntry(String.Format("Error al publicar información de profiling a WMI: {0}", ex), EventLogEntryType.Warning,CoreLogEventIdentifier.MONITOR_EVENT_ID);
            }

            try
            {
                // Actualizar contadores de desempeño
                PerformanceCountersHelper.PublishValue(performanceCounterName, this._stopwatch.ElapsedMilliseconds); 
            }
            catch (Exception ex)
            {
                EventLogHelper.WriteEntry(String.Format("Error al publicar información de profiling a performance counters: {0}", ex), EventLogEntryType.Warning,CoreLogEventIdentifier.MONITOR_EVENT_ID);
            }
        }

        private bool IsInstrumentationDisabled
        {
            get
            {
                // No usamos cache para este setting y lo leemos del .config cada vez para soportar apagarlo y prenderlo
                // sin reiniciar el proceso cliente. Esto no afecta las mediciones de profiling por que sólo se usa en el
                // método stop cuando la lectura ya se realizó.

                string settingValue = System.Configuration.ConfigurationManager.AppSettings[DISABLE_INSTRUMENTATION_SETTING];
                if (!String.IsNullOrEmpty(settingValue) && settingValue.ToLower().Equals("true"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}