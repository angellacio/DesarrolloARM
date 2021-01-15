//@(#)SCADE2(W:SKDN08221CO2:SAT.DyP.Routing.Configuration.BusinessLogic:RoutingTableProcessor:0:21/May/2008[SAT.DyP.Routing.Configuration.BusinessLogic:1.0:21/May/2008])
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using DataAccess = SAT.DyP.Routing.Configuration.DataAccess;
using Entities = SAT.DyP.Routing.Configuration.BusinessEntities;

namespace SAT.DyP.Routing.Configuration.BusinessLogic {
    public class RoutingTableProcessor {
        private string _defaultExceptionPolicy = string.Empty;
        private string _defaultDB = string.Empty;

        public RoutingTableProcessor() {
            this._defaultExceptionPolicy = ConfigurationManager.AppSettings["BusinessObjectsExceptionPolicy"];
            this._defaultDB = ConfigurationManager.AppSettings["DefaultDBName"];
        }

        public void AddRoute(Entities.SCADERoute route) {
            try {
                DataAccess.SCADERouteRepository dbRoute = new SAT.DyP.Routing.Configuration.DataAccess.SCADERouteRepository(this._defaultDB);
                dbRoute.Add(route);
            }
            catch (Exception ex) {
                bool bFlag = ExceptionPolicy.HandleException(ex, this._defaultExceptionPolicy);
                throw (ex);
            }
        }

        public void SaveRoute(Entities.SCADERoute route) {
            try {
                DataAccess.SCADERouteRepository dbRoute = new SAT.DyP.Routing.Configuration.DataAccess.SCADERouteRepository(this._defaultDB);
                dbRoute.Save(route);
            }
            catch (Exception ex) {
                bool bFlag = ExceptionPolicy.HandleException(ex, this._defaultExceptionPolicy);
                throw (ex);
            }
        }

        public void DeleteRoute(int id_route) {
            try {
                DataAccess.SCADERouteRepository dbRoute = new SAT.DyP.Routing.Configuration.DataAccess.SCADERouteRepository(this._defaultDB);
                dbRoute.Remove(id_route);
            }
            catch (Exception ex) {
                bool bFlag = ExceptionPolicy.HandleException(ex, this._defaultExceptionPolicy);
                throw (ex);
            }
        }

        public List<Entities.SCADERoute> GetAllRoutes() {
            try {
                DataAccess.SCADERouteRepository dbRoute = new SAT.DyP.Routing.Configuration.DataAccess.SCADERouteRepository(this._defaultDB);
                return (dbRoute.GetAllRoutes());
            }
            catch (Exception ex) {
                bool bFlag = ExceptionPolicy.HandleException(ex, this._defaultExceptionPolicy);
                throw (ex);
            }
        }

        public Entities.SCADERoute GetRoute(int id_route) {
            try {
                DataAccess.SCADERouteRepository dbRoute = new SAT.DyP.Routing.Configuration.DataAccess.SCADERouteRepository(this._defaultDB);
                return(dbRoute.GetRouteByID(id_route));
            }
            catch (Exception ex) {
                bool bFlag = ExceptionPolicy.HandleException(ex, this._defaultExceptionPolicy);
                throw (ex);
            }
        }
    }
}
