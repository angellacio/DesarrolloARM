
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Datos.AccesoDatos.Catalogos:Sat.CreditosFiscales.Datos.AccesoDatos.Catalogos.DalApplicationSettings:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Sat.CreditosFiscales.Comunes.Entidades.Catalogos;
using Sat.CreditosFiscales.Comunes.Herramientas;

namespace Sat.CreditosFiscales.Datos.AccesoDatos.Catalogos
{
    public class DalApplicationSettings
    {
        //public object ActualizaSetting(string settingName, string settingValue)
        //{
        //    Database db = DatabaseFactory.CreateDatabase();
        //    DbCommand cmd = db.GetStoredProcCommand("pApplicationSettingsConsulta");
        //    db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(ApplicationSetting.Campos.SettingName), DbType.String, settingName);
        //    return db.ExecuteScalar(cmd);
        //}

        //public object ActualizaSettingHash(string settingName, string settingValue)
        //{
        //    Database db = DatabaseFactory.CreateDatabase();
        //    DbCommand cmd = db.GetStoredProcCommand("pApplicationSettingsConsulta");
        //    db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(ApplicationSetting.Campos.SettingName), DbType.String, settingName);
        //    return db.ExecuteScalar(cmd);
        //}

        public object RecuperaConfiguracion(string settingName)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("pApplicationSettingsConsulta");
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(ApplicationSetting.Campos.SettingName), DbType.String, settingName);
            return db.ExecuteScalar(cmd);
            
        }

        public List<ApplicationSetting> RecuperaGrupoConfiguracion(string settingName)
        {

            var lista = new List<ApplicationSetting>();

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("pApplicationSettingsConsultaGrupo");
            db.AddInParameter(cmd, MetodosComunes.ObtieneNombreParametroSql(ApplicationSetting.Campos.SettingName), DbType.String, settingName);
            using (var dataReader = db.ExecuteReader(cmd))
            {
                while(dataReader.Read())
                {
                lista.Add(new ApplicationSetting()
                    {
                        SettingName = (string)dataReader[ApplicationSetting.Campos.SettingName.ToString()],
                        SettingValue = (string)dataReader[ApplicationSetting.Campos.SettingValue.ToString()]
                    });
                }
            }
            return lista;
        }
    }
}
