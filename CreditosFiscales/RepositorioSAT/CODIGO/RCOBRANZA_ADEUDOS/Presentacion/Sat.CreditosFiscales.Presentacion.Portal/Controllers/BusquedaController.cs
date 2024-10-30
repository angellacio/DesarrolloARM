
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Presentacion.Controllers:Sat.CreditosFiscales.Presentacion.Controllers.BusquedaController:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Runtime.Serialization;
using LN = Sat.CreditosFiscales.Procesamiento.LogicaNegocio;
using Sat.CreditosFiscales.Presentacion.Portal.ViewModels;
using Sat.CreditosFiscales.Presentacion.Herramientas;
using Sat.CreditosFiscales.Comunes.Entidades.Servicios;
using Sat.CreditosFiscales.Comunes.Entidades.GeneraFormato;

namespace Sat.CreditosFiscales.Presentacion.Portal.Controllers
{
    public class BusquedaController : Controller
    {

        #region Index
        public ActionResult Index()
        {

            GenerarBusquedaViewModel model = new GenerarBusquedaViewModel();
            model.listaALR = LN.Catalogos.Catalogo.obtenerCatalogoALR();
            model.listaAutoridad = LN.Catalogos.Catalogo.obtenerCatalogoAutoridad();
            model.FechaDocumento = DateTime.Now.ToString("dd/MM/yyy");
            return View(model);
        }



        [HttpPost]
        public ActionResult Index(GenerarBusquedaViewModel model)
        {
            if (modeloEsValido(model))
            {
                List<DocumentoDeterminante> listaDocumento = new List<DocumentoDeterminante>();
                listaDocumento.Add(new DocumentoDeterminante());

                //List<DocumentoDeterminante> listaDocumento = LN.Servicios.ProxyManagerArca.ObtenerInformacionContribuyentePuro(
                //     model.Rfc, model.DocumentoDeterminante, model.IdAutoridad, model.IdALR, Convert.ToDateTime(model.FechaDocumento)
                //    );

                if (listaDocumento.Count > 0)
                {
                    GenerarDocumentosDeterminanteViewModel modelDocumentos = new GenerarDocumentosDeterminanteViewModel();
                    modelDocumentos.listaDocumentos = listaDocumento;

                    return View("GenerarDocumento", modelDocumentos);
                }
                else
                {
                    ViewBag.OnLoadCompleteMessage = Alertas.Errror("No existen documentos para la información registrada.", "Aceptar");
                }
            }


            model.listaALR = LN.Catalogos.Catalogo.obtenerCatalogoALR();
            model.listaAutoridad = LN.Catalogos.Catalogo.obtenerCatalogoAutoridad();
            return View(model);

        }
        private bool modeloEsValido(GenerarBusquedaViewModel model)
        {
            bool esValido = false;
            esValido = ModelState.IsValid;
            if (!ValidaFecha())
            {
                ModelState.AddModelError("FechaDocumento", string.Empty);
                esValido = false;
            }
            return esValido;
        }
        private bool ValidaFecha()
        {
            return true;
        }
        #endregion

        #region Generar Documento
        //public void GuardarDocumento(GenerarBusquedaViewModel model)
        //{
        //    int idSolicitud = 0;
        //    new LN.GeneraFormato.GeneraFormato().GuardaSolicitud(doc);
        //    RedirectToAction("LineaCaptura", new { @idSolicitud = idSolicitud });
        //}

        #endregion
    }
}
