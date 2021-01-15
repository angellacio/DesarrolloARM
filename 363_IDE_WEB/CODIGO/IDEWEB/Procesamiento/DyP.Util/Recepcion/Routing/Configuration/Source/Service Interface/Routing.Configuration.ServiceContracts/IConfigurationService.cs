//@(#)SCADE2(W:SKDN08221CO2:SAT.DyP.Routing.Configuration.ServiceContracts:IConfigurationService:0:21/May/2008[SAT.DyP.Routing.Configuration.ServiceContracts:1.0:21/May/2008])
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace SAT.DyP.Routing.Configuration.ServiceContracts {
    [ServiceContract(Namespace = "http://www.sat.gob.mx/scade.net/2007_10_07/routing", Name = "IConfigurationService", SessionMode = SessionMode.Allowed)]
    public interface IConfigurationService {
        [FaultContract(typeof(SAT.DyP.Routing.Configuration.FaultContracts.DefaultFaultContract))]
        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "GetAllRoutes")]
        List<SAT.DyP.Routing.Configuration.DataContracts.RouteMessage> GetAllRoutes();
        [FaultContract(typeof(SAT.DyP.Routing.Configuration.FaultContracts.DefaultFaultContract))]
        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "GetRoute")]
        SAT.DyP.Routing.Configuration.DataContracts.RouteMessage GetRoute(System.Int32 request);
        [FaultContract(typeof(SAT.DyP.Routing.Configuration.FaultContracts.DefaultFaultContract))]
        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "AddRoute")]
        void AddRoute(SAT.DyP.Routing.Configuration.DataContracts.RouteMessage request);
        [FaultContract(typeof(SAT.DyP.Routing.Configuration.FaultContracts.DefaultFaultContract))]
        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "DeleteRoute")]
        void DeleteRoute(System.Int32 request);
        [FaultContract(typeof(SAT.DyP.Routing.Configuration.FaultContracts.DefaultFaultContract))]
        [OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "SaveRoute")]
        void SaveRoute(SAT.DyP.Routing.Configuration.DataContracts.RouteMessage request);
    }
}
