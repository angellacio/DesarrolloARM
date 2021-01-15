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
                            SgiCertificateHelper::SgiCertificateHelper(array<Byte>^ certificate):_pSgiCert(NULL), _initialized(false), _serialNumber(String::Empty), _taxPayerID(String::Empty), _taxPayerName(String::Empty), _validityStartDate(DateTime::MinValue), _validityEndDate(DateTime::MinValue), _profilingHelper(gcnew ProfilingHelper("SgiCertificado"))
                            {
                                InternalInitialize(certificate);
                            }

                            SgiCertificateHelper::~SgiCertificateHelper()
                            {
                                InternalDispose();
                            }

                            SgiCertificateHelper::!SgiCertificateHelper()
                            {
                                InternalDispose();
                            }

                            void SgiCertificateHelper::InternalInitialize(array<Byte>^ certificate)
                            {
                                unsigned char* cert = NULL;
                                SgiCripto::tError result;

                                try
                                {
                                    if (this->_pSgiCert == NULL)
                                        this->_pSgiCert = SgiCripto::new_SgiCertificado();

                                    int certLen = certificate->Length;
                                    cert = new unsigned char[certLen];
                                    if (cert == NULL)
                                        throw gcnew PlatformException("Memory allocation failed invoking SgiCertificateHelper::InternalInitialize().\n", ERROR_OUTOFMEMORY);

                                    for (int i = 0; i < certLen; i++)
                                        cert[i] = certificate[i];

                                    this->_profilingHelper->Start();
                                    result = SgiCripto::inicializa(this->_pSgiCert, cert, certLen);
                                    this->_profilingHelper->Stop(ScadePerfCounter::Security_inicializa);

                                    if (SGICERTIFICADO_BUSINESSERROR(result))
                                        throw gcnew BusinessException(SgiErrorLookupHelper::FormatErrorMessage("SgiCripto::inicializa()", result), (int)result);

                                    if (SGICERTIFICADO_SYSTEMERROR(result))
                                        throw gcnew PlatformException(SgiErrorLookupHelper::FormatErrorMessage("SgiCripto::inicializa()", result), (int)result);

                                    if (SGICERTIFICADO_SUCCEEDED(result))
                                        this->_initialized = true;
                                }
                                finally
                                {
                                    if (cert != NULL)
                                        delete[] cert;
                                }
                            }

                            void SgiCertificateHelper::InternalDispose()
                            {
                                if (this->_pSgiCert != NULL)
                                {
                                    SgiCripto::delete_SgiCertificado(this->_pSgiCert);
                                    this->_pSgiCert = NULL;
                                }
                            }

                            String^ SgiCertificateHelper::InternalGetCertificateSerialNumber()
                            {
                                std::string serialNumber;
                                SgiCripto::tError result;

                                this->_profilingHelper->Start();
                                result = SgiCripto::getNSerie(this->_pSgiCert, &serialNumber);
                                this->_profilingHelper->Stop(ScadePerfCounter::Security_getNSerie);

                                if (SGICERTIFICADO_BUSINESSERROR(result))
                                    throw gcnew BusinessException(SgiErrorLookupHelper::FormatErrorMessage("SgiCripto::getNSerie()", result), (int)result);

                                if (SGICERTIFICADO_SYSTEMERROR(result))
                                    throw gcnew PlatformException(SgiErrorLookupHelper::FormatErrorMessage("SgiCripto::getNSerie()", result), (int)result);

                                return gcnew String(serialNumber.c_str());
                            }

                            String^ SgiCertificateHelper::InternalGetCertificateTaxPayerID()
                            {
                                std::string payerID;
                                SgiCripto::tError result;

                                this->_profilingHelper->Start();
                                result = SgiCripto::getRFC(this->_pSgiCert, &payerID);
                                this->_profilingHelper->Stop(ScadePerfCounter::Security_getRFC);

                                if (SGICERTIFICADO_BUSINESSERROR(result))
                                    throw gcnew BusinessException(SgiErrorLookupHelper::FormatErrorMessage("SgiCripto::getRFC()", result), (int)result);

                                if (SGICERTIFICADO_SYSTEMERROR(result))
                                    throw gcnew PlatformException(SgiErrorLookupHelper::FormatErrorMessage("SgiCripto::getRFC()", result), (int)result);

                                return gcnew String(payerID.c_str());
                            }

                            String^ SgiCertificateHelper::InternalGetCertificateTaxPayerName()
                            {
                                std::string payerName;
                                SgiCripto::tError result;

                                this->_profilingHelper->Start();
                                result = SgiCripto::getNom(this->_pSgiCert, &payerName);
                                this->_profilingHelper->Stop(ScadePerfCounter::Security_getNom);

                                if (SGICERTIFICADO_BUSINESSERROR(result))
                                    throw gcnew BusinessException(SgiErrorLookupHelper::FormatErrorMessage("SgiCripto::getNom()", result), (int)result);

                                if (SGICERTIFICADO_SYSTEMERROR(result))
                                    throw gcnew PlatformException(SgiErrorLookupHelper::FormatErrorMessage("SgiCripto::getNom()", result), (int)result);

                                return gcnew String(payerName.c_str());
                            }

                            DateTime SgiCertificateHelper::InternalGetValidityStartDate()
                            {
                                std::string startDate;
                                SgiCripto::tError result;
                                DateTime dt;

                                this->_profilingHelper->Start();
                                result = SgiCripto::getVigIni(this->_pSgiCert, &startDate);
                                this->_profilingHelper->Stop(ScadePerfCounter::Security_getVigIni);

                                if (SGICERTIFICADO_BUSINESSERROR(result))
                                    throw gcnew BusinessException(SgiErrorLookupHelper::FormatErrorMessage("SgiCripto::getVigIni()", result), (int)result);

                                if (SGICERTIFICADO_SYSTEMERROR(result))
                                    throw gcnew PlatformException(SgiErrorLookupHelper::FormatErrorMessage("SgiCripto::getVigIni()", result), (int)result);

                                String^ dateAsString = gcnew String(startDate.c_str());
                                if (!DateTime::TryParseExact(dateAsString, "yyMMddHHmmss", nullptr, DateTimeStyles::AssumeLocal, dt))
                                    throw gcnew InvalidOperationException("An invalid timestamp string was returned from SgiCripto::getVigIni().\n");

                                return dt;
                            }

                            DateTime SgiCertificateHelper::InternalGetValidityEndDate()
                            {
                                std::string endDate;
                                SgiCripto::tError result;
                                DateTime dt;

                                this->_profilingHelper->Start();
                                result = SgiCripto::getVigFin(this->_pSgiCert, &endDate);
                                this->_profilingHelper->Stop(ScadePerfCounter::Security_getVigFin);

                                if (SGICERTIFICADO_BUSINESSERROR(result))
                                    throw gcnew BusinessException(SgiErrorLookupHelper::FormatErrorMessage("SgiCripto::getVigFin()", result), (int)result);

                                if (SGICERTIFICADO_SYSTEMERROR(result))
                                    throw gcnew PlatformException(SgiErrorLookupHelper::FormatErrorMessage("SgiCripto::getVigFin()", result), (int)result);

                                String^ dateAsString = gcnew String(endDate.c_str());
                                if (!DateTime::TryParseExact(dateAsString, "yyMMddHHmmss", nullptr, DateTimeStyles::AssumeLocal, dt))
                                    throw gcnew InvalidOperationException("An invalid timestamp string was returned from SgiCripto::getVigFin().\n");

                                return dt;
                            }
                        }   // namespace SgiHelpers
                    }       // namespace Interop
                }           // namespace Security
            }               // namespace Util
    }                       // namespace DyP
}                           // namespace SAT
