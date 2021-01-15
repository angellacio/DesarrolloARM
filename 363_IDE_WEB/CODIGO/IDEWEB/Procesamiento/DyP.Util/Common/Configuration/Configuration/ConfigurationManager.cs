//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Util:ConfigurationManager:0:21/May/2008[SAT.DyP.Util:1.0:21/May/2008])
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using System.Windows.Shapes;
using System.IO;
using System.Diagnostics;
using SAT.DyP.Util.Types;

namespace SAT.DyP.Util.Configuration
{
  /// <summary>
  /// Clase que ofrece acceso a la configuración de la aplicación y a la 
  /// configuración de los bloques del enterprise library.
  /// </summary>
  public static class ConfigurationManager
  {
 

    // Esta variable estática está diseñada para que el cliente pueda inyectar cualquier
    // otra clase que implemente IApplicationSettingsProvider en tiempo de ejecución
    // usando el método RegisterConfigurationProvider.
    private static IApplicationSettingsProvider _provider = new SqlServerApplicationSettingsProvider();

    private static IConfigurationSource _entLibConfigurationSource;
    private static object _entLibConfigSyncRoot = new object();

    public static IApplicationSettingsProvider ApplicationSettings
    {
      get { return ConfigurationManager._provider; }
    }

    public static void RegisterConfigurationProvider(IApplicationSettingsProvider provider)
    {
      _provider = provider;
    }

    public static IConfigurationSource EnterpriseLibraryConfigurationSource
    {
      get
      {
        if (_entLibConfigurationSource == null)
        {
          lock (_entLibConfigSyncRoot)
          {
            if (_entLibConfigurationSource == null)
            {
              _entLibConfigurationSource = CreateEntLibConfigurationSource();
            }
          }
        }
        return _entLibConfigurationSource;
      }
      set { _entLibConfigurationSource = value; }
    }

    private static IConfigurationSource CreateEntLibConfigurationSource()
    {
      IConfigurationSource configurationSource;

      String installPath = RegistryHelper.ReadStringValueFromProductRegistryKey("InstallPath");
      string configurationFolder = System.IO.Path.Combine(installPath, "Configuration");
      string entlibConfigFile = System.IO.Path.Combine(configurationFolder, @"SAT.DyP.Util.Configuration.EnterpriseLibrary.config");

      if (!File.Exists(entlibConfigFile))
      {
        string errorMessage = String.Format(
            "Error al inicializar configuración de Enterprise Library: no existe el archivo '{0}'.",
            entlibConfigFile);

        throw new PlatformException(errorMessage);
      }

      configurationSource = new FileConfigurationSource(entlibConfigFile);

      return configurationSource;
    }
  }
}