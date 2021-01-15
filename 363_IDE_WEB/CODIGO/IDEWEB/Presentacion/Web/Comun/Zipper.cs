using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using java.util;
using System.Text;
using java.util.zip;
using System.Configuration;

namespace Sat.Scade.Net.IDE.Presentacion.Web.Comun
{
    [Serializable]
    public static class Zipper
    {
        public static bool CheckSignature(byte[] Content, int SignatureSize, string ExpectedSignature)
        {
            byte[] Signature = ToolSet.ShrinkBuffer(Content, SignatureSize);
            string ActualSignature = BitConverter.ToString(Signature);
            bool retValue = (ActualSignature == ExpectedSignature) ? true : false;
            return retValue;
        }

        public static byte[] Extract(byte[] Content)
        {
            byte[] UnZipped = new byte[0];
            sbyte[] InData = ToolSet.CastByteTosByte(Content);
            ZipInputStream xXx = new ZipInputStream(new java.io.ByteArrayInputStream(InData));
            do
            {
                ZipEntry entry = xXx.getNextEntry();

                if (entry == null) break;
                if (!entry.isDirectory())
                {
                    try
                    {
                        int Len = 0;
                        int BuffSize = 1024;
                        sbyte[] buffer = new sbyte[BuffSize];
                        while ((Len = xXx.read(buffer)) >= 0)
                        {
                            byte[] PartialContent = ToolSet.CastsByteToByte(buffer);
                            PartialContent = ToolSet.ShrinkBuffer(PartialContent, Len);
                            UnZipped = ToolSet.ByteAppend(UnZipped, PartialContent);
                        }
                    }
                    catch
                    { throw; }
                }
            } while (1 == 1);

            return UnZipped;
        }

        private static List<ZipEntry> GetZipFiles(ZipFile TheFile)
        {
            List<ZipEntry> lstZip = new List<ZipEntry>();
            Enumeration zipEnum = TheFile.entries();
            while (zipEnum.hasMoreElements())
            {
                ZipEntry zip = (ZipEntry)zipEnum.nextElement();
                lstZip.Add(zip);
            }
            return lstZip;
        }

        public static string Extract(string ZippedFile)
        {
            ZipFile TheFile = new ZipFile(ZippedFile);
            List<ZipEntry> zipFiles = GetZipFiles(TheFile);
            string retValue = string.Empty;

            foreach (ZipEntry zipFile in zipFiles)
            {
                if (!zipFile.isDirectory())
                {
                    java.io.InputStream s = TheFile.getInputStream(zipFile);
                    try
                    {
                        Directory.CreateDirectory(Path.Combine(Environment.GetEnvironmentVariable("temp"), Path.GetDirectoryName(zipFile.getName())));
                        retValue = Path.Combine(Path.Combine(Environment.GetEnvironmentVariable("temp"),
                                                             Path.GetDirectoryName(zipFile.getName())),
                                                Path.GetFileName(zipFile.getName()));
                        java.io.FileOutputStream dest = new java.io.FileOutputStream(retValue);
                        try
                        {
                            int len = 0;
                            sbyte[] buffer = new sbyte[7168];
                            while ((len = s.read(buffer)) >= 0)
                                dest.write(buffer, 0, len);
                        }
                        finally
                        { dest.close(); }
                    }
                    finally { s.close(); }
                }
            }
            TheFile.close();

            return retValue;
        }
    }
}