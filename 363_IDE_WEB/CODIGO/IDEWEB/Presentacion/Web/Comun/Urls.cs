using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Sat.Scade.Net.IDE.Presentacion.Web
{
    public static class Urls
    {
        //Urls´s Existentes en el sistema
        public const string Notificacion = "~/Notificacion.aspx";
        public const string NotificacionEncrip = "~/NotificacionEncrip.aspx";
        public const string ServicioNodisponible = "~/ServicioNoDisponible.aspx";

        //Transformaciones 
        public const string DeclaracionAceptada = "~/App_LocalResources/DeclaracionAceptada.xsl";
        public const string DeclaracionRechazada = "~/App_LocalResources/DeclaracionRechazada.xsl";
    }
}
