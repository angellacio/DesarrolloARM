﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18047
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SAT.CreditosFiscales.Motor.Pruebas.MotorWCF {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="MotorWCF.ICreditosFiscales")]
    public interface ICreditosFiscales {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICreditosFiscales/ObtieneLineaCaptura", ReplyAction="http://tempuri.org/ICreditosFiscales/ObtieneLineaCapturaResponse")]
        string ObtieneLineaCaptura(short idAplicacion, short idTipoDoc, string documento);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICreditosFiscales/ObtieneLineaCapturaConPDF", ReplyAction="http://tempuri.org/ICreditosFiscales/ObtieneLineaCapturaConPDFResponse")]
        string ObtieneLineaCapturaConPDF(short idAplicacion, short idTipoDoc, string documento);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICreditosFiscales/ConsultaLineasCapturaXDocumento", ReplyAction="http://tempuri.org/ICreditosFiscales/ConsultaLineasCapturaXDocumentoResponse")]
        string ConsultaLineasCapturaXDocumento(string documentos);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICreditosFiscales/ObtieneDocumentosEnLineaCaptura", ReplyAction="http://tempuri.org/ICreditosFiscales/ObtieneDocumentosEnLineaCapturaResponse")]
        string ObtieneDocumentosEnLineaCaptura(string lineaCaptura);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICreditosFiscalesChannel : SAT.CreditosFiscales.Motor.Pruebas.MotorWCF.ICreditosFiscales, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CreditosFiscalesClient : System.ServiceModel.ClientBase<SAT.CreditosFiscales.Motor.Pruebas.MotorWCF.ICreditosFiscales>, SAT.CreditosFiscales.Motor.Pruebas.MotorWCF.ICreditosFiscales {
        
        public CreditosFiscalesClient() {
        }
        
        public CreditosFiscalesClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CreditosFiscalesClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CreditosFiscalesClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CreditosFiscalesClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string ObtieneLineaCaptura(short idAplicacion, short idTipoDoc, string documento) {
            return base.Channel.ObtieneLineaCaptura(idAplicacion, idTipoDoc, documento);
        }
        
        public string ObtieneLineaCapturaConPDF(short idAplicacion, short idTipoDoc, string documento) {
            return base.Channel.ObtieneLineaCapturaConPDF(idAplicacion, idTipoDoc, documento);
        }
        
        public string ConsultaLineasCapturaXDocumento(string documentos) {
            return base.Channel.ConsultaLineasCapturaXDocumento(documentos);
        }
        
        public string ObtieneDocumentosEnLineaCaptura(string lineaCaptura) {
            return base.Channel.ObtieneDocumentosEnLineaCaptura(lineaCaptura);
        }
    }
}