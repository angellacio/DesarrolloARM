using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.IO;
using System.Linq;
using System.Reflection;


namespace Sat.Scade.Net.IDE.Presentacion.Web.Instalador
{
    [RunInstaller(true)]
    public partial class ConfiguracionAmbiente : System.Configuration.Install.Installer
    {
        public ConfiguracionAmbiente()
        {
            InitializeComponent();
        }

        public override void Install(System.Collections.IDictionary stateSaver)
        {
            base.Install(stateSaver);

            TextReader reader = null;
            TextWriter writer = null;

            try
            {
                reader = this.ArchivoConfiguracionAmbiente;
                writer = this.ArchivoConfiguracionAplicacion;
                writer.Write(reader.ReadToEnd());
            }
            catch (Exception excepcion)
            {
                Utilerias.RegistrarLogEventos(excepcion);
                throw;
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                    writer = null;
                }

                if (reader != null)
                {
                    reader.Close();
                    reader = null;
                }
            }
        }

        public string Medio
        {
            get
            {
                string medio = Context.Parameters["Medio"];

                if (String.IsNullOrEmpty(medio))
                {
                    throw new InstallException("No sé encontró el parámetro 'Medio'.");
                }

                return medio;
            }
        }

        public string Ambiente
        {
            get
            {
                string ambiente = Context.Parameters["Ambiente"];

                if (String.IsNullOrEmpty(ambiente))
                {
                    throw new InstallException("No sé encontró el parámetro 'Ambiente'.");
                }

                return ambiente;
            }
        }

        public StreamWriter ArchivoConfiguracionAplicacion
        {
            get
            {
                string configPath = Path.Combine(Path.GetDirectoryName(Context.Parameters["assemblypath"]), "Web.config").Replace(@"bin\", "");

                if (!File.Exists(configPath))
                {
                    throw new InstallException("No se encontró el archivo de configuración de la aplicación Web.");
                }

                return new StreamWriter(configPath);
            }
        }

        public StreamReader ArchivoConfiguracionAmbiente
        {
            get
            {
                string recurso = String.Format(@"Sat.Scade.Net.IDE.Presentacion.Web.Configuracion.{0}.{1}.config", Ambiente, Medio);
                Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(recurso);

                if (stream == null)
                {
                    throw new InstallException(String.Format("No se encontró el recurso '{0}'.", recurso));
                }

                return new StreamReader(stream);
            }
        }  
    }
}
