//@(#)SCADE2(W:SKDN08221CO2:SAT.DyP.Routing.Configuration.DataAccess:SCADERouteRepository:0:21/May/2008[SAT.DyP.Routing.Configuration:1.0:21/May/2008])
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SAT.DyP.Routing.Configuration.BusinessEntities;
using System.Data.SqlClient;
using System.Diagnostics;
using SAT.DyP.Routing.Configuration.DataAccess.SQLServer;

namespace SAT.DyP.Routing.Configuration.DataAccess {
    /// <summary>
    /// Respository that lets you find SCADERoute in the
    /// CRM database.
    /// </summary>
    public class SCADERouteRepository : Repository<SCADERoute> {
        private string databaseName;

        public SCADERouteRepository(string databaseName)
            : base(databaseName) {
            this.databaseName = databaseName;
        }


        public SCADERoute GetRouteByID(System.Int32 id_route) {
            ISelectionFactory<System.Int32> selectionFactory = new GetSCADERouteByid_routeSelectionFactory();

            try {
                return base.FindOne(selectionFactory, new GetSCADERouteByid_routeFactory(), id_route);
            }
            catch (SqlException ex) {
                HandleSqlException(ex, selectionFactory);
            }

            return new SCADERoute();
        }

        public List<SCADERoute> GetAllRoutes() {
            ISelectionFactory<NullableIdentity> selectionFactory = new GetAllFromSCADERouteSelectionFactory();

            try {
                NullableIdentity nullableIdentity = new NullableIdentity();
                return base.Find(selectionFactory, new GetAllFromSCADERouteFactory(), nullableIdentity);
            }
            catch (SqlException ex) {
                HandleSqlException(ex, selectionFactory);
            }

            return new List<SCADERoute>();
        }

        public void Add(SCADERoute SCADERoute) {
            SCADERouteInsertFactory insertFactory = new SCADERouteInsertFactory();
            try {
                base.Add(insertFactory, SCADERoute);
            }
            catch (SqlException ex) {
                HandleSqlException(ex, insertFactory);
            }
        }

        public void Remove(System.Int32 id_route) {
            IDeleteFactory<System.Int32> deleteFactory = new SCADERouteDeleteFactory();

            try {
                base.Remove(deleteFactory, id_route);
            }
            catch (SqlException ex) {
                HandleSqlException(ex, deleteFactory);
            }
        }


        public void Save(SCADERoute SCADERoute) {
            SCADERouteUpdateFactory updateFactory = new SCADERouteUpdateFactory();
            try {
                base.Save(updateFactory, SCADERoute);
            }
            catch (SqlException ex) {
                HandleSqlException(ex, updateFactory);
            }
        }

        private void HandleSqlException(SqlException ex, IDbToBusinessEntityNameMapper mapper) {
            if (ex.Number == ErrorCodes.SqlUserRaisedError) {
                switch (ex.State) {
                    case ErrorCodes.ValidationError:
                        string[] messageParts = ex.Errors[0].Message.Split(':');
                        throw new RepositoryValidationException(
                            mapper.MapDbParameterToBusinessEntityProperty(messageParts[1]),
                            messageParts[0], ex);

                    case ErrorCodes.ConcurrencyViolationError:
                        throw new ConcurrencyViolationException(ex.Message, ex);

                }
            }

            throw new RepositoryFailureException(ex);
        }
    }
}

