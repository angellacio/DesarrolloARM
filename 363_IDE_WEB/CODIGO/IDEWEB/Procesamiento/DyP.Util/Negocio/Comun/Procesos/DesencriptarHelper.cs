
//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Negocio.Comun.Procesos:DesencriptarHelper:0:21/Mayo/2008[SAT.DyP.Negocio.Comun.Procesos:1.1:21/Mayo/2008])

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using SAT.DyP.Util.Security;
using SAT.DyP.Util.Caching;
using SAT.DyP.Util.Configuration;
using SAT.DyP.Util.Logging;
using SAT.DyP.Util.Types;
using SAT.DyP.Util.Security.Interop.CryptDecrypt;
//using SAT.SCADE.NET.Common.Security.Interop.CryptDecrypt;
using SAT.DyP.Negocio.Comun.Tipos;
using SAT.DyP.Negocio.Comun.Procesos.SgiHelper;
using SAT.DyP.Util.Data;

namespace SAT.DyP.Negocio.Comun.Procesos
{
  public class DesencriptarHelper
  {

 
    
    private const string FIEL_ERROR_MESSAGE = @"La Operación no pudo ser realizada por: 1. Su certificado de FIEL fue revocado por usted o ha caducado, por lo cual lo invitamos a cualquiera de las oficinas de servicios al contribuyente del SAT para generar un nuevo certificado 2. Usted esta utilizando un certificado anterior al mas reciente por lo que lo invitamos a verificarlo";
    public static string ENCRYPTED_PATH = "SAT.DyP.Negocio.Declaraciones.DeclaraSAT.Tradicional::EncryptedFilesPath";
    private string DECRYPTED_PATH = "SAT.DyP.Negocio.Declaraciones.DeclaraSAT.Tradicional::DecryptedFilesPath";
    private string DECRYPT_CERTIFICATE_PATH = "SAT.DyP.Negocio.Declaraciones.DeclaraSAT.Tradicional::DecryptionCertificatePath";
    private string DECRIPT_PRIVATE_KEY_PATH = "SAT.DyP.Negocio.Declaraciones.DeclaraSAT.Tradicional::DecryptionPrivateKeyPath";
    private string DECRYPT_PASSWORD = "SAT.DyP.Negocio.Declaraciones.DeclaraSAT.Tradicional::DecryptionPassword";
    private string DECRYPT_KEY = "SAT.DyP.Negocio.Declaraciones.DeclaraSAT.Tradicional::DecryptionKey";
    private string CERTIFICATE_AUTHORITY = "SAT.DyP.Negocio.Declaraciones.DeclaraSAT.Tradicional::SATCertificateAuthority";
    private List<int> CodigosErrorNegocio = new List<int>(new int[] { 401, 403, 406, 407, 408, 410, 411, 215, 217 });

    public string ProcesarDocumento(Documento documento)
    {
      string contenidoDocumento = string.Empty;
      if (!SgiFactory.IsInProcessMode)
      {
        try
        {
          contenidoDocumento = SgiManager.Provider.DesencriptarDeclaracion(documento.NombreArchivo, documento.Contenido, documento.MedioPresentacion, documento.RfcAutenticacion, documento.Rfc);
        }
        catch (Exception ex)
        {
          throw new SAT.DyP.Util.Types.PlatformException(ex.Message);
        }
      }
      else
      {
        contenidoDocumento = ProcesarInProc(documento);
      }

      return contenidoDocumento;
    }

    public string DesencriptarDeclaracion(string NombreArchivo, string ContenidoDocumento, int MedioPresentacion, string rfcAutenticacion, string rfc)
    {
      NetSgiHelper cryptoHelper = new NetSgiHelper();
      string errorMessage = string.Empty;
      string ContenidoOut = string.Empty;
      string rfcError = string.Empty;
      string NoSerie = string.Empty;

      if (MedioDePresentacion.EsInternet(MedioPresentacion))
      { rfcError = rfcAutenticacion; }

      if (MedioDePresentacion.EsModuloSAT(MedioPresentacion))
      { rfcError = rfc; }

      try
      {
        // Validar que el archivo esté firmado con FIEL a partir del nombre.
        if (NombreArchivoContieneFirma(NombreArchivo))
        {
          try
          {
            NoSerie = cryptoHelper.ProcesaDeclaracion(Convert.FromBase64String(ContenidoDocumento));
          }
          catch (PlatformException)
          { throw; }
          catch (BusinessException ex)
          {
            //Se ajustó para que el error que se mande a la base de datos del monitoreo no tenga datos técnicos
            if (EsErrorNegocio(ex.LegacyErrorCode))
            {
              errorMessage = String.Format("Error al obtener del ARA los certificados vigentes para {0}", rfcError);
              throw new BusinessException(errorMessage);
            }
            else
            { throw new PlatformException(errorMessage, ex); }
          }

          if (NoSerie == string.Empty)
          {
            errorMessage = String.Format("No existen certificados vigentes para {0}.", rfcError);
            throw new BusinessException(errorMessage);
          }

          CertificateInfo CertInfo;
          byte[] CertificateBytes;
          try
          {
              /// Mejoras MS. DAC MAR 2011 
            CertificateBytes = cryptoHelper.GetCertificateBySerialNumber(NoSerie);
            CertInfo = cryptoHelper.CertInfo;
            /// FIN-Mejoras MS. DAC MAR 2011 

          }
          catch (BusinessException ex)
          {
            errorMessage = String.Format("No se pudo obtener el certificado por el numero de serie: '{0}'.", NoSerie);
            if (EsErrorNegocio(ex.LegacyErrorCode))
            { throw new BusinessException(errorMessage, ex); }
            else
            { throw new PlatformException(errorMessage, ex); }
          }

          if (CertInfo.State != CertificateState.Active)
          {
            errorMessage = String.Format(FIEL_ERROR_MESSAGE, NoSerie);
            throw new BusinessException(errorMessage);
          }

          string rfcDelCertificado = cryptoHelper.GetCertificateTaxPayerID(CertificateBytes);
          rfcDelCertificado = String.IsNullOrEmpty(rfcDelCertificado) ? String.Empty : rfcDelCertificado;

          if (!rfcDelCertificado.Equals(rfc))
          {
            errorMessage = String.Format("El RFC firmante '{0}' no corresponde con el RFC autenticado '{1}'.",
            rfcDelCertificado, rfcError);
            throw new BusinessException(errorMessage);
          }
        }

        ContenidoOut = DecryptBase64Text(ref cryptoHelper, ContenidoDocumento, rfc);
      }
      finally
      {
        if (cryptoHelper != null)
        { cryptoHelper.Dispose(); }
      }

      return ContenidoOut;
    }

    public string ProcesarInProc(Documento documento)
    {
      NetSgiHelper cryptoHelper = new NetSgiHelper();
      string errorMessage = string.Empty;
      string rfcError = string.Empty;
      string NoSerie = string.Empty;

      if (MedioDePresentacion.EsInternet(documento.MedioPresentacion))
      { rfcError = documento.RfcAutenticacion; }

      if (MedioDePresentacion.EsModuloSAT(documento.MedioPresentacion))
      { rfcError = documento.Rfc; }

      try
      {
        // Validar que el archivo esté firmado con FIEL a partir del nombre.
        if (NombreArchivoContieneFirma(documento.NombreArchivo))
        {
          // NOTA DE IMPLEMENTACÍON: Aqui se baja el archivo a disco porque GetCertificateSerialNumbersFromPkcs7File de SgiCripto 
          // (API del ARA) necesita como argumento la ruta de un archivo del cual lee el contenido.
          // Esta es una consecuencia de diseño del API del ARA que puede tener costo en desempeño. Considerar cambiar por 
          // una implementación que use una API nueva del ARA que pueda recibir directamente el contenido del archivo como byte[].

          //string originalFilePath = DumpFileToDisk(documento);

          List<string> certificateSerialNumbers = null;
          try
          {
            //certificateSerialNumbers = cryptoHelper.GetCertificateSerialNumbersFromPkcs7(Convert.FromBase64String(documento.Contenido));
            NoSerie = cryptoHelper.ProcesaDeclaracion(Convert.FromBase64String(documento.Contenido));
          }
          catch (PlatformException)
          { throw; }
          catch (BusinessException ex)
          {
            //Se ajustó para que el error que se mande a la base de datos del monitoreo no tenga datos técnicos
            if (EsErrorNegocio(ex.LegacyErrorCode))
            {
              errorMessage = String.Format("Error al obtener del ARA los certificados vigentes para {0}", rfcError);
              throw new BusinessException(errorMessage);
            }
            else
            { throw new PlatformException(errorMessage, ex); }
          }

          if (NoSerie == string.Empty)
          {
            errorMessage = String.Format("No existen certificados vigentes para {0}.", rfcError);
            throw new BusinessException(errorMessage);
          }

          CertificateInfo certificateInfo;
          byte[] certificateBytes;
          try
          {
            /// Mejoras MS. DAC MAR 2011 
            certificateBytes = cryptoHelper.GetCertificateBySerialNumber(NoSerie);
            certificateInfo = cryptoHelper.CertInfo;
            /// FIN-Mejoras MS. DAC MAR 2011 

          }
          catch (BusinessException ex)
          {
            errorMessage = String.Format("No se pudo obtener el certificado por el numero de serie: '{0}'.", certificateSerialNumbers[0]);
            if (EsErrorNegocio(ex.LegacyErrorCode))
            { throw new BusinessException(errorMessage, ex); }
            else
            { throw new PlatformException(errorMessage, ex); }
          }

          //if (certificateInfo.State != CertificateState.Active)
          //{
          //    errorMessage = String.Format(FIEL_ERROR_MESSAGE, certificateSerialNumbers[0]);
          //    throw new BusinessException(errorMessage);
          //}

          errorMessage = ValidateCertificateExpirationDate(certificateInfo.ValidityEnd, documento.FechaPresentacion);
          if (!string.IsNullOrEmpty(errorMessage))
          { throw new BusinessException(errorMessage); }

          if (certificateInfo.Type != CertificateType.ElectronicSignature)
          {
            errorMessage = String.Format("El certificado '{0}' no es de tipo válido.", certificateSerialNumbers[0]);
            throw new BusinessException(errorMessage);
          }

          string rfcDelCertificado = cryptoHelper.GetCertificateTaxPayerID(certificateBytes);
          rfcDelCertificado = String.IsNullOrEmpty(rfcDelCertificado) ? String.Empty : rfcDelCertificado;

          //Se agregó una validación para saber que el RFC firmado es válido.
          if (!RFC.Validate(documento.Rfc.Trim()))
          {
            errorMessage = string.Format("El RFC {0} no es válido", documento.Rfc);
            EventLogHelper.WriteErrorEntry(errorMessage, IdentificadorEvento.DECLARASAT_COMUN_EVENT_ID);
            throw new BusinessException(errorMessage);
          }

          if (!rfcDelCertificado.Contains(documento.Rfc.Trim()))
          {
            errorMessage = String.Format("El RFC firmante '{0}' no corresponde con el RFC autenticado '{1}'.",
            rfcDelCertificado, rfcError);
            throw new BusinessException(errorMessage);
          }
        }

        // Desencriptar el contenido del documento y reemplazar los datos del mensaje con
        // el contenido desencriptado.

        documento.Contenido = DecryptBase64Text(ref cryptoHelper, documento.Contenido, documento.Rfc);
      }
      finally
      {
        if (cryptoHelper != null)
        { cryptoHelper.Dispose(); }
      }

      return documento.Contenido;
    }

    /// <summary>
    /// Valida la vigencia de un certificado sumando n dias a la fecha de expiración.
    /// </summary>
    /// <param name="expirationDate">Fecha de expiración del certificado.</param>
    /// <param name="days">Dias que se agregan para extender la fecha de expiración del certificado.</param>
    /// <returns>Devuelve una cadena vacia o nulo si el certificado no ha expirado.</returns>
    public static string ValidateCertificateExpirationDate(DateTime expirationDate)
    {
      if (DateTime.Now < expirationDate)
        return string.Empty;
      else
        return String.Format(FIEL_ERROR_MESSAGE, expirationDate);
    }

    public static string ValidateCertificateExpirationDate(DateTime expirationDate, DateTime FechaPresentacion)
    {
      if (FechaPresentacion < expirationDate)
        return string.Empty;
      else
        return String.Format(FIEL_ERROR_MESSAGE, expirationDate);
    }

    private static void BorrarArchivoCertificado(string originalFilePath)
    {
      try
      {
        File.Delete(originalFilePath);
      }
      catch
      { ;}
    }

    private string DumpFileToDisk(string NombreArchivo, string Contenido)
    {
      string encryptedFilePath = string.Empty;
      try
      {
        string encriptedFilesTemporaryPath = GetEncryptedFilesTemporaryPath();
        encryptedFilePath = Path.Combine(encriptedFilesTemporaryPath, NombreArchivo);
        byte[] binaryContents = Convert.FromBase64String(Contenido);
        File.WriteAllBytes(encryptedFilePath, binaryContents);
      }
      catch (Exception ex)
      {
        throw new PlatformException(string.Format("Error al escribir la declaracion en la ruta '{0}'", encryptedFilePath), ex);
      }
      return encryptedFilePath;
    }

    private string DumpFileToDisk(Documento documento)
    {
      string encryptedFilePath = string.Empty;
      try
      {
        string encriptedFilesTemporaryPath = GetEncryptedFilesTemporaryPath();
        encryptedFilePath = Path.Combine(encriptedFilesTemporaryPath, documento.NombreArchivo);
        byte[] binaryContents = Convert.FromBase64String(documento.Contenido);
        File.WriteAllBytes(encryptedFilePath, binaryContents);
      }
      catch (Exception ex)
      {
        throw new PlatformException(string.Format("Error al escribir la declaracion en la ruta '{0}'", encryptedFilePath), ex);
      }
      return encryptedFilePath;
    }

    private void TempDumpFileToDisk(byte[] encryptedContentBytes)
    {
      try
      {
        string fileName = string.Format("{0}.dec", Guid.NewGuid().ToString());
        File.WriteAllBytes("C:\\SCADE_TEST\\DEC_FILES\\" + fileName, encryptedContentBytes);
      }
      catch
      { ; }
    }

    public string DecryptBase64Text(ref NetSgiHelper CryptoHelper, string base64EncodedEncryptedContents, string rfc)
    {
      string decryptionCertificatePath = null;
      string decryptionPrivateKeyPath = null;
      string decryptionPassword = null;
      string certificateSATPath = null;

      Byte[] encryptedContentBytes = Convert.FromBase64String(base64EncodedEncryptedContents);

      if (!File.Exists((decryptionCertificatePath = GetDecryptionCertificatePath())))
        throw new PlatformException(String.Format("No se encontro la llave publica de desencripcion ({0}).", decryptionCertificatePath));

      if (!File.Exists((decryptionPrivateKeyPath = GetDecryptionPrivateKeyPath())))
        throw new PlatformException(String.Format("No se encontro la llave privada de desencripcion ({0}).", decryptionPrivateKeyPath));

      if (!File.Exists((certificateSATPath = GetCertificateSATPath())))
        throw new PlatformException(String.Format("No se encontro el certificado de la Entidad Certificadora del SAT ({0}).", certificateSATPath));

      if (String.IsNullOrEmpty(decryptionPassword = GetDecryptionPassword()))
        throw new PlatformException("La contraseña de desencripcion tiene un valor invalido (nulo/vacio).");

      Byte[] decryptedBytes = null;

      try
      {
        Stopwatch timer = new Stopwatch();
        timer.Start();

        decryptedBytes = CryptoHelper.ProcessPkcs7FileInfo(decryptionCertificatePath,
                                                           decryptionPrivateKeyPath,
                                                           decryptionPassword,
                                                           encryptedContentBytes);
        timer.Stop();

        // TODO: envial los timer.EllapsedMilliseconds a log o a un evento de WMI.
        Trace.WriteLine(String.Format("Tiempo de respuesta del ARA al desencriptar archivo: {0} milisegundos.", timer.ElapsedMilliseconds));
      }
      catch (SecurityException ex)
      {
        //Ajuste para mandar al Evento log el error tecnco y a la BD sólo el error descriptivo
        string errorMessage = "Error al desencriptar contenido de documento recibido, para el rfc " + rfc;
        EventLogHelper.WriteErrorEntry(string.Format("ERR_SECURITY= {0}: {1}", errorMessage, ex.Message), CoreLogEventIdentifier.SGIHELPER_EVENT_ID);
        throw new BusinessException(errorMessage, ex);
      }
      catch (BusinessException ex)
      {
        string errorMessage = "Error al desencriptar contenido de documento recibido, del RFC: " + rfc;

        if (EsErrorNegocio(ex.LegacyErrorCode))
          throw new BusinessException(errorMessage, ex);
        else
          throw new PlatformException(errorMessage, ex);
      }

      if (decryptedBytes == null || decryptedBytes.Length == 0)
      {
        string errorMessage = String.Format("Los datos devueltos por el ARA al desencriptar el archivo son vacíos.");
        throw new BusinessException(errorMessage);
      }

      MemoryStream stream = new MemoryStream(decryptedBytes);
      StreamReader reader = new StreamReader(stream, Encoding.GetEncoding("ISO-8859-1"));
      string decryptedText = reader.ReadToEnd();
      reader.Close();

      return decryptedText;
    }

    private bool EsErrorNegocio(int codigoError)
    {
      return CodigosErrorNegocio.Contains(codigoError);
    }

    private string ReadFromSettings(string key)
    {
      string _settingValue = string.Empty;

      if (CachingService.Contains(key))
      {
        _settingValue = CachingService.GetItem(key).ToString();
      }
      else
      {
        _settingValue = ConfigurationManager.ApplicationSettings.ReadSetting(key);
        CachingService.AddItem(key, _settingValue);
      }

      return _settingValue;
    }

    private string GetEncryptedFilesTemporaryPath()
    {
      string configKey = ENCRYPTED_PATH;
      string settingValue = ReadFromSettings(configKey);
      return settingValue;
    }

    private string GetDecryptedFilesPath()
    {
      string configKey = DECRYPTED_PATH;
      string settingValue = ReadFromSettings(configKey);
      return settingValue;
    }

    private string GetDecryptionPassword()
    {
      string configKey = DECRYPT_PASSWORD;
      string settingValue = ReadFromSettings(configKey);

      string configurationDecryptionKey = ReadFromSettings(DECRYPT_KEY);
      CCryptDecrypt cryptoHelper = new CCryptDecryptClass();
      try
      {
        string clearTextPassword = cryptoHelper.Decripta(ref settingValue, ref configurationDecryptionKey);
        return clearTextPassword;
      }
      catch (Exception ex)
      {

        throw new PlatformException(string.Format("Error al desencriptar el valor de la llave '{0}'", DECRYPT_KEY), ex);
      }
    }

    private object PlatformException(string p, object exception)
    {
      throw new Exception("The method or operation is not implemented.");
    }

    private string GetDecryptionPrivateKeyPath()
    {
      string configKey = DECRIPT_PRIVATE_KEY_PATH;
      string settingValue = ReadFromSettings(configKey);
      return settingValue;
    }

    private string GetDecryptionCertificatePath()
    {
      string configKey = DECRYPT_CERTIFICATE_PATH;
      string settingValue = ReadFromSettings(configKey);
      return settingValue;
    }

    private string GetCertificateSATPath()
    {
      string configKey = CERTIFICATE_AUTHORITY;
      string settingValue = ReadFromSettings(configKey);
      return settingValue;
    }

    public bool NombreArchivoContieneFirma(string filename)
    {
      return filename.ToUpper().EndsWith("6.DEC");
    }
  }
}
