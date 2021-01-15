
//@(#)SCADE2(W:SKDN08221CO2:SAT.DyP.Routing.Configuration.DataAccess.SQLServer:ErrorCodes:0:21/May/2008[SAT.DyP.Routing.Configuration:1.0:21/May/2008])
	
using System;
using System.Collections.Generic;
using System.Text;

namespace SAT.DyP.Routing.Configuration.DataAccess.SQLServer {
    internal static class ErrorCodes {
        public const byte ValidationError = 1;
        public const byte ConcurrencyViolationError = 2;
        public const int SqlUserRaisedError = 50000;
    }
}

