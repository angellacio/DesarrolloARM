
//@(#)SCADE2(W:SKDN08221CO2:SAT.DyP.Routing.Configuration.DataContracts:RouteMessage:0:21/May/2008[SAT.DyP.Routing.Configuration.DataContracts:1.0:21/May/2008])
	
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SAT.DyP.Routing.Configuration.DataContracts {
    /// <summary>
    /// Data Contract Class - RouteMessage
    /// </summary>
    [DataContract(Namespace = "http://www.sat.gob.mx/scade.net/2007_10_07/routing", Name = "RouteMessage")]
    public partial class RouteMessage {
        private System.Int32 IDRouteField;

        [DataMember(IsRequired = true, Name = "IDRoute", Order = 0)]
        public System.Int32 IDRoute {
            get { return IDRouteField; }
            set { IDRouteField = value; }
        }

        private System.String SoapActionField;

        [DataMember(IsRequired = true, Name = "SoapAction", Order = 1)]
        public System.String SoapAction {
            get { return SoapActionField; }
            set { SoapActionField = value; }
        }

        private System.String DestinationField;

        [DataMember(IsRequired = true, Name = "Destination", Order = 2)]
        public System.String Destination {
            get { return DestinationField; }
            set { DestinationField = value; }
        }

        private System.String DescriptionField;

        [DataMember(IsRequired = false, Name = "Description", Order = 3)]
        public System.String Description {
            get { return DescriptionField; }
            set { DescriptionField = value; }
        }

    }
}
