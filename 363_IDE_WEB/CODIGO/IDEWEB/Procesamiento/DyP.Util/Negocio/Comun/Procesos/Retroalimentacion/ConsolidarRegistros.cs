
//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Negocio.Comun.Procesos:ConsolidarRegistros:0:21/Mayo/2008[Assembly:1.1:21/Mayo/2008])
	

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Diagnostics;
using SAT.DyP.Util.Types;

namespace SAT.DyP.Negocio.Comun.Procesos.Retroalimentacion
{
    /// <summary>
    /// Clase que transforma un documento XML de retroalimentación de ducto en donde pueden haber varios registros para el mismo folio
    /// en un documento idéntico pero que sólo tiene un registro por folio.
    /// </summary>
    [Serializable]
    public class ConsolidarRegistros
    {
        /// <summary>
        /// Consolidar registros en el documento XML de retroalimentación de ducto.
        /// </summary>
        /// <param name="inputXmlDocument">El documento de entrada.</param>
        /// <returns>El documento XML transformado.</returns>
        /// <exception cref="SAT.DyP.Util.Types.PlatformException">Cuando ocurre cualquier error de conversión o parsing en cualquier registro del documento.</exception>
        public XmlDocument Execute(XmlDocument inputXmlDocument)
        {
            try
            {
                return ConsolidateNodes(inputXmlDocument);
            }
            catch(Exception ex)
            {
                throw new PlatformException("Error al realizar la transformación del formato XML de retroalimentación al formato XML consolidado.", ex);
            }
        }

        public XmlDocument ExecuteSingle(XmlDocument inputXmldoc, string nameSpace)
        {
            try
            {
                return GenerateConsolidateSingleFile(inputXmldoc, nameSpace);
            }
            catch (Exception ex)
            {
                throw new PlatformException("Error al realizar creacion del documento de retroalimentación al formato XML.", ex);
            }
            
        }

        #region Private

        private XmlDocument ConsolidateNodes(XmlDocument inputXmlDocument)
        {
            if (inputXmlDocument == null) throw new ArgumentNullException("El documento de entrada no puede ser nulo.");

            // Creamos nodo raíz de nuevo xml
            XmlDocument outputXmlDocument = new XmlDocument();
            string rootNamespace = inputXmlDocument.DocumentElement.NamespaceURI + "Consolidada";
            string rootNamespacePrefix = inputXmlDocument.DocumentElement.Prefix;
            XmlNode documentElement = outputXmlDocument.CreateElement(rootNamespacePrefix, @"RespuestaGenerica", rootNamespace);

            // recorremos los nodos de tipo DuctoRoot del documento original y los vamos copiando al documento nuevo,
            // pero eliminando nodos con folios duplicados y consolidando en el nodo único los errores de los nodos eliminados.
            XmlNodeList registros = inputXmlDocument.SelectNodes(@"//DuctoRoot");
            if (registros.Count > 0)
            {
                string folioRegistroConsolidado = null; // folio de los registros que se van a consolidar
                string materiaRegistroConsolidado = null; // materia de los registros que se van a consolidar
                XmlElement registroConsolidado = null; // nodo en el documento xml nuevo que representará el registro transformado.
                XmlElement anexosRegistroConsolidado = null; // nodo en el que se guardarán los anexos del registro consolidado

                foreach (XmlNode registroOriginal in registros)
                {
                    string erroresRegistroActual = null; // agregado de todos los errores de los registros que se consolidarán
                    string folio = GetChildElementValue(registroOriginal, @"Folio");

                    // Comparar folio del registro actual del documento de entrada con el que estamos usando para consolidar en
                    // en el documento de salida. Mientras sea el mismo folio, se consolidarán en el mismo registro de salida.

                    if (!folio.Equals(folioRegistroConsolidado)) // nos encontramos con un nuevo folio
                    {
                        // 1.- Completamos y escribir registro consolidado actual

                        if (registroConsolidado != null) // no es el primer folio del documento, no hay registro consolidado actual que escribir.
                        {
                            CompletarRegistroConsolidado(registroConsolidado, materiaRegistroConsolidado, documentElement);
                        }

                        // 2.- Comenzar siguiente registro consolidado

                        folioRegistroConsolidado = folio;

                        registroConsolidado = registroOriginal.Clone() as XmlElement; // para que todo el contenido del registro se conserve intacto menos los campos que cambiaremos (materia y cadena de errores)
                        registroConsolidado = (XmlElement)outputXmlDocument.ImportNode(registroConsolidado, true);
                        anexosRegistroConsolidado = (XmlElement)registroConsolidado.AppendChild(outputXmlDocument.CreateElement("Anexos"));

                        documentElement.AppendChild(registroConsolidado);

                        // como estamos en el primer registro en el que aparece este folio, en la cadena de errores debe venir la materia: la quitamos de la cadena de errores.
                        string erroresMateria = GetChildElementValue(registroOriginal, @"CadenaError");
                        SplitErroresMateria(erroresMateria, out materiaRegistroConsolidado, out erroresRegistroActual);
                    }
                    else
                    {
                        erroresRegistroActual = GetChildElementValue(registroOriginal, @"CadenaError");
                    }

                    XmlElement anexo = (XmlElement)anexosRegistroConsolidado.AppendChild(outputXmlDocument.CreateElement("Anexo"));
                    
                    XmlElement anexoControl = outputXmlDocument.CreateElement("AnexoControl");
                    anexoControl.InnerText = GetChildElementValue(registroOriginal, @"AnexoControl");

                    XmlElement consecutivoControl = outputXmlDocument.CreateElement("ConsecutivoControl");
                    consecutivoControl.InnerText = GetChildElementValue(registroOriginal, @"ConsecutivoControl");

                    XmlElement cadenaError = outputXmlDocument.CreateElement("CadenaError");
                    cadenaError.InnerText = erroresRegistroActual;

                    anexo.AppendChild(anexoControl);
                    anexo.AppendChild(consecutivoControl);
                    anexo.AppendChild(cadenaError);
                }// end foreach

                if (registroConsolidado != null) // no es el primer folio del documento, no hay registro consolidado actual que escribir.
                {
                    CompletarRegistroConsolidado(registroConsolidado, materiaRegistroConsolidado, documentElement);
                }
            }

            outputXmlDocument.AppendChild(documentElement);
            return outputXmlDocument;
        }

        private void CompletarRegistroConsolidado(XmlElement registroConsolidado, string materiaRegistroConsolidado, XmlNode documentElement)
        {
            SafeRemoveChild(registroConsolidado, "AnexoControl");
            SafeRemoveChild(registroConsolidado, "ConsecutivoControl");
            SafeRemoveChild(registroConsolidado, "CadenaError");
            SetChildElementValue(registroConsolidado, @"Materia", materiaRegistroConsolidado);
            documentElement.AppendChild(registroConsolidado);
        }

        private void SafeRemoveChild(XmlElement element, string childElementName)
        {
            XmlNode n = element.SelectSingleNode(childElementName);
            if(n != null) element.RemoveChild(n);
        }

        private const int LONGITUD_MATERIA = 4;

        private void SplitErroresMateria(string erroresMateria, out string materia, out string errores)
        {
            // la materia son los últimos LONGITUD_MATERIA caracteres de la cadena de errores.

            if (erroresMateria.Length > LONGITUD_MATERIA)
            {
                errores = erroresMateria.Substring(0, erroresMateria.Length - LONGITUD_MATERIA);
                materia = erroresMateria.Substring(erroresMateria.Length - LONGITUD_MATERIA);
            }
            else if (erroresMateria.Length == LONGITUD_MATERIA)
            { 
                errores = "";
                materia = erroresMateria;
            }
            else
            {
                string errorString = String.Format(
                    @"No se pudo extraer la materia de la cadena de errores porque sólo mide {0} caracteres y la longitud del campo Materia debe ser de {1} caracteres.",
                    erroresMateria.Length, 
                    LONGITUD_MATERIA
                    );
                throw new XmlException(errorString);
            }
        }

        private void SetChildElementValue(XmlNode registroOriginal, string childElementName, string newValue)
        {
            XmlElement e = registroOriginal.SelectSingleNode(childElementName) as XmlElement;
            if (e != null)
            {
                e.InnerText = newValue;
            }
            else
            {
                string errorString = String.Format(@"El elemento '{0}' no contiene un sub elemento llamado '{1}'", registroOriginal.Name, childElementName);
                throw new XmlException(errorString);
            }
        }

        private string GetChildElementValue(XmlNode registroOriginal, string childElementName)
        {
            XmlElement e = registroOriginal.SelectSingleNode(childElementName) as XmlElement;
            if (e != null)
            {
                return e.InnerText;
            }
            else
            {
                string errorString = String.Format(@"El elemento '{0}' no contiene un sub elemento llamado '{1}'", registroOriginal.Name, childElementName);
                throw new XmlException(errorString);
            }
        }

        private XmlDocument GenerateConsolidateSingleFile(XmlDocument inputXmlDoc, string nameSpace)
        {
            if (inputXmlDoc == null) throw new ArgumentNullException("El documento de entrada no puede ser nulo.");

            // Creamos nodo raíz de nuevo xml
            XmlDocument outputXmlDocument = new XmlDocument();
            string rootNamespace = nameSpace + "Single";
            string rootNamespacePrefix = inputXmlDoc.DocumentElement.Prefix;
            XmlNode documentElement = outputXmlDocument.CreateElement(rootNamespacePrefix, @"ProcesarDucto", rootNamespace);

            XmlElement odocNode = inputXmlDoc.FirstChild.CloneNode(true) as XmlElement;

            odocNode = (XmlElement)outputXmlDocument.ImportNode(odocNode, true);

            documentElement.AppendChild(odocNode);

            outputXmlDocument.AppendChild(documentElement);
            

            return outputXmlDocument;
        }        
    #endregion
    }
}
