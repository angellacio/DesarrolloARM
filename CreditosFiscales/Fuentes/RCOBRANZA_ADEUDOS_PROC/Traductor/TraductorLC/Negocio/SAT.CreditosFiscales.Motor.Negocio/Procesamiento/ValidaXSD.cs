
//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.Negocio:SAT.CreditosFiscales.Motor.Negocio.ValidaXSD:1:12/07/2012[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using SAT.CreditosFiscales.Motor.Entidades.ManejoErrores;
using SAT.CreditosFiscales.Motor.Entidades.Procesamiento;
using SAT.CreditosFiscales.Motor.Negocio.Mensajes;
using SAT.CreditosFiscales.Motor.Utilidades;

namespace SAT.CreditosFiscales.Motor.Negocio.Procesamiento
{
    /// <summary>
    /// Clase de validación XSD
    /// </summary>
    [Serializable]    
    public class ValidaXSD
    {
        /// <summary>
        /// Lista de errores generados en la validación del esquema
        /// </summary>
        public List<string> ListaErroresEsquema { get; set; }

        /// <summary>
        /// Devuelve el número de Errores de un esquema
        /// </summary>
        /// <returns></returns>
        public int ErroresTotales()
        {
            return ListaErroresEsquema.Count();
        }

        /// <summary>
        /// Constructor para validación de Esquemas
        /// </summary>
        /// <param name="transporteRecepcion">Solicitud de recepción</param>
        public RespuestaGenerica ValidaEsquemaXSD(string documento, string xsd, string NameSpace) 
        {
            RespuestaGenerica respuestaValidaXsd;
            try
            {
                respuestaValidaXsd = new RespuestaGenerica();
                var xmlmsg = new XmlDocument();
                xmlmsg.LoadXml(documento);
                ValidaEsquema(xmlmsg, xsd, ref respuestaValidaXsd);
                if (ErroresTotales() > 0)
                {
                    respuestaValidaXsd.EsExitoso = false;
                    //Asignar Errores a Respuesta Genérica
                    foreach (string errorEsquema in ListaErroresEsquema)
                    {
                        respuestaValidaXsd.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.ValidacionEsquema), new Exception(errorEsquema));
                    }
                }
                else
                    respuestaValidaXsd.EsExitoso = true;                
            }
            catch (Exception err)
            {
                respuestaValidaXsd = new RespuestaGenerica();
                respuestaValidaXsd.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.ValidacionEsquema), err);
            }
            return respuestaValidaXsd;            
        }

        /// <summary>
        /// Valida documento xml contra esquema xsd
        /// </summary>
        /// <param name="Xml">Xml a validar</param>
        /// <param name="Xsd">Esquema Xsd</param>
        /// <param name="respuestaValidaXsd">respuesta de validación</param>
        private void ValidaEsquema(XmlDocument Xml, string Xsd, ref  RespuestaGenerica respuestaValidaXsd)
        {
            try
            {
                if (Xml.FirstChild.NodeType == XmlNodeType.XmlDeclaration)
                {
                    string Encoding = ((XmlDeclaration)Xml.FirstChild).Encoding;
                    if (!string.IsNullOrEmpty(Encoding))
                        Xml.InnerXml = Xml.InnerXml.Replace(Encoding, Constantes.ValidEncondingXML);
                }

                UTF8Encoding encoding = new UTF8Encoding();
                using (MemoryStream stream = new MemoryStream(encoding.GetBytes(Xml.InnerXml)))
                {
                    //Se carga el esquema a utilizar               
                    XmlReaderSettings settings = CargaEsquemas(Xsd);
                    settings.ValidationEventHandler += new ValidationEventHandler(this.ValidacionEsquema);

                    ////Se crea la lista que contendrá los errores de esquema
                    this.ListaErroresEsquema = new List<string>();

                    XmlDocument documento = new XmlDocument();
                    documento.Load(XmlReader.Create(stream, settings));
                }

            }
            catch (Exception err)
            {
                respuestaValidaXsd.EsExitoso = false;
                respuestaValidaXsd.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.ValidacionEsquema), err);
            }
        }

        /// <summary>
        /// Validación de esquema xsd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValidacionEsquema(object sender, ValidationEventArgs e)
        {
            this.ListaErroresEsquema.Add(string.Format("{0} Línea: {1}", e.Message, e.Exception.LineNumber));
        }

        /// <summary>
        /// Carga esquema xsd
        /// </summary>
        /// <param name="xsd"></param>
        /// <returns></returns>
        private static XmlReaderSettings CargaEsquemas(string xsd)
        {
            //Se obtiene el nombre del esquema desde la tabla de configuraciones.                        
            try
            {
                using (StringReader strReader = new StringReader(xsd))
                {
                    XmlSchemaSet esquemaSet = new XmlSchemaSet();

                    esquemaSet.Add(null, XmlReader.Create(strReader));
                    XmlReaderSettings settings = new XmlReaderSettings()
                    {
                        ValidationType = ValidationType.Schema,
                        Schemas = esquemaSet,
                        DtdProcessing = DtdProcessing.Parse
                    };

                    return settings;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
