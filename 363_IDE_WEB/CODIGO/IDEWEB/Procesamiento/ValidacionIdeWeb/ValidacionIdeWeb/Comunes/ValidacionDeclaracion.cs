using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;

using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using System.Xml.Linq;
using System.Configuration;

namespace ValidacionIdeWeb
{
    public class ValidacionDeclaracion
    {
        private Declaracion declaracion;
        private ResultadoValidacion erroresContendio;

        public Declaracion Declaracion
        {
            get { return declaracion; }
            set { declaracion = value; }
        }

        //CONSTRUCTOR
        public ValidacionDeclaracion(Declaracion declaracion)
        {
            this.declaracion = declaracion;
        }

        //VALIDACION NOMBRE ARCHIVO
        public ResultadoValidacion validarNombreArchivo()
        {
            ResultadoValidacion resultado = new ResultadoValidacion();
            resultado.EsCorreca = true;
            resultado.ListaErrores = new List<string>();

            string error = "";
            if (this.declaracion != null)
            {

                if (this.declaracion.NombreArchivo == null)
                {
                    error = AccesoDatos.ObtenerMensaje(AccesoDatos.getValorInt(Constantes.Validar.idTipoMensaje), Constantes.MensajeError.NomArchNull, declaracion.Folio);
                    resultado.ListaErrores.Add(error);
                    resultado.EsCorreca = false;
                }
                else
                {
                    if (!validarLongitudNomenclatura())
                    {
                        error = AccesoDatos.ObtenerMensaje(AccesoDatos.getValorInt(Constantes.Validar.idTipoMensaje), Constantes.MensajeError.NomArchLongitudNoValida, declaracion.Folio);
                        resultado.ListaErrores.Add(error);
                        resultado.EsCorreca = false;
                    }
                    if (!validarMensualAnual())
                    {
                        error = AccesoDatos.ObtenerMensaje(AccesoDatos.getValorInt(Constantes.Validar.idTipoMensaje), Constantes.MensajeError.NomArchMensualAnualNoValida, declaracion.Folio);
                        resultado.ListaErrores.Add(error);
                        resultado.EsCorreca = false;
                    }
                    if (!validarRfcNombreArchivo())
                    {
                        error = AccesoDatos.ObtenerMensaje(AccesoDatos.getValorInt(Constantes.Validar.idTipoMensaje), Constantes.MensajeError.NomArchRfcNoValido, declaracion.Folio);
                        resultado.ListaErrores.Add(error);
                        resultado.EsCorreca = false;
                    }
                    if (!validarPeriodo())
                    {
                        error = AccesoDatos.ObtenerMensaje(AccesoDatos.getValorInt(Constantes.Validar.idTipoMensaje), Constantes.MensajeError.NomArchPeriodoNoValido, declaracion.Folio);
                        resultado.ListaErrores.Add(error);
                        resultado.EsCorreca = false;
                    }
                    if (!validarEjercicio())
                    {
                        error = AccesoDatos.ObtenerMensaje(AccesoDatos.getValorInt(Constantes.Validar.idTipoMensaje), Constantes.MensajeError.NomArchEjercicioNoValido, declaracion.Folio);
                        resultado.ListaErrores.Add(error);
                        resultado.EsCorreca = false;
                    }
                    if (!validarTipoDeclaracion())
                    {
                        error = AccesoDatos.ObtenerMensaje(AccesoDatos.getValorInt(Constantes.Validar.idTipoMensaje), Constantes.MensajeError.NomArchTipoDeclaracionNoValido, declaracion.Folio);
                        resultado.ListaErrores.Add(error);
                        resultado.EsCorreca = false;
                    }
                    if (!validarNumeroComplementaria())
                    {
                        error = AccesoDatos.ObtenerMensaje(AccesoDatos.getValorInt(Constantes.Validar.idTipoMensaje), Constantes.MensajeError.NomArchNumeroComplementariaNoValido, declaracion.Folio);
                        resultado.ListaErrores.Add(error);
                        resultado.EsCorreca = false;
                    }
                    if (!validarFormato())
                    {
                        resultado.ListaErrores.Add(Constantes.MensajeError.NomArchFormatoNoValido);
                        resultado.EsCorreca = false;
                    }
                }
            }
            else
            {
                resultado.ListaErrores.Add("0");
                resultado.EsCorreca = false;
            }
            resultado.ValidacionConcluida = true;
            return resultado;
        }

        private bool validarLongitudNomenclatura()
        {
            if (this.declaracion.NombreArchivo != null)
            {
                if (Path.GetFileNameWithoutExtension(this.declaracion.NombreArchivo) != null)
                {
                    if (Path.GetFileNameWithoutExtension(this.declaracion.NombreArchivo).Length == AccesoDatos.getValorInt(Constantes.Validar.longitudCharsNombre))
                        return true;

                }
            }
            return false;
        }

        private bool validarMensualAnual()
        {
            if (this.declaracion.NombreArchivo != null && this.declaracion.NombreArchivo.Length >= 3)
            {
                string declaracionIDE = this.declaracion.NombreArchivo.Substring(0, 3);
                if (declaracionIDE.Equals(AccesoDatos.getValorString(Constantes.ExpresionRegular.DeclaracionMensual)) || declaracionIDE.Equals(AccesoDatos.getValorString(Constantes.ExpresionRegular.DeclaracionAnual)))
                    return true;
            }
            return false;
        }

        private bool validarRfcNombreArchivo()
        {
            if (this.declaracion.NombreArchivo != null && this.declaracion.NombreArchivo.Length >= 15)
            {
                string rfc = this.declaracion.NombreArchivo.Substring(3, 12);
                Regex expresionRegular = new Regex(AccesoDatos.getValorString(Constantes.ExpresionRegular.Rfc));
                if (expresionRegular.IsMatch(rfc))
                {
                    string cadFecha = rfc.Substring(3, 6);
                    try
                    {
                        DateTime fecha;
                        fecha = DateTime.ParseExact(cadFecha, AccesoDatos.getValorString(Constantes.ExpresionRegular.FechaRfc), null, DateTimeStyles.None);
                        return true;
                    }
                    catch (Exception e)
                    {
                        AccesoDatos.RegistrarEvento(this.declaracion.Folio, e.Message);
                        return false;
                    }
                }
            }
            return false;
        }

        private bool validarPeriodo()
        {
            if (this.declaracion.NombreArchivo != null && this.declaracion.NombreArchivo.Length >= 21)
            {
                string mensualAnual = this.declaracion.NombreArchivo.Substring(0, 3);
                string periodo = this.declaracion.NombreArchivo.Substring(19, 2);
                Regex expresionRegular = new Regex(AccesoDatos.getValorString(Constantes.ExpresionRegular.Periodo));
                if (expresionRegular.IsMatch(periodo))
                {
                    if (mensualAnual.Equals(AccesoDatos.getValorString(Constantes.ExpresionRegular.DeclaracionAnual)) && periodo.Equals(AccesoDatos.getValorString(Constantes.ExpresionRegular.SinPeriodo)))
                        return true;

                    if (mensualAnual.Equals(AccesoDatos.getValorString(Constantes.ExpresionRegular.DeclaracionMensual)))
                    {
                        int intPeriodo = Convert.ToInt32(periodo);
                        if (intPeriodo > 0 && intPeriodo < 13)
                            return true;

                    }
                }
            }
            return false;
        }

        private bool validarEjercicio()
        {
            if (this.declaracion.NombreArchivo != null && this.declaracion.NombreArchivo.Length >= 19)
            {
                string ejercicio = this.declaracion.NombreArchivo.Substring(15, 4);
                Regex expresionRegular = new Regex(AccesoDatos.getValorString(Constantes.ExpresionRegular.Ejercicio));
                if (expresionRegular.IsMatch(ejercicio))
                {
                    int intEjercicio = Convert.ToInt32(ejercicio);
                    DateTime fecha = new DateTime();
                    fecha = DateTime.Now;
                    int ejercicioEnCurso = fecha.Year;

                    if (intEjercicio < (ejercicioEnCurso + 1) && intEjercicio > (AccesoDatos.getValorInt(Constantes.Validar.ejercicioMinimo) - 1))
                        return true;
                }
            }
            return false;
        }

        private bool validarTipoDeclaracion()
        {
            if (this.declaracion.NombreArchivo != null && this.declaracion.NombreArchivo.Length >= 22)
            {
                string tipoDeclaracion = this.declaracion.NombreArchivo.Substring(21, 1);
                Regex expresionRegular = new Regex(AccesoDatos.getValorString(Constantes.ExpresionRegular.TipoDeclaracion));
                if (expresionRegular.IsMatch(tipoDeclaracion))
                    return true;
            }
            return false;
        }

        private bool validarNumeroComplementaria()
        {
            if (this.declaracion.NombreArchivo != null && this.declaracion.NombreArchivo.Length >= 24)
            {
                string tipoDeclaracion = this.declaracion.NombreArchivo.Substring(21, 1);
                string numeroComplementaria = this.declaracion.NombreArchivo.Substring(22, 2);
                Regex expresionRegular = new Regex(AccesoDatos.getValorString(Constantes.ExpresionRegular.NumeroComplementaria));
                if (expresionRegular.IsMatch(numeroComplementaria))
                {
                    if (tipoDeclaracion.Equals(AccesoDatos.getValorString(Constantes.ExpresionRegular.DeclaracionNormal)) && !numeroComplementaria.Equals(AccesoDatos.getValorString(Constantes.ExpresionRegular.SinPeriodo)))
                        return false;

                    if (tipoDeclaracion.Equals(AccesoDatos.getValorString(Constantes.ExpresionRegular.DeclaracionComplementaria)) && numeroComplementaria.Equals(AccesoDatos.getValorString(Constantes.ExpresionRegular.SinPeriodo)))
                        return false;

                    return true;
                }

            }
            return false;
        }

        private bool validarFormato()
        {
            if (this.declaracion.NombreArchivo != null)
            {
                if (Path.GetExtension(this.declaracion.NombreArchivo) != null)
                {
                    if (Path.GetExtension(this.declaracion.NombreArchivo).Equals(AccesoDatos.getValorString(Constantes.Validar.formatoXML))
                        || Path.GetExtension(this.declaracion.NombreArchivo).Equals(AccesoDatos.getValorString(Constantes.Validar.formatoZIP)))
                        return true;
                }
            }
            return false;
        }

        //VALIDACION CONTENIDO ARCHIVO
        public ResultadoValidacion validarContenidoArchivo()
        {
            string pathArchivoFisico = Path.Combine(AccesoDatos.getValorString(Constantes.Validar.fileshare), this.declaracion.ArchivoFisico);
            this.erroresContendio = new ResultadoValidacion();
            this.erroresContendio.EsCorreca = true;
            this.erroresContendio.ListaErrores = new List<string>();
            this.erroresContendio.SinErroresEsquema = true;
            this.erroresContendio.ListaErroresEsquema = new List<string>();
            string error = "";

            if (pathArchivoFisico == null)
            {
                error = AccesoDatos.ObtenerMensaje(AccesoDatos.getValorInt(Constantes.Validar.idTipoMensaje), Constantes.MensajeError.ArchNull, declaracion.Folio);
                this.erroresContendio.ListaErrores.Add(error);
                this.erroresContendio.EsCorreca = false;
            }
            else
            {
                if (!File.Exists(pathArchivoFisico))
                {
                    error = AccesoDatos.ObtenerMensaje(AccesoDatos.getValorInt(Constantes.Validar.idTipoMensaje), Constantes.MensajeError.ArchNoExite, declaracion.Folio);
                    this.erroresContendio.ListaErrores.Add(error);
                    this.erroresContendio.EsCorreca = false;
                }
                else
                {
                    if (!validarTamanio(pathArchivoFisico, declaracion.IdMedioRecepcion))
                    {
                        error = AccesoDatos.ObtenerMensaje(AccesoDatos.getValorInt(Constantes.Validar.idTipoMensaje), Constantes.MensajeError.ArchTamanioNoValido, declaracion.Folio);
                        this.erroresContendio.ListaErrores.Add(error);
                        this.erroresContendio.EsCorreca = false;
                    }

                    if (Path.GetExtension(pathArchivoFisico).Equals(AccesoDatos.getValorString(Constantes.Validar.formatoZIP)))
                    {
                        string pathArchivoUnZip = this.unZip(pathArchivoFisico, AccesoDatos.getValorString(Constantes.Validar.directorioArchivosUnZip));

                        pathArchivoFisico = pathArchivoUnZip;
                    }


                    if (!ValidarVersionXML(pathArchivoFisico))
                    {
                        error = AccesoDatos.ObtenerMensaje(AccesoDatos.getValorInt(Constantes.Validar.idTipoMensaje), Constantes.MensajeError.ValidarAnioVersion, declaracion.Folio);
                        this.erroresContendio.ListaErrores.Add(error);
                        this.erroresContendio.EsCorreca = false;
                    }

                    if (Path.GetExtension(pathArchivoFisico).Equals(AccesoDatos.getValorString(Constantes.Validar.formatoXML)))
                    {
                        if (!validarContenido(pathArchivoFisico))
                        {
                            this.erroresContendio.EsCorreca = false;
                        }
                    }
                    else
                    {
                        //DESCOMPRIMIR ARCHIVO
                        string pathArchivoUnZip = this.unZip(pathArchivoFisico, AccesoDatos.getValorString(Constantes.Validar.directorioArchivosUnZip));
                        if (pathArchivoUnZip != null && this.erroresContendio.SinErroresEsquema)
                        {
                            if (File.Exists(pathArchivoUnZip))
                            {
                                if (!validarContenido(pathArchivoUnZip))
                                {
                                    this.erroresContendio.EsCorreca = false;
                                }
                                File.Delete(pathArchivoUnZip);
                            }
                            else
                            {
                                this.erroresContendio.ValidacionConcluida = false;
                                AccesoDatos.RegistrarEvento(declaracion.Folio, "El archivo descomprimido: " + pathArchivoUnZip + ", no existe.");
                            }
                        }
                    }

                    if (this.declaracion.IdMedioRecepcion == AccesoDatos.getValorInt(Constantes.Validar.medioRecepcionInternet))
                    {
                        if (!this.declaracion.RfcAutenticacion.Equals(this.getRfcNombreArchivo(this.declaracion.NombreArchivo)))
                        {
                            error = AccesoDatos.ObtenerMensaje(AccesoDatos.getValorInt(Constantes.Validar.idTipoMensaje), Constantes.MensajeError.RfcAutenticadoNoCoincideNombreArchivo, declaracion.Folio);
                            this.erroresContendio.ListaErrores.Add(error);
                            this.erroresContendio.EsCorreca = false;
                        }
                    }
                }
            }
            return this.erroresContendio;
        }

        private bool validarTamanio(string pathArchivo, int medioRecepcion)
        {
            FileInfo archivo = new FileInfo(pathArchivo);
            if (medioRecepcion == AccesoDatos.getValorInt(Constantes.Validar.medioRecepcionModulo))
            {
                //ES POR MODULO
                if (Path.GetExtension(pathArchivo).Equals(AccesoDatos.getValorString(Constantes.Validar.formatoXML)))
                {
                    //ES XML
                    if (archivo.Length <= AccesoDatos.getValorLong(Constantes.Validar.sizeMaxArchivoModulo) && archivo.Length >= AccesoDatos.getValorLong(Constantes.Validar.sizeMinArchivoModulo))
                        return true;
                }
                else
                {
                    //ES ZIP
                    if (archivo.Length <= AccesoDatos.getValorLong(Constantes.Validar.sizeMaxArchivoModuloZIP) && archivo.Length >= AccesoDatos.getValorLong(Constantes.Validar.sizeMinArchivoModuloZIP))
                        return true;
                }
            }
            else
            {
                //ES POR INTERNET
                if (Path.GetExtension(pathArchivo).Equals(AccesoDatos.getValorString(Constantes.Validar.formatoXML)))
                {
                    //ES XML
                    if (archivo.Length <= AccesoDatos.getValorLong(Constantes.Validar.sizeMaxArchivoInternet) && archivo.Length >= AccesoDatos.getValorLong(Constantes.Validar.sizeMinArchivoInternet))
                        return true;
                }
                else
                {
                    //ES ZIP
                    if (archivo.Length <= AccesoDatos.getValorLong(Constantes.Validar.sizeMaxArchivoInternetZIP) && archivo.Length >= AccesoDatos.getValorLong(Constantes.Validar.sizeMinArchivoInternetZIP))
                        return true;
                }
            }

            return false;
        }

        private bool validarContenido(string pathArchivoFisico)
        {
            string error = "";
            this.erroresContendio.SinErroresEsquema = true;
            this.erroresContendio.ListaErroresEsquema = new List<string>();

            XmlReaderSettings configuracionLector = new XmlReaderSettings();
            configuracionLector.ProhibitDtd = false;
            configuracionLector.ValidationType = ValidationType.Schema;
            configuracionLector.ValidationFlags = System.Xml.Schema.XmlSchemaValidationFlags.ReportValidationWarnings;

            XmlSchemaSet configEsquemas = new XmlSchemaSet();

            if (File.Exists(Declaracion.CadenaOriginal.ArchivoXSD))
            {
                configEsquemas.Add("", Declaracion.CadenaOriginal.ArchivoXSD);

                configuracionLector.Schemas = configEsquemas;
                configuracionLector.ValidationEventHandler += new ValidationEventHandler(validationHandler);

                XmlReader lectorXML = XmlReader.Create(pathArchivoFisico, configuracionLector);

                DatosArchivo datosNombre = getDatosDeNombreArchivo(this.declaracion.NombreArchivo);

                string nodoPrincipal = Declaracion.CadenaOriginal.NodoPrincipal;
                string nodoTipoDeclaracion = Declaracion.CadenaOriginal.NodoTipoDeclaracion;
                try
                {
                    if (lectorXML.ReadToFollowing(nodoPrincipal))
                    {
                        this.declaracion.CadenaOriginal.RfcDeclarante = lectorXML.GetAttribute(Constantes.AtributoXml.RfcDeclarante);
                        this.declaracion.CadenaOriginal.Denominacion = lectorXML.GetAttribute(Constantes.AtributoXml.Denominacion);

                        if (lectorXML.ReadToFollowing(nodoTipoDeclaracion))
                        {
                            if (!datosNombre.EsAnual)
                                this.declaracion.CadenaOriginal.Periodo = this.getValorAtributoXMLNumericoInt(lectorXML.GetAttribute(Constantes.AtributoXml.Periodo));
                            else
                                this.declaracion.CadenaOriginal.Periodo = 0;

                            this.erroresContendio.SinErroresEsquema = this.validarDatos(datosNombre);

                            if (this.erroresContendio.SinErroresEsquema)
                            {
                                if (lectorXML.ReadToFollowing(Constantes.ElementoXml.Totales))
                                {
                                    this.declaracion.CadenaOriginal.ImporteCheques = this.getValorAtributoXMLNumericoLong(lectorXML.GetAttribute(Constantes.AtributoXml.ImporteCheques));
                                    this.declaracion.CadenaOriginal.ImporteExcedenteDepositos = this.getValorAtributoXMLNumericoLong(lectorXML.GetAttribute(Constantes.AtributoXml.ImporteExcedenteDepositos));
                                    this.declaracion.CadenaOriginal.ImporteDeterminadoDepositos = this.getValorAtributoXMLNumericoLong(lectorXML.GetAttribute(Constantes.AtributoXml.ImporteDeterminadoDepositos));
                                    this.declaracion.CadenaOriginal.ImporteRecaudadoDepositos = this.getValorAtributoXMLNumericoLong(lectorXML.GetAttribute(Constantes.AtributoXml.ImporteRecaudadoDepositos));
                                    this.declaracion.CadenaOriginal.ImportePendienteDepositos = this.getValorAtributoXMLNumericoLong(lectorXML.GetAttribute(Constantes.AtributoXml.ImportePendienteDepositos));
                                    this.declaracion.CadenaOriginal.ImporteRemanenteDepositos = this.getValorAtributoXMLNumericoLong(lectorXML.GetAttribute(Constantes.AtributoXml.ImporteRemanenteDepositos));
                                    this.declaracion.CadenaOriginal.ImporteEnterado = this.getValorAtributoXMLNumericoLong(lectorXML.GetAttribute(Constantes.AtributoXml.ImporteEnterado));
                                    this.declaracion.CadenaOriginal.OperacionesRelacionadas = this.getValorAtributoXMLNumericoLong(lectorXML.GetAttribute(Constantes.AtributoXml.OperacionesRelacionadas));
                                    if (this.declaracion.NombreArchivo != null && this.declaracion.NombreArchivo.Length >= 3)
                                    {
                                        //string declaracionIDE = this.declaracion.NombreArchivo.Substring(0, 3);
                                        //if (declaracionIDE.Equals(AccesoDatos.getValorString(Constantes.ExpresionRegular.DeclaracionMensual)))
                                        ValidarImportes(Path.Combine(AccesoDatos.getValorString(Constantes.Validar.fileshare), this.declaracion.ArchivoFisico), this.declaracion.CadenaOriginal.ImporteExcedenteDepositos, this.declaracion.CadenaOriginal.ImporteDeterminadoDepositos);
                                    }
                                }
                            }
                        }
                        else
                        {
                            error = AccesoDatos.ObtenerMensaje(AccesoDatos.getValorInt(Constantes.Validar.idTipoMensaje), Constantes.MensajeError.ArchTipoDeclaracionNoCorrespondeAlContenido, declaracion.Folio);
                            this.erroresContendio.ListaErrores.Add(error);
                            this.erroresContendio.SinErroresEsquema = false;
                        }
                    }
                    else
                    {
                        error = AccesoDatos.ObtenerMensaje(AccesoDatos.getValorInt(Constantes.Validar.idTipoMensaje), Constantes.MensajeError.ArchDeclaracionNoCorrespondeAlContenido, declaracion.Folio);
                        this.erroresContendio.ListaErrores.Add(error);
                        this.erroresContendio.SinErroresEsquema = false;
                    }
                    while (lectorXML.Read()) { }

                }
                catch (XmlException xmlEx)
                {
                    this.erroresContendio.ListaErroresEsquema.Add("Error en la estructura XML: " + TraductorXML(xmlEx.Message));
                    this.erroresContendio.SinErroresEsquema = false;
                }
                finally
                {
                    this.erroresContendio.ValidacionConcluida = true;
                    lectorXML.Close();
                }

                if (!this.erroresContendio.SinErroresEsquema)
                    this.declaracion.CadenaOriginal = null;
            }
            else
            {
                //NO EXISTE EL ARCHIVO XSD PARA VALIDAR
                this.erroresContendio.ValidacionConcluida = false;
                AccesoDatos.RegistrarEvento(this.declaracion.Folio, "El archivo de validación: " + AccesoDatos.getValorString(Constantes.Validar.pathXsdValidar) + ", no existe en el directorio: " + Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, Constantes.Validar.directorioEsquemas));
            }
            return this.erroresContendio.SinErroresEsquema;
        }

        private void validationHandler(object sender, ValidationEventArgs args)
        {
            this.erroresContendio.SinErroresEsquema = false;
            if (args.Exception.GetType().Name == "XmlSchemaValidationException")
            {
                XmlSchemaValidationException excepcionEsquema = (XmlSchemaValidationException)args.Exception;
                this.erroresContendio.ListaErroresEsquema.Add("Línea: " + args.Exception.LineNumber + ". " + Traductor(args.Exception.Message));
            }
        }

        private string Traductor(string vCadena)
        {
            vCadena.Replace("The value of the","El valor del atributo");
            vCadena=vCadena.Replace("attribute does not equal its fixed value","no es igual a su valor fijo");

            vCadena=vCadena.Replace("The required attribute","El Atributo");
            vCadena=vCadena.Replace("is missing","es requerido");
            
             vCadena=vCadena.Replace("The element cannot contain white space. Content model is empty","El elemento no puede contener espacio en blanco. El modelo de contenido está vacío");
			 
            if(vCadena.IndexOf("attribute")>0)
               vCadena.Replace("The '","El atributo '");
            else
               vCadena.Replace("The '","El elemento '");

			 vCadena.Replace("element is not declared","no esta declarado");
			 
			 vCadena.Replace("The element","El elemento");
			 vCadena.Replace("cannot contain child element","no puede contener el elemento hijo");
			 vCadena.Replace("because the parent element's content model is","porque el modelo de contenido del elemento primario está");
			 vCadena.Replace("empty","vacío");
			 vCadena.Replace("text only","solo texto");

			 vCadena.Replace("is invalid - The value","es invalido - El valor");
			 vCadena.Replace("is invalid according to its datatype","es invalido de acuerdo al tipo de dato");
			 vCadena.Replace("The Pattern constraint failed","El patrón de restricción falló");

			 vCadena.Replace("attribute is not declared","no está declarado");

//The element 'PersonaFisica' has invalid child element 'DepositoEnEfectivo'. List of possible elements expected: 'numeroCuenta'.			 
			 
			 vCadena.Replace("has invalid child element","tiene un elemento hijo invalido");
             vCadena.Replace("List of possible elements expected", "Lista de los posibles elementos esperados");
			 
			 return vCadena;

        }


        private string TraductorXML(string vCadena)
        {
        //Error en la estructura XML: Name cannot begin with the '=' character, hexadecimal value 0x3D. Line 164, position 12.

        vCadena.Replace("Name cannot begin with the","El nombre no puede comenzar con el caracter");
        vCadena.Replace(" character","");
        vCadena.Replace("hexadecimal value","valor hexadecimal");
			 
        //Error en la estructura XML: The 'tDeclaracionAnual' start tag on line 10 does not match the end tag of 'tDeclaracionMensual'. Line 10, position 74. OK


        vCadena.Replace("The '","La etiquete inicial '");
        vCadena.Replace("start tag on line","en la línea");
        vCadena.Replace("does not match the end tag of","no coincide con la etiqueta final");
        vCadena.Replace("Line","Línea");
        vCadena.Replace("position","posición");

        //Error en la estructura XML: Name cannot begin with the '=' character, hexadecimal value 0x3D. Line 164, position 12.

        vCadena.Replace("Name cannot begin with the","El nombre no puede comenzar con el caracter");
        vCadena.Replace(" character","");
        vCadena.Replace("hexadecimal value", "valor hexadecimal");

        return vCadena;
        }

        private DatosArchivo getDatosDeNombreArchivo(string nombreArchivo)
        {
            DatosArchivo datos = new DatosArchivo();
            string mensualAnual = nombreArchivo.Substring(0, 3);
            if (mensualAnual.Equals(AccesoDatos.getValorString(Constantes.ExpresionRegular.DeclaracionAnual)))
                datos.EsAnual = true;
            if (mensualAnual.Equals(AccesoDatos.getValorString(Constantes.ExpresionRegular.DeclaracionMensual)))
                datos.EsAnual = false;
            datos.Rfc = nombreArchivo.Substring(3, 12);
            datos.Ejercicio = Convert.ToInt32(nombreArchivo.Substring(15, 4));
            datos.Periodo = Convert.ToInt32(nombreArchivo.Substring(19, 2));
            string tipoDeclaracion = nombreArchivo.Substring(21, 1);
            if (tipoDeclaracion.Equals(AccesoDatos.getValorString(Constantes.ExpresionRegular.DeclaracionNormal)))
                datos.EsNormal = true;
            if (tipoDeclaracion.Equals(AccesoDatos.getValorString(Constantes.ExpresionRegular.DeclaracionComplementaria)))
                datos.EsNormal = false;

            return datos;
        }

        private bool validarDatos(DatosArchivo datosNombre)
        {
            bool respuesta = true;
            string error = "";

            if (!datosNombre.Rfc.Equals(this.declaracion.CadenaOriginal.RfcDeclarante))
            {
                error = AccesoDatos.ObtenerMensaje(AccesoDatos.getValorInt(Constantes.Validar.idTipoMensaje), Constantes.MensajeError.ArchRfcNoValido, declaracion.Folio);
                this.erroresContendio.ListaErrores.Add(error);
                respuesta = false;
            }
            if (datosNombre.Ejercicio != this.declaracion.CadenaOriginal.Ejercicio)
            {
                error = AccesoDatos.ObtenerMensaje(AccesoDatos.getValorInt(Constantes.Validar.idTipoMensaje), Constantes.MensajeError.ArchEjercicioNoValido, declaracion.Folio);
                this.erroresContendio.ListaErrores.Add(error);
                respuesta = false;
            }

            if (datosNombre.Periodo != this.declaracion.CadenaOriginal.Periodo)
            {
                error = AccesoDatos.ObtenerMensaje(AccesoDatos.getValorInt(Constantes.Validar.idTipoMensaje), Constantes.MensajeError.ArchPeriodoNoValido, declaracion.Folio);
                this.erroresContendio.ListaErrores.Add(error);
                respuesta = false;
            }
            return respuesta;
        }

        private string getRfcNombreArchivo(string nombreArchivo)
        {
            if (nombreArchivo != null && nombreArchivo.Length >= 15)
            {
                return nombreArchivo.Substring(3, 12);
            }
            return null;
        }

        private int getValorAtributoXMLNumericoInt(string atributo)
        {
            int valor = 0;

            try
            {
                valor = Convert.ToInt32(atributo);
            }
            catch
            {
                valor = 0;
            }

            return valor;
        }

        private long getValorAtributoXMLNumericoLong(string atributo)
        {
            long valor = 0;

            try
            {
                valor = Convert.ToInt64(atributo);
            }
            catch
            {
                valor = 0;
            }

            return valor;
        }

        //UNZIP ARCHIVOS .zip
        private string unZip(string rutaArchivoZip, string rutaDirectorioUnZip)
        {
            string rutaArchivoUnzip = null;

            if (rutaArchivoZip != null && rutaDirectorioUnZip != null)
            {
                if (File.Exists(rutaArchivoZip) && Path.GetExtension(rutaArchivoZip).Equals(AccesoDatos.getValorString(Constantes.Validar.formatoZIP)))
                {
                    FileInfo info = new FileInfo(rutaArchivoZip);
                    if (info.Length > 0)
                    {
                        if (!Directory.Exists(rutaDirectorioUnZip))
                            Directory.CreateDirectory(rutaDirectorioUnZip);

                        string archivoDescomprimido = null;

                        try
                        {
                            using (ZipInputStream stream = new ZipInputStream(File.OpenRead(rutaArchivoZip)))
                            {
                                ZipEntry entradaZip = null;
                                try
                                {
                                    entradaZip = stream.GetNextEntry();
                                }
                                catch (Exception e)
                                {
                                    AccesoDatos.RegistrarEvento(this.declaracion.Folio, Constantes.Excepcion.errorProcesoDescompresion + " " + e.Message);
                                }

                                if (!stream.CanDecompressEntry)
                                {
                                    this.erroresContendio.SinErroresEsquema = false;
                                    this.erroresContendio.ListaErroresEsquema.Add(Constantes.Excepcion.archivoZipNoValido);
                                    AccesoDatos.RegistrarEvento(this.declaracion.Folio, Constantes.Excepcion.archivoZipNoValido);
                                }

                                bool esXml = false;
                                while (!esXml && entradaZip != null && !Path.GetExtension(entradaZip.Name).Equals(AccesoDatos.getValorString(Constantes.Validar.formatoXML)))
                                {
                                    entradaZip = stream.GetNextEntry();
                                    if (entradaZip != null && Path.GetExtension(entradaZip.Name).Equals(AccesoDatos.getValorString(Constantes.Validar.formatoXML)))
                                        esXml = true;
                                }

                                if (entradaZip == null)
                                {
                                    this.erroresContendio.SinErroresEsquema = false;
                                    this.erroresContendio.ListaErroresEsquema.Add(Constantes.Excepcion.contenidoArchivoZipNoProcesable);
                                    AccesoDatos.RegistrarEvento(this.declaracion.Folio, Constantes.Excepcion.contenidoArchivoZipNoProcesable);
                                }
                                else
                                {
                                    if (Path.GetExtension(entradaZip.Name).Equals(AccesoDatos.getValorString(Constantes.Validar.formatoXML)))
                                    {
                                        archivoDescomprimido = entradaZip.Name + ".descomprimido";
                                        archivoDescomprimido = Path.GetFileName(entradaZip.Name);
                                        archivoDescomprimido = Path.Combine(rutaDirectorioUnZip, archivoDescomprimido);

                                        if (File.Exists(archivoDescomprimido))
                                        {
                                            File.Delete(archivoDescomprimido);

                                        }

                                        using (FileStream streamWriter = File.Create(archivoDescomprimido))
                                        {
                                            int size = 10240;
                                            byte[] datos = new byte[10240];

                                            while (true)
                                            {
                                                size = stream.Read(datos, 0, datos.Length);
                                                if (size > 0)
                                                {
                                                    streamWriter.Write(datos, 0, size);
                                                }
                                                else
                                                {
                                                    break;
                                                }
                                            }
                                        }

                                        if (File.Exists(archivoDescomprimido))
                                            rutaArchivoUnzip = archivoDescomprimido;

                                    }
                                    else
                                    {
                                        this.erroresContendio.SinErroresEsquema = false;
                                        this.erroresContendio.ListaErroresEsquema.Add(Constantes.Excepcion.contenidoArchivoZipNoProcesable);
                                        AccesoDatos.RegistrarEvento(this.declaracion.Folio, Constantes.Excepcion.contenidoArchivoZipNoProcesable);
                                        Directory.Delete(rutaDirectorioUnZip);
                                    }
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            AccesoDatos.RegistrarEvento(this.declaracion.Folio, Constantes.Excepcion.errorProcesoDescompresion + " " + e.Message);
                        }
                    }
                }
            }
            return rutaArchivoUnzip;
        }

        /// <summary>
        /// Método que valida los importes montoExcedente e impuestoDeterminado de los detalles con respecto a los totales  
        /// </summary>
        /// <returns>No devuelve ningun valor 
        /// </returns>
        private void ValidarImportes(string sArchivo, long importeExcedenteDepositos, long importeDeterminadoDepositos)
        {
            XDocument doc = null;
            int nEjercicio = 0, nPeriodo = 0;
            List<long> lstMontExce = null, lstImpuDete = null;

            int EI_ImpoExceDepo = 0, PI_ImpoExceDepo = 0;
            int EF_ImpoExceDepo = 0, PF_ImpoExceDepo = 0;
            int EI_ImpoDeteDepo = 0, PI_ImpoDeteDepo = 0;
            int EF_ImpoDeteDepo = 0, PF_ImpoDeteDepo = 0;

            bool bolImpoExceDepo = false, bolImpoDeteDepo = false;
            try
            {
                nEjercicio = Declaracion.CadenaOriginal.Ejercicio;
                nPeriodo = Declaracion.CadenaOriginal.Periodo;
                EI_ImpoExceDepo = int.Parse(ConfigurationManager.AppSettings["EjercicioInicioImporteExcedenteDepositos"]);
                PI_ImpoExceDepo = int.Parse(ConfigurationManager.AppSettings["PeriodoInicioImporteExcedenteDepositos"]);
                EF_ImpoExceDepo = int.Parse(ConfigurationManager.AppSettings["EjercicioFinImporteExcedenteDepositos"]);
                PF_ImpoExceDepo = int.Parse(ConfigurationManager.AppSettings["PeriodoFinImporteExcedenteDepositos"]);
                EI_ImpoDeteDepo = int.Parse(ConfigurationManager.AppSettings["EjercicioInicioImporteDeterminadoDepositos"]);
                PI_ImpoDeteDepo = int.Parse(ConfigurationManager.AppSettings["PeriodoInicioImporteDeterminadoDepositos"]);
                EF_ImpoDeteDepo = int.Parse(ConfigurationManager.AppSettings["EjercicioFinImporteDeterminadoDepositos"]);
                PF_ImpoDeteDepo = int.Parse(ConfigurationManager.AppSettings["PeriodoFinImporteDeterminadoDepositos"]);

                doc = new XDocument(XDocument.Load(sArchivo));
                lstMontExce = new List<long>();
                lstImpuDete = new List<long>();

                if (Declaracion.CadenaOriginal.VersionSistema == "3.0")
                {
                    if (importeExcedenteDepositos > 0)
                    {
                        foreach (var registroDetalle in doc.Descendants("RegistroDeDetalle"))
                        {
                            XElement depositoEnEfectivo = registroDetalle.Element("DepositoEnEfectivo");
                            if (depositoEnEfectivo != null)
                            {
                                if (depositoEnEfectivo.Attribute("montoExcedenteDeposito") != null && depositoEnEfectivo.Attribute("montoExcedenteDeposito").Value != "")
                                    lstMontExce.Add(long.Parse(depositoEnEfectivo.Attribute("montoExcedenteDeposito").Value));
                            }
                        }
                        if (lstMontExce.Count > 0)
                        {
                            long sumaExedente = 0;
                            lstMontExce.ForEach(dato => { sumaExedente = sumaExedente + dato; });
                            if (sumaExedente <= 0)
                            {
                                this.erroresContendio.SinErroresEsquema = false;
                                this.erroresContendio.ListaErroresEsquema.Add(AccesoDatos.ObtenerMensaje(AccesoDatos.getValorInt(Constantes.Validar.idTipoMensaje), Constantes.MensajeError.ValidaMontoExcedenteDepósitosNoCoincide, declaracion.Folio));
                            }
                        }
                        else
                        {
                            this.erroresContendio.SinErroresEsquema = false;
                            this.erroresContendio.ListaErroresEsquema.Add(AccesoDatos.ObtenerMensaje(AccesoDatos.getValorInt(Constantes.Validar.idTipoMensaje), Constantes.MensajeError.ValidaMontoExcedenteDepósitosNoCoincide, declaracion.Folio));
                        }
                    }
                }
                else
                {
                    foreach (var registroDetalle in doc.Descendants("RegistroDeDetalle"))
                    {
                        XElement depositoEnEfectivo = registroDetalle.Element("DepositoEnEfectivo");
                        if (depositoEnEfectivo != null)
                        {
                            if (depositoEnEfectivo.Attribute("montoExcedente") != null && depositoEnEfectivo.Attribute("montoExcedente").Value != "")
                                lstMontExce.Add(long.Parse(depositoEnEfectivo.Attribute("montoExcedente").Value));
                            if (depositoEnEfectivo.Attribute("impuestoDeterminado") != null && depositoEnEfectivo.Attribute("impuestoDeterminado").Value != "")
                                lstImpuDete.Add(long.Parse(depositoEnEfectivo.Attribute("impuestoDeterminado").Value));
                        }
                    }

                    if (nEjercicio <= EF_ImpoExceDepo && nEjercicio >= EI_ImpoExceDepo)
                    {
                        if (nEjercicio == EI_ImpoExceDepo)
                        {
                            if (nPeriodo >= PI_ImpoExceDepo) bolImpoExceDepo = true;
                        }
                        else
                        {
                            if (nEjercicio == EF_ImpoExceDepo)
                            {
                                if (nPeriodo <= PF_ImpoExceDepo) bolImpoExceDepo = true;
                            }
                            else bolImpoExceDepo = true;
                        }
                    }
                    if (nEjercicio <= EF_ImpoDeteDepo && nEjercicio >= EI_ImpoDeteDepo)
                    {
                        if (nEjercicio == EI_ImpoDeteDepo)
                        {
                            if (nPeriodo >= PI_ImpoDeteDepo) bolImpoDeteDepo = true;
                        }
                        else
                        {
                            if (nEjercicio == EF_ImpoDeteDepo)
                            {
                                if (nPeriodo <= PF_ImpoDeteDepo) bolImpoDeteDepo = true;
                            }
                            else bolImpoDeteDepo = true;
                        }
                    }
                    if (bolImpoExceDepo)
                    {
                        doc = null;
                        long sumaExedente = 0;
                        if (importeExcedenteDepositos > 0)
                        {
                            lstMontExce.ForEach(dato => { sumaExedente = sumaExedente + dato; });
                            if (sumaExedente <= 0)
                            {
                                this.erroresContendio.SinErroresEsquema = false;
                                this.erroresContendio.ListaErroresEsquema.Add(AccesoDatos.ObtenerMensaje(AccesoDatos.getValorInt(Constantes.Validar.idTipoMensaje), Constantes.MensajeError.ValidaMontoOperacionesRegistrosDetalleMontoExcedenteDeclarado, declaracion.Folio));
                            }
                        }
                    }
                    if (bolImpoDeteDepo)
                    {
                        long sumaDeterminado = 0;
                        if (importeDeterminadoDepositos > 0)
                        {
                            lstImpuDete.ForEach(dato => { sumaDeterminado = sumaDeterminado + dato; });
                            if (sumaDeterminado <= 0)
                            {
                                this.erroresContendio.SinErroresEsquema = false;
                                this.erroresContendio.ListaErroresEsquema.Add(AccesoDatos.ObtenerMensaje(AccesoDatos.getValorInt(Constantes.Validar.idTipoMensaje), Constantes.MensajeError.ValidaMontoOperacionesRegistrosDetalleImpuestoDeterminado, declaracion.Folio));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.erroresContendio.ValidacionConcluida = false;
                AccesoDatos.RegistrarEvento(declaracion.Folio, string.Format("Error en el XML: {0} Archivo: {1}", ex.Message, sArchivo));
            }
            finally
            {
                doc = null;
            }

        }

        private bool ValidarVersionXML(string sArchivo)
        {
            bool result = true;
            string sEjercicio = "";
            int nEjercicio = 0;
            XmlReader xr = null;
            try
            {
                xr = XmlReader.Create(sArchivo);
                Declaracion.CadenaOriginal.Version = "2.0";
                while (xr.Read())
                {
                    if (xr.Name == Constantes.ElementoXml.DeclaracionInformativaAnualIDE ||
                        xr.Name == Constantes.ElementoXml.DeclaracionInformativaMensualIDE ||
                        xr.Name == Constantes.ElementoXml.DeclaracionInformativaAnualISR)
                    {
                        if (!string.IsNullOrEmpty(xr.GetAttribute(Constantes.AtributoXml.Version)))
                            Declaracion.CadenaOriginal.Version = xr.GetAttribute(Constantes.AtributoXml.Version);
                    }

                    if (xr.Name == Constantes.ElementoXml.Normal ||
                        xr.Name == Constantes.ElementoXml.Complementaria)
                    {
                        sEjercicio = xr.GetAttribute(Constantes.AtributoXml.Ejercicio);

                        int.TryParse(sEjercicio, out nEjercicio);
                        break;
                    }
                }

                Declaracion.CadenaOriginal.Ejercicio = nEjercicio;

                if (nEjercicio <= 0) result = false;
                if (Declaracion.CadenaOriginal.VersionSistema != Declaracion.CadenaOriginal.Version)
                    result = false;
            }
            catch (XmlException ex)
            {
                result = false;
                this.erroresContendio.ValidacionConcluida = false;
                AccesoDatos.RegistrarEvento(declaracion.Folio, string.Format("Error en el XML: {0} Archivo: {1}", ex.Message, sArchivo));
            }
            catch (Exception ex)
            {
                result = false;
                this.erroresContendio.ValidacionConcluida = false;
                AccesoDatos.RegistrarEvento(declaracion.Folio, string.Format("Error en el XML: {0} Archivo: {1}", ex.Message, sArchivo));
            }
            finally
            {
                if (xr != null)
                    xr.Close();
            }
            return result;
        }
    }
}