using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Globalization;

namespace Sat.Scade.Net.IDE.Presentacion.Web
{
    public static class Parametros
    {
        public static MedioPresentacion MedioPresentacion
        {
            get { return (MedioPresentacion)Convert.ToInt32(Utilerias.ConsultarArchivoConfiguracion("MedioPresentacion")); }
        }

        public static bool UsarCookies
        {
            get { return Convert.ToBoolean(Utilerias.ConsultarArchivoConfiguracion("UsarCookies")); }
        }
        public static bool ForzarServicioNoDisponible
        {
            get { return Convert.ToBoolean(Utilerias.ConsultarArchivoConfiguracion("ForzarServicioNoDisponible")); }
        }

        public static string UrlServicioConfiguracion
        {
            get { return Utilerias.ConsultarArchivoConfiguracion("UrlServicioConfiguracion"); }
        }

        public static string FormatoFecha
        {
            get { return Utilerias.ConsultarArchivoConfiguracion("FormatoFecha"); }
        }

        public static string FormatoHora
        {
            get { return Utilerias.ConsultarArchivoConfiguracion("FormatoHora"); }
        }

        public static CultureInfo Cultura
        {
            get { return new CultureInfo(Utilerias.ConsultarArchivoConfiguracion("Cultura")); }
        }

        public static string DataBaseKeyConfig
        {
            get { return Utilerias.ConsultarArchivoConfiguracion("DataBaseKeyConfig"); }
        }

        public static string StoreProcedureName
        {
            get { return Utilerias.ConsultarArchivoConfiguracion("StoreProcedureName"); }
        }

        public static string StoreProcedureParameterName
        {
            get { return Utilerias.ConsultarArchivoConfiguracion("StoreProcedureParameterName"); }
        }

       

        public static string RutaLocal
        {
            get { return Utilerias.ConsultarBDConfiguracion("Sat.DyP.IDE::RutaLocal"); }
        }

        public static string DecryptionCertPath
        {
            get { return Utilerias.ConsultarBDConfiguracion("SAT.SCADE.NET.Negocio.Declaraciones.DeclaraSAT.Tradicional::DecryptionCertificatePath"); }
        }

        public static string DecryptionKeyPath
        {
            get { return Utilerias.ConsultarBDConfiguracion("SAT.SCADE.NET.Negocio.Declaraciones.DeclaraSAT.Tradicional::DecryptionPrivateKeyPath"); }
        }

        public static string DecryptionPassword
        {
            get { return Utilerias.ConsultarBDConfiguracion("SAT.SCADE.NET.Negocio.Declaraciones.DeclaraSAT.Tradicional::DecryptionPassword"); }
        }

        public static string DecryptionKey
        {
            get { return Utilerias.ConsultarBDConfiguracion("SAT.SCADE.NET.Negocio.Declaraciones.DeclaraSAT.Tradicional::DecryptionKey"); }
        }

        public static string Sesion
        {
            get { return "AcuseMensaje"; }
        }

        public static int IdEvento
        {
            get { return 13000; }
        }

        public static string OrigenEvento
        {
            get { return "Scade.Net.IDE"; }
        }

        public static string IdMateria
        {
            get { return Utilerias.ConsultarArchivoConfiguracion("IdMateria"); }
        }

        public static string EntidadReceptora
        {
            get { return Utilerias.ConsultarArchivoConfiguracion("EntidadReceptora"); }
        }

        public static string IdInternet
        {
            get { return Utilerias.ConsultarBDConfiguracion("Sat.DyP.IDE::Internet"); }
        }

        public static string IdTipoMensaje
        {
            get { return Utilerias.ConsultarBDConfiguracion("Sat.DyP.IDE::TipoMensajeVista"); }
        }
    }
}
