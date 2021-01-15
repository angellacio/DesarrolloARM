//@(#)SCADE2(W:SKDN08221CO2:SAT.DyP.Routing.Configuration.DataAccess:SCADERouteUpdateFactory:0:21/May/2008[SAT.DyP.Routing.Configuration:1.0:21/May/2008])
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SAT.DyP.Routing.Configuration.BusinessEntities;
using SAT.DyP.Routing.Configuration.DataAccess.Generic;
using System.Globalization;

namespace SAT.DyP.Routing.Configuration.DataAccess {
    /// <summary>
    /// This factory class takes a SCADERoute object and generates
    /// the dbcommand object needed to call the underlying update
    /// stored proc.
    /// </summary>
    internal class SCADERouteUpdateFactory : IDbToBusinessEntityNameMapper, IUpdateFactory<SCADERoute> {
        /// <summary>
        /// Creates the SCADERouteUpdateFactory to build an update statement for
        /// the given SCADERoute object.
        /// </summary>
        /// <param name="SCADERoute">SCADERoute to update into the database.</param>
        public SCADERouteUpdateFactory() {
        }

        #region IUpdateFactory<SCADERoute> Members

        public DbCommand ConstructUpdateCommand(Database db, SCADERoute SCADERoute) {
            DbCommand command = db.GetStoredProcCommand("dbo.UpdateSCADE_RoutingTable");

            if (SCADERoute.description != null) {
                db.AddInParameter(command, "description", DbType.String, SCADERoute.description);
            }
            if (SCADERoute.destination != null) {
                db.AddInParameter(command, "destination", DbType.String, SCADERoute.destination);
            }
            db.AddInParameter(command, "id_route", DbType.Int32, SCADERoute.id_route);
            if (SCADERoute.soap_action != null) {
                db.AddInParameter(command, "soap_action", DbType.String, SCADERoute.soap_action);
            }
            return command;
        }
        #endregion

        #region IDbToBusinessEntityNameMapper Members
        /// <summary>
        /// Maps a field name in the database (usually a parameter name for a stored proc)
        /// to the corresponding business entity property name.
        /// </summary>
        /// <remarks>This method is intended for error message handling, not for reflection.</remarks>
        /// <param name="dbParameter">Name of field/parameter that the database knows about.</param>
        /// <returns>Corresponding business entity field name.</returns>
        public string MapDbParameterToBusinessEntityProperty(string dbParameter) {
            switch (dbParameter) {
                case "description":
                    return "description";
                case "destination":
                    return "destination";
                case "id_route":
                    return "id_route";
                case "soap_action":
                    return "soap_action";
                default:
                    throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, GenericResources.InvalidParameterName), dbParameter);
            }
        }
        #endregion
    }
}

