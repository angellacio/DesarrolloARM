using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Sat.CreditosFiscales.Comunes.Entidades.CodigosError;
using Sat.CreditosFiscales.Procesamiento.LogicaNegocio.AccesoLogEventos;
using Sat.CreditosFiscales.Procesamiento.Pagos.Servicios;

namespace Sat.CreditosFiscales.Procesamiento.Pagos
{
    /// <summary>
    /// Punto de entrada de la aplicación del proceso de descargo de pagos para SIR.
    /// </summary>
    static class Program
    {
        private static readonly ConsolaCreditosFiscalesPagos _appManager = new ConsolaCreditosFiscalesPagos();

        /// <summary>
        /// Método principal del proceso de pagos.
        /// </summary>
        /// <param name="args"></param>
        [STAThread]
        static void Main(string[] args)
        {
            Application.ThreadException +=  Application_Threadexception;
            try
            {
                _appManager.LoadApplication();
                _appManager.EjecutarProcesoPagos();
                _appManager.CloseAplication();
            }
            catch(Exception exception)
            {
                if(ApplicationManager.LoadingApplication)
                {
                    LogEventos.EscribirEntradaLog((int)EnumErroresPagos.ErrorAlIniciarAplicacion,exception);
                }
            }
        }

        private static void Application_Threadexception(object sender, ThreadExceptionEventArgs e)
        {
            if(e.Exception.Source == "System.Drawing")
            {
                LogEventos.EscribirEntradaLog((int) EnumErroresPagos.ErrorAlIniciarAplicacion, e.Exception);
            }
        }
    }
}
