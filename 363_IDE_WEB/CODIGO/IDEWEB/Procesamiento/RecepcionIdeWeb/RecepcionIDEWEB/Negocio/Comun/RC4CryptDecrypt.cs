using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace RecepcionIDEWEB.Negocio.Comun
{
    public class RC4CryptDecrypt
    {
        private byte[] S;
        private UInt32 i, j;
        Encoding encCrip;

        public RC4CryptDecrypt(Encoding CriptoEncoding)
        { encCrip = CriptoEncoding; }

        public RC4CryptDecrypt()
        { encCrip = new UTF8Encoding(); }


        #region "Aux Functions"

        private void Swap(byte[] s, UInt32 Loci, UInt32 Locj)
        {
            byte tmp = s[Loci];
            s[Loci] = s[Locj];
            s[Locj] = tmp;
        }

        private static byte[] HexStringToByteArray(string Hex)
        {
            byte[] Bytes = new byte[Hex.Length / 2];
            int[] HexValue = new int[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 
                                   0x06, 0x07, 0x08, 0x09, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                                   0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };

            for (int x = 0, i = 0; i < Hex.Length; i += 2, x += 1)
            {
                Bytes[x] = (byte)(HexValue[Char.ToUpper(Hex[i + 0]) - '0'] << 4 |
                                  HexValue[Char.ToUpper(Hex[i + 1]) - '0']);
            }

            return Bytes;
        }

        #endregion

        #region "RC4"

        private void rc4Init(byte[] Key, int KeyLength)
        {
            S = new byte[256];

            for (i = 0; i < 256; i++)
                S[i] = (byte)i;

            for (i = j = 0; i < 256; i++)
            {
                j = (j + Key[i % KeyLength] + S[i]) & 255;
                Swap(S, i, j);
            }

            i = j = 0;
        }

        private byte rc4Output()
        {
            i = (i + 1) & 255;
            j = (j + S[i]) & 255;
            Swap(S, i, j);

            return S[(S[i] + S[j]) & 255];
        }

        private byte[] rc4Execute(byte[] Key, byte[] Input)
        {
            rc4Init(Key, Key.Length);
            byte[] rc4Result = new byte[Input.Length];

            for (int y = 0; y < Input.Length; y++)
                rc4Result[y] = (byte)((int)Input[y] ^ (int)rc4Output());

            return rc4Result;
        }

        #endregion

        public string Code(string Input, string Key)
        {
            string strOutput = string.Format("{0:X} ", (byte)Input.Length);
            byte[] Coded = rc4Execute(encCrip.GetBytes(Key), encCrip.GetBytes(Input));

            for (int y = 0; y < Coded.Length; y++)
                strOutput += string.Format("{0:X} ", Coded[y]);

            return strOutput;
        }

        public string Decode(string Input, string Key)
        {
            string[] strBytes = Input.Trim().Split(' ');

            for (int x = 1; x < strBytes.Length; x++)
                if (strBytes[x].Length == 1) strBytes[x] = "0" + strBytes[x];

            string strHex = "";
            for (int x = 1; x < strBytes.Length; x++)
                strHex += strBytes[x];

            byte[] Decoded = rc4Execute(encCrip.GetBytes(Key), HexStringToByteArray(strHex));

            return encCrip.GetString(Decoded);
        }
    }
}