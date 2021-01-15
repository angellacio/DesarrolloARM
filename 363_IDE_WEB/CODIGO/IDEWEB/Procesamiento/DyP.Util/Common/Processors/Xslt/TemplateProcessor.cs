//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Util.Processors.Xslt:TemplateProcessor:0:21/May/2008[ SAT.DyP.Util.Processors:1.0:21/May/2008])
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Xsl;
using System.Xml.XPath;
using SAT.DyP.Util.ExceptionHandling;

namespace SAT.DyP.Util.Processors.Xslt
{
    public class TemplateProcessor
    {
        private XslCompiledTransform _xsltProcessor;

        public void Initialize(string xsltTemplate)
        {
            try
            {
                TextReader _textReader = new StringReader(xsltTemplate);
                XmlTextReader _xReader = new XmlTextReader(_textReader);
                XPathDocument _xDoc = new XPathDocument(_xReader);

                _xsltProcessor = new XslCompiledTransform();
                _xsltProcessor.Load(_xDoc);

            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionHandler.HandleException(ex);               
            }
            
        }

        public string Transform(XmlDocument xmlContent)
        {
            if (_xsltProcessor == null)
                throw new SAT.DyP.Util.Types.PlatformException("You must first initialize the XSLT transformer before you can use it.");

            string _result = string.Empty;

            //Serilize entity object
            string _serializableObject = xmlContent.InnerXml;
            _result=TransformXslt(_serializableObject);
            
            return _result;
        }

        public string Transform(object serializableEntity)
        {
            if (_xsltProcessor == null)
                throw new SAT.DyP.Util.Types.PlatformException("You must first initialize the XSLT transformer before you can use it.");

            string _result = string.Empty;

            //Serilize entity object
            string _serializableObject = SerializeObject(serializableEntity);

            //Se agrega funcionalidad para eliminar caracteres inválidos para el Estandard XML
            Utilities.CleanInvalidXmlChars(_serializableObject);
            _result=TransformXslt(_serializableObject);
            
            return _result;
        }

        public string TransformXslt(string content)
        {
            string _result = string.Empty;
            try
            {
                //define reader
                TextReader _textReader = new StringReader(content);
                XmlReader _reader = XmlReader.Create(_textReader);
                               
                //define writer
                StringBuilder _builder=new StringBuilder();
                TextWriter _textWriter=new StringWriter(_builder);
                XmlWriter _writer=XmlWriter.Create(_textWriter);                

                //apply transform 
                _xsltProcessor.Transform(_reader, _writer);                
                //get result

                _result = _builder.ToString();

                _writer.Close();               
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionHandler.HandleException(ex);
                if (rethrow)                    
                    throw new SAT.DyP.Util.Types.PlatformException(ex.Message, ex);
            }

            return _result;
        }

        private string SerializeObject(object parameter)
        {
            Encoding _documentEncoding=Encoding.GetEncoding("iso-8859-1");

            MemoryStream _serializationStream = new MemoryStream();
            XmlSerializer _serializer = new XmlSerializer(parameter.GetType());
            XmlTextWriter _writer = new XmlTextWriter(_serializationStream, _documentEncoding);
            _serializer.Serialize(_writer,parameter);

            _serializationStream = (MemoryStream)_writer.BaseStream;

            string _data = _documentEncoding.GetString(_serializationStream.ToArray());
            _serializationStream.Close();

            return _data;
        }
    }
}
