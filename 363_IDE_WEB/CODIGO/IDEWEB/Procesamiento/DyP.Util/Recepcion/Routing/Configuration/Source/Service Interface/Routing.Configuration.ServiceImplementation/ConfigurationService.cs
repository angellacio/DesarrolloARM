//@(#)SCADE2(W:SKDN08221CO2:SAT.DyP.Routing.Configuration.ServiceImplementation:ConfigurationService:0:21/May/2008[SAT.DyP.Routing.Configuration.ServiceImplementation:1.0:21/May/2008])
using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using SAT.DyP.Routing.Configuration.BusinessLogic;
using SAT.DyP.Routing.Configuration.FaultContracts;
using SAT.DyP.Routing.Configuration.ServiceContracts;

namespace SAT.DyP.Routing.Configuration.ServiceImplementation {
    [ServiceBehavior(Name = "ConfigurationService", Namespace = "http://www.sat.gob.mx/scade.net/2007_10_07/routing")]
    public class ConfigurationService : SAT.DyP.Routing.Configuration.ServiceContracts.IConfigurationService {
        #region IConfigurationService Members
        
        public List<SAT.DyP.Routing.Configuration.DataContracts.RouteMessage> GetAllRoutes() {
            try {
                BusinessLogic.RoutingTableProcessor routingTableProc = new RoutingTableProcessor();
                List<BusinessEntities.SCADERoute> routes = routingTableProc.GetAllRoutes();
                List<Routing.Configuration.DataContracts.RouteMessage> routeMessages = TranslateBetweenRouteMessageAndSCADERoute.TranslateSCADERouteListToRouteMessageList(routes);
                return (routeMessages);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public SAT.DyP.Routing.Configuration.DataContracts.RouteMessage GetRoute(int request) {
            try {
                BusinessLogic.RoutingTableProcessor routingTableProc = new RoutingTableProcessor();
                BusinessEntities.SCADERoute scadeRoute = routingTableProc.GetRoute(request);
                DataContracts.RouteMessage routeMessage = TranslateBetweenRouteMessageAndSCADERoute.TranslateSCADERouteToRouteMessage(scadeRoute);
                return (routeMessage);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public void AddRoute(SAT.DyP.Routing.Configuration.DataContracts.RouteMessage request) {
            try {
                BusinessEntities.SCADERoute scadeRouteEntity = TranslateBetweenRouteMessageAndSCADERoute.TranslateRouteMessageToSCADERoute(request);
                BusinessLogic.RoutingTableProcessor routingTableProc = new RoutingTableProcessor();
                routingTableProc.AddRoute(scadeRouteEntity);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public void DeleteRoute(int request) {
            try {
                BusinessLogic.RoutingTableProcessor routingTableProc = new RoutingTableProcessor();
                routingTableProc.DeleteRoute(request);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public void SaveRoute(SAT.DyP.Routing.Configuration.DataContracts.RouteMessage request) {
            try {
                BusinessEntities.SCADERoute scadeRouteEntity = TranslateBetweenRouteMessageAndSCADERoute.TranslateRouteMessageToSCADERoute(request);
                BusinessLogic.RoutingTableProcessor routingTableProc = new RoutingTableProcessor();
                routingTableProc.SaveRoute(scadeRouteEntity);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        #endregion
    }
}
