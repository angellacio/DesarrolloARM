//@(#)SCADE2(W:SKDN08221CO2:SAT.DyP.Routing.Configuration.DataAccess:GetAllFromSCADERouteSelectionFactory:0:21/May/2008[SAT.DyP.Routing.Configuration:1.0:21/May/2008])
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
    /// This class is responsible for turning a selection criteria
    /// into a select statement and database query parameters.
    /// </summary>

    internal class GetAllFromSCADERouteSelectionFactory : ISelectionFactory<NullableIdentity> {
        /// <summary>
        /// Creates the GetAllFromSCADERouteSelectionFactory to build a select statement for
        /// the given .
        /// </summary>
        /// <param name=""> to select from the database.</param>      
        public GetAllFromSCADERouteSelectionFactory() {
        }

        #region ISelectionFactory<NullableIdentity> Members
        public DbCommand ConstructSelectCommand(Database db, NullableIdentity nullableIdentity) {

            DbCommand command = db.GetStoredProcCommand("dbo.GetAllFromSCADE_RoutingTable");
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
                default:
                    throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, GenericResources.InvalidParameterName), dbParameter);
            }
        }
        #endregion
    }
}

