using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Text;
using System.IO;
using System.Xml.Schema;
using System.Xml.Xsl;
using System.Text.RegularExpressions;
using SAT.DyP.Negocio.Comun.Tipos;
using SAT.DyP.Negocio.Comun.Procesos;


namespace ValidacionIdeWeb
{
    public class Acuse
    {
        private String pathAcuse;

        public String PathAcuse
        {
            get { return pathAcuse; }
            set { pathAcuse = value; }
        }

        public String generarAcuseAceptacion(Declaracion declaracion)
        {
            String pathNombreAcuse = null;
            if (declaracion != null)
            {
               
                string nombreAcuse = this.generarNombreAcuse(true, declaracion);
                
                string directorioAcuses = AccesoDatos.getValorString(Constantes.Validar.pathAcuses);
                if (!Directory.Exists(directorioAcuses))
                    Directory.CreateDirectory(directorioAcuses);
                pathNombreAcuse = Path.Combine(directorioAcuses, nombreAcuse);

                XmlTextWriter acuseAceptacionXml = new XmlTextWriter(pathNombreAcuse, Encoding.UTF8);
                acuseAceptacionXml.Formatting = Formatting.Indented;
                acuseAceptacionXml.WriteStartDocument(false);
                if (declaracion.EsAnual)
                {
                    acuseAceptacionXml.WriteStartElement(Constantes.ElementoXmlAcuse.AcuseAceptacionAnualIDE);
                }
                else
                {
                    acuseAceptacionXml.WriteStartElement(Constantes.ElementoXmlAcuse.AcuseAceptacionMensualIDE);
                }
                acuseAceptacionXml.WriteAttributeString(Constantes.AtributoXmlAcuse.Version,AccesoDatos.getValorString(Constantes.Validar.versionAcuse));
                acuseAceptacionXml.WriteAttributeString(Constantes.AtributoXmlAcuse.Rfc, declaracion.CadenaOriginal.RfcDeclarante);
                acuseAceptacionXml.WriteAttributeString(Constantes.AtributoXmlAcuse.Denominacion, declaracion.CadenaOriginal.Denominacion);
                string strFechaRecepcion = String.Format("{0:dd/MM/yyyy hh:mm:ss}", declaracion.FechaRecepcion);
                acuseAceptacionXml.WriteAttributeString(Constantes.AtributoXmlAcuse.FechaPresentacion, strFechaRecepcion);
                acuseAceptacionXml.WriteAttributeString(Constantes.AtributoXmlAcuse.FolioRecepcion, declaracion.Folio.ToString());
                acuseAceptacionXml.WriteAttributeString(Constantes.AtributoXmlAcuse.NumeroOperacion, declaracion.NumeroOperacion);
                acuseAceptacionXml.WriteAttributeString(Constantes.AtributoXmlAcuse.NombreArchivo, declaracion.NombreArchivo);
                acuseAceptacionXml.WriteAttributeString(Constantes.AtributoXmlAcuse.TamanoArchivo, declaracion.TamañoArchivo.ToString());
                acuseAceptacionXml.WriteAttributeString(Constantes.AtributoXmlAcuse.Ejercicio, declaracion.CadenaOriginal.Ejercicio.ToString());
                if(declaracion.CadenaOriginal.Periodo!=0)
                    acuseAceptacionXml.WriteAttributeString(Constantes.AtributoXmlAcuse.Periodo, this.getPeriodo(declaracion.NombreArchivo));
                string tipo="";
                if (declaracion.EsNormal)
                {
                    tipo = Constantes.Validar.acuseEnumNormal;
                }
                else
                {
                    tipo = Constantes.Validar.acuesEnumComplementaria;
                }
                acuseAceptacionXml.WriteAttributeString(Constantes.AtributoXmlAcuse.Tipo, tipo);
                acuseAceptacionXml.WriteAttributeString(Constantes.AtributoXmlAcuse.TotalRecaudado, declaracion.CadenaOriginal.ImporteRecaudadoDepositos.ToString());
                acuseAceptacionXml.WriteAttributeString(Constantes.AtributoXmlAcuse.TotalEnterado, declaracion.CadenaOriginal.ImporteEnterado.ToString());
                string strFechaModificacion = String.Format("{0:dd/MM/yyyy hh:mm:ss}", declaracion.FechaModificacion);
                acuseAceptacionXml.WriteAttributeString(Constantes.AtributoXmlAcuse.FechaHoraEmisionAcuse, strFechaModificacion);
                acuseAceptacionXml.WriteAttributeString(Constantes.AtributoXmlAcuse.Sello, declaracion.SelloDigital);
                acuseAceptacionXml.WriteAttributeString(Constantes.AtributoXmlAcuse.CadenaOriginal,declaracion.CadenaOriginal.ToString());
                acuseAceptacionXml.WriteEndElement();

                acuseAceptacionXml.Flush();
                acuseAceptacionXml.Close();
            }
            return pathNombreAcuse;
        }

        public String generarAcuseRechazo(Declaracion declaracion, List<String> motivosRechazo)
        {
            String pathNombreAcuse = null;
            if (declaracion != null && motivosRechazo != null && motivosRechazo.Count>0)
            {
                string nombreAcuse = this.generarNombreAcuse(false, declaracion);
                bool anual = false;
                bool normal = false;

                string directorioAcuses=AccesoDatos.getValorString(Constantes.Validar.pathAcuses);
                if (!Directory.Exists(directorioAcuses))
                    Directory.CreateDirectory(directorioAcuses);
                pathNombreAcuse = Path.Combine(directorioAcuses, nombreAcuse);
                
                XmlTextWriter acuseRechazoXml = new XmlTextWriter(pathNombreAcuse, Encoding.UTF8);
                acuseRechazoXml.Formatting = Formatting.Indented;
                acuseRechazoXml.WriteStartDocument(false);
                if (declaracion.EsAnual)
                {
                    acuseRechazoXml.WriteStartElement(Constantes.ElementoXmlAcuse.AcuseRechazoAnualIDE);
                    anual = true;
                }
                else
                {
                    acuseRechazoXml.WriteStartElement(Constantes.ElementoXmlAcuse.AcuseRechazoMensualIDE);
                }
                acuseRechazoXml.WriteAttributeString(Constantes.AtributoXmlAcuse.Version,AccesoDatos.getValorString(Constantes.Validar.versionAcuse));
                acuseRechazoXml.WriteAttributeString(Constantes.AtributoXmlAcuse.Rfc, declaracion.RfcContribuyente);
                acuseRechazoXml.WriteAttributeString(Constantes.AtributoXmlAcuse.Denominacion,this.getDenominacion(declaracion.RfcContribuyente));
                string strFechaRecepcion = String.Format("{0:dd/MM/yyyy hh:mm:ss}", declaracion.FechaRecepcion);                
                acuseRechazoXml.WriteAttributeString(Constantes.AtributoXmlAcuse.FechaPresentacion, strFechaRecepcion);
                acuseRechazoXml.WriteAttributeString(Constantes.AtributoXmlAcuse.FolioRecepcion, declaracion.Folio.ToString());
                acuseRechazoXml.WriteAttributeString(Constantes.AtributoXmlAcuse.NombreArchivo, declaracion.NombreArchivo);
                acuseRechazoXml.WriteAttributeString(Constantes.AtributoXmlAcuse.Ejercicio, this.getEjercicioFiscal(declaracion.NombreArchivo));
                acuseRechazoXml.WriteAttributeString(Constantes.AtributoXmlAcuse.TamanoArchivo, declaracion.TamañoArchivo.ToString());
                string tipo = "";
                if (declaracion.EsNormal)
                {
                    tipo = Constantes.Validar.acuseEnumNormal;
                    normal = true;
                }
                else
                {
                    tipo = Constantes.Validar.acuesEnumComplementaria;
                }
                acuseRechazoXml.WriteAttributeString(Constantes.AtributoXmlAcuse.Tipo, tipo);
                acuseRechazoXml.WriteAttributeString(Constantes.AtributoXmlAcuse.Periodo, this.getPeriodo(declaracion.NombreArchivo));
                string strFechaRechazo = String.Format("{0:dd/MM/yyyy hh:mm:ss}", declaracion.FechaModificacion);
                acuseRechazoXml.WriteAttributeString(Constantes.AtributoXmlAcuse.FechaRechazo, strFechaRechazo); 
                for (int i = 0; i < motivosRechazo.Count; i++)
                {
                    acuseRechazoXml.WriteStartElement(Constantes.ElementoXmlAcuse.Error);
                    acuseRechazoXml.WriteAttributeString(Constantes.AtributoXmlAcuse.Clave,AccesoDatos.getValorString(Constantes.Validar.claveErrorAcuse));
                    acuseRechazoXml.WriteAttributeString(Constantes.AtributoXmlAcuse.Descripcion, motivosRechazo[i]);
                    acuseRechazoXml.WriteEndElement();

                }
                acuseRechazoXml.WriteEndElement();

                acuseRechazoXml.Flush();
                acuseRechazoXml.Close();
                try
                {
                    declaracion.CadenaOriginal = new CadenaOriginal() { EsAnual = anual, EsNormal = normal };
                    declaracion.CadenaOriginal.Periodo = Convert.ToInt16(declaracion.NombreArchivo.Substring(19, 2));
                    declaracion.CadenaOriginal.Ejercicio = Convert.ToInt32(this.getEjercicioFiscal(declaracion.NombreArchivo));
                    declaracion.CadenaOriginal.OperacionesRelacionadas = 0;
                    AccesoDatos.ComplementarDatosRecepcion(declaracion, Constantes.Validar.tipoArchivoDelcaracion);
                    declaracion.CadenaOriginal = null;
                }
                catch (Exception e)
                {
                    AccesoDatos.RegistrarEvento(declaracion.Folio, e.Message + " - Error Al cargar datos complementarios de rechazo == ");
                }
            }
            return pathNombreAcuse;
        }

        private string generarNombreAcuse(bool esAcuseAceptacion, Declaracion declaracion)
        {
            string nombre = null;
            DateTime fecha = new DateTime();
            if (declaracion != null)
            {
                if (esAcuseAceptacion)
                {
                    if (declaracion.EsAnual)
                    {
                        nombre = AccesoDatos.getValorString(Constantes.ExpresionRegular.AcuseAceptacionAnual);
                    }
                    else
                    {
                        nombre = AccesoDatos.getValorString(Constantes.ExpresionRegular.AcuseAceptacionMensual);
                    }
                    fecha = DateTime.Now;
                }
                else
                {
                    if (declaracion.EsAnual)
                    {
                        nombre = AccesoDatos.getValorString(Constantes.ExpresionRegular.AcuseRechazoAnual);
                    }
                    else
                    {
                        nombre = AccesoDatos.getValorString(Constantes.ExpresionRegular.AcuseRechazoMensual);
                    }
                    fecha = declaracion.FechaModificacion;
                }

                nombre = nombre + AccesoDatos.getValorString(Constantes.ExpresionRegular.NumeroNombreAcuse) + this.getFecha(fecha) + ".xml";

            }
            return nombre;
        }

        private String getFecha(DateTime fecha)
        {
            String  strFecha =null;
            if (fecha != null)
            {
                strFecha= String.Format("{0:yyyyMMddhhmmss}", fecha);                
            }
            return strFecha;
        }

        private string getEjercicioFiscal(string nombreArchivo)
        {
            string ejercicioFiscal = "";

            if (nombreArchivo != null && nombreArchivo.Length >= 19)
            {
                ejercicioFiscal = nombreArchivo.Substring(15, 4);               
            }            
            return ejercicioFiscal;
        }

        private string getPeriodo(string nombreArchivo)
        {
            string periodo = "";
            if (nombreArchivo != null && nombreArchivo.Length >= 21)
            {
                periodo = nombreArchivo.Substring(19, 2);
                try
                {
                    int intPeriodo = Convert.ToInt32(periodo);

                    switch (intPeriodo)
                    {
                        case 1:
                            periodo = "ENERO";
                            break;
                        case 2:
                            periodo = "FEBRERO";
                            break;
                        case 3:
                            periodo = "MARZO";
                            break;
                        case 4:
                            periodo = "ABRIL";
                            break;
                        case 5:
                            periodo = "MAYO";
                            break;
                        case 6:
                            periodo = "JUNIO";
                            break;
                        case 7:
                            periodo = "JULIO";
                            break;
                        case 8:
                            periodo = "AGOSTO";
                            break;
                        case 9:
                            periodo = "SEPTIEMBRE";
                            break;
                        case 10:
                            periodo = "OCTUBRE";
                            break;
                        case 11:
                            periodo = "NOVIEMBRE";
                            break;
                        case 12:
                            periodo = "DICIEMBRE";
                            break;
                        default:
                            periodo = "";
                            break;
                    }
                }
                catch
                {
                    periodo = nombreArchivo.Substring(19, 2);
                }
            }
            return periodo;
        }

        private string getDenominacion(string rfc)
        {
            string denominacion = "";
            Contribuyente contribuyente = new Contribuyente();
            contribuyente = ObtenerDatosContribuyente.Execute(rfc);
            if (contribuyente != null && contribuyente.RazonSocial != null)
            {
              denominacion = contribuyente.RazonSocial;
            }
            else
            {
              SAT.DyP.Util.Logging.EventLogHelper.WriteWarningEntry("No se encontró información del Contribuyente, para la generación del acuse de rechazo.", 251);
            }
            return denominacion;
        }
       
        public String leerAcuse(string path)
        {
            String contenido = null;
            if (File.Exists(path))
            {               
                XmlDocument doc = new XmlDocument();

                doc.Load(path);
                contenido=doc.InnerXml;               
            }
            return contenido;
        }

        public string runXSLT(string xsltFile, string inputXML)
        {

            XmlDocument XmlDoc = new XmlDocument();



            XslCompiledTransform xslt = new XslCompiledTransform(true);

            xslt.Load(xsltFile);



            StringReader StrReader = new StringReader(inputXML);

            XmlTextReader XmlReader = new XmlTextReader(StrReader);





            Stream stream = new MemoryStream();

            XmlWriter writer = new XmlTextWriter(stream, Encoding.UTF8);



            xslt.Transform(XmlReader, writer);



            stream.Position = 0;

            XmlDoc.Load(stream);

            return XmlDoc.InnerXml;

        }

 


    }
}