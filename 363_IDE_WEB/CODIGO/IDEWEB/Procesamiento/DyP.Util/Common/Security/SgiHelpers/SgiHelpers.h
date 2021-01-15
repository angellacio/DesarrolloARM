// SgiHelpers.h
//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Util.Security.Interop:SgiCertificateHelper:0:21/May/2008[ SAT.DyP.Util.Security.Interop.SgiHelpers:1.0:21/May/2008])
#pragma once

using namespace System;
using namespace System::Collections::Generic;
using namespace System::Diagnostics;
using namespace System::EnterpriseServices;
using namespace System::Globalization;
using namespace System::Runtime::InteropServices;
using namespace System::Text;
using namespace System::Threading;

#pragma warning(disable: 4945)
using namespace SAT::DyP::Util;
using namespace SAT::DyP::Util::Configuration;
using namespace SAT::DyP::Util::Monitoring;
using namespace SAT::DyP::Util::Security;
using namespace SAT::DyP::Util::Types;
#pragma warning(default: 4945)

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
                            public ref class SgiCertificateHelper
                            {
                            public:
                                SgiCertificateHelper(array<Byte>^ certificate);
                                ~SgiCertificateHelper();
                                !SgiCertificateHelper();

                                property String^ CertificateSerialNumber {
                                    String^ get() {
                                        if (String::IsNullOrEmpty(this->_serialNumber))
                                            this->_serialNumber = InternalGetCertificateSerialNumber();

                                        return this->_serialNumber;
                                    }
                                }

                                property String^ CertificateTaxPayerID {
                                    String^ get() {
                                        if (String::IsNullOrEmpty(this->_taxPayerID))
                                            this->_taxPayerID = InternalGetCertificateTaxPayerID();

                                        return this->_taxPayerID;
                                    }
                                }

                                property String^ CertificateTaxPayerName {
                                    String^ get() {
                                        if (String::IsNullOrEmpty(this->_taxPayerName))
                                            this->_taxPayerName = InternalGetCertificateTaxPayerName();

                                        return this->_taxPayerName;
                                    }
                                }

                                property DateTime ValidityStartDate {
                                    DateTime get() {
                                        if (this->_validityStartDate == DateTime::MinValue)
                                            this->_validityStartDate = InternalGetValidityStartDate();

                                        return this->_validityStartDate;
                                    }
                                }

                                property DateTime ValidityEndDate {
                                    DateTime get() {
                                        if (this->_validityEndDate == DateTime::MinValue)
                                            this->_validityEndDate = InternalGetValidityEndDate();

                                        return this->_validityEndDate;
                                    }
                                }

                            internal:
                                property SgiCripto::PtrSgiCert Instance {
                                    SgiCripto::PtrSgiCert get() {
                                        return this->_pSgiCert;
                                    }
                                }

                            private:
                                SgiCripto::PtrSgiCert _pSgiCert;
                                Boolean _initialized;
                                String^ _serialNumber;
                                String^ _taxPayerID;
                                String^ _taxPayerName;
                                DateTime _validityStartDate;
                                DateTime _validityEndDate;
                                ProfilingHelper^ _profilingHelper;

                            private:
                                bool SGICERTIFICADO_SUCCEEDED(int result) { return (result >= 0); }
                                bool SGICERTIFICADO_FAILED(int result) { return (result < 0); }

                                bool SGICERTIFICADO_SUCCEEDED(SgiCripto::tError result) { return (result == SgiCripto::Error_OK); }
                                bool SGICERTIFICADO_BUSINESSERROR(SgiCripto::tError result) { return ((int)result > 0); }
                                bool SGICERTIFICADO_SYSTEMERROR(SgiCripto::tError result) { return ((int)result < 0); }

                                void InternalInitialize(array<Byte>^ certificate);
                                void InternalDispose();
                                String^ InternalGetCertificateSerialNumber();
                                String^ InternalGetCertificateTaxPayerID();
                                String^ InternalGetCertificateTaxPayerName();
                                DateTime InternalGetValidityStartDate();
                                DateTime InternalGetValidityEndDate();
                            };

                            public ref class SgiCryptoHelper /*: public ServicedComponent*/
                            {
                            private:
                                static const int CertificateBufferLength = 4096;

                            public:
                                SgiCryptoHelper();
                                SgiCryptoHelper(Boolean connect, String^ configPath);
                                ~SgiCryptoHelper();
                                !SgiCryptoHelper();

                                /* virtual bool CanBePooled() override; */

                                array<Byte>^ GetCertificateBySerialNumber(String^ certificateSerialNumber, CertificateInfo% certificateInfo);
                                CertificateInfo^ GetCertificateInfoBySerialNumber(String^ certificateSerialNumber);
                                String^ GetCertificateSerialNumberByTaxPayerID(String^ taxPayerID);
                                List<String^>^ GetCertificateSerialNumbersByTaxPayerID(String^ taxPayerID, CertificateType certificateType, CertificateState certificateState, UInt16 serialNumberCount);

                                String^ GenerateSignature(String^ inputData, String^ privateKeyPath, DigestAlgorithm digestAlgorithm, String^ password);
                                Boolean IsSignatureValid(SgiCertificateHelper^ certificateHelper, String^ inputData, String^ signature);
                                Boolean IsPrivateKeyValid(String^ certificatePath, String^ privateKeyPath, String^ password);

                                array<Byte>^ Encrypt(array<Byte>^ inputData, EncryptionAlgorithm encryptionAlgorithm, String^ password);
                                array<Byte>^ Encrypt(String^ inputData, EncryptionAlgorithm encryptionAlgorithm, String^ password);
                                array<Byte>^ Decrypt(array<Byte>^ inputData, EncryptionAlgorithm encryptionAlgorithm, String^ password);
                                String^ Decrypt(String^ inputData, EncryptionAlgorithm encryptionAlgorithm, String^ password);

                                void CreatePkcs7File(String^ senderCertificatePath, String^ privateKeyPath, String^ password, String^ sourceFilePath, Pkcs7Type pkcs7type, String^ targetFilePath, EncryptionAlgorithm encryptionAlgorithm, String^ receiverCertificatePath);
                                List<String^>^ GetCertificateSerialNumbersFromPkcs7File(String^ pkcs7FilePath);
                                [Obsolete("This method is deprecated. Use the SgiCryptoHelper.ProcessPkcs7FileInfo() instead.")]
                                void ProcessPkcs7File(String^ receiverCertificatePath, String^ receiverPrivateKeyPath, String^ password, String^ pkcs7FilePath, String^ targetFilePath, String^ caCertificatePath, CertificateType certificateType);
                                array<Byte>^ ProcessPkcs7FileInfo(String^ receiverCertificatePath, String^ receiverPrivateKeyPath, String^ password, array<Byte>^ pkcs7FileInfo, String^ caCertificatePath, CertificateType certificateType);

                            private:
                                SgiCripto::PtrSgiCripto _pSgiCripto;
                                Boolean _initialized;
                                Boolean _connect;
                                String^ _configPath;
                                ProfilingHelper^ _profilingHelper;

                            private:
                                bool SGICRIPTO_SUCCEEDED(int result) { return (result >= 0); }
                                bool SGICRIPTO_FAILED(int result) { return (result < 0); }

                                bool SGICRIPTO_SUCCEEDED(SgiCripto::tError result) { return (result == SgiCripto::Error_OK); }
                                bool SGICRIPTO_BUSINESSERROR(SgiCripto::tError result) { return ((int)result > 0); }
                                bool SGICRIPTO_SYSTEMERROR(SgiCripto::tError result) { return ((int)result < 0); }

                                void InternalInitialize(Boolean connect, String^ configPath);
                                void InternalDispose();
                                std::string& MarshalString(String^ someString);
                            };

                            public ref class SgiDigitalSealHelper /*: public ServicedComponent*/
                            {
                            public:
                                static const char* DefaultServiceName = "SgiSelloDigital";

                            public:
                                SgiDigitalSealHelper();
                                SgiDigitalSealHelper(String^ hostName, String^ serviceName);
                                ~SgiDigitalSealHelper();
                                !SgiDigitalSealHelper();

                                /* virtual bool CanBePooled() override; */

                                String^ GetDigitalSealSerialNumber();
                                String^ GenerateDigitalSeal(String^ inputData);

                            private:
                                SgiSelloDigital::PtrSelloDigital _pSgiSelloDigital;
                                Boolean _sgiDigitalSealInitialized;
                                String^ _hostName;
                                String^ _serviceName;
                                ProfilingHelper^ _profilingHelper;

                            private:
                                bool SGISELLODIGITAL_SUCCEEDED(int result) { return (result == 0); }
                                bool SGISELLODIGITAL_FAILED(int result) { return (result != 0); }

                                void InternalInitialize(String^ hostName, String^ serviceName);
                                void InternalDispose();
                                std::string& MarshalString(String^ someString);
                            };

                            private ref class SgiErrorLookupHelper
                            {
                            public:
                                static String^ GetErrorDescription(SgiCripto::tError errorCode);
                                static String^ FormatErrorMessage(String^ context, SgiCripto::tError errorCode);

                            private:
                                static String^ InternalGetErrorDescription(SgiCripto::tError errorCode);
                            };
                        }
                    }
                }
        }
    }
}
