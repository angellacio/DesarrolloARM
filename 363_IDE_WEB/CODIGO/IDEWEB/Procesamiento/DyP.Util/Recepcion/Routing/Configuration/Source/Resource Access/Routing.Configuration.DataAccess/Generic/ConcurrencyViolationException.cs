//@(#)SCADE2(W:SKDN08221CO2:SAT.DyP.Routing.Configuration.DataAccess:ConcurrencyViolationException:0:21/May/2008[SAT.DyP.Routing.Configuration.DataAccess:1.0:21/May/2008])
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace SAT.DyP.Routing.Configuration.DataAccess {
    /// <summary>
    /// This exception is thrown when a concurrency
    /// violation is detected in the database.
    /// </summary>
    [Serializable]
    public class ConcurrencyViolationException : RepositoryException {
        public ConcurrencyViolationException() {
        }

        public ConcurrencyViolationException(string message)
            : base(message) {
        }

        public ConcurrencyViolationException(string message, Exception inner)
            : base(message, inner) {
        }

        protected ConcurrencyViolationException(SerializationInfo info, StreamingContext context)
            : base(info, context) {
        }
    }
}

