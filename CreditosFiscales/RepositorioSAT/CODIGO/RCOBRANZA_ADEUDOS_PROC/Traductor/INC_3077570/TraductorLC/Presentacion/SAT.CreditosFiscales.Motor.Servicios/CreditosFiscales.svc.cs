//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.Servicios:SAT.CreditosFiscales.Motor.Servicios.CreditosFiscales:1:12/07/2012[Assembly:1.0:12/07/2013])

/*
 * Actualizó: Mario Escarpulli
 * Fecha: 10/Jun/2019
*/
// Referencias de sistema.
using System;
using System.Net;
using System.ServiceModel;
using System.Configuration;
using System.ServiceModel.Channels;
using System.ServiceModel.Activation;
using System.DirectoryServices.Protocols;
using System.Security.Cryptography.X509Certificates;

// Referencias personalizadas.
using SAT.CreditosFiscales.Motor.Negocio.Procesamiento;
using SAT.CreditosFiscales.Motor.Entidades.Herramientas;
using SAT.CreditosFiscales.Motor.Negocio.Catalogos;

namespace SAT.CreditosFiscales.Motor.Servicios
{
	/// <summary>
	/// 
	/// </summary>
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
	public class CreditosFiscales : ICreditosFiscales
	{
		/// <summary>
		/// Permite generar una Línea de Captura.
		/// </summary>
		/// <param name="idAplicacion">Identificador único (ID) de la Aplicación que envía la petición.</param>
		/// <param name="idTipoDoc">Identificador único (ID) del Tipo de Documento que se envía.</param>
		/// <param name="documento">Documento en formato XML con los datos necesarios para generar la Línea de Captura.</param>
		/// <returns>Cadena de caracter.</returns>
		public string ObtieneLineaCaptura(short idAplicacion, short idTipoDoc, string documento)
		{
			string sRespuestaWcf = string.Empty;

			try
			{
				if (PermiteAccesoUsuario())
				{
					var motor = new Traductor();
					sRespuestaWcf = motor.EjecutaMotor(idAplicacion, idTipoDoc, documento, false);
				}
			}
			catch (Exception ex)
			{
				sRespuestaWcf = "<RespuestaLC><ListaErrores><Error>" + ex.Message + "</Error></ListaErrores><TipoRespuesta>0</TipoRespuesta><IdProcesamiento></IdProcesamiento></RespuestaLC>";
			}

			return sRespuestaWcf;
		}

		/// <summary>
		/// Permite generar una Línea de Captura que se guardará en un archivo con formato PDF.
		/// </summary>
		/// <param name="idAplicacion">Identificador único (ID) de la Aplicación que envía la petición.</param>
		/// <param name="idTipoDoc">Identificador único (ID) del Tipo de Documento que se envía.</param>
		/// <param name="documento">Documento en formato XML con los datos necesarios para generar la Línea de Captura.</param>
		/// <returns>Cadena de caracter.</returns>
		public string ObtieneLineaCapturaConPDF(short idAplicacion, short idTipoDoc, string documento)
		{
			string sRespuestaWcf = string.Empty;

			try
			{
				if (PermiteAccesoUsuario())
				{
					var voMotor = new Traductor();
					sRespuestaWcf = voMotor.EjecutaMotor(idAplicacion, idTipoDoc, documento, true);
				}
			}
			catch (Exception eError)
			{
				sRespuestaWcf = "<RespuestaLC><ListaErrores><Error>" + eError.Message + "</Error></ListaErrores><TipoRespuesta>0</TipoRespuesta><IdProcesamiento></IdProcesamiento></RespuestaLC>";
			}

			return sRespuestaWcf;
		}

		/// <summary>
		/// Permite generar una Línea de Captura que se guardará en una imagen con formato PNG.
		/// </summary>
		/// <param name="pshIdAplicacion">Identificador único (ID) de la Aplicación que envía la petición.</param>
		/// <param name="pshIdTipoDoc">Identificador único (ID) del Tipo de Documento que se envía.</param>
		/// <param name="psDocumento">Documento en formato XML con los datos necesarios para generar la Línea de Captura.</param>
		/// <returns>Cadena de caracter.</returns>
		public string ObtieneLineaCapturaConImagen(short pshIdAplicacion, short pshIdTipoDoc, string psDocumento)
		{
			/*
			 * ----------------------------------------
			 * Creado por: Mario Escarpulli 
			 * Creado: 15/Junio/2019
			 * NOTAS: Esta clase se creó con base al Requerimiento de Servicio (RES)
			 *        CZA_CREDFIS: Nuevo Servicio en Motor Traductor para la entrega de sección de línea de captura.
			 * ----- Historial de actualizaciones -----
			 * Actualizado por: Mario Escarpulli
			 * Actualizado el: 20/Junio/2019
			 * ----------------------------------------
			*/
			string sRespuestaWcf = string.Empty;

			try
			{
				if (PermiteAccesoUsuario())
				{
					var voMotor = new Traductor();
					sRespuestaWcf = voMotor.EjecutaMotorImagen(pshIdAplicacion, pshIdTipoDoc, psDocumento, true);
				}
			}
			catch (Exception eError)
			{
				sRespuestaWcf = "<RespuestaLC><ListaErrores><Error>" + eError.Message + "</Error></ListaErrores><TipoRespuesta>0</TipoRespuesta><IdProcesamiento></IdProcesamiento></RespuestaLC>";
			}

			return sRespuestaWcf;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="documentos"></param>
		/// <returns></returns>
		public string ConsultaLineasCapturaXDocumento(string documentos)
		{
			string sRespuestaWcf = string.Empty;

			try
			{
				if (PermiteAccesoUsuario())
				{
					SolicitudLineasCapturaExistentes solicitud = MetodosComunes.Deserializa<SolicitudLineasCapturaExistentes>(documentos);
					RespuestaLineasCapturaExistentes respuestaLcExistentes = BusquedaLineas.BuscaLineasCapturaExistentes(solicitud);
					sRespuestaWcf = MetodosComunes.SerlializaObjeto(respuestaLcExistentes);
				}
			}
			catch (Exception ex)
			{
				sRespuestaWcf = ex.Message;
			}

			return sRespuestaWcf;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="lineaCaptura"></param>
		/// <returns></returns>
		public string ObtieneDocumentosEnLineaCaptura(string lineaCaptura)
		{
			string sRespuestaWcf = string.Empty;
			try
			{
				if (PermiteAccesoUsuario())
				{
					RespuestaResolucionesEnLC respuestaResolucionEnLC = BusquedaLineas.BuscaResolucionesEnLC(lineaCaptura);
					sRespuestaWcf = MetodosComunes.SerlializaObjeto(respuestaResolucionEnLC);
				}
			}
			catch (Exception ex)
			{
				sRespuestaWcf = ex.Message;
			}

			return sRespuestaWcf;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="Folio"></param>
		/// <returns></returns>
		public string ObtieneXMLOriginal(string Folio)
		{
			string sRespuestaWcf = string.Empty;
			try
			{
				if (PermiteAccesoUsuario())
				{
					RespuestaSolicitudOriginal respuestaOriginal = BusquedaLineas.BuscaXMLOriginal(Folio);
					sRespuestaWcf = MetodosComunes.SerlializaObjeto(respuestaOriginal);
				}

			}
			catch (Exception ex)
			{
				sRespuestaWcf = ex.Message;
			}

			return sRespuestaWcf;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="LineaCaptura"></param>
		/// <returns></returns>
		public string ObtienePDF(string LineaCaptura)
		{
			//var voMotor = new Traductor();
			string sRespuestaWcf = string.Empty;

			try
			{
				if (PermiteAccesoUsuario())
				{
					string sValLineaCaptura = string.Empty;
					sValLineaCaptura = LineaCaptura;
					var voMotor = new Traductor();
					sRespuestaWcf = voMotor.ObtienePDF(sValLineaCaptura);
				}
			}
			catch (Exception eError)
			{
				sRespuestaWcf = "<RespuestaLC><ListaErrores><Error>" + eError.Message + "</Error></ListaErrores><TipoRespuesta>0</TipoRespuesta><IdProcesamiento></IdProcesamiento></RespuestaLC>"; ;
			}

			return sRespuestaWcf;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="Folio"></param>
		/// <param name="LineaCaptura"></param>
		/// <returns></returns>
		public string ObtienePDFXFolioyLineadeCaptura(string Folio, string LineaCaptura)
		{
			//var motor = new Traductor();
			string sRespuestaWcf = string.Empty;

			try
			{
				if (PermiteAccesoUsuario())
				{
					string sValLineaCaptura = string.Empty;
					sValLineaCaptura = LineaCaptura;
					var motor = new Traductor();
					sRespuestaWcf = motor.ObtienePDFXFolio(Folio, sValLineaCaptura);
				}
			}
			catch (Exception ex)
			{
				sRespuestaWcf = "<RespuestaLC><ListaErrores><Error>" + ex.Message + "</Error></ListaErrores><TipoRespuesta>0</TipoRespuesta><IdProcesamiento></IdProcesamiento></RespuestaLC>"; ;
			}

			return sRespuestaWcf;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ADR"></param>
		/// <param name="RFC"></param>
		/// <param name="Folio"></param>
		/// <param name="LineaDeCaptura"></param>
		/// <param name="No_de_Resolucion"></param>
		/// <param name="rango_de_emision_FechaPago"></param>
		/// <param name="fecha_ini"></param>
		/// <param name="fecha_fin"></param>
		/// <returns></returns>
        public string ObtieneConsultaFormatos(string ADRRFC, string Folio, string LineaDeCaptura, string No_de_Resolucion, int rango_de_emision_FechaPago, DateTime fecha_ini, DateTime fecha_fin)
		{
			//var motor = new Traductor();
            string sRespuestaWcf = "";
            RespuestaListaFormatos Formato = new RespuestaListaFormatos();
            String[] separar = ADRRFC.Split('|');
            int ADR = 0; ;
            String RFC = String.Empty;
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
					sRespuestaWcf = motor.ObtieneFormatos(ADR, RFC, Folio, LineaDeCaptura, No_de_Resolucion, rango_de_emision_FechaPago, fecha_ini, fecha_fin);
				}
			}
			catch (Exception ex)
			{
				Formato.Error = "<RespuestaLC><ListaErrores><Error>" + ex.Message + "</Error></ListaErrores><TipoRespuesta>0</TipoRespuesta><IdProcesamiento></IdProcesamiento></RespuestaLC>"; ;
			}

			return sRespuestaWcf;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="RFC"></param>
		/// <returns></returns>
		public string ObtieneListaFolios(string RFC)
		{
			return "s";
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
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
					httpRequestProperty = (HttpRequestMessageProperty) propertyValue;

				if (httpRequestProperty != null)
					datos = httpRequestProperty.Headers["Authorization"];

				if (!string.IsNullOrEmpty(datos))
				{
					string[] datosAutenticacion = datos.Split('|');
					if (datosAutenticacion.Length.Equals(2))
						accesoConcedido = AutenticaVsLDAP(datosAutenticacion);
					else
						throw new Exception("Los datos de autenticación no se encuentran en el formato correcto");
				}
				else
					throw new Exception("La petición no cuenta con los datos de autenticación");
			}
			else
				accesoConcedido = true;

			return accesoConcedido;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="datosAutenticacion"></param>
		/// <returns>Valor de tipo Lógico.</returns>
		private bool AutenticaVsLDAP(string[] datosAutenticacion)
		{
			bool autenticacionValida = false;

			try
			{
				string dn = AplicationSettings.ConsultaConfiguracion("LDAP:dn").ToString();
				string usuarioLdap = dn.Replace("{0}", datosAutenticacion[0]);
				string passwordLdap = datosAutenticacion[1];
				string servidorLdap = AplicationSettings.ConsultaConfiguracion("LDAP:Server").ToString();
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
					throw new Exception("El usuario o la contraseña son incorrectos.");
				else if (err.Message.Equals("The LDAP server is unavailable."))
					throw new Exception("El servicio de autenticación LDAP no se encuentra disponible, intentelo más tarde.");
				else
					throw;
			}

			return autenticacionValida;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="connection"></param>
		/// <param name="certificate"></param>
		/// <returns>Valor de tipo Lógico.</returns>
		private static bool ServerCallback(LdapConnection connection, X509Certificate certificate)
		{
			////Simepre se confia en el certificado SSL del lado de NOVELL
			return true;
		}
	}
}