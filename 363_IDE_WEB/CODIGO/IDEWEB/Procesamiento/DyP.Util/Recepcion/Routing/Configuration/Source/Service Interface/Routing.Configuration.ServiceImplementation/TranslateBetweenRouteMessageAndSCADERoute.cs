//@(#)SCADE2(W:SKDN08221CO2:SAT.DyP.Routing.Configuration.ServiceImplementation:TranslateBetweenRouteMessageAndSCADERoute:0:21/May/2008[SAT.DyP.Routing.Configuration.ServiceImplementation:1.0:21/May/2008])
using System;
using System.Collections.Generic;
using SAT.DyP.Routing.Configuration.DataContracts;
using SAT.DyP.Routing.Configuration.BusinessEntities;

namespace SAT.DyP.Routing.Configuration.ServiceImplementation {
    public static class TranslateBetweenRouteMessageAndSCADERoute {

        public static List<SCADERoute> TranslateRouteMessageListToSCADERouteList(List<RouteMessage> lRouteMessage) {
            List<SCADERoute> lScadeRoute = new List<SCADERoute>();
            foreach(RouteMessage rMessage in lRouteMessage){
                SCADERoute route = TranslateBetweenRouteMessageAndSCADERoute.TranslateRouteMessageToSCADERoute(rMessage);
                lScadeRoute.Add(route);
            }
            return (lScadeRoute);
        }

        public static List<RouteMessage> TranslateSCADERouteListToRouteMessageList(List<SCADERoute> lScadeRoute) {
            List<RouteMessage> lRouteMessage = new List<RouteMessage>();
            foreach (SCADERoute scadeRoute in lScadeRoute) {
                RouteMessage routeMessage = TranslateBetweenRouteMessageAndSCADERoute.TranslateSCADERouteToRouteMessage(scadeRoute);
                lRouteMessage.Add(routeMessage);
            }
            return (lRouteMessage);
        }

        public static SAT.DyP.Routing.Configuration.DataContracts.RouteMessage TranslateSCADERouteToRouteMessage(SAT.DyP.Routing.Configuration.BusinessEntities.SCADERoute from) {
            SAT.DyP.Routing.Configuration.DataContracts.RouteMessage to = new SAT.DyP.Routing.Configuration.DataContracts.RouteMessage();
            to.IDRoute = from.id_route;
            to.SoapAction = from.soap_action;
            to.Destination = from.destination;
            to.Description = from.description;
            return to;
        }

        public static SAT.DyP.Routing.Configuration.BusinessEntities.SCADERoute TranslateRouteMessageToSCADERoute(SAT.DyP.Routing.Configuration.DataContracts.RouteMessage from) {
            SAT.DyP.Routing.Configuration.BusinessEntities.SCADERoute to = new SAT.DyP.Routing.Configuration.BusinessEntities.SCADERoute();
            to.id_route = from.IDRoute;
            to.soap_action = from.SoapAction;
            to.destination = from.Destination;
            to.description = from.Description;
            return to;
        }
    }
}

