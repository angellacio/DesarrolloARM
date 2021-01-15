
//@(#)SCADE2(W:SKDN08221CO2:SAT.DyP.Presentacion.Seguridad.Autenticacion:wsIDCValidate:0:21/Mayo/2008[SAT.DyP.Presentacion.Seguridad.Autenticacion:1.2:21/Mayo/2008])
	
using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Configuration;
using System.Net;

using SAT.DyP.Negocio.Comun.Procesos;
using SAT.DyP.Negocio.Comun.Tipos;
using SAT.DyP.Presentacion.Seguridad.Autenticacion.Componentes;
using System.Diagnostics;
using SAT.DyP.Util.Logging;
using SAT.DyP.Util.ExceptionHandling;
using SAT.DyP.Util.Security;

namespace SAT.DyP.Presentacion.Seguridad.Autenticacion
{
    /// <summary>
    /// Summary description for IDCValidate
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class idcValidation : System.Web.Services.WebService
    {
        #region ValidateUser
        [WebMethod(Description="Servicio de autenticación para Internet")]
        public bool ValidateUser(string _userName, string _password,bool enableCIECFortalecida)
        {
            bool userIsValid = false;
            try
            {
                IAuthenticationProvider authenticator = CreateConfiguredAuthenticationProvider();
                authenticator.ValidarCIECFortalecida = enableCIECFortalecida;

                userIsValid = authenticator.ValidateUser(_userName, _password);
            }
            catch (Exception ex)
            {
                string errorMessage = String.Format("Error al validar usuario contra servicio de directorio: {0}.", ex);
                EventLogHelper.WriteErrorEntry(errorMessage, CoreLogEventIdentifier.AUTHENTICATION_EVENT_ID);
            }

            return userIsValid;
        }
        #endregion

        [WebMethod(Description="Servicio de autenticación por Modulo")]
        public bool ValidateUserModule(string userName,string password,string profile)
        {
            bool userIsValid = false;

            try
            {
                IAuthenticationProvider authenticator = CreateConfiguredAuthenticationProvider();
                userIsValid = authenticator.ValidateUserModule(userName, password, profile);
            }
            catch (Exception ex)
            {
                string errorMessage = String.Format("Error al validar usuario contra MODULO: {0}.", ex);
                EventLogHelper.WriteErrorEntry(errorMessage, CoreLogEventIdentifier.AUTHENTICATION_EVENT_ID);
            }

            return userIsValid;
        }

        [WebMethod(Description="Obtiene la información del perfil")]
        public string ObtenerPerfil(string userName)
        {
            string perfil = string.Empty;

            Negocio.Comun.Procesos.Autenticacion.VerificarUsuario verificar=new SAT.DyP.Negocio.Comun.Procesos.Autenticacion.VerificarUsuario();

            try
            {
                ControlAcceso datos = verificar.GetInfo(userName);

                if (datos != null)
                {
                    if (!string.IsNullOrEmpty(datos.Perfil))
                        perfil = datos.Perfil;
                    else
                    {
                        string warningMessage = string.Format("No existe información del perfil para {0}.", userName);
                        EventLogHelper.WriteWarningEntry(warningMessage, CoreLogEventIdentifier.AUTHENTICATION_EVENT_ID);
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMessage = String.Format("Error al obtener los datos del perfil {0}.", ex);
                EventLogHelper.WriteWarningEntry(errorMessage, CoreLogEventIdentifier.AUTHENTICATION_EVENT_ID);
            }

            return perfil;      
        }

        [WebMethod(Description="Obtiene la información del certificado FIEL a partir del número de serie.")]
        public FIELCertificateInfo ObtenerDatosFIEL(string certificateSerialNumber)
        {
            IAuthenticationProvider authenticator = CreateConfiguredAuthenticationProvider();
            try
            {
                return authenticator.ObtenerDatosCertificadoFIEL(certificateSerialNumber);
            }
            catch (Exception ex)
            {
                string errorMessage = String.Format(
                    @"Error al obtener los datos del certificado de firma FIEL con número de serie '{0}': {1}.", 
                    certificateSerialNumber, 
                    ex
                );
                EventLogHelper.WriteErrorEntry(errorMessage, CoreLogEventIdentifier.AUTHENTICATION_EVENT_ID);
            }

            return null;
        }

        #region ObtenNombreRazonSocial
        [WebMethod]
        public string ObtenNombreRazonSocial(string _rfc)
        {
            IAuthenticationProvider authenticator = CreateConfiguredAuthenticationProvider();
            return authenticator.ObtenNombreRazonSocial(_rfc);
        }
        #endregion

        #region Obten datos del usuario
        [WebMethod]
        public UserResponse ObtenDatosUsuario(string rfc)
        {
            IAuthenticationProvider authenticator = CreateConfiguredAuthenticationProvider();
            return authenticator.ObtenDatosUsuario(rfc);
        }
        #endregion

        #region Actualiza Mail Usuario
        [WebMethod]
        public bool ActualizaMail(string rfc, string email)
        {
            IAuthenticationProvider authenticator = CreateConfiguredAuthenticationProvider();
            return authenticator.ActualizaMail(rfc, email);
        }
        #endregion

        #region Verificar FIEL
        [WebMethod]
        public bool VerificarFIEL(string rfc)
        {
            bool resultado = false;
            IAuthenticationProvider authenticator = CreateConfiguredAuthenticationProvider();

            try
            {
                resultado = authenticator.VerificarFielContribuyente(rfc);
            }
            catch (Exception ex)
            {
                EventLogHelper.WriteErrorEntry(ex.Message, CoreLogEventIdentifier.AUTHENTICATION_EVENT_ID);
            }

            return resultado;
        }


        #endregion

        private IAuthenticationProvider CreateConfiguredAuthenticationProvider()
        {
            // Aqui antes se decidía entre crear el LDAP provider o el Mock provider
            // (el que simula la autenticación), pero ya ahora el LDAP provider
            // sabe que no tiene que ir al directorio cuando se está simulando.
            return new LdapProvider();
        }

        [WebMethod(Description="Obtiene el valor de un atributo de un usuario de Directorio")]
        public string LeerAtributoDirectorio(string userName, string attributeName)
        {
            string result = string.Empty;

            LdapManager conn = new LdapManager();

            try
            {
                conn.Connect(50);
                result = conn.GetUserAttribute(userName, attributeName);                
            }
            catch (Exception ex)
            {
                EventLogHelper.WriteErrorEntry("Error:" + ex.Message + " " + ex.StackTrace, CoreLogEventIdentifier.AUTHENTICATION_EVENT_ID);
            }
            finally
            {
                conn.Dispose();
            }

            return result;
        }
    }
}
