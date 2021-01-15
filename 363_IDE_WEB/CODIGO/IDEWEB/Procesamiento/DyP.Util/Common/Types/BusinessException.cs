
//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Util.Types:BusinessException:0:21/May/2008[SAT.DyP.Util.Types:1.0:21/May/2008])
	
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace SAT.DyP.Util.Types
{
    /// <summary>
    /// Clase que representa una excepción de negocio
    /// </summary>
    [Serializable]
    public class BusinessException:Exception,ISerializable
    {
        private int _legacyErrorCode;

        public int LegacyErrorCode
        {
            get { return _legacyErrorCode; }
            set { _legacyErrorCode = value; }
        }

        private long _processId;

        public long ProcessId
        {
            get { return _processId; }
            set { _processId = value; }
        }

        private int _MessageCode;

        public int MessageCode
        {
            set { _MessageCode = value; }
            get { return _MessageCode; }
        }

        public BusinessException()
        {
        }

        public BusinessException(string message)
            : base(message)
        {
        }

        public BusinessException(string message, int legacyErrorCode)
            : base(message)
        {
            _legacyErrorCode = legacyErrorCode;
        }

        public BusinessException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public BusinessException(string message, int legacyErrorCode, Exception inner)
            : base(message, inner)
        {
            _legacyErrorCode = legacyErrorCode;
        }

        public BusinessException(string message, long processId, int messageCode, Exception inner)
            :base(message,inner)
        {
            _processId = processId;
            _MessageCode = messageCode;
        }


        protected BusinessException(SerializationInfo info, StreamingContext ctx)
            :base(info,ctx)
        {
            _processId = info.GetInt64("ProcessId");
            _MessageCode = info.GetInt32("MessageCode");
            _legacyErrorCode = info.GetInt32("LegacyErrorCode");
        }

        [SecurityPermissionAttribute(SecurityAction.Demand,SerializationFormatter=true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ProcessId", _processId);
            info.AddValue("MessageCode", _MessageCode);
            info.AddValue("LegacyErrorCode", _legacyErrorCode);

            base.GetObjectData(info, context);
        }
    }
}
