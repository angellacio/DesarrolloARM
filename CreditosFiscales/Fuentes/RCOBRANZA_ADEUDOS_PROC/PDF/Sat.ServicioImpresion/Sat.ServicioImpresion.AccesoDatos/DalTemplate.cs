//@(#)CREDITOSFISCALES(W:71950:Sat.ServicioImpresion.AccesoDatos:Sat.ServicioImpresion.AccesoDatos.DalTemplate:1:12/07/2013[Assembly:1.0:12/07/2013])

// Referencias de sistema.
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Sat.ServicioImpresion.AccesoDatos
{
	/// <summary>
	/// Clase con los métodos requeridos para la obtención de la información de la base de datos
	/// </summary>
	public class DalTemplate
	{
		/// <summary>
		/// Método para obtener el archivo xsd para la validación
		/// </summary>
		/// <param name="piIdDocumento">Identificador único del documento o template.</param>
		/// <returns>Cadena de caracter en formato XSD para la validación de documentos.</returns>
		public string ObtieneTemplateValidacion(int piIdDocumento)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand cmd = db.GetStoredProcCommand("pObtenerTemplateValidacion");
			db.AddInParameter(cmd, "@IdDocumento", System.Data.DbType.Int16, piIdDocumento);
			return db.ExecuteScalar(cmd).ToString();
		}

		/// <summary>
		/// Método para obtener un template definido
		/// </summary>
		/// <param name="idDocumento">identificador del template</param>
		/// <param name="nombre">Nombre o tipo de template</param>
		/// <param name="version">Versión de template</param>
		/// <returns></returns>
		public string ConsultaTemplate(int idDocumento, string nombre, int version)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand cmd = db.GetStoredProcCommand("pObtenerTemplate");
			db.AddInParameter(cmd, "@IdDocumento", System.Data.DbType.Int16, idDocumento);
			db.AddInParameter(cmd, "@nombreTemplate", System.Data.DbType.String, nombre);
			db.AddInParameter(cmd, "@version", System.Data.DbType.Int32, version);
			return db.ExecuteScalar(cmd).ToString();
		}
	}
}