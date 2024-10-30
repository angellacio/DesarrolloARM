using System;
using System.Security;
using System.Windows.Forms;
using Sat.CreditosFiscales.Comunes.Entidades.CodigosError;
using Sat.CreditosFiscales.Procesamiento.LogicaNegocio.AccesoLogEventos;

namespace Sat.CreditosFiscales.Procesamiento.Pagos.Servicios
{
    /// <summary>
    /// Lllamada asíncrona.
    /// </summary>
    /// <param name="method">Método que realiza el invoke.</param>
    /// <param name="args">Lista de argumentos.</param>
    /// <returns>Delegado a llamar.</returns>
    public delegate object InvokerDelegate(Delegate method, params object[] args);

    /// <summary>
    /// Clase que sirve como orquestador de la aplicación de pagos.
    /// </summary>
    public abstract class ApplicationManager
    {
        #region Miembros privados
        private static ApplicationManager _instance;

        private static bool _loadingApplication;

        private static bool _shutDownStarted;

        private static bool _closingApplication;

        private static Guid _aplicationInstanceId = Guid.NewGuid();

        private ProcesoPagos _mainProgram;

        #endregion

        #region Propiedades
        /// <summary>
        /// Propiedad que expone si se cierra la aplicación o no.
        /// </summary>
        public static bool ClosingApplication
        {
            get { return _closingApplication; }
        }

        /// <summary>
        /// Propiedad que obtiene la fecha del sistema en la capa de procesamiento.
        /// </summary>
        public DateTime SystemDate { get; set; }

        /// <summary>
        /// El método estático.
        /// </summary>
        /// <value>The synchronization method.</value>
        public static InvokerDelegate SynchronizationMethod
        {
            get { return _instance.SynchMethod; }
        }

        /// <summary>
        /// Obtiene o Asinga el GUI que se creo en ApplicationManagerService o ApplicationManagerCliente.
        /// </summary>
        /// <value>El formato de GUI.</value>
        public static Guid ApplicationInstanceId
        {
            get { return _aplicationInstanceId; }

            //get { return Guid.Empty; }
            set { _aplicationInstanceId = value; }
        }

        /// <summary>
        /// Gets or sets the synch method.
        /// </summary>
        /// <value>The synch method.</value>
        public InvokerDelegate SynchMethod { get; set; }

        /// <summary>
        /// Nombre de la aplicación
        /// </summary>
        public abstract string ApplicationName { get; }

        /// <summary>
        /// ID de la aplicación
        /// </summary>
        public abstract string ApplicationID { get; }

        /// <summary>
        /// Propiedad que expone el programa principal.
        /// </summary>
        protected ProcesoPagos MainProgram
        {
            get { return _mainProgram; }
            set { _mainProgram = value; }
        }

        /// <summary>
        /// Publica la instancia actual de ApplicationManager
        /// </summary>
        public static ApplicationManager Instance
        {
            get { return _instance; }
            private set { _instance = value; }
        }

        /// <summary>
        /// Propiedad que se utiliza para saber si se esta cargando la aplicación.
        /// </summary>
        public static bool LoadingApplication
        {
            get { return _loadingApplication; }
        }

        #endregion

        #region Métodos
        protected void InitializeFramwork()
        {
            Application.ApplicationExit += CloseApplication;
        }

        /// <summary>
        /// Inicia la carga de la aplicación.
        /// </summary>
        public void LoadApplication()
        {
            Instance = this;
            _loadingApplication = true;

            try
            {
                InitializeFramwork();
                //TODO: Inicializar culaquier otro subsistema.
                _mainProgram = InitializeProgram();

            }
            catch (AbortLoadApplicationException exception)
            {
                LogEventos.EscribirEntradaLog((int) EnumErroresPagos.ErrorAlIniciarAplicacion,exception);
                return;
            }
            _loadingApplication = false;

        }

        /// <summary>
        /// Realiza la aplicación del proceso de pagos.
        /// </summary>
        public void EjecutarProcesoPagos()
        {
            try
            {
               // Sat.CreditosFiscales.Procesamiento.Pagos.NegocioPagos.SIAT pru = new NegocioPagos.SIAT();

                //pru.CrearArchivosSalida(469);
                _mainProgram.EjecutarProcesoPagos();
            }
            catch (Exception exception)
            {
                LogEventos.EscribirEntradaLog((int) EnumErroresPagos.ErrorEjecucionPagos,exception);                
            }
        }

        /// <summary>
        /// Finaliza la instancia de la aplicación.
        /// </summary>
        public void CloseAplication()
        {
            ExitApplication();
        }

        /// <summary>
        /// Método para terminar la aplicación.
        /// </summary>
        public static void ExitApplication()
        {
            _closingApplication = true;
            ApplicationManager.ExecuteLogOff();
            // Aqui se puede agregar funcionalidad para salver configuraciones de usuario
            // si lo llegan a pedir.
            ShutDown();
        }

        /// <summary>
        /// Método que manda a llamar el cerrado de la aplicación.
        /// </summary>
        /// <param name="sender">Elemento que envía el cerrado de la aplicación.</param>
        /// <param name="e">Argumentos de la función.</param>
        private void CloseApplication(object sender, EventArgs e)
        {
            //TODO:Logear la salida del sistema
            OnCloseApplication();
        }

        /// <summary>
        /// Método que se dispara cuando se cierra la aplicación.
        /// </summary>
        protected virtual void OnCloseApplication()
        {
            if (!_shutDownStarted)
            {
                //ApplicationManager.ExecuteLogOff();
                ApplicationManager.ShutDown();
            }
        }

        /// <summary>
        /// Método para terminar la aplicación.
        /// </summary>
        private static void ShutDown()
        {
            _shutDownStarted = true;
            var failDuringShutdown = false;
            try
            {
                CloseApplication();
            }

            catch (Exception ex)
            {
                //TODO:Logear la exception
                failDuringShutdown = true;

            }

            try
            {
                //Environment.Exit(0);
                Application.Exit();
            }
            catch (Exception ex)
            {
                failDuringShutdown = true;
            }

            try
            {
                if (failDuringShutdown)
                    Environment.FailFast(null);
                else
                {
                    Environment.Exit(0);
                }
            }
            catch (Exception exception)
            {
                LogEventos.EscribirEntradaLog((int) EnumErroresPagos.ErrorAlCerrarAplicacion,exception);
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Método que carga la ejecución de la aplicación.
        /// </summary>
        /// <returns>Objeto para inicio de procesamiento de pagos.</returns>
        protected abstract ProcesoPagos InitializeProgram();

        /// <summary>
        /// Método que cierra la ventana principal.
        /// </summary>
        private static void CloseApplication()
        {
            try
            {
                if (_instance != null && _instance.MainProgram != null)
                {
                    _instance.MainProgram.Dispose();
                }
            }
            catch (Exception exception)
            {
                LogEventos.EscribirEntradaLog((int) EnumErroresPagos.ErrorAlCerrarAplicacion,exception);                
            }
        }

        /// <summary>
        /// Método que que ejecutará el logoff.
        /// </summary>
        private static void ExecuteLogOff()
        {
            try
            {
                //TODO:Deslogearse
            }
            catch (SecurityException security)
            {
                //TODO:Logear la exception de seguridad
            }
            catch (Exception)
            {
                //TODO:Logear la exception principal.
            }
        }
        #endregion
    }
}
