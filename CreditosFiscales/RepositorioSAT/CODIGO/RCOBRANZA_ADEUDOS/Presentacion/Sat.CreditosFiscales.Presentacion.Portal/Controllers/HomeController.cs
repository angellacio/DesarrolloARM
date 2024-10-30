
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Presentacion.Controllers:Sat.CreditosFiscales.Presentacion.Controllers.HomeController:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Sat.CreditosFiscales.Comunes.Entidades.AccionesPantalla;
using Sat.CreditosFiscales.Comunes.Entidades.Catalogos;
using Sat.CreditosFiscales.Comunes.Entidades.Seguridad;
using Sat.CreditosFiscales.Comunes.Herramientas;
using Sat.CreditosFiscales.Presentacion.Herramientas;

namespace Sat.CreditosFiscales.Presentacion.Portal.Controllers
{
    public class HomeController : Controller
    {        
        public ActionResult Index()
        {
            this.ViewBag.ModoProduccion = bool.Parse(ConfigurationManager.AppSettings["ModoProduccion"].ToString());
            return View();
        }

        [HttpPost]
        public ActionResult Index(string rfcContribuyente)
        {
            try
            {
                this.ViewBag.ModoProduccion = bool.Parse(ConfigurationManager.AppSettings["ModoProduccion"].ToString());
                using (ClienteServicioSeguridad cliente = new ClienteServicioSeguridad())
                {
                    InfoContribuyente infoIDC = cliente.CreateChannel().ObtenerInfoContribuyente(rfcContribuyente);

                    MemberCache.UsuarioActual = new Comunes.Entidades.Usuario()
                    {
                        ApellidoPaterno = infoIDC.ApellidoPaterno,
                        ApellidoMaterno = infoIDC.ApellidoMaterno,
                        Nombres = infoIDC.Nombre,
                        IdALR = (byte)infoIDC.IdALR,
                        Rfc = infoIDC.RfcVigente,
                        PersonaFisica = infoIDC.PersonaFisica,
                        RazonSocial = infoIDC.RazonSocial,
                        Rfcs = infoIDC.Rfcs,
                        BoId = infoIDC.BoId
                    };
                    MemberCache.SeleccionSolicitud=new Comunes.Entidades.SeleccionUsuario();
                    FormsAuthentication.SetAuthCookie(MemberCache.UsuarioActual.Rfc, false);

                    
                }
            }
            catch (Exception err)
            {
                this.ViewBag.Error = MetodosComunes.AplicaFormatoError(err.Message, string.Empty);

            }

            return View();
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return View("Index");
        }
            
        public ActionResult Landing()
        {
            this.ViewBag.RutaAcceso = string.Empty;

            bool existeHeader = ExisteHeader();
          
            if (existeHeader)
            {
                using (ClienteServicioSeguridad cliente = new ClienteServicioSeguridad())
                {
                    InfoContribuyente infoIDC = cliente.CreateChannel().ObtenerInfoContribuyente(this.ViewBag.Rfc);

                    MemberCache.UsuarioActual = new Comunes.Entidades.Usuario()
                    {
                        ApellidoPaterno = infoIDC.ApellidoPaterno,
                        ApellidoMaterno = infoIDC.ApellidoMaterno,
                        Nombres = infoIDC.Nombre,
                        IdALR = (byte)infoIDC.IdALR,
                        Rfc = infoIDC.RfcVigente,
                        PersonaFisica = infoIDC.PersonaFisica,
                        RazonSocial = infoIDC.RazonSocial,
                        Rfcs = infoIDC.Rfcs,
                        BoId = infoIDC.BoId
                    };

                    MemberCache.SeleccionSolicitud = new Comunes.Entidades.SeleccionUsuario();
                    FormsAuthentication.SetAuthCookie(MemberCache.UsuarioActual.Rfc, false);
                    this.ViewBag.RutaAcceso = FormsAuthentication.GetRedirectUrl(MemberCache.UsuarioActual.Rfc, false);
                }
            }
            return View();
        }

        private bool ExisteHeader()
        {
            var headerRequest = System.Web.HttpContext.Current.Request.Headers["Authorization"];
            var headerResponse = System.Web.HttpContext.Current.Response.Headers["Authorization"];
            
            bool existeHeader = false;

            if (headerRequest != null)
            {
                existeHeader = true;
                this.ViewBag.Rfc = headerRequest;
            }
            else if (headerResponse != null)
            {
                existeHeader = true;
                this.ViewBag.Rfc = headerResponse;
            }
            return existeHeader;
        }
    }
}
