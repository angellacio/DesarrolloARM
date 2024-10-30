//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Presentacion.Portal.Controllers:Sat.CreditosFiscales.Presentacion.Controllers.Portal.CatReglasController:1:27/08/2013[Assembly:1.0:27/08/2013])

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
    public class CatReglasController : Controller
    {


        public ActionResult CatReglas()
        {
            return View("../ReglasEquivalencia/CatReglas");
        }

        private List<CatReglas> ListaCatReglas(string idRegla, string descripcion = "")
        {
            List<CatReglas> lista = new List<CatReglas>();
            using (ClienteServicioCatalogos servicio = new ClienteServicioCatalogos())
            {
                lista = servicio.CreateChannel().ListaCatReglas(idRegla, descripcion);
            }
            return lista;
        }


        public ActionResult Buscar(string IdRegla = "", string Descripcion = "")
        {
            CatReglasViewModel model = new CatReglasViewModel();
            try
            {
                bool esValido = true;
                if (!string.IsNullOrEmpty(IdRegla))
                { 
                    esValido =  IsGuid(IdRegla);
                    if(!esValido)
                        ModelState.AddModelError("IdRegla", "(*) Formato incorrecto");
                }

                if (esValido)
                {
                    model.Lista = ListaCatReglas(IdRegla, Descripcion);
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

            var tuple = new Tuple<CatReglasViewModel, CatReglasCatalogoViewModel>(model, new CatReglasCatalogoViewModel());
            return View("../ReglasEquivalencia/CatReglas", tuple);
        }

        public ActionResult Editar(string idRegla)
        {
            CatReglasCatalogoViewModel model = new CatReglasCatalogoViewModel();
            try
            {
                List<CatReglas> Lista = ListaCatReglas(idRegla);
                if (Lista.Count > 0)
                {
                    model.Descripcion = Lista[0].Descripcion;
                    model.EsValidacion = Convert.ToBoolean(Lista[0].EsValidacion);
                    model.IdRegla = new Guid(Lista[0].IdRegla.ToString());
                    model.Regla = Lista[0].Regla;
                }
            }
            catch (Exception ex)
            {
                ViewBag.OnLoadCompleteMessage = Alertas.Errror(ex.Message, "Aceptar");
            }
            var tuple = new Tuple<CatReglasViewModel, CatReglasCatalogoViewModel>(new CatReglasViewModel(), model);
            return View("../ReglasEquivalencia/CatReglas", tuple);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Guardar([Bind(Prefix = "Item2")] CatReglasCatalogoViewModel modelCatalogo)
        {
            try
            {
                if (ValidaModelo(modelCatalogo))
                {
                    using (ClienteServicioCatalogos servicio = new ClienteServicioCatalogos())
                    {
                        servicio.CreateChannel().GuardarRegla(
                            (CatReglas.Accion)modelCatalogo.Accion,
                            new CatReglas()
                            {
                                Descripcion = modelCatalogo.Descripcion,
                                EsValidacion = modelCatalogo.EsValidacion,
                                IdRegla = (CatReglas.Accion)modelCatalogo.Accion == Comunes.Entidades.Catalogos.CatReglas.Accion.Guardar ? Guid.NewGuid() : modelCatalogo.IdRegla,
                                Regla = modelCatalogo.Regla
                            });
                    }

                    ViewBag.OnLoadCompleteMessage = Alertas.Informacion("Regla almacenada exitosamente.", "Aceptar");
                    ModelState.Clear();
                }
                else
                {
                    
                    var tuple = new Tuple<CatReglasViewModel, CatReglasCatalogoViewModel>(new CatReglasViewModel(), modelCatalogo);
                    return View("../ReglasEquivalencia/CatReglas", tuple);
                }
            }
            catch (Exception ex)
            {
                ViewBag.OnLoadCompleteMessage = Alertas.Errror(ex.Message, "Aceptar");
            }
            return View("../ReglasEquivalencia/CatReglas");

        }

        public ActionResult Eliminar(Guid idRegla)
        {
            try
            {
                using (ClienteServicioCatalogos servicio = new ClienteServicioCatalogos())
                {
                    servicio.CreateChannel().EliminarRegla(idRegla);
                }
                ViewBag.OnLoadCompleteMessage = Alertas.Informacion("Regla eliminada exitosamente.", "Aceptar");
            }
            catch (Exception ex)
            {
                ViewBag.OnLoadCompleteMessage = Alertas.Errror(ex.Message, "Aceptar");
            }
            return View("../ReglasEquivalencia/CatReglas");
        }


        private bool ValidaModelo(CatReglasCatalogoViewModel model)
        {
            bool EsValido = ModelState.IsValid;
            if (EsValido)
            {
                Comunes.Entidades.Catalogos.CatReglas.Accion accion = (CatReglas.Accion)model.Accion;
                if (accion == Comunes.Entidades.Catalogos.CatReglas.Accion.Actualizar)
                {
                    if (model.IdRegla == Guid.Empty)
                    {
                        ModelState.AddModelError("IdRegla", "(*) Requerido.");
                        EsValido = false;
                    }
                }

                try
                {
                    var doc = new System.Xml.XmlDocument();
                    doc.LoadXml(model.Regla);
                }
                catch
                {
                    ModelState.AddModelError("Regla", "(*) No es un xml válido.");
                    EsValido = false;
                }


            }

            return EsValido;
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
