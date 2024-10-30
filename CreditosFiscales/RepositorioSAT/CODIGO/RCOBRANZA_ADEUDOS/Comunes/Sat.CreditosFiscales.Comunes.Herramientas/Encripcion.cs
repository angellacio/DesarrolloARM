
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Comunes.Herramientas:Sat.CreditosFiscales.Comunes.Herramientas.Encripcion:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sat.CreditosFiscales.Comunes.Herramientas
{
    using System;
    using System.Text;
    using System.Security.Cryptography;
    using System.IO;

    public class Encripcion
    {

        #region Declaraciones
        /// <summary>
        /// Llave de encriptación
        /// </summary>
        private static readonly byte[] _keyA = new byte[] { 2, 0, 1, 1, 5, 4, 7, 3, 8, 2, 1, 5, 3, 9, 5, 6 };

        /// <summary>
        /// Llave de encriptación
        /// </summary>
        private static readonly byte[] _keyB = new byte[] { 1, 1, 0, 2, 4, 1, 1, 2, 0, 0, 3, 5, 3, 9, 8, 4 };

        /// <summary>
        /// Llave de encriptación
        /// </summary>
        private static readonly byte[] _keyC = new byte[] { 4, 7, 3, 4, 4, 1, 1, 2, 8, 0, 3, 5, 7, 6, 2, 4 };
        #endregion

        #region Metodos
        /// <summary>
        /// Encripta el arreglo de llaves y valores del diccionario
        /// </summary>
        /// <param name="cadenaOriginal">Llaves y valores serializados</param>
        /// <returns>Cadena encriptada</returns>
        public string EncriptaCadena(string cadenaOriginal)
        {
            ////Obtiene los bytes de los parámetros a encriptar
            byte[] cadena = Encoding.UTF8.GetBytes(cadenaOriginal);

            try
            {
                int usarLlave = new Random().Next(1, 4);
                SymmetricAlgorithm algoritmoCifrado = this.DefineAlgoritmo(usarLlave.ToString());

                using (var codificador = algoritmoCifrado.CreateEncryptor())
                {
                    byte[] textoCodificado = null;
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, codificador, CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(cadena, 0, cadena.Length);
                            cryptoStream.FlushFinalBlock();
                            cryptoStream.Close();
                        }

                        textoCodificado = memoryStream.ToArray();
                        memoryStream.Close();
                    }

                    string codificacion = Convert.ToBase64String(this.ConcatenaIV(textoCodificado, algoritmoCifrado.IV.Length, algoritmoCifrado.IV));
                    string claveLlave = ObtieneClaveLlave(usarLlave);

                    return codificacion.Insert(0, claveLlave);
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public string ObtieneMD5(string str)
        {
            MD5 md5 = MD5CryptoServiceProvider.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] cadenaBytes = null;
            StringBuilder sb = new StringBuilder();
            cadenaBytes = md5.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < cadenaBytes.Length; i++) 
                sb.AppendFormat("{0:x2}", cadenaBytes[i]);
            return sb.ToString();
        }

        /// <summary>
        /// Obtiene la clave de la llave a concatenar en la cadena cifrada.
        /// </summary>
        /// <param name="usarLlave">Número Aleatorio</param>
        /// <returns>Clave de la llave a emplear en la cadena cifrada.</returns>
        private static string ObtieneClaveLlave(int usarLlave)
        {
            string claveLlave = string.Empty;
            switch (usarLlave)
            {
                case 1:
                    claveLlave = "a";
                    break;
                case 2:
                    claveLlave = "b";
                    break;
                case 3:
                    claveLlave = "c";
                    break;
            }

            return claveLlave;
        }

        /// <summary>
        /// Desencripta el mensaje y guarda el arreglo llave/valor.
        /// </summary>
        /// <param name="textoCifrado">Cadena que contiene los parametros encriptados.</param>
        /// <returns>Cadena desencriotada.</returns>
        public string DesencriptaCadena(string textoCifrado)
        {
            byte[] llaveSimetrica = null;
            byte[] cadenaCifrada = null;

            textoCifrado = textoCifrado.Replace(" ", "+");
            string textoDesencriptar = textoCifrado.Substring(1, textoCifrado.Length - 1);
            string claveLlave = textoCifrado.Substring(0, 1);

            SymmetricAlgorithm algoritmoCifrado = this.DefineAlgoritmo(claveLlave);

            this.SeparaTextoCifrado(textoDesencriptar, out llaveSimetrica, out cadenaCifrada, algoritmoCifrado.IV.Length);
            algoritmoCifrado.IV = llaveSimetrica;
            try
            {
                using (var descodificador = algoritmoCifrado.CreateDecryptor())
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, descodificador, CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(cadenaCifrada, 0, cadenaCifrada.Length);
                            cryptoStream.FlushFinalBlock();
                            cryptoStream.Close();
                        }

                        byte[] textoDecodificado = memoryStream.ToArray();
                        memoryStream.Close();

                        string querystring = Encoding.UTF8.GetString(textoDecodificado);
                        return querystring;
                    }
                }
            }
            catch (Exception err)
            {
                throw new Exception(string.Format("No se ha podido desencriptar el mensaje: {0}", err.Message));
            }
        }

        /// <summary>
        /// Agrega el vector de inicialización como parte del mesaje cifrado.
        /// </summary>
        /// <param name="textoCodificado">query string que ha sido ecriptada previamente.</param>
        /// <param name="tamanioVector">Tamaño de la llave simétrica.</param>
        /// <param name="vectorInicializacion">Vector de inicialización.</param>
        /// <returns>Arreglo de bytes.</returns>
        private byte[] ConcatenaIV(byte[] textoCodificado, int tamanioVector, Array vectorInicializacion)
        {
            byte[] concatenacion = new byte[textoCodificado.Length + tamanioVector];

            //// Se agrega el vector de inicialización al principio de la cadena cifrada.
            System.Buffer.BlockCopy(vectorInicializacion, 0, concatenacion, 0, tamanioVector);

            //// Se agrega la cadena cifrada.
            System.Buffer.BlockCopy(textoCodificado, 0, concatenacion, tamanioVector, textoCodificado.Length);

            return concatenacion;
        }

        /// <summary>
        /// Separa el IV del texto encriptado.
        /// </summary>
        /// <param name="textoCifrado">Cadena concatenada.</param>
        /// <param name="vectorInicializacion">Sálida IV de encriptación.</param>
        /// <param name="cadenaEncriptada">Sálida mensaje encriptado.</param>
        /// <param name="tamanioVector">Tamaño de la llave simétrica.</param>
        private void SeparaTextoCifrado(string textoCifrado, out byte[] vectorInicializacion, out byte[] cadenaEncriptada, int tamanioVector)
        {
            byte[] caracteresCifrados = Convert.FromBase64String(textoCifrado);
            int longitudIV = tamanioVector;

            vectorInicializacion = new byte[longitudIV];
            cadenaEncriptada = new byte[caracteresCifrados.Length - longitudIV];

            System.Buffer.BlockCopy(caracteresCifrados, 0, vectorInicializacion, 0, longitudIV);
            System.Buffer.BlockCopy(caracteresCifrados, longitudIV, cadenaEncriptada, 0, caracteresCifrados.Length - longitudIV);
        }

        /// <summary>
        /// Define el algoritmo de cifrado.
        /// </summary>
        /// <param name="random">Número Aleatorio</param>
        /// <returns></returns>
        private SymmetricAlgorithm DefineAlgoritmo(string random)
        {
            byte[] llave = null;
            switch (random)
            {
                case "1":
                case "a":
                default:
                    llave = _keyA;
                    break;
                case "2":
                case "b":
                    llave = _keyB;
                    break;
                case "3":
                case "c":
                    llave = _keyC;
                    break;
            }

            SymmetricAlgorithm algoritmoCifrado = new RijndaelManaged { Key = llave, Mode = CipherMode.CBC };
            return algoritmoCifrado;
        }
        #endregion
    }
}
