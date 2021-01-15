//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Util.Security:SecurityException:0:21/May/2008[SAT.DyP.Util.Security:1.0:21/May/2008])
using System;
using System.Runtime.Serialization;

namespace SAT.DyP.Util.Security
{
    public class SecurityException : ApplicationException
    {
        public SecurityException()
        {
        }

        public SecurityException(string message)
        {
        }

        public SecurityException(string message, Exception innerException)
        {
        }

        protected SecurityException(SerializationInfo serializationInfo, StreamingContext context)
        {
        }
    }
}
