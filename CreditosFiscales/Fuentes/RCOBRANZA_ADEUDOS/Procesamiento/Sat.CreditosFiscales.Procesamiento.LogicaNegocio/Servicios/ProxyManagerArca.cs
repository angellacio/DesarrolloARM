
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Procesamiento.LogicaNegocio.Servicios:Sat.CreditosFiscales.Procesamiento.LogicaNegocio.Servicios.ProxyManagerArca:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Sat.CreditosFiscales.Comunes.Entidades;
using Sat.CreditosFiscales.Comunes.Entidades.AccesoLogEventos;
using Sat.CreditosFiscales.Comunes.Entidades.Catalogos;
using Sat.CreditosFiscales.Comunes.Entidades.CodigosError;
using Sat.CreditosFiscales.Comunes.Entidades.GeneraFormato;
using Sat.CreditosFiscales.Comunes.Entidades.Servicios;
using Sat.CreditosFiscales.Comunes.Herramientas;
using Sat.CreditosFiscales.Procesamiento.LogicaNegocio.Catalogos;

namespace Sat.CreditosFiscales.Procesamiento.LogicaNegocio.Servicios
{
    /// <summary>
    /// Clase que maneja los llamados al Servicio de ARCA para la consulta de creditos fiscales
    /// </summary>
    public static class ProxyManagerArca
    {
        #region Métodos publicos

        /// <summary>
        /// Recupera los documentos determinantes de un contribuyente de la información recuperada de ARCA
        /// </summary>
        /// <param name="rfc">Rfc de principal del contribuyente</param>
        /// <param name="rfcs">RFC´s historicos del contribuyente</param>
        /// <returns>Documentos determinantes</returns>
        public static List<DocumentoDeterminante> ObtenerInformacionContribuyente(string rfc, List<string> rfcs)
        {
            List<ServicioArcaCF.DocumentosDeterminantes> documentosArca = ObtenerInformacionContribuyenteArca(rfc, rfcs);
            return MapearDocumentosDeterminantes(documentosArca, rfc);
        }

        /// <summary>
        /// Recupera los documentos determinantes y la información de un contribuyente puro de la información recuperada de ARCA
        /// </summary>
        /// <param name="rfc">RFC del documento determinante</param>
        /// <param name="documentoDeterminante">Número del documento determinante</param>
        /// <param name="idAutoridad">Autoridad</param>
        /// <param name="claveAlr">Alr</param>
        /// <param name="fechaDocumento">Fecha del documento</param>
        /// <returns>Documentos determinantes e información de usuario</returns>
        public static DocumentosContribuyentePuro ObtenerInformacionContribuyentePuro(string rfc, string documentoDeterminante, int idAutoridad, int claveAlr, DateTime fechaDocumento)
        {
            List<ServicioArcaCF.DocumentosDeterminantes> documentosArca = ObtenerInformacionContribuyentePuroArca(rfc, documentoDeterminante, idAutoridad, claveAlr, fechaDocumento);
            DocumentosContribuyentePuro documentosContPuro = new DocumentosContribuyentePuro();
            
            documentosContPuro.Documentos = MapearDocumentosDeterminantes(documentosArca, rfc);
            if (documentosArca.Count > 0)
                documentosContPuro.Usuario = MapeaContribuyentePuro(documentosArca, rfc);
            else
                documentosContPuro.Usuario = new Usuario();

            return documentosContPuro;
        }

        #endregion

        #region Mapeo de la informacion  CreditosFiscales Vs ARCA

        private static Usuario MapeaContribuyentePuro(List<ServicioArcaCF.DocumentosDeterminantes> documentosArca, string rfcPeticionArca)
        {
            try
            {
                ServicioArcaCF.DocumentosDeterminantes documentoArca = documentosArca.FirstOrDefault();
                Usuario usuario = new Usuario()
                {
                    Nombres = documentoArca.NombreRazon,
                    ApellidoPaterno = documentoArca.ApellidoPaterno,
                    ApellidoMaterno = documentoArca.ApellidoMaterno,
                    IdALR = Convert.ToByte(documentoArca.AdministracionLocal),
                    Rfc = documentoArca.RFC,
                    PersonaFisica = true
                };
                //usuario.PersonaFisica = (documentoArca.MedioDePago.Identificador == 2 ? true : false);
                //if (usuario.PersonaFisica)
                //{
                //    usuario.Nombres = documentoArca.NombreRazon;
                //}
                //else
                //{
                //    usuario.RazonSocial = documentoArca.NombreRazon;
                //}

                return usuario;
            }
            catch (ExcepcionTipificada ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                string mensaje = string.Format("Error al mapear el contribuyente puro obtenido de ARCA::RFC='{0}'::.", rfcPeticionArca);
                string ticket = AccesoLogEventos.LogEventos.EscribirEntradaLog((int)EnumErroresArca.ErrorAlMapearContribuyentePuro, mensaje, ex);
                throw new ExcepcionTipificada("Hay inconsistencias que impiden mostrar la información de sus adeudos. Acuda a la Administración Local de Servicios al Contribuyente que corresponda a su domicilio fiscal, o llame a INFOSAT.", ex, ticket);
            }
        }

        private static List<DocumentoDeterminante> MapearDocumentosDeterminantes(List<ServicioArcaCF.DocumentosDeterminantes> documentosArca, string rfcPeticionArca)
        {
            try
            {
                List<DocumentoDeterminante> documentosCF = new List<DocumentoDeterminante>();
                
                foreach (ServicioArcaCF.DocumentosDeterminantes doctoArca in documentosArca)
                {
                    decimal doctoCFImporteActualizacion = 0;
                    decimal doctoCFImporteDescuentos = 0;
                    decimal doctoCFImporteHistorico = 0;
                    decimal doctoCFImportePagar = 0;
                    decimal doctoCFImporteRecargos = 0;

                    //Mapeamos el documento con datos directos

                    
                    DocumentoDeterminante doctoCF = new DocumentoDeterminante()
                    {
                        IdALR = Convert.ToByte(doctoArca.AdministracionLocal),
                        IdAutoridad = doctoArca.IdAutoridad,
                        FechaDocumento = DateTime.ParseExact(doctoArca.FechaDeterminante.Substring(0, 10), "dd/MM/yyyy", CultureInfo.InstalledUICulture, DateTimeStyles.None),
                        
                        
                        NumDocumento = doctoArca.NumeroDoumento,
                        Rfc = doctoArca.RFC,
                        ImporteActualizacion = 0,
                        ImporteHistorico = 0,
                        ImporteDescuentos = 0,
                        ImportePagar = 0,
                        ImporteRecargos = 0,
                        SaldoInformativo = Convert.ToDecimal(doctoArca.SaldoInformativo),
                        FormaPago = !string.IsNullOrWhiteSpace(doctoArca.FormaPago) ? doctoArca.FormaPago : "920048-4423",//EN REALIDAD DEBE VENIR DE ARCA Y SI NO VIENE POR DEFAULT SE DEBE ASIGNAR 920048docto
                        IdPersonaMAT = doctoArca.IdPersonaMAT == "0" ? null : doctoArca.IdPersonaMAT,
                        IdTipoLineaMAT=Convert.ToByte(doctoArca.IdTipoLinea),
                        IdResolucionMAT= doctoArca.IdDocumentoMAT
                    };

                    //Validación para la fecha de notificación
                    if (!string.IsNullOrWhiteSpace(doctoArca.FechaNotificacion))
                    {
                        doctoCF.FechaNotificacion = DateTime.ParseExact(doctoArca.FechaNotificacion.Substring(0, 10), "dd/MM/yyyy", CultureInfo.InstalledUICulture, DateTimeStyles.None);
                        if (doctoCF.FechaNotificacion == new DateTime(1900, 1, 1))
                        {
                            doctoCF.FechaNotificacion = DateTime.MinValue;
                        }
                    }
                    else
                    {
                        doctoCF.FechaNotificacion =  DateTime.MinValue;
                    }

                    if (!MetodosComunes.EsCadenaValida(doctoCF.FormaPago, "^\\d{6}-\\d{4}"))
                        throw new Exception(string.Format("Error al mapear los documentos del RFC='{0}'. La Forma de Pago no es válida ya que no cumple con la expresión regular ='nnnnnn-nnnn' . Para el  Documento = '{1}'. Datos de Arca: FormaPago={2}", doctoArca.RFC, doctoArca.NumeroDoumento, doctoArca.FormaPago));

                    //Obtenemos las marcas para el documento
                    doctoCF.Marcas = new List<CatMarca>();
                    foreach (ServicioArcaCF.Observacion marcaArca in doctoArca.MarcasPorDocumento)
                    {
                        CatMarca marca = new CatMarca();
                        marca.CveMarca = marcaArca.Id;
                        marca.Descripcion = marcaArca.Descripcion;
                        marca.MostrarImportes = marcaArca.MuestraImportes;
                        marca.Observacion = marcaArca.Observaciones;
                        if (marcaArca.Entidades != null)
                        {
                            marca.Portal = marcaArca.Entidades.Portal;
                            foreach (var telefono in marcaArca.Entidades.Telefonos)
                            {
                                marca.Telefonos = marca.Telefonos + " " + telefono.Telefono;
                            }                            
                        }
                        doctoCF.Marcas.Add(marca);
                    }

                    //Creamos la lista de conceptos
                    doctoCF.Conceptos = new List<DocumentoDeterminanteConcepto>();
                    List<string> creditosArca = doctoArca.ListaConceptos.Select(m => m.CreditoArca).Distinct().ToList<string>();

                    if (doctoArca.ListaConceptos == null)
                        throw new Exception(string.Format("Error al mapear los documentos del RFC='{0}'. El documento no contiene conceptos, para el  Documento = '{1}'.", doctoArca.RFC, doctoArca.NumeroDoumento));

                    if (doctoArca.ListaConceptos.Count() == 0)
                        throw new Exception(string.Format("Error al mapear los documentos del RFC='{0}'. El documento no contiene conceptos, para el Documento = '{1}'.", doctoArca.RFC, doctoArca.NumeroDoumento));



                    foreach (string numCredito in creditosArca)
                    {
                        List<ServicioArcaCF.Concepto> conceptosEnCreditoArca = doctoArca.ListaConceptos.Where(m => m.CreditoArca == numCredito).ToList();
                        // RN: Solo un concepto dentro del credito arca puede estar mostrado como padre
                        int numConceptosPadre = conceptosEnCreditoArca.Count(m => m.EsCreditoPadre == true);
                        if (numConceptosPadre != 1)
                            throw new Exception(string.Format("Error al mapear los documentos del RFC='{0}'. El concepto marcado como padre no es igual a uno, para el Credito arca = '{1}' del Documento = '{2}' hay {3} conceptos marcados como padre.",
                                    doctoArca.RFC, numCredito, doctoArca.NumeroDoumento, numConceptosPadre));

                        ServicioArcaCF.Concepto conceptoPadreArca = conceptosEnCreditoArca.FirstOrDefault(m => m.EsCreditoPadre == true);
                        conceptosEnCreditoArca.Remove(conceptoPadreArca);

                        DocumentoDeterminanteConcepto conceptoPadre = new DocumentoDeterminanteConcepto()
                        {
                            CreditoARCA = conceptoPadreArca.CreditoArca,
                            CreditoSIR = conceptoPadreArca.CreditoSir,
                            IdConcepto = conceptoPadreArca.ConceptoLey,
                            IdMotivo = conceptoPadreArca.IdMotivo,
                            ImporteParteActualizada = Convert.ToDecimal(conceptoPadreArca.Actualizacion),
                            ImporteDescuentos = Convert.ToDecimal(conceptoPadreArca.Descuento),
                            ImporteHistorico = Convert.ToDecimal(conceptoPadreArca.ImporteHistorico),
                            ImportePagar = Convert.ToDecimal(conceptoPadreArca.ImporteTotal),
                            ImporteRecargos = 0,
                            IdConceptoMAT=(conceptoPadreArca.IdSaldo==0?null:conceptoPadreArca.IdSaldo.ToString())
                        };
                       
                        
                        if (!string.IsNullOrWhiteSpace(conceptoPadreArca.FechaCausacion))
                        {
                            if (conceptoPadreArca.Ejercicio != 0)
                                throw new Exception(string.Format("Error al mapear los documentos del RFC='{0}'. El concepto ley cuenta con la fecha de causación y ademas tiene ejercicio lo cual es inválido, para el Credito arca = '{1}' del Documento = '{2}'. Datos de Arca: Ejercicio={3}, Periodicidad={4}, Periodo={5} y Fecha causación={6} para el Concepto padre={7}",
                                    doctoArca.RFC, numCredito, doctoArca.NumeroDoumento, conceptoPadreArca.Ejercicio, conceptoPadreArca.Periodicidad, conceptoPadreArca.IdPeriodo, conceptoPadreArca.FechaCausacion, conceptoPadreArca.ConceptoLey));

                            conceptoPadre.FechaCausacion = DateTime.ParseExact(conceptoPadreArca.FechaCausacion.Substring(0, 10), "dd/MM/yyyy", CultureInfo.InstalledUICulture, DateTimeStyles.None);
                            //conceptoPadre.Ejercicio = 0;//conceptoPadreArca.Ejercicio;
                            conceptoPadre.IdPeriodo = Convert.ToByte(conceptoPadreArca.IdPeriodo);//ApplicationSettings.ConsultaConfiguracion<byte>("IdPeriodoSinPeriodo");
                            conceptoPadre.IdPeriodicidad = Convert.ToByte(conceptoPadreArca.Periodicidad);//ApplicationSettings.ConsultaConfiguracion<byte>("IdPeriodicidadSinPeriodo"); 
                        }
                        else
                        {
                            if (conceptoPadreArca.Periodicidad == "0" || conceptoPadreArca.Ejercicio == 0 || conceptoPadreArca.IdPeriodo == 0)
                                throw new Exception(string.Format("Error al mapear los documentos del RFC='{0}'. No se cuenta con la fecha de causación y no se pudieron recuperar correctamente los campos de ejercicio, periodicidad y periodo, para el Credito arca = '{1}' del Documento = '{2}'. Datos de Arca: Ejercicio={3},Periodicidad={4} y Periodo={5} para el Concepto={6}",
                                    doctoArca.RFC, numCredito, doctoArca.NumeroDoumento, conceptoPadreArca.Ejercicio, conceptoPadreArca.Periodicidad, conceptoPadreArca.IdPeriodo, conceptoPadreArca.ConceptoLey));
                            conceptoPadreArca.FechaCausacion = null;
                            conceptoPadre.Ejercicio = conceptoPadreArca.Ejercicio;
                            conceptoPadre.IdPeriodo = Convert.ToByte(conceptoPadreArca.IdPeriodo);
                            conceptoPadre.IdPeriodicidad = Convert.ToByte(conceptoPadreArca.Periodicidad);
                        }

                        if (conceptoPadreArca.Descuentos.Length > 0)
                        {
                            conceptoPadre.Descuentos = new List<DescuentoConcepto>();
                            foreach (ServicioArcaCF.Descuento descuento in conceptoPadreArca.Descuentos)
                            {
                                conceptoPadre.Descuentos.Add(
                                    new DescuentoConcepto()
                                    {
                                        IdDescuento = descuento.Identificador.ToString(),
                                        ImporteDescuento = Convert.ToDecimal(descuento.Importe)

                                    }
                                    );

                                if (!MetodosComunes.EsCadenaValida(descuento.Identificador.ToString(), "^\\d{6}-\\d{4}"))
                                    throw new Exception(string.Format("Error al mapear los documentos del RFC='{0}'. El Identificador del descuento no es válido ya que no cumple con la expresión regular ='nnnnnn-nnnn'"+
                                    ", para el Credito arca = '{1}' del Documento = '{2}'. Datos de Arca: Identificador descuento={3} para el Concepto padre ={4}",
                                        doctoArca.RFC, numCredito, doctoArca.NumeroDoumento, descuento.Identificador, conceptoPadreArca.ConceptoLey));

                            }
                        }

                        List<DocumentoDeterminanteConceptoHijo> conceptosHijo = new List<DocumentoDeterminanteConceptoHijo>();
                        //Id concepto
                        foreach (ServicioArcaCF.Concepto conceptoHijoArca in conceptosEnCreditoArca)
                        {
                            DocumentoDeterminanteConceptoHijo conceptoHijo = new DocumentoDeterminanteConceptoHijo()
                            {
                                CreditoSIR = conceptoHijoArca.CreditoSir,
                                IdConcepto = conceptoHijoArca.ConceptoLey,
                                IdMotivo = conceptoHijoArca.IdMotivo,
                                ImporteParteActualizada = Convert.ToDecimal(conceptoHijoArca.Actualizacion),
                                ImporteDescuentos = Convert.ToDecimal(conceptoHijoArca.Descuento),
                                ImporteHistorico = Convert.ToDecimal(conceptoHijoArca.ImporteHistorico),
                                ImportePagar = Convert.ToDecimal(conceptoHijoArca.ImporteTotal),
                                ImporteRecargos = 0
                            };



                           
                            if (!string.IsNullOrWhiteSpace(conceptoHijoArca.FechaCausacion))
                            {
                                if (conceptoHijoArca.Ejercicio != 0)
                                    throw new Exception(string.Format("Error al mapear los documentos del RFC='{0}'. El concepto ley cuenta con la fecha de causación y ademas tiene ejercicio lo cual es inválido, para el Credito arca = '{1}' del Documento = '{2}'. Datos de Arca: Ejercicio={3}, Periodicidad={4}, Periodo={5} y Fecha causación={6} para el Concepto hijo={7}",
                                        doctoArca.RFC, numCredito, doctoArca.NumeroDoumento, conceptoHijoArca.Ejercicio, conceptoHijoArca.Periodicidad, conceptoHijoArca.IdPeriodo, conceptoHijo.FechaCausacion, conceptoHijoArca.ConceptoLey));

                                conceptoHijo.FechaCausacion = DateTime.ParseExact(conceptoHijoArca.FechaCausacion.Substring(0, 10), "dd/MM/yyyy", CultureInfo.InstalledUICulture, DateTimeStyles.None);
                                conceptoHijo.Ejercicio = 0;//conceptoHijoArca.Ejercicio;
                                conceptoHijo.IdPeriodo = Convert.ToByte(conceptoHijoArca.IdPeriodo);//ApplicationSettings.ConsultaConfiguracion<byte>("IdPeriodoSinPeriodo");
                                conceptoHijo.IdPeriodicidad = Convert.ToByte(conceptoHijoArca.Periodicidad);//ApplicationSettings.ConsultaConfiguracion<byte>("IdPeriodicidadSinPeriodo");
                            }
                            else
                            {
                                if (string.IsNullOrWhiteSpace(conceptoHijoArca.Periodicidad) || conceptoHijoArca.Ejercicio == 0 || conceptoHijoArca.IdPeriodo == 0)
                                    throw new Exception(string.Format("Error al mapear los documentos del RFC='{0}'. No se cuenta con la fecha de causación y no se pudieron recuperar correctamente los campos de ejercicio, periodicidad y periodo, para el Credito arca = '{1}' del Documento = '{2}'. Datos de Arca: Ejercicio={3},Periodicidad={4} y Periodo={5} para el Concepto={6}",
                                        doctoArca.RFC, numCredito, doctoArca.NumeroDoumento, conceptoHijoArca.Ejercicio, conceptoHijoArca.Periodicidad, conceptoHijoArca.IdPeriodo, conceptoHijoArca.ConceptoLey));

                                conceptoHijo.Ejercicio = conceptoHijoArca.Ejercicio;
                                conceptoHijo.IdPeriodo = Convert.ToByte(conceptoHijoArca.IdPeriodo);
                                conceptoHijo.IdPeriodicidad = Convert.ToByte(conceptoHijoArca.Periodicidad);
                            }

                            if (conceptoHijoArca.Descuentos.Length > 0)
                            {
                                conceptoHijo.Descuentos = new List<DescuentoConceptoHijo>();
                                foreach (ServicioArcaCF.Descuento descuento in conceptoHijoArca.Descuentos)
                                {
                                    conceptoHijo.Descuentos.Add(
                                        new DescuentoConceptoHijo()
                                        {
                                            IdDescuento = descuento.Identificador.ToString(),
                                            ImporteDescuento = Convert.ToDecimal(descuento.Importe)
                                        }
                                        );

                                    if (!MetodosComunes.EsCadenaValida(descuento.Identificador.ToString(), "^\\d{6}-\\d{4}"))
                                        throw new Exception(string.Format("Error al mapear los documentos del RFC='{0}'. El Identificador del descuento no es válido ya que no cumple con la expresión regular ='nnnnnn-nnnn'" +
                                        ", para el Credito arca = '{1}' del Documento = '{2}'. Datos de Arca: Identificador descuento={3} para el Concepto hijo ={4} del Concepto padre ={5}",
                                            doctoArca.RFC, numCredito, doctoArca.NumeroDoumento, descuento.Identificador, conceptoHijoArca.ConceptoLey,conceptoPadreArca.ConceptoLey));
                                }
                            }

                            doctoCFImporteActualizacion += conceptoHijo.ImporteParteActualizada;
                            doctoCFImporteDescuentos += conceptoHijo.ImporteDescuentos;
                            doctoCFImporteHistorico += conceptoHijo.ImporteHistorico;
                            doctoCFImportePagar += conceptoHijo.ImportePagar;
                            //Solo se suma si el concepto tiene el id de recargos
                            List<string> conceptosRecargo = ApplicationSettings.ConsultaConfiguracion<string>("IdsConceptosRecargo").Split('|').ToList();
                            if (conceptosRecargo.Exists(m => m == conceptoHijo.IdConcepto.ToString()))
                            {
                                //conceptoHijo.ImporteRecargos = conceptoHijo.ImportePagar;
                                conceptoPadre.ImporteRecargos += conceptoHijo.ImportePagar;
                                doctoCFImporteRecargos += conceptoHijo.ImportePagar;
                            }
                            //RN: Un credico arca no puede tener mas de un concepto hijo con la misma Fecha de causacion, ejercicio, periodicidad y periodo
                            conceptosHijo.Add(conceptoHijo);

                            /* No se requiere esta validacion, es un escenario de negocio valido
                            if (conceptosHijo.Count(m => m.FechaCausacion == conceptoHijo.FechaCausacion && m.Ejercicio == conceptoHijo.Ejercicio 
                                && m.IdPeriodicidad == conceptoHijo.IdPeriodicidad && m.IdPeriodo == conceptoHijo.IdPeriodo) > 1)
                                throw new Exception(string.Format("Error al mapear los documentos del RFC='{0}'. Un credito arca no puede tener mas de un concepto hijo con la misma fecha de causacion, ejercicio, periodicidad y periodo, para el Credito arca = '{1}' del Documento = '{2}'. Datos de Arca: Fecha de causación={3}, Ejercicio={4}, Periodicidad={5} y Periodo={6}, Error encontrado para para el Concepto hijo={7} del Concepto padre={8}",
                                    doctoArca.RFC, numCredito, doctoArca.NumeroDoumento, conceptoHijo.FechaCausacion, 
                                    conceptoHijo.Ejercicio, conceptoHijo.IdPeriodicidad, conceptoHijo.IdPeriodo, conceptoHijo.IdConcepto, conceptoPadre.IdConcepto));
                            */
                        }
                        conceptoPadre.ConceptosHijo = conceptosHijo;

                        doctoCFImporteActualizacion += conceptoPadre.ImporteParteActualizada;
                        doctoCFImporteDescuentos += conceptoPadre.ImporteDescuentos;
                        doctoCFImporteHistorico += conceptoPadre.ImporteHistorico;
                        doctoCFImportePagar += conceptoPadre.ImportePagar;

                        doctoCF.Conceptos.Add(conceptoPadre);

                        /* No se requiere validar este caso ya que en el caso de multas la fecha de causación es la misma.
                        if (doctoCF.Conceptos.Count(m => m.FechaCausacion == conceptoPadre.FechaCausacion && m.Ejercicio == conceptoPadre.Ejercicio
                                && m.IdPeriodicidad == conceptoPadre.IdPeriodicidad && m.IdPeriodo == conceptoPadre.IdPeriodo) > 1)
                            throw new Exception(string.Format("Error al mapear los documentos del RFC='{0}'. Un credito arca no puede tener mas de un concepto padre con la misma fecha de causación, ejercicio, periodicidad y periodo, para el Credito arca = '{1}' del Documento = '{2}'. Datos Arca: Fecha de causación={3}, Ejercicio={4}, Periodicidad={5} y Periodo={6}, Error encontrado para para el Concepto padre={7}",
                                doctoArca.RFC, numCredito, doctoArca.NumeroDoumento, conceptoPadre.FechaCausacion, 
                                conceptoPadre.Ejercicio, conceptoPadre.IdPeriodicidad, conceptoPadre.IdPeriodo, conceptoPadre.IdConcepto));
                         */
                    }

                    //Asignacion de los acumuladores al documento
                    doctoCF.ImporteActualizacion = doctoCFImporteActualizacion;
                    doctoCF.ImporteDescuentos = doctoCFImporteDescuentos;
                    doctoCF.ImporteHistorico = doctoCFImporteHistorico - doctoCFImporteRecargos;
                    doctoCF.ImportePagar = doctoCFImportePagar;
                    doctoCF.ImporteRecargos = doctoCFImporteRecargos;

                    //doctoCF.ImportePagos = No se necesita por el momento;
                    documentosCF.Add(doctoCF);
                }


                return documentosCF;
            }
            catch (ExcepcionTipificada ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                string mensaje = string.Format("Error al mapear los documentos obtenidos de ARCA::RFC='{0}'::.", rfcPeticionArca);
                string ticket = AccesoLogEventos.LogEventos.EscribirEntradaLog((int)EnumErroresArca.ErrorAlMapearDocumentoArcaADocumentoCF, mensaje, ex);
                throw new ExcepcionTipificada("Hay inconsistencias que impiden mostrar la información de sus adeudos. Acuda a la Administración Local de Servicios al Contribuyente que corresponda a su domicilio fiscal, o llame a INFOSAT.", ex, ticket);
            }
        }
        
        #endregion

        #region Invocaciones al servicio de ARCA

        private static List<ServicioArcaCF.DocumentosDeterminantes> ObtenerInformacionContribuyenteArca(string rfc, List<string> rfcs)
        {

            DateTime fechaPeticion = DateTime.Now;

            if (rfcs == null)
            {
                rfcs = new List<string>();
            }
            rfcs.Add(rfc);
            rfcs = rfcs.Distinct().ToList();

            Peticion peticion = new Peticion { RFC = rfc, TipoOrigen = (int)EnumTipoOrigen.ARCA, Accion = (int)EnumAccionesArca.ObtenerInformacionContribuyente, Fecha = DateTime.Now };
            InterceptorMensajes interceptor = new InterceptorMensajes();
            Stopwatch duracion = new Stopwatch();

            ServicioArcaCF.ObtenerInformacionContribuyentesRequest request = new ServicioArcaCF.ObtenerInformacionContribuyentesRequest(rfcs.ToArray(), fechaPeticion);
            ServicioArcaCF.ServicioArcaCreditosFiscalesClient cliente;
            try
            {
                using (cliente = new ServicioArcaCF.ServicioArcaCreditosFiscalesClient())
                {
                    cliente.Endpoint.Behaviors.Add(interceptor);
                    duracion.Start();
                    List<ServicioArcaCF.DocumentosDeterminantes> doctosDet = cliente.ObtenerInformacionContribuyentes(request).ObtenerInformacionContribuyentesResult.ToList<ServicioArcaCF.DocumentosDeterminantes>();
                   /* List<ServicioArcaCF.DocumentosDeterminantes> doctosDet = new List<ServicioArcaCF.DocumentosDeterminantes>();
                    System.IO.StreamReader file = new System.IO.StreamReader(@"C:\C\prueba.xml");
                    String s = file.ReadToEnd();
                    ServicioArcaCF.DocumentosDeterminantes det = Sat.CreditosFiscales.Comunes.Herramientas.MetodosComunes.Deserializa<ServicioArcaCF.DocumentosDeterminantes>(s);
                    file.Close();
                    doctosDet.Add(det);*/
                    duracion.Stop();
                    cliente.Close();
                    //string xmlSerializado = Sat.CreditosFiscales.Comunes.Herramientas.MetodosComunes.Serializa(doctosDet);
                    return doctosDet;
                }
            }
            catch (Exception ex)
            {
                if (duracion.IsRunning)
                    duracion.Stop();

                peticion.HuboError = true;
                peticion.Observaciones = ex.Message;

                string mensaje = string.Format("Error al invocar ObtenerInformacionContribuyentes::RFC='{0}', RFCs='{1}', Fecha peticion={2}::. ", rfc, string.Join(",", rfcs.ToArray()), fechaPeticion);
                string ticket = AccesoLogEventos.LogEventos.EscribirEntradaLog((int)EnumErroresArca.ErrorAlInvocarARCA,
                    mensaje, ex);
                throw new ExcepcionTipificada("Por el momento no es posible atender su petición; inténtelo más tarde.", ex, ticket);
            }
            finally
            {
                peticion.Duracion = Convert.ToDecimal(duracion.Elapsed.TotalSeconds);
                peticion.XmlPeticion = interceptor.XMLPeticion;
                peticion.XmlRespuesta = interceptor.XMLRespuesta;
                Peticiones.GuardaPeticion(peticion);
            }

        }

        private static List<ServicioArcaCF.DocumentosDeterminantes> ObtenerInformacionContribuyentePuroArca(
            string rfc, string documentoDeterminante, int idAutoridad, int claveAlr, DateTime fechaDocumento)
        {

            Peticion peticion = new Peticion { RFC = rfc, TipoOrigen = (int)EnumTipoOrigen.ARCA, Accion = (int)EnumAccionesArca.ObtenerInformacionContribuyentePuro, Fecha = DateTime.Now };
            InterceptorMensajes interceptor = new InterceptorMensajes();
            Stopwatch duracion = new Stopwatch();

            ServicioArcaCF.ObtenerInformacionContribuyentePuroRequest request = new ServicioArcaCF.ObtenerInformacionContribuyentePuroRequest(rfc, documentoDeterminante, idAutoridad, claveAlr, fechaDocumento, DateTime.Now);
            try
            {
                using (ServicioArcaCF.ServicioArcaCreditosFiscalesClient cliente = new ServicioArcaCF.ServicioArcaCreditosFiscalesClient())
                {
                    cliente.Endpoint.Behaviors.Add(interceptor);
                    duracion.Start();
                    List<ServicioArcaCF.DocumentosDeterminantes> doctosDet = cliente.ObtenerInformacionContribuyentePuro(request).ObtenerInformacionContribuyentePuroResult.ToList();
                    duracion.Stop();
                    cliente.Close();
                    return doctosDet;
                }
            }
            catch (Exception ex)
            {
                if (duracion.IsRunning)
                    duracion.Stop();

                peticion.HuboError = true;
                peticion.Observaciones = ex.Message;
                string mensaje = string.Format("Error al invocar ObtenerInformacionContribuyentePuro::RFC={0}, Documento determinate={1}, Autoridad={2}, ALR={3}, Fecha documento={4}::.",
                    rfc, documentoDeterminante, idAutoridad, claveAlr, fechaDocumento);
                string ticket = AccesoLogEventos.LogEventos.EscribirEntradaLog((int)EnumErroresArca.ErrorAlInvocarArcaContribuyentePuro, mensaje, ex);
                throw new ExcepcionTipificada("Por el momento no es posible atender su petición; inténtelo más tarde.", ex, ticket);
            }
            finally
            {
                peticion.Duracion = Convert.ToDecimal(duracion.Elapsed.TotalSeconds);
                peticion.XmlPeticion = interceptor.XMLPeticion;
                peticion.XmlRespuesta = interceptor.XMLRespuesta;
                Peticiones.GuardaPeticion(peticion);
            }
        }

        #endregion

    }
}
