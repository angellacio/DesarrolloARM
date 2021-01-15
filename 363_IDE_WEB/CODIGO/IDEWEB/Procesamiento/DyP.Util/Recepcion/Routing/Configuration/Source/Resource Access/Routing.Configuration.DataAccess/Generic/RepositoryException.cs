//@(#)SCADE2(W:SKDN08221CO2:SAT.DyP.Routing.Configuration.DataAccess:RepositoryException:0:21/May/2008[SAT.DyP.Routing.Configuration.DataAccess:1.0:21/May/2008])
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace SAT.DyP.Routing.Configuration.DataAccess {
    /// <summary>
    /// This class is the base class for all exceptions from our
    /// repositories.
    /// </summary>
    [Serializable]
    public class RepositoryException : Exception {
        public RepositoryException() {
        }

        public RepositoryException(string message)
            : base(message) {
        }

        public RepositoryException(string message, Exception inner)
            : base(message, inner) {
        }

        protected RepositoryException(SerializationInfo info, StreamingContext context)
            : base(info, context) {
        }
    }
}
