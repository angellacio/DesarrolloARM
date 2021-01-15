//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Util:Logger:0:21/May/2008[SAT.DyP.Util:1.0:21/May/2008])
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;

using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging.Formatters;
using Microsoft.Practices.EnterpriseLibrary.Logging.Filters;
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;

using SAT.DyP.Util.Configuration;
using SAT.DyP.Util.ExceptionHandling;
using SAT.DyP.Util.Types;

namespace SAT.DyP.Util.Logging
{
    /// <summary>
    /// Clase que permite implementar los servicios de logging para el 
    /// framework de SCADE.Net
    /// </summary>
    public static class Logger
    {
        private static string DEFAULT_CATEGORY = "DyP";
        private static LogWriter _writer;

        /// <summary>
        /// Inicializa los servicios de logging considerando la configuración
        /// de Enterprise Library (Configuration Block)
        /// </summary>
        /// <param name="configurationSource">Datos de configuración</param>
        public static void Initialize()
        {
            try
            {
                //se obtiene la configuración del ConfigurationManager
                IConfigurationSource configurationSource = ConfigurationManager.EnterpriseLibraryConfigurationSource;

                //se obtiene el provider de logging
                LogWriterFactory _factory = new LogWriterFactory(configurationSource);
                _writer = _factory.Create();
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionHandler.HandleException(ex);
                if (rethrow)
                    throw new SAT.DyP.Util.Types.PlatformException(ex.Message, ex);
            }
        }        

        /// <summary>
        /// Permite crear una entrada para logging
        /// </summary>
        /// <param name="Message">Texto del mensaje</param>
        /// <param name="type">Tipo</param>
        /// <param name="category">Categoria</param>
        /// <returns></returns>
        private static LogEntry CreateEntry(string Message, TraceEventType type,string category)
        {
            LogEntry _entry = new LogEntry();
            _entry.Severity = type;
            _entry.Priority = 1;
            _entry.Message = Message;
            _entry.Categories.Add(category);

            return _entry;
        }

        private static void CommonWriter(string message, TraceEventType type, string category)
        {
            if (_writer == null)
                throw new SAT.DyP.Util.Types.PlatformException("Must first initialize logging application block");

            _writer.Write(CreateEntry(message, TraceEventType.Error, category));
        }

        /// <summary>
        /// Permite escribir un mensaje de error al Log
        /// </summary>
        /// <param name="message">texto del mensaje</param>
        public static void WriteError(string message)
        {
            CommonWriter(message, TraceEventType.Error, DEFAULT_CATEGORY);            
        }

        /// <summary>
        /// Permite escribir un mensaje de información al Log
        /// </summary>
        /// <param name="message"></param>
        public static void WriteInformation(string message)
        {
            CommonWriter(message, TraceEventType.Information, DEFAULT_CATEGORY);   
        }
    }

    /// <summary>
    /// Clase que permite definir un servicio de logging (Logging Application Block) sin ocupar
    /// la configuración de Enterprise Library.
    /// </summary>
    public class CustomLogger
    {
        private static LogWriter _writer;
        private const string CONFIG_VALUE = "SAT.DyP.Util.Logging::ENABLE_LOG";

        /// <summary>
        /// Inicializa el servicio de logging, el valor pasado como
        /// parámetro sirve para definir el nombre del archivo.
        /// </summary>
        /// <param name="appName">Nombre de la aplicación</param>
        public static void Initialize(string appName,string location)
        {
            //se revisa la configuración
            ReviewConfiguration();

            if (_enableLogger)
            {
                try
                {
                    string _template = "{timestamp}[{severity}]:{message}";

                    TextFormatter _formatter = new TextFormatter(_template);

                    string _path = GetLocation(location);
                    int _processId = System.Diagnostics.Process.GetCurrentProcess().Id;

                    string _token = string.Format("{0}{1}{2}_{3}", DateTime.Now.Day,
                                                              DateTime.Now.Month,
                                                              DateTime.Now.Year,
                                                              _processId);

                    string _location = string.Format("{0}\\{1}_{2}.log", _path, appName, _token);

                    FlatFileTraceListener _fileListener = null;

                    _fileListener = new FlatFileTraceListener(_location, _formatter);

                    LogSource _source = new LogSource(appName, SourceLevels.All);
                    _source.Listeners.Add(_fileListener);

                    //Se crear un proveedor vació ya que lo pido el constructor
                    LogSource _emptyLogSource = new LogSource("Empty");

                    IDictionary<string, LogSource> _traceSources = new Dictionary<string, LogSource>();
                    _traceSources.Add("Error", _source);
                    _traceSources.Add("Debug", _source);
                    _traceSources.Add("Information", _source);

                    _writer = new LogWriter(new LogFilter[0], _traceSources, _emptyLogSource, "Information");
                }
                catch(Exception ex)
                {
                    EventLogHelper.WriteErrorEntry(string.Format("Error al inicializar el Log '{0}' en la localidad '{1}'", appName,location), 2000);
                    throw new PlatformException(string.Format("Error al inicializar el Log '{0}' en la localidad '{1}'", appName, location), ex);
                }
            }
        }

        private static bool _enableLogger = true;
        private static void ReviewConfiguration()
        {
            string configValue = ConfigurationManager.ApplicationSettings.ReadSetting(CONFIG_VALUE);

            if (!string.IsNullOrEmpty(configValue))
            {
                _enableLogger=Convert.ToBoolean(configValue);
            }
        }

        private static string GetLocation(string location)
        {           
            DirectoryInfo _dir = new DirectoryInfo(location);
            if (!_dir.Exists)
            {
                //se crea el directorio con la ubicación de los logs
                _dir.Create();
            }

            return location;
        }

        private static LogEntry CreateEntry(string Message, TraceEventType type, string category)
        {
            LogEntry _entry = new LogEntry();
            _entry.Severity = type;
            _entry.Priority = 1;
            _entry.Message = Message;
            _entry.Categories.Add(category);

            return _entry;
        }

        /// <summary>
        /// Escribe un mensaje de error en el log
        /// </summary>
        /// <param name="message">mensaje</param>
        public static void WriteError(string message)
        {
            WriteCommon(CreateEntry(message, TraceEventType.Error, "Error"));
        }

        /// <summary>
        /// Escribe un mensaje de información en el log
        /// </summary>
        /// <param name="message">mensaje</param>
        public static void WriteInfo(string message)
        {
            WriteCommon(CreateEntry(message, TraceEventType.Information, "Information"));
        }

        /// <summary>
        /// Escribe un mensaje de debug en el log
        /// </summary>
        /// <param name="message">mensaje</param>
        public static void WriteDebug(string message)
        {
            WriteCommon(CreateEntry(message, TraceEventType.Error, "Debug"));
        }

        private static void WriteCommon(LogEntry entry)
        {
            if(_enableLogger)
               _writer.Write(entry);
        }
    }
}
