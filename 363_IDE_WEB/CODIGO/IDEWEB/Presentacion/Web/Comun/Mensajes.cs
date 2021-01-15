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
        public const string ServicioNoDisponible = "[29] Por el momento el servicio no est� disponible. Favor de intentarlo m�s tarde.";
        public const string ServicioAutenticacionNoDisponible = "El servicio de autenticacion no esta disponible o el certificado del contribuyente no se encontro.";
        public const string ServicioConfiguracionNoDisponible = "El servicio de configuraci�n no est� disponible.";
        public const string ServicioTransferenciaNoDisponible = "El Servicio de transferencia de archivos (BigFilesService) no estuvo disponible.";
        public const string ServicioRecepcionNoDisponible = "El servicio de recepci�n de la declaraci�n no estuvo disponible.";
        public const string ErrorAutenticacionFiel = "No se pudo obtener los datos de la Firma Electr�nica Avanzada (FIEL/FEA) del contribuyente.";
        public const string ParametroArchivoConfiguracionRequerido = "El par�metro de configuraci�n '{0}' no se encontr� en el archivo de configuraci�n de la aplicaci�n web.";
        public const string ParametroBDConfiguracionRequerido = "El par�metro de configuraci�n '{0}' no se encontr� en la base de datos SCADENET.";
        public const string UsuarioNoEncontrado = "El usuario '{0}' no se encontr�, por lo cual no pudo ser confirmada su identidad.";
        public const string ArchivoExistenteSF = "Ya existe un env�o en progreso de este archivo. Si el archivo estaba correcto no lo vuelva a enviar y espere su acuse; en caso contrario gen�relo nuevamente y env�elo.";
        public const string ArchivoExistenteCF = "Ya existe un env�o previo de este archivo con el n�mero de folio: {0}, por favor verifique que el enviado sea correcto. Si es correcto, no lo vuelva a enviar y espere su acuse; en caso contrario, gen�relo nuevamente y vu�lvalo a enviar.";

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
