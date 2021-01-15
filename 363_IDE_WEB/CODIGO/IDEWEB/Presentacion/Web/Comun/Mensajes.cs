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
    public static class Mensajes
    {
        public const string ServicioNoDisponible = "[29] Por el momento el servicio no está disponible. Favor de intentarlo más tarde.";
        public const string ServicioAutenticacionNoDisponible = "El servicio de autenticacion no esta disponible o el certificado del contribuyente no se encontro.";
        public const string ServicioConfiguracionNoDisponible = "El servicio de configuración no está disponible.";
        public const string ServicioTransferenciaNoDisponible = "El Servicio de transferencia de archivos (BigFilesService) no estuvo disponible.";
        public const string ServicioRecepcionNoDisponible = "El servicio de recepción de la declaración no estuvo disponible.";
        public const string ErrorAutenticacionFiel = "No se pudo obtener los datos de la Firma Electrónica Avanzada (FIEL/FEA) del contribuyente.";
        public const string ParametroArchivoConfiguracionRequerido = "El parámetro de configuración '{0}' no se encontró en el archivo de configuración de la aplicación web.";
        public const string ParametroBDConfiguracionRequerido = "El parámetro de configuración '{0}' no se encontró en la base de datos SCADENET.";
        public const string UsuarioNoEncontrado = "El usuario '{0}' no se encontró, por lo cual no pudo ser confirmada su identidad.";
        public const string ArchivoExistenteSF = "Ya existe un envío en progreso de este archivo. Si el archivo estaba correcto no lo vuelva a enviar y espere su acuse; en caso contrario genérelo nuevamente y envíelo.";
        public const string ArchivoExistenteCF = "Ya existe un envío previo de este archivo con el número de folio: {0}, por favor verifique que el enviado sea correcto. Si es correcto, no lo vuelva a enviar y espere su acuse; en caso contrario, genérelo nuevamente y vuélvalo a enviar.";

        public static string RutaRequerida
        {
            get { return Utilerias.ConsultarArchivoConfiguracion("RutaRequerida"); }
        }

        public static string RutaInvalida
        {
            get { return Utilerias.ConsultarArchivoConfiguracion("RutaInvalida"); }
        }
    }
}
