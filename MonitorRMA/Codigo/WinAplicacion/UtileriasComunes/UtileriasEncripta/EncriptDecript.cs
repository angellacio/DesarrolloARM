using System;
using System.Security.Cryptography;
using System.Text;
using EnumText = UtileriasComunes.ManejoEnumTextos;
using System.Configuration;

namespace UtileriasComunes.UtileriasEncripta
{
    public static class EncriptDecript
    {
        private static string CadenaLlave = "";
        private static void LlenaCadenaLave(EnumText.Encriptador.TipoLlave? TLlave)
        {
            switch (TLlave)
            {
                case ManejoEnumTextos.Encriptador.TipoLlave.Usuario:
                    CadenaLlave = ConfigurationManager.AppSettings["LlavesEncriptar"].ToString().Trim().Split('|')[0];
                    //CadenaLlave = "V0b0L1HAm2aoLEp";
                    break;
                case ManejoEnumTextos.Encriptador.TipoLlave.CadenaConexion:
                    CadenaLlave = ConfigurationManager.AppSettings["LlavesEncriptar"].ToString().Trim().Split('|')[1];
                    //CadenaLlave = "Ii708Yapz0VMn8G";
                    break;
                default:
                    CadenaLlave = "9Kp96ofFsi3NyVV";
                    break;
            }
        }

        /// <summary>
        /// Cifrar una cadena utilizando el método de cifrado. Regresa un texto de cifrado.
        /// </summary>
        /// <param name="texto">Cadena de caracteres que se va a encriptar</param>
        /// <param name="TLlave">Tipo de semilla con la que se encriptara</param>
        /// <returns>Regresa el texto encriptado</returns>
        public static string Encriptar(string texto, EnumText.Encriptador.TipoLlave? TLlave)
        {
            LlenaCadenaLave(TLlave);
            // Arreglo de bytes donde guardaremos la llave
            byte[] keyArray;
            // Arreglo de bytes donde guardaremos el texto que vamos a encriptar
            byte[] Arreglo_a_Cifrar = UTF8Encoding.UTF8.GetBytes(texto);
            // Se utilizan las clases de encriptacion proveidas por el Framework Algritmo MD5
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            // Se guarda la llave para que se le realice hashing
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(CadenaLlave));
            hashmd5.Clear();
            // Algoritmo 3DAS
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;
            // Se empieza con la transformaion de la cadena
            ICryptoTransform cTransform = tdes.CreateEncryptor();
            // Arreglo de bytes donde se guarda la cadena cifrada
            byte[] ArrayResultado = cTransform.TransformFinalBlock(Arreglo_a_Cifrar, 0, Arreglo_a_Cifrar.Length);
            tdes.Clear();
            // Se regresa el resultado en forma de una cadena
            return Convert.ToBase64String(ArrayResultado, 0, ArrayResultado.Length);
        }
        /// <summary>
        /// Desencripta un texto usando el metodo de deble cadena Regresa una cadena desencriptada.
        /// </summary>
        /// <param name="textoEncriptado">Cadena encriptada</param>
        /// <param name="TLlave">Tipo de semilla con la que se desencriptara</param>
        /// <returns>Regresa el texto desenciptado</returns>
        public static string Desencriptar(string textoEncriptado, EnumText.Encriptador.TipoLlave TLlave)
        {
            LlenaCadenaLave(TLlave);
            byte[] keyArray;
            // Convierte el texto en una secuencia de bytes
            byte[] Array_a_Descifrar = Convert.FromBase64String(textoEncriptado);
            // Se llama a las clases ke tienen los algoritmos de encriptacion se le aplica hashing
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(CadenaLlave));
            hashmd5.Clear();
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(Array_a_Descifrar, 0, Array_a_Descifrar.Length);
            tdes.Clear();
            string res = UTF8Encoding.UTF8.GetString(resultArray);
            return res;
        }
    }
}
