﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RecepcionIDEWEB.ServicioValidacion {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServicioValidacion.IValidacion")]
    public interface IValidacion {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IValidacion/iniciarValidacion", ReplyAction="http://tempuri.org/IValidacion/iniciarValidacionResponse")]
        void iniciarValidacion(int folio);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/IValidacion/iniciarValidacion", ReplyAction="http://tempuri.org/IValidacion/iniciarValidacionResponse")]
        System.IAsyncResult BegininiciarValidacion(int folio, System.AsyncCallback callback, object asyncState);
        
        void EndiniciarValidacion(System.IAsyncResult result);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IValidacionChannel : RecepcionIDEWEB.ServicioValidacion.IValidacion, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ValidacionClient : System.ServiceModel.ClientBase<RecepcionIDEWEB.ServicioValidacion.IValidacion>, RecepcionIDEWEB.ServicioValidacion.IValidacion {
        
        private BeginOperationDelegate onBegininiciarValidacionDelegate;
        
        private EndOperationDelegate onEndiniciarValidacionDelegate;
        
        private System.Threading.SendOrPostCallback oniniciarValidacionCompletedDelegate;
        
        public ValidacionClient() {
        }
        
        public ValidacionClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ValidacionClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ValidacionClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ValidacionClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> iniciarValidacionCompleted;
        
        public void iniciarValidacion(int folio) {
            base.Channel.iniciarValidacion(folio);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BegininiciarValidacion(int folio, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BegininiciarValidacion(folio, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public void EndiniciarValidacion(System.IAsyncResult result) {
            base.Channel.EndiniciarValidacion(result);
        }
        
        private System.IAsyncResult OnBegininiciarValidacion(object[] inValues, System.AsyncCallback callback, object asyncState) {
            int folio = ((int)(inValues[0]));
            return this.BegininiciarValidacion(folio, callback, asyncState);
        }
        
        private object[] OnEndiniciarValidacion(System.IAsyncResult result) {
            this.EndiniciarValidacion(result);
            return null;
        }
        
        private void OniniciarValidacionCompleted(object state) {
            if ((this.iniciarValidacionCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.iniciarValidacionCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void iniciarValidacionAsync(int folio) {
            this.iniciarValidacionAsync(folio, null);
        }
        
        public void iniciarValidacionAsync(int folio, object userState) {
            if ((this.onBegininiciarValidacionDelegate == null)) {
                this.onBegininiciarValidacionDelegate = new BeginOperationDelegate(this.OnBegininiciarValidacion);
            }
            if ((this.onEndiniciarValidacionDelegate == null)) {
                this.onEndiniciarValidacionDelegate = new EndOperationDelegate(this.OnEndiniciarValidacion);
            }
            if ((this.oniniciarValidacionCompletedDelegate == null)) {
                this.oniniciarValidacionCompletedDelegate = new System.Threading.SendOrPostCallback(this.OniniciarValidacionCompleted);
            }
            base.InvokeAsync(this.onBegininiciarValidacionDelegate, new object[] {
                        folio}, this.onEndiniciarValidacionDelegate, this.oniniciarValidacionCompletedDelegate, userState);
        }
    }
}
