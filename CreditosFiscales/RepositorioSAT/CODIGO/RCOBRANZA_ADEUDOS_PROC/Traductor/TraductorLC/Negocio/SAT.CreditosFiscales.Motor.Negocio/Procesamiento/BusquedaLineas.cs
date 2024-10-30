
//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.Negocio:SAT.CreditosFiscales.Motor.Negocio.BusquedaLineas:1:12/07/2012[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using SAT.CreditosFiscales.Motor.AccesoDatos.Busquedas;
using SAT.CreditosFiscales.Motor.Entidades.Procesamiento;
using SAT.CreditosFiscales.Motor.Negocio.Mensajes;
using SAT.CreditosFiscales.Motor.Utilidades;
using DatosProcesamiento = SAT.CreditosFiscales.Motor.Entidades.DatosProcesamiento;

namespace SAT.CreditosFiscales.Motor.Negocio.Procesamiento
{
    public static class BusquedaLineas
    {
        /// <summary>
        /// Busca en base de datos si existe una línea previamente generada
        /// </summary>
        /// <param name="solicitudCanonica">Objeto creditos fiscales con los datos de la solicitud</param>
        /// <returns>Respuesta<RespuestaLC></returns>
        public static Respuesta<RespuestaLC> BusquedaLC(DatosProcesamiento.CreditosFiscales solicitudCanonica)
        {
            Respuesta<RespuestaLC> respuestaBusqueda = new Respuesta<RespuestaLC>();
            bool continuar;

            try
            {
                //Obtiene procesamientos con el mismo tipo de concepto
                List<string> ProcesamientosBD = BusquedaLineaCaptura.ConsultaProcesamientPorDatosGenerales(solicitudCanonica.DatosGenerales.RFC, 
                    solicitudCanonica.DatosGenerales.ALR, solicitudCanonica.DatosGenerales.TipoDocumento, 
                    solicitudCanonica.DatosGenerales.LineasCaptura.DatosLineaCaptura[0].FechaVigencia, 
                    solicitudCanonica.Agrupadores[0].FormaPago, 
                    solicitudCanonica.DatosGenerales.LineasCaptura.DatosLineaCaptura[0].Importe,
                    solicitudCanonica.DatosGenerales.NumeroParcialidad,solicitudCanonica.DatosGenerales.IdAplicacion);
                respuestaBusqueda.EsExitoso = true;
                if (ProcesamientosBD.Count > 0)
                {
                    foreach (string procesamientoDB in ProcesamientosBD)
                    {
                        //Obtener agrupadores por datos generales                               
                        List<DatosProcesamiento.AgrupadorDB> AgrupadoresBD = BusquedaLineaCaptura.ConsultaAgrupadoresPorDatosGenerales(procesamientoDB);

                        //Comparación de Agrupadores
                        continuar = ComparaAgrupadores(AgrupadoresBD, solicitudCanonica.Agrupadores);

                        //Buscar DatosLineaCaptura
                        if (continuar)
                        {
                            respuestaBusqueda.ExisteLineaCaptura = continuar;
                            respuestaBusqueda.Entidad = BuscaDatosLC(Guid.Parse(procesamientoDB));
                            if (respuestaBusqueda.Entidad.ListaErrores!=null)
                            {
                                respuestaBusqueda.AgregaError(respuestaBusqueda.Entidad.ListaErrores[0]);
                                respuestaBusqueda.EsExitoso = false;
                            }
                            break;
                        }
                    }
                }
                else
                {
                    respuestaBusqueda.ExisteLineaCaptura = false;
                }
            }
            catch (Exception ex)
            {
                respuestaBusqueda.EsExitoso = false;
                respuestaBusqueda.AgregaError(ObtieneMensajes.ObtieneMensajeError(Constantes.BusquedaLCGenerada), ex);
            }

            return respuestaBusqueda;
        }

        private static bool ComparaAgrupadores(List<DatosProcesamiento.AgrupadorDB> AgrupadoresBD, DatosProcesamiento.CreditosFiscalesAgrupador[] creditosFiscalesAgrupador)
        {
            bool ExisteAgrupador = true;

            if (AgrupadoresBD.Count() > 0)
            {
                if (AgrupadoresBD.Count == creditosFiscalesAgrupador.Count())
                {

                    //Comparar los datos para saber si son iguales
                    foreach (DatosProcesamiento.CreditosFiscalesAgrupador agrupadorSol in creditosFiscalesAgrupador)
                    {
                        string ValorAgrupador = agrupadorSol.NumeroAgrupador + "|" + agrupadorSol.Fecha + "|" + agrupadorSol.AutoridadId.ToString() + "|" + agrupadorSol.ALR + "|" + agrupadorSol.RFC;
                        //Revisar si existen agrupadores iguales                                                
                        var Agrupador = AgrupadoresBD.FindAll(a => a.ValorAgrupador.ToLower() == ValorAgrupador.ToLower()).FirstOrDefault();
                        if (Agrupador != null)
                        {
                            ExisteAgrupador = ComparaConceptos(agrupadorSol, Agrupador);
                            if (ExisteAgrupador)
                            {
                                //Eliminar transacciones de original
                                AgrupadoresBD.Remove(Agrupador);
                            }
                            else
                            {
                                ExisteAgrupador = false;
                                break;
                            }
                        }
                        else
                        {
                            ExisteAgrupador = false;
                            break;
                        }
                    }
                }
                else
                    ExisteAgrupador = false;
            }
            else
                ExisteAgrupador = false;

            return ExisteAgrupador;
        }

        private static bool ComparaConceptos(DatosProcesamiento.CreditosFiscalesAgrupador agrupadorSol, DatosProcesamiento.AgrupadorDB Agrupador)
        {
            bool ExisteConcepto = true;

            List<DatosProcesamiento.ConceptoOriginalDB> ConceptosOriginalDB = BusquedaLineaCaptura.ConsultaConceptosPorAgrupador(Agrupador.IdAgrupador, Agrupador.IdProcesamiento);

            if (ConceptosOriginalDB.Count == agrupadorSol.ConceptosOriginal.Count())
            {
                foreach (DatosProcesamiento.CreditosFiscalesAgrupadorConceptos conceptoSol in agrupadorSol.ConceptosOriginal)
                {
                    //Comparación ConceptoOrigen, Ejercicio, FechaCausacion, CreditoSIR, ImportePagar
                    var Concepto = ConceptosOriginalDB.FindAll(c => conceptoSol.Clave == c.ConceptoOrigen &&
                        conceptoSol.Ejercicio == c.Ejercicio &&
                        conceptoSol.FechaCausacion == c.FechaCausacion &&
                        conceptoSol.CreditoSir == c.CreditoSir &&
                        conceptoSol.ImportePagar == c.ImportePagar).FirstOrDefault();

                    if (Concepto != null)
                    {
                        ExisteConcepto = ComparaDescuentos(conceptoSol, Concepto);
                        if (ExisteConcepto)
                        {
                            ExisteConcepto = ComparaHijos(conceptoSol, Concepto);
                            if (ExisteConcepto)
                            {
                                //Eliminar transacciones de original
                                ConceptosOriginalDB.Remove(Concepto);
                            }
                            else
                            {
                                ExisteConcepto = false;
                                break;
                            }
                        }
                    }
                    else
                    {
                        ExisteConcepto = false;
                        break;
                    }

                }
            }
            else
                ExisteConcepto = false;

            return ExisteConcepto;
        }

        private static bool ComparaHijos(DatosProcesamiento.CreditosFiscalesAgrupadorConceptos conceptoSol, DatosProcesamiento.ConceptoOriginalDB Concepto)
        {
            bool ExisteHijo = true;

            if (conceptoSol.Hijos != null)
            {
                //Consulta todos los conceptos hijos del concepto original
                List<DatosProcesamiento.ConceptoOriginalHijo> ConceptosOriginalHijoDB = BusquedaLineaCaptura.ConsultaConceptosHijoPorAgrupador(Concepto.IdAgrupador, Concepto.IdProcesamiento, Concepto.ConceptoOrigen, Concepto.IdConceptoOriginal);

                if (conceptoSol.Hijos.Count() == ConceptosOriginalHijoDB.Count)
                {
                    foreach (DatosProcesamiento.CreditosFiscalesAgrupadorConceptosHijo HijoSol in conceptoSol.Hijos)
                    {
                        //ConceptoOriginalPadre, ConceptoOriginalHijo, Ejercicio, ImportePagar    
                        var ConceptoHijo = ConceptosOriginalHijoDB.Where(c => conceptoSol.Clave == c.ConceptoOrigen &&
                                HijoSol.Clave == c.ConceptoOrigenHijo &&
                                HijoSol.Ejercicio == c.Ejercicio &&
                                (HijoSol.FechaCausacion == string.Empty ? null : HijoSol.FechaCausacion) == c.FechaCausacion &&
                                HijoSol.ImportePagar == c.ImportePagar).FirstOrDefault();

                        if (ConceptoHijo != null)
                        {
                            ExisteHijo = ComparaDescuentosHijo(HijoSol, ConceptoHijo, Concepto.ConceptoOrigen, Concepto.IdConceptoOriginal);
                            if (ExisteHijo)
                            {
                                //Eliminar transacciones de original
                                ConceptosOriginalHijoDB.Remove(ConceptoHijo);
                            }
                            else
                            {
                                ExisteHijo = false;
                                break;
                            }
                        }
                        else
                        {
                            ExisteHijo = false;
                            break;
                        }
                    }
                }
                else
                    ExisteHijo = false;
            }
            else
                ExisteHijo = false;

            return ExisteHijo;
        }

        private static bool ComparaDescuentosHijo(DatosProcesamiento.CreditosFiscalesAgrupadorConceptosHijo HijoSol, DatosProcesamiento.ConceptoOriginalHijo ConceptoHijo, string ClaveConceptoOriginal, int IdConceptoOriginal)
        {
            bool ExisteDescuentoHijo = true;

            if (HijoSol.Descuentos != null)
            {
                //Consulta todos los descuentos del concepto
                List<DatosProcesamiento.DescuentoOriginalHijo> descuentosHijoDB = BusquedaLineaCaptura.ConsultaDescuentosPorConceptoHijo(ConceptoHijo.IdAgrupador, ConceptoHijo.IdProcesamiento, IdConceptoOriginal, ConceptoHijo.IdConceptoOriginalHijo);

                if (HijoSol.Descuentos.Count() == descuentosHijoDB.Count)
                {
                    foreach (DatosProcesamiento.CreditosFiscalesAgrupadorConceptosHijoDescuento descuentoHijoSol in HijoSol.Descuentos)
                    {
                        //ConceptoOriginal, ConceptoOriginalHijo, IdDescuento, ImporteDescuento  
                        var DescuentoHijo = descuentosHijoDB.Where(d => ClaveConceptoOriginal == d.ConceptoOriginal &&
                                HijoSol.Clave == d.ConceptoOriginalHijo &&
                                descuentoHijoSol.IdDescuento == d.IdDescuento &&
                                descuentoHijoSol.ImporteDescuento == d.ImporteDescuento).FirstOrDefault();

                        if (DescuentoHijo != null)
                        {
                            //Eliminar descuento de original
                            descuentosHijoDB.Remove(DescuentoHijo);
                        }
                        else
                        {
                            ExisteDescuentoHijo = false;
                            break;
                        }
                    }
                }
                else
                    ExisteDescuentoHijo = false;
            }
            else
                ExisteDescuentoHijo = true;

            return ExisteDescuentoHijo;
        }

        private static bool ComparaDescuentos(DatosProcesamiento.CreditosFiscalesAgrupadorConceptos conceptoSol, DatosProcesamiento.ConceptoOriginalDB Concepto)
        {
            bool ExisteDescuento = true;

            if (conceptoSol.Descuentos != null)
            {
                //Consulta todos los descuentos del concepto
                List<DatosProcesamiento.DescuentosOriginal> DescuentosOriginalDB = BusquedaLineaCaptura.ConsultaDescuentosPorConcepto(Concepto.IdAgrupador, Concepto.IdProcesamiento, Concepto.IdConceptoOriginal);

                if (conceptoSol.Descuentos.Count() == DescuentosOriginalDB.Count)
                {
                    foreach (DatosProcesamiento.CreditosFiscalesAgrupadorConceptosDescuento descuentoOriginalSol in conceptoSol.Descuentos)
                    {
                        //ConceptoOriginal, IdDescuento, ImporteDescuento
                        var Descuento = DescuentosOriginalDB.Where(d => conceptoSol.Clave == d.ConceptoOriginal &&
                            descuentoOriginalSol.IdDescuento == d.IdDescuento &&
                            descuentoOriginalSol.ImporteDescuento == d.ImporteDescuento).FirstOrDefault();

                        if (Descuento != null)
                        {
                            //Eliminar descuento de original
                            DescuentosOriginalDB.Remove(Descuento);
                        }
                        else
                        {
                            ExisteDescuento = false;
                            break;
                        }
                    }
                }
                else
                    ExisteDescuento = false;
            }
            else
                ExisteDescuento = false;

            return ExisteDescuento;
        }

        /// <summary>
        /// Busca los datos de una línea de captura por procesamiento
        /// </summary>
        /// <param name="IdProcesamiento">Id del procesamiento a buscar</param>
        /// <returns>RespuestaLC datos de línea de captura</returns>
        private static RespuestaLC BuscaDatosLC(Guid IdProcesamiento)
        {
            RespuestaLC respuestaLC = new RespuestaLC();

            try
            {
                respuestaLC = BusquedaLineaCaptura.ConsultaLineaCapturaPorProcesamiento(IdProcesamiento);
            }
            catch
            {
                throw;
            }

            return respuestaLC;
        }


        public static RespuestaSolicitudOriginal BuscaXMLOriginal(string Folio)
        {

            RespuestaSolicitudOriginal respuestaXMLOriginal = new RespuestaSolicitudOriginal();


            try
            {

                respuestaXMLOriginal = BusquedaLineaCaptura.ConsultaXMLOriginal(Folio);
            }
            catch
            {
                throw;
            }

            return respuestaXMLOriginal;


        }

        /// <summary>
        /// Busca los datos de resoluciones de una línea de captura
        /// </summary>
        /// <param name="lineaCaptura">Línea de captura</param>
        /// <returns>RespuestaResolucionesEnLC datos de resolucion determinante</returns>
        public static RespuestaResolucionesEnLC BuscaResolucionesEnLC(string lineaCaptura)
        {
            RespuestaResolucionesEnLC respuestaResolucionesEnLC = new RespuestaResolucionesEnLC();

            try
            {
                respuestaResolucionesEnLC = BusquedaLineaCaptura.ConsultaResolucionesEnLineaCaptura(lineaCaptura);
            }
            catch
            {
                throw;
            }

            return respuestaResolucionesEnLC;
        }

        /// <summary>
        /// Busqueda de lineas captura existentes
        /// </summary>
        /// <param name="solicitud">solicitud de lineas de captura</param>
        /// <returns>RespuestaLineasCapturaExistentes</returns>
        public static RespuestaLineasCapturaExistentes BuscaLineasCapturaExistentes(SolicitudLineasCapturaExistentes solicitud)
        {
            RespuestaLineasCapturaExistentes respuestaLcExistentes = new RespuestaLineasCapturaExistentes();
            List<RespuestaLineasCapturaExistentesDatosLinea> lineasCaptura = new List<RespuestaLineasCapturaExistentesDatosLinea>();
            try
            {
                foreach (SolicitudLineasCapturaExistentesResolucion resolucion in solicitud.Resoluciones)
                {
                    List<RespuestaLineasCapturaExistentesDatosLinea> lineasCapturaPorResolucion = BusquedaLineaCaptura.ConsultaLineasDeCapturaExistentes(resolucion.NumeroResolucion, resolucion.FechaDocumento, Convert.ToInt32(resolucion.Autoridad), resolucion.ALR, resolucion.RFC);
                    lineasCaptura.AddRange(lineasCapturaPorResolucion);
                }
                respuestaLcExistentes.LineasCaptura = lineasCaptura.ToArray();
                return respuestaLcExistentes;

            }
            catch
            {
                throw;
            }


        }
    }
}
