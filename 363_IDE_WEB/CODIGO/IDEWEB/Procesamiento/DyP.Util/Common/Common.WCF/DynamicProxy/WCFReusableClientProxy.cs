//@(#)SCADE2(W:SKD08212CO2:SAT.DyP.Util.Service.WCF:WCFReusableClientProxy:0:21/May/2008[SAT.DyP.Util.Service.WCF:1.0:21/May/2008])
using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace SAT.DyP.Util.Service.WCF.DynamicClientProxy
{
	/// <summary>
	/// Dynamically generated classes will inherit from this class.
	/// </summary>
	public abstract class WCFReusableClientProxy<T> : IClientBase where T : class
	{
		protected T _cachedProxy;
		private readonly string _configName;

		protected WCFReusableClientProxy(string configName)
		{
			this._configName = configName;
		}

		/// <summary>
		/// Called to retrieve a cached instance of the proxy.
		/// </summary>
		protected T Proxy
		{
			get
			{
				AssureProxy();
				return _cachedProxy;
			}
		}

		/// <summary>
		/// Close the proxy because it was fauled.		
		/// </summary>
		protected void CloseProxyBecauseOfException()
		{
			if (_cachedProxy != null)
			{
				ICommunicationObject wcfProxy = _cachedProxy as ICommunicationObject;
				try
				{
					if (wcfProxy != null)
					{
						if (wcfProxy.State != CommunicationState.Faulted)
						{
							wcfProxy.Close();
						}
						else
						{
							wcfProxy.Abort();
						}
					}
				}
				catch (CommunicationException)
				{
					wcfProxy.Abort();
				}
				catch (TimeoutException)
				{
					wcfProxy.Abort();
				}
				catch
				{
					wcfProxy.Abort();
					throw;
				}
				finally
				{
					_cachedProxy = null;
				}
			}
		}

		/// <summary>
		/// Create a new proxy if there is none already in use.
		/// If the previous proxy was faulted, it will be nulled and a new proxy is created
		/// </summary>
		private void AssureProxy()
		{
			if (_cachedProxy == null)
			{
				_cachedProxy = WCFClientProxy<T>.GetInstance(_configName);

				if (ProxyCreated != null)
				{
					ProxyCreated(this);
				}
			}
		}

		#region IClientBase Members
		public event ProxyCreatedHandler ProxyCreated;

		public ClientCredentials ClientCredentials
		{
			get
			{
				return (Proxy as ClientBase<T>).ClientCredentials;
			}
		}

		public System.ServiceModel.Description.ServiceEndpoint Endpoint
		{
			get
			{
				return (Proxy as ClientBase<T>).Endpoint;
			}
		}

		public IClientChannel InnerChannel
		{
			get
			{
				return (Proxy as ClientBase<T>).InnerChannel;
			}
		}

		public CommunicationState State
		{
			get
			{
				return (Proxy as ClientBase<T>).State;
			}
		}

		public void Abort()
		{
			try
			{
				(Proxy as ClientBase<T>).Abort();
			}
			finally
			{
				CloseProxyBecauseOfException();
			}
		}

		public void Close()
		{
			try
			{
				(Proxy as ClientBase<T>).Close();
			}
			finally
			{
				CloseProxyBecauseOfException();
			}
		}

		public void Open()
		{
			try
			{
				(Proxy as ClientBase<T>).Open();
				return;
			}
			catch
			{
				CloseProxyBecauseOfException();
				throw;
			}
		}

		#endregion
	}
}
