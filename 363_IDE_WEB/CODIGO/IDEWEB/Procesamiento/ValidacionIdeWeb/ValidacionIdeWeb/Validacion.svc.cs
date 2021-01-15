using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;
using System.Globalization;
using SAT.DyP.Negocio.Comun.Procesos;
using SAT.DyP.Negocio.Comun.Procesos.Seguridad;
using SAT.DyP.Util.Processors;
using SAT.DyP.Util.Types;
using SAT.DyP.Util.Processors.Mail;
using SAT.DyP.Negocio.Comun.Tipos;
using System.ServiceModel.Activation;
using System.Web;
using EX = ValidacionIdeWeb.Comunes.Exepciones;

namespace ValidacionIdeWeb
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class Validacion : IValidacion
    {
        void IValidacion.iniciarValidacion(int folio)
        {
            Declaracion declaracion = null;
            string pathArchivoFisico = "", subject = null;

            List<string> lstErrores = null;
            ValidacionDeclaracion validarDeclaracion = null;
            ResultadoValidacion resultadoValidacion = null;
            Acuse acuse = null;

            try
            {
                if (folio == null || folio == 0) throw new ApplicationException(string.Format("El folio: {0}, no es válido.", folio));

                declaracion = AccesoDatos.BuscarDeclaracionPorFolioEstatus(folio, Constantes.Estatus.recibidaProcesoValidacion); // OBTENER DECLARACION RECIBIDA

                if (declaracion == null || declaracion.Folio == 0) throw new ApplicationException(string.Format("No se encontró en recepción una declaración registrada con el folio: {0}, en estauts: {1}", folio, Constantes.Estatus.recibidaProcesoValidacion));

                acuse = new Acuse(); // GENERAR ACUSE DE ACEPTACION 
                validarDeclaracion = new ValidacionDeclaracion(declaracion); // VALIDACION

                pathArchivoFisico = Path.Combine(AccesoDatos.getValorString(Constantes.Validar.fileshare), declaracion.ArchivoFisico);

                if (!File.Exists(pathArchivoFisico))
                {
                    resultadoValidacion = new ResultadoValidacion() { EsCorreca = false, SinErroresEsquema = false, ValidacionConcluida = true, ListaErrores = new List<string>() { string.Format("El archivo: {0}, no existe en el directorio. Favor de volver a enviar la declaración.", declaracion.NombreArchivo) } };
                    throw new EX.ecAplicacion(string.Format("El archivo: {0}, no existe en el directorio: {1}.", declaracion.ArchivoFisico, AccesoDatos.getValorString(Constantes.Validar.fileshare)));//NO EXISTE EL ARCHIVO DE LA DECLARACION
                }

                if (AccesoDatos.GuardarDeclaracion(declaracion, pathArchivoFisico)) // GUARDAR DECLARACION EN BD
                {
                    AccesoDatos.ActualizarEstatusDeclaracion(folio, Constantes.Estatus.almacenadaEnBaseDatos); // ACTUALIZAR ESTATUS DECLARACION A ALMACENADA EN BD
                    AccesoDatos.RegistrarBitacora(folio, Constantes.Estatus.almacenadaEnBaseDatos);
                }

                resultadoValidacion = validarDeclaracion.validarNombreArchivo(); // VALIDACION NOMBRE ARCHIVO

                if (resultadoValidacion == null || !resultadoValidacion.EsCorreca) throw new EX.ecAplicacion(string.Format("Errror al validar el archivo {0}", pathArchivoFisico));

                resultadoValidacion = validarDeclaracion.validarContenidoArchivo(); // VALIDACION CONTENIDO

                if (resultadoValidacion == null || !resultadoValidacion.ValidacionConcluida) throw new EX.ecAplicacion("No se obtuvo un resultado de validacion o la validación no fue concluida.");

                if (AccesoDatos.ActualizarEstatusDeclaracion(folio, Constantes.Estatus.validacionConcluida))
                    AccesoDatos.RegistrarBitacora(folio, Constantes.Estatus.validacionConcluida);

                if (!resultadoValidacion.EsCorreca || !resultadoValidacion.SinErroresEsquema) throw new EX.ecAplicacion("Hubo errores al validar la declaración");

                AccesoDatos.ComplementarDatosRecepcion(declaracion, Constantes.Validar.tipoArchivoDelcaracion);

                try
                {
                    //OBTENER SELLO DIGITAL
                    declaracion.SelloDigital = SelloDigital.ObtenerFirma(declaracion.CadenaOriginal.ToString());

                    if (declaracion.SelloDigital == null && declaracion.SelloDigital == "") throw new ApplicationException(Constantes.Excepcion.selloDigital);

                    if (AccesoDatos.ActualizarEstatusDeclaracion(folio, Constantes.Estatus.sellada))
                        AccesoDatos.RegistrarBitacora(folio, Constantes.Estatus.sellada);

                    //OBTENER NUMERO DE OPERACION, NUMERO SERIE
                    string strFechaRecepcion = string.Format("{0:yyyyMMdd}", declaracion.FechaRecepcion);
                    declaracion.NumeroOperacion = GeneradorNumeroOperacion.Execute(folio, declaracion.RfcContribuyente, strFechaRecepcion);
                    declaracion.NumeroSerie = SelloDigital.NumeroSerieCertificadoOrganizacional();

                    //GUARDA ACEPTACION EN BD Y ACTUALIZA ESTATUS
                    AccesoDatos.GuardarAceptacion(declaracion);
                    if (AccesoDatos.ActualizarEstatusDeclaracion(folio, Constantes.Estatus.aceptada))
                        AccesoDatos.RegistrarBitacora(folio, Constantes.Estatus.aceptada);

                    //BUSCA LA DECLARACION CON ESTATUS ACEPTADA, PARA OBTENER LA ULTIMA FECHA DE MODIFICACION
                    Declaracion declaracionModificada = AccesoDatos.BuscarDeclaracionPorFolioEstatus(folio, Constantes.Estatus.aceptada);
                    declaracion.FechaModificacion = declaracionModificada.FechaModificacion;
                    acuse.PathAcuse = acuse.generarAcuseAceptacion(declaracion);

                }
                catch (Exception e)
                {
                    throw new ApplicationException(e.Message);
                }

                subject = Constantes.Validar.subjectAceptada; // TITULO MAIL ACEPTADA
            }
            catch (EX.ecAplicacion ex)
            {
                AccesoDatos.RegistrarBitacora(folio, Constantes.Estatus.validacionConcluida);
                AccesoDatos.RegistrarEvento(folio, ex.Message);

                if (resultadoValidacion == null) resultadoValidacion = new ResultadoValidacion();
                if (resultadoValidacion.ListaErrores == null) resultadoValidacion.ListaErrores = new List<string>();
                if (resultadoValidacion.ListaErroresEsquema == null) resultadoValidacion.ListaErroresEsquema = new List<string>();

                lstErrores = obtenerMensajesMotivosRechazo(resultadoValidacion, folio);

                AccesoDatos.GuardarMotivosRechazo(declaracion, lstErrores); // GUARDA RECHAZO EN BD Y ACTUALIZA ESTATUS
                if (AccesoDatos.ActualizarEstatusDeclaracion(folio, Constantes.Estatus.rechazada))
                    AccesoDatos.RegistrarBitacora(folio, Constantes.Estatus.rechazada);

                //GENERAR ACUSE DE RECHAZO POR NOMBRE DE ARCHIVO
                Declaracion declaracionModificada = AccesoDatos.BuscarDeclaracionPorFolioEstatus(folio, Constantes.Estatus.rechazada); // BUSCA LA DECLARACION CON ESTATUS RECHAZADA, PARA OBTENER LA ULTIMA FECHA DE MODIFICACION
                declaracion.FechaModificacion = declaracionModificada.FechaModificacion;
                acuse.PathAcuse = acuse.generarAcuseRechazo(declaracion, lstErrores);

                subject = Constantes.Validar.subjectRechazada; // TITULO MAIL RECHAZADA
            }
            catch (ApplicationException ex)
            {
                AccesoDatos.RegistrarEvento(folio, ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (subject != null || subject != "") //ENVIAR ACUSE VIA MAIL
                {
                    if (resultadoValidacion.ValidacionConcluida /*&& resultadoValidacion.EsCorreca && resultadoValidacion.SinErroresEsquema*/)
                        this.enviarAcuse(subject, acuse, declaracion);
                }
                validarDeclaracion = null;
                resultadoValidacion = null;
                acuse = null;
            }
        }

        private List<String> obtenerMensajesMotivosRechazo(ResultadoValidacion resultadoValidacion, int folio)
        {
            List<String> motivosRechazo = new List<string>();
            if (resultadoValidacion != null)
            {
                if (!resultadoValidacion.EsCorreca && resultadoValidacion.ListaErrores != null && resultadoValidacion.ListaErrores.Count > 0)
                {
                    for (int i = 0; i < resultadoValidacion.ListaErrores.Count; i++)
                    {
                        motivosRechazo.Add(resultadoValidacion.ListaErrores[i]);
                    }
                }
                if (!resultadoValidacion.SinErroresEsquema && resultadoValidacion.ListaErroresEsquema != null && resultadoValidacion.ListaErroresEsquema.Count > 0)
                {
                    for (int i = 0; i < resultadoValidacion.ListaErroresEsquema.Count; i++)
                    {
                        motivosRechazo.Add(resultadoValidacion.ListaErroresEsquema[i]);
                    }
                }
            }
            return motivosRechazo;
        }

        private void enviarAcuse(string subject, Acuse acuse, Declaracion declaracion)
        {
            if (acuse != null && declaracion != null && acuse.PathAcuse != null && declaracion.NombreArchivo != null)
            {
                try
                {
                    MessageNotification mail = new MessageNotification();
                    Contribuyente contribuyente = new Contribuyente();
                    contribuyente = ObtenerDatosContribuyente.Execute(declaracion.RfcContribuyente);

                    if (contribuyente != null)
                    {
                        mail.Email = contribuyente.Email;
                        mail.Subject = subject + declaracion.NombreArchivo;

                        string pathXSLT = Path.Combine(Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, Constantes.Validar.directorioEsquemas), AccesoDatos.getValorString(Constantes.Validar.pathXsltPlantilla));
                        mail.Message = acuse.runXSLT(pathXSLT, acuse.leerAcuse(acuse.PathAcuse));

                        if (SendMail.Execute(mail))
                        {
                            AccesoDatos.NotificarProcesamiento(declaracion.Folio);
                        }

                        File.Delete(acuse.PathAcuse);
                    }
                    else
                    {
                        SAT.DyP.Util.Logging.EventLogHelper.WriteWarningEntry("No se encontró información del Contribuyente, para el envío del correo electrónico.", 251);
                    }
                }
                catch (Exception e)
                {
                    AccesoDatos.RegistrarEvento(declaracion.Folio, e.Message);
                }
            }
            else
            {
                AccesoDatos.RegistrarEvento(declaracion.Folio, Constantes.Excepcion.envioMail);
            }
        }
    }
}
