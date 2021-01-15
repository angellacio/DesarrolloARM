//@(#)SCADE2(W:SKD08212CO2:SAT.DyP.Util.Service.WCF:ServiceProxyHelper:0:21/May/2008[SAT.DyP.Util.Service.WCF:1.0:21/May/2008])
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Diagnostics;

namespace SAT.DyP.Util.Service.WCF
{
    /// <summary>
    /// Clase base que permite el acceso a un cliente proxy del servicio WCF
    /// asegurando el Dispose de los recursos
    /// </summary>
    /// <typeparam name="TServiceProxy">ClientBase Proxy</typeparam>
    /// <typeparam name="TServiceChannel">Service Channel</typeparam>
    public class ServiceProxyHelper<TServiceProxy,TServiceChannel>:IDisposable
        where TServiceProxy:ClientBase<TServiceChannel>,new()
        where TServiceChannel: class
    {

        private TServiceProxy _proxy;

        protected ServiceProxyHelper()
        {
            _proxy = new TServiceProxy();
        }

        protected TServiceProxy Proxy
        {
            get 
            {
                if (_proxy != null)
                    return _proxy;
                else
                    throw new ObjectDisposedException("ServiceProxyHelper");
            }       
        }


        #region IDisposable Members

        public void Dispose()
        {
            //se intenta cerrar la comunicación con el servicio
            //mediante el proxy asignado
            try
            {
                if (_proxy != null)
                {
                    if (_proxy.State != CommunicationState.Faulted)
                    {
                        _proxy.Close();
                    }
                    else
                    {
                        _proxy.Abort();
                    }
                }
            }
            catch (CommunicationException comEx)
            {
                EventLog.WriteEntry("DyP", comEx.Message);
                _proxy.Abort();                
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("DyP", ex.Message);
                _proxy.Abort();
                throw;
            }
            finally
            {
                _proxy = null;
            }
        }

        #endregion
    }
}
