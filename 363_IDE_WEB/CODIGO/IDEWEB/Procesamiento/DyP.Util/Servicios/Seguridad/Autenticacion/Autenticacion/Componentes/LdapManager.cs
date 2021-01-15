using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.DirectoryServices.Protocols;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.Diagnostics;
using SAT.DyP.Util.Monitoring;
using SAT.DyP.Util.Logging;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace SAT.DyP.Presentacion.Seguridad.Autenticacion.Componentes
{
    /// <summary>
    /// Clase responsable de realizar las operaciones
    /// al directorio de contribuyentes de Novell mediante LDAP
    /// </summary>
    public class LdapManager:IDisposable
    {
        private string _ldapServer = string.Empty;
        private bool _bUseSSL;
        private string _userProxy;
        private string _passwordProxy;
        private string _distinguisedName;

        private LdapConnection _connection;
        private ProfilingHelper _profiler;
        private bool _isLoggingEnabled = false;

        private const string MY_PROFILING_CLASS_NAME = "LdapProvider";


        public LdapManager()
        {
            //se carga la configuración
            _ldapServer = ConfigurationManager.AppSettings["LDAPServerModule"];
            _bUseSSL = bool.Parse(ConfigurationManager.AppSettings["LDAPUseSSLModule"]);
            _userProxy = ConfigurationManager.AppSettings["LDAPServiceUIDModule"];
            _passwordProxy = ConfigurationManager.AppSettings["LDAPServicePwdModule"];
            _distinguisedName = ConfigurationManager.AppSettings["LDAPDistinguisedNameModule"];

            //se revisa la opción de loggeo
            ReviewLoggingConfiguration();
        }


        private void ReviewLoggingConfiguration()
        {
            string keyConfig = "SAT.DyP.Util.Logging::ENABLE_LOG";
            string configValue = SAT.DyP.Util.Configuration.ConfigurationManager.ApplicationSettings.ReadSetting(keyConfig);

            if (!string.IsNullOrEmpty(configValue))
            {
                _isLoggingEnabled=Convert.ToBoolean(configValue);
                if (_isLoggingEnabled)
                {
                    //se habilita el loggeo en un archivo
                    string sysDir = Environment.SystemDirectory.ToLower().Replace("system32","");
                    TextWriter logFile = (TextWriter)File.AppendText( sysDir + "Temp\\autenticacion.log");
                    TextWriterTraceListener listener = new TextWriterTraceListener(logFile);
                    Trace.Listeners.Add(listener);
                }
            }
        }

        /// <summary>
        /// Realiza la conexión al directorio de contribuyentes
        /// </summary>
        /// <param name="timeOut">tiempo de espera (segundos)</param>
        public void Connect(int timeOut)
        {
            _connection = new LdapConnection(new LdapDirectoryIdentifier(_ldapServer));
            _connection.SessionOptions.SecureSocketLayer = _bUseSSL;
            _connection.SessionOptions.VerifyServerCertificate = new VerifyServerCertificateCallback(ServerCertificateCallback);
            _connection.Credential = new NetworkCredential(_userProxy, _passwordProxy);
            _connection.AuthType = AuthType.Basic;

            _profiler = new ProfilingHelper(MY_PROFILING_CLASS_NAME);

            _connection.Timeout = new TimeSpan(0, 0, timeOut);
            _connection.Bind();
        }


        /// <summary>
        /// Ejecuta una consulta LDAP al directorio
        /// </summary>
        /// <param name="userName">nombre de usuario</param>
        /// <returns>Resultado</returns>
        private SearchResultEntry ExecuteSearch(string userName)
        {
            SearchResultEntry entry = null;

            _distinguisedName = "ou=EXTERNOS,ou=PEOPLE,o=SAT";
            string filter = String.Format("(&(objectClass=User)(cn={0}))", EscapeDirectoryUnsupportedChars(userName));

            SearchRequest request = new SearchRequest(_distinguisedName, filter, SearchScope.Subtree);

            _profiler.Start();
            SearchResponse response = (SearchResponse)_connection.SendRequest(request);
            _profiler.Stop(ScadePerfCounter.Directory_ValidateUser);

            Trace.WriteLine(String.Format("{0} entries found.", response.Entries.Count));

            if (response.Entries.Count > 0)
            {
                entry = response.Entries[0];
            }
            else
            {
                Trace.WriteLine("Password is invalid.");
                throw new Exception("No fue posible encontrar el usuario en el directorio");
            }

            return entry;
        }

        /// <summary>
        /// Realiza la busqueda de un usuario en el directorio haciendo uso de la cuenta de acceso
        /// </summary>
        /// <param name="userName">Nombre de usuario</param>
        /// <param name="password">Contraseña</param>
        /// <param name="validateCIECFortalecida">validar Ciec Fortalecida</param>
        /// <returns></returns>
        public bool FindAccount(string userName,string password,bool validatePowerCIEC)
        {
            bool result = false;

            //se busca la cuenta en el directorio de Novell
            SearchResultEntry entry = ExecuteSearch(userName);

            Trace.WriteLine(String.Format("Attempting to validate password: {0} for account {1}", password,userName));

            //se valida la credencia del usuario 
            _connection.Credential = new NetworkCredential(entry.DistinguishedName, password);
            _connection.Bind();

            if (validatePowerCIEC)
            {
                Trace.WriteLine("Validate Power CIEC for user:" + entry.DistinguishedName);

                result = this.ContainsCIECF(entry);
            }
            else
            {
                Trace.WriteLine("Password is valid.");
                result = true;
            }
                      
            return result;
        }

        /// <summary>
        /// Consulta un atributo de una cuenta de usuario
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public string GetUserAttribute(string userName, string attributeName)
        {
            string attributeValue = string.Empty;

            Trace.WriteLine("Attempting read value for attribute:" + attributeName);
            SearchResultEntry entry = ExecuteSearch(userName);

            if (entry != null)
            {
                if (entry.Attributes.Contains(attributeName))
                {
                    Trace.WriteLine("Match attribute");
                    attributeValue = entry.Attributes[attributeName][0].ToString();
                }
            }

            return attributeValue;
        }

        /// <summary>
        /// Verifica que un usuario cuente con el atributo de CIEC Fortalecida
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        private bool ContainsCIECF(SearchResultEntry entry)
        {
            bool result = false;
            string atributeName = "satmarcaciecf";

            if (_isLoggingEnabled)
            {
                Trace.WriteLine("Listing attributes for :" + entry.DistinguishedName);
                
                foreach (DictionaryEntry attr in entry.Attributes)
                {                    
                    string attrName = attr.Key.ToString();
                    string attrValue = entry.Attributes[attrName][0].ToString();
                    Trace.WriteLine(string.Format("Attribute Name:{0} Value:{1}", attrName, attrValue));
                }                            
            }

            if (!entry.Attributes.Contains(atributeName))
            {
                Trace.WriteLine("Missing attribute:" + atributeName);
                throw new Exception(string.Format("El usuario {0} no cuenta con el attributo: {1}", entry.DistinguishedName, atributeName));
            }
            
            string valorAtributo = entry.Attributes[atributeName][0].ToString();
            result = valorAtributo.Equals("Y");

            if (!result)
            {
                string message = string.Format("El usuario {0} no tiene habilitado el atributo de CIEC Fortalecida.", entry.DistinguishedName);
                EventLogHelper.WriteWarningEntry(message, CoreLogEventIdentifier.AUTHENTICATION_EVENT_ID);

                Trace.WriteLine(message);
            }

            return result;
        }

        /// <summary>
        /// Sustituye los caracteres no soportados por el directorio de autenticación con
        /// un caracter auxiliar temporal que el directorio sustituirá de vuelta para validar el usuario.
        /// </summary>
        /// <param name="originalValue"></param>
        /// <returns>La misma cadena recibida, pero con todos los caracteres no soportados por el directorio
        /// reemplazados por caracteres sustitutos que fuéron especificados por el devteam de directorio.</returns>
        private string EscapeDirectoryUnsupportedChars(string originalValue)
        {
            originalValue = originalValue.Replace('&', '_');
            originalValue = originalValue.Replace("Ñ", "\\C3\\91");
            return originalValue;
        }

        #region ServerCertificateCallback
        private static bool ServerCertificateCallback(LdapConnection connection, X509Certificate certificate)
        {
            return (true);
        }
        #endregion

        #region IDisposable Members

        //Se implementa el patron Dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_connection != null)
                    _connection.Dispose();

                if(_isLoggingEnabled)
                  Trace.Close();
            }
        }

        ~LdapManager()
        {
            Dispose(false);
        }

        #endregion
    }
}
