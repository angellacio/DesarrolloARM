//@(#)SCADE2(W:SKDN08221CO2:SAT.DyP.Routing.Configuration.DataAccess:SCADERouteDeleteFactory:0:21/May/2008[SAT.DyP.Routing.Configuration:1.0:21/May/2008])
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
    /// A class that converts a SCADERoute
    /// into a DbCommand that will delete
    /// that SCADERoute.
    /// </summary>
    internal class SCADERouteDeleteFactory : IDeleteFactory<System.Int32> {
        /// <summary>
        /// Creates the SCADERouteDeleteFactory to build a delete statement for
        /// the given System.Int32.
        /// </summary>
        /// <param name="System.Int32">System.Int32 to delete from the database.</param>    
        public SCADERouteDeleteFactory() {
        }

        #region IDeleteFactory<System.Int32> Members
        public DbCommand ConstructDeleteCommand(Database db, System.Int32 id_route) {
            DbCommand command = db.GetStoredProcCommand("dbo.DeleteSCADE_RoutingTable");
            db.AddInParameter(command, "id_route", DbType.Int32, id_route);
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
                case "id_route":
                    return "id_route";
                default:
                    throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, GenericResources.InvalidParameterName), dbParameter);
            }
        }
        #endregion
    }
}

