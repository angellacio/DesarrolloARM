
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
using Sat.CreditosFiscales.Comunes.Entidades.Servicios.Traductor;

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
                        porFechaFin = porFechaFin.AddHours(model.Horario).AddMinutes(model.Minutos + 15);
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

        public ActionResult TraductorMonitorPagos()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("es-MX");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("es-MX");

            GeneraTraductorMonitorPagosViewMode model = new GeneraTraductorMonitorPagosViewMode();
            try
            {
                model.MonitorPagoDetalle = new TraductorMonitorPagoDetalle();
                if (!validaCookie()) return Redirect("Index");

                model.MonitorPagoDetalle.FechaInicio = DateTime.Now.ToString("dd/MM/yyy");
                model.MonitorPagoDetalle.FechaFin = DateTime.Now.ToString("dd/MM/yyy");

                model.MonitorArchivoZIP.FechaInicio = DateTime.Now.ToString("dd/MM/yyy");
                model.MonitorArchivoZIP.FechaFin = DateTime.Now.ToString("dd/MM/yyy");

                model.MonitorTareaProgramada.FechaInicio = DateTime.Now.ToString("dd/MM/yyy");
                model.MonitorTareaProgramada.FechaFin = DateTime.Now.ToString("dd/MM/yyy");

                model.listaEstatus = MetodosComunesPortal.ObtieneCatalogoEstatus();
                model.listaBanco = MetodosComunesPortal.ObtieneCatalogoBancos();
            }
            catch (Exception ex)
            {
                ViewBag.OnLoadCompleteMessage = Alertas.Errror(ex.Message, "Aceptar");
            }
            return View("TraductorMonitorPagos", model);
        }
        public ActionResult TraductorMonitorPagos_DetPagos_Buscar(GeneraTraductorMonitorPagosViewMode model)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("es-MX");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("es-MX");
            int porIdMonitor = -1, porIdTipoPago = -1, porIdEstatus = -1, porIdBanco = -1;
            DateTime? porFechaInicio = null, porFechaFin = null;
            string porLineaCaptura = string.Empty, porArchivoZIP = string.Empty;
            Boolean bolBuscar = true;
            try
            {
                porIdMonitor = model.IdMonitor;

                model.listaEstatus = MetodosComunesPortal.ObtieneCatalogoEstatus();
                model.listaBanco = MetodosComunesPortal.ObtieneCatalogoBancos();
                if (model.IdMonitor == 1)
                {
                    if (string.IsNullOrEmpty(model.MonitorPagoDetalle.LineaCaptura))
                    {
                        if (!validaFechasMonitorPagos(model.MonitorPagoDetalle.FechaInicio, model.MonitorPagoDetalle.FechaFin, true, 14, "MonitorPagoDetalle.FechaInicio", "MonitorPagoDetalle.FechaFin"))
                        {
                            bolBuscar = false;
                        }
                    }
                    else
                    {
                        if (model.MonitorPagoDetalle.LineaCaptura.Length < 20)
                        {
                            ModelState.AddModelError("MonitorPagoDetalle.LineaCaptura", "Favor de especificar una línea de captura correcta.");
                            bolBuscar = false;
                        }
                    }
                    if (bolBuscar)
                    {
                        if (model.MonitorPagoDetalle.IdTipoPago != -1) porIdTipoPago = model.MonitorPagoDetalle.IdTipoPago;
                        if (model.MonitorPagoDetalle.IdEstatus != -1) porIdEstatus = model.MonitorPagoDetalle.IdEstatus;
                        if (model.MonitorPagoDetalle.IdBanco != -1) porIdBanco = model.MonitorPagoDetalle.IdBanco;
                        if (!string.IsNullOrEmpty(model.MonitorPagoDetalle.FechaInicio)) porFechaInicio = Convert.ToDateTime($"{model.MonitorPagoDetalle.FechaInicio} 00:00:00");
                        if (!string.IsNullOrEmpty(model.MonitorPagoDetalle.FechaFin)) porFechaFin = Convert.ToDateTime($"{model.MonitorPagoDetalle.FechaFin} 23:59:59");
                        if (!string.IsNullOrEmpty(model.MonitorPagoDetalle.LineaCaptura)) porLineaCaptura = model.MonitorPagoDetalle.LineaCaptura.ToString().Trim();

                        if (!string.IsNullOrEmpty(model.MonitorPagoDetalle.LineaCaptura))
                        {
                            porIdTipoPago = -1;
                            porIdEstatus = -1;
                            porIdBanco = -1;
                            porFechaInicio = null;
                            porFechaFin = null;
                        }
                        using (ClienteServicioConsultaEventos log = new ClienteServicioConsultaEventos())
                        {
                            model.MonitorPagoDetalle.DatosBusqueda = log.CreateChannel().BuscarEnMonitorPagoDetalle(porIdTipoPago, porIdEstatus, porIdBanco, porFechaInicio, porFechaFin, porLineaCaptura);
                        }

                        if (model.MonitorPagoDetalle.DatosBusqueda.Count == 0)
                        {
                            ViewBag.OnLoadCompleteMessage = Alertas.Informacion("No hay información para este criterio de búsqueda.", "Aceptar");
                        }
                    }
                }
                else if (model.IdMonitor == 2)
                {
                    if (string.IsNullOrEmpty(model.MonitorArchivoZIP.ArchivoZIP))
                    {
                        if (!validaFechasMonitorPagos(model.MonitorArchivoZIP.FechaInicio, model.MonitorArchivoZIP.FechaFin, true, 14, "MonitorArchivoZIP.FechaInicio", "MonitorArchivoZIP.FechaFin"))
                        {
                            bolBuscar = false;
                        }
                    }

                    if (bolBuscar)
                    {
                        if (model.MonitorArchivoZIP.IdTipoPago != -1) porIdTipoPago = model.MonitorArchivoZIP.IdTipoPago;
                        if (!string.IsNullOrEmpty(model.MonitorArchivoZIP.ArchivoZIP)) porArchivoZIP = model.MonitorArchivoZIP.ArchivoZIP.ToString().Trim();
                        if (!string.IsNullOrEmpty(model.MonitorArchivoZIP.FechaInicio)) porFechaInicio = Convert.ToDateTime($"{model.MonitorArchivoZIP.FechaInicio} 00:00:00");
                        if (!string.IsNullOrEmpty(model.MonitorArchivoZIP.FechaFin)) porFechaFin = Convert.ToDateTime($"{model.MonitorArchivoZIP.FechaFin} 23:59:59");

                        using (ClienteServicioConsultaEventos log = new ClienteServicioConsultaEventos())
                        {
                            model.MonitorArchivoZIP.DatosBusqueda = log.CreateChannel().BuscarEnMonitorArchivoZIP(porIdTipoPago, porArchivoZIP, porFechaInicio, porFechaFin);
                        }

                        if (model.MonitorPagoDetalle.DatosBusqueda.Count == 0)
                        {
                            ViewBag.OnLoadCompleteMessage = Alertas.Informacion("No hay información para este criterio de búsqueda.", "Aceptar");
                        }
                    }
                }
                else if (model.IdMonitor == 3)
                {
                    if (!validaFechasMonitorPagos(model.MonitorTareaProgramada.FechaInicio, model.MonitorTareaProgramada.FechaFin, true, 14, "MonitorTareaProgramada.FechaInicio", "MonitorTareaProgramada.FechaFin"))
                    {
                        bolBuscar = false;
                    }
                    if (bolBuscar)
                    {
                        if (model.MonitorTareaProgramada.IdTipoPago != -1) porIdTipoPago = model.MonitorTareaProgramada.IdTipoPago;
                        if (model.MonitorTareaProgramada.IdEstatus != -1) porIdEstatus = model.MonitorTareaProgramada.IdEstatus;
                        if (!string.IsNullOrEmpty(model.MonitorTareaProgramada.FechaInicio)) porFechaInicio = Convert.ToDateTime($"{model.MonitorTareaProgramada.FechaInicio} 00:00:00");
                        if (!string.IsNullOrEmpty(model.MonitorTareaProgramada.FechaFin)) porFechaFin = Convert.ToDateTime($"{model.MonitorTareaProgramada.FechaFin} 23:59:59");

                        using (ClienteServicioConsultaEventos log = new ClienteServicioConsultaEventos())
                        {
                            model.MonitorTareaProgramada.DatosBusqueda = log.CreateChannel().BuscarEnMonitorTareaProgramada(porIdTipoPago, porIdEstatus, porFechaInicio, porFechaFin);
                        }

                        if (model.MonitorTareaProgramada.DatosBusqueda.Count == 0)
                        {
                            ViewBag.OnLoadCompleteMessage = Alertas.Informacion("No hay información para este criterio de búsqueda.", "Aceptar");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.OnLoadCompleteMessage = Alertas.Errror(ex.Message, "Aceptar");
            }


            return View("TraductorMonitorPagos", model);
        }
        public ActionResult TraductorMonitorPagos_DetPagos_Limpiar(GeneraTraductorMonitorPagosViewMode model)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("es-MX");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("es-MX");

            GeneraTraductorMonitorPagosViewMode modelLimpio = new GeneraTraductorMonitorPagosViewMode();
            try
            {
                modelLimpio.IdMonitor = model.IdMonitor;

                modelLimpio.MonitorPagoDetalle.FechaInicio = DateTime.Now.ToString("dd/MM/yyy");
                modelLimpio.MonitorPagoDetalle.FechaFin = DateTime.Now.ToString("dd/MM/yyy");

                modelLimpio.MonitorArchivoZIP.FechaInicio = DateTime.Now.ToString("dd/MM/yyy");
                modelLimpio.MonitorArchivoZIP.FechaFin = DateTime.Now.ToString("dd/MM/yyy");

                modelLimpio.MonitorTareaProgramada.FechaInicio = DateTime.Now.ToString("dd/MM/yyy");
                modelLimpio.MonitorTareaProgramada.FechaFin = DateTime.Now.ToString("dd/MM/yyy");

                modelLimpio.listaEstatus = MetodosComunesPortal.ObtieneCatalogoEstatus();
                modelLimpio.listaBanco = MetodosComunesPortal.ObtieneCatalogoBancos();
            }
            catch (Exception ex)
            {
                ViewBag.OnLoadCompleteMessage = Alertas.Errror(ex.Message, "Aceptar");
            }
            return View("TraductorMonitorPagos", modelLimpio);
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
        private Boolean validaFechasMonitorPagos(string fechaInicio, string fechaFin, Boolean ValidaTiempo, int nValidaTiempo, string ObjectFInicio, string ObjectFFin)
        {
            Boolean result = true;
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("es-MX");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("es-MX");
            DateTime dtInicio = DateTime.Now, dtFin = DateTime.Now;
            try
            {
                if (string.IsNullOrEmpty(fechaInicio) || fechaInicio.Trim().Length < 6) throw new ApplicationException("(*) La fecha inicio no puede ser vacía o está mal conformada.");

                dtInicio = Convert.ToDateTime(fechaInicio);
            }
            catch (ApplicationException ex)
            {
                ModelState.AddModelError(ObjectFInicio, ex.Message);
                result = false;
            }
            catch
            {
                ModelState.AddModelError(ObjectFInicio, "(*) La fecha inicio está mal conformada.");
                result = false;
            }
            try
            {
                if (string.IsNullOrEmpty(fechaFin) || fechaFin.Trim().Length < 6) throw new ApplicationException("(*) La fecha fin no puede ser vacía o está mal conformada.");

                dtFin = Convert.ToDateTime(fechaFin);
            }
            catch (ApplicationException ex)
            {
                ModelState.AddModelError(ObjectFFin, ex.Message);
                result = false;
            }
            catch
            {
                ModelState.AddModelError(ObjectFFin, "(*) La fecha fin está mal conformada.");
                result = false;
            }
            if (!(dtFin.CompareTo(dtInicio) >= 0))
            {
                ModelState.AddModelError(ObjectFInicio, "(*) La fecha inicio no puede ser mayor a la fin.");
                result = false;
            }
            else if (ValidaTiempo && (dtFin - dtInicio).Days > nValidaTiempo)
            {
                ModelState.AddModelError(ObjectFFin, "(*) La diferencia de fecha inicio y fecha fin no puede ser mayor a 2 semanas.");
                result = false;
            }

            return result;
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
