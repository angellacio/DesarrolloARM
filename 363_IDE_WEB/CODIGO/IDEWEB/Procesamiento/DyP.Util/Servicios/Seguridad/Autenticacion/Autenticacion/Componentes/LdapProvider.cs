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
using SAT.DyP.Negocio.Comun.Tipos;
using SAT.DyP.Negocio.Comun.Procesos;
using System.Diagnostics;
using SAT.DyP.Util.Monitoring;
using SAT.DyP.Util.Security;
using SAT.DyP.Negocio.Comun.Procesos.Seguridad;
using SAT.DyP.Util.Types;
using SAT.DyP.Util.Logging;


namespace SAT.DyP.Presentacion.Seguridad.Autenticacion.Componentes
{
    public class LdapProvider : IAuthenticationProvider
    {       

        #region IAuthenticationProvider Members        

        /// <summary>
        /// Se valida que el RFC enviado no contenga caracteres extraños a excepción de Ñ y &
        /// </summary>
        /// <param name="userName">RFC del usuario a autenticar</param>
        /// <returns>True o False</returns>
        private bool ValidateUserRFC(string userName)
        {
            string expresionRegular = DyP.Util.Configuration.ConfigurationManager.ApplicationSettings.ReadSetting("SAT.DyP.Presentacion.Segurdad.Autenticacion.Componentes::RFCRegEx");
            System.Text.RegularExpressions.Regex expresion = new System.Text.RegularExpressions.Regex(expresionRegular);
            return expresion.IsMatch(userName);
        }

        /// <summary>
        /// Devuelve el estado de la configuración que permite simular la autenticación
        /// </summary>
        private bool SimulateAuthentication
        {
            get
            {
                string simulate = ConfigurationManager.AppSettings["SimularAutenticacion"];

                if (!string.IsNullOrEmpty(simulate))
                {
                    if (simulate.Equals("true"))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Devuelve la cantidad de segundos de espera de la conexión a eDirectory
        /// </summary>
        private int ConnectionTimeOut
        {
            get
            {
                int seconds = 0;

                string configValue = ConfigurationManager.AppSettings["TimeOut"];
                if (!string.IsNullOrEmpty(configValue))
                {
                    try
                    {
                        seconds = Convert.ToInt32(configValue);
                    }
                    catch
                    {
                        seconds = 0;
                    }
                }

                return seconds;
            }
        }

        private bool _ValidarCIECFortalecida;
        /// <summary>
        /// Indica el uso de CIEC Fortalecida para la autenticación
        /// </summary>
        public bool ValidarCIECFortalecida
        {
            get
            {
                return _ValidarCIECFortalecida; 
            }

            set
            {
                _ValidarCIECFortalecida = value;
            }
        }

        public bool ValidateUser(string userName, string password)
        {
            if (!ValidateUserRFC(userName))
            {
                Trace.WriteLine(String.Format("El RFC está mal formado: '{0}'", userName));
                return false;
            }

            Trace.WriteLine(String.Format("Validating user: '{0}' password: '{1}'", userName, password));


            //TODO:Se deja pasar al usuario de pruebas (DUMMY), en el futuro considerar
            //si es necesario su uso
            if (userName.Equals("SETA870101AAA"))
            {                
                string operationToken = SAT.DyP.Util.Configuration.ConfigurationManager.ApplicationSettings.ReadSetting("SAT.DyP.Util::OperationToken");
                if (operationToken.Equals(password))
                    return true;
                else
                    return false;
            }


            bool result = false;
            int seconds = this.ConnectionTimeOut;


            if (this.SimulateAuthentication)
            {
                result = true;
            }
            else
            {
                LdapManager _ldapConn = new LdapManager();

                using (_ldapConn)
                {
                    try
                    {
                        //se realiza la conexión al directorio
                        _ldapConn.Connect(seconds);

                        //se busca el usuario
                        result = _ldapConn.FindAccount(userName, password, this.ValidarCIECFortalecida);
                    }
                    catch (Exception ex)
                    {
                        if (SAT.DyP.Util.ExceptionHandling.ExceptionHandler.HandleException(ex)) throw;
                    }
                    finally
                    {
                        _ldapConn.Dispose();
                    }
                }
            }
                        
            return result;
        }
                

        public bool ValidateUserModule(string userName, string password,string profile)
        {
            bool result = false;

            //Validación para usuarios contra la base de SPED
            try
            {
                result = ValidateDatabaseUsers(userName, password, profile);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return result;
        }

     

        public string ObtenNombreRazonSocial(string rfc)
        {
            string _nombreRazonSocial = string.Empty;

            Contribuyente _contribuyente = ObtenerDatosContribuyente.Execute(rfc);

            if (_contribuyente.RazonSocial != null && _contribuyente.RazonSocial.Trim().Length > 0)
                _nombreRazonSocial = _contribuyente.RazonSocial;

            return _nombreRazonSocial;
        }

        public UserResponse ObtenDatosUsuario(string rfc)
        {
            Contribuyente user = ObtenerDatosContribuyente.Execute(rfc);

            string mail = string.Empty;
            string razonSocial = string.Empty;
            string password = string.Empty;

            if (user.RazonSocial != null && user.RazonSocial.Trim().Length > 0)
                razonSocial = user.RazonSocial;

            if (user.Email != null && user.Email.Trim().Length > 0)
                mail = user.Email;

            //if (user.Password != null && user.Password.Trim().Length > 0)
            //    password = user.Password;

            UserResponse userResponse = new UserResponse(mail, password, razonSocial);
            return userResponse;
        }

        public bool ActualizaMail(string rfc, string email)
        {
            return ActualizaMailContribuyente.Execute(rfc, email);
        }

        public bool VerificarFielContribuyente(string rfc)
        {
          VerificarFIELContribuyente TheHelper = new VerificarFIELContribuyente();
          return TheHelper.Execute(rfc);
        }

        public FIELCertificateInfo ObtenerDatosCertificadoFIEL(string numeroSerie)
        {

            FIELCertificateInfo fielInfo = new FIELCertificateInfo();
            ObtenerDatosCertificadoFIEL processor = new ObtenerDatosCertificadoFIEL();

            try
            {
                CertificateInfo info = processor.Execute(numeroSerie);
                ValidadorCertificado validador = new ValidadorCertificado(info);
                fielInfo=new FIELCertificateInfo(info.SerialNumber, info.ValidityEnd,validador.EsCertificadoActivo,validador.EsTipoFIEL,validador.EsTipoAutenticacion);
            }
            catch (PlatformException pEx)
            {
                EventLogHelper.WriteErrorEntry(pEx.Message, CoreLogEventIdentifier.SECURITY_EVENT_ID);
            }

            return fielInfo;
        }

        #endregion

        /// <summary>
        /// Realiza la validación del usuario en la bd de SPED
        /// </summary>
        /// <param name="userName">usuario</param>
        /// <param name="password">contraseña</param>
        /// <returns></returns>
        private bool ValidateDatabaseUsers(string userName, string password,string profile)
        {
            DbProvider dbSecurityProvider = new DbProvider();
            return dbSecurityProvider.ValidateUserModule(userName, password, profile);
        }
    }
}
