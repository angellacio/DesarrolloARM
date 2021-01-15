//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Util.Web.Login:BaseLoginForm:0:21/May/2008[SAT.DyP.Util.Web:1.0:21/May/2008])
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Configuration;
using SAT.DyP.Presentacion.Seguridad.Membresia;
using SAT.DyP.Util.Web.Page;
using SAT.DyP.Util.Logging;
using System.Diagnostics;
using SAT.DyP.Util.Web.Security;

namespace SAT.DyP.Util.Web.Login
{
  /// <summary>
  /// Clase principal que sirve de base para las páginas de login
  /// </summary>
  /// 
[Obsolete("Debe utilizar la clase: 'SAT.DyP.Util.Web.Login.BaseLoginFormAbstract'.")]
  public class BaseLoginForm : BaseWebForm       
  {
    //Constantes
    public const string FIEL_LOGIN_MODE = "FIEL";
    public const string PREFIEL_LOGIN_MODE = "PREFIEL";
    public const string CIEC_LOGIN_MODE = "CIEC";
    private const string FIEL_EXPIRED_MESSAGE = @"Su certificado digital expiró el día {0:dd/MM/yyyy}. Puede renovarlo ingresando a la opción ""Renovación de certificados en línea"" dentro de Firma Electrónica Avanzada (FIEL).";

    //Constantes de configuración
    private const string KEY_URL_AUTENTICACION = "autenticacion";
    private const string KEY_FORCE_FIEL = "ForzarAutenticacionFIEL";
    private const string EXECUTE_LOGOUT = "1";

    private const string KEY_CIEC_URL = "SAT.DyP.Negocio.Comun::LigaCIEC";
    private const string KEY_CIEC_MODULE_URL = "SAT.DyP.Negocio.Comun::LigaCIECModulo";

    protected override void OnPreLoad(EventArgs e)
    {
      string jsValue = "function adjustValues(userItem){var _item=document.getElementById(userItem); var _value=new String(_item.value); _item.value=_value.toUpperCase(); }";
      Page.ClientScript.RegisterClientScriptBlock(typeof(string), "VALUE_LOGIN", jsValue, true);

      base.OnPreLoad(e);
    }

    #region Custom Properties
    private WebAccessMethod _metodoAcceso;
    /// Devuelve el metodo de acceso utilizado en el Membership
    /// ya sea Internet / Modulo
    /// </summary>
    public WebAccessMethod WebSiteAccessMethod
    {
      get
      {
        IDCMembershipProvider provider = Membership.Provider as IDCMembershipProvider;
        _metodoAcceso = provider.AuthenticacionMetodo;
        return _metodoAcceso;
      }
    }

    /// <summary>
    /// Devuelve la URL del Web Service de autenticación
    /// </summary>
    public string AuthenticationWebServiceURL
    {
      get { return ConfigurationManager.AppSettings[KEY_URL_AUTENTICACION]; }
    }

    /// <summary>
    /// Devuelve el valor si esta habilitado forzar la autenticación para un
    /// usuario que tiene FIEL
    /// </summary>
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

    public string GetCIEC_Url(bool useModule)
    {
      string settingName = !useModule ? KEY_CIEC_URL : KEY_CIEC_MODULE_URL;

      string settingValue = SAT.DyP.Util.Configuration.ConfigurationManager.ApplicationSettings.ReadSetting(settingName);

      return settingValue;
    }

    #endregion

    #region Custom Methods

    /// <summary>
    /// Prepara la configuración inicial de la página
    /// </summary>
    private void PreparePage()
    {
      SetLoginLayout(true);
      PrepareLogin(LoginControl);
      ReviseLoginLabels();
      CiecHyperLink.NavigateUrl = this.GetCIEC_Url(false);

      if (this.WebSiteAccessMethod == WebAccessMethod.Internet)
      {
        if (Request.QueryString.Get("mode") != null)
        {
          if (Request.QueryString.Get("mode").Equals(FIEL_LOGIN_MODE))
          {
            SetLoginLayout(false);
          }

          if (Request.QueryString.Get("mode").Equals(PREFIEL_LOGIN_MODE))
          {
            SetLoginFIELMessageLayout();
          }
        }
      }
      else
      {
        PanelAvisosCiec.Visible = false;
        System.Web.UI.Control checkMail = this.LoginControl.FindControl("chkActualizaMail");
        if (checkMail != null)
        {
          checkMail.Visible = false;
        }
      }
    }

    private void Page_Load(object sender, EventArgs e)
    {
 

      if (!Page.IsPostBack)
      {
        if (this.IsLogoutRequest())
        {
          this.ExecuteSignOut(true);
        }
        else
        {
          PreparePage();
        }
      }
    }

    /// <summary>
    /// Revisa las opciones seleccionadas el momento de iniciar sesión
    /// en este caso aplica a "Actualizar Correo Electrónico"
    /// </summary>
    private void ReviewAuthenticationOptions(bool useFIEL)
    {
      if (!useFIEL)
      {
        if (UserWantsToChangeEmail_CIEC)
          RedirectToChangeEmailPage();
        else
          RedirectToMainPage();
      }
      else
      {
        if (UserWantsToChangeEmail_FIEL)
          RedirectToChangeEmailPage();
        else
          RedirectToMainPage();
      }
    }

    private void ReviseLoginLabels()
    {
      if (this.WebSiteAccessMethod == WebAccessMethod.Internet)
      {
        SetPasswordLabel("Contraseña :");
        SetUsernameLabel("RFC :");
      }
      else
      {
        SetUsernameLabel("Usuario :");
        SetPasswordLabel("Contraseña :");
      }
    }

    private void SetUsernameLabel(string labelText)
    {
      Label userlabel = LoginControl.FindControl("UserNameLabel") as Label;

      if (userlabel == null)
      {
        throw new InvalidOperationException("El control de login no tiene un Label llamado 'UserNameLabel'.");
      }

      userlabel.Text = labelText;
    }

    private void SetPasswordLabel(string labelText)
    {
      Label passwordLabel = LoginControl.FindControl("PasswordLabel") as Label;

      if (passwordLabel == null)
      {
        throw new InvalidOperationException("El control de login no tiene un Label llamado 'PasswordLabel'.");
      }

      passwordLabel.Text = labelText;
    }

    private void RedirectToChangeEmailPage()
    {
      Response.Redirect(EmailPageURL, true);
    }

    private void RedirectToMainPage()
    {
      Response.Redirect(MainPageURL, true);
    }
    /// <summary>
    /// Administra la vista de la página de login
    /// </summary>
    /// <param name="defaultLoginStatus"></param>
    private void SetLoginLayout(bool defaultLoginStatus)
    {
      PanelLoginCiec.Visible = defaultLoginStatus;
      PanelAppletFiel.Visible = !defaultLoginStatus;

      if (PanelLoginCiec.Visible || PanelAppletFiel.Visible)
        PanelAvisosFiel.Visible = false;
    }
    /// <summary>
    /// Prepara la vista de aviso de FIEL
    /// </summary>
    private void SetLoginFIELMessageLayout()
    {
      PanelLoginCiec.Visible = PanelAppletFiel.Visible = false;
      PanelAvisosFiel.Visible = true;
    }

    /// <summary>
    /// Ejecuta la autenticación en el sitio
    /// </summary>
    /// <param name="userName"></param>
    protected void ExecuteSignOn(string userName, bool validateFIEL)
    {
      string controlStep = "";

      FormsAuthentication.SetAuthCookie(userName, false);
      controlStep = "ENABLE_AUTH";

      if (this.WebSiteAccessMethod == WebAccessMethod.Internet && validateFIEL)
      {
        try
        {
          IDCMembershipUser currentUser = this.GetCurrentMembershipUser();
          controlStep = "GET_USER_INFO";

          if (currentUser != null)
          {
            if (currentUser.TieneFIEL && this.ForceFIELAuthentication)
            {
              //se cierra la sesión
              ExecuteSignOut(false);
              controlStep = "CLOSE_SESSION";
              FormsAuthentication.RedirectToLoginPage("mode=" + PREFIEL_LOGIN_MODE);
              Response.End();
            }
          }
        }
        catch (Exception ex)
        {
          EventLogHelper.WriteErrorEntry("Control Error:" + controlStep, CoreLogEventIdentifier.COMMON_WEB_LOGIN_EVENT_ID);
          throw new SAT.DyP.Util.Types.PlatformException("Se presento un error en el proceso de autenticación del usuario", ex);
        }

        ReviewAuthenticationOptions(false);
      }
    }

    /// <summary>
    /// Envia la solicitud para habilitar la vista correspondiente
    /// </summary>
    /// <param name="viewMode"></param>

    protected void EnableViewAuthentication(string viewMode)
    {
      string url = string.Format("{0}?mode={1}", FormsAuthentication.LoginUrl, viewMode);
      Response.Redirect(url, true);
    }

    /// <summary>
    /// Inicializa la autenticación por formularios, para activar la
    /// cuenta de acceso por Applet de FIEL
    /// </summary>
    /// <param name="userName">cuenta de acceso</param>
    /// <param name="minutes">cantidad de minutos para expirar la cuenta</param>
    public void InitializeFormsAuthentication(string userName, int minutes)
    {
      FormsAuthentication.Initialize();
      FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(2,
                                                                       userName.Trim(),
                                                                       DateTime.Now,
                                                                       DateTime.Now.AddMinutes(minutes),
                                                                       false,
                                                                       FormsAuthentication.FormsCookieName,
                                                                       FormsAuthentication.FormsCookiePath);

      string securityData = FormsAuthentication.Encrypt(ticket);
      HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, securityData);
      Response.Cookies.Add(cookie);
      ReviewAuthenticationOptions(true);
    }

    /// <summary>
    /// Valida la fecha de vigencia del certificado FIEL del contribuyente
    /// </summary>
    /// <param name="fielCertSerialNumber"></param>
    //public bool ValidateCertificateExpirationDate(string serialNumber)
    //{
    //    string message = "";
    //    Nullable<DateTime> expirationDate = CurrentMembershipProvider.GetFIELExpirationDate(serialNumber);
    //    if (expirationDate != null)
    //    {
    //        if (DateTime.Now < expirationDate.Value)
    //        {
    //            return true;
    //        }
    //        else
    //        {
    //            message = String.Format(FIEL_EXPIRED_MESSAGE, expirationDate);
    //            ShowErrorMessage(message);
    //            return false;
    //        }
    //    }

    //    message = "No se pudo validar la vigencia de su Firma Electrónica Avanzada.";
    //    ShowErrorMessage(message);
    //    return false;
    //}

    public bool ValidateCertificate(string serialNumber)
    {
      //--> FSM: Recomendaciones MS
      string mensajeValidacion = CertificateHelper.IsValidAutenticationCertificate(serialNumber);

      if (!String.IsNullOrEmpty(mensajeValidacion))
        ShowErrorMessage(mensajeValidacion);

      return string.IsNullOrEmpty(mensajeValidacion);
    }

    public bool ValidateCertificate(string serialNumber, int days)
    {
      //--> FSM: Recomendaciones MS
      string message = CertificateHelper.IsValidAutenticationCertificate(serialNumber, days);

      if (!String.IsNullOrEmpty(message))
      {
        ShowErrorMessage(message);
      }

      return String.IsNullOrEmpty(message);
    }

    /// <summary>
    /// Revisa la solicitud de un cierre de sesión por 
    /// QueryString
    /// </summary>
    /// <returns></returns>
    protected bool IsLogoutRequest()
    {
      if (Request.QueryString.Get("logout") != null)
      {
        if (Request.QueryString.Get("logout").Equals(EXECUTE_LOGOUT))
          return true;
      }

      return false;
    }

    /// <summary>
    /// Permite agregar el boton submit una función de javascript
    /// que coloca en mayusculas el nombre del usuario
    /// </summary>
    /// <param name="loginControl"></param>
    protected void PrepareLogin(System.Web.UI.WebControls.Login loginControl)
    {
      string textBoxUserNameId = string.Format("{0}${1}", loginControl.ID, "UserName");
      Button btnSubmit = loginControl.FindControl("LoginButton") as Button;
      btnSubmit.Attributes["onclick"] = "adjustValues('" + textBoxUserNameId + "');";
    }

    #endregion



    #region Must be overriden by inherited login page
    private const string PROPERTY_NOT_IMPLEMENTED = @"La página de login debe implementar la propiedad '{0}' definida en SAT.DyP.Util.Web.Login.BaseLoginForm";
    protected virtual bool UserWantsToChangeEmail_CIEC { get { throw new NotImplementedException(String.Format(PROPERTY_NOT_IMPLEMENTED, "UserWantsToChangeEmail_CIEC")); } }
    protected virtual bool UserWantsToChangeEmail_FIEL { get { throw new NotImplementedException(String.Format(PROPERTY_NOT_IMPLEMENTED, "UserWantsToChangeEmail_FIEL")); } }
    protected virtual string EmailPageURL { get { throw new NotImplementedException(String.Format(PROPERTY_NOT_IMPLEMENTED, "EmailPageURL")); } }
    protected virtual string MainPageURL { get { throw new NotImplementedException(String.Format(PROPERTY_NOT_IMPLEMENTED, "MainPageURL")); } }
    protected virtual System.Web.UI.WebControls.Login LoginControl { get { throw new NotImplementedException(String.Format(PROPERTY_NOT_IMPLEMENTED, "LoginControl")); } }
    protected virtual System.Web.UI.WebControls.HyperLink CiecHyperLink { get { throw new NotImplementedException(String.Format(PROPERTY_NOT_IMPLEMENTED, "CiecHyperLink")); } }
    protected virtual System.Web.UI.WebControls.Panel PanelLoginCiec { get { throw new NotImplementedException(String.Format(PROPERTY_NOT_IMPLEMENTED, "PanelLoginCiec")); } }
    protected virtual System.Web.UI.WebControls.Panel PanelAppletFiel { get { throw new NotImplementedException(String.Format(PROPERTY_NOT_IMPLEMENTED, "PanelAppletFiel")); } }
    protected virtual System.Web.UI.WebControls.Panel PanelAvisosFiel { get { throw new NotImplementedException(String.Format(PROPERTY_NOT_IMPLEMENTED, "PanelAvisosFiel")); } }
    protected virtual System.Web.UI.WebControls.Panel PanelAvisosCiec { get { throw new NotImplementedException(String.Format(PROPERTY_NOT_IMPLEMENTED, "PanelAvisosCiec")); } }
    #endregion
  }

  public abstract class BaseLoginFormAbstract : BaseWebForm
  {
    //Constantes
    public const string FIEL_LOGIN_MODE = "FIEL";
    public const string PREFIEL_LOGIN_MODE = "PREFIEL";
    public const string CIEC_LOGIN_MODE = "CIEC";
    private const string FIEL_EXPIRED_MESSAGE = @"Su certificado digital expiró el día {0:dd/MM/yyyy}. Puede renovarlo ingresando a la opción ""Renovación de certificados en línea"" dentro de Firma Electrónica Avanzada (FIEL).";

    //Constantes de configuración
    private const string KEY_URL_AUTENTICACION = "autenticacion";
    private const string KEY_FORCE_FIEL = "ForzarAutenticacionFIEL";
    private const string EXECUTE_LOGOUT = "1";

    private const string KEY_CIEC_URL = "SAT.DyP.Negocio.Comun::LigaCIEC";
    private const string KEY_CIEC_MODULE_URL = "SAT.DyP.Negocio.Comun::LigaCIECModulo";

    protected override void OnPreLoad(EventArgs e)
    {
      string jsValue = "function adjustValues(userItem){var _item=document.getElementById(userItem); var _value=new String(_item.value); _item.value=_value.toUpperCase(); }";
      Page.ClientScript.RegisterClientScriptBlock(typeof(string), "VALUE_LOGIN", jsValue, true);

      base.OnPreLoad(e);
    }

    #region Custom Properties
    private WebAccessMethod _metodoAcceso;
    /// Devuelve el metodo de acceso utilizado en el Membership
    /// ya sea Internet / Modulo
    /// </summary>
    public WebAccessMethod WebSiteAccessMethod
    {
      get
      {
        IDCMembershipProvider provider = Membership.Provider as IDCMembershipProvider;
        _metodoAcceso = provider.AuthenticacionMetodo;
        return _metodoAcceso;
      }
    }

    /// <summary>
    /// Devuelve la URL del Web Service de autenticación
    /// </summary>
    public string AuthenticationWebServiceURL
    {
      get { return ConfigurationManager.AppSettings[KEY_URL_AUTENTICACION]; }
    }

    /// <summary>
    /// Devuelve el valor si esta habilitado forzar la autenticación para un
    /// usuario que tiene FIEL
    /// </summary>
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

    public string GetCIEC_Url(bool useModule)
    {
      string settingName = !useModule ? KEY_CIEC_URL : KEY_CIEC_MODULE_URL;

      string settingValue = SAT.DyP.Util.Configuration.ConfigurationManager.ApplicationSettings.ReadSetting(settingName);

      return settingValue;
    }

    #endregion

    #region Custom Methods

    /// <summary>
    /// Prepara la configuración inicial de la página
    /// </summary>
    private void PreparePage()
    {
      SetLoginLayout(true);
      PrepareLogin(LoginControl);
      ReviseLoginLabels();
      CiecHyperLink.NavigateUrl = this.GetCIEC_Url(false);

      if (this.WebSiteAccessMethod == WebAccessMethod.Internet)
      {
        if (Request.QueryString.Get("mode") != null)
        {
          if (Request.QueryString.Get("mode").Equals(FIEL_LOGIN_MODE))
          {
            SetLoginLayout(false);
          }

          if (Request.QueryString.Get("mode").Equals(PREFIEL_LOGIN_MODE))
          {
            SetLoginFIELMessageLayout();
          }
        }
      }
      else
      {
        PanelAvisosCiec.Visible = false;
        System.Web.UI.Control checkMail = this.LoginControl.FindControl("chkActualizaMail");
        if (checkMail != null)
        {
          checkMail.Visible = false;
        }
      }
    }

    private void Page_Load(object sender, EventArgs e)
    {
      if (!Page.IsPostBack)
      {
        if (this.IsLogoutRequest())
        {
          this.ExecuteSignOut(true);
        }
        else
        {
          PreparePage();
        }
      }
    }

    /// <summary>
    /// Revisa las opciones seleccionadas el momento de iniciar sesión
    /// en este caso aplica a "Actualizar Correo Electrónico"
    /// </summary>
    private void ReviewAuthenticationOptions(bool useFIEL)
    {
      if (!useFIEL)
      {
        if (UserWantsToChangeEmail_CIEC)
          RedirectToChangeEmailPage();
        else
          RedirectToMainPage();
      }
      else
      {
        if (UserWantsToChangeEmail_FIEL)
          RedirectToChangeEmailPage();
        else
          RedirectToMainPage();
      }
    }

    private void ReviseLoginLabels()
    {
      if (this.WebSiteAccessMethod == WebAccessMethod.Internet)
      {
        SetPasswordLabel("Contraseña :");
        SetUsernameLabel("RFC :");
      }
      else
      {
        SetUsernameLabel("Usuario :");
        SetPasswordLabel("Contraseña :");
      }
    }

    private void SetUsernameLabel(string labelText)
    {
      Label userlabel = LoginControl.FindControl("UserNameLabel") as Label;

      if (userlabel == null)
      {
        throw new InvalidOperationException("El control de login no tiene un Label llamado 'UserNameLabel'.");
      }

      userlabel.Text = labelText;
    }

    private void SetPasswordLabel(string labelText)
    {
      Label passwordLabel = LoginControl.FindControl("PasswordLabel") as Label;

      if (passwordLabel == null)
      {
        throw new InvalidOperationException("El control de login no tiene un Label llamado 'PasswordLabel'.");
      }

      passwordLabel.Text = labelText;
    }

    private void RedirectToChangeEmailPage()
    {
      Response.Redirect(EmailPageURL, true);
    }

    private void RedirectToMainPage()
    {
      Response.Redirect(MainPageURL, true);
    }
    /// <summary>
    /// Administra la vista de la página de login
    /// </summary>
    /// <param name="defaultLoginStatus"></param>
    private void SetLoginLayout(bool defaultLoginStatus)
    {
      PanelLoginCiec.Visible = defaultLoginStatus;
      PanelAppletFiel.Visible = !defaultLoginStatus;

      if (PanelLoginCiec.Visible || PanelAppletFiel.Visible)
        PanelAvisosFiel.Visible = false;
    }
    /// <summary>
    /// Prepara la vista de aviso de FIEL
    /// </summary>
    private void SetLoginFIELMessageLayout()
    {
      PanelLoginCiec.Visible = PanelAppletFiel.Visible = false;
      PanelAvisosFiel.Visible = true;
    }

    /// <summary>
    /// Ejecuta la autenticación en el sitio
    /// </summary>
    /// <param name="userName"></param>
    protected void ExecuteSignOn(string userName, bool validateFIEL)
    {
      string controlStep = "";

      FormsAuthentication.SetAuthCookie(userName, false);
      controlStep = "ENABLE_AUTH";

      if (this.WebSiteAccessMethod == WebAccessMethod.Internet && validateFIEL)
      {
        try
        {
          IDCMembershipUser currentUser = this.GetCurrentMembershipUser();
          controlStep = "GET_USER_INFO";

          if (currentUser != null)
          {
            if (currentUser.TieneFIEL && this.ForceFIELAuthentication)
            {
              //se cierra la sesión
              ExecuteSignOut(false);
              controlStep = "CLOSE_SESSION";
              FormsAuthentication.RedirectToLoginPage("mode=" + PREFIEL_LOGIN_MODE);
              Response.End();
            }
          }
        }
        catch (Exception ex)
        {
          EventLogHelper.WriteErrorEntry("Control Error:" + controlStep, CoreLogEventIdentifier.COMMON_WEB_LOGIN_EVENT_ID);
          throw new SAT.DyP.Util.Types.PlatformException("Se presento un error en el proceso de autenticación del usuario", ex);
        }

        ReviewAuthenticationOptions(false);
      }
    }

    /// <summary>
    /// Envia la solicitud para habilitar la vista correspondiente
    /// </summary>
    /// <param name="viewMode"></param>

    protected void EnableViewAuthentication(string viewMode)
    {
      string url = string.Format("{0}?mode={1}", FormsAuthentication.LoginUrl, viewMode);
      Response.Redirect(url, true);
    }

    /// <summary>
    /// Inicializa la autenticación por formularios, para activar la
    /// cuenta de acceso por Applet de FIEL
    /// </summary>
    /// <param name="userName">cuenta de acceso</param>
    /// <param name="minutes">cantidad de minutos para expirar la cuenta</param>
    public void InitializeFormsAuthentication(string userName, int minutes)
    {
      FormsAuthentication.Initialize();
      FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(2,
                                                                       userName.Trim(),
                                                                       DateTime.Now,
                                                                       DateTime.Now.AddMinutes(minutes),
                                                                       false,
                                                                       FormsAuthentication.FormsCookieName,
                                                                       FormsAuthentication.FormsCookiePath);

      string securityData = FormsAuthentication.Encrypt(ticket);
      HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, securityData);
      Response.Cookies.Add(cookie);
      ReviewAuthenticationOptions(true);
    }

    /// <summary>
    /// Valida la fecha de vigencia del certificado FIEL del contribuyente
    /// </summary>
    /// <param name="fielCertSerialNumber"></param>
    //public bool ValidateCertificateExpirationDate(string serialNumber)
    //{
    //    string message = "";
    //    Nullable<DateTime> expirationDate = CurrentMembershipProvider.GetFIELExpirationDate(serialNumber);
    //    if (expirationDate != null)
    //    {
    //        if (DateTime.Now < expirationDate.Value)
    //        {
    //            return true;
    //        }
    //        else
    //        {
    //            message = String.Format(FIEL_EXPIRED_MESSAGE, expirationDate);
    //            ShowErrorMessage(message);
    //            return false;
    //        }
    //    }

    //    message = "No se pudo validar la vigencia de su Firma Electrónica Avanzada.";
    //    ShowErrorMessage(message);
    //    return false;
    //}

    public bool ValidateCertificate(string serialNumber)
    {
      //--> FSM: Recomendaciones MS
      string mensajeValidacion = CertificateHelper.IsValidAutenticationCertificate(serialNumber);

      if (!String.IsNullOrEmpty(mensajeValidacion))
        ShowErrorMessage(mensajeValidacion);

      return string.IsNullOrEmpty(mensajeValidacion);
    }

    public bool ValidateCertificate(string serialNumber, int days)
    {
      //--> FSM: Recomendaciones MS
      string message = CertificateHelper.IsValidAutenticationCertificate(serialNumber, days);

      if (!String.IsNullOrEmpty(message))
      {
        ShowErrorMessage(message);
      }

      return String.IsNullOrEmpty(message);
    }

    /// <summary>
    /// Revisa la solicitud de un cierre de sesión por 
    /// QueryString
    /// </summary>
    /// <returns></returns>
    protected bool IsLogoutRequest()
    {
      if (Request.QueryString.Get("logout") != null)
      {
        if (Request.QueryString.Get("logout").Equals(EXECUTE_LOGOUT))
          return true;
      }

      return false;
    }

    /// <summary>
    /// Permite agregar el boton submit una función de javascript
    /// que coloca en mayusculas el nombre del usuario
    /// </summary>
    /// <param name="loginControl"></param>
    protected void PrepareLogin(System.Web.UI.WebControls.Login loginControl)
    {
      string textBoxUserNameId = string.Format("{0}${1}", loginControl.ID, "UserName");
      Button btnSubmit = loginControl.FindControl("LoginButton") as Button;
      btnSubmit.Attributes["onclick"] = "adjustValues('" + textBoxUserNameId + "');";
    }

    #endregion

    #region Must be overriden by inherited login page
    abstract protected bool UserWantsToChangeEmail_CIEC { get; }
    abstract protected bool UserWantsToChangeEmail_FIEL { get; }
    abstract protected string EmailPageURL { get; }
    abstract protected string MainPageURL { get; }
    abstract protected System.Web.UI.WebControls.Login LoginControl { get; }
    abstract protected System.Web.UI.WebControls.HyperLink CiecHyperLink { get; }
    abstract protected System.Web.UI.WebControls.Panel PanelLoginCiec { get; }
    abstract protected System.Web.UI.WebControls.Panel PanelAppletFiel { get; }
    abstract protected System.Web.UI.WebControls.Panel PanelAvisosFiel { get; }
    abstract protected System.Web.UI.WebControls.Panel PanelAvisosCiec { get; }
    #endregion


  }
}