
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Comunes.Herramientas:Sat.CreditosFiscales.Comunes.Herramientas.MetodosComunes:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;


namespace Sat.CreditosFiscales.Comunes.Herramientas
{
    public class MetodosComunes
    {
        /// <summary>
        /// Obtiene la descripción de un enumerado.
        /// </summary>
        /// <param name="enumerador">Enumerado.</param>
        /// <returns>Descripción del enumerado.</returns>
        public static string ObtieneDescripcionEnum(Enum enumerador)
        {
            Type type = enumerador.GetType();
            MemberInfo[] memInfo = type.GetMember(enumerador.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                var atributos = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (atributos != null && atributos.Length > 0)
                {
                    return ((DescriptionAttribute)atributos[0]).Description;
                }
            }

            //// Si no cuenta con el atributo se regresa el nombre.
            return enumerador.ToString();
        }

        /// <summary>
        /// Obtiene el nombre del campo de un enumerador.
        /// </summary>
        /// <param name="enumerador">Enumerador.</param>
        /// <returns>Nombre del campo.</returns>
        public static string ObtieneNombreCampo(Enum enumerador)
        {
            Type type = enumerador.GetType();
            MemberInfo[] memInfo = type.GetMember(enumerador.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                var atributos = memInfo[0].GetCustomAttributes(typeof(Enumeradores.CustomAttributes.NombreCampo), false);

                if (atributos != null && atributos.Length > 0)
                {
                    var x = (atributos[0] as Enumeradores.CustomAttributes.NombreCampo).Value;
                    return x;
                }
            }

            //// Si no cuenta con el atributo se regresa el nombre.
            return enumerador.ToString();
        }

        /// <summary>
        /// Obtiene el nombre del parametro de sql de un enumerador.
        /// </summary>
        /// <param name="enumerador">Enumerador.</param>
        /// <returns>Nombre del campo.</returns>
        public static string ObtieneNombreParametroSql(Enum enumerador)
        {
            Type type = enumerador.GetType();
            MemberInfo[] memInfo = type.GetMember(enumerador.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                var atributos = memInfo[0].GetCustomAttributes(typeof(Enumeradores.CustomAttributes.NombreParametroSql), false);

                if (atributos != null && atributos.Length > 0)
                {
                    var x = (atributos[0] as Enumeradores.CustomAttributes.NombreParametroSql).Value;
                    return x;
                }
            }

            //// Si no cuenta con el atributo se regresa el nombre.
            return enumerador.ToString();
        }

        /// <summary>
        /// Método que serializa un objeto.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Serializa(Object value)
        {
            StringBuilder xmlFuncionario = new StringBuilder();
            XmlSerializer serilizador = new XmlSerializer(value.GetType());
            serilizador.Serialize(XmlTextWriter.Create(xmlFuncionario), value);
            return xmlFuncionario.ToString();
        }

        /// <summary>
        /// Método que deserializa un objeto
        /// </summary>
        /// <typeparam name="T">Objeto de retorno</typeparam>
        /// <param name="strXml">string con XML del objeto serializado</param>
        /// <returns></returns>
        public static T Deserializa<T>(string strXml)
        {
            return (T)Deserializa(strXml, typeof(T));
        }

        public static object Deserializa(string strXml, Type type)
        {
            XmlSerializer serializer = new XmlSerializer(type);
            StringReader rdr = new StringReader(strXml);
            return serializer.Deserialize(rdr);

            //XmlSerializer serializer = new XmlSerializer(type);
            //MemoryStream memStream = new MemoryStream(Encoding.UTF8.GetBytes(strXml));
            //return (type.ReflectedType)serializer.Deserialize(memStream);

        }

        public static bool EsCadenaValida(string cadena, string expReg)
        {
            Regex regex = new Regex(expReg);
            return regex.IsMatch(cadena);
        }

        public static string AplicaFormatoError(string mensaje, string innerException)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("<div class=\"mensajeError\" title=\"Mensaje\"><span class=\"ui-icon ui-icon-alert\" style=\"float: left; margin: 0 7px 0px 0;\"></span><p  style=\"float: left;\">{0}</p></div>", mensaje));
            if (!string.IsNullOrEmpty(innerException)) 
            {
                sb.Append(string.Format("<div id=\"innerException\" style=\"display:none\">{0}</div>", innerException));
            }
            sb.Append("<script>muestraDialogoError();</script>");
            return sb.ToString();

        }

        public static string MensajeCambioDatosOrigen(string mensaje)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("<div class=\"mensajeCambioOrigen\" title=\"Mensaje\"><p style=\"float: left;\">{0}</p></div>", mensaje));
           
            sb.Append("<script>muestraDialogoCambioOrigen();</script>");
            return sb.ToString();

        }
        //public static string AplicaFormatoError(FaultException fe)
        //{
        //return AplicaFormatoError(new Exception(fe.Reason,new Exception(fe.co)))
        //}
    }
}
