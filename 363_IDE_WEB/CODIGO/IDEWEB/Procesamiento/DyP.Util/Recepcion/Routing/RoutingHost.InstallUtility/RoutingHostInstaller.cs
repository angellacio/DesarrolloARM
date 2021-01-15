using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Configuration.Install;
using System.Xml;

namespace RoutingHost.InstallUtility {
    [RunInstaller(true)]
    public partial class RoutingHostInstaller : Installer {
        public RoutingHostInstaller() {
            InitializeComponent();
        }

        public override void Install(System.Collections.IDictionary stateSaver) {
            string connectionString = Context.Parameters["connectionString"].ToString();
            string targetDir = Context.Parameters["targetDir"].ToString();
            try {
                string configLocation = targetDir + @"web.config";
                XmlDocument dom = new XmlDocument();
                dom.Load(configLocation);
                XmlElement cnElement = (XmlElement)dom.SelectSingleNode("configuration/connectionStrings/add[@name['SCADEDB']]");
                cnElement.Attributes["connectionString"].Value = connectionString;
                dom.Save(configLocation);
                base.Install(stateSaver);
            }
            catch (System.Xml.XPath.XPathException ex) {
                throw (ex);
            }
            catch (Exception ex) {
                throw (ex);
            }
        }
    }
}