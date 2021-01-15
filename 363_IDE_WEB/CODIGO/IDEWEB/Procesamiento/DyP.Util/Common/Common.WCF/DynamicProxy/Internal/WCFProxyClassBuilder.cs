//@(#)SCADE2(W:SKD08212CO2:SAT.DyP.Util.Service.WCF:WCFProxyClassBuilder:0:21/May/2008[SAT.DyP.Util.Service.WCF:1.0:21/May/2008])
using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using System.Reflection.Emit;
using System.Reflection;

namespace SAT.DyP.Util.Service.WCF.DynamicClientProxy.Internal
{
	/// <summary>
	/// Use a bit of reflection and code emiting to emit a nice proxy class that inherits from ClientBase<TInterface>, TInterface.
	/// The emited class follows the recommended ClientBase pattern.
	/// </summary>
	/// <typeparam name="TInterface"></typeparam>
	internal class WCFProxyClassBuilder<TInterface> : ClassBuilder<TInterface> where TInterface : class
	{
		public WCFProxyClassBuilder()
			: base(typeof(ClientBase<TInterface>), "DynamicClientProxy")
		{
		}

		/// <summary>
		/// Generate the contents of the method.
		/// </summary>
		/// <param name="method"></param>
		/// <param name="parameterTypes"></param>
		/// <param name="iLGenerator"></param>
		protected override void GenerateMethodImpl(MethodInfo method, Type[] parameterTypes, ILGenerator iLGenerator)
		{
			iLGenerator.Emit(OpCodes.Ldarg_0);	// this

			// Get the details Property of the ClientBase
			MethodInfo channelProperty = GetMethodFromBaseClass("get_Channel");
			// Get the channel: "base.Channel<TInterface>."
			iLGenerator.EmitCall(OpCodes.Call, channelProperty, null);

			// Prepare the parameters for the call
			ParameterInfo[] parameters = method.GetParameters();
			for (int index = 0; index < parameterTypes.Length; index++)
			{
				iLGenerator.Emit(OpCodes.Ldarg, (int)(((short)index) + 1));
			}

			// Call the Channel via the interface
			iLGenerator.Emit(OpCodes.Callvirt, method);

			// Thanks, all done
			iLGenerator.Emit(OpCodes.Ret);
		}

	}
}
