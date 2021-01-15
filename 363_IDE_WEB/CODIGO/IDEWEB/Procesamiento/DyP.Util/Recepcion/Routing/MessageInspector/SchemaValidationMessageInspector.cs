//@(#)SCADE2(W:SKDN08221CO2:SAT.DyP.Routing.Services.SchemaValidationMessageInspector:SchemaValidationBehavior:0:21/May/2008[SAT.DyP.Routing.Services.MessageInspection:1.0:21/May/2008])
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace SAT.DyP.Routing.Services.MessageInspection {
    public class SchemaValidationMessageInspector : IDispatchMessageInspector {
        XmlSchemaSet schemaSet;
        bool validateRequest;
        bool validateReply;

        public SchemaValidationMessageInspector(XmlSchemaSet schemaSet, bool validateRequest, bool validateReply) {
            this.schemaSet = schemaSet;
            this.validateReply = validateReply;
            this.validateRequest = validateRequest;
        }

        void ValidateMessageBody(ref System.ServiceModel.Channels.Message message) {
            if (!message.IsFault) {
                try {
                    XmlReader dr = message.GetReaderAtBodyContents();
                    if (dr.NamespaceURI != string.Empty && schemaSet.Contains(dr.NamespaceURI)) {

                        string strXmlToValidate = dr.ReadOuterXml();
                        XmlReaderSettings settings = new XmlReaderSettings();
                        settings.CheckCharacters = true;
                        settings.ConformanceLevel = ConformanceLevel.Auto;
                        settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessSchemaLocation & XmlSchemaValidationFlags.ProcessIdentityConstraints & XmlSchemaValidationFlags.ProcessInlineSchema & XmlSchemaValidationFlags.ReportValidationWarnings;
                        settings.ValidationType = ValidationType.Schema;
                        settings.Schemas = this.schemaSet;
                        XmlReader validationReader = XmlReader.Create(new StringReader(strXmlToValidate), settings);

                        Message newMessage = Message.CreateMessage(message.Version, null, validationReader);
                        newMessage.Headers.CopyHeadersFrom(message);
                        newMessage.Properties.CopyProperties(message.Properties);
                        message = newMessage;
                    }
                    else {
                        throw (new ArgumentException(string.Format(Properties.Resources.MSG_Error_ErrorValidatingMessageAtInspection, message.Headers.To, message.ToString()), "Message"));
                    }
                }
                catch (Exception ex) {
                    Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex, ConfigurationManager.AppSettings["DefaultExceptionPolicy"]);
                    throw (new RequestValidationFault(ex.Message));
                }
            }
        }

        void settings_ValidationEventHandler(object sender, ValidationEventArgs e) {
            Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.ExceptionPolicy.HandleException(e.Exception, ConfigurationManager.AppSettings["DefaultExceptionPolicy"]);
            throw (new RequestValidationFault(e.Message));
        }

        #region IDispatchMessageInspector Members
        object IDispatchMessageInspector.AfterReceiveRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel, System.ServiceModel.InstanceContext instanceContext) {
            if (validateRequest) {
                ValidateMessageBody(ref request);
            }
            return (null);
        }

        void IDispatchMessageInspector.BeforeSendReply(ref System.ServiceModel.Channels.Message reply, object correlationState) {
            if (validateReply) {
                ValidateMessageBody(ref reply);
            }
        }
        #endregion
    }
}