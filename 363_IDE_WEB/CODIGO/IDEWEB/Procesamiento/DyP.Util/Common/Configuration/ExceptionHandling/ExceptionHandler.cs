//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Util:ExceptionHandling:0:21/May/2008[SAT.DyP.Util:1.0:21/May/2008])
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using SAT.DyP.Util.Logging;

namespace SAT.DyP.Util.ExceptionHandling
{
    public static class ExceptionHandler
    {
        private static ExceptionPolicyImpl _exceptionPolicy;

        public static bool HandleException( System.Exception ex)
        {
            bool result=false;
            try
            {
                if (_exceptionPolicy == null)
                {
                    ExceptionPolicyFactory factory = new ExceptionPolicyFactory(DyP.Util.Configuration.ConfigurationManager.EnterpriseLibraryConfigurationSource);
                    _exceptionPolicy = factory.Create("Default Exception Policy");
                }

                result=_exceptionPolicy.HandleException(ex);
            }
            catch(Exception ext)
            {
                EventLogHelper.WriteInformationEntry(ext.Message,CoreLogEventIdentifier.SECURITY_EVENT_ID);
            }

            return result;
        }
    }
}
