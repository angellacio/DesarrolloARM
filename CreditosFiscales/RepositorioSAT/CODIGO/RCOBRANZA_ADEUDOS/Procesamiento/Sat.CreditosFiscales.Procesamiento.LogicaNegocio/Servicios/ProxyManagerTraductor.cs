
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Procesamiento.LogicaNegocio.Servicios:Sat.CreditosFiscales.Procesamiento.LogicaNegocio.Servicios.MensajesError:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Sat.CreditosFiscales.Comunes.Entidades;
using Sat.CreditosFiscales.Comunes.Entidades.AccesoLogEventos;
using Sat.CreditosFiscales.Comunes.Entidades.Catalogos;
using Sat.CreditosFiscales.Comunes.Entidades.CodigosError;
using Sat.CreditosFiscales.Comunes.Entidades.GeneraFormato;
using Sat.CreditosFiscales.Comunes.Entidades.Servicios;
using Sat.CreditosFiscales.Comunes.Herramientas;
using Sat.CreditosFiscales.Datos.AccesoDatos.GeneraFormato;

namespace Sat.CreditosFiscales.Procesamiento.LogicaNegocio.Servicios
{
    public static class MensajesError
    {
        public static string RecuperaMensajeError(EscenariosNoGeneracionLC escenario, bool esContPuro)
        {
            if (!esContPuro)
                return RecuperaMensajeError(escenario);
            else
                return RecuperaMensajeErrorContPuro(escenario);
        }

        public static string RecuperaMensajeError(EscenariosNoGeneracionLC escenario)
        {
            switch (escenario)
            {
                /**/case EscenariosNoGeneracionLC.MarcadosEstatal: return "Los formatos para el pago de adeudos con cobro a cargo de la entidad federativa debe solicitarlos en las oficinas estatales.";
                /**/case EscenariosNoGeneracionLC.MarcadosPlazos: return "Los formatos con convenio de pago en parcialidades debe solicitarlos a la Administración Local de Servicios al Contribuyente que corresponda a su domicilio fiscal.";
                /**/case EscenariosNoGeneracionLC.MarcadosPlazosYEstatales: return "Los formatos para el pago de adeudos con convenio de pago en parcialidades y adeudos con cobro a cargo de la entidad federativa debe solicitarlos en la Administración Local de Servicios al Contribuyente o en las oficinas estatales, según corresponda.";
                /**/case EscenariosNoGeneracionLC.MarcadosPlazosYEstatalesYOtros: return "Los formatos para el pago de los adeudos seleccionados debe solicitarlos a la Administración Local de Servicios al Contribuyente que corresponda a su domicilio fiscal o en las oficinas estatales, según corresponda.";
                /**/case EscenariosNoGeneracionLC.MarcadosOtros: return "Los formatos para el pago de los adeudos seleccionados debe solicitarlos a la Administración Local de Servicios al Contribuyente que corresponda a su domicilio fiscal.";
                /**/case EscenariosNoGeneracionLC.MarcadosOtrosYPlazos: return "Los formatos para el pago de los adeudos seleccionados debe solicitarlos a la Administración Local de Servicios al Contribuyente que corresponda a su domicilio fiscal.";
                /**/case EscenariosNoGeneracionLC.MarcadosOtrosYEstatales: return "Los formatos para el pago de los adeudos seleccionados debe solicitarlos a la Administración Local de Servicios al Contribuyente que corresponda a su domicilio fiscal o en las oficinas estatales, según corresponda.";

                /**/case EscenariosNoGeneracionLC.MarcadosEstatalError: return "Ingrese más tarde para verificar si existen formatos para el pago de adeudos con cobro a cargo de la entidad federativa, o solicítelos en las oficinas estatales.";
                /**/case EscenariosNoGeneracionLC.MarcadosPlazosError: return "Ingrese más tarde para verificar si existen formatos para el pago de adeudos con convenio de pago en parcialidades, o solicítelos a la Administración Local de Servicios al Contribuyente que corresponda a su domicilio fiscal.";
                /**/case EscenariosNoGeneracionLC.MarcadosPlazosYEstatalesError: return "Ingrese más tarde para verificar si existen formatos para el pago de adeudos con convenio de pago en parcialidades y adeudos con cobro a cargo de la entidad federativa, o solicítelos en la Administración Local de Servicios al Contribuyente o en las oficinas estatales, según corresponda.";
                /**/case EscenariosNoGeneracionLC.MarcadosPlazosYEstatalesYOtrosError: return "Ingrese más tarde para verificar si existen formatos para el pago de los adeudos seleccionados, o solicítelos en la Administración Local de Servicios al Contribuyente o en las oficinas estatales, según corresponda.";
                /**/case EscenariosNoGeneracionLC.MarcadosOtrosError: return "Ingrese más tarde para verificar si existen formatos para el pago de los adeudos seleccionados, o solicítelos a la Administración Local de Servicios al Contribuyente que corresponda a su domicilio fiscal.";
                /**/case EscenariosNoGeneracionLC.MarcadosOtrosYPlazosError: return "Ingrese más tarde para verificar si existen formatos para el pago de los adeudos seleccionados, o solicítelos a la Administración Local de Servicios al Contribuyente que corresponda a su domicilio fiscal.";
                /**/case EscenariosNoGeneracionLC.MarcadosOtrosYEstatalesError: return "Ingrese más tarde para verificar si existen formatos para el pago de los adeudos seleccionados, o solicítelos en la Administración Local de Servicios al Contribuyente o en las oficinas estatales, según corresponda.";

                //Error traductor
                /**/case EscenariosNoGeneracionLC.NoMarcados: return "Hay inconsistencias en el proceso de generación del formato para pago. Acuda a la Administración Local de Servicios al Contribuyente que corresponda a su domicilio fiscal, o llame a INFOSAT.";
                /**/case EscenariosNoGeneracionLC.NoMarcadosError: return "No fue posible procesar su solicitud; inténtelo más tarde.";

                //Errores DyP
                /**/case EscenariosNoGeneracionLC.NoMarcadosDyP: return "No es posible generar el formato para el pago; para obtenerlo, debe solicitarlo en la Administración Local de Servicios al Contribuyente que corresponda a su domicilio fiscal.";
                /**/case EscenariosNoGeneracionLC.NoMarcadosDyPError: return "Por el momento no es posible generar el formato para pago; inténtelo más tarde.";


                /**/case EscenariosNoGeneracionLC.CombinadosNingunaLCEstatal: return "No es posible generar los formatos para el pago de algunos de los adeudos seleccionados;  para obtenerlos, debe solicitarlos en las oficinas estatales.";
                /**/case EscenariosNoGeneracionLC.CombinadosNingunaLCNoEstatal: return "No es posible generar los formatos para el pago de algunos de los adeudos seleccionados;  para obtenerlos, debe solicitarlos en la Administración Local de Servicios al Contribuyente que corresponda a su domicilio fiscal.";

                
                /**/case EscenariosNoGeneracionLC.CombinadosNingunaLCEstatalError: return "Ingrese más tarde para verificar si existen formatos para el pago de los adeudos seleccionado, o solicítelos en la Administración Local de Servicios al Contribuyente o en las oficinas estatales, según corresponda.";
                /**/case EscenariosNoGeneracionLC.CombinadosNingunaLCNoEstatalError: return "Ingrese más tarde para verificar si existen formatos para el pago de los adeudos seleccionados, o solicítelos a la Administración Local de Servicios al Contribuyente que corresponda a su domicilio fiscal.";
                
                // Solo se recuperaron las o algunas LC de los marcados y no se genero/recupero para los no marcados
                /**/case EscenariosNoGeneracionLC.CombinadosAlgunasLC: return "No se pudo concluir el proceso para la obtención y generación de los formatos; inténtelo nuevamente.";

                default: return "Hay inconsistencias en el proceso de generación del formato para pago. Acuda a la Administración Local de Servicios al Contribuyente que corresponda a su domicilio fiscal, o llame a INFOSAT.";
            }

        }

        public static string RecuperaMensajeErrorContPuro(EscenariosNoGeneracionLC escenario)
        {
            switch (escenario)
            {
                /**/case EscenariosNoGeneracionLC.MarcadosEstatal: return "Los formatos para el pago de adeudos con cobro a cargo de la entidad federativa debe solicitarlos en las oficinas estatales.";
                /**/case EscenariosNoGeneracionLC.MarcadosPlazos: return "Los formatos para el pago de adeudos con convenio de pago en parcialidades debe solicitarlos a la Administración Local de Servicios al Contribuyente que corresponda a su domicilio fiscal.";
                /**/case EscenariosNoGeneracionLC.MarcadosOtros: return "Los formatos para el pago del adeudo mostrado debe solicitarlos a la Administración Local de Servicios al Contribuyente que corresponda a su domicilio fiscal.";

                /**/case EscenariosNoGeneracionLC.MarcadosEstatalError: return "Ingrese más tarde para verificar si existen formatos para el pago de adeudos con cobro a cargo de la entidad federativa, o solicítelos en las oficinas estatales.";
                /**/case EscenariosNoGeneracionLC.MarcadosPlazosError: return "Ingrese más tarde para verificar si existen formatos para el pago de adeudos con convenio de pago en parcialidades, o solicítelos a la Administración Local de Servicios al Contribuyente que corresponda a su domicilio fiscal.";
                /**/case EscenariosNoGeneracionLC.MarcadosOtrosError: return "Ingrese más tarde para verificar si existen formatos para el pago del adeudo mostrado, o solicítelos a la Administración Local de Servicios al Contribuyente que corresponda a su domicilio fiscal.";

                //Error traductor
                case EscenariosNoGeneracionLC.NoMarcados: return "Hay inconsistencias en el proceso de generación del formato para pago. Acuda a la Administración Local de Servicios al Contribuyente que corresponda a su domicilio fiscal, o llame a INFOSAT.";
                case EscenariosNoGeneracionLC.NoMarcadosError: return "No fue posible procesar su solicitud; inténtelo más tarde."; 
                //Errores DyP
                case EscenariosNoGeneracionLC.NoMarcadosDyP: return "Por el momento no es posible generar el formato para pago; para obtenerlo, debe solicitarlo en la Administración Local de Servicios al Contribuyente que corresponda a su domicilio fiscal.";
                case EscenariosNoGeneracionLC.NoMarcadosDyPError: return "Por el momento no es posible generar el formato para pago; inténtelo más tarde.";

                default: return "Se detectaron inconsistencias en el proceso de generación del formato para pago. Le sugerimos acudir al módulo de atención de la Administración Local de Servicios al Contribuyente que corresponda a su domicilio fiscal, o bien, a llamar a INFOSAT.";
            }

        }
    }

    public enum EscenariosNoGeneracionLC
    {
        SinClasificar = 0,
        MarcadosEstatal = 1,
        MarcadosPlazos = 2,
        MarcadosPlazosYEstatales = 3,
        MarcadosPlazosYEstatalesYOtros = 4,
        MarcadosOtros = 5,
        MarcadosOtrosYPlazos = 6,
        MarcadosOtrosYEstatales = 7,


        MarcadosEstatalError = 8,
        MarcadosPlazosError = 9,
        MarcadosPlazosYEstatalesError = 10,
        MarcadosPlazosYEstatalesYOtrosError = 11,
        MarcadosOtrosError = 12,
        MarcadosOtrosYPlazosError = 13,
        MarcadosOtrosYEstatalesError = 14,

        //Error traductor
        NoMarcados = 15,//Si no genera o recupera la lc hay mensajes en la lista de errores
        NoMarcadosError = 16,//No hay respuesta del traductor
        NoMarcadosDyP= 17,
        NoMarcadosDyPError=18,
            
        CombinadosNingunaLCEstatal = 19,
        CombinadosNingunaLCNoEstatal = 20,

        CombinadosNingunaLCEstatalError = 21,
        CombinadosNingunaLCNoEstatalError = 22,



        //Solo se recuperaron las o algunas LC para los Marcados y no se genero o recupero la de los no marcados
        CombinadosAlgunasLC = 23,//Puede ser la de los No marcados con alguna de los Marcados o algunas de los No marcados

        

    }

    public enum ClasificacionMarcados
    {
        SinClasificar=0,
        MarcadosEstatal = 1,
        MarcadosPlazos = 2,
        MarcadosPlazosYEstatales = 3,
        MarcadosPlazosYEstatalesYOtros = 4,
        MarcadosOtros = 5,
        MarcadosOtrosYPlazos = 6,
        MarcadosOtrosYEstatales = 7,
    }
    
    /// <summary>
    /// Clase a que maneja los llamados al Servicio del Traductor para la consulta y generación de líneas de catura.
    /// </summary>
    public static class ProxyManagerTraductor
    {
        
        #region Métodos publicos

        /// <summary>
        /// Método que recupera las lineas de captura correspondientes a los documentos determinantes del contribuyente.
        /// </summary>
        /// <param name="documentosDet">Lista de documentos determinantes</param>
        /// <param name="usuario">Datos del contribuyente</param>
        /// <param name="exGeneral">Excepciones generadas en el proceso</param>
        /// <returns>Líneas de captura de los documentos determinantes</returns>
        public static List<LineaCaptura> RecuperaLineasDeCaptura(List<DocumentoDeterminante> documentosDet, Usuario usuario, ref ExcepcionTipificada exGeneral)
        {
            bool hayMarcados;
            bool hayNoMarcados;

            MensajesUsuario msgMarcados = null;
            MensajesUsuario msgNoMarcados = null;
            

            LineaCaptura lineaNoMarcados = null;
            List<LineaCaptura> lineasMarcados = null;
            List<LineaCaptura> lineasDeCaptura = new List<LineaCaptura>();

            List<DocumentoDeterminante> documentosDetMarcados = documentosDet.Select(m => m).Where(m => m.Marcado == true).ToList(); ;
            List<DocumentoDeterminante> documentosDetNoMarcados = documentosDet.Select(m => m).Where(m => m.Marcado == false).ToList();

            hayNoMarcados = documentosDetNoMarcados.Count() > 0 ? true : false;
            hayMarcados = documentosDetMarcados.Count() > 0 ? true : false;
            

            //Hay marcados y no marcados
            if (hayMarcados && hayNoMarcados)
            {
                Parallel.Invoke(
                    () => lineaNoMarcados = RecuperaLineaCapturaNoMarcados(documentosDetNoMarcados, usuario, ref msgNoMarcados),
                    () => lineasMarcados = RecuperaLineasCapturaMarcados(documentosDetMarcados, usuario, ref msgMarcados));

                if (lineaNoMarcados != null)
                    lineasDeCaptura.Add(lineaNoMarcados);

                if (lineasMarcados != null)
                    lineasDeCaptura.AddRange(lineasMarcados);

                MensajesUsuario msgCombinado = AplicaValidacionesNegocio(lineasMarcados, lineaNoMarcados,msgMarcados, msgNoMarcados);

                if (msgCombinado != null)
                {
                    string mensaje = MensajesError.RecuperaMensajeError((EscenariosNoGeneracionLC)(msgCombinado.CodigoMensaje), usuario.EsContribuyentePuro);
                    exGeneral = new ExcepcionTipificada(mensaje, null, msgCombinado.Ticket);
                }
            }

            //Solo hay marcados
            if (hayMarcados && !hayNoMarcados)
            {
                lineasMarcados = RecuperaLineasCapturaMarcados(documentosDetMarcados, usuario, ref msgMarcados);
                if (lineasMarcados != null)
                    lineasDeCaptura.AddRange(lineasMarcados);

                if (msgMarcados != null)
                {
                    string mensaje = MensajesError.RecuperaMensajeError((EscenariosNoGeneracionLC)(msgMarcados.CodigoMensaje), usuario.EsContribuyentePuro);
                    exGeneral = new ExcepcionTipificada(mensaje, null, msgMarcados.Ticket);
                }

            }
            //Solo hay no marcados
            if (!hayMarcados && hayNoMarcados)
            {
                lineaNoMarcados = RecuperaLineaCapturaNoMarcados(documentosDetNoMarcados, usuario, ref msgNoMarcados);
                if (lineaNoMarcados != null)
                    lineasDeCaptura.Add(lineaNoMarcados);

                if (msgNoMarcados != null)
                {
                    string mensaje = MensajesError.RecuperaMensajeError((EscenariosNoGeneracionLC)(msgNoMarcados.CodigoMensaje), usuario.EsContribuyentePuro);
                    exGeneral = new ExcepcionTipificada(mensaje, null, msgNoMarcados.Ticket);
                }

            }


            return lineasDeCaptura;
        }

        /// <summary>
        /// Recupera los documentos determinantes contenidos en una línea de captura
        /// </summary>
        /// <param name="lineaCaptura">Línea de captura</param>
        /// <param name="rfc">Rfc del contribuyente</param>
        /// <returns></returns>
        public static List<string> RecuperaDocumentosEnLC(string lineaCaptura, string rfc)
        {
            RespuestaResolucionesEnLC resolucionesEnLC = InvocarAlTraductorRecuperarDocumentosEnLC(lineaCaptura, rfc);
            return resolucionesEnLC.Resoluciones.Select(m => m.NumeroResolucion).ToList();
        }
        
        #endregion
       
        #region Llamados al Traductor

        private static RespuestaLineasCapturaExistentes InvocarAlTraductorMarcados(string rfc, SolicitudLineasCapturaExistentes solicitudTraductor,ClasificacionMarcados clasificacion, ref MensajesUsuario mensajeUsuario)
        {

            string strDocumentos = MetodosComunes.Serializa(solicitudTraductor);
            string strRespuestaTraductor= string.Empty;
            RespuestaLineasCapturaExistentes respuesta=null;

            Peticion peticion = new Peticion { RFC = rfc, TipoOrigen = (int)EnumTipoOrigen.Traductor, Accion = (int)EnumAccionesTraductor.ConsultarLCMarcados, Fecha = DateTime.Now };
            InterceptorMensajes interceptor = new InterceptorMensajes();
            ServicioTraductorLC.CreditosFiscalesClient cliente;
            Stopwatch duracion = new Stopwatch();

            try
            {
                cliente = new ServicioTraductorLC.CreditosFiscalesClient();
                cliente.Endpoint.Behaviors.Add(interceptor);
                using (OperationContextScope scope = new OperationContextScope(cliente.InnerChannel))
                {
                    HttpRequestMessageProperty property = new HttpRequestMessageProperty();
                    string autenticacion = Catalogos.ApplicationSettings.ConsultaConfiguracion<string>("Traductor:Autenticacion");
                    property.Headers.Add("Authorization", autenticacion);
                    OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = property;
                    duracion.Start();
                    strRespuestaTraductor = cliente.ConsultaLineasCapturaXDocumento(strDocumentos);
                    duracion.Stop();
                    cliente.Close();
                }

                //Se agrego la deserialización dentro del try para que se guarde el error dentro de peticion
                respuesta = MetodosComunes.Deserializa<RespuestaLineasCapturaExistentes>(strRespuestaTraductor);

                
            }
            catch (Exception ex)
            {
                if (duracion.IsRunning)
                    duracion.Stop();
                peticion.HuboError = true;
                peticion.Observaciones = ex.Message;
                string ticket = AccesoLogEventos.LogEventos.EscribirEntradaLog((int)EnumErroresTraductor.ErrorMarcados, "Error al invocar Traductor::ConsultaLineasCapturaXDocumento.", ex);
                mensajeUsuario = new MensajesUsuario((int)clasificacion, ticket);
            }
            finally
            {
                peticion.Duracion = Convert.ToDecimal(duracion.Elapsed.TotalSeconds);
                peticion.XmlPeticion = interceptor.XMLPeticion;
                peticion.XmlRespuesta = interceptor.XMLRespuesta;
                Peticiones.GuardaPeticion(peticion);
            }

            return respuesta;

        }

        private static RespuestaLC InvocarAlTraductorNoMarcados(SolicitudCreditosFiscales solicitudTraductor,ref MensajesUsuario mensajeUsuario)
        {

            RespuestaLC respuesta = null;
            string strRespuestaTraductor = string.Empty;
            string scfSerializado = MetodosComunes.Serializa(solicitudTraductor);
            Peticion peticion = new Peticion { RFC = solicitudTraductor.Mensaje.DatosGenerales.RFC, TipoOrigen = (int)EnumTipoOrigen.Traductor, Accion = (int)EnumAccionesTraductor.GenerarLC, Fecha = DateTime.Now };
            InterceptorMensajes interceptor = new InterceptorMensajes();
            ServicioTraductorLC.CreditosFiscalesClient cliente;
            Stopwatch duracion = new Stopwatch();

            try
            {
                cliente = new ServicioTraductorLC.CreditosFiscalesClient();
                cliente.Endpoint.Behaviors.Add(interceptor);
                using (OperationContextScope scope = new OperationContextScope(cliente.InnerChannel))
                {
                    HttpRequestMessageProperty property = new HttpRequestMessageProperty();
                    string autenticacion = Catalogos.ApplicationSettings.ConsultaConfiguracion<string>("Traductor:Autenticacion");
                    property.Headers.Add("Authorization", autenticacion);
                    OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = property;
                    duracion.Start();
                    strRespuestaTraductor = cliente.ObtieneLineaCaptura(Convert.ToInt16(solicitudTraductor.Encabezado.Aplicacion), Convert.ToInt16(solicitudTraductor.Encabezado.TipoDocumento), scfSerializado);
                    duracion.Stop();
                    cliente.Close();
                }

                respuesta = MetodosComunes.Deserializa<RespuestaLC>(strRespuestaTraductor);
                
            }
            catch (Exception ex)
            {
                if (duracion.IsRunning)
                    duracion.Stop();
                peticion.HuboError = true;
                peticion.Observaciones = ex.Message;
                string ticket = AccesoLogEventos.LogEventos.EscribirEntradaLog((int)EnumErroresTraductor.ErrorNoMarcados, "Error al invocar Traductor::ObtieneLineaCaptura", ex);
                mensajeUsuario= new MensajesUsuario((int)EscenariosNoGeneracionLC.NoMarcadosError, ticket);
                
            }
            finally
            {
                peticion.Duracion = Convert.ToDecimal(duracion.Elapsed.TotalSeconds);
                peticion.XmlPeticion = interceptor.XMLPeticion;
                peticion.XmlRespuesta = interceptor.XMLRespuesta;
                Peticiones.GuardaPeticion(peticion);
            }


            return respuesta;
            
        }

        private static RespuestaResolucionesEnLC InvocarAlTraductorRecuperarDocumentosEnLC(string lineaCaptura, string rfc)
        {
           
            string strRespuestaTraductor;
            Peticion peticion = new Peticion { RFC = rfc, TipoOrigen = (int)EnumTipoOrigen.Traductor, Accion = (int)EnumAccionesTraductor.RecuperaResolucuionesEnLC, Fecha = DateTime.Now };
            InterceptorMensajes interceptor = new InterceptorMensajes();
            ServicioTraductorLC.CreditosFiscalesClient cliente;
            RespuestaResolucionesEnLC respuesta = null;
            Stopwatch duracion = new Stopwatch();

            try
            {
                cliente = new ServicioTraductorLC.CreditosFiscalesClient();
                cliente.Endpoint.Behaviors.Add(interceptor);


                using (OperationContextScope scope = new OperationContextScope(cliente.InnerChannel))
                {
                    HttpRequestMessageProperty property = new HttpRequestMessageProperty();
                    string autenticacion = Catalogos.ApplicationSettings.ConsultaConfiguracion<string>("Traductor:Autenticacion");
                    property.Headers.Add("Authorization", autenticacion);
                    OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = property;
                    duracion.Start();
                    strRespuestaTraductor = cliente.ObtieneDocumentosEnLineaCaptura(lineaCaptura);
                    duracion.Stop();
                    cliente.Close();
                }

                respuesta = MetodosComunes.Deserializa<RespuestaResolucionesEnLC>(strRespuestaTraductor);
            }
            catch (Exception ex)
            {
                if (duracion.IsRunning)
                    duracion.Stop();
                peticion.HuboError = true;
                peticion.Observaciones = ex.Message;
                string ticket = AccesoLogEventos.LogEventos.EscribirEntradaLog((int)EnumErroresTraductor.ErrorConsultaDocumentosEnLC, "Error al invocar Traductor::ObtieneDocumentosEnLineaCaptura", ex);
                throw new ExcepcionTipificada("Error al invocar Traductor::ObtieneDocumentosEnLineaCaptura", ex, ticket);
            }
            finally
            {
                peticion.Duracion = Convert.ToDecimal(duracion.Elapsed.TotalSeconds);
                peticion.XmlPeticion = interceptor.XMLPeticion;
                peticion.XmlRespuesta = interceptor.XMLRespuesta;
                Peticiones.GuardaPeticion(peticion);
            }

            
            return respuesta;            

        }

        #endregion

        #region Marcados

        private static List<LineaCaptura> RecuperaLineasCapturaMarcados(List<DocumentoDeterminante> documentosDetMarcados, Usuario usuario, ref MensajesUsuario mensajeUsuario)
        {
            ClasificacionMarcados clasificacion= ClasificacionMarcados.SinClasificar;
            try
            {
                clasificacion=ClasificaMarcados(documentosDetMarcados);
                SolicitudLineasCapturaExistentes solicitudTraductor = mapearSolicitudMarcados(documentosDetMarcados, usuario);
                RespuestaLineasCapturaExistentes respuestaTraductor = InvocarAlTraductorMarcados(usuario.Rfc, solicitudTraductor,clasificacion + 7 ,ref mensajeUsuario);
                if (mensajeUsuario == null)
                {
                    List<LineaCaptura> lineasCaptura = new List<LineaCaptura>();
                    if ((respuestaTraductor.LineasCaptura == null? 0 : respuestaTraductor.LineasCaptura.Count()) ==0)
                    {
                        Exception exTraductor = new Exception("El traductor no logro recuperar líneas de captura::Documentos marcados");
                        string ticket = AccesoLogEventos.LogEventos.EscribirEntradaLog((int)EnumErroresLineaCaptura.ErrorAlGenerarLineasDeCapturaMarcadosTraductor, "Error reportado por el traductor al recuperar las líneas de captura::Documentos marcados", exTraductor);
                        mensajeUsuario = new MensajesUsuario((int)clasificacion, ticket);
                    }
                    else   
                    {
                        foreach (var lcTraductor in respuestaTraductor.LineasCaptura)
                        {
                            LineaCaptura lineaCaptura = new LineaCaptura()
                            {
                                Linea = lcTraductor.LineaCaptura,
                                Folio = lcTraductor.Folio,
                                FechaEmision = lcTraductor.FechaGeneracion,
                                FechaVencimiento = lcTraductor.FechaVigenecia,
                                ImporteTotal = Convert.ToDecimal(lcTraductor.LineaCaptura),
                            };

                            DocumentoDeterminante docOrigen = documentosDetMarcados.FirstOrDefault(
                                m => m.IdALR == Convert.ToByte(lcTraductor.ALR)
                                && m.IdAutoridad == Convert.ToInt32(lcTraductor.Autoridad)
                                && m.Rfc == lcTraductor.RFC
                                && m.FechaDocumento == DateTime.ParseExact(lcTraductor.FechaDocumento.Substring(0, 10), "dd/MM/yyyy", CultureInfo.InstalledUICulture, DateTimeStyles.None)
                                && m.NumDocumento == lcTraductor.NumeroResolucion);

                            lineaCaptura.Documentos.Add(
                                new IdentificadorDocumento()
                                {
                                    IdDocumento = docOrigen.IdDocumento,
                                    NumDocumento = docOrigen.NumDocumento
                                }
                                );

                            lineasCaptura.Add(lineaCaptura);
                        }
                        if (lineasCaptura.Count() > 0)
                            return lineasCaptura;
                    }


                    //if (respuestaTraductor.ListaErrores != null)//Se recuperan los errores reportados por el traductor
                    //{
                    //    StringBuilder erroresTraductor = new StringBuilder();
                    //    foreach (string errorTraductor in respuestaTraductor.ListaErrores)
                    //    {
                    //        erroresTraductor.AppendFormat(" Error:{0} ", errorTraductor);
                    //    }
                    //    if (!string.IsNullOrWhiteSpace(erroresTraductor.ToString()))
                    //    {
                    //        Exception exTraductor = new Exception(erroresTraductor.ToString());
                    //        string ticket = AccesoLogEventos.LogEventos.EscribirEntradaLog((int)EnumErroresLineaCaptura.ErrorAlGenerarLineasDeCapturaMarcadosTraductor, "Error reportado por el traductor al recuperar las líneas de captura::Documentos marcados", exTraductor);
                    //        mensajeUsuario = new MensajesUsuario((int)clasificacion, ticket);
                    //    }
                    //}

                }
            }
            catch (Exception ex)
            {
                string ticket = AccesoLogEventos.LogEventos.EscribirEntradaLog((int)EnumErroresLineaCaptura.ErrorAlGenerarLineasDeCapturaMarcados, "Error al recuperar las líneas de captura::Documentos marcados", ex);
                mensajeUsuario = new MensajesUsuario((int)clasificacion, ticket);
            }
            return null;
        }

        private static SolicitudLineasCapturaExistentes mapearSolicitudMarcados(List<DocumentoDeterminante> documentosDetMarcados, Usuario usuario)
        {
            SolicitudLineasCapturaExistentes solicitudLCExistentes = new SolicitudLineasCapturaExistentes();

            solicitudLCExistentes.Resoluciones =
                documentosDetMarcados.Select(m => new SolicitudLineasCapturaExistentesResolucion()
                {
                    ALR = m.IdALR.ToString(),
                    Autoridad = m.IdAutoridad.ToString(),
                    FechaDocumento = m.FechaDocumento.ToString("dd/MM/yyyy"),
                    NumeroResolucion = m.NumDocumento,
                    RFC = m.Rfc
                }).ToArray();

            //List<SolicitudLineasCapturaExistentesResolucion> resoluciones = new List<SolicitudLineasCapturaExistentesResolucion>();
            //foreach(DocumentoDeterminante docDeterminante in documentosDetMarcados)
            //{
            //    resoluciones.Add(
            //        new SolicitudLineasCapturaExistentesResolucion()
            //        {
            //            ALR = docDeterminante.IdALR.ToString(),
            //            Autoridad = docDeterminante.IdAutoridad.ToString(),
            //            FechaDocumento =  docDeterminante.FechaDocumento.ToString("dd/MM/yyyy"),
            //            NumeroResolucion= docDeterminante.NumDocumento,
            //            RFC = docDeterminante.Rfc
            //        }
            //    );

            //}
            //solicitudLCExistentes.Resoluciones = resoluciones.ToArray();

            return solicitudLCExistentes;
        }

        #endregion

        #region No Marcados

        private static LineaCaptura RecuperaLineaCapturaNoMarcados(List<DocumentoDeterminante> documentosDetNoMarcados, Usuario usuario, ref MensajesUsuario mensajeUsuario)//Antes solicitud
        {
            try
            {
                SolicitudCreditosFiscales solicitudTraductor = mapearSolicitudNoMarcados(documentosDetNoMarcados, usuario);
                RespuestaLC respuestaTraductor = InvocarAlTraductorNoMarcados(solicitudTraductor, ref mensajeUsuario);

                if (mensajeUsuario == null)
                {
                    List<LineaCaptura> lineasCaptura = new List<LineaCaptura>();
                    //Validar que la lista no sea nula o vacia para recorrerla
                    if ((respuestaTraductor.LineasCaptura == null ? 0 : respuestaTraductor.LineasCaptura.Count()) > 0)
                    {
                        foreach (RespuestaLCDatosLinea lcTraductor in respuestaTraductor.LineasCaptura)
                        {
                            LineaCaptura lineaCaptura = new LineaCaptura()
                            {
                                Linea = lcTraductor.LineaCaptura,
                                Folio = lcTraductor.Folio
                            };

                            lineaCaptura.FechaVencimiento = solicitudTraductor.Mensaje.DatosGenerales.LineaCaptura.FechaVigencia;
                            lineaCaptura.ImporteTotal = solicitudTraductor.Mensaje.DatosGenerales.LineaCaptura.Importe;
                            lineaCaptura.FechaEmision = DateTime.Now.ToString("dd/MM/yyyy");

                            foreach (DocumentoDeterminante docDet in documentosDetNoMarcados)
                            {
                                if (!lineaCaptura.Documentos.Exists(m => m.IdDocumento == docDet.IdDocumento))
                                    lineaCaptura.Documentos.Add(new IdentificadorDocumento() { IdDocumento = docDet.IdDocumento, NumDocumento = docDet.NumDocumento });
                            }

                            lineasCaptura.Add(lineaCaptura);
                        }
                        if (lineasCaptura.Count() > 0)
                        {
                            return lineasCaptura.FirstOrDefault();
                        }
                    }
                    else //Si no hay LC  trae mensajes de error. if (respuestaTraductor.ListaErrores != null)
                    {
                        string errorEsperado = respuestaTraductor.ListaErrores[0].Split('|')[0];
                        StringBuilder erroresTraductor = new StringBuilder();
                        foreach (string errorTraductor in respuestaTraductor.ListaErrores)
                        {
                            erroresTraductor.AppendFormat(" Error:{0} ", errorTraductor);
                        }
                        
                        Exception exTraductor = new Exception(erroresTraductor.ToString());
                        string ticket = AccesoLogEventos.LogEventos.EscribirEntradaLog((int)EnumErroresLineaCaptura.ErrorAlGenerarLineasDeCapturaMarcadosTraductor, "Error reportado por el traductor al generar/recuperar las líneas de captura::Documentos no marcados", exTraductor);

                        int msg = 0;
                        switch (errorEsperado)
                        {
                            case "5023": msg = (int)EscenariosNoGeneracionLC.NoMarcadosDyPError; break;//Comunicacion o timeout
                            case "5019": msg = (int)EscenariosNoGeneracionLC.NoMarcadosDyP; break;
                            default: msg = (int)EscenariosNoGeneracionLC.NoMarcados; break;
                        }

                        mensajeUsuario = new MensajesUsuario(msg, ticket);
                    }
                }

            }
            catch (Exception ex)
            {
                string ticket = AccesoLogEventos.LogEventos.EscribirEntradaLog((int)EnumErroresLineaCaptura.ErrorAlGenerarLineasDeCapturaNoMarcados, "Error al generar/recuperar las líneas de captura::Documentos no marcados", ex);
                mensajeUsuario = new MensajesUsuario((int)EscenariosNoGeneracionLC.NoMarcados, ticket);
            }

            return null;

        }
        
        private static SolicitudCreditosFiscales mapearSolicitudNoMarcados(List<DocumentoDeterminante> documentosDet, Usuario usuario)
        {
            SolicitudCreditosFiscales scf = new SolicitudCreditosFiscales();
            scf.Encabezado = new SolicitudCreditosFiscalesEncabezado();
            scf.Encabezado.Aplicacion = Catalogos.ApplicationSettings.ConsultaConfiguracion<int>("Traductor:IdAplicacion");
            scf.Encabezado.DescripcionTipoDocumento = Catalogos.ApplicationSettings.ConsultaConfiguracion<string>("Traductor:TipoDocumento");
            scf.Encabezado.TipoDocumento = Catalogos.ApplicationSettings.ConsultaConfiguracion<int>("Traductor:IdTipoDocumento");


            scf.Mensaje = new SolicitudCreditosFiscalesMensaje();
            scf.Mensaje.DatosGenerales = new SolicitudCreditosFiscalesMensajeDatosGenerales();
            scf.Mensaje.DatosGenerales.Nombre = usuario.Nombres;
            scf.Mensaje.DatosGenerales.ApellidoPaterno = usuario.ApellidoPaterno;
            scf.Mensaje.DatosGenerales.ApellidoMaterno = usuario.ApellidoMaterno;
            scf.Mensaje.DatosGenerales.RazonSocial = usuario.RazonSocial;
            scf.Mensaje.DatosGenerales.RFC = usuario.Rfc;
            scf.Mensaje.DatosGenerales.IdPersona = usuario.IdPersonaMAT;
            scf.Mensaje.DatosGenerales.LineaCaptura = new SolicitudCreditosFiscalesMensajeDatosGeneralesLineaCaptura();
            scf.Mensaje.DatosGenerales.ALR = usuario.IdALR;
            //Recordar solicitar que este dato se obtenga de IDC
            //RV
            scf.Mensaje.DatosGenerales.BOID = usuario.EsContribuyentePuro? usuario.Rfc : usuario.BoId;
            scf.Mensaje.DatosGenerales.DeudorPuro = Convert.ToInt32(usuario.EsContribuyentePuro);
            
            LineaCaptura lc = GeneraLineasCaptura.LineasCaptura.RecuperaDatosBaseLineaDeCaptura(documentosDet);
            scf.Mensaje.DatosGenerales.LineaCaptura.Importe = Convert.ToInt64(lc.ImporteTotal);
            scf.Mensaje.DatosGenerales.LineaCaptura.FechaVigencia = lc.FechaVencimiento;
            scf.Mensaje.DatosGenerales.LineaCaptura.PagoObligadoInternet = usuario.PersonaFisica?2:4;
            scf.Mensaje.DatosGenerales.LineaCaptura.Tipo = usuario.IdTipoLineaMAT;
            scf.Mensaje.DatosGenerales.Observaciones = Catalogos.ApplicationSettings.ConsultaConfiguracion<string>("Traductor:Observaciones");

            scf.Mensaje.ResolucionesDeterminante = mapearDocumentosDeterminantes(documentosDet);

            return scf;
        }

        private static SolicitudCreditosFiscalesMensajeResolucionDeterminante[] mapearDocumentosDeterminantes(List<DocumentoDeterminante> documentosDetNoMarcados)
        {
            List<SolicitudCreditosFiscalesMensajeResolucionDeterminante> docDeterminantesTraductor = new List<SolicitudCreditosFiscalesMensajeResolucionDeterminante>();
            foreach (DocumentoDeterminante doc in documentosDetNoMarcados)
            {
                SolicitudCreditosFiscalesMensajeResolucionDeterminante docTraductor = new SolicitudCreditosFiscalesMensajeResolucionDeterminante()
                {
                    ALR = doc.IdALR,
                    AutoridadId = doc.IdAutoridad,
                    Fecha = doc.FechaDocumento.ToString("dd/MM/yyyy"),
                    RFC = doc.Rfc,
                    NumeroResolucion = doc.NumDocumento,
                    IdResolucion=doc.IdResolucionMAT,
                    SaldoInformativo = doc.SaldoInformativo,
                    FormaPago = string.IsNullOrWhiteSpace(doc.FormaPago) ? "920048" : doc.FormaPago,

                };

                docTraductor.Conceptos = mapearConceptos(doc.Conceptos);
                docDeterminantesTraductor.Add(docTraductor);
            }
            return docDeterminantesTraductor.ToArray();
        }

        private static SolicitudCreditosFiscalesMensajeResolucionDeterminanteConcepto[] mapearConceptos(List<DocumentoDeterminanteConcepto> conceptos)
        {
            List<SolicitudCreditosFiscalesMensajeResolucionDeterminanteConcepto> conceptosTraductor = new List<SolicitudCreditosFiscalesMensajeResolucionDeterminanteConcepto>();
            foreach (DocumentoDeterminanteConcepto concepto in conceptos)
            {
                SolicitudCreditosFiscalesMensajeResolucionDeterminanteConcepto conceptoTraductor = new SolicitudCreditosFiscalesMensajeResolucionDeterminanteConcepto()
                {
                    Clave = concepto.IdConcepto.ToString(),
                    IdConcepto=concepto.IdConceptoMAT,
                    CredicoARCA = concepto.CreditoARCA,
                    CreditoSir = concepto.CreditoSIR,
                    //ImporteCondonacion = concepto.ImporteCondonacion, 
                    //ImporteDescuento = concepto.ImporteDescuentos,
                    ImporteHistorico = concepto.ImporteHistorico,
                    ImportePagar = concepto.ImportePagar,
                    ImporteParteActualizada = concepto.ImporteParteActualizada,
                    MotivoId = concepto.IdMotivo,
                    PeriodicidadId = concepto.IdPeriodicidad,
                    PeriodoId = concepto.IdPeriodo

                };

                if(concepto.Ejercicio>0)
                    conceptoTraductor.Ejercicio = concepto.Ejercicio;

                if (concepto.FechaCausacion.HasValue)
                    conceptoTraductor.FechaCausacion = concepto.FechaCausacion.Value.ToString("dd/MM/yyyy");
                if(concepto.FechaNotificacion.HasValue)
                    conceptoTraductor.FechaNotificacion = concepto.FechaNotificacion.Value.ToString("dd/MM/yyyy");

                //Se agregan los descuentos de los conceptos padre
                List<SolicitudCreditosFiscalesMensajeResolucionDeterminanteConceptoDescuento> descuentosConceptoTraductor = new List<SolicitudCreditosFiscalesMensajeResolucionDeterminanteConceptoDescuento>();
                foreach (DescuentoConcepto descuento in concepto.Descuentos)
                {
                    descuentosConceptoTraductor.Add(
                        new SolicitudCreditosFiscalesMensajeResolucionDeterminanteConceptoDescuento()
                        {
                            IdDescuento = descuento.IdDescuento,
                            ImporteDescuento = descuento.ImporteDescuento

                        }
                    );
                }

                if (descuentosConceptoTraductor.Count()>0)
                    conceptoTraductor.Descuentos = descuentosConceptoTraductor.ToArray();
                conceptoTraductor.Hijos = mapearConceptosHijo(concepto.ConceptosHijo);
                conceptosTraductor.Add(conceptoTraductor);
            }
            return conceptosTraductor.ToArray();
        }

        private static SolicitudCreditosFiscalesMensajeResolucionDeterminanteConceptoHijo[] mapearConceptosHijo(List<DocumentoDeterminanteConceptoHijo> conceptosHijo)
        {
            List<SolicitudCreditosFiscalesMensajeResolucionDeterminanteConceptoHijo> conceptosHijoTraductor = new List<SolicitudCreditosFiscalesMensajeResolucionDeterminanteConceptoHijo>();
            foreach (DocumentoDeterminanteConceptoHijo conceptoHijo in conceptosHijo)
            {
                SolicitudCreditosFiscalesMensajeResolucionDeterminanteConceptoHijo conceptoHijoTraductor = new SolicitudCreditosFiscalesMensajeResolucionDeterminanteConceptoHijo()
                {
                    Clave = conceptoHijo.IdConcepto.ToString(),
                    CreditoSir = conceptoHijo.CreditoSIR,
                    //FechaCausacion = conceptoHijo.FechaCausacion != null ? conceptoHijo.FechaCausacion.Value.ToString("dd/MM/yyyy") : string.Empty,
                    //FechaNotificacion = conceptoHijo.FechaNotificacion != null ? conceptoHijo.FechaNotificacion.Value.ToString("dd/MM/yyyy") : string.Empty,
                    //ImporteCondonacion = conceptoHijo.ImporteCondonacion,
                    //ImporteDescuento = conceptoHijo.ImporteDescuentos,
                    ImporteHistorico = conceptoHijo.ImporteHistorico,
                    ImportePagar = conceptoHijo.ImportePagar,
                    ImporteParteActualizada = conceptoHijo.ImporteParteActualizada,
                    MotivoId = conceptoHijo.IdMotivo,
                    PeriodicidadId =  conceptoHijo.IdPeriodicidad,
                    PeriodoId = conceptoHijo.IdPeriodo
                };

                if (conceptoHijo.Ejercicio > 0)
                    conceptoHijoTraductor.Ejercicio = conceptoHijo.Ejercicio;
               
                if (conceptoHijo.FechaCausacion.HasValue)
                    conceptoHijoTraductor.FechaCausacion = conceptoHijo.FechaCausacion.Value.ToString("dd/MM/yyyy");
                if (conceptoHijo.FechaNotificacion.HasValue)
                    conceptoHijoTraductor.FechaNotificacion = conceptoHijo.FechaNotificacion.Value.ToString("dd/MM/yyyy");

                //Se agregan los descuentos de los conceptos hijo
                List<SolicitudCreditosFiscalesMensajeResolucionDeterminanteConceptoHijoDescuento> descuentosConceptoHijoTraductor = new List<SolicitudCreditosFiscalesMensajeResolucionDeterminanteConceptoHijoDescuento>();
                foreach (DescuentoConceptoHijo descuento in conceptoHijo.Descuentos)
                {
                    descuentosConceptoHijoTraductor.Add(
                        new SolicitudCreditosFiscalesMensajeResolucionDeterminanteConceptoHijoDescuento()
                        {
                            IdDescuento = descuento.IdDescuento,
                            ImporteDescuento = descuento.ImporteDescuento
                        }

                    );
                }
                if (descuentosConceptoHijoTraductor.Count()>0)
                    conceptoHijoTraductor.Descuentos = descuentosConceptoHijoTraductor.ToArray();
                conceptosHijoTraductor.Add(conceptoHijoTraductor);
            }
            return conceptosHijoTraductor.ToArray();
        }

        #endregion

        #region Validaciones de Negocio

        private static MensajesUsuario AplicaValidacionesNegocio(List<LineaCaptura> lineasMarcados, LineaCaptura lineaNoMarcados, MensajesUsuario msgMarcados, MensajesUsuario msgNoMarcados)
        {
            MensajesUsuario mensajeUsuario;
            // Las listas solo vienen nulas cuando hay mensajes de usuario
            if (lineaNoMarcados == null && lineasMarcados != null)// Solo se recuperaron las LC de los marcados y no se genero/recupero para los no marcados
            {
                //Combinados algunas LC - Es cuando nos se encontro o genero pero si de recuperaron para los marcado
                mensajeUsuario = new MensajesUsuario((int)EscenariosNoGeneracionLC.CombinadosAlgunasLC, msgNoMarcados.Ticket);
                return mensajeUsuario;
            }
            else if (lineaNoMarcados != null && lineasMarcados == null)// Solo se genero/recuperaro la LC de los no marcados y no se recuperaron las lc para los marcados
            {
                EscenariosNoGeneracionLC escenario = EscenariosNoGeneracionLC.SinClasificar;
                switch ((EscenariosNoGeneracionLC)msgMarcados.CodigoMensaje)
                {
                    case EscenariosNoGeneracionLC.MarcadosEstatal: escenario = EscenariosNoGeneracionLC.CombinadosNingunaLCEstatal; break;
                    case EscenariosNoGeneracionLC.MarcadosOtrosYEstatales: escenario = EscenariosNoGeneracionLC.CombinadosNingunaLCEstatal; break;
                    case EscenariosNoGeneracionLC.MarcadosPlazosYEstatales: escenario = EscenariosNoGeneracionLC.CombinadosNingunaLCEstatal; break;
                    case EscenariosNoGeneracionLC.MarcadosPlazosYEstatalesYOtros: escenario = EscenariosNoGeneracionLC.CombinadosNingunaLCEstatal; break;

                    case EscenariosNoGeneracionLC.MarcadosPlazos: escenario = EscenariosNoGeneracionLC.CombinadosNingunaLCNoEstatal; break;
                    case EscenariosNoGeneracionLC.MarcadosOtros: escenario = EscenariosNoGeneracionLC.CombinadosNingunaLCNoEstatal; break;
                    case EscenariosNoGeneracionLC.MarcadosOtrosYPlazos: escenario = EscenariosNoGeneracionLC.CombinadosNingunaLCNoEstatal; break;


                    case EscenariosNoGeneracionLC.MarcadosEstatalError: escenario = EscenariosNoGeneracionLC.CombinadosNingunaLCEstatalError; break;
                    case EscenariosNoGeneracionLC.MarcadosOtrosYEstatalesError: escenario = EscenariosNoGeneracionLC.CombinadosNingunaLCEstatalError; break;
                    case EscenariosNoGeneracionLC.MarcadosPlazosYEstatalesError: escenario = EscenariosNoGeneracionLC.CombinadosNingunaLCEstatalError; break;
                    case EscenariosNoGeneracionLC.MarcadosPlazosYEstatalesYOtrosError: escenario = EscenariosNoGeneracionLC.CombinadosNingunaLCEstatalError; break;

                    case EscenariosNoGeneracionLC.MarcadosPlazosError: escenario = EscenariosNoGeneracionLC.CombinadosNingunaLCNoEstatalError; break;
                    case EscenariosNoGeneracionLC.MarcadosOtrosError: escenario = EscenariosNoGeneracionLC.CombinadosNingunaLCNoEstatalError; break;
                    case EscenariosNoGeneracionLC.MarcadosOtrosYPlazosError: escenario = EscenariosNoGeneracionLC.CombinadosNingunaLCNoEstatalError; break;

                }

                mensajeUsuario = new MensajesUsuario((int)escenario, msgMarcados.Ticket);
                return mensajeUsuario;

            }
            else if (lineaNoMarcados == null && lineasMarcados == null)//No hay lineas de captura para ambos adeudos los mensajes de usuario no pueden ser nulos
            {
                string mensaje = string.Format("Error en marcados :: ticket = {0}, Error en No marcado :: ticket= {1}", msgMarcados.Ticket, msgNoMarcados.Ticket);
                string ticket = AccesoLogEventos.LogEventos.EscribirEntradaLog((int)EnumErroresLineaCaptura.ErrorAlGenerarLineasDeCapturaAgrupador, "Error al generar/recuperar las lineas de captura(Agrupador)", new Exception(mensaje));

                mensajeUsuario = new MensajesUsuario(msgNoMarcados.CodigoMensaje, ticket);
            }


            return null;
        }

        private static ClasificacionMarcados ClasificaMarcados(List<DocumentoDeterminante> documentosDetMarcados)
        {
            using (DalGeneracionFormato dalMarcas = new DalGeneracionFormato())
            {
                foreach (DocumentoDeterminante doc in documentosDetMarcados)
                {
                    doc.Marcas = dalMarcas.ObtenerMarcas(doc.IdSolicitud, doc.IdDocumento).Where(m => m.GenerarLC == false).ToList();
                }
                int numDoc = documentosDetMarcados.Count();
                int totMarcas = documentosDetMarcados.Sum(m => m.Marcas.Count());
                int totCobroACargoEstado = documentosDetMarcados.Sum(m => m.Marcas.Where(c => c.CveMarca == RecuperaIdMarca('A')).Count());
                int par1PagoPlazos = documentosDetMarcados.Sum(m => m.Marcas.Where(c => c.CveMarca == RecuperaIdMarca('B')).Count()); //Recordar sumar Bo c
                int par2PagoPlazos = documentosDetMarcados.Sum(m => m.Marcas.Where(c => c.CveMarca == RecuperaIdMarca('C')).Count());//
                int totPagoPlazos = par1PagoPlazos + par2PagoPlazos;
                int totOtrasMarcas = documentosDetMarcados.Sum(m => m.Marcas.Where(c => c.CveMarca != RecuperaIdMarca('A') && c.CveMarca != RecuperaIdMarca('B') && c.CveMarca != RecuperaIdMarca('C')).Count());

                if (totMarcas == totCobroACargoEstado)//Todas son a cargo del estado
                {
                    return ClasificacionMarcados.MarcadosEstatal;
                }
                else if (totMarcas == totPagoPlazos) //Todas son pago a plazos
                {
                    return ClasificacionMarcados.MarcadosPlazos;
                }
                else if (totMarcas == totCobroACargoEstado + totPagoPlazos)// todas son a cargo del estado o pago a plazos
                {
                    return ClasificacionMarcados.MarcadosPlazosYEstatales;
                }
                else if (totMarcas == totOtrasMarcas)//Otros
                {
                    return ClasificacionMarcados.MarcadosOtros;
                }
                else if (totMarcas == totPagoPlazos + totOtrasMarcas)//Otros y plazos
                {
                    return ClasificacionMarcados.MarcadosOtrosYPlazos;
                }
                else if (totMarcas == totCobroACargoEstado + totOtrasMarcas)//Otros y estatales
                {
                    return ClasificacionMarcados.MarcadosOtrosYEstatales;
                }
                else if (totMarcas == totCobroACargoEstado + totPagoPlazos + totOtrasMarcas)//Plazos, estatales y otros
                {
                    return ClasificacionMarcados.MarcadosPlazosYEstatalesYOtros;
                }
                else
                {
                    throw new Exception("No hay clasificación para los documentos marcados que no generan formato de pago seleccionados");
                }

            }

        }

        #endregion

        #region Test

        public static void TestInvocarAlTraductorMarcados()
        {
            
                
        }

        private static int RecuperaIdMarca(char clavaMarca)
        {
            return Convert.ToInt32(clavaMarca) - 64;
        }

        #endregion


    }
}
