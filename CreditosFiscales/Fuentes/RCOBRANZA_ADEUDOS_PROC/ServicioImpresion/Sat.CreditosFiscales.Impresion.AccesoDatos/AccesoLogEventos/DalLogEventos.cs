//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Impresion.AccesoDatos.AccesoLogEventos:Sat.CreditosFiscales.Impresion.AccesoDatos.AccesoLogEventos.DalLogEventos:1:12/07/2013[Assembly:1.0:12/07/2013])

// Referencias de sistema.
using System;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

// Referencias personalizadas.
using Sat.CreditosFiscales.Impresion.Entidades.AccesoLogEventos;

namespace Sat.CreditosFiscales.Impresion.AccesoDatos.AccesoLogEventos
{
	/// <summary>
	/// Clase para el manejo de los metodos en base de datos del log
	/// </summary>
	public class DalLogEventos
	{
		/// <summary>
		/// Guarda los eventos generación por alguna excepción
		/// </summary>
		/// <param name="aplicacion">Nombre de la aplicación</param>
		/// <param name="mensaje">Mensaje descriptivo de la excepción</param>
		/// <param name="idEvento">Identificador del evento</param>
		/// <param name="tipoEvento">Enumeración del tipo de evento<see cref="EnumTipoEvento"/>></param>
		/// <returns>Ticket con el que se registra la excepción</returns>
		public string GuardaEvento(string aplicacion, string mensaje, int idEvento, EnumTipoEvento tipoEvento)
		{
			Guid ticket = Guid.NewGuid();
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand cmd = db.GetStoredProcCommand("pLogEventosInserta");
			db.AddInParameter(cmd, "@aplicacion", System.Data.DbType.String, aplicacion);
			db.AddInParameter(cmd, "@mensaje", System.Data.DbType.String, mensaje);
			db.AddInParameter(cmd, "@evento", System.Data.DbType.Int64, idEvento);
			db.AddInParameter(cmd, "@idTipoEvento", System.Data.DbType.Int16, (int) tipoEvento);
			db.AddInParameter(cmd, "@ticket", System.Data.DbType.Guid, ticket);
			db.ExecuteNonQuery(cmd);
			return ticket.ToString();
		}
	}
}