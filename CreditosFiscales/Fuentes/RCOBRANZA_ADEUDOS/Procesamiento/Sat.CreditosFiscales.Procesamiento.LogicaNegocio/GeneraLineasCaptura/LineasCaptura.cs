
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Procesamiento.LogicaNegocio.GeneraLineasCaptura:Sat.CreditosFiscales.Procesamiento.LogicaNegocio.GeneraLineasCaptura.LineasCaptura:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Linq;
using Sat.CreditosFiscales.Datos.AccesoDatos.GeneraFormato;
using Sat.CreditosFiscales.Datos.AccesoDatos.GeneraLineaCaptura;
using Sat.CreditosFiscales.Comunes.Entidades.AccesoLogEventos;
using Sat.CreditosFiscales.Comunes.Entidades.CodigosError;
using Sat.CreditosFiscales.Procesamiento.LogicaNegocio.AccesoLogEventos;
using Sat.CreditosFiscales.Comunes.Entidades.GeneraFormato;
using Sat.CreditosFiscales.Comunes.Entidades.Catalogos;
using Sat.CreditosFiscales.Comunes.Entidades;
using Sat.CreditosFiscales.Procesamiento.LogicaNegocio.Catalogos;
using Sat.CreditosFiscales.Procesamiento.LogicaNegocio.ServicioGeneraFormatoImpresion;
using Sat.CreditosFiscales.Comunes.Entidades.Seguridad;

namespace Sat.CreditosFiscales.Procesamiento.LogicaNegocio.GeneraLineasCaptura
{
    /// <summary>
    /// Clase para el manejo de las líneas de captura
    /// </summary>
    public static class LineasCaptura
    {
        /// <summary>
        /// Método para generar la líneas de captura de una solicitud realizada por el contribuyente
        /// </summary>
        /// <param name="idSolicitud">Identificador de la solicitud</param>
        /// <param name="usuario"><see cref="Usuario"/></param>
        /// <param name="excepcion"><see cref="ExcepcionTipificada"/>Excepción o mensaje regresado por referencia al obtener las líneas de captura</param>
        /// <returns>Lista de líneas de captura<see cref="LineaCaptura"/>></returns>
        public static List<LineaCaptura> GenerarLineasDeCaptura(Int64 idSolicitud, Usuario usuario, ref ExcepcionTipificada excepcion)
        {
            List<LineaCaptura> listaLineasCaptura = new List<LineaCaptura>();
            Solicitud solicitud = null;
            try
            {
                // Obtenemos los datos de la solicitud
                using (DalSolicitud dal = new DalSolicitud())
                {
                    solicitud = dal.ObtenerSolicitud(idSolicitud);
                }

                // Enviamos una petición al servicio proxy del traductor para la recuperación de las líneas de captura
                listaLineasCaptura = Servicios.ProxyManagerTraductor.RecuperaLineasDeCaptura(solicitud.Documentos, usuario, ref excepcion);

                // Guardamos las lineas de captura recuperadas por el servicio
                new DalLineaCaptura().GuardarLineasCaptura(solicitud.IdSolicitud, listaLineasCaptura);

            }
            catch (ExcepcionTipificada ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                string ticket = LogEventos.EscribirEntradaLog((int)EnumErroresLineaCaptura.ErrorAlGenerarLineasDeCaptura, ex);
                throw new ExcepcionTipificada("No se logró generar las líneas de captura.", ex, ticket);
            }


            return listaLineasCaptura;
        }
        /// <summary>
        /// Método para la generación del archivo en un mapa de bytes de acuerdo a una lista de folios
        /// </summary>
        /// <param name="listaDeFolios">Lista de folios</param>
        /// <returns>Mapa de bytes</returns>
        public static byte[] GeneraArchivos(List<string> listaDeFolios)
        {
            try
            {
                // Enviamos una petición al servicio de impresión para la obtención del archivos de impresión
                return Servicios.ProxyManagerImpresion.GeneraArchivos(EnumTemplate.FormatoParaPago, listaDeFolios);
            }
            catch (ExcepcionGeneracionLineaCaptura err)
            {
                string ticket = LogEventos.EscribirEntradaLog((int)EnumErroresLineaCaptura.ErrorAlGenerarArhivo, err);
                throw new ExcepcionTipificada(err.Message, err, ticket);
            }
            catch (Exception err)
            {
                string ticket = LogEventos.EscribirEntradaLog((int)EnumErroresLineaCaptura.ErrorGenerico, err);
                throw new ExcepcionTipificada("No se logró generar el archivo.", err, ticket);
            }
        }
        /// <summary>
        /// Método para obtener los documentos asociados a una línea de captura
        /// </summary>
        /// <param name="lineaCaptura">Línea de captura</param>
        /// <param name="rfc">Rfc del contribuyente</param>
        /// <returns>Lista de documentos</returns>
        public static List<string> ObtenerDocumentosAsociados(string lineaCaptura, string rfc)
        {
            try
            {
                // Enviamos una petición al servicio proxy del traductor para recuperar los documentos asociados a la línea de captura
                return Servicios.ProxyManagerTraductor.RecuperaDocumentosEnLC(lineaCaptura, rfc);

            }
            catch (ExcepcionGeneracionLineaCaptura err)
            {
                string ticket = LogEventos.EscribirEntradaLog((int)EnumErroresLineaCaptura.ErrorAlGenerarArhivo, err);
                throw new ExcepcionTipificada(err.Message, err, ticket);
            }
            catch (Exception err)
            {
                string ticket = LogEventos.EscribirEntradaLog((int)EnumErroresLineaCaptura.ErrorGenerico, err);
                throw new ExcepcionTipificada("No se logró obtener los documentos asociados a la línea de captura.", err, ticket);
            }

        }
        /// <summary>
        /// Método para obtener la información de un usuario verificando si es un deudor puro
        /// </summary>
        /// <param name="idSolicitud">Identificador de la solicitud</param>
        /// <returns><see cref="Usuario"/></returns>
        public static Usuario ObtenerInfoUsuarioDeudorPuro(Int64 idSolicitud)
        {
            var usuario = new Usuario();
            try
            {
                // Obtenemos los datos del usuario de acuerdo a la solicitud
                usuario = new DalSolicitud().ObtenerUsuarioSolicitud(idSolicitud);
                usuario.EsContribuyentePuro = true;
                // Mediante el rfc que registra el usuario lo buscamos en IDC

                InfoContribuyente infoContribuyente;
                try
                {
                    infoContribuyente = new Seguridad.AccesoIdc().ObtenerInfoContribuyente(usuario.Rfc);
                }
                catch (ExcepcionTipificada)
                {
                    infoContribuyente = null;
                }

                // Si encontro la info del contribuyente cambiamos los datos del usuario registrado
                if (infoContribuyente != null)
                {
                    usuario.IdALR = (byte)infoContribuyente.IdALR;
                    usuario.Rfc = infoContribuyente.RfcSolicitado;
                    usuario.Nombres = infoContribuyente.Nombre;
                    usuario.ApellidoPaterno = infoContribuyente.ApellidoPaterno;
                    usuario.ApellidoMaterno = infoContribuyente.ApellidoMaterno;
                    usuario.RazonSocial = infoContribuyente.RazonSocial;
                    usuario.BoId = infoContribuyente.BoId;
                    usuario.EsContribuyentePuro = false;
                }
                

            }
            catch (ExcepcionTipificada ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                string ticket = LogEventos.EscribirEntradaLog((int)EnumErroresLineaCaptura.ErrorAlObtenerUsuarioDeudorPuro, ex);
                throw new ExcepcionTipificada("No se logró obtener la información del deudor puro.", ex, ticket);
            }

            return usuario;
        }

      


        public static LineaCaptura RecuperaDatosBaseLineaDeCaptura(List<DocumentoDeterminante> determinantes)
        {
            LineaCaptura lineaCaptura = new LineaCaptura();
            long importePagarLC = 0;
            bool conceptoAnual = false;
            bool conceptoMensual = false;
            bool conceptoCausaRecargo = false; //Conceptos que causan recargos y no tienen periodicidad, es decir cuentan con fecha de causación.
            bool conceptoDescuento30;//Se debe limpiar por cada documento ya que considera la fecha de notificación
            bool conceptoDescuento45;//Se debe limpiar por cada documento ya que considera la fecha de notificación

            List<string> idsConceptosCausanRecargos = ApplicationSettings.ConsultaConfiguracion<string>("IdsConceptosCausaRecargos").Split('|').ToList();
            List<string> idsDescuentos30 = ApplicationSettings.ConsultaConfiguracion<string>("IdsDescuentos30").Split('|').ToList();
            List<string> idsDescuentos45 = ApplicationSettings.ConsultaConfiguracion<string>("IdsDescuentos45").Split('|').ToList();

            var listaFechas = new List<DateTime>();
            var catInhabiles = Catalogos.Catalogo.ObtenerCatalogoDiaInhabil();
            var fechaOperacion = DateTime.Now.Date;

            //1 Agregamos la fecha de vencimiento obtenida por la fecha de publicación del INPC
            var catInpc = Catalogos.Catalogo.ObtenerCatalogoFechaINPC().Where(inpc => inpc.Fecha > fechaOperacion).ToList();
            //AgregaFecha(ref listaFechas, fechaOperacion, RecuperarVigenciaXFechaInpc(catInpc)); //Se pasa al final del proceso

            foreach (DocumentoDeterminante doc in determinantes)
            {
                conceptoDescuento30 = false;
                conceptoDescuento45 = false;
                long importePagarDocumento = 0;
                foreach (DocumentoDeterminanteConcepto concepto in doc.Conceptos)
                {
                    if (concepto.IdPeriodicidad == ApplicationSettings.ConsultaConfiguracion<int>("IdPeriodicidadAnual"))
                    {
                        conceptoAnual = true;
                    }
                    if (concepto.IdPeriodicidad == ApplicationSettings.ConsultaConfiguracion<int>("IdPeriodicidadMensual"))
                    {
                        conceptoMensual = true;
                    }
                    if (concepto.IdPeriodicidad == ApplicationSettings.ConsultaConfiguracion<int>("IdPeriodicidadSinPeriodo"))
                    {
                        if (idsConceptosCausanRecargos.Exists(m => m == concepto.IdConcepto.ToString()))
                            conceptoCausaRecargo = true;
                    }
                    //Si tiene fecha de notificación revisa los descuentos
                    if (doc.FechaNotificacion > DateTime.MinValue)
                    {
                        foreach (DescuentoConcepto descuento in concepto.Descuentos)
                        {
                            if (idsDescuentos30.Exists(m => m == descuento.IdDescuento))
                                conceptoDescuento30 = true;
                            if (idsDescuentos45.Exists(m => m == descuento.IdDescuento))
                                conceptoDescuento45 = true;
                        }
                    }
                    //else
                    //    Console.WriteLine("Fecha de notificación igual a 01/01/1900 que se transformo en DateTime.Min");

                    foreach(DocumentoDeterminanteConceptoHijo conceptoHijo in concepto.ConceptosHijo)
                    {
                        if (conceptoHijo.IdPeriodicidad == ApplicationSettings.ConsultaConfiguracion<int>("IdPeriodicidadAnual"))
                        {
                            conceptoAnual = true;
                        }
                        if (conceptoHijo.IdPeriodicidad == ApplicationSettings.ConsultaConfiguracion<int>("IdPeriodicidadMensual"))
                        {
                            conceptoMensual = true;
                        }
                        if (conceptoHijo.IdPeriodicidad == ApplicationSettings.ConsultaConfiguracion<int>("IdPeriodicidadSinPeriodo"))
                        {
                            if (idsConceptosCausanRecargos.Exists(m => m == conceptoHijo.IdConcepto.ToString()))
                                conceptoCausaRecargo = true;
                        }

                        //Si tiene fecha de notificación revisa los descuentos
                        if (doc.FechaNotificacion > DateTime.MinValue)
                        {
                            foreach (DescuentoConceptoHijo descuento in conceptoHijo.Descuentos)
                            {
                                if (idsDescuentos30.Exists(m => m == descuento.IdDescuento))
                                    conceptoDescuento30 = true;
                                if (idsDescuentos45.Exists(m => m == descuento.IdDescuento))
                                    conceptoDescuento45 = true;
                            }
                        }
                        //else
                        //    Console.WriteLine("Fecha de notificación igual a 01/01/1900 que se transformo en DateTime.Min");
                        importePagarDocumento += Convert.ToInt64(conceptoHijo.ImportePagar);
                    }

                    importePagarDocumento += Convert.ToInt64(concepto.ImportePagar);
                }

                //2.1 Validacion de descuento 30, se aplica por documento determinante
                if (conceptoDescuento30)
                {
                    AgregaFecha(ref listaFechas, fechaOperacion, RecuperarVigenciaXConceptoDescuento30(doc.FechaNotificacion, ref catInhabiles));
                }
                //2.2 Validacion de descuento 45, se aplica por documento determinante
                if (conceptoDescuento45)
                {
                    AgregaFecha(ref listaFechas, fechaOperacion, RecuperarVigenciaXConceptoDescuento45(doc.FechaNotificacion, ref catInhabiles));
                }

                doc.ImportePagar = importePagarDocumento;
                importePagarLC += Convert.ToInt64(doc.ImportePagar);
            }

            if (listaFechas.Count() == 0) //Si ya tiene fechas es porque aplico la regla de los 30 o 45 dias
            {
                //Agrega Fecha del INPC
                AgregaFecha(ref listaFechas, fechaOperacion, RecuperarVigenciaXFechaInpc(catInpc));

                //3. Validacion de la fecha de recargo mensual
                if (conceptoMensual)
                {
                    AgregaFecha(ref listaFechas, fechaOperacion, RecuperarVigenciaXFechaRecargoMensual(fechaOperacion, ref catInhabiles));
                }

                //4. Validacion de la fecha de recargo anual

                if (conceptoAnual)
                {
                    AgregaFecha(ref listaFechas, fechaOperacion, RecuperarVigenciaXFechaRecargoAnual(fechaOperacion, ref catInhabiles));
                }

                //5. Validación  de la fecha por concepto causa recargos

                if (conceptoCausaRecargo)
                {
                    AgregaFecha(ref listaFechas, fechaOperacion, RecuperarVigenciaXConceptoCausaRecargo(fechaOperacion, ref catInhabiles));
                }
            }

            lineaCaptura.FechaVencimiento = listaFechas.Min().ToString("dd/MM/yyyy");
            lineaCaptura.ImporteTotal = importePagarLC;
            return lineaCaptura;

        }

        private static void AgregaFecha(ref List<DateTime> fechasVencimiento, DateTime fechaOperacion, DateTime fechaPorAgregar)
        {
            if (fechaPorAgregar.Date >= fechaOperacion.Date)
                fechasVencimiento.Add(fechaPorAgregar);

        }

        private static DateTime RecuperarVigenciaXFechaInpc(List<FechaINPC> catalogoInpc)
        {
            var fechaInpc = catalogoInpc.Min(fecha => fecha.Fecha);
            return fechaInpc.AddDays(-1);
        }

        private static DateTime RecuperarVigenciaXConceptoDescuento30(DateTime fechaNotificacion,ref List<DiaInhabil> catalogoInhabiles)
        {
            int dias = ApplicationSettings.ConsultaConfiguracion<int>("DiasDescuentos30");
            int i = 0;
            DateTime fechaVencimiento = fechaNotificacion;
            
            while (i < dias)
            {   fechaVencimiento = fechaVencimiento.AddDays(1);
                if (!EsInhabil(fechaVencimiento, catalogoInhabiles))
                {i++;}
            }

            fechaVencimiento = RecuperaVigenciaXDiaInhabil(fechaVencimiento, ref catalogoInhabiles);
            return fechaVencimiento;
        }

        private static bool EsInhabil(DateTime fecha, List<DiaInhabil> catalogoInhabiles)
        {
            bool resultado = false;

            if(catalogoInhabiles.Exists(f => f.Fecha.Date == fecha.Date) || fecha.DayOfWeek == DayOfWeek.Saturday || fecha.DayOfWeek == DayOfWeek.Sunday)
            {
                resultado = true;
            }

            return (resultado);
        }

        private static DateTime RecuperarVigenciaXConceptoDescuento45(DateTime fechaNotificacion,ref List<DiaInhabil> catalogoInhabiles)
        {
            int dias = ApplicationSettings.ConsultaConfiguracion<int>("DiasDescuentos45");
            int i = 0;
            DateTime fechaVencimiento = fechaNotificacion;

            while (i < dias)
            {   fechaVencimiento = fechaVencimiento.AddDays(1);
                if (!EsInhabil(fechaVencimiento,catalogoInhabiles))
                {i++;}
            }

            fechaVencimiento = RecuperaVigenciaXDiaInhabil(fechaVencimiento, ref catalogoInhabiles);
            return fechaVencimiento;
        }

        private static DateTime RecuperarVigenciaXConceptoCausaRecargo(DateTime fechaOperacion, ref List<DiaInhabil> catalogoInhabiles)
        {
            DateTime fechaVencimiento = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
            fechaVencimiento = RecuperaVigenciaXDiaInhabil(fechaVencimiento, ref catalogoInhabiles);
            return fechaVencimiento;
        }

        private static DateTime RecuperarVigenciaXFechaRecargoMensual(DateTime fechaVencimiento,ref List<DiaInhabil> catalogoInhabiles)
        {
            fechaVencimiento = fechaVencimiento.Day > 17 ? new DateTime(fechaVencimiento.Year, fechaVencimiento.Month, 17).AddMonths(1) : new DateTime(fechaVencimiento.Year, fechaVencimiento.Month, 17);
            fechaVencimiento = RecuperaVigenciaXDiaInhabil(fechaVencimiento,ref catalogoInhabiles);
            return fechaVencimiento;
        }

        private static DateTime RecuperarVigenciaXFechaRecargoAnual(DateTime fechaVencimiento,ref List<DiaInhabil> catalogoInhabiles)
        {
            var mesOperacion = fechaVencimiento.Month;
            fechaVencimiento = fechaVencimiento.AddMonths(1);
            while (fechaVencimiento.Month > mesOperacion)
            {
                fechaVencimiento = fechaVencimiento.AddDays(-1);
            }

            fechaVencimiento = RecuperaVigenciaXDiaInhabil(fechaVencimiento,ref catalogoInhabiles);
            return fechaVencimiento;
        }

        private static DateTime RecuperaVigenciaXDiaInhabil(DateTime fechaVencimiento,ref List<DiaInhabil> catalogoInhabiles)
        {
            while (catalogoInhabiles.Exists(fecha => fecha.Fecha.Date == fechaVencimiento.Date) || fechaVencimiento.DayOfWeek == DayOfWeek.Saturday || fechaVencimiento.DayOfWeek == DayOfWeek.Sunday)
            {
                fechaVencimiento = fechaVencimiento.AddDays(1);
            }

            return fechaVencimiento;
        }

    }
}
