
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Presentacion.Portal:Sat.CreditosFiscales.Presentacion.Portal.MemberCache:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using Sat.CreditosFiscales.Comunes.Entidades;
using Sat.CreditosFiscales.Comunes.Entidades.GeneraFormato;
using Sat.CreditosFiscales.Comunes.Entidades.Servicios;
using Sat.CreditosFiscales.Comunes.Herramientas;
using Sat.CreditosFiscales.Presentacion.Portal.Models;

namespace Sat.CreditosFiscales.Presentacion.Portal
{
    public static class MemberCache
    {
        private static string NombreCookie = "Sat.CreditosFiscales.Cookie";
        private static string NombreCookieCaptcha = "Sat.CreditosFiscales.Cookie.Captcha";
        private static string NombreCookieSeleccion = "Sat.CreditosFiscales.Cookie.Seleccion";

        public static Usuario UsuarioActual
        {
            get
            {
                if (HttpContext.Current.Request.Cookies[NombreCookie] != null)
                {
                    Encripcion encripcion = new Encripcion();
                    string valoresCookie = HttpContext.Current.Request.Cookies[NombreCookie].Value.Replace("Usuario=", "");
                    string cadenaDescifrada = encripcion.DesencriptaCadena(valoresCookie);
                    string valorFuncionario = cadenaDescifrada;
                    StringReader reader = new StringReader(valorFuncionario);

                    XmlSerializer serilizador = new XmlSerializer(typeof(Usuario));
                    return (Usuario)serilizador.Deserialize(new XmlTextReader(reader));
                }
                else
                {
                    return null;
                }
            }

            set
            {
                HttpCookie usuarioCookie = new HttpCookie(NombreCookie);
                Encripcion encripcion = new Encripcion();
                usuarioCookie.Value = encripcion.EncriptaCadena(MetodosComunes.Serializa(value));
                HttpContext.Current.Response.Cookies.Add(usuarioCookie);
            }
        }

        public static string ValorCaptcha
        {
            get
            {
                Encripcion encripcion = new Encripcion();
                string valoresCookie = HttpContext.Current.Request.Cookies[NombreCookieCaptcha].Value;
                string cadenaDescifrada = encripcion.DesencriptaCadena(valoresCookie);
                return cadenaDescifrada;
            }
            set
            {
                HttpCookie captchaCookie = new HttpCookie(NombreCookieCaptcha);
                Encripcion encripcion = new Encripcion();
                captchaCookie.Value = encripcion.EncriptaCadena(value);
                HttpContext.Current.Response.Cookies.Add(captchaCookie);
            }
        }

        public static SeleccionUsuario SeleccionSolicitud
        {
            get
            {
                Encripcion encripcion = new Encripcion();
                if (HttpContext.Current.Request.Cookies[NombreCookieSeleccion] != null)
                {
                    string valoresCookie = HttpContext.Current.Request.Cookies[NombreCookieSeleccion].Value.Replace("SeleccionUsuario=", "");
                    string cadenaDescifrada = encripcion.DesencriptaCadena(valoresCookie);
                    string valorSeleccion = cadenaDescifrada;
                    StringReader reader = new StringReader(valorSeleccion);

                    XmlSerializer serilizador = new XmlSerializer(typeof(SeleccionUsuario));
                    return (SeleccionUsuario)serilizador.Deserialize(new XmlTextReader(reader));
                }
                else
                {
                    return new SeleccionUsuario() { ArchivoTemporal = string.Empty, DocumentosSeleccionados = new List<DocumentoDeterminante>() };
                }
            }

            set
            {
                HttpCookie seleccionCookie = new HttpCookie(NombreCookieSeleccion);
                Encripcion encripcion = new Encripcion();
                seleccionCookie.Value = encripcion.EncriptaCadena(MetodosComunes.Serializa(value));
                HttpContext.Current.Response.Cookies.Add(seleccionCookie);
            }
        }
    }
}