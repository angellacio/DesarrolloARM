
//@(#)SCADE2(W:SKDN08221CO6:SAT.DyP.Util:CachingService:0:21/May/2008[SAT.DyP.Util:1.0:21/May/2008])
	
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using System.Diagnostics;
using SAT.DyP.Util.Configuration;

namespace SAT.DyP.Util.Caching
{
    /// <summary>
    /// Clase de uso general que provee servicios de cache en memoria.
    /// </summary>
    public static class CachingService
    {
        private static CacheManager _cacheManager;
        private static object _cacheManagerMutex = new object();

        internal static CacheManager CacheManager
        {
            get 
            {
                if (CachingService._cacheManager == null)
                {
                    lock (_cacheManagerMutex)
                    {
                        if (CachingService._cacheManager == null)
                        {
                            InitializeCacheManager();
                        }
                    }
                }
                return CachingService._cacheManager;
            }
            set 
            { 
                CachingService._cacheManager = value; 
            }
        }

        private static void InitializeCacheManager()
        {
            // Crear cache manager a partir de la configuración del enterprise library
            // Si no se ha inicializado enterprise library a partir de su archivo de configuración, 
            // esto no funcionará.
            try
            {
                CacheManagerFactory factory = new CacheManagerFactory(ConfigurationManager.EnterpriseLibraryConfigurationSource);
                _cacheManager = factory.CreateDefault();
            }
            catch (Exception ex)
            {
                string errorMessage = String.Format(
                    "No se pudo inicializar subsistema de cache. La causa mas probable es un error de configuración en el archivo .config de Enterprise Library.");

                throw new  SAT.DyP.Util.Types.PlatformException(errorMessage, ex);
            }
        }

        public static object GetItem(string itemKey)
        {
            return CacheManager.GetData(itemKey);
        }

        /// <summary>
        /// Guarda un elemento en cache. Si el elemento existe, será actualizado.
        /// </summary>
        /// <param name="itemKey">El nombre del elemento</param>
        /// <param name="itemValue">El valor a almacenar</param>
        public static void AddItem(string itemKey, object itemValue)
        {
            CacheManager.Add(itemKey, itemValue);
        }

        /// <summary>
        /// Devuelve true si el elemento existe en cache.
        /// </summary>
        /// <param name="itemKey"></param>
        /// <returns></returns>
        public static bool Contains(string itemKey)
        {
            return CacheManager.Contains(itemKey);
        }
    }
}
