//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Presentacion.Portal.Controllers:Sat.CreditosFiscales.Presentacion.Controllers.Portal.CatalogosController:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sat.CreditosFiscales.Presentacion.Herramientas;
using Sat.CreditosFiscales.Presentacion.Portal.ViewModels;
using Sat.CreditosFiscales.Comunes.Entidades.Catalogos;

namespace Sat.CreditosFiscales.Presentacion.Portal.Controllers
{
    public class ClaveComputoController : Controller
    {
        public ActionResult ClaveComputo()
        {
            return View("../ReglasEquivalencia/ClaveComputo");
        }

        #region Clave Computo

        private List<CatClaveComputo> ListaClaveComputo(string claveComputo = "")
        {
            List<CatClaveComputo> lista = new List<CatClaveComputo>();
            using (ClienteServicioCatalogos servicio = new ClienteServicioCatalogos())
            {
                lista = servicio.CreateChannel().ListaClaveComputo(claveComputo);
            }
            return lista;
        }


        public ActionResult BuscarClaveComputo(string claveComputo = "")
        {
            ClaveComputoViewModel model = new ClaveComputoViewModel();
            try
            {
                model.Lista = ListaClaveComputo(claveComputo);
                if (model.Lista.Count == 0)
                {
                    ViewBag.OnLoadCompleteMessage = Alertas.Informacion("No hay registros para este criterio", "Aceptar");
                }
            }
            catch (Exception ex)
            {
                ViewBag.OnLoadCompleteMessage = Alertas.Errror(ex.Message, "Aceptar");
            }

            var tuple = new Tuple<ClaveComputoViewModel, ClaveComputoCatalogoViewModel>(model, new ClaveComputoCatalogoViewModel());
            return View("../ReglasEquivalencia/ClaveComputo", tuple);
        }

        public ActionResult EditarClaveComputo(string claveComputo)
        {
            ClaveComputoCatalogoViewModel model = new ClaveComputoCatalogoViewModel();
            try
            {
                List<CatClaveComputo> Lista = ListaClaveComputo(claveComputo);
                if (Lista.Count > 0)
                {
                    model.ClaveComputo = Lista[0].ClaveComputo;
                    model.Descripcion = Lista[0].Descripcion;
                }
            }
            catch (Exception ex)
            {
                ViewBag.OnLoadCompleteMessage = Alertas.Errror(ex.Message, "Aceptar");
            }
            var tuple = new Tuple<ClaveComputoViewModel, ClaveComputoCatalogoViewModel>(new ClaveComputoViewModel(), model);
            return View("../ReglasEquivalencia/ClaveComputo", tuple);
        }

        [HttpPost]
        public ActionResult GuardarClaveComputo([Bind(Prefix = "Item2")] ClaveComputoCatalogoViewModel modelCatalogo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (ClienteServicioCatalogos servicio = new ClienteServicioCatalogos())
                    {
                        servicio.CreateChannel().GuardarClaveComputo(
                            (CatClaveComputo.Accion)modelCatalogo.Accion,
                            new CatClaveComputo()
                            {
                                ClaveComputo = modelCatalogo.ClaveComputo.Trim(),
                                Descripcion = modelCatalogo.Descripcion.Trim()
                            });
                    } 
                    
                    ViewBag.OnLoadCompleteMessage = Alertas.Informacion("Clave almacenada exitosamente.", "Aceptar"); 
                    ModelState.Clear();
                }

               
            }
            catch (Exception ex)
            {
                ViewBag.OnLoadCompleteMessage = Alertas.Errror(ex.Message, "Aceptar");
            }

            return View("../ReglasEquivalencia/ClaveComputo");
        }

        public ActionResult EliminarClaveComputo(string claveComputo)
        {
            try
            {
                using (ClienteServicioCatalogos servicio = new ClienteServicioCatalogos())
                {
                    servicio.CreateChannel().EliminarClaveComputo(claveComputo);
                }
                ViewBag.OnLoadCompleteMessage = Alertas.Informacion("Clave eliminada exitosamente.", "Aceptar");
            }
            catch (Exception ex)
            {
                ViewBag.OnLoadCompleteMessage = Alertas.Errror(ex.Message, "Aceptar");
            }
            return View("../ReglasEquivalencia/ClaveComputo");
        }

        #endregion

    }
}
