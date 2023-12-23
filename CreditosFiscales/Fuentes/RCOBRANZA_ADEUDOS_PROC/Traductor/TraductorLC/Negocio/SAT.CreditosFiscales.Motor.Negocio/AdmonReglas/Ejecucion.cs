
//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.Negocio:SAT.CreditosFiscales.Motor.Negocio.Ejecucion:1:12/07/2012[Assembly:1.0:12/07/2013])

using System;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.Xsl;




using SAT.CreditosFiscales.Motor.Entidades.Procesamiento;
using SAT.CreditosFiscales.Motor.Entidades.AdmonRegla;
using SAT.CreditosFiscales.Motor.AccesoDatos.AdmonReglas;
using SAT.CreditosFiscales.Motor.Negocio.ReglasNet;
using SAT.CreditosFiscales.Motor.Negocio.Mensajes;
using SAT.CreditosFiscales.Motor.Utilidades;

namespace SAT.CreditosFiscales.Motor.Negocio.AdmonReglas
{
    public class Ejecucion 
    {
        /// <summary>
        /// Aplica las reglas al documento de entrada.
        /// </summary>
        /// <param name="documentoEntrada">Documento de entrada.</param>
        /// <param name="reglas">Reglas que se aplicarán al documento.</param>        
        /// <param name="resultadoValidacion">Resultado de las validaciones.</param>
        public void AplicaReglas(string documentoEntrada, ReglasPorDocumento reglas, Respuesta<ResultadoReglas> resultadoValidacion)
        {
            CatalogoReglas catalogoReglas = null;
            ReglaBaseNet reglaBase = null;
            XmlDocument documentoXml = new XmlDocument();

            if (reglas.ListaReglas.Where(rN => rN.EsNET == true).Count() > 0)
            {
                catalogoReglas = new CatalogoReglas(reglas.DirectorioReglasNet);
            }

            RespuestaGenerica respuestaNET = new RespuestaGenerica();

            reglas.ListaReglas.Select(
                r =>
                {
                    if (resultadoValidacion.ListaErrores.Count == 0)
                    {
                        resultadoValidacion.Entidad.AgregaRegla(r.IdRegla, r.Descripcion, r.EsValidacion);

                        if (r.EsNET)
                        {
                            reglaBase = catalogoReglas.ObtenerRegla(r.Xslt);

                            documentoXml.LoadXml(resultadoValidacion.Entidad.Mensaje);
                            respuestaNET = reglaBase.EjecutaRegla(documentoXml);
                            resultadoValidacion.Entidad.Reglas[resultadoValidacion.Entidad.Reglas.Count - 1].FueExitosa = respuestaNET.EsExitoso;

                            if (!respuestaNET.EsExitoso)
                            {
                                resultadoValidacion.ListaErrores.AddRange(respuestaNET.ListaErrores);
                            }
                        }
                        else
                        {
                            ejecutaXSLT(r, resultadoValidacion);
                        }
                    }

                    return r;
                }
                ).ToList();

            resultadoValidacion.EsExitoso = resultadoValidacion.ListaErrores.Count == 0;
        }

        /// <summary>
        /// Ejecuta la transformación.
        /// </summary>
        /// <param name="reglaActual">Regla actual.</param>
        /// <param name="resultado">Resultado.</param>
        private void ejecutaXSLT(Regla reglaActual, Respuesta<ResultadoReglas> resultado)
        {
            XsltSettings settings = new XsltSettings();
            settings.EnableScript = true;
            RespuestaXslt respuestaXslt = new RespuestaXslt();
            bool seTransformo = false;


            try
            {
                respuestaXslt.Mensaje = TransformaXml(resultado.Entidad.Mensaje, reglaActual.Xslt);

                if (respuestaXslt.Mensaje != string.Empty)
                {
                    seTransformo = true;
                    resultado.Entidad.Reglas[resultado.Entidad.Reglas.Count - 1].FueExitosa = true;
                }
                else
                {
                    seTransformo = false;
                    resultado.Entidad.Reglas[resultado.Entidad.Reglas.Count - 1].FueExitosa = false;
                    respuestaXslt.AgregaError("Transformación nula");
                }

                if (!reglaActual.EsValidacion)
                {
                    resultado.Entidad.Mensaje = respuestaXslt.Mensaje;
                }
            }
            catch (Exception ex)
            {
                resultado.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.InicializaXSLT), ex, reglaActual.IdRegla);
            }

            if (!seTransformo)
            {
                resultado.Entidad.Reglas[resultado.Entidad.Reglas.Count - 1].Errores = respuestaXslt.ListaErrores;
                resultado.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.EjecutaTransformacion), reglaActual.IdRegla, respuestaXslt.ErroresEncontrados);
            }

        }


        /// <summary>
        /// Transforma un documento xml en base a un xslt
        /// </summary>
        /// <param name="xml">Documento a transformar o validar</param>
        /// <param name="xslt">Documento xslt a aplicar</param>
        /// <returns>Mensaje transformado</returns>
        public static string TransformaXml(string xml, string xslt)
        {
            // Create required readers for working with xml and xslt
            using (StringReader xsltInput = new StringReader(xslt))
            {
                StringReader xmlInput = new StringReader(xml);
                XmlTextReader xsltReader = new XmlTextReader(xsltInput);
                XmlTextReader xmlReader = new XmlTextReader(xmlInput);

                // Create required writer for output
                StringWriter stringWriter = new StringWriter();
                XmlTextWriter transformedXml = new XmlTextWriter(stringWriter);

                //Configura la transformación
                XsltSettings settings = new XsltSettings();
                settings.EnableScript = true;

                // Create a XslCompiledTransform to perform transformation
                XslCompiledTransform xsltTransform = new XslCompiledTransform();


                try
                {
                    xsltTransform.Load(xsltReader, settings, null);
                    xsltTransform.Transform(xmlReader, transformedXml);
                }
                catch
                {
                    throw;
                }

                return stringWriter.ToString();
            }
        }

        /// <summary>
        /// Lee el resultado de la transformación y lo asigna al objeto RespuestaXslt.
        /// </summary>
        /// <param name="tranformacion">Resultado de la transformación.</param>
        /// <returns>Respuesta de la transformación.</returns>
        private RespuestaXslt leeResultadoTransformacion(string tranformacion)
        {
            using (XmlReader reader = XmlReader.Create(new StringReader(tranformacion)))
            {
                RespuestaXslt respuesta = new RespuestaXslt();

                reader.MoveToContent();

                while (!reader.EOF)
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        switch (reader.LocalName)
                        {
                            case "Documento":
                                respuesta.Mensaje = reader.ReadInnerXml();
                                break;
                            case "EsExitoso":
                                respuesta.EsExitoso = bool.Parse(reader.ReadInnerXml());
                                break;
                            case "Error":
                                while (reader.Read() && !reader.LocalName.Equals("Descripcion")) ;
                                respuesta.AgregaError(reader.ReadInnerXml());
                                break;
                            default:
                                reader.Read();
                                break;
                        }
                    }
                    else
                    {
                        reader.Read();
                    }
                }


                return respuesta;
            }
        }




        /// <summary>
        /// Obtiene todas las reglas relacionadas a un tipo de documento de pago y una aplicación.
        /// </summary>
        /// <param name="idAplicacion">Id de la aplicación.</param>
        /// <param name="idTipoDocPago">Id de tipo de documento de pago.</param>
        /// <returns>Respuesta con las reglas encontradas.</returns>
        public Respuesta<ReglasPorDocumento> ConsultaReglas(short? idAplicacion, short? idTipoDocPago, bool AntesInsercion,bool EsSIAT)
        {
            Respuesta<ReglasPorDocumento> respuesta = new Respuesta<ReglasPorDocumento>();

            try
            {
                    respuesta.Entidad = DalReglas.ConsultaRecursosValidacion(idAplicacion.Value, idTipoDocPago.Value, AntesInsercion, EsSIAT);

                if (respuesta.Entidad.ListaContratos == null || respuesta.Entidad.ListaContratos.Count == 0)
                {
                    respuesta.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.SinContratos));
                }

                if (respuesta.Entidad.ListaReglas == null || respuesta.Entidad.ListaReglas.Count == 0)
                {
                    respuesta.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.SinReglas));
                }
            }
            catch (Exception ex)
            {
                respuesta.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.ObtenerConfiguracion), ex);
            }

            respuesta.EsExitoso = respuesta.ListaErrores.Count == 0;

            return respuesta;
        }

        /// <summary>
        /// Inserta una regla relacionada a una aplicación y a un tipo de documento de pago.
        /// </summary>
        /// <param name="regla">Regla que se desea insertar.</param>
        public Guid InsertaRegla(Regla regla)
        {
            return DalReglas.InsertaRegla(regla.Descripcion, regla.Xslt, regla.EsValidacion);
        }
      
    }
}
