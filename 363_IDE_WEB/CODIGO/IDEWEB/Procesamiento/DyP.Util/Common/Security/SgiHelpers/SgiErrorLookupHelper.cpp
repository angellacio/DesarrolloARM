//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Util.Security.Interop:SgiCertificateHelper:0:21/May/2008[ SAT.DyP.Util.Security.Interop.SgiHelpers:1.0:21/May/2008])
#include "StdAfx.h"

namespace SAT
{
    namespace DyP
    {
            namespace Util
            {
                namespace Security
                {
                    namespace Interop
                    {
                        namespace SgiHelpers
                        {
                            /*static*/ String^ SgiErrorLookupHelper::GetErrorDescription(SgiCripto::tError errorCode)
                            {
                                return InternalGetErrorDescription(errorCode);
                            }

                            /*static*/ String^ SgiErrorLookupHelper::FormatErrorMessage(String^ context, SgiCripto::tError errorCode)
                            {
                                String^ errorMessage = String::Empty;

                                try
                                {
                                    String^ description = InternalGetErrorDescription(errorCode);
                                    int error = (int)errorCode;

                                    StringBuilder^ fullMessage = gcnew StringBuilder();
                                    fullMessage->AppendFormat("Error {0} (0x{1:X}) has occurred", error, error);

                                    if (!String::IsNullOrEmpty(context))
                                        fullMessage->AppendFormat(" invoking {0}", context);

                                    if (!String::IsNullOrEmpty(description))
                                        fullMessage->AppendFormat(": {0}.", description);
                                    else
                                        fullMessage->Append(".");

                                    errorMessage = fullMessage->ToString();
                                }
                                catch (Exception^ /*ex*/)
                                {
                                }

                                return errorMessage;
                            }

                            /*static*/ String^ SgiErrorLookupHelper::InternalGetErrorDescription(SgiCripto::tError errorCode)
                            {
                                char* pErrorDescription = NULL;
                                String^ description = String::Empty;

                                try
                                {
                                    pErrorDescription = const_cast<char*>(SgiCripto::msgError(errorCode));
                                    if (pErrorDescription != NULL)
                                        description = gcnew String(pErrorDescription);
                                }
                                finally
                                {
                                }

                                return description;
                            }
                        }   // namespace SgiHelpers
                    }       // namespace Interop
                }           // namespace Security
            }               // namespace Util
    }                       // namespace DyP
}                           // namespace SAT
