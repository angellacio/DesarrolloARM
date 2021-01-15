//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Util.Types:PlatformException:0:21/May/2008[SAT.DyP.Util.Types:1.0:21/May/2008])
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace SAT.DyP.Util.Types
{
    /// <summary>
    /// Clase que representa una excepción general del sistema
    /// </summary>
    [Serializable]
    public class PlatformException:Exception,ISerializable
    {
        private int _legacyErrorCode;

        public int LegacyErrorCode
        {
            get { return _legacyErrorCode; }
            set { _legacyErrorCode = value; }
        }

        public PlatformException()
        {
        }

        public PlatformException(string message)
            : base(message)
        {
        }

        public PlatformException(Exception ex)
            : base(ex.Message,ex)
        {
        }

        public PlatformException(string message, int legacyErrorCode)
            : base(message)
        {
        }

        public PlatformException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public PlatformException(string message, int legacyErrorCode, Exception inner)
            : base(message, inner)
        {

        }

        protected PlatformException(SerializationInfo info, StreamingContext ctx)
            :base(info,ctx)
        {

        }

        [SecurityPermissionAttribute(SecurityAction.Demand,SerializationFormatter=true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            LegacyErrorCode = _legacyErrorCode;

            base.GetObjectData(info, context);
        }
    }
}
