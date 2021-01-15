using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Configuration;


namespace Sat.Scade.Net.IDE.Presentacion.Web.Comun
{
    [Serializable]
    public static class Tipos
    {
        private static string _DecryptionPassword;
        private static int _MaxSize;
        private static string _TmpPath;
        private static string _DecryptionCertPath;
        private static string _DecryptionKeyPath;
        private static Int16 _InWindow;

        public static int MaxInMemoryProcessSize
        { get { return _MaxSize; } }

        public static string DecryptionKeyPath
        { get { return _DecryptionKeyPath; } }

        public static string DecryptionCertPath
        { get { return _DecryptionCertPath; } }

        public static bool InWindow
        { get { return (_InWindow == 1); } }

        public static string TemporaryPath
        {
            get
            {
                if (string.IsNullOrEmpty(_TmpPath))
                    return Environment.GetEnvironmentVariable("TEMP");
                else
                    return _TmpPath;
            }
        }

        public static string DecryptionPassword
        { get { return Decrypt(_DecryptionPassword); } }

        private static string Decrypt(string strCoded)
        {
            string decryptionKey = ConfigurationManager.AppSettings["DecryptionKey"];
            RC4CryptDecrypt _Crypt = new RC4CryptDecrypt();
            return _Crypt.Decode(strCoded, decryptionKey);
        }

        static Tipos()
        {
            if (ConfigurationManager.AppSettings["DecryptionCertPath"] != null)
                _DecryptionCertPath = ConfigurationManager.AppSettings["DecryptionCertPath"];

            if (ConfigurationManager.AppSettings["DecryptionKeyPath"] != null)
                _DecryptionKeyPath = ConfigurationManager.AppSettings["DecryptionKeyPath"];

            _DecryptionPassword = ConfigurationManager.AppSettings["DecryptionPassword"];

            if (ConfigurationManager.AppSettings["MaxInMemoryProcessSize"] != null)
                _MaxSize = Convert.ToInt32(ConfigurationManager.AppSettings["MaxInMemoryProcessSize"]);
            else
                _MaxSize = -1;

            if (ConfigurationManager.AppSettings["InWindow"] != null)
                _InWindow = Convert.ToInt16(ConfigurationManager.AppSettings["InWindow"]);
            else
                _InWindow = 0;

            _TmpPath = ConfigurationManager.AppSettings["TemporaryPath"]; ;
        }
    }
}