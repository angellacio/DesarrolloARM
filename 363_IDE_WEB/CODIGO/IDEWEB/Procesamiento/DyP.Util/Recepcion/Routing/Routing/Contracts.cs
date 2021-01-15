
//@(#)SCADE2(W:SKDN08221CO2:SAT.DyP.Routing.Services:IRequestReplyDatagramRouter:0:21/May/2008[SAT.DyP.Routing.Services:1.0:21/May/2008])
	
using System;
using System.ServiceModel.Channels;
using System.ServiceModel;

namespace SAT.DyP.Routing.Services {

    [ServiceContract(SessionMode = SessionMode.Allowed)]
    public interface IRequestReplyDatagramRouter {
        [OperationContract(IsOneWay = false, Action = "*", ReplyAction = "*")]
        Message ProcessMessage(Message message);
    }
}