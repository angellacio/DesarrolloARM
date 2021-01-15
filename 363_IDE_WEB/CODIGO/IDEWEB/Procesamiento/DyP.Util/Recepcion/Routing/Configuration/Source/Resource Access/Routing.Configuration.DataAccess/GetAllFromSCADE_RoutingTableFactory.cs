//@(#)SCADE2(W:SKDN08221CO2:SAT.DyP.Routing.Configuration.DataAccess:GetAllFromSCADERouteFactory:0:21/May/2008[SAT.DyP.Routing.Configuration:1.0:21/May/2008])
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SAT.DyP.Routing.Configuration.BusinessEntities;

namespace SAT.DyP.Routing.Configuration.DataAccess {
    /// <summary>
    /// Construct a SCADERoute object from a datareader.
    /// </summary>
    internal class GetAllFromSCADERouteFactory : IDomainObjectFactory<SCADERoute> {
        public SCADERoute Construct(IDataReader reader) {
            SCADERoute SCADERoute = new SCADERoute();

            int id_routeIndex = reader.GetOrdinal("id_route");
            if (!reader.IsDBNull(id_routeIndex)) {
                SCADERoute.id_route = reader.GetInt32(id_routeIndex);

            }

            int soap_actionIndex = reader.GetOrdinal("soap_action");
            if (!reader.IsDBNull(soap_actionIndex)) {
                SCADERoute.soap_action = reader.GetString(soap_actionIndex);

            }

            int destinationIndex = reader.GetOrdinal("destination");
            if (!reader.IsDBNull(destinationIndex)) {
                SCADERoute.destination = reader.GetString(destinationIndex);

            }

            int descriptionIndex = reader.GetOrdinal("description");
            if (!reader.IsDBNull(descriptionIndex)) {
                SCADERoute.description = reader.GetString(descriptionIndex);

            }

            return SCADERoute;
        }
    }
}

