using Sat.CreditosFiscales.Comunes.Entidades.CodigosError;
using Sat.CreditosFiscales.Procesamiento.LogicaNegocio.AccesoLogEventos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Ionic.Zip;

namespace Sat.CreditosFiscales.Procesamiento.PagosV.NegocioPagos
{
    using Sat.CreditosFiscales.Comunes.Entidades.Pagos;
    using Sat.CreditosFiscales.Comunes.Herramientas;
    using Sat.CreditosFiscales.Comunes.Herramientas.Archivo;
    using Sat.CreditosFiscales.Datos.AccesoDatos.Pagos;
    using System.IO;
    using System.Threading.Tasks;
    public class SIAT
    {

        bool bitacoriza = Boolean.Parse(ConfigurationManager.AppSettings["Bitacoriza"]);
        long ArchivoActual = 0;

        public bool GeneracionLineasDePago(long idProceso, Dictionary<string, object> archivosProcesar, bool esReproceso, long idArchivoActual)
        {
            //var listArchivosGral = new Dictionary<byte, List<InfoArchivoSIR>>();
            var listaArchivos = new List<InfoArchivoSIAT>();
            try
            {
                if (archivosProcesar != null && archivosProcesar.Count > 0)
                {
                    var cifraControl = archivosProcesar.FirstOrDefault(x => x.Key.StartsWith("Q"));
                    var detalle = archivosProcesar.FirstOrDefault(x => x.Key.StartsWith("E"));
                    Console.WriteLine("Procesando MAT archivo: " + detalle.Key);
                    ManejadorArchivos.WriteMessage("Procesando archivo MAT : " + detalle.Key);
                    listaArchivos = ProcesaArchivo(idProceso, cifraControl, detalle, idArchivoActual);
                    AlmacenarInformacionPorArchivo(idProceso, listaArchivos, esReproceso);
                    SIATDALPagos.ActualizaEstadoProceso(idProceso, idArchivoActual, 3);  //Procesado

                }
            }
            catch (Exception exception)
            {
                if (archivosProcesar != null)
                    LogEventos.EscribirEntradaLog((int)EnumErroresPagos.ErrorProcesandoArchivos,
                                                  archivosProcesar.FirstOrDefault(x => x.Key.StartsWith("Q")).Key, exception);
                else
                {
                    LogEventos.EscribirEntradaLog((int)EnumErroresPagos.ErrorProcesandoArchivos, exception);
                }
                return false;
            }
            return true;
        }

        public bool GeneracionLineasDePagoVirtuales(long idProceso, bool esReproceso, List<TblLineaCaptura> lineas, long idArchi)
        {
            //var listArchivosGral = new Dictionary<byte, List<InfoArchivoSIR>>();
            var listaArchivos = new List<InfoArchivoSIAT>();
            try
            {
                listaArchivos = ProcesaArchivo(idProceso, lineas, idArchi); //FGR pensar en los diferentes layouts por tipo de crédito.        
                AlmacenarInformacionPorArchivoVirtual(idProceso, listaArchivos, esReproceso, idArchi);
            }
            catch (Exception exception)
            {

                LogEventos.EscribirEntradaLog((int)EnumErroresPagos.ErrorProcesandoArchivos, exception);
                return false;
            }
            return true;
        }
        private List<InfoArchivoSIAT> ProcesaArchivo(long idProceso, KeyValuePair<string, object> cifraControl, KeyValuePair<string, object> detalle, long idArchivoActual)
        {
            ProcesamientoArchivos procApoyo = new ProcesamientoArchivos();
            var contenidoArchivos = new List<InfoArchivoSIAT>();
            try
            {
                String formatoFecha = ConfigurationManager.AppSettings["ForFechaPagoSIAT"].ToString();
                var lineasDeCaptura = procApoyo.ObtenerLineasCaptura(detalle);

                if (lineasDeCaptura != null)
                {
                    lineasDeCaptura = procApoyo.ValidarLineasDeCaptura(lineasDeCaptura, detalle, false, idProceso);

                    foreach (TblLineaCaptura lineaCaptura in lineasDeCaptura) //Base de datos
                    {
                        CreditosFiscales pago = SIATDALPagos.ObtenerPagoSIAT(lineaCaptura, formatoFecha);
                        if (pago.DatosGenerales.TipoLinea != -1 && pago.DatosGenerales.TipoLinea != 0 && pago.DatosGenerales.TipoLinea != 4)//Si es SIAT/MAT
                        {
                            InfoArchivoSIAT archivo = new InfoArchivoSIAT();
                            archivo.Contenido = MetodosComunes.Serializa(pago);
                            archivo.Generales = lineaCaptura;
                            archivo.NomArchivoDetalle = detalle.Key;
                            contenidoArchivos.Add(archivo);
                        }
                        else
                        {
                            SIATDALPagos.InsertarNoProcesados(idProceso, detalle.Key, lineaCaptura.IdSolicitud);
                            SIATDALPagos.ActualizaErrorProcesoLC(idProceso, idArchivoActual, lineaCaptura.LineaCaptura, (int)EnumErroresProceso.TipoLineaInvalido);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                LogEventos.EscribirEntradaLog((int)EnumErroresPagos.ErrorProcesandoArchivos, exception);
                throw;
            }
            return contenidoArchivos;
        }
        private List<InfoArchivoSIAT> ProcesaArchivo(long idProceso, List<TblLineaCaptura> lineasDeCaptura, long idArchi)
        {
            ProcesamientoArchivos procApoyo = new ProcesamientoArchivos();
            var contenidoArchivos = new List<InfoArchivoSIAT>();
            int nl = 1;
            try
            {
                String formatoFecha = ConfigurationManager.AppSettings["ForFechaPagoSIAT"].ToString();
                if (lineasDeCaptura != null)
                {
                    if (lineasDeCaptura.Count() != 0)
                    {
                        Console.WriteLine("Procesando Transacciones Virtuales MAT");
                        ManejadorArchivos.WriteMessage("\nProcesando Transacciones Virtuales MAT");
                    }
                    foreach (TblLineaCaptura lineaCaptura in lineasDeCaptura) //Base de datos
                    {
                        CreditosFiscales pago = SIATDALPagos.ObtenerPagoSIAT(lineaCaptura, formatoFecha);


                        RegistraDetalleVirtual(idProceso, idArchi, pago, nl, lineaCaptura.IdSolicitud);
                        nl++;


                        if (pago.DatosGenerales.TipoLinea != -1 && pago.DatosGenerales.TipoLinea != 0 && pago.DatosGenerales.TipoLinea != 4)//Si es SIAT/MAT
                        {
                            InfoArchivoSIAT archivo = new InfoArchivoSIAT();
                            archivo.Contenido = MetodosComunes.Serializa(pago);
                            archivo.Generales = lineaCaptura;
                            archivo.NomArchivoDetalle = Constantes.Virtual;
                            contenidoArchivos.Add(archivo);
                        }
                        else
                        {
                            SIATDALPagos.InsertarNoProcesados(idProceso, "Virtual", lineaCaptura.IdSolicitud);

                        }
                    }
                }
            }
            catch (Exception exception)
            {
                LogEventos.EscribirEntradaLog((int)EnumErroresPagos.ErrorProcesandoArchivos, exception);
                throw;
            }
            return contenidoArchivos;
        }
        private void AlmacenarInformacionPorArchivo(long idProceso, List<InfoArchivoSIAT> listArchivosGral, bool esReproceso)
        {
            if (esReproceso)
                SIATDALPagos.EliminaRegistroExistente(idProceso);
            string nombreArchivo = "";

            try
            {
                foreach (InfoArchivoSIAT archivoLocal in listArchivosGral)
                {
                    SIATDALPagos.InsertarDetallePagos(idProceso, archivoLocal);
                    nombreArchivo = archivoLocal.NomArchivoDetalle;
                }



            }
            catch (Exception exception)
            {
                LogEventos.EscribirEntradaLog((int)EnumErroresPagos.ErrorInsertarDetallePagos, exception);
                throw;
            }

        }
        private void AlmacenarInformacionPorArchivoVirtual(long idProceso, List<InfoArchivoSIAT> listArchivosGral, bool esReproceso, long idArchi)
        {
            try
            {
                if (esReproceso)
                    DalPagos.EliminaRegistroExistenteVirtual(idProceso, Constantes.Virtual);

                foreach (InfoArchivoSIAT archivoLocal in listArchivosGral)
                {

                    SIATDALPagos.InsertarDetallePagos(idProceso, archivoLocal);
                }

                SIATDALPagos.ActualizaEstadoProceso(idProceso, idArchi, 3);

            }
            catch (Exception exception)
            {
                LogEventos.EscribirEntradaLog((int)EnumErroresPagos.ErrorInsertarDetallePagos, exception);
                throw;
            }

        }
        public bool CrearArchivosSalida(long idProceso, int pagina, int registros)
        {
            var rutaArchivos = ConfigurationManager.AppSettings["RutaArchivosMAT"];
            var rutaRespaldo = ConfigurationManager.AppSettings["RutaArchivosProcesados"];
            var iTipoArchivo = ConfigurationManager.AppSettings["TipoArchivo"];
            String directorioZIP = ConfigurationManager.AppSettings["DirectorioZIPSIAT"];

            var rutaArchivosXML = ConfigurationManager.AppSettings["RutaArchivosMATXML"];

            if (bitacoriza)
                ManejadorArchivos.WriteMessage("Rutas Archivos Salida: rutaArchivos|rutaRespaldo|directorioZIP " + rutaArchivos + "|" + rutaRespaldo + "|" + directorioZIP);


            NombreSIAT nombres = new NombreSIAT();

            // POL 8241
            ArchivosZip DatosZip = new ArchivosZip();
            var DetallesLC = new List<TblControlPagosDet>();

            String nomArchivoCifras = nombres.getCifras();

            try
            {
                //Aqui obtiene los datos de BD para generar los XML
                var listArchivos = SIATDALPagos.ObtenerInformacionArchivosDeSalida(idProceso, pagina, registros, Convert.ToInt16(iTipoArchivo));

                if (listArchivos.Count() != 0)
                {
                    ManejadorArchivos.WriteMessage("\n Creando Archivos de salida MAT:" + rutaArchivosXML);
                    Console.WriteLine("Creando Archivos de salida MAT:" + rutaArchivosXML);
                }
                else
                {
                    return false;    // Ya no hay archivos a generar
                }

                using (ZipFile zip = new ZipFile())
                {
                    List<String> consecutivos = new List<String>();
                    foreach (InfoArchivoSIAT archivoLocal in listArchivos)
                    {

                        int consecutivo = 0;
                        var nombreArchivo = nombres.getArchivoXML(archivoLocal, consecutivo);
                        var nombreArchivoPuro = archivoLocal.Generales.FolioSolicitud == null ? archivoLocal.Generales.LineaCaptura : archivoLocal.Generales.FolioSolicitud;
                        if (consecutivos.Contains(nombreArchivoPuro))
                        {
                            consecutivo = consecutivos.Count(n => n == nombreArchivoPuro);
                            nombreArchivo = nombres.getArchivoXML(archivoLocal, consecutivo);
                        }

                        // rutaArchivos = C://Temp//CreditosFiscales//ArchivosParaSiat 
                        //this.ReemplazaArchivo(new StringBuilder(archivoLocal.Contenido), rutaArchivos, nombreArchivo);
                        this.ReemplazaArchivo(new StringBuilder(archivoLocal.Contenido), rutaArchivosXML, nombreArchivo);

                        // zip.AddFile(Path.Combine(rutaArchivos, nombreArchivo), directorioZIP);

                        zip.AddFile(Path.Combine(rutaArchivosXML, nombreArchivo), directorioZIP);
                        ManejadorArchivos.EscribeArchivo(nombres.GeneraLineaCifras(archivoLocal, consecutivo), rutaArchivosXML, nomArchivoCifras);

                        consecutivos.Add(nombreArchivoPuro);

                        //POL 8142
                        TblControlPagosDet Ad = new TblControlPagosDet();
                        Ad.LineaCaptura = archivoLocal.Generales.LineaCaptura;
                        Ad.NombreXML = nombreArchivo.Replace(".xml", "");
                        Ad.NumOperacion = archivoLocal.Generales.FolioSolicitud;
                        DetallesLC.Add(Ad);

                    }


                    if (listArchivos.Count() != 0)
                    {
                        zip.AddFile(Path.Combine(rutaArchivosXML, nomArchivoCifras), directorioZIP);

                        string nombreZip = nombres.ObtenerRutaZIP(rutaArchivos);
                        string nombreArchivoSolo = Path.GetFileNameWithoutExtension(nombreZip);

                        zip.Save(nombreZip);
                        zip.Save(nombres.ObtenerRutaZIP(rutaRespaldo));


                        // LLamado para guardar datos del archivo ZIP y actaulziar DEtalle

                        try
                        {
                            SIATDALPagos.ActualizaNombreZip(idProceso, nombreArchivoSolo, DetallesLC, Convert.ToInt16(iTipoArchivo));
                        }
                        catch (Exception ex)
                        {
                            LogEventos.EscribirEntradaLog(2010, "Error al actualizar archivo ZIP Idproceso: " + idProceso, System.Diagnostics.EventLogEntryType.Warning, LogEventos.ApplicationName);

                        }

                        foreach (ZipEntry entry in zip)
                        {
                    //        ManejadorArchivos.BorraArchivo(rutaArchivos, entry.FileName);
                            ManejadorArchivos.BorraArchivo(rutaArchivosXML, entry.FileName);
                        }
                        ManejadorArchivos.BorraArchivo(rutaArchivosXML, nomArchivoCifras);
                    }
                }

                return true;   // Faltan archivos de generar
            }

            catch (Exception exception)
            {
                if (bitacoriza)
                    ManejadorArchivos.WriteMessage("Error al crear ZIP " + exception.Message + "|" + exception.StackTrace);

                LogEventos.EscribirEntradaLog((int)EnumErroresPagos.ErrorCrearProcArchivoSalida, exception);
                throw;
            }
        }

        public void ReemplazaArchivo(StringBuilder mensaje, string rutaArchivo, string nombreArchivo)
        {
            if (string.IsNullOrEmpty(rutaArchivo)) return;
            rutaArchivo = Path.Combine(rutaArchivo, nombreArchivo);
            if (File.Exists(rutaArchivo))
            {
                File.Delete(rutaArchivo);
            }
            Escribe(rutaArchivo, mensaje);
        }

        private void Escribe(string rutaArchivo, StringBuilder mensaje)
        {

            //bool existeArchivo = !File.Exists(rutaArchivo);
            using (var outFile = new StreamWriter(rutaArchivo, true, Encoding.Default))
            {
                outFile.Write(mensaje);
            }
        }





        public long RegistraHeadVirutal(long IdProceso, int Registros)
        {

            // Registra Head
            TblControlPagosHead DatosArchivo = new TblControlPagosHead
            {
                NombreArchivo = "Virtual",
                IdProceso = IdProceso,
                NumRegistros = Registros,
                Importe = 0
            };


            return SIATDALPagos.RegistraArchivoQ(IdProceso, DatosArchivo);

        }


        public bool RegistraDetalleVirtual(long idProceso, long ArchivoActual, CreditosFiscales lineaCaptura, int nl, string Solicitud)
        {


            //if (lineasDeCaptura != null)
            //{


            //if (lineasDeCaptura.Count() != 0)
            //{
            //    Console.WriteLine("Registrando Transacciones Virtuales MAT");
            //    ManejadorArchivos.WriteMessage("Registrando Transacciones Virtuales MAT");
            //}

            //foreach (TblLineaCaptura lineaCaptura in lineasDeCaptura) //Base de datos
            //{

            TblControlPagosDet LineaDetalle = new TblControlPagosDet
            {
                IdArchivo = ArchivoActual,
                NumLinea = nl,
                Consecutivo = "0",
                LineaCaptura = lineaCaptura.DatosGenerales.LineaCaptura,
                FechaPago = lineaCaptura.DatosGenerales.PagoVirtual.FechaPago,
                HoraPago = "00:00",
                Importe = lineaCaptura.DatosGenerales.PagoVirtual.Importe.ToString(),
                NumOperacion = lineaCaptura.DatosGenerales.NumeroDocumento,
                MedioRecepcion = 0,
                Version = "0",
                TipoPago = lineaCaptura.DatosGenerales.TipoPago,
                IdEstado = 2,
                IdZip = 0,
                NombreXML = "",
                IdError = 0,
                idProcesamiento = Solicitud
            };


            SIATDALPagos.RegistraArchivoE(LineaDetalle, true);
            nl++;

            //               }
            //}

            return true;
        }



    }
}
