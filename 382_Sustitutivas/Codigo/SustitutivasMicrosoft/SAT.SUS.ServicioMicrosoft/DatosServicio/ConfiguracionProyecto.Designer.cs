namespace SAT.SUS.ServicioMicrosoft.DatosServicio
{
    partial class ConfiguracionProyecto
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.ServiceProcess.ServiceProcessInstaller spSerProInstaller;
        private System.ServiceProcess.ServiceInstaller spSerInstaller;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.spSerProInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.spSerInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // spSerProInstaller
            // 
            this.spSerProInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.spSerProInstaller.Password = null;
            this.spSerProInstaller.Username = null;
            // 
            // spSerInstaller
            // 
            this.spSerInstaller.DisplayName = "SAT Sustitutivas Microsoft";
            this.spSerInstaller.ServiceName = "SAT.Sustitutivas.Microsoft";
            this.spSerInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ConfiguracionProyecto
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.spSerProInstaller,
            this.spSerInstaller});

        }

        #endregion

        
    }
}