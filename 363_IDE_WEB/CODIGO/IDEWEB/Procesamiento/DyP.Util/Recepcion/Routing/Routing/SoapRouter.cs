//@(#)SCADE2(W:SKDN08221CO2:SAT.DyP.Routing.Services:SoapRouter:0:21/May/2008[SAT.DyP.Routing.Services:1.0:21/May/2008])
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Description;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Xml;
using System.Xml.Serialization;
using ExceptionHandling = Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Diagnostics;
using System.Text;
using System.IO;

namespace SAT.DyP.Routing.Services {

    [SoapRouterServiceBehavior]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Multiple, ValidateMustUnderstand = false)]
    public sealed class SoapRouter : IRequestReplyDatagramRouter, IDisposable {

        SoapRouterExtension  _soapRouterExtension = null;
        string _defaultExceptionPolicyName = string.Empty;

        public SoapRouter() {
            ServiceHostBase host = OperationContext.Current.Host;
            this._soapRouterExtension = host.Extensions.Find<SoapRouterExtension>();
            this._defaultExceptionPolicyName = System.Configuration.ConfigurationManager.AppSettings["DefaultExceptionPolicy"];
        }

        #region SoapIntermediary Request-Reply Datagram
        Message IRequestReplyDatagramRouter.ProcessMessage(Message message) {
            try {

                EndpointAddress to = this._soapRouterExtension.RoutingTable.SelectDestination(message);

                if (to == null) {
                    throw (new System.ServiceModel.EndpointNotFoundException(string.Format(Routing.Services.Properties.Resources.MSG_Error_EndpointNotFoundException, message.Headers.Action)));
                }

                IRequestReplyDatagramRouter forwardingChannel;
                if (!this._soapRouterExtension.RequestReplyDatagramChannels.TryGetValue(to, out forwardingChannel) 
                    || ((IClientChannel)forwardingChannel).State != CommunicationState.Opened)
                {

                    lock (this._soapRouterExtension.RequestReplyDatagramChannels){

                        if (!this._soapRouterExtension.RequestReplyDatagramChannels.TryGetValue(to, out forwardingChannel) 
                            || ((IClientChannel)forwardingChannel).State != CommunicationState.Opened) {

                            ChannelFactory<IRequestReplyDatagramRouter> factory = new ChannelFactory<IRequestReplyDatagramRouter>(this._soapRouterExtension.Bindings[to.Uri.Scheme], to);
                            factory.Endpoint.Behaviors.Add(new MustUnderstandBehavior(false));
                            forwardingChannel = factory.CreateChannel();
                            this._soapRouterExtension.RequestReplyDatagramChannels[to] = forwardingChannel;
                        }
                    }
                }

                message.Headers.To = to.Uri;

                try {

                    Message response = forwardingChannel.ProcessMessage(message);
                    return response;
                }
                catch (Exception ex){
                    //se agrega descripción para el seguimiento del error
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine( string.Format("Servicio de Ruteo : Error al procesar el mensaje para {0} ", message.Headers.Action));
                    sb.AppendLine( string.Format(Routing.Services.Properties.Resources.MSG_Error_ErrorProcessingMessageAtDestination, message.Headers.Action, to.Uri.ToString(), ex.Message));

                    throw new FaultException(sb.ToString());
                }

            }
            catch (Exception ex) {
                bool bRethrow = ExceptionHandling.ExceptionPolicy.HandleException(ex, this._defaultExceptionPolicyName);
                if (bRethrow)
                    throw (ex);
                else
                    return null;
            }
        }

        private void LogMessage(Message message)
        {
            XmlTextWriter writer = null;
            try
            {
                StringBuilder builder = new StringBuilder();
                StringWriter stringWriter = new StringWriter(builder);
                writer = new XmlTextWriter(stringWriter);
                message.WriteBody(writer);
                writer.Flush();

                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(builder.ToString(), "DyP Routing Events Category");
            }
            catch
            {
            }
            finally
            {
                if (writer != null) writer.Close();
            }
                
        }
        #endregion

        void IDisposable.Dispose() {            
        }
    }
}