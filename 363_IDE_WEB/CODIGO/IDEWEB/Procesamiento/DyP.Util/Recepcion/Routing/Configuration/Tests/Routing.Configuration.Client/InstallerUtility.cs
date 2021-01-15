//@(#)SCADE2(W:SKDN08221CO2:SAT.DyP.Routing.Configuration.Client:InstallerUtility:0:21/May/2008[SAT.DyP.Routing.Configuration.Client:1.0:21/May/2008])
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Configuration.Install;
using System.ServiceModel.Configuration;
using System.Text.RegularExpressions;
using System.Xml;

namespace SAT.DyP.Routing.Configuration.Client {
    [RunInstaller(true)]
    public partial class InstallerUtility : Installer {
        const string urlRegEx = @"(?:(?:(?:http)://)(?:w{3}\.)?(?:[a-zA-Z0-9/;\?&=:\-_\$\+!\*'\(\|\\~\[\]#%\.])+)";

        public InstallerUtility() {
            InitializeComponent();
        }

        private bool ValidUri(string uri) {            
            System.Text.RegularExpressions.Regex regExp = new System.Text.RegularExpressions.Regex(urlRegEx);
            Match match = Match.Empty;
            match = regExp.Match(uri);
            if (match.Success) {
                return (true);
            }
            else {
                return (false);
            }
        }

        public override void Install(System.Collections.IDictionary stateSaver) {
            string configurationServiceURI = Context.Parameters["configurationServiceUri"].ToString();
            string targetDir = Context.Parameters["targetDir"].ToString();
            if (ValidUri(configurationServiceURI)) {
                try {
                    string configLocation = targetDir + @"SAT.DyP.Routing.Configuration.Client.exe.config";
                    XmlDocument dom = new XmlDocument();
                    dom.Load(configLocation);
                    XmlElement endpoint = (XmlElement)dom.SelectSingleNode("/configuration/system.serviceModel/client/endpoint");
                    endpoint.Attributes["address"].Value = configurationServiceURI;
                    dom.Save(configLocation);
                    base.Install(stateSaver);
                }
                catch (Exception ex) {
                    throw (ex);
                }
            }
            else {
                throw (new System.Configuration.Install.InstallException("Invalid URI for the configuration service"));
            }                        
        }

        public override void Commit(System.Collections.IDictionary savedState) {
        }
    }
}