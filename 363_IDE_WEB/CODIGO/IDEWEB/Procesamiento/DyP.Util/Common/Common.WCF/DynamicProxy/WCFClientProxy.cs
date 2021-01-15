//@(#)SCADE2(W:SKD08212CO2:SAT.DyP.Util.Service.WCF:WCFClientProxy:0:21/May/2008[SAT.DyP.Util.Service.WCF:1.0:21/May/2008])
using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using SAT.DyP.Util.Service.WCF.DynamicClientProxy.Internal;

namespace SAT.DyP.Util.Service.WCF.DynamicClientProxy
{
	/// <summary>
	/// Dynamic generator for a WCF ClientBase proxies
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class WCFClientProxy<TInterface> where TInterface : class 
	{
		private static IDictionary<Type, string> _registeredContracts = new Dictionary<Type, string>();

		/// <summary>
		/// Registers a specific interface with a specific configuration.
		/// </summary>
		/// <param name="configName"></param>
		public static void RegisterEndpoint(string configName)
		{
			lock (_registeredContracts)
			{
				_registeredContracts[typeof(TInterface)] = configName;
				WCFProxyClassBuilder<TInterface> builder = new WCFProxyClassBuilder<TInterface>();
				builder.GenerateType();	// force the Class builder to generate it's type
			}
		}

		/// <summary>
		/// Returns a configured registered instance.
		/// </summary>
		/// <returns></returns>
		public static TInterface GetRegisteredInstance()
		{
			lock (_registeredContracts)
			{
				string configName = null;
				if (_registeredContracts.TryGetValue(typeof(TInterface), out configName))
				{
					return GetReusableInstance(configName);
				}
			}
			throw new ApplicationException("Could not find registered contract:" + typeof(TInterface).FullName);
		}

		/// <summary>
		/// Returns a new instance for a client proxy over the specified interface with the specified config name used for initialization.
		/// This is a simple instance of a ClientBase derived class.
		/// </summary>
		public static TInterface GetInstance(string configName)
		{
			// Build the type
			WCFProxyClassBuilder<TInterface> builder = new WCFProxyClassBuilder<TInterface>();
			Type type = builder.GenerateType();

			// Create new instance
			TInterface instance = (TInterface)Activator.CreateInstance(type, new object[] { configName });

			return instance;
		}

		/// <summary>
		/// Returns a new instance for a client proxy over the specified interface with the specified config name used for initialization.
		/// This instance of the proxy can be reused as it will always "clean" itself
		/// if the channel is faulted.
		/// </summary>
		public static TInterface GetReusableInstance(string configName)
		{
			// Build the type
			WCFReusableProxyClassBuilder<TInterface> builder = new WCFReusableProxyClassBuilder<TInterface>();
			Type type = builder.GenerateType();

			// Create new instance
			TInterface instance = (TInterface)Activator.CreateInstance(type, new object[] { configName });

			return instance;
		}

		/// <summary>
		/// Returns a new instance for a client proxy over the specified interface with the specified config name used for initialization.
		/// This instance of the proxy can be reused as it will always "clean" itself
		/// if the channel is faulted.
		/// The class will also unwrap any FaultException and throw the original Exception.
		/// </summary>
		public static TInterface GetReusableFaultUnwrappingInstance(string configName)
		{
			// Build the type
			WCFReusableFaultWrapperProxyClassBuilder<TInterface> builder = new WCFReusableFaultWrapperProxyClassBuilder<TInterface>();
			Type type = builder.GenerateType();

			// Create new instance
			TInterface instance = (TInterface)Activator.CreateInstance(type, new object[] { configName });

			return instance;
		}

	}
}
