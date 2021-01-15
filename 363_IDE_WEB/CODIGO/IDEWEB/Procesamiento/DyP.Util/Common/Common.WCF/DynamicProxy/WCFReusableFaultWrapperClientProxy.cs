//@(#)SCADE2(W:SKD08212CO2:SAT.DyP.Util.Service.WCF:WCFReusableFaultWrapperClientProxy:0:21/May/2008[SAT.DyP.Util.Service.WCF:1.0:21/May/2008])
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
	public abstract class WCFReusableFaultWrapperClientProxy<T> : WCFReusableClientProxy<T> where T : class
	{
		protected WCFReusableFaultWrapperClientProxy(string configName)
			: base ( configName )
		{
		}

		protected virtual void HandleFault<TFaultDetail>(FaultException<TFaultDetail> fault)
		{
			Exception ex = fault.Detail as Exception;
			if (ex != null)
			{
				throw ex;
			}
		}
	}
}
