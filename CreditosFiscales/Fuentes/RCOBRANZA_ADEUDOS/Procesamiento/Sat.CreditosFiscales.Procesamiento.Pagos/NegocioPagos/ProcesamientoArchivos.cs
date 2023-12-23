using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sat.CreditosFiscales.Comunes.Entidades.Catalogos;
using Sat.CreditosFiscales.Comunes.Entidades.CodigosError;
using Sat.CreditosFiscales.Comunes.Entidades.GeneraFormato;
using Sat.CreditosFiscales.Comunes.Entidades.Pagos;
using Sat.CreditosFiscales.Comunes.Herramientas;
using Sat.CreditosFiscales.Comunes.Herramientas.Archivo;
using Sat.CreditosFiscales.Comunes.Herramientas.Enumeradores;
using Sat.CreditosFiscales.Datos.AccesoDatos.Catalogos;
using Sat.CreditosFiscales.Datos.AccesoDatos.Pagos;
using Sat.CreditosFiscales.Procesamiento.LogicaNegocio.AccesoLogEventos;

namespace Sat.CreditosFiscales.Procesamiento.Pagos.NegocioPagos
{
    /// <summary>
    /// Clase que contiene toda la información referente al manejo de archivos.
    /// </summary>
    public class ProcesamientoArchivos
    {

        private List<CatBanco> _listaBancosValidos;

        bool bitacoriza = Boolean.Parse(ConfigurationManager.AppSettings["Bitacoriza"]);

        private IEnumerable<CatBanco> ListaBancosValidos
        {
            get { return _listaBancosValidos ?? (_listaBancosValidos = DalCatalogo.ObtenerCatalogoBancos()); }
        }

        /// <summary>
        /// Sobrecarga de creación de mensaje apartir de la entidad InfoArchivoSIR.
        /// </summary>
        /// <param name="archivosSIR">Lista de entidades para formar una línea de texto.</param>
        /// <returns>Arreglo de líneas de texto para conformar archivos.</returns>
        public static StringBuilder CrearMensaje(IEnumerable<InfoArchivoSIR> archivosSIR)
        {
            var mensaje = new StringBuilder();
            foreach (var archivoSIR in archivosSIR)
            {
                string item = String.Concat("2",
                                            archivoSIR.FechaPago.ToString("yyyyMMdd"),
                                            archivoSIR.LineaDeCaptura,
                                            archivoSIR.RFC.PadLeft(13, ' '),
                                            archivoSIR.ImportePagado.ToString().PadLeft(14, '0'),
                                            archivoSIR.FechaPago.ToString("yyyyMMdd"),
                                            archivoSIR.NumeroDeCreditoSir.ToString().PadLeft(7, '0'),
                    //Constantes.TipoFormulario.PadRight(8, ' '),             //TODO: Aqui hay que verificar los tipos de formulario.
                                            archivoSIR.MontoCondonar.ToString().PadLeft(10, '0'));
                mensaje.Append(item + "\n");
                ManejadorArchivos.WriteMessage(archivoSIR.DetalleArchivo + " " + item + " \n");
            }
            return mensaje;
        }

        /// <summary>
        /// Método que agrupa la información de los archivos de banco y base de datos para descargo en SIR.
        /// </summary>
        /// <param name="archivoSIR">Objeto con la información para descargo en SIR.</param>
        /// <returns>Linea de texto que conforma el detalle de los archivos de desacergo.</returns>
        public static string CrearMensaje(InfoArchivoSIR archivoSIR)
        {
            string item;
            archivoSIR.RFC = (archivoSIR.RFC.Length < 12) ? archivoSIR.RFC.PadRight(13, ' ') : archivoSIR.RFC.PadLeft(13, ' ');
               

            if (archivoSIR.IdTipoDocumento == 22)  //Pagos parciales
            {
                item = string.Concat("1",
                                     archivoSIR.FechaPago.ToString("yyyyMMdd"),
                                     archivoSIR.LineaDeCaptura,
                                     archivoSIR.RFC,
                                     archivoSIR.ImportePagado.ToString().PadLeft(14, '0'),
                                     archivoSIR.FechaPago.ToString("yyyyMMdd"),
                                     archivoSIR.NumeroDeCreditoSir.ToString().PadLeft(7, '0'),
                                     archivoSIR.NumeroDeParcialidad.ToString().PadLeft(2, '0'),
                                     archivoSIR.TipoDeFormulario.PadRight(8, ' '),
                                     archivoSIR.MontoCondonar.ToString().PadLeft(10, '0'));
            }
            else     // resto del mundo
                item = String.Concat("2",
                                           archivoSIR.FechaPago.ToString("yyyyMMdd"),
                                           archivoSIR.LineaDeCaptura,
                                           archivoSIR.RFC,
                                           archivoSIR.ImportePagado.ToString().PadLeft(14, '0'),
                                           archivoSIR.FechaPago.ToString("yyyyMMdd"),
                                           archivoSIR.NumeroDeCreditoSir.ToString().PadLeft(7, '0'),
                                           archivoSIR.TipoDeFormulario.PadRight(8, ' '),
                                           archivoSIR.MontoCondonar.ToString().PadLeft(10, '0'));
            return item;
        }

        /// <summary>
        /// Crear mensajes de archivos para sir.
        /// </summary>
        /// <param name="archivosSIR">Lista de entidades de archivos para sir.</param>
        /// <returns>Arreglo de líneas de texto para formar el mensaje.</returns>
        public static string[] CrearMensajeLineas(IEnumerable<InfoArchivoSIR> archivosSIR)
        {
            var infoArchivoSirs = archivosSIR as List<InfoArchivoSIR> ?? archivosSIR.ToList();
            if (infoArchivoSirs.Any())
            {
                var mensaje = new string[infoArchivoSirs.Count()];
                var cont = 0;
                foreach (var archivoSIR in infoArchivoSirs)
                {
                    string item = String.Concat("2",
                                                archivoSIR.FechaPago.ToString("yyyyMMdd"),
                                                archivoSIR.LineaDeCaptura,
                                                archivoSIR.RFC.PadLeft(13, ' '),
                                                archivoSIR.ImportePagado.ToString().PadLeft(14, '0'),
                                                archivoSIR.FechaPago.ToString("yyyyMMdd"),
                                                archivoSIR.NumeroDeCreditoSir.ToString().PadLeft(7, '0'),
                                                Constantes.TipoFormulario.PadRight(8, ' '),
                                                archivoSIR.MontoCondonar.ToString().PadLeft(10, '0')).Trim();
                    mensaje[cont] = item;
                    cont++;
                }
                return mensaje;
            }
            return null;
        }

        /// <summary>
        /// Crear el archivo con lineas de captura inválidas.
        /// </summary>
        /// <param name="listLineasInvalidas">Lista con las líneas de captura inválidas.</param>
        /// <returns></returns>
        public static StringBuilder CrearMensaje(List<ReporteDiferencias> listLineasInvalidas)
        {
            var mensaje = new StringBuilder();
            foreach (var diferencias in listLineasInvalidas)
            {
                string item = string.Concat(diferencias.Alr,
                                            "|",
                                            diferencias.Rfc,
                                            "|",
                                            diferencias.LineaDeCaptura,
                                            "|",
                                            diferencias.MontoPagadoLinea,
                                            "|",
                                            diferencias.MontoPagarAplicativo,
                                            "|",
                                            diferencias.FechaOperacion.ToString("yyyyMMdd"),
                                            "|",
                                            diferencias.FechaProcesado.ToString(),
                                            "|");
                mensaje.Append(item);
            }

            return mensaje;
        }

        /// <summary>
        /// Método que realiza el procesamiento de archivos seleccionados.
        /// </summary>
        /// <param name="idProceso">IdProceso de ejecución.</param>
        /// <param name="rutaRepositorio">Ruta repositorio de archivos a procesar.</param>
        /// <param name="listaArchivos">Lista de archivos de bancos.</param>
        /// <param name="esReproceso"> </param>
        public void EjecutarProcesamientoArchivos(long idProceso, string rutaRepositorio, List<string> listaArchivos, bool esReproceso)
        {
            var archivosProcesar = new Dictionary<string, object>();
            SIAT siat= new SIAT();
            if(listaArchivos.Count()==0)
            {
                LogEventos.EscribirEntradaLog(2010, "No hay archivos Q y E para procesar en: " + rutaRepositorio, System.Diagnostics.EventLogEntryType.Warning, LogEventos.ApplicationName);
                Console.WriteLine("No hay archivos Q y E para procesar en: " + rutaRepositorio);             
             }
            foreach (var archivo in listaArchivos)
            {
                archivosProcesar = ObtenerArchivosProcesar(rutaRepositorio,
                                                           archivo);
                if (AplicarReglasDeValidacion(rutaRepositorio, archivosProcesar))
                {
                    //AJGG
                    if (bitacoriza)
                        ManejadorArchivos.WriteMessage("Termina AplicarReglasDeValidacion Entra si");


                    if (GeneracionLineasDePagoParaSIR(idProceso, archivosProcesar,esReproceso)
                        && siat.GeneracionLineasDePago(idProceso, archivosProcesar,esReproceso))
                    { MueveArchivoProcesadoExitoso(rutaRepositorio, archivosProcesar);

                        //AJGG
                        if (bitacoriza)
                            ManejadorArchivos.WriteMessage("Termina MueveArchivoProcesadoExitoso");


                    }
                  
                    //ManejadorArchivos.MueveArchivo(); // archivo procesado de manera existosa;
                }
                else
                {
                    MueveArchivoSinProcesarConError(rutaRepositorio,
                                                    archivosProcesar);
                    //ManejadorArchivos.MueveArchivo(); //Mover archivos con error
                }
            }
        }

        /// <summary>
        /// Método que generar los archivos de salida para descargo depagos.
        /// </summary>
        /// <param name="idProceso"> </param>
        public void CrearArchivosSalida(long idProceso)
        {
            var rutaArchivosSIR = ConfigurationManager.AppSettings["RutaArchivosParaSIR"];
            try
            {
                var listArchivos = DalPagos.ObtenerInformacionArchivosDeSalida(idProceso);
                var archivosPorLocal = new Dictionary<byte, List<ArchivoSalida>>();

                if (listArchivos.Count() != 0)
                {
                    ManejadorArchivos.WriteMessage("\n Creando Archivos de salida SIR:"+ rutaArchivosSIR );
                    Console.WriteLine("Creando Archivos de salida SIR:" + rutaArchivosSIR);
                }
                foreach (var archivo in listArchivos)
                {
                    if (archivosPorLocal.ContainsKey((byte)archivo.IdAlr))
                    {
                        var listTemp = archivosPorLocal[(byte)archivo.IdAlr];
                        {
                            listTemp.Add(archivo);
                        }
                    }
                    else
                    {
                        var listInfo = new List<ArchivoSalida> { archivo };
                        archivosPorLocal.Add((byte)archivo.IdAlr, listInfo);
                    }
                }

                var fechaMenor = DateTime.Now;
                var fechaMayor = DateTime.Now;
                Parallel.ForEach(archivosPorLocal, archivoLocal =>
                                                       {
                                                           var nombreArchivo = string.Concat("DL",
                                                                                             archivoLocal.Key.ToString()
                                                                                                 .PadLeft(2, '0'),
                                                               //esto se agrego porque el pad, sólo venía en el log.
                                                                                             fechaMenor.Day.ToString().
                                                                                                 PadLeft(2, '0'),
                                                                                             fechaMenor.Month.ToString()
                                                                                                 .PadLeft(2, '0'),
                                                                                             fechaMayor.Day.ToString().
                                                                                                 PadLeft(2, '0'),
                                                                                             fechaMayor.Month.ToString()
                                                                                                 .PadLeft(2, '0'));
                                                           var mensaje = CrearMensaje(archivoLocal.Value);
                                                           if (File.Exists(Path.Combine(rutaArchivosSIR, nombreArchivo)))
                                                           {
                                                               string[] contenido =
                                                                   File.ReadAllLines(Path.Combine(rutaArchivosSIR,
                                                                                                  nombreArchivo));
                                                               ManejadorArchivos.AppendContenido(ref mensaje, contenido);
                                                               ManejadorArchivos.BorraArchivo(rutaArchivosSIR,
                                                                                              nombreArchivo);
                                                               ManejadorArchivos.EscribeArchivo(mensaje, rutaArchivosSIR,
                                                                                                nombreArchivo);
                                                           }
                                                           else
                                                           {
                                                               ManejadorArchivos.EscribeArchivo(mensaje, rutaArchivosSIR,
                                                                                                nombreArchivo);
                                                           }

                                                           var nombreCifrasControl = string.Concat("HL",
                                                                                                   archivoLocal.Key.
                                                                                                       ToString().
                                                                                                       PadLeft(2, '0'),
                                                               //Se corrige el nombre del archivo para incluir el pad del 0 en las locales con un dígito
                                                                                                   fechaMenor.Day.
                                                                                                       ToString().
                                                                                                       PadLeft(2, '0'),
                                                                                                   fechaMenor.Month.
                                                                                                       ToString().
                                                                                                       PadLeft(2, '0'),
                                                                                                   fechaMayor.Day.
                                                                                                       ToString().
                                                                                                       PadLeft(2, '0'),
                                                                                                   fechaMayor.Month.
                                                                                                       ToString().
                                                                                                       PadLeft(2, '0'));

                                                           var fileInfo =
                                                               new FileInfo(Path.Combine(rutaArchivosSIR, nombreArchivo));
                                                           var sizeOfFile = fileInfo.Length;
                                                           var fechaCreacion = fileInfo.CreationTime;
                                                           var importeArchivoAnterior = 0;
                                                           var cuentaArchivoAnterior = 0;
                                                           var existeArchivoCifras =
                                                               File.Exists(Path.Combine(rutaArchivosSIR,
                                                                                        nombreCifrasControl));
                                                           if (existeArchivoCifras)
                                                           {
                                                               var contenidoArchivo =
                                                                   ManejadorArchivos.LeerArchivo(
                                                                       Path.Combine(rutaArchivosSIR, nombreCifrasControl));
                                                               cuentaArchivoAnterior =
                                                                   Convert.ToInt32(
                                                                       contenidoArchivo.FirstOrDefault().Split('|')[1]);
                                                               importeArchivoAnterior =
                                                                   Convert.ToInt32(
                                                                       contenidoArchivo.FirstOrDefault().Split('|')[5]);
                                                               ManejadorArchivos.BorraArchivo(rutaArchivosSIR,
                                                                                              nombreCifrasControl);
                                                           }
                                                           var sumaImportes = archivoLocal.Value.Aggregate(0,
                                                                                                           (current,
                                                                                                            inforArchivoSIR)
                                                                                                           =>
                                                                                                           (int)
                                                                                                           (current +
                                                                                                            inforArchivoSIR
                                                                                                                .
                                                                                                                ImportePagado));
                                                           if (existeArchivoCifras)
                                                               sumaImportes = sumaImportes + importeArchivoAnterior;
                                                           var cuentaLineas = archivoLocal.Value.Count;
                                                           if (existeArchivoCifras)
                                                               cuentaLineas = cuentaLineas + cuentaArchivoAnterior;

                                                           var msgCifrasControl =
                                                               ManejadorArchivos.CrearMensaje(sizeOfFile.ToString(),
                                                                                              cuentaLineas.ToString(),
                                                                                              archivoLocal.Key.ToString(),
                                                                                              fechaCreacion.ToString(
                                                                                                  "yyyyMMdd"),
                                                                                              fechaCreacion.ToString(
                                                                                                  "HH:mm:ss").Replace(
                                                                                                      ":", "").Trim(),
                                                                                              sumaImportes.ToString());
                                                           ManejadorArchivos.EscribeArchivo(msgCifrasControl,
                                                                                            rutaArchivosSIR,
                                                                                            nombreCifrasControl);
                                                       });
            }
            catch (Exception exception)
            {
                LogEventos.EscribirEntradaLog((int)EnumErroresPagos.ErrorCrearProcArchivoSalida, exception);
                throw;
            }
        }

        private void MueveArchivoSinProcesarConError(string rutaRepositorio, Dictionary<string, object> archivosProcesar)
        {
            try
            {
                var archivosRen = archivosProcesar.ToDictionary(archivo => archivo.Key.Replace("Q", "N"), archivo => archivo.Value);

                GeneraArchivosConError(rutaRepositorio, archivosRen);
            }
            catch (Exception exception)
            {
                LogEventos.EscribirEntradaLog((int)EnumErroresPagos.ErrorMoverArchivoConError, exception);
            }
        }

        private void MueveArchivoProcesadoExitoso(string rutaRepositorio, Dictionary<string, object> archivosProcesar)
        {
            try
            {
                var rutaArchivosDyP = ConfigurationManager.AppSettings["RutaArchivosDYP"];
                var rutaArchivosProc = ConfigurationManager.AppSettings["RutaArchivosProcesados"];
                foreach (KeyValuePair<string, object> archivo in archivosProcesar)
                {
                    if (archivo.Key.StartsWith("Q"))
                    {
                        var rutaOrigen = Path.Combine(rutaRepositorio, archivo.Key);
                        var nombreNuevoArchivo = archivo.Key.Replace('Q', 'K');
                        var mensaje = ManejadorArchivos.CrearMensaje(((string[])archivo.Value).ToList());
                        ManejadorArchivos.EscribeArchivo(mensaje, rutaArchivosDyP, nombreNuevoArchivo);

                        ManejadorArchivos.MueveArchivo(rutaOrigen, rutaArchivosProc, nombreNuevoArchivo);
                        continue;
                    }
                    var rutaOrigenDetalle = Path.Combine(rutaRepositorio, archivo.Key);
                    if (File.Exists(rutaOrigenDetalle))
                        ManejadorArchivos.MueveArchivo(rutaOrigenDetalle, rutaArchivosProc, archivo.Key);
                }
            }
            catch (Exception exception)
            {
                LogEventos.EscribirEntradaLog((int)EnumErroresPagos.ErrorMoverArchivoProcExitoso, exception);
            }
        }

        private bool GeneracionLineasDePagoParaSIR(long idProceso, Dictionary<string, object> archivosProcesar, bool esReproceso)
        {
            var listArchivosGral = new Dictionary<byte, List<InfoArchivoSIR>>();
            var rutaArchivosProc = ConfigurationManager.AppSettings["RutaArchivosProcesados"];
            var listaArchivosTemp = new List<InfoArchivoSIR>();
            try
            {

                //AJGG
                if (bitacoriza)
                    ManejadorArchivos.WriteMessage("Comienza GeneracionLineasDePagoParaSIR");

                if (archivosProcesar != null && archivosProcesar.Count > 0)
                {
                    var cifraControl = archivosProcesar.FirstOrDefault(x => x.Key.StartsWith("Q"));
                    var detalle = archivosProcesar.FirstOrDefault(x => x.Key.StartsWith("E"));
                    Console.WriteLine("Procesando SIR archivo: " + detalle.Key);
                    ManejadorArchivos.WriteMessage("\nProcesando SIR archivo: " + detalle.Key);
                    listaArchivosTemp = ProcesaArchivo(cifraControl, detalle); //FGR pensar en los diferentes layouts por tipo de crédito.                    
                    ActualizaSolicitudes(listaArchivosTemp);
                    EliminaAutoDeterminados(listaArchivosTemp);
                    OrganizaRegistrosDeArchivoPorLocal(ref listArchivosGral, listaArchivosTemp);
                    AlmacenarInformacionPorArchivo(idProceso, listArchivosGral, esReproceso);
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

                    //AJGG
                    if (bitacoriza)
                        ManejadorArchivos.WriteMessage("Error: GeneracionLineasDePagoParaSIR" + exception.Message + "|" + exception.StackTrace);


                }
                return false;
            }

            //AJGG
            if (bitacoriza)
                ManejadorArchivos.WriteMessage("Termina GeneracionLineasDePagoParaSIR: Retresa true");


            return true;
        }

        private void EliminaAutoDeterminados(List<InfoArchivoSIR> listaArchivosTemp)
        {
            var listaRemover = listaArchivosTemp.Where(archivoSIR => archivoSIR.NumeroDeCreditoSir == 0000 || archivoSIR.NumeroDeCreditoSir == 0).ToList();
            foreach (var archivoSIR in listaRemover)
            {
                listaArchivosTemp.Remove(archivoSIR);
            }
        }

        private void AlmacenarInformacionPorArchivo(long idProceso, Dictionary<byte, List<InfoArchivoSIR>> listArchivosGral, bool esReproceso)
        {
            try
            {
                if (esReproceso)
                    DalPagos.EliminaRegistroExistente(idProceso);

                foreach (KeyValuePair<byte, List<InfoArchivoSIR>> archivoLocal in listArchivosGral)
                {
                    foreach (InfoArchivoSIR infoArchivoSIR in archivoLocal.Value)
                    {
                        var mensaje = CrearMensaje(infoArchivoSIR);
                        DalPagos.InsertarDetallePagos(idProceso, infoArchivoSIR,
                                                      mensaje);
                    }
                }

            }
            catch (Exception exception)
            {
                LogEventos.EscribirEntradaLog((int)EnumErroresPagos.ErrorInsertarDetallePagos, exception);
                throw;
            }

        }

        private void AlmacenarInformacionPorArchivoVirtual(long idProceso, Dictionary<byte, List<InfoArchivoSIR>> listArchivosGral, bool esReproceso)
        {
            try
            {
                if (esReproceso)
                    DalPagos.EliminaRegistroExistenteVirtual(idProceso, Constantes.Virtual);

                foreach (KeyValuePair<byte, List<InfoArchivoSIR>> archivoLocal in listArchivosGral)
                {
                    foreach (InfoArchivoSIR infoArchivoSIR in archivoLocal.Value)
                    {
                        var mensaje = CrearMensaje(infoArchivoSIR);
                        DalPagos.InsertarDetallePagos(idProceso, infoArchivoSIR,
                                                      mensaje);
                    }
                }

            }
            catch (Exception exception)
            {
                LogEventos.EscribirEntradaLog((int)EnumErroresPagos.ErrorInsertarDetallePagos, exception);
                throw;
            }

        }

        private void OrganizaRegistrosDeArchivoPorLocal(ref Dictionary<byte, List<InfoArchivoSIR>> listArchivosGral, List<InfoArchivoSIR> listaArchivosTemp)
        {
            try
            {
                foreach (InfoArchivoSIR infoArchivoSIR in listaArchivosTemp)
                {
                    if (infoArchivoSIR.Alr != null &&
                        listArchivosGral.ContainsKey((byte)infoArchivoSIR.Alr))
                    {
                        var listTemp =
                            listArchivosGral[(byte)infoArchivoSIR.Alr];
                        {
                            listTemp.Add(infoArchivoSIR);
                            listArchivosGral[(byte)infoArchivoSIR.Alr] = listTemp;
                        }
                    }
                    else
                    {
                        var listInfo = new List<InfoArchivoSIR>();
                        listInfo.Add(infoArchivoSIR);
                        if (infoArchivoSIR.Alr != null)
                            listArchivosGral.Add((byte)infoArchivoSIR.Alr,
                                     listInfo);
                    }
                }
            }
            catch (Exception exception)
            {
                LogEventos.EscribirEntradaLog((int)EnumErroresPagos.ErrorOrganizarRegistroPorLocal, exception);
                throw;
            }
        }

        private void ActualizaSolicitudes(List<InfoArchivoSIR> listaArchivosTemp)
        {
            try
            {
                DalPagos.ActualizarLineasDeCaptura(listaArchivosTemp);
            }
            catch (Exception exception)
            {
                LogEventos.EscribirEntradaLog((int)EnumErroresPagos.ErrorActualizarLineasCaptura, exception);
                throw;
            }
        }

        private List<InfoArchivoSIR> ProcesaArchivo(KeyValuePair<string, object> cifraControl, KeyValuePair<string, object> detalle)
        {
            var contenidoArchivos = new List<InfoArchivoSIR>();
            try
            {
                var lineasDeCaptura = ObtenerLineasCapturaSIR(detalle);
                if (lineasDeCaptura != null)
                {
                    lineasDeCaptura = ValidarLineasDeCaptura(lineasDeCaptura, detalle, false);
                    var contenido = (string[])detalle.Value;
                    foreach (TblLineaCaptura lineaCaptura in lineasDeCaptura) //Base de datos
                    {
                        var lineaDeCaptura = string.Empty;
                        foreach (string s in contenido.Where(s => !s.StartsWith("1") && s.Trim() != Constantes.EOF).Where(
                            s => s.Split('|')[2].Trim() == lineaCaptura.LineaCaptura))
                        {
                            lineaDeCaptura = s;
                        }

                        var listaDoctosDeterminantes = DalPagos.ObtenerDocumentosDeterminantes(lineaCaptura.IdSolicitud);
                        if (listaDoctosDeterminantes.Count > 0)
                        {
                            foreach (Agrupadores determinante in listaDoctosDeterminantes)
                            {
                                var listCreditos = new Dictionary<int, int>();
                                foreach (var credito in determinante.Conceptos.Where(credito => !listCreditos.ContainsKey(credito.CreditoSir)/* && credito.EsPadre*/))//quitar EsPAdre
                                {
                                    listCreditos.Add(credito.CreditoSir, credito.IdConceptoOriginal);
                                }

                                foreach (var credito in listCreditos)
                                {
                                    var lstConceptos = new List<ConceptoOriginal>();
                                    var lstConceptosTemp = determinante.Conceptos.Where(x => x.CreditoSir == credito.Key /*&& x.EsPadre*/);//quitar EsPadre
                                    foreach (var conceptoOriginal in lstConceptosTemp)
                                    {
                                        foreach (var concepto in determinante.Conceptos)
                                        {
                                            if (concepto.CreditoSir == 0 && concepto.IdConceptoOriginal == conceptoOriginal.IdConceptoOriginal)
                                                lstConceptos.Add(concepto);
                                            else
                                            {
                                                if (credito.Key == conceptoOriginal.CreditoSir && !lstConceptos.Contains(conceptoOriginal))
                                                    lstConceptos.Add(conceptoOriginal);
                                            }
                                        }
                                    }
                                    //var lstConceptos = (from concepto in determinante.Conceptos
                                    //                    where
                                    //                        concepto.CreditoSir == credito.Key ||
                                    //                        (concepto.CreditoSir == 0 &&
                                    //                         concepto.IdConceptoOriginal == credito.Value)
                                    //                    select concepto).ToList();

                                    var infoCreditoSir = new InfoArchivoSIR();
                                    infoCreditoSir.DetalleArchivo = detalle.Key;
                                    infoCreditoSir.Alr = lineaCaptura.IdALR;
                                    infoCreditoSir.IdSolicitud = lineaCaptura.IdSolicitud;
                                    infoCreditoSir.FechaPago = DateTime.ParseExact(lineaDeCaptura.Split('|')[3],
                                                                                   "yyyyMMdd",
                                                                                   CultureInfo.InvariantCulture,
                                                                                   DateTimeStyles.None);
                                    lineaCaptura.FechaPago = infoCreditoSir.FechaPago;
                                    infoCreditoSir.LineaDeCaptura = lineaCaptura.LineaCaptura;
                                    infoCreditoSir.RFC = lineaCaptura.Rfc;
                                    decimal? montoPagar =
                                        lstConceptos.Aggregate<ConceptoOriginal, decimal?>(0,
                                                                                           (current,
                                                                                            concepto) =>
                                                                                           current +
                                                                                           concepto.ImportePagar) ??
                                        0;
                                    infoCreditoSir.ImportePagado =
                                        Convert.ToInt64(Math.Round((decimal)montoPagar, 0));
                                    infoCreditoSir.NumeroDeCreditoSir = credito.Key;
                                    decimal? importeDescuento = lstConceptos.Aggregate
                                                                    <ConceptoOriginal, decimal?>(0,
                                                                                                 (current,
                                                                                                  concepto)
                                                                                                 =>
                                                                                                 current +
                                                                                                 concepto.
                                                                                                     ImporteDescuentos
                                                                    ) ?? 0;
                                    infoCreditoSir.MontoCondonar =
                                        Convert.ToInt64(Math.Round((decimal)importeDescuento, 0));

                                    //se agrega el cálculo de tipo de formulario.
                                    if (lineaCaptura.IdAplicacion == (short)EnumTipoAplicacion.SIR)
                                        infoCreditoSir.TipoDeFormulario = Constantes.PLCE01;
                                    else
                                        infoCreditoSir.TipoDeFormulario = ObtenerTipoFormulario(lstConceptos.First(),
                                                              lineaCaptura);//antes: lstConceptos.First(x => x.EsPadre)
                                    infoCreditoSir.NumeroDeParcialidad = lineaCaptura.NumeroParcialidad;
                                    infoCreditoSir.IdTipoDocumento = lineaCaptura.IdTipoDocumento;
                                    contenidoArchivos.Add(infoCreditoSir);
                                }
                            }
                        }
                    }
                    ContribuyenteCumplido.AddRFC(lineasDeCaptura);
                }
            }
            catch (Exception exception)
            {
                LogEventos.EscribirEntradaLog((int)EnumErroresPagos.ErrorProcesandoArchivos, exception);
                throw;
            }
            return contenidoArchivos;
        }

        private List<TblLineaCaptura> ObtenerLineasCapturaSIR(KeyValuePair<string, object> detalle)
        {
            var lineasAObtener = new ArrayList();
            var lineaTexto = (string[])detalle.Value;
            foreach (var contenido in lineaTexto.Where(contenido => contenido.StartsWith("2") && contenido.Trim() != Constantes.EOF))
            {
                lineasAObtener.Add(contenido.Split('|')[2].Trim());
            }
            try
            {
                if (lineasAObtener.Count > 0)
                {
                    var lineasDeCaptura = lineasAObtener.Cast<string>().Aggregate(string.Empty,
                                                                                  (current, lineaCaptura) =>
                                                                                  current + lineaCaptura + ",");
                    lineasDeCaptura = lineasDeCaptura.Substring(0, lineasDeCaptura.Length - 1);
                    List<TblLineaCaptura> lstLineasCaptura = DalPagos.ObtenerLineasDeCaptura(lineasDeCaptura);
                    return lstLineasCaptura;
                }
                return null;
            }
            catch (Exception exception)
            {
                LogEventos.EscribirEntradaLog((int)EnumErroresPagos.ErrorObtenerInfoBaseDeDatos, exception);
                throw;
            }
        }

        private string ObtenerTipoFormulario(ConceptoOriginal concepto, TblLineaCaptura lineaCaptura)
        {
            //var esMultaCo = EsMultaControlObligaciones(concepto);
            if (lineaCaptura.IdTipoDocumento == Constantes.PagoParcialidades)
            {
                if (concepto.Liquidar)
                    return Constantes.PLCP02;
                if (concepto.Liquidar == false)
                    return Constantes.PLCP03;
                return Constantes.PLCP01;
            }
            if (concepto.Liquidar)
                return Constantes.PLCE02;
            if (concepto.Liquidar == false)
                return Constantes.PLCE03;
            return Constantes.PLCE01; //validar default.
        }

        private bool EsMultaControlObligaciones(ConceptoOriginal concepto)
        {
            var retorno = false;
            if (string.IsNullOrEmpty(concepto.MotivoId))
                return false;
            var listaMotivos = new List<string> { "F", "J" }; // Aqui hay que validar los motivos;
            if (listaMotivos.Any(motivo => concepto.MotivoId == motivo))
            {
                retorno = true;
            }
            return retorno;
        }

        public List<TblLineaCaptura> ValidarLineasDeCaptura(List<TblLineaCaptura> lineasDeCaptura, KeyValuePair<string, object> detalle, bool esCapturaMasiva)
        {
            try
            {
                var listLineasValidas = new List<TblLineaCaptura>();
                var listLineasInvalidas = new List<ReporteDiferencias>();
                var lineasTexto = (string[])detalle.Value;

                foreach (TblLineaCaptura tblLineaCaptura in lineasDeCaptura)
                {
                    TblLineaCaptura captura = tblLineaCaptura;
                    string montoPagado = (from cadenaPago in
                                              lineasTexto.Where(
                                                  cadenaPago =>
                                                  cadenaPago.StartsWith("2") && cadenaPago.Trim() != Constantes.EOF)
                                          let lineaCaptura = cadenaPago.Split('|')[2].Trim()
                                          where lineaCaptura == captura.LineaCaptura
                                          select cadenaPago.Split('|')[5].Trim()).FirstOrDefault();

                    string fechaPago =
                        (from cadenaPago in lineasTexto.Where(
                            cadenaPago => cadenaPago.StartsWith("2") && cadenaPago.Trim() != Constantes.EOF)
                         let lineaCaptura = cadenaPago.Split('|')[2].Trim()
                         where lineaCaptura == captura.LineaCaptura
                         select cadenaPago.Split('|')[3].Trim()).FirstOrDefault();

                    DateTime fechaOperacion = DateTime.ParseExact(fechaPago, "yyyyMMdd", CultureInfo.InvariantCulture,
                                                                  DateTimeStyles.None);

                    if (tblLineaCaptura.MontoLineaCaptura == Convert.ToInt64(montoPagado)||tblLineaCaptura.IdSolicitud==null)
                    {
                        listLineasValidas.Add(tblLineaCaptura);
                    }
                    else 
                    {
                        var reporteDiferencias = new ReporteDiferencias();
                        reporteDiferencias.IdSolicitud = tblLineaCaptura.IdSolicitud;
                        reporteDiferencias.NombreArchivo = detalle.Key;
                        reporteDiferencias.LineaDeCaptura = tblLineaCaptura.LineaCaptura;
                        reporteDiferencias.Rfc = tblLineaCaptura.Rfc;
                        reporteDiferencias.Alr = tblLineaCaptura.IdALR ?? 0;
                        reporteDiferencias.MontoPagarAplicativo = tblLineaCaptura.MontoLineaCaptura;
                        reporteDiferencias.MontoPagadoLinea = Convert.ToDecimal(montoPagado);
                        reporteDiferencias.FechaOperacion = fechaOperacion;
                        listLineasInvalidas.Add(reporteDiferencias);
                    }
                }

                AgregarRegistrosInconsistenciaDePago(listLineasInvalidas);

                return listLineasValidas;
            }
            catch (Exception exception)
            {
                LogEventos.EscribirEntradaLog((int)EnumErroresPagos.ErrorValidandoLineasCaptura, exception);
            }

            return null;
        }

        private void AgregarRegistrosInconsistenciaDePago(List<ReporteDiferencias> listLineasInvalidas)
        {
            try
            {
                DalPagos.AgregarRegistrosIncosistenciasPago(listLineasInvalidas);
            }
            catch (Exception exception)
            {
                LogEventos.EscribirEntradaLog((int)EnumErroresPagos.ErrorGuardarArchivosConError, exception);
            }
        }

        public List<TblLineaCaptura> ObtenerLineasCaptura(KeyValuePair<string, object> detalle)
        {
            var lineasAObtener = new ArrayList();
            var lineaTexto = (string[])detalle.Value;
            List<TblLineaCaptura> lstLineasCapturaFinal = new List<TblLineaCaptura>();
            try
            {
            foreach (var contenido in lineaTexto.Where(contenido => contenido.StartsWith("2") && contenido.Trim() != Constantes.EOF))
            {
                TblLineaCaptura lc = new TblLineaCaptura();
                lc.LineaCaptura = contenido.Split('|')[2].Trim();
                lc.FechaPago = DateTime.ParseExact(contenido.Split('|')[3].Trim(),"yyyyMMdd",CultureInfo.InvariantCulture,DateTimeStyles.None);
                lc.MontoLineaCaptura = long.Parse(contenido.Split('|')[5].Trim());
                lc.IdTipoDocumento = 22;
                lstLineasCapturaFinal.Add(lc);
                lineasAObtener.Add(contenido.Split('|')[2].Trim());
            }
            
                if (lineasAObtener.Count > 0)
                {
                    var lineasDeCaptura = lineasAObtener.Cast<string>().Aggregate(string.Empty,
                                                                                  (current, lineaCaptura) =>
                                                                                  current + lineaCaptura + ",");
                    lineasDeCaptura = lineasDeCaptura.Substring(0, lineasDeCaptura.Length - 1);
                    List<TblLineaCaptura> lstLineasCaptura = DalPagos.ObtenerLineasDeCaptura(lineasDeCaptura);

                    foreach (var lc in lstLineasCaptura)
                    {
                        TblLineaCaptura resultado=lstLineasCapturaFinal.Find(x => x.LineaCaptura == lc.LineaCaptura);
                        lstLineasCapturaFinal.Remove(resultado);
                        lc.FechaPago = resultado.FechaPago;
                        lc.MontoLineaCaptura = resultado.MontoLineaCaptura;
                        lstLineasCapturaFinal.Add(lc);
                    }
                    return lstLineasCapturaFinal;
                }
                return null;
            }
            catch (Exception exception)
            {
                LogEventos.EscribirEntradaLog((int)EnumErroresPagos.ErrorObtenerInfoBaseDeDatos, exception);
                throw;
            }
        }

        private bool AplicarReglasDeValidacion(string rutaRepositorio, Dictionary<string, object> archivosProcesar)
        {
            var archivoQ = archivosProcesar.FirstOrDefault(archivo => archivo.Key.StartsWith("Q"));
            var archivoE = archivosProcesar.FirstOrDefault(archivo => archivo.Key.StartsWith("E"));
            var archivosError = new Dictionary<string, object>();


            if (ValidarArchivoQBancoFecha(archivoQ, ref archivosError))
            {

                if (ValidarArchivoDetalles(archivoQ, archivoE, ref archivosError))
                {
                    return true;
                }
                else
                {

                    //AJGG
                    if (bitacoriza)
                        ManejadorArchivos.WriteMessage("Entra SINO ValidarArchivoDetalles guarda eventlog");


                    LogEventos.EscribirEntradaLog(2010, "Archivo no valido: " + archivoE.Key, System.Diagnostics.EventLogEntryType.Warning, LogEventos.ApplicationName);
                }
            }
            else
            {
                LogEventos.EscribirEntradaLog(2010, "Archivo no valido: " + archivoQ.Key, System.Diagnostics.EventLogEntryType.Warning, LogEventos.ApplicationName);
            }


            //AJGG
            if (bitacoriza)
                ManejadorArchivos.WriteMessage("Comienza GeneraArchivosConError()");

            GeneraArchivosConError(rutaRepositorio, archivosError);
            return false;
        }

        private bool ValidarArchivoDetalles(KeyValuePair<string, object> archivoQ, KeyValuePair<string, object> archivoE, ref Dictionary<string, object> archivosError)
        {
            try
            {
                var newCadena = string.Empty;
                var archivoValido = true;
                var contenidoCifraControl = ((string[])archivoQ.Value)[0].Split('|');
                var contenidoDetalle = (string[])archivoE.Value;
                var numLineasCaptura = contenidoDetalle.Count(s => s.StartsWith("2"));
                //Obtiene el total de registros para validar contra la cifra de control.
                long sumaOperaciones = contenidoDetalle.Where(s => s.StartsWith("2")).Aggregate<string, long>(
                    0, (current, s) => current + Convert.ToInt64(s.Split('|')[5]));
                //Obtiene el importe de las operaciones



                //AJGG
                if (bitacoriza)
                    ManejadorArchivos.WriteMessage("Comienza ValidarArchivoDetalles");



                if (contenidoCifraControl[2].Trim() != numLineasCaptura.ToString())
                {
                    archivoValido = false; //Numero de registros no corresponde.
                    newCadena = ((string[])archivoQ.Value)[0].Insert(
                        ((string[])archivoQ.Value)[0].Length, "014|");


                    //AJGG
                    if (bitacoriza)
                        ManejadorArchivos.WriteMessage("contenidoCifraControl: Numero de registros corresponde: contenidoCifraControl[2]/ numLineasCaptura: " + contenidoCifraControl[2].Trim() +"/" + numLineasCaptura.ToString() + archivoValido);


                }





                if (contenidoCifraControl[3].Trim() != sumaOperaciones.ToString())
                {
                    archivoValido = false; // importe no corresponde.
                    newCadena = newCadena != string.Empty
                                    ? newCadena.Insert(newCadena.Length, "015|")
                                    : ((string[])archivoQ.Value)[0].Insert(
                                        ((string[])archivoQ.Value)[0].Length, "015|");


                    //AJGG
                    if (bitacoriza)
                        ManejadorArchivos.WriteMessage("contenidoCifraControl: importe corresponde: contenidoCifraControl[3]/SumaOperaciones: " + contenidoCifraControl[3].Trim() +"/" +sumaOperaciones.ToString() + archivoValido);



                }

                var fechaArchivoText = archivoE.Key.Substring(6, 6);
                var claveBanco = archivoE.Key.Substring(1, 5);

                var encabezado = contenidoDetalle.FirstOrDefault().Split('|');
                var fechaEncabezado = DateTime.ParseExact(encabezado[3], "yyyyMMdd",
                                                          CultureInfo.InvariantCulture,
                                                          DateTimeStyles.None);
                var fechaArchivo = DateTime.ParseExact(fechaArchivoText, "yyMMdd", CultureInfo.InvariantCulture,
                                                       DateTimeStyles.None);
                if (fechaEncabezado.ToString("yyyyMMdd") != fechaArchivo.ToString("yyyyMMdd"))
                {
                    archivoValido = false; //la fecha de encabezado no coincide con el nombre del archivo.
                    newCadena = newCadena != string.Empty
                                    ? newCadena.Insert(newCadena.Length, "016|")
                                    : ((string[])archivoQ.Value)[0].Insert(
                                        ((string[])archivoQ.Value)[0].Length, "016|");



                    //AJGG
                    if (bitacoriza)
                        ManejadorArchivos.WriteMessage("contenidoCifraControl: fecha de encabezado: " + archivoValido);



                }

                if (claveBanco != encabezado[2].Trim())
                {
                    archivoValido = false; //la clave del banco no coincide con el nombre del archivo.
                    newCadena = newCadena != string.Empty
                                    ? newCadena.Insert(newCadena.Length, "\n" + encabezado[1] + "|017|")
                                    : ((string[])archivoQ.Value)[0].Insert(
                                        ((string[])archivoQ.Value)[0].Length,
                                        "\n" + encabezado[1] + "|017|");


                    //AJGG
                    if (bitacoriza)
                        ManejadorArchivos.WriteMessage("contenidoCifraControl: clave del banco: (claveBanco/encabezado)  "  + claveBanco +"/" + encabezado[2].Trim() + archivoValido);



                }

                var fechaDetalle = DateTime.ParseExact(fechaArchivoText, "yyMMdd", CultureInfo.InvariantCulture,
                                                       DateTimeStyles.None);
                foreach (var fechaOp in from lineaTexto in contenidoDetalle
                                        where lineaTexto.Trim().ToUpper() != Constantes.EOF
                                        where !lineaTexto.StartsWith("1")
                                        select lineaTexto.Split('|')
                                            into infoDetalle
                                            select
                                                DateTime.ParseExact(infoDetalle[3], "yyyyMMdd",
                                                                    CultureInfo.InvariantCulture,
                                                //Aqui se valida que las fechas de op. correspondan con el archivo.
                                                                    DateTimeStyles.None)
                                                into fechaOp
                                                where fechaDetalle.ToString("yyyyMMdd") != fechaOp.ToString("yyyyMMdd")
                                                select fechaOp)
                {
                    archivoValido = false; //fechas de operación no corresponden con el nombre del archivo.
                    foreach (var infoDetalle in contenidoDetalle.Where(linea => linea.Trim().ToUpper() != Constantes.EOF && !linea.StartsWith("1")).Select(linea => linea.Split('|')).Where(infoDetalle => infoDetalle[3] != fechaDetalle.ToString("yyyyMMdd")))
                    {
                        var secuencia = Convert.ToInt32(infoDetalle[1]);
                        newCadena = newCadena != string.Empty ? newCadena.Insert(newCadena.Length, "\n" + secuencia + "|018|") : ((string[])archivoE.Value)[0].Insert(((string[])archivoE.Value)[0].Length, "\n" + secuencia + "|018|");
                    }


                    //AJGG
                    if (bitacoriza)
                        ManejadorArchivos.WriteMessage("contenidoCifraControl: fechas de operación: " + archivoValido);




                }
                if (Boolean.Parse(ConfigurationManager.AppSettings["EvaluarVersiones"]))
                {


                    //AJGG
                    if (bitacoriza)
                        ManejadorArchivos.WriteMessage("Entra EvaluarVersiones");





                    List<String> versionesValidas = ConfigurationManager.AppSettings["VersionesValidas"].ToString().Split(',').ToList();
                    foreach (
                        var arrDetalles in
                            contenidoDetalle.Where(lineaTexto => lineaTexto.StartsWith("2")).Select(
                                lineaTexto => lineaTexto.Split('|')))
                    {
                        if (!versionesValidas.Contains(arrDetalles[8]))
                        {
                            archivoValido = false;
                            //La versión de los criterios de validación no corresponde con la versión vigente.
                            newCadena = newCadena != string.Empty ? newCadena.Insert(newCadena.Length, "019|") : ((string[])archivoE.Value)[0].Insert(((string[])archivoE.Value)[0].Length, "019|");
                        
                        
 
                        //AJGG
                        if (bitacoriza)
                            ManejadorArchivos.WriteMessage("EvaluarVersiones: " + arrDetalles[8].ToString() +": " + archivoValido);                       
                        
                        }






                    }
                }

                var contenido = ((string[])archivoQ.Value);
                if (!archivoValido)
                {
                    contenido[0] = newCadena;


                    //AJGG
                    if (bitacoriza)
                        ManejadorArchivos.WriteMessage("Archivo es NO Valido: " + newCadena);

                }






                if (archivoValido)
                {
                    contenido[0] = ((string[])archivoQ.Value)[0].Insert(((string[])archivoQ.Value)[0].Length, "000|");
                    //archivosError.Add(archivoQ.Key, contenido);





                    //AJGG
                    if (bitacoriza)
                        ManejadorArchivos.WriteMessage("Archivo es Valido: " + contenido[0].ToString()); 




                }
                else
                {
                    archivosError.Add(archivoQ.Key.Replace('Q', 'N'), contenido);

                    //AJGG
                    if (bitacoriza)
                        ManejadorArchivos.WriteMessage("archivosError.Add  " + archivoQ.Key.Replace('Q', 'N'));





                    return false;
                }
            }
            catch (Exception exception)
            {

                //AJGG
                if (bitacoriza)
                    ManejadorArchivos.WriteMessage("Error Controlado: Msg " + exception.Message + "|stack" + exception.StackTrace + "|Source: " + exception.Source);



                LogEventos.EscribirEntradaLog((int)EnumErroresPagos.ErrorAplicarValidacionDetalle, exception);
                return false;
            }


            //AJGG
            if (bitacoriza)
                ManejadorArchivos.WriteMessage("ValidarArchivoDetalles regresa TRUE");

            return true;
        }

        private bool ValidarArchivoQBancoFecha(KeyValuePair<string, object> archivoQ, ref Dictionary<string, object> archivosError)
        {
            ManejadorArchivos.WriteMessage("Validando archivo.." + archivoQ.Key);
            var contenido = (string[])archivoQ.Value;
            try
            {
                var claveBanco = archivoQ.Key.Substring(1, 5); // Se obtiene la clave del banco
                var fechaArchivoText = archivoQ.Key.Substring(6, 6);
                var fechaArchivo = DateTime.ParseExact(fechaArchivoText, "yyMMdd", CultureInfo.InvariantCulture,
                                                       DateTimeStyles.None);


                var newCadena = string.Empty;
                foreach (string cadena in contenido)
                {
                    if (cadena.Trim().ToUpper() == Constantes.EOF) continue;
                    bool validoProcesar = true;


                    if (contenido.Length <= 1)
                    {
                        ManejadorArchivos.WriteMessage("Archivo con contenido invalido " + archivoQ.Key);
                        archivosError.Add(archivoQ.Key.Replace('Q', 'N'), contenido);
                        return false;
                    }
                    var linea = cadena.Split('|');


                    if (!ExisteBanco(linea[0]))
                    {
                        //Esta línea se debe enviar al log, como banco inválido y no se agrega a la lista y luego continue.
                        validoProcesar = false;
                        newCadena = cadena.Insert(cadena.Length, "011|");


                        //AJGG
                        if (bitacoriza)
                            ManejadorArchivos.WriteMessage("No ExisteBanco " + linea[0]);

                    }


                    if (linea[0].Trim() != claveBanco)
                    {
                        validoProcesar = false;
                        newCadena = newCadena != string.Empty
                                        ? newCadena.Insert(newCadena.Length, "012|")
                                        : cadena.Insert(cadena.Length, "012|");
                        //Clave Banco no coincide con el nombre del archivo.
                    }

                    var fechaControlText = linea[1];
                    var fechaControl = DateTime.ParseExact(fechaControlText, "yyyyMMdd",
                                                           CultureInfo.InvariantCulture,
                                                           DateTimeStyles.None);




                    if (fechaArchivo.ToString("yyyyMMdd") != fechaControl.ToString("yyyyMMdd"))
                    {
                        validoProcesar = false;
                        newCadena = newCadena != string.Empty
                                        ? newCadena.Insert(newCadena.Length, "013|")
                                        : cadena.Insert(cadena.Length, "013|");
                        //log la fecha no corresponde con la del archivo y luego continue.



                        //AJGG
                        if (bitacoriza)
                            ManejadorArchivos.WriteMessage("SI " + fechaArchivo.ToString("yyyyMMdd") + "!=" + fechaControl.ToString("yyyyMMdd"));


                    }
                    if (!validoProcesar)
                    {
                        contenido[0] = newCadena;


                        //AJGG
                        if (bitacoriza)
                            ManejadorArchivos.WriteMessage("ValidarArchivoQBancoFecha regresa FALSE: cadena/newCadena" + cadena  + "|" + newCadena);
                    }



                    if (!validoProcesar)
                    {
                        archivosError.Add(archivoQ.Key.Replace('Q', 'N'), contenido);
                        return false;
                    }
                }
            }
            catch (Exception exception)
            {
                LogEventos.EscribirEntradaLog((int)EnumErroresPagos.ErrorAplicandoReglasValidacion, exception);
                archivosError.Add(archivoQ.Key.Replace('Q', 'N'), contenido);
                return false;
            }


            //AJGG
            if (bitacoriza)
                ManejadorArchivos.WriteMessage("ValidarArchivoQBancoFecha regresa TRUE");


            return true;
        }

        private bool ExisteBanco(string claveBanco)
        {
            return ListaBancosValidos.Any(banco => Convert.ToInt32(claveBanco.Trim()) == banco.IdBanco);
        }

        private Dictionary<string, object> ObtenerArchivosProcesar(string rutaRepositorio, string archivo)
        {
            var listaArchivos = new Dictionary<string, object>();
            try
            {
                var detalle = archivo.Replace('Q', 'E');
                var directory = Directory.CreateDirectory(rutaRepositorio);
                var file = directory.GetFiles().FirstOrDefault(x => x.Name == archivo);
                var fileDetalle = directory.GetFiles().FirstOrDefault(x => x.Name == detalle);
                if (file != null)
                {
                    var content = File.ReadAllLines(file.FullName);
                    listaArchivos.Add(file.Name, content);
                }
                if (fileDetalle != null)
                {
                    var contentDetalle = File.ReadAllLines(fileDetalle.FullName);
                    listaArchivos.Add(fileDetalle.Name, contentDetalle);
                }
            }
            catch (Exception exception)
            {
                LogEventos.EscribirEntradaLog((int)EnumErroresPagos.ErrorObtenerArchivos, exception);
            }
            return listaArchivos;
        }

        /// <summary>
        /// Método que genera archivos con error.
        /// </summary>
        /// <param name="rutaArchivoOrigen">Ruta donde fue tomado el repositorio para procesar archivos. </param>
        /// <param name="listaArchivosQ">Lista de archivos con diferencias en las cifras de control.</param>
        private void GeneraArchivosConError(string rutaArchivoOrigen, Dictionary<string, object> listaArchivosQ)
        {
            var rutaArchivo = ConfigurationManager.AppSettings["RutaArchivosConError"];
            //var rutaArchivoOrigen = ConfigurationManager.AppSettings["RutaInputArchivos"];
            var rutaArchivosAcuse = ConfigurationManager.AppSettings["RutaArchivosDYP"];

            if (listaArchivosQ == null) return;
            try
            {
                ManejadorArchivos.WriteMessage("Archivos con error:");
                foreach (KeyValuePair<string, object> archivo in listaArchivosQ)
                {
                    if (archivo.Key.StartsWith("N"))
                    {
                        var mensaje = ManejadorArchivos.CrearMensaje(((string[])archivo.Value).ToList());
                        ManejadorArchivos.EscribeArchivo(mensaje, rutaArchivosAcuse, archivo.Key.Replace("N", "X"));
                        ManejadorArchivos.WriteMessage(archivo.Key); // Escribe en el Log

                        ManejadorArchivos.MueveArchivo(Path.Combine(rutaArchivoOrigen, archivo.Key.Replace("N", "E")), rutaArchivo);
                        ManejadorArchivos.MueveArchivo(Path.Combine(rutaArchivoOrigen, archivo.Key.Replace("N", "Q")), rutaArchivo);
                        ManejadorArchivos.BorraArchivo(rutaArchivoOrigen, archivo.Key.Replace("N", "Q"));
                        // Borra el archivo Q.
                        ManejadorArchivos.BorraArchivo(rutaArchivoOrigen, archivo.Key.Replace("N", "E"));
                        // Borra el archivo E.
                    }
                }
            }
            catch (Exception exception)
            {
                LogEventos.EscribirEntradaLog((int)EnumErroresPagos.ErrorCrearArchivoError, exception);
            }
        }

        private StringBuilder CrearMensaje(IEnumerable<ArchivoSalida> listArchivosSalida)
        {
            var mensaje = new StringBuilder();
            foreach (string item in listArchivosSalida.Select(archivoSIR => archivoSIR.Contenido))
            {
                mensaje.Append(item + "\n");
            }
            return mensaje;
        }

        public bool EjecutarProcesamientoVirtuales(long idProceso, bool esReproceso, ref List<TblLineaCaptura> lineasApoyo)
        {
            var listArchivosGral = new Dictionary<byte, List<InfoArchivoSIR>>();
            var listaArchivosTemp = new List<InfoArchivoSIR>();
            try
            {
                listaArchivosTemp = ObtenerTransaccionesVirtuales(ref lineasApoyo); //FGR pensar en los diferentes layouts por tipo de crédito. 
                if (listaArchivosTemp.Count() != 0)
                {
                    Console.WriteLine("Procesando Transacciones Virtuales SIR");
                    ManejadorArchivos.WriteMessage("\nProcesando Transacciones Virtuales SIR");
                }


                //AJGG
                if (bitacoriza)
                    ManejadorArchivos.WriteMessage("Termina MueveArchivoProcesadoExitoso");




                ActualizaSolicitudes(listaArchivosTemp);


                //AJGG
                if (bitacoriza)
                    ManejadorArchivos.WriteMessage("Termina ActualizaSolicitudes");

                EliminaAutoDeterminados(listaArchivosTemp);


                //AJGG
                if (bitacoriza)
                    ManejadorArchivos.WriteMessage("Termina EliminaAutoDeterminados");

                OrganizaRegistrosDeArchivoPorLocal(ref listArchivosGral, listaArchivosTemp);


                //AJGG
                if (bitacoriza)
                    ManejadorArchivos.WriteMessage("Termina OrganizaRegistrosDeArchivoPorLocal");

                AlmacenarInformacionPorArchivoVirtual(idProceso, listArchivosGral, esReproceso);



                //AJGG
                if (bitacoriza)
                    ManejadorArchivos.WriteMessage("Termina AlmacenarInformacionPorArchivoVirtual");




            }
            catch (Exception exception)
            {
                LogEventos.EscribirEntradaLog((int)EnumErroresPagos.ErrorProcesandoArchivos, exception);
                return false;
            }
            return true;
        }

        private List<InfoArchivoSIR> ObtenerTransaccionesVirtuales(ref List<TblLineaCaptura> lineas)
        {
            var contenidoArchivos = new List<InfoArchivoSIR>();
            try
            {
                var lineasDeCaptura = ObtenerOperacionesVirtuales();
                lineas = lineasDeCaptura;
                if (lineasDeCaptura != null)
                {
                    foreach (TblLineaCaptura lineaCaptura in lineasDeCaptura)
                    {

                        //AJGG
                        if (bitacoriza)
                            ManejadorArchivos.WriteMessage("Comienza procesaLineasCaptura DocumentoDet: SOlicitud" + lineaCaptura.IdSolicitud);




                        var listaDoctosDeterminantes = DalPagos.ObtenerDocumentosDeterminantes(lineaCaptura.IdSolicitud);
                        if (listaDoctosDeterminantes.Count > 0)
                        {
                            foreach (Agrupadores determinante in listaDoctosDeterminantes)
                            {
                                var listCreditos = new Dictionary<int, int>();
                                foreach (var credito in determinante.Conceptos.Where(credito => !listCreditos.ContainsKey(credito.CreditoSir) /*&& credito.EsPadre*/))
                                {
                                    listCreditos.Add(credito.CreditoSir, credito.IdConceptoOriginal);
                                }

                                foreach (var credito in listCreditos)
                                {
                                    var lstConceptos = new List<ConceptoOriginal>();
                                    var lstConceptosTemp = determinante.Conceptos.Where(x => x.CreditoSir == credito.Key/* && x.EsPadre*/);
                                    foreach (var conceptoOriginal in lstConceptosTemp)
                                    {
                                        foreach (var concepto in determinante.Conceptos)
                                        {
                                            if (concepto.CreditoSir == 0 && concepto.IdConceptoOriginal == conceptoOriginal.IdConceptoOriginal)
                                                lstConceptos.Add(concepto);
                                            else
                                            {
                                                if (credito.Key == conceptoOriginal.CreditoSir && !lstConceptos.Contains(conceptoOriginal))
                                                    lstConceptos.Add(conceptoOriginal);
                                            }
                                        }
                                    }

                                    var infoCreditoSir = new InfoArchivoSIR();
                                    infoCreditoSir.DetalleArchivo = Constantes.Virtual;
                                    infoCreditoSir.Alr = lineaCaptura.IdALR;
                                    infoCreditoSir.IdSolicitud = lineaCaptura.IdSolicitud;
                                    infoCreditoSir.FechaPago = lineaCaptura.FechaSolicitud;
                                    infoCreditoSir.LineaDeCaptura = "00000000000000000000";
                                    
                                    lineaCaptura.FechaPago = infoCreditoSir.FechaPago;
                                    lineaCaptura.LineaCaptura = infoCreditoSir.LineaDeCaptura;
                                    
                                    infoCreditoSir.RFC = lineaCaptura.Rfc;
                                    decimal? montoPagar =
                                        lstConceptos.Aggregate<ConceptoOriginal, decimal?>(0,
                                                                                           (current, concepto) =>
                                                                                           current +
                                                                                           concepto.ImportePagar) ?? 0;
                                    infoCreditoSir.ImportePagado = Convert.ToInt64(Math.Round((decimal)montoPagar, 0));
                                    infoCreditoSir.NumeroDeCreditoSir = credito.Key;
                                    decimal? importeDescuento = lstConceptos.Aggregate
                                                                    <ConceptoOriginal, decimal?>(0,
                                                                                                 (current, concepto) =>
                                                                                                 current +
                                                                                                 concepto.
                                                                                                     ImporteDescuentos) ?? 0;
                                    infoCreditoSir.MontoCondonar =
                                        Convert.ToInt64(Math.Round((decimal)importeDescuento, 0));
                                    if (lineaCaptura.IdAplicacion == (short)EnumTipoAplicacion.SIR)
                                        infoCreditoSir.TipoDeFormulario = Constantes.PLCE01;
                                    else
                                        infoCreditoSir.TipoDeFormulario = ObtenerTipoFormulario(lstConceptos.First(),
                                            lineaCaptura);//lstConceptos.First(x => x.EsPadre)
                                    infoCreditoSir.NumeroDeParcialidad = lineaCaptura.NumeroParcialidad;
                                    infoCreditoSir.IdTipoDocumento = lineaCaptura.IdTipoDocumento;
                                    contenidoArchivos.Add(infoCreditoSir);
                                }
                            }
                            ContribuyenteCumplido.AddRFC(lineasDeCaptura);
                        }                 
                    }
                    
                }
            }
            catch (Exception exception)
            {
                LogEventos.EscribirEntradaLog((int)EnumErroresPagos.ErrorProcesarTransaccionesVirtuales, exception);
                throw;
            }
            return contenidoArchivos;
        }

        public List<TblLineaCaptura> ObtenerOperacionesVirtuales()
        {
            try
            {
                List<TblLineaCaptura> lstLineasCaptura = DalPagos.ObtenerLineasDeCapturaVirtuales();
                return lstLineasCaptura;
            }
            catch (Exception exception)
            {
                LogEventos.EscribirEntradaLog((int)EnumErroresPagos.ErrorAlObtenerInforBaseDatos, exception);
                throw;
            }
        }

    }
}
