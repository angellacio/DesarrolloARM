
//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Negocio.Comun.Procesos:DALMensaje:0:21/Mayo/2008[Assembly:1.1:21/Mayo/2008])
	
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using SAT.DyP.Util.Data;
using SAT.DyP.Util.Configuration;
using SAT.DyP.Util.Types;

namespace SAT.DyP.Negocio.Comun.Procesos.Datos
{
  /// <summary>
  /// Capa de acceso a datos para mensajes
  /// </summary>
  internal class DALMensaje
  {
    public string ObtenerMensajeComun(string constanteMensaje)
    {
      string _result = string.Empty;
      string _sql = "SCADENET_ObtenerMensajeComun";
      IDataParameter[] _parameters ={ new SqlParameter("@Mensaje", SqlDbType.VarChar) };
      _parameters[0].Value = constanteMensaje;

      DataAccessHelper _helper = null;

      try
      {
        _helper = new DataAccessHelper(newConfigurationConstants.DATABASE_SCADE_NET, DataProviderType.SqlServer);
        object _contenido = _helper.ExecuteScalar(_sql, CommandType.StoredProcedure, _parameters);
        if (_contenido != null)
          _result = _contenido.ToString();
      }
      catch (Exception ex)
      {
        throw new PlatformException(string.Format("Error al intentar obtener Mensaje '{0}'", constanteMensaje), ex);
      }

      return _result;
    }

    public string ObtenerMensaje(int idMensaje, string entidadMensajes)
    {
      string _result = string.Empty;
      string _entidad = DeterminarEntidad(entidadMensajes);

      string _sql = "sp_DALMensaje_Obtiene_Mensaje";

      IDataParameter[] _parameters ={ new SqlParameter("@Tabla", SqlDbType.VarChar),
                                      new SqlParameter("@Mensaje", SqlDbType.VarChar) };
      _parameters[0].Value = _entidad;
      _parameters[1].Value = idMensaje;

      DataAccessHelper _helper = new DataAccessHelper(DeterminarConexion(entidadMensajes), DataProviderType.SqlServer);

      try
      {
        object _contenido = _helper.ExecuteScalar(_sql, CommandType.StoredProcedure, _parameters);
        if (_contenido != null)
          _result = _contenido.ToString();
      }
      catch (Exception ex)
      {
        throw new PlatformException(string.Format("Error al intentar obtener Mensaje ID'{0}', Entidad '{1}'", idMensaje, entidadMensajes), ex);
      }

      return _result;
    }

    private string DeterminarEntidad(string entidadMensajes)
    {
      string _resultado = string.Empty;

      string[] _data = entidadMensajes.Split(new char[] { '_' });

      return _data[1];
    }

    private string DeterminarConexion(string entidadMensajes)
    {
      string _resultado = string.Empty;

      switch (entidadMensajes)
      {
        case Constantes.MSG_ENTIDAD_ANUAL:
          _resultado = newConfigurationConstants.DATABASE_ANUALES;
          break;

        case Constantes.MSG_ENTIDAD_CEROS:
          _resultado = newConfigurationConstants.DATABASE_AVISOSCERO;
          break;

        case Constantes.MSG_ENTIDAD_DIOT:
          _resultado = newConfigurationConstants.DATABASE_DOT;
          break;
      }

      return _resultado;
    }
  }
}