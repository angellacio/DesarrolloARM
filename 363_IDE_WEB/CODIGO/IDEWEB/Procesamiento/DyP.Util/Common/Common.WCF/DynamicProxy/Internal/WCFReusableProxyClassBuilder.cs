//@(#)SCADE2(W:SKD08212CO2:SAT.DyP.Util.Service.WCF:WCFReusableProxyClassBuilder:0:21/May/2008[SAT.DyP.Util.Service.WCF:1.0:21/May/2008])
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;

namespace SAT.DyP.Util.Service.WCF.DynamicClientProxy.Internal
{
	/// <summary>
	/// Builds a class inheriting from WCFClientProxy.cs that will wrap a WCF Proxy
	/// and automatically rebuild the proxy if the channel is faulted.
	/// </summary>
	internal class WCFReusableProxyClassBuilder<TInterface> : ClassBuilder<TInterface> where TInterface : class
	{
		public WCFReusableProxyClassBuilder()
			: base(typeof(WCFReusableClientProxy<TInterface>), "ReusableProxy")
		{
		}
		public WCFReusableProxyClassBuilder(Type baseClassType, string classNameSuffix)
			: base(baseClassType, classNameSuffix)
		{
		}

		/// <summary>
		/// Generate the contents of the method. This will generate:
		/// ...
		/// try
		///	{
		///		return Proxy.MethodName(arg1, arg2);
		///	}
		///	catch
		///	{
		///		CloseProxyBecauseOfException();
		///		throw;
		///	}
		/// ...
		/// </summary>
		/// <param name="method"></param>
		/// <param name="parameterTypes"></param>
		/// <param name="iLGenerator"></param>
		protected override void GenerateMethodImpl(System.Reflection.MethodInfo method, Type[] parameterTypes, System.Reflection.Emit.ILGenerator iLGenerator)
		{
			bool hasReturn = !IsVoidMethod(method);
			if (hasReturn)
			{
				// declare a variable to contain the return type
				// string returnValue;
				iLGenerator.DeclareLocal(method.ReturnType);
			}

			// try {
			Label tryLabel = iLGenerator.BeginExceptionBlock();
			{
				// this
				iLGenerator.Emit(OpCodes.Ldarg_0);

				// Get the details Property of the ClientBase
				MethodInfo proxyProperty = GetMethodFromBaseClass("get_Proxy");
				// Get the channel: "base.Channel<TInterface>."
				iLGenerator.EmitCall(OpCodes.Call, proxyProperty, null);

				// Prepare the parameters for the call
				ParameterInfo[] parameters = method.GetParameters();
				for (int index = 0; index < parameterTypes.Length; index++)
				{
					iLGenerator.Emit(OpCodes.Ldarg, (int)(((short)index) + 1));
				}

				// Call the Proxy via the interface
				iLGenerator.Emit(OpCodes.Callvirt, method);

				if (hasReturn)
				{
					// returnValue = result of the function call
					iLGenerator.Emit(OpCodes.Stloc_0);
				}
			}
			// catch {
			{
				GenerateStandardCatch(iLGenerator);
			}
			// }
			iLGenerator.EndExceptionBlock();

			if (hasReturn)
			{
				// return returnValue;
				iLGenerator.Emit(OpCodes.Ldloc_0);
			}

			// Thanks, all done
			iLGenerator.Emit(OpCodes.Ret);
		}

		protected void GenerateStandardCatch(System.Reflection.Emit.ILGenerator iLGenerator)
		{
			iLGenerator.BeginCatchBlock(typeof(object));

			iLGenerator.Emit(OpCodes.Pop);	// get the exception from the stack

			// this
			iLGenerator.Emit(OpCodes.Ldarg_0);

			//call base.CloseProxyBecauseOfException();
			MethodInfo closeProxyMethod = GetMethodFromBaseClass("CloseProxyBecauseOfException");
			iLGenerator.Emit(OpCodes.Call, closeProxyMethod);
			// throw;
			iLGenerator.Emit(OpCodes.Rethrow);
		}
	}
}
