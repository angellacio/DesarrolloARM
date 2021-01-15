using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using CriptoNet = mx.gob.sat.sgi.SgiCripto;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace ProDec_ReglaNegocio.Proceso
{
    public static class EncriptDecriptArchivo
    {
        public enum ArchivoEncript
        {
            Key = 0,
            Cer = 1
        }

        static string _sArchivoOriginal;
        static string _sArchivoEncriptaBD;

        private static string sRutaFSH { get; set; }
        private static int FileStrime_ReadTimeout
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
        private static string sArchivoOriginal
        {
            get
            {
                return string.Format(@"{0}\{1}", sRutaFSH, _sArchivoOriginal);
            }
        }
        private static string sArchivoFileShale
        {
            get
            {
                string sArchivo = _sArchivoEncriptaBD.Replace(sArchivoEncripta_Ext, ".dec");

                return string.Format(@"{0}\{1}", sRutaFSH, sArchivo);
            }
        }
        private static string sArchivoSinExt
        {
            get
            {
                string sArchivo = _sArchivoEncriptaBD.Replace(sArchivoEncripta_Ext, "");

                return string.Format(@"{0}\{1}", sRutaFSH, sArchivo);
            }
        }
        private static string sArchivoDesincriptado
        {
            get
            {
                string sArchivo = _sArchivoEncriptaBD.Replace(sArchivoEncripta_Ext, "");

                return string.Format(@"{0}\{1}", sRutaFSH, sArchivo);
            }
        }
        private static string sArchivoOriginal_Ext
        {
            get
            {
                return Path.GetExtension(_sArchivoOriginal);
            }
        }
        private static string sArchivoEncripta_Ext
        {
            get
            {
                return Path.GetExtension(_sArchivoEncriptaBD);
            }
        }

        private static CriptoNet.SgiIO sgiDecryption(ArchivoEncript TipArchivo)
        {
            CriptoNet.SgiIO ioIn = null;
            int nDecryptLength = -1, nRet = -1;
            FileStream fsArchivo = null;
            byte[] bDecrypt = null;

            ioIn = new CriptoNet.SgiIO();

            if (TipArchivo == ArchivoEncript.Key)
            {
                fsArchivo = new FileStream(ConectBD.Parametros.DecryptionKeyPath, FileMode.Open, FileAccess.Read);
            }
            if (TipArchivo == ArchivoEncript.Cer)
            {
                fsArchivo = new FileStream(ConectBD.Parametros.DecryptionCertPath, FileMode.Open, FileAccess.Read);
            }
            using (BinaryReader br = new BinaryReader(fsArchivo))
            {
                bDecrypt = br.ReadBytes((int)fsArchivo.Length);
                br.Close();
            }

            fsArchivo.Close();
            fsArchivo.Dispose();

            nDecryptLength = bDecrypt.Length;
            nRet = ioIn.inicia(0, ref bDecrypt, ref nDecryptLength);

            if (TipArchivo == ArchivoEncript.Key)
            {
                if (nRet != 0) throw new ApplicationException("Error la llave privada de desencripción : " + nRet);
            }

            if (TipArchivo == ArchivoEncript.Cer)
            {
                if (nRet != 0) throw new ApplicationException("Error al cargar el certificado de desencripción : " + nRet);
            }

            return ioIn;
        }

        private static void CriptoNetProcess()
        {
            CriptoNet.SgiCertificado sgiCert = null;
            CriptoNet.SgiLlavePriv sgiPrivK = null;
            CriptoNet.SgiSobre sgiSobre = null;
            CriptoNet.SgiIO ioIn = null;
            CriptoNet.SgiIO ioOut = null;
            CriptoNet.SgiUtils sgiUtil = null;
            int ret = 0;
            string pkPassword = "", decryptionKey = "";
            RC4CryptDecrypt _Crypt = null;
            try
            {
                sgiUtil = new CriptoNet.SgiUtils();
                _Crypt = new RC4CryptDecrypt();
                sgiCert = new CriptoNet.SgiCertificado();
                sgiPrivK = new CriptoNet.SgiLlavePriv();
                sgiSobre = new CriptoNet.SgiSobre();

                decryptionKey = ConectBD.Parametros.DecryptionKey;
                pkPassword = _Crypt.Decode(ConectBD.Parametros.DecryptionPassword, decryptionKey);

                ioIn = sgiDecryption(ArchivoEncript.Cer);
                ret = sgiCert.inicia(0, ioIn);
                if (ret != 0) throw new ApplicationException(string.Format("Error al cargar el certificado de desencripción : {0} - {1}", ret, "{0}"));
                ioIn.Dispose();

                ioIn = sgiDecryption(ArchivoEncript.Key);
                ret = sgiPrivK.inicia(ioIn, pkPassword, pkPassword.Length);
                if (ret != 0) throw new ApplicationException(string.Format("Error al validar la contraseña de la llave privada : {0} - {1}", ret, "{0}"));

                ret = sgiPrivK.verificaLlaves(sgiCert);
                if (ret != 0)
                    throw new ApplicationException(string.Format("La llave privada del destinatario no corresponde con el certificado : {0} - {1}", ret, "{0}"));

                ioIn = new CriptoNet.SgiIO();
                ret = ioIn.inicia(0, sArchivoFileShale);
                if (ret == 0)
                {
                    ret = sgiSobre.inicia(ioIn);
                    if (ret != 0)
                        throw new ApplicationException(string.Format("Error al cargar el sobre : {0} - {1}", ret, "{0}"));
                }
                else
                    throw new ApplicationException(string.Format("Error al iniciar el sobre : {0} - {1}", ret, "{0}"));
                ioIn.Dispose();

                ioOut = new CriptoNet.SgiIO();
                Guid FileGuid = Guid.NewGuid();

                ret = ioOut.inicia(1, sArchivoSinExt);
                if (ret != 0)
                    throw new ApplicationException(string.Format("Error al iniciar archivo de datos desencriptados : {0} - {1}", ret, "{0}"));

                ret = sgiSobre.procesa(sgiCert, sgiPrivK, ref ioOut);
                if (ret != 0)
                    throw new ApplicationException(string.Format("Error al procesar : {0} - {1}", ret, "{0}"));
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(string.Format(ex.Message, sgiUtil.msgError(ret)), ex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sgiPrivK.Dispose();
                sgiCert.Dispose();
                sgiSobre.Dispose();
                if (ioIn != null) ioIn.Dispose();
                if (ioOut != null) ioOut.Dispose();
            }
        }

        private static void UnZip(bool eliminar)
        {
            // descomprimir el contenido de zipFic en el directorio indicado. Si zipFic no tiene la extensión .zip, se entenderá que es un directorio y se procesará el primer fichero .zip de ese directorio.
            // Si eliminar es True se eliminará ese fichero zip después de descomprimirlo. Si renombrar es True se añadirá al final .descomprimido
            //if (!zipFic.ToLower().EndsWith(".zip"))
            //{
            //    zipFic = Directory.GetFiles(zipFic, "*.zip")[0];
            //}
            // si no se ha indicado el directorio, usar el actual
            //if (directorio == "") directorio = ".";
            //
            string fileName = null;

            using (ZipInputStream z = new ZipInputStream(File.OpenRead(sArchivoSinExt)))
            {
                int size = 2048;
                //int size = 64000;
                byte[] dataGen = null;
                ZipEntry theEntry;
                try
                {
                    while ((theEntry = z.GetNextEntry()) != null)
                    {
                        try
                        {
                            if (!(theEntry == null))
                            {
                                fileName = string.Format(@"{0}\{1}", Environment.GetEnvironmentVariable("temp"), Path.GetFileName(theEntry.Name));
                                if (Path.GetExtension(theEntry.Name).ToUpper().Trim() == ".XML")
                                {
                                    fileName = sRutaFSH + @"\" + _sArchivoEncriptaBD;
                                }

                                if (!Directory.Exists(Path.GetDirectoryName(fileName))) Directory.CreateDirectory(Path.GetDirectoryName(fileName));
                                using (FileStream sWriter = File.Create(fileName))
                                {
                                    try
                                    {
                                        if (Path.GetExtension(fileName).ToUpper().Trim() == ".XML")
                                        {
                                            if (FileStrime_ReadTimeout > -1) sWriter.ReadTimeout = FileStrime_ReadTimeout;
                                        }

                                        int nR = 0;
                                        int sizeLectura = 0;
                                        while (true)
                                        {
                                            dataGen = new byte[size];
                                            sizeLectura = z.Read(dataGen, 0, dataGen.Length);

                                            byte[] data = null;

                                            if (dataGen.Length == sizeLectura) data = dataGen;
                                            else
                                            {
                                                data = new byte[sizeLectura];

                                                for (int nRA = 0; nRA < dataGen.Length; nRA++)
                                                {
                                                    if ((data.Length) == nRA) break;
                                                    data[nRA] = dataGen[nRA];
                                                }
                                            }

                                            if ((sizeLectura > 0))
                                            {
                                                nR++;
                                                sWriter.Write(data, 0, sizeLectura);
                                            }
                                            else { break; }
                                        };
                                    }
                                    catch (Exception ex)
                                    {
                                        ManejoErrores.MensajeError("Error", ex);
                                    }
                                    finally
                                    {
                                        if (sWriter != null)
                                        {
                                            sWriter.Flush();
                                            sWriter.Close();
                                            sWriter.Dispose();
                                        }
                                        if (dataGen != null)
                                        {
                                            dataGen = null;
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

            //
            // cuando se hayan extraído los ficheros, renombrarlo
            //if (renombrar) File.Copy(sArchivoSinExt, sArchivoSinExt + ".descomprimido");

            if (eliminar) File.Delete(sArchivoSinExt);
        }

        public static void DesencriptaDeclaracion(string sArchOrigen, string sArchivoEncrip, string sFH)
        {
            _sArchivoOriginal = sArchOrigen;
            _sArchivoEncriptaBD = sArchivoEncrip;
            sRutaFSH = sFH;

            CriptoNetProcess();

            if (sArchivoEncripta_Ext.Trim().ToUpper() != ".ZIP")
            {
                UnZip(true);
            }
            else
            {
                File.Copy(sArchivoSinExt, string.Format("{0}.zip", sArchivoSinExt));
                File.Delete(sArchivoSinExt);
            }

        }
    }
}
