//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Comunes.Herramientas:Sat.CreditosFiscales.Comunes.Herramientas.InterceptorMensajes:1:12/07/2013[Assembly:1.0:12/07/2013])

/*
 * Actualizó: Mario Escarpulli
 * Fecha: 10/Jun/2019
*/

// Referencias de sistema.
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Sat.CreditosFiscales.Comunes.Herramientas
{
	/// <summary>
	/// 
	/// </summary>
	public class InterceptorMensajes : IEndpointBehavior, IClientMessageInspector
	{
		/// <summary>
		/// 
		/// </summary>
		public string XMLPeticion { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string XMLRespuesta { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public InterceptorMensajes()
		{ }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="endpoint"></param>
		/// <param name="clientRuntime"></param>
		public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
		{
			clientRuntime.MessageInspectors.Add(this);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="endpoint"></param>
		/// <param name="bindingParameters"></param>
		public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters) { }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="endpoint"></param>
		/// <param name="endpointDispatcher"></param>
		public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher) { }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="endpoint"></param>
		public void Validate(ServiceEndpoint endpoint) { }

		public void AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
		{
			XMLRespuesta = reply.ToString();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="request"></param>
		/// <param name="channel"></param>
		/// <returns></returns>
		public object BeforeSendRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel)
		{
			XMLPeticion = request.ToString();
			return null;
		}
	}
}