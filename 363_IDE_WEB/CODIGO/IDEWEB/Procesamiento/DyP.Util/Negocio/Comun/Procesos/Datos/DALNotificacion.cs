
//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Negocio.Comun.Procesos:DALNotificacion:0:21/Mayo/2008[SAT.DyP.Negocio.Comun.Procesos:1.1:21/Mayo/2008])
	
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
  /// Capa de acceso a datos para notificación
  /// </summary>
  internal class DALNotificacion
  {
    public string ObtenerTextoNotificacion(int idMensaje, string entidadNotificacion)
    {
      string _result = string.Empty;
      string _entidad = DeterminarEntidad(entidadNotificacion);
      string _sql = "sp_DALNotificacion_Obtener_Texto_Notificacion";

      IDataParameter[] _parameters ={ new SqlParameter("@Tabla", SqlDbType.VarChar), 
                                      new SqlParameter("@Mensaje", SqlDbType.VarChar) };
      _parameters[0].Value = _entidad;
      _parameters[1].Value = idMensaje;

      DataAccessHelper _helper = null;

      try
      {
        _helper = new DataAccessHelper(DeterminarConexion(entidadNotificacion), DataProviderType.SqlServer);
        object _contenido = _helper.ExecuteScalar(_sql, CommandType.StoredProcedure, _parameters);
        if (_contenido != null)
          _result = _contenido.ToString();
      }
      catch (Exception ex)
      {
        throw new PlatformException(string.Format("Error al obtener mensaje de notificación '{0}' para la entidad '{1}'", idMensaje, entidadNotificacion), ex);
      }

      return _result;
    }

    private string DeterminarEntidad(string entidadNotificacion)
    {
      string _resultado = string.Empty;

      string[] _data = entidadNotificacion.Split(new char[] { '_' });

      return _data[1];
    }

    private string DeterminarConexion(string entidadNotificacion)
    {
      string _resultado = string.Empty;

      switch (entidadNotificacion)
      {
        case Constantes.MSG_NOTIFY_ANUAL:
          _resultado = newConfigurationConstants.DATABASE_ANUALES;
          break;

        case Constantes.MSG_NOTIFY_DIOT:
          _resultado = newConfigurationConstants.DATABASE_DOT;
          break;
      }

      return _resultado;
    }
  }
}