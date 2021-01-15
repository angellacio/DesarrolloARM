//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Util.Monitoring:FireMethodCallEvent:0:21/May/2008[SAT.DyP.Util.Monitoring:1.0:21/May/2008])
	
using System;
using System.Collections.Generic;
using System.Management;
using System.Management.Instrumentation;
using System.Text;
using SAT.DyP.Util.Types;

namespace SAT.DyP.Util.Monitoring
{
    public sealed class FireMethodCallEvent
    {
        public FireMethodCallEvent()
        {
        }

        public void Execute(Guid invokeID, string className, string methodName, long callPeriod, DateTime callStart, DateTime callEnd)
        {
            InternalExecute(invokeID, className, methodName, callPeriod, callStart, callEnd);
        }

        private void InternalExecute(Guid invokeID, string className, string methodName, long callPeriod, DateTime callStart, DateTime callEnd)
        {
            try
            {
                MethodCallEventInfo methodCallEventInfo = new MethodCallEventInfo();
                methodCallEventInfo.InvokeID = invokeID.ToString(@"N");
                methodCallEventInfo.ClassName = className;
                methodCallEventInfo.MethodName = methodName;
                methodCallEventInfo.CallPeriod = callPeriod;
                methodCallEventInfo.CallStart = callStart;
                methodCallEventInfo.CallEnd = callEnd;

                methodCallEventInfo.Fire();
            }
            catch (ManagementException mgmtEx)
            {
                throw new PlatformException(@"System error invoking FireMethodCallEvent.InternalExecute().", mgmtEx);
            }
        }
    }
}
