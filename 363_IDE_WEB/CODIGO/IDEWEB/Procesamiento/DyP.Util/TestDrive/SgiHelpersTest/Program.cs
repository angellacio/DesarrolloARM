//@(#)SCADE2(W:SKD08212CO2:DyP.Util.Security.Interop.SgiHelpersTest:Program:0:22/Mayo/2008[DyP.Util.Security.Interop.SgiHelpersTest:1.0:22/Mayo/2008])
//
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Text;

using SAT.DyP.Util.Security;
using SAT.DyP.Util.Security.Interop.SgiHelpers;

namespace SAT.DyP.Util.Security.Interop.SgiHelpersTest
{
    public sealed class Program
    {
        private EncryptionAlgorithm[] _algorithms = {
            EncryptionAlgorithm.DES_CBC     ,
            EncryptionAlgorithm.DES_EDE     ,
            EncryptionAlgorithm.DES_EDE3    ,
            EncryptionAlgorithm.DES_CFB     ,
            EncryptionAlgorithm.DES_EDE_CFB ,
            EncryptionAlgorithm.DES_EDE3_CFB,
            EncryptionAlgorithm.DES_OFB     ,
            EncryptionAlgorithm.DES_EDE_OFB ,
            EncryptionAlgorithm.DES_EDE3_OFB,
            EncryptionAlgorithm.DES_EDE_CBC ,
            EncryptionAlgorithm.DES_EDE3_CBC,
            EncryptionAlgorithm.DESX_CBC    ,
            EncryptionAlgorithm.RC2_CBC     ,
            EncryptionAlgorithm.RC2_CFB     ,
            EncryptionAlgorithm.RC2_OFB     ,
            EncryptionAlgorithm.RC4         ,
            EncryptionAlgorithm.RC4_40      ,
            EncryptionAlgorithm.IDEA_CBC    ,
            EncryptionAlgorithm.IDEA_CFB    ,
            EncryptionAlgorithm.IDEA_OFB    ,
            EncryptionAlgorithm.BF_CBC      ,
            EncryptionAlgorithm.BF_CFB      ,
            EncryptionAlgorithm.BF_OFB      ,
            EncryptionAlgorithm.CAST5_CBC   ,
            EncryptionAlgorithm.CAST5_CFB   ,
            EncryptionAlgorithm.CAST5_OFB   ,
            EncryptionAlgorithm.RC5_CBC     ,
            EncryptionAlgorithm.RC5_CFB     ,
            EncryptionAlgorithm.RC5_OFB     ,
            EncryptionAlgorithm.AES_128_CBC ,
            EncryptionAlgorithm.AES_128_CFB ,
            EncryptionAlgorithm.AES_128_OFB ,
            EncryptionAlgorithm.AES_192_CBC ,
            EncryptionAlgorithm.AES_192_CFB ,
            EncryptionAlgorithm.AES_192_OFB ,
            EncryptionAlgorithm.AES_256_CBC ,
            EncryptionAlgorithm.AES_256_CFB ,
            EncryptionAlgorithm.AES_256_OFB
        };

        private bool _traceFlag;
        private bool _testCertificateStuff, _testCryptographyStuff, _testDigitalSignaturesStuff, _testPkcs7InfoStuff;
        private string _inputFilesPath;

        public static void Main(string[] args)
        {
            string logFileName = String.Empty;
            int exitCode = 1;

            try
            {
                logFileName = String.Format(@"{0}\SgiHelpersTest-{1}.log", Directory.GetCurrentDirectory(), DateTime.Now.ToString("yyy-MM-dd-HH.mm.ss"));
                StreamWriter logFile = File.CreateText(logFileName);

                Trace.Listeners.Add(new ConsoleTraceListener());
                Trace.Listeners.Add(new TextWriterTraceListener(logFile));

                new Program().Execute();
                exitCode = 0;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(@"Oops!: " + ex.Message);
            }
            finally
            {
                Trace.Flush();
            }

            Environment.Exit(exitCode);
        }

        public Program()
        {
            Initialize();
        }

        public void Execute()
        {
            if (this._testCertificateStuff)
                TestCertificateStuff();
            if (this._testCryptographyStuff)
                TestCryptographyStuff();
            if (this._testDigitalSignaturesStuff)
                TestDigitalSignatureStuff();
            if (this._testPkcs7InfoStuff)
                TestPkcs7InfoStuff();
        }

        private void Initialize()
        {
            this._inputFilesPath = ConfigurationManager.AppSettings[@"InputFilesPath"];
            this._traceFlag      = GetFlagValue(@"WriteTrace");

            this._testCertificateStuff       = GetFlagValue(@"TestCertificateStuff");
            this._testCryptographyStuff      = GetFlagValue(@"TestCryptographyStuff");
            this._testDigitalSignaturesStuff = GetFlagValue(@"TestDigitalSignaturesStuff");
            this._testPkcs7InfoStuff         = GetFlagValue(@"TestPkcs7InfoStuff");
        }

        private void TestCertificateStuff()
        {
            try
            {
                string testCertificatePath = GetFilePath(@"CertificateStuff_TestCertificate");
                byte[] certificateData = File.ReadAllBytes(testCertificatePath);

                using (SgiCertificateHelper helper = new SgiCertificateHelper(certificateData))
                {
                    WriteTraceMessage(String.Format(@"Certificate file:          '{0}'.", testCertificatePath));
                    WriteTraceMessage(String.Format(@"Certificate serial number: '{0}'.", helper.CertificateSerialNumber));
                    WriteTraceMessage(String.Format(@"Tax Payer ID:              '{0}'.", helper.CertificateTaxPayerID));
                    WriteTraceMessage(String.Format(@"Tax Payer Name:            '{0}'.", helper.CertificateTaxPayerName));
                    WriteTraceMessage(String.Format(@"Validity Starting Date:    '{0}'.", helper.ValidityStartDate.ToString()));
                    WriteTraceMessage(String.Format(@"Validity Ending Date:      '{0}'.", helper.ValidityEndDate.ToString()));
                }
            }
            catch (Exception ex)
            {
                WriteTraceMessage(@"Oops!: " + ex.Message);
            }
        }

        private void TestCryptographyStuff()
        {
            using (SgiCryptoHelper cryptoHelper = new SgiCryptoHelper(true, null))
            {
                // Certificate fetching operations
                string serialNumber = ConfigurationManager.AppSettings[@"CryptographyStuff_CertificateSerialNumber"];
                try
                {
                    CertificateInfo ci = new CertificateInfo();

                    byte[] certificate = cryptoHelper.GetCertificateBySerialNumber(serialNumber, ref ci);
                    WriteTraceMessage(String.Format(@"Certificate serial number: '{0}'.", ci.SerialNumber));
                    WriteTraceMessage(String.Format(@"Certificate Type:          '{0}'.", ci.Type));
                    WriteTraceMessage(String.Format(@"Certificate State:         '{0}'.", ci.State));
                    WriteTraceMessage(String.Format(@"Tax Payer ID:              '{0}'.", ci.TaxPayerID));
                    WriteTraceMessage(String.Format(@"Validity Starting Date:    '{0}'.", ci.ValidityStart.ToString()));
                    WriteTraceMessage(String.Format(@"Validity Ending Date:      '{0}'.", ci.ValidityEnd.ToString()));
                }
                catch (Exception ex)
                {
                    WriteTraceMessage(String.Format(@"Oops!: " + ex.Message));
                }

                CertificateInfo ci2;
                try
                {
                    ci2 = (CertificateInfo)cryptoHelper.GetCertificateInfoBySerialNumber(serialNumber);
                    WriteTraceMessage(String.Format(@"Certificate serial number: '{0}'.", ci2.SerialNumber));
                    WriteTraceMessage(String.Format(@"Certificate Type:          '{0}'.", ci2.Type));
                    WriteTraceMessage(String.Format(@"Certificate State:         '{0}'.", ci2.State));
                    WriteTraceMessage(String.Format(@"Tax Payer ID:              '{0}'.", ci2.TaxPayerID));
                    WriteTraceMessage(String.Format(@"Validity Starting Date:    '{0}'.", ci2.ValidityStart.ToString()));
                    WriteTraceMessage(String.Format(@"Validity Ending Date:      '{0}'.", ci2.ValidityEnd.ToString()));
                }
                catch (Exception ex)
                {
                    WriteTraceMessage(String.Format(@"Oops!: " + ex.Message));
                }

                string someTaxPayerID = ConfigurationManager.AppSettings[@"CryptographyStuff_TaxPayerID"];

                try
                {
                    string serialNumber2 = cryptoHelper.GetCertificateSerialNumberByTaxPayerID(someTaxPayerID);
                    WriteTraceMessage(String.Format(@"Certificate serial number for tax payer ID '{0}' is '{1}'.", someTaxPayerID, serialNumber2));
                }
                catch (Exception ex)
                {
                    WriteTraceMessage(String.Format(@"Oops!: " + ex.Message));
                }

                try
                {
                    List<string> serialNumbers = cryptoHelper.GetCertificateSerialNumbersByTaxPayerID(someTaxPayerID, CertificateType.Any, CertificateState.Active, 100);
                    if (serialNumbers.Count > 0)
                    {
                        WriteTraceMessage(String.Format(@"The following active certificates were found for tax payer ID '{0}':"));
                        foreach (string sn in serialNumbers)
                            WriteTraceMessage(String.Format(@"{0}", sn));
                    }
                    else
                        WriteTraceMessage(String.Format(@"No active certificates were found for tax payer ID '{0}'.", someTaxPayerID));
                }
                catch (Exception ex)
                {
                    WriteTraceMessage(String.Format(@"Oops!: " + ex.Message));
                }

                // Signature generation & verification operations
                string certificatePath = GetFilePath(@"CryptographyStuff_TestCertificate");
                string privateKeyPath  = GetFilePath(@"CryptographyStuff_TestPrivateKey");
                string privateKeyPassword = ConfigurationManager.AppSettings[@"CryptographyStuff_PrivateKeyPassword"];

                try
                {
                    if (cryptoHelper.IsPrivateKeyValid(certificatePath, privateKeyPath, privateKeyPassword))
                        WriteTraceMessage(String.Format(@"The private key information for file '{0}' is valid.", certificatePath));
                    else
                        WriteTraceMessage(String.Format(@"The private key information for file '{0}' is invalid.", certificatePath));
                }
                catch (Exception ex)
                {
                    WriteTraceMessage(String.Format(@"Oops!: " + ex.Message));
                }

                string signatureInputData = ConfigurationManager.AppSettings[@"CryptographyStuff_SignatureInputData"];
                string signatureOutputData = String.Empty;

                try
                {
                    signatureOutputData = cryptoHelper.GenerateSignature(signatureInputData, privateKeyPath, DigestAlgorithm.SHA1, privateKeyPassword);
                    WriteTraceMessage(String.Format(@"Input Data:  '{0}'.", signatureInputData));
                    WriteTraceMessage(String.Format(@"Output Data: '{0}'.", signatureOutputData));
                }
                catch (Exception ex)
                {
                    WriteTraceMessage(String.Format(@"Oops!: " + ex.Message));
                }

                try
                {
                    byte[] certificateData = File.ReadAllBytes(certificatePath);
                    using (SgiCertificateHelper certificateHelper = new SgiCertificateHelper(certificateData))
                    {
                        if (cryptoHelper.IsSignatureValid(certificateHelper, signatureInputData, signatureOutputData))
                            WriteTraceMessage(String.Format(@"The generated signature is valid."));
                        else
                            WriteTraceMessage(String.Format(@"The generated signature is invalid."));
                    }
                }
                catch (Exception ex)
                {
                    WriteTraceMessage(String.Format(@"Oops!: " + ex.Message));
                }

                // Encryption & Decryption operations
                foreach (EncryptionAlgorithm ea in _algorithms)
                {
                    try
                    {
                        string input    = ConfigurationManager.AppSettings[@"CryptographyStuff_EncryptionInputData"];
                        string password = ConfigurationManager.AppSettings[@"CryptographyStuff_EncryptionPassword"];

                        byte[] intermediate = cryptoHelper.Encrypt(input, ea, password);
                        byte[] output = cryptoHelper.Decrypt(intermediate, ea, password);
                        string outputData = Encoding.Unicode.GetString(output);

                        if (input.Equals(outputData))
                            WriteTraceMessage(String.Format(@"En(De)cryption stuff worked ok using method '{0}'.", ea.ToString()));
                    }
                    catch (Exception ex)
                    {
                        WriteTraceMessage(String.Format(@"Oops!: " + ex.Message));
                    }
                }
            }
        }

        private void TestDigitalSignatureStuff()
        {
            try
            {
                string hostName    = ConfigurationManager.AppSettings[@"DigitalSignatureStuff_HostName"];
                string serviceName = ConfigurationManager.AppSettings[@"DigitalSignatureStuff_ServiceName"];
                string inputString = ConfigurationManager.AppSettings[@"DigitalSignatureStuff_InputData"];

                using (SgiDigitalSealHelper helper = new SgiDigitalSealHelper(hostName, serviceName))
                {
                    try
                    {
                        string signedString = helper.GenerateDigitalSeal(inputString);

                        WriteTraceMessage(String.Format(@"Input string:  '{0}'.", inputString));
                        WriteTraceMessage(String.Format(@"Signed string: '{0}'.", signedString));
                    }
                    catch (Exception ex)
                    {
                        WriteTraceMessage(String.Format(@"Oops!: " + ex.Message));
                    }
                }
            }
            catch (Exception ex)
            {
                WriteTraceMessage(String.Format(@"Oops!: " + ex.Message));
            }
        }

        private void TestPkcs7InfoStuff()
        {
            string certificatePath = GetFilePath(@"Pkcs7InfoStuff_CertificatePath");
            string privateKeyPath  = GetFilePath(@"Pkcs7InfoStuff_PrivateKeyPath");
            string inputFilePath   = GetFilePath(@"Pkcs7InfoStuff_InputFilePath");

            string inputCreateFilePath  = GetFilePath(@"Pkcs7InfoStuff_Create_InputFilePath");
            string outputCreateFilePath = inputCreateFilePath + @".out";

            string password = ConfigurationManager.AppSettings[@"Pkcs7InfoStuff_Password"];

            FileInfo fi = new FileInfo(inputFilePath);
            Guid fileName = Guid.NewGuid();
            string outputFilePath = fi.DirectoryName + @"\\" + fileName.ToString(@"N") + @".out";

            using (SgiCryptoHelper cryptoHelper = new SgiCryptoHelper(true, null))
            {
                try
                {
                    cryptoHelper.CreatePkcs7File(certificatePath, privateKeyPath, password, inputCreateFilePath, Pkcs7Type.Envelope, outputCreateFilePath, EncryptionAlgorithm.RC4, String.Empty);
                }
                catch (Exception ex)
                {
                    WriteTraceMessage(String.Format(@"The following error occurred testing PKCS7 files stuff: '{0}'.", ex.Message));
                }

                try
                {
                    cryptoHelper.ProcessPkcs7File(certificatePath, privateKeyPath, password, inputFilePath, outputFilePath, String.Empty, CertificateType.ElectronicSignature);
                }
                catch (Exception ex)
                {
                    WriteTraceMessage(String.Format(@"The following error testing PKCS7 files stuff: '{0}'.", ex.Message));
                }

                byte[] inputData   = File.ReadAllBytes(inputFilePath);
                byte[] outputData  = File.ReadAllBytes(outputFilePath);
                byte[] outputData2 = null;

                try
                {
                    outputData2 = cryptoHelper.ProcessPkcs7FileInfo(certificatePath, privateKeyPath, password, inputData, String.Empty, CertificateType.ElectronicSignature);
                }
                catch (Exception ex)
                {
                    WriteTraceMessage(String.Format(@"The following error testing PKCS7 files stuff: '{0}'.", ex.Message));
                }

                bool equalOK = true;
                if (null != outputData && null != outputData2 && outputData.Length == outputData2.Length)
                {
                    for (int i = 0; i < outputData.Length; i++)
                    {
                        if (outputData[i] != outputData2[i])
                        {
                            equalOK = false;
                            break;
                        }
                    }

                    if (equalOK)
                        WriteTraceMessage(String.Format(@"The PKCS7 output data are equal."));
                    else
                        WriteTraceMessage(String.Format(@"The PKCS7 output data are not equal."));
                }
                else
                    WriteTraceMessage(String.Format(@"The PKCS7 output data lengths don't match."));
            }
        }

        private bool GetFlagValue(string flagConfigurationKey)
        {
            if (String.IsNullOrEmpty(flagConfigurationKey))
                throw new ArgumentException(@"The flag configuration key cannot be null or an empty string");

            string flag    = ConfigurationManager.AppSettings[flagConfigurationKey];
            bool flagValue = (!String.IsNullOrEmpty(flag) && (@"true".Equals(flag.ToLower()) || @"yes".Equals(flag.ToLower()) || @"1".Equals(flag)));

            return flagValue;
        }

        private string GetFilePath(string fileConfigurationKey)
        {
            if (String.IsNullOrEmpty(fileConfigurationKey))
                throw new ArgumentException(@"The file configuration key cannot be null or an empty string");

            string fullPath = String.Format(@"{0}\{1}", this._inputFilesPath, ConfigurationManager.AppSettings[fileConfigurationKey]);
            if (!File.Exists(fullPath))
                throw new ArgumentException(String.Format(@"The file '{0}' is missing.", fullPath));

            return fullPath;
        }

        private void WriteTraceMessage(string someMessage)
        {
            Trace.WriteLineIf(this._traceFlag, someMessage);
        }
    }
}
