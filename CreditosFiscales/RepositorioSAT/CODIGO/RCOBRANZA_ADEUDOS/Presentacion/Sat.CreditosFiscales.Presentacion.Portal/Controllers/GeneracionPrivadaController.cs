
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Presentacion.Controllers:Sat.CreditosFiscales.Presentacion.Controllers.GeneracionPrivadaController:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;
using Sat.CreditosFiscales.Comunes.Entidades.AccesoLogEventos;
using Sat.CreditosFiscales.Comunes.Entidades.AccionesPantalla;
using Sat.CreditosFiscales.Comunes.Entidades.Catalogos;
using Sat.CreditosFiscales.Comunes.Entidades.GeneraFormato;
using Sat.CreditosFiscales.Comunes.Herramientas;
using Sat.CreditosFiscales.Presentacion.Herramientas;
using Sat.CreditosFiscales.Presentacion.Portal.Models;
using Sat.CreditosFiscales.Presentacion.Portal.ViewModels;
using Sat.CreditosFiscales.Presentacion.Herramientas.ExtensionsMethods;
using Sat.CreditosFiscales.Comunes.Entidades;
using Sat.CreditosFiscales.Comunes.Entidades.CodigosError;
using System.Web.Security;
using Sat.CreditosFiscales.Comunes.Entidades.Seguridad;
using System.Globalization;

namespace Sat.CreditosFiscales.Presentacion.Portal.Controllers
{


    /// <summary>
    /// Clase que representa al controlador GeneracionPrivada
    /// </summary>
    public class GeneracionPrivadaController : Controller
    {
        private string rfcAuth = string.Empty;
        #region Métodos públicos

        public ActionResult Index(EnumAccionGeneraFormato accion)
        {
            if (ExisteHeader())
            {
                string ruta=  Url.Action("AccionContribuyente", "GeneracionPrivada", new { accionC = accion });
                MetodosComunesPortal.EscribeLogHeader(string.Format("Existe header se realiza redirect a {0}", ruta));
                CreaTicketSesion();

                this.ViewBag.Ruta =ruta;
                return View();
            }
            else
            {
                string ruta = Url.Action("Index", "Home");
                MetodosComunesPortal.EscribeLogHeader(string.Format("No existe header se realiza redirect a {0}", ruta));
                return RedirectToAction("Index", "Home");
            }
        }


        /// <summary>
        /// Acción que muestra la consulta de créditos fiscales por contribuyente.
        /// </summary>
        /// <param name="accion">Accion del tipo <see cref="EnumAccionGeneraFormato"/></param>
        /// <returns></returns>
        public ActionResult AccionContribuyente(EnumAccionGeneraFormato accionC)
        {
            try
            {
                #region Serializando fuente
                //DescuentoConceptoHijo descuentoHijo = new DescuentoConceptoHijo() { ImporteDescuento = 1000 };
                //DescuentoConcepto descuento = new DescuentoConcepto() { ImporteDescuento = 1000 };
                //DocumentoDeterminanteConceptoHijo hijo = new DocumentoDeterminanteConceptoHijo()
                //    {

                //        CreditoSIR = 2,
                //        FechaCausacion = DateTime.Now,
                //        IdConcepto = 820069,
                //        IdMotivo = "C",
                //        ImporteCondonacion = 123456.00M,
                //        ImporteDescuentos = 123456.00M,
                //        ImporteHistorico = 123456.00M,
                //        ImportePagar = 123456.00M,
                //        ImporteParteActualizada = 123456.00M,
                //        ImporteRecargos = 123456.00M,
                //        Descuentos = new List<DescuentoConceptoHijo>(){descuentoHijo,descuentoHijo}

                //    };


                //DocumentoDeterminanteConcepto concepto = new DocumentoDeterminanteConcepto()
                //{
                //    ConceptosHijo = new List<DocumentoDeterminanteConceptoHijo>() { hijo, hijo },
                //    CreditoARCA = 1,
                //    CreditoSIR = 2,
                //    Ejercicio = 2013,
                //    FechaCausacion = DateTime.Now,
                //    IdConcepto = 700176,
                //    IdMotivo = "C",
                //    IdPeriodicidad = 1,
                //    IdPeriodo = 1,
                //    IdTipoConcepto = 1,
                //    ImporteCondonacion = 123456.00M,
                //    ImporteDescuentos = 123456.00M,
                //    ImporteHistorico = 123456.00M,
                //    ImportePagar = 123456.00M,
                //    ImporteParteActualizada = 123456.00M,
                //    ImporteRecargos = 123456.00M,
                //    Descuentos = new List<DescuentoConcepto>() { descuento, descuento }

                //    //IdDocumento="500-27-00-04-00-2012-1196",
                //};
                //List<CatMarca> lMarca = new List<CatMarca>(){ 
                // new CatMarca(){
                //     CveMarca= 1,
                //     Descripcion="Resoluciones con cobro a cargo de la Entidad Federativa",
                //     Observacion ="Cobro a cargo de la Entidad Fedrativa",
                //     MostrarImportes=false,
                //     GenerarLC = false
                //}
                //, 
                //new CatMarca{
                //     CveMarca= 4,
                //     Descripcion="Resoluciones que se encuentran notificadas y dentro del plazo legal de los 45 días para el pago",
                //     Observacion ="En plazo legal para pago",
                //     MostrarImportes=true,
                //     GenerarLC = true
                //},
                // new CatMarca{
                //     CveMarca= 6,
                //     Descripcion="Resoluciones que se encuentran con algún medio de defensa registrado en el sistema",
                //     Observacion ="Con medio de defensa",
                //     MostrarImportes=true,
                //     GenerarLC = true
                //}
                //};

                //List<DocumentoDeterminante> doc = new List<DocumentoDeterminante>(){

                //    new DocumentoDeterminante(){

                //    Conceptos = new List<DocumentoDeterminanteConcepto>() { concepto, concepto },
                //    Marcas = lMarca,
                //    FechaDocumento = DateTime.Now,
                //    IdALR = 10,
                //    IdAutoridad = 1,
                //    ImporteActualizacion = 123456.00M,
                //    ImporteDescuentos = 123456.00M,
                //    ImporteHistorico = 123456.00M,
                //    ImportePagar = 123456.00M,
                //    ImporteRecargos = 123456.00M,
                //    Marcado = false,
                //    NumDocumento = "500-27-00-04-00-2012-1196",
                //    Rfc = "VAFA830616RMA"                    
                //    }
                //};

                //using (MemoryStream s = new MemoryStream())
                //{
                //    XmlSerializer serializador = new XmlSerializer(doc.GetType());
                //    serializador.Serialize(s, doc);
                //    s.Seek(0, SeekOrigin.Begin);
                //    XmlDocument xmlDoc = new XmlDocument();
                //    xmlDoc.Load(s);
                //    xmlDoc.Save(@"c:\CreditosFiscalesDummy.xml");

                //}
                #endregion


                GeneraFormatoPagoViewModel model = ObtieneModeloDatos();

                this.ViewBag.Accion = accionC;
                return View(model);
            }
            catch (FaultException<ReporteErrorWcf> err)
            {
                this.ViewBag.Accion = accionC;
                this.ViewBag.Error = MetodosComunes.AplicaFormatoError(err.Message, string.Empty);
                return View();
            }
            catch (Exception err)
            {
                this.ViewBag.Accion = accionC;
                var ticket = AccesoLogEventos.EscribirEntradaLog((int)EnumErroresGeneracionFormato.ErrorCargaInformacion, err);
                Exception errorManejado = new Exception(string.Format("No se pudo cargar con éxito la información de los créditos fiscales. Para dar seguimiento al error por favor conserve el siguiente número:{0} ", ticket), err);
                string innerException = errorManejado.InnerException.Message ?? string.Empty;
                this.ViewBag.Error = MetodosComunes.AplicaFormatoError(errorManejado.Message, innerException);

                return View();
            }
        }

        /// <summary>
        /// Acción para al
        /// </summary>
        /// <param name="model">Modelo del tipo <see cref="GeneraFormatoPagoViewModel"/></param>
        /// <returns>Vista con los créditos fiscales</returns>
        [HttpPost]
        public ActionResult AccionContribuyente(EnumAccionGeneraFormato accion, GeneraFormatoPagoViewModel model)
        {
            try
            {

                using (ClienteServicioGeneraFormato cliente = new ClienteServicioGeneraFormato())
                {
                    this.ViewBag.ModoProduccion = bool.Parse(ConfigurationManager.AppSettings["ModoProduccion"].ToString());
                    List<DocumentoDeterminante> docsSeleccionados = RecuperaSeleccionadosDelModelo(model);

                    ////Se almacena la información de los docs seleccionados en la cookie de sesión.
                    SeleccionUsuario su = MemberCache.SeleccionSolicitud;
                    su.DocumentosSeleccionados = docsSeleccionados;
                    MemberCache.SeleccionSolicitud = su;

                    if (model.AccionPantalla.Equals(3))
                    {
                        ////Se Obtienen los documentos almacenados de manera temporal en XML            
                        List<DocumentoDeterminante> docsAAlmacenar = ObtieneDocumentosAAlmacenar(docsSeleccionados);

                        Solicitud solicitud = new Solicitud()
                        {
                            Documentos = docsAAlmacenar,
                            Usuario = MemberCache.UsuarioActual
                        };

                        if (docsAAlmacenar.Where(det => det.IdPersonaMAT != null).Count() != 0)
                        {
                            DocumentoDeterminante primero = docsAAlmacenar.Where(det => det.IdPersonaMAT != null).First();
                            if (primero != null)
                            {
                                solicitud.Usuario.IdPersonaMAT = primero.IdPersonaMAT;
                                solicitud.Usuario.IdTipoLineaMAT = primero.IdTipoLineaMAT;
                            }
                        }

                        var idSolicitudCreditos = cliente.CreateChannel().GuardaSolicitud(solicitud);
                        EliminaArchivoTemporal();

                        ////Se limpia la seleccion de archivos, toda vez que se generó la solicitud
                        var seleccion = MemberCache.SeleccionSolicitud;
                        seleccion.DocumentosSeleccionados.Clear();
                        MemberCache.SeleccionSolicitud = seleccion;

                        ////TODO:Dirijir a Pago total
                        this.ViewBag.IdSolicitud = idSolicitudCreditos;
                        return PartialView("_GeneracionPrivada", model);                        
                    }
                    else
                    {
                        return PartialView("_GeneracionPrivada", model);
                        
                    }

                }

            }
            catch (FaultException<ReporteErrorWcf> err)
            {
                this.ViewBag.Error = MetodosComunes.AplicaFormatoError(err.Detail.Mensaje, err.Detail.InnerException);
                this.ViewBag.Accion = EnumAccionGeneraFormato.PagoTotal;
                return PartialView("_GeneracionPrivada", ObtieneModeloDatos());
                //return View(ObtieneModeloDatos());
            }
        }

        /// <summary>
        /// Acción que muestra la captura de paramétros para la consulta de deudores puros.
        /// </summary>
        /// <returns>Vista con los paramétros de consulta.</returns>
        public ActionResult DeudoresPuros()
        {
            DeudoresPurosViewModel model = new DeudoresPurosViewModel()
                {
                    Determinantes = null,
                    Parametros = null
                };

            try
            {
                FormsAuthentication.SetAuthCookie("DeudorPuro", false);
                this.ViewBag.n = new Encripcion().EncriptaCadena(new Random().Next(1, 99999).ToString());

                model.ListaAutoridad = MetodosComunesPortal.ObtieneCatalogoAutoridad();
                model.ListaAlr = MetodosComunesPortal.ObtieneCatalogoAlr();

                return View(model);

            }
            catch (FaultException<ReporteErrorWcf> err)
            {
                //this.ViewBag.Accion = accion;
                this.ViewBag.Error = MetodosComunes.AplicaFormatoError(err.Message, string.Empty);
                return View();
            }
            catch (Exception err)
            {

                model.ListaAutoridad = MetodosComunesPortal.ObtieneCatalogoAutoridad();
                model.ListaAlr = MetodosComunesPortal.ObtieneCatalogoAlr();
                var ticket = AccesoLogEventos.EscribirEntradaLog((int)EnumErroresGeneracionFormato.ErrorCargaPaginaPublica, err);
                Exception errorManejado = new Exception(string.Format("Ha ocurrido un error al cargar la página de consulta. Para dar seguimiento al error por favor conserve el siguiente número:{0} ", ticket), err);
                string innerException = errorManejado.InnerException.Message ?? string.Empty;
                model.MensajeError = MetodosComunes.AplicaFormatoError(errorManejado.Message, innerException);
                return View(model);
            }

        }

        /// <summary>
        /// Acción que muestra los resultados de la consulta de deudores puros.
        /// </summary>
        /// <param name="model">Modelo del tipo <see cref="DeudoresPurosViewModel"/></param>
        /// <returns>Vista con los resultados de la consulta de deudores puros.</returns>
        [HttpPost]
        public ActionResult DeudoresPuros(DeudoresPurosViewModel model)
        {
            try
            {

                ModelState.Clear();
                model.ListaAutoridad = MetodosComunesPortal.ObtieneCatalogoAutoridad();
                model.ListaAlr = MetodosComunesPortal.ObtieneCatalogoAlr();
                model.IdsRecargos = model.IdsRecargos = ObtieneIdsRecargos();
                
                this.ViewBag.n = new Encripcion().EncriptaCadena(new Random().Next(1, 99999).ToString());
                Int64 idSolicitudCreditos = 0;
                if (ModelState.IsValid)
                {

                    if (model.Accion.Equals(1))
                    {
                        MemberCache.UsuarioActual = new Comunes.Entidades.Usuario() { Rfc = model.Parametros.Rfc, IdALR = byte.Parse(model.Parametros.IdAlr), PersonaFisica = false };
                        if (!model.Parametros.Captcha.Trim().Equals(MemberCache.ValorCaptcha))
                        {
                            this.ViewBag.ErrorCaptcha = "Los valores ingresados no corresponden a los mostrados en la imagen";

                        }
                        else
                        {
                            model.Determinantes = this.ObtieneDeterminantesPuros(model);
                            model.Parametros.Captcha = string.Empty;
                        }
                    }
                    else if (model.Accion.Equals(2))
                    {
                        using (ClienteServicioGeneraFormato cliente = new ClienteServicioGeneraFormato())
                        {
                            ////Se Obtienen los documentos almacenados de manera temporal en XML   
                            List<DocumentoDeterminante> determinantes = new List<DocumentoDeterminante>();

                            foreach (var docModel in model.Determinantes)
                            {
                                determinantes.Add(MapeaModelADeterminante(docModel));
                            }
                            List<DocumentoDeterminante> docsAAlmacenar = ObtieneDocumentosAAlmacenar(determinantes);

                            Solicitud solicitud = new Solicitud()
                            {
                                Documentos = docsAAlmacenar,
                                Usuario = MemberCache.SeleccionSolicitud.DeudorPuro
                            };

                            if(docsAAlmacenar.Where(det => det.IdPersonaMAT != null).Count()!=0)
                            {
                            DocumentoDeterminante primero = docsAAlmacenar.Where(det => det.IdPersonaMAT != null).First();
                            if (primero != null)
                            {
                                solicitud.Usuario.IdPersonaMAT = primero.IdPersonaMAT;
                                solicitud.Usuario.IdTipoLineaMAT = primero.IdTipoLineaMAT;
                            }
                           }
                            idSolicitudCreditos = cliente.CreateChannel().GuardaSolicitud(solicitud);
                            EliminaArchivoTemporal();
                            model.Accion = -1;
                            this.ViewBag.IdSolicitud = idSolicitudCreditos;
                        }
                    }
                }


                return PartialView("_Deudores", model);

            }
            catch (FaultException<ReporteErrorWcf> err)
            {
                model.Determinantes = model.Determinantes = this.ObtieneDeterminantesPuros(model);
                model.ListaAutoridad = MetodosComunesPortal.ObtieneCatalogoAutoridad();
                model.ListaAlr = MetodosComunesPortal.ObtieneCatalogoAlr();
                model.MensajeError = MetodosComunes.AplicaFormatoError(err.Detail.Mensaje, err.Detail.InnerException);
                this.ViewBag.n = new Encripcion().EncriptaCadena(new Random().Next(1, 99999).ToString());
                return PartialView("_Deudores", model);
            }
            catch (Exception err)
            {
                model.Determinantes = model.Determinantes = this.ObtieneDeterminantesPuros(model);
                model.ListaAutoridad = MetodosComunesPortal.ObtieneCatalogoAutoridad();
                model.ListaAlr = MetodosComunesPortal.ObtieneCatalogoAlr();
                var ticket = AccesoLogEventos.EscribirEntradaLog((int)EnumErroresGeneracionFormato.ErrorDudoresPurosGenerico, err.Message, err);
                var et= new ExcepcionTipificada("Ha ocurrido un error al ejecutar la consulta. Intentelo más tarde", err, ticket);
                model.MensajeError = MetodosComunes.AplicaFormatoError(et.Mensaje, string.Empty);
                this.ViewBag.n = new Encripcion().EncriptaCadena(new Random().Next(1, 99999).ToString());
                return PartialView("_Deudores", model);
            }
        }

        /// <summary>
        /// Acción que genera el captcha
        /// </summary>
        /// <param name="n">Valor que se mostrará en la imagen del captcha.</param>
        /// <returns>Imagen del captcha.</returns>
        [HttpGet]
        public ActionResult Captcha(string n)
        {
            n = new Encripcion().DesencriptaCadena(n);
            MemberCache.ValorCaptcha = n;
            System.Drawing.Bitmap bm = new Bitmap(150, 100);
            Graphics g = Graphics.FromImage(bm);

            Font font = new Font("Comic Sans MS", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (byte)0);
            HatchBrush hatch = new HatchBrush(HatchStyle.Wave, Color.Green, Color.Beige);
            Point[] p = { new Point(0, 50), new Point(30, 10), new Point(100, 80), new Point(190, 50) };
            g.FillRectangle(hatch, 0, 0, 200, 100);

            Matrix myMatrix = new Matrix();
            myMatrix.Shear(0.8F, 0.07F);
            g.MultiplyTransform(myMatrix);
            g.RotateTransform(-10);

            for (int x = 0; x < 10; x++)
            {
                g.DrawBezier(new Pen(Color.DarkRed), p[0], p[1], p[2], p[3]);
                p[0] = new Point(0, x * 20);
                p[1] = new Point(30, x * 10);
                p[2] = new Point(100, (80 + x) * -10);
                p[3] = new Point(190, x * 20);

            }
            g.Transform.Shear(10.1F, 50.2F);
            g.DrawString(n, font, new SolidBrush(Color.FromArgb(205, 48, 49, 21)), new PointF(0, 30));
            g.Dispose();

            MemoryStream memoryStream = new MemoryStream();
            bm.Save(memoryStream, ImageFormat.Jpeg);
            Response.ContentType = "image/jpeg";
            return new FileContentResult(memoryStream.ToArray(), "image/jpeg");//;Response.BinaryWrite(memoryStream.ToArray());
        }


        [Obsolete("Los datos del mensaje emergente ahora se obtienen de ARCA.")]
        [HttpGet]
        public ActionResult ObtieneEmergente(string CveCortaAlr)
        {
            using (ClienteServicioCatalogos cliente = new ClienteServicioCatalogos())
            {
                ALR alr = cliente.CreateChannel().ObtenerAlrXCveCorta(CveCortaAlr);
                if (alr != null)
                {
                    return Json(new { estado = alr.Estado, prefijo = alr.Prefijo, telefono = alr.Telefono, rutainternet = alr.RutaInternet }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("-1", JsonRequestBehavior.AllowGet);
                }
            }
        }
        #endregion

        #region Métodos privados

        private bool ExisteHeader()
        {
            bool existeHeader = false;
            bool modoProduccion = false;
            modoProduccion = bool.Parse(ConfigurationManager.AppSettings["ModoProduccion"].ToString());

            MetodosComunesPortal.EscribeLogHeader("Inicia validacion header");
            MetodosComunesPortal.EscribeLogHeader(System.Web.HttpContext.Current.Request.ToRaw());

            MetodosComunesPortal.EscribeLogHeader("Buscando header en el request");  
            var headerRequest = System.Web.HttpContext.Current.Request.Headers["Authorization"];

            MetodosComunesPortal.EscribeLogHeader("Buscando header en el response");  
            var headerResponse = System.Web.HttpContext.Current.Response.Headers["Authorization"];

            if (headerRequest != null)
            {
                existeHeader = true;
                rfcAuth = headerRequest;
                MetodosComunesPortal.EscribeLogHeader(string.Format("Existe header en el request:{0}", rfcAuth));
            }
            else if (headerResponse != null)
            {
                existeHeader = true;
                rfcAuth = headerResponse;
                MetodosComunesPortal.EscribeLogHeader(string.Format("Existe header en el request:{0}", rfcAuth));
            }
            else if (MemberCache.UsuarioActual != null && !modoProduccion)
            {
                existeHeader = true;
                rfcAuth = MemberCache.UsuarioActual.Rfc;
                MetodosComunesPortal.EscribeLogHeader(string.Format("Existe cookie local :{0}", rfcAuth));
            }

            return existeHeader;
        }

        private void CreaTicketSesion()
        {

            if (!string.IsNullOrEmpty(rfcAuth))
            {

                using (ClienteServicioSeguridad c = new ClienteServicioSeguridad())
                {
                    InfoContribuyente infoIDC = c.CreateChannel().ObtenerInfoContribuyente(rfcAuth);
                    MemberCache.UsuarioActual = new Comunes.Entidades.Usuario()
                    {
                        ApellidoPaterno = infoIDC.ApellidoPaterno,
                        ApellidoMaterno = infoIDC.ApellidoMaterno,
                        Nombres = infoIDC.Nombre,
                        IdALR = (byte)infoIDC.IdALR,
                        Rfc = infoIDC.RfcVigente,
                        PersonaFisica = infoIDC.PersonaFisica,
                        RazonSocial = infoIDC.RazonSocial,
                        Rfcs = infoIDC.Rfcs,
                        //RV
                        BoId = infoIDC.BoId
                    };

                }
            }

        }

        /// <summary>
        /// Obtiene de la tabla de configuración los Ids para identificar a los concepto recargo.
        /// </summary>
        /// <returns><see cref="List<string>"/> con los Ids</returns>
        private List<string> ObtieneIdsRecargos()
        {
            using (ClienteServicioCatalogos cliente = new ClienteServicioCatalogos())
            {

                var ids = (string)cliente.CreateChannel().ConsultaConfiguracion("IdsConceptosRecargo");
                return ids.Split('|').ToList();
            }
        }

        /// <summary>
        /// Filtra los documentos determinantes a mostrar en la vista GeneracionPrivada con base a la seleccion previa del usuario.
        /// </summary>
        /// <returns><see cref="GeneraFormatoPagoViewModel"/></returns>
        private GeneraFormatoPagoViewModel ObtieneModeloDatos()
        {
            EliminaArchivoTemporal();
            var lista = this.ObtieneDeterminantes();
            GeneraFormatoPagoViewModel model = new GeneraFormatoPagoViewModel() { Determinantes = lista };

            model.IdsRecargos = ObtieneIdsRecargos();


            ////Se valida que no existan cambios en la fuente de información
            int originalesSeleccionados = lista.Where(se => se.Seleccionado).Count();
            if (originalesSeleccionados < MemberCache.SeleccionSolicitud.DocumentosSeleccionados.Count())
            {
                this.ViewBag.CambioOrigen = MetodosComunes.MensajeCambioDatosOrigen(@"Los datos de los documentos determinantes han cambiado desde la última vez que visitó esta página.
                                <br/><br/> <b>Se desmarcaran los documentos previamente seleccionados y que hayan cambiado</b>");
            }
            return model;
        }

        /// <summary>
        /// Obtiene los docuementos determiantes desde ARCA.
        /// </summary>
        /// <returns><see cref="List<DocDetermintanteModel>"/></returns>
        private List<DocDetermintanteModel> ObtieneDeterminantes()
        {
            List<DocumentoDeterminante> determinantes = new List<DocumentoDeterminante>();
            List<DocDetermintanteModel> determinantesPresentacion = new List<DocDetermintanteModel>();

            Usuario usuario = MemberCache.UsuarioActual;

            #region Obtiene los Docs


            XmlSerializer serializador = new XmlSerializer(determinantes.GetType());
            //XmlDocument doc = new XmlDocument();
            //doc.Load(@"c:\CreditosFiscalesDummy.xml");
            //StringReader reader = new StringReader(doc.InnerXml);
            //determinantes = (List<DocumentoDeterminante>)serializador.Deserialize(new XmlTextReader(reader));
            using (ClienteServicioGeneraFormato cliente = new ClienteServicioGeneraFormato())
            {

                determinantes = cliente.CreateChannel().ObtenerInformacionContribuyente(usuario.Rfc, usuario.Rfcs);

            }
            #endregion

            foreach (var determinante in determinantes)
            {
                determinante.NumeroConceptos = determinante.Conceptos.Count();
                determinante.Conceptos = determinante.Conceptos.OrderBy(c => c.CreditoSIR).ThenBy(c => c.CreditoARCA).ToList();
                determinantesPresentacion.Add(MapeaDocDeterminante(determinante));
            }

            //Se almancena el XML de los documentos para no realizar llamada a ARCA
            using (MemoryStream ms = new MemoryStream())
            {
                serializador.Serialize(ms, determinantes);
                ms.Seek(0, SeekOrigin.Begin);
                XmlDocument docTemp = new XmlDocument();
                docTemp.Load(ms);
                string ruta = ConfigurationManager.AppSettings["RutaDocsTemporal"];
                string nombreArchivo = string.Format("{0}{1}{2}.xml", MemberCache.UsuarioActual.Rfc, DateTime.Now.ToFileString(), Guid.NewGuid());
                ruta += nombreArchivo;
                SeleccionUsuario su = MemberCache.SeleccionSolicitud;
                su.ArchivoTemporal = ruta;
                MemberCache.SeleccionSolicitud = su;
                docTemp.Save(ruta);
            }


            return determinantesPresentacion;
        }

        /// <summary>
        /// Obtiene la lista de los documentos determinates desde ARCA para los deudores puros.
        /// </summary>
        /// <param name="model">Modelo del tipo <see cref="DeudoresPurosViewModel"/ > con los paramétros de consulta</param>
        /// <returns><see cref="List<DocDetermintanteModel> "/></returns>
        private List<DocDetermintanteModel> ObtieneDeterminantesPuros(DeudoresPurosViewModel model)
        {

            EliminaArchivoTemporal();

            List<DocumentoDeterminante> determinantes = new List<DocumentoDeterminante>();
            List<DocDetermintanteModel> determinantesPresentacion = new List<DocDetermintanteModel>();
            DocumentosContribuyentePuro datosContPuro = new DocumentosContribuyentePuro();
            #region Obtiene los Docs
            XmlSerializer serializador = new XmlSerializer(determinantes.GetType());
            
            using (ClienteServicioGeneraFormato cliente = new ClienteServicioGeneraFormato())
            {
                DateTime fecha = DateTime.ParseExact(model.Parametros.Fecha, "dd/MM/yyyy", CultureInfo.InvariantCulture);// DateTime.Parse(model.Parametros.Fecha);
                datosContPuro =
                cliente.CreateChannel().ObtenerInformacionContribuyentePuro(model.Parametros.Rfc,
                                                                                            model.Parametros.NumeroDocumento,
                                                                                            int.Parse(model.Parametros.IdAutoridad),
                                                                                            int.Parse(model.Parametros.IdAlr),
                                                                                            fecha);

                determinantes = datosContPuro.Documentos;
            }
            #endregion

            foreach (var determinante in determinantes)
            {
                determinante.NumeroConceptos = determinante.Conceptos.Count();
                determinantesPresentacion.Add(MapeaDocDeterminante(determinante));
            }
            //Se almancena el XML de los documentos para no realizar llamada a ARCA
            using (MemoryStream ms = new MemoryStream())
            {
                serializador.Serialize(ms, determinantes);
                ms.Seek(0, SeekOrigin.Begin);
                XmlDocument docTemp = new XmlDocument();
                docTemp.Load(ms);
                string ruta = ConfigurationManager.AppSettings["RutaDocsTemporal"];
                string nombreArchivo = string.Format("{0}{1}{2}.xml", model.Parametros.Rfc, DateTime.Now.ToFileString(), Guid.NewGuid());
                ruta += nombreArchivo;
                SeleccionUsuario su = MemberCache.SeleccionSolicitud;
                su.ArchivoTemporal = ruta;
                su.DeudorPuro = datosContPuro.Usuario;
                MemberCache.SeleccionSolicitud = su;
                docTemp.Save(ruta);
            }


            return determinantesPresentacion;
        }

        /// <summary>
        /// Elimina el archivo temporal con los datos de los documentos determinantes.
        /// </summary>
        private static void EliminaArchivoTemporal()
        {
            ////Si existe un archivo temporal previo, se elimina
            string rutaArchivoTemportal = MemberCache.SeleccionSolicitud.ArchivoTemporal;
            if (!string.IsNullOrEmpty(rutaArchivoTemportal))
            {
                if (System.IO.File.Exists(rutaArchivoTemportal))
                {
                    System.IO.File.Delete(rutaArchivoTemportal);
                }
            }
        }

        private List<DocDeterminanteConceptoModel> MapeaConceptosPadre(List<DocumentoDeterminanteConcepto> Conceptos)
        {
            using (ClienteServicioCatalogos cliente = new ClienteServicioCatalogos())
            {

                List<DocDeterminanteConceptoModel> conceptosPadre = new List<DocDeterminanteConceptoModel>();
                foreach (var c in Conceptos)
                {
                    var descripciones = cliente.CreateChannel().ObtenerDescripcionConceptoPeriodicidad(c.IdConcepto, c.IdPeriodo, c.IdPeriodicidad);
                    conceptosPadre.Add(new DocDeterminanteConceptoModel()
                    {
                        ConceptosHijo = MapeaConceptosHijo(c.ConceptosHijo, cliente),
                        CreditoARCA = c.CreditoARCA,
                        CreditoSIR = c.CreditoSIR,
                        Descripcion = descripciones.DescripcionConcepto,
                        DescripcionPerido = descripciones.DescripcionPeriodo,
                        Ejercicio = c.Ejercicio,
                        FechaCausacion = c.FechaCausacion,
                        IdConcepto = c.IdConcepto,
                        IdDocumento = c.IdDocumento,
                        IdMotivo = c.IdMotivo,
                        IdPeriodicidad = c.IdPeriodicidad,
                        IdPeriodo = c.IdPeriodo,
                        ImporteCondonacion = c.ImporteCondonacion,
                        ImporteDescuentos = c.ImporteDescuentos,
                        ImporteHistorico = c.ImporteHistorico,
                        ImporteParteActualizada = c.ImporteParteActualizada,
                        ImporteRecargos = c.ImporteRecargos,
                        ImportePagar = c.ImportePagar
                    });


                }
                return conceptosPadre;
            }


        }

        private List<DocDeterminanteConceptoHijoModel> MapeaConceptosHijo(List<DocumentoDeterminanteConceptoHijo> Conceptos, ClienteServicioCatalogos cliente)
        {

            List<DocDeterminanteConceptoHijoModel> conceptosHijo = new List<DocDeterminanteConceptoHijoModel>();
            foreach (var c in Conceptos)
            {
                var descripciones = cliente.CreateChannel().ObtenerDescripcionConceptoPeriodicidad(c.IdConcepto, c.IdPeriodo, c.IdPeriodicidad);
                conceptosHijo.Add(new DocDeterminanteConceptoHijoModel()
                {

                    CreditoSIR = c.CreditoSIR,
                    Descripcion = descripciones.DescripcionConcepto,
                    FechaCausacion = c.FechaCausacion,
                    IdConcepto = c.IdConcepto,
                    IdDocumento = c.IdDocumento,
                    IdMotivo = c.IdMotivo,
                    ImporteCondonacion = c.ImporteCondonacion,
                    ImporteDescuentos = c.ImporteDescuentos,
                    ImporteHistorico = c.ImporteHistorico,
                    ImporteParteActualizada = c.ImporteParteActualizada,
                    ImporteRecargos = c.ImporteRecargos,
                    ImportePagar = c.ImportePagar,
                    //RV
                    IdPeriodo = c.IdPeriodo,
                    IdPeriodicidad = c.IdPeriodicidad

                });
            }
            return conceptosHijo;
        }

        private DocDetermintanteModel MapeaDocDeterminante(DocumentoDeterminante docOriginal)
        {
            //CatMarca marca = MetodosComunesPortal.ObtieneMarcaDeCache(docOriginal.CveMarca);

            List<CatMarcaModel> listaMarcas = new List<CatMarcaModel>();
            foreach (var m in docOriginal.Marcas)
            {
                listaMarcas.Add(MapeaMarcaModel(m, docOriginal.IdALR));
            }

            bool marcado = listaMarcas.Where(a => a.GenerarLC.Equals(false)).Count() >= 1;
            bool mostrarImportes = !(listaMarcas.Where(a => a.MostrarImportes.Equals(false)).Count() >= 1);

            DocDetermintanteModel doc = new DocDetermintanteModel()
            {
                ConceptosPadre = MapeaConceptosPadre(docOriginal.Conceptos),
                NumeroConceptos = docOriginal.Conceptos.Count(),
                MarcasModel = listaMarcas,
                DescripcionAutoridad = MetodosComunesPortal.ObtieneDescripcionAutoridadDeCache(docOriginal.IdAutoridad),
                FechaDocumento = docOriginal.FechaDocumento,
                IdALR = docOriginal.IdALR,
                IdAutoridad = docOriginal.IdAutoridad,
                IdDocumento = docOriginal.IdDocumento,
                IdSolicitud = docOriginal.IdSolicitud,
                ImporteActualizacion = docOriginal.ImporteActualizacion,
                ImporteDescuentos = docOriginal.ImporteDescuentos,
                ImporteHistorico = docOriginal.ImporteHistorico,
                ImporteRecargos = docOriginal.ImporteRecargos,
                ImportePagar = docOriginal.ImportePagar,
                Marcado = marcado,
                MostrarImportes = mostrarImportes,
                NumDocumento = docOriginal.NumDocumento,
                Rfc = docOriginal.Rfc,
                Seleccionado = true
            };

            if (MemberCache.SeleccionSolicitud.DocumentosSeleccionados.Count() > 0)
            {
                ////Se busca que el documento haya sido seleccionado previamente
                var seleccionado = (from s in MemberCache.SeleccionSolicitud.DocumentosSeleccionados
                                    where (
                                        //s.CveMarca == doc.CveMarca &&
                                        s.FechaDocumento.ToString().Equals(doc.FechaDocumento.ToString()) &&
                                        s.IdALR == doc.IdALR &&
                                        s.IdAutoridad == doc.IdAutoridad &&
                                        s.ImporteActualizacion == doc.ImporteActualizacion &&
                                        s.ImporteDescuentos == doc.ImporteDescuentos &&
                                        s.ImporteHistorico == doc.ImporteHistorico &&
                                        s.NumeroConceptos == doc.NumeroConceptos &&
                                        s.NumDocumento == doc.NumDocumento &&
                                           s.Rfc == doc.Rfc)
                                    select s).ToList().Count();
                doc.Seleccionado = seleccionado > 0 ? true : false;
            }



            return doc;

        }

        private CatMarcaModel MapeaMarcaModel(CatMarca marca, int idAlr)
        {
            ALR alr = MetodosComunesPortal.ObtenerAlrXId((byte)idAlr);
            if (alr != null)
            {
                CatMarcaModel marcaModel = new CatMarcaModel()
                {
                    CveMarca = marca.CveMarca,
                    Prefijo = alr.Prefijo,
                    Entidad = alr.Estado,
                    Descripcion = marca.Descripcion,
                    GenerarLC = marca.GenerarLC,
                    MostrarImportes = marca.MostrarImportes,
                    Observacion = marca.Observacion,
                    Portal = marca.Portal,
                    Telefonos = HttpUtility.HtmlEncode(marca.Telefonos)
                };
                return marcaModel;

            }
            else
            {
                throw new Exception(string.Format("No existe informacion para la ALR con el id {0}", idAlr));
            }

        }

        private DocumentoDeterminante MapeaModelADeterminante(DocDetermintanteModel docOriginal)
        {

            DocumentoDeterminante doc = new DocumentoDeterminante()
            {
                Conceptos = docOriginal.Conceptos,
                NumeroConceptos = docOriginal.NumeroConceptos,
                //CveMarca = docOriginal.CveMarca,
                FechaDocumento = docOriginal.FechaDocumento,
                IdALR = docOriginal.IdALR,
                IdAutoridad = docOriginal.IdAutoridad,
                IdDocumento = docOriginal.IdDocumento,
                IdSolicitud = docOriginal.IdSolicitud,
                ImporteActualizacion = docOriginal.ImporteActualizacion,
                ImporteDescuentos = docOriginal.ImporteDescuentos,
                ImporteHistorico = docOriginal.ImporteHistorico,
                ImporteRecargos = docOriginal.ImporteRecargos,
                ImportePagar = docOriginal.ImportePagar,
                Marcado = docOriginal.Marcado,
                NumDocumento = docOriginal.NumDocumento,
                Rfc = docOriginal.Rfc,
            };

            return doc;
        }

        private static List<DocumentoDeterminante> ObtieneDocumentosAAlmacenar(List<DocumentoDeterminante> docsSeleccionados)
        {
            List<DocumentoDeterminante> docsTemporales = new List<DocumentoDeterminante>();
            List<DocumentoDeterminante> docsAAlmacenar = new List<DocumentoDeterminante>();
            XmlSerializer serializador = new XmlSerializer(docsTemporales.GetType());
            XmlDocument xmlDocsTemporales = new XmlDocument();
            xmlDocsTemporales.Load(MemberCache.SeleccionSolicitud.ArchivoTemporal);
            StringReader reader = new StringReader(xmlDocsTemporales.InnerXml);


            docsTemporales = (List<DocumentoDeterminante>)serializador.Deserialize(new XmlTextReader(reader));

            foreach (var docSeleccionado in docsSeleccionados)
            {
                var docAAlmacenar = (from docTemporal in docsTemporales
                                     where (
                                         //docTemporal.CveMarca == docSeleccionado.CveMarca &&
                                        docTemporal.FechaDocumento.ToString().Equals(docSeleccionado.FechaDocumento.ToString()) &&
                                        docTemporal.IdALR == docSeleccionado.IdALR &&
                                        docTemporal.IdAutoridad == docSeleccionado.IdAutoridad &&
                                        docTemporal.ImporteActualizacion == docSeleccionado.ImporteActualizacion &&
                                        docTemporal.ImporteDescuentos == docSeleccionado.ImporteDescuentos &&
                                        docTemporal.ImporteHistorico == docSeleccionado.ImporteHistorico &&
                                        docTemporal.NumeroConceptos == docSeleccionado.NumeroConceptos &&
                                        docTemporal.NumDocumento == docSeleccionado.NumDocumento &&
                                            docTemporal.Rfc == docSeleccionado.Rfc)
                                     select docTemporal).FirstOrDefault();
                docsAAlmacenar.Add(docAAlmacenar);
            }
            return docsAAlmacenar;
        }

        private List<DocumentoDeterminante> RecuperaSeleccionadosDelModelo(GeneraFormatoPagoViewModel model)
        {
            List<DocDetermintanteModel> seleccionados = new List<DocDetermintanteModel>();
            List<DocumentoDeterminante> docsSeleccionados = new List<DocumentoDeterminante>();
            if (model.Determinantes != null)
            {
                seleccionados = (from docSeleccionado in model.Determinantes
                                 where docSeleccionado.Seleccionado
                                 select docSeleccionado).ToList();


                foreach (var seleccionado in seleccionados)
                {
                    docsSeleccionados.Add(MapeaModelADeterminante(seleccionado));
                }
            }
            return docsSeleccionados;
        }
        #endregion
    }
}





