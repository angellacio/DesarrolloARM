//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Util.Security:SynchronizationHelper:0:21/May/2008[SAT.DyP.Util.Security:1.0:21/May/2008])
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SAT.DyP.Util.Security
{
    public static class SynchronizationHelper
    {
        private static object __syncObject = new object();

        public static void AcquireLock()
        {
            Monitor.Enter(__syncObject);
        }

        public static void ReleaseLock()
        {
            Monitor.Exit(__syncObject);
        }
    }
}
