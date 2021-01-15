//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Util.Processors.Mail.Data:DALTemplate:0:21/May/2008[ SAT.DyP.Util.Processors:1.0:21/May/2008])
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using SAT.DyP.Util.Configuration;
using SAT.DyP.Util.Data;

namespace SAT.DyP.Util.Processors.Mail.Data
{
  internal class DALTemplate
  {
    public string LoadTemplateContent(string templateCode)
    {
      string _result = string.Empty;

      string _sql = "sp_DALTemplate_Obtiene_Contenido";

      IDataParameter[] _parameters ={ new SqlParameter("@Template", SqlDbType.VarChar) };
      _parameters[0].Value = templateCode;

      DataAccessHelper _helper = new DataAccessHelper(newConfigurationConstants.DATABASE_SCADE_NET,
                                                      DataProviderType.SqlServer);

      try
      {
        object _content = _helper.ExecuteScalar(_sql, CommandType.StoredProcedure, _parameters);
        if (_content != null)
        {
          _result = _content.ToString();
        }
      }
      catch (Exception ex)
      {
        throw new SAT.DyP.Util.Types.PlatformException(ex.Message, ex);
      }

      return _result;
    }
  }
}