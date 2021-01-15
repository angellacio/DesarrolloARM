using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CriptoNet = mx.gob.sat.sgi.SgiCripto;
using RecepcionIDEWEB.Negocio.Comun;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;


namespace RecepcionIDEWEB.Negocio.Comun
{
    public class Desencripta
    {
        private System.IO.FileInfo FinalChecker;
        private int FileStrime_ReadTimeout
        {
            get
            {
                int nResult = 0;

                if (System.Configuration.ConfigurationManager.AppSettings["FileStrime_ReadTimeout"] != null)
                {
                    string sTO = System.Configuration.ConfigurationManager.AppSettings["FileStrime_ReadTimeout"].ToString().Trim();
                    int.TryParse(sTO, out nResult);
                }

                return nResult;
            }
        }

        public static byte[] CriptoNetProcess(byte[] Envelope)
        {
            int ret = 0;
            int EnvelopeLength = Envelope.Length;
            byte[] retValue = new byte[EnvelopeLength * 3];

            CriptoNet.SgiLlavePriv privk = new CriptoNet.SgiLlavePriv();
            CriptoNet.SgiCertificado cert = new CriptoNet.SgiCertificado();
            CriptoNet.SgiSobre sobre = new CriptoNet.SgiSobre();
            CriptoNet.SgiIO ioIn = null;
            CriptoNet.SgiIO ioOut = null;

            try
            {
                ioIn = InitDecryptionCertificate();
                ret = cert.inicia(0, ioIn);
                if (ret != 0)
                { throw new Exception("Error al cargar el certificado de desencripción : " + ret); }

                ioIn.Dispose();



                ioIn = InitDecryptionKey();
                string pkPassword = Decrypt(Parametros.DecryptionPassword);
                ret = privk.inicia(ioIn, pkPassword, pkPassword.Length);
                if (ret != 0)
                { throw new Exception("Error al cargar la llave privada de desencripción : " + ret); }



                ret = privk.verificaLlaves(cert);
                if (ret != 0)
                { throw new Exception("La llave privada del destinatario no corresponde con el certificado :" + ret); }

                ioIn = new CriptoNet.SgiIO();
                ret = ioIn.inicia(0, ref Envelope, ref EnvelopeLength);
                if (ret == 0)
                {
                    ret = sobre.inicia(ioIn);
                    if (ret != 0)
                    { throw new Exception("Error al cargar el sobre : " + ret); }
                }
                else
                { throw new Exception("Error al iniciar el sobre : " + ret); }
                ioIn.Dispose();

                int buffLength = retValue.Length;
                ioOut = new CriptoNet.SgiIO();
                ret = ioOut.inicia(1, ref retValue, ref buffLength);
                if (ret != 0)
                { throw new Exception("Error al iniciar el buffer de datos desencriptados : " + ret); }

                ret = sobre.procesa(cert, privk, ref ioOut);
                if (ret != 0)
                { throw new Exception("Error al procesar el sobre : " + ret); }

                ret = ioOut.getbuffer(ref buffLength, ref retValue);
                if (ret != 0)
                { throw new Exception("Error al llamar al metodo SgiIO.getbuffer : " + ret); }
                ioOut.Dispose();
                retValue = ToolSet.ShrinkBuffer(retValue, buffLength);
            }
            catch
            { throw; }
            finally
            {
                privk.Dispose();
                cert.Dispose();
                sobre.Dispose();
                if (ioIn != null) ioIn.Dispose();
                if (ioOut != null) ioOut.Dispose();
            }

            return retValue;
        }
        private static CriptoNet.SgiIO InitDecryptionCertificate()
        {
            CriptoNet.SgiIO ioIn = new CriptoNet.SgiIO();
            int ret = 0;
            byte[] DecryptCert = DecryptionCertificate;
            int DecryptCertLength = DecryptCert.Length;
            ret = ioIn.inicia(0, ref DecryptCert, ref DecryptCertLength);


            if (ret != 0) throw new Exception("Error al cargar el certificado de desencripción : " + ret);

            return ioIn;
        }

        private static CriptoNet.SgiIO InitDecryptionKey()
        {
            CriptoNet.SgiIO ioIn = new CriptoNet.SgiIO();
            int ret = 0;

            byte[] DecryptKey = DecryptionKey;
            int DecryptKeyLength = DecryptKey.Length;
            ret = ioIn.inicia(0, ref DecryptKey, ref DecryptKeyLength);


            if (ret != 0) throw new Exception("Error la llave privada de desencripción : " + ret);

            return ioIn;
        }

        public static byte[] DecryptionCertificate
        {
            get
            {
                String File = Parametros.DecryptionCertPath;
                FileStream s = new FileStream(File, FileMode.Open, FileAccess.Read);
                byte[] _DecryptionCert;
                using (BinaryReader br = new BinaryReader(s))
                { _DecryptionCert = br.ReadBytes((int)s.Length); }
                return _DecryptionCert;
            }
        }

        public static byte[] DecryptionKey
        {
            get
            {
                String File = Parametros.DecryptionKeyPath;
                FileStream s = new FileStream(File, FileMode.Open, FileAccess.Read);
                byte[] _DecryptionKey;
                using (BinaryReader br = new BinaryReader(s))
                { _DecryptionKey = br.ReadBytes((int)s.Length); }
                return _DecryptionKey;
            }
        }

        public long ProcessWithTempFiles(String rutaArchivo, String archOrigen)
        {
            try
            {
                string strOutPath = CriptoNetProcess(rutaArchivo);
                string extensionArch;

                string strNewName = "";

                extensionArch = archOrigen.Substring(archOrigen.Length - 3, 3);
                System.IO.FileInfo FileChecker = new FileInfo(rutaArchivo);
                if ((extensionArch.Equals("zip")) || (extensionArch.Equals("ZIP")))
                {
                    strNewName = Path.Combine(Parametros.RutaLocal, FileChecker.Name.Replace(".dec", ".zip").Replace(".DEC", ".zip"));
                }
                else
                {
                    strOutPath = this.UnZip(Environment.GetEnvironmentVariable("temp"), strOutPath, false, false);
                    strNewName = Path.Combine(Parametros.RutaLocal, FileChecker.Name.Replace(".dec", ".xml").Replace(".DEC", ".xml"));
                }
                if (System.IO.File.Exists(strNewName)) System.IO.File.Delete(strNewName);
                System.IO.File.Move(strOutPath, strNewName);
                FinalChecker = new FileInfo(strNewName);


                return FinalChecker.Length;

            }
            catch (Exception ex)
            {
                Utilerias.RegistrarLogEventos("Error: ", ex);


                string[] txtList = Directory.GetFiles(Environment.GetEnvironmentVariable("temp") + "\\", "*.dec");
                foreach (string f in txtList)
                {
                    File.Delete(f);
                }
                return 0;
            }
        }

        public static string CriptoNetProcess(string EnvelopePath)
        {
            CriptoNet.SgiLlavePriv privk = new CriptoNet.SgiLlavePriv();
            CriptoNet.SgiCertificado cert = new CriptoNet.SgiCertificado();
            CriptoNet.SgiSobre sobre = new CriptoNet.SgiSobre();
            CriptoNet.SgiIO ioIn = null;
            CriptoNet.SgiIO ioOut = null;
            int ret = 0;
            string retValue = string.Empty;

            try
            {
                ioIn = InitDecryptionCertificate();
                ret = cert.inicia(0, ioIn);
                if (ret != 0)
                { throw new Exception("Error al cargar el certificado de desencripción : " + ret); }

                ioIn.Dispose();

                ioIn = InitDecryptionKey();
                string pkPassword = Decrypt(Parametros.DecryptionPassword);
                ret = privk.inicia(ioIn, pkPassword, pkPassword.Length);
                if (ret != 0)
                { throw new Exception("Error al validar la contraseña de la llave privada : " + ret); }

                ret = privk.verificaLlaves(cert);
                if (ret != 0)
                { throw new Exception("La llave privada del destinatario no corresponde con el certificado :" + ret); }

                ioIn = new CriptoNet.SgiIO();
                ret = ioIn.inicia(0, EnvelopePath);
                if (ret == 0)
                {
                    ret = sobre.inicia(ioIn);
                    if (ret != 0)
                    { throw new Exception("Error al cargar el sobre : " + ret); }
                }
                else
                { throw new Exception("Error al iniciar el sobre : " + ret); }
                ioIn.Dispose();

                ioOut = new CriptoNet.SgiIO();
                Guid FileGuid = Guid.NewGuid();
                string strOpenedEnvelopePath = Path.Combine(Environment.GetEnvironmentVariable("temp"), FileGuid.ToString());

                ret = ioOut.inicia(1, strOpenedEnvelopePath);
                if (ret != 0)
                { throw new Exception("Error al iniciar archivo de datos desencriptados : " + ret); }

                ret = sobre.procesa(cert, privk, ref ioOut);
                if (ret != 0)
                { throw new Exception("Error al procesar el sobre : " + ret); }

                retValue = strOpenedEnvelopePath;
            }
            catch (Exception ex)
            {
                Utilerias.RegistrarLogEventos("Error: ", ex);
            }
            finally
            {
                privk.Dispose();
                cert.Dispose();
                sobre.Dispose();
                if (ioIn != null) ioIn.Dispose();
                if (ioOut != null) ioOut.Dispose();
            }

            return retValue;

        }
        private static string Decrypt(string strCoded)
        {
            string decryptionKey = Parametros.DecryptionKey;
            RC4CryptDecrypt _Crypt = new RC4CryptDecrypt();
            return _Crypt.Decode(strCoded, decryptionKey);
        }



        public string UnZip(string directorio, string zipFic, bool eliminar, bool renombrar)
        {
            // descomprimir el contenido de zipFic en el directorio indicado.
            // si zipFic no tiene la extensión .zip, se entenderá que es un directorio y
            // se procesará el primer fichero .zip de ese directorio.
            // si eliminar es True se eliminará ese fichero zip después de descomprimirlo.
            // si renombrar es True se añadirá al final .descomprimido
            //if (!zipFic.ToLower().EndsWith(".zip"))
            //{
            //    zipFic = Directory.GetFiles(zipFic, "*.zip")[0];
            //}
            // si no se ha indicado el directorio, usar el actual
            //if (directorio == "") directorio = ".";
            //
            string fileName = null;
            string rutaArchivoUnzip = null;

            using (ZipInputStream z = new ZipInputStream(File.OpenRead(zipFic)))
            {
                //int size = 2048;
                int size = 64000;
                byte[] data = null;
                ZipEntry theEntry;
                try
                {
                    while ((theEntry = z.GetNextEntry()) != null)
                    {
                        try
                        {
                            if (!(theEntry == null))
                            {
                                fileName = directorio + @"\" + Path.GetFileName(theEntry.Name);

                                if (!Directory.Exists(Path.GetDirectoryName(fileName))) Directory.CreateDirectory(Path.GetDirectoryName(fileName));
                                using (FileStream sWriter = File.Create(fileName))
                                {
                                    try
                                    {
                                        data = new byte[size];

                                        if (Path.GetExtension(fileName).ToUpper().Trim() == ".XML")
                                        {
                                            if (FileStrime_ReadTimeout > -1) sWriter.ReadTimeout = FileStrime_ReadTimeout;
                                        }

                                        int nR = 0;
                                        int sizeLectura = 0;
                                        while (true)
                                        {
                                            sizeLectura = z.Read(data, 0, data.Length);
                                            if ((sizeLectura > 0))
                                            {
                                                nR++;
                                                sWriter.Write(data, 0, size);
                                            }
                                            else { break; }
                                        };
                                    }
                                    catch (Exception ex)
                                    {
                                        Utilerias.RegistrarLogEventos("Error: ", ex);
                                    }
                                    finally
                                    {
                                        if (sWriter != null)
                                        {
                                            sWriter.Flush();
                                            sWriter.Close();
                                            sWriter.Dispose();
                                        }
                                        if (data != null)
                                        {
                                            data = null;
                                        }
                                    }
                                }

                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        { }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (z != null)
                    {
                        z.Close();
                        z.Dispose();
                    }
                }
            }

            if (File.Exists(fileName))
                rutaArchivoUnzip = fileName;

            //
            // cuando se hayan extraído los ficheros, renombrarlo
            if (renombrar)
            {
                File.Copy(zipFic, zipFic + ".descomprimido");
            }
            if (eliminar)
            {
                File.Delete(zipFic);
            }
            return rutaArchivoUnzip;
        }




    }
}