//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Util:SqlServerApplicationSettingsProvider:0:21/May/2008[SAT.DyP.Util:1.0:21/May/2008])
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Diagnostics;
using SAT.DyP.Util.Caching;
using SAT.DyP.Util.Types;

namespace SAT.DyP.Util.Configuration
{
    /// <summary>
    /// Proveedor de valores de configuración que tiene una base de datos de SQLServer como repositorio.
    /// </summary>
    public class SqlServerApplicationSettingsProvider : IApplicationSettingsProvider
    {
        private static string _configDatabase = null;

        public string ReadSetting(string settingName, string defaultValue)
        {
            Trace.Assert(settingName != null);
            Trace.Assert(defaultValue != null);

            string resultValue;

            // primero, intentar leer de cache:
            if (CachingService.Contains(settingName))
            {
                resultValue = (string)CachingService.GetItem(settingName);
            }
            else
            {
                // si no está en cache, leer de repositorio
                resultValue = ReadOrCreateSetting(settingName, defaultValue);
                CachingService.AddItem(settingName, resultValue);
            }

            return resultValue;
        }


        public string ReadSetting(string settingName)
        {
            Trace.Assert(settingName != null);

            string resultValue;

            if (CachingService.Contains(settingName))
            {
                resultValue = (string)CachingService.GetItem(settingName);
            }
            else
            {
                // si no está en cache, leer de repositorio
                resultValue = ReadOrCreateSetting(settingName, null);
                CachingService.AddItem(settingName, resultValue);
            }

            return resultValue;
        }

        /*
         * Fecha de Modificacion : 27 Julio 2012
         * 
         */
        public string ReadSettingCfg(string settingName, string tpDeclaracion)
        {
            Trace.Assert(settingName != null);
            Trace.Assert(tpDeclaracion != null);

            string resultValue;

            if (CachingService.Contains(settingName))
            {
                resultValue = (string)CachingService.GetItem((settingName + ":" + tpDeclaracion));
            }
            else
            {
                // si no está en cache, leer de repositorio
                resultValue = ReadOrCreateSetting(settingName, null, tpDeclaracion);
                CachingService.AddItem((settingName + ":" + tpDeclaracion), resultValue);
            }

            return resultValue;
        }

        public string ReadEncryptSetting(string settingName)
        {
            string resultValue = ReadSetting(settingName);

            if (!string.IsNullOrEmpty(resultValue))
            {

            }

            return resultValue;
        }

        public void SaveSetting(string settingName, string settingValue)
        {
            Trace.Assert(settingName != null);
            Trace.Assert(settingValue != null);

            UpdateOrCreateSetting(settingName, settingValue);

            // agregar a cache
            CachingService.AddItem(settingName, settingValue);
        }

        #region Private Implementation

        private string ReadOrCreateSetting(string settingName, string defaultValue)
        {
            // TODO: Implementar caching?

            string settingValue = null;

            SqlConnection connection = new SqlConnection(LoadConectionString());
            SqlCommand command = new SqlCommand("ApplicationSettings_GetSetting", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@SettingName", settingName));
            command.Parameters.Add(new SqlParameter("@DefaultValue", defaultValue));

            object scalarResult = null;

            try
            {
                connection.Open();

                // Como el campo SettingValue de la tabla ApplicationSettings no acepta nulos, 
                // el valor escalar leido será null sólo cuando no existe el registro en la tabla con el 
                // valor de SettingName especificado y defaultValue es null.
                scalarResult = command.ExecuteScalar();
            }
            catch (SqlException sqlEx)
            {
                string errorMessage = String.Format("La tabla de valores de configuración de la base de datos '{0}' en la instancia de SQLServer '{1}' no estuvo disponible al intentar escribir el valor de la variable de configuración '{2}'.", connection.Database, connection.DataSource, settingName);
                throw new PlatformException(errorMessage, sqlEx);
            }
            catch (Exception ex)
            {

                throw new PlatformException(ex);
            }
            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                }
            }

            if (scalarResult == null)
            {
                string errorMessage = String.Format("No existe el parámetro de configuración '{0}' en la base de datos '{1}' en la instancia de SQL Server '{2}'.", settingName, connection.Database, connection.DataSource);
                throw new PlatformException(errorMessage);
            }
            else
            {
                settingValue = Convert.ToString(scalarResult);
            }

            return settingValue;
        }


        /*
         * Fecha de Modificacion : 27 Julio 2012
         * 
         */
        private string ReadOrCreateSetting(string settingName, string defaultValue, string tipoDeclaracion)
        {
            // TODO: Implementar caching?

            string settingValue = null;
            int tpDeclaracion = Int32.Parse(tipoDeclaracion);

            SqlConnection connection = new SqlConnection(LoadConectionString());
            SqlCommand command = new SqlCommand("ApplicationSettingsForms_GetSetting", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@SettingName", settingName));
            command.Parameters.Add(new SqlParameter("@DefaultValue", defaultValue));
            command.Parameters.Add(new SqlParameter("@TpDeclaracion", tpDeclaracion));

            object scalarResult = null;

            try
            {
                connection.Open();

                // Como el campo SettingValue de la tabla ApplicationSettings no acepta nulos, 
                // el valor escalar leido será null sólo cuando no existe el registro en la tabla con el 
                // valor de SettingName especificado y defaultValue es null.
                scalarResult = command.ExecuteScalar();
            }
            catch (SqlException sqlEx)
            {
                string errorMessage = String.Format("La tabla de valores de configuración de la base de datos '{0}' en la instancia de SQLServer '{1}' no estuvo disponible al intentar escribir el valor de la variable de configuración '{2}'.", connection.Database, connection.DataSource, settingName);
                throw new PlatformException(errorMessage, sqlEx);
            }
            catch (Exception ex)
            {

                throw new PlatformException(ex);
            }
            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                }
            }

            if (scalarResult == null)
            {
                string errorMessage = String.Format("No existe el parámetro de configuración '{0}' en la base de datos '{1}' en la instancia de SQL Server '{2}'.", settingName, connection.Database, connection.DataSource);
                throw new PlatformException(errorMessage);
            }
            else
            {
                settingValue = Convert.ToString(scalarResult);
            }

            return settingValue;
        }

        private void UpdateOrCreateSetting(string settingName, string settingValue)
        {
            SqlConnection connection = new SqlConnection(LoadConectionString());
            SqlCommand command = new SqlCommand("ApplicationSettings_SaveSetting", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@SettingName", settingName));
            command.Parameters.Add(new SqlParameter("@SettingValue", settingValue));

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (SqlException sqlEx)
            {
                string errorMessage = String.Format("La tabla de valores de configuración de la base de datos '{0}' en la instancia de SQLServer '{1}' no estuvo disponible al intentar escribir el valor de la variable de configuración '{2}'.", connection.Database, connection.DataSource, settingName);
                throw new PlatformException(errorMessage, sqlEx);
            }
            catch (Exception ex)
            {
                throw new PlatformException(ex);
            }
            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                }
            }
        }

        private static string LoadConectionString()
        {
            if (_configDatabase == null)
            {
                _configDatabase = RegistryHelper.ReadStringValueFromProductRegistryKey("ConfigurationDatabase");
            }

            return _configDatabase;
        }

        #endregion
    }
}
