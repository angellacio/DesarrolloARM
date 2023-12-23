
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Presentacion.Controllers:Sat.CreditosFiscales.Presentacion.Controllers.LineaCapturaController:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sat.CreditosFiscales.Presentacion.Portal.ViewModels;
using Sat.CreditosFiscales.Comunes.Entidades.Servicios;
using Sat.CreditosFiscales.Comunes.Entidades.AccesoLogEventos;
using Sat.CreditosFiscales.Presentacion.Herramientas;
using Sat.CreditosFiscales.Comunes.Entidades.CodigosError;
using Sat.CreditosFiscales.Comunes.Entidades;
using System.IO;
using Sat.CreditosFiscales.Comunes.Entidades.GeneraFormato;

namespace Sat.CreditosFiscales.Presentacion.Portal.Controllers
{
    public class LineaCapturaController : Controller
    {


        public ActionResult Index(Int64 idSolicitud = 0, bool deudorPuro = false)
        {
            GeneraLineasCapturaViewModel model = new GeneraLineasCapturaViewModel();
            model.listaLineaCaptura = new List<LineaCaptura>();
            model.deudorPuro = deudorPuro;
            string mensajeExUsuario = string.Empty;
            try
            {
                using (ClienteServicioLineaCaptura servicio = new ClienteServicioLineaCaptura())
                {
                    var usuario = new Usuario();
                     // Si solicitud proviene de la página de deudor puro obtenemos los datos del usuario
                    var usuarioApoyo = servicio.CreateChannel().ObtenerInfoUsuarioDeudorPuro(idSolicitud);
                    if (deudorPuro)
                    {
                        usuario = usuarioApoyo;
                    }
                    else
                    {
                        usuario = MemberCache.UsuarioActual;
                        usuario.IdTipoLineaMAT = usuarioApoyo.IdTipoLineaMAT;
                        usuario.IdPersonaMAT = usuarioApoyo.IdPersonaMAT;
                    }
                    var listaLineaCapturaRespuesta = servicio.CreateChannel().GenerarLineasCaptura(idSolicitud, usuario);
                    model.listaLineaCaptura = listaLineaCapturaRespuesta.LineasCaptura;
                    mensajeExUsuario = listaLineaCapturaRespuesta.Excepcion != null ? listaLineaCapturaRespuesta.Excepcion.Mensaje : string.Empty;
                }

                if (model.listaLineaCaptura.Count == 0)
                {
                    if (!string.IsNullOrEmpty(mensajeExUsuario))
                    {
                        ViewBag.OnLoadCompleteMessage = Alertas.Errror(mensajeExUsuario, "Aceptar");
                    }
                    else
                    {
                        ViewBag.OnLoadCompleteMessage = Alertas.Informacion("No hay líneas de captura para los documentos seleccionados.", "Aceptar");
                    }
                }
                else
                {

                    if (!string.IsNullOrEmpty(mensajeExUsuario))
                    {
                        ViewBag.OnLoadCompleteMessage = Alertas.Errror(mensajeExUsuario, "Aceptar");
                    }
                    else
                    {
                        //Si es solo una linea de captura generamos el pdf automaticamente
                        if (model.listaLineaCaptura.Count == 1)
                        {
                            return RedirectToAction("Index", "GeneraArchivo", new { folios = model.listaLineaCaptura[0].Folio , deudorPuro = model.deudorPuro  });
                        }
                    }

                }
            }
            catch (ExcepcionTipificada ex)
            {
                ViewBag.OnLoadCompleteMessage = Alertas.Errror(ex.Mensaje, "Aceptar");
            }
            catch (Exception ex)
            {

                string ticket = AccesoLogEventos.EscribirEntradaLog((int)EnumErroresLineaCaptura.ErrorAlGenerarLineasDeCaptura, ex);
                ExcepcionTipificada exTip = new ExcepcionTipificada("No se logró generar la línea(s) de captura.", ex, ticket);
                ViewBag.OnLoadCompleteMessage = Alertas.Errror(exTip.Mensaje, "Aceptar");

            }
            return View(model);

        }

        public ActionResult GeneraArchivo(string[] chklistaDeFolios, bool deudorPuro)
        {
            return RedirectToAction("Index", "GeneraArchivo", new { folios = string.Join(",", chklistaDeFolios), deudorPuro = deudorPuro });
        }


        public ActionResult ObtenerDocumentosAsociados(string lineaCaptura)
        {
            GenerarDocumentosDeterminanteViewModel model = new GenerarDocumentosDeterminanteViewModel();
            using (ClienteServicioLineaCaptura servicio = new ClienteServicioLineaCaptura())
            {
                model.listaDocumentosAsociados = servicio.CreateChannel().ObtenerDocumentosAsociados(lineaCaptura, MemberCache.UsuarioActual.Rfc);
            }
            return PartialView("_GridDocumentos", model);
        }
    }
}
