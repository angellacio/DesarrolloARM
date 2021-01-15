//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Util.Security.Windows:SecurityContext:0:21/May/2008[SAT.DyP.Util.Security:1.0:21/May/2008])
using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Principal;
using System.Security.Permissions;
using System.Runtime.InteropServices;
using SAT.DyP.Util.Configuration;
using SAT.DyP.Util.Types;
using System.ComponentModel;

namespace SAT.DyP.Util.Security.Windows
{
  public enum SECURITY_IMPERSONATION_LEVEL : int
  {
    SecurityAnonymous = 0,
    SecurityIdentification = 1,
    SecurityImpersonation = 2,
    SecurityDelegation = 3
  }

  /// <summary>
  /// Clase que permite realizar los servicios de "impersonate" de cuentas
  /// se seguridad
  /// </summary>
  public class SecurityContext
  {

    private const int LOGON32_PROVIDER_DEFAULT = 0;
    private const int LOGON32_LOGON_INTERACTIVE = 2;

    [DllImport("advapi32.dll", SetLastError = true)]
    private static extern bool LogonUser(string pszUsername, string pszDomain, string pszPassword,
        int dwLogonType, int dwLogonProvider, ref IntPtr phToken);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
    private extern static bool CloseHandle(IntPtr handle);

    [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private extern static bool DuplicateToken(IntPtr ExistingTokenHandle,
        int SECURITY_IMPERSONATION_LEVEL, ref IntPtr DuplicateTokenHandle);


    private string _userName;
    private string _domain;
    private string _password;
    private string _result = string.Empty;

    public SecurityContext(string userName, string domain, string password)
    {
 
      _userName = userName;
      _domain = domain;
      _password = password;
    }

    public SecurityContext()
    {
 
      _userName = ConfigurationManager.ApplicationSettings.ReadSetting(newConfigurationConstants.SECURITY_CTX_USERNAME);
      _domain = ConfigurationManager.ApplicationSettings.ReadSetting(newConfigurationConstants.SECURITY_CTX_DOMAIN);
      _password = ConfigurationManager.ApplicationSettings.ReadSetting(newConfigurationConstants.SECURITY_CTX_PASSWORD);
    }

    public string Message
    {
      get { return _result; }
    }


    /// <summary>
    /// Realizar el "impersonate" de una cuenta de usuario
    /// </summary>
    /// <returns></returns>
    [Obsolete("Debe utilizar el método: 'SAT.DyP.Util.Security.Windows.ImpersonateUser(WindowsImpersonationContext contextUser)'.")]
    public void ImpersonateUser(out WindowsImpersonationContext contextUser)
    {
      if (string.IsNullOrEmpty(_userName) && string.IsNullOrEmpty(_password))
      {
        throw new ArgumentException("Missing username and/or password");
      }

      IntPtr _tokenHandle = new IntPtr(0);
      IntPtr _duplicateTokenHandle = new IntPtr(0);
      _result = string.Empty;

      int _errCode = 0;

      if (string.IsNullOrEmpty(_domain))
      {
        _domain = System.Environment.MachineName;
      }

      try
      {
        bool _isImpersonated = LogonUser(_userName, _domain, _password,
                                         LOGON32_LOGON_INTERACTIVE,
                                         LOGON32_PROVIDER_DEFAULT,
                                         ref _tokenHandle);

        if (!_isImpersonated)
        {
          _errCode = Marshal.GetLastWin32Error();
          _result = string.Format("Method LogonUser failed with error code: {0}\r\n", _errCode);
          throw new Win32Exception(_errCode);
        }
        else
        {

          bool _duplicate = DuplicateToken(_tokenHandle,
                                           (int)SECURITY_IMPERSONATION_LEVEL.SecurityImpersonation,
                                           ref _duplicateTokenHandle);

          if (!_duplicate)
          {
            CloseHandle(_tokenHandle);
            _result = string.Format("Method DuplicateToken failed with error code: {0}\r\n", _errCode);
            throw new Win32Exception(_errCode);
          }
          else
          {
            WindowsIdentity _newIndentity = new WindowsIdentity(_duplicateTokenHandle);
            contextUser = _newIndentity.Impersonate();
          }
        }
      }
      catch (Exception ex)
      {
        throw new PlatformException(String.Format(@"Error al impersonar al usuario '{0}\{1}'.", _domain, _userName), ex);
      }
      finally
      {
        if (_tokenHandle != IntPtr.Zero)
          CloseHandle(_tokenHandle);
        if (_duplicateTokenHandle != IntPtr.Zero)
          CloseHandle(_duplicateTokenHandle);
      }
    }



    /// <summary>
    /// Realizar el "impersonate" de una cuenta de usuario
    /// </summary>
    /// <returns></returns>
    /// Mejoras MS. DAC MAR 2011 
    public WindowsImpersonationContext ImpersonateUser(WindowsImpersonationContext contextUser)
    {
      if (string.IsNullOrEmpty(_userName) && string.IsNullOrEmpty(_password))
      {
        throw new ArgumentException("Missing username and/or password");
      }

      IntPtr _tokenHandle = new IntPtr(0);
      IntPtr _duplicateTokenHandle = new IntPtr(0);
      _result = string.Empty;

      int _errCode = 0;

      if (string.IsNullOrEmpty(_domain))
      {
        _domain = System.Environment.MachineName;
      }

      try
      {
        bool _isImpersonated = LogonUser(_userName, _domain, _password,
                                         LOGON32_LOGON_INTERACTIVE,
                                         LOGON32_PROVIDER_DEFAULT,
                                         ref _tokenHandle);

        if (!_isImpersonated)
        {
          _errCode = Marshal.GetLastWin32Error();
          _result = string.Format("Method LogonUser failed with error code: {0}\r\n", _errCode);
          throw new Win32Exception(_errCode);
        }
        else
        {

          bool _duplicate = DuplicateToken(_tokenHandle,
                                           (int)SECURITY_IMPERSONATION_LEVEL.SecurityImpersonation,
                                           ref _duplicateTokenHandle);

          if (!_duplicate)
          {
            CloseHandle(_tokenHandle);
            _result = string.Format("Method DuplicateToken failed with error code: {0}\r\n", _errCode);
            throw new Win32Exception(_errCode);
          }
          else
          {
            WindowsIdentity _newIndentity = new WindowsIdentity(_duplicateTokenHandle);
            contextUser = _newIndentity.Impersonate();
          }
        }
      }
      catch (Exception ex)
      {
        throw new PlatformException(String.Format(@"Error al impersonar al usuario '{0}\{1}'.", _domain, _userName), ex);
      }
      finally
      {
        if (_tokenHandle != IntPtr.Zero)
          CloseHandle(_tokenHandle);
        if (_duplicateTokenHandle != IntPtr.Zero)
          CloseHandle(_duplicateTokenHandle);
      }

      return contextUser;
    }
    /// FIN - Mejoras MS. DAC MAR 2011 
  }
}