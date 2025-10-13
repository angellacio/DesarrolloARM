// Referencias de sistema.
using System;
using System.Configuration;
using System.Runtime.Caching;
using System.Collections.Generic;

// Referencias personalizadas.
using SAT.CreditosFiscales.Motor.Entidades.Catalogos;
using SAT.CreditosFiscales.Motor.Entidades.DatosProcesamiento;
using SAT.CreditosFiscales.Motor.AccesoDatos.Procesamiento;

namespace SAT.CreditosFiscales.Motor.Negocio.DatosCache
{
	/// <summary>
	/// 
	/// </summary>
	public class GeneraDatosCache
	{
		public static List<CreditosFiscalesConcepto> conceptosEquivalencia { get; set; }
		public static List<CatReglasEquivalencia> reglasEquivalencia { get; set; }
		public static List<CatPeriodicidadEquivalencia> periodicidadEquivalencia { get; set; }
		public static List<CatPeriodoEquivalencia> periodoEquivalencia { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public static void GeneraCache()
		{
			ObjectCache cache = MemoryCache.Default;
			var policy = new CacheItemPolicy();
			policy.AbsoluteExpiration = DateTimeOffset.Now.AddMilliseconds(Convert.ToInt32(ConfigurationManager.AppSettings["Milisegundos"]));

			conceptosEquivalencia = cache["conceptosEquivalencia"] as List<CreditosFiscalesConcepto>;
			reglasEquivalencia = cache["reglasEquivalencia"] as List<CatReglasEquivalencia>;
			periodicidadEquivalencia = cache["periodicidadEquivalencia"] as List<CatPeriodicidadEquivalencia>;
			periodoEquivalencia = cache["periodoEquivalencia"] as List<CatPeriodoEquivalencia>;

			if (conceptosEquivalencia == null)
			{
				// Conceptos Equivalencia
				conceptosEquivalencia = DalConceptosEquivalencia.ObtenerConceptoEquivalencia();
				cache.Add("conceptosEquivalencia", conceptosEquivalencia, policy);
			}

			if (reglasEquivalencia == null)
			{
				reglasEquivalencia = DalConceptosEquivalencia.ObtenerReglasEquivalencia();
				cache.Add("reglasEquivalencia", reglasEquivalencia, policy);
			}

			if (periodicidadEquivalencia == null)
			{
				periodicidadEquivalencia = DalConceptosEquivalencia.ObtenerPeriodicidadEquivalencia();
				cache.Add("periodicidadEquivalencia", periodicidadEquivalencia, policy);
			}

			if (periodoEquivalencia == null)
			{
				periodoEquivalencia = DalConceptosEquivalencia.ObtenerPeriodoEquivalencia();
				cache.Add("periodoEquivalencia", periodoEquivalencia, policy);
			}
		}
	}
}