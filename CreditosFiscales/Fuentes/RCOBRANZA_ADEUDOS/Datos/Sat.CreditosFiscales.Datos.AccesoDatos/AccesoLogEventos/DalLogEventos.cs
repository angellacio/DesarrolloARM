
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Datos.AccesoDatos.AccesoLogEventos:Sat.CreditosFiscales.Datos.AccesoDatos.AccesoLogEventos.DalLogEventos:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Sat.CreditosFiscales.Comunes.Entidades.AccesoLogEventos;


namespace Sat.CreditosFiscales.Datos.AccesoDatos.AccesoLogEventos
{
    public class DalLogEventos
    {

        public string GuardaEvento(string aplicacion, string mensaje, int idEvento, EnumTipoEvento tipoEvento)
        {
            Guid ticket = Guid.NewGuid();
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("pLogEventosInserta");
            db.AddInParameter(cmd, "@aplicacion", System.Data.DbType.String, aplicacion);
            db.AddInParameter(cmd, "@mensaje", System.Data.DbType.String, mensaje);
            db.AddInParameter(cmd, "@evento", System.Data.DbType.Int64, idEvento);
            db.AddInParameter(cmd, "@idTipoEvento", System.Data.DbType.Int16, (int)tipoEvento);
            db.AddInParameter(cmd, "@ticket", System.Data.DbType.Guid, ticket);
            db.ExecuteNonQuery(cmd);
            return ticket.ToString();
        }



    }
}
