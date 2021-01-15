
//@(#)SCADE2(W:SKDN08221CO2:SAT.DyP.Routing.Services.MessageInspection:SchemaValidationBehavior:0:21/May/2008[SAT.DyP.Routing.Services.MessageInspection:1.0:21/May/2008])
	
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.Xml;
using System.Xml.Schema;


namespace SAT.DyP.Routing.Services.MessageInspection
{
    public class SchemaValidationBehavior : IEndpointBehavior
    {
        XmlSchemaSet    _schemaSet = null; 
        bool            _validateRequest = false;
        bool            _validateReply = false;
        bool            _initialized = false;
        string          _behaviorName = string.Empty;

        public SchemaValidationBehavior(XmlSchemaSet schemaSet, bool inspectRequest, bool inspectReply)
        {          
            this._schemaSet = schemaSet;
            this._validateReply = inspectReply;
            this._validateRequest = inspectRequest;
            this._initialized = true;
        }

        public SchemaValidationBehavior(string behaviorName) {
            this._schemaSet = new XmlSchemaSet();
            this._behaviorName = behaviorName;
        }
   

        #region IEndpointBehavior Members

        public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
            if (!this._initialized) {
                BehaviorsSection behaviorsSection = (BehaviorsSection)ConfigurationManager.GetSection("system.serviceModel/behaviors");
                EndpointBehaviorElementCollection endpointBehaviors = behaviorsSection.EndpointBehaviors;
                EndpointBehaviorElement behaviorElement = (EndpointBehaviorElement)endpointBehaviors[this._behaviorName];
                SchemaValidationBehaviorExtensionElement schemaSection = (SchemaValidationBehaviorExtensionElement)behaviorElement[typeof(SchemaValidationBehaviorExtensionElement)];

                foreach (SchemaConfigElement schemaCfg in schemaSection.Schemas) {
                    Uri baseSchema = new Uri(AppDomain.CurrentDomain.BaseDirectory);
                    string location = new Uri(baseSchema, schemaCfg.Location).ToString();
                    XmlSchema schema = XmlSchema.Read(new XmlTextReader(location), null);
                    this._schemaSet.Add(schema);
                }

                this._validateReply = schemaSection.ValidateReply;
                this._validateRequest = schemaSection.ValidateRequest;
            }
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
        {

        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher)
        {
            SchemaValidationMessageInspector inspector = new SchemaValidationMessageInspector(this._schemaSet, this._validateRequest, this._validateReply);
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(inspector);
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }

        #endregion
    }
}
