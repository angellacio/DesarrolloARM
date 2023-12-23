
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Presentacion.Herramientas:Sat.CreditosFiscales.Presentacion.Herramientas.MetodosComunesPortal:1:12/07/2013[Assembly:1.0:12/07/2013])

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Caching;
using Sat.CreditosFiscales.Comunes.Entidades.Catalogos;


using Microsoft.Practices.EnterpriseLibrary.Caching;
using System.Configuration;
namespace Sat.CreditosFiscales.Presentacion.Herramientas
{
    public class MetodosComunesPortal
    {
        private const string CacheCatAutoridad = "CacheCatAutoridad";
        private const string CacheCatMarca = "CacheCatMarca";
        private const string CacheCatAlr = "CacheCatAlr";
        private static object _lock = new object();

        private const string CacheCatAplicacion = "CacheCatAplicacion";
        private const string CacheCatTipoDocumento = "CacheCatTipoDocumento";
        private const string CacheCatPaso = "CacheCatPaso";

        public static void CargaCatalogos()
        {
            lock (_lock)
            {
                using (ClienteServicioCatalogos cliente = new ClienteServicioCatalogos())
                {
                    var c = CacheFactory.GetCacheManager("CreditosFiscalesCacheManager");

                    if (c[CacheCatAutoridad] == null)
                    {
                        var listaCatAutoridad = cliente.CreateChannel().ObtenerCatalogoAutoridad();
                        c.Add(CacheCatAutoridad, listaCatAutoridad);
                    }
                    //if (c[CacheCatMarca] == null)
                    //{
                    //    var listaCatMarca = cliente.CreateChannel().ObtenerCatalogoMarca();
                    //    c.Add(CacheCatMarca, listaCatMarca);
                    //}
                    if (c[CacheCatAlr] == null)
                    {
                        var listaCatAlr = cliente.CreateChannel().ObtenerCatalogoAlr();
                        c.Add(CacheCatAlr, listaCatAlr);
                    }
                }
            }
        }

        public static string ObtieneDescripcionAutoridadDeCache(int idAutoridad)
        {
            lock (_lock)
            {
                string descripcion = string.Empty;
                var c = CacheFactory.GetCacheManager("CreditosFiscalesCacheManager");
                if (c[CacheCatAutoridad] != null)
                {
                    List<Autoridad> listaAutoridad = (List<Autoridad>)c[CacheCatAutoridad];
                    var autoridad = listaAutoridad.Where(a => a.IdAutoridad.Equals(idAutoridad)).FirstOrDefault();
                    descripcion = autoridad.Descripcion;
                }
                return descripcion;
            }
        }

        public static CatMarca ObtieneMarcaDeCache(string cveMarca)
        {
            lock (_lock)
            {
                CatMarca marca = new CatMarca();
                var c = CacheFactory.GetCacheManager("CreditosFiscalesCacheManager");
                if (c[CacheCatMarca] != null)
                {
                    List<CatMarca> listaMarca = (List<CatMarca>)c[CacheCatMarca];
                    marca = listaMarca.Where(m => m.CveMarca.Equals(cveMarca.ToUpper())).FirstOrDefault();
                }

                return marca;
            }
        }

        public static List<ALR> ObtieneCatalogoAlr()
        {
            lock (_lock)
            {
                var c = CacheFactory.GetCacheManager("CreditosFiscalesCacheManager");
                List<ALR> listaAlr = new List<ALR>();
                if (c[CacheCatAlr] != null)
                {
                    listaAlr = (List<ALR>)c[CacheCatAlr];

                }
                return listaAlr;
            }
        }

        public static List<Autoridad> ObtieneCatalogoAutoridad()
        {
            lock (_lock)
            {
                var c = CacheFactory.GetCacheManager("CreditosFiscalesCacheManager");
                List<Autoridad> listaAutoridad = new List<Autoridad>();
                if (c[CacheCatAutoridad] != null)
                {
                    listaAutoridad = (List<Autoridad>)c[CacheCatAutoridad];

                }
                return listaAutoridad;
            }
        }

        public static ALR ObtenerAlrXId(byte idAlr)
        {

            lock (_lock)
            {
                var c = CacheFactory.GetCacheManager("CreditosFiscalesCacheManager");
                ALR alr = new ALR();
                if (c[CacheCatAlr] != null)
                {
                    List<ALR> catalogoAlr = (List<ALR>)c[CacheCatAlr];

                    alr = catalogoAlr.Where(a => a.IdAlr.Equals(idAlr)).FirstOrDefault();

                }
                return alr;
            }
        }

        public static void EscribeLogHeader(string log)
        {
            string archivo = ConfigurationManager.AppSettings["ArchivoLogHeader"].ToString();
            bool escribir = bool.Parse(ConfigurationManager.AppSettings["EscribirLogHeader"].ToString());

            if (escribir)
            {
                System.IO.StreamWriter sw = System.IO.File.AppendText(archivo);
                try
                {
                    sw.WriteLine("--------------------------------------");
                    sw.WriteLine(log);
                    sw.WriteLine("--------------------------------------");
                }
                finally
                {
                    sw.Close();
                }
            }
        }

        #region Log Traductor
        public static void CargaCatalogosLogTraductor()
        {
            lock (_lock)
            {
                using (var log = new ClienteServicioConsultaEventos())
                {
                    var c = CacheFactory.GetCacheManager("CreditosFiscalesCacheManager");
                    if (c[CacheCatAplicacion] == null)
                    {
                        var listaAplicacion = log.CreateChannel().ObtenerCatalogo(1);
                        c.Add(CacheCatAplicacion, listaAplicacion);
                    }

                    if (c[CacheCatTipoDocumento] == null)
                    {
                        var listaTipoDocumento = log.CreateChannel().ObtenerCatalogo(2);
                        c.Add(CacheCatTipoDocumento, listaTipoDocumento);
                        listaTipoDocumento.Add(-1, "Ver Todos");
                    }

                    if (c[CacheCatPaso] == null)
                    {
                        var listaPasos = log.CreateChannel().ObtenerCatalogo(3);
                        c.Add(CacheCatPaso, listaPasos);
                        listaPasos.Add(-1, "Ver Todos");
                    }
                }

            }
        }

        public static Dictionary<int, string> ObtieneCatalogoAplicaciones()
        {
            lock (_lock)
            {
                var c = CacheFactory.GetCacheManager("CreditosFiscalesCacheManager");
                Dictionary<int, string> listaAplicacion = new Dictionary<int, string>();
                if (c[CacheCatAplicacion] != null)
                {
                    listaAplicacion = (Dictionary<int,string>)c[CacheCatAplicacion];
                }

                return listaAplicacion;
            }
        }

        public static Dictionary<int, string> ObtieneCatalogoTipoDocumento()
        {
            lock (_lock)
            {
                var c = CacheFactory.GetCacheManager("CreditosFiscalesCacheManager");
                Dictionary<int, string> listaTipoDocumento = new Dictionary<int, string>();
                if (c[CacheCatTipoDocumento] != null)
                {
                    listaTipoDocumento = (Dictionary<int, string>)c[CacheCatTipoDocumento];
                }

                return listaTipoDocumento;
            }
        }

        public static Dictionary<int, string> ObtieneCatalogoPasos()
        {
            lock (_lock)
            {
                var c = CacheFactory.GetCacheManager("CreditosFiscalesCacheManager");
                Dictionary<int, string> listaPasos = new Dictionary<int, string>();
                if (c[CacheCatPaso] != null)
                {
                    listaPasos = (Dictionary<int, string>)c[CacheCatPaso];
                }

                return listaPasos;
            }
        }

        #endregion
    }
}