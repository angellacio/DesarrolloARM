//@(#)SCADE2(W:SKDN08221CO2:SAT.DyP.Routing.Services:RoutingTable:0:21/May/2008[SAT.DyP.Routing.Services:1.0:21/May/2008])
using System;
using System.Data;
using System.Data.SqlClient;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;
using System.Configuration;
using System.Xml;
using System.Xml.Serialization;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.Collections.Generic;
using ExceptionHandling = Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Logging           = Microsoft.Practices.EnterpriseLibrary.Logging;
using Caching           = Microsoft.Practices.EnterpriseLibrary.Caching;

namespace SAT.DyP.Routing.Services {

    public class RoutingTable {
        XPathMessageFilterTable<EndpointAddress> filterTable = null;

        public RoutingTable() {
            this.LoadRoutingTable();
        }

        #region RoutingTable
        private void LoadRoutingTable() {          
            Caching.CacheManager cacheManager = Caching.CacheFactory.GetCacheManager(ConfigurationManager.AppSettings["DefaultCacheManager"]);            
            if (cacheManager.Contains("SAT.DyP.Routing.Services.RoutingTable")) {
                this.filterTable = (XPathMessageFilterTable<EndpointAddress>)cacheManager.GetData("SAT.DyP.Routing.Services.RoutingTable");
            }
            else {
                if (this.filterTable == null) this.filterTable = new XPathMessageFilterTable<EndpointAddress>();
                XmlNamespaceManager manager = new XPathMessageContext();
                SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SCADEDB"].ConnectionString);
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = Properties.Resources.Res_SP_GetAllRoutes;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                try {
                    cn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read()) {
                        this.filterTable.Add(new XPathMessageFilter(dr.GetSqlString(1).Value, manager), new EndpointAddress(dr.GetSqlString(2).Value));
                    }
                    dr.Close();
                    dr.Dispose();
                    cacheManager.Add("SAT.DyP.Routing.Services.RoutingTable", this.filterTable);
                }
                finally {
                    if (cn != null) {
                        cn.Close();
                        cn.Dispose();
                    }
                }
            }            
        }

        #endregion

        public XPathMessageFilterTable<EndpointAddress> FilterTable {
            set { this.filterTable = value; }
            get { return this.filterTable; }
        }

        public EndpointAddress SelectDestination(Message message) {
            EndpointAddress selectedAddress = null;
            IList<EndpointAddress> results = new List<EndpointAddress>();
            this.filterTable.GetMatchingValues(message, results);
            if (results.Count >= 1) {
                selectedAddress = results[0];
            }
            return selectedAddress;
        }
    }
}