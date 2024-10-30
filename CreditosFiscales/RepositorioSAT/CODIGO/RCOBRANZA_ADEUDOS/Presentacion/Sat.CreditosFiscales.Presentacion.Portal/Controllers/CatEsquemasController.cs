//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Presentacion.Portal.Controllers:Sat.CreditosFiscales.Presentacion.Controllers.Portal.CatEsquemasController:1:27/08/2013[Assembly:1.0:27/08/2013])

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
    public class CatEsquemasController : Controller
    {


        public ActionResult CatEsquemas()
        {
            return View("../ReglasEquivalencia/CatEsquemas");
        }

        private List<CatEsquemas> ListaCatEsquemas(string idEsquema, string descripcion = "")
        {
            List<CatEsquemas> lista = new List<CatEsquemas>();
            using (ClienteServicioCatalogos servicio = new ClienteServicioCatalogos())
            {
                lista = servicio.CreateChannel().ListaCatEsquemas(idEsquema, descripcion);
            }
            return lista;
        }


        public ActionResult Buscar(string IdEsquema = "", string Descripcion = "")
        {
            CatEsquemasViewModel model = new CatEsquemasViewModel();
            try
            {
                bool esValido = true;
                if (!string.IsNullOrEmpty(IdEsquema))
                {
                    esValido = IsValidID(IdEsquema);
                    if (!esValido)
                        ModelState.AddModelError("IdEsquema", "(*) Formato incorrecto");
                }

                if (esValido)
                {
                    model.Lista = ListaCatEsquemas(IdEsquema, Descripcion);
                    if (model.Lista.Count == 0)
                    {
                        ViewBag.OnLoadCompleteMessage = Alertas.Informacion("No hay registros para este criterio", "Aceptar");
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.OnLoadCompleteMessage = Alertas.Errror(ex.Message, "Aceptar");
            }

            var tuple = new Tuple<CatEsquemasViewModel, CatEsquemasCatalogoViewModel>(model, new CatEsquemasCatalogoViewModel());
            return View("../ReglasEquivalencia/CatEsquemas", tuple);
        }

        public ActionResult Editar(string idEsquema)
        {
            CatEsquemasCatalogoViewModel model = new CatEsquemasCatalogoViewModel();
            try
            {
                List<CatEsquemas> Lista = ListaCatEsquemas(idEsquema);
                if (Lista.Count > 0)
                {
                    model.Descripcion = Lista[0].Descripcion;
                    model.TargetNamespace = Lista[0].TargetNamespace;
                    model.IdEsquema = Lista[0].IdEsquema.Value;
                    model.Esquema = Lista[0].Esquema;
                }
            }
            catch (Exception ex)
            {
                ViewBag.OnLoadCompleteMessage = Alertas.Errror(ex.Message, "Aceptar");
            }
            var tuple = new Tuple<CatEsquemasViewModel, CatEsquemasCatalogoViewModel>(new CatEsquemasViewModel(), model);
            return View("../ReglasEquivalencia/CatEsquemas", tuple);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Guardar([Bind(Prefix = "Item2")] CatEsquemasCatalogoViewModel modelCatalogo)
        {
            try
            {
                if (ValidaModelo(modelCatalogo))
                {
                    using (ClienteServicioCatalogos servicio = new ClienteServicioCatalogos())
                    {
                        servicio.CreateChannel().GuardarEsquema(
                            (CatEsquemas.Accion)modelCatalogo.Accion,
                            new CatEsquemas()
                            {
                                Descripcion = modelCatalogo.Descripcion,
                                TargetNamespace = modelCatalogo.TargetNamespace,
                                IdEsquema = (CatEsquemas.Accion)modelCatalogo.Accion == Comunes.Entidades.Catalogos.CatEsquemas.Accion.Guardar ? Convert.ToInt16(0) : modelCatalogo.IdEsquema,
                                Esquema = modelCatalogo.Esquema
                            });
                    }

                    ViewBag.OnLoadCompleteMessage = Alertas.Informacion("Esquema almacenada exitosamente.", "Aceptar");
                    ModelState.Clear();
                }
                else
                {

                    var tuple = new Tuple<CatEsquemasViewModel, CatEsquemasCatalogoViewModel>(new CatEsquemasViewModel(), modelCatalogo);
                    return View("../ReglasEquivalencia/CatEsquemas", tuple);
                }
            }
            catch (Exception ex)
            {
                ViewBag.OnLoadCompleteMessage = Alertas.Errror(ex.Message, "Aceptar");
            }
            return View("../ReglasEquivalencia/CatEsquemas");

        }

        public ActionResult Eliminar(Int16 idEsquema)
        {
            try
            {
                using (ClienteServicioCatalogos servicio = new ClienteServicioCatalogos())
                {
                    servicio.CreateChannel().EliminarEsquema(idEsquema);
                }
                ViewBag.OnLoadCompleteMessage = Alertas.Informacion("Esquema eliminada exitosamente.", "Aceptar");
            }
            catch (Exception ex)
            {
                ViewBag.OnLoadCompleteMessage = Alertas.Errror(ex.Message, "Aceptar");
            }
            return View("../ReglasEquivalencia/CatEsquemas");
        }


        private bool ValidaModelo(CatEsquemasCatalogoViewModel model)
        {
            bool EsValido = ModelState.IsValid;
            if (EsValido)
            {
                Comunes.Entidades.Catalogos.CatEsquemas.Accion accion = (CatEsquemas.Accion)model.Accion;
                if (accion == Comunes.Entidades.Catalogos.CatEsquemas.Accion.Actualizar)
                {
                    if (model.IdEsquema == 0)
                    {
                        ModelState.AddModelError("IdEsquema", "(*) Requerido.");
                        EsValido = false;
                    }
                }

                try
                {
                    var doc = new System.Xml.XmlDocument();
                    doc.LoadXml(model.Esquema);
                }
                catch
                {
                    ModelState.AddModelError("Esquema", "(*) No es un xml válido.");
                    EsValido = false;
                }


            }

            return EsValido;
        }

        private bool IsValidID(string cadena)
        {
            try
            {
                Int16 id;
                if (Int16.TryParse(cadena,out id))
                {
                    if (id > 0)
                        return true;
                    else 
                        return false;
                }
                else
                    return false;

            }
            catch { return false; }
        }
    }
}
