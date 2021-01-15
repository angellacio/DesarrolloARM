//@(#)SCADE2(W:SKDN08221CO2:SAT.DyP.Routing.Services.SchemaValidationMessageInspector:RequestValidationFault:0:21/May/2008[SAT.DyP.Routing.Services.MessageInspection:1.0:21/May/2008])
using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using System.Threading;

namespace SAT.DyP.Routing.Services.MessageInspection
{
    class RequestValidationFault : FaultException
    {
        public RequestValidationFault(string validationErrorDetail)
            :base(new FaultReason(new FaultReasonText(validationErrorDetail,Thread.CurrentThread.CurrentUICulture)),
                  FaultCode.CreateSenderFaultCode("SchemaValidationFault", Properties.Resources.Att_DefaultNamespace))
        {
        }
    }

    class ReplyValidationFault : FaultException
    {
        public ReplyValidationFault(string validationErrorDetail)
            : base(new FaultReason(new FaultReasonText(validationErrorDetail, Thread.CurrentThread.CurrentUICulture)),
                  FaultCode.CreateReceiverFaultCode("SchemaValidationFault", Properties.Resources.Att_DefaultNamespace))
        {
        }
    }
}
