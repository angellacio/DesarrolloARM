using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Sat.CreditosFiscales.Comunes.Entidades.CodigosError;
using Sat.CreditosFiscales.Comunes.Entidades.Pagos;
using Sat.CreditosFiscales.Comunes.Herramientas.Archivo;
using Sat.CreditosFiscales.Datos.AccesoDatos.Pagos;
using Sat.CreditosFiscales.Procesamiento.LogicaNegocio.AccesoLogEventos;
using Sat.CreditosFiscales.Procesamiento.Pagos.NegocioPagos;

using System.Configuration;
using System.Diagnostics;

namespace Sat.CreditosFiscales.Procesamiento.Pagos
{
    /// <summary>
    /// Clase que contiene la lógica principal del proceso de descargo de pagos.
    /// </summary>
    public class ProcesoPagos : IDisposable
    {
        private bool _esReproceso;

        private long _idProceso;


        string rutaArchivosSIR = ConfigurationManager.AppSettings["RutaArchivosParaSIR"];

        bool bitacoriza = Convert.ToBoolean(ConfigurationManager.AppSettings["Bitacoriza"]);




        /// <summary>
        /// Método que implementa el dispose de la clase principal del proceso de pagos.
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Inicia proceso de archivos de pagos.
        /// </summary>
        public void EjecutarProcesoPagos()
        {
            ManejadorArchivos.WriteMessage("->-> Inicia Proceso de Pagos.....");

            // POL-8241
            if (InstanciaPrevia())
                return;

            Console.WriteLine("Iniciando Proceso de pagos");

            var listaRepositorios = ObtenerRutasDeArchivosProcesar();
            _idProceso = ObtenerIdProceso(ref _esReproceso);


            if (bitacoriza)
                ManejadorArchivos.WriteMessage("Proceso:" + _idProceso);


            bool fisicos = Boolean.Parse(ConfigurationManager.AppSettings["PagosFisicos"]);

            if (fisicos)
            {
                foreach (var repositorio in listaRepositorios)
                {

   //                 repositorio.RutaRepositorio = @"C:\YK\ProcesoPagos\Generados_20000102";  // AJGG Quitar
                    var listaArchivos = ObtenerArchivosProcesar(repositorio.RutaRepositorio);
                    EjecutarProcesoArchivosPorRepositorio(_idProceso, repositorio.RutaRepositorio, listaArchivos, _esReproceso);
                    ManejadorArchivos.WriteMessage("Termina proceso repositorio " + repositorio.RutaRepositorio);

                }
            }

            bool virtuales = Boolean.Parse(ConfigurationManager.AppSettings["PagosVirtuales"]);

            if (virtuales)
                InlcuirTransaccionesVirtuales(_idProceso, _esReproceso);



            bool GenZip = Boolean.Parse(ConfigurationManager.AppSettings["GeneraZip"]);

            if (GenZip)
            {
                if (GenerarArchivos(_idProceso))
                    ActualizaFechaFin(_idProceso);
            }



            bool rfcCumplidos = Boolean.Parse(ConfigurationManager.AppSettings["GeneraRFCCumplido"]);

            if (rfcCumplidos)
                ContribuyenteCumplido.CreaArchivoRFCs();


            ManejadorArchivos.WriteMessage("Termina Proceso de Pagos->-> ");
            Console.WriteLine("Termina Proceso de pagos");


        }

        private void InlcuirTransaccionesVirtuales(long idProceso, bool esReproceso)
        {
            try
            {
                var procesamientoArchivos = new ProcesamientoArchivos();
                var siat = new SIAT();
                var lineasApoyo = new List<TblLineaCaptura>();

                if (bitacoriza)
                    ManejadorArchivos.WriteMessage("Entro al proceso InlcuirTransaccionesVirtuales:EjecutarProcesamientoVirtuales");


                procesamientoArchivos.EjecutarProcesamientoVirtuales(idProceso, esReproceso, ref lineasApoyo);


                if (bitacoriza)
                    ManejadorArchivos.WriteMessage("Concluye proceso InlcuirTransaccionesVirtuales:EjecutarProcesamientoVirtuales");


                long idArchi =  siat.RegistraHeadVirutal(idProceso, lineasApoyo.Count());
 //               siat.RegistraDetalleVirtual(idProceso, idArchi, lineasApoyo);





                siat.GeneracionLineasDePagoVirtuales(idProceso, esReproceso, lineasApoyo, idArchi);

                if (bitacoriza)
                    ManejadorArchivos.WriteMessage("Concluye InlcuirTransaccionesVirtuales:GeneracionLineasDePagoVirtuales");





            }
            catch (Exception exception)
            {
                LogEventos.EscribirEntradaLog((int)EnumErroresPagos.ErrorProcesarTransaccionesVirtuales, exception);
            }
        }

        private void ActualizaFechaFin(long idProceso)
        {
            try
            {

                if (bitacoriza)
                    ManejadorArchivos.WriteMessage("ActualizaFechaFin" + idProceso);

                DalPagos.ActualizaFechaFin(idProceso);
            }
            catch (Exception exception)
            {
                LogEventos.EscribirEntradaLog((int)EnumErroresPagos.ErrorActualizarFechaFin, exception);
            }
        }

        public bool GenerarArchivos(long idProceso)
        {
            try
            {

                if (bitacoriza)
                    ManejadorArchivos.WriteMessage("Inicia GenerarArchivos: " + idProceso);


                var procesamientoArchivos = new ProcesamientoArchivos();
                var siat = new SIAT();
                //procesamientoArchivos.CrearArchivosSalida(idProceso);  //AJGG 8241 Se quita Generación de archivos SIR


                if (bitacoriza)
                    ManejadorArchivos.WriteMessage("Comienza CrearArchivosSalida: " + idProceso);


                int ArchivosxZip = int.Parse(ConfigurationManager.AppSettings["XMLxZIP"]);

                bool regresa = true;
                int pagina = 1;
        //        int tomar = 0;
                while (regresa)
                {
       //             tomar = pagina * ArchivosxZip;
                    regresa = siat.CrearArchivosSalida(idProceso, pagina, ArchivosxZip);
                    pagina++;
                }


                if (bitacoriza)
                    ManejadorArchivos.WriteMessage("Termina CrearArchivosSalida: " + idProceso);

            }
            catch (Exception exception)
            {
                LogEventos.EscribirEntradaLog((int)EnumErroresPagos.ErrorCrearArchivoSalida, exception);
                return false;
            }
            return true;
        }

        private long ObtenerIdProceso(ref bool esReproceso)
        {
            try
            {
                return DalPagos.ObtenerIdProcesoPagos(ref esReproceso);
            }
            catch (Exception exception)
            {
                LogEventos.EscribirEntradaLog((int)EnumErroresPagos.ErrorObtenerIdProceso, exception);
            }

            return 0;
        }

        public void EjecutarProcesoArchivosPorRepositorio(long idproceso, string rutaRepositorio, List<string> listaArchivos, bool esReproceso)
        {
            try
            {
                var procesamientoArchivos = new ProcesamientoArchivos();
                procesamientoArchivos.EjecutarProcesamientoArchivos(idproceso, rutaRepositorio, listaArchivos, esReproceso);
            }
            catch (Exception exception)
            {
                LogEventos.EscribirEntradaLog((int)EnumErroresPagos.ErrorProcesandoArchivos, exception);
                SIATDALPagos.ActualizaErrorProceso(idproceso, -1, 98);
            }
        }

        private List<string> ObtenerArchivosProcesar(string rutaRepositorio)
        {
            var listaArchivos = new List<string>();
            try
            {
                ManejadorArchivos.WriteMessage(string.Format("\n ->-> Inicia obtener información del repositorio {0}", rutaRepositorio));
                var directory = Directory.CreateDirectory(rutaRepositorio);
                listaArchivos.AddRange(from file in directory.GetFiles().Where(x => x.Name.StartsWith("Q")) where !file.Name.ToUpper().EndsWith("PART") select file.Name);
            }
            catch (Exception exception)
            {
                LogEventos.EscribirEntradaLog((int)EnumErroresPagos.ErrorObtenerArchivos, exception);
                return null;
            }
            return listaArchivos;
        }

        private IEnumerable<RutaArchivos> ObtenerRutasDeArchivosProcesar()
        {
            try
            {
                return DalPagos.ObtenerRepositorioPagos();
            }
            catch (Exception exception)
            {
                LogEventos.EscribirEntradaLog((int)EnumErroresPagos.ErrorAlObtenerInforBaseDatos, exception);
            }

            return new List<RutaArchivos>();
        }



        #region POL-8241

        private static bool InstanciaPrevia()
        {

            Process[] processes = Process.GetProcessesByName("Sat.CreditosFiscales.Procesamiento.Pagos");


            if (processes.Length > 1)
            {
                ManejadorArchivos.WriteMessage("Termina Proceso - Instancia previa ejecutandose");
                return true;
            }
            else
            {
                return false;
            }
        }


        #endregion


    }
}
