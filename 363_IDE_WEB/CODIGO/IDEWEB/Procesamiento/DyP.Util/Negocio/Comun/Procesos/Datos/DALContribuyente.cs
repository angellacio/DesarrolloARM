
//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Negocio.Comun.Procesos:DALContribuyente:0:21/Mayo/2008[SAT.DyP.Negocio.Comun.Procesos:1.1:21/Mayo/2008])
	
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using SAT.DyP.Util.Data;
using SAT.DyP.Util.Configuration;
using SAT.DyP.Negocio.Comun.Tipos;
using SAT.DyP.Util.Types;

namespace SAT.DyP.Negocio.Comun.Procesos.Datos
{
  /// <summary>
  /// Capa de acceso a datos para contribuyente
  /// </summary>
  internal class DALContribuyente
  {
    public Contribuyente Load(string RFC)
    {
      Contribuyente _resultado = null;

      string _sql = @"SCADENET_DatosContribuyente";

      IDataParameter[] _parameters ={ new SqlParameter("@RFC", SqlDbType.Char) };
      _parameters[0].Value = RFC;
      DataAccessHelper _helper = null;
      try
      {
        _helper = new DataAccessHelper(newConfigurationConstants.DATABASE_SPED, DataProviderType.SqlServer);
        IDataReader _reader = _helper.ExecuteSQL(_sql, CommandType.StoredProcedure, _parameters);

        if (!_reader.IsClosed)
        {
          _resultado = new Contribuyente();

          if (_reader.Read())
          {
            _resultado.Rfc = _reader.GetString(0);
            _resultado.RazonSocial = _reader.GetString(1);
            _resultado.TipoContribuyente = _reader.GetString(2);
            _resultado.Email = _reader.GetString(3);

            if (!_reader.IsDBNull(4))
              _resultado.RfcRepresentanteLegal = _reader.GetString(4);

            if (!_reader.IsDBNull(5))
              _resultado.RepresentanteLegal = _reader.GetString(5);

            _resultado.Clave = _reader.GetString(6);
            _resultado.EstadoClave = Convert.ToInt32(_reader.GetValue(7).ToString());
          }

          _reader.Close();
        }
        _reader.Dispose();
        _reader = null;
      }
      catch (Exception ex)
      {
        throw new SAT.DyP.Util.Types.PlatformException(string.Format("Error al obtener datos del contribuyente '{0}'", RFC), ex);
      }
      finally
      {
        if (_helper != null)
        {
          _helper.CloseConnection();
        }
      }

      return _resultado;
    }

    public bool ActualizaMailContribuyente(string rfc, string email)
    {

      string sql = @"sp_SSActualizarEMail";

      IDataParameter[] _parameters = { new SqlParameter("@RFC", SqlDbType.Char, 13),
                                       new SqlParameter("@EMail", SqlDbType.VarChar, 100)};
      _parameters[0].Value = rfc;
      _parameters[1].Value = email;
      _parameters[1].Direction = ParameterDirection.InputOutput;


      DataAccessHelper _helper = new DataAccessHelper(newConfigurationConstants.DATABASE_SPED, DataProviderType.SqlServer);
      bool result = false;

      try
      {
        object returnValue = _helper.ExecuteScalar(sql, CommandType.StoredProcedure, _parameters);

        if (email.Equals(_parameters[1].Value.ToString()))
          result = true;
      }
      catch (Exception ex)
      {
        throw new PlatformException(string.Format("Error al intentar actualizar el Correo Electrónico '{0}' del RFC '{1}'", email, rfc), ex);
      }

      return result;
    }

    /// <summary>
    /// <!--FSM @ PPMC 65622-->
    ///  Actualiza en BD el correo electrónico adicional de un contribuyente
    /// </summary>
    /// <param name="rfc">RFC del Contribuyente</param>
    /// <param name="email">Dirección de correro electrónico adicional</param>
    /// <returns>cierto/falso. Resultado de la operación</returns>
    public bool ActualizaMailAdicionalContribuyente(string rfc, string email)
    {

      string sql = @"sp_SSActualizarEMailAdicional";

      IDataParameter[] _parameters = { new SqlParameter("@RFC", SqlDbType.Char, 13),
                                       new SqlParameter("@EMail", SqlDbType.VarChar, 100)};
      _parameters[0].Value = rfc;
      _parameters[1].Value = email;

      DataAccessHelper _helper = new DataAccessHelper(newConfigurationConstants.DATABASE_SPED, DataProviderType.SqlServer);
      bool result = false;

      try
      {
        object returnValue = _helper.ExecuteScalar(sql, CommandType.StoredProcedure, _parameters);
        result = true;
      }
      catch (Exception ex)
      {
        throw new PlatformException(string.Format("Error al intentar actualizar el Correo Electrónico Adicional '{0}' del RFC '{1}'", email, rfc), ex);
      }

      return result;
    }

    /// <summary>
    /// <!--FSM @ PPMC 65622-->
    /// Elimina el correo electrónico adicional de la BD
    /// </summary>
    /// <param name="rfc">RFC del contribuyente</param>
    /// <returns>cierto/falso. Resultado de la operación</returns>
    public bool EliminaMailAdicionalContribuyente(string rfc)
    {
      string sql = @"sp_SSEliminarEMailAdicional";

      IDataParameter[] _parameters = { new SqlParameter("@RFC", SqlDbType.Char, 13) };
      _parameters[0].Value = rfc;

      DataAccessHelper _helper = new DataAccessHelper(newConfigurationConstants.DATABASE_SPED, DataProviderType.SqlServer);
      bool result = false;

      try
      {
        object returnValue = _helper.ExecuteScalar(sql, CommandType.StoredProcedure, _parameters);
        result = true;
      }
      catch (Exception ex)
      {
        throw new PlatformException(string.Format("Error al intentar eliminar Correo Electrónico Adicional del RFC '{0}'", rfc), ex);
      }

      return result;
    }

    /// <summary>
    /// <!--FSM @ PPMC 65622-->
    /// Registra los cambios realizados en correo(s) electrónicos del contribuyente.
    /// </summary>
    /// <param name="rfc">RFC del contribuyente</param>
    /// <param name="email">Correo electrónico obligatorio</param>
    /// <param name="email_adicional">Correo electrónico adicional</param>
    /// <returns>cierto/falso Resultado de la operación</returns>
    public bool RegistraLogCambios(string rfc, string email, string email_adicional)
    {
      string sql = @"sp_SSRegistraEMailLogCambios";

      IDataParameter[] _parameters = { new SqlParameter("@RFC", SqlDbType.Char, 13),
                                       new SqlParameter("@EMail", SqlDbType.VarChar, 100),
                                       new SqlParameter("@EMail_Adicional", SqlDbType.VarChar, 100)
                                     };
      _parameters[0].Value = rfc;
      _parameters[1].Value = email;
      if (!string.IsNullOrEmpty(email_adicional)) _parameters[2].Value = email_adicional;

      DataAccessHelper _helper = new DataAccessHelper(newConfigurationConstants.DATABASE_SPED, DataProviderType.SqlServer);
      bool result = false;
      try
      {
        object returnValue = _helper.ExecuteScalar(sql, CommandType.StoredProcedure, _parameters);
        result = true;
      }
      catch (Exception ex)
      {
        result = false;
        throw new PlatformException(string.Format("Error al intentar registrar en bitácora de cambios {2} Correo{3} Electrónico{3} '{0}' del RFC '{1}'",
                                    string.IsNullOrEmpty(email_adicional) ? email : email + ";" + email_adicional, rfc, string.IsNullOrEmpty(email_adicional) ? "el" : "los", string.IsNullOrEmpty(email_adicional) ? "" : "s"), ex);
      }

      return result;
    }
  }
}