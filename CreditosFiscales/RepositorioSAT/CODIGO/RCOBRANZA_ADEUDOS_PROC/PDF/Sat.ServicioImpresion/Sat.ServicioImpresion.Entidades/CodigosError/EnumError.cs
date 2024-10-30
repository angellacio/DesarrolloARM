//@(#)CREDITOSFISCALES(W:71950:Sat.ServicioImpresion.Entidades.CodigosError:Sat.ServicioImpresion.Entidades.CodigosError.EnumErroresImpresion:1:12/07/2013[Assembly:1.0:12/07/2013])

namespace Sat.ServicioImpresion.Entidades.CodigosError
{
	/// <summary>
	/// Enumeración para los diversos tipos de posibles errores
	/// </summary>
	public enum EnumErroresImpresion
	{
		/// <summary>
		/// Error generico
		/// </summary>
		ErrorGenerico = 1000,
		/// <summary>
		/// Error de validación del xml
		/// </summary>
		ErrorXMLInvalido = 1001,
		/// <summary>
		/// Error en la generación de archivo
		/// </summary>
		ErrorGenerarArchivo = 1002,
		/// <summary>
		/// Error al validar la existencia del directoro temporal
		/// </summary>
		ErrorDirectorioNoExiste = 1003,
		/// <summary>
		/// Error al guardar el archivo temporal
		/// </summary>
		ErrorGuardarArchivo = 1004
	}
}