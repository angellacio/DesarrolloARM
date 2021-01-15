//@(#)SCADE2(W:SKD08212CO2:SAT.DyP.Util.Service.WCF:IClientBase:0:21/May/2008[SAT.DyP.Util.Service.WCF:1.0:21/May/2008])
using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace SAT.DyP.Util.Service.WCF.DynamicClientProxy
{
    /// <summary>
    /// Delegate event when proxy class is created
    /// </summary>
    /// <param name="proxy">ClientBase Interface</param>
	public delegate void ProxyCreatedHandler(IClientBase proxy);

	/// <summary>
	/// Interface to expose the inner ClientBase properties hidden by the Proxy
	/// </summary>
	public interface IClientBase
	{
		/// <summary>
		/// Fired when a new proxy is created.
		/// In here you can initialize the Credentials and EndPoints.
		/// </summary>
		event ProxyCreatedHandler ProxyCreated;

		/// <summary>
		/// Gets the client credentials used to call an operation.
		/// Returns a System.ServiceModel.Description.ClientCredentials that represents
		/// the proof of identity presented by the client.
		/// </summary>
		ClientCredentials ClientCredentials { get; }

		/// <summary>
		/// Gets the target endpoint for the service to which the WCF client can connect.
		/// </summary>
		ServiceEndpoint Endpoint { get; }

		/// <summary>
		/// Gets the underlying System.ServiceModel.IClientChannel implementation.
		/// </summary>
		/// <value>The client channel for the WCF client object.</value>
		IClientChannel InnerChannel { get; }

		/// <summary>
		/// Gets the current state of the System.ServiceModel.ClientBase<TChannel> object.
		/// </summary>
		CommunicationState State { get; }

		/// <summary>
		/// Causes the System.ServiceModel.ClientBase<TChannel> object to transition
		//     immediately from its current state into the closed state.
		/// </summary>
		void Abort();

		/// <summary>
		/// Causes the System.ServiceModel.ClientBase<TChannel> object to transition
		//     from its current state into the closed state.
		/// </summary>
		void Close();

		/// <summary>
		/// Causes the System.ServiceModel.ClientBase<TChannel> object to transition
		//     from the created state into the opened state.
		/// </summary>
		void Open();
	}
}
