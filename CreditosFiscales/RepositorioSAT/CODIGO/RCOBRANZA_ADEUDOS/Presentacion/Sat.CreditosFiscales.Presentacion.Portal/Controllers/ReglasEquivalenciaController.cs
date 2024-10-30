using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sat.CreditosFiscales.Comunes.Entidades.Catalogos;
using Sat.CreditosFiscales.Presentacion.Herramientas;
using Sat.CreditosFiscales.Presentacion.Portal.ViewModels;

namespace Sat.CreditosFiscales.Presentacion.Portal.Controllers
{
    public class ReglasEquivalenciaController : Controller
    {
        private string nombreCookie = "LCReglasEquivalencia";

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(GeneraAccesoLogViewModel model)
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

                        return RedirectToAction("Consulta");
                    }
                }

            }
            catch (Exception ex)
            {
                ViewBag.OnLoadCompleteMessage = Alertas.Errror(ex.Message, "Aceptar");
            }

            return View();
        }

        public ActionResult Consulta()
        {
            if (!validaCookie())
            {
                return RedirectToAction("Index");
            }

            ReglasEquivalenciaViewModel model = new ReglasEquivalenciaViewModel();
            CargaCatalogos(model);
            return View(model);

        }

        [HttpPost]
        public ActionResult Consulta(ReglasEquivalenciaViewModel model)
        {
            ModelState.Clear();

            switch (model.Accion)
            {
                ///Consulta
                case 1:
                    model.Reglas = this.ConsultaReglas(model.Parametros);
                    break;
                ////Agrega
                case 2:
                    AgregaRegla(model);
                    model.Reglas = this.ConsultaReglas(model.Parametros);
                    break;
                ////Actualiza
                case 3:
                    ActualizaRegla(model);
                    model.Reglas = this.ConsultaReglas(model.Parametros);
                    break;
            }
            CargaCatalogos(model);
            return View(model);
        }

        public ActionResult Conceptos()
        {
            if (!validaCookie())
            {
                return RedirectToAction("Index");
            }
            ConceptoEquivalenciaViewModel model = new ConceptoEquivalenciaViewModel();
            CargaCatalogosParaConceptos(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Conceptos(ConceptoEquivalenciaViewModel model)
        {
            ModelState.Clear();

            switch (model.Accion)
            {
                ///Consulta
                case 1:
                    model.Conceptos = this.ConsultaConceptos(model.Parametros);
                    break;
                ////Agrega
                case 2:
                    AgregaConcepto(model);
                    model.Conceptos = this.ConsultaConceptos(model.Parametros);
                    break;
                ////Actualiza
                case 3:
                    ActualizaConcepto(model);
                    model.Conceptos = this.ConsultaConceptos(model.Parametros);
                    break;
            }
            CargaCatalogosParaConceptos(model);
            return View(model);
        }

        private bool validaCookie()
        {
            HttpCookie cookie = Request.Cookies[nombreCookie];
            return (cookie != null);
        }

        private void AgregaRegla(ReglasEquivalenciaViewModel model)
        {
            try
            {
                using (ClienteServicioReglasEquivalencia cliente = new ClienteServicioReglasEquivalencia())
                {
                    cliente.CreateChannel().AgregaRegla(model.Regla);
                }
                model.Parametros.IdAplicacion = model.Regla.IdAplicacion;
                model.Parametros.IdTipoDocumento = model.Regla.IdTipoDocumento;
                model.Parametros.IdTipoObjeto = model.Regla.IdTipoObjeto;
                model.Parametros.ValorObjeto = model.Regla.ValorObjeto;
            }
            catch (Exception ex)
            {
                model.Regla.Excepcion = ex.Message;
            }
        }

        private void AgregaConcepto(ConceptoEquivalenciaViewModel model)
        {
            using (ClienteServicioReglasEquivalencia cliente = new ClienteServicioReglasEquivalencia())
            {
                cliente.CreateChannel().AgregaConcepto(model.Concepto);
            }
            model.Parametros.IdAplicacion = model.Concepto.IdAplicacion;
            model.Parametros.ConceptoDyP = model.Concepto.ConceptoDyP;
            model.Parametros.ConceptoOrigen = model.Concepto.ConceptoOrigen;
            
        }

        private void ActualizaRegla(ReglasEquivalenciaViewModel model)
        {
            try
            {
                using (ClienteServicioReglasEquivalencia cliente = new ClienteServicioReglasEquivalencia())
                {
                    cliente.CreateChannel().ActualizaRegla(model.Regla);
                }
                model.Parametros.IdAplicacion = model.Regla.IdAplicacion;
                model.Parametros.IdTipoDocumento = model.Regla.IdTipoDocumento;
                model.Parametros.IdTipoObjeto = model.Regla.IdTipoObjeto;
                model.Parametros.ValorObjeto = model.Regla.ValorObjeto;
            }
            catch (Exception ex)
            {
                model.Regla.Excepcion = ex.Message;
            }
            
        }

        private void ActualizaConcepto(ConceptoEquivalenciaViewModel model)
        {
            using (ClienteServicioReglasEquivalencia cliente = new ClienteServicioReglasEquivalencia())
            {
                cliente.CreateChannel().ActualizaConcepto(model.Concepto,model.ConceptoOriginal);
            }
            model.Parametros.IdAplicacion = model.Concepto.IdAplicacion;
            model.Parametros.ConceptoDyP = model.Concepto.ConceptoDyP;
            model.Parametros.ConceptoOrigen = model.Concepto.ConceptoOrigen;

        }

        private static void CargaCatalogos(ReglasEquivalenciaViewModel model)
        {
            using (ClienteServicioReglasEquivalencia cliente = new ClienteServicioReglasEquivalencia())
            {
                var clienteActivo = cliente.CreateChannel();
                model.CatAplicacion = clienteActivo.ObtieneCatAplicacion();
                model.CatTipoDocumento = clienteActivo.ObtieneCatTipoDocumento();
                model.CatTipoObjeto = clienteActivo.ObtieneCatTipoObjeto();
            }
        }

        private static void CargaCatalogosParaConceptos(ConceptoEquivalenciaViewModel model)
        {
            using (ClienteServicioReglasEquivalencia cliente = new ClienteServicioReglasEquivalencia())
            {
                var clienteActivo = cliente.CreateChannel();
                model.CatAplicacion = clienteActivo.ObtieneCatAplicacion();
                model.CatConceptoDyP = clienteActivo.ObtieneCatConceptoDyP();                
            }
        }

        private List<ReglaEquivalencia> ConsultaReglas(ReglaEquivalenciaParametroViewModel parametros)
        {
            using (ClienteServicioReglasEquivalencia cliente = new ClienteServicioReglasEquivalencia())
            {
                return cliente.CreateChannel().ObtieneReglas(parametros.IdAplicacion, parametros.IdTipoDocumento, parametros.IdTipoObjeto, parametros.ValorObjeto);
            }
        }

        private List<ConceptoEquivalencia> ConsultaConceptos(ConceptoEquivalenciaParametroViewModel parametros)
        {
            using (ClienteServicioReglasEquivalencia cliente = new ClienteServicioReglasEquivalencia())
            {
                return cliente.CreateChannel().ObtieneConceptos(parametros.IdAplicacion, parametros.ConceptoOrigen, parametros.ConceptoDyP);
            }
        }
    }
}
