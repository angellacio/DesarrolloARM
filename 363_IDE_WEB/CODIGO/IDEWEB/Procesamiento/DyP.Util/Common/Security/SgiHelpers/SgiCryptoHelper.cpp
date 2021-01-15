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
                            SgiCryptoHelper::SgiCryptoHelper():_pSgiCripto(NULL), _initialized(false), _profilingHelper(gcnew ProfilingHelper("SgiCripto"))
                            {
                                String^ connectToPKI  = ConfigurationManager::ApplicationSettings->ReadSetting("SAT.DyP.Util.Security.Interop.SgiHelpers::ConnectToARA");
                                String^ pkiConfigPath = ConfigurationManager::ApplicationSettings->ReadSetting("SAT.DyP.Util.Security.Interop.SgiHelpers::ARAConfigurationPath");

                                this->_connect    = (!String::IsNullOrEmpty(connectToPKI) && ((gcnew String("yes"))->Equals(connectToPKI->ToLower()) || (gcnew String("true"))->Equals(connectToPKI->ToLower())));
                                this->_configPath = pkiConfigPath;

                                InternalInitialize(this->_connect, this->_configPath);
                            }

                            SgiCryptoHelper::SgiCryptoHelper(Boolean connect, String^ configPath):_pSgiCripto(NULL), _initialized(false), _profilingHelper(gcnew ProfilingHelper("SgiCripto"))
                            {
                                this->_connect    = connect;
                                this->_configPath = configPath;

                                InternalInitialize(this->_connect, this->_configPath);
                            }

                            SgiCryptoHelper::~SgiCryptoHelper()
                            {
                                InternalDispose();
                            }

                            SgiCryptoHelper::!SgiCryptoHelper()
                            {
                                InternalDispose();
                            }

                            // /*virtual*/ bool SgiCryptoHelper::CanBePooled() /*override*/
                            // {
                            //     return this->_initialized;
                            // }

                            array<Byte>^ SgiCryptoHelper::GetCertificateBySerialNumber(String^ certificateSerialNumber, CertificateInfo% certificateInfo)
                            {
                                array<Byte>^ certificate = nullptr;

                                std::string serialNumber = MarshalString(certificateSerialNumber);
                                SgiCripto::tCertTipo type;
                                SgiCripto::tCertEdo state;
                                std::string validityStart, validityEnd;
                                unsigned char* cert = NULL;
                                SgiCripto::tError result = SgiCripto::Error_OK;

                                try
                                {
                                    cert = new unsigned char[CertificateBufferLength];
                                    if (cert == NULL)
                                        throw gcnew PlatformException("Memory allocation failed invoking SgiCryptoHelper::GetCertificateBySerialNumber().\n", ERROR_OUTOFMEMORY);

                                    int certLen = CertificateBufferLength;

                                    this->_profilingHelper->Start();
                                    result = SgiCripto::solCDxNS(this->_pSgiCripto, serialNumber.c_str(), &state, &type, &validityStart, &validityEnd, cert, &certLen);
                                    this->_profilingHelper->Stop(ScadePerfCounter::Security_solCDxNS);

                                    if (SGICRIPTO_BUSINESSERROR(result))
                                        throw gcnew BusinessException(SgiErrorLookupHelper::FormatErrorMessage("SgiCripto::solCDxNS()", result), (int)result);

                                    if (SGICRIPTO_SYSTEMERROR(result))
                                        throw gcnew PlatformException(SgiErrorLookupHelper::FormatErrorMessage("SgiCripto::solCDxNS()", result), (int)result);

                                    String^ startDateAsString = gcnew String(validityStart.c_str());
                                    String^ endDateAsString   = gcnew String(validityEnd.c_str());

                                    DateTime startDate, endDate;
                                    if (!DateTime::TryParseExact(startDateAsString, "yyMMddHHmmssK", nullptr, DateTimeStyles::AssumeUniversal, startDate))
                                        throw gcnew InvalidOperationException("An invalid timestamp string was returned from SgiCripto::solCDxNS().\n");

                                    if (!DateTime::TryParseExact(endDateAsString, "yyMMddHHmmssK", nullptr, DateTimeStyles::AssumeUniversal, endDate))
                                        throw gcnew InvalidOperationException("An invalid timestamp string was returned from SgiCripto::solCDxNS().\n");

                                    certificateInfo.Type          = (CertificateType)type;
                                    certificateInfo.State         = (CertificateState)state;
                                    certificateInfo.SerialNumber  = certificateSerialNumber;
                                    certificateInfo.ValidityStart = startDate;
                                    certificateInfo.ValidityEnd   = endDate;

                                    certificate = gcnew array<Byte>(certLen);
                                    for (int i = 0; i < certLen; i++)
                                        certificate[i] = cert[i];
                                }
                                finally
                                {
                                    if (SGICRIPTO_SYSTEMERROR(result))
                                        InternalDispose();

                                    if (cert != NULL)
                                        delete[] cert;
                                }

                                return certificate;
                            }

                            CertificateInfo^ SgiCryptoHelper::GetCertificateInfoBySerialNumber(String^ certificateSerialNumber)
                            {
                                std::string serialNumber = MarshalString(certificateSerialNumber);
                                SgiCripto::tCertTipo type;
                                SgiCripto::tCertEdo state;
                                std::string validityStart, validityEnd;
                                CertificateInfo^ certificateInfo = nullptr;
                                SgiCripto::tError result = SgiCripto::Error_OK;

                                try
                                {
                                    this->_profilingHelper->Start();
                                    result = SgiCripto::solEdoCDxNS(this->_pSgiCripto, serialNumber.c_str(), &state, &type, &validityStart, &validityEnd);
                                    this->_profilingHelper->Stop(ScadePerfCounter::Security_solEdoCDxNS);

                                    if (SGICRIPTO_BUSINESSERROR(result))
                                        throw gcnew BusinessException(SgiErrorLookupHelper::FormatErrorMessage("SgiCripto::solEdoCDxNS()", result), (int)result);

                                    if (SGICRIPTO_SYSTEMERROR(result))
                                        throw gcnew PlatformException(SgiErrorLookupHelper::FormatErrorMessage("SgiCripto::solEdoCDxNS()", result), (int)result);

                                    String^ startDateAsString = gcnew String(validityStart.c_str());
                                    String^ endDateAsString   = gcnew String(validityEnd.c_str());

                                    DateTime startDate, endDate;
                                    if (!DateTime::TryParseExact(startDateAsString, "yyMMddHHmmssK", nullptr, DateTimeStyles::AssumeUniversal, startDate))
                                        throw gcnew InvalidOperationException("An invalid timestamp string was returned from SgiCripto::solEdoCDxNS().\n");

                                    if (!DateTime::TryParseExact(endDateAsString, "yyMMddHHmmssK", nullptr, DateTimeStyles::AssumeUniversal, endDate))
                                        throw gcnew InvalidOperationException("An invalid timestamp string was returned from SgiCripto::solEdoCDxNS().\n");

                                    certificateInfo = gcnew CertificateInfo();
                                    certificateInfo->Type          = (CertificateType)type;
                                    certificateInfo->State         = (CertificateState)state;
                                    certificateInfo->SerialNumber  = certificateSerialNumber;
                                    certificateInfo->ValidityStart = startDate;
                                    certificateInfo->ValidityEnd   = endDate;
                                }
                                finally
                                {
                                    if (SGICRIPTO_SYSTEMERROR(result))
                                        InternalDispose();
                                }

                                return certificateInfo;
                            }

                            String^ SgiCryptoHelper::GetCertificateSerialNumberByTaxPayerID(String^ taxPayerID)
                            {
                                // El servicio de ARA es case-sensitive, existe el requerimiento de que sea case insensitive todo upercase.
                                std::string payerID = MarshalString(taxPayerID->ToUpper());
                                SgiCripto::tError result = SgiCripto::Error_OK;
                                std::string serialNumber;
                                String^ certificateSerialNumber = String::Empty;

                                try
                                {
                                    this->_profilingHelper->Start();
                                    result = SgiCripto::solNSCDFEAxRFC(this->_pSgiCripto, payerID.c_str(), &serialNumber);
                                    this->_profilingHelper->Stop(ScadePerfCounter::Security_solNSCDFEAxRFC);

                                    if (SGICRIPTO_BUSINESSERROR(result))
                                        throw gcnew BusinessException(SgiErrorLookupHelper::FormatErrorMessage("SgiCripto::solNSCDFEAxRFC()", result), (int)result);

                                    if (SGICRIPTO_SYSTEMERROR(result))
                                        throw gcnew PlatformException(SgiErrorLookupHelper::FormatErrorMessage("SgiCripto::solNSCDFEAxRFC()", result), (int)result);

                                    certificateSerialNumber = gcnew String(serialNumber.c_str());
                                }
                                finally
                                {
                                    if (SGICRIPTO_SYSTEMERROR(result))
                                        InternalDispose();
                                }

                                return certificateSerialNumber;
                            }

                            List<String^>^ SgiCryptoHelper::GetCertificateSerialNumbersByTaxPayerID(String^ taxPayerID, CertificateType certificateType, CertificateState certificateState, UInt16 serialNumberCount)
                            {
                                // El servicio de ARA es case-sensitive, existe el requerimiento de que sea case insensitive todo upercase.
                                std::string payerID = MarshalString(taxPayerID->ToUpper());
                                SgiCripto::tCertTipo type = (SgiCripto::tCertTipo)certificateType;
                                SgiCripto::tCertEdo state = (SgiCripto::tCertEdo)certificateState;
                                unsigned short count = serialNumberCount;
                                std::string serialNums;
                                List<String^>^ certSerialNumbers = nullptr;
                                SgiCripto::tError result = SgiCripto::Error_OK;

                                try
                                {
                                    this->_profilingHelper->Start();
                                    result = SgiCripto::solListaCDxRFC(this->_pSgiCripto, payerID.c_str(), state, type, count, &serialNums);
                                    this->_profilingHelper->Stop(ScadePerfCounter::Security_solListaCDxRFC);

                                    if (SGICRIPTO_BUSINESSERROR(result))
                                        throw gcnew BusinessException(SgiErrorLookupHelper::FormatErrorMessage("SgiCripto::solListaCDxRFC()", result), (int)result);

                                    if (SGICRIPTO_SYSTEMERROR(result))
                                        throw gcnew PlatformException(SgiErrorLookupHelper::FormatErrorMessage("SgiCripto::solListaCDxRFC()", result), (int)result);

                                    certSerialNumbers = gcnew List<String^>();
                                    if (SGICRIPTO_SUCCEEDED(result))
                                    {
                                        String^ serialNumbers = gcnew String(serialNums.c_str());
                                        array<String^>^ sns = serialNumbers->Split(gcnew array<Char> {'|'});

                                        for each (String^ s in sns)
                                            certSerialNumbers->Add(s);
                                    }
                                }
                                finally
                                {
                                    if (SGICRIPTO_SYSTEMERROR(result))
                                        InternalDispose();
                                }

                                return certSerialNumbers;
                            }

                            String^ SgiCryptoHelper::GenerateSignature(String^ inputData, String^ privateKeyPath, DigestAlgorithm digestAlgorithm, String^ password)
                            {
                                SgiCripto::tAlgDig algorithm = (SgiCripto::tAlgDig)digestAlgorithm;
                                std::string input      = MarshalString(inputData);
                                std::string privateKey = MarshalString(privateKeyPath);
                                std::string pass       = MarshalString(password);
                                SgiCripto::tError result = SgiCripto::Error_OK;
                                std::string signature;
                                String^ signatureData = String::Empty;

                                try
                                {
                                    this->_profilingHelper->Start();
                                    result = SgiCripto::generaFirma(this->_pSgiCripto, input.c_str(), privateKey.c_str(), pass.c_str(), algorithm, &signature);
                                    this->_profilingHelper->Stop(ScadePerfCounter::Security_generaFirma);

                                    if (SGICRIPTO_BUSINESSERROR(result))
                                        throw gcnew BusinessException(SgiErrorLookupHelper::FormatErrorMessage("SgiCripto::generaFirma()", result), (int)result);

                                    if (SGICRIPTO_SYSTEMERROR(result))
                                        throw gcnew PlatformException(SgiErrorLookupHelper::FormatErrorMessage("SgiCripto::generaFirma()", result), (int)result);

                                    signatureData = gcnew String(signature.c_str());
                                }
                                finally
                                {
                                    if (SGICRIPTO_SYSTEMERROR(result))
                                        InternalDispose();
                                }

                                return signatureData;
                            }

                            Boolean SgiCryptoHelper::IsSignatureValid(SgiCertificateHelper^ certificateHelper, String^ inputData, String^ signature)
                            {
                                SgiCripto::PtrSgiCert pSgiCert = certificateHelper->Instance;
                                std::string input = MarshalString(inputData);
                                std::string sign  = MarshalString(signature);
                                SgiCripto::tError result = SgiCripto::Error_OK;

                                try
                                {
                                    this->_profilingHelper->Start();
                                    result = SgiCripto::verFirma(this->_pSgiCripto, pSgiCert, sign.c_str(), input.c_str());
                                    this->_profilingHelper->Stop(ScadePerfCounter::Security_verFirma);

                                    if (SGICRIPTO_BUSINESSERROR(result))
                                        throw gcnew BusinessException(SgiErrorLookupHelper::FormatErrorMessage("SgiCripto::verFirma()", result), (int)result);

                                    if (SGICRIPTO_SYSTEMERROR(result))
                                        throw gcnew PlatformException(SgiErrorLookupHelper::FormatErrorMessage("SgiCripto::verFirma()", result), (int)result);
                                }
                                finally
                                {
                                    if (SGICRIPTO_SYSTEMERROR(result))
                                        InternalDispose();
                                }

                                return SGICRIPTO_SUCCEEDED(result);
                            }

                            Boolean SgiCryptoHelper::IsPrivateKeyValid(String^ certificatePath, String^ privateKeyPath, String^ password)
                            {
                                std::string certPath = MarshalString(certificatePath);
                                std::string pkPath   = MarshalString(privateKeyPath);
                                std::string passwd   = MarshalString(password);
                                SgiCripto::tError result = SgiCripto::Error_OK;

                                try
                                {
                                    this->_profilingHelper->Start();
                                    result = SgiCripto::verificaLlaves(this->_pSgiCripto, certPath.c_str(), pkPath.c_str(), passwd.c_str());
                                    this->_profilingHelper->Stop(ScadePerfCounter::Security_verificaLlaves);

                                    if (SGICRIPTO_BUSINESSERROR(result))
                                        throw gcnew BusinessException(SgiErrorLookupHelper::FormatErrorMessage("SgiCripto::verificaLlaves()", result), (int)result);

                                    if (SGICRIPTO_SYSTEMERROR(result))
                                        throw gcnew PlatformException(SgiErrorLookupHelper::FormatErrorMessage("SgiCripto::verificaLlaves()", result), (int)result);
                                }
                                finally
                                {
                                    if (SGICRIPTO_SYSTEMERROR(result))
                                        InternalDispose();
                                }

                                return SGICRIPTO_SUCCEEDED(result);
                            }

                            array<Byte>^ SgiCryptoHelper::Encrypt(array<Byte>^ inputData, EncryptionAlgorithm encryptionAlgorithm, String^ password)
                            {
                                array<Byte>^ outputData = nullptr;
                                unsigned char* input  = NULL;
                                unsigned char* output = NULL;
                                SgiCripto::tError result = SgiCripto::Error_OK;

                                try
                                {
                                    int n = SgiCripto::tAlgEnc(Convert::ToInt32(encryptionAlgorithm));
                                    SgiCripto::tAlgEnc encAlgorithm = SgiCripto::AEnc_DES_CBC;
                                    encAlgorithm = SgiCripto::tAlgEnc(n);

                                    std::string pass = MarshalString(password);

                                    int inputLen = inputData->Length;
                                    input = new unsigned char[inputLen];
                                    if (input == NULL)
                                        throw gcnew PlatformException("Memory allocation for input data failed invoking SgiCryptoHelper::Encrypt().\n", ERROR_OUTOFMEMORY);

                                    for (int i = 0; i < inputLen; i++)
                                        input[i] = inputData[i];

                                    int outputLen = inputLen * 3;
                                    output = new unsigned char[outputLen];
                                    if (output == NULL)
                                        throw gcnew PlatformException("Memory allocation for output data failed invoking SgiCryptoHelper::Encrypt().\n", ERROR_OUTOFMEMORY);

                                    this->_profilingHelper->Start();
                                    result = SgiCripto::encripta(this->_pSgiCripto, input, (unsigned short)inputLen, encAlgorithm, (unsigned char*)pass.c_str(), password->Length, output, (unsigned short*)&outputLen);
                                    this->_profilingHelper->Stop(ScadePerfCounter::Security_encripta);

                                    if (SGICRIPTO_BUSINESSERROR(result))
                                        throw gcnew BusinessException(SgiErrorLookupHelper::FormatErrorMessage("SgiCripto::encripta()", result), (int)result);

                                    if (SGICRIPTO_SYSTEMERROR(result))
                                        throw gcnew PlatformException(SgiErrorLookupHelper::FormatErrorMessage("SgiCripto::encripta()", result), (int)result);

                                    outputData = gcnew array<Byte>(outputLen);
                                    for (int j = 0; j < outputLen; j++)
                                        outputData[j] = output[j];
                                }
                                finally
                                {
                                    if (SGICRIPTO_SYSTEMERROR(result))
                                        InternalDispose();

                                    if (input != NULL)
                                        delete[] input;

                                    if (output != NULL)
                                        delete[] output;
                                }

                                return outputData;
                            }

                            array<Byte>^ SgiCryptoHelper::Encrypt(String^ inputData, EncryptionAlgorithm encryptionAlgorithm, String^ password)
                            {
                                return Encrypt(Encoding::Unicode->GetBytes(inputData), encryptionAlgorithm, password);
                            }

                            array<Byte>^ SgiCryptoHelper::Decrypt(array<Byte>^ inputData, EncryptionAlgorithm encryptionAlgorithm, String^ password)
                            {
                                array<Byte>^ outputData = nullptr;
                                unsigned char* input  = NULL;
                                unsigned char* output = NULL;
                                SgiCripto::tError result = SgiCripto::Error_OK;

                                try
                                {
                                    SgiCripto::tAlgEnc algorithm = (SgiCripto::tAlgEnc)encryptionAlgorithm;
                                    std::string pass = MarshalString(password);

                                    int inputLen = inputData->Length;
                                    input = new unsigned char[inputLen];
                                    if (input == NULL)
                                        throw gcnew PlatformException("Memory allocation for input data failed invoking SgiCryptoHelper::Decrypt().\n", ERROR_OUTOFMEMORY);

                                    for (int i = 0; i < inputLen; i++)
                                        input[i] = inputData[i];

                                    int outputLen = inputLen * 3;
                                    output = new unsigned char[outputLen];
                                    if (output == NULL)
                                        throw gcnew PlatformException("Memory allocation for output data failed invoking SgiCryptoHelper::Decrypt().\n", ERROR_OUTOFMEMORY);

                                    this->_profilingHelper->Start();
                                    result = SgiCripto::desencripta(this->_pSgiCripto, input, (unsigned short)inputLen, algorithm, (unsigned char*)pass.c_str(), password->Length, output, (unsigned short*)&outputLen);
                                    this->_profilingHelper->Stop(ScadePerfCounter::Security_desencripta);

                                    if (SGICRIPTO_BUSINESSERROR(result))
                                        throw gcnew BusinessException(SgiErrorLookupHelper::FormatErrorMessage("SgiCripto::desencripta()", result), (int)result);

                                    if (SGICRIPTO_SYSTEMERROR(result))
                                        throw gcnew PlatformException(SgiErrorLookupHelper::FormatErrorMessage("SgiCripto::desencripta()", result), (int)result);

                                    outputData = gcnew array<Byte>(outputLen);
                                    for (int j = 0; j < outputLen; j++)
                                        outputData[j] = output[j];
                                }
                                finally
                                {
                                    if (SGICRIPTO_SYSTEMERROR(result))
                                        InternalDispose();

                                    if (input != NULL)
                                        delete[] input;

                                    if (output != NULL)
                                        delete[] output;
                                }

                                return outputData;
                            }

                            String^ SgiCryptoHelper::Decrypt(String^ inputData, EncryptionAlgorithm encryptionAlgorithm, String^ password)
                            {
                                return Encoding::Unicode->GetString(Decrypt(Encoding::Unicode->GetBytes(inputData), encryptionAlgorithm, password));
                            }

                            void SgiCryptoHelper::CreatePkcs7File(String^ senderCertificatePath, String^ privateKeyPath, String^ password, String^ sourceFilePath, Pkcs7Type pkcs7type, String^ targetFilePath, EncryptionAlgorithm encryptionAlgorithm, String^ receiverCertificatePath)
                            {
                                SgiCripto::tTipoPKCS7 type   = (SgiCripto::tTipoPKCS7)pkcs7type;
                                SgiCripto::tAlgEnc algorithm = (SgiCripto::tAlgEnc)encryptionAlgorithm;

                                std::string senderPath   = MarshalString(senderCertificatePath);
                                std::string pkPath       = MarshalString(privateKeyPath);
                                std::string pass         = MarshalString(password);
                                std::string sourcePath   = MarshalString(sourceFilePath);
                                std::string targetPath   = MarshalString(targetFilePath);
                                std::string receiverPath = MarshalString(receiverCertificatePath);
                                SgiCripto::tError result = SgiCripto::Error_OK;

                                try
                                {
                                    this->_profilingHelper->Start();
                                    result = SgiCripto::genP7(this->_pSgiCripto, senderPath.c_str(), pkPath.c_str(), pass.c_str(), sourcePath.c_str(), type, targetPath.c_str(), algorithm, receiverPath.c_str());
                                    this->_profilingHelper->Stop(ScadePerfCounter::Security_genP7);

                                    if (SGICRIPTO_BUSINESSERROR(result))
                                        throw gcnew BusinessException(SgiErrorLookupHelper::FormatErrorMessage("SgiCripto::genP7()", result), (int)result);

                                    if (SGICRIPTO_SYSTEMERROR(result))
                                        throw gcnew PlatformException(SgiErrorLookupHelper::FormatErrorMessage("SgiCripto::genP7()", result), (int)result);
                                }
                                finally
                                {
                                    if (SGICRIPTO_SYSTEMERROR(result))
                                        InternalDispose();
                                }
                            }

                            List<String^>^ SgiCryptoHelper::GetCertificateSerialNumbersFromPkcs7File(String^ pkcs7FilePath)
                            {
                                std::string pkcs7Path = MarshalString(pkcs7FilePath);
                                std::string serialNums;
                                List<String^>^ certSerialNumbers = nullptr;
                                SgiCripto::tError result = SgiCripto::Error_OK;

                                try
                                {
                                    this->_profilingHelper->Start();
                                    result = SgiCripto::obtieneNumSerieP7(this->_pSgiCripto, pkcs7Path.c_str(), &serialNums);
                                    this->_profilingHelper->Stop(ScadePerfCounter::Security_obtieneNumSerieP7);

                                    if (SGICRIPTO_BUSINESSERROR(result))
                                        throw gcnew BusinessException(SgiErrorLookupHelper::FormatErrorMessage("SgiCripto::obtieneNumSerieP7()", result), (int)result);

                                    if (SGICRIPTO_SYSTEMERROR(result))
                                        throw gcnew PlatformException(SgiErrorLookupHelper::FormatErrorMessage("SgiCripto::obtieneNumSerieP7()", result), (int)result);

                                    certSerialNumbers = gcnew List<String^>();
                                    if (SGICRIPTO_SUCCEEDED(result))
                                    {
                                        String^ serialNumbers = gcnew String(serialNums.c_str());
                                        array<String^>^ sns = serialNumbers->Split(gcnew array<Char> {'|'});

                                        for each (String^ s in sns)
                                            certSerialNumbers->Add(s);
                                    }
                                }
                                finally
                                {
                                    if (SGICRIPTO_SYSTEMERROR(result))
                                        InternalDispose();
                                }

                                return certSerialNumbers;
                            }

                            /*[Obsolete("This method is deprecated. Use the SgiCryptoHelper.ProcessPkcs7FileInfo() instead.")]*/
                            void SgiCryptoHelper::ProcessPkcs7File(String^ receiverCertificatePath, String^ receiverPrivateKeyPath, String^ password, String^ pkcs7FilePath, String^ targetFilePath, String^ caCertificatePath, CertificateType certificateType)
                            {
                                std::string recvCertificatePath = MarshalString(receiverCertificatePath);
                                std::string recvPrivateKey      = MarshalString(receiverPrivateKeyPath);
                                std::string pass                = MarshalString(password);
                                std::string pkcs7File           = MarshalString(pkcs7FilePath);
                                std::string targetFile          = MarshalString(targetFilePath);
                                std::string caCertificate       = MarshalString(caCertificatePath);
                                SgiCripto::tCertTipo type       = (SgiCripto::tCertTipo)certificateType;
                                SgiCripto::tError result        = SgiCripto::Error_OK;

                                try
                                {
                                    this->_profilingHelper->Start();
                                    result = SgiCripto::procP7(this->_pSgiCripto, recvCertificatePath.c_str(), recvPrivateKey.c_str(), pass.c_str(), pkcs7File.c_str(), targetFile.c_str(), caCertificate.c_str(), type);
                                    this->_profilingHelper->Stop(ScadePerfCounter::Security_procP7);

                                    if (SGICRIPTO_BUSINESSERROR(result))
                                        throw gcnew BusinessException(SgiErrorLookupHelper::FormatErrorMessage("SgiCripto::procP7()", result), (int)result);

                                    if (SGICRIPTO_SYSTEMERROR(result))
                                        throw gcnew PlatformException(SgiErrorLookupHelper::FormatErrorMessage("SgiCripto::procP7()", result), (int)result);
                                }
                                finally
                                {
                                    if (SGICRIPTO_SYSTEMERROR(result))
                                        InternalDispose();
                                }
                            }

                            array<Byte>^ SgiCryptoHelper::ProcessPkcs7FileInfo(String^ receiverCertificatePath, String^ receiverPrivateKeyPath, String^ password, array<Byte>^ pkcs7FileInfo, String^ caCertificatePath, CertificateType certificateType)
                            {
                                array<Byte>^ outputData = nullptr;
                                unsigned char* input  = NULL;
                                unsigned char* output = NULL;
                                SgiCripto::tError result = SgiCripto::Error_OK;

                                try
                                {
                                    int inputLen = pkcs7FileInfo->Length;
                                    input = new unsigned char[inputLen];
                                    if (input == NULL)
                                        throw gcnew PlatformException("Memory allocation for input data failed invoking SgiCryptoHelper::ProcessPkcs7FileInfo().\n", ERROR_OUTOFMEMORY);

                                    for (int i = 0; i < inputLen; i++)
                                        input[i] = Convert::ToByte(pkcs7FileInfo[i]);

                                    int outputLen = inputLen * 3;
                                    output = new unsigned char[outputLen];
                                    if (output == NULL)
                                        throw gcnew PlatformException("Memory allocation for output data failed invoking SgiCryptoHelper::ProcessPkcs7FileInfo().\n", ERROR_OUTOFMEMORY);

                                    std::string recvCertificatePath = MarshalString(receiverCertificatePath);
                                    std::string recvPrivateKey      = MarshalString(receiverPrivateKeyPath);
                                    std::string pass                = MarshalString(password);
                                    std::string caCertificate       = MarshalString(caCertificatePath);
                                    SgiCripto::tCertTipo type       = (SgiCripto::tCertTipo)certificateType;

                                    this->_profilingHelper->Start();
                                    result = SgiCripto::procP7(this->_pSgiCripto, recvCertificatePath.c_str(), recvPrivateKey.c_str(), pass.c_str(), reinterpret_cast<const char*>(input), inputLen, reinterpret_cast<char**>(&output), &outputLen, caCertificate.c_str(), type);
                                    this->_profilingHelper->Stop(ScadePerfCounter::Security_procP7);

                                    if (SGICRIPTO_BUSINESSERROR(result))
                                        throw gcnew BusinessException(SgiErrorLookupHelper::FormatErrorMessage("SgiCripto::procP7()", result), (int)result);

                                    if (SGICRIPTO_SYSTEMERROR(result))
                                        throw gcnew PlatformException(SgiErrorLookupHelper::FormatErrorMessage("SgiCripto::procP7()", result), (int)result);

                                    outputData = gcnew array<Byte>(outputLen);
                                    for (int j = 0; j < outputLen; j++)
                                        outputData[j] = output[j];
                                }
                                finally
                                {
                                    if (SGICRIPTO_SYSTEMERROR(result))
                                        InternalDispose();

                                    if (input != NULL)
                                        delete[] input;

                                    if (output != NULL)
                                        delete[] output;
                                }

                                return outputData;
                            }

                            void SgiCryptoHelper::InternalInitialize(Boolean connect, String^ configPath)
                            {
                                if (!this->_initialized)
                                {
                                    if (this->_pSgiCripto == NULL)
                                        this->_pSgiCripto = SgiCripto::new_SgiCripto();

                                    const char* pCfgPath = NULL;
                                    std::string path;
                                    if (!String::IsNullOrEmpty(configPath))
                                    {
                                        path = MarshalString(configPath);
                                        pCfgPath = path.c_str();
                                    }

                                    this->_profilingHelper->Start();
                                    SgiCripto::tError result = SgiCripto::inicia(this->_pSgiCripto, connect, pCfgPath);
                                    this->_profilingHelper->Stop(ScadePerfCounter::Security_Inicia);

                                    int res = (int)result;

                                    if (SGICRIPTO_BUSINESSERROR(result))
                                        throw gcnew BusinessException(SgiErrorLookupHelper::FormatErrorMessage("SgiCripto::inicia()", result), (int)result);

                                    if (SGICRIPTO_SYSTEMERROR(result))
                                        throw gcnew PlatformException(SgiErrorLookupHelper::FormatErrorMessage("SgiCripto::inicia()", result), (int)result);

                                    if (SGICRIPTO_SUCCEEDED(result)&& connect)
                                    {
                                        this->_profilingHelper->Start();
                                        result = SgiCripto::conectaARA(this->_pSgiCripto);
                                        this->_profilingHelper->Stop(ScadePerfCounter::Security_conectaARA);

                                        res = (int)result;

                                        if (SGICRIPTO_BUSINESSERROR(result))
                                            throw gcnew BusinessException(SgiErrorLookupHelper::FormatErrorMessage("SgiCripto::conectaARA()", result), (int)result);

                                        if (SGICRIPTO_SYSTEMERROR(result))
                                            throw gcnew PlatformException(SgiErrorLookupHelper::FormatErrorMessage("SgiCripto::conectaARA()", result), (int)result);
                                    }

                                    this->_initialized = true;
                                }
                            }

                            void SgiCryptoHelper::InternalDispose()
                            {
                                this->_initialized = false;
                                if (this->_pSgiCripto != NULL)
                                {
                                    SgiCripto::delete_SgiCripto(this->_pSgiCripto);
                                    this->_pSgiCripto = NULL;
                                }
                            }

                            std::string& SgiCryptoHelper::MarshalString(String^ someString)
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
