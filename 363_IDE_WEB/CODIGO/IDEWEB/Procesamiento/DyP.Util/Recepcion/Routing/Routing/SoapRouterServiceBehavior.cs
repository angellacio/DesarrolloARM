//@(#)SCADE2(W:SKDN08221CO2:SAT.DyP.Routing.Services:SoapRouterServiceBehavior:0:21/May/2008[SAT.DyP.Routing.Services:1.0:21/May/2008])
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Description;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace SAT.DyP.Routing.Services {
    sealed class SoapRouterServiceBehavior : Attribute, IServiceBehavior {
        void IServiceBehavior.Validate(ServiceDescription description, ServiceHostBase serviceHostBase) { }

        void IServiceBehavior.AddBindingParameters(ServiceDescription description, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection parameters) { }

        void IServiceBehavior.ApplyDispatchBehavior(ServiceDescription description, ServiceHostBase serviceHostBase) {
            SoapRouterExtension extension = new SoapRouterExtension();
            serviceHostBase.Extensions.Add(extension);
        }
    }
}
