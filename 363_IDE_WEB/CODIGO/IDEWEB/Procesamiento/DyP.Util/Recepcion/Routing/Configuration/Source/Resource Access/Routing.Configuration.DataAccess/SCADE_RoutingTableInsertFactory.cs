//@(#)SCADE2(W:SKDN08221CO2:SAT.DyP.Routing.Configuration.DataAccess:SCADERouteInsertFactory:0:21/May/2008[SAT.DyP.Routing.Configuration:1.0:21/May/2008])
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
    /// A factory object that is responsible for taking a SCADERoute
    /// and generating the corresponding SQL to insert that
    /// SCADERoute into the database. It also includes a method
    /// to grab the returned ID from the call and updating the
    /// SCADERoute object with it.
    /// </summary>
    internal class SCADERouteInsertFactory : IDbToBusinessEntityNameMapper, IInsertFactory<SCADERoute> {
        /// <summary>
        /// Creates the SCADERouteInsertFactory to build an insert statement for
        /// the given SCADERoute object.
        /// </summary>
        /// <param name="SCADERoute">New SCADERoute to insert into the database.</param>
        public SCADERouteInsertFactory() {
        }

        #region IInsertFactory<SCADERoute> Members

        public DbCommand ConstructInsertCommand(Database db, SCADERoute SCADERoute) {
            DbCommand command = db.GetStoredProcCommand("dbo.InsertSCADE_RoutingTable");

            if (SCADERoute.description != null) {
                db.AddInParameter(command, "description", DbType.String, SCADERoute.description);
            }
            if (SCADERoute.destination != null) {
                db.AddInParameter(command, "destination", DbType.String, SCADERoute.destination);
            }
            if (SCADERoute.soap_action != null) {
                db.AddInParameter(command, "soap_action", DbType.String, SCADERoute.soap_action);
            }
            return command;
        }

        public void SetNewID(Database db, DbCommand command, SCADERoute SCADERoute) {
            //TODO: Provide set mapping
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
                case "soap_action":
                    return "soap_action";
                default:
                    throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, GenericResources.InvalidParameterName), dbParameter);
            }
        }
        #endregion
    }

}

