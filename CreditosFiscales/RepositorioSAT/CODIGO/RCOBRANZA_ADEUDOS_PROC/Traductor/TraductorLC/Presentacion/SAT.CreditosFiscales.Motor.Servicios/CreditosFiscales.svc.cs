
//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.Servicios:SAT.CreditosFiscales.Motor.Servicios.CreditosFiscales:1:12/07/2012[Assembly:1.0:12/07/2013])




using System;
using System.ServiceModel.Activation;
using System.Xml;
using SAT.CreditosFiscales.Motor.Entidades.Herramientas;
using SAT.CreditosFiscales.Motor.Negocio.Procesamiento;
using System.ServiceModel.Channels;
using System.ServiceModel;
using System.DirectoryServices.Protocols;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Configuration;
using System.Collections.Generic;


namespace SAT.CreditosFiscales.Motor.Servicios
{

    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class CreditosFiscales : ICreditosFiscales
    {
        public string ObtieneLineaCaptura(short idAplicacion, short idTipoDoc, string documento)
        {

            string respuestaWCF = string.Empty;

            try
            {
                if (PermiteAccesoUsuario())
                {
                    var motor = new Traductor();
                    respuestaWCF = motor.EjecutaMotor(idAplicacion, idTipoDoc, documento, false);

                }
            }
            catch (Exception ex)
            {
                respuestaWCF = "<RespuestaLC><ListaErrores><Error>" + ex.Message + "</Error></ListaErrores><TipoRespuesta>0</TipoRespuesta><IdProcesamiento></IdProcesamiento></RespuestaLC>";
            }

            return respuestaWCF;
        }

        public string ObtieneLineaCapturaConPDF(short idAplicacion, short idTipoDoc, string documento)
        {
            //Traductor motor = new Traductor();
            string respuestaWCF = string.Empty;

            try
            {
                if (PermiteAccesoUsuario())
                {
                    var motor = new Traductor();
                    respuestaWCF = motor.EjecutaMotor(idAplicacion, idTipoDoc, documento, true);
                }
            }
            catch (Exception ex)
            {
                respuestaWCF = "<RespuestaLC><ListaErrores><Error>" + ex.Message + "</Error></ListaErrores><TipoRespuesta>0</TipoRespuesta><IdProcesamiento></IdProcesamiento></RespuestaLC>"; ;
            }

            return respuestaWCF;
        }

        public string ObtieneLineaCapturaConImagen(short pshIdAplicacion, short pshIdTipoDoc, string psDocumento)
        {
            return String.Empty;
        }

        public string ConsultaLineasCapturaXDocumento(string documentos)
        {

            string respuestaWCF = string.Empty;

            try
            {
                if (PermiteAccesoUsuario())
                {
                    SolicitudLineasCapturaExistentes solicitud = MetodosComunes.Deserializa<SolicitudLineasCapturaExistentes>(documentos);
                    RespuestaLineasCapturaExistentes respuestaLcExistentes = BusquedaLineas.BuscaLineasCapturaExistentes(solicitud);
                    respuestaWCF = MetodosComunes.SerlializaObjeto(respuestaLcExistentes);
                }
            }
            catch (Exception ex)
            {
                respuestaWCF = ex.Message;
            }

            return respuestaWCF;

        }

        public string ObtieneDocumentosEnLineaCaptura(string lineaCaptura)
        {
            string respuestaWCF = string.Empty;
            try
            {
                if (PermiteAccesoUsuario())
                {
                    RespuestaResolucionesEnLC respuestaResolucionEnLC = BusquedaLineas.BuscaResolucionesEnLC(lineaCaptura);
                    respuestaWCF = MetodosComunes.SerlializaObjeto(respuestaResolucionEnLC);
                }
            }
            catch (Exception ex)
            {
                respuestaWCF = ex.Message;
            }

            return respuestaWCF;
        }

        public string ObtieneXMLOriginal(string Folio)
        {
            string respuestaWCF = string.Empty;
            try
            {
                if (PermiteAccesoUsuario())
                {
                    RespuestaSolicitudOriginal respuestaOriginal = BusquedaLineas.BuscaXMLOriginal(Folio);
                    respuestaWCF = MetodosComunes.SerlializaObjeto(respuestaOriginal);
                }

            }
            catch (Exception ex)
            {
                respuestaWCF = ex.Message;
            }

            return respuestaWCF;
        }

        public string ObtienePDF(string LineaCaptura)
        {

            //Traductor motor = new Traductor();
            string respuestaWCF = string.Empty;

            try
            {
                if (PermiteAccesoUsuario())
                {
                    string ValLineaCaptura = "";
                    ValLineaCaptura = LineaCaptura;
                    var motor = new Traductor();
                    respuestaWCF = motor.ObtienePDF(ValLineaCaptura);
                }
            }
            catch (Exception ex)
            {
                respuestaWCF = "<RespuestaLC><ListaErrores><Error>" + ex.Message + "</Error></ListaErrores><TipoRespuesta>0</TipoRespuesta><IdProcesamiento></IdProcesamiento></RespuestaLC>"; ;
            }

            return respuestaWCF;
        }

        public string ObtienePDFXFolioyLineadeCaptura(string Folio,string LineaCaptura)
        {

            //Traductor motor = new Traductor();
            string respuestaWCF = string.Empty;

            try
            {
                if (PermiteAccesoUsuario())
                {
                    string ValLineaCaptura = "";
                    ValLineaCaptura = LineaCaptura;
                    var motor = new Traductor();
                    respuestaWCF = motor.ObtienePDFXFolio(Folio,ValLineaCaptura);
                }
            }
            catch (Exception ex)
            {
                respuestaWCF = "<RespuestaLC><ListaErrores><Error>" + ex.Message + "</Error></ListaErrores><TipoRespuesta>0</TipoRespuesta><IdProcesamiento></IdProcesamiento></RespuestaLC>"; ;
            }

            return respuestaWCF;
        }
        public string ObtieneConsultaFormatos(string ADRRFC,string Folio, string LineaDeCaptura,string No_de_Resolucion,int rango_de_emision_FechaPago,DateTime fecha_ini,DateTime fecha_fin)
        {

            //Traductor motor = new Traductor();
            string respuestaWCF = "";
            RespuestaListaFormatos Formato = new RespuestaListaFormatos();
            String[] separar = ADRRFC.Split('|');
            int ADR = 0; ;
            String RFC=String.Empty;
            if (separar[0] != null)
            {
                 ADR = System.Convert.ToInt16(separar[0]);    
            }

            if (separar[1] != null)
            {
                RFC = separar[1];
            }

            try
            {
                if (PermiteAccesoUsuario())
                {
                    
                    
                    var motor = new Traductor();
                    respuestaWCF = motor.ObtieneFormatos(ADR,RFC, Folio, LineaDeCaptura, No_de_Resolucion, rango_de_emision_FechaPago, fecha_ini, fecha_fin);
                }
            }
            catch (Exception ex)
            {
                Formato.Error = "<RespuestaLC><ListaErrores><Error>" + ex.Message + "</Error></ListaErrores><TipoRespuesta>0</TipoRespuesta><IdProcesamiento></IdProcesamiento></RespuestaLC>"; ;
            }

            return respuestaWCF;
        }


        public string ObtieneListaFolios(string RFC)
        {
            return "s";
        }
   
        private bool PermiteAccesoUsuario()
        {
            bool usarAutenticacion = bool.Parse(ConfigurationManager.AppSettings["UsaAutenticacion"].ToString());
            bool accesoConcedido = false;

            if (usarAutenticacion)
            {
                HttpRequestMessageProperty httpRequestProperty = null;
                object propertyValue;
                string datos = string.Empty;
                if (OperationContext.Current.IncomingMessageProperties.TryGetValue(HttpRequestMessageProperty.Name, out propertyValue))
                {
                    httpRequestProperty = (HttpRequestMessageProperty)propertyValue;
                }

                if (httpRequestProperty != null)
                {
                    datos = httpRequestProperty.Headers["Authorization"];
                }

                if (!string.IsNullOrEmpty(datos))
                {
                    string[] datosAutenticacion = datos.Split('|');
                    if (datosAutenticacion.Length.Equals(2))
                    {
                        accesoConcedido = AutenticaVsLDAP(datosAutenticacion);
                    }
                    else
                    {
                        throw new Exception("Los datos de autenticación no se encuentran en el formato correcto");
                    }
                }
                else
                {
                    throw new Exception("La petición no cuenta con los datos de autenticación");
                }
            }
            else
            {
                accesoConcedido = true;
            }

            return accesoConcedido;
        }

        private bool AutenticaVsLDAP(string[] datosAutenticacion)
        {
            bool autenticacionValida = false;

            try
            {
                string dn = SAT.CreditosFiscales.Motor.Negocio.Catalogos.AplicationSettings.ConsultaConfiguracion("LDAP:dn").ToString();
                string usuarioLdap = dn.Replace("{0}", datosAutenticacion[0]);
                string passwordLdap = datosAutenticacion[1];
                string servidorLdap = SAT.CreditosFiscales.Motor.Negocio.Catalogos.AplicationSettings.ConsultaConfiguracion("LDAP:Server").ToString();
                using (LdapConnection ldapConnection = new LdapConnection(new LdapDirectoryIdentifier(servidorLdap)))
                {
                    ldapConnection.Credential = new NetworkCredential(usuarioLdap, passwordLdap);
                    ldapConnection.SessionOptions.SecureSocketLayer = true;
                    ldapConnection.SessionOptions.VerifyServerCertificate = new VerifyServerCertificateCallback(ServerCallback);
                    ldapConnection.AuthType = AuthType.Basic;
                    ldapConnection.Bind();
                    autenticacionValida = true;
                }
            }
            catch (Exception err)
            {
                if (err.Message.Equals("The supplied credential is invalid."))
                {
                    throw new Exception("El usuario o la contraseña son incorrectos.");
                }
                else if (err.Message.Equals("The LDAP server is unavailable."))
                {
                    throw new Exception("El servicio de autenticación LDAP no se encuentra disponible, intentelo más tarde.");
                }
                else
                {
                    throw;
                }
            }
            return autenticacionValida;
        }

        private static bool ServerCallback(LdapConnection connection, X509Certificate certificate)
        {
            ////Simepre se confia en el certificado SSL del lado de NOVELL
            return true;
        }
    }
}
