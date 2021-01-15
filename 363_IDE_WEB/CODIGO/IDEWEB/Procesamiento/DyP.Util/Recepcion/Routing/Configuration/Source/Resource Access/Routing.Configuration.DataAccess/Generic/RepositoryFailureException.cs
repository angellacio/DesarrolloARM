//@(#)SCADE2(W:SKDN08221CO2:SAT.DyP.Routing.Configuration.DataAccess:RepositoryFailureException:0:21/May/2008[SAT.DyP.Routing.Configuration.DataAccess:1.0:21/May/2008])
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using SAT.DyP.Routing.Configuration.DataAccess.Generic;
using System.Globalization;

namespace SAT.DyP.Routing.Configuration.DataAccess {
    /// <summary>
    /// This exception is thrown when an unexpected error
    /// is received from the database.
    /// </summary>
    [Serializable]
    public class RepositoryFailureException : RepositoryException {
        public RepositoryFailureException() {
        }

        public RepositoryFailureException(string message)
            : base(message) {
        }

        public RepositoryFailureException(string message, Exception inner)
            : base(message, inner) {
        }

        public RepositoryFailureException(Exception inner)
            : base(string.Format(CultureInfo.CurrentCulture, GenericResources.DefaultMessage), inner) {
        }

        protected RepositoryFailureException(SerializationInfo info, StreamingContext context)
            : base(info, context) {
        }
    }
}

