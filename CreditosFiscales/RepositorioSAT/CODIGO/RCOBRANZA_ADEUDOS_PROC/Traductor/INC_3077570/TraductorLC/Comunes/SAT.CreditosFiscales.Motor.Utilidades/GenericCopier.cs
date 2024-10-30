/*
 * Actualizó: Mario Escarpulli
 * Fecha: 10/Jun/2019
*/

// Referencias de sistema.
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SAT.CreditosFiscales.Motor.Utilidades
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public static class GenericCopier<T>
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="objectToCopy"></param>
		/// <returns></returns>
		public static T DeepCopy(object objectToCopy)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				var binaryFormatter = new BinaryFormatter();
				binaryFormatter.Serialize(memoryStream, objectToCopy);
				memoryStream.Seek(0, SeekOrigin.Begin);
				return (T) binaryFormatter.Deserialize(memoryStream);
			}
		}
	}
}