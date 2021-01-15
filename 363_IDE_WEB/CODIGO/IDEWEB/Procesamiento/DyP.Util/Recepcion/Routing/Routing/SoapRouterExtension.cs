//@(#)SCADE2(W:SKDN08221CO2:SAT.DyP.Routing.Services:SoapRouterExtension:0:21/May/2008[SAT.DyP.Routing.Services:1.0:21/May/2008])
using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Description;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace SAT.DyP.Routing.Services {
    class SoapRouterExtension : IExtension<ServiceHostBase> {
        IDictionary<string, Binding> bindings = new Dictionary<string, Binding>(1);
        IDictionary<EndpointAddress, IRequestReplyDatagramRouter> requestReplyDatagramChannels = new Dictionary<EndpointAddress, IRequestReplyDatagramRouter>();

        RoutingTable routingTable = null;

        public SoapRouterExtension() {
            this.routingTable = new RoutingTable();
            this.bindings.Add("http", new RouterBinding(RouterTransport.Http));
            this.bindings.Add("https", new RouterBinding(RouterTransport.Https));
        }

        public IDictionary<string, Binding> Bindings {
            get { return this.bindings; }
        }

        public IDictionary<EndpointAddress, IRequestReplyDatagramRouter> RequestReplyDatagramChannels {
            get { return this.requestReplyDatagramChannels; }
        }

        public RoutingTable RoutingTable {
            get { return this.routingTable; }
        }

        public void Attach(ServiceHostBase owner) { }
        public void Detach(ServiceHostBase owner) { }
    }
}
