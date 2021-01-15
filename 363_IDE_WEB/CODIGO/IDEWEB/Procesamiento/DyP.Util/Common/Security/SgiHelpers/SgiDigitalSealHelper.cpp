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
                            SgiDigitalSealHelper::SgiDigitalSealHelper():_pSgiSelloDigital(NULL), _sgiDigitalSealInitialized(false), _profilingHelper(gcnew ProfilingHelper("SgiSelloDigital"))
                            {
                                String^ hostName = ConfigurationManager::ApplicationSettings->ReadSetting("SAT.DyP.Util.Security.Interop.SgiHelpers::DigitalSealHostName");
                                if (String::IsNullOrEmpty(hostName))
                                    throw gcnew PlatformException("Configuration value retrieval for Digital Seal Service's hostname failed invoking SgiDigitalSealHelper::SgiDigitalSealHelper().\n", ERROR_INVALID_PARAMETER);

                                String^ serviceName = ConfigurationManager::ApplicationSettings->ReadSetting("SAT.DyP.Util.Security.Interop.SgiHelpers::DigitalSealServiceName", gcnew String(SgiDigitalSealHelper::DefaultServiceName));

                                this->_hostName    = hostName;
                                this->_serviceName = serviceName;

                                InternalInitialize(this->_hostName, this->_serviceName);
                            }

                            SgiDigitalSealHelper::SgiDigitalSealHelper(String^ hostName, String^ serviceName):_pSgiSelloDigital(NULL), _sgiDigitalSealInitialized(false), _profilingHelper(gcnew ProfilingHelper("SgiSelloDigital"))
                            {
                                if (String::IsNullOrEmpty(hostName))
                                    throw gcnew PlatformException("Invalid configuration value for Digital Seal Service's hostname invoking SgiDigitalSealHelper::SgiDigitalSealHelper().\n", ERROR_INVALID_PARAMETER);

                                if (String::IsNullOrEmpty(serviceName))
                                    throw gcnew PlatformException("Invalid configuration value for Digital Seal Service's service name invoking SgiDigitalSealHelper::SgiDigitalSealHelper().\n", ERROR_INVALID_PARAMETER);

                                this->_hostName    = hostName;
                                this->_serviceName = serviceName;

                                InternalInitialize(this->_hostName, this->_serviceName);
                            }

                            SgiDigitalSealHelper::~SgiDigitalSealHelper()
                            {
                                InternalDispose();
                            }

                            void SgiDigitalSealHelper::!SgiDigitalSealHelper()
                            {
                                InternalDispose();
                            }

                            // /*virtual*/ bool SgiDigitalSealHelper::CanBePooled() /*override*/
                            // {
                            //     return this->_sgiDigitalSealInitialized;
                            // }

                            String^ SgiDigitalSealHelper::GetDigitalSealSerialNumber()
                            {
                                int result = 0;
                                std::string serial;
                                String^ serialNumber = String::Empty;

                                try
                                {
                                    this->_profilingHelper->Start();
                                    result = SgiSelloDigital::GetNSerie(this->_pSgiSelloDigital, &serial);
                                    this->_profilingHelper->Stop(ScadePerfCounter::Security_GetNSerie);

                                    if (result != 0)
                                        throw gcnew PlatformException(String::Format("System error {0} (0x{1:X}) invoking SgiSelloDigital::GetNSerie().\n", result, result), result);

                                    serialNumber = gcnew String(serial.c_str());
                                }
                                finally
                                {
                                    if (SGISELLODIGITAL_FAILED(result))
                                        InternalDispose();
                                }

                                return serialNumber;
                            }

                            String^ SgiDigitalSealHelper::GenerateDigitalSeal(String^ inputData)
                            {
                                std::string input = MarshalString(inputData);
                                std::string output;
                                int result = 0;
                                String^ outputData = String::Empty;

                                try
                                {
                                    this->_profilingHelper->Start();
                                    result = SgiSelloDigital::GetFirma(this->_pSgiSelloDigital, input.c_str(), &output);
                                    this->_profilingHelper->Stop(ScadePerfCounter::Security_GetFirma);

                                    if (result != 0)
                                        throw gcnew PlatformException(String::Format("System error {0} (0x{1:X}) invoking SgiSelloDigital::GetFirma().\n", result, result), result);

                                    outputData = gcnew String(output.c_str());
                                }
                                finally
                                {
                                    if (SGISELLODIGITAL_FAILED(result))
                                        InternalDispose();
                                }

                                return outputData;
                            }

                            void SgiDigitalSealHelper::InternalInitialize(String^ hostName, String^ serviceName)
                            {
                                if (!this->_sgiDigitalSealInitialized)
                                {
                                    if (this->_pSgiSelloDigital == NULL)
                                        this->_pSgiSelloDigital = SgiSelloDigital::new_SelloDigital();

                                    std::string host = MarshalString(hostName);
                                    std::string service;
                                    const char* pService = SgiDigitalSealHelper::DefaultServiceName;
                                    if (!String::IsNullOrEmpty(serviceName))
                                    {
                                        service = MarshalString(serviceName);
                                        pService = service.c_str();
                                    }

                                    this->_profilingHelper->Start();
                                    int result = SgiSelloDigital::Inicia(this->_pSgiSelloDigital, host.c_str(), pService);
                                    this->_profilingHelper->Stop(ScadePerfCounter::Security_Inicia);

                                    if (result != 0)
                                        throw gcnew PlatformException(String::Format("System error {0} (0x{1:X}) invoking SgiSelloDigital::Inicia().\n", result, result), result);

                                    this->_sgiDigitalSealInitialized = true;
                                }
                            }

                            void SgiDigitalSealHelper::InternalDispose()
                            {
                                if (this->_pSgiSelloDigital != NULL)
                                {
                                    this->_profilingHelper->Start();
                                    SgiSelloDigital::Termina(this->_pSgiSelloDigital);
                                    this->_profilingHelper->Stop(ScadePerfCounter::Security_Termina);

                                    SgiSelloDigital::delete_SelloDigital(this->_pSgiSelloDigital);
                                    this->_pSgiSelloDigital = NULL;
                                }

                                this->_sgiDigitalSealInitialized = false;
                            }

                            std::string& SgiDigitalSealHelper::MarshalString(String^ someString)
                            {
                                const char* pszString = reinterpret_cast<const char*>(Marshal::StringToHGlobalAnsi(someString).ToPointer());
                                std::string* pString = new std::string(pszString);

                                Marshal::FreeHGlobal(IntPtr((void*)pszString));
                                return *pString;
                            }
                        }   // namespace SgiHelpers
                    }       // namespace Interop
                }           // namespace Security
            }               // namespace Util
    }                       // namespace DyP
}                           // namespace SAT
