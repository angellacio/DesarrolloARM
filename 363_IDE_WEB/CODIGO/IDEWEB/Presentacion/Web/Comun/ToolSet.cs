using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;

namespace Sat.Scade.Net.IDE.Presentacion.Web.Comun
{
    public static class ToolSet
    {
        public static sbyte[] CastByteTosByte(byte[] ArrIn)
        {
            sbyte[] retData = new sbyte[ArrIn.Length];

            for (int nData = 0; nData < ArrIn.Length; nData++)
                retData[nData] = (sbyte)ArrIn[nData];

            return retData;
        }

        public static byte[] CastsByteToByte(sbyte[] ArrIn)
        {
            byte[] retData = new byte[ArrIn.Length];

            for (int nData = 0; nData < ArrIn.Length; nData++)
                retData[nData] = (byte)ArrIn[nData];

            return retData;
        }

        public static byte[] ByteAppend(byte[] Arr1, byte[] Arr2)
        {
            int Len1, Len2;
            Len1 = Len2 = 0;

            Len1 = (Arr1 == null) ? 0 : Arr1.Length;
            Len2 = (Arr2 == null) ? 0 : Arr2.Length;

            byte[] retArray = new byte[Len1 + Len2];

            if (Len1 > 0)
                Array.Copy(Arr1, 0, retArray, 0, Arr1.Length);

            if (Len2 > 0)
                Array.Copy(Arr2, 0, retArray, (Len1 == 0) ? 0 : Len1 - 1, Arr2.Length);

            return retArray;
        }

        public static byte[] ShrinkBuffer(byte[] buffIn, int buffLength)
        {
            byte[] ret = new byte[buffLength];
            Array.Copy(buffIn, ret, buffLength);
            return ret;
        }

        public static byte[] GetRawData(string FileName, int IntroSize)
        {
            FileStream fs = new FileStream(FileName, FileMode.Open, FileAccess.Read);
            byte[] retData = new byte[IntroSize];
            fs.Read(retData, 0, retData.Length);
            fs.Close();
            return retData;
        }

        public static byte[] GetRawData(string FileName)
        {
            FileStream fs = new FileStream(FileName, FileMode.Open, FileAccess.Read);
            int fsize = (int)fs.Length;
            byte[] retData = new byte[fsize];
            fs.Read(retData, 0, retData.Length);
            fs.Close();
            return retData;
        }
    }
}