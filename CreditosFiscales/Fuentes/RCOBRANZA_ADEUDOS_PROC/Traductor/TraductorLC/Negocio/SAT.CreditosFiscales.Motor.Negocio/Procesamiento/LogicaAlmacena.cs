
//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.Negocio:SAT.CreditosFiscales.Motor.Negocio.LogicaAlmacena:1:12/07/2012[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using SAT.CreditosFiscales.Motor.AccesoDatos.Informacion;
using SAT.CreditosFiscales.Motor.Entidades;
using SAT.CreditosFiscales.Motor.Entidades.Herramientas;
using SAT.CreditosFiscales.Motor.Entidades.Procesamiento;
using SAT.CreditosFiscales.Motor.Negocio.Mensajes;
using SAT.CreditosFiscales.Motor.Utilidades;
using DatosProcesamiento = SAT.CreditosFiscales.Motor.Entidades.DatosProcesamiento;
using Enumeraciones = SAT.CreditosFiscales.Motor.Entidades.Enumeraciones;
using SAT.CreditosFiscales.Motor.Entidades.Catalogos;
using SAT.CreditosFiscales.Motor.Entidades.DatosProcesamiento;

namespace SAT.CreditosFiscales.Motor.Negocio.Procesamiento
{
    /// <summary>
    /// Lógica de negocio para el almacenamiento de los datos en base de datos
    /// </summary>
    public class LogicaAlmacena
    {
        #region Propiedades

        /// <summary>
        /// Documento XML
        /// </summary>
        public string Documento { get; set; }

        /// <summary>
        /// Folio DyP
        /// </summary>
        public string FolioDyP { get; set; }

        /// <summary>
        /// Aplicación
        /// </summary>
        public int Aplicacion { get; set; }

        /// <summary>
        /// Id del procesamiento
        /// </summary>
        public Guid IdProcesamiento { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Inicializa la clase lógica almacena
        /// </summary>
        /// <param name="documento">Documento XML en string</param>
        /// <param name="idAplicacion">Aplicación</param>
        /// <param name="idProcesamiento">Id procesamiento</param>
        public LogicaAlmacena(string documento, int idAplicacion, Guid idProcesamiento)
        {
            Documento = documento;
            Aplicacion = idAplicacion;
            IdProcesamiento = idProcesamiento;
        }

        #endregion

        #region Métodos Públicos

        /// <summary>
        /// Almacena los datos del objeto canonico en base de datos
        /// </summary>
        /// <param name="oCreditosFiscales">objeto canonico con los datos a insertar</param>
        /// <returns></returns>
        public Respuesta<string> AlmacenaDatosBD(DatosProcesamiento.CreditosFiscales oCreditosFiscales)
        {
            Respuesta<string> resultadoInserta = new Respuesta<string>();

            try
            {
                resultadoInserta.EsExitoso = DalDatosGenerales.InsertaDatosGenerales(IdProcesamiento, Aplicacion, oCreditosFiscales.DatosGenerales.RFC,
                                                                                     oCreditosFiscales.DatosGenerales.ALR, oCreditosFiscales.DatosGenerales.LineasCaptura.DatosLineaCaptura[0].Importe,
                                                                                     oCreditosFiscales.DatosGenerales.LineasCaptura.DatosLineaCaptura[0].FechaVigencia, oCreditosFiscales.DatosGenerales.TipoDocumento,
                                                                                     oCreditosFiscales.DatosGenerales.TipoOperacion == null || oCreditosFiscales.DatosGenerales.TipoOperacion == string.Empty ? (oCreditosFiscales.Agrupadores[0].ConceptosOriginal[0].TipoMovimiento != null ? oCreditosFiscales.Agrupadores[0].ConceptosOriginal[0].TipoMovimiento : null) : oCreditosFiscales.DatosGenerales.TipoOperacion, oCreditosFiscales.Agrupadores[0].FormaPago,
                                                                                     oCreditosFiscales.DatosGenerales.Nombre, oCreditosFiscales.DatosGenerales.ApellidoMaterno, oCreditosFiscales.DatosGenerales.ApellidoPaterno, oCreditosFiscales.DatosGenerales.RazonSocial,
                                                                                     oCreditosFiscales.DatosGenerales.LineasCaptura.DatosLineaCaptura[0].PagoObligadoInternet, oCreditosFiscales.DatosGenerales.DeudorPuro, oCreditosFiscales.DatosGenerales.Observaciones, oCreditosFiscales.DatosGenerales.SolicitudARectificar);

                if (resultadoInserta.EsExitoso)
                {
                    if (oCreditosFiscales.Agrupadores.Count() > 0)
                    {
                        int idAgrupador = 1;
                        //Insertar Agrupadores
                        foreach (DatosProcesamiento.CreditosFiscalesAgrupador agrupador in oCreditosFiscales.Agrupadores)
                        {
                            decimal TotalAgrupador = 0;
                            string ValorAgrupador = agrupador.NumeroAgrupador + "|" + agrupador.Fecha + "|" + agrupador.AutoridadId.ToString() + "|" + agrupador.ALR + "|" + agrupador.RFC;
                            if (DalAgrupador.InsertaAgrupador(IdProcesamiento, idAgrupador, ValorAgrupador, TotalAgrupador, agrupador.SaldoInformativo))
                            {
                                if (agrupador.ConceptosOriginal.Count() > 0)
                                {
                                    //Insertar Conceptos del Agrupador
                                    foreach (DatosProcesamiento.CreditosFiscalesAgrupadorConceptos conceptoPadre in agrupador.ConceptosOriginal)
                                    {
                                        string IdConceptoPadre = DalConceptos.InsertaConceptoOriginal(IdProcesamiento, idAgrupador, conceptoPadre.Clave,
                                                                            conceptoPadre.Ejercicio, conceptoPadre.PeriodicidadId.ToString(), conceptoPadre.PeriodoId.ToString(),
                                                                            conceptoPadre.FechaCausacion, conceptoPadre.CreditoSir, conceptoPadre.ImporteHistorico,
                                                                            conceptoPadre.ImporteParteActualizada,
                                                                            conceptoPadre.ImportePagar, conceptoPadre.Liquidar, conceptoPadre.MotivoId, conceptoPadre.EsCorrecto);
                                        if (IdConceptoPadre != string.Empty)
                                        {

                                            if (conceptoPadre.Descuentos != null)
                                            {
                                                //Inserta Descuentos de Concepto Original
                                                foreach (DatosProcesamiento.CreditosFiscalesAgrupadorConceptosDescuento descuentoOriginal in conceptoPadre.Descuentos)
                                                {
                                                    DalDescuentos.InsertaDescuentoConceptoOriginal(idAgrupador, IdProcesamiento, Convert.ToInt32(IdConceptoPadre), descuentoOriginal.IdDescuento, descuentoOriginal.ImporteDescuento);
                                                }
                                            }

                                            if (conceptoPadre.Hijos != null)
                                            {
                                                //Insertar Conceptos Hijo del Concepto Padre del Agrupador
                                                foreach (DatosProcesamiento.CreditosFiscalesAgrupadorConceptosHijo conceptoHijo in conceptoPadre.Hijos)
                                                {
                                                    string IdConceptoHijo = DalConceptos.InsertaConceptoHijo(IdProcesamiento, idAgrupador, Convert.ToInt32(IdConceptoPadre), conceptoHijo.Clave,
                                                                                     conceptoHijo.ImporteHistorico, conceptoHijo.ImporteParteActualizada,
                                                                                     conceptoHijo.ImportePagar, conceptoHijo.Ejercicio, conceptoHijo.FechaCausacion,
                                                                                     conceptoHijo.PeriodicidadId, conceptoHijo.PeriodoId, conceptoHijo.Liquidar, conceptoHijo.MotivoId, conceptoHijo.TipoTransaccion, conceptoHijo.CreditoSir);

                                                    if (IdConceptoHijo != string.Empty && conceptoHijo.Descuentos != null)
                                                    {
                                                        foreach (DatosProcesamiento.CreditosFiscalesAgrupadorConceptosHijoDescuento descuentoHijo in conceptoHijo.Descuentos)
                                                        {
                                                            DalDescuentos.InsertaDescuentoConceptoOriginalHijo(idAgrupador, IdProcesamiento, Convert.ToInt32(IdConceptoPadre),
                                                                                                               Convert.ToInt32(IdConceptoHijo), descuentoHijo.IdDescuento, descuentoHijo.ImporteDescuento);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            //Actualiza ImporteTotal del Agrupador
                            DalAgrupador.ActualizaImporteAgrupador(IdProcesamiento, idAgrupador);
                            idAgrupador++;
                        }
                    }


                    if (oCreditosFiscales.Conceptos != null)
                    {
                        if (oCreditosFiscales.Conceptos.Count() > 0)
                        {
                            //Insertar Conceptos Dyp
                            foreach (DatosProcesamiento.CreditosFiscalesConcepto concepto in oCreditosFiscales.Conceptos)
                            {
                                string IdConcepto = DalConceptos.InsertaConceptoDyP(IdProcesamiento, concepto.Clave, concepto.Ejercicio, concepto.ClaveOrigen,
                                                                  concepto.TipoPeriodicidad, concepto.ClavePeriodo, concepto.NumeroCredito,
                                                                 concepto.FechaCausacion);
                                if (IdConcepto != string.Empty)
                                {
                                    //Insertar Transacciones
                                    foreach (DatosProcesamiento.CreditosFiscalesConceptoTransaccion transaccion in concepto.Transacciones)
                                    {
                                        if (!DalTransacciones.InsertaTransaccion(IdProcesamiento, concepto.Clave, transaccion.Clave,
                                                                             transaccion.Descripcion, transaccion.Importe, oCreditosFiscales.DatosGenerales.TipoDocumento, Aplicacion
                                                                             , Convert.ToInt32(IdConcepto),concepto.IdAgrupador))
                                        {
                                            //Asignar Errores
                                            resultadoInserta.EsExitoso = false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultadoInserta.EsExitoso = false;
                resultadoInserta.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.RegistroMensajeCanonico), ex);
            }

            return resultadoInserta;
        }

        /// <summary>
        /// Almacena los datos del objeto canonico en base de datos
        /// </summary>
        /// <param name="oCreditosFiscales">objeto canonico con los datos a insertar</param>
        /// <returns></returns>
        public Respuesta<string> AlmacenaDatosBDSIAT(DatosProcesamiento.CreditosFiscalesSIAT oCreditosFiscales, DatosProcesamiento.CreditosFiscalesConcepto[] conceptosSIAT)
        {
            Respuesta<string> resultadoInserta = new Respuesta<string>();
            try
            {
                resultadoInserta.EsExitoso = DalDatosGenerales.InsertaSIATDatosGenerales(IdProcesamiento,oCreditosFiscales.DatosGenerales);

                if (resultadoInserta.EsExitoso && oCreditosFiscales.Agrupadores!=null)
                {
                    if (oCreditosFiscales.Agrupadores.Count() > 0)
                    {
                        int idAgrupador = 1;
                        //Insertar Agrupadores
                        foreach (DatosProcesamiento.CreditosFiscalesSIATAgrupador agrupador in oCreditosFiscales.Agrupadores)
                        {

                            if (DalAgrupador.InsertaSIATAgrupador(IdProcesamiento, idAgrupador, agrupador))
                            {
                                if (agrupador.ConceptosOriginal.Count() > 0)
                                {
                                    //Insertar Conceptos del Agrupador
                                    foreach (DatosProcesamiento.CreditosFiscalesSIATAgrupadorConceptos conceptoPadre in agrupador.ConceptosOriginal)
                                    {
                                        string IdConceptoPadre = DalConceptos.InsertaSIATConceptoOriginal(IdProcesamiento, idAgrupador, conceptoPadre);
                                    }
                                }
                            }
                            idAgrupador++;
                        }
                    }


                    /*if (oCreditosFiscales.Conceptos != null)
                    {
                        if (oCreditosFiscales.Conceptos.Count() > 0)
                        {
                            //Insertar Conceptos Dyp
                            foreach (DatosProcesamiento.CreditosFiscalesConcepto concepto in oCreditosFiscales.Conceptos)
                            {
                                string IdConcepto = DalConceptos.InsertaConceptoDyP(IdProcesamiento, concepto.Clave, concepto.Ejercicio, concepto.ClaveOrigen,
                                                                  concepto.TipoPeriodicidad, concepto.ClavePeriodo, concepto.NumeroCredito,
                                                                 concepto.FechaCausacion);
                                if (IdConcepto != string.Empty)
                                {
                                    //Insertar Transacciones
                                    foreach (DatosProcesamiento.CreditosFiscalesConceptoTransaccion transaccion in concepto.Transacciones)
                                    {
                                        if (!DalTransacciones.InsertaTransaccion(IdProcesamiento, concepto.Clave, transaccion.Clave,
                                                                             transaccion.Descripcion, transaccion.Importe, oCreditosFiscales.DatosGenerales.TipoDocumento, Aplicacion
                                                                             , Convert.ToInt32(IdConcepto)))
                                        {
                                            //Asignar Errores
                                            resultadoInserta.EsExitoso = false;
                                        }
                                    }
                                }
                            }
                        }
                    }*/
                }
                if (oCreditosFiscales.Agrupadores != null)
                {
                    if (oCreditosFiscales.Agrupadores.Count() > 0)
                    {
                        foreach (DatosProcesamiento.CreditosFiscalesConcepto concepto in conceptosSIAT)
                        {
                            foreach (CreditosFiscalesConceptoTransaccion transaccion in concepto.Transacciones)
                            {
                                DalTransacciones.InsertaTransaccionSIAT(IdProcesamiento, concepto.IdAgrupador, concepto.NoSecuencia, transaccion.Clave);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultadoInserta.EsExitoso = false;
                resultadoInserta.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.RegistroMensajeCanonico), ex);
            }

            return resultadoInserta;
        }


        #endregion
    }
}
