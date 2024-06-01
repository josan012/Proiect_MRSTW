using handmadeShop.Web.Filters;
using System.Web;
using System.Web.Mvc;

namespace handmadeShop.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        
        }
    }
}
