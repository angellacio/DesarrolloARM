//@(#)SCADE2(W:SKDN08515FF4:Sat.Scade.Net.IDE.Presentacion.Proxy:ProxyRecepcionDeclaracion:0:23/Diciembre/2008[Sat.Scade.Net.IDE.Presentacion.Proxy:0:23/Diciembre/2008]) 
namespace Sat.Scade.Net.IDE.Presentacion.Proxy
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace = "http://www.sat.gob.mx/scade.net/2007_10_07/", ConfigurationName = "IDE_RecepcionDeclaracion_RecepcionSolicitud")]
    public interface IDE_RecepcionDeclaracion_RecepcionSolicitud
    {

        // CODEGEN: Generating message contract since the operation ProcesarIDE is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action = "ProcesarIDE", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        ProcesarIDEResponse ProcesarIDE(ProcesarIDERequest request);
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class ProcesarIDERequest
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.sat.gob.mx/scade.net/2007_10_07/Request", Order = 0)]
        public DocumentRequest DocumentRequest;

        public ProcesarIDERequest()
        {
        }

        public ProcesarIDERequest(DocumentRequest DocumentRequest)
        {
            this.DocumentRequest = DocumentRequest;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class ProcesarIDEResponse
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.sat.gob.mx/scade.net/2007_10_07/Response", Order = 0)]
        public DocumentResponse DocumentResponse;

        public ProcesarIDEResponse()
        {
        }

        public ProcesarIDEResponse(DocumentResponse DocumentResponse)
        {
            this.DocumentResponse = DocumentResponse;
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface IDE_RecepcionDeclaracion_RecepcionSolicitudChannel : IDE_RecepcionDeclaracion_RecepcionSolicitud, System.ServiceModel.IClientChannel
    {
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class IDE_RecepcionDeclaracion_RecepcionSolicitudClient : System.ServiceModel.ClientBase<IDE_RecepcionDeclaracion_RecepcionSolicitud>, IDE_RecepcionDeclaracion_RecepcionSolicitud
    {

        public IDE_RecepcionDeclaracion_RecepcionSolicitudClient()
        {
        }

        public IDE_RecepcionDeclaracion_RecepcionSolicitudClient(string endpointConfigurationName)
            :
                base(endpointConfigurationName)
        {
        }

        public IDE_RecepcionDeclaracion_RecepcionSolicitudClient(string endpointConfigurationName, string remoteAddress)
            :
                base(endpointConfigurationName, remoteAddress)
        {
        }

        public IDE_RecepcionDeclaracion_RecepcionSolicitudClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress)
            :
                base(endpointConfigurationName, remoteAddress)
        {
        }

        public IDE_RecepcionDeclaracion_RecepcionSolicitudClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress)
            :
                base(binding, remoteAddress)
        {
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ProcesarIDEResponse IDE_RecepcionDeclaracion_RecepcionSolicitud.ProcesarIDE(ProcesarIDERequest request)
        {
            return base.Channel.ProcesarIDE(request);
        }

        public DocumentResponse ProcesarIDE(DocumentRequest DocumentRequest)
        {
            ProcesarIDERequest inValue = new ProcesarIDERequest();
            inValue.DocumentRequest = DocumentRequest;
            ProcesarIDEResponse retVal = ((IDE_RecepcionDeclaracion_RecepcionSolicitud)(this)).ProcesarIDE(inValue);
            return retVal.DocumentResponse;
        }
    }

}
