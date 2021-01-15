
//@(#)SCADE2(W:SKDN08221CO2:SAT.DyP.Presentacion.Seguridad.Membresia:IDCMembershipProvider:0:21/Mayo/2008[SAT.DyP.Presentacion.Seguridad.Membresia:1.1:21/Mayo/2008])
	
using System;
using System.Web.Security;
using System.Configuration;
using SAT.DyP.Presentacion.Seguridad.Membresia.wsIDCValidate;
using System.Diagnostics;
using SAT.DyP.Util.Logging;
using SAT.DyP.Util.Types;

/// <summary>
/// Proveedor de servicios de autenticación para SCADE.Net
/// </summary>
namespace SAT.DyP.Presentacion.Seguridad.Membresia
{
  public class IDCMembershipProvider : System.Web.Security.MembershipProvider
  {
    private const string SAT_MEMBERSHIP = "SATMembership";
    private const string SAT_PERMISOS_USUARIO = "SATPermisos";
    private const string SAT_DIRECTORIO = "Directory";
    private const string SAT_URL_WEBSERVICE = "autenticacion";
    private const string KEY_FORCE_FIEL = "ForzarAutenticacionFIEL";
    private const string KEY_UsarCIECFortalecida = "UsarCIECFortalecida";
    private string _applicationName = "IDCMembershipProvider";

 

    private bool _useFielAuthentication;
    public bool UseFIELAuthentication
    {
      get { return _useFielAuthentication; }
      set { _useFielAuthentication = value; }
    }

    /// <summary>
    /// Especifica el tipo de autenticación configurado
    /// </summary>
    public WebAccessMethod AuthenticacionMetodo
    {
      get { return GetAuthenticationMethod(); }
    }

    #region MembershipProvider Implementation

    public override string ApplicationName
    {
      get { return (this._applicationName); }
      set { this._applicationName = value; }
    }

    public override MembershipUser GetUser(string username, bool userIsOnline)
    {
      IDCMembershipUser _member = GetFromDB(username.ToUpper());
      return _member;
    }

    /// <summary>
    /// Determina el método de autenticación que utilizará con base
    /// en la clave de configuración
    /// </summary>
    /// <returns></returns>
    private WebAccessMethod GetAuthenticationMethod()
    {
      string metodo = ConfigurationManager.ConnectionStrings[SAT_DIRECTORIO].ConnectionString;
      WebAccessMethod tipoAcceso = WebAccessMethod.Internet;

      //se determina el metodo de acceso que se requiere
      if (!metodo.Equals(SAT_MEMBERSHIP))
      {
        tipoAcceso = WebAccessMethod.Modulo;
      }

      return tipoAcceso;
    }

    public bool ForceFIELAuthentication
    {
      get
      {
        string forceFiel = ConfigurationManager.AppSettings[KEY_FORCE_FIEL];

        if (string.IsNullOrEmpty(forceFiel))
          return false;
        else
          return Convert.ToBoolean(forceFiel);
      }
    }

    public bool UseCIECFortalecida
    {
      get
      {
        string usoCIEC = ConfigurationManager.AppSettings[KEY_UsarCIECFortalecida];

        if (string.IsNullOrEmpty(usoCIEC))
          return false;
        else
          return Convert.ToBoolean(usoCIEC);
      }
    }

    public override bool ValidateUser(string username, string password)
    {
      string _permisos = string.Empty;

      WebAccessMethod acceso = GetAuthenticationMethod();

      //se determina el metodo de acceso que se requiere
      if (acceso == WebAccessMethod.Modulo)
      {
        //se obtiene la lista de ID´s de la aplicación para validar
        //los permisos del usuario. Estos se configuran en el web.config
        //de cada declaración
        _permisos = ConfigurationManager.AppSettings[SAT_PERMISOS_USUARIO];
      }


      idcValidation _val = new idcValidation();
      _val.Url = ConfigurationManager.AppSettings[SAT_URL_WEBSERVICE];

      bool authenticated = false;
      try
      {
        if (acceso == WebAccessMethod.Internet)
        {
          authenticated = _val.ValidateUser(username.ToUpper(), password, this.UseCIECFortalecida);
        }
        else
        {
          authenticated = _val.ValidateUserModule(username, password, _permisos);
        }
      }
      catch (Exception ex)
      {
        throw new PlatformException("Error de comunicación con el servicio de autenticación.", ex);
      }
      finally
      {
        if (_val != null)
        {
          _val.Dispose();
          _val = null;
        }
      }

      return authenticated;
    }

    public Nullable<DateTime> GetFIELExpirationDate(string serialNumber)
    {
      idcValidation authenticationService = new idcValidation();
      authenticationService.Url = ConfigurationManager.AppSettings[SAT_URL_WEBSERVICE];

      FIELCertificateInfo certificate = authenticationService.ObtenerDatosFIEL(serialNumber);
      if (certificate != null)
      {
        return certificate.ExpirationDate;
      }
      else
      {
        string message = String.Format("No se pudo determinar la fecha de expiración del certificado FIEL con número de serie '{0}'. Verificar servicio de autenticación de SCADE.", serialNumber);
        EventLogHelper.WriteInformationEntry(message, CoreLogEventIdentifier.AUTHENTICATION_EVENT_ID);
        return null;
      }
    }

    public FIELCertificateInfo GetFIELCertificateInfo(string serialNumber)
    {
      idcValidation authenticationService = new idcValidation();
      authenticationService.Url = ConfigurationManager.AppSettings[SAT_URL_WEBSERVICE];

      FIELCertificateInfo certificateInfo = authenticationService.ObtenerDatosFIEL(serialNumber);

      return certificateInfo;
    }

    #endregion

    #region Unsupported MembershipProvider features

    public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
    {
      throw new NotSupportedException(GetNotSupportedMessageForOperation("GetUser"));
    }

    public override bool ChangePassword(string username, string oldPassword, string newPassword)
    {
      throw new NotSupportedException(GetNotSupportedMessageForOperation("ChangePassword"));
    }

    public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
    {
      throw new NotSupportedException(GetNotSupportedMessageForOperation("ChangePasswordQuestionAndAnswer"));
    }

    public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
    {
      throw new NotSupportedException(GetNotSupportedMessageForOperation("CreateUser"));
    }

    public override bool DeleteUser(string username, bool deleteAllRelatedData)
    {
      throw new NotSupportedException(GetNotSupportedMessageForOperation("DeleteUser"));
    }

    public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
    {
      throw new NotSupportedException(GetNotSupportedMessageForOperation("FindUsersByEmail")); ;
    }

    public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
    {
      throw new NotSupportedException(GetNotSupportedMessageForOperation("FindUsersByName")); ;
    }

    public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
    {
      throw new NotSupportedException(GetNotSupportedMessageForOperation("GetAllUsers")); ;
    }

    public override int GetNumberOfUsersOnline()
    {
      throw new NotSupportedException(GetNotSupportedMessageForOperation("GetNumberOfUsersOnline"));
    }

    public override string GetPassword(string username, string answer)
    {
      throw new NotSupportedException(GetNotSupportedMessageForOperation("GetPassword"));
    }

    public override string GetUserNameByEmail(string email)
    {
      idcValidation _user = new idcValidation();

      _user.Url = ConfigurationManager.AppSettings[SAT_URL_WEBSERVICE];
      string _nombreRazonSocial = string.Empty;

      _nombreRazonSocial = _user.ObtenNombreRazonSocial(email);
      return _nombreRazonSocial;//throw new NotSupportedException(GetNotSupportedMessageForOperation("GetUserNameByEmail"));
    }

    public override string ResetPassword(string username, string answer)
    {
      throw new NotSupportedException(GetNotSupportedMessageForOperation("ResetPassword"));
    }

    public override bool UnlockUser(string userName)
    {
      throw new NotSupportedException(GetNotSupportedMessageForOperation("UnlockUser"));
    }

    public override void UpdateUser(MembershipUser user)
    {
      UpdateUserEmail((IDCMembershipUser)user);
    }

    public void UpdateUserEmail(IDCMembershipUser user)
    {
      idcValidation val = new idcValidation();
      val.Url = ConfigurationManager.AppSettings[SAT_URL_WEBSERVICE];

      val.ActualizaMail(user.RFC, user.Email);
    }

    public override int MaxInvalidPasswordAttempts
    {
      get { throw new NotSupportedException(GetNotSupportedMessageForProperty("MaxInvalidPasswordAttempts")); }
    }

    public override int MinRequiredNonAlphanumericCharacters
    {
      get { throw new NotSupportedException(GetNotSupportedMessageForProperty("MinRequiredNonAlphanumericCharacters")); }
    }

    public override int MinRequiredPasswordLength
    {
      get { throw new NotSupportedException(GetNotSupportedMessageForProperty("MinRequiredPasswordLength")); }
    }

    public override int PasswordAttemptWindow
    {
      get { throw new NotSupportedException(GetNotSupportedMessageForProperty("PasswordAttemptWindow")); }
    }

    public override MembershipPasswordFormat PasswordFormat
    {
      get { throw new NotSupportedException(GetNotSupportedMessageForProperty("PasswordFormat")); }
    }

    public override string PasswordStrengthRegularExpression
    {
      get { throw new NotSupportedException(GetNotSupportedMessageForProperty("PasswordStrengthRegularExpression")); }
    }

    public override bool RequiresQuestionAndAnswer
    {
      get { throw new NotSupportedException(GetNotSupportedMessageForProperty("RequiresQuestionAndAnswer")); }
    }

    public override bool RequiresUniqueEmail
    {
      get { throw new NotSupportedException(GetNotSupportedMessageForProperty("RequiresUniqueEmail")); }
    }

    public override bool EnablePasswordReset
    {
      get { throw new NotSupportedException(GetNotSupportedMessageForProperty("EnablePasswordReset")); }
    }

    public override bool EnablePasswordRetrieval
    {
      get { throw new NotSupportedException(GetNotSupportedMessageForProperty("EnablePasswordRetrieval")); }
    }
    #endregion

    #region Private Implementation

    private IDCMembershipUser GetFromDB(string rfc)
    {
      idcValidation idc = new idcValidation();
      idc.Url = ConfigurationManager.AppSettings[SAT_URL_WEBSERVICE];
      UserResponse user = idc.ObtenDatosUsuario(rfc.ToUpper());
      IDCMembershipUser userMember = new IDCMembershipUser(ApplicationName, rfc, user.RazonSocial, user.Mail, user.Password);

      if (this.AuthenticacionMetodo == WebAccessMethod.Internet)
      {
        if (this.ForceFIELAuthentication)
          userMember.TieneFIEL = idc.VerificarFIEL(rfc.ToUpper());
        else
          userMember.TieneFIEL = false;
      }
      else
      {
        userMember.TieneFIEL = false;
      }

      return userMember;
    }

    private string GetNotSupportedMessageForOperation(string operationName)
    {
      return String.Format("The operation '{0}' is not supported by {1}.", operationName, _applicationName);
    }

    private string GetNotSupportedMessageForProperty(string propertyName)
    {
      return String.Format("The property '{0}' is not supported by {1}.", propertyName, _applicationName);
    }

    #endregion
  }
}