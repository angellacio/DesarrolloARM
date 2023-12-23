
//@(#)CREDITOSFISCALES(W:71950:Sat.CreditosFiscales.Presentacion.Portal:Sat.CreditosFiscales.Presentacion.Portal.FilterConfig:1:12/07/2013[Assembly:1.0:12/07/2013])

using System.Web;
using System.Web.Mvc;

namespace Sat.CreditosFiscales.Presentacion.Portal
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}