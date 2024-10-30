
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Procesamiento.LogicaNegocio.Seguridad:Sat.CreditosFiscales.Procesamiento.LogicaNegocio.Seguridad.AutenticacionEmpleados:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using Sat.CreditosFiscales.Comunes.Entidades.AccesoLogEventos;
using Sat.CreditosFiscales.Comunes.Entidades.CodigosError;
using Sat.CreditosFiscales.Comunes.Entidades.Seguridad;
using Sat.CreditosFiscales.Procesamiento.LogicaNegocio.AccesoLogEventos;
using Sat.CreditosFiscales.Procesamiento.LogicaNegocio.Catalogos;
using Sat.CreditosFiscales.Procesamiento.LogicaNegocio.WSIdentity;


namespace Sat.CreditosFiscales.Procesamiento.LogicaNegocio.Seguridad
{
    public class AutenticacionEmpleados
    {
        #region Variables
        public const string fuenteLog = "SAT.Condonaciones.AutenticacionEmpleado";
        public const string exitoso = "_exitoso_";
        public const string errorejecucion = "_error_";


        public Dictionary<string, string> configuracion;
        public List<string> jerarquiaRoles;
        public Dictionary<string, string> relacionRolSatCondonacion;
        public Dictionary<string, string> relacionRolDescripcion;
        public string RolCentral;
        #endregion

        #region Métodos públicos
        /// <summary>
        /// Función que obtine la información del empleado desde el LDAP.
        /// </summary>
        /// <param name="rfc">RFC del empleado autenticado</param>
        /// <returns>Entidad del tipo <see cref="EmpleadoLdap"/></returns>
        public EmpleadoLdap ObtieneInfoEmpleado(string rfc)
        {
            ObtineConfiguraciones();

            EmpleadoLdap empleado = new EmpleadoLdap();
            List<string> roles = new List<string>();
            var binding = CreaBinding(bool.Parse(configuracion["Seguridad:Empleado::UsaHttps"]));

            EndpointAddress endpoint = new EndpointAddress(
                    new Uri(configuracion["Seguridad:Empleado::UrlServicio"]), EndpointIdentity.CreateDnsIdentity(configuracion["Seguridad:Empleado::DnsIdentity"]));

            WSIdentityClient servicio = new WSIdentityClient(binding, endpoint);

            DefineCredenciales(servicio);

            ValidaRoles(empleado, rfc, servicio, binding);

            ObtieneDatosEmpleado(empleado, rfc, servicio);

            empleado.AccesoValido = true;

            return empleado;

        }
        #endregion

        #region Métodos privados
        /// <summary>
        /// Valida la exitencia y jerarquía de los roles del empleado.
        /// </summary>
        /// <param name="empleado">Entidad del empleado donde se agregaran los roles encontrados</param>
        /// <param name="rfc">RFC del empleado</param>
        /// <param name="servicio">Instancia del servicio WSIdentity</param>
        private void ValidaRoles(EmpleadoLdap empleado, string rfc, WSIdentityClient servicio, CustomBinding binding)
        {
            
            AppRolesRequest rolesReq = DefinePeticionRoles(rfc, configuracion["Seguridad:Empleado::Usrapp"], configuracion["Seguridad:Empleado::Passapp"]);

            AppRolesResponse responseRoles = ObtieneRoles(servicio, rolesReq);

            List<string> roles = new List<string>();
            if ((responseRoles.aplicacion.estatusAutenticacion.estatus != errorejecucion) && (responseRoles.aplicacion.estatusAutenticacion.estatus.Equals(exitoso)))
            {
                if (responseRoles.aplicacion.listaEmpleados != null)
                {
                    if (responseRoles.aplicacion.listaEmpleados.empleado.estatusBusqueda.estatus.Equals(exitoso))
                    {
                        if (responseRoles.aplicacion.listaEmpleados.empleado.accesos.estatusAccesos.razon == 0)
                        {
                            if (responseRoles.aplicacion.listaEmpleados.empleado.accesos.listaAccesos.Count() > 0)
                            {
                                var rolesSat = responseRoles.aplicacion.listaEmpleados.empleado.accesos.listaAccesos.ToList();
                                ////var rolesSat = new List<string>() { "SAT_CON_USR_CONA", "SAT_CON_USR_ENT_FED", "SAT_CON_ADM_LOC_REC", "SAT_CON_USR_CENT", "SAT_CON_USR_CENT_SHCP" };

                                //////Se obtienen las jerarquias de cada uno de los roles regresados por Novell.
                                //var rolOrdenado = from acceso in rolesSat
                                //                  join jr in jerarquiaRoles on acceso equals jr
                                //                  select new { Rol = acceso.ToString(), Indice = jerarquiaRoles.IndexOf(acceso.ToString()) };

                                //////Se ordenan los roles por jerarquía y se obtiene el de mayor rango
                                //var rol = (from ro in rolOrdenado
                                //           orderby ro.Indice ascending
                                //           where rolesSat.Contains(ro.Rol)
                                //           select ro.Rol).FirstOrDefault();

                                //if (rol.Contains("CENT"))
                                //{
                                //    empleado.Perfil = PerfilEmpleado.Central;
                                //}
                                //else if (rol.Contains("ENT"))
                                //{
                                //    empleado.Perfil = PerfilEmpleado.Entidad;
                                //}
                                //else if (rol.Contains("LOC"))
                                //{
                                //    empleado.Perfil = PerfilEmpleado.Local;
                                //}
                                //else if (rol.Contains("CONA"))
                                //{
                                //    empleado.Perfil = PerfilEmpleado.Conagua;
                                //}

                                ////Se traduce el RolSAT a RolCondonaciones.
                                //roles.Add(relacionRolSatCondonacion[rol]);
                                //empleado.DescripcionRol = relacionRolDescripcion[rol];
                                roles.Add(rolesSat[0]);
                                
                            }
                            else
                            {
                                throw new Exception("El usuario no cuenta con roles de acceso a la aplicación");
                            }

                        }
                        else
                        {
                            var error = TraduceError(responseRoles.aplicacion.listaEmpleados.empleado.accesos.estatusAccesos.razon.ToString());
                            throw new Exception(error);
                        }
                    }
                    else
                    {
                        var error = TraduceError(responseRoles.aplicacion.listaEmpleados.empleado.estatusBusqueda.razon.ToString());
                        throw new Exception(error);
                    }
                }
            }
            else
            {
                var error = TraduceError(responseRoles.aplicacion.estatusAutenticacion.razon.ToString());
                throw new Exception(error);
            }

            empleado.Rol = roles;

        }

        /// <summary>
        /// Método que construye la consulta de roles
        /// </summary>
        /// <param name="rfc">RFC del empleado</param>
        /// <param name="userApp">Nombre del usuario de servicio de identidad</param>
        /// <param name="pwdApp">Contraseña del usuario de servicio de identidad</param>
        /// <returns></returns>
        private AppRolesRequest DefinePeticionRoles(string rfc, string userApp, string pwdApp)
        {

            AppRolesRequest rolesReq = new AppRolesRequest
            {
                claveUsuario = pwdApp,
                nombreUsuario = userApp,
                contextoBusqueda = ContextType.ambos
            };

            RoleType[] tiposRol = new RoleType[1];
            tiposRol[0] = RoleType.accesos;

            rolesReq.listaTipoRoles = tiposRol;
            RFCList rfcLista = new RFCList { rfcEmpleado = rfc };
            rolesReq.listaEmpleados = rfcLista;

            return rolesReq;
        }

        /// <summary>
        /// Se definen las credenciales de trasnporte mediante certificado identidad
        /// </summary>
        /// <param name="servicio">Instancia del servico WSIdentity</param>
        private void DefineCredenciales(WSIdentityClient servicio)
        {
            string thumbPrint = configuracion["Seguridad:Empleado::CertFindValue"];
            StoreLocation storeLocationCast = (StoreLocation)Enum.Parse(typeof(StoreLocation), configuracion["Seguridad:Empleado::CertLocation"]);
            StoreName storeNameCast = (StoreName)Enum.Parse(typeof(StoreName), configuracion["Seguridad:Empleado::CertStoreName"]);
            X509FindType findByTypeCast = (X509FindType)Enum.Parse(typeof(X509FindType), configuracion["Seguridad:Empleado::CertFindType"]);

            ClientCredentials credenciales = new ClientCredentials();
            credenciales.ServiceCertificate.SetDefaultCertificate(
                storeLocationCast, storeNameCast, findByTypeCast, thumbPrint);
            credenciales.ServiceCertificate.Authentication.CertificateValidationMode =
                X509CertificateValidationMode.None;
            credenciales.ClientCertificate.SetCertificate(
                storeLocationCast, storeNameCast, findByTypeCast, thumbPrint);

            servicio.ClientCredentials.ServiceCertificate.DefaultCertificate =
                credenciales.ServiceCertificate.DefaultCertificate;
            servicio.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode =
                credenciales.ServiceCertificate.Authentication.CertificateValidationMode;
            servicio.ClientCredentials.ClientCertificate.Certificate =
               credenciales.ClientCertificate.Certificate;
        }

        /// <summary>
        /// Método que crea el binding del servivio WsIdentity
        /// </summary>
        /// <returns></returns>
        private CustomBinding CreaBinding(bool conexionSegura)
        {
            BindingElementCollection ciecBinding = new BindingElementCollection();
            TextMessageEncodingBindingElement textMessageEncoding = new TextMessageEncodingBindingElement();
            ciecBinding.Add(textMessageEncoding);


            textMessageEncoding.MessageVersion = MessageVersion.Soap11;



            ////Si la url del servicio Ldap es https
            if (conexionSegura)
            {
                AsymmetricSecurityBindingElement security =
                SecurityBindingElement.CreateMutualCertificateDuplexBindingElement(
                    MessageSecurityVersion.WSSecurity11WSTrust13WSSecureConversation13WSSecurityPolicy12);
                security.AllowSerializedSigningTokenOnReply = false;
                security.RequireSignatureConfirmation = false;
                security.IncludeTimestamp = true;
                security.MessageProtectionOrder = MessageProtectionOrder.SignBeforeEncrypt;
                security.DefaultAlgorithmSuite = SecurityAlgorithmSuite.TripleDesRsa15;

                ciecBinding.Add(security);
                HttpsTransportBindingElement httpsTransport = new HttpsTransportBindingElement();
                httpsTransport.AllowCookies = true;
                ciecBinding.Add(httpsTransport);
            }
            else
            {
                AsymmetricSecurityBindingElement seguridad =
                SecurityBindingElement.CreateMutualCertificateDuplexBindingElement(
                    MessageSecurityVersion.WSSecurity11WSTrust13WSSecureConversation13WSSecurityPolicy12);
                seguridad.AllowSerializedSigningTokenOnReply = false;
                seguridad.RequireSignatureConfirmation = false;
                seguridad.IncludeTimestamp = true;
                seguridad.MessageProtectionOrder = MessageProtectionOrder.SignBeforeEncrypt;
                seguridad.DefaultAlgorithmSuite = SecurityAlgorithmSuite.TripleDesRsa15;

                ciecBinding.Add(seguridad);
                HttpTransportBindingElement httpTransport = new HttpTransportBindingElement();
                httpTransport.AllowCookies = true;
                ciecBinding.Add(httpTransport);
            }



            return new CustomBinding(ciecBinding);
        }

        /// <summary>
        /// Método que solicita los roles al servicio WsIdentity
        /// </summary>
        /// <param name="servicio">Instancia del servicio WsIdentity</param>
        /// <param name="rolesReq">Consulta armada.</param>
        /// <returns></returns> 
        private AppRolesResponse ObtieneRoles(WSIdentityClient servicio, AppRolesRequest rolesReq)
        {
            AppRolesResponse responseRoles = null;

            try
            {
                responseRoles = servicio.AppRoles(rolesReq);
            }
            catch (Exception oex)
            {
                string ticket = LogEventos.EscribirEntradaLog((int)EnumErroresGeneracionFormato.ErrorGenerico, oex);
                throw new ExcepcionTipificada("Error al conectarse al servicio de LDAP", oex, ticket);
                
            }

            return responseRoles;
        }

        /// <summary>
        /// Método que obtiene los datos personales y de ALR del empleado.
        /// </summary>
        /// <param name="empleado">Intancia del tipo <see cref="EmpleadoLdap"/> donde se agregaran los datos</param>
        /// <param name="rfc">RFC del empleado.</param>
        /// <param name="servicio">Instancia del servicio WsIdentity </param>
        private void ObtieneDatosEmpleado(EmpleadoLdap empleado, string rfc, WSIdentityClient servicio)
        {
            AppInfoRequest appInfoReq = new AppInfoRequest
            {
                contextoBusqueda = ContextType.ambos,
                claveUsuario = configuracion["Seguridad:Empleado::Passapp"],
                nombreUsuario = configuracion["Seguridad:Empleado::Usrapp"],
                listaCategorias = new CategoryType[] { CategoryType.personal, CategoryType.ubicacion, CategoryType.identificacion },
                listaEmpleados = new RFCList() { rfcEmpleado = rfc }
            };

            empleado.Alr = new List<string>();
            empleado.AlrCveCorta = new List<string>();

            var infoReq = servicio.AppInfo(appInfoReq);
            if (infoReq.aplicacion.estatusAutenticacion.estatus.Equals(exitoso))
            {
                if (infoReq.aplicacion.listaEmpleados.empleado.personal.listaAtributos != null)
                {
                    empleado.NombreCompleto = infoReq.aplicacion.listaEmpleados.empleado.personal.listaAtributos.nombreCompleto;
                }
                else
                {
                    throw new Exception("El empleado no cuenta con información personal.");
                }

                if (infoReq.aplicacion.listaEmpleados.empleado.identificacion.listaAtributos != null)
                {
                    empleado.Rfc = infoReq.aplicacion.listaEmpleados.empleado.identificacion.listaAtributos.rfcLargo;
                    empleado.RfcCorto = infoReq.aplicacion.listaEmpleados.empleado.identificacion.listaAtributos.rfcCorto;
                }
                else
                {
                    throw new Exception("El empleado no cuenta con información de identificación.");
                }

                if (infoReq.aplicacion.listaEmpleados.empleado.ubicacion.listaAtributos != null
                    && infoReq.aplicacion.listaEmpleados.empleado.ubicacion.listaAtributos.localidadCRM != null)
                {
                    if (!empleado.Rol.FirstOrDefault().Equals(RolCentral))
                    {

                        if (infoReq.aplicacion.listaEmpleados.empleado.ubicacion.listaAtributos.localidadCRM.Substring(0, 1) != "D")
                        {
                            ////El valor de la localidad viene como  "A-#"                            
                            empleado.AlrCveCorta = new List<string> { infoReq.aplicacion.listaEmpleados.empleado.ubicacion.listaAtributos.localidadCRM };
                            empleado.Alr.Add(infoReq.aplicacion.listaEmpleados.empleado.ubicacion.listaAtributos.localidadCRM.Replace("A-",""));

                            #region Traducción
                            //using (TraduccionAlr traduccion = new TraduccionAlr())
                            //{
                            //    string alrTraducida = traduccion.ObtieneTraduccionAlr(empleado.AlrCveCorta[0].ToString());
                            //    if (!string.IsNullOrEmpty(alrTraducida))
                            //    {
                            //        empleado.Alr = new List<string>() { alrTraducida };
                            //    }
                            //    else
                            //    {
                            //        throw new Exception(string.Format("No existe traducción para la ALR {0}", empleado.AlrCveCorta[0]));
                            //    }
                            //} 
                            #endregion
                        }
                    }
                }
                else
                {
                    throw new Exception("El empleado no cuenta con información de ALR.");
                }
            }
            else
            {
                throw new Exception("Error al obtener información del empleado.");
            }
        }

        /// <summary>
        /// Transforma el Número de error a texto.
        /// </summary>
        /// <param name="valor">
        /// El número de error.
        /// </param>
        /// <returns>
        /// La descripción del error
        /// </returns>
        private static string TraduceError(string valor)
        {
            switch (valor)
            {
                case "100":
                    return "WSA_RETURNCODE_AUTHREQUEST";
                case "101":
                    return "WSA_RETURNCODE_AUTHSUCCESS";
                case "102":
                    return "WSA_RETURNCODE_ROLESUCCESS";
                case "103":
                    return "WSA_RETURNCODE_AUTHGENERALERROR";
                case "104":
                    // return "WSA_RETURNCODE_AUTHWRONGPASSWORD";
                    return "La contraseña no corresponde al usuario ingresado, Favor de verificar sus datos.";
                case "105":
                    // return "WSA_RETURNCODE_AUTHNOTSUCHENTRY";
                    return "El usuario no cuenta con los permisos de acceso a este servicio";
                case "106":
                    // return "WSA_RETURNCODE_AUTHBLOCKEDACCOUNT";
                    return "Cuenta Bloqueada.";
                case "107":
                    // return "WSA_RETURNCODE_AUTHMULTIPLEENTRIES";
                    return "Usuario con varios registros.";
                case "108":
                    // return "WSA_RETURNCODE_AUTHNOTACCESS";
                    return "El usuario no puede accesar.";
                case "109":
                    // return "WSA_RETURNCODE_AUTHINVALIDACCESS";
                    return "Acceso inválido.";
                case "110":
                    // return "WSA_RETURNCODE_AUTHNOTROLEACCESS";
                    return "No se puede accesar a los roles.";
                case "111":
                    // return "WSA_RETURNCODE_AUTHNOTCOMMUNICATION";
                    return "No cuenta con comunicación para accesar a la autenticación.";
                case "112":
                    // return "WSA_RETURNCODE_AUTHDISABLEDUSER";
                    return "Usuario desabilitado.";
                case "200":
                    return "WSA_RETURNCODE_SEARCHREQUEST";
                case "201":
                    return "WSA_RETURNCODE_SEARCHSUCCESS";
                case "202":
                    return "WSA_RETURNCODE_SEARCHGENERALERROR";
                case "203":
                    return "WSA_RETURNCODE_SEARCHNOTSUCHENTRY";
                case "204":
                    return "WSA_RETURNCODE_SEARCHMULTIPLEENTRIES";
                case "205":
                    return "WSA_RETURNCODE_SEARCHNOTCOMMUNICATION";
                case "206":
                    return "WSA_RETURNCODE_SEARCHADMINPASSWORD";
                case "207":
                    return "WSA_RETURNCODE_SEARCHDISABLEDUSER";
                case "300":
                    return "WSA_RETURNCODE_SECTIONREQUEST";
                case "301":
                    return "WSA_RETURNCODE_SECTIONSUCCESS";
                case "302":
                    return "WSA_RETURNCODE_SECTIONGENERALERROR";
                case "303":
                    return "WSA_RETURNCODE_SECTIONNOTFOUND";
                case "304":
                    return "WSA_RETURNCODE_SECTIONDENIED";
            }

            return "No identificado.";
        }

        private void ObtineConfiguraciones()
        {            
            configuracion = new Dictionary<string, string>();
            
            var configuraciones = ApplicationSettings.ObtieneGrupoConfiguracion("Seguridad:Empleado");
            
            //////Se agregan las configuraciones necesarias a un diccionario para su posterior utilización.
            foreach (var conf in configuraciones)
            {
                
                    configuracion.Add(conf.SettingName, conf.SettingValue.ToString());
            
            }            
        }
        #endregion
    }
}
