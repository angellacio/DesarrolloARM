//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Util.Web.Security:CertificateHelper:0:21/May/2008[SAT.DyP.Util.Web:1.0:21/May/2008])
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Text.RegularExpressions;
using SAT.DyP.Presentacion.Seguridad.Membresia;
using SAT.DyP.Util.Web.Page;
using SAT.DyP.Presentacion.Seguridad.Membresia.wsIDCValidate;
using SAT.DyP.Util.Logging;
using SAT.DyP.Util.Types;

namespace SAT.DyP.Util.Web.Security
{
  public class CertificateHelper
  {
    //private const string FIEL_EXPIRED_MESSAGE = @"Su certificado digital expir� el d�a {0:dd/MM/yyyy}. Puede renovarlo ingresando a la opci�n ""Renovaci�n de certificados en l�nea"" dentro de Firma Electr�nica Avanzada (FIEL).";
    //private const string INACTIVE_CERTIFICATE = @"Su certificado digital no se encuentra activo. Puede renovarlo ingresando a la opci�n ""Renovaci�n de certificados en l�nea"" dentro de Firma Electr�nica Avanzada (FIEL).";
    private const string FIEL_EXPIRED_MESSAGE = @"La Operaci�n no pudo ser realizada por:\n\n 1. Su certificado de FIEL fue revocado por usted o ha caducado, por lo cual lo invitamos a cualquiera de las oficinas de servicios al contribuyente del SAT para generar un nuevo certificado.\n\n 2. Usted esta utilizando un certificado anterior al mas reciente por lo que lo invitamos a verificarlo";
    private const string INACTIVE_CERTIFICATE = @"La Operaci�n no pudo ser realizada por:\n\n 1. Su certificado de FIEL fue revocado por usted o ha caducado, por lo cual lo invitamos a cualquiera de las oficinas de servicios al contribuyente del SAT para generar un nuevo certificado.\n\n 2. Usted esta utilizando un certificado anterior al mas reciente por lo que lo invitamos a verificarlo";
    private const string INVALID_CERTIFICATE_TYPE = @"El tipo del certificado proporcionado es inv�lido.";
    private const string INVALID_SERIAL_NUMBER = @"El n�mero de serie del certificado es inv�lido.";

 
    
    /// <summary>
    /// Indica si un certificado esta vigente
    /// </summary>
    /// <param name="serialNumber">N�mero de Seria</param>
    /// <param name="Message">Mensaje de retorno</param>
    /// <returns></returns>
    public static bool IsExpired(string serialNumber, out string Message)
    {
      Message = string.Empty;

      if (!IsValidSerialNumber(serialNumber))
      {
        Message = INVALID_SERIAL_NUMBER;
        return false;
      }

      try
      {
        IDCMembershipProvider customProvider = Membership.Provider as IDCMembershipProvider;

        Nullable<DateTime> expirationDate = customProvider.GetFIELExpirationDate(serialNumber);
        if (expirationDate != null)
        {
          if (DateTime.Now < expirationDate.Value)
          {
            return false;
          }
          else
          {
            Message = String.Format(FIEL_EXPIRED_MESSAGE, expirationDate);
            return true;
          }
        }
      }
      catch (Exception ex)
      {
        throw new CertificateValidationException(ex.Message);
      }

      Message = "No se pudo validar la vigencia de su Firma Electr�nica Avanzada.";

      return false;
    }

    /// <summary>
    /// Valida la vigencia de un certificado en base a su fecha de expiraci�n.
    /// </summary>
    /// <param name="expirationDate">Fecha de expiraci�n del certificado.</param>
    /// <returns>Devuelve una cadena vacia o nulo si el certificado no ha expirado.</returns>
    public static string ValidateCertificateExpirationDate(Nullable<DateTime> expirationDate)
    {
      string message = string.Empty;

      if (expirationDate != null)
      {
        if (DateTime.Now < expirationDate.Value)
        {
          return string.Empty;
        }
        else
        {
          message = String.Format(FIEL_EXPIRED_MESSAGE, expirationDate);
          return message;
        }
      }
      message = "No se pudo validar la vigencia de su Firma Electr�nica Avanzada.";
      return message;
    }

    /// <summary>
    /// Valida la vigencia de un certificado sumando n dias a la fecha de expiraci�n.
    /// </summary>
    /// <param name="expirationDate">Fecha de expiraci�n del certificado.</param>
    /// <param name="days">Dias que se agregan para extender la fecha de expiraci�n del certificado.</param>
    /// <returns>Devuelve una cadena vacia o nulo si el certificado no ha expirado.</returns>
    public static string ValidateCertificateExpirationDate(DateTime expirationDate, int days)
    {
      if (DateTime.Now < expirationDate.AddDays(days))
      {
        return string.Empty;
      }
      else
      {
        return String.Format(FIEL_EXPIRED_MESSAGE, expirationDate);
      }
    }

    /// <summary>
    /// Valida si un certificado es de tipo FIEL
    /// </summary>
    /// <param name="serialNumber">N�mero de Serie</param>
    /// <param name="message">Mensaje de respuesta</param>
    /// <returns>Sino es un certificado FIEL valido devuelve un mensaje de error, de lo contrario regresa 'Nulo'.</returns>
    public static string IsValidFIELCertificate(string serialNumber)
    {
      if (!IsValidSerialNumber(serialNumber))
      {
        return INVALID_SERIAL_NUMBER;
      }

      try
      {
        IDCMembershipProvider customProvider = Membership.Provider as IDCMembershipProvider;
        FIELCertificateInfo certificateInfo = customProvider.GetFIELCertificateInfo(serialNumber);

        if (!certificateInfo.IsActive)
        {
          return INACTIVE_CERTIFICATE;
        }

        if (!certificateInfo.IsFielCertificate)
        {
          return INVALID_CERTIFICATE_TYPE;
        }

        return ValidateCertificateExpirationDate(certificateInfo.ExpirationDate);
      }
      catch (Exception ex)
      {
        throw new CertificateValidationException(ex.Message);
      }
    }


    /// <summary>
    /// Valida si un certificado es de tipo FIEL (se permite solo por compatibilidad)
    /// </summary>
    /// <param name="serialNumber">N�mero de Serie</param>
    /// <param name="message">Mensaje de respuesta</param>
    /// <returns>Devuelve true si es un certificado de tipo FIEL, de lo contrario false.</returns>
    [Obsolete("MS codereview Feb. 2010. Debe utilizar el m�todo 'IsValidFIELCertificate(string, int)' ")]
    public static bool IsValidFIELCertificate(string serialNumber, out string message)
    {
      message = string.Empty;
      bool result = true;

      if (!IsValidSerialNumber(serialNumber))
      {
        message = INVALID_SERIAL_NUMBER;
        return false;
      }

      try
      {
        IDCMembershipProvider customProvider = Membership.Provider as IDCMembershipProvider;
        FIELCertificateInfo certificateInfo = customProvider.GetFIELCertificateInfo(serialNumber);
        if (!certificateInfo.IsActive)
        {
          message = INACTIVE_CERTIFICATE;
          return false;
        }

        if (!certificateInfo.IsFielCertificate)
        {
          message = INVALID_CERTIFICATE_TYPE;
          return false;
        }

        message = ValidateCertificateExpirationDate(certificateInfo.ExpirationDate);
        if (!string.IsNullOrEmpty(message))
          return false;

      }
      catch (Exception ex)
      {
        throw new CertificateValidationException(ex.Message);
      }

      return result;
    }

    /// <summary>
    /// Valida si un certificado es de tipo FIEL.
    /// </summary>
    /// <param name="serialNumber">Numero de serie del certificado.</param>
    /// <param name="days">Dias que se agregan para extender la fecha de expiraci�n del certificado.</param>
    /// <param name="message">Mensaje de respuesta</param>
    /// <returns>Devuelve true si es un certificado de tipo FIEL, de lo contrario false.</returns>
    [Obsolete("MS codereview Feb. 2010. Debe utilizar el m�todo 'IsValidFIELCertificate(string, int)'")]
    public static bool IsValidFIELCertificate(string serialNumber, int days, out string message)
    {
      message = string.Empty;

      if (!IsValidSerialNumber(serialNumber))
      {
        message = INVALID_SERIAL_NUMBER;
        return false;
      }

      try
      {
        IDCMembershipProvider customProvider = Membership.Provider as IDCMembershipProvider;
        FIELCertificateInfo certificateInfo = customProvider.GetFIELCertificateInfo(serialNumber);

        if (!certificateInfo.IsActive)
        {
          message = INACTIVE_CERTIFICATE;
          return false;
        }

        if (!certificateInfo.IsFielCertificate)
        {
          message = INVALID_CERTIFICATE_TYPE;
          return false;
        }

        message = ValidateCertificateExpirationDate(certificateInfo.ExpirationDate, days);
      }
      catch (Exception ex)
      {
        throw new CertificateValidationException(ex.Message);
      }

      return String.IsNullOrEmpty(message);
    }

    /// <summary>
    /// Valida si un certificado es de tipo FIEL.
    /// </summary>
    /// <param name="serialNumber">N�mero de serie del certificado.</param>
    /// <param name="days">D�as que se agregan para extender la fecha de expiraci�n del certificado.</param>
    /// <returns>Devuelve null en caso de ser un certificaco de tipo FIEL v�lido, de lo contrario, el mensaje de respuesta.</returns>
    public static string IsValidFIELCertificate(string serialNumber, int days)
    {
      if (!IsValidSerialNumber(serialNumber))
        return INVALID_SERIAL_NUMBER;

      try
      {
        IDCMembershipProvider customProvider = Membership.Provider as IDCMembershipProvider;
        FIELCertificateInfo certificateInfo = customProvider.GetFIELCertificateInfo(serialNumber);

        if (!certificateInfo.IsActive)
          return INACTIVE_CERTIFICATE;

        if (!certificateInfo.IsFielCertificate)
          return INVALID_CERTIFICATE_TYPE;

        string message = ValidateCertificateExpirationDate(certificateInfo.ExpirationDate, days);
        if (!String.IsNullOrEmpty(message))
          return message;
      }
      catch (Exception ex)
      { throw new CertificateValidationException(ex.Message); }

      return null;
    }

    ///--> FSM: Recomendaciones MS
    /// <summary>
    /// Valida si un certificado es de tipo Autenticaci�n
    /// </summary>
    /// <param name="serialNumber">N�mero de serie</param>
    /// <param name="message">Mensaje de respuesta</param>
    /// <returns>Devuelve true si es un certificado de tipo Autenticaci�n, de lo contrario false.</returns>
    [Obsolete("MS codereview Feb. 2010. Debe utilizar el m�todo 'IsValidAutenticationCertificate(string)'")]
    public static bool IsValidAutenticationCertificate(string serialNumber, out string message)
    {
      message = string.Empty;
      bool result = true;

      if (!IsValidSerialNumber(serialNumber))
      {
        message = INVALID_SERIAL_NUMBER;
        return false;
      }

      try
      {
        IDCMembershipProvider customProvider = Membership.Provider as IDCMembershipProvider;
        FIELCertificateInfo certificateInfo = customProvider.GetFIELCertificateInfo(serialNumber);
        if (!certificateInfo.IsActive)
        {
          message = INACTIVE_CERTIFICATE;
          return false;
        }

        if (!certificateInfo.IsAutCertificate)
        {
          message = INVALID_CERTIFICATE_TYPE;
          return false;
        }

        message = ValidateCertificateExpirationDate(certificateInfo.ExpirationDate);
        if (!string.IsNullOrEmpty(message))
          return false;

      }
      catch (Exception ex)
      {
        throw new CertificateValidationException(ex.Message);
      }

      return result;
    }

    /// <summary>
    ///  Valida si un certificado es de tipo Autenticaci�n
    /// </summary>
    /// <param name="serialNumber">N�mero de serie del certificado</param>
    /// <returns>Devuelve null si el certificado es de tipo Autenticaci�n, de lo contrario el Mensaje de respuesta.</returns>
    public static string IsValidAutenticationCertificate(string serialNumber)
    {
      if (!IsValidSerialNumber(serialNumber))
        return INVALID_SERIAL_NUMBER;

      try
      {
        IDCMembershipProvider customProvider = Membership.Provider as IDCMembershipProvider;
        FIELCertificateInfo certificateInfo = customProvider.GetFIELCertificateInfo(serialNumber);
        if (!certificateInfo.IsActive)
          return INACTIVE_CERTIFICATE;

        if (!certificateInfo.IsAutCertificate)
          return INVALID_CERTIFICATE_TYPE;

        string message = ValidateCertificateExpirationDate(certificateInfo.ExpirationDate);
        if (!string.IsNullOrEmpty(message))
          return message;

      }
      catch (Exception ex)
      { throw new CertificateValidationException(ex.Message); }

      return null;
    }

    /// <summary>
    /// Valida si un certificado es de tipo de Autenticaci�n.
    /// </summary>
    /// <param name="serialNumber">N�mero de serie del certificado.</param>
    /// <param name="days">Dias que se agregan para extender la fecha de expiraci�n del certificado.</param>
    /// <param name="message">Mensaje de respuesta.</param>
    /// <returns>Devuelve true si es un numero de serie valido, de lo contrario false.</returns>
    [Obsolete("Debe utilizar el m�todo: 'SAT.DyP.Util.Web.Security.Certificate.IsValidAutenticationCertificate(string, int)'")]
    public static bool IsValidAutenticationCertificate(string serialNumber, int days, out string message)
    {
      message = string.Empty;

      if (!IsValidSerialNumber(serialNumber))
      {
        message = INVALID_SERIAL_NUMBER;
        return false;
      }

      try
      {
        IDCMembershipProvider customProvider = Membership.Provider as IDCMembershipProvider;
        FIELCertificateInfo certificateInfo = customProvider.GetFIELCertificateInfo(serialNumber);

        if (!certificateInfo.IsActive)
        {
          message = INACTIVE_CERTIFICATE;
          return false;
        }

        if (!certificateInfo.IsFielCertificate)
        {
          message = INVALID_CERTIFICATE_TYPE;
          return false;
        }

        message = ValidateCertificateExpirationDate(certificateInfo.ExpirationDate, days);
      }
      catch (Exception ex)
      {
        throw new CertificateValidationException(ex.Message);
      }

      return String.IsNullOrEmpty(message);
    }

    /// <summary>
    /// Valida si un certificado es de tipo de Autenticaci�n.
    /// </summary>
    /// <param name="serialNumber">N�mero de serie del certificado.</param>
    /// <param name="days">D�as que se agregan para extender la fecha de expiraci�n del certificado.</param>
    /// <returns>Devuelve 'Nulo' si el certificado es v�lido, de lo contrario devuelve el mensaje de error.</returns>
    public static string IsValidAutenticationCertificate(string serialNumber, int days)
    {
      string message = null;

      if (!IsValidSerialNumber(serialNumber))
      {
        message = INVALID_SERIAL_NUMBER;
      }
      else
      {
        FIELCertificateInfo certificateInfo = GetFIELCertificateInfo(serialNumber);

        if (!certificateInfo.IsActive)
        {
          message = INACTIVE_CERTIFICATE;
        }
        else if (!certificateInfo.IsFielCertificate)
        {
          message = INVALID_CERTIFICATE_TYPE;
        }
        else
        {
          message = ValidateCertificateExpirationDate(certificateInfo.ExpirationDate, days);
        }
      }

      return message;
    }

    private static FIELCertificateInfo GetFIELCertificateInfo(string serialNumber)
    {
      try
      {
        IDCMembershipProvider customProvider = Membership.Provider as IDCMembershipProvider;
        return customProvider.GetFIELCertificateInfo(serialNumber);
      }
      catch (Exception ex)
      {
        throw new CertificateValidationException(ex.Message);
      }
    }

    /// <summary>
    /// Valida la consistencia de los datos del n�mero de serie
    /// </summary>
    /// <param name="serialNumber">N�mero de serie del certificado.</param>
    /// <returns>Devuelve true si es un numero de serie valido, de lo contrario false.</returns>
    public static bool IsValidSerialNumber(string serialNumber)
    {
      string serialNumberPattern = "[^a-z0-9]";
      Regex validator = new Regex(serialNumberPattern, RegexOptions.IgnoreCase);
      bool isValid = !validator.IsMatch(serialNumber);

      if (!isValid)
      {
        EventLogHelper.WriteWarningEntry("N�mero de serie inv�lido:" + serialNumber, CoreLogEventIdentifier.AUTHENTICATION_EVENT_ID);
      }

      return isValid;
    }
  }


  /// <summary>
  /// Excepci�n para controlar la validaci�n de certificados
  /// </summary>
  [Serializable]
  public class CertificateValidationException : Exception
  {
    public CertificateValidationException()
    {
    }

    public CertificateValidationException(string Message)
      : base(Message)
    {
    }

    public CertificateValidationException(string message, Exception innerException)
      : base(message, innerException)
    {
    }
  }
}