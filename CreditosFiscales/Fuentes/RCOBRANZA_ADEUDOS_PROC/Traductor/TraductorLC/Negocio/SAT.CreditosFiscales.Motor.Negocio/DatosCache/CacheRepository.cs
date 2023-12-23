using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;
using SAT.CreditosFiscales.Motor.AccesoDatos.Procesamiento;
using SAT.CreditosFiscales.Motor.Entidades.Catalogos;
using SAT.CreditosFiscales.Motor.Entidades.DatosProcesamiento;

namespace SAT.CreditosFiscales.Motor.Negocio.DatosCache
{
    public static class CacheRepository
    {
        private static IEnumerable<CreditosFiscalesConcepto> ObtenerConceptosEquivalencia()
        {
            return DalConceptosEquivalencia.ObtenerConceptoEquivalencia();
        }

        private static IEnumerable<CatReglasEquivalencia> ObtenerReglasEquivalencia()
        {
            return DalConceptosEquivalencia.ObtenerReglasEquivalencia();
        }

        private static IEnumerable<CatPeriodicidadEquivalencia> ObtenerPeriodicidadEquivalencia()
        {
            return DalConceptosEquivalencia.ObtenerPeriodicidadEquivalencia();
        }

        private static IEnumerable<CatPeriodoEquivalencia> ObtenerPeriodoEquivalencia()
        {
            return DalConceptosEquivalencia.ObtenerPeriodoEquivalencia();
        }

        private static IEnumerable<CatTransaccion> ObtenerSIATTransaccionBaja()
        {
            return DalConceptosEquivalencia.ObtenerCatSIATTransaccionesBaja();
        }

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

        public static List<CreditosFiscalesConcepto> ConceptosEquivalencia
        {
            get { return HttpRuntime.Cache["conceptosEquivalencia"] as List<CreditosFiscalesConcepto>; }
        }

        public static List<CatReglasEquivalencia> ReglasEquivalencia
        {
            get { return HttpRuntime.Cache["reglasEquivalencia"] as List<CatReglasEquivalencia>; }
        }

        public static List<CatPeriodicidadEquivalencia> PeriodicidadEquivalencia
        {
            get { return HttpRuntime.Cache["periodicidadEquivalencia"] as List<CatPeriodicidadEquivalencia>; }
        }

        public static List<CatPeriodoEquivalencia> PeriodoEquivalencia
        {
            get { return HttpRuntime.Cache["periodoEquivalencia"] as List<CatPeriodoEquivalencia>; }
        }

        public static List<CatTransaccion> CatSIATTransaccionesBaja
        {
            get { return HttpRuntime.Cache["catSIATTransaccionesBaja"] as List<CatTransaccion>; }
        }
    }
}
