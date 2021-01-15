using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration.Install;
using System.Diagnostics;
using System.ComponentModel;

namespace DyP.Setup.CreateEventLogSource
{
    [RunInstaller(true)]
    public class EventLogSourceInstaller : Installer
    {
        private const string INSTALL_PARAM_EVENTSOURCE_NAME = "EventLogSourceName";

        public string ExpectedParameterName
        {
            get { return INSTALL_PARAM_EVENTSOURCE_NAME; }
        }

        private string _logSourceName;

        public string LogSourceName
        {
            get { return _logSourceName; }
            set { _logSourceName = value; }
        }

        public override void Install(System.Collections.IDictionary stateSaver)
        {
            base.Install(stateSaver);
            this.CreateEventLogSource();
        }

        public override void Uninstall(System.Collections.IDictionary savedState)
        {
            try
            {
                this.DeleteEventLogSource();
            }
            catch
            {
                // no lanzamos excepción durante el uninstall para evitar que el MSI que nos invocó 
                // quede atorado en el sistema sin poder desinstalarse.
            }

            base.Uninstall(savedState);
        }

        public override void Rollback(System.Collections.IDictionary savedState)
        {
            try
            {
                this.DeleteEventLogSource();
            }
            catch 
            {
                // mejor no lanzamos excepciones durante el rollback.
            }

            base.Rollback(savedState);
        }

        private void CreateEventLogSource()
        {
            ValidateInstallContext();

            this.Context.LogMessage(String.Format("Eliminando el EventSource '{0}'...", this.LogSourceName));

            if (!EventLog.SourceExists(this.LogSourceName))
            {
                // si esta línea lanza una excepción, el instalador hará rollback.
                EventLog.CreateEventSource(this.LogSourceName, @"Application");
                this.Context.LogMessage(String.Format("EventLog source '{0}' fue creado.", this.LogSourceName));
            }
            else
            {
                this.Context.LogMessage(String.Format("No se eliminó el EventSource '{0}' por que no existe.", this.LogSourceName));
            }
        }

        private void DeleteEventLogSource()
        {
            ValidateInstallContext();

            this.Context.LogMessage(String.Format("Eliminando EventLog source '{0}'...", this.LogSourceName));

            if (EventLog.SourceExists(this.LogSourceName))
            {
                EventLog.DeleteEventSource(this.LogSourceName);
                this.Context.LogMessage(String.Format("EventLog source '{0}' fué eliminado.", this.LogSourceName));
            }
            else
            {
                this.Context.LogMessage(String.Format("No se creó el EventSource '{0}' por que ya existe.", this.LogSourceName));
            }
        }

        private void  ValidateInstallContext()
        {
            if (this.Context == null)
            {
                string errorMessage = String.Format("El InstallContext es nulo. Se debe invocar este installer desde un contexto de instalación.");
                throw new InstallException(errorMessage);
            }

            if (!this.Context.Parameters.ContainsKey(INSTALL_PARAM_EVENTSOURCE_NAME))
            {
                string errorMessage = String.Format("Se esperaba el parámetro '{0}' en el InstallContext.", INSTALL_PARAM_EVENTSOURCE_NAME);
                throw new InstallException(errorMessage);
            }

            string logSource = this.Context.Parameters[INSTALL_PARAM_EVENTSOURCE_NAME];

            if (String.IsNullOrEmpty(logSource) || logSource.Trim().Length == 0)
            {
                string errorMessage = String.Format("El valor '{0}' del parámetro '{1}' no es un nombre válido para un event source.", logSource, INSTALL_PARAM_EVENTSOURCE_NAME);
                throw new InstallException(errorMessage);
            }

            this._logSourceName = logSource.Trim();
        }
    }
}
