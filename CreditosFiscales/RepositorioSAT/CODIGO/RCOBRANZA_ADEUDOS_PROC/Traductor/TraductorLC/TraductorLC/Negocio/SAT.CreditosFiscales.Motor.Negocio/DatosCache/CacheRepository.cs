// Referencias de sistema.
using System;
using System.Web;
using System.Web.Caching;
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
	public static class CacheRepository
	{
		#region Propiedades
			/// <summary>
			/// 
			/// </summary>
			public static List<CreditosFiscalesConcepto> ConceptosEquivalencia
			{
				get { return HttpRuntime.Cache["conceptosEquivalencia"] as List<CreditosFiscalesConcepto>; }
			}

			/// <summary>
			/// 
			/// </summary>
			public static List<CatReglasEquivalencia> ReglasEquivalencia
			{
				get { return HttpRuntime.Cache["reglasEquivalencia"] as List<CatReglasEquivalencia>; }
			}

			/// <summary>
			/// 
			/// </summary>
			public static List<CatPeriodicidadEquivalencia> PeriodicidadEquivalencia
			{
				get { return HttpRuntime.Cache["periodicidadEquivalencia"] as List<CatPeriodicidadEquivalencia>; }
			}

			/// <summary>
			/// 
			/// </summary>
			public static List<CatPeriodoEquivalencia> PeriodoEquivalencia
			{
				get { return HttpRuntime.Cache["periodoEquivalencia"] as List<CatPeriodoEquivalencia>; }
			}

			/// <summary>
			/// 
			/// </summary>
			public static List<CatTransaccion> CatSIATTransaccionesBaja
			{
				get { return HttpRuntime.Cache["catSIATTransaccionesBaja"] as List<CatTransaccion>; }
			}
		#endregion


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		private static IEnumerable<CreditosFiscalesConcepto> ObtenerConceptosEquivalencia()
		{
			return DalConceptosEquivalencia.ObtenerConceptoEquivalencia();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		private static IEnumerable<CatReglasEquivalencia> ObtenerReglasEquivalencia()
		{
			return DalConceptosEquivalencia.ObtenerReglasEquivalencia();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		private static IEnumerable<CatPeriodicidadEquivalencia> ObtenerPeriodicidadEquivalencia()
		{
			return DalConceptosEquivalencia.ObtenerPeriodicidadEquivalencia();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		private static IEnumerable<CatPeriodoEquivalencia> ObtenerPeriodoEquivalencia()
		{
			return DalConceptosEquivalencia.ObtenerPeriodoEquivalencia();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		private static IEnumerable<CatTransaccion> ObtenerSIATTransaccionBaja()
		{
			return DalConceptosEquivalencia.ObtenerCatSIATTransaccionesBaja();
		}

		/// <summary>
		/// 
		/// </summary>
		public static void RecargaCache()
		{
			if (HttpRuntime.Cache["conceptosEquivalencia"] == null)
				HttpRuntime.Cache.Insert("conceptosEquivalencia", ObtenerConceptosEquivalencia(), null, DateTime.Now.AddHours(1), Cache.NoSlidingExpiration);

			if (HttpRuntime.Cache["reglasEquivalencia"] == null)
				HttpRuntime.Cache.Insert("reglasEquivalencia", ObtenerReglasEquivalencia(), null, DateTime.Now.AddHours(1), Cache.NoSlidingExpiration);

			if (HttpRuntime.Cache["periodicidadEquivalencia"] == null)
				HttpRuntime.Cache.Insert("periodicidadEquivalencia", ObtenerPeriodicidadEquivalencia(), null, DateTime.Now.AddHours(1), Cache.NoSlidingExpiration);

			if (HttpRuntime.Cache["periodoEquivalencia"] == null)
				HttpRuntime.Cache.Insert("periodoEquivalencia", ObtenerPeriodoEquivalencia(), null, DateTime.Now.AddHours(1), Cache.NoSlidingExpiration);

			if (HttpRuntime.Cache["catSIATTransaccionesBaja"] == null)
				HttpRuntime.Cache.Insert("catSIATTransaccionesBaja", ObtenerSIATTransaccionBaja(), null, DateTime.Now.AddHours(1), Cache.NoSlidingExpiration);
		}
	}
}
