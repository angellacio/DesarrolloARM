
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Presentacion.Controllers:Sat.CreditosFiscales.Presentacion.Controllers.LogEventosController:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sat.CreditosFiscales.Presentacion.Herramientas;
using Sat.CreditosFiscales.Presentacion.Portal.ViewModels;
using Sat.CreditosFiscales.Procesamiento.LogicaNegocio.AccesoLogEventos;
using Sat.CreditosFiscales.Procesamiento.LogicaNegocio.Servicios;

using System.Web.Security;
using System.Globalization;
using System.Threading;

namespace Sat.CreditosFiscales.Presentacion.Portal.Controllers
{
    public class LogEventosController : Controller
    {

        private string nombreCookie = "LCLogEventos";
        enum Aplicacion
        {
            CreditosFiscales,
            Traductor,
            Templates
        }



        #region Acceso
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LogOff()
        {
            HttpCookie cookie = Request.Cookies[nombreCookie];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }
            return Redirect("Index");
        }
        private bool validaCookie()
        {
            HttpCookie cookie = Request.Cookies[nombreCookie];
            return (cookie != null);
        }
        public ActionResult ValidaAcceso(GeneraAccesoLogViewModel model)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    bool accesoValido = false;
                    using (ClienteServicioConsultaEventos log = new ClienteServicioConsultaEventos())
                    {
                        accesoValido = log.CreateChannel().VerificaAcceso(model.Usuario, model.Contraseña);
                    }

                    if (!accesoValido)
                    {
                        ViewBag.OnLoadCompleteMessage = Alertas.Alerta("Acceso no autorizado", "Aceptar");
                    }
                    else
                    {
                        HttpCookie cookie = new HttpCookie(nombreCookie, model.Contraseña);
                        cookie.Expires = DateTime.Now.AddMinutes(20);
                        Response.Cookies.Add(cookie);

                        return RedirectToAction("CreditosLogEvento", "LogEventos");
                    }
                }

            }
            catch (Exception ex)
            {
                ViewBag.OnLoadCompleteMessage = Alertas.Errror(ex.Message, "Aceptar");
            }

            return View("Index");
        }

        public ActionResult CambiarPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CambiarPassword(CambiaPasswordViewModel model)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    bool accesoValido = false;
                    if (model.NuevaContraseña == model.ConfirmarContraseña)
                    {
                        using (ClienteServicioConsultaEventos log = new ClienteServicioConsultaEventos())
                        {
                            accesoValido = log.CreateChannel().VerificaAcceso(model.Usuario, model.Contraseña);
                            if (accesoValido)
                            {
                                if (log.CreateChannel().CambiarPassword(model.Usuario, model.NuevaContraseña))
                                {
                                    //ViewBag.OnLoadCompleteMessage = Alertas.Alerta("Su contraseña fue actualizada correctamente.", "Aceptar");
                                    ViewBag.OnLoadCompleteScript = new MvcHtmlString("window.location = './';");
                                }
                                else
                                {
                                    ViewBag.OnLoadCompleteMessage = Alertas.Alerta("No se logró actualizar la contraseña.", "Aceptar");
                                }
                            }
                            else
                            {
                                ViewBag.OnLoadCompleteMessage = Alertas.Alerta("La contraseña del usuario actual no es correcta.", "Aceptar");
                            }
                        }
                    }
                    else
                    {
                        ViewBag.OnLoadCompleteMessage = Alertas.Alerta("La nueva contraseña y la confirmación no coinciden.", "Aceptar");
                    }
                }

            }
            catch (Exception ex)
            {
                ViewBag.OnLoadCompleteMessage = Alertas.Errror(ex.Message, "Aceptar");
            }
            model = new CambiaPasswordViewModel();
            return View();
        }
        #endregion
        #region Creditos Fiscales


        #region Logs de eventos
        public ActionResult CreditosLogEvento()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("es-MX");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("es-MX");

            GeneraLogEventosViewModel model = new GeneraLogEventosViewModel();
            try
            {
                if (!validaCookie()) return Redirect("Index");


                model.FechaInicio = DateTime.Now.ToString("dd/MM/yyy");
                model.FechaFin = DateTime.Now.ToString("dd/MM/yyy");

                //string porFechaInicio = Convert.ToDateTime(model.FechaInicio).ToString("MM/dd/yyyy");
                //string porFechaFin = Convert.ToDateTime(model.FechaFin).ToString("MM/dd/yyyy");
                //using (ClienteServicioConsultaEventos log = new ClienteServicioConsultaEventos())
                //{
                //    model.listaEventos = log.CreateChannel().BuscarEventos(Aplicacion.CreditosFiscales.ToString(), string.Empty, porFechaInicio, porFechaFin);
                //}


            }
            catch (Exception ex)
            {
                ViewBag.OnLoadCompleteMessage = Alertas.Errror(ex.Message, "Aceptar");
            }
            return View("CreditosLogEvento", model);

        }
        public ActionResult CreditosBuscarEventos(GeneraLogEventosViewModel model)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("es-MX");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("es-MX");

            try
            {
                if (modeloEsValido(model))
                {
                    string porTicket = model.Ticket;
                    string porFechaInicio = Convert.ToDateTime(model.FechaInicio).ToString("MM/dd/yyyy");
                    string porFechaFin = Convert.ToDateTime(model.FechaFin).ToString("MM/dd/yyyy");

                    using (ClienteServicioConsultaEventos log = new ClienteServicioConsultaEventos())
                    {
                        model.listaEventos = log.CreateChannel().BuscarEventos(Aplicacion.CreditosFiscales.ToString(), porTicket, porFechaInicio, porFechaFin);
                    }

                    if (model.listaEventos.Count == 0)
                    {
                        ViewBag.OnLoadCompleteMessage = Alertas.Informacion("No hay información para este criterio de búsqueda.", "Aceptar");
                    }
                }

            }
            catch (Exception ex)
            {
                ViewBag.OnLoadCompleteMessage = Alertas.Errror(ex.Message, "Aceptar");
            }
            return View("CreditosLogEvento", model);
        }
        private bool modeloEsValido(GeneraLogEventosViewModel model)
        {
            HttpContext.Session["culture"] = "es-ES";
            bool resultado = ModelState.IsValid;
            if (resultado)
            {
                if (!string.IsNullOrEmpty(model.Ticket))
                {
                    resultado = IsGuid(model.Ticket);
                    if (!resultado)
                    {
                        ModelState.AddModelError("Ticket", "(*)");
                    }
                }
                if (!validaFechas(model.FechaInicio, model.FechaFin))
                {
                    resultado = false;
                    ModelState.AddModelError("FechaInicio", "(*) La fecha inicio no puede ser mayor a la fin.");
                }
            }

            return resultado;
        }

        #endregion


        #region Peticiones
        public ActionResult CreditosPeticion()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("es-MX");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("es-MX");

            if (!validaCookie()) return Redirect("Index");

            GeneraPeticionesViewModel model = new GeneraPeticionesViewModel();

            model.FechaInicio = DateTime.Now.ToString("dd/MM/yyy");
            model.FechaFin = DateTime.Now.ToString("dd/MM/yyy");
            return View("CreditosPeticion", model);

        }
        public ActionResult CreditosBuscarPeticiones(GeneraPeticionesViewModel model)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("es-MX");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("es-MX");

            try
            {
                if (!validaFechas(model.FechaInicio, model.FechaFin))
                {
                    ModelState.AddModelError("FechaInicio", "(*) La fecha inicio no puede ser mayor a la fin.");
                }
                else
                {
                    string porRfc = model.Rfc;
                    string porFechaInicio = Convert.ToDateTime(model.FechaInicio).ToString("MM/dd/yyyy");
                    string porFechaFin = Convert.ToDateTime(model.FechaFin).ToString("MM/dd/yyyy");
                    int conError = model.conError;

                    using (ClienteServicioConsultaEventos log = new ClienteServicioConsultaEventos())
                    {
                        model.listaPeticiones = log.CreateChannel().BuscarPeticiones(porRfc, porFechaInicio, porFechaFin, conError);
                    }
                    if (model.listaPeticiones.Count == 0)
                    {
                        ViewBag.OnLoadCompleteMessage = Alertas.Informacion("No hay información para este criterio de búsqueda.", "Aceptar");
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.OnLoadCompleteMessage = Alertas.Errror(ex.Message, "Aceptar");
            }
            return View("CreditosPeticion", model);
        }
        #endregion

        #endregion

        #region Traductor
        public ActionResult TraductorLogEvento()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("es-MX");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("es-MX");

            GeneraLogEventosViewModel model = new GeneraLogEventosViewModel();
            try
            {
                if (!validaCookie()) return Redirect("Index");


                model.FechaInicio = DateTime.Now.ToString("dd/MM/yyy");
                model.FechaFin = DateTime.Now.ToString("dd/MM/yyy");
            }
            catch (Exception ex)
            {
                ViewBag.OnLoadCompleteMessage = Alertas.Errror(ex.Message, "Aceptar");
            }
            return View("TraductorLogEvento", model);

        }
        public ActionResult TraductorBuscarEventos(GeneraLogEventosViewModel model)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("es-MX");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("es-MX");

            try
            {
                if (modeloEsValido(model))
                {
                    string porTicket = model.Ticket;
                    string porFechaInicio = Convert.ToDateTime(model.FechaInicio).ToString("MM/dd/yyyy");
                    string porFechaFin = Convert.ToDateTime(model.FechaFin).ToString("MM/dd/yyyy");

                    using (ClienteServicioConsultaEventos log = new ClienteServicioConsultaEventos())
                    {
                        model.listaEventos = log.CreateChannel().BuscarEventos(Aplicacion.Traductor.ToString(), porTicket, porFechaInicio, porFechaFin);
                    }

                    if (model.listaEventos.Count == 0)
                    {
                        ViewBag.OnLoadCompleteMessage = Alertas.Informacion("No hay información para este criterio de búsqueda.", "Aceptar");
                    }
                }

            }
            catch (Exception ex)
            {
                ViewBag.OnLoadCompleteMessage = Alertas.Errror(ex.Message, "Aceptar");
            }
            return View("TraductorLogEvento", model);
        }


        public ActionResult TraductorBitacora()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("es-MX");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("es-MX");

            GeneraTraductorBitacoraViewModel model = new GeneraTraductorBitacoraViewModel();
            try
            {
                if (!validaCookie()) return Redirect("Index");

                model.FechaInicio = DateTime.Now.ToString("dd/MM/yyy");
                //model.FechaFin = DateTime.Now.ToString("dd/MM/yyy");
                model.listaAplicacion = MetodosComunesPortal.ObtieneCatalogoAplicaciones();
                model.listaTipoDocumento = MetodosComunesPortal.ObtieneCatalogoTipoDocumento();
                model.listaPasos = MetodosComunesPortal.ObtieneCatalogoPasos();


            }
            catch (Exception ex)
            {
                ViewBag.OnLoadCompleteMessage = Alertas.Errror(ex.Message, "Aceptar");
            }
            return View("TraductorBitacora", model);
        }

        public ActionResult TraductorBitacoraBuscar(GeneraTraductorBitacoraViewModel model)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("es-MX");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("es-MX");
            Guid idProcesamiento;

            try
            {
                model.listaAplicacion = MetodosComunesPortal.ObtieneCatalogoAplicaciones();
                model.listaTipoDocumento = MetodosComunesPortal.ObtieneCatalogoTipoDocumento();
                model.listaPasos = MetodosComunesPortal.ObtieneCatalogoPasos();

                if (!validaFechas(model.FechaInicio, model.FechaInicio))
                {
                    ModelState.AddModelError("FechaInicio", "(*) La fecha inicio no puede ser mayor a la fin.");
                }
                else if (!string.IsNullOrWhiteSpace(model.IdProcesamiento) && !Guid.TryParse(model.IdProcesamiento, out idProcesamiento))
                {

                    ModelState.AddModelError("IdProcesamiento", "(*) IdProcesamiento inválido.");
                }
                else
                {



                    int porIdAplicacion = model.IdAplicacion;
                    int porIdTipoDocumento = model.IdTipoDocumento;
                    DateTime porFechaInicio = Convert.ToDateTime(model.FechaInicio);
                    DateTime porFechaFin = porFechaInicio;

                    if (!string.IsNullOrWhiteSpace(model.IdProcesamiento))
                    {
                        porFechaInicio = DateTime.MinValue;
                        porFechaFin = DateTime.MinValue;
                    }
                    else if (!string.IsNullOrWhiteSpace(model.Rfc))
                    {
                        porFechaFin = porFechaFin.AddDays(1);
                    }
                    else if (model.Minutos == -1)
                    {
                        porFechaInicio = porFechaInicio.AddHours(model.Horario);
                        porFechaFin = porFechaFin.AddHours(model.Horario + 1);
                    }
                    else
                    {
                        porFechaInicio = porFechaInicio.AddHours(model.Horario).AddMinutes(model.Minutos);
                        porFechaFin = porFechaFin.AddHours(model.Horario).AddMinutes(model.Minutos+ 15);
                    }
                    
                    

                    
                    int conError = model.conError;
                    string porIdProcesamiento = model.IdProcesamiento;
                    string porRfc = model.Rfc;
                    int porIdPaso = model.IdPaso;


                    using (ClienteServicioConsultaEventos log = new ClienteServicioConsultaEventos())
                    {
                        model.listaBitacora = log.CreateChannel().BuscarEnBitacora(porIdAplicacion, porIdTipoDocumento, porFechaInicio, porFechaFin, conError, porIdProcesamiento, porRfc, porIdPaso);
                    }



                    if (model.listaBitacora.Count == 0)
                    {
                        ViewBag.OnLoadCompleteMessage = Alertas.Informacion("No hay información para este criterio de búsqueda.", "Aceptar");
                    }
                }

            }
            catch (Exception ex)
            {
                ViewBag.OnLoadCompleteMessage = Alertas.Errror(ex.Message, "Aceptar");
            }
            return View("TraductorBitacora", model);
        }

        #endregion


        #region Logs de eventos template
        public ActionResult TemplateLogEvento()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("es-MX");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("es-MX");

            GeneraLogEventosViewModel model = new GeneraLogEventosViewModel();
            try
            {
                if (!validaCookie()) return Redirect("Index");


                model.FechaInicio = DateTime.Now.ToString("dd/MM/yyy");
                model.FechaFin = DateTime.Now.ToString("dd/MM/yyy");

            }
            catch (Exception ex)
            {
                ViewBag.OnLoadCompleteMessage = Alertas.Errror(ex.Message, "Aceptar");
            }
            return View("TemplateLogEvento", model);

        }
        public ActionResult TemplateBuscarEventos(GeneraLogEventosViewModel model)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("es-MX");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("es-MX");

            try
            {
                if (modeloEsValido(model))
                {
                    string porTicket = model.Ticket;
                    string porFechaInicio = Convert.ToDateTime(model.FechaInicio).ToString("MM/dd/yyyy");
                    string porFechaFin = Convert.ToDateTime(model.FechaFin).ToString("MM/dd/yyyy");

                    using (ClienteServicioConsultaEventos log = new ClienteServicioConsultaEventos())
                    {
                        model.listaEventos = log.CreateChannel().BuscarEventos(Aplicacion.Templates.ToString(), porTicket, porFechaInicio, porFechaFin);
                    }

                    if (model.listaEventos.Count == 0)
                    {
                        ViewBag.OnLoadCompleteMessage = Alertas.Informacion("No hay información para este criterio de búsqueda.", "Aceptar");
                    }
                }

            }
            catch (Exception ex)
            {
                ViewBag.OnLoadCompleteMessage = Alertas.Errror(ex.Message, "Aceptar");
            }
            return View("TemplateLogEvento", model);
        }

        #endregion

        private bool validaFechas(string fechaInicio, string fechaFin)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("es-MX");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("es-MX");

            DateTime dtInicio = Convert.ToDateTime(fechaInicio);
            DateTime dtFin = Convert.ToDateTime(fechaFin);
            return dtFin.CompareTo(dtInicio) >= 0;
        }
        private bool IsGuid(string cadena)
        {
            try
            {
                Guid guid = new Guid(cadena);
                return true;
            }
            catch { return false; }
        }
    }
}
