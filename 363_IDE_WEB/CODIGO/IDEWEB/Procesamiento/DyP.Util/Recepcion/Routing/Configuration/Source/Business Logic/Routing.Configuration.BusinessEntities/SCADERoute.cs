
//@(#)SCADE2(W:SKDN08221CO2:SAT.DyP.Routing.Configuration.BusinessEntities:SCADERoute:0:21/May/2008[SAT.DyP.Routing.Configuration.BusinessEntities:1.0:21/May/2008])
	
using System;
using System.Collections.Generic;
using System.Text;

namespace SAT.DyP.Routing.Configuration.BusinessEntities {
    public partial class SCADERoute {
        public SCADERoute() {
        }

        public SCADERoute(System.String description, System.String destination, System.Int32 id_route, System.String soap_action) {
            this.descriptionField = description;
            this.destinationField = destination;
            this.id_routeField = id_route;
            this.soap_actionField = soap_action;
        }

        private System.String descriptionField;

        public System.String description {
            get { return this.descriptionField; }
            set { this.descriptionField = value; }
        }

        private System.String destinationField;

        public System.String destination {
            get { return this.destinationField; }
            set { this.destinationField = value; }
        }

        private System.Int32 id_routeField;

        public System.Int32 id_route {
            get { return this.id_routeField; }
            set { this.id_routeField = value; }
        }

        private System.String soap_actionField;

        public System.String soap_action {
            get { return this.soap_actionField; }
            set { this.soap_actionField = value; }
        }

    }
}

