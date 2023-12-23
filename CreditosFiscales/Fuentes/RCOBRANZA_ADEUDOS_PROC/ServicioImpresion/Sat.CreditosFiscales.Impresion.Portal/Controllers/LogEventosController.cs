using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sat.CreditosFiscales.Impresion.Herramientas;
using Sat.CreditosFiscales.Impresion.LogicaNegocio.AccesoLogEventos;
using Sat.CreditosFiscales.Impresion.Portal.ViewModels;

namespace Sat.CreditosFiscales.Impresion.Portal.Controllers
{
    public class LogEventosController : Controller
    {
        private string nombreCookie = "LCLogEventosImpresion";

        #region Valida Acceso
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

                    if (!LogEventos.VerificaAcceso(model.Usuario, model.Contraseña))
                    {
                        ViewBag.OnLoadCompleteMessage = Alertas.Alerta("Acceso no autorizado", "Aceptar");
                    }
                    else
                    {
                        HttpCookie cookie = new HttpCookie(nombreCookie, model.Contraseña);
                        cookie.Expires = DateTime.Now.AddMinutes(20);
                        Response.Cookies.Add(cookie);

                        return RedirectToAction("LogEvento", "LogEventos");
                    }
                }

            }
            catch (Exception ex)
            {
                ViewBag.OnLoadCompleteMessage = Alertas.Errror(ex.Message, "Aceptar");
            }

            return View("Index");
        }
        #endregion
        #region Logs de eventos
        public ActionResult LogEvento()
        {
            GeneraLogEventosViewModel model = new GeneraLogEventosViewModel();
            try
            {
                if (!validaCookie()) return Redirect("Index");


                model.FechaInicio = DateTime.Now.ToString("dd/MM/yyy");
                model.FechaFin = DateTime.Now.ToString("dd/MM/yyy");

                string porFechaInicio = Convert.ToDateTime(model.FechaInicio).ToString("MM/dd/yyyy");
                string porFechaFin = Convert.ToDateTime(model.FechaFin).ToString("MM/dd/yyyy");
                model.listaEventos = LogEventos.BuscarEventos(string.Empty, porFechaInicio, porFechaFin);
            }
            catch (Exception ex)
            {
                ViewBag.OnLoadCompleteMessage = Alertas.Errror(ex.Message, "Aceptar");
            }
            return View("LogEvento", model);

        }
        public ActionResult BuscarEventos(GeneraLogEventosViewModel model)
        {
            try
            {
                if (modeloEsValido(model))
                {
                    string porTicket = model.Ticket;
                    string porFechaInicio = Convert.ToDateTime(model.FechaInicio).ToString("MM/dd/yyyy");
                    string porFechaFin = Convert.ToDateTime(model.FechaFin).ToString("MM/dd/yyyy");
                    model.listaEventos = LogEventos.BuscarEventos(porTicket, porFechaInicio, porFechaFin);

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
            return View("LogEvento", model);
        }
        private bool modeloEsValido(GeneraLogEventosViewModel model)
        {
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
        private bool IsGuid(string cadena)
        {
            try
            {
                Guid guid = new Guid(cadena);
                return true;
            }
            catch { return false; }
        }
        private bool validaFechas(string fechaInicio, string fechaFin)
        {
            DateTime dtInicio = Convert.ToDateTime(fechaInicio);
            DateTime dtFin = Convert.ToDateTime(fechaFin);
            return dtFin.CompareTo(dtInicio) >= 0;
        }
        #endregion


    }
}
