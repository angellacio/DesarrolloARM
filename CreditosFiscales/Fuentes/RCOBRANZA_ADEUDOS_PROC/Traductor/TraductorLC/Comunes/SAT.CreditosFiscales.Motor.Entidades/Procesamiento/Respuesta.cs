
//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.Entidades:SAT.CreditosFiscales.Motor.Entidades.RespuestaGenerica:1:12/07/2012[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SAT.CreditosFiscales.Motor.Entidades.ManejoErrores;
using Enumeracion = SAT.CreditosFiscales.Motor.Entidades.Enumeraciones;
using SAT.CreditosFiscales.Motor.Utilidades;

namespace SAT.CreditosFiscales.Motor.Entidades.Procesamiento
{
    /// <summary>
    /// Clase base para indicar el resultado de una ejecución.
    /// </summary>
    public class RespuestaGenerica
    {
        /// <summary>
        /// Indica si la ejecución fue exitosa.
        /// </summary>
        public bool EsExitoso { get; set; }

        /// <summary>
        /// Indica si la petición tiene una línea de captura
        /// </summary>
        public bool ExisteLineaCaptura { get; set; }

        /// <summary>
        /// Contiene la lista de errores encontrados en la ejecución.
        /// </summary>
        public Errores ListaErrores { get; set; }

        /// <summary>
        /// Duración de algun servicio
        /// </summary>
        public decimal Duracion { get; set; }

        /// <summary>
        /// Constructor predeterminado de la clase.
        /// </summary>
        public RespuestaGenerica()
        {
            ListaErrores = new Errores();
            EsExitoso = false;
        }

        /// <summary>
        /// Agrega un nuevo error a la lista.
        /// </summary>
        /// <param name="mensajeError">Mensaje de error de usuario.</param>
        /// <param name="excepcionAplicacion">Excepción generada por la aplicación.</param>
        /// <param name="codigoError">Código de error.</param>
        public void AgregaError(string mensajeError, Exception excepcionAplicacion = null, int codigoError = 0)
        {
            ListaErrores.Add(new Error { Codigo = codigoError, ErrorUsuario = mensajeError, ErrorAplicacion = excepcionAplicacion });
        }

        /// <summary>
        /// Agrega un nuevo error a la lista.
        /// </summary>
        /// <param name="codigoError">Código de error.</param>
        /// <param name="exception">Excepción generada por la aplicación.</param>
        public void AgregaError(Catalogos.CatMensajes mensajeError, Exception exception = null)
        {
            ListaErrores.Add(new Error { Codigo = mensajeError.Codigo, ErrorUsuario = mensajeError.Valor, ErrorAplicacion = exception });
        }

        /// <summary>
        /// Agrega un nuevo error a la lista.
        /// </summary>
        /// <param name="codigoError">Código de error.</param>
        /// <param name="parametros">Lista de parámetros asociados al mensaje.</param>
        public void AgregaError(Catalogos.CatMensajes mensajeError, params object[] parametros)
        {
            ListaErrores.Add(new Error { Codigo = mensajeError.Codigo, ErrorUsuario = string.Format(mensajeError.Valor, parametros), ErrorAplicacion = null });
        }

        /// <summary>
        /// Agrega un nuevo error a la lista.
        /// </summary>
        /// <param name="codigoError">Código de error.</param>
        /// <param name="ex">Excepción generada por la aplicación.</param>
        /// <param name="parametros">Lista de parámetros asociados al mensaje.</param>
        public void AgregaError(Catalogos.CatMensajes mensajeError, Exception ex, params object[] parametros)
        {
            ListaErrores.Add(new Error { Codigo = mensajeError.Codigo, ErrorUsuario = string.Format(mensajeError.Valor, parametros), ErrorAplicacion = ex });
        }
      
    }

    /// <summary>
    /// Respuesta con una entidad devuelta.
    /// </summary>
    /// <typeparam name="T">Entidad.</typeparam>
    public class Respuesta<T> : RespuestaGenerica
    {
        /// <summary>
        /// Entidad que se espera en la ejecución.
        /// </summary>
        public T Entidad { get; set; }

        /// <summary>
        /// Constructor predeterminado de la clase.
        /// </summary>
        public Respuesta()
            : base()
        { }
    }





}
