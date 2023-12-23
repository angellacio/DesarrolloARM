﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestMotor.Motor {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Motor.ICreditosFiscales")]
    public interface ICreditosFiscales {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICreditosFiscales/ObtieneLineaCaptura", ReplyAction="http://tempuri.org/ICreditosFiscales/ObtieneLineaCapturaResponse")]
        string ObtieneLineaCaptura(short idAplicacion, short idTipoDoc, string documento);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICreditosFiscales/ObtieneLineaCaptura", ReplyAction="http://tempuri.org/ICreditosFiscales/ObtieneLineaCapturaResponse")]
        System.Threading.Tasks.Task<string> ObtieneLineaCapturaAsync(short idAplicacion, short idTipoDoc, string documento);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICreditosFiscales/ObtieneLineaCapturaConPDF", ReplyAction="http://tempuri.org/ICreditosFiscales/ObtieneLineaCapturaConPDFResponse")]
        string ObtieneLineaCapturaConPDF(short idAplicacion, short idTipoDoc, string documento);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICreditosFiscales/ObtieneLineaCapturaConPDF", ReplyAction="http://tempuri.org/ICreditosFiscales/ObtieneLineaCapturaConPDFResponse")]
        System.Threading.Tasks.Task<string> ObtieneLineaCapturaConPDFAsync(short idAplicacion, short idTipoDoc, string documento);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICreditosFiscales/ObtieneLineaCapturaConImagen", ReplyAction="http://tempuri.org/ICreditosFiscales/ObtieneLineaCapturaConImagenResponse")]
        string ObtieneLineaCapturaConImagen(short idAplicacion, short idTipoDoc, string documento);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICreditosFiscales/ObtieneLineaCapturaConImagen", ReplyAction="http://tempuri.org/ICreditosFiscales/ObtieneLineaCapturaConImagenResponse")]
        System.Threading.Tasks.Task<string> ObtieneLineaCapturaConImagenAsync(short idAplicacion, short idTipoDoc, string documento);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICreditosFiscales/ConsultaLineasCapturaXDocumento", ReplyAction="http://tempuri.org/ICreditosFiscales/ConsultaLineasCapturaXDocumentoResponse")]
        string ConsultaLineasCapturaXDocumento(string documentos);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICreditosFiscales/ConsultaLineasCapturaXDocumento", ReplyAction="http://tempuri.org/ICreditosFiscales/ConsultaLineasCapturaXDocumentoResponse")]
        System.Threading.Tasks.Task<string> ConsultaLineasCapturaXDocumentoAsync(string documentos);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICreditosFiscales/ObtieneDocumentosEnLineaCaptura", ReplyAction="http://tempuri.org/ICreditosFiscales/ObtieneDocumentosEnLineaCapturaResponse")]
        string ObtieneDocumentosEnLineaCaptura(string lineaCaptura);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICreditosFiscales/ObtieneDocumentosEnLineaCaptura", ReplyAction="http://tempuri.org/ICreditosFiscales/ObtieneDocumentosEnLineaCapturaResponse")]
        System.Threading.Tasks.Task<string> ObtieneDocumentosEnLineaCapturaAsync(string lineaCaptura);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICreditosFiscales/ObtieneXMLOriginal", ReplyAction="http://tempuri.org/ICreditosFiscales/ObtieneXMLOriginalResponse")]
        string ObtieneXMLOriginal(string Folio);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICreditosFiscales/ObtieneXMLOriginal", ReplyAction="http://tempuri.org/ICreditosFiscales/ObtieneXMLOriginalResponse")]
        System.Threading.Tasks.Task<string> ObtieneXMLOriginalAsync(string Folio);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICreditosFiscales/ObtieneListaFolios", ReplyAction="http://tempuri.org/ICreditosFiscales/ObtieneListaFoliosResponse")]
        string ObtieneListaFolios(string RFC);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICreditosFiscales/ObtieneListaFolios", ReplyAction="http://tempuri.org/ICreditosFiscales/ObtieneListaFoliosResponse")]
        System.Threading.Tasks.Task<string> ObtieneListaFoliosAsync(string RFC);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICreditosFiscales/ObtienePDF", ReplyAction="http://tempuri.org/ICreditosFiscales/ObtienePDFResponse")]
        string ObtienePDF(string LineaCaptura);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICreditosFiscales/ObtienePDF", ReplyAction="http://tempuri.org/ICreditosFiscales/ObtienePDFResponse")]
        System.Threading.Tasks.Task<string> ObtienePDFAsync(string LineaCaptura);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICreditosFiscales/ObtieneConsultaFormatos", ReplyAction="http://tempuri.org/ICreditosFiscales/ObtieneConsultaFormatosResponse")]
        string ObtieneConsultaFormatos(int ADR, string RFC, string Folio, string LineaDeCaptura, string No_de_Resolucion, int rango_de_emision_FechaPago, System.DateTime fecha_ini, System.DateTime fecha_fin);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICreditosFiscales/ObtieneConsultaFormatos", ReplyAction="http://tempuri.org/ICreditosFiscales/ObtieneConsultaFormatosResponse")]
        System.Threading.Tasks.Task<string> ObtieneConsultaFormatosAsync(int ADR, string RFC, string Folio, string LineaDeCaptura, string No_de_Resolucion, int rango_de_emision_FechaPago, System.DateTime fecha_ini, System.DateTime fecha_fin);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICreditosFiscales/ObtienePDFXFolioyLineadeCaptura", ReplyAction="http://tempuri.org/ICreditosFiscales/ObtienePDFXFolioyLineadeCapturaResponse")]
        string ObtienePDFXFolioyLineadeCaptura(string Folio, string LineaCaptura);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICreditosFiscales/ObtienePDFXFolioyLineadeCaptura", ReplyAction="http://tempuri.org/ICreditosFiscales/ObtienePDFXFolioyLineadeCapturaResponse")]
        System.Threading.Tasks.Task<string> ObtienePDFXFolioyLineadeCapturaAsync(string Folio, string LineaCaptura);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICreditosFiscalesChannel : TestMotor.Motor.ICreditosFiscales, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CreditosFiscalesClient : System.ServiceModel.ClientBase<TestMotor.Motor.ICreditosFiscales>, TestMotor.Motor.ICreditosFiscales {
        
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
        
        public System.Threading.Tasks.Task<string> ObtieneLineaCapturaAsync(short idAplicacion, short idTipoDoc, string documento) {
            return base.Channel.ObtieneLineaCapturaAsync(idAplicacion, idTipoDoc, documento);
        }
        
        public string ObtieneLineaCapturaConPDF(short idAplicacion, short idTipoDoc, string documento) {
            return base.Channel.ObtieneLineaCapturaConPDF(idAplicacion, idTipoDoc, documento);
        }
        
        public System.Threading.Tasks.Task<string> ObtieneLineaCapturaConPDFAsync(short idAplicacion, short idTipoDoc, string documento) {
            return base.Channel.ObtieneLineaCapturaConPDFAsync(idAplicacion, idTipoDoc, documento);
        }
        
        public string ObtieneLineaCapturaConImagen(short idAplicacion, short idTipoDoc, string documento) {
            return base.Channel.ObtieneLineaCapturaConImagen(idAplicacion, idTipoDoc, documento);
        }
        
        public System.Threading.Tasks.Task<string> ObtieneLineaCapturaConImagenAsync(short idAplicacion, short idTipoDoc, string documento) {
            return base.Channel.ObtieneLineaCapturaConImagenAsync(idAplicacion, idTipoDoc, documento);
        }
        
        public string ConsultaLineasCapturaXDocumento(string documentos) {
            return base.Channel.ConsultaLineasCapturaXDocumento(documentos);
        }
        
        public System.Threading.Tasks.Task<string> ConsultaLineasCapturaXDocumentoAsync(string documentos) {
            return base.Channel.ConsultaLineasCapturaXDocumentoAsync(documentos);
        }
        
        public string ObtieneDocumentosEnLineaCaptura(string lineaCaptura) {
            return base.Channel.ObtieneDocumentosEnLineaCaptura(lineaCaptura);
        }
        
        public System.Threading.Tasks.Task<string> ObtieneDocumentosEnLineaCapturaAsync(string lineaCaptura) {
            return base.Channel.ObtieneDocumentosEnLineaCapturaAsync(lineaCaptura);
        }
        
        public string ObtieneXMLOriginal(string Folio) {
            return base.Channel.ObtieneXMLOriginal(Folio);
        }
        
        public System.Threading.Tasks.Task<string> ObtieneXMLOriginalAsync(string Folio) {
            return base.Channel.ObtieneXMLOriginalAsync(Folio);
        }
        
        public string ObtieneListaFolios(string RFC) {
            return base.Channel.ObtieneListaFolios(RFC);
        }
        
        public System.Threading.Tasks.Task<string> ObtieneListaFoliosAsync(string RFC) {
            return base.Channel.ObtieneListaFoliosAsync(RFC);
        }
        
        public string ObtienePDF(string LineaCaptura) {
            return base.Channel.ObtienePDF(LineaCaptura);
        }
        
        public System.Threading.Tasks.Task<string> ObtienePDFAsync(string LineaCaptura) {
            return base.Channel.ObtienePDFAsync(LineaCaptura);
        }
        
        public string ObtieneConsultaFormatos(int ADR, string RFC, string Folio, string LineaDeCaptura, string No_de_Resolucion, int rango_de_emision_FechaPago, System.DateTime fecha_ini, System.DateTime fecha_fin) {
            return base.Channel.ObtieneConsultaFormatos(ADR, RFC, Folio, LineaDeCaptura, No_de_Resolucion, rango_de_emision_FechaPago, fecha_ini, fecha_fin);
        }
        
        public System.Threading.Tasks.Task<string> ObtieneConsultaFormatosAsync(int ADR, string RFC, string Folio, string LineaDeCaptura, string No_de_Resolucion, int rango_de_emision_FechaPago, System.DateTime fecha_ini, System.DateTime fecha_fin) {
            return base.Channel.ObtieneConsultaFormatosAsync(ADR, RFC, Folio, LineaDeCaptura, No_de_Resolucion, rango_de_emision_FechaPago, fecha_ini, fecha_fin);
        }
        
        public string ObtienePDFXFolioyLineadeCaptura(string Folio, string LineaCaptura) {
            return base.Channel.ObtienePDFXFolioyLineadeCaptura(Folio, LineaCaptura);
        }
        
        public System.Threading.Tasks.Task<string> ObtienePDFXFolioyLineadeCapturaAsync(string Folio, string LineaCaptura) {
            return base.Channel.ObtienePDFXFolioyLineadeCapturaAsync(Folio, LineaCaptura);
        }
    }
}
