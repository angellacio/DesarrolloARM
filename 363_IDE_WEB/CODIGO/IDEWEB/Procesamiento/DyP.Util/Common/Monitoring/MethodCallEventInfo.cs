//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Util.Monitoring:MethodCallEventInfo:0:21/May/2008[SAT.DyP.Util.Monitoring:1.0:21/May/2008])
using System;
using System.Collections.Generic;
using System.Management;
using System.Management.Instrumentation;
using System.Text;

namespace SAT.DyP.Util.Monitoring
{
    public sealed class MethodCallEventInfo : BaseEvent
    {
        public string InvokeID;
        public string ClassName;
        public string MethodName;
        public long CallPeriod;
        public DateTime CallStart;
        public DateTime CallEnd;
    }
}
