using System;
using System.Collections.Generic;
using System.Text;

namespace SAT.SCADE.NET.Common.Security
{
    public sealed class CertificateHelper
    {
        public static string GetTaxPayerID(string certificate)
        {
            return String.Empty;
        }

        public static string GetTaxPayerName(string certificate)
        {
            return String.Empty;
        }

        public static string GetCertificateSerialNumber(string certificate)
        {
            return String.Empty;
        }

        public static string GetCertificatePublicKey(string certificate)
        {
            return String.Empty;
        }

        public static void GetCertificateValidityPeriod(string certificate, out DateTime start, out DateTime end)
        {
            start = DateTime.Now;
            end   = DateTime.Now;
        }
    }
}
