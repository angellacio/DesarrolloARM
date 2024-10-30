
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Presentacion.Herramientas:Sat.CreditosFiscales.Presentacion.Herramientas.Alertas:1:12/07/2013[Assembly:1.0:12/07/2013])

namespace Sat.CreditosFiscales.Presentacion.Herramientas
{
    using System.Web.Mvc;

    /// <summary>
    /// Clase para la generación de mensajes de error en pantalla
    /// </summary>
    public static class Alertas
    {
        /// <summary>
        /// Genera mensaje de error
        /// </summary>
        /// <param name="mensaje">Mensaje en pantalla</param>
        /// <param name="textoAceptar">Texto para el boton aceptar</param>
        /// <param name="scriptAceptar">Script a ejecutar al cerrar el mensaje o seleccionar aceptar</param>
        /// <returns></returns>
        //public static MvcHtmlString Errror(string mensaje, string textoAceptar, string scriptAceptar)
        //{
        //    return MvcHtmlString.Create(string.Format("ModalError('{0}', '{1}' ,'{2}');", mensaje.Replace("'", @"\'"), textoAceptar, scriptAceptar.Replace("'", @"\'")));
        //}

        /// <summary>
        /// Genera mensaje de error
        /// </summary>
        /// <param name="mensaje">Mensaje en pantalla</param>
        /// <param name="textoAceptar">Texto para el boton aceptar</param>
        /// <returns></returns>
        public static MvcHtmlString Errror(string mensaje, string textoAceptar)
        {
            return MvcHtmlString.Create(string.Format("openDialog('{0}', 1, '{1}');", mensaje.Replace("'", @"\'"), textoAceptar));
        }

        /// <summary>
        /// Genera mensaje de alerta
        /// </summary>
        /// <param name="mensaje">Mensaje en pantalla</param>
        /// <param name="textoAceptar">Texto para el boton aceptar</param>
        /// <param name="scriptAceptar">Script a ejecutar al cerrar el mensaje o seleccionar aceptar</param>
        /// <returns></returns>
        //public static MvcHtmlString Alerta(string mensaje, string textoAceptar, string scriptAceptar)
        //{
        //    return MvcHtmlString.Create(string.Format("ModalAlerta('{0}', '{1}' ,'{2}');", mensaje.Replace("'", @"\'"), textoAceptar, scriptAceptar.Replace("'", @"\'")));
        //}

        /// <summary>
        /// Genera mensaje de alerta
        /// </summary>
        /// <param name="mensaje">Mensaje en pantalla</param>
        /// <param name="textoAceptar">Texto para el boton aceptar</param>
        /// <returns></returns>
        public static MvcHtmlString Alerta(string mensaje, string textoAceptar)
        {
            return MvcHtmlString.Create(string.Format("openDialog('{0}', 3, '{1}');", mensaje.Replace("'", @"\'"), textoAceptar));
        }

        /// <summary>
        /// Genera mensaje de información
        /// </summary>
        /// <param name="mensaje">Mensaje en pantalla</param>
        /// <param name="textoAceptar">Texto para el boton aceptar</param>
        /// <param name="scriptAceptar">Script a ejecutar al cerrar el mensaje o seleccionar aceptar</param>
        /// <returns></returns>
        //public static MvcHtmlString Informacion(string mensaje, string textoAceptar, string scriptAceptar)
        //{
        //    return MvcHtmlString.Create(string.Format("ModalInfo('{0}', '{1}' ,'{2}');", mensaje.Replace("'", @"\'"), textoAceptar, scriptAceptar.Replace("'", @"\'")));
        //}

        /// <summary>
        /// Genera mensaje de información
        /// </summary>
        /// <param name="mensaje">Mensaje en pantalla</param>
        /// <param name="textoAceptar">Texto para el boton aceptar</param>
        /// <param name="scriptAceptar">Script a ejecutar al cerrar el mensaje o seleccionar aceptar</param>
        /// <returns></returns>
        public static MvcHtmlString Informacion(string mensaje, string textoAceptar)
        {
            return MvcHtmlString.Create(string.Format("openDialog('{0}', 2, '{1}');", mensaje.Replace("'", @"\'"), textoAceptar));
        }

        /// <summary>
        /// Genera mensaje de confirmar
        /// </summary>
        /// <param name="mensaje">Mensaje en pantalla</param>
        /// <param name="textoAceptar">Texto para el boton aceptar</param>
        /// <param name="textoCancelar">Texto para el boton cancelar</param>
        /// <param name="scriptAceptar">Script a ejecutar al cerrar el mensaje o seleccionar aceptar</param>
        /// <returns></returns>
        //public static MvcHtmlString Confirmar(string mensaje, string textoAceptar, string textoCancelar, string scriptAceptar)
        //{
        //    return MvcHtmlString.Create(string.Format("ModalConfirmar('{0}', '{1}' ,'{2}','{3}');", mensaje.Replace("'", @"\'"), textoAceptar, textoCancelar, scriptAceptar.Replace("'", @"\'")));
        //}

    }
}
