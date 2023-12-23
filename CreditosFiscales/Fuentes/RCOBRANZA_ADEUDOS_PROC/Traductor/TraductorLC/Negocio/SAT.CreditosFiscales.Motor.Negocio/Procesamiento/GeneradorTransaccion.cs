
//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.Negocio:SAT.CreditosFiscales.Motor.Negocio.GeneradorTransaccion:1:12/07/2012[Assembly:1.0:12/07/2013])




using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using SAT.CreditosFiscales.Motor.AccesoDatos.Procesamiento;
using SAT.CreditosFiscales.Motor.Entidades.Catalogos;
using SAT.CreditosFiscales.Motor.Entidades.CodigosError;
using SAT.CreditosFiscales.Motor.Entidades.DatosProcesamiento;
using SAT.CreditosFiscales.Motor.Entidades.Enumeradores;
using SAT.CreditosFiscales.Motor.Entidades.Herramientas;
using SAT.CreditosFiscales.Motor.Negocio.AccesoLogEventos;
using SAT.CreditosFiscales.Motor.Utilidades;

namespace SAT.CreditosFiscales.Motor.Negocio.Procesamiento
{
    /// <summary>
    /// Clase que sirve como auxiliar para la generación de transacciones.
    /// </summary>
    public class GeneradorTransaccion
    {

        private static readonly Lazy<GeneradorTransaccion> InstanciaLocal = new Lazy<GeneradorTransaccion>(() => new GeneradorTransaccion());

        /// <summary>
        /// Instancia del método que genera transacciones, para evitar recarga de catálogos de manera innecesaria.
        /// </summary>
        public static GeneradorTransaccion Instancia
        {
            get { return InstanciaLocal.Value; }
        }

        /// <summary>
        /// Propiedad que almacena el listado de reglas posibles a ser ocupadas por el motor de reglas.
        /// </summary>
        public List<CatReglasEquivalencia> ListaReglas { get; protected set; }

        /// <summary>
        /// Lista de reglas de descuentos.
        /// </summary>
        public List<CatReglasEquivalencia> ListaReglasDescuentos { get; set; }  

        /// <summary>
        /// Método que genera transacciones para declaraciones y pagos.
        /// </summary>
        /// <param name="idAplicacion">Identificador del aplicativo que realizó la petición.</param>
        /// <param name="idTipodocumento">Identificador del tipo de documento que contendrá la línea de captura.</param>
        /// <param name="valorObjeto">Valor del objeto que se encontrará en el catálogo de reglas de equivalencia.</param>
        /// <param name="importe">Importe de la transacción.</param>
        /// <param name="objetoSerializado">Concepto original serializado.</param>
        /// <returns>Transacción de dyp correspondiente al valor del objeto proporcionado.</returns>
        public CreditosFiscalesConceptoTransaccion GeneraTransaccion(int idAplicacion, int idTipodocumento,
                                                                            string valorObjeto, decimal importe, string objetoSerializado)
        {
            string idTransaccion = "-1";
            //var lstReglas = DalConceptosEquivalencia.ObtenerReglasEquivalencia(idAplicacion, EnumTipoObjeto.Transaccion,
            //                                                                  idTipodocumento, valorObjeto);            
            var lstReglas = HerramientasReglasEquivalencia.ObtenerReglasEquivalencia(idAplicacion,
                                                                                     EnumTipoObjeto.Transaccion,
                                                                                     valorObjeto, idTipodocumento);
            var objetoFiltro = XDocument.Parse(objetoSerializado);
            idTransaccion = HerramientasReglasEquivalencia.AplicarReglasEquivalencia(objetoFiltro, lstReglas);
            if (idTransaccion == "-1")
            {
                var mensaje =
                    string.Format("No existe transacción definida en el catálogo de reglas para el valor objeto {0}",
                                  valorObjeto);
                LogEventos.EscribirEntradaLog((int) EnumErrorMotor.ErrorCrearTransaccion, mensaje, EventLogEntryType.Error);
                throw new Exception(mensaje);
            }
            var transaccion = DalConceptosEquivalencia.ObtenerTransaccionPorId(idTransaccion,idAplicacion,idTipodocumento);
            if (transaccion == null)
            {
                var mensaje = string.Format(
                    "No existe transacción definida en el catálogo de transacciones para la transacción {0} y el valor objeto {1}",
                    idTransaccion, valorObjeto);
                LogEventos.EscribirEntradaLog((int) EnumErrorMotor.ErrorCrearTransaccion, mensaje, EventLogEntryType.Error);
                throw new Exception(mensaje);
            }
            return CreaTransaccion(transaccion, Convert.ToInt64(importe));
        }

        /// <summary>
        /// Sobrecarga de método que genera transacciones de D y P para descuentos de conceptos padre.
        /// </summary>
        /// <param name="idAplicacion">Identificador de la aplicación que realiza la solicitud.</param>
        /// <param name="idTipodocumento">Tipo de documento que identifica cuales son las transacciones que lo conforman.</param>
        /// <param name="valorObjeto">Constante que identifica a las reglas de descuento de conceptos padres.</param>
        /// <param name="descuento">Entidad que representa al descuento padre.</param>
        /// <returns>Transacción que identifica a los descuentos de conceptos padres</returns>
        public CreditosFiscalesConceptoTransaccion GeneraTransaccion(int idAplicacion, int idTipodocumento, string valorObjeto, CreditosFiscalesAgrupadorConceptosDescuento descuento)
        {
            var doc = new XmlDocument();
            doc.InnerXml = MetodosComunes.SerlializaObjeto(descuento);
            //var lstReglas = RecuperarReglasDescuento(idAplicacion, idTipodocumento, EnumTipoObjeto.Transaccion,
            //                                         valorObjeto);
            var lstReglas = HerramientasReglasEquivalencia.ObtenerReglasEquivalencia(idAplicacion,
                                                                                     EnumTipoObjeto.Transaccion,
                                                                                     valorObjeto, idTipodocumento);
            var idTransaccion = "-1";
            var objetoFiltro = XDocument.Parse(doc.InnerXml);
            long importeDescuento = 0;
            foreach (CatReglasEquivalencia regla in lstReglas)
            {
                var nodoObjeto = objetoFiltro.XPathSelectElement(regla.XPathEvaluacion);
                if (nodoObjeto != null && (nodoObjeto.Value == regla.ValorEvaluacion || string.IsNullOrEmpty(regla.ValorEvaluacion)))
                {
                    idTransaccion = regla.ValorRetorno;
                    importeDescuento = Convert.ToInt64(descuento.ImporteDescuento);
                    break;
                }
            }
            if (idTransaccion == "-1")
            {
                var mensaje =
                    string.Format("No existe transacción definida en el catálogo de reglas para el valor objeto {0}",
                                  valorObjeto);
                LogEventos.EscribirEntradaLog((int) EnumErrorMotor.ErrorCrearTransaccion, mensaje, EventLogEntryType.Error);
                throw new Exception(mensaje);
            }
            var transaccion = DalConceptosEquivalencia.ObtenerTransaccionPorId(idTransaccion, idAplicacion,
                                                                               idTipodocumento);
            if (transaccion == null)
            {
                var mensaje = string.Format(
                    "No existe transacción definida en el catálogo de transacciones para el idtransacción {0} y valor objeto {1}",
                    idTransaccion, valorObjeto);
                LogEventos.EscribirEntradaLog((int) EnumErrorMotor.ErrorCrearTransaccion, mensaje, EventLogEntryType.Error);
                throw new Exception(mensaje);
            }
            return CreaTransaccion(transaccion, importeDescuento);
        }

        private CreditosFiscalesConceptoTransaccion CreaTransaccion(CatTransaccion catTransaccion, long importe)
        {
            try
            {
                var conceptoTransaccion = new CreditosFiscalesConceptoTransaccion
                                              {
                                                  Clave = catTransaccion.IdTransaccion,
                                                  Descripcion = catTransaccion.Descripcion,
                                                  Tipo = catTransaccion.TipoTransaccion,
                                                  Importe = importe
                                              };
                return conceptoTransaccion;
            }
            catch(Exception exception)
            {
                LogEventos.EscribirEntradaLog((int) EnumErrorMotor.ErrorCrearTransaccion, exception);
                throw;
            }
        }

        //private List<CatReglasEquivalencia> RecuperarReglasDescuento(int idAplicacion, int idTipoDocumento, EnumTipoObjeto transaccion, string valorObjeto)
        //{
        //    RecargarReglas(idAplicacion,idTipoDocumento,transaccion,valorObjeto);
        //    return ListaReglasDescuentos.Where(regla => regla.IdTipoObjecto == (int)transaccion && regla.ValorObjeto == valorObjeto).ToList();
        //}

        private void RecargarReglas(int idAplicacion, int idTipoDocumento, EnumTipoObjeto transaccion, string valorObjeto)
        {
            // if (null != ListaReglasDescuentos && ListaReglasDescuentos.Count > 0)
            //{
            //    return;
            //}
            try
            {
                ListaReglasDescuentos = DalConceptosEquivalencia.ObtenerReglasEquivalencia(idAplicacion, transaccion,
                                                                                           idTipoDocumento, valorObjeto);
            }
            catch(SqlException exception)
            {
                LogEventos.EscribirEntradaLog((int) EnumErrorMotor.ErrorAccesoBaseDeDatos, exception);
                throw;
            }
        }
       
        /// <summary>
        /// Sobrecarga de método que genera transacciones de D y P para descuentos de conceptos hijo.
        /// </summary>
        /// <param name="idAplicacion">Identificador de la aplicación que realiza la solicitud.</param>
        /// <param name="idTipodocumento">Tipo de documento que identifica cuales son las transacciones que lo conforman.</param>
        /// <param name="valorObjeto">Constante que identifica a las reglas de descuento de conceptos hijos.</param>
        /// <param name="descuento">Entidad que representa al descuento.</param>
        /// <returns>Transacción que identifica a los descuentos de conceptos hijos.</returns>
        public CreditosFiscalesConceptoTransaccion GeneraTransaccion(int idAplicacion, int idTipodocumento, string valorObjeto, CreditosFiscalesAgrupadorConceptosHijoDescuento descuento)
        {
            var doc = new XmlDocument();
            doc.InnerXml = MetodosComunes.SerlializaObjeto(descuento);
            //var lstReglas = RecuperarReglasDescuento(idAplicacion, idTipodocumento, EnumTipoObjeto.Transaccion,
            //                                         valorObjeto);

            var lstReglas = HerramientasReglasEquivalencia.ObtenerReglasEquivalencia(idAplicacion,
                                                                                     EnumTipoObjeto.Transaccion,
                                                                                     valorObjeto, idTipodocumento);
            var idTransaccion = "-1";
            var objetoFiltro = XDocument.Parse(doc.InnerXml);
            long importeDescuento = 0;
            foreach (CatReglasEquivalencia regla in lstReglas)
            {
                var nodoObjeto = objetoFiltro.XPathSelectElement(regla.XPathEvaluacion);
                if (nodoObjeto != null && (nodoObjeto.Value == regla.ValorEvaluacion || string.IsNullOrEmpty(regla.ValorEvaluacion)))
                {
                    idTransaccion = regla.ValorRetorno;
                    importeDescuento = Convert.ToInt64(descuento.ImporteDescuento);
                    break;
                }
            }
            if (idTransaccion == "-1")
            {
                var mensaje =
                    string.Format("No existe transacción definida en el catálogo de reglas para el valor objeto {0}",
                                  valorObjeto);
                LogEventos.EscribirEntradaLog((int) EnumErrorMotor.ErrorCrearTransaccion, mensaje, EventLogEntryType.Error);
                throw new Exception(mensaje);
            }
            var transaccion = DalConceptosEquivalencia.ObtenerTransaccionPorId(idTransaccion, idAplicacion,
                                                                               idTipodocumento);
            if (transaccion == null)
            {
                var mensaje = string.Format("No existe transacción para el idtransacción {0} y el valor objeto {1}",
                                            idTransaccion,
                                            valorObjeto);
                LogEventos.EscribirEntradaLog((int) EnumErrorMotor.ErrorCrearTransaccion, mensaje, EventLogEntryType.Error);
                throw new Exception(mensaje);
            }
            return CreaTransaccion(transaccion, importeDescuento);
        }

        /// <summary>
        /// Sobrecarga del método que genera transacciones para los conceptos hijo.
        /// </summary>
        /// <param name="idAplicacion">Identificador de la aplicación que realiza la solicitud.</param>
        /// <param name="idTipodocumento">Tipo de documento que identifica cuales son las transacciones que lo conforman.</param>
        /// <param name="valorObjeto">Concepto ley o clave de computo.</param>
        /// <param name="objetoSerializadoHijo">Concepto hijo serilizado.</param>
        /// <param name="importeHistorico">Monto del importe histórico del concepto hijo.</param>
        /// <returns>Transacción que identifica al importe histórico de los descuentos hijos.</returns>
        public CreditosFiscalesConceptoTransaccion GeneraTransaccion(int idAplicacion, int idTipodocumento, string valorObjeto, string objetoSerializadoHijo, decimal importeHistorico)
        {
            var objetoFiltro = XDocument.Parse(objetoSerializadoHijo);
            //var listaReglas = RecuperarReglas(idAplicacion, idTipodocumento, EnumTipoObjeto.Transaccion, valorObjeto); // el valor objeto aqui debería ser el concepto Ley.

            var listaReglas = HerramientasReglasEquivalencia.ObtenerReglasEquivalencia(idAplicacion,
                                                                                       EnumTipoObjeto.Transaccion,
                                                                                       valorObjeto, idTipodocumento);
            var idTransaccion = "-1";
            idTransaccion = HerramientasReglasEquivalencia.AplicarReglasEquivalencia(objetoFiltro, listaReglas);
            if (idTransaccion == "-1")
            {
                var mensaje = string.Format(
                    "No existe transacción definida para el en el catálogo de reglas para el valor objeto {0}",
                    valorObjeto);
                LogEventos.EscribirEntradaLog((int) EnumErrorMotor.ErrorCrearTransaccion, mensaje, EventLogEntryType.Error);
                throw new Exception(mensaje);
            }
            var transaccion = DalConceptosEquivalencia.ObtenerTransaccionPorId(idTransaccion, idAplicacion,
                                                                               idTipodocumento);
            if (transaccion == null)
            {
                var mensaje = string.Format(
                    "No existe transacción definida en el catálogo de transacciones para el idtransacción {0} y el valor objeto {1}",
                    idTransaccion, valorObjeto);
                LogEventos.EscribirEntradaLog((int) EnumErrorMotor.ErrorCrearTransaccion, mensaje, EventLogEntryType.Error);
                throw new Exception(mensaje);
            }
            return CreaTransaccion(transaccion, Convert.ToInt64(importeHistorico));
        }

        private List<CatReglasEquivalencia> RecuperarReglas(int idAplicacion, int idTipodocumento, EnumTipoObjeto transaccion, string valorObjeto)
        {
            try
            {
                //return DalConceptosEquivalencia.ObtenerReglasEquivalencia(idAplicacion, transaccion, idTipodocumento,valorObjeto);
                return HerramientasReglasEquivalencia.ObtenerReglasEquivalencia(idAplicacion,transaccion,
                                                                                valorObjeto, idTipodocumento);
            }
            catch(SqlException sqlException)
            {
                LogEventos.EscribirEntradaLog((int) EnumErrorMotor.ErrorAccesoBaseDeDatos, sqlException);
                throw;
            }
            catch(Exception exception)
            {
                LogEventos.EscribirEntradaLog((int) EnumErrorMotor.ErrorCrearTransaccion, exception);
                throw;
            }
        }

        /// <summary>
        /// Método que se utiliza para generar las transacciones de total monto cargo, total monto abonos y su diferencia.
        /// </summary>
        /// <param name="idAplicacion">Identificador de la aplicación que realiza la solicitud.</param>
        /// <param name="idTipodocumento">Tipo de documento que identifica cuales son las transacciones que lo conforman.</param>
        /// <param name="objetoSerializado">Concepto padre o hijo serializado.</param>
        /// <returns>Lista de transacciones válidas para DyP.</returns>
        public List<CreditosFiscalesConceptoTransaccion> GeneraTransaccion(int idAplicacion, sbyte idTipodocumento, string objetoSerializado)
        {
            var listaTransacciones = new List<CreditosFiscalesConceptoTransaccion>();
            var objetoFiltro = XDocument.Parse(objetoSerializado);
            var listaReglas = RecuperarReglas(idAplicacion, idTipodocumento, EnumTipoObjeto.Transaccion, Constantes.TotalMontoCargo);
            listaReglas.AddRange(RecuperarReglas(idAplicacion,idTipodocumento,EnumTipoObjeto.Transaccion, Constantes.TotalMontoAbono));
            listaReglas.AddRange(RecuperarReglas(idAplicacion,idTipodocumento, EnumTipoObjeto.Transaccion, Constantes.TotalACargo));
            listaReglas.AddRange(RecuperarReglas(idAplicacion,idTipodocumento, EnumTipoObjeto.Transaccion, Constantes.ImportePagosAnteriores));
            listaReglas.AddRange(RecuperarReglas(idAplicacion,idTipodocumento, EnumTipoObjeto.Transaccion, Constantes.ImporteSinPagoInicial));
            listaReglas.AddRange(RecuperarReglas(idAplicacion, idTipodocumento, EnumTipoObjeto.Transaccion, Constantes.ImportePagoInicial));
            // se agrega esta lista de reglas para evaluar de forma dinámica el nodo de evaluación.
            foreach (var regla in listaReglas)
            {
                var nodoObjeto = objetoFiltro.XPathSelectElement(regla.XPathEvaluacion);
                if(nodoObjeto != null)
                {
                    var idTransaccion = regla.ValorRetorno;
                    long importe = Convert.ToInt64(nodoObjeto.Value);
                    var catTransaccion = DalConceptosEquivalencia.ObtenerTransaccionPorId(idTransaccion, idAplicacion,
                                                                               idTipodocumento);
                    if (catTransaccion == null)
                    {
                        var mensaje =
                            string.Format("No existe transacción definida para este identificador de transacción {0}",
                                          idTransaccion);
                        LogEventos.EscribirEntradaLog((int) EnumErrorMotor.ErrorCrearTransaccion, mensaje,
                                                      EventLogEntryType.Error);
                        throw new Exception(mensaje);
                    }
                    var transaccion= CreaTransaccion(catTransaccion, Convert.ToInt64(importe));
                    listaTransacciones.Add(transaccion);
                }
            }

            return listaTransacciones;
        }
    }
}