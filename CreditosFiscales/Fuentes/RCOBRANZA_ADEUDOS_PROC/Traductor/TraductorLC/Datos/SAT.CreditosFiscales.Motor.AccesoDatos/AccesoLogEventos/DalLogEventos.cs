
//@(#)CREDITOSFISCALES(W:71950:SAT.CreditosFiscales.Motor.AccesoDatos.AccesoLogEventos:SAT.CreditosFiscales.Motor.AccesoDatos.AccesoLogEventos.DalLogEventos:1:12/07/2012[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Sat.CreditosFiscales.Motor.Entidades.AccesoLogEventos;

namespace SAT.CreditosFiscales.Motor.AccesoDatos.AccesoLogEventos
{
    /// <summary>
    /// Clase de acceso a datos al log de eventos.
    /// </summary>
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

        public List<LogEvento> BuscarEventos(string porTicket, string porFechaInicio, string porFechaFin)
        {
            var listaEventos = new List<LogEvento>();
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("pLogEventosBuscar");
            db.AddInParameter(cmd, "@pTicket", System.Data.DbType.String, porTicket);
            db.AddInParameter(cmd, "@pFechaInicio", System.Data.DbType.String, porFechaInicio);
            db.AddInParameter(cmd, "@pFechaFin", System.Data.DbType.String, porFechaFin);

            using (var dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    LogEvento evento = new LogEvento();
                    evento.Id = new Guid(dr["Id"].ToString());
                    evento.IdTipoEvento = Convert.ToInt16(dr["IdTipoEvento"]);
                    evento.Mensaje = dr["Mensaje"].ToString();
                    evento.Aplicacion = dr["Aplicacion"].ToString();
                    evento.Evento = dr["Evento"].ToString();
                    evento.FechaOrigen = Convert.ToDateTime(dr["FechaOrigen"]);

                    listaEventos.Add(evento);
                }
            }


            return listaEventos;
        }


        public bool VerificaAcceso(string usuario, string contraseña)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("pVerificaAcceso");
            db.AddInParameter(cmd, "@pUsuario", DbType.String, usuario);
            db.AddInParameter(cmd, "@pContrasena", DbType.String, contraseña);
            return (int)db.ExecuteScalar(cmd) == 1;
        }
    }
}
