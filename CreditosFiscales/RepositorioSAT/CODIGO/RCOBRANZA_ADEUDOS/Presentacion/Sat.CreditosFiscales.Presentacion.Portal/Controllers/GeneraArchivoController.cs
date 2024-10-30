
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Presentacion.Controllers:Sat.CreditosFiscales.Presentacion.Controllers.GeneraArchivoController:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Sat.CreditosFiscales.Procesamiento.LogicaNegocio.Servicios;
using Sat.CreditosFiscales.Presentacion.Herramientas;
using Sat.CreditosFiscales.Comunes.Entidades.AccesoLogEventos;
using Sat.CreditosFiscales.Comunes.Entidades.CodigosError;

namespace Sat.CreditosFiscales.Presentacion.Portal.Controllers
{
    public class GeneraArchivoController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GeneraArchivoPdf(string folios)
        {
            try
            {
                List<string> listaDeFolios = folios.Split(',').ToList();
                byte[] archivoDeBytes = GeneraArchivoDeBytes(listaDeFolios);
                MemoryStream ms = new MemoryStream();
                ms.Write(archivoDeBytes, 0, archivoDeBytes.Length);
                ms.Position = 0;
                return File(ms, "application/pdf");
            }
            catch (ExcepcionTipificada ex)
            {
                ViewBag.OnLoadCompleteMessage = Alertas.Errror(ex.Mensaje, "Aceptar");
            }
            catch (Exception ex)
            {
                string ticket = AccesoLogEventos.EscribirEntradaLog((int)EnumErroresLineaCaptura.ErrorAlGenerarArhivo, ex);
                ExcepcionTipificada exTip = new ExcepcionTipificada("No se logró generar el archivo para las líneas de captura.", ex, ticket);
                ViewBag.OnLoadCompleteMessage = exTip.Mensaje;
            }
            return View("Blank");
        }


        private byte[] GeneraArchivoDeBytes(List<string> listaDeFolios)
        {
            using (ClienteServicioLineaCaptura servicio = new ClienteServicioLineaCaptura())
            {
                return servicio.CreateChannel().GeneraArchivos(listaDeFolios);
            }
        }
    }
}
