//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Util.Data:DataAccessHelper:0:21/May/2008[SAT.DyP.Util.Data:1.0:21/May/2008])	
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using SAT.DyP.Util.Configuration;
using System.Data.Odbc;
using SAT.DyP.Util.Types;
using System.Data.SqlClient;


namespace SAT.DyP.Util.Data
{
    /// <summary>
    /// DataAccess Helper
    /// </summary>
    public class DataAccessHelper
    {
        private DbConnection _connection;
        private string _dbKey;
        private DataProviderType _providerType;
        private string _keyForm;

        public DataAccessHelper(string databaseKeyConfig, DataProviderType provider)
        {


            _dbKey = databaseKeyConfig;
            _providerType = provider;

            CreateConnection();
        }

        public DataAccessHelper(string databaseKeyConfig, DataProviderType provider, string form)
        {


            _dbKey = databaseKeyConfig;
            _providerType = provider;
            _keyForm = form;

            CreateConnectionCfg();
        }


        /// <summary>
        /// Obtain Connection Object
        /// </summary>
        private void CreateConnectionCfg()
        {
            string _cnnstring = GetConfiguration(_dbKey, _keyForm);

            if (_providerType != DataProviderType.Informix)
            {
                DbProviderFactory _factory = DbProviderFactories.GetFactory(GetProviderName());
                _connection = _factory.CreateConnection();
            }
            else
            {
                _connection = new OdbcConnection();
            }

            _connection.ConnectionString = _cnnstring;
        }


        public DataAccessHelper(DbConnection connection)
        {

            _connection = connection;
        }

        public DbConnection GetConnection()
        {
            return _connection;
        }
        /// <summary>
        /// Read value from ApplicatonSettings
        /// </summary>
        /// <param name="key">Key configuration</param>
        /// <returns></returns>
        private string GetConfiguration(string key)
        {
            return ConfigurationManager.ApplicationSettings.ReadSetting(key);
        }

        private string GetConfiguration(string key, string form)
        {
            return ConfigurationManager.ApplicationSettings.ReadSettingCfg(key, form);
        }



        /// <summary>
        /// Obtain Connection Object
        /// </summary>
        private void CreateConnection()
        {
            string _cnnstring = GetConfiguration(_dbKey);

            if (_providerType != DataProviderType.Informix)
            {
                DbProviderFactory _factory = DbProviderFactories.GetFactory(GetProviderName());
                _connection = _factory.CreateConnection();
            }
            else
            {
                _connection = new OdbcConnection();
            }

            _connection.ConnectionString = _cnnstring;
        }

        private string GetProviderName()
        {
            string _providerString = "System.Data.SqlClient";

            switch (_providerType)
            {
                case DataProviderType.Informix:
                    _providerString = "IBM.Data.Informix";
                    break;
                case DataProviderType.Oracle:
                    _providerString = "System.Data.OracleClient";
                    break;
            }

            return _providerString;
        }

        /// <summary>
        /// Close current open connection
        /// </summary>
        public void CloseConnection()
        {
            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
        }

        /// <summary>
        /// Create and configure SqlCommand object
        /// </summary>
        /// <param name="commandText">Command Text</param>
        /// <param name="commandType">Command Type</param>
        /// <param name="parameters">Parameter List</param>
        /// <returns>SqlCommand</returns>
        private IDbCommand PrepareCommand(string commandText, CommandType commandType, IDataParameter[] parameters)
        {
            IDbCommand _command = _connection.CreateCommand();
            _command.CommandText = commandText;
            _command.CommandType = commandType;

            if (parameters != null)
            {
                foreach (IDataParameter _p in parameters)
                    _command.Parameters.Add(_p);
            }
            return _command;
        }

        /// <summary>
        /// Execute SQL sentence and return SqlDataReader
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <param name="parameters">Parameter List</param>
        /// <returns>SqlDataReader</returns>
        public IDataReader ExecuteSQL(string sql, IDataParameter[] parameters)
        {
            IDataReader _reader = null;

            try
            {
                _connection.Open();
                IDbCommand _command = PrepareCommand(sql, CommandType.Text, parameters);
                _reader = _command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                throw new PlatformException(ex);
            }

            return _reader;
        }

        public IDataReader ExecuteSQL(string sql, CommandType type, IDataParameter[] parameters)
        {
            IDataReader _reader = null;

            try
            {
                _connection.Open();
                IDbCommand _command = PrepareCommand(sql, type, parameters);
                _reader = _command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                throw new PlatformException(ex);
            }

            return _reader;
        }

        public object ExecuteScalar(string sql, IDataParameter[] parameters)
        {
            object _result = null;

            try
            {
                _connection.Open();
                IDbCommand _command = PrepareCommand(sql, CommandType.Text, parameters);
                _result = _command.ExecuteScalar();

                _command.Dispose();
                _command = null;
            }
            catch (Exception ex)
            {
                throw new PlatformException(ex);
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                    _connection.Close();
            }

            return _result;
        }

        public object ExecuteScalar(string sql, CommandType type, IDataParameter[] parameters)
        {
            object _result = null;

            try
            {
                _connection.Open();
                IDbCommand _command = PrepareCommand(sql, type, parameters);
                _result = _command.ExecuteScalar();

                _command.Dispose();
                _command = null;
            }
            catch (Exception ex)
            {
                throw new PlatformException(ex);
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                    _connection.Close();
            }

            return _result;
        }

        /// <summary>
        /// Execute store procedure in db
        /// </summary>
        /// <param name="commandText">Command Text</param>
        /// <param name="parameters">Parameter List</param>
        /// <returns></returns>
        public object ExecuteStoreProcedure(string commandText, IDataParameter[] parameters)
        {
            object _returnValue = null;

            try
            {
                _connection.Open();

                IDbCommand _command = PrepareCommand(commandText, CommandType.StoredProcedure, parameters);

                _command.CommandTimeout = 0;
                _returnValue = _command.ExecuteScalar();

                _command.Dispose();
                _command = null;
            }
            catch (Exception ex)
            {
                throw new PlatformException(ex);
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                    _connection.Close();
            }

            return _returnValue;
        }


        /// <summary>
        /// Mejoras MS. DAC MAR 2011 
        /// </summary>
        /// <param name="commandText">Command Text</param>
        /// <param name="parameters">Parameter List</param>
        /// <returns></returns>
        public object ExecuteStoredProcedureWithTransaction(string commandText, IDataParameter[] parameters, DbTransaction transaction)
        {
            object _returnValue = null;

            try
            {

                IDbCommand _command = PrepareCommand(commandText, CommandType.StoredProcedure, parameters);
                _command.Transaction = transaction;

                _command.CommandTimeout = 0;
                _returnValue = _command.ExecuteScalar();

                _command.Dispose();
                _command = null;
            }
            catch (Exception ex)
            {
                throw new PlatformException(ex);
            }

            return _returnValue;
        }
        //FIN-Mejoras MS. DAC MAR 2011 

        /// <summary>
        /// Execute store procedure in db
        /// </summary>
        /// <param name="commandText">Command Text</param>
        /// <param name="parameters">Parameter List</param>
        /// <returns></returns>
        public IDataReader ExecuteReaderStoreProcedure(string commandText, IDataParameter[] parameters)
        {
            IDataReader reader = null;

            try
            {
                _connection.Open();

                IDbCommand _command = PrepareCommand(commandText, CommandType.StoredProcedure, parameters);

                _command.CommandTimeout = 0;
                reader = _command.ExecuteReader(CommandBehavior.CloseConnection);

                _command.Dispose();
                _command = null;
            }
            catch (Exception ex)
            {
                throw new PlatformException(ex);
            }
            return reader;
        }

        /// <summary>
        /// Execute SQL Statement
        /// </summary>
        /// <param name="sql">SQL Sentence</param>
        /// <param name="parameters">Parameter List</param>
        /// <returns>Records Affected</returns>
        public int ExecuteStatement(string sql, IDataParameter[] parameters)
        {
            int _recordsAffected = 0;

            try
            {
                _connection.Open();
                IDbCommand _command = PrepareCommand(sql, CommandType.Text, parameters);

                _recordsAffected = _command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new PlatformException(ex);
            }
            finally
            {
                _connection.Close();
            }

            return _recordsAffected;
        }

        public int ExecuteIfxStatement(OdbcCommand cmd)
        {
            int _recordsAffected = 0;

            try
            {
                _connection.Open();


                cmd.Connection = _connection as OdbcConnection;
                _recordsAffected = cmd.ExecuteNonQuery();

                cmd.Dispose();

            }
            catch (Exception ex)
            {
                throw new PlatformException(ex);
            }
            finally
            {
                _connection.Close();
            }

            return _recordsAffected;
        }

        public int ExecuteIfxStatement(OdbcCommand cmd, OdbcConnection cnn)
        {
            int _recordsAffected = 0;

            try
            {
                _recordsAffected = cmd.ExecuteNonQuery();
                cmd.Dispose();

            }
            catch (Exception ex)
            {
                throw new PlatformException(ex);
            }

            return _recordsAffected;
        }

        public int ExecuteIfxStatement(string sql, IDataParameter[] parameters)
        {
            int _recordsAffected = 0;

            try
            {
                _connection.Open();

                OdbcCommand _cmd = new OdbcCommand(sql, (OdbcConnection)_connection);
                _cmd.CommandType = CommandType.Text;
                _recordsAffected = _cmd.ExecuteNonQuery();

                _cmd.Dispose();

            }
            catch (Exception ex)
            {
                throw new PlatformException(ex);
            }
            finally
            {
                _connection.Close();
            }

            return _recordsAffected;
        }

        public int ExecuteStatement(string sql, CommandType type, IDataParameter[] parameters)
        {
            int _recordsAffected = 0;

            try
            {
                _connection.Open();
                IDbCommand _command = PrepareCommand(sql, type, parameters);

                _recordsAffected = _command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new PlatformException(ex);
            }
            finally
            {
                _connection.Close();
            }

            return _recordsAffected;
        }

        public int ExecuteStmtWithTransaction(string sql, CommandType type, IDataParameter[] parameters, DbTransaction transaction)
        {
            int _recordsAffected = 0;
            IDbCommand _command = null;
            try
            {
                using (_command = PrepareCommand(sql, type, parameters))
                {
                    _command.Transaction = transaction;
                    _recordsAffected = _recordsAffected = _command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new PlatformException(ex);
            }
            return _recordsAffected;
        }

        public OdbcDataReader ExecuteIfxReader(string spName, OdbcParameter[] parameters)
        {
            OdbcDataReader reader = null;
            OdbcCommand cmd = new OdbcCommand(spName, (OdbcConnection)_connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(parameters);

            try
            {
                _connection.Open();
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                throw new PlatformException(ex);
            }

            return reader;
        }

    }
}